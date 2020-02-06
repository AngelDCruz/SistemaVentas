
using AutoMapper;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.DTO.Respuestas.v1;
using Autenticacion.Dominio.DTO.Solicitudes.v1;

namespace Autenticacion.Api.Automapper
{
    public class UsuariosMapper : Profile
    {

        public UsuariosMapper()
        {

            CreateMap<UsuariosEntidad, UsuariosDTO>()
                 .ForMember(u => u.Usuario, opcion => opcion.MapFrom(src => src.UserName))
                 .ForMember(u => u.Telefono, opcion => opcion.MapFrom(src => src.PhoneNumber))
                 .ReverseMap();

            CreateMap<UsuariosEntidad, CrearUsuarioDTO>()
                .ForMember(u => u.Usuario, opcion => opcion.MapFrom(src => src.UserName))
                .ForMember(u => u.Contrasena, opcion => opcion.MapFrom(src => src.PasswordHash))
                .ForMember(u => u.Telefono, opcion => opcion.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<UsuariosEntidad, ActualizarUsuarioDTO>()
               //.ForMember(u => u.Contrasena, opcion => opcion.MapFrom(src => src.PasswordHash))
               .ForMember(u => u.Telefono, opcion => opcion.MapFrom(src => src.PhoneNumber))
               .ReverseMap();

        }

    }
}
