using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Newtonsoft.Json;
using SIGERSIV.Models;

namespace SIGERSIV
{
    public class Home : Fragment
    {
        private List<ReporteVista> reportes;
        private ListView listaReportes;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var view = inflater.Inflate(Resource.Layout.home, container, false);

            listaReportes = (ListView)view.FindViewById(Resource.Id.listaReportes);
            TextView empty = (TextView)view.FindViewById(Resource.Id.empty);

            reportes = new List<ReporteVista>();

            Cliente cliente = MainActivity.Cliente;

            if (cliente != null)
            {
                var request = (HttpWebRequest)WebRequest.Create("http://192.168.43.74/Reporte/ObtenerPorCliente");

                var postData = $"cliente={cliente.IdCliente}";
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    List<Reporte> reportesResp = JsonConvert.DeserializeObject<List<Reporte>>(responseString);

                    foreach(Reporte reporte in reportesResp)
                    {
                        ReporteVista reporteV = new ReporteVista
                        {
                            Foto = this.Activity.GetDrawable(Resource.Drawable.carrousel1),
                            Lugar = reporte.Lugar,
                            Cliente = $"{cliente.Nombre} {cliente.Apellidos}",
                            EstatusReporte = reporte.EstatusReporte == 0 ? "No determinado" : "Determinado"
                        };

                        reportes.Add(reporteV);
                    }
                }
                else
                {
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this.Activity);
                    dialog.SetTitle("Error al obtener los reportes");
                    dialog.SetMessage("Se generó un error al obtener los reportes." +
                        "Inténtelo nuevamente en unos minutos.");
                    dialog.SetNegativeButton("Aceptar", (senderAlert, args) => {
                        dialog.Dispose();
                    });
                    Dialog diag = dialog.Create();
                    diag.Show();
                }
            }

            if (reportes != null || reportes.Count > 0)
            {
                empty.Visibility = ViewStates.Invisible;
            }

            AdaptadorReportes adaptador = new AdaptadorReportes(this.Activity, reportes);
            listaReportes.Adapter = adaptador;

            return view;
        }

        class AdaptadorReportes : BaseAdapter<ReporteVista>
        {

            private LayoutInflater inflador;
            private List<ReporteVista> reportes;

            public AdaptadorReportes(Context contexto,List<ReporteVista> reportes)
            {
                this.reportes = reportes;
                this.inflador = (LayoutInflater)contexto.GetSystemService(Context.LayoutInflaterService);
            }

            public override ReporteVista this[int position] => throw new NotImplementedException();

            public override int Count => this.reportes.Count;

            public override Java.Lang.Object GetItem(int position)
            {
                return reportes.ElementAt(position);
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                ReporteVista reporte = reportes.ElementAt(position);

                if(convertView == null)
                {
                    convertView = inflador.Inflate(Resource.Layout.reporte_list_item, null);
                }

                TextView lugar = (TextView)convertView.FindViewById(Resource.Id.lugar);
                TextView cliente = (TextView)convertView.FindViewById(Resource.Id.cliente);
                ImageView foto = (ImageView)convertView.FindViewById(Resource.Id.foto);

                foto.SetImageDrawable(reporte.Foto);
                foto.SetScaleType(ImageView.ScaleType.FitEnd);
                lugar.Text = reporte.Lugar;
                cliente.Text = reporte.Cliente;

                return convertView;
            }
        }
    }
}