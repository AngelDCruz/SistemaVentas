using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticacion.Dominio.Swagger
{
    public class ConvencionAgrupamientoVersiones : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            // Ejemplo: "Controllers.V1"
            var controllerNamespace = controller.ControllerType.Namespace;
            var apiVersion = controllerNamespace.Split('.').Last().ToLower();
            controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}
