using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cheques_Integracion.Models

{
    public class RegistroSolicitudCheque
    {
        public int Id { get; set; }
        [Required]
        public string NumeroSolicitud { get; set; }
        [Required]
        public string Proveedor { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public double Monto { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        [NotMapped]
        public string FechaRegistroString => FechaRegistro.ToShortDateString();
        [Required]
        public string Estado { get; set; }
        [Required]
        public string CuentaContableProveedor { get; set; }
        [Required]
        public string CuentaContableRelacionada { get; set; }

        public virtual Proveedore? ProveedorNavigation { get; set; }
    }
}


