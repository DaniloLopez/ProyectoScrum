using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppEjemploLayout.Models;
using AppEjemploLayout.Models.ClasesSprints;
using AppEjemploLayout.Models.ViewModel;

namespace AppEjemploLayout.Controllers.MVCControllers
{
    public class SprintsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
 

        // GET: Sprints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprint.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }

            InformacionSprint info = new InformacionSprint();
            info.sprint = sprint;
            info.tareas = db.TareaSprint.Where(p=>p.SprintId==id).ToList();
            info.historiasUsuario = db.HistoriaUsuarios.Where(p => p.SprintId == id).ToList();
            ViewBag.rol = db.ProyectoUsuario.Where(p => p.ProyectoId == sprint.ProyectoId && p.UsuarioID == (int)Session["UsuarioId"]);
            return View(info);
        }

        // GET: Sprints/Create
        public ActionResult Create(int? idProyecto)
        {
            if (idProyecto == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProyectoId = idProyecto;
            return View();
        }

        // POST: Sprints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SprintId,duracion,estado,ProyectoId")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Sprint.Add(sprint);
                db.SaveChanges();
                return RedirectToAction("UsuariosProyecto", "Proyectoes", new { IdProyecto = sprint.ProyectoId });
            }

            ViewBag.ProyectoId = new SelectList(db.Proyectoes, "ProyectoId", "nombreProyecto", sprint.ProyectoId);
            return View(sprint);
        }

        // GET: Sprints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprint.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProyectoId = new SelectList(db.Proyectoes, "ProyectoId", "nombreProyecto", sprint.ProyectoId);
            return View(sprint);
        }

        // POST: Sprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SprintId,duracion,estado,ProyectoId")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProyectoId = new SelectList(db.Proyectoes, "ProyectoId", "nombreProyecto", sprint.ProyectoId);
            return View(sprint);
        }

        // GET: Sprints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprint.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // POST: Sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sprint sprint = db.Sprint.Find(id);
            db.Sprint.Remove(sprint);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
