using Autenticacion.Dominio.Entidades;
using Autenticacion.Infraestructura;
using Microsoft.AspNetCore.Identity;
using SistemaVentas.Infraestructura.Transversal.Helpers.Encriptar_Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaVentas.Infraestructura
{
    public class InicializarDatos
    {

      
        public static  void CrearDatos(AppDbContext context)
        {

            context.Database.EnsureCreated();

            if(context.Roles.Any())
            {
                return;
            }

            var lstRoles = new List<RolesEntidad>
            {
                new RolesEntidad
                {
                    Name = "Administrador",
                    Estatus = "Act"
                },
                new RolesEntidad
                {
                    Name = "Super Usuario",
                    Estatus = "Act"
                }
            };

            context.Roles.AddRange(lstRoles);

            context.SaveChanges();

        }

    }
}
