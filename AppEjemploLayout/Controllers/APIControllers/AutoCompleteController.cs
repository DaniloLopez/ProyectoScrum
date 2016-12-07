using AppEjemploLayout.Models;
using AppEjemploLayout.Models.ClasesUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppEjemploLayout.Controllers
{
    public class AutoCompleteController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //Usted en éstos métodos puede devolver casi cualquier cosa
        //Recuerde siempre poner el verbo que va a usar jajajaja, es mejor. 
        //La ruta para llamar el método va a ser: [direccionServidor]/api/AutoComplete/{texto}
        [HttpGet]
        [Route("api/AutoComplete/{texto}")]
        public IHttpActionResult GetUsuarios(string texto)
        {
            //var consulta = db.Usuarios.Include("Proyectos").Where(u => u.apellidosUsuario.Contains(texto)
            //|| u.nombresUsuario.Contains(texto));
            var consulta = (from usu in db.Usuarios
                            where usu.apellidosUsuario.Contains(texto) || usu.nombresUsuario.Contains(texto)
                            select new
                            {
                                UsuarioId = usu.UsuarioId,
                                Nombre = usu.nombresUsuario + " " + usu.apellidosUsuario + "-"+usu.correoElectronicoUsuario
                            });
            var lista = consulta.ToList();
            return Ok(lista);
        }
        //El método sepuede llamar como a usted se le de la gana
        //El post es mejor cuando usted va a meter info a la base de datos

        //[HttpGet]
        //[Route("api/AutoComplete/GetProyectos/{texto}")]
        //public IHttpActionResult GetProyectos(string texto)
        //{
        //    var consulta = db.Proyectoes.Where(u => u.nombreProyecto.Contains(texto));
        //    return Ok(consulta.ToList());
        //}
    }
}
