using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiciosTransito.Models;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiciosTransito.Controllers
{
    [Route("api/[controller]")]
    public class FotoController : Controller
    {
        TransitoContext _context = new TransitoContext();
        private readonly IHostingEnvironment _environment;

        [Route("/Foto/ObtenerPorReporte")]
        [Produces("application/json")]
        [HttpPost]
        public IQueryable<Foto> obtenerTiposEmpleado([FromForm] int idReporte)
        {
            return _context.Foto.Where(f => f.Reporte == idReporte);
        }

        [Route("/Foto/ObtenerFotos")]
        [Produces("application/json")]
        [HttpPost]
        public List<IFormFile> obtenerFotos([FromForm] int idReporte)
        {
            List<IFormFile> fotos = new List<IFormFile>();
            List<Foto> registros = _context.Foto.Where(f => f.Reporte == idReporte).ToList();
            foreach(Foto f in registros)
            {
                string ruta = f.Foto1;

            }
            return fotos;
        }

        [Route("/Foto/Registrar")]
        [Produces("application/json")]
        [HttpPost]
        public async void guardarFoto([FromForm] IFormFile foto, [FromForm] int idReporte)
        {
            Foto fotoGuardar = new Foto();
            String rutaApp = Directory.GetCurrentDirectory();
            fotoGuardar.Reporte = idReporte;
            fotoGuardar.Foto1 = foto.FileName;
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            using (var stream = new FileStream(Path.Combine(uploads,foto.FileName), FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
            _context.Foto.Add(fotoGuardar);
            _context.SaveChanges();
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
        [HttpDelete]
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
