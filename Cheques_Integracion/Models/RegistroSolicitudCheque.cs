using System;
using System.Collections.Generic;

namespace Cheques_Integracion.Models;

public partial class RegistroSolicitudCheque
{
    public int NumeroSolicitud { get; set; }

    public int? Proveedor { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Estado { get; set; }

    public string? CuentaContableProveedor { get; set; }

    public string? CuentaContableRelacionada { get; set; }

    public virtual Proveedores? ProveedorNavigation { get; set; }
}
