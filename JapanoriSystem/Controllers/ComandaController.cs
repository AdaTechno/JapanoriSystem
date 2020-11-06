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
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "preco_decre" : ""; // objeto que organiza a lista em ordem de preço

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

            var produto = from cp in db.tbProduto
                          select cp;


            if (!string.IsNullOrEmpty(searchString))
            {
                comandas = comandas.Where(s => s.ID.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "cod_cre":
                    comandas = comandas.OrderBy(s => s.ID);
                    break;
                case "sit_cre":
                    comandas = comandas.OrderBy(s => s.Situacao);
                    break;
                default:
                    comandas = comandas.OrderBy(s => s.ID);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(comandas.ToPagedList(pageNumber, pageSize));
        }


        //          Tela Detalhes
        public ActionResult Details(int? id)
        {
            // Ação de redirecionar para Tela inicial Comandas se não retornar um ID para detalhar
            if (id == null)
            {
                return RedirectToAction("Index", "Comanda");
            }
            Comanda comanda = db.tbComanda.Find(id); // Retorna em lista os dados da comanda que possuir o ID retornado ao sistema
            if (comanda == null)
            {
                return RedirectToAction("Index", "Comanda");
            }
            return View(comanda);
        }

        //          Tela Criação
        public ActionResult Create()
        {
            return View();
        }

        //      POST Tela Criação
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Situacao,Status")] Comanda comanda)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.tbComanda.Add(comanda);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
                ViewBag.Message = "Não foi possível Criar a comanda";
            }
            return View(comanda);
        }

        //      Tela Inserir 1
        public ActionResult Inserir()
        {
            
            //ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "ID");
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inserir([Bind(Include = "ComandaID,ProdutoID")] ProdutoComanda produtoComanda)
        {
            
           
            if (ModelState.IsValid)
            {
                db.tbProdutoComanda.Add(produtoComanda);
                db.SaveChanges();
                ViewBag.Msg = "Produto inserido com sucesso!";
                return RedirectToAction("Inserir");
            }

            //ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "ID", produtoComanda.ComandaID);
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome", produtoComanda.ProdutoID);

            return View(produtoComanda);
        }

        public JsonResult getProdutoByID(int id)
        {
            List<ProdutoComanda> list = new List<ProdutoComanda>();
            list = db.tbProdutoComanda.Where(i => i.ComandaID == id).ToList();
            return Json(new ListItem());
        }


        //          Tela Edição de Comanda
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Comanda");
            }

            Comanda comanda = db.tbComanda
                .Include(i => i.Produtos)
                .Where(i => i.ID == id)
                .Single();

            PopulateAssignedProdutoData(comanda);

            if (comanda == null)
            {
                return RedirectToAction("Index", "Comanda");
            }

            return View(comanda);
        }

        private void PopulateAssignedProdutoData(Comanda comanda)
        {
            var allProdutos = db.tbProduto;
            var ComandaProdutos = new HashSet<int>(comanda.Produtos.Select(c => c.ProdutoID));
            var viewModel = new List<AssignedProdutoData>();
            foreach (var produto in allProdutos)
            {
                viewModel.Add(new AssignedProdutoData
                {
                    ProdutoID = produto.ProdutoID,
                    Nome = produto.Nome,
                    Desc = produto.Desc,
                    Preco = produto.Preco,
                    Assigned = ComandaProdutos.Contains(produto.ProdutoID)
                });
            }
            ViewBag.Produtos = viewModel;
        }

        //      POST Tela Edição de Comanda
        /*[HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(int? id, string[] selectedProdutos)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Comanda");
            }
            var comandaToUpdate = db.tbComanda
               .Include(i => i.Produtos)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(comandaToUpdate, "",
               new string[] { "ID", "Situacao" }))
            {
                try
                {
                    UpdateComandaProdutos(selectedProdutos, comandaToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException )
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedProdutoData(comandaToUpdate);
            return View(comandaToUpdate);
        }

        private void UpdateComandaProdutos(string[] selectedProdutos, Comanda comandaToUpdate)
        {
            if (selectedProdutos == null)
            {
                comandaToUpdate.Produtos = new ICollection<Produto>();
                return;
            }

            var selectedProdutosHS = new HashSet<string>(selectedProdutos);
            var ComandaProdutos = new HashSet<int>
                (comandaToUpdate.Produtos.Select(c => c.ProdutoID));
            foreach (var produto in db.tbProduto)
            {
                if (selectedProdutosHS.Contains(produto.ProdutoID.ToString()))
                {
                    if (!ComandaProdutos.Contains(produto.ProdutoID))
                    {
                        comandaToUpdate.Produtos.Add(produto);
                    }
                }
                else
                {
                    if (ComandaProdutos.Contains(produto.ProdutoID))
                    {
                        comandaToUpdate.Produtos.Remove(produto);
                    }
                }
            }
        }*/

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
