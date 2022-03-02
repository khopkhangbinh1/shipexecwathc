using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPSToMESWcf.MODEL
{
    public class CommonModel
    {
        public class ResMsg
        {
            public bool result { get; set; }
            public string msg { get; set; }
        }

        public class FGINMODEL
        {
            public string INSN { get; set; }
        }

        public class FGINRETURNMODEL
        {
            public string INSN { get; set; }
            public ResMsg ResMsg { get; set; }
            public SNMODEL[] SNList { get; set; }

        }
        public class SNMODEL 
        {
            //工单号
            public string WorkOrder { get; set; }
            //厂内序号
            public string SerialNumber { get; set; }
            //客户序号
            public string CustomerSN { get; set; }
            //箱号
            public string CartonNO { get; set; }
            //栈板号 //如果是金刚车 这是车号
            public string PalletNO { get; set; }
            //ICT料号
            public string ICTPartNo { get; set; }
            //客户料号
            public string CustModel { get; set; }
            //QHoldflag  Y or N 
            public string QHoldFlag { get; set; }
            //如果是金刚车 ，车号
            public string TrolleyLineNo { get; set; }
            //车行的点位
            public string TrolleyLineNoPoint { get; set; }
            public string DeliveryNo { get; set; }
            public string DNLineNO { get; set; }
        }

        public class PARTNORETURNMODEL 
        {
            public  string PartNO { get; set; }
            public string Spec1 { get; set; }
            public string Spec2 { get; set; }
            public string UPC { get; set; }
            public string CustPartNO { get; set; }
            public string EAN { get; set; }
            public string OPTION21 { get; set; }
            public string OPTION22 { get; set; }
            public string OPTION26 { get; set; }
            public string OPTION27 { get; set; }
            
        }

        public class PPSFGINRETURNMODEL
        {
            public string INSN { get; set; }
            public ResMsg ResMsg { get; set; }

        }

        public class PPSQHoldModel
        {

            public string INSN { get; set; }
            public bool QHOLDFLAG { get; set; }
            public string HOLD_FLAG { get { return (QHOLDFLAG==true) ? "Y":"N" ; } }
        }

        public class PPSQHoldModelList
        {
            public PPSQHoldModel[] SNFLAGLIST{ get; set; }
        }
        public class PPSQHoldReturnModel
        {
            public bool RESULT { get; set; }
            public string MSG { get; set; }
            public string OUTSN { get; set; }
            public bool QHOLDFLAG { get; set; }
        }

        public class PPSTrolleyNoReturnModel
        {
            public bool RESULT { get; set; }
            public string MSG { get; set; }
            public string TROLLEYNO { get; set; }
        }

        public class PPSUpdateSNDNModel
        {
            public string INSN { get; set; }
            public string DELIVERYNO { get; set; }
            public string DNLINE { get; set; }
            public string WORKORDER { get; set; }
        }
        public class PPSUpdateSNDNReturnModel
        {
            public bool RESULT { get; set; }
            public string MSG { get; set; }
            public string OUTSN { get; set; }
            public string DELIVERYNO { get; set; }
            public string DNLINE { get; set; }
            public string WORKORDER { get; set; }
        }

    }
}
