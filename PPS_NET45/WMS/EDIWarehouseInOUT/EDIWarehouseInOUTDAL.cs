using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIWarehouseInOUT
{
    class EDIWarehouseInOUTDAL
    {
        public DataSet GetSNInfoDataTable(string sn, string sntype)
        {
            string sql = string.Empty;
            if (sntype.Equals("CARTON"))
            {
                sql = string.Format(@"
                                 select a.carton_no, a.customer_sn, a.custpart, a.isinware
                                   from ppsuser.t_other_locate_sn a
                                  where a.carton_no in (select distinct carton_no
                                                          from ppsuser.t_other_locate_sn
                                                         where customer_sn = '{0}'
                                                            or carton_no = '{1}')
                                     ", sn, sn);
            }
            else
            {

                sql = string.Format(@"
                    select a.palletno, a.carton_no, count(a.customer_sn) cartonqty
                       from ppsuser.t_other_locate_sn a
                      where a.palletno in
                            (select distinct palletno
                               from ppsuser.t_other_locate_sn
                              where palletno = '{0}'
                                 or carton_no = '{1}')
                        and a.palletno is not null
                      group by a.palletno, a.carton_no
                      order by count(a.customer_sn) asc
                    ", sn, sn);

            }

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        //old Sample
        public string WMSTransINBySP(string strsn, string strLocationId, string sntype, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strsn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationid", strLocationId };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            //create or replace procedure SP_WMS_TRANSIN(insn         in varchar2,
            //                               inlocationid in varchar2,
            //                               errmsg       out varchar2) as

            string strSPname = string.Empty;
            if (sntype.Equals("PALLET"))
            { strSPname = "PPSUSER.SP_WMS_TRANSIN"; }
            else
            { strSPname = "PPSUSER.SP_WMS_TRANSINCARTON"; }
            DataSet ds = ClientUtils.ExecuteProc(strSPname, procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        //(strGUID, strWorkOrder, strSerialNumber, strCustomerSN, strCartonNO, strPalletNO, strBatchType, strLoadID,
        //                        strICTPartNo, strMODEL, strCustModel, strQHoldFlag, strTrolleyNo, strTrolleyLineNo, strTrolleyLineNoPoint, strWH,
        //                        strLocation, strProduct, strDeliveryNo, strDNLineNo, out errmsg)
        public string FGINBySP(string strGUID, string strWorkOrder,string strSerialNumber ,string strCustomerSN ,string strCartonNO ,string strPalletNO, string strBatchType, string strLoadID,
                               string strICTPartNo ,string strMODEL, string strCustModel,string strRegion,string strQHoldFlag,string strTrolleyNo, string strTrolleyLineNo ,string strTrolleyLineNoPoint ,
                               string strWH, string strLocation, string strProduct, string strDeliveryNo, string strDNLineNo,string strGoodsType, out string RetMsg)
        {
            object[][] procParams = new object[23][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strGUID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwo", strWorkOrder };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSerialNumber };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", strCustomerSN };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incartonno", strCartonNO };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNO };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inbatchtype", strBatchType };
            procParams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inloadid", strLoadID };
            procParams[8] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inictpartno", strICTPartNo };
            procParams[9] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inmodel", strMODEL };
            procParams[10] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incustmodel", strCustModel };
            procParams[11] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inregion", strRegion };
            procParams[12] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inqholdflag", strQHoldFlag };
            procParams[13] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarno", strTrolleyNo };
            procParams[14] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlineno", strTrolleyLineNo };
            procParams[15] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpoint", strTrolleyLineNoPoint };
            procParams[16] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwh", strWH };
            procParams[17] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationid", strLocation };
            procParams[18] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inproduct", strProduct };
            procParams[19] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "indn", strDeliveryNo };
            procParams[20] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "indnline", strDNLineNo };
            procParams[21] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ingoodstype", strGoodsType };
            procParams[22] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_INSERTFGSN", procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public string FGWMSTransINBySP(string strGUID, string strQty, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strGUID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inqty", strQty };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_FGTRANSIN", procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public DataSet GetNoExistPartNODataTableBySQL(string strGUID)
        {
            string sql = string.Empty;
            
                sql = string.Format(@"
                                 select distinct a.part_no
                              from ppsuser.mes_sn_status a
                             where a.in_guid = '{0}'
                               and a.part_no not in (select b.part_no from sajet.sys_part b)
                                     ", strGUID);
          
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public DataSet GetRegionNoMatchDataTableBySQL(string strGUID, string strLocationRegion)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 select distinct a.pallet_no , a.region 
                              from ppsuser.mes_sn_status a
                             where a.in_guid = '{0}'
                              and a.region <>'{1}'
                                     ", strGUID, strLocationRegion);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public string PNINBySP(string strPartNo, string strCustModelType, string strUPC, string strJAN, string strCountry, string strRegion, string strCustModel, string strSCC, out string RetMsg)
        {

            object[][] procParams = new object[10][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpartno", strPartNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incustmodeltype", strCustModelType };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inupc", strUPC };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "injan", strJAN };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incountry", strCountry };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inregion", strRegion };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incustmodel", strCustModel };
            procParams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inloadid", "" };
            procParams[8] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inscc", strSCC };
            procParams[9] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_INSERTPARTNO", procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }


        public DataSet GetSNSAPInfoDataTableBySQL(string strGUID)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                select b.work_order, b.part_no, count(b.customer_sn) sncount
                                  from ppsuser.mes_sn_status b
                                 where b.in_guid = '{0}'
                                 group by b.work_order, b.part_no
                                     ", strGUID);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public DataSet GetLocationRegionBySQL(string strLocationID)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                select a.region  from ppsuser.wms_location a where a.location_id ='{0}'
                                     ", strLocationID);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
        public DataSet GetSAPWHBySQL(string strWHID)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                               select a.sap_wh_no ||'-'||a.sap_wh_name  sapwhno ,a.sap_wh_no sapwhid  from ppsuser.wms_warehouse a where a.warehouse_id ='{0}'
                                     ", strWHID);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

    }
}
