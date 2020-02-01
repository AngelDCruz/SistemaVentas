using Autenticacion.Api.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.Automapper
{
    public class Automapper : Profile
    {

        public Automapper()
        {

            CreateMap<Usuarios, UsuariosSolicitudDTO>()
                .ForMember(u => u.Contrasena, opcion => opcion.MapFrom(src => src.PasswordHash))
                .ForMember(u => u.Telefono, opcion => opcion.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

        }

    }
}
