using LCNetv5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public class LoanChange : Log 
    {
        public LoanChange() { }
        public LoanChange(Loan loan)
        {
            Info = loan.getInfo();
        }
    }
}