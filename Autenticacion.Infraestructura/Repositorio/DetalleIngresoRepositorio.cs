using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SistemaVentas.Dominio.ModelStore;
using SistemaVentas.Dominio.Repositorio;

namespace SistemaVentas.Infraestructura.Repositorio
{
    public class DetalleIngresoRepositorio : IDetallesIngresoRepositorio
    {

        private readonly string _conexionSql; 

        public DetalleIngresoRepositorio(
            IConfiguration configuration
        )
        {

            _conexionSql = configuration.GetConnectionString("Autenticacion");

        }

        public async Task<List<FacturaDetalleIngreso>> ObtenerDetallesIngresosIdAsync(Guid idIngreso)
        {

            using (SqlConnection sql = new SqlConnection(_conexionSql))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerDetallesIngresosId", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", idIngreso));

                    List<FacturaDetalleIngreso> respuesta = new List<FacturaDetalleIngreso>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            respuesta.Add(mapToFacturaDetallesIngreso(reader));
                        }
                    }

                    return respuesta;

                }
            }

        }

        private FacturaDetalleIngreso mapToFacturaDetallesIngreso(SqlDataReader reader)
        {

            return new FacturaDetalleIngreso
            {
                Id = (Guid)reader["Id"],
                Producto = reader["Producto"].ToString(),
                Cantidad = (int)reader["Cantidad"],
                Precio =  (double)reader["Precio"],
                FechaCreacion = (DateTime)reader["FechaCreacion"],
                Estatus = reader["Estatus"].ToString()
            };

        }

    }
}
