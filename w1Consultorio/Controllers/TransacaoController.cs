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
    public class TransacaoController : Controller
    {
        private Consultorio db = new Consultorio();

        public ActionResult SetSistema(string sistemaid, string moduloid)
        {
            return RedirectToAction("Index", new { sistemaid = sistemaid, moduloid = moduloid });
        }

        //
        // GET: /Transacao/

        public ActionResult Index(string sistemaid, string moduloid)
        {
            int moduloidAux = 0;
            int sistemaidAux = Convert.ToInt32(sistemaid);

            if (moduloid != null)
            {
                moduloidAux = Convert.ToInt32(moduloid);
            }

            var transacao = db.Transacao.Include(m => m.Modulo).Where(m => m.CodModulo == moduloidAux);
            var sistemaCollection = db.Sistema;

            //Na primeira vez, traz os dados do primeiro sistema da lista
            if (sistemaid == null)
            {
                sistemaidAux = sistemaCollection.FirstOrDefault().CodSistema;
            }

            ViewBag.Sistemas = new SelectList(sistemaCollection, "CodSistema", "Nome", sistemaid);
            ViewBag.Modulos = new SelectList(db.Modulo.Where(s => s.CodSistema == sistemaidAux), "CodModulo", "Nome", moduloid);
            return View(transacao.ToList());
        }

        //
        // GET: /Transacao/Details/5

        public ActionResult Details(int id = 0)
        {
            Transacao transacao = db.Transacao.Find(id);
            if (transacao == null)
            {
                return HttpNotFound();
            }
            return View(transacao);
        }

        //
        // GET: /Transacao/Create

        public ActionResult Create(string sistemaid, string moduloid)
        {
            CarregarAtivo();

            Transacao transacao = new Transacao();
            transacao.CodModulo = Convert.ToInt32(moduloid);
            return View(transacao);
        }

        //
        // POST: /Transacao/Create

        [HttpPost]
        public ActionResult Create(Transacao transacao, string sistemaid, string moduloid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Transacao.Add(transacao);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { sistemaid = sistemaid, moduloid = moduloid });
                }
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao salvar informações. Contate o resposával pelo sistema");
            }

            CarregarAtivo();
            return View(transacao);
        }

        //
        // GET: /Transacao/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Transacao transacao = db.Transacao.Find(id);
            if (transacao == null)
            {
                return HttpNotFound();
            }
            CarregarAtivo();
            return View(transacao);
        }

        //
        // POST: /Transacao/Edit/5

        [HttpPost]
        public ActionResult Edit(Transacao transacao, string sistemaid, string moduloid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { sistemaid = sistemaid, moduloid = moduloid });
            }
            CarregarAtivo();
            return View(transacao);
        }

        //
        // GET: /Transacao/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transacao transacao = db.Transacao.Find(id);
            if (transacao == null)
            {
                return HttpNotFound();
            }
            return View(transacao);
        }

        //
        // POST: /Transacao/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string sistemaid, string moduloid)
        {
            Transacao transacao = db.Transacao.Find(id);
            db.Transacao.Remove(transacao);
            db.SaveChanges();
            return RedirectToAction("Index", new { sistemaid = sistemaid, moduloid = moduloid });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private void CarregarAtivo()
        {
            var ativo = new SelectList(new[]
                                          {
                                              new {ID="0",Descricao="Não"},
                                              new{ID="1",Descricao="Sim"},
                                          },
                            "ID", "Descricao", 1);
            ViewBag.AtivoLista = ativo;
        }
    }
}