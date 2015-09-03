using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCNetv5.Models
{
    public class StandardForm : FormHeading
    {



        public int Id { get; set; }

        public bool InterestGood { get; set; }

        public bool RepaySchedGood { get; set; }

        //LATE ON REPAYMENT

        public bool? PriceShock { get; set; }

        public string StepsTaken { get; set; }

        public bool? AdjustModeOfIncome { get; set; }

        public bool? BuisnessImped { get; set; }

        public bool? ChangeBuisnessModel { get; set; }

        public bool? LessThan90Days { get; set; }

        //END

        public bool FormalSavings { get; set; }

        public bool InformalSavings { get; set; }

        public bool CapitalInvest { get; set; }

        public bool Freedom { get; set; }

        
        //If they have a Business

        public bool? Seasonal { get; set; }

        public string HowSoSeasonal { get; set; }

        public string WhatLargerLoanFor { get; set; }

        public bool? FamilyDoingWell { get; set; }





        //End

        public string ProblemsToChange { get; set; }

        public string HowCanWeHelp { get; set; }


    }
}
