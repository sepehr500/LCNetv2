using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCNetv5.Models
{

    public enum LoanFor
    {
        BasicNecessitites,
        School,
        Debts,
        OtherLoans,
        Land,
        Business,
        Meds
    }
    public  abstract class FormHeading
    {

        public int Id { get; set; }

        protected FormHeading()
        {
            Date = DateTime.Now;
        }
        public virtual Client Client { get; set; }

        public virtual ApplicationUser User { get; set; }

        protected DateTime Date { get; set; }

        protected DateTime StartTime { get; set; }
        protected DateTime EndTime { get; set; }

        protected bool FinancialExp { get; set; }

        protected LoanFor LoanFor { get; set; }

        protected bool Savings { get; set; }

        public bool Business { get; set; }

        public string TypeOfWork { get; set; }

        public string Misc { get; set; }


    }
}
