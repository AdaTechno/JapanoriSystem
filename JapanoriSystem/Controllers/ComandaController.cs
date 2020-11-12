using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JapanoriSystem.DAL;
using JapanoriSystem.Models;
using WebGrease.Activities;
using PagedList.Mvc;
using PagedList;
using JapanoriSystem.ViewModels;
using System.Data.Entity.Infrastructure;
using System.Web.UI.WebControls;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Antlr.Runtime.Tree;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JapanoriSystem.Controllers
{
    public class ComandaController : Controller
    {
        bdJapanoriContext db = new bdJapanoriContext();


        //              Tela Inicial
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //ProdutoComanda pc = new ProdutoComanda();
            //      Cadeia de objetos para definir a "current" Ordem da listagem das comandas
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CodSortParm = String.IsNullOrEmpty(sortOrder) ? "cod_cre" : ""; // objeto que organiza a lista em ordem do código
            ViewBag.SitSortParm = String.IsNullOrEmpty(sortOrder) ? "sit_cre" : ""; // objeto que organiza a lista em ordem da situacao
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "valor_decre" : ""; // objeto que organiza a lista em ordem de preço

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var comandas = from s in db.tbComanda
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                comandas = comandas.Where(s => 
                                                s.ID.ToString().Contains(searchString) ||
                                                s.Situacao.ToString().Contains(searchString)
                                            );
            }
            switch (sortOrder)
            {
                case "cod_cre":
                    comandas = comandas.OrderBy(s => s.ID);
                    break;
                case "sit_cre":
                    comandas = comandas.OrderBy(s => s.Situacao);
                    break;
                case "valor_decre":
                    comandas = comandas.OrderByDescending(s => s.ValorTotal);
                    break;
                default:
                    comandas = comandas.OrderByDescending(s => s.ValorTotal);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(comandas.ToPagedList(pageNumber, pageSize));
        }

        //          Tela Criação
        public ActionResult Create()
        {
            return View();
        }

        //      POST Tela Criação
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? comandaID)
        {

            if (comandaID == null || comandaID < 100)
            {
                ViewBag.ConfErro = "1";
                ViewBag.Erro = "Digite um número válido de pelo menos 3 dígitos";
            }
            if (comandaID != null && comandaID >= 100)
            {
                var param = db.tbComanda
                                .Where(i => i.ID == comandaID)
                                .ToList()
                                .Count();

                if (param > 0)
                {
                    ViewBag.ConfErro = "2";
                    ViewBag.Erro = "Já existe uma comanda com este código";
                }
                if (param == 0)
                {
                    ViewBag.addComanda = db.Database
                                        .ExecuteSqlCommand("INSERT INTO tbComanda (ID, Situacao, cStatus, ValorTotal) " +
                                                            "VALUES(" + comandaID + ", 'Vazia', 'On', 0)");
                    db.SaveChanges();
                    ViewBag.Success = "Comanda criada com sucesso";
                }
            }

            return View();
        }

        //      Tela Inserir 1
        public ActionResult Inserir()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inserir(int? ComandaID, int? Quant, int? ProdutoID)
        {

            if (ComandaID == null || ProdutoID == null || Quant == null)
            {
                ViewBag.ConfirmErro = "1";
                ViewBag.MsgErro = "Não foi possível salvar as informações";
            }
            else
            {

                var param = db.tbProdutoComanda
                                    .Where(i => i.ComandaID == ComandaID)
                                    .Where(i => i.ProdutoID == ProdutoID)
                                    .Where(i => i.cStatus == "Ativo")
                                    .ToList()
                                    .Count();
                if (param > 0)
                {
                    ViewBag.UpProduto = db.Database
                                            .ExecuteSqlCommand("UPDATE tbProdutoComanda " +
                                                                "SET Quantidade = Quantidade + " + Quant + ", " +
                                                                    "ValorTotal = (Quantidade+" + Quant + ")*(SELECT Preco " +
                                                                                                        "FROM tbProduto " +
                                                                                                        "WHERE ProdutoID = " + ProdutoID + "), " +
                                                                    "cStatus = 'Ativo' " +
                                                                "WHERE ComandaID = " + ComandaID + " and ProdutoID = " + ProdutoID);
                    db.Database.ExecuteSqlCommand("UPDATE tbComanda " +
                                                    "SET Situacao = 'Ativa', ValorTotal = (SELECT SUM(ValorTotal) " +
                                                                                            "FROM tbProdutoComanda " +
                                                                                            "WHERE ComandaID = " + ComandaID +
                                                                                            " and cStatus = 'Ativo') " +
                                                    "WHERE ID = " + ComandaID);

                    db.SaveChanges();
                    return RedirectToAction("Edit", new { id = ComandaID });
                }
                if (param == 0)
                {
                    ViewBag.addProduto = db.Database
                                            .ExecuteSqlCommand("INSERT INTO tbProdutoComanda (ComandaID, ProdutoID, Quantidade, ValorTotal, cStatus) " +
                                                                "VALUES(" + ComandaID + ", " + ProdutoID + ", " + Quant + ", (SELECT Preco*" + Quant +
                                                                                                                            " FROM tbProduto " +
                                                                                                                            "WHERE ProdutoID = " + ProdutoID + "), " +
                                                                                                                            "'Ativo')");
                    db.Database.ExecuteSqlCommand("UPDATE tbComanda " +
                                                     "SET Situacao = 'Ativa', ValorTotal = (SELECT SUM(ValorTotal) " +
                                                                                             "FROM tbProdutoComanda " +
                                                                                             "WHERE ComandaID = " + ComandaID +
                                                                                             " and cStatus = 'Ativo') " +
                                                     "WHERE ID = " + ComandaID);

                    db.SaveChanges();
                    return RedirectToAction("Edit", new { id = ComandaID });
                }


            }

            return View();
        }


        //          Tela Edição de Comanda
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "Comanda");
            }
            else
            {

                var lista = db.tbProdutoComanda.Where(x => x.ComandaID == id).Where(x => x.cStatus == "Ativo").ToList();

                ViewBag.ID = id;

                string situacao = db.tbComanda
                                    .Where(x => x.ID == id)
                                    .Select(x => x.Situacao)
                                    .Single();
                ViewBag.Situacao = situacao;

                var total = db.tbComanda
                                .Where(x => x.ID == id)
                                .Select(x => x.ValorTotal)
                                .Single();
                //double total = db.Database.ExecuteSqlCommand("SELECT ValorTotal FROM tbComanda WHERE ID = " + id); 
                ViewBag.ValorTotal = total;

                db.SaveChanges();
                return View(lista);
            }

        }


        //          Tela Excluir dados da Comanda
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Comanda");
            }
            Comanda comanda = db.tbComanda.Find(id);
            if (comanda == null)
            {
                return RedirectToAction("Index", "Comanda");
            }
            return View(comanda);
        }

        //          POST Tela Excluir dados da Comanda
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comanda comanda = db.tbComanda.Find(id);
            db.tbComanda.Remove(comanda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //          Ação de se "desconectar" do banco
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
