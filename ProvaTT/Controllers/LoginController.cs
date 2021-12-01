using ProvaTT.DAO;
using ProvaTT.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProvaTT.Controllers
{
    public class LoginController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Acessar(string login, string senha)
        {
            Usuario ps = new Usuario();

            Usuario usuario = db.Usuario.Where(u => u.Login.Equals(login)
                                                && u.Senha.Equals(senha)).FirstOrDefault();

            if (usuario != null)
            {

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                                                            usuario.Nome,
                                                                            DateTime.Now,
                                                                            DateTime.Now.AddMinutes(5),
                                                                            false,
                                                                            usuario.Id.ToString());
                string encTicket = FormsAuthentication.Encrypt(ticket);

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = ticket.Expiration;
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }
    }
}