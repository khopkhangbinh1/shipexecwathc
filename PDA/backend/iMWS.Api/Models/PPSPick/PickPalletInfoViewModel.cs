using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSPick
{
    public class PickPalletInfoViewModel
    {
        public string ShipmentId { get; set; }
        public string Carrier { get; set; }
        public string Priority { get; set; }
        public int DealQty { get; set; }
        public int Qty { get; set; }
        public string Packcodedesc { get; set; }
    }
}