using Android.Graphics.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGERSIV.Models
{
    public class ReporteVista : Java.Lang.Object
    {
        public Drawable Foto { get; set; }
        public String Lugar { get; set; }
        public String Cliente { get; set; }
        public String EstatusReporte { get; set; }
    }
}
