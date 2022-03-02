using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSPick
{
    public class PickTaskItemViewModel
    {
        public string ICTPN { get; set; }
        public int QTY { get; set; }
        public int CTNQty { get; set; }
        public string PICKCTN { get; set; }
        public string RESTCTN { get; set; }
    }
}