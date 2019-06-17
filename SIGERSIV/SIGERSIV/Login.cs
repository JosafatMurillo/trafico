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
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SIGERSIV.Models;

namespace SIGERSIV
{
    [Activity(Label = "Login", MainLauncher = true)]
    public class Login : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login_activity);

            var aceptarBtn = (Button)FindViewById(Resource.Id.aceptarBtn);

            aceptarBtn.Click += DoLogin;

        }

        public void DoLogin(object sender, EventArgs eventArgs)
        {

            var phoneTxt = (EditText)FindViewById(Resource.Id.phoneTxt);
            var passwordTxt = (EditText)FindViewById(Resource.Id.passwordTxt);

            var phone = phoneTxt.Text;
            var contrasena = passwordTxt.Text;

            if(phone != null && contrasena != null)
            {
                var request = (HttpWebRequest)WebRequest.Create("http://192.168.43.74/Cliente/Login");

                var postData = $"telefono={phone}&contrasenia={contrasena}";
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    Cliente cliente = JsonConvert.DeserializeObject<Cliente>(responseString);

                    MainActivity.Cliente = cliente;

                    StartActivity(typeof(MainActivity));
                }
                else if(response.StatusCode == HttpStatusCode.NoContent)
                {
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Usuario no encontrado");
                    dialog.SetMessage("El teléfono y contraseña introducidos no coiciden con ningún registro. " +
                        "Inténtelo nuevamente.");
                    dialog.SetNegativeButton("Aceptar", (senderAlert, args) => {
                        dialog.Dispose();
                    });
                    Dialog diag = dialog.Create();
                    diag.Show();
                }
            }

        }


    }
}