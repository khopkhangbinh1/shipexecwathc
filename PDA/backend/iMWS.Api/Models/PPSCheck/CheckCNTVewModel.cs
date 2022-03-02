using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSCheck
{
    public class CheckCNTVewModel
    {
        public string id { get; set; }
        public string IsFirst { get; set; }
        public string locationID { get; set; }

        public string name { get; set; }
        public string empNo { get; set; }
        public string palletNo { get; set; }
        public string CTNQty { get; set; }
        public string cartonQTY { get; set; }
        public string Qty { get; set; }
        public string QHCaton { get; set; }
        public string QHQty { get; set; }
        public string locationNo { get; set; }
        public string deliveryNo { get; set; }
        public string ICTPN { get; set; }
        public string dateTime { get; set; }
        public string checkTime { get; set; }
        public string CTNSN { get; set; }
        public string result { get; set; }
        public string passcartonqty { get; set; }
        public string errorcartonqty { get; set; }
        public string ErrMsg { get; set; }
        public string TEMPNAME { get; set; }
    }
}