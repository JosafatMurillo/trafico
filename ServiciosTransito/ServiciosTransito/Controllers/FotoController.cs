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
    public class FotoController : Controller
    {
        TransitoContext _context = new TransitoContext();

        [Route("/Foto/ObtenerPorReporte")]
        [Produces("application/json")]
        [HttpGet]
        public IQueryable<Foto> obtenerTiposEmpleado([FromForm] int idReporte)
        {
            return _context.Foto.Where(f => f.Reporte == idReporte);
        }

        [Route("/Foto/Registrar")]
        [Produces("application/json")]
        [HttpPost]
        public Mensaje guardarFoto([FromForm] string foto, [FromForm] int idReporte)
        {
            Mensaje mensaje = new Mensaje();
            Foto fotoGuardar = new Foto();
            fotoGuardar.Foto1 = foto;
            fotoGuardar.Reporte = idReporte;
            try
            {
                _context.Foto.Add(fotoGuardar);
                _context.SaveChanges();
                mensaje.mensaje = "Foto registrada con exito";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.correcto = false;
                mensaje.mensaje = "Fallo al almacenar la imagen";
            }
            return mensaje;
        }

        [Route("/Foto/Actualizar")]
        [Produces("application/json")]
        [HttpPut]
        public Mensaje actualizarFoto([FromForm] string foto, [FromForm] int idReporte, [FromForm]int idFoto)
        {
            Mensaje mensaje = new Mensaje();
            Foto fotoGuardar = new Foto();
            fotoGuardar.Foto1 = foto;
            fotoGuardar.Reporte = idReporte;
            fotoGuardar.IdFoto = idFoto;
            try
            {
                _context.Foto.Update(fotoGuardar);
                _context.SaveChanges();
                mensaje.mensaje = "Foto actualizada con exito";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.correcto = false;
                mensaje.mensaje = "Fallo al actualizar la imagen";
            }
            return mensaje;
        }

        [Route("/Foto/Eliminar")]
        [Produces("application/json")]
        [HttpPut]
        public Mensaje eliminarFoto([FromForm]int idFoto)
        {
            Mensaje mensaje = new Mensaje();
            Foto fotoEliminar = new Foto();
            fotoEliminar.IdFoto = idFoto;
            try
            {
                _context.Foto.Remove(fotoEliminar);
                _context.SaveChanges();
                mensaje.mensaje = "Foto eliminada con exito";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.correcto = false;
                mensaje.mensaje = "Fallo al eliminar la imagen";
            }
            return mensaje;
        }
    }
}
