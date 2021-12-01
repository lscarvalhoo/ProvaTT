using ProvaTT.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProvaTT.Controllers
{
    public class LogoffController : Controller
    {
        private Contexto db = new Contexto();
        // GET: Logoff
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            Session.Clear();  // This may not be needed -- but can't hurt
            Session.Abandon();

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rSessionCookie); 

            return RedirectToAction("", "Login");
        }

    }
}