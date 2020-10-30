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
    public class ComandaController : Controller
    {
        private bdJapanoriContext db = new bdJapanoriContext();

        
        public ActionResult Index()
        {
            return View(db.tbComanda.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comanda comanda = db.tbComanda.Find(id);
            if (comanda == null)
            {
                return HttpNotFound();
            }
            return View(comanda);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comanda/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Situacao,Status")] Comanda comanda)
        {
            if (ModelState.IsValid)
            {
                db.tbComanda.Add(comanda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comanda);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comanda comanda = db.tbComanda.Find(id);
            if (comanda == null)
            {
                return HttpNotFound();
            }
            return View(comanda);
        }

        // POST: Comanda/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Situacao,Status")] Comanda comanda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comanda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comanda);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comanda comanda = db.tbComanda.Find(id);
            if (comanda == null)
            {
                return HttpNotFound();
            }
            return View(comanda);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comanda comanda = db.tbComanda.Find(id);
            db.tbComanda.Remove(comanda);
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
