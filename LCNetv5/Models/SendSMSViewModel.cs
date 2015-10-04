using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public class SendSMSViewModel
    {
        public List<string> Clients { get; set; }

        public bool SendToAll { get; set; }


    }
}