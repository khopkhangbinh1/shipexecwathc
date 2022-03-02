using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSCheck
{
    public class CheckCNTModel
    {
        public string IsFirst { get; set; }
        public string locationNo { get; set; }
        public string locationID { get; set; }
        public string empNo { get; set; }
        public string palletCartonQTY { get; set; }
        public string CTN { get; set; }
        public string CTN2 { get; set; }
        public string checkHold { get; set; }
        public string checkCTN { get; set; }
        public string checkCNTPllet { get; set; }
    }
}