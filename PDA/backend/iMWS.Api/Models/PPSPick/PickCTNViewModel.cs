using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSPick
{
    public class PickCTNViewModel
    {
        public string Palletid { get; set; }
        public string PickPalletNo { get; set; }
        public string ShipmentId { get; set; }
        public string CTNNO { get; set; }
        public string UUID { get; set; }
        public string empNo { get; set; }
    }
}