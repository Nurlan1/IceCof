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
    public class BuyingController : Controller
    {
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Buying/
        [Authorize(Roles = "Director,Manager")]
        public ActionResult Index()
        {
            var buyings = db.buyings.Include(b => b.raw1).Include(b => b.worker1);
            return View(buyings.ToList());
        }

        // GET: /Buying/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buying buying = db.buyings.Find(id);
            if (buying == null)
            {
                return HttpNotFound();
            }
            return View(buying);
        }

        // GET: /Buying/Create
        public ActionResult Create()
        {   
            ViewBag.raw = new SelectList(db.raws, "id", "raw1");
            ViewBag.worker = new SelectList(db.workers, "id", "name");
            return View();
        }

        // POST: /Buying/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,raw,count,worker,date,sum")] buying buying)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.buyings.Add(buying);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.raw = new SelectList(db.raws, "id", "raw1", buying.raw);
                ViewBag.worker = new SelectList(db.workers, "id", "name", buying.worker);
                return View(buying);
            }
            catch
            {
                Response.Write("<script>alert('Не хватает денег в бюджете!');</script>");
            }
            ViewBag.raw = new SelectList(db.raws, "id", "raw1", buying.raw);
            ViewBag.worker = new SelectList(db.workers, "id", "name", buying.worker);
            return View(buying);
    }
        // GET: /Buying/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buying buying = db.buyings.Find(id);
            if (buying == null)
            {
                return HttpNotFound();
            }
            ViewBag.raw = new SelectList(db.raws, "id", "raw1", buying.raw);
            ViewBag.worker = new SelectList(db.workers, "id", "name", buying.worker);
            return View(buying);
        }

        // POST: /Buying/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,raw,count,worker,date,sum")] buying buying)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buying).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.raw = new SelectList(db.raws, "id", "raw1", buying.raw);
            ViewBag.worker = new SelectList(db.workers, "id", "name", buying.worker);
            return View(buying);
        }

        // GET: /Buying/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            buying buying = db.buyings.Find(id);
            if (buying == null)
            {
                return HttpNotFound();
            }
            return View(buying);
        }

        // POST: /Buying/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            buying buying = db.buyings.Find(id);
            db.buyings.Remove(buying);
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
