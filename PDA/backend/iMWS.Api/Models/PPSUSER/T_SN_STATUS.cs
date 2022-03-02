using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSUSER
{
    public class T_SN_STATUS
    {
        string SERIAL_NUMBER { get; set; }
        string CUSTOMER_SN { get; set; }
        string PALLET_NO { get; set; }
        string LOCATION_ID { get; set; }
        string LOCATION_NO { get; set; }
        string PART_NO { get; set; }
        string WC { get; set; }
        string DELIVERY_NO { get; set; }

        string LINE_ITEM { get; set; }
        string HAWB { get; set; }
        string SHIPMENT_ID { get; set; }
        string BOX_NO { get; set; }
        string PICK_PALLET_NO { get; set; }
        string PACK_PALLET_NO { get; set; }
        string SSCC { get; set; }
        string KNBOXNO { get; set; }

        string UUICODE { get; set; }
        string TRACKINGNO { get; set; }
        string BABY_TRACKING_NO { get; set; }

        string WORK_ORDER { get; set; }
        string COO { get; set; }
        string BATCH_NO { get; set; }



    }
}