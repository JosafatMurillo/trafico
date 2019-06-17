using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SIGERSIV.Models
{
    class Vehiculo : Java.Lang.Object
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Anio { get; set; }
        public string Color { get; set; }
        public bool TieneAseguradora { get; set; }
        public string PolizaSeguro { get; set; }
        public string NumPlacas { get; set; }

    }
}