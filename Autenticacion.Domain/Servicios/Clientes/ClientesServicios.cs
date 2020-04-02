using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Paginacion;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Repositorio;

namespace SistemaVentas.Dominio.Servicios.Clientes
{
    public class ClientesServicios : IClientesServicios
    {

        private readonly IPersonaRepositorio _personaRepositorio;

        public ClientesServicios(IPersonaRepositorio personaRepositorio)
        {

            _personaRepositorio = personaRepositorio;

        }


        public async Task<List<PersonaEntidad>> ObtenerClientesAsync(FiltroPagina filtro, string estatus = "todos")
        {

            var lstPersonas = _personaRepositorio.ObtenerPersonas("clientes");

            if(filtro != null)
            {

                var pagina = (filtro.Pagina - 1) * filtro.Limite;
                var limite = filtro.Limite;

                lstPersonas = lstPersonas.Skip(pagina).Take(limite);

            }

            if (estatus == "bajas")
            {

                return await lstPersonas.Where(x => x.Estatus == "Baj").ToListAsync();

            }

            if(estatus == "activos")
            {

                return await lstPersonas.Where(x => x.Estatus == "Act").ToListAsync();

            }

            return await lstPersonas.ToListAsync();

        }

        public async Task<PersonaEntidad> ObtenerClienteIdAsync(Guid id)
        {

            return await _personaRepositorio.ObtenerPersonaIdAsync(id, "clientes");

        }

        public async Task<PersonaEntidad> ObtenerClienteNumDocumento(string numDocumento)
        {

            return await _personaRepositorio.ObtenerPersonaNumDocumento(numDocumento, "clientes");

        }

        public async Task<PersonaEntidad> CrearClienteAsync(PersonaEntidad persona)
        {

            persona.TipoPersona = "clientes";
            persona.Estatus = "Act";

            return await _personaRepositorio.CrearPersonaAsync(persona);

        }

        public async Task<bool> ActualizarClienteAsync(PersonaEntidad persona)
        {

            var clienteActualizar = await _personaRepositorio.ObtenerPersonaIdAsync(persona.Id, "clientes");
            clienteActualizar.Nombre = persona.Nombre;
            clienteActualizar.Telefono = persona.Telefono;
            clienteActualizar.Email = persona.Email;
            clienteActualizar.Direccion = persona.Direccion;
            clienteActualizar.TipoDocumento = persona.TipoDocumento;
            clienteActualizar.NumDocumento = persona.NumDocumento;

            return await _personaRepositorio.ActualizarPersonaAsync(clienteActualizar);

        }

        public async Task<bool> EliminarClienteAsync(PersonaEntidad persona)
        {

            return await _personaRepositorio.EliminarPersonaAsync(persona);

        }

        public async Task<bool> ActivarClienteAsync(Guid Id)
        {

            var clienteActualizar = await _personaRepositorio.ObtenerPersonaIdAsync(Id, "clientes");
            clienteActualizar.Estatus = "Act";

            return await _personaRepositorio.ActualizarPersonaAsync(clienteActualizar);

        }

        public async Task<List<PersonaEntidad>> ObtenerFiltroCliente(FiltroPersonaDTO filtro)
        {

            var lstClientes = _personaRepositorio.ObtenerPersonas("clientes");


            if(filtro.Id != Guid.Empty)
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
