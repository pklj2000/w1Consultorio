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
    public class PerguntaController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Pergunta/

        public ActionResult Index(int? grupoid)
        {
            string grupo = "";
            if (grupoid != null)
            {
                grupo = db.PerguntaGrupos.Where(x => x.CodPerguntaGrupo == grupoid).FirstOrDefault().Descricao;
            }
            ViewBag.grupo = grupo;

            var pergunta = db.Pergunta.Include(p => p.PerguntaGrupo).Where(x => x.CodPerguntaGrupo == grupoid);
            return View(pergunta.ToList());
        }

        //
        // GET: /Pergunta/Details/5

        public ActionResult Details(int id = 0)
        {
            Pergunta pergunta = db.Pergunta.Find(id);
            if (pergunta == null)
            {
                return HttpNotFound();
            }
            return View(pergunta);
        }

        //
        // GET: /Pergunta/Create

        public ActionResult Create()
        {
            ViewBag.CodPerguntaGrupo = new SelectList(db.PerguntaGrupos, "CodPerguntaGrupo", "Descricao");
            CarregarTipoResposta();
            CarregarRespostaObrigatoria();
            CarregarAtivo();
            return View();
        }

        //
        // POST: /Pergunta/Create

        [HttpPost]
        public ActionResult Create(Pergunta pergunta, int grupoid)
        {
            pergunta.CodPerguntaGrupo = grupoid;
            if (ModelState.IsValid)
            {
                db.Pergunta.Add(pergunta);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = grupoid });
            }

            ViewBag.CodPerguntaGrupo = new SelectList(db.PerguntaGrupos, "CodPerguntaGrupo", "Descricao", pergunta.CodPerguntaGrupo);
            return View(pergunta);
        }

        //
        // GET: /Pergunta/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Pergunta pergunta = db.Pergunta.Include(p => p.PerguntaGrupo).Where(x=>x.CodPergunta == id).FirstOrDefault();
            if (pergunta == null)
            {
                return HttpNotFound();
            }
            CarregarTipoResposta();
            CarregarRespostaObrigatoria();
            CarregarAtivo();
            //ViewBag.CodPerguntaGrupo = new SelectList(db.PerguntaGrupos, "CodPerguntaGrupo", "Descricao", pergunta.CodPerguntaGrupo);
            return View(pergunta);
        }

        //
        // POST: /Pergunta/Edit/5

        [HttpPost]
        public ActionResult Edit(Pergunta pergunta, int grupoid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pergunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = grupoid });
            }
            ViewBag.CodPerguntaGrupo = new SelectList(db.PerguntaGrupos, "CodPerguntaGrupo", "Descricao", pergunta.CodPerguntaGrupo);
            return View(pergunta);
        }

        //
        // GET: /Pergunta/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Pergunta pergunta = db.Pergunta.Find(id);
            if (pergunta == null)
            {
                return HttpNotFound();
            }
            return View(pergunta);
        }

        //
        // POST: /Pergunta/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int grupoid, int id)
        {
            Pergunta pergunta = db.Pergunta.Find(id);
            db.Pergunta.Remove(pergunta);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = grupoid });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private void CarregarTipoResposta()
        {
            TipoPergunta tipoPergunta = new TipoPergunta();
            var tipoRespostas = new SelectList(tipoPergunta.TiposPergunta, "key", "value", 1);

            ViewBag.TipoResposta = tipoRespostas;
        }

        private void CarregarRespostaObrigatoria()
        {
            var respostaObrigatoria = new SelectList(new[]
                                          {
                                              new {ID="0",Descricao="Não"},
                                              new{ID="1",Descricao="Sim"},
                                          },
                            "ID", "Descricao", 1);
            ViewBag.RespostaObrigatoriaLista = respostaObrigatoria;
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