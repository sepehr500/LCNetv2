﻿using LCNetv5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Classes
{
    public class PaymentPlan
    {
        public string Term { get; set; }
        public string Often { get; set; }

        public PaymentPlan()
        {
            this.Payments = new List<Payment>();
        }
        public List<Payment> Payments { get; set; }
        public decimal getTotalPrincipal()
        {
            return Payments.Sum(x => x.Principal);
        }
        public decimal getTotalInterest()
        {
            return Payments.Sum(x => x.Interest);
        }
        public decimal getTotalPaymentDue()
        {
            return Payments.Sum(x => x.Principal) + Payments.Sum(x => x.Interest);
        }
        /// <summary>
        /// Fills the payments property with a list of payments that represents the payment
        /// plan.
        /// </summary>
        /// <param name="loan"></param>
        public void CreatePaymentPlan(Loan loan)
        {
            //Check if Create Payment Plan is Null
            double IR = 0;
            try
            {
                IR = loan.InterestRate / 100d;
            }
            catch (NullReferenceException x)
            {
                IR = loan.InterestRate / 100d;

            }



            DateTime temp = loan.TransferDate;
            //Total the client has paid
            decimal PaymentTotal = (decimal)loan.Payments.Sum(x => x.AmtPaid);
            double PDLength = 0;
            int days = 0;
            bool month = false;
            decimal Balance = loan.AmtLoan;
            var num = 0;
            switch (loan.TimePeriod)
            {
                case TimePeriod.Weeks:
                    num = loan.Frequency*7;
                    
                    break;
                case TimePeriod.Months:
                    num = loan.Frequency * 30;
                    
                    break;
               case TimePeriod.Days: 
                    num = loan.Frequency;
                    break;

            }
            PDLength = num/365d;
            //Interest Rate per Period
            decimal IRPP = (decimal)IR * (decimal)PDLength;
            //Calculate EMI
            decimal EMI = IRPP * loan.AmtLoan / (decimal)(1 - (Math.Pow(1 + (double)IRPP, (double)loan.Instalments * -1)));

            //decimal rounded = decimal.Round(EMI , 2);
            EMI = decimal.Round(EMI, 2, MidpointRounding.AwayFromZero);

            for (int i = 1; i <= loan.Instalments; i++)
            {
                Payment y = new Payment();
                y.PaymentDue = decimal.Round(EMI, 2);
                y.Installment = i;
                y.Interest = decimal.Round(Balance * IRPP, 2);
                y.Principal = y.PaymentDue - y.Interest;
                Balance = Balance - y.Principal;
                y.Balance = Balance;
                //if on the last installment there is money left over because of rounding, add it to the final principal. 
                if (i == loan.Instalments && y.Balance != 0)
                {
                    y.Principal += Balance;
                    y.Balance -= Balance;
                }
                y.PaymentDue = y.Principal + y.Interest;

                //Compute Due Dates
                if (loan.TimePeriod == TimePeriod.Months)
                {
                    y.DateDue = temp.AddMonths(loan.Frequency);
                }
                else
                {
                    y.DateDue = temp.AddDays(days);
                    //if (y.DateDue.DayOfWeek == DayOfWeek.Sunday)
                    //{
                    //     y.DateDue = y.DateDue.AddDays(1);
                    //}
                }
                temp = y.DateDue;



                Payments.Add(y);



            }
            switch (loan.TimePeriod)
            {
                case TimePeriod.Months:
                    this.Often = "Months";
                    this.Term = (loan.Instalments * loan.Frequency)  + " " + "Months";
                    break;
                case TimePeriod.Days:

                    this.Often= "Days";
                    this.Term = (loan.Instalments * loan.Frequency) + " " + "Days";
                    break;
                case TimePeriod.Weeks:

                    this.Often= "Weeks";
                    this.Term = (loan.Instalments * loan.Frequency) + " " + "Weeks";
                    break;
            }

        }
        public void applyPayments(decimal TotalPaid)
        {


            foreach (var item in this.Payments)
            {
                if (TotalPaid >= item.PaymentDue)
                {
                    TotalPaid -= (decimal)item.PaymentDue;
                    item.AmtDue = 0;
                }
                else
                {
                    item.AmtDue = item.PaymentDue -= TotalPaid;
                    TotalPaid = 0;
                }
                if (item.AmtDue != 0)
                {


                    if (0 > item.DateDue.AddMonths(3).CompareTo(DateTime.Now))
                    {
                        item.status = 3;
                    }
                    else if (0 > item.DateDue.CompareTo(DateTime.Now))
                    {
                        item.status = 2;
                    }
                    else
                    {
                        item.status = 0;
                    }

                }
                else
                {
                    item.status = 1;
                }

                item.AmtDue = decimal.Round(item.AmtDue, 2);



            }
        }
    }

    public class Payment
    {
        public int Installment { get; set; }
        public DateTime DateDue { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal PaymentDue { get; set; }

        //public DateTime DatePaid { get; set; }
        public decimal Balance { get; set; }
        //Keep track of how much as been payed off toward a payment
        public decimal AmtDue { get; set; }

        public int status { get; set; }

    }



}