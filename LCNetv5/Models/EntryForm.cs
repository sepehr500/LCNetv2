using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCNetv5.Models
{




    
    public class EntryForm : FormHeading
    {
        public HowFind HowFindOut { get; set; }

        public int NumberInHousehold { get; set; }

        public bool HomeOwner { get; set; }
        public bool  PayRent { get; set; }
        public int? AmtRent   { get; set; }
        

        public int AmountOfTimeInCommunity { get; set; }

        public string WhereLiveBefore { get; set; }
        public string WhyMove { get; set; }
        public TransportTypes TransportTypes  { get; set; }

        public bool FinanceExperience { get; set; }

        public LoanFor FirstLoanFor { get; set; }










        



    }
}
