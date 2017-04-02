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
    public class ExameController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Exame/

        public ActionResult Index()
        {
            return View(db.Exames.ToList());
        }

        //
        // GET: /Exame/Details/5

        public ActionResult Details(int id = 0)
        {
            //Exame exame = db.Exames.Find(id);
            Exame exame = db.Exames
                .Include(i => i.Periodicidade)
                .Where(i => i.CodExame == id).FirstOrDefault();
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        //
        // GET: /Exame/Create

        public ActionResult Create()
        {
            PopularPeriodicidadeData();
            return View();
        }

        //
        // POST: /Exame/Create

        [HttpPost]
        public ActionResult Create(Exame exame, string[] selectedPeriodicidades)
        {
            if (ModelState.IsValid)
            {
                Periodicidade periodicidade;
                int periodicidadeCodigo;
                if (selectedPeriodicidades.Length > 0 && exame.Periodicidade == null) exame.Periodicidade = new List<Periodicidade>();
                foreach (string item in selectedPeriodicidades)
                {
                    periodicidadeCodigo = Convert.ToInt32(item);
                    periodicidade = db.Periodicidades.Where(x=> x.CodPeriodicidade == periodicidadeCodigo).FirstOrDefault();
                    exame.Periodicidade.Add(periodicidade);
                }

                db.Exames.Add(exame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopularAssignedPeriodicidadeData(new Exame());
            //ViewBag.Periodicidades = db.Periodicidades.ToList();
            return View(exame);
        }

        private void PopularPeriodicidadeData()
        {
            var periodicidades = db.Periodicidades;
            var viewModel = new List<AssignedPeriodicidadeData>();
            foreach (var item in periodicidades)
            {
                viewModel.Add(new AssignedPeriodicidadeData
                {
                    CodPeriodicidade = item.CodPeriodicidade,
                    Descricao = item.Descricao,
                    Assigned = false
                });
            }
            ViewBag.Periodicidades = viewModel;
        }

        private void PopularAssignedPeriodicidadeData(Exame exame)
        {
            var periodicidades = db.Periodicidades;
            var examePeriodicidade = new HashSet<int>(exame.Periodicidade.Select(c=> c.CodPeriodicidade));
            var viewModel = new List<AssignedPeriodicidadeData>();
            foreach (var item in periodicidades)
            {
                viewModel.Add(new AssignedPeriodicidadeData
                {
                    CodPeriodicidade = item.CodPeriodicidade,
                    Descricao = item.Descricao,
                    Assigned = examePeriodicidade.Contains(item.CodPeriodicidade)
                });
            }
            ViewBag.Periodicidades = viewModel;
        }

        //
        // GET: /Exame/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Exame exame = db.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            PopularAssignedPeriodicidadeData(exame);
            return View(exame);
        }

        //
        // POST: /Exame/Edit/5

        [HttpPost]
        public ActionResult Edit(Exame exame, string[] selectedPeriodicidades)
        {
            if (ModelState.IsValid)
            {
                var exameAtualizar = db.Exames
                    .Include(i => i.Periodicidade)
                    .Where(i => i.CodExame == exame.CodExame)
                    .Single();

                if (selectedPeriodicidades == null)
                {
                    exameAtualizar.Periodicidade = new List<Periodicidade>();
                }
                else
                {
                    var selectedPeriodicidadesHash = new HashSet<string>(selectedPeriodicidades);
                    var examePeriodicidade = new HashSet<int>(exameAtualizar.Periodicidade.Select(c => c.CodPeriodicidade));

                    foreach (var periodicidade in db.Periodicidades)
                    {
                        if (selectedPeriodicidadesHash.Contains(periodicidade.CodPeriodicidade.ToString()))
                        {
                            if (!exameAtualizar.Periodicidade.Contains(periodicidade))
                            {
                                exameAtualizar.Periodicidade.Add(periodicidade);
                            }
                        }
                        else
                        {
                            if (exameAtualizar.Periodicidade.Contains(periodicidade))
                            {
                                exameAtualizar.Periodicidade.Remove(periodicidade);
                            }
                        }
                    }
                }

                db.Entry(exameAtualizar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exame);
        }

        //
        // GET: /Exame/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Exame exame = db.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        //
        // POST: /Exame/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Exame exame = db.Exames.Find(id);
            db.Exames.Remove(exame);
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