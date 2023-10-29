using System;
using System.Collections.Generic;

namespace Cheques_Integracion.Models;

public partial class ConceptosPago
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }
}
