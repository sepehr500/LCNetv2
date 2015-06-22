using LCNetv5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public class LoanChange : Log 
    {
        public int LoanId { get; set; }
        public virtual Loan Loan { get; set; }

        public override string getDeets()
        {
            return this.getUserandChange() + Loan.getInfo();
        }
    }
}