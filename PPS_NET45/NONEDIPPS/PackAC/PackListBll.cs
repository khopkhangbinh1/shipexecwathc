using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace PackListAC
{
    class PackListBll
    {
        public DataTable GetShippingPrintInfo(string strPartNo)
        {
            if (string.IsNullOrEmpty(strPartNo)) { return null; }
            PackListDal wd = new PackListDal();
            DataSet dataSet = wd.GetShippingPrintInfoBySQL(strPartNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetPickPalletNo(string strPartNo)
        {
            if (string.IsNullOrEmpty(strPartNo)) { return null; }
            PackListDal wd = new PackListDal();
            DataSet dataSet = wd.GetPickPalletNoBySQL(strPartNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
    }
}
