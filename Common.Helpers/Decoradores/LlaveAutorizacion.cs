using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Decoradores
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LlaveAutorizacion : Attribute, IAsyncActionFilter
    {

        private const string _grantType = "grant_type";
        private const string _password = "password";
        private const string _userName = "username";

        /// <summary>
        /// LLAVES NECESARIAS PARA REGISTRARSE EN EL SERVICIO DE USUARIOS O REGISTRO DE PERSONAS
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (
                !context.HttpContext.Request.Headers.TryGetValue(_grantType, out var grantTypeCabecera) &&
                !context.HttpContext.Request.Headers.TryGetValue(_password, out var passwordCabecera) &&
                !context.HttpContext.Request.Headers.TryGetValue(_userName, out var userNameCabecera)
              )
            {

                context.Result = new UnauthorizedResult();

            }

            //ACCEDER AL ARCHIVO DE CONFIGURACION
            var configuracion = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var grantType = configuracion["LLaveAutorizacion:grant_type"];
            var password = configuracion["LLaveAutorizacion:username"];
            var username = configuracion["LLaveAutorizacion:password"];
       
            if(!grantType.Equals(grantTypeCabecera) &&
               !password.Equals(passwordCabecera) &&
               !username.Equals(_userName)
                )
            {

                context.Result = new UnauthorizedResult();

                return;

            }

            await next();

        }
    }
}
