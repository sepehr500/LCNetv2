using LCNetv5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public class PaymentChange : Log 
    {
        public PaymentChange()
        {
            
        }
        public PaymentChange(Payment payment)
        {
            Info =  payment.Loan.getInfo() + " payment Id is " + payment.Id;
        }
     
    }
}