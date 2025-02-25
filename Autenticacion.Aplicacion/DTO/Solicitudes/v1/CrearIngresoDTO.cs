﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class CrearIngresoDTO
    {

        [Required(ErrorMessage = "El proveedor es requerido")]
        public Guid PersonaId { get; set; }

        [Required(ErrorMessage = "El tipo de comprobante es requerido")]
        public string TipoComprobante { get; set; }

        [Required(ErrorMessage = "El número de serie o comprobante es requerido")]
        public string SerieComprobante { get; set; }

        [Required(ErrorMessage = "El impuesto es requerido")]
        public double Impuesto { get; set; }

        [Required(ErrorMessage = "El total es requerido")]
        public double Total { get; set; }

        public List< CrearDetalleIngresoDTO> Detalles { get; set; }

    }
}
