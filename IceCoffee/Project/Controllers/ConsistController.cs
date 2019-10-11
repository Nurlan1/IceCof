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
    public class ConsistController : Controller
    {
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Consist/
        [Authorize(Roles = "Director,Tech")]
        public ActionResult Index()
        {
            var consists = db.consists.Include(c => c.product1).Include(c => c.raw1);
            return View(consists.ToList());
        }

        // GET: /Consist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consist consist = db.consists.Find(id);
            if (consist == null)
            {
                return HttpNotFound();
            }
            return View(consist);
        }

        // GET: /Consist/Create
        public ActionResult Create()
        {
            ViewBag.product = new SelectList(db.products, "id", "product1");
            ViewBag.raw = new SelectList(db.raws, "id", "raw1");
            return View();
        }

        // POST: /Consist/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,product,raw,count")] consist consist)
        {
            if (ModelState.IsValid)
            {
                db.consists.Add(consist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product = new SelectList(db.products, "id", "product1", consist.product);
            ViewBag.raw = new SelectList(db.raws, "id", "raw1", consist.raw);
            return View(consist);
        }

        // GET: /Consist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consist consist = db.consists.Find(id);
            if (consist == null)
            {
                return HttpNotFound();
            }
            ViewBag.product = new SelectList(db.products, "id", "product1", consist.product);
            ViewBag.raw = new SelectList(db.raws, "id", "raw1", consist.raw);
            return View(consist);
        }

        // POST: /Consist/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,product,raw,count")] consist consist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product = new SelectList(db.products, "id", "product1", consist.product);
            ViewBag.raw = new SelectList(db.raws, "id", "raw1", consist.raw);
            return View(consist);
        }

        // GET: /Consist/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            consist consist = db.consists.Find(id);
            if (consist == null)
            {
                return HttpNotFound();
            }
            return View(consist);
        }

        // POST: /Consist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            consist consist = db.consists.Find(id);
            db.consists.Remove(consist);
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
