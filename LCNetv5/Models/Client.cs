using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LCNetv5.Classes;

namespace LCNetv5.Models
{
    public enum Center
    {
        EC,
        LB,
        MO,
        PE,
        VS
    }

    public enum Status
    {
        Active,
        Inactive,
        Exited
    }
    public class Client
    {

        public Client()
        {
            this.Programs = new HashSet<Program>();
        }

        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Father Surname")]
        public string LastName { get; set; }

        public Center Center { get; set; }

        [Display(Name = "Middle Name (opt)")]
        public string MiddleName1 { get; set; }
        [Display(Name = "Mother Surname(opt)")]
        public string MiddleName2 { get; set; }

        public System.DateTime DateAdded { get; set; }
        [Display(Name = "Birth Day")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> BirthDay { get; set; }

        [Display(Name = "Legacy Score(%)")]
        public Nullable<int> LegacyScore { get; set; }
        public Status Status { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.MultilineText)]
        public string Narrative { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<EntryForm> EntryForms { get; set; } 
        public virtual ICollection<StandardForm> StandardForms { get; set; } 
        public string getFullName()
        {
            return String.Format("{0} {1} {2} {3}", this.FirstName, this.MiddleName1, this.MiddleName2, this.LastName);
        }
        /// <summary>
        /// Gets the numerator of the credit score.
        /// </summary>
        /// <returns>The numerator of the credit score.</returns>
        private int getNum()
        {

            int Num = 0;
            using (var db = new ApplicationDbContext())
            {
                foreach (var y in db.Payments.Where(x => x.Loan.Program.ClientId == Id).ToList())
                {
                    DateTime a = y.DatePmtPaid ?? DateTime.Now;
                    TimeSpan z = a - y.DatePmtDue;
                    if (z.Days <= 9)
                    {
                        Num += 10;
                    }
                    if (z.Days >= 10 && z.Days <= 19)
                    {
                        Num += 9;
                    }
                    if (z.Days >= 20 && z.Days <= 29)
                    {
                        Num += 8;
                    }
                    if (z.Days >= 30 && z.Days <= 39)
                    {
                        Num += 7;
                    }
                    if (z.Days >= 40 && z.Days <= 49)
                    {
                        Num += 6;
                    }
                    if (z.Days >= 50 && z.Days <= 59)
                    {
                        Num += 5;
                    }
                    if (z.Days >= 60 && z.Days <= 69)
                    {
                        Num += 4;
                    }
                    if (z.Days >= 70 && z.Days <= 79)
                    {
                        Num += 3;
                    }
                    if (z.Days >= 80 && z.Days <= 89)
                    {
                        Num += 2;
                    }
                    if (z.Days >= 90)
                    {
                        Num += 1;
                    }


                }
            }
            return Num;
        }
        /// <summary>
        /// Gets the denomenator of the credit score.(The number of payments * 10)
        /// </summary>
        /// <returns>The denomenator</returns>
        private int getPaymentCount()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Payments.Where(x => x.Loan.Program.ClientId == Id).ToList().Count() * 10;

            }
        }

        /// <summary>
        /// Gets the credit score for a client. Multiply by 100 do turn into a percentage.
        /// </summary>
        /// <returns>Credit Score</returns>
        public float getCScore()
        {
            if (this.LegacyScore != null)
            {
                var num = getNum();
                var dem = getPaymentCount();
                try
                {
                var total = num/dem;
                return (float) ((total + (float)this.LegacyScore) / 2 * .01);

                }
                catch (Exception)
                {
                    return (float) ((float)this.LegacyScore * .01);

                }
            }
            return ((float)this.getNum() / (float)this.getPaymentCount());

        }



    }
}