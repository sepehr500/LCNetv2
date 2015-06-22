using LCNetv5.Classes;
using LCNetv5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCNetv5.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var UserLog = new List<Log>();
            UserLog.AddRange(db.ClientChanges.ToList());
            UserLog.AddRange(db.PaymentChanges.ToList());
            UserLog.AddRange(db.LoanChanges.ToList());
            UserLog.OrderBy(x => x.Date);
             
            
           
            return View(UserLog);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}