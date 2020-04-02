using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Paginacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Servicios.Categoria;

namespace SistemaVentas.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriasController : ControllerBase
    {

        private ICategoriaServicios _categoriaServicios { get; set; }

        private IMapper _mapper { get; set; }

        public CategoriasController(
            ICategoriaServicios categoriaServicios,
            IMapper mapper
         )
        {

            _categoriaServicios = categoriaServicios;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult> ObtenerCategoriasAsync([FromQuery] FiltroPagina filtro, [FromQuery] string estatus = "todos")
        {

            var lstCategorias = await _categoriaServicios.ObtenerCategoriasAsync(filtro, estatus);

            if (lstCategorias == null) return NoContent();

            return Ok(_mapper.Map<List<CategoriasDTO>>(lstCategorias));

        }

        [HttpGet("{id:guid}", Name = "ObtenerCategoriaPorIdAsync")]
        public async Task<ActionResult> ObtenerCategoriaPorIdAsync([FromRoute]  Guid id)
        {

            var categoria = await _categoriaServicios.ObtenerCategoriaPorIdAsync(id);

            if (categoria == null) return NotFound("Categoría no encontrada");

            return Ok(_mapper.Map<CategoriasDTO>(categoria));

        }

        [HttpPost]
        public async Task<ActionResult> CrearCategoriaAsync([FromBody] CrearCategoriaDTO crearCategoriaDTO )
        {

            var categoria = await _categoriaServicios.ObtenerCategoriaPorNombreAsync(crearCategoriaDTO.Nombre);

            if (categoria != null) return BadRequest("La categoría ya ha sido registrada anteriormente");

            var crearCategoria = _mapper.Map<CategoriasEntidad>(crearCategoriaDTO);

            var respuesta = await _categoriaServicios.CrearCategoriaAsync(crearCategoria);

            if (respuesta == null) return NotFound("La categoria no se ha creado");

            var respuestaDTO = _mapper.Map<CategoriasDTO>(respuesta);

            return CreatedAtRoute("ObtenerCategoriaPorIdAsync", new { id = respuesta.Id }, respuestaDTO);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> ActualizarCategoriaAsync([FromRoute] Guid id, [FromBody] ActualizarCategoriaDTO actualizarCategoriaDTO )
        {

            if (id != actualizarCategoriaDTO.Id) return BadRequest("Modelo no válido");

            var existeCategoria = await _categoriaServicios.ObtenerCategoriaPorIdAsync(id);

            if (existeCategoria == null) return NotFound("Categoría no encontrada");

            var categoriaDTO = _mapper.Map<CategoriasEntidad>(actualizarCategoriaDTO);

            var respuesta = await _categoriaServicios.ActualizarCategoriaAsync(categoriaDTO);

            if(!respuesta)
            {

                return BadRequest("La categoría no pudo actualizarse correctamente");

            }

            return NoContent();

        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> EliminarCategoriaAsync([FromRoute] Guid id)
        {

            var categoria = await _categoriaServicios.ObtenerCategoriaPorIdAsync(id);

            if (categoria == null) return BadRequest("Categoría no encontrada");

            var respuesta = await _categoriaServicios.EliminarCategoriaAsync(categoria);

            if (!respuesta) return BadRequest("La categoría no pudo eliminarse correctamente");

            return Ok(_mapper.Map<CategoriasDTO>(categoria));

        }

        [HttpPost("filtrar")]
        public async Task<ActionResult> ObtenerFiltroCategoriasAsync([FromBody] FiltroCategoriaDTO filtro )
        {

            var lstCategorias = await _categoriaServicios.ObtenerFiltroCategoriasAsync(filtro.Nombre);

            if (lstCategorias == null) return NoContent();

            return Ok(_mapper.Map<List<CategoriasDTO>>(lstCategorias));

        }

        [HttpGet("{Id:guid}/activar-categoria")]
        public async Task<ActionResult> ActivarCategoriaAsync([FromRoute] Guid Id)
        {

            var existeCategoria = await _categoriaServicios.ObtenerCategoriaPorIdAsync(Id);

            if (existeCategoria == null) return NotFound("Categoría no encontrada");

            var respuesta = await _categoriaServicios.ActivarCategoriaAsync(Id);

            if (!respuesta) return BadRequest("La categoria no pudo activarse correctamente");

            return NoContent();

        }

    }
}