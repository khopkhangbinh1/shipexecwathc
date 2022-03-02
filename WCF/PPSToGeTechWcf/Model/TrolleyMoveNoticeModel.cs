using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class TrolleyMoveNoticeInModel
    {
        public string TaskNo { get; set; }
        public string OPTYPE { get; set; }
        public List<TrolleyMoveNoticeInItemModel> CARS { get; set; }
    }

    public class TrolleyMoveNoticeOutModel : ICTReturnModel
    {
        public string TaskNo { get; set; }
    }


    public class TrolleyMoveNoticeInItemModel
    {
        public string WAREHOUSE_NO { get; set; }
        public string LOCATION_NO { get; set; }
        public int QTY { get; set; }
        public int CARTONQTY { get; set; }
        public string TROLLEYID { get; set; }
        public TrolleyMovCarton[] CARTONS { get; set; }
    }

    public class TrolleyMovCarton
    {
        public string ICT_PARTNO { get; set; }
        public string CARTONID { get; set; }
        public string BUCKETID { get; set; }
        public string PalletNo { get; set; }
        public string ATSPalletNo { get; set; }

        public TrolleyInfoModel TROLLEY { get; set; }
        public List<SNModel> SN { get; set; }
    }



    public class TrolleyInfoModel
    {
        public string ID { get; set; }
        public string LINENO { get; set; }
    }
    public class SNModel
    {
        public string SN { get; set; }
    }
}
