using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Perfil : Fragment
    {
        private List<Vehiculo> vehiculos;

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

            this.vehiculos = new List<Vehiculo>();

            Vehiculo vehiculo = new Vehiculo
            {
                Marca = "Volkswagen",
                Modelo = "Beetle",
                Anio = "2000",
                NumPlacas = "YX-22390"
            };

            vehiculos.Add(vehiculo);

            AdaptadorVehiculos adaptador = new AdaptadorVehiculos(this.Activity, vehiculos);
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