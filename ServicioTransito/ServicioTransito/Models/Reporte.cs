using System;
using System.Collections.Generic;

namespace ServicioTransito.Models
{
    public partial class Reporte
    {
        public Reporte()
        {
            Dictamen = new HashSet<Dictamen>();
            Foto = new HashSet<Foto>();
            VehiculoAgeno = new HashSet<VehiculoAgeno>();
        }

        public int IdReporte { get; set; }
        public int? Vehiculo { get; set; }
        public string Lugar { get; set; }
        public string NombreConductorAgeno { get; set; }
        public int? Cliente { get; set; }

        public Cliente ClienteNavigation { get; set; }
        public Vehiculo VehiculoNavigation { get; set; }
        public ICollection<Dictamen> Dictamen { get; set; }
        public ICollection<Foto> Foto { get; set; }
        public ICollection<VehiculoAgeno> VehiculoAgeno { get; set; }
    }
}
