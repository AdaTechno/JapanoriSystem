using JapanoriSystem.DAL;
using JapanoriSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        // GET: Caixa
        public ActionResult CaixaIDComanda()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Caixa(ProdutoComanda pc)
        {
            int comandaID = pc.ComandaID;

            
            var list = db.tbProdutoComanda.Where(i => i.ComandaID == comandaID).ToList();
            return View(list);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            
            ProdutoComanda pc = db.tbProdutoComanda.Find(id);

            //ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "Situacao", produtoComanda.ComandaID);
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome", pc.ProdutoID);
            return PartialView(pc);
        }

        [HttpPost]
        public JsonResult Edit(ProdutoComanda pc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pc).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            //ViewBag.ComandaID = new SelectList(db.tbComanda, "ID", "Situacao", produtoComanda.ComandaID);
            ViewBag.ProdutoID = new SelectList(db.tbProduto, "ProdutoID", "Nome", pc.ProdutoID);
            return Json(pc,JsonRequestBehavior.AllowGet);
        }

        private int IsExistingProduto(int id)
        {
            return id;
        }
    }
}