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
    public class RepaymentController : Controller
    {
        private ConfectioneryEntities db = new ConfectioneryEntities();

        // GET: /Repayment/
        [Authorize(Roles = "Director,Accountant")]
        public ActionResult Index()
        {
            var repayments = db.Repayments.Include(r => r.credit);
            return View(repayments.ToList());
        }

        // GET: /Repayment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repayment repayment = db.Repayments.Find(id);
            if (repayment == null)
            {
                return HttpNotFound();
            }
            return View(repayment);
        }

        // GET: /Repayment/Create
        public ActionResult Create()
        {
            ViewBag.bank = new SelectList(db.credits, "id", "bank");
            ViewData["credit_info"] = db.credits.ToList();
            ViewData["payment_list"] = db.Repayments.ToList();
            return View();
        }

        // POST: /Repayment/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,bank,payment_date,sum_All,payment_sum,percents,fine")] Repayment repayment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Repayments.Add(repayment);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
                
                ViewBag.bank = new SelectList(db.credits, "id", "bank", repayment.bank);
                return View(repayment);
            }
            catch {
                Response.Write("<script>alert('Не хватает денег в бюджете!');</script>");
            }
            ViewBag.bank = new SelectList(db.credits, "id", "bank", repayment.bank);
            return View(repayment);
        }

        // GET: /Repayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repayment repayment = db.Repayments.Find(id);
            if (repayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.bank = new SelectList(db.credits, "id", "bank", repayment.bank);
            return View(repayment);
        }

        // POST: /Repayment/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,bank,payment_date,sum_All,payment_sum,percents,fine")] Repayment repayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bank = new SelectList(db.credits, "id", "bank", repayment.bank);
            return View(repayment);
        }

        // GET: /Repayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repayment repayment = db.Repayments.Find(id);
            if (repayment == null)
            {
                return HttpNotFound();
            }
            return View(repayment);
        }

        // POST: /Repayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repayment repayment = db.Repayments.Find(id);
            db.Repayments.Remove(repayment);
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
