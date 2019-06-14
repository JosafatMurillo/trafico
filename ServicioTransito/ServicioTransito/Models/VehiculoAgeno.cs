using System;
using System.Collections.Generic;

namespace ServicioTransito.Models
{
    public partial class VehiculoAgeno
    {
        public int IdVehiculoAgeno { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Anio { get; set; }
        public string NombreAseguradora { get; set; }
        public string NumeroPoliza { get; set; }
        public string NumeroPlacas { get; set; }
        public int? Reporte { get; set; }

        public Reporte ReporteNavigation { get; set; }
    }
}
