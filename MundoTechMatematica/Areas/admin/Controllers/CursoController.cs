using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MundoTechMatematica.Models;
using System.IO;

namespace MundoTechMatematica.Areas.admin.Controllers
{
    public class CursoController : Controller
    {
        private ComercioEntities db = new ComercioEntities();

        // GET: admin/Curso
        public ActionResult Index()
        {
            return View(db.Curso.ToList());
        }

        // GET: admin/Curso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Curso.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // GET: admin/Curso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Curso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CursoId,Denominacion,Descripcion,Precio,Activo")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                db.Curso.Add(curso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(curso);
        }

        // GET: admin/Curso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Curso.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: admin/Curso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CursoId,Denominacion,Descripcion,Precio,Activo")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(curso);
        }

        // GET: admin/Curso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Curso.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: admin/Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Curso.Find(id);
            db.Curso.Remove(curso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        public JsonResult Adjuntar(int CursoId, HttpPostedFileBase documento)
        {
            var respuesta = new Models.ResponseModel
            {
                respuesta = true,
                error = ""
            };

            if (documento != null)
            {
                string adjunto = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(documento.FileName);
                documento.SaveAs(Server.MapPath("~/ImgCursos/" + adjunto));

                db.CursoImagen.Add(new CursoImagen { CursoId =CursoId, Imagen = adjunto, Titulo = "Ejemplo", Descripcion = "Ejemplo" });
                db.SaveChanges();

            }
            else
            {
                respuesta.respuesta = false;
                respuesta.error = "Debe adjuntar un documento";
            }

            return Json(respuesta);
        }

        public PartialViewResult Adjuntos(int CursoId)
        {
            return PartialView(db.CursoImagen.Where(x => x.CursoId == CursoId).ToList());
        }


        public JsonResult EliminarImagen(int CursoImagenId)
        {
            var rpt = new Models.ResponseModel()
            {
                respuesta = true,
                error = ""
            };
            var img = db.CursoImagen.Find(CursoImagenId);

            if (System.IO.File.Exists(Server.MapPath("~/ImgProductos/" + img.Imagen)))
                System.IO.File.Delete(Server.MapPath("~/ImgProductos/" + img.Imagen));

            db.CursoImagen.Remove(img);
            db.SaveChanges();

            return Json(rpt);
        }

        ///


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
