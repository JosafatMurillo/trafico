using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiciosTransito.Models;

namespace ServiciosTransito.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        TransitoContext _context = new TransitoContext();

        public static IHostingEnvironment _environment;
        public ImageController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public class FIleUploadAPI
        {
            public IFormFile files { get; set; }
        }

        [HttpPost]
        public async Task<string> Post(FIleUploadAPI files, int idReporte)
        {
            Foto foto = new Foto();
            if (files.files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + files.files.FileName))
                    {
                        files.files.CopyTo(filestream);
                        filestream.Flush();
                        foto.Reporte = idReporte;
                        foto.Foto1 = _environment.WebRootPath + "\\uploads\\" + files.files.FileName;
                        _context.Foto.Add(foto);
                        _context.SaveChanges();
                        return "\\uploads\\" + files.files.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Unsuccessful";
            }

        }

    }
}