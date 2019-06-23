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
    public class ReporteController : Controller
    {
        TransitoContext _context = new TransitoContext();

        [Route("/Reporte/ObtenerPorCliente")]
        [Produces("application/json")]
        [HttpPost]
        public IQueryable <Reporte> obtenerReportesPorCliente([FromForm] int cliente)
        {
            return _context.Reporte.Where(r => r.Cliente == cliente);
        }

        [Route("/Reporte/ObtenerPorId")]
        [Produces("application/json")]
        [HttpPost]
        public Reporte obtenerPorId([FromForm] int idReporte)
        {
            return _context.Reporte.FirstOrDefault(r => r.IdReporte == idReporte);
        }

        [Route("/Reporte/ObtenerTodos")]
        [Produces("application/json")]
        [HttpGet]
        public List<Reporte> obtenerTodosReportes()
        {
            return _context.Reporte.ToList();
        }

        [Route("/Reporte/Agregar")]
        [Produces("application/json")]
        [HttpPost]
        public Mensaje agregarReporte([FromForm]int cliente, [FromForm]string lugar, [FromForm]string nombreConductor, [FromForm]int vehiculo,
            [FromForm]int vehiculoAgeno)
        {
            Mensaje mensaje = new Mensaje();
            Reporte reporte = new Reporte();
            reporte.Cliente = cliente;
            reporte.EstatusReporte = 8;
            reporte.Lugar = lugar;
            reporte.NombreConductorAgeno = nombreConductor;
            reporte.Vehiculo = vehiculo;
            reporte.VehiculoAgeno = vehiculoAgeno;
            try
            {
                _context.Reporte.Add(reporte);
                _context.SaveChanges();
                mensaje.mensaje = "Reporte almacenado correctamente";
                mensaje.correcto = true;
            } catch (Exception e)
            {
                mensaje.mensaje = "Fallo al almacenar el reporte";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/Reporte/Actualizar")]
        [Produces("application/json")]
        [HttpPost]
        public Mensaje actualizarReporte([FromForm]int cliente, [FromForm]string lugar, [FromForm]string nombreConductor, [FromForm]int vehiculo,
            [FromForm]int vehiculoAgeno,[FromForm] int idReporte)
        {
            Mensaje mensaje = new Mensaje();
            Reporte reporte = new Reporte();
            reporte.Cliente = cliente;
            reporte.IdReporte = idReporte;
            reporte.EstatusReporte = 7;
            reporte.Lugar = lugar;
            reporte.NombreConductorAgeno = nombreConductor;
            reporte.Vehiculo = vehiculo;
            reporte.VehiculoAgeno = vehiculoAgeno;
            try
            {
                _context.Reporte.Update(reporte);
                _context.SaveChanges();
                mensaje.mensaje = "Reporte actualizado correctamente";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al actualizar el reporte";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/Reporte/Eliminar")]
        [Produces("application/json")]
        [HttpDelete]
        public Mensaje eliminarReporte([FromForm]int idReporte)
        {
            Mensaje mensaje = new Mensaje();
            Reporte reporte = new Reporte();
            reporte.IdReporte = idReporte;
            try
            {
                _context.Reporte.Remove(reporte);
                _context.SaveChanges();
                mensaje.mensaje = "Reporte actualizado correctamente";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al actualizar el reporte";
                mensaje.correcto = false;
            }
            return mensaje;
        }
    }
}
