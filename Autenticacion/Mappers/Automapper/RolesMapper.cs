
using AutoMapper;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.DTO.Respuestas.v1;
using SistemaVentas.DTO.Solicitudes.v1;

namespace Autenticacion.Api.Automapper
{
    public class RolesMapper :  Profile
    {

        public RolesMapper()
        {

            CreateMap<RolesEntidad, RolesDTO>()
                .ForMember(r => r.Id, opcion => opcion.MapFrom(x => x.Id))
                .ForMember(r => r.Nombre, opcion => opcion.MapFrom(x => x.Name))
                .ForMember(r => r.FechaCreacion, opcion => opcion.MapFrom(x => x.FechaCreacion))
                .ForMember(r => r.Estatus, opcion => opcion.MapFrom(x => x.Estatus))
                .ReverseMap();

            CreateMap<RolesEntidad, CrearRoleDTO>()
             .ForMember(r => r.Nombre, opcion => opcion.MapFrom(x => x.Name))
             .ReverseMap();

            CreateMap<RolesEntidad, ActualizarRoleDTO>()
            .ForMember(r => r.Id, opcion => opcion.MapFrom(x => x.Id))
            .ForMember(r => r.Nombre, opcion => opcion.MapFrom(x => x.Name))
            .ReverseMap();

        }

    }
}
