using Autenticacion.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.ModelStore;
using SistemaVentas.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Infraestructura.Repositorio
{
    public class VentaRepositorio : IVentasRepositorio
    {

        private readonly AppDbContext _context;
        private readonly string _conexionSql;

        public VentaRepositorio(
            AppDbContext context,
            IConfiguration configuration
         )
        {
            _context = context;
            _conexionSql = configuration.GetConnectionString("Autenticacion");
        }

        public async Task<bool> CrearVentaAsync(VentaEntidad venta)
        {

            venta.UsuariosId = _context.UsuarioAutenticado();

            await _context.Ventas.AddAsync(venta);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> EliminarVentaAsync(VentaEntidad venta)
        {

            _context.Ventas.Remove(venta);

           return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<List<FacturaVenta>> ObtenerVentasAsync()
        {
            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {
                using (SqlCommand cmd = new SqlCommand("RegistrosVentas", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<FacturaVenta>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while(await reader.ReadAsync())
                        {
                            response.Add(MapToFacturaVentas(reader));
                        };

                    };

                    return response;

                }
            };
        }

        public async Task<FacturaVenta> ObtenerVentasPorIdAsync(Guid id)
        {

            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {
                using (SqlCommand cmd = new SqlCommand("RegistrosVentasPorId",  sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id",  id));

                    FacturaVenta response = null;

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            response = MapToFacturaVentas(reader);
                        }
                    }

                    return response;

                }
            }

        }

        public FacturaVenta MapToFacturaVentas(SqlDataReader reader)
        {
            return new FacturaVenta
            {
                Id = (Guid)reader["Id"],
                Cliente = reader["Cliente"].ToString(),
                Usuario = reader["Usuario"].ToString(),
                TipoComprobante = reader["TipoComprobante"].ToString(),
                SerieComprobante = reader["SerieComprobante"].ToString(),
                Impuesto = (decimal)reader["Impuesto"],
                Total = (decimal)reader["Total"],
                Fecha = (DateTime)reader["Fecha"],
                Estatus = reader["Estatus"].ToString()
            };
        }

        public async Task<VentaEntidad> ObtenerVentaPorIdAsync(Guid Id)
        {
            return await _context.Ventas.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<decimal> TotalVentasDiaAsync()
        {
           
            using(SqlConnection sql = new SqlConnection(_conexionSql))
            {

                using (SqlCommand cmd = new SqlCommand("TotalDiaVenta", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    decimal response = 0;

                    await sql.OpenAsync();

                    using(var reader = await cmd.ExecuteReaderAsync())
                    {

                        while(await reader.ReadAsync())
                        {
                            if (reader["Total"] == null) response = 0;

                            response = (decimal)reader["Total"];
                        }

                    }

                    return response;

                }

            }

        }

        public async Task<List<VentaUltimos10Dias>>TotalUltimos10DiasAsync()
        {
            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {

                using (SqlCommand cmd = new SqlCommand("VentasUltimo10Dias", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    List< VentaUltimos10Dias> response = new List<VentaUltimos10Dias>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {

                            response.Add(MapToTotalUltimos10Dias(reader));

                        }

                    }

                    return response;

                }

            }
        }

        public VentaUltimos10Dias MapToTotalUltimos10Dias(SqlDataReader reader)
        {
            return new VentaUltimos10Dias
            {
                Fecha = reader["Dia"].ToString().Substring(0, 10),
                Total = (decimal)reader["Total"]
            };
        }

    }
}
