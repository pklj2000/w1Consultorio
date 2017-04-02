using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w1Consultorio.Models;
using System.Globalization;
using w1Consultorio.Models.ViewModel;
using Services;

namespace w1Consultorio.Controllers
{
    public class UsuarioController : Controller
    {
        private Consultorio db = new Consultorio();

        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(string id = null)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            CarregarAtivo();
            var usuario = new Usuario();
            usuario.DataUltAcesso = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return View(usuario);
        }

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            CarregarAtivo();
            return View(usuario);
        }

        //
        // GET: /Usuario/Edit/5

        public ActionResult Edit(string id = null)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            CarregarAtivo();
            return View(usuario);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            CarregarAtivo();
            return View(usuario);
        }

        //
        // GET: /Usuario/Delete/5

        public ActionResult Delete(string id = null)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UsuarioPerfil(string id, string[] perfilAssociado, string comando, int? sistemaid, int? moduloid)
        {
            int moduloCodigo = moduloid ?? 0;
            Usuario usuario = db.Usuario.Include(x=>x.Perfil).Where(p=>p.CodUsuario == id).FirstOrDefault();
            

            if (usuario == null)
            {
                return HttpNotFound();
            }

            if (comando == "Salvar")
            {
                if (perfilAssociado == null)
                {
                    usuario.Perfil = new List<Perfil>();
                }
                else
                {
                    var perfilAssociadoHash = new HashSet<string>(perfilAssociado);
                    var usuarioPerfil = new HashSet<int>(usuario.Perfil.Select(c => c.CodPerfil));

                    foreach (var perfilItem in db.Perfil)
                    {
                        if (perfilAssociadoHash.Contains(perfilItem.CodPerfil.ToString()))
                        {
                            if (!usuario.Perfil.Contains(perfilItem))
                            {
                                usuario.Perfil.Add(perfilItem);
                            }
                        }
                        else
                        {
                            if (usuario.Perfil.Contains(perfilItem))
                            {
                                usuario.Perfil.Remove(perfilItem);
                            }
                        }
                    }
                }

                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var sistemasCollection = db.Sistema;
            ViewBag.Sistemas = new SelectList(sistemasCollection, "CodSistema", "Nome", sistemaid);
            if (sistemaid == null)
            {
                sistemaid = sistemasCollection.FirstOrDefault().CodSistema;
            }
            
            var modulosCollection = db.Modulo.Where(x => x.CodSistema == sistemaid);
            ViewBag.Modulos = new SelectList(modulosCollection, "CodModulo", "Nome", moduloCodigo);
            if (moduloCodigo == 0)
            {
                moduloCodigo = modulosCollection.FirstOrDefault().CodModulo;
            }

            CarregarAtivo();

            PopularPerfilData(usuario, moduloCodigo);
            
            return View(usuario);
        }

        public ActionResult SalvarUsuarioPerfil(string id, int sistemaid, int moduloid, string[] perfilAssociado)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Senha(string id = null)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Senha")]
        public ActionResult SenhaConfirm(string id, string ExecMail)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            string senhaNova = System.Web.Security.Membership.GeneratePassword(8, 4);
            Seguranca seguranca = new Seguranca();
            string senhaNovaEncriptada = seguranca.Encrypt(senhaNova);
            db.Entry(usuario).State = EntityState.Modified;
            usuario.Senha = senhaNovaEncriptada;
            db.SaveChanges();

            if (ExecMail != null)
            {
                //mandar email
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ExibeSenha = true;
                ViewBag.Senha = senhaNova;
                return View(usuario);
            }
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

        private void PopularPerfilData(Usuario usuario, int moduloid)
        {
            var perfilCollection = db.Perfil.Where(x=>x.Modulo.CodModulo == moduloid).ToList();
            var viewModel = new List<AssignedPerfilData>();

            var perfisUsuarioAux = db.Usuario.Include(d=>d.Perfil).Where(x => x.CodUsuario == usuario.CodUsuario).FirstOrDefault();
            var perfisUsuario = perfisUsuarioAux.Perfil.ToList();

            foreach (var item in perfilCollection)
            {
                viewModel.Add(new AssignedPerfilData
                {
                    CodPerfil = item.CodPerfil,
                    Descricao = item.Descricao,
                    Assigned = perfisUsuario.Contains(item)
                });
            }
            ViewBag.Perfis = viewModel;
        }

    }
}