using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCNetv5.Models
{
    public enum HowFind
    {
        Friend,
        Neighbor,
        Other
    }

    public enum TransportTypes
    {
        Bike,
        Moto,
        Car,
        Bus,
        Other
    }

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

        public string UserId { get; set; }

        public int ClientId { get; set; }

        protected FormHeading()
        {
            Date = DateTime.Now;
        }
        public virtual Client Client { get; set; }

        public virtual ApplicationUser User { get; set; }

        protected DateTime Date { get; set; }

        protected DateTime StartTime { get; set; }
        protected DateTime EndTime { get; set; }

        [Display(Name = "Do you have any experience with a financial institution?  ¿Tiene experiencia con alguna organización financeria?")]
        protected bool FinancialExp { get; set; }
        [Display(Name = "What are you using your loan for?  ¿Para queesta usando su primer préstamo? ")]
        protected LoanFor LoanFor { get; set; }
        [Display(Name ="Do you use any form of savings in your house, the house of someone you know or in a bank? ¿Utilizausted algún método de ahorrar o guardar dinero en su casa o un banco?" )]
        protected bool Savings { get; set; }
        [Display(Name = "Do you have a business?")]
        public bool Business { get; set; }
        [Display(Name = "What type of work do you do?")]
        public string TypeOfWork { get; set; }
        [Display(Name = "Other relevent information")]
        public string Misc { get; set; }


    }
}
