using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AppEjemploLayout.Models;
using AppEjemploLayout.Models.ClasesSprints;

namespace AppEjemploLayout.Controllers.APIControllers
{
    public class CrudTareasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CrudTareas
        public IQueryable<TareaSprint> GetTareaSprint()
        {
            return db.TareaSprint;
        }

        // GET: api/CrudTareas/5
        [ResponseType(typeof(TareaSprint))]
        public IHttpActionResult GetTareaSprint(int id)
        {
            TareaSprint tareaSprint = db.TareaSprint.Find(id);
            if (tareaSprint == null)
            {
                return NotFound();
            }

            return Ok(tareaSprint);
        }

        // PUT: api/CrudTareas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTareaSprint(int id, TareaSprint tareaSprint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tareaSprint.TareaSprintId)
            {
                return BadRequest();
            }

            db.Entry(tareaSprint).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                var lista = db.TareaSprint.Where(p => p.SprintId == tareaSprint.SprintId
                && p.estado != "Terminada").ToList();
                var sprint = db.Sprint.Find(tareaSprint.SprintId);
                sprint.estado = (lista.Count > 0) ? "Abierto" : "Cerrado";
                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();

                //var cont = 0;
                //foreach (var i in lista)
                //{
                //    if (i.estado.CompareTo("Terminada") == 0)
                //        cont++;
                //}
                //if (lista.Count()-cont==0)
                //{
                //    var sprint = db.Sprint.Find(tareaSprint.SprintId);
                //    sprint.estado = "CERRADO";

                //}
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaSprintExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CrudTareas
        [ResponseType(typeof(TareaSprint))]
        public IHttpActionResult PostTareaSprint(TareaSprint tareaSprint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TareaSprint.Add(tareaSprint);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tareaSprint.TareaSprintId }, tareaSprint);
        }

        // DELETE: api/CrudTareas/5
        [ResponseType(typeof(TareaSprint))]
        public IHttpActionResult DeleteTareaSprint(int id)
        {
            TareaSprint tareaSprint = db.TareaSprint.Find(id);
            if (tareaSprint == null)
            {
                return NotFound();
            }

            db.TareaSprint.Remove(tareaSprint);
            db.SaveChanges();

            return Ok(tareaSprint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TareaSprintExists(int id)
        {
            return db.TareaSprint.Count(e => e.TareaSprintId == id) > 0;
        }
    }
}