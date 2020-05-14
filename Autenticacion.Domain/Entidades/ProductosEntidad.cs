using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using Autenticacion.Dominio.Entidades;

namespace SistemaVentas.Dominio.Entidades
{
    public class ProductosEntidad : IAuditoria
    {

        public Guid Id {get; set; }

        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Imagen { get; set; }

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


        //  PROPIEDADES DE NAVEGACION

        public Guid CategoriaId { get; set; }

        public CategoriasEntidad Categorias { get; set; }

        public List<DetalleIngresoEntidad> DetalleIngresos { get; set; }

        public List<DetalleVentaEntidad> DetalleVentas { get; set; }

    }
}
