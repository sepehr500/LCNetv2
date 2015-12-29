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

namespace LCNetv5.Controllers
{
    public class StandardFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StandardForms
        public ActionResult Index()
        {
            
            return View(db.StandardForms.ToList());
        }

        // GET: StandardForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StandardForm standardForm = db.StandardForms.Find(id);
            if (standardForm == null)
            {
                return HttpNotFound();
            }
            return View(standardForm);
        }

        // GET: StandardForms/Create
        public ActionResult Create(int ClientId)
        {
            try
            {
            
            Session["clientid"] =  db.Loans.Where(x => x.Program.Client.Id == ClientId).OrderByDescending(z => z.TransferDate).First() ;
            ViewBag.HasBuisnes = db.StandardForms.OrderByDescending(x => x.StartTime).First().Business;
            ViewBag.IsLate = db.Loans.Where(x => x.Program.Client.Id == ClientId).OrderByDescending(z => z.TransferDate).First().HowLate();
            }
            catch (Exception)
            {
                ViewBag.HasBuisnes = true;
                ViewBag.IsLate = 2;

            }
            Session["StartTime"] = DateTime.Now;
            return View();
        }

        // POST: StandardForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InterestGood,RepaySchedGood,PriceShock,StepsTaken,AdjustModeOfIncome,BuisnessImped,ChangeBuisnessModel,LessThan90Days,FormalSavings,InformalSavings,CapitalInvest,Freedom,Seasonal,HowSoSeasonal,WhatLargerLoanFor,FamilyDoingWell,ProblemsToChange,HowCanWeHelp,UserId,LoanFor,StartTime,EndTime,Savings,Business,TypeOfWork,Misc,LoanAmt")] StandardForm standardForm)
        {

            standardForm.UserId = User.Identity.GetUserId();
            standardForm.StartTime = (DateTime)Session["StartTime"];
            try
            {
             
            standardForm.LoanId= (int)Session["clientid"];

            }
            catch (Exception)
            {
                
            }
            standardForm.EndTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.StandardForms.Add(standardForm);
                db.SaveChanges();
                return RedirectToAction("Index", "Clients");
            }

            return View(standardForm);
        }

        // GET: StandardForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StandardForm standardForm = db.StandardForms.Find(id);
            if (standardForm == null)
            {
                return HttpNotFound();
            }
            return View(standardForm);
        }

        // POST: StandardForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InterestGood,RepaySchedGood,PriceShock,StepsTaken,AdjustModeOfIncome,BuisnessImped,ChangeBuisnessModel,LessThan90Days,FormalSavings,InformalSavings,CapitalInvest,Freedom,Seasonal,HowSoSeasonal,WhatLargerLoanFor,FamilyDoingWell,ProblemsToChange,HowCanWeHelp,ClientId,UserId,LoanFor,StartTime,EndTime,Savings,Business,TypeOfWork,Misc")] StandardForm standardForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(standardForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(standardForm);
        }

        // GET: StandardForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StandardForm standardForm = db.StandardForms.Find(id);
            if (standardForm == null)
            {
                return HttpNotFound();
            }
            return View(standardForm);
        }

        // POST: StandardForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StandardForm standardForm = db.StandardForms.Find(id);
            db.StandardForms.Remove(standardForm);
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
