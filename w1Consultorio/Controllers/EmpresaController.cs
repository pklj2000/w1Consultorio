using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data;

namespace w1Consultorio.Controllers
{
    public class EmpresaController : Controller
    {
        //Conexao.Conexao cnConexao = new Conexao.Conexao();
        Consultorio dbConsultorio = new Consultorio();

        /*public ActionResult Index()
        {
            return View(dbConsultorio.Empresas.ToList());
        }*/

        public ActionResult Index(string empresaNome)
        {
            if (string.IsNullOrEmpty(empresaNome))
                return View(dbConsultorio.Empresas.ToList());

            var empresas = from emp in dbConsultorio.Empresas
                           orderby emp.Nome
                           where emp.Nome.Contains(empresaNome)
                           select emp;

            return View(empresas);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Empresa empresa = dbConsultorio.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        public ActionResult Create()
        {
            CarregarAtivo();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbConsultorio.Empresas.Add(empresa);
                    dbConsultorio.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Erro ao salvar empresa");
            }
            return View(empresa);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Empresa empresa = dbConsultorio.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Models.Empresa empresa)
        {
            Models.Empresa empresaFind = dbConsultorio.Empresas.Find(empresa.CodEmpresa);
            if (empresa == null)
            {
                return HttpNotFound();
            }

            dbConsultorio.Empresas.Remove(empresaFind);
            dbConsultorio.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Empresa empresa = dbConsultorio.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dbConsultorio.Entry(empresa).State = EntityState.Modified;
                    dbConsultorio.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Erro ao salvar empresa");
            }
            return View(empresa);
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
