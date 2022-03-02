using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    public class ATSStockInCheckInModel
    {
        public string TaskNo { get; set; }
        public string PalletNo { get; set; }
    }

    public class ATSStockInCheckOutModel : ICTReturnModel
    {
        public List<MESSNList> SNList { get; set; }
        public string TaskNo { get; set; }
        public string PalletNo { get; set; }
        public string ATSPalletNo { get; set; }
    }

    public class MESSNList
    {
        /// <summary>
        /// 料号
        /// </summary>
        public string PART_NO { get; set; }
        /// <summary>
        /// 料号描述
        /// </summary>
        public string PART_DESC { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string CARTONID { get; set; }
        /// <summary>
        /// SN 号
        /// </summary>
        public string SN { get; set; }
        /// <summary>
        ///  周转箱ID
        /// </summary>
        public string BUCKETID { get; set; }
    }

    public class ICTReturnModel
    {
        public bool Result { get; set; }
        public string Msg { get; set; }
    }

}
