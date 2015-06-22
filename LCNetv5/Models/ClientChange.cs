using LCNetv5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public enum Change
    {
        Deleted,
        Modified,
        Created
    }
    public class ClientChange : Log 
    {
        
        public int ClientId { get; set; }
        
        public virtual Client Client { get; set; }



        public override string getDeets()
        {
            return this.getUserandChange() + Client.getFullName();
        }
    }
}