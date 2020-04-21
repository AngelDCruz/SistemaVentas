using AutoMapper;
using SistemaVentas.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;


namespace SistemaVentas.Api.Mapper.AutoMapper
{
    public class ProductosMapper : Profile
    {

        public ProductosMapper()
        {

            CreateMap<ProductosEntidad, ProductosDTO>()
                .ForMember(p => p.Id, opcion => opcion.MapFrom(x => x.Id))
                .ForMember(p => p.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                .ForMember(p => p.Codigo, opcion => opcion.MapFrom(x => x.Codigo))
                .ForMember(p => p.Imagen, opcion => opcion.MapFrom(x => x.Imagen))
                .ForMember(p => p.Descripcion, opcion => opcion.MapFrom(x => x.Descripcion))
                .ForMember(p => p.Categoria, opcion => opcion.MapFrom(x => x.CategoriaId))
                .ForMember(p => p.Estatus, opcion => opcion.MapFrom(x => x.Estatus))
                .ReverseMap();

            CreateMap<ProductosEntidad, CrearProductoDTO>()
                .ForMember(p => p.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                .ForMember(p => p.Descripcion, opcion => opcion.MapFrom(x => x.Descripcion))
                .ForMember(p => p.Codigo, opcion => opcion.MapFrom(x => x.Codigo))
                .ForMember(p => p.CategoriaId, opcion => opcion.MapFrom(x => x.CategoriaId))
                .ReverseMap();

            CreateMap<ProductosEntidad, ActualizarProductosDTO>()
                .ForMember(p => p.Id, opcion => opcion.MapFrom(x => x.Id))
                .ForMember(p => p.Nombre, opcion => opcion.MapFrom(x => x.Nombre))
                .ForMember(p => p.Descripcion, opcion => opcion.MapFrom(x => x.Descripcion))
                .ForMember(p => p.Codigo, opcion => opcion.MapFrom(x => x.Codigo))
                .ForMember(p => p.Categoria, opcion => opcion.MapFrom(x => x.CategoriaId))
                .ReverseMap();

        }

    }
}
