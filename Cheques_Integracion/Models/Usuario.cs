using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheques_Integracion.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }
    [Required]
    public string NombreCompleto { get; set; }
    [Required]
    public string Correo { get; set; }
    [Required]
    public string Clave { get; set; }
}
