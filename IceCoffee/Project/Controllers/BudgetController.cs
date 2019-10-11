using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class BudgetController : Controller
    {
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Budget/
        [Authorize(Roles = "Director,Accountant")]
        public ActionResult Index()
        {
            return View(db.budjets.ToList());
        }

        // GET: /Budget/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            budjet budjet = db.budjets.Find(id);
            if (budjet == null)
            {
                return HttpNotFound();
            }
            return View(budjet);
        }

        // GET: /Budget/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Budget/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="budjet1,proc_cost,prem_proc,id")] budjet budjet)
        {
            if (ModelState.IsValid)
            {
                db.budjets.Add(budjet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(budjet);
        }

        // GET: /Budget/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            budjet budjet = db.budjets.Find(id);
            if (budjet == null)
            {
                return HttpNotFound();
            }
            return View(budjet);
        }

        // POST: /Budget/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="budjet1,proc_cost,prem_proc,id")] budjet budjet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budjet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(budjet);
        }

        // GET: /Budget/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            budjet budjet = db.budjets.Find(id);
            if (budjet == null)
            {
                return HttpNotFound();
            }
            return View(budjet);
        }

        // POST: /Budget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            budjet budjet = db.budjets.Find(id);
            db.budjets.Remove(budjet);
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
