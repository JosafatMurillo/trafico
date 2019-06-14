using System;
using System.Collections.Generic;

namespace ServiciosTransito.Models
{
    public partial class Reporte
    {
        public int IdReporte { get; set; }
        public int? Vehiculo { get; set; }
        public string Lugar { get; set; }
        public string NombreConductorAgeno { get; set; }
        public int? Cliente { get; set; }
        public int? EstatusReporte { get; set; }
        public int? VehiculoAgeno { get; set; }
    }
}
