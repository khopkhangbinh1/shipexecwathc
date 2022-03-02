using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.DataGateWay
{
    public class StatusData
    {
        public string TROLLEY_LINE_NO { get; set; }

        public int MAXQTY { get; set; }

        public int USEDQTY { get; set; }
    }
}