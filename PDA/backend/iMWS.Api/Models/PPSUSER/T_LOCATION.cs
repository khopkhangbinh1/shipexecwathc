using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSUSER
{
    public class T_LOCATION
    {
        public int LOCATION_ID { get; set; }

        public string LOCATION_NO { get; set; }

        public string LOCATION_NAME { get; set; }

        public string PALLET_NO { get; set; }

        public int? QTY { get; set; }

        public int? CARTONQTY { get; set; }

        public string PACK_CODE { get; set; }

        public string STATUS { get; set; }

        public string TYPE { get; set; }

        public Decimal? WAREHOUSE_ID { get; set; }

        public DateTime? UDT { get; set; }

        public int? QHCARTONQTY { get; set; }

        public int? QHQTY { get; set; }

        public string PART_NO { get; set; }
    }
}