using AppEjemploLayout.Models;
using AppEjemploLayout.Models.Proyecto_Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace AppEjemploLayout.Controllers.APIControllers
{
    public class CrudIntegranteController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("api/InsertarIntegrante/{IdProyecto}/{IdUsuario}/{rol}")]
        public IHttpActionResult InsertarIntegrante(int IdProyecto, int IdUsuario, string rol)
        {
            try
            {
                ProyectoUsuarioRelacion relacionNueva = new ProyectoUsuarioRelacion()
                {
                    ProyectoId = IdProyecto,
                    UsuarioID = IdUsuario,
                    rolUsuario = rol
                };
                db.ProyectoUsuario.Add(relacionNueva);
                db.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(Exception)
            {
                return NotFound();
            }

        }

        


    }    
}
