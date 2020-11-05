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

namespace JapanoriSystem.Controllers
{
    public class ProdutoComandasController : Controller
    {
        private bdJapanoriContext db = new bdJapanoriContext();

        // GET: ProdutoComandas
        public ActionResult Index()
        {
            var tbProdutoComanda = db.tbProdutoComanda.Include(p => p.Comanda).Include(p => p.Produto);
            return View(tbProdutoComanda.ToList());
        }

        // GET: ProdutoComandas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoComanda produtoComanda = db.tbProdutoComanda.Find(id);
            if (produtoComanda == null)
            {
                return HttpNotFound();
            }
            return View(produtoComanda);
        }

        // GET: ProdutoComandas/Create
        public ActionResult Create()
        {
            ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "Situacao");
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome");
            return View();
        }

        // POST: ProdutoComandas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdutoComandaID,ComandaID,ProdutoID,Quantidade")] ProdutoComanda produtoComanda)
        {
            if (ModelState.IsValid)
            {
                db.tbProdutoComanda.Add(produtoComanda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "Situacao", produtoComanda.ComandaID);
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome", produtoComanda.ProdutoID);
            return View(produtoComanda);
        }

        // GET: ProdutoComandas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoComanda produtoComanda = db.tbProdutoComanda.Find(id);
            if (produtoComanda == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "Situacao", produtoComanda.ComandaID);
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome", produtoComanda.ProdutoID);
            return View(produtoComanda);
        }

        // POST: ProdutoComandas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoComandaID,ComandaID,ProdutoID,Quantidade")] ProdutoComanda produtoComanda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produtoComanda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "Situacao", produtoComanda.ComandaID);
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome", produtoComanda.ProdutoID);
            return View(produtoComanda);
        }

        // GET: ProdutoComandas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoComanda produtoComanda = db.tbProdutoComanda.Find(id);
            if (produtoComanda == null)
            {
                return HttpNotFound();
            }
            return View(produtoComanda);
        }

        // POST: ProdutoComandas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProdutoComanda produtoComanda = db.tbProdutoComanda.Find(id);
            db.tbProdutoComanda.Remove(produtoComanda);
            db.SaveChanges();
            return RedirectToAction("Index");
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
