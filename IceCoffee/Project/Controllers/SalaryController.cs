using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
namespace Project.Controllers

{
    
    public class SalaryController : Controller
    {
        
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Salary/
        [Authorize(Roles = "Director,Accountant")]
        public ActionResult Index()
        {
            var salaries = db.salaries.Include(s => s.worker1).Include(s => s.month1).Include(s => s.year1);
            return View(salaries.ToList());
        }

        // GET: /Salary/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary salary = db.salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // GET: /Salary/Create
        public ActionResult Create()
        {
            ViewBag.worker = new SelectList(db.workers, "id", "name");
            ViewBag.month = new SelectList(db.months, "id", "month1");
            ViewBag.year = new SelectList(db.years, "id", "year1");
            return View();
        }

        // POST: /Salary/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,worker,month,year,date,summm,premium,count,payment,count_making,count_buying,count_selling")] salary salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.salaries.Add(salary);
                    db.SaveChanges();
                    return RedirectToAction("Edit/"+salary.id);
                }
                ViewBag.worker = new SelectList(db.workers, "id", "name", salary.worker);
                ViewBag.month = new SelectList(db.months, "id", "month1", salary.month);
                ViewBag.year = new SelectList(db.years, "id", "year1", salary.year);
                return View(salary);
            }
            catch (Exception e)
            {
                if (e.GetBaseException().Message.ToString().StartsWith("Cannot insert duplicate key"))
                {
                    Response.Write("<script>alert('Ошибка! Невозможно повторная выплата!');</script>");
                }
                else {

                    Response.Write("<script>alert('Ошибка!" + e.GetBaseException().Message.ToString().Split('\n')[0].Remove(30) + "');</script>");
                }
               
                
                
                    
                    
                    
                
            }
            ViewBag.worker = new SelectList(db.workers, "id", "name", salary.worker);
            ViewBag.month = new SelectList(db.months, "id", "month1", salary.month);
            ViewBag.year = new SelectList(db.years, "id", "year1", salary.year);
            return View(salary);
        

        }

        // GET: /Salary/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary salary = db.salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.worker = new SelectList(db.workers, "id", "name", salary.worker);
            ViewBag.month = new SelectList(db.months, "id", "month1", salary.month);
            ViewBag.year = new SelectList(db.years, "id", "year1", salary.year);
            return View(salary);
        }

        // POST: /Salary/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,worker,month,year,date,summm,premium,count,payment,count_making,count_buying,count_selling")] salary salary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.worker = new SelectList(db.workers, "id", "name", salary.worker);
            ViewBag.month = new SelectList(db.months, "id", "month1", salary.month);
            ViewBag.year = new SelectList(db.years, "id", "year1", salary.year);
            return View(salary);
        }

        // GET: /Salary/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary salary = db.salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: /Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            salary salary = db.salaries.Find(id);
            db.salaries.Remove(salary);
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
