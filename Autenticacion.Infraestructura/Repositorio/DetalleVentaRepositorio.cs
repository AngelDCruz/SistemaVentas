using Microsoft.Extensions.Configuration;
using SistemaVentas.Dominio.ModelStore;
using SistemaVentas.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Infraestructura.Repositorio
{
    public class DetalleVentaRepositorio : IDetalleVentaRepositorio
    {

        private readonly string _conexionSql;

        public DetalleVentaRepositorio(IConfiguration configuration)
        {
            _conexionSql = configuration.GetConnectionString("Autenticacion");
        }

        public async Task<List<FacturaDetalleVenta>> ObtenerDetallesVentaPorIdAsync(Guid idVenta)
        {
            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerDetallesVentaId", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", idVenta));

                    List<FacturaDetalleVenta> respuesta = new List<FacturaDetalleVenta>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            respuesta.Add(mapToFacturaDetallesVenta(reader));
                        }
                    }

                    return respuesta;

                }
            }
        }

        private FacturaDetalleVenta mapToFacturaDetallesVenta(SqlDataReader reader)
        {
            return new FacturaDetalleVenta
            {
                Id = (Guid)reader["Id"],
                Producto = reader["Producto"].ToString(),
                Cantidad = (int)reader["Cantidad"],
                Precio = (decimal)reader["Precio"],
                Fecha = (DateTime)reader["Fecha"],
                Estatus = reader["Estatus"].ToString()
            };
        }

    }
}
