using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProvaTT.DAO;
using ProvaTT.Models;

namespace ProvaTT.Controllers
{
    public class InscricaoController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Inscricaos
        [Authorize]
        public ActionResult Index()
        {
            var inscricao = db.Inscricao.Include(i => i.Curso).Include(i => i.Usuario);
            return View(inscricao.ToList());
        }

        // GET: Inscricaos/Details/5
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

        // GET: Inscricaos/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Curso, "Id", "Id");
            ViewBag.UsuarioId = new SelectList(db.Usuario, "Id", "Login");
            return View();
        }

        // POST: Inscricaos/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,CPF,Email,Telefone,CursoId,UsuarioId,DataInscricao")] Inscricao inscricao)
        {
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

        // GET: Inscricaos/Edit/5
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

        // POST: Inscricaos/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
