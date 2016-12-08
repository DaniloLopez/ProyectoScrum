using AppEjemploLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppEjemploLayout.Controllers.APIControllers
{
    public class TareasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("api/Tareas/GetSprint/{idSprint}")]
        public IHttpActionResult Get_Sprint(int idSprint)
        {
            var consulta = db.Sprint.Find(idSprint);
            return Ok(consulta);
        }

        [HttpGet]
        [Route("api/Tareas/GetTareas/{idUsuario}/{SprintId}")]
        public IHttpActionResult Get_Tareas(int idUsuario, int SprintId)
        {
            var consulta = db.TareaSprint.Where(s => s.UsuarioId == idUsuario && s.SprintId == SprintId)
                .OrderBy(s => s.estado).ToList();
            return Ok(consulta);
        }

        [HttpGet]
        [Route("api/Tareas/GetTareasAsignar/{SprintId}")]
        public IHttpActionResult Get_TareasAsignar(int SprintId)
        {
            var consulta = db.TareaSprint.Where(s => s.UsuarioId == null && s.SprintId == SprintId)
                .ToList();

            return Ok(consulta);
        }
    }
}
