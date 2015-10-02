using LCNetv5.Classes;
using LCNetv5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;


namespace LCNetv5.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            //string AccountSid = "AC31d7b9ac457d5a0c6675be668067de6f";
            //string AuthToken = "b33a599c894b14f5943216b0b4943b10";
            //var twilio = new TwilioRestClient(AccountSid, AuthToken);
            
            //var message = twilio.SendMessage("+14049947137", "+50432332987" ,"LA CEIBA MFI SENT THIS"); 
            var UserLog = new List<Log>();
            UserLog.AddRange(db.ClientChanges.ToList());
            UserLog.AddRange(db.PaymentChanges.ToList());
            UserLog.AddRange(db.LoanChanges.ToList());
           List<Log> sortedlog = UserLog.OrderByDescending(x => x.Date.Date).ThenByDescending(z => z.Date.TimeOfDay).Take(10).ToList();
             
            
           
            return View(sortedlog);
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