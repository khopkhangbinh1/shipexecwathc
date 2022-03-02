using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class D1_DeliveryNote
    {
        public D1_DeliveryNote(string acDn)
        {
            setDataSoure(acDn);
        }


        private void setDataSoure(String acDn)
        {
            string strSql = @"SELECT
	                            T0.AC_DN,
	                            T0.SALES_ORG_CODE,
	                            T0.WO_NUM,
	                            T0.CARR_CODE,
	                            T0.SHIP_CONDI_CODE,
	                            T0.SO_NAME,
	                            T0.SO_COMPANY,
	                            T0.SO_ADDR1,
	                            T0.SO_ADDR2,
	                            T0.SO_ADDR3,
	                            T0.SO_DISTRICT,
	                            T0.SO_CITY,
	                            T0.SO_REGION,
	                            T0.SO_POSTAL,
	                            T0.SO_COUNTRY,
	                            T0.ST_NAME,
	                            T0.ST_COMPANY,
	                            T0.ST_ADDR1,
	                            T0.ST_ADDR2,
	                            T0.ST_ADDR3,
	                            T0.ST_DISTRICT,
	                            T0.ST_CITY,
	                            T0.ST_REGION,
	                            T0.ST_POSTAL,
	                            T0.ST_COUNTRY,
	                            T0.CUST_SHIP_INST AS CUST_SHIP_INST1,
	                            T0.CUST_WH_INST AS CUST_WH_INST1,
	                            T0.EXT_CUST_NOTE,
	                            T1.AC_ECPON,
	                            T1.AC_SO,
	                            T1.AC_DN,
	                            T1.AC_DN_LINE,
	                            T1.AC_PN,
	                            T1.MATE_DESC,
	                            T1.QTY,
	                            T1.CTO_TEXT,
	                            T1.CUST_WH_INST,
	                            T1.EXT_ITEM_NOTE,
	                            T1.CUST_SHIP_INST
                            FROM
	                            WMUSER.AC_DS_APAC_DELN_HEADER@dgedi T0
                            CROSS JOIN WMUSER.AC_DS_APAC_DELN_LINE@dgedi T1
                            WHERE
	                            T0.AC_DN = T1.AC_DN
                            AND T0.AC_DN = '" + acDn + "'";
            DataSet action = new DataSet();
            action.Tables.Add(Util.getDataTaleC(strSql, "DataTable1"));

            Util.CreateDataTable(
            //Constant.D1_DeliveryNote_URL,
            //无D1 DN URL 随意替换成PAC DN URL
            Constant.PAC_DeliveryNote_URL,
            action);
        }
    }
}
