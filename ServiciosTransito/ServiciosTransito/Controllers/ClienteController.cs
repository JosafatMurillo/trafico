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
    public class ClienteController : Controller
    {
        TransitoContext _context = new TransitoContext();

        [Route("/Cliente/Login")]
        [Produces("application/json")]
        [HttpPost]
        public Cliente login([FromForm]String telefono, [FromForm]String contrasenia)
        {
            Cliente cliente = _context.Cliente.FirstOrDefault(c => c.Telefono.Equals(telefono) && c.Contrasenia.Equals(contrasenia));

            return cliente;
        }

        [Route("/Cliente/Registrar")]
        [Produces("application/json")]
        [HttpPost]
        public Mensaje registrar([FromForm]String nombre, [FromForm]String apellidos, [FromForm]DateTime fechaNacimiento,
            [FromForm]String numeroLicencia, [FromForm]String telefono, [FromForm]String contrasenia)
        {
            Mensaje mensaje = new Mensaje();
            Cliente cliente = new Cliente();
            cliente.Apellidos = apellidos;
            cliente.Contrasenia = contrasenia;
            cliente.FechaNacimiento = fechaNacimiento;
            cliente.Nombre = nombre;
            cliente.NumeroLicencia = numeroLicencia;
            cliente.Telefono = telefono;
            try
            {
                _context.Cliente.Add(cliente);
                _context.SaveChanges();
                mensaje.correcto = true;
                mensaje.mensaje = "Cliente registrado exitosamente";
            }
            catch (Exception e)
            {
                mensaje.correcto = false;
                mensaje.mensaje = "Fallo al registrar al cliente";
            }
            return mensaje;
        }

        [Route("/Cliente/Actualizar")]
        [Produces("application/json")]
        [HttpPut]
        public Mensaje actualizar([FromForm]String nombre, [FromForm]String apellidos, [FromForm]DateTime fechaNacimiento,
            [FromForm]String numeroLicencia, [FromForm]String telefono, [FromForm]String contrasenia, [FromForm]int idCliente)
        {
            Mensaje mensaje = new Mensaje();
            Cliente cliente = new Cliente();
            cliente.Apellidos = apellidos;
            cliente.Contrasenia = contrasenia;
            cliente.FechaNacimiento = fechaNacimiento;
            cliente.Nombre = nombre;
            cliente.NumeroLicencia = numeroLicencia;
            cliente.Telefono = telefono;
            cliente.IdCliente = idCliente;
            try
            {
                _context.Cliente.Update(cliente);
                _context.SaveChanges();
                mensaje.correcto = true;
                mensaje.mensaje = "Cliente actualizado exitosamente";
            }
            catch (Exception e)
            {
                mensaje.correcto = false;
                mensaje.mensaje = "Fallo al actualizar al cliente";
            }
            return mensaje;
        }

        [Route("/Cliente/Eliminar")]
        [Produces("application/json")]
        [HttpDelete]
        public Mensaje eliminar([FromForm]int idCliente)
        {
            Mensaje mensaje = new Mensaje();
            Cliente cliente = new Cliente();
            cliente.IdCliente = idCliente;
            try
            {
                _context.Cliente.Remove(cliente);
                _context.SaveChanges();
                mensaje.correcto = true;
                mensaje.mensaje = "Cliente eliminado exitosamente";
            }
            catch (Exception e)
            {
                mensaje.correcto = false;
                mensaje.mensaje = "Fallo al eliminar al cliente";
            }
            return mensaje;
        }
    }
}
