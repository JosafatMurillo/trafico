using System;
using System.Collections.Generic;

namespace ServicioTransito.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Reporte = new HashSet<Reporte>();
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string NumeroLicencia { get; set; }
        public string Telefono { get; set; }
        public string Contrasenia { get; set; }

        public ICollection<Reporte> Reporte { get; set; }
        public ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
