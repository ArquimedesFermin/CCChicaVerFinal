using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CChica.Models;

namespace CChica.Controllers
{
    [Authorize]
    public class TipoEstadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoEstados
        public ActionResult Index()
        {
            return View(db.TipoEstados.ToList());
        }

        // GET: TipoEstados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEstado tipoEstado = db.TipoEstados.Find(id);
            if (tipoEstado == null)
            {
                return HttpNotFound();
            }
            return View(tipoEstado);
        }

        // GET: TipoEstados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEstados/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Estado")] TipoEstado tipoEstado)
        {
            if (ModelState.IsValid)
            {
                db.TipoEstados.Add(tipoEstado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoEstado);
        }

        // GET: TipoEstados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEstado tipoEstado = db.TipoEstados.Find(id);
            if (tipoEstado == null)
            {
                return HttpNotFound();
            }
            return View(tipoEstado);
        }




        // POST: TipoEstados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Estado")] TipoEstado tipoEstado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoEstado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoEstado);
        }

        // GET: TipoEstados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEstado tipoEstado = db.TipoEstados.Find(id);
            if (tipoEstado == null)
            {
                return HttpNotFound();
            }
            return View(tipoEstado);
        }

        // POST: TipoEstados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoEstado tipoEstado = db.TipoEstados.Find(id);
            db.TipoEstados.Remove(tipoEstado);
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
