﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class ConsolCIAMRLayoutCOForm
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public string diskCompelteSerialPath = "";
        public ConsolCIAMRLayoutCOForm(string shipID, bool print)
        {
            Initialize(shipID, print);
        }

        private void Initialize(string shipID, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            string headerSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CCI_CO_HEADER@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string lineSql = @"SELECT * FROM WMUSER.AC_AMR_DS_CCI_CHI_LINE@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string serialSql = @"SELECT DISTINCT b.AC_PN, b.MATE_DESC, a.Serial_Number
                                  FROM PPSUSER.G_DS_SCANDATA_DETAIL_T     a,
                                       WMUSER.AC_AMR_DS_CCI_CHI_LINE@DGEDI b
                                 where a.SHIPMENT_ID = '" + shipID + "'"
                    + @"AND a.mpn = b.AC_PN AND a.SHIPMENT_ID = b.SHIPMENT_ID ORDER BY  b.AC_PN ,a.Serial_Number";

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(serialSql, "Serial"));
            string tmp = "";
            string tmp1 = "";

            tmp = Application.StartupPath + "\\PDF\\" + shipID + "CCICO.pdf";
            tmp1 = Application.StartupPath + "\\PDF\\" + shipID + "CCICOSERIAL.pdf";
            diskCompletePath = tmp; //全局变量返回pdf路径
            diskCompelteSerialPath = tmp1;
            if (print)
            {
                Util.CreateDataTable(Constant.CCILayoutCO_URL, ds);
                Util.CreateDataTable(Constant.CCILayoutCOSERIAL_URL, ds);
            }
            else
            {
                Util.exportCRPDFAndSendEmail(Constant.CCILayoutCO_URL, ds, tmp);
                Util.exportCRPDFAndSendEmail(Constant.CCILayoutCOSERIAL_URL, ds, tmp1);
            }

        }
    }
}
