using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIGERSIV.Models;
using SIGERSIV_web.Models;

namespace SIGERSIV_web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult Dashboard(String username, String contrasena)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:53884/Personal/Login");

            var postData = $"nombreUsuario={username}&contrasenia={contrasena}";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            Personal personal = JsonConvert.DeserializeObject<Personal>(responseString);

            ViewBag.empleado = personal;

            TodosReportes();

            return View("Dashboard");
        }

        public void TodosReportes()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:53884/Reporte/ObtenerTodos");

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            List<Reporte> reportes = JsonConvert.DeserializeObject<List<Reporte>>(responseString);
            List<Cliente> clientes = new List<Cliente>();

            foreach(Reporte r in reportes)
            {
                clientes.Add(ClientesPorReporte(r));
            }

            obtenerVistas(reportes, clientes);

            ViewBag.reportes = reportes;


        }

        public Cliente ClientesPorReporte(Reporte reporte)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:53884/Cliente/ObtenerPorId");

            int? id = reporte.Cliente;
            var postData = $"idCliente={id}";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(responseString);
            
            return cliente;
        }

        public void obtenerVistas(List<Reporte> reportes, List<Cliente> clientes)
        {
            int cont = 0;
            List<ReporteVista> vistas = new List<ReporteVista>();
            foreach(Reporte r in reportes)
            {
                ReporteVista vista = new ReporteVista();
                vista.Cliente = clientes[cont].Nombre + " " + clientes[cont].Apellidos;
                if(r.EstatusReporte == 7)
                {
                    vista.EstatusReporte = "Dictaminado";
                } else
                {
                    vista.EstatusReporte = "En espera";
                }
                vista.Lugar = r.Lugar;
                vistas.Add(vista);
                cont++;
            }

            ViewBag.reporteVista = vistas;
        }

        public ActionResult CerrarSesion()
        {
            return View("/Home/Index");
        }

        public ActionResult Detalles(int idReporte)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:53884/Reporte/ObtenerPorId");
            
            var postData = $"idReporte={idReporte}";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            Reporte reporteSeleccionado = JsonConvert.DeserializeObject<Reporte>(responseString);

            Cliente clienteReporte = obtenerClientePorReporte(reporteSeleccionado);

            Vehiculo vehiculoCliente = obtenerVehiculoPorCliente(reporteSeleccionado);

            VehiculoAgeno vehiculoAgenoSeleccionado = obtenerVehiculoAgenoPorReporte(reporteSeleccionado);

            ViewBag.cliente = clienteReporte;
            ViewBag.vehiculoCliente = vehiculoCliente;
            ViewBag.vehiculoAgeno = vehiculoAgenoSeleccionado;
            ViewBag.reporte = reporteSeleccionado;

            return View();
        }

        public VehiculoAgeno obtenerVehiculoAgenoPorReporte(Reporte reporteSeleccionado)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:53884/VehiculoAgeno/ObtenerPorId");

            var postData = $"idCliente={reporteSeleccionado.VehiculoAgeno}";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            VehiculoAgeno vehiculoAgeno = JsonConvert.DeserializeObject<VehiculoAgeno>(responseString);

            return vehiculoAgeno;
        }

        public Vehiculo obtenerVehiculoPorCliente(Reporte reporteSeleccionado)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:53884/Vehiculo/ObtenerPorIdVehiculo");

            var postData = $"idCliente={reporteSeleccionado.Vehiculo}";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            Vehiculo vehiculoCliente = JsonConvert.DeserializeObject<Vehiculo>(responseString);

            return vehiculoCliente;
        }

        public Cliente obtenerClientePorReporte(Reporte reporte)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:53884/Cliente/ObtenerPorId");

            var postData = $"idCliente={reporte.Cliente}";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            Cliente clienteReporte = JsonConvert.DeserializeObject<Cliente>(responseString);

            return clienteReporte;
        }

        public void Dictaminar(String descripcion, int idReporte)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:53884/Dictamen/Registrar");

            Personal personal = ViewBag.empleado;
            String folio = "folio n";


            var postData = $"personal={personal.IdPersonal}&reporte={idReporte}&folio={folio}&descripcion={descripcion}&nombrePerito={personal.Nombre + " " + personal.Apellidos}";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            Mensaje mensajeRespuesta = JsonConvert.DeserializeObject<Mensaje>(responseString);
        }
    }
}
