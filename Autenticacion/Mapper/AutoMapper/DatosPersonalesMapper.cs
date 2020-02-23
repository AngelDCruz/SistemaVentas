
using Autenticacion.Aplicacion.DTO.Respuestas.v1;
using Autenticacion.Aplicacion.DTO.Solicitudes.v1;

using Autenticacion.Dominio.Entidades;

using AutoMapper;


namespace Autenticacion.Api.Automapper
{
    public class DatosPersonalesMapper : Profile
    {

        public DatosPersonalesMapper()
        {

            CreateMap<DatosPersonalesEntidad, Aplicacion.DTO.Respuestas.v1.DatosPersonalesDTO>()
                .ForMember(d => d.Id, opcion => opcion.MapFrom(x => x.Id))
                .ForMember(d => d.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                .ForMember(d => d.ApellidoPaterno, opcion => opcion.MapFrom(x => x.ApellidoPaterno))
                .ForMember(d => d.Pais, opcion => opcion.MapFrom(x => x.Pais))
                .ForMember(d => d.Ciudad, opcion => opcion.MapFrom(x => x.Ciudad))
                .ForMember(d => d.Calle, opcion => opcion.MapFrom(x => x.Calle))
                .ForMember(d => d.Telefono, opcion => opcion.MapFrom(x => x.Telefono))
                .ForMember(d => d.UsuarioCreacion, opcion => opcion.MapFrom(x => x.UsuarioCreacion))
                .ForMember(d => d.FechaCreacion, opcion => opcion.MapFrom(x => x.FechaCreacion))
                .ReverseMap();

            CreateMap<DatosPersonalesEntidad, ActualizarDatosPersonalesDTO>()
          .ForMember(d => d.Id, opcion => opcion.MapFrom(x => x.Id))
          .ForMember(d => d.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
          .ForMember(d => d.ApellidoPaterno, opcion => opcion.MapFrom(x => x.ApellidoPaterno))
          .ForMember(d => d.Pais, opcion => opcion.MapFrom(x => x.Pais))
          .ForMember(d => d.Ciudad, opcion => opcion.MapFrom(x => x.Ciudad))
          .ForMember(d => d.Calle, opcion => opcion.MapFrom(x => x.Calle))
          .ForMember(d => d.Telefono, opcion => opcion.MapFrom(x => x.Telefono))
          .ReverseMap();

        }

    }
}
