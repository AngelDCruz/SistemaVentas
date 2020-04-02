using AutoMapper;
using SistemaVentas.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Api.Mapper.AutoMapper
{
    public class PersonaMapper : Profile
    {

        public PersonaMapper()
        {

            CreateMap<PersonaEntidad, PersonasDTO>()
                     .ForMember(x => x.Id, opcion => opcion.MapFrom(x => x.Id))
                     .ForMember(x => x.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                     .ForMember(x => x.Direccion, opcion => opcion.MapFrom(x => x.Direccion))
                     .ForMember(x => x.Telefono, opcion => opcion.MapFrom(x => x.Telefono))
                     .ForMember(x => x.Email, opcion => opcion.MapFrom(x => x.Email))
                     .ForMember(x => x.TipoDocumento, opcion => opcion.MapFrom(x => x.TipoDocumento))
                     .ForMember(x => x.NumDocumento, opcion => opcion.MapFrom(x => x.NumDocumento))
                    .ForMember(x => x.TipoPersona, opcion => opcion.MapFrom(x => x.TipoPersona))
                    .ForMember(x => x.Estatus, opcion => opcion.MapFrom(x => x.Estatus))
                    .ReverseMap();

            CreateMap<PersonaEntidad, CrearPersonaDTO>()
                .ForMember(x => x.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                .ForMember(x => x.Direccion, opcion => opcion.MapFrom(x => x.Direccion))
                .ForMember(x => x.Telefono, opcion => opcion.MapFrom(x => x.Telefono))
                .ForMember(x => x.Email, opcion => opcion.MapFrom(x => x.Email))
                .ForMember(x => x.TipoDocumento, opcion => opcion.MapFrom(x => x.TipoDocumento))
                .ForMember(x => x.NumDocumento, opcion => opcion.MapFrom(x => x.NumDocumento))
               .ReverseMap();

            CreateMap<PersonaEntidad, ActualizarPersonaDTO>()
             .ForMember(x => x.Id, opcion => opcion.MapFrom(x => x.Id))
             .ForMember(x => x.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
             .ForMember(x => x.Direccion, opcion => opcion.MapFrom(x => x.Direccion))
             .ForMember(x => x.Telefono, opcion => opcion.MapFrom(x => x.Telefono))
             .ForMember(x => x.Email, opcion => opcion.MapFrom(x => x.Email))
             .ForMember(x => x.TipoDocumento, opcion => opcion.MapFrom(x => x.TipoDocumento))
             .ForMember(x => x.NumDocumento, opcion => opcion.MapFrom(x => x.NumDocumento))
            .ReverseMap();

        }

    }
}
