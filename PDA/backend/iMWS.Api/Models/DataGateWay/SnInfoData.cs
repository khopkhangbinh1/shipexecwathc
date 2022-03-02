using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.DataGateWay
{
    public class SnInfoData
    {
		public string CUSTOMER_SN { get; set; }

		public string CARTON_NO { get; set; }

		public string PART_NO { get; set; }

		public string PALLET_NO { get; set; }

		public int? PACKQTY { get; set; }

		public string WC { get; set; }

		public string PARTTYPE { get; set; }

		public int? TOTAL { get; set; }
	}
}