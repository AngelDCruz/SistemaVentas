using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Api.Mapper.Personalizados
{
    public class VentaMapper
    {
        public static VentaEntidad Map(CrearVentaDTO crearEntidadDTO)
        {

            VentaEntidad ventaEntidad = new VentaEntidad();
            List<DetalleVentaEntidad> lstDetalleVenta = new List<DetalleVentaEntidad>();

            if (crearEntidadDTO != null)
            {

                ventaEntidad.PersonaId = crearEntidadDTO.PersonaId;
                ventaEntidad.TipoComprobante = crearEntidadDTO.TipoComprobante;
                ventaEntidad.SerieComprobante = crearEntidadDTO.SerieComprobante;
                ventaEntidad.Impuesto = crearEntidadDTO.Impuesto;
                ventaEntidad.Total = crearEntidadDTO.Total;
                ventaEntidad.Estatus = "Act";
           
                 if(crearEntidadDTO.DetalleVentas != null)
                {

                    foreach (var elem in crearEntidadDTO.DetalleVentas)
                    {

                        lstDetalleVenta.Add(new DetalleVentaEntidad
                        {
                            ProductoId = elem.ProductoId,
                            Cantidad = elem.Cantidad,
                            Descuento = elem.Descuento,
                            Precio = elem.Precio,
                            Estatus = "Act"
                        });

                    }
         
                }

                ventaEntidad.DetalleVentas = lstDetalleVenta;

            }

            return ventaEntidad;

        }
    }
}
