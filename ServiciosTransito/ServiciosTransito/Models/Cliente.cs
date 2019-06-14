using System;
using System.Collections.Generic;

namespace ServiciosTransito.Models
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string NumeroLicencia { get; set; }
        public string Telefono { get; set; }
        public string Contrasenia { get; set; }
    }
}
