using LCNetv5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public class PaymentChange : Log 
    {
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        public override string getDeets()
        {
            return this.getUserandChange() + Payment.Loan.getInfo() + " payment Id is " + Payment.Id;
        }
    }
}