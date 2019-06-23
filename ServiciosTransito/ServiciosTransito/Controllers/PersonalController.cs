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
    public class PersonalController : Controller
    {
        TransitoContext _context = new TransitoContext();

        [Route("/Personal/Login")]
        [Produces("application/json")]
        [HttpPost]
        public Personal login([FromForm]string nombreUsuario, [FromForm]string contrasenia)
        {
            return _context.Personal.FirstOrDefault(p => p.NombreUsuario.Equals(nombreUsuario) && p.Contrasenia.Equals(contrasenia));
        }

        [Route("/Personal/ObtenerPorId")]
        [Produces("application/json")]
        [HttpPost]
        public Personal obtenerPorId([FromForm]int idPersonal)
        {
            return _context.Personal.FirstOrDefault(p => p.IdPersonal == idPersonal);
        }

        [Route("/Personal/Agregar")]
        [Produces("application/json")]
        [HttpPost]
        public Mensaje agregar([FromForm]string apellidos, [FromForm]string contrasenia, [FromForm]int catalogo,
            [FromForm]string nombre, [FromForm]string nombreUsuario)
        {
            Mensaje mensaje = new Mensaje();
            Personal personal = new Personal();
            personal.Apellidos = apellidos;
            personal.Catalogo = catalogo;
            personal.Contrasenia = contrasenia;
            personal.Nombre = nombre;
            personal.NombreUsuario = nombreUsuario;
            try
            {
                _context.Personal.Add(personal);
                _context.SaveChanges();
                mensaje.mensaje = "Empleado registrado con exito";
                mensaje.correcto = true;
            } catch (Exception e)
            {
                mensaje.mensaje = "Fallo al registrar al empleado";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/Personal/Actualizar")]
        [Produces("application/json")]
        [HttpPut]
        public Mensaje actualizar([FromForm]string apellidos, [FromForm]string contrasenia, [FromForm]int catalogo,
            [FromForm]string nombre, [FromForm]string nombreUsuario,[FromForm]int idPersonal)
        {
            Mensaje mensaje = new Mensaje();
            Personal personal = new Personal();
            personal.Apellidos = apellidos;
            personal.Catalogo = catalogo;
            personal.Contrasenia = contrasenia;
            personal.Nombre = nombre;
            personal.IdPersonal = idPersonal;
            personal.NombreUsuario = nombreUsuario;
            try
            {
                _context.Personal.Update(personal);
                _context.SaveChanges();
                mensaje.mensaje = "Empleado actualizado con exito";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al actualizar al empleado";
                mensaje.correcto = false;
            }
            return mensaje;
        }

        [Route("/Personal/Eliminar")]
        [Produces("application/json")]
        [HttpDelete]
        public Mensaje eliminar([FromForm]int idPersonal)
        {
            Mensaje mensaje = new Mensaje();
            Personal personal = new Personal();
            personal.IdPersonal = idPersonal;
            try
            {
                _context.Personal.Remove(personal);
                _context.SaveChanges();
                mensaje.mensaje = "Empleado eliminado con exito";
                mensaje.correcto = true;
            }
            catch (Exception e)
            {
                mensaje.mensaje = "Fallo al eliminar al empleado";
                mensaje.correcto = false;
            }
            return mensaje;
        }
    }
}
