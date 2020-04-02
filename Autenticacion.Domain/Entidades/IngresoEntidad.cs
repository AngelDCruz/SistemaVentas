using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaVentas.Dominio.Entidades
{
    public class IngresoEntidad : IAuditoria
    {

        public Guid Id { get; set; }

        public Guid UsuariosId { get; set; }

        public string TipoComprobante { get; set; }

        public string SerieComprobante { get; set; }

        public double Impuesto { get; set; }

        public double Total { get; set; }

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

        public List<DetalleIngresoEntidad> DetalleIngresos { get; set; }

        public Guid PersonasId { get; set; }

        public PersonaEntidad Personas { get; set; }

    }
}
