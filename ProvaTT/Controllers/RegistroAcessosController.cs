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
    public class RegistroAcessosController : Controller
    {
        private Contexto db = new Contexto();
         
        public ActionResult Index()
        {
            var registroAcesso = db.RegistroAcesso.Include(r => r.Usuario);
            return View(registroAcesso.ToList());
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
