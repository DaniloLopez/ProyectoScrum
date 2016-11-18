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
            if (Session["Usuario"] != null && (bool)Session["Usuario"] != false)
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
            return RedirectToAction("InicioSesion", "Usuarios", null);
            
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
                
                return RedirectToAction("ListaUsuarios",new {IdProyecto=p.ProyectoId});
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

        //ANOTACION SI SE VA A ELIMINAR EL PROYECTO SE DEBE ELIMINAR TODA LA INFORMACION ASOSCIADA AL PROYECTO NO SOLO EL PROYECTO
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
        //REVISAR PORQUE NO SE ESTA HACIENDO LA CONFIRMACION
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

        public ActionResult ListaUsuarios(int? IdProyecto)
        {
            if (IdProyecto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectoes.Find(IdProyecto);
            if (proyecto == null)
            {
                return HttpNotFound();
            }

            //SE MIRA SI EL USUARIO QUE ESTA CONSULTANDO LOS INTEGRANTES DEL PROYECTO ES EL ADMINISTRADOR
            //EN CASO DE QUE SEA SE LE DA PERMISO DE EDITAR SINO SOLAMENTE SE MOSTRARA LOS INTEGRANTES
            string usuario = Session["NombreUsuario"].ToString();
            var validacion = db.ProyectoUsuario.Where(p=>p.ProyectoId==IdProyecto);
            
            foreach(ProyectoUsuarioRelacion i in validacion)
            {
                if (i.correoElectronicoUsuario.CompareTo((string)Session["NombreUsuario"]) == 0)
                {
                    if (i.rolUsuario.CompareTo("administrador") == 0)
                    {
                        Session["PermisoEditarUsuariosProyecto"] = true;
                    }
                    else
                    {
                        Session["PermisoEditarUsuariosProyecto"] = false;
                        return View();
                    }
                    break;
                }
            }
                        
            var lista = db.ProyectoUsuario.Where(p => p.ProyectoId == IdProyecto).Include(i=>i.proyecto).Include(u=>u.usuario).ToList();
            return View(lista);
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
