using LCNetv5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public abstract class Log
    {
        public Log()
        {
            this.Date = DateTime.Now;
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public Change ChangeType { get; set; }
        public DateTime Date { get; set; }

        public virtual ApplicationUser User { get; set; }

        public abstract string getDeets();

        public string getUserandChange()
        {
            
            return this.Date +"- " + this.User.UserName + " " + this.ChangeType + " ";
        }

    }
}