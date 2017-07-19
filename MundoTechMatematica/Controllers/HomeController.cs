using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using MundoTechMatematica.Models;
using System.Web.Mvc;

namespace MundoTechMatematica.Controllers
{
    public class HomeController : Controller
    {
        private Models.ComercioEntities bd = new Models.ComercioEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //pruebas
        public ActionResult Buscar(string id="")
        {
            //acceso a base de datos
            var cursos = bd.Curso.Where(x => x.Descripcion.Contains(id)).ToList();
            //lista de productos
            return View(cursos);
        }

        public ActionResult Cursodetalle(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = bd.Curso.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
            
        }

        public ActionResult Nosotros()
        {
            return View();
        }

        
        public ActionResult Contacto()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Carrito()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }
    }
}