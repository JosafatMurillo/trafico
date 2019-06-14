using System;
using System.Collections.Generic;

namespace ServicioTransito.Models
{
    public partial class Personal
    {
        public Personal()
        {
            Dictamen = new HashSet<Dictamen>();
        }

        public int IdPersonal { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int? Catalogo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }

        public Catalogo CatalogoNavigation { get; set; }
        public ICollection<Dictamen> Dictamen { get; set; }
    }
}
