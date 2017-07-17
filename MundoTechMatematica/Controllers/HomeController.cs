using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult Buscar(string id="")
        {
            //acceso a base de datos
            var cursos = bd.Curso.Where(x => x.Descripcion.Contains(id)).ToList();
            //lista de productos
            return View(cursos);
        }

        public ActionResult Cursodetalle()
        {
            return View();
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
    }
}