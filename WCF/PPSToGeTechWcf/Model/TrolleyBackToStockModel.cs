using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class TrolleyBackToStockInModel
    {
        public string TROLLEYID { get; set; }
        public string OPTYPE { get; set; }
        public int QTY { get; set; }
        public int CARTONQTY { get; set; }
        public TrolleyMovCarton[] CARTONS { get; set; }
    }

    public class TrolleyBackToStockOutModel : ICTReturnModel
    {
    }
}
