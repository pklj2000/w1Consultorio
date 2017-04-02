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
    public class ProfissionalController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Profissional/

        public ActionResult Index()
        {
            return View(db.Profissionais.ToList());
        }

        //
        // GET: /Profissional/Details/5

        public ActionResult Details(string id = null)
        {
            int codProfissional = Convert.ToInt32(id);
            Profissional profissional = db.Profissionais.Where(x => x.CodProfissional == codProfissional).FirstOrDefault();
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        //
        // GET: /Profissional/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Profissional/Create

        [HttpPost]
        public ActionResult Create(Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                db.Profissionais.Add(profissional);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profissional);
        }

        //
        // GET: /Profissional/Edit/5

        public ActionResult Edit(string id = null)
        {
            //Profissional profissional = db.Profissionais.Find(id);
            int codProfissional = Convert.ToInt32(id);
            Profissional profissional = db.Profissionais.Where(x => x.CodProfissional == codProfissional).FirstOrDefault();
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        //
        // POST: /Profissional/Edit/5

        [HttpPost]
        public ActionResult Edit(Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profissional).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profissional);
        }

        //
        // GET: /Profissional/Delete/5

        public ActionResult Delete(string id = null)
        {
            int codProfissional = Convert.ToInt32(id);
            Profissional profissional = db.Profissionais.Where(x => x.CodProfissional == codProfissional).FirstOrDefault();
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        //
        // POST: /Profissional/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            int codProfissional = Convert.ToInt32(id);
            Profissional profissional = db.Profissionais.Where(x => x.CodProfissional == codProfissional).FirstOrDefault();
            db.Profissionais.Remove(profissional);
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