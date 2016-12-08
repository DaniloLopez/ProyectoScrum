using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppEjemploLayout.Models;
using AppEjemploLayout.Models.ClasesHistoriaUsuario;

namespace AppEjemploLayout.Controllers
{
    public class HistoriaUsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        

        // GET: HistoriaUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios", null);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriaUsuario historiaUsuario = db.HistoriaUsuarios.Find(id);
            if (historiaUsuario == null)
            {
                return HttpNotFound();
            }
            return View(historiaUsuario);
        }

        // GET: HistoriaUsuarios/Create
        public ActionResult Create(int? idProyecto)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios", null);
            }
            if (idProyecto == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProyectoId = idProyecto;
            return View();
        }

        // POST: HistoriaUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoriaUsuarioId,ProyectoId,nombre,contexto,descripcion,prioridad,ezfuerzo")] HistoriaUsuario historiaUsuario)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios", null);
            }
            if (ModelState.IsValid)
            {
                db.HistoriaUsuarios.Add(historiaUsuario);
                db.SaveChanges();
                return RedirectToAction("UsuariosProyecto","Proyectoes",new { IdProyecto=historiaUsuario.ProyectoId });
            }

            ViewBag.ProyectoId = new SelectList(db.Proyectoes, "ProyectoId", "nombreProyecto", historiaUsuario.ProyectoId);
            return View(historiaUsuario);
        }

        // GET: HistoriaUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios", null);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriaUsuario historiaUsuario = db.HistoriaUsuarios.Find(id);
            if (historiaUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProyectoId = new SelectList(db.Proyectoes, "ProyectoId", "nombreProyecto", historiaUsuario.ProyectoId);
            return View(historiaUsuario);
        }

        // POST: HistoriaUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoriaUsuarioId,ProyectoId,nombre,contexto,descripcion,prioridad,ezfuerzo")] HistoriaUsuario historiaUsuario)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios", null);
            }
            if (ModelState.IsValid)
            {
                db.Entry(historiaUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProyectoId = new SelectList(db.Proyectoes, "ProyectoId", "nombreProyecto", historiaUsuario.ProyectoId);
            return View(historiaUsuario);
        }

        // GET: HistoriaUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios", null);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriaUsuario historiaUsuario = db.HistoriaUsuarios.Find(id);
            if (historiaUsuario == null)
            {
                return HttpNotFound();
            }
            return View(historiaUsuario);
        }

        // POST: HistoriaUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("InicioSesion", "Usuarios", null);
            }
            HistoriaUsuario historiaUsuario = db.HistoriaUsuarios.Find(id);
            db.HistoriaUsuarios.Remove(historiaUsuario);
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
