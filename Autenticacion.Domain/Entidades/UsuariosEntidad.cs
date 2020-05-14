using Autenticacion.Infraestructura.EntidadesConfiguracion;
using Microsoft.AspNetCore.Identity;
using SistemaVentas.Dominio.Entidades;
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

        public override Guid Id { get; set; }

        [Column(TypeName = "VARCHAR(MAX)")]
        public string ImagenPerfil { get; set; }

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

        public List<UsuariosRolesEntidad> UsuariosRoles { get; set; }

        public List<TokenEntidad> Token { get; set; }

        public List<IngresoEntidad> Ingresos { get; set; }

        public List<VentaEntidad> Ventas { get; set; }

    }
}
