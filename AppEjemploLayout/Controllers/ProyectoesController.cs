using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppEjemploLayout.Models;
using AppEjemploLayout.Models.ClasesProyecto;
using AppEjemploLayout.Models.Proyecto_Usuario;

namespace AppEjemploLayout.Controllers
{
    public class ProyectoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proyectoes
        public ActionResult Index()
        {
            if (Session["Usuario"] != null && (bool)Session["Usuario"] != false)
            {
                string usuario= Session["NombreUsuario"].ToString();
                return View(db.ProyectoUsuario.Where(d => d.correoElectronicoUsuario.CompareTo(usuario)==0).Include(p=>p.proyecto).Include(u=>u.usuario).ToList());
            }
            return RedirectToAction("InicioSesion", "Usuarios", null);
        }

        // GET: Proyectoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectoes.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // GET: Proyectoes/Create
        public ActionResult CrearProyecto()
        {
            if (Session["Usuario"] != null && (bool)Session["Usuario"] != false)
            {
                return View();
            }
            return RedirectToAction("InicioSesion", "Usuarios", null);
        }

        // POST: Proyectoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearProyecto(Proyecto proyecto)
        {       

            if (ModelState.IsValid)
            {
                proyecto.estadoProyecto ="abierto";     
                
                ProyectoUsuarioRelacion p = new ProyectoUsuarioRelacion();
                p.ProyectoId = proyecto.ProyectoId;
                p.correoElectronicoUsuario = (string)Session["NombreUsuario"];
                p.rolUsuario = "administrador";                
                db.Proyectoes.Add(proyecto);
                db.ProyectoUsuario.Add(p);
                db.SaveChanges();
                
                return RedirectToAction("AgregarIntegrante");
            }

            return View(proyecto);
        }

        // GET: Proyectoes/Edit/5
        public ActionResult EditarProyecto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectoes.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // POST: Proyectoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProyecto([Bind(Include = "ProyectoId,nombreProyecto,descripcionProyecto,fechaInicioProyecto,fechaFinalizacionProyecto,estadoProyecto")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proyecto);
        }

        // GET: Proyectoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectoes.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // POST: Proyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proyecto proyecto = db.Proyectoes.Find(id);
            db.Proyectoes.Remove(proyecto);
            db.ProyectoUsuario.RemoveRange(db.ProyectoUsuario.Where(p => p.ProyectoId.Equals(id)).ToList());
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
