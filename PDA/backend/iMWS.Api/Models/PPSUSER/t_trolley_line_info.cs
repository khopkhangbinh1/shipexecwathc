using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSUSER
{
    public class T_TROLLEY_LINE_INFO
    {
        public string TROLLEY_LINE_NO { get; set; }
        
        public string TROLLEY_NO { get; set; }
        
        public string SIDES_NO { get; set; }
        
        public int? LEVEL_NO { get; set; }
        
        public int? SEQ_NO { get; set; }

        public int? MAXQTY { get; set; }

        public int? ISENABLED { get; set; }

        public string LINE_NO { get; set; }

        public string GROUP_CODE { get; set; }

        public int? USEDQTY { get; set; }

        public DateTime? CDT { get; set; }

    }
}