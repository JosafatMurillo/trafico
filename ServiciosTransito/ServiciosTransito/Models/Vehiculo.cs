using System;
using System.Collections.Generic;

namespace ServiciosTransito.Models
{
    public partial class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public int? Cliente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Anio { get; set; }
        public string NombreAseguradora { get; set; }
        public string NumeroPoliza { get; set; }
        public string NumeroPlacas { get; set; }
    }
}
