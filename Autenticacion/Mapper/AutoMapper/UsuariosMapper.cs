
using AutoMapper;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Aplicacion.DTO.Respuestas.v1;

namespace Autenticacion.Api.Automapper
{
    public class UsuariosMapper : Profile
    {

        public UsuariosMapper()
        {

            CreateMap<UsuariosDTO, UsuariosEntidad>()
                .ForMember(u => u.Id, src => src.MapFrom(x => x.Id))
                .ForMember(u => u.UserName, src => src.MapFrom(x => x.Usuario))
                .ForMember(u => u.Email, src => src.MapFrom(x => x.Email))
                .ForMember(u => u.FechaCreacion, src => src.MapFrom(x  => x.FechaCreacion))
                .ForMember(u => u.Estatus, src => src.MapFrom(x => x.Estatus))
                .ForMember(u => u.ImagenPerfil, src => src.MapFrom(x => x.Imagen))
                .ReverseMap();
         

        }

    }
}
