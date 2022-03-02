using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class PPSStockTakeByLogInModel
    {
        public string AREA { get; set; }
        public string WAREHOUSE_NO { get; set; }
        public string LOCATION_NO { get; set; }
        public string STARTDATE { get; set; }
        public string ENDDATE { get; set; }
        public string TASKNO { get; set; }
    }

    public class PPSStockTakeByLogOutModel : ICTReturnModel
    {
        public TakeTask[] Tasks { get; set; }
    }

    public class TakeTask
    {
        public string TASKNO { get; set; }
        public string PALLETTROLLYNO { get; set; }
        public string OPTYPE { get; set; }
        public string DIRECTION { get; set; }
        public TakePart[] PARTS { get; set; }
    }


}
