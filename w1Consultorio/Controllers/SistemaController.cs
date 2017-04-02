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
    public class SistemaController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Sistema/

        public ActionResult Index()
        {
            return View(db.Sistema.ToList());
        }

        //
        // GET: /Sistema/Details/5

        public ActionResult Details(int id = 0)
        {
            Sistema sistema = db.Sistema.Find(id);
            if (sistema == null)
            {
                return HttpNotFound();
            }
            return View(sistema);
        }

        //
        // GET: /Sistema/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Sistema/Create

        [HttpPost]
        public ActionResult Create(Sistema sistema)
        {
            if (!ValidarCodSistemaUnico(sistema))
            {
                ModelState.AddModelError("Sistema", "Código de sistema já utilizado");
                return View(sistema);
            }

            if (ModelState.IsValid)
            {
                db.Sistema.Add(sistema);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sistema);
        }

        //
        // GET: /Sistema/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Sistema sistema = db.Sistema.Find(id);
            if (sistema == null)
            {
                return HttpNotFound();
            }
            return View(sistema);
        }

        //
        // POST: /Sistema/Edit/5

        [HttpPost]
        public ActionResult Edit(Sistema sistema)
        {
            if (!ValidarCodSistemaUnico(sistema))
            {
                ModelState.AddModelError("Sistema", "Código de sistema já utilizado");
                return View(sistema);
            }

            if (ModelState.IsValid)
            {
                db.Entry(sistema).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sistema);
        }

        //
        // GET: /Sistema/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Sistema sistema = db.Sistema.Find(id);
            if (sistema == null)
            {
                return HttpNotFound();
            }
            return View(sistema);
        }

        //
        // POST: /Sistema/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Sistema sistema = db.Sistema.Find(id);
            db.Sistema.Remove(sistema);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private bool ValidarCodSistemaUnico(Sistema sistema)
        {
            var sistemaCollection = db.Sistema.Where(x => x.CodRefSistema == sistema.CodRefSistema);
            int cont = sistemaCollection.Where(x=>x.CodSistema != sistema.CodSistema).Count();
            return cont == 0;
        }
    }
}