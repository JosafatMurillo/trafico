using System;
using System.Collections.Generic;

namespace ServicioTransito.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            InverseTipoCatalogoNavigation = new HashSet<Catalogo>();
            Personal = new HashSet<Personal>();
        }

        public int IdCatalogo { get; set; }
        public string Nombre { get; set; }
        public int? TipoCatalogo { get; set; }

        public Catalogo TipoCatalogoNavigation { get; set; }
        public ICollection<Catalogo> InverseTipoCatalogoNavigation { get; set; }
        public ICollection<Personal> Personal { get; set; }
    }
}
