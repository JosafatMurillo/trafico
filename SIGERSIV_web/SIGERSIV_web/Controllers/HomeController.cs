using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIGERSIV.Models;
using SIGERSIV_web.Models;

namespace SIGERSIV_web.Controllers
{
    public class HomeController : Controller
    {
        private static String url = "http://192.168.43.17";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Chat(int idPersonal, string mensaje, string nombreUsuario, List<string> mensajes)
        {
            ViewBag.nombreUsuario = nombreUsuario;
            Personal p = obtenerPersonalPorId(idPersonal);
            if (p != null)
            {
                conectar(p.NombreUsuario);
            } else
            {
                //mensajes.Add(nombreUsuario + ": " + mensaje);
                conectar(nombreUsuario + ": " + mensaje + "\n");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Registrarse(string apellidos, string contrasenia, string nombre, string nombreUsuario)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/Personal/Agregar");

            var postData = $"apellidos={apellidos}&contrasenia={contrasenia}&catalogo={2}&nombre={nombre}&nombreUsuario={nombreUsuario}";
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

            Personal iniciado = JsonConvert.DeserializeObject<Personal>(responseString);

            Json(responseString);
            return View("Login");
        }

        [HttpPost]
        public IActionResult Detalles(int idReporte, int idPersonal)
        {
            ViewBag.id = idPersonal;
            Det(idReporte);
            return View();
        }

        public IActionResult Determinar(int idReporte, int idPersonal)
        {
            Personal personal = obtenerPersonalPorId(idPersonal);
            ViewBag.idP = personal.IdPersonal;
            Reporte reporte = obtenerReportePorId(idReporte);
            ViewBag.prueba = reporte;
            Dictamen d = tieneDictamen(idReporte);
            ViewBag.id = idReporte;
            ViewBag.dictamen = d;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Dash()
        {
            return View("Dashboard");
        }

        [HttpPost]
        public ActionResult Dashboard(String username, String contrasena)
        {
            if (username == null || contrasena == null)
            {
                return View("Login");
            }

            var request = (HttpWebRequest)WebRequest.Create(url + "/Personal/Login");

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
            ViewBag.idPersonal = personal.IdPersonal;

            if (personal == null)
            {
                return View("Login");
            }
            else
            {
                TodosReportes();

                return View("Dashboard");
            }
        }

        public void TodosReportes()
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/Reporte/ObtenerTodos");

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Json(responseString);

            List<Reporte> reportes = JsonConvert.DeserializeObject<List<Reporte>>(responseString);
            List<Cliente> clientes = new List<Cliente>();

            foreach (Reporte r in reportes)
            {
                clientes.Add(ClientesPorReporte(r));
            }

            obtenerVistas(reportes, clientes);

            ViewBag.reportes = reportes;


        }

        public Cliente ClientesPorReporte(Reporte reporte)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/Cliente/ObtenerPorId");

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
            int cont = clientes.Count;
            int ide = 0;
            List<ReporteVista> vistas = new List<ReporteVista>();
            foreach (Reporte r in reportes)
            {
                ReporteVista vista = new ReporteVista();
                if (ide < cont)
                {
                    vista.Cliente = clientes[ide].Nombre + " " + clientes[ide].Apellidos;
                }
                if (r.EstatusReporte == 7)
                {
                    vista.EstatusReporte = "Dictaminado";
                }
                else
                {
                    vista.EstatusReporte = "En espera";
                }
                vista.Lugar = r.Lugar;
                vista.idReporte = r.IdReporte;
                vistas.Add(vista);
                ide++;
            }

            ViewBag.reporteVista = vistas;
        }

        public ReporteVista obtenerVistaIndividual(Reporte r, Cliente c)
        {
            ReporteVista vista = new ReporteVista();
            vista.Cliente = c.Nombre + " " + c.Apellidos;
            if (r.EstatusReporte == 7)
            {
                vista.EstatusReporte = "Dictaminado";
            }
            else
            {
                vista.EstatusReporte = "En espera";
            }
            vista.Lugar = r.Lugar;
            vista.idReporte = r.IdReporte;
            return vista;
        }

        public ActionResult CerrarSesion()
        {
            return View("/Home/Index");
        }

        [HttpPost]
        public IActionResult Det(int idReporte)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/Reporte/ObtenerPorId");

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

            Vehiculo vehiculoCliente = obtenerVehiculoPorReporte(reporteSeleccionado);

            VehiculoAgeno vehiculoAgenoSeleccionado = obtenerVehiculoAgenoPorReporte(reporteSeleccionado);

            ViewBag.cliente = clienteReporte;
            ViewBag.vehiculoCliente = vehiculoCliente;
            ViewBag.vehiculoAgeno = vehiculoAgenoSeleccionado;
            ViewBag.reporte = reporteSeleccionado;
            ViewBag.datos = obtenerVistaIndividual(reporteSeleccionado, clienteReporte);

