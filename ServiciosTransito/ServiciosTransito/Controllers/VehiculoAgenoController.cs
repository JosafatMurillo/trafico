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
    public class VehiculoAgenoController : Controller
    {
        TransitoContext _context = new TransitoContext();

        [Route("/VehiculoAgeno/ObtenerPorId")]
        [Produces("application/json")]
        [HttpGet]
        public IQueryable<VehiculoAgeno> obtenerVehiculosAgenoPorId([FromForm]int idVehiculo)
        {
            return _context.VehiculoAgeno.Where(v => v.IdVehiculoAgeno == idVehiculo);
        }

        [Route("/VehiculoAgeno/Agregar")]
        [Produces("application/json")]
        [HttpPost]
        public Mensaje agregarVehiculoAgeno([FromForm]string anio,[FromForm]string color, [FromForm]string marca,
            [FromForm]string modelo, [FromForm]string nombreAseguradora, [FromForm]string numeroPlacas, [FromForm]string numeroPoliza)
        {
            VehiculoAgeno vehiculoGuardar = new VehiculoAgeno();
            Mensaje mensaje = new Mensaje();
            vehiculoGuardar.Anio = anio;
            vehiculoGuardar.Color = color;
            vehiculoGuardar.Marca = marca;
            vehiculoGuardar.Modelo = modelo;
            vehiculoGuardar.NombreAseguradora = nombreAseguradora;
            vehiculoGuardar.NumeroPlacas = numeroPlacas;
            vehiculoGuardar.NumeroPoliza = numeroPoliza;
            try
            {
                _context.VehiculoAgeno.Add(vehiculoGuardar);
                _context.SaveChanges();
                mensaje.mensaje = "Vehiculo registrados exitosamente";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al registrar el vehiculo";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/VehiculoAgeno/Actualizar")]
        [Produces("application/json")]
        [HttpPut]
        public Mensaje actualizarVehiculo([FromForm]string anio, [FromForm]string color, [FromForm]string marca,
            [FromForm]string modelo, [FromForm]string nombreAseguradora, [FromForm]string numeroPlacas, [FromForm]string numeroPoliza,
            [FromForm]int idVehiculoAgeno)
        {
            VehiculoAgeno vehiculoGuardar = new VehiculoAgeno();
            Mensaje mensaje = new Mensaje();
            vehiculoGuardar.Anio = anio;
            vehiculoGuardar.IdVehiculoAgeno = idVehiculoAgeno;
            vehiculoGuardar.Color = color;
            vehiculoGuardar.Marca = marca;
            vehiculoGuardar.Modelo = modelo;
            vehiculoGuardar.NombreAseguradora = nombreAseguradora;
            vehiculoGuardar.NumeroPlacas = numeroPlacas;
            vehiculoGuardar.NumeroPoliza = numeroPoliza;
            try
            {
                _context.VehiculoAgeno.Update(vehiculoGuardar);
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

        [Route("/VehiculoAgeno/Eliminar")]
        [Produces("application/json")]
        [HttpDelete]
        public Mensaje eliminarVehiculo([FromForm]int idVehiculo)
        {
            Mensaje mensaje = new Mensaje();
            VehiculoAgeno vehiculoEliminar = new VehiculoAgeno();
            vehiculoEliminar.IdVehiculoAgeno = idVehiculo;
            try
            {
                _context.VehiculoAgeno.Remove(vehiculoEliminar);
                _context.SaveChanges();
                mensaje.mensaje = "Vehiculo eliminado exitosamente";
                mensaje.correcto = true;
            } catch (Exception e)
            {
                mensaje.mensaje = "Fallo al eliminar el vehiculo exitosamente";
                mensaje.correcto = false;
            }
            return mensaje;
        }
    }
}
