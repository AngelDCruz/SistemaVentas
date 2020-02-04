using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Dominio.Entidades
{
    public class Roles : IdentityRole<Guid>, IAuditoria
    {

        [Required]
        [Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid UsuarioCreacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid UsuarioModificacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        [Required]
        [DefaultValue("Act")]
        [Column(TypeName = "CHAR(3)")]
        public string Estatus { get; set; }


        public List<UsuariosRoles> UsuariosRoles { get; set; }

        public List<RolesReclamaciones> RoleReclamacion { get; set; }

    }
}
