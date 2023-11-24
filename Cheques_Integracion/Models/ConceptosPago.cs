using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cheques_Integracion.Models

{
    public class ConceptosPago
    {
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}


