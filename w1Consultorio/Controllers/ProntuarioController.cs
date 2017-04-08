using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using w1Consultorio.Models;
using w1Consultorio.Models.ViewModel;
using AutoMapper;

namespace w1Consultorio.Controllers
{
    public class ProntuarioController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Prontuario/

        public ActionResult Index()
        {
            var prontuario = db.Prontuario.Include(p => p.Empresa).Include(p => p.Departamento).Include(p => p.Cargo).Include(p => p.Funcionario).Include(p => p.Profissional).Include(p => p.TipoExame);
            
           return View(prontuario.ToList());
        }

        //
        // GET: /Prontuario/Details/5

        public ActionResult Details(int id = 0)
        {
            Prontuario prontuario = db.Prontuario.Find(id);
            if (prontuario == null)
            {
                return HttpNotFound();
            }
            return View(prontuario);
        }

        //
        // GET: /Prontuario/Create

        public ActionResult Create(int codAtendimento)
        {
            ViewBag.codProfissional = new SelectList(db.Profissionais, "CodProfissional", "Nome");
            ViewBag.codResultadoClinico = new SelectList(new ResultadoClinico().ResultadosClinicos, "Key", "Value");
            ViewBag.codResultadoProntuario = new SelectList(new ResultadoProntuario().ResultadosProntuario, "Key", "Value");

            Atendimento atendimento = db.Atendimento.Where(x => x.codAtendimento == codAtendimento).FirstOrDefault();
            if (atendimento == null)
            {
                TempData["mensagemErro"] = "Erro ao obter dados do agendamento";
                return View();
            }

            ProntuarioViewModel prontuario = new ProntuarioViewModel();

            prontuario.CodAtendimento = codAtendimento;
            prontuario.CodEmpresa = atendimento.codEmpresa;
            prontuario.Empresa = atendimento.Empresa;
            prontuario.CodFuncionario = atendimento.codFuncionario;
            prontuario.Funcionario = atendimento.Funcionario;
            prontuario.CodTipoExame = atendimento.codTipoExame;
            prontuario.TipoExame = atendimento.TipoExame;
            prontuario.Cargo = db.Cargos.Where(x => x.CodCargo == atendimento.Funcionario.CodCargo).FirstOrDefault();
            prontuario.CodCargo = prontuario.Cargo.CodCargo;
            prontuario.Departamento = db.Departamentos.Where(x => x.CodDepartamento == atendimento.Funcionario.Cargo.codDepartamento).FirstOrDefault();
            prontuario.CodDepartamento = prontuario.Departamento.CodDepartamento;
            prontuario.PerguntasRespostas = CarregarPerguntasRespostas(0);
            return View(prontuario);
        }

        //
        // POST: /Prontuario/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProntuarioViewModel prontuario)
        {
            if (ModelState.IsValid)
            {
                prontuario.dataExame = DateTime.Now;
                db.Prontuario.Add(prontuario);

                try
                {
                    db.SaveChanges();
                    //salvou o pronturio, agora salva as respostas
                    SalvarRespostas(prontuario);

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    TempData["mensagemErro"] = "Erro ao salvar prontuário";
                }
            }

            ViewBag.codProfissional = new SelectList(db.Profissionais, "CodProfissional", "Nome");
            ViewBag.codResultadoClinico = new SelectList(new ResultadoClinico().ResultadosClinicos, "Key", "Value");
            ViewBag.codResultadoProntuario = new SelectList(new ResultadoProntuario().ResultadosProntuario, "Key", "Value");
            return View(prontuario);
        }

        //
        // GET: /Prontuario/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Prontuario prontuario = db.Prontuario.Find(id);
            Mapper.CreateMap<Prontuario, ProntuarioViewModel>();
            var prontuarioView = Mapper.Map<Prontuario, ProntuarioViewModel>(prontuario);

                        if (prontuario == null)
            {
                return HttpNotFound();
            }

            ViewBag.codProfissional = new SelectList(db.Profissionais, "CodProfissional", "Nome");
            ViewBag.codResultadoClinico = new SelectList(new ResultadoClinico().ResultadosClinicos, "Key", "Value");
            ViewBag.codResultadoProntuario = new SelectList(new ResultadoProntuario().ResultadosProntuario, "Key", "Value");

