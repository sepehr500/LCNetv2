using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LCNetv5.Classes;

namespace LCNetv5.Models
{
    public class StandardForm : FormHeading 
    {



        public int Id { get; set; }

        [Display(Name = "Is the interest rate manageable?¿Es la taza de interés manejable yapropiado para usted?")]
        public bool InterestGood { get; set; }

        [Display(Name = "Is the repayment schedule good?¿Esta cómoda con le frecuencia de pagos?")]
        public bool RepaySchedGood { get; set; }

        public int? LoanId { get; set; }

        public virtual Loan Loan {get; set; }



        //LATE ON REPAYMENT

        [Display(Name = "(If Late) Was there a sudden change in the price of basic commodities available to the client? ¿Hubo uncambio inesperado en los precios de necesidades básicos, como alimentos, transportación, ropa, etc.?")]
        public bool? PriceShock { get; set; }

        [Display(Name = "(If Late) What steps is the client taking to pay back the loan? ¿Que pasos está tomando para pagar elpréstamo?")]
        public string StepsTaken { get; set; }

        [Display(Name = "(If Late) Is the client adjusting thier modes of income? ¿Está ajustando sus métodos de ganancia? (buscandotrabajo, pidiendo ayuda de familia o amigos, etc.)")]
        public bool? AdjustModeOfIncome { get; set; }

        [Display(Name = "(If Late) Are payments late because of buisness impediments? ¿Ha atrasado en sus pagos a causa de su negocio?")]
        public bool? BuisnessImped { get; set; }

        [Display(Name = "(If Late) Is the client changing their business model? ¿Ha cambiado su modelo de negocio?")]
        public bool? ChangeBuisnessModel { get; set; }

        [Display(Name = "(If Late) Does the client feel that they will be able to repay their loan less than 90 days late? ¿Usted cree queva a poder pagar su préstamo menos de 90 días tarde?")]
        public bool? LessThan90Days { get; set; }

        //END


        [Display(Name = "Is the client currently borrowing from any other informal (family, friends, neighbors) or formal(banks, another MFI, etc.) creditors to pay your loan?¿Estáusando un prestamo de algún otro financiero formal o informal para pagar su prestamo?")]
        public bool InformalSavings { get; set; }

        [Display(Name = "Is the client investing in capital or assets? ¿Está invirtiendo en recursos y/o propiedad?")]
        public bool CapitalInvest { get; set; }

        [Display(Name = "Does the client have freedom over how to spend the loan (or does the loan go into husbands/familyhands)? ¿Tiene usted la libertad sobre su préstamo, es decir, tiene poder sobre las decisiones del préstamo?")]
        public bool Freedom { get; set; }

        
        //If they have a Business

        [Display(Name = "(If Business) Does the service and products they provide change depending on season? ¿Cambia los productos/servicios de su negocio segun la temporada?")]
        public bool? Seasonal { get; set; }

        [Display(Name = "(If Business) How so? ¿En que forma cambian los productos/servicios?")]
        public string HowSoSeasonal { get; set; }

        [Display(Name = "(If Business) For what purpose will you use the next loan?  ¿Para qué cosa utilizará el próximo préstamo?")]
        public string WhatLargerLoanFor { get; set; }

        [Display(Name = "(If Business) How is your family doing?¿Como esta su familia? ¿")]
        public bool? FamilyDoingWell { get; set; }





        //End
        [Display(Name = "What problems do you wish you could change in Las Brisas/Villa? ¿Si usted pudiera cambiar algunas condiciones problemáticas en Las Brisas/Villa, El Progreso, o Honduras entero, cuáles serían? ¿Por ejemplo, si usted fueras el alcalde de la comunidad, que cosa cambiarías de la comunidad?")]
        public string ProblemsToChange { get; set; }
        [Display(Name = "How can La Ceiba help you improve your repayment? Educational courses, more notifications aboutmissed repayments and repayment record, greater access/communication with La Ceiba Staff.  ¿Cómo La Ceiba puede ayudarle mejorar su frecuencia de repago? Cursos educativos, más notificaciones sobre pagos perdidos y récord de repago, mejor acceso/comunicación al personal de la Ceiba.")]
        public string HowCanWeHelp { get; set; }
        [Display(Name = "How much is there current loan?")]
        public double? LoanAmt { get; set; }

}
    }

