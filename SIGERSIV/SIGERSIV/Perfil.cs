using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;
using SIGERSIV.Models;

namespace SIGERSIV
{
    public class Perfil : Fragment
    {
        private static List<Vehiculo> Vehiculos { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.perfil, container, false);

            var listaVehiculos = (ListView)view.FindViewById(Resource.Id.listaVehiculos);

            Vehiculos = new List<Vehiculo>();

            Cliente cliente = MainActivity.Cliente;

            var nombreUsuario = (TextView)view.FindViewById(Resource.Id.nombreUsuario);

            nombreUsuario.Text = $"{cliente.Nombre} {cliente.Apellidos}";

            if (cliente != null)
            {
                var request = (HttpWebRequest)WebRequest.Create("http://192.168.43.74/Vehiculo/ObtenerPorCliente");

                var postData = $"idCliente={cliente.IdCliente}";
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

                    Vehiculos = JsonConvert.DeserializeObject<List<Vehiculo>>(responseString);
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

            AdaptadorVehiculos adaptador = new AdaptadorVehiculos(this.Activity, Vehiculos);
            listaVehiculos.Adapter = adaptador;

            return view;
        }

        class AdaptadorVehiculos : BaseAdapter<Vehiculo>
        {

            private LayoutInflater inflador;
            private List<Vehiculo> vehiculos;

            public AdaptadorVehiculos(Context contexto, List<Vehiculo> vehiculos)
            {
                this.vehiculos = vehiculos;
                this.inflador = (LayoutInflater)contexto.GetSystemService(Context.LayoutInflaterService);
            }

            public override Vehiculo this[int position] => throw new NotImplementedException();

            public override int Count => this.vehiculos.Count;

            public override Java.Lang.Object GetItem(int position)
            {
                return vehiculos.ElementAt(position);
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                Vehiculo vehiculo = vehiculos.ElementAt(position);

                if (convertView == null)
                {
                    convertView = inflador.Inflate(Resource.Layout.vehiculo_list_item, null);
                }

                TextView marcaLabel = (TextView)convertView.FindViewById(Resource.Id.marcalabel);
                TextView modeloLabel = (TextView)convertView.FindViewById(Resource.Id.modelolabel);
                TextView anioLabel = (TextView)convertView.FindViewById(Resource.Id.aniolabel);
                TextView placasLabel = (TextView)convertView.FindViewById(Resource.Id.placaslabel);

                marcaLabel.Text = vehiculo.Marca;
                modeloLabel.Text = vehiculo.Modelo;
                anioLabel.Text = vehiculo.Anio;
                placasLabel.Text = vehiculo.NumPlacas;

                return convertView;
            }
        }
    }
}