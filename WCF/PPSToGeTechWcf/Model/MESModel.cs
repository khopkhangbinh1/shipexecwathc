using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{


    public class MESReturnModel
    {
        public string INSN { get; set; }
        public string RESULT { get; set; }
        public string MSG { get; set; }
        public MESReturnSNList[] SNLIST { get; set; }
    }

    public class MESReturnSNList
    {
        public string PART_NO { get { return string.IsNullOrEmpty(BATCHTYPE) ? "PN" : PN + "-" + BATCHTYPE; } }
        public string PART_DESC { get { return PART_NO; } }
        public string CARTONID { get; set; }
        public string BUCKETID { get; set; }
        public string ATSPalletNo { get; set; }


        public string SN { get; set; }
        public string WO { get; set; }
        public string BATCHTYPE { get; set; }
        public string LOAD_ID { get; set; }
        public string BOXID { get; set; }
        public string PALLETID { get; set; }
        public string PN { get; set; }
        public string MODEL { get; set; }
        public string REGION { get; set; }
        public string CUSTPN { get; set; }
        public string QHOLDFLAG { get; set; }
        public string TROLLEYNO { get; set; }
        public string TROLLEYLINENO { get; set; }
        public string TROLLEYLINENOPOINT { get; set; }
        public string DN { get; set; }
        public string ITEMNO { get; set; }

        public string IN_GUID { get; set; }
        public string WORK_ORDER { get { return WO; } }
        public string SERIAL_NUMBER { get { return SN + "01"; } }
        public string CUSTOMER_SN { get { return SN; } }
        public string CARTON_NO { get { return BOXID; } }
        public string PALLET_NO { get { return PALLETID; } }
        // 加入包装方式
        public string MPN { get { return CUSTPN; } }
        public string HOLD_FLAG { get { return QHOLDFLAG; } }
        public string TROLLEY_LINE_NO { get { return TROLLEYLINENO?.ToString(); } }
        public string POINT_NO { get { return TROLLEYLINENOPOINT?.ToString(); } }
        public string DELIVERY_NO { get { return DN?.ToString(); } }
        public string LINE_ITEM { get { return ITEMNO?.ToString(); } }
        public string TROLLEY_NO { get { return TROLLEYNO?.ToString(); } }
        public string PRODUCT_NAME { get; set; }
        public string WAREHOUSE_ID { get; set; }
        public string LOCATION_ID { get; set; }

    }
}
