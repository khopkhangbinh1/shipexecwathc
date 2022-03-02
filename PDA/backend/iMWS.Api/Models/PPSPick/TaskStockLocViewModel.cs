using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSPick
{
    public class TaskStockLocViewModel
    {
        public string Area { get; set; }
        public string udt { get; set; }
        public string Loc { get; set; }
        public string ICTPN { get; set; }
        public int CTNQty { get; set; }
        public string LineNo { get; set; }
        public string partcount { get; set; }
        public string total { get; set; }
    }
}