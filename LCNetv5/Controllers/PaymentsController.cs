using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LCNetv5.Models;
using LCNetv5.Classes;

namespace LCNetv5.Controllers
{
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        public ActionResult Index(int Id = -1)
        {
            var payments = db.Payments.Include(p => p.Loan);
            if (Id != -1)
            {
            ViewBag.Loan = db.Loans.Find(Id);
            payments = db.Payments.Where(x => x.LoanId == Id).Include(p => p.Loan);
            }
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LCNetv5.Models.Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DatePmtDue,DatePmtPaid,AmtPaid,LoanId")] LCNetv5.Models.Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", payment.LoanId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LCNetv5.Models.Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", payment.LoanId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DatePmtDue,DatePmtPaid,AmtPaid,LoanId")] LCNetv5.Models.Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.PaymentChanges.Add(new PaymentChange(payment) { UserId = User.Identity.GetUserId(), ChangeType = Change.Modified }); 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", payment.LoanId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LCNetv5.Models.Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LCNetv5.Models.Payment payment = db.Payments.Find(id);
            var returnID = payment.Loan.Id;
            db.Payments.Remove(payment);
            db.PaymentChanges.Add(new PaymentChange(payment) { UserId = User.Identity.GetUserId(), ChangeType = Change.Deleted});
            db.SaveChanges();
            return RedirectToAction("IndPayment" , new {id = returnID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult IndPayment(int id)
        {
            PaymentPlan x = new PaymentPlan();
            

            Loan loaninfo = db.Loans.Find(id);
            ViewBag.Loan = loaninfo;
            x.CreatePaymentPlan(loaninfo);
            x.applyPayments((decimal)loaninfo.Payments.Sum(y => y.AmtPaid));
            ViewBag.plan = x;
            ViewBag.Total = x.getTotalPaymentDue() - loaninfo.Payments.Sum(y => y.AmtPaid);


            return View(loaninfo);
        }
        public ActionResult IndPaymentCreate(int id, System.DateTime DueDate, double AmtDue = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.LoanId = id;
            LCNetv5.Models.Payment newPayment = new LCNetv5.Models.Payment();
            newPayment.LoanId = id;
            newPayment.DatePmtDue = DueDate;
            if (DueDate == null)
            {
                DueDate = new DateTime(1981, 03, 01);
            }
            newPayment.AmtPaid = (decimal)AmtDue;
            return View(newPayment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndPaymentCreate([Bind(Exclude = "Id")] LCNetv5.Models.Payment paymentTbl)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(paymentTbl);
                db.PaymentChanges.Add(new PaymentChange (paymentTbl){ UserId = User.Identity.GetUserId(), ChangeType = Change.Created });
                db.SaveChanges();
                return RedirectToAction("IndPayment", new {id = paymentTbl.LoanId});
            }

            ViewBag.LoanId = new SelectList(db.Loans, "Id", "Id", paymentTbl.LoanId);
            return View(paymentTbl);
        }
        public ActionResult Contract(int id)
        {
            Loan loan = db.Loans.Find(id);
            
            PaymentPlan x = new PaymentPlan();

            Loan loaninfo = db.Loans.Find(id);
            x.CreatePaymentPlan(loaninfo);
            ViewBag.plan = x;
            ViewBag.EMI = x.Payments.First().PaymentDue;

            ViewBag.planDeets = x;

            ViewBag.ClientName = loan.Program.Client.getFullName();


            return View(loan);
        }


        public ActionResult CloseLoan(int id)
        {
            Loan loan = db.Loans.Find(id);
            loan.Active = false;
            db.SaveChanges();





            return RedirectToAction("IndPayment", new { id = id});
        }
    }
}
