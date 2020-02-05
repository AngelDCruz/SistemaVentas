using Autenticacion.Api.DTO.Respuestas.v1;
using Autenticacion.Api.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using AutoMapper;

namespace Autenticacion.Api.Automapper
{
    public class RolesMapper :  Profile
    {

        public RolesMapper()
        {

            CreateMap<Roles, RolesDTO>()
                .ForMember(r => r.Id, opcion => opcion.MapFrom(x => x.Id))
                .ForMember(r => r.Nombre, opcion => opcion.MapFrom(x => x.Name))
                .ForMember(r => r.FechaCreacion, opcion => opcion.MapFrom(x => x.FechaCreacion))
                .ForMember(r => r.Estatus, opcion => opcion.MapFrom(x => x.Estatus))
                .ReverseMap();

            CreateMap<Roles, CrearRoleDTO>()
             .ForMember(r => r.Nombre, opcion => opcion.MapFrom(x => x.Name))
             .ReverseMap();

            CreateMap<Roles, ActualizarRoleDTO>()
            .ForMember(r => r.Id, opcion => opcion.MapFrom(x => x.Id))
            .ForMember(r => r.Nombre, opcion => opcion.MapFrom(x => x.Name))
            .ReverseMap();

        }

    }
}
