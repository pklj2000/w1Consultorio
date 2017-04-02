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
    public class AtendimentoController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Atendimento/

        public ActionResult Index()
        {
            return View(db.Atendimento.ToList());
        }

        //
        // GET: /Atendimento/Details/5

        public ActionResult Details(int id = 0)
        {
            Atendimento atendimento = db.Atendimento.Find(id);
            if (atendimento == null)
            {
                return HttpNotFound();
            }
            return View(atendimento);
        }

        //
        // GET: /Atendimento/Create

        public ActionResult Create(string codEmpresa)
        {
            int _codEmp = 0;

            if (!string.IsNullOrEmpty(codEmpresa)) _codEmp = Convert.ToInt32(codEmpresa);

            CarregarEmpresas(_codEmp);
            CarregarFuncionarios(_codEmp, 0);
            CarregarTipoExame();

            Atendimento atendimento = new Atendimento();
            atendimento.dataAtendimento = DateTime.Now;
            return View(atendimento);
        }

        //
        // POST: /Atendimento/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                atendimento.Ativo = true;
                db.Atendimento.Add(atendimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                CarregarEmpresas(0);
                CarregarFuncionarios(0,0);
                CarregarTipoExame();
            }
            return View(atendimento);
        }

        //
        // GET: /Atendimento/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Atendimento atendimento = db.Atendimento.Find(id);
            if (atendimento == null)
            {
                return HttpNotFound();
            }


            CarregarEmpresas(0);
            CarregarFuncionarios(atendimento.codEmpresa, atendimento.codFuncionario);
            CarregarTipoExame();

            return View(atendimento);
        }

        //
        // POST: /Atendimento/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atendimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(atendimento);
        }

        //
        // GET: /Atendimento/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Atendimento atendimento = db.Atendimento.Find(id);
            if (atendimento == null)
            {
                return HttpNotFound();
            }
            return View(atendimento);
        }

        //
        // POST: /Atendimento/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Atendimento atendimento = db.Atendimento.Find(id);
            db.Atendimento.Remove(atendimento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private void CarregarEmpresas(int codEmp)
        {
            ViewBag.Empresas = new SelectList(db.Empresas, "CodEmpresa", "Nome", codEmp);
        }

        private void CarregarFuncionarios(int codEmpresa, int codFuncionarioSel)
        {
            var result = db.Funcionarios.Where(x => x.CodEmpresa == codEmpresa);
            ViewBag.Funcionarios = new SelectList(result, "codFuncionario", "Nome", codFuncionarioSel);
        }

        private void CarregarTipoExame()
        {
            ViewBag.TiposExame = new SelectList(db.TiposExame, "CodTipoExame", "Descricao");
        }

        public JsonResult CarregarFuncionariosJSON(string id)
        {
            int empresa = 0;

            try { empresa = Convert.ToInt32(id); }
            catch { empresa = 0; }

            var aux = db.Funcionarios.Where(x => x.CodEmpresa == empresa).ToList();
            return Json(new SelectList(aux, "codFuncionario", "Nome"));
        }
    }
}