using JapanoriSystem.DAL;
using JapanoriSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace JapanoriSystem.Controllers
{
    public class CaixaController : Controller
    {
        bdJapanoriContext db = new bdJapanoriContext();

        public ActionResult CaixaIDComanda()
        {

            return View();
        }

        public ActionResult Caixa(ProdutoComanda pc, int? id)
        {
            int comandaID = pc.ComandaID;

            ViewBag.nomeUsuarioLog = Session["usuarioLogado"];
            // SQL QUERY SELECT
            if (id != 0 && id != null)
            {


                var lista2 = db.tbProdutoComanda.Where(i => i.ComandaID == id).Where(i => i.cStatus != "Fechado").ToList();
                ViewBag.ID2 = id;
                ViewBag.ID = comandaID;
                var total = db.tbComanda.Where(i => i.ID == id)
                                        .Select(i => i.ValorTotal)
                                        .Single();

                ViewBag.Total = total;
                return View(lista2);
            }
            // SQL QUERY SELECT
            else if (comandaID != 0)
            {

                var lista = db.tbProdutoComanda.Where(i => i.ComandaID == comandaID).Where(i => i.cStatus != "Fechado").ToList();
                ViewBag.ID = comandaID;
                ViewBag.ID2 = id;
                var total2 = db.tbComanda.Where(i => i.ID == comandaID)
                                        .Select(i => i.ValorTotal)
                                        .Single();
                ViewBag.Total2 = total2;
                return View(lista);
            }
            else
            {
                return RedirectToAction("CaixaIDComanda");
            }

        }

        public ActionResult addProduto(int? id)
        {

            ViewBag.ID = id;

            return View();
        }

        [HttpPost, ActionName("addProduto")]
        [ValidateAntiForgeryToken]
        public ActionResult addProdutoConfirmed(int? Quant, int? ProdutoID, int? id)
        {

            ViewBag.ID = id;

            if (ProdutoID == null || Quant == null)
            {
                ViewBag.ConfirmErro = "1";
                ViewBag.MsgErro = "Não foi possível salvar as informações";
            }
            if (ProdutoID != null && Quant != null)
            {

                /*string param = db.Database.ExecuteSqlCommand("SELECT * " +
                                                        "FROM tbProdutoComanda " +
                                                        "WHERE ComandaID = "+id+" and ProdutoID = "+ProdutoID).ToString();*/

                var param = db.tbProdutoComanda
                                    .Where(i => i.ComandaID == id)
                                    .Where(i => i.ProdutoID == ProdutoID)
                                    .Where(i => i.cStatus == "Ativo")
                                    .ToList()
                                    .Count();
                if (param > 0)
                {
                    ViewBag.UpProduto = db.Database
                                            .ExecuteSqlCommand("UPDATE tbProdutoComanda " +
                                                                "SET Quantidade = Quantidade+" + Quant + ", " +
                                                                    "ValorTotal = (Quantidade+" + Quant + ")*(SELECT Preco " +
                                                                                                        "FROM tbProduto " +
                                                                                                        "WHERE ProdutoID = " + ProdutoID + ") " +
                                                                "WHERE ComandaID = " + id + " and ProdutoID = " + ProdutoID);

                    db.Database.ExecuteSqlCommand("UPDATE tbComanda " +
                                                    "SET Situacao = 'Ativa', ValorTotal = (SELECT SUM(ValorTotal) " +
                                                                                            "FROM tbProdutoComanda " +
                                                                                            "WHERE ComandaID = " + id +
                                                                                            " and cStatus = 'Ativo') " +
                                                    "WHERE ID = " + id);
                    db.SaveChanges();
                    int? idkkk = id;
                    return RedirectToAction("Caixa", new { id = idkkk });
                }
                if (param == 0)
                {
                    ViewBag.addProduto = db.Database
                                            .ExecuteSqlCommand("INSERT INTO tbProdutoComanda (ComandaID, ProdutoID, Quantidade, ValorTotal, cStatus) " +
                                                                "VALUES(" + id + ", " + ProdutoID + ", " + Quant + ", (SELECT Preco*" + Quant +
                                                                                                        " FROM tbProduto " +
                                                                                                        "WHERE ProdutoID = " + ProdutoID + "), " +
                                                                                                        "'Ativo')");
                    db.Database.ExecuteSqlCommand("UPDATE tbComanda " +
                                                    "SET Situacao = 'Ativa', ValorTotal = (SELECT SUM(ValorTotal) " +
                                                                                            "FROM tbProdutoComanda " +
                                                                                            "WHERE ComandaID = " + id +
                                                                                            " and cStatus = 'Ativo') " +
                                                    "WHERE ID = " + id);
                    db.SaveChanges();
                    int? idkk = id;
                    return RedirectToAction("Caixa", new { id = idkk });
                }


            }

            return View();
        }

        public ActionResult Finalizar(int? id)
        {
            // "Aguardando pagamento da comanda x"
            //var lista2 = db.tbProdutoComandaVendas.Where(i => i.ProdutoComanda.ComandaID == id).Where(i => i.ProdutoComanda.cStatus == "Ativo");
            return View();
        }

        [HttpPost, ActionName("Finalizar")]
        [ValidateAntiForgeryToken]
        public ActionResult PagFinalizado(string FormaPag, int? id)
        {
            var NomeFunc = Session["usuarioLogado"];
            var total = db.tbComanda
                                .Where(i => i.ID == id)
                                .Select(i => i.ValorTotal)
                                .Single();

            var param = db.tbVendas
                            .ToList()
                            .Count();

            if (param > 0)
            {
                // Criando a int "identity" da vendaID

                var consulta = db.tbVendas
                                    .Where(i => i.VendaID != 0)
                                    .OrderByDescending(i => i.VendaID)
                                    .Select(i => i.VendaID)
                                    .Take(1)
                                    .Single();

                var VendaID = consulta + 1;

                // Criar uma nova venda
                db.Database.ExecuteSqlCommand(
                                                "SET IDENTITY_INSERT tbVendas ON " +
                                                "INSERT INTO tbVendas (VendaID, NomeFuncionario, FormaPag, Comanda, ValorTotal) " +
                                                "VALUES (" + VendaID + ", '" + NomeFunc + "', '" + FormaPag + "', " + id + ", " + total + ")"
                                            );

                // Insert das Relações Produto e Comanda 
                db.Database.ExecuteSqlCommand(
                                                "INSERT INTO tbProdutoComandaVendas (ProdutoComandaID,VendaID) " +
                                                "SELECT ProdutoComandaID, " + VendaID + " FROM tbProdutoComanda WHERE ComandaID = " + id +
                                                " and cStatus = 'Ativo'"
                                            );

                // Alterando a comanda para vazia
                db.Database.ExecuteSqlCommand(
                                                "UPDATE tbComanda SET Situacao = 'Vazia', ValorTotal = 0 WHERE ID = " + id
                                            );

                // Setando os ProdutoComanda para "Fechado"
                db.Database.ExecuteSqlCommand(
                                                "UPDATE tbProdutoComanda SET cStatus = 'Fechado' WHERE ComandaID = " + id
                                            );
                return RedirectToAction("CaixaIDComanda");
            }
            if (param == 0)
            {
                db.Database.ExecuteSqlCommand(
                                                "SET IDENTITY_INSERT tbVendas ON " +
                                                "INSERT INTO tbVendas (VendaID, NomeFuncionario, FormaPag, Comanda, ValorTotal) " +
                                                "VALUES (100, '" + NomeFunc + "', '" + FormaPag + "', " + id + ", " + total + ")"
                                            );

                db.Database.ExecuteSqlCommand(
                                                "INSERT INTO tbProdutoComandaVendas (ProdutoComandaID,VendaID) " +
                                                "SELECT ProdutoComandaID, 100 FROM tbProdutoComanda WHERE ComandaID = " + id +
                                                " and cStatus = 'Ativo'"
                                            );

                db.Database.ExecuteSqlCommand(
                                                "UPDATE tbComanda SET Situacao = 'Vazia', ValorTotal = 0 WHERE ID = " + id
                                            );

                db.Database.ExecuteSqlCommand(
                                                "UPDATE tbProdutoComanda SET cStatus = 'Fechado' WHERE ComandaID = " + id
                                            );
                return RedirectToAction("CaixaIDComanda");
            }
            return View();
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
        public ActionResult EditConfirmed(int? ProdutoComandaID, int? ComandaID, int? ProdutoID, int? Quantidade, ProdutoComanda pc)
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
                                        "SET ProdutoID = {0}, " +
                                            "Quantidade = {2}, " +
                                            "ValorTotal = (SELECT Preco*" + Quantidade +
                                                        " FROM tbProduto " +
                                                        "WHERE ProdutoID = " + ProdutoID + ") " +
                                        "WHERE ProdutoComandaID = {1}", ProdutoID, ProdutoComandaID, Quantidade);
                db.Database.ExecuteSqlCommand("UPDATE tbComanda " +
                                                "SET ValorTotal = (SELECT SUM(ValorTotal) " +
                                                                "FROM tbProdutoComanda " +
                                                                "WHERE ComandaID = " + ComandaID +
                                                                " and cStatus = 'Ativo') " +
                                                "WHERE ID = " + ComandaID);
                db.SaveChanges();
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
                db.SaveChanges();
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