            return View();
        }

        public VehiculoAgeno obtenerVehiculoAgenoPorReporte(Reporte reporteSeleccionado)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/VehiculoAgeno/ObtenerPorId");

            var postData = $"idVehiculo={reporteSeleccionado.VehiculoAgeno}";
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

        public Vehiculo obtenerVehiculoPorReporte(Reporte reporteSeleccionado)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/Vehiculo/ObtenerPorIdVehiculo");

            var postData = $"idVehiculo={reporteSeleccionado.Vehiculo}";
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
            var request = (HttpWebRequest)WebRequest.Create(url + "/Cliente/ObtenerPorId");

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

        [HttpPost]
        public IActionResult Dictaminar(String justificacion, int idReporte, int idPersonal)
        {
            Dictamen d = tieneDictamen(idReporte);
            if (d == null)
            {
                var request = (HttpWebRequest)WebRequest.Create(url + "/Dictamen/Registrar");

                Personal p = obtenerPersonalPorId(idPersonal);

                String folio = "folio n";

                var postData = $"personal={p.IdPersonal}&reporte={idReporte}&folio={folio}&descripcion={justificacion}&nombrePerito={p.Nombre + " " + p.Apellidos}";
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

                if (mensajeRespuesta.correcto)
                {
                    Reporte reporte = obtenerReportePorId(idReporte);
                    actualizarEstatus(reporte);
                }
            }
            return View("Login");
        }

        public Reporte obtenerReportePorId(int idReporte)
        {

            var request = (HttpWebRequest)WebRequest.Create(url + "/Reporte/ObtenerPorId");

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

            Reporte reporte = JsonConvert.DeserializeObject<Reporte>(responseString);

            return reporte;
        }

        public Personal obtenerPersonalPorId(int idPersonal)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/Personal/ObtenerPorId");

            var postData = $"idPersonal={idPersonal}";
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

            return personal;
        }

        public void actualizarEstatus(Reporte reporte)
        {
            var cliente = reporte.Cliente;
            var lugar = reporte.Lugar;
            var nombreConductor = reporte.NombreConductorAgeno;
            var vehiculo = reporte.Vehiculo;
            var vehiculoAgeno = reporte.VehiculoAgeno;
            var idReporte = reporte.IdReporte;

            var request = (HttpWebRequest)WebRequest.Create(url + "/Reporte/Actualizar");

            var postData = $"cliente={cliente}&lugar={lugar}&nombreConductor={nombreConductor}&vehiculo={vehiculo}&vehiculoAgeno={vehiculoAgeno}&idReporte={idReporte}";
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
        }

        public Dictamen tieneDictamen(int idReporte)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "/Dictamen/ObtenerPorReporte");
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

            Dictamen d = JsonConvert.DeserializeObject<Dictamen>(responseString);

            return d;
        }

        //Sección del chat
        static private NetworkStream stream;
        static private StreamWriter streamw;
        static private StreamReader streamr;
        static private TcpClient cliente = new TcpClient();
        static private string nick = "unknown";
        static private List<string> mensajes = new List<string>();

        void Listen()
        {
            while (cliente.Connected)
            {
                string mensaje = streamr.ReadLine();
                if (mensaje != nick)
                {
                    mensajes.Add(mensaje);
                }
                ViewBag.mensajes = mensajes;
            }
        }

        public TcpClient conectar(String nombreUsuario)
        {
            nick = nombreUsuario;
            if (!cliente.Connected)
            {
                cliente.Connect("127.0.0.1", 8000);
            }
            if (cliente.Connected)
            {
                Thread t = new Thread(Listen);

                stream = cliente.GetStream();
                streamw = new StreamWriter(stream);
                streamr = new StreamReader(stream);

                streamw.WriteLine(nick);
                streamw.Flush();

                //mensajes.Add(nombreUsuario);

                t.Start();
            }
            ViewBag.clie = cliente;
            ViewBag.mensajes = mensajes;
            return cliente;
        }

        [HttpPost]
        private void Enviar(string mensaje, TcpClient socket, string nombreUsuario, List<string> mensajes)
        {
            var stream = cliente.GetStream();
            var streamr = new StreamReader(stream);
            var streamw = new StreamWriter(stream);
            if(mensaje == "" || mensaje == null)
            {
                mensaje = "Hola";
            }
            Chat(0, mensaje, nombreUsuario, mensajes);
            streamw.WriteLine(mensaje);
            streamw.Flush();
            mensajes.Add(streamr.ReadLine());
            //ViewBag.mensajes = mensajes;
        }
    }
}
