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
    public class SellingController : Controller
    {
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Selling/
        [Authorize(Roles = "Director,Manager")]
        public ActionResult Index()
        {
            var sellings = db.sellings.Include(s => s.product1).Include(s => s.worker1);
            return View(sellings.ToList());
        }

        // GET: /Selling/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            selling selling = db.sellings.Find(id);
            if (selling == null)
            {
                return HttpNotFound();
            }
            return View(selling);
        }

        // GET: /Selling/Create
        public ActionResult Create()
        {
            ViewBag.product = new SelectList(db.products, "id", "product1");
            ViewBag.worker = new SelectList(db.workers, "id", "name");
            return View();
        }

        // POST: /Selling/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,product,count,worker,date,sum")] selling selling)
        {   try
            {
            if (ModelState.IsValid)
            {
                db.sellings.Add(selling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product = new SelectList(db.products, "id", "product1", selling.product);
            ViewBag.worker = new SelectList(db.workers, "id", "name", selling.worker);
            return View(selling);
            }
        catch
        {
            Response.Write("<script>alert('Не хватает готового продукта!');</script>");
        }
        ViewBag.product = new SelectList(db.products, "id", "product1", selling.product);
        ViewBag.worker = new SelectList(db.workers, "id", "name", selling.worker);
        return View(selling);

        }

        // GET: /Selling/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            selling selling = db.sellings.Find(id);
            if (selling == null)
            {
                return HttpNotFound();
            }
            ViewBag.product = new SelectList(db.products, "id", "product1", selling.product);
            ViewBag.worker = new SelectList(db.workers, "id", "name", selling.worker);
            return View(selling);
        }

        // POST: /Selling/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,product,count,worker,date,sum")] selling selling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(selling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product = new SelectList(db.products, "id", "product1", selling.product);
            ViewBag.worker = new SelectList(db.workers, "id", "name", selling.worker);
            return View(selling);
        }

        // GET: /Selling/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            selling selling = db.sellings.Find(id);
            if (selling == null)
            {
                return HttpNotFound();
            }
            return View(selling);
        }

        // POST: /Selling/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            selling selling = db.sellings.Find(id);
            db.sellings.Remove(selling);
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
