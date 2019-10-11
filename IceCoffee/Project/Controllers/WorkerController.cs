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
    public class WorkerController : Controller
    {
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Worker/
        [Authorize(Roles = "Director,HR")]
        public ActionResult Index()
        {
            var workers = db.workers.Include(w => w.post1);
            return View(workers.ToList());
        }

        // GET: /Worker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            worker worker = db.workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // GET: /Worker/Create
        public ActionResult Create()
        {
            ViewBag.post = new SelectList(db.posts, "id", "post1");
            return View();
        }

        // POST: /Worker/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,name,post,address,phone,salary,login,password")] worker worker)
        {
            if (ModelState.IsValid)
            {
                db.workers.Add(worker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.post = new SelectList(db.posts, "id", "post1", worker.post);
            return View(worker);
        }

        // GET: /Worker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            worker worker = db.workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            ViewBag.post = new SelectList(db.posts, "id", "post1", worker.post);
            return View(worker);
        }

        // POST: /Worker/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name,post,address,phone,salary,login,password")] worker worker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(worker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.post = new SelectList(db.posts, "id", "post1", worker.post);
            return View(worker);
        }

        // GET: /Worker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            worker worker = db.workers.Find(id);
            if (worker == null)
            {
                return HttpNotFound();
            }
            return View(worker);
        }

        // POST: /Worker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            worker worker = db.workers.Find(id);
            db.workers.Remove(worker);
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
