using Common.Paginacion;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Servicios.Proveedores
{
   public interface IProveedoresServicios
    {
        Task<List<PersonaEntidad>> ObtenerProveedoresAsync(FiltroPagina filtro, string estatus = "todos");

        Task<PersonaEntidad> ObtenerProveedorIdAsync(Guid id);

        Task<PersonaEntidad> ObtenerProveedorNumDocumento(string numDocumento);

        Task<List<PersonaEntidad>> ObtenerFiltroProveedor(FiltroPersonaDTO filtro);

        Task<PersonaEntidad> CrearProveedorAsync(PersonaEntidad persona);

        Task<bool> ActualizarProveedorAsync(PersonaEntidad persona);

        Task<bool> EliminarProveedorAsync(PersonaEntidad persona);

        Task<bool> ActivarProveedorAsync(Guid Id);
    }
}
