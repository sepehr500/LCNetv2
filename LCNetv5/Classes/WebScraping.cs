using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace LCNetv5.Classes
{
    public static class WebScraping
    {
        public static double HNInterestRate()
        {
           var webget = new HtmlWeb();
           var page = webget.Load("http://54.83.44.220/honduras/inflation-cpi");
           var inflate =  page.GetElementbyId("ctl00_ContentPlaceHolder1_ctl00_IndicatorSummaryUC_PanelDefinition").ChildNodes[1]
                .ChildNodes[1].ChildNodes[3].ChildNodes[3].InnerHtml;
            return Convert.ToDouble(inflate);
        }
    }
}
