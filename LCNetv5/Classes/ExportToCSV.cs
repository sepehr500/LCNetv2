using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LCNetv5.Models;

namespace LCNetv5.Classes
{


    public class ExportToCSV
    {



        public static string ExportCSV(string TableName)
        {
            var constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Clients inner join Programs on Clients.Id = Programs.ClientId inner join Loans on Programs.Id = Loans.ProgramId left outer join Payments on Loans.Id = Payments.LoanId"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            var LastOneInList = dt.Columns[dt.Columns.Count - 1];
                            foreach (DataColumn column in dt.Columns)
                            {
                                if (column.ColumnName != "Narrative")
                                {


                                    if (column == LastOneInList)
                                    {
                                        csv += column.ColumnName;

                                    }
                                    else
                                    {

                                        //Add the Header row for CSV file.
                                        csv += column.ColumnName + ',';
                                    }
                                }
                            }

                            //Add new line.
                            csv += "\r\n";
                            
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    if (column.ColumnName != "Narrative")
                                    {
                                        var AnotherOneInList = dt.Columns[dt.Columns.Count - 1];
                                        if (AnotherOneInList == column)
                                        {

                                            csv += row[column.ColumnName].ToString().Replace(",", ";");
                                        }
                                        else
                                        {

                                            //Add the Data rows.
                                            csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                        } 
                                    }
                                    
                                }

                                //Add new line.
                                csv += "\r\n";
                            }
                            return csv;
                        }
                    
                    }
                }

            }
        }
    }
}