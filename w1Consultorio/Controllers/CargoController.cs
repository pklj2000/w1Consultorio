using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w1Consultorio.Models;
using w1Consultorio.Models.ViewModel;

namespace w1Consultorio.Controllers
{
    public class CargoController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Cargo/

        public ActionResult Index(string codEmp, string codDepto)
        {
            if(string.IsNullOrEmpty(codEmp) && !string.IsNullOrEmpty(codDepto))
            {
                int codDeptoAux = Convert.ToInt32(codDepto);
                codEmp = db.Departamentos.Where(x => x.CodDepartamento == codDeptoAux).Select(x => x.CodEmpresa).FirstOrDefault().ToString();
            }

            CarregarEmpresas();
            CarregarDepartamentos(codEmp);

            Int32 codDepartamento = string.IsNullOrEmpty(codDepto) ? 0 : Convert.ToInt32(codDepto);
            ViewBag.codEmp = codEmp;
            ViewBag.codDepto = codDepartamento;

            var cargos = db.Cargos
                .Include(c => c.Departamento)
                .Include(c => c.Periodicidade)
                .Where(x => x.codDepartamento == codDepartamento);

            if (cargos.Count() == 0 && !string.IsNullOrEmpty(codEmp) && !string.IsNullOrEmpty(codDepto))
                TempData["mensagemErro"] = "Nenhum registro selecionado";

            return View(cargos.ToList());
        }

        //
        // GET: /Cargo/Details/5

        public ActionResult Details(int id = 0)
        {
            Cargo cargo = db.Cargos
    .Include(c => c.Exames)
    .Include(c => c.Periodicidade)
    .Where(x => x.CodCargo == id)
    .FirstOrDefault();

            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        //
        // GET: /Cargo/Create

        public ActionResult Create(string codEmp, string codDepto)
        {
            Cargo cargo = new Cargo();

            if (!string.IsNullOrEmpty(codDepto))
            {
                int codDeptoInt = Convert.ToInt32(codDepto);
                cargo.codDepartamento = codDeptoInt;
                cargo.Departamento = db.Departamentos.Where(d => d.CodDepartamento == codDeptoInt).FirstOrDefault();
            }

            if (cargo.Departamento == null)
            {
                cargo.codDepartamento = 0;
                cargo.Departamento = new Departamento();
                TempData["mensagemErro"] = "O sistema perdeu a referência para o departamento. Volte para a tela de pesquisa e filtre a empresa/departamento novamente.";
            }

            CarregarEmpresas();
            CarregarDepartamentos(codEmp);
            PopularExameData();
            PopularRiscoData();

            ViewBag.codDepartamento = new SelectList(db.Departamentos, "CodDepartamento", "Descricao");
            ViewBag.codPeriodicidade = new SelectList(db.Periodicidades, "CodPeriodicidade", "Descricao");
            return View(cargo);
        }

        //
        // POST: /Cargo/Create

        [HttpPost]
        public ActionResult Create(string command, Cargo cargo, string codEmp, string[] exameAssociado, string[] riscoAssociado)
        {
            Exame exame;
            Risco risco;

            if (exameAssociado != null)
            {
                if (exameAssociado.Length > 0 && cargo.Exames == null) cargo.Exames = new List<Exame>();
                foreach (string item in exameAssociado)
                {
                    exame = new Exame();
                    int codExame = Convert.ToInt32(item);
                    exame = db.Exames.Where(x => x.CodExame == codExame).FirstOrDefault();
                    cargo.Exames.Add(exame);
                }
            }

            if (riscoAssociado != null)
            {
                if (riscoAssociado.Length > 0 && cargo.Riscos == null) cargo.Riscos = new List<Risco>();
                foreach (string item in riscoAssociado)
                {
                    risco = new Risco();
                    int codRisco = Convert.ToInt32(item);
                    risco = db.Riscos.Where(x => x.CodRisco == codRisco).FirstOrDefault();
                    cargo.Riscos.Add(risco);
                }
            }

            if (ModelState.IsValid)
            {
                cargo.Departamento = null;
                db.Cargos.Add(cargo);
                db.SaveChanges();
                return RedirectToAction("Index", new { codDepto = cargo.codDepartamento });
            }

            if (string.IsNullOrEmpty(command))
            {
                foreach (var modelValue in ModelState.Values)
                {
                    modelValue.Errors.Clear();
                }
            }

            CarregarEmpresas();
            CarregarDepartamentos(codEmp);
            if (cargo.Exames != null)
                PopularExameData(cargo);
            else
                PopularExameData();

            if (cargo.Riscos != null)
                PopularRiscoData(cargo);
            else
                PopularRiscoData();

            ViewBag.codDepartamento = new SelectList(db.Departamentos, "CodDepartamento", "Descricao", cargo.codDepartamento);
            ViewBag.codPeriodicidade = new SelectList(db.Periodicidades, "CodPeriodicidade", "Descricao", cargo.codPeriodicidade);
            return View(cargo);
        }

        //
        // GET: /Cargo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cargo cargo = db.Cargos
                .Include(c => c.Exames)
                .Include(c => c.Periodicidade)
                .Where(x=>x.CodCargo == id)
                .FirstOrDefault();

            if (cargo == null)
            {
                return HttpNotFound();
            }
            PopularExameData(cargo);
            PopularRiscoData(cargo);

            ViewBag.listaDepartamento = new SelectList(db.Departamentos, "CodDepartamento", "Descricao", cargo.codDepartamento);
            ViewBag.listaPeriodicidade = new SelectList(db.Periodicidades, "CodPeriodicidade", "Descricao", cargo.codPeriodicidade);
            return View(cargo);
        }

