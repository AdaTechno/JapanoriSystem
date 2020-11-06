using JapanoriSystem.DAL;
using JapanoriSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace JapanoriSystem.Controllers
{
    public class CaixaController : Controller
    {
        bdJapanoriContext db = new bdJapanoriContext();
        // GET: Comanda
        public ActionResult CaixaIDComanda()
        {

            return View();
        }

        public ActionResult Caixa(ProdutoComanda pc, int? id)
        {
            int comandaID = pc.ComandaID;
            ViewBag.ID = comandaID;
            ViewBag.ID2 = id;
            if (id != null)
            {
                var lista2 = db.tbProdutoComanda.Where(i => i.ComandaID == id).Where(i => i.Status != "Fechado").ToList();
                return View(lista2);
            }
            else if (comandaID != 0)
            {
                var list = db.tbProdutoComanda.Where(i => i.ComandaID == comandaID).Where(i => i.Status != "Fechado").ToList();
                return View(list);
            }
            else
            {
                return RedirectToAction("CaixaIDComanda");
            }
        }


        public ActionResult Edit(int id)
        {

            ProdutoComanda pc = db.tbProdutoComanda.Find(id);
            int comandaID = pc.ComandaID;
            ViewBag.ID = comandaID;
            //ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "Situacao", produtoComanda.ComandaID);
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome", pc.ProdutoID);

            return View(pc);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(int? ProdutoComandaID, int? ProdutoID, ProdutoComanda pc)
        {
            if ((ProdutoComandaID == null) || (ProdutoID == null))
            {
                ViewBag.ConfirmErro = "1";
                ViewBag.MsgErro = "Não foi possível salvar as informações ";
            }
            if ((ProdutoComandaID != null) && (ProdutoID != null))
            {
                ViewBag.UpdateProduto = db.Database
                    .ExecuteSqlCommand("UPDATE tbProdutoComanda " +
                                        "SET ProdutoID = {0} " +
                                        "WHERE ProdutoComandaID = {1}", ProdutoID, ProdutoComandaID);
                return RedirectToAction("Caixa", new { id = pc.ComandaID });
            }
            //ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "ID", pc.ComandaID);
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome", pc.ProdutoID);
            int comandaID = pc.ComandaID;
            ViewBag.ID = comandaID;

            return View(pc);
        }

        public ActionResult Excluir(int id)
        {
            ProdutoComanda pc = db.tbProdutoComanda.Find(id);

            int comandaID = pc.ComandaID;
            ViewBag.ID = comandaID;

            return View(pc);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmed(int? ProdutoComandaID, ProdutoComanda pc)
        {
            if (ProdutoComandaID == null)
            {
                ViewBag.ConfirmErro = "1";
                ViewBag.MsgErro = "Não foi possível excluir o produto";
            }
            if (ProdutoComandaID != null)
            {
                ViewBag.DeleteProduto = db.Database
                    .ExecuteSqlCommand("DELETE FROM tbProdutoComanda " +
                                        "WHERE ProdutoComandaID = {0}", ProdutoComandaID);
                return RedirectToAction("Caixa", new { id = pc.ComandaID });
            }

            int comandaID = pc.ComandaID;
            ViewBag.ID = comandaID;

            return View(pc);
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