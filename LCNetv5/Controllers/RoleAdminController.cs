using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Hosting;
using System.Web.Services;
using Antlr.Runtime.Tree;
using LCNetv5.Classes;
using LCNetv5.Models;
using Twilio;


namespace LCNetv5.Controllers
{

    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {

        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result
                    = await RoleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Role Not Found" });
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            AppRole role = await RoleManager.FindByIdAsync(id);
            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();
            IEnumerable<ApplicationUser> members
                = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            IEnumerable<ApplicationUser> nonMembers = UserManager.Users.Except(members);
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = await UserManager.AddToRoleAsync(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await UserManager.RemoveFromRoleAsync(userId,
                        model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Error", new string[] { "Role Not Found" });
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }


        public ActionResult SendSMS()
        {
            var db = new ApplicationDbContext();
            var returnlist = db.Clients.Where(x => x.PhoneNumber != null).ToList();
            return View(returnlist);
        }
        [HttpPost]
        public ActionResult ReceiveSMS( string[] Numbers , string SendToAll , string Message)
        {
            string AccountSid = "AC31d7b9ac457d5a0c6675be668067de6f";
            string AuthToken = "b33a599c894b14f5943216b0b4943b10";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            
            var CountryCode = "+504";
            foreach (var number in Numbers.ToList())
            {
            var  NewNumber = number.Replace(" ",string.Empty);
            var message = twilio.SendMessage("+14343814668",  CountryCode + NewNumber , Message);
               

            }

            return Json("DONE");

        }

        public ActionResult DownloadLoans()
        {
           var csv = ExportToCSV.ExportCSV("Clients");
           Response.Clear();

           string fileName = "Data From " + DateTime.Now.ToString("yyyy-MMM-dd-HHmmss") + ".csv";
           Response.ContentType = "application/vnd.ms-excel";
           Response.AddHeader("content-disposition", "filename=" + fileName);

           // write string data to Response.OutputStream here
           Response.Write(csv);

           Response.End();
           return null;
        }

        public ActionResult TestMultiselect()
        {
            return View();
        }


        //public FileResult Download()
        //{
        //var Excel = new Reports();
        //    //Writes the reservations to the txt file
        //Excel.ReservationsToExcel();
        //string path = HostingEnvironment.MapPath("~");
        //byte[] fileBytes = System.IO.File.ReadAllBytes(path + "/Reservations.txt");
        //string fileName = "Reservations.csv";
        //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        //}


        
    }
}
