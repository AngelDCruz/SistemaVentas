using SistemaVentas.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Api.Mapper.Personalizados
{
    public class IngresoMapper
    {

        public static IngresoEntidad Map(CrearIngresoDTO ingresoDTO)
        {

            IngresoEntidad ingreso = new IngresoEntidad();

            if( ingresoDTO != null )
            {

                ingreso.PersonasId = ingresoDTO.PersonaId;
                ingreso.TipoComprobante = ingresoDTO.TipoComprobante;
                ingreso.SerieComprobante = ingresoDTO.SerieComprobante;
                ingreso.Impuesto = ingresoDTO.Impuesto;
                ingreso.Total = ingresoDTO.Total;
                ingreso.Estatus = "Act";

            }

            return ingreso;

        }

        public static IngresosDTO Map(IngresoEntidad ingreso)
        {

            IngresosDTO ingresoDTO = new IngresosDTO();

            if (ingresoDTO != null)
            {

                ingresoDTO.Id = ingreso.Id;
                ingresoDTO.TipoComprobante = ingreso.TipoComprobante;
                ingresoDTO.SerieComprobante = ingreso.SerieComprobante;
                ingresoDTO.Impuesto = ingreso.Impuesto;
                ingresoDTO.Total = ingreso.Total;
                ingresoDTO.Estatus = ingreso.Estatus;

            }

            return ingresoDTO;

        }

    }
}
