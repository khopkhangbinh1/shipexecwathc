﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class EMEAOEMCommercialInvocieNetherlandsForm 
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public EMEAOEMCommercialInvocieNetherlandsForm(string shipID, string diskPath, bool print)
        {
            Initialize(shipID, diskPath, print);
        }

        private void Initialize(string shipID, string diskPath, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();

            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_EMEIA_DS_CCI_HEADER@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";

            string lineSql = @"SELECT T0.*,(T0.QTY*T0.UNIT_PRICE) AS AMOUNT FROM WMUSER.AC_EMEIA_DS_CCI_LINE@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            String filePath = "";
            filePath = diskPath + shipID + "EMEAOEMCommercialNetherlands.pdf";
            diskCompletePath = filePath; //全局变量返回pdf路径
            if (print)
            {
                Util.CreateDataTable(Constant.EMEACommercialNetherlands_URL, ds);
            }
            else
            {
                Util.exportCRPDFAndSendEmail(Constant.EMEACommercialNetherlands_URL, ds, filePath);
            }
        }
    }
}
