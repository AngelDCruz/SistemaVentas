

using Common.Paginacion;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Servicios.Proveedores
{
    public class ProveedoresServicios : IProveedoresServicios
    {

        private readonly IPersonaRepositorio _personaRepositorio;

        public ProveedoresServicios(IPersonaRepositorio personaRepositorio)
        {

            _personaRepositorio = personaRepositorio;

        }


        public async Task<List<PersonaEntidad>> ObtenerProveedoresAsync(FiltroPagina filtro, string estatus = "todos")
        {

            var lstPersonas = _personaRepositorio.ObtenerPersonas("proveedores");

            if (filtro != null)
            {

                var pagina = (filtro.Pagina - 1) * filtro.Limite;
                var limite = filtro.Limite;

                lstPersonas = lstPersonas.Skip(pagina).Take(limite);

            }

            if (estatus == "bajas")
            {

                return await lstPersonas.Where(x => x.Estatus == "Baj").ToListAsync();

            }

            if (estatus == "activos")
            {

                return await lstPersonas.Where(x => x.Estatus == "Act").ToListAsync();

            }

            return await lstPersonas.ToListAsync();

        }

        public async Task<PersonaEntidad> ObtenerProveedorIdAsync(Guid id)
        {

            return await _personaRepositorio.ObtenerPersonaIdAsync(id, "proveedores");

        }

        public async Task<PersonaEntidad> ObtenerProveedorNumDocumento(string numDocumento)
        {

            return await _personaRepositorio.ObtenerPersonaNumDocumento(numDocumento, "proveedores");

        }

        public async Task<PersonaEntidad> CrearProveedorAsync(PersonaEntidad persona)
        {

            persona.TipoPersona = "proveedores";
            persona.Estatus = "Act";

            return await _personaRepositorio.CrearPersonaAsync(persona);

        }

        public async Task<bool> ActualizarProveedorAsync(PersonaEntidad persona)
        {

            var clienteActualizar = await _personaRepositorio.ObtenerPersonaIdAsync(persona.Id, "proveedores");
            clienteActualizar.Nombre = persona.Nombre;
            clienteActualizar.Telefono = persona.Telefono;
            clienteActualizar.Email = persona.Email;
            clienteActualizar.Direccion = persona.Direccion;
            clienteActualizar.TipoDocumento = persona.TipoDocumento;
            clienteActualizar.NumDocumento = persona.NumDocumento;

            return await _personaRepositorio.ActualizarPersonaAsync(clienteActualizar);

        }

        public async Task<bool> EliminarProveedorAsync(PersonaEntidad persona)
        {

            return await _personaRepositorio.EliminarPersonaAsync(persona);

        }

        public async Task<bool> ActivarProveedorAsync(Guid Id)
        {

            var clienteActualizar = await _personaRepositorio.ObtenerPersonaIdAsync(Id, "proveedores");
            clienteActualizar.Estatus = "Act";

            return await _personaRepositorio.ActualizarPersonaAsync(clienteActualizar);

        }

        public async Task<List<PersonaEntidad>> ObtenerFiltroProveedor(FiltroPersonaDTO filtro)
        {

            var lstClientes = _personaRepositorio.ObtenerPersonas("proveedores");


            if (filtro.Id != Guid.Empty)
            {

                lstClientes = lstClientes.Where(x => x.Id == filtro.Id);

            }

            if (filtro.Nombre != null)
            {

                lstClientes = lstClientes.Where(x => x.Nombre.Contains(filtro.Nombre));

            }

            if (filtro.TipoDocumento != null)
            {

                lstClientes = lstClientes.Where(x => x.TipoDocumento == filtro.TipoDocumento);

            }

            if (filtro.NumDocumento != null)
            {

                lstClientes = lstClientes.Where(x => x.NumDocumento.Contains(filtro.NumDocumento));

            }

            if (filtro.Direccion != null)
            {

                lstClientes = lstClientes.Where(x => x.Direccion.Contains(filtro.Direccion));

            }

            if (filtro.Telefono != null)
            {

                lstClientes = lstClientes.Where(x => x.Telefono.Contains(filtro.Telefono));

            }

            if (filtro.Email != null)
            {

                lstClientes = lstClientes.Where(x => x.Email.Contains(filtro.Email));

            }

            if (filtro.Email != null)
            {

                lstClientes = lstClientes.Where(x => x.Email.Contains(filtro.Email));

            }

            return await lstClientes.ToListAsync();

        }

    }
}
