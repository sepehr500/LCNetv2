using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            
            StartTime = DateTime.Now;
        }
        
        public string UserId { get; set; }  
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "What are you using your loan for? ¿Para que esta usando su prestamo?")]
       
        public LoanFor LoanFor { get; set; }  

        [Display(Name = "Call Start time")]
        [DataType(DataType.Date)]
        public  DateTime StartTime { get; set; }
        [Display(Name = "Call end time")]
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }


        [Display(Name = "Do you use any form of savings in your house, the house of someone you know or in a bank? ¿Utilizausted algún método de ahorrar o guardar dinero en su casa o un banco?")]
        public bool Savings { get; set; }

        [Display(Name = "Do you have a business? ¿Tiene un negocio?")]
        public bool Business { get; set; }

        [Display(Name = "What type of work do you do? ¿Que tipo de empleo tiene?")]
        public string TypeOfWork { get; set; }

        [Display(Name = "Write comments here.")]
        [DataType(DataType.MultilineText)]
        public string Misc { get; set; }


    }
}