            prontuarioView.PerguntasRespostas = CarregarPerguntasRespostas(0);

            return View(prontuarioView);
        }

        //
        // POST: /Prontuario/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                prontuario.dataExame = DateTime.Now;

                db.Entry(prontuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodEmpresa = new SelectList(db.Empresas, "CodEmpresa", "Nome", prontuario.CodEmpresa);
            ViewBag.CodDepartamento = new SelectList(db.Departamentos, "CodDepartamento", "Descricao", prontuario.CodDepartamento);
            ViewBag.CodCargo = new SelectList(db.Cargos, "CodCargo", "Descricao", prontuario.CodCargo);
            ViewBag.CodFuncionario = new SelectList(db.Funcionarios, "CodFuncionario", "Nome", prontuario.CodFuncionario);
            ViewBag.codProfissional = new SelectList(db.Profissionais, "CodProfissional", "Nome", prontuario.codProfissional);
            return View(prontuario);
        }

        //
        // GET: /Prontuario/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Prontuario prontuario = db.Prontuario.Find(id);
            if (prontuario == null)
            {
                return HttpNotFound();
            }
            return View(prontuario);
        }

        //
        // POST: /Prontuario/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prontuario prontuario = db.Prontuario.Find(id);
            db.Prontuario.Remove(prontuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private IList<GrupoViewModel> CarregarPerguntasRespostas(int codProntuario)
        {
            IList<GrupoViewModel> grupoCollection;
            grupoCollection = new List<GrupoViewModel>();
            GrupoViewModel grupo;

            PerguntaRespostaViewModel pergunta;

            if (codProntuario == 0)
            {
                foreach (PerguntaGrupo grupoItem in db.PerguntaGrupos.Where(x => x.Ativo == "1").OrderBy(g=>g.Sequencia))
                {
                    grupo = new GrupoViewModel();
                    grupo.CodGrupo = grupoItem.CodPerguntaGrupo;
                    grupo.GrupoDescricao = grupoItem.Descricao;
                    grupo.GrupoSequencia = grupoItem.Sequencia;
                    grupo.Perguntas = new List<PerguntaRespostaViewModel>();

                    foreach (Pergunta perguntaItem in db.Pergunta.Where(x => x.Ativo == "1" && x.CodPerguntaGrupo== grupo.CodGrupo))
                    {
                        pergunta = new PerguntaRespostaViewModel();
                        pergunta.CodProntuario = 0;
                        pergunta.CodResposta = 0;
                        pergunta.CodPergunta = perguntaItem.CodPergunta;
                        pergunta.PerguntaDescricao = perguntaItem.Descricao;
                        pergunta.PerguntaSequencia = perguntaItem.Sequencia;
                        pergunta.PerguntaTipoResposta = perguntaItem.CodTipoResposta;

                        grupo.Perguntas.Add(pergunta);
                    }
                    grupoCollection.Add(grupo);
                }
            }

            return grupoCollection;
        }

        private void SalvarRespostas(ProntuarioViewModel prontuario)
        {
            Resposta resposta;
            Pergunta pergunta;

            foreach (GrupoViewModel grupoItem in prontuario.PerguntasRespostas)
            {
                foreach (PerguntaRespostaViewModel respostaItem in grupoItem.Perguntas)
                {
                    pergunta = db.Pergunta.Where(x => x.CodPergunta == respostaItem.CodPergunta).FirstOrDefault();

                    resposta = new Resposta();
                    resposta.CodPergunta = respostaItem.CodPergunta;
                    resposta.CodProntuario = prontuario.CodProntuario;
                    resposta.CodResposta = 0;
                    resposta.CodTipoPergunta = pergunta.CodTipoResposta;
                    resposta.PerguntaDescricao = pergunta.Descricao;
                    resposta.RespostaObservacao = respostaItem.RespostaObservacao;
                    resposta.RespostaValor = respostaItem.RespostaValor;
                    resposta.CodGrupo = pergunta.PerguntaGrupo.CodPerguntaGrupo;
                    resposta.GrupoSequencia = pergunta.PerguntaGrupo.Sequencia;
                    db.Resposta.Add(resposta);
                }
            }
            db.SaveChanges();
        }
    }
}