        //
        // POST: /Cargo/Edit/5

        [HttpPost]
        public ActionResult Edit(Cargo cargo, string[] exameAssociado, string[] riscoAssociado)
        {

            if (ModelState.IsValid)
            {
                var cargoAtualizar = db.Cargos
                    .Include(i => i.Exames)
                    .Include(x => x.Riscos)
                    .Where(w => w.CodCargo == cargo.CodCargo)
                    .Single();

                if (exameAssociado == null)
                {
                    cargoAtualizar.Exames = new List<Exame>();
                }
                else
                {
                    var selectedExamesHash = new HashSet<string>(exameAssociado);
                    var cargoExame = new HashSet<int>(cargoAtualizar.Exames.Select(c => c.CodExame));

                    foreach (Exame exameItem in db.Exames)
                    {
                        if (selectedExamesHash.Contains(exameItem.CodExame.ToString()))
                        {
                            if (!cargoAtualizar.Exames.Contains(exameItem))
                            {
                                cargoAtualizar.Exames.Add(exameItem);
                            }
                        }
                        else
                        {
                            if(cargoAtualizar.Exames.Contains(exameItem))
                            {
                                cargoAtualizar.Exames.Remove(exameItem);
                            }
                        }
                    }
                }

                if (riscoAssociado == null)
                {
                    cargoAtualizar.Riscos = new List<Risco>();
                }
                else
                {
                    var selectedRiscoHash = new HashSet<string>(riscoAssociado);
                    var cargoRisco = new HashSet<int>(cargoAtualizar.Riscos.Select(r=>r.CodRisco));

                    foreach(Risco riscoItem in db.Riscos)
                    {
                        if(selectedRiscoHash.Contains(riscoItem.CodRisco.ToString()))
                        {
                            if(!cargoAtualizar.Riscos.Contains(riscoItem))
                            {
                                cargoAtualizar.Riscos.Add(riscoItem);
                            }
                        }
                        else
                        {
                            if(cargoAtualizar.Riscos.Contains(riscoItem))
                            {
                                cargoAtualizar.Riscos.Remove(riscoItem);
                            }
                        }
                    }
                }


                db.Entry(cargoAtualizar).State = EntityState.Modified;
                db.SaveChanges();

                cargo.Departamento = db.Departamentos.Where(x => x.CodDepartamento == cargo.codDepartamento).FirstOrDefault();
                return RedirectToAction("Index", new { codEmp = cargo.Departamento.CodEmpresa, codDepto = cargo.codDepartamento});
            }
            ViewBag.codDepartamento = new SelectList(db.Departamentos, "CodDepartamento", "Descricao", cargo.codDepartamento);
            ViewBag.codPeriodicidade = new SelectList(db.Periodicidades, "CodPeriodicidade", "Descricao", cargo.codPeriodicidade);
            return View(cargo);
        }

        //
        // GET: /Cargo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cargo cargo = db.Cargos.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        //
        // POST: /Cargo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cargo cargo = db.Cargos.Find(id);
            db.Cargos.Remove(cargo);
            db.SaveChanges();
            return RedirectToAction("Index", new { codDepto = cargo.codDepartamento });
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

        private void CarregarDepartamentos(string codEmp)
        {
            Int32 codEmpresa = string.IsNullOrEmpty(codEmp) ? 0 : Convert.ToInt32(codEmp);
            var deptos = db.Departamentos.Where(x => x.CodEmpresa == codEmpresa);
            ViewBag.Departamento = new SelectList(deptos, "CodDepartamento", "Descricao");
        }

        private void PopularExameData(Cargo cargo)
        {
            var exames = db.Exames;
            var viewModel = new List<AssignedExameData>();

            var cargoExame = new HashSet<int>(cargo.Exames.Select(c => c.CodExame));

            foreach (var item in exames)
            {
                viewModel.Add(new AssignedExameData
                {
                    CodExame = item.CodExame,
                    Descricao = item.Descricao,
                    Assigned = cargoExame.Contains(item.CodExame)
                });
            }
            ViewBag.Exames = viewModel;
        }

        private void PopularExameData()
        {
            var exames = db.Exames;
            var viewModel = new List<AssignedExameData>();

            foreach (var item in exames)
            {
                viewModel.Add(new AssignedExameData
                {
                    CodExame = item.CodExame,
                    Descricao = item.Descricao,
                    Assigned = false
                });
            }
            ViewBag.Exames = viewModel;
        }

        private void PopularRiscoData(Cargo cargo)
        {
            var riscos = db.Riscos;
            var viewModel = new List<AssignedRiscoData>();

            var cargoRisco = new HashSet<int>(cargo.Riscos.Select(c => c.CodRisco));

            foreach (var item in riscos)
            {
                viewModel.Add(new AssignedRiscoData
                {
                    CodRisco = item.CodRisco,
                    Descricao = item.Descricao,
                    Assigned = cargoRisco.Contains(item.CodRisco)
                });
            }
            ViewBag.Riscos = viewModel;
        }

        private void PopularRiscoData()
        {
            var riscos = db.Riscos;
            var viewModel = new List<AssignedRiscoData>();

            foreach (var item in riscos)
            {
                viewModel.Add(new AssignedRiscoData
                {
                    CodRisco = item.CodRisco,
                    Descricao = item.Descricao,
                    Assigned = false
                });
            }
            ViewBag.Riscos = viewModel;
        }
    }
}