using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{

    public class CleanStockInModel
    {
        public string TaskNo { get; set; }
        public string OPTYPE { get; set; }
        public SNModel[] SN { get; set; }
    }

    public class CleanStockOutModel : ICTReturnModel
    {
    }
}
