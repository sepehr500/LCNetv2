using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Models
{
    public enum ProgramName
    {
        PLP,
        EAL
    }
    public class Program
    {

        public Program()
         {
            this.Loans = new HashSet<Loan>();
        }
    
        public int Id { get; set; }

        public ProgramName ProgName { get; set; }

        public int ClientId { get; set; }
    
        public virtual ICollection<Loan> Loans { get; set; }

        public virtual Client Client { get; set; }

        public string getInfo()
        {
            return this.Client.getFullName() + " " +  this.ProgName;
        }
    }
}