
using AutoMapper;

using Autenticacion.Api.DTO.Respuestas.v1;
using Autenticacion.Api.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;


namespace Autenticacion.Api.Automapper
{
    public class UsuariosMapper : Profile
    {

        public UsuariosMapper()
        {


            CreateMap<Usuarios, UsuariosDTO>()
                 .ForMember(u => u.Usuario, opcion => opcion.MapFrom(src => src.UserName))
                 .ForMember(u => u.Telefono, opcion => opcion.MapFrom(src => src.PhoneNumber))
                 .ReverseMap();

            CreateMap<Usuarios, CrearUsuarioDTO>()
                .ForMember(u => u.Usuario, opcion => opcion.MapFrom(src => src.UserName))
                .ForMember(u => u.Contrasena, opcion => opcion.MapFrom(src => src.PasswordHash))
                .ForMember(u => u.Telefono, opcion => opcion.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<Usuarios, ActualizarUsuarioDTO>()
               //.ForMember(u => u.Contrasena, opcion => opcion.MapFrom(src => src.PasswordHash))
               .ForMember(u => u.Telefono, opcion => opcion.MapFrom(src => src.PhoneNumber))
               .ReverseMap();

        }

    }
}
