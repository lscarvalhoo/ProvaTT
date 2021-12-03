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

        public void RegistrarUsuario(Usuario usuario)
        {
            RegistroAcesso registrar = new RegistroAcesso();
            registrar.UsuarioId = usuario.Id;
            registrar.DataAcesso = DateTime.Now;
            registrar.Usuario = usuario;

            db.RegistroAcesso.Add(registrar);
            db.SaveChanges();
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
                                                                            DateTime.Now.AddMinutes(10),
                                                                            false,
                                                                            usuario.Id.ToString());
                string encTicket = FormsAuthentication.Encrypt(ticket);

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.Expires = ticket.Expiration;
                Response.Cookies.Add(cookie);

                RegistrarUsuario(usuario);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }
    }
}