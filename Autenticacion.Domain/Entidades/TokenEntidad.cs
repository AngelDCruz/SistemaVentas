using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    [Table(name: "Token", Schema ="Autenticacion")]
    public class TokenEntidad
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Token { get; set; }

        public string JwtId { get; set; }

        public bool Usado { get; set; }

        public bool Valido { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime Expiracion { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UsuariosEntidad Usuarios { get; set; }

    }
}
