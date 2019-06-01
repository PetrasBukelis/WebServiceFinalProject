using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Bets4You_Rest_Webservice.Models
{
    [DataContract]
    public class BetInfo
    {
        private string BetName { get; set; }
        private string Category { get; set; }
        private string Date { get; set; }
        private double Coefficient { get; set; }
        private string SubmitorName { get; set; }
    }
}