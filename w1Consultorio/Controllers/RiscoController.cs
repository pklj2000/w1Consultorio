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
    public class RiscoController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Risco/

        public ActionResult Index()
        {
            return View(db.Riscos.ToList());
        }

        //
        // GET: /Risco/Details/5

        public ActionResult Details(int id = 0)
        {
            Risco risco = db.Riscos.Find(id);
            if (risco == null)
            {
                return HttpNotFound();
            }
            return View(risco);
        }

        //
        // GET: /Risco/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Risco/Create

        [HttpPost]
        public ActionResult Create(Risco risco)
        {
            if (ModelState.IsValid)
            {
                db.Riscos.Add(risco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(risco);
        }

        //
        // GET: /Risco/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Risco risco = db.Riscos.Find(id);
            if (risco == null)
            {
                return HttpNotFound();
            }
            return View(risco);
        }

        //
        // POST: /Risco/Edit/5

        [HttpPost]
        public ActionResult Edit(Risco risco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(risco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(risco);
        }

        //
        // GET: /Risco/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Risco risco = db.Riscos.Find(id);
            if (risco == null)
            {
                return HttpNotFound();
            }
            return View(risco);
        }

        //
        // POST: /Risco/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Risco risco = db.Riscos.Find(id);
            db.Riscos.Remove(risco);
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