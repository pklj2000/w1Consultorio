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
    public class PeriodicidadeController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Periodicidade/

        public ActionResult Index()
        {
            return View(db.Periodicidades.ToList());
        }

        //
        // GET: /Periodicidade/Details/5

        public ActionResult Details(int id = 0)
        {
            Periodicidade periodicidade = db.Periodicidades.Find(id);
            if (periodicidade == null)
            {
                return HttpNotFound();
            }
            return View(periodicidade);
        }

        //
        // GET: /Periodicidade/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Periodicidade/Create

        [HttpPost]
        public ActionResult Create(Periodicidade periodicidade)
        {
            if (ModelState.IsValid)
            {
                db.Periodicidades.Add(periodicidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(periodicidade);
        }

        //
        // GET: /Periodicidade/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Periodicidade periodicidade = db.Periodicidades.Find(id);
            if (periodicidade == null)
            {
                return HttpNotFound();
            }
            return View(periodicidade);
        }

        //
        // POST: /Periodicidade/Edit/5

        [HttpPost]
        public ActionResult Edit(Periodicidade periodicidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periodicidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periodicidade);
        }

        //
        // GET: /Periodicidade/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Periodicidade periodicidade = db.Periodicidades.Find(id);
            if (periodicidade == null)
            {
                return HttpNotFound();
            }
            return View(periodicidade);
        }

        //
        // POST: /Periodicidade/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Periodicidade periodicidade = db.Periodicidades.Find(id);
            db.Periodicidades.Remove(periodicidade);
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