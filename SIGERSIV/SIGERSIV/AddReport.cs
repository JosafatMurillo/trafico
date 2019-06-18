using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SIGERSIV.Models;

namespace SIGERSIV
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class AddReport : Android.Support.V7.App.AppCompatActivity
    {

        private LinearLayout galery;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.reporte);

            var hideImagesBtn = (Button)FindViewById(Resource.Id.hideImagesBtn);
            var guardarBtn = (Button)FindViewById(Resource.Id.guardarBtn);
            galery = (LinearLayout)FindViewById(Resource.Id.galeryPane);

            hideImagesBtn.Click += hideOrShowGalery;
            guardarBtn.Click += registrarReporte;
        }

        public void hideOrShowGalery(object sender, EventArgs eventArgs)
        {
            if(galery.IsShown)
            {
                galery.Visibility = ViewStates.Invisible;
            }else
            {
                galery.Visibility = ViewStates.Visible;
            }
        }

        public void registrarReporte(object sender, EventArgs eventArgs)
        {
            Reporte reporte = new Reporte();

            var lugarTxt = (EditText)FindViewById(Resource.Id.lugarTxt);
            var involucradoTxt = (EditText)FindViewById(Resource.Id.involucradoTxt);
            var aseguradoraTxt = (EditText)FindViewById(Resource.Id.aseguradoraTxt);
            var polizaTxt = (EditText)FindViewById(Resource.Id.polizaTxt);
            var marcaTxt = (EditText)FindViewById(Resource.Id.marcaTxt);
            var modeloTxt = (EditText)FindViewById(Resource.Id.modeloTxt);
            var colorTxt = (EditText)FindViewById(Resource.Id.colorTxt);
            var numPlacasTxt = (EditText)FindViewById(Resource.Id.numPlacasTxt);

            //Vehiculo ageno

            var request1 = (HttpWebRequest)WebRequest.Create("http://192.168.43.74/VehiculoAgeno/Agregar");

            var postData1 = $"color={colorTxt.Text}&marca={marcaTxt.Text}&modelo={modeloTxt.Text}&nombreAseguradora={aseguradoraTxt.Text}" +
                $"&numeroPlacas={numPlacasTxt.Text}&numPoliza={polizaTxt.Text}";
            var data1 = Encoding.ASCII.GetBytes(postData1);

            request1.Method = "POST";
            request1.ContentType = "application/x-www-form-urlencoded";
            request1.ContentLength = data1.Length;

            using (var stream = request1.GetRequestStream())
            {
                stream.Write(data1, 0, data1.Length);
            }

            var response = (HttpWebResponse)request1.GetResponse();

            if(response.StatusCode == HttpStatusCode.OK)
            {
                var request2 = (HttpWebRequest)WebRequest.Create("http://192.168.43.74/Reporte/Agregar");

                var postData2 = $"cliente={MainActivity.Cliente.IdCliente}&lugar={lugarTxt.Text}&nombreConductor={involucradoTxt.Text}" +
                    $"&vehiculo=1&vehiculoAgeno=1";
                var data2 = Encoding.ASCII.GetBytes(postData2);

                request2.Method = "POST";
                request2.ContentType = "application/x-www-form-urlencoded";
                request2.ContentLength = data2.Length;

                using (var stream = request2.GetRequestStream())
                {
                    stream.Write(data2, 0, data2.Length);
                }

                var response2 = (HttpWebResponse)request2.GetResponse();
                this.Finish();
            }
        }

    }
}