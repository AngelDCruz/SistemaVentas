using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaVentas.Dominio.Entidades
{
    public  class DetalleIngresoEntidad : IAuditoria
    {

        public Guid Id { get; set; }

        public Guid IngresosId { get; set; }

        public Guid ProductosId { get; set; }

        public int Cantidad { get; set; }

        public double Precio { get; set; }

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


        public IngresoEntidad Ingresos { get; set; }

        public ProductosEntidad Productos { get; set; }


    }
}
