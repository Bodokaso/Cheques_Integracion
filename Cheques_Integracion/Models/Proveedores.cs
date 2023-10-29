using System;
using System.Collections.Generic;

namespace Cheques_Integracion.Models;

public partial class Proveedore
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? TipoPersona { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public decimal? Balance { get; set; }

    public string? CuentaContableProveedor { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<RegistroSolicitudCheque> RegistroSolicitudCheques { get; set; } = new List<RegistroSolicitudCheque>();
}
