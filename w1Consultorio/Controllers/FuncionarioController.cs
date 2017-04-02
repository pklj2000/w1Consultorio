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
    public class FuncionarioController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Funcionario/

        public ActionResult Index(string codEmp)
        {
            int _codEmp = string.IsNullOrEmpty(codEmp) ? 0 : Convert.ToInt32(codEmp);
            ViewBag.codEmp = _codEmp;
            CarregarEmpresas();
            var funcionarios = db.Funcionarios.Include(f => f.Cargo).Where(x => x.Cargo.Departamento.CodEmpresa == _codEmp);

            if (_codEmp > 0 && funcionarios.Count() == 0)
            {
                TempData["mensagemErro"] = "Nenhum funcionáro encontrado";
            }

            return View(funcionarios.ToList());
        }

        //
        // GET: /Funcionario/Details/5

        public ActionResult Details(int id = 0)
        {
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        //
        // GET: /Funcionario/Create

        public ActionResult Create(string codEmp)
        {
            Int32 _codEmp;
            try
            {
                _codEmp = Convert.ToInt32(codEmp);
            }
            catch
            {
                _codEmp = 0;
            }

            var funcionarioItem = new Funcionario();
            funcionarioItem.CodEmpresa = _codEmp;
            funcionarioItem.Cargo = new Cargo();
            funcionarioItem.Cargo.Departamento = new Departamento();
            funcionarioItem.Cargo.Departamento.CodEmpresa = _codEmp;

            if (!string.IsNullOrEmpty(codEmp))
            {
                CarregarCargos(Convert.ToInt32(codEmp));
                ViewBag.CodCargo = new SelectList(db.Cargos, "CodCargo", "Descricao");
                ViewBag.CodEstCivil = new SelectList(db.EstadosCivis, "CodEstCivil", "Descricao");
                ViewBag.CodPeriodicidade = new SelectList(db.Periodicidades, "CodPeriodicidade", "Descricao");
            }
            return View(funcionarioItem);
        }

        //
        // POST: /Funcionario/Create

        [HttpPost]
        public ActionResult Create(Funcionario funcionario, string codEmp)
        {
            if (ModelState.IsValid)
            {
                db.Funcionarios.Add(funcionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodCargo = new SelectList(db.Cargos, "CodCargo", "Descricao", funcionario.CodCargo);
            ViewBag.CodEstCivil = new SelectList(db.EstadosCivis, "CodEstCivil", "Descricao", funcionario.CodEstCivil);
            ViewBag.CodPeriodicidade = new SelectList(db.Periodicidades, "CodPeriodicidade", "Descricao", funcionario.CodPeriodicidade);

            funcionario.Cargo = new Cargo();
            funcionario.Cargo.Departamento = new Departamento();
            funcionario.Cargo.Departamento.CodEmpresa = Convert.ToInt32(codEmp);

            return View(funcionario);
        }

        //
        // GET: /Funcionario/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Funcionario funcionario = db.Funcionarios.Include(est=>est.EstadoCivil).Where(f=>f.CodFuncionario == id).FirstOrDefault();
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            CarregarCargos(funcionario.Cargo.Departamento.CodEmpresa);
            ViewBag.EstCivilCollection = new SelectList(db.EstadosCivis, "CodEstCivil", "Descricao", funcionario.CodEstCivil);
            ViewBag.PeriodicidadeCollection = new SelectList(db.Periodicidades, "CodPeriodicidade", "Descricao", funcionario.CodPeriodicidade);
            return View(funcionario);
        }

        //
        // POST: /Funcionario/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind(Exclude="Cargo")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodCargo = new SelectList(db.Cargos, "CodCargo", "Descricao", funcionario.CodCargo);
            ViewBag.CodEstCivil = new SelectList(db.EstadosCivis, "CodEstCivil", "Descricao", funcionario.CodEstCivil);
            ViewBag.CodPeriodicidade = new SelectList(db.Periodicidades, "CodPeriodicidade", "Descricao", funcionario.CodPeriodicidade);
            return View(funcionario);
        }

        //
        // GET: /Funcionario/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        //
        // POST: /Funcionario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Funcionario funcionario = db.Funcionarios.Find(id);
            db.Funcionarios.Remove(funcionario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private void CarregarEmpresas()
        {
            ViewBag.Empresas = new SelectList(db.Empresas, "CodEmpresa", "Nome");
        }

        private void CarregarDepartamentos(int codEmp)
        {
            var departamentoLista = db.Departamentos.Where(x => x.CodEmpresa == codEmp);
            ViewBag.Departamento = new SelectList(departamentoLista, "CodDepartamento", "Descricao");
        }

        private void CarregarCargos(int codEmp)
        {
            var cargoCollection = db.Cargos.Where(x => x.Departamento.CodEmpresa == codEmp);
            //var cargoLista = new SelectList();
            foreach (Cargo cargoItem in cargoCollection)
            {
                cargoItem.Descricao = string.Format("{0} / {1}", cargoItem.Departamento.Descricao, cargoItem.Descricao);
            }

            ViewBag.Cargo = new SelectList(cargoCollection, "CodCargo", "Descricao");
        }
    }
}