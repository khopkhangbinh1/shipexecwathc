using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class StockInConfirmInModel
    {
        public string TaskNo { get; set; }
        public string OPTYPE { get; set; }
        public List<TrolleyMoveNoticeInItemModel> CARS { get; set; }
    }

    public class StockInConfirmOutModel : ICTReturnModel
    {
        public string TaskNo { get; set; }
    }

}
