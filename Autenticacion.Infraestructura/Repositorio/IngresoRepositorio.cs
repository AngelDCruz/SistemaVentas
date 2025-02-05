﻿using Autenticacion.Infraestructura;
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
                using (SqlCommand cmd = new SqlCommand("IngresosRegistros", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<FacturaIngreso>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
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
                Impuesto = (decimal)reader["Impuesto"],
                Total = (decimal)reader["Total"],
                Fecha = reader["FechaCreacion"].ToString(),
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

        public async Task<decimal> TotalIngresoDiaAsync()
        {
            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {
                using (SqlCommand cmd = new SqlCommand("TotalDiaIngreso", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    decimal response = 0;

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            if (reader["Total"] == null) response = 0;

                            response = (decimal)reader["Total"];

                        }

                    }

                    return response;

                }
            }
        }

        public async Task<List<IngresoUltimos10Dias>> TotalIngresoUltimo10DiasAsync()
        {

            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {
                using (SqlCommand cmd = new SqlCommand("TotalIngreso10Dias", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<IngresoUltimos10Dias>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToIngreso10Dias(reader));
                        }

                    }

                    return response;

                }
            }

        }

        private IngresoUltimos10Dias MapToIngreso10Dias(SqlDataReader reader)
        {
           return new IngresoUltimos10Dias {
               Fecha = reader["Dia"].ToString().Substring(0, 10),
               Total = (decimal) reader["Total"]
            };
        }
    }
}
