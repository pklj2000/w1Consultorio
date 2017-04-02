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
    public class DepartamentoController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Departamento/

        public ActionResult Index(string codEmp)
        {

            ViewBag.Empresas = new SelectList(db.Empresas, "CodEmpresa", "Nome");


            Int32 cod = string.IsNullOrEmpty(codEmp) ? 0 : Convert.ToInt32(codEmp);
            ViewBag.codEmp = cod;

            var departamentoes = db.Departamentos.Include(d => d.Empresa);

                departamentoes = from dep in departamentoes
                                 orderby dep.Descricao
                                 where dep.CodEmpresa == cod
                                 select dep;

                return View(departamentoes.ToList());

        }

        //
        // GET: /Departamento/Details/5

        public ActionResult Details(int id = 0)
        {
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        //
        // GET: /Departamento/Create

        public ActionResult Create(string codEmp)
        {
            //ViewBag.CodEmpresa = new SelectList(db.Empresas, "CodEmpresa", "Nome");

            //if (!string.IsNullOrEmpty(codEmp))
            //{
                Departamento departamento = new Departamento();
                departamento.CodEmpresa = Convert.ToInt32(codEmp);
                return View(departamento);
            //}

        }

        //
        // POST: /Departamento/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Departamentos.Add(departamento);
                db.SaveChanges();
                return RedirectToAction("Index", new { codEmp = departamento.CodEmpresa });
            }

            ViewBag.CodEmpresa = new SelectList(db.Empresas, "CodEmpresa", "Nome", departamento.CodEmpresa);
            return View(departamento);
        }

        //
        // GET: /Departamento/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodEmpresa = new SelectList(db.Empresas, "CodEmpresa", "Nome", departamento.CodEmpresa);
            return View(departamento);
        }

        //
        // POST: /Departamento/Edit/5

        [HttpPost]
        public ActionResult Edit(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { codEmp = departamento.CodEmpresa });
            }
            ViewBag.CodEmpresa = new SelectList(db.Empresas, "CodEmpresa", "Nome", departamento.CodEmpresa);
            return View(departamento);
        }

        //
        // GET: /Departamento/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Departamento departamento = db.Departamentos.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        //
        // POST: /Departamento/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento departamento = db.Departamentos.Find(id);
            db.Departamentos.Remove(departamento);
            db.SaveChanges();
            return RedirectToAction("Index", new { codEmp = departamento.CodEmpresa });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}