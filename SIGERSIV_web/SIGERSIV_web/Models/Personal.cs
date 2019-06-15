using System;
using System.Collections.Generic;

namespace SIGERSIV.Models
{
    public partial class Personal
    {
        public int IdPersonal { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int? Catalogo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
    }
}
