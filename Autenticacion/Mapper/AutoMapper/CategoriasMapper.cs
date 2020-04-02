using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVentas.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Api.Mapper.AutoMapper
{
    public class CategoriasMapper : Profile
    {

        public CategoriasMapper()
        {


            CreateMap<CategoriasEntidad, CategoriasDTO>()
                .ForMember(x => x.Id, opcion => opcion.MapFrom(x => x.Id))
                .ForMember(x => x.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                .ForMember(x => x.Descripcion, opcion => opcion.MapFrom(x => x.Descripcion))
                .ForMember(x => x.Estatus, opcion => opcion.MapFrom(x => x.Estatus))
                .ReverseMap();


            CreateMap<CategoriasEntidad, CrearCategoriaDTO>()
                .ForMember(x => x.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                .ForMember(x => x.Descripcion, opcion => opcion.MapFrom(x => x.Descripcion))
                .ReverseMap();

            CreateMap<CategoriasEntidad, ActualizarCategoriaDTO>()
                .ForMember(x => x.Id, opcion => opcion.MapFrom(x => x.Id))
                .ForMember(x => x.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                .ForMember(x => x.Descripcion, opcion => opcion.MapFrom(x => x.Descripcion))
                .ReverseMap();
        }

    }
}
