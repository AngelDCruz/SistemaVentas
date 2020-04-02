using Common.Paginacion;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Servicios.Clientes
{
   public  interface IClientesServicios
    {

        Task<List<PersonaEntidad>> ObtenerClientesAsync(FiltroPagina filtro, string estatus = "todos");

        Task<PersonaEntidad> ObtenerClienteIdAsync(Guid id);

        Task<PersonaEntidad> ObtenerClienteNumDocumento(string numDocumento);

        Task<List<PersonaEntidad>> ObtenerFiltroCliente(FiltroPersonaDTO filtro);

        Task<PersonaEntidad> CrearClienteAsync(PersonaEntidad persona);

        Task<bool> ActualizarClienteAsync(PersonaEntidad persona);

        Task<bool> EliminarClienteAsync(PersonaEntidad persona);

        Task<bool> ActivarClienteAsync(Guid Id);

    }
}
