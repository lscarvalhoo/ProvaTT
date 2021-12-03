using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using ProvaTT.DAO; 
using ProvaTT.Models;

namespace ProvaTT.Controllers
{
    [Authorize]
    public class InscricaoController : Controller
    {
        private Contexto db = new Contexto(); 


        [Authorize]
        public ActionResult Index()
        {
            var inscricao = db.Inscricao.Include(i => i.Curso).Include(i => i.Usuario);
            return View(inscricao.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscricao inscricao = db.Inscricao.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            return View(inscricao);
        } 
        
        [Authorize]
        public ActionResult Informacoes(int? cursoId)
        {
            if (cursoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Informacoes informacoes = new Informacoes();

            informacoes.ValorCurso = db.Curso.Where(c => c.Id == cursoId).Select(v => v.Valor).FirstOrDefault();
            informacoes.QuantidadeVagas = db.Curso.Where(c => c.Id == cursoId).Select(v => v.QuantidadeVagas).FirstOrDefault();
            informacoes.QuantidadeInscritos = db.Inscricao.Where(i => i.CursoId == cursoId).ToList().Count();

            IList<Informacoes> informacoesRetorno = (IList<Informacoes>)informacoes;

            return PartialView(informacoesRetorno);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Curso, "Id", "Id");
            ViewBag.Valor = new SelectList(db.Curso, "Valor", "Valor");
            ViewBag.UsuarioId = new SelectList(db.Usuario, "Id", "Login");
           
            return View();
        }

        [Authorize]
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Nome, CPF, Email, Telefone, CursoId, UsuarioId, DataInscricao")] Inscricao inscricao)
        {
            if (inscricao.Nome == null)
            {
                MessageBox.Show("Inscrição não preenchida!");

                ViewBag.CursoId = new SelectList(db.Curso, "Id", "Id", inscricao.CursoId);
                ViewBag.UsuarioId = new SelectList(db.Usuario, "Id", "Login", inscricao.UsuarioId);
                return View(inscricao);
            }

            if (verificaQuantidadeVagas(inscricao)) { 
                MessageBox.Show("Quantidade de vagas excedidas!"); 
                
                ViewBag.CursoId = new SelectList(db.Curso, "Id", "Id", inscricao.CursoId);
                ViewBag.UsuarioId = new SelectList(db.Usuario, "Id", "Login", inscricao.UsuarioId);
                return View(inscricao);
            }

            if (verificaCpfJaCadastrado(inscricao))
            {
                MessageBox.Show("CPF JÁ CADASTRADO!");

                ViewBag.CursoId = new SelectList(db.Curso, "Id", "Id", inscricao.CursoId);
                ViewBag.UsuarioId = new SelectList(db.Usuario, "Id", "Login", inscricao.UsuarioId);
                return View(inscricao);
            }

            string userName = User.Identity.Name;
            Usuario usuario = db.Usuario.Where(u => u.Nome == userName).FirstOrDefault();

            inscricao.UsuarioId = usuario.Id;
            inscricao.Usuario = usuario;

            if (ModelState.IsValid)
            {
                db.Inscricao.Add(inscricao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
             
            ViewBag.CursoId = new SelectList(db.Curso, "Id", "Id", inscricao.CursoId);
            ViewBag.UsuarioId = new SelectList(db.Usuario, "Id", "Login", inscricao.UsuarioId);
            return View(inscricao);
        }
            
        private bool verificaCpfJaCadastrado(Inscricao inscricao)
        {
            int verificaMatriculaComMesmoCPF = db.Inscricao.Where(i => i.CPF == inscricao.CPF).ToList().Count();

            if (verificaMatriculaComMesmoCPF > 0)
                return true;

            return false;
        }

        private bool verificaQuantidadeVagas(Inscricao inscricao)
        { 
            Curso curso = db.Curso.Where(c => c.Id == inscricao.CursoId).FirstOrDefault();
            int quantidadeInscricaoCurso = db.Inscricao.Where(i => i.CursoId == inscricao.CursoId).ToList().Count();

            if (quantidadeInscricaoCurso >= curso.QuantidadeVagas)
                return true;

            return false;
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscricao inscricao = db.Inscricao.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Curso, "Id", "Id", inscricao.CursoId);
            ViewBag.UsuarioId = new SelectList(db.Usuario, "Id", "Login", inscricao.UsuarioId);
            return View(inscricao);
        }
 
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,CPF,Email,Telefone,CursoId,UsuarioId,DataInscricao")] Inscricao inscricao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscricao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Curso, "Id", "Id", inscricao.CursoId);
            ViewBag.UsuarioId = new SelectList(db.Usuario, "Id", "Login", inscricao.UsuarioId);
            return View(inscricao);
        }
 
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscricao inscricao = db.Inscricao.Find(id);
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            return View(inscricao);
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscricao inscricao = db.Inscricao.Find(id);
            db.Inscricao.Remove(inscricao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
