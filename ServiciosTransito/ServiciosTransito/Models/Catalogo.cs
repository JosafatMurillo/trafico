using System;
using System.Collections.Generic;

namespace ServiciosTransito.Models
{
    public partial class Catalogo
    {
        public int IdCatalogo { get; set; }
        public string Nombre { get; set; }
        public int? TipoCatalogo { get; set; }
    }
}
