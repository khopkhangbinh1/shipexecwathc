using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class MultiPalletCombineInModel
    {
        /// <summary>
        /// 任务唯一流水号
        /// </summary>
        public string TaskNo { get; set; }

        public PPSPALLETNO[] PALLETLIST { get; set; }


    }
    public class PPSPALLETNO
    {
        /// <summary>
        /// PPS栈板号
        /// </summary>
        public string PALLETNO { get; set; }
    }

    public class MultiPalletCombineOutModel : ICTReturnModel
    {

    }

}
