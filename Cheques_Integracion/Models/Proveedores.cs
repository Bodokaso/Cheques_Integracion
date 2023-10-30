using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheques_Integracion.Models;

public partial class Proveedore
{
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string TipoPersona { get; set; }
    [Required]
    public string NumeroIdentificacion { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public double Balance { get; set; }
    [Required]
    public string CuentaContableProveedor { get; set; }
    [Required]
    public bool Estado { get; set; }

    public virtual ICollection<RegistroSolicitudCheque> RegistroSolicitudCheques { get; set; } = new List<RegistroSolicitudCheque>();
}
