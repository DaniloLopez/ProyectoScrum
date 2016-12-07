using AppEjemploLayout.Models;
using AppEjemploLayout.Models.Proyecto_Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppEjemploLayout.Controllers.APIControllers
{
    public class EquipoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        [Route("api/AgregarAEquipo/{IdProyecto}/{IdUsuario}/{rol}")]
        public IHttpActionResult AgregarAEquipo(int IdProyecto, int IdUsuario, string rol)
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
            catch (Exception)
            {
                return NotFound();
            }

        }
    }
}
