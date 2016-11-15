using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppEjemploLayout.Models;
using AppEjemploLayout.Models.ClasesUsuario;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace AppEjemploLayout.Controllers
{
    public class UsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            return RedirectToAction("InicioSesion");
        }

        // GET: Usuarios/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InformacionUsuario()
        {
            
            Usuario usuario = db.Usuarios.Find((string)Session["NombreUsuario"]);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }
        
        // GET: Usuarios/Create
        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarUsuario([Bind(Include = "correoElectronicoUsuario,nombresUsuario,apellidosUsuario,aliasUsuario,contraseñaUsuario,ComparePass")] Registro registro)
        {
            if (ModelState.IsValid)
            {
                if (db.Usuarios.Find(registro.correoElectronicoUsuario)==null)
                {
                    Usuario usuario = new Usuario();
                    usuario.correoElectronicoUsuario = registro.correoElectronicoUsuario;
                    usuario.nombresUsuario = registro.nombresUsuario;
                    usuario.apellidosUsuario = registro.apellidosUsuario;
                    usuario.aliasUsuario = registro.aliasUsuario;
                    usuario.contraseñaUsuario = Encrypt(registro.contraseñaUsuario);
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                    Session["Usuario"] = true;
                    Session["NombreUsuario"] = usuario.correoElectronicoUsuario;
                    return RedirectToAction("Index", "Proyectoes", null);
                }
                else
                {
                    Session["ExisteUsuario"] = true;
                    registro.correoElectronicoUsuario = "";
                    registro.contraseñaUsuario = "";
                    registro.ComparePass = "";
                    return View(registro);
                }

            }

            return View(registro);
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            Session["NombreUsuario"] = null;
            Session["FalloSesion"] = null;

            return RedirectToAction("Index","Home",null);

        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        public ActionResult InicioSesion()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InicioSesion(InicioSesion datos)
        {
            try
            {
                Usuario usuario = db.Usuarios.Find(datos.correoSesion);
                string con = Encrypt(datos.contraseñaUsuario);
                if (usuario != null && usuario.contraseñaUsuario.Equals(con))
                {
                    Session["Usuario"] = true;
                    Session["NombreUsuario"] = datos.correoSesion;
                    return RedirectToAction("Index", "Proyectoes", null);
                }
                else
                {
                    Session["FalloSesion"] = true;
                    return View();
                }
            }
            catch
            {
                return View();
            }
            
        }

        // GET: Usuarios/Edit/5
        public ActionResult EditarUsuario()
        {
            if (Session["NombreUsuario"]!=null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find((string)Session["NombreUsuario"]);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarUsuario([Bind(Include = "correoElectronicoUsuario,nombresUsuario,apellidosUsuario,aliasUsuario,contraseñaUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
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
