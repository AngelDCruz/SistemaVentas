using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Paginacion;
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

        /// <summary>
        /// OBTENE LISTA DE PRODUCTOS
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ObtenerProductosAsync([FromQuery] FiltroPagina filtro, [FromQuery] string estatus = "todos")
        {

            var lstProductos = await _productosServicios.ObtenerProductosAsync(filtro, estatus);

            if (lstProductos == null) return NoContent();

            return Ok(_mapper.Map<List<ProductosDTO>>(lstProductos));

        }

        /// <summary>
        ///  OBTIENE PRODUCTO POR ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}", Name = "ObtenerProductosPorIdAsync")]
        public async Task<ActionResult> ObtenerProductosPorIdAsync([FromRoute] Guid id)
        {

            var producto = await _productosServicios.ObtenerProductoPorIdAsync(id);

            if (producto == null) return NotFound("Producto no encontrado");

            return Ok(_mapper.Map<ProductosDTO>(producto));

        }

        /// <summary>
        /// CREAR PRODUCTO
        /// </summary>
        /// <param name="crearProductoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CrearProductosAsync([FromForm] CrearProductoDTO crearProductoDTO)
        {

            var productoExiste = await _productosServicios.ObtenerProductoPorCodigoAsync(crearProductoDTO.Codigo);

            if (productoExiste != null) return BadRequest("El producto ya esta registrado con el mismo codigo");

            if (await _categoriaServicios.ObtenerCategoriaPorIdAsync(crearProductoDTO.CategoriaId) == null)
            {

                return BadRequest("La categoria no es válida");

            }

            var crearProducto = _mapper.Map<ProductosEntidad>(crearProductoDTO);



            if (crearProducto.Imagen != null)
            {
                // CARGA DE IMAGEN
                //var raizProyecto = _hostingEnvironment.ContentRootPath;
                var destino = $"wwwroot\\assets\\img\\productos\\";
                var imagenFinal = SubidaImagenProducto(crearProductoDTO.Imagen, destino);

                crearProducto.Imagen = imagenFinal;
            
            }

            var respuesta = await _productosServicios.CrearProductoAsync(crearProducto);

            if (respuesta == null) return BadRequest("El producto no pudo crearse");

            var productoDTO = _mapper.Map<ProductosDTO>(respuesta);

            return CreatedAtRoute("ObtenerProductosPorIdAsync", new { id = respuesta.Id }, productoDTO);

        }

        /// <summary>
        /// ACTUALIZA PRODUCTO
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="actualizarProductosDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// ELIMINA PRODUCTO POR ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> EliminarProductoAsync([FromRoute] Guid Id)
        {

            var existeProducto = await _productosServicios.ObtenerProductoPorIdAsync(Id);

            if (existeProducto == null) return NotFound("El producto no existe");

            var respuesta = await _productosServicios.EliminarProductoAsync(existeProducto);

            return Ok(_mapper.Map<ProductosDTO>(existeProducto));

        }

        /// <summary>
        /// FILTRA LISTA DE PRODUCTOS POR BUSQUEDA AVANZADA
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpPost("filtrar")]
        public async Task<ActionResult> FiltrarProductosAsync([FromBody] FiltroProductoDTO filtro)
        {

            var lstProductos = await _productosServicios.ObtenerProductosFiltroAsync(filtro.Nombre, filtro.Codigo);

            if (lstProductos == null) return NoContent();

            return Ok(_mapper.Map<List<ProductosDTO>>(lstProductos));

        }

        /// <summary>
        /// CAMBIA IMAGEN DE PRODUCTO
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("{Id:guid}/cambio-imagen")]
        public async Task<ActionResult> CambiarImagenProducto([FromRoute] Guid Id,  [FromForm] ImagenDTO file)
        {

            var productoExiste = await _productosServicios.ObtenerProductoPorIdAsync(Id);

            if (productoExiste == null) return NotFound("Producto no encontrado");

            var destino = $"wwwroot\\assets\\img\\productos\\";
            var rutaImagen = SubidaImagenProducto(file.Imagen, destino);

            var respuesta = await _productosServicios.ActualizarImagenProductoAsync(Id, rutaImagen);

            if (!respuesta) return BadRequest("No se pudo actualizar la imagen");

            return Ok(rutaImagen);

        }

        /// <summary>
        /// ACTIVA PRODUCTO DADO DE BAJA
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:guid}/activar-producto")]
        public async Task<ActionResult> ActivarProductoAsync([FromRoute] Guid Id)
        {

            var productoExiste = await _productosServicios.ObtenerProductoPorIdAsync(Id);

            if (productoExiste == null) return NotFound("Producto no encontrado");

            var respuesta = await _productosServicios.ActivarProductoAsync(Id);

            if (!respuesta) return BadRequest("El producto no pudo activarse correctamente");

            return NoContent();

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