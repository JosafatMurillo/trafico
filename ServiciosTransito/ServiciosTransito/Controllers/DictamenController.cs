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
    public class DictamenController : Controller
    {
        TransitoContext _context = new TransitoContext();

        [Route("/Dictamen/ObtenerPorReporte")]
        [Produces("application/json")]
        [HttpGet]
        public Dictamen obtenerDictamenPorReporte([FromForm]int idReporte)
        {
            return _context.Dictamen.FirstOrDefault(d => d.Reporte == idReporte);
        }

        [Route("/Dictamen/Registrar")]
        [Produces("application/json")]
        [HttpPost]
        public Mensaje registrarDictamen([FromForm]int personal, [FromForm]int reporte, [FromForm]string folio,
            [FromForm]string descripcion, [FromForm]string nombrePerito)
        {
            Mensaje mensaje = new Mensaje();
            Dictamen dictamenNuevo = new Dictamen();
            dictamenNuevo.Descripcion = descripcion;
            dictamenNuevo.Folio = folio;
            dictamenNuevo.NombrePerito = nombrePerito;
            dictamenNuevo.Personal = personal;
            dictamenNuevo.Reporte = reporte;
            try
            {
                _context.Dictamen.Add(dictamenNuevo);
                _context.SaveChanges();
                mensaje.mensaje = "Dictamen registrado exitosamente";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al registrar el dictamen";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/Dictamen/Actualizar")]
        [Produces("application/json")]
        [HttpPut]
        public Mensaje actualizarDictamen([FromForm]int personal, [FromForm]int reporte, [FromForm]string folio,
            [FromForm]string descripcion, [FromForm]string nombrePerito, [FromForm] int idDictamen)
        {
            Mensaje mensaje = new Mensaje();
            Dictamen dictamenNuevo = new Dictamen();
            dictamenNuevo.Descripcion = descripcion;
            dictamenNuevo.Folio = folio;
            dictamenNuevo.NombrePerito = nombrePerito;
            dictamenNuevo.Personal = personal;
            dictamenNuevo.Reporte = reporte;
            dictamenNuevo.IdDictamen = idDictamen;
            try
            {
                _context.Dictamen.Update(dictamenNuevo);
                _context.SaveChanges();
                mensaje.mensaje = "Dictamen registrado exitosamente";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al registrar el dictamen";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/Dictamen/Eliminar")]
        [Produces("application/json")]
        [HttpDelete]
        public Mensaje eliminarDictamen([FromForm] int idDictamen)
        {
            Mensaje mensaje = new Mensaje();
            Dictamen dictamenEliminar = new Dictamen();
            dictamenEliminar.IdDictamen = idDictamen;
            try
            {
                _context.Dictamen.Remove(dictamenEliminar);
                _context.SaveChanges();
                mensaje.mensaje = "Dictamen eliminado con exito";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Dictamen eliminado con exito";
                mensaje.correcto = false;
            }
            return mensaje;
        }
    }
}
