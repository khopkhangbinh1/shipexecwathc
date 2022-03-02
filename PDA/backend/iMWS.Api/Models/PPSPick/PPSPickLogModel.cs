using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSPick
{
    public class PPSPickLogModel
    {
        public string shipmentId { get; set; }
        public string empNo { get; set; }
        public string palletNo { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int qty { get; set; }
        public int dealQty { get; set; }
    }
}