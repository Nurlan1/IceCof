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
    public class RawController : Controller
    {
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Raw/
        [Authorize(Roles = "Director,Tech")]
        public ActionResult Index()
        {
            var raws = db.raws.Include(r => r.unit1);
            return View(raws.ToList());
        }

        // GET: /Raw/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            raw raw = db.raws.Find(id);
            if (raw == null)
            {
                return HttpNotFound();
            }
            return View(raw);
        }

        // GET: /Raw/Create
        public ActionResult Create()
        {
            ViewBag.unit = new SelectList(db.units, "id", "unit1");
            return View();
        }

        // POST: /Raw/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,raw1,unit,count,cost,sum")] raw raw)
        {
            if (ModelState.IsValid)
            {
                db.raws.Add(raw);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.unit = new SelectList(db.units, "id", "unit1", raw.unit);
            return View(raw);
        }

        // GET: /Raw/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            raw raw = db.raws.Find(id);
            if (raw == null)
            {
                return HttpNotFound();
            }
            ViewBag.unit = new SelectList(db.units, "id", "unit1", raw.unit);
            return View(raw);
        }

        // POST: /Raw/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,raw1,unit,count,cost,sum")] raw raw)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raw).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.unit = new SelectList(db.units, "id", "unit1", raw.unit);
            return View(raw);
        }

        // GET: /Raw/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            raw raw = db.raws.Find(id);
            if (raw == null)
            {
                return HttpNotFound();
            }
            return View(raw);
        }

        // POST: /Raw/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            raw raw = db.raws.Find(id);
            db.raws.Remove(raw);
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
