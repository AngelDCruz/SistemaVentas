using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Autenticacion.Dominio.Entidades
{
    public class UsuariosReclamaciones : IdentityUserClaim<Guid>, IAuditoria
    {

        [Key]
        [Required]
        [Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

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


        public Usuarios Usuarios { get; set; }

    }
}
