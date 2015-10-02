using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LCNetv5.Models;
using Microsoft.AspNet.Identity;
using LCNetv5.Classes;

namespace LCNetv5.Controllers
{
    [Authorize(Roles = "Admin,Loans")]
    public class LoansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Loans
        public ActionResult Index(int Id = -1)
        {
            
            var loans = db.Loans.Include(l => l.Program);
            ViewBag.Program = new Program { Id = -1};
            if (Id != -1)
            {
                ViewBag.Program = db.Programs.Find(Id);
                loans= db.Loans.Where(x => Id == x.ProgramId).Include(p => p.Program);
            }
            
            return View(loans.ToList());
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create(int Id = -1)
        {

            var loan = new Loan { ProgramId = Id };
            return View(loan);
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AmtLoan,TransferDate,Round,InterestRate,Frequency,Instalments,ProgramId,TimePeriod")] Loan loan)
        {
            loan.Active = true;
            if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                db.LoanChanges.Add(new LoanChange(loan){ UserId = User.Identity.GetUserId(), ChangeType = Change.Created});
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramId = new SelectList(db.Programs, "Id", "Id", loan.ProgramId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramId = new SelectList(db.Programs, "Id", "Id", loan.ProgramId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AmtLoan,TransferDate,Active,Round,InterestRate,Frequency,Instalments,ProgramId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.LoanChanges.Add(new LoanChange(loan){ UserId = User.Identity.GetUserId(), ChangeType = Change.Modified});
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = loan.Id });
            }
            ViewBag.ProgramId = new SelectList(db.Programs, "Id", "Id", loan.ProgramId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
            db.LoanChanges.Add(new LoanChange(loan){ UserId = User.Identity.GetUserId(), ChangeType = Change.Deleted});
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

        public ActionResult IndLoanCreate(int Id)
        {
            ViewBag.Program = db.Programs.Find(Id);
            
            var loan = new Loan { ProgramId = Id , Program = ViewBag.Program };
            return View(loan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndLoanCreate([Bind(Exclude = "Id")] Loan newloan, string Command )
        {
            if (Command == "Calculate")
            {
                //THIS ADDS INTEREST
                //newloan.InterestRate = (float) (newloan.InterestRate + WebScraping.HNInterestRate());
                Program RoundTbl = db.Programs.Find(newloan.ProgramId);
                PaymentPlan Plan = new PaymentPlan();
                //PPlanHold holder = new PPlanHold();
                //holder.CreatePlan(Convert.ToInt32(newloan.Instalments), newloan.RepFreqId, newloan.AmtLoan, Program, newloan.TransferDate);
                Plan.CreatePaymentPlan(newloan);
                //holder.CreatePlanV2(newloan, ProgramIR);
                //ViewBag.RoundId = (int)newloan.RoundId;

                //ViewBag.ProgramName = newloan.Program.ProgName;
                //ViewBag.ProgramIR = newloan.InterestRate;
                //ViewBag.sugAmt = RoundTbl.LoanAmt();
                Loan newloan2 = new Loan();
                //newloan.AmtLoan = RoundTbl.LoanAmt();
                //ViewBag.RepFreqId = new SelectList(db.RepFreq, "Id", "Frequency");
                ViewBag.plan = Plan;
                ViewBag.Program = db.Programs.Find(newloan.ProgramId);
                return View(newloan2);

            }
            else
            {


                if (ModelState.IsValid)
                {
                    newloan.Active = true;
                    newloan.Program = db.Programs.Find(newloan.ProgramId);
                    db.Loans.Add(newloan);
                    db.LoanChanges.Add(new LoanChange(newloan){ UserId = User.Identity.GetUserId(), ChangeType = Change.Created});
                    db.SaveChanges();
                    return RedirectToAction("Index", new {Id = newloan.ProgramId });
                }

                //ViewBag.RoundId = new SelectList(db.RoundTbls, "Id", "Id", newloan.RoundId);

                return View(newloan);
            }
        }
    }
}
