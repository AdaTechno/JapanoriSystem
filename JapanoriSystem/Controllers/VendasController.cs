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
    public class VendasController : Controller
    {
        private bdJapanoriContext db = new bdJapanoriContext();

        // GET: Vendas
        public ActionResult Index()
        {
            return View(db.tbVendas.ToList());
        }

        // GET: Vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vendas = db.tbProdutoComandaVendas.Where(i => i.VendaID == id).ToList();

            ViewBag.idVenda = id;

            var NomeFunc = db.tbVendas
                                        .Where(i => i.VendaID == id)
                                        .Select(i => i.NomeFuncionario)
                                        .Single();
            ViewBag.NomeFunc = NomeFunc;

            var FormaPag = db.tbVendas
                                        .Where(i => i.VendaID == id)
                                        .Select(i => i.FormaPag)
                                        .Single();
            ViewBag.FormaPag = FormaPag;

            var Comanda = db.tbVendas
                                        .Where(i => i.VendaID == id)
                                        .Select(i => i.Comanda)
                                        .Single();
            ViewBag.Comanda = Comanda;

            if (vendas == null)
            {
                return HttpNotFound();
            }
            return View(vendas);
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
