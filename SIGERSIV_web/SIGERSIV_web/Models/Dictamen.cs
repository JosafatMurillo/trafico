using System;
using System.Collections.Generic;

namespace SIGERSIV.Models
{
    public partial class Dictamen
    {
        public int IdDictamen { get; set; }
        public int? Personal { get; set; }
        public int? Reporte { get; set; }
        public string Folio { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaHora { get; set; }
        public string NombrePerito { get; set; }
    }
}
