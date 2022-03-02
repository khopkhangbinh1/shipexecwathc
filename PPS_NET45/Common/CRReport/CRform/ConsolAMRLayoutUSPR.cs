﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class ConsolAMRLayoutUSPR
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public ConsolAMRLayoutUSPR(string shipID, bool print)
        {
            Initialize(shipID, print);
        }

        private void Initialize(string shipID, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CPL_US_PR_HEADER@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string lineSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CPL_US_PR_LINE@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string PalletSql = @"select COUNT(DISTINCT PALLET_NO) AS PALLET_NO  from ppsuser.g_ds_scandata_t where SHIPMENT_ID = '" + shipID + "'";

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(PalletSql, "Pallet"));

            string tempDirc = Application.StartupPath + "\\PDF\\" + shipID + "AMRUS.pdf";
            diskCompletePath = tempDirc; //全局变量返回pdf路径
            if (print)
            {
                Util.CreateDataTable(Constant.ConsolAMRCAN_URL, ds);
            }
            else
            {
                Util.exportCRPDFAndSendEmail(Constant.ConsolAMRCAN_URL, ds, tempDirc);
            }
        }
    }
}
