using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSUSER
{
    public class T_TROLLEY_MPART_SN
    {
        public string CUSTOM_SN { get; set; }

        public string TROLLEY_NO { get; set; }

        public string PALLET_NO { get; set; }

        public string SIDES_NO { get; set; }

        public int? LEVEL_NO { get; set; }

        public int? SEQ_NO { get; set; }

        public int? POINTNO { get; set; }

        public DateTime? PACKCDT { get; set; }

        public DateTime? CDT { get; set; }

        public DateTime? UDT { get; set; }

        public int? EMP_ID { get; set; }

        public string DELIVERY_NO { get; set; }

        public string LINE_ITEM { get; set; }

        public string ICTPARTNO { get; set; }

        public string CARTON_NO { get; set; }

        public string TROLLEY_LINE_NO { get; set; }

        public string DNPALLET { get; set; }

    }
}