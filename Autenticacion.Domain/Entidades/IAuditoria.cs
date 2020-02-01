using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Dominio.Entidades
{
    public interface IAuditoria
    {

        [Required]
        [Column(TypeName = "UNIQUEIDENTIFIER")]
        Guid UsuarioCreacion { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        DateTime FechaCreacion { get; set; }

        [Column(TypeName = "UNIQUEIDENTIFIER")]
        Guid UsuarioModificacion { get; set; }

        DateTime FechaModificacion { get; set; }

        [Required]
        [DefaultValue("Act")]
        [Column(TypeName = "CHAR(3)")]
        string Estatus { get; set; }

    }
}
