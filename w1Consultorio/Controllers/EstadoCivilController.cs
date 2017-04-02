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
    public class EstadoCivilController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /EstadoCivil/

        public ActionResult Index()
        {
            return View(db.EstadosCivis.ToList());
        }

        //
        // GET: /EstadoCivil/Details/5

        public ActionResult Details(int id = 0)
        {
            EstadoCivil estadocivil = db.EstadosCivis.Find(id);
            if (estadocivil == null)
            {
                return HttpNotFound();
            }
            return View(estadocivil);
        }

        //
        // GET: /EstadoCivil/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /EstadoCivil/Create

        [HttpPost]
        public ActionResult Create(EstadoCivil estadocivil)
        {
            if (ModelState.IsValid)
            {
                db.EstadosCivis.Add(estadocivil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadocivil);
        }

        //
        // GET: /EstadoCivil/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EstadoCivil estadocivil = db.EstadosCivis.Find(id);
            if (estadocivil == null)
            {
                return HttpNotFound();
            }
            return View(estadocivil);
        }

        //
        // POST: /EstadoCivil/Edit/5

        [HttpPost]
        public ActionResult Edit(EstadoCivil estadocivil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadocivil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadocivil);
        }

        //
        // GET: /EstadoCivil/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EstadoCivil estadocivil = db.EstadosCivis.Find(id);
            if (estadocivil == null)
            {
                return HttpNotFound();
            }
            return View(estadocivil);
        }

        //
        // POST: /EstadoCivil/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoCivil estadocivil = db.EstadosCivis.Find(id);
            db.EstadosCivis.Remove(estadocivil);
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