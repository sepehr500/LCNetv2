using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Display(Name = "Date Payment Due")]
        [DataType(DataType.Date)]
        public System.DateTime DatePmtDue { get; set; }
        [Display(Name = "Date Paid")]
        [DataType(DataType.Date)]
        [Required]
        public Nullable<System.DateTime> DatePmtPaid { get; set; }
        [Display(Name = "Amount Paid")]
        public Nullable<decimal> AmtPaid { get; set; }

        public int LoanId { get; set; }

        public virtual Loan Loan { get; set; }
       
    }
}