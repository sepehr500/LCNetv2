﻿using LCNetv5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCNetv5.Classes
{
    public static class SpanDate
    {
        public static string SpanishDate(DateTime date)
        {

            string monthSpanish = "";
            switch (date.Month)
            {
                case 1:
                    monthSpanish = "Enero";
                    break;
                case 2:
                    monthSpanish = "Febrero";
                    break;
                case 3:
                    monthSpanish = "Marzo";
                    break;
                case 4:
                    monthSpanish = "Abril";
                    break;

                case 5:
                    monthSpanish = "Mayo";
                    break;

                case 6:
                    monthSpanish = "Junio";

                    break;
                case 7:
                    monthSpanish = "Julio";
                    break;
                case 8:
                    monthSpanish = "Agosto";
                    break;
                case 9:
                    monthSpanish = "Septiembre";
                    break;
                case 10:
                    monthSpanish = "Octubre";
                    break;
                case 11:
                    monthSpanish = "Noviembre";
                    break;
                case 12:
                    monthSpanish = "Diciembre";
                    break;

            }

            return String.Format("{0} de {1} de {2}", date.Day, monthSpanish, date.Year);


        }
        //Checks for duplicate names. Return false if duplicate is found.
        public static bool DupCheck(this Client orig)
        {

             ApplicationDbContext db = new ApplicationDbContext();
            var name = orig.FirstName + orig.MiddleName1 + orig.MiddleName2 + orig.LastName;
            string CheckName = "";

            foreach (var item in db.Clients)
            {

                item.MiddleName1 = item.MiddleName1 ?? " ";
                item.MiddleName2 = item.MiddleName2 ?? " ";
                CheckName = item.FirstName.TrimEnd() + item.MiddleName1.TrimEnd() + item.MiddleName2.TrimEnd() + item.LastName.TrimEnd();
                if (CheckName.Equals(name))
                {
                    return false;
                }
            }

            return true;


        }
        public static int? getRecentLoan( this Client x)
        {


            try
            {
                if (x.Programs.Last().Loans.OrderBy(z => z.TransferDate).Last() != null)
                {
                    return x.Programs.ToList().Last().Loans.ToList().OrderBy(z => z.TransferDate).ToList().Last().Id;
                }
                else
                {
                    return null;
                }
            }
            catch (InvalidOperationException excep)
            {

                return null;
            }





        }
    }
}