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
    public class PerguntaGrupoController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /PerguntaGrupo/

        public ActionResult Index()
        {
            return View(db.PerguntaGrupos.ToList());
        }

        //
        // GET: /PerguntaGrupo/Details/5

        public ActionResult Details(int id = 0)
        {
            PerguntaGrupo perguntagrupo = db.PerguntaGrupos.Find(id);
            if (perguntagrupo == null)
            {
                return HttpNotFound();
            }
            return View(perguntagrupo);
        }

        //
        // GET: /PerguntaGrupo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PerguntaGrupo/Create

        [HttpPost]
        public ActionResult Create(PerguntaGrupo perguntagrupo)
        {
            if (ModelState.IsValid)
            {
                db.PerguntaGrupos.Add(perguntagrupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(perguntagrupo);
        }

        //
        // GET: /PerguntaGrupo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PerguntaGrupo perguntagrupo = db.PerguntaGrupos.Find(id);
            if (perguntagrupo == null)
            {
                return HttpNotFound();
            }
            return View(perguntagrupo);
        }

        //
        // POST: /PerguntaGrupo/Edit/5

        [HttpPost]
        public ActionResult Edit(PerguntaGrupo perguntagrupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perguntagrupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(perguntagrupo);
        }

        //
        // GET: /PerguntaGrupo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PerguntaGrupo perguntagrupo = db.PerguntaGrupos.Find(id);
            if (perguntagrupo == null)
            {
                return HttpNotFound();
            }
            return View(perguntagrupo);
        }

        //
        // POST: /PerguntaGrupo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PerguntaGrupo perguntagrupo = db.PerguntaGrupos.Find(id);
            db.PerguntaGrupos.Remove(perguntagrupo);
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