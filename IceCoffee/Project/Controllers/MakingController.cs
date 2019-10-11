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
    public class MakingController : Controller
    {
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Making/
        [Authorize(Roles = "Director,Tech")]
        public ActionResult Index()
        {
            var makings = db.makings.Include(m => m.product1).Include(m => m.worker1);
            return View(makings.ToList());
        }

        // GET: /Making/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            making making = db.makings.Find(id);
            if (making == null)
            {
                return HttpNotFound();
            }
            return View(making);
        }

        // GET: /Making/Create
        public ActionResult Create()
        {
            ViewBag.product = new SelectList(db.products, "id", "product1");
            ViewBag.worker = new SelectList(db.workers, "id", "name");
            return View();
        }

        // POST: /Making/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,product,count,worker,date")] making making)
        {   try
            {
            if (ModelState.IsValid)
            {
                db.makings.Add(making);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product = new SelectList(db.products, "id", "product1", making.product);
            ViewBag.worker = new SelectList(db.workers, "id", "name", making.worker);
            return View(making);
            }
        catch
        {
            Response.Write("<script>alert('Не хватает сырья для изготовления!');</script>");
        }
        ViewBag.product = new SelectList(db.products, "id", "product1", making.product);
        ViewBag.worker = new SelectList(db.workers, "id", "name", making.worker);
        return View(making);

        }

        // GET: /Making/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            making making = db.makings.Find(id);
            if (making == null)
            {
                return HttpNotFound();
            }
            ViewBag.product = new SelectList(db.products, "id", "product1", making.product);
            ViewBag.worker = new SelectList(db.workers, "id", "name", making.worker);
            return View(making);
        }

        // POST: /Making/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,product,count,worker,date")] making making)
        {
            if (ModelState.IsValid)
            {
                db.Entry(making).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product = new SelectList(db.products, "id", "product1", making.product);
            ViewBag.worker = new SelectList(db.workers, "id", "name", making.worker);
            return View(making);
        }

        // GET: /Making/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            making making = db.makings.Find(id);
            if (making == null)
            {
                return HttpNotFound();
            }
            return View(making);
        }

        // POST: /Making/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            making making = db.makings.Find(id);
            db.makings.Remove(making);
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
