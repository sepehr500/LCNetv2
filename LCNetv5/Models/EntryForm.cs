using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCNetv5.Models
{
   public  enum HowFind
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




    public class EntryForm : FormHeading
    {


        public int ClientId { get; set; }
        public virtual Client Client { get; set; }


        [Display(Name = "How did the client find out about LC? ¿Comó llegó usted a conocer la organización?")]
        public HowFind HowFindOut { get; set; }
        [Display(Name = "Can you give me info about the number of people who live with you? ¿Me puede darinformación acerca de su hogar y las personas que viven con usted?")]
        public int NumberInHousehold { get; set; }

        [Display(Name = "Are you the owner of the home?¿Usted es dueño/a de sucasa? ")]
        public bool HomeOwner { get; set; }
        [Display(Name = "Do you pay rent? ¿Paga renta en su casa?")]
        public bool  PayRent { get; set; }


        [Display(Name = "How long have you lived in the community? ¿Cuánto tiempo ha vivido en la comunidad?")]
        public int AmountOfTimeInCommunity { get; set; }

        [Display(Name = "Where did you live before? ¿Dónde vivía antes de mudarse?")]
        public string WhereLiveBefore { get; set; }
        [Display(Name = "Why did you move? ¿Por qué se mudó?")]
        public string WhyMove { get; set; }
        [Display(Name = "What type of transportation do you use? ¿Que tipo de transportación usa? (Ej: autobus, bicicleta,moto, etc.)")]
        public TransportTypes TransportTypes  { get; set; }
        [Display(Name = "Do you have experieince with a bank? ¿Tienes experiencia con los bancos? (ej. un prestamo con un banco)  ")]
        public bool FinanceExperience { get; set; }












        



    }
}
