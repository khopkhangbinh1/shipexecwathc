using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class PPSStockTakeByICTPNInModel
    {
        public string AREA { get; set; }
        public string WAREHOUSE_NO { get; set; }
        public string LOCATION_NO { get; set; }
        public string PARTNO { get; set; }
    }

    public class PPSStockTakeByICTPNOutModel : ICTReturnModel
    {
        public TakePart[] PARTS { get; set; }
    }

    public class TakePart
    {
        public string PART_NO { get; set; }
        public TakeLocation[] LOCATION { get; set; }
        public int QTY { get; set; }
        public int CARTONQTY { get; set; }
        public TakeSnList[] SNList { get; set; }
    }

    public class TakeLocation
    {
        public string AREA { get; set; }
        public string WAREHOUSE_NO { get; set; }
        public string LOCATION_NO { get; set; }
        public int QTY { get; set; }
        public int CARTONQTY { get; set; }
    }
    public class TakeSnList
    {
        public string CARTONID { get; set; }
        public string SN { get; set; }
        public string BUCKETID { get; set; }

    }


}
