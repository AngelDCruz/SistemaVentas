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
    public class IngresoRepositorio : IIngresoRepositorio
    {

        private readonly AppDbContext _context;
        private readonly string _conexionSql;


        public IngresoRepositorio(
            AppDbContext context,
            IConfiguration configuration
         )
        {
            _context = context;
            _conexionSql = configuration.GetConnectionString("Autenticacion");
        }

        public async Task<List<FacturaIngreso>> ObtenerIngresoAsync()
        {

            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {
                using(SqlCommand cmd = new SqlCommand("IngresosRegistros", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<FacturaIngreso>();

                    await sql.OpenAsync();

                    using(var reader = await cmd.ExecuteReaderAsync())
                    {

                        while(await reader.ReadAsync())
                        {
                            response.Add(MapToFacturaIngreso(reader));
                        }

                    }

                    return response;

                }
            }

        }

        public async Task<bool> EliminarIngresoAsync(IngresoEntidad ingreso)
        {

            _context.Ingresos.Remove(ingreso);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<FacturaIngreso> ObtenerIngresoIdAsync(Guid id)
        {

            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {
                using (SqlCommand cmd = new SqlCommand("IngresosRegistrosPorId", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));

                    FacturaIngreso response = null;

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToFacturaIngreso(reader);
                        }
                    }

                    return response;
                }
            }

        }

        private FacturaIngreso MapToFacturaIngreso(SqlDataReader reader)
        {
            return new FacturaIngreso
            {
                Id = (Guid)reader["Id"],
                Proveedor = reader["Proveedor"].ToString(),
                Usuario = reader["Usuario"].ToString(),
                TipoComprobante = reader["TipoComprobante"].ToString(),
                SerieComprobante = reader["SerieComprobante"].ToString(),
                Fecha = reader["FechaCreacion"].ToString(),
                Total = (double)reader["Total"],
                Estatus = reader["Estatus"].ToString()
            };
        }

        public async Task<IngresoEntidad> ObtenerIngresoPorIdAsync(Guid id)
        {

            return await _context.Ingresos.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<bool> ActualizarIngresoAsync(IngresoEntidad ingreso)
        {

             _context.Ingresos.Update(ingreso);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<IngresoEntidad> CrearIngresoDetalle(IngresoEntidad ingreso)
        {

            ingreso.UsuariosId = _context.UsuarioAutenticado();

            await _context.Ingresos.AddAsync(ingreso);

            return await _context.SaveChangesAsync() > 0 ? ingreso : null;

        }

        public async Task<DetalleIngresoEntidad> CrearDetalleIngreso(DetalleIngresoEntidad detalleIngreso)
        {

            await _context.DetalleIngresos.AddAsync(detalleIngreso);

            return await _context.SaveChangesAsync() > 0 ? detalleIngreso : null;

        }
    }
}
