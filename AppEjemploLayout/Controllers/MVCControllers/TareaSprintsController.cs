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

namespace AppEjemploLayout.Controllers.MVCControllers
{
    public class TareaSprintsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: TareaSprints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TareaSprint tareaSprint = db.TareaSprint.Find(id);
            if (tareaSprint == null)
            {
                return HttpNotFound();
            }
            return View(tareaSprint);
        }

        // GET: TareaSprints/Create
        public ActionResult Create(int? idSprint)
        {
            if (idSprint == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProyectoId = idSprint;
            return View();
        }

        // POST: TareaSprints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TareaSprintId,asunto,descripcion,estado,estimacionHoras,SprintId,UsuarioId")] TareaSprint tareaSprint)
        {
            if (ModelState.IsValid)
            {
                db.TareaSprint.Add(tareaSprint);
                db.SaveChanges();
                return RedirectToAction("");
            }

            ViewBag.SprintId = new SelectList(db.Sprint, "SprintId", "estado", tareaSprint.SprintId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "correoElectronicoUsuario", tareaSprint.UsuarioId);
            return View(tareaSprint);
        }

        // GET: TareaSprints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TareaSprint tareaSprint = db.TareaSprint.Find(id);
            if (tareaSprint == null)
            {
                return HttpNotFound();
            }
            ViewBag.SprintId = new SelectList(db.Sprint, "SprintId", "estado", tareaSprint.SprintId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "correoElectronicoUsuario", tareaSprint.UsuarioId);
            return View(tareaSprint);
        }

        // POST: TareaSprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TareaSprintId,asunto,descripcion,estado,estimacionHoras,SprintId,UsuarioId")] TareaSprint tareaSprint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tareaSprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SprintId = new SelectList(db.Sprint, "SprintId", "estado", tareaSprint.SprintId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "correoElectronicoUsuario", tareaSprint.UsuarioId);
            return View(tareaSprint);
        }

        // GET: TareaSprints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TareaSprint tareaSprint = db.TareaSprint.Find(id);
            if (tareaSprint == null)
            {
                return HttpNotFound();
            }
            return View(tareaSprint);
        }

        // POST: TareaSprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TareaSprint tareaSprint = db.TareaSprint.Find(id);
            db.TareaSprint.Remove(tareaSprint);
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
