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
    public class InscricaoController : Controller
    {
        private Contexto db = new Contexto();
        [Authorize]


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
        public ActionResult Create([Bind(Include = "Id,Nome,CPF,Email,Telefone,CursoId,UsuarioId,DataInscricao")] Inscricao inscricao)
        {
            Curso curso = db.Curso.Where(c => c.Id == inscricao.CursoId).FirstOrDefault();
            int quantidadeInscricaoCurso = db.Inscricao.Where(i => i.CursoId == inscricao.CursoId).ToList().Count();

            if (quantidadeInscricaoCurso >= curso.QuantidadeVagas)
                MessageBox.Show("Quantidade de vagas excedidas!");

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

        // POST: Inscricaos/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Inscricaos/Delete/5 
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
