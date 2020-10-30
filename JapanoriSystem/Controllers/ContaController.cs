using JapanoriSystem.DAL;
using JapanoriSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JapanoriSystem.Controllers
{
    public class ContaController : Controller
    {
        bdJapanoriContext db = new bdJapanoriContext();

        public ActionResult Login()
        {

            return View();
        }



        [HttpPost]
        public ActionResult Login(Funcionario login)
        {
            var r = getuser(login.EmailCorp);
            if (r == null)
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome do usuário e a senha!";
                return View();
            }
            else
            {
                if (r.EmailCorp == login.EmailCorp && r.Senha == login.Senha)
                {
                    Session["emailUsuarioLogado"] = r.EmailCorp.ToString();
                    Session["usuarioLogado"] = r.Nome.ToString();
                    Session["senhaLogado"] = r.Senha.ToString();
                    Session["sobrenomeLogado"] = r.Sobrenome.ToString();
                    Session["permUsuarioLogado"] = r.Perm.ToString();
                    Session["nomeCompleto"] = r.NomeCompleto.ToString();
                    FormsAuthentication.SetAuthCookie(r.EmailCorp, false);
                    return RedirectToAction("Inicio", "Home");
                }
                else
                {
                    ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome do usuário e a senha!";
                    return View();
                }
            }



        }

        private Funcionario getuser(string getemail)
        {
            var res = db.tbFuncionario.Where(x => x.EmailCorp == getemail).ToList();

            if (res != null && res.Count > 0 && res[0].Status != "Off")
            {

                return (Funcionario)res[0];

            }
            else
            {
                return null;
            }
        }

        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;

            return RedirectToAction("Login", "Conta");
        }

        public ActionResult semAcesso()
        {
            ViewBag.Message = "Você não tem acesso a essa página";
            return View();
        }
    }
}