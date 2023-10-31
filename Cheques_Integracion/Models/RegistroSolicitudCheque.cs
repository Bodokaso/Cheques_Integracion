using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheques_Integracion.Models;

public class RegistroSolicitudCheque
{
    public int Id { get; set; }
    [Required]
    public string NumeroSolicitud { get; set; }
    [Required]
    public int Proveedor { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public double Monto { get; set; }
    [Required]
    public DateTime FechaRegistro { get; set; }
    [Required]
    public string Estado { get; set; }
    [Required]
    public string CuentaContableProveedor { get; set; }
    [Required]
    public string CuentaContableRelacionada { get; set; }

    public virtual Proveedore? ProveedorNavigation { get; set; }
}
