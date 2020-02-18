using Autenticacion.Infraestructura.EntidadesConfiguracion;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Dominio.Entidades
{
    public class UsuariosEntidad : IdentityUser<Guid>, IAuditoria
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


        public List<UsuariosReclamacionesEntidad> UsuariosReclamaciones { get; set; }

        public List<UsuarioLoginEntidad> UsuarioLogin { get; set; }

        public List<UsuariosTokenEntidad> UsuariosTokens { get; set; }

        public override Guid Id { get; set; }

        public List<UsuariosRolesEntidad> UsuariosRoles { get; set; }

        public List<TokenEntidad> Token { get; set; }

    }
}
