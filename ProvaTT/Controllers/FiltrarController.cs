using ProvaTT.DAO;
using ProvaTT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProvaTT.Controllers
{
    public class FiltrarController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Buscar(int? cursoId)
        {
            if (cursoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Inscricao> inscricao = db.Inscricao.Where(i => i.CursoId == cursoId).ToList();
            if (inscricao == null)
            {
                return HttpNotFound();
            }
            return View(inscricao);
        }

    }
}