using LCNetv5.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using RestSharp.Extensions;

namespace LCNetv5.Models
{
    public enum Change
    {
        Deleted,
        Modified,
        Created
    }
    public class ClientChange : Log 
    {
        public ClientChange()
        {
            
        }
        public ClientChange(Client client)
        {
            Info = client.getFullName();
        }


      
    }
}