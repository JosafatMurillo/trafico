using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiciosTransito.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiciosTransito.Controllers
{
    [Route("api/[controller]")]
    public class VehiculoController : Controller
    {
        TransitoContext _context = new TransitoContext();

        [Route("/Vehiculo/ObtenerPorIdVehiculo")]
        [Produces("application/json")]
        [HttpPost]
        public Vehiculo obtenerVehiculoPorId([FromForm]int idVehiculo)
        {
            return _context.Vehiculo.FirstOrDefault(v => v.IdVehiculo == idVehiculo);
        }

        [Route("/Vehiculo/ObtenerPorIdCliente")]
        [Produces("application/json")]
        [HttpPost]
        public Vehiculo obtenerVehiculoPorIdCliente([FromForm]int idCliente)
        {
            return _context.Vehiculo.FirstOrDefault(v => v.Cliente == idCliente);
        }

        [Route("/Vehiculo/ObtenerPorCliente")]
        [Produces("application/json")]
        [HttpPost]
        public List<Vehiculo> obtenerVehiculosPorCliente([FromForm]int idCliente)
        {
            return _context.Vehiculo.Where(v => v.Cliente == idCliente).ToList();
        }

        [Route("/Vehiculo/Agregar")]
        [Produces("application/json")]
        [HttpPost]
        public Mensaje agregarVehiculo([FromForm]string anio, [FromForm]int cliente,[FromForm]string color,[FromForm]string marca,
            [FromForm]string modelo,[FromForm]string nombreAseguradora,[FromForm]string numeroPlacas, [FromForm]string numeroPoliza)
        {
            Vehiculo vehiculoGuardar = new Vehiculo();
            Mensaje mensaje = new Mensaje();
            vehiculoGuardar.Anio = anio;
            vehiculoGuardar.Cliente = cliente;
            vehiculoGuardar.Color = color;
            vehiculoGuardar.Marca = marca;
            vehiculoGuardar.Modelo = modelo;
            vehiculoGuardar.NombreAseguradora = nombreAseguradora;
            vehiculoGuardar.NumeroPlacas = numeroPlacas;
            vehiculoGuardar.NumeroPoliza = numeroPoliza;
            try
            {
                _context.Vehiculo.Add(vehiculoGuardar);
                _context.SaveChanges();
                mensaje.mensaje = "Vehiculo registrados exitosamente";
                mensaje.correcto = true;
            } catch (Exception e)
            {
                mensaje.mensaje = "Fallo al registrar el vehiculo";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/Vehiculo/Actualizar")]
        [Produces("application/json")]
        [HttpPut]
        public Mensaje actualizarVehiculo([FromForm]string anio, [FromForm]int cliente, [FromForm]string color, [FromForm]string marca,
            [FromForm]string modelo, [FromForm]string nombreAseguradora, [FromForm]string numeroPlacas, [FromForm]string numeroPoliza,
            [FromForm]int idVehiculo)
        {
            Vehiculo vehiculoGuardar = new Vehiculo();
            Mensaje mensaje = new Mensaje();
            vehiculoGuardar.Anio = anio;
            vehiculoGuardar.Cliente = cliente;
            vehiculoGuardar.Color = color;
            vehiculoGuardar.Marca = marca;
            vehiculoGuardar.Modelo = modelo;
            vehiculoGuardar.NombreAseguradora = nombreAseguradora;
            vehiculoGuardar.NumeroPlacas = numeroPlacas;
            vehiculoGuardar.NumeroPoliza = numeroPoliza;
            vehiculoGuardar.IdVehiculo = idVehiculo;
            try
            {
                _context.Vehiculo.Update(vehiculoGuardar);
                _context.SaveChanges();
                mensaje.mensaje = "Vehiculo actualizado exitosamente";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al actualizar el vehiculo";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/Vehiculo/Eliminar")]
        [Produces("application/json")]
        [HttpDelete]
        public Mensaje eliminarVehiculo([FromForm]int idVehiculo)
        {
            Mensaje mensaje = new Mensaje();
            Vehiculo vehiculoEliminar = new Vehiculo();
            vehiculoEliminar.IdVehiculo = idVehiculo;
            try
            {
                _context.Vehiculo.Remove(vehiculoEliminar);
                _context.SaveChanges();
                mensaje.mensaje = "Vehiculo eliminado exitosamente";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al eliminar el vehiculo exitosamente";
                mensaje.correcto = false;
            }
            return mensaje;
        }
    }
}
