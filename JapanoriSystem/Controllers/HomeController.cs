using JapanoriSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JapanoriSystem.Controllers
{
    public class HomeController : Controller
    {
        bdJapanoriContext db = new bdJapanoriContext();
        public ActionResult Inicio()
        {
            

            if ((Session["emailUsuarioLogado"] == null) || (Session["senhaLogado"] == null) || (Session["usuarioLogado"] == null))
            {
                return RedirectToAction("semAcesso", "Conta");
            }
            else
            {
                ViewBag.idUsuarioLog = Session["idUsuario"];
                ViewBag.nomeUsuarioLog = Session["usuarioLogado"];
                ViewBag.sobrenomeLog = Session["sobrenomeLogado"];
                ViewBag.cargoUsuarioLog = Session["cargoUsuario"];
                ViewBag.nomeCompletoLog = Session["nomeCompleto"];
                ViewBag.emailUsuarioLog = Session["emailUsuarioLogado"];
                ViewBag.permUsuarioLog = Session["permUsuarioLogado"];
                

                var Comandas = db.tbComanda
                                    //.Where(i => i.cStatus == "Ativo")
                                    .ToList()
                                    .Count();
                ViewBag.Comandas = Comandas;
                

                var Funcionarios = db.tbFuncionario
                                    //.Where(i => i.cStatus == "Ativo")
                                    .ToList()
                                    .Count();
                ViewBag.Funcionarios = Funcionarios;


                var Produtos = db.tbProduto
                                    //.Where(i => i.cStatus == "Ativo")
                                    .ToList()
                                    .Count();
                ViewBag.Produtos = Produtos;


                var Vendas = db.tbVendas
                                    //.Where(i => i.cStatus == "Ativo")
                                    .ToList()
                                    .Count();
                ViewBag.Vendas = Vendas;

                return View();
            }

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