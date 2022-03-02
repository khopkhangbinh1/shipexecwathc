using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class ATSStockInInModel
    {
        /// <summary>
        /// 任务唯一流水号
        /// </summary>
        public string TaskNo { get; set; }
        /// <summary>
        /// ATSStockIn
        /// </summary>
        public string OPTYPE { get; set; }
        /// <summary>
        ///  MES栈板号
        /// </summary>
        public string PalletNo { get; set; }
        /// <summary>
        /// 托盘号
        /// </summary>
        public string ATSPalletNo { get; set; }
        /// <summary>
        /// 自动仓 指派 location Id
        /// </summary>
        public string ATSLocId { get; set; }

        public MESSNList[] SNList { get; set; }

    }

    public class ATSStockInOutInModel : ICTReturnModel
    {
        public List<MESSNList> SNList { get; set; }
        public string TaskNo { get; set; }
        public string PalletNo { get; set; }
        public string ATSPalletNo { get; set; }
    }

}
