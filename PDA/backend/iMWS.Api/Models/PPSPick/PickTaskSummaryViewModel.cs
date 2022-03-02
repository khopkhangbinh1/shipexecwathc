using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSPick
{
    public class PickTaskSummaryViewModel
    {
        public string ShipmentID { get; set; }
        public string PalletNo { get; set; }
        public string carrierName { get; set; }
        public string Region { get; set; }
        public string ShipmentType { get; set; }
        public string ShipType { get; set; }
        public string pickStatus { get; set; }
        public string PalletType { get; set; }


    }
}