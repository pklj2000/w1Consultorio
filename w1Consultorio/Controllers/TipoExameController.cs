using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w1Consultorio.Models;

namespace w1Consultorio.Controllers
{
    public class TipoExameController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /TipoExame/

        public ActionResult Index()
        {
            return View(db.TiposExame.ToList());
        }

        //
        // GET: /TipoExame/Details/5

        public ActionResult Details(int id = 0)
        {
            TipoExame tipoexame = db.TiposExame.Find(id);
            if (tipoexame == null)
            {
                return HttpNotFound();
            }
            return View(tipoexame);
        }

        //
        // GET: /TipoExame/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TipoExame/Create

        [HttpPost]
        public ActionResult Create(TipoExame tipoexame)
        {
            if (ModelState.IsValid)
            {
                db.TiposExame.Add(tipoexame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoexame);
        }

        //
        // GET: /TipoExame/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TipoExame tipoexame = db.TiposExame.Find(id);
            if (tipoexame == null)
            {
                return HttpNotFound();
            }
            return View(tipoexame);
        }

        //
        // POST: /TipoExame/Edit/5

        [HttpPost]
        public ActionResult Edit(TipoExame tipoexame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoexame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoexame);
        }

        //
        // GET: /TipoExame/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TipoExame tipoexame = db.TiposExame.Find(id);
            if (tipoexame == null)
            {
                return HttpNotFound();
            }
            return View(tipoexame);
        }

        //
        // POST: /TipoExame/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoExame tipoexame = db.TiposExame.Find(id);
            db.TiposExame.Remove(tipoexame);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}