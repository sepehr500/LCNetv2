using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LCNetv5.Models;

namespace LCNetv5.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        public ActionResult Index()
        {
           
            return View(db.Clients.ToList().OrderBy(x => x.DateAdded));
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin , Loans")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Loans")]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Center,MiddleName1,MiddleName2,BirthDay,LegacyScore, PhoneNumber, Narrative")] Client client)
        {
            client.DateAdded = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.ClientChanges.Add(new ClientChange(client){UserId = User.Identity.GetUserId() ,ChangeType = Change.Created });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin, Loans")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Loans")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Center,MiddleName1,MiddleName2,DateAdded,BirthDay,LegacyScore,Status, PhoneNumber, Narrative")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.ClientChanges.Add(new ClientChange(client) { UserId = User.Identity.GetUserId(), ChangeType = Change.Modified});
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin,Loans")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Loans")]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            var deleteclient = new ClientChange(client)
            {
                UserId = User.Identity.GetUserId(),
                ChangeType = Change.Deleted,
                         };
            db.ClientChanges.Add(deleteclient);
            db.Clients.Remove(client);
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

        public ActionResult History(int Id){
            var loans = db.Loans.Where(x => x.Program.ClientId == Id ).ToList();
            return View(loans);
        }

    }
}
