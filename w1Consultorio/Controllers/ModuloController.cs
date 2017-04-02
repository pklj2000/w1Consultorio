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
    public class ModuloController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Modulo/

        public ActionResult SetSistema(string sistemaid)
        {
            return RedirectToAction("Index", new { sistemaid = sistemaid });
        }

        public ActionResult Index(string sistemaid)
        {
            var modulo = db.Modulo.Include(m => m.Sistema);
            if (sistemaid != null)
            {
                int sistemaidAux = Int32.Parse(sistemaid);
                modulo = modulo.Where(x => x.CodSistema == sistemaidAux);
            }
            ViewBag.Sistemas = new SelectList(db.Sistema, "CodSistema", "Nome",sistemaid);
            return View(modulo.ToList());
        }

        //
        // GET: /Modulo/Details/5

        public ActionResult Details(int id = 0)
        {
            Modulo modulo = db.Modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        //
        // GET: /Modulo/Create

        public ActionResult Create(string sistemaid)
        {
            ViewBag.Sistemas = new SelectList(db.Sistema, "CodSistema", "CodRefSistema", sistemaid);
            return View();
        }

        //
        // POST: /Modulo/Create

        [HttpPost]
        public ActionResult Create(Modulo modulo, string sistemaid)
        {
            if (ModelState.IsValid)
            {
                db.Modulo.Add(modulo);
                db.SaveChanges();
                return RedirectToAction("Index", new { sistemaid = sistemaid });
            }

            ViewBag.CodSistema = new SelectList(db.Sistema, "CodSistema", "CodRefSistema", modulo.CodSistema);
            return View(modulo);
        }

        //
        // GET: /Modulo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Modulo modulo = db.Modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodSistema = new SelectList(db.Sistema, "CodSistema", "CodRefSistema", modulo.CodSistema);
            return View(modulo);
        }

        //
        // POST: /Modulo/Edit/5

        [HttpPost]
        public ActionResult Edit(Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { sistemaid = modulo.CodSistema});
            }
            ViewBag.CodSistema = new SelectList(db.Sistema, "CodSistema", "CodRefSistema",modulo.CodSistema);
            return View(modulo);
        }

        //
        // GET: /Modulo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Modulo modulo = db.Modulo.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        //
        // POST: /Modulo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Modulo modulo = db.Modulo.Find(id);
            db.Modulo.Remove(modulo);
            db.SaveChanges();
            return RedirectToAction("Index", new { sistemaid = modulo.CodSistema });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}