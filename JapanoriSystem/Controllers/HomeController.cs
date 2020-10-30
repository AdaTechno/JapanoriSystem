using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JapanoriSystem.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Inicio()
        {
            if ((Session["emailUsuarioLogado"] == null) || (Session["senhaLogado"] == null) || (Session["usuarioLogado"] == null))
            {
                return RedirectToAction("semAcesso", "Conta");
            }
            else
            {
                ViewBag.nomeUsuarioLog = Session["usuarioLogado"];
                ViewBag.sobrenomeLog = Session["sobrenomeLogado"];
                ViewBag.nomeCompletoLog = Session["nomeCompleto"];
                ViewBag.emailUsuarioLog = Session["emailUsuarioLogado"];
                ViewBag.permUsuarioLog = Session["permUsuarioLogado"];
                return View();
            }
        }

       
    }
}