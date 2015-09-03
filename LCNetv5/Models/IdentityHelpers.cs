using System.Web;
using System.Web.Mvc;
using LCNetv5;
using Microsoft.AspNet.Identity.Owin;

namespace LCNetv5.Models
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            ApplicationUserManager mgr
                = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }
    }
}
