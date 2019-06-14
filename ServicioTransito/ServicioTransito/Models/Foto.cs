using System;
using System.Collections.Generic;

namespace ServicioTransito.Models
{
    public partial class Foto
    {
        public int IdFoto { get; set; }
        public string Foto1 { get; set; }
        public int? Reporte { get; set; }

        public Reporte ReporteNavigation { get; set; }
    }
}
