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
    public class EntryFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntryForms
        public ActionResult Index()
        {
            return View(db.EntryForms.ToList());
        }

        // GET: EntryForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryForm entryForm = db.EntryForms.Find(id);
            if (entryForm == null)
            {
                return HttpNotFound();
            }
            return View(entryForm);
        }

        // GET: EntryForms/Create
        public ActionResult Create(int ClientId)
        {
            Session["clientId"] = ClientId;
            return View();
        }

        // POST: EntryForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HowFindOut,NumberInHousehold,HomeOwner,PayRent,AmtRent,AmountOfTimeInCommunity,WhereLiveBefore,WhyMove,TransportTypes,FinanceExperience,FirstLoanFor,Business,TypeOfWork,Misc")] EntryForm entryForm)
        {
            if (ModelState.IsValid)
            {
                entryForm.ClientId = (int)Session["clientId"];
                entryForm.UserId = User.Identity.GetUserId();
                db.EntryForms.Add(entryForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entryForm);
        }

        // GET: EntryForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryForm entryForm = db.EntryForms.Find(id);
            if (entryForm == null)
            {
                return HttpNotFound();
            }
            return View(entryForm);
        }

        // POST: EntryForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HowFindOut,NumberInHousehold,HomeOwner,PayRent,AmtRent,AmountOfTimeInCommunity,WhereLiveBefore,WhyMove,TransportTypes,FinanceExperience,FirstLoanFor,Business,TypeOfWork,Misc")] EntryForm entryForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entryForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entryForm);
        }

        // GET: EntryForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryForm entryForm = db.EntryForms.Find(id);
            if (entryForm == null)
            {
                return HttpNotFound();
            }
            return View(entryForm);
        }

        // POST: EntryForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntryForm entryForm = db.EntryForms.Find(id);
            db.EntryForms.Remove(entryForm);
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
