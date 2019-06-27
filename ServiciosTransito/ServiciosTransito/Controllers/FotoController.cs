using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiciosTransito.Models;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using static ServiciosTransito.Controllers.FotoController;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BitmapNet;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiciosTransito.Controllers
{
    [Route("api/[controller]")]
    public class FotoController : Controller
    {
        TransitoContext _context = new TransitoContext();

        public static IHostingEnvironment _environment;
        public FotoController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

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

        public class FIleUploadAPI
        {
            public IFormFile files { get; set; }
        }

        [Route("/Foto/Obtener")]
        //[Produces("application/json")]
        [HttpPost]
        public void obtenerFoto()
        {
            int idReporte = 1;
            List<Foto> fotos = new List<Foto>();
            //Foto foto = new Foto();
            fotos = _context.Foto.Where(f => f.Reporte == idReporte).ToList();
            //foto = _context.Foto.FirstOrDefault(f => f.Reporte == idReporte);

            var path = fotos[2].Foto1;

            string[] filepath = Directory.GetFiles(path);

            using(var stream = new FileStream(@path, FileMode.Open))
            {
                
            }

            /*var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            return File(memory, Path.GetExtension(path).ToLowerInvariant(), Path.GetFileName(path));
            //return imagen;*/
        }

        /*
        [Route("/Foto/Registrar")]
        [Produces("application/json")]
        [HttpPost]
        public async void guardarFoto([FromForm] IFormFile foto, [FromForm] int idReporte)
        {
            Foto fotoGuardar = new Foto();
            String rutaApp = Directory.GetCurrentDirectory();
            fotoGuardar.Reporte = idReporte;
            fotoGuardar.Foto1 = foto.FileName;
            var uploads = Path.Combine(Environment.WebRootPath, "uploads");
            using (var stream = new FileStream(Path.Combine(uploads,foto.FileName), FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }
            _context.Foto.Add(fotoGuardar);
            _context.SaveChanges();
        }*/

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
