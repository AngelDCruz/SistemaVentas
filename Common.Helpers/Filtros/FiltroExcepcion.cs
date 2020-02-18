using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Filtros
{
    public class FiltroExcepcion : ExceptionFilterAttribute
    {


        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }


    }
}
