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
    public class PerfilController : Controller
    {
        private Consultorio db = new Consultorio();


        public ActionResult SetSistema(string sistemaid, string moduloid)
        {
            return RedirectToAction("Index", new { sistemaid = sistemaid, moduloid = moduloid });
        }

        public ActionResult Index(string sistemaid, string moduloid)
        {
            int moduloidAux = 0;
            int sistemaidAux = Convert.ToInt32(sistemaid);

            if (moduloid != null)
            {
                moduloidAux = Convert.ToInt32(moduloid);
            }

            var perfil = db.Perfil.Include(p => p.Modulo).Where(m => m.CodModulo == moduloidAux);
            var sistemaCollection = db.Sistema;

            //Na primeira vez, traz os dados do primeiro sistema da lista
            if (sistemaid == null)
            {
                sistemaidAux = sistemaCollection.FirstOrDefault().CodSistema;
            }

            ViewBag.Sistemas = new SelectList(sistemaCollection, "CodSistema", "Nome", sistemaid);
            ViewBag.Modulos = new SelectList(db.Modulo.Where(s => s.CodSistema == sistemaidAux), "CodModulo", "Nome", moduloid);
            return View(perfil.ToList());
        }

        //
        // GET: /Perfil/Details/5

        public ActionResult Details(int id = 0)
        {
            Perfil perfil = db.Perfil.Include(p=>p.Transacao).Where(x=>x.CodPerfil == id).FirstOrDefault();
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        //
        // GET: /Perfil/Create

        public ActionResult Create(string sistemaid, string moduloid)
        {
            CarregarAtivo();
            PopularTransacaoData();

            Perfil perfil = new Perfil();
            perfil.CodModulo = Convert.ToInt32(moduloid);
            return View(perfil);
        }

        //
        // POST: /Perfil/Create

        [HttpPost]
        public ActionResult Create(Perfil perfil, string sistemaid, string moduloid, string[] transacaoAssociado)
        {
            try
            {
                Transacao transacao;
                if (transacaoAssociado != null)
                {
                    if (transacaoAssociado.Length > 0 && perfil.Transacao == null) perfil.Transacao = new List<Transacao>();
                    foreach (string item in transacaoAssociado)
                    {
                        transacao = new Transacao();
                        int codTransacao = Convert.ToInt32(item);
                        transacao = db.Transacao.Where(x => x.CodTransacao == codTransacao).FirstOrDefault();
                        perfil.Transacao.Add(transacao);
                    }
                }

                if (ModelState.IsValid)
                {
                    db.Perfil.Add(perfil);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { sistemaid = sistemaid, moduloid = moduloid });
                }
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao salvar informações. Contate o resposával pelo sistema");
            }

            CarregarAtivo();
            return View(perfil);
        }

        //
        // GET: /Perfil/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Perfil perfil = db.Perfil.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            CarregarAtivo();
            PopularTransacaoData(perfil);
            return View(perfil);
        }

        //
        // POST: /Perfil/Edit/5

        [HttpPost]
        public ActionResult Edit(Perfil perfil, string sistemaid, string moduloid, string[] transacaoAssociado)
        {
            if (ModelState.IsValid)
            {

                var perfilAtualizar = db.Perfil
                    .Include(i => i.Transacao)
                    .Single();

                if (transacaoAssociado == null)
                {
                    perfilAtualizar.Transacao = new List<Transacao>();
                }
                else
                {
                    var selectedTransacaoHash = new HashSet<string>(transacaoAssociado);
                    var perfilTransacao = new HashSet<int>(perfilAtualizar.Transacao.Select(c => c.CodTransacao));

                    foreach (Transacao transacaoItem in db.Transacao)
                    {
                        if (selectedTransacaoHash.Contains(transacaoItem.CodTransacao.ToString()))
                        {
                            if (!perfilAtualizar.Transacao.Contains(transacaoItem))
                            {
                                perfilAtualizar.Transacao.Add(transacaoItem);
                            }
                        }
                        else
                        {
                            if (perfilAtualizar.Transacao.Contains(transacaoItem))
                            {
                                perfilAtualizar.Transacao.Remove(transacaoItem);
                            }
                        }
                    }
                }

                db.Entry(perfilAtualizar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { sistemaid = sistemaid, moduloid = moduloid });
            }
            CarregarAtivo();
            return View(perfil);
        }

        //
        // GET: /Perfil/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Perfil perfil = db.Perfil.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        //
        // POST: /Perfil/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string sistemaid, string moduloid)
        {
            Perfil perfil = db.Perfil.Find(id);
            db.Perfil.Remove(perfil);
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

        private void PopularTransacaoData()
        {
            var transacaoCollection = db.Transacao;
            var viewModel = new List<AssignedTransacaoData>();

            foreach (var item in transacaoCollection)
            {
                viewModel.Add(new AssignedTransacaoData
                {
                    CodTransacao = item.CodTransacao,
                    Descricao = item.Descricao,
                    Assigned = false
                });
            }
            ViewBag.Transacoes = viewModel;
        }

        private void PopularTransacaoData(Perfil perfil)
        {
            var transacaoCollection = db.Transacao;
            var viewModel = new List<AssignedTransacaoData>();

            var cargoExame = new HashSet<int>(perfil.Transacao.Select(c => c.CodTransacao));

            foreach (var item in transacaoCollection)
            {
                viewModel.Add(new AssignedTransacaoData
                {
                    CodTransacao = item.CodTransacao,
                    Descricao = item.Descricao,
                    Assigned = cargoExame.Contains(item.CodTransacao)
                });
            }
            ViewBag.Transacoes = viewModel;
        }

    }
}