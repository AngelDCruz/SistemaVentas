using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Dominio.Entidades
{
    public class Usuarios : IdentityUser<Guid>, IAuditoria
    {


        [Required]
        [Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid UsuarioCreacion { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaCreacion { get; set; }

        [Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid UsuarioModificacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        [Required]
        [DefaultValue("Act")]
        [Column(TypeName = "CHAR(3)")]
        public string Estatus { get; set; }


        public List<UsuariosReclamaciones> UsuariosReclamaciones { get; set; }

        public List<UsuarioLogin> UsuarioLogin { get; set; }

        public List<UsuariosToken> UsuariosTokens { get; set; }

        public List<UsuariosRoles> UsuariosRoles { get; set; }

    }
}
