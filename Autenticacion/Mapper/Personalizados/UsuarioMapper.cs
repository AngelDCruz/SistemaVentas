
using System.Collections.Generic;
using System.Linq;

using Autenticacion.Aplicacion.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;

namespace Autenticacion.Api.Mapper.Personalizados
{
    public class UsuarioMapper
    {

        public static UsuariosEntidad Map(CrearUsuarioDTO entidad)
        {

            UsuariosEntidad usuario = new UsuariosEntidad();
            var lstUsuariosRoles = new List<UsuariosRolesEntidad>();

            if(entidad != null)
            {

                usuario.UserName = entidad.Usuario;
                usuario.PasswordHash = entidad.Contrasena;
                usuario.Email = entidad.Email;

                if (usuario.DatosPersonales != null)
                {

                    usuario.DatosPersonales = new DatosPersonalesEntidad
                    {
                        Nombre = entidad.DatosPersonales.Nombre,
                        ApellidoPaterno = entidad.DatosPersonales.ApellidoMaterno,
                        ApellidoMaterno = entidad.DatosPersonales.ApellidoMaterno,
                        Pais = entidad.DatosPersonales.Pais,
                        Ciudad = entidad.DatosPersonales.Ciudad,
                        Calle = entidad.DatosPersonales.Calle,
                        Telefono = entidad.DatosPersonales.Telefono,
                    };

                }

                if (entidad.Roles.Count > 0)
                {

                    foreach (var role in entidad.Roles)
                    {

                        lstUsuariosRoles.Add(new UsuariosRolesEntidad
                        {
                            UserId = usuario.Id,
                            RoleId = role
                        });

                    }

                    usuario.UsuariosRoles = lstUsuariosRoles;

                }

            

            }

            return usuario;

        }

    }
}
