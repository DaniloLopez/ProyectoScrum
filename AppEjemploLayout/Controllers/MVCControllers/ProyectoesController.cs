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
using AppEjemploLayout.Models.ViewModel;

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
                string usuario = Session["NombreUsuario"].ToString();
                var consulta = db.Usuarios.Where(u => u.correoElectronicoUsuario.Equals(usuario))
                    .Include(p => p.Proyectos.Select(pr => pr.proyecto)).FirstOrDefault();
                //return View(db.ProyectoUsuario.Where(d => d.correoElectronicoUsuario.CompareTo(usuario) == 0).Include(p => p.proyecto).Include(u => u.usuario).ToList());
                List<Proyecto> listaProyectos = new List<Proyecto>();
                foreach (var item in consulta.Proyectos)
                {
                    
                    listaProyectos.Add(item.proyecto);
                }
                //return View(listaProyectos);
                return View(consulta.Proyectos);
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
                proyecto.estadoProyecto = "abierto";

                ProyectoUsuarioRelacion p = new ProyectoUsuarioRelacion();
                p.ProyectoId = proyecto.ProyectoId;
                string nombreUsuario = (string)Session["NombreUsuario"];
                var usuario = db.Usuarios.Where(u => u.correoElectronicoUsuario.Equals(nombreUsuario)).FirstOrDefault();
                //p.usuario.correoElectronicoUsuario = (string)Session["NombreUsuario"];
                p.usuario = usuario;
                p.rolUsuario = "administrador";
                db.Proyectoes.Add(proyecto);
                db.ProyectoUsuario.Add(p);
                db.SaveChanges();

                return RedirectToAction("ListaUsuarios", new { IdProyecto = p.ProyectoId });
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
            var validacion = db.ProyectoUsuario.Where(p => p.ProyectoId == IdProyecto).Include( p => p.usuario).ToList();

            List<string> usuarios = new List<string>();

            foreach (ProyectoUsuarioRelacion i in validacion)
            {

                usuarios.Add(i.usuario.correoElectronicoUsuario);

                if (i.usuario.correoElectronicoUsuario.CompareTo(usuario) == 0)
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
            var lista = db.ProyectoUsuario.Where(p => p.ProyectoId == IdProyecto).Include(i => i.proyecto).Include(u => u.usuario).ToList();
            return View(lista);
        }


        public ActionResult UsuariosProyecto(int? IdProyecto)
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

            InformacionDeProyecto info = new InformacionDeProyecto();
            info.proyecto = proyecto;
            info.equipo= db.ProyectoUsuario.Where(p => p.ProyectoId == IdProyecto).Include(p => p.usuario).ToList();
            info.productBacklog = db.HistoriaUsuarios.Where(p => p.ProyectoId== IdProyecto).OrderBy(c=>c.prioridad).ToList();
            info.sprints = db.Sprint.Where(p => p.ProyectoId == IdProyecto).ToList();
            
            
            return View(info);
        }
        

        public ActionResult AgregarIntegrante(int? id)
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
            ViewBag.ID = id;
            return View();//debe enviarle a la vista el parametro id que tiene el id del proyecto al que se va a agregar el integrnate
        }


        [System.Web.Mvc.Route("api/InsertarIntegrante/{IdUsuario}")]
        public ActionResult EliminarIntegrante(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProyectoUsuarioRelacion integrante = db.ProyectoUsuario.Find(id);
            if (integrante == null)
            {
                return HttpNotFound();
            }
            return View(integrante);
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
