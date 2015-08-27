using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
   public enum Frequency
    {
        Weekly,
        Biweekly,
        Monthly
    }

    public enum TimePeriod
    {
        Days,
        Months,
        Weeks,
        Years
    }
    public class Loan
    {
         public Loan()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public int Id { get; set; }
        [Display(Name = "Loan Amount Is")]
        public decimal AmtLoan { get; set; }
        
        [Display(Name = "Loan Start Date")]
        [DataType(DataType.Date)]
        public System.DateTime TransferDate { get; set; }

        public bool Active { get; set; }

        public int Round { get; set; }

        public float InterestRate { get; set; }

        [Display(Name = "Repaied every")]
        public int Frequency { get; set; }

        public TimePeriod TimePeriod { get; set; }

        [Display(Name = "Over how many insatllments?")]
        public int Instalments { get; set; }

        public int ProgramId { get; set; }
    
        public virtual ICollection<Payment> Payments { get; set; }

        public virtual Program Program { get; set; }
        public string getInfo()
        {
            return this.Program.getInfo() + " " + this.AmtLoan + " Round " + Round;
        }
    }
}