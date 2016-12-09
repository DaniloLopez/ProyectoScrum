using AppEjemploLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppEjemploLayout.Controllers.APIControllers
{
    public class GraficosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("api/Graficos/{idProyecto}")]
        public IHttpActionResult Sprintburndown(int idProyecto)
        {
            var consulta = (from spr in db.Sprint
                            join tsk in db.TareaSprint 
                            on spr.SprintId equals tsk.SprintId
                            where spr.ProyectoId == idProyecto
                            && spr.estado == "Cerrado"
                            group spr by spr.SprintId into con
                            select new
                            {
                                Dato = "Sprint" + con.Key,
                                Cantidad = con.Sum(c => c.duracion)
                            }).OrderBy(s => s.Dato).ToList();
            return Ok(consulta);
        }

    }
}
