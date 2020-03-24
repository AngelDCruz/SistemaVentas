using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Servicios.Categoria;
using SistemaVentas.Dominio.Servicios.Productos;
using SistemaVentas.Infraestructura.Transversal.Helpers.Base64;
using SistemaVentas.Infraestructura.Transversal.Helpers.SubidaArchivos;

namespace SistemaVentas.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductosController : ControllerBase
    {

        private readonly IProductosServicios _productosServicios;
        private readonly ICategoriaServicios _categoriaServicios;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductosController(
            IProductosServicios productosServicios,
            ICategoriaServicios categoriaServicios,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment
       )
        {
            _productosServicios = productosServicios;
            _categoriaServicios = categoriaServicios;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public async Task<ActionResult> ObtenerProductosAsync()
        {

            var lstProductos = await _productosServicios.ObtenerProductosAsync();

            if (lstProductos == null) return NoContent();

            return Ok(_mapper.Map<List<ProductosDTO>>(lstProductos));

        }

        [HttpGet("{id:guid}", Name = "ObtenerProductosPorIdAsync")]
        public async Task<ActionResult> ObtenerProductosPorIdAsync([FromRoute] Guid id)
        {

            var producto = await _productosServicios.ObtenerProductoPorIdAsync(id);

            if (producto == null) return NotFound("Producto no encontrado");

            return Ok(_mapper.Map<ProductosDTO>(producto));

        }

        [HttpPost]
        public async Task<ActionResult> CrearProductosAsync([FromForm] CrearProductoDTO crearProductoDTO)
        {

            var productoExiste = await _productosServicios.ObtenerProductoPorCodigoAsync(crearProductoDTO.Codigo);

            if (productoExiste != null) return BadRequest("El producto ya esta registrado con el mismo codigo");

            if(await _categoriaServicios.ObtenerCategoriaPorIdAsync(crearProductoDTO.Categoria) == null)
            {

                return BadRequest("La categoria no es válida");

            }

            var crearProducto = _mapper.Map<ProductosEntidad>(crearProductoDTO);


            // CARGA DE IMAGEN
            //var raizProyecto = _hostingEnvironment.ContentRootPath;
            var destino = $"wwwroot\\assets\\img\\productos\\";
            var imagenFinal = SubidaImagenProducto(crearProductoDTO.Imagen, destino);

            crearProducto.Imagen = imagenFinal;

            var respuesta = await _productosServicios.CrearProductoAsync(crearProducto);

            if (respuesta == null) return BadRequest("El producto no pudo crearse");

            var productoDTO = _mapper.Map<ProductosDTO>(respuesta);

            return CreatedAtRoute("ObtenerProductosPorIdAsync", new { id = respuesta.Id }, productoDTO);

        }

        [HttpPut("{Id:guid}")]
        public async Task<ActionResult> ActualizarProductosAsync([FromRoute] Guid Id, [FromBody] ActualizarProductosDTO actualizarProductosDTO)
        {

            if (Id != actualizarProductosDTO.Id) return BadRequest("El modelo no es valido");

            var existeProducto = await _productosServicios.ObtenerProductoPorIdAsync(Id);

            if (existeProducto == null) return NotFound("Producto no encontrado");

            var actualizarProducto = _mapper.Map<ProductosEntidad>(actualizarProductosDTO);

            var respuesta = await _productosServicios.ActualizarProductoAsync(actualizarProducto);

            if (!respuesta) return BadRequest("El producto no se actualizo correctamente");

            return NoContent();

        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> EliminarProductoAsync([FromRoute] Guid Id)
        {

            var existeProducto = await _productosServicios.ObtenerProductoPorIdAsync(Id);

            if (existeProducto == null) return NotFound("El producto no existe");

            var respuesta = await _productosServicios.EliminarProductoAsync(existeProducto);

            return Ok(_mapper.Map<ProductosDTO>(existeProducto));

        }
        private string SubidaImagenProducto(IFormFile archivo, string ruta)
        {

            var subirImagen = new SubidaImagen();
            var imagenSubida = subirImagen.CargarArchivo(archivo, ruta);
            var codex = imagenSubida.Codex;
            var url = imagenSubida.Url;

            var imagenConvertida64 = ConvertirBase64.Convertir64String(url);

            var imagenBase64YCodex = $"{codex}{imagenConvertida64}";

            return imagenBase64YCodex;

        }

    }



}