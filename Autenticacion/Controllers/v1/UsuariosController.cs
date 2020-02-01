using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autenticacion.Dominio.Repositorio.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacion.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

       

        public UsuariosController(IUsuariosInformacionRepositorio usuariosInformacionRepositorio)
        {

            

        }

        [HttpGet]
        public IActionResult ObtenerUsuarios()
        {


            return Ok();

        }


    }
}