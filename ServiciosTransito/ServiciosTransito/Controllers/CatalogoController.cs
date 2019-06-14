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
    public class CatalogoController : Controller
    {
        TransitoContext _context = new TransitoContext();

        [Route("/Catalogo/ObtenerEmpleados")]
        [Produces("application/json")]
        [HttpGet]
        public IQueryable<Catalogo> obtenerTiposEmpleado()
        {
            return _context.Catalogo.Where(c => c.TipoCatalogo == 1);
        }

        [Route("/Catalogo/ObtenerEstatus")]
        [Produces("application/json")]
        [HttpGet]
        public IQueryable<Catalogo> obtenerEstatusreporte()
        {
            return _context.Catalogo.Where(c => c.TipoCatalogo == 6);
        }
    }
}
