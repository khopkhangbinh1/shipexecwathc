using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIWarehouseIN
{
    class EDIWarehouseINDAL
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


        public string FGINBySP(string strGUID, string strWorkOrder, string strSerialNumber, string strCustomerSN, string strCartonNO, string strPalletNO, string strBatchType, string strLoadID,
                               string strICTPartNo, string strMODEL, string strCustModel, string strRegion, string strQHoldFlag, string strTrolleyNo, string strTrolleyLineNo, string strTrolleyLineNoPoint,
                               string strProduct, string strDeliveryNo, string strDNLineNo, string strGoodsType, string strMesLine, string strSAPTransfer, string strForceFlag, string srtCOO, string strMesWhNo, string strMesPlant, out string RetMsg)
        {
            object[][] procParams = new object[27][];
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
            procParams[16] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inproduct", strProduct };
            procParams[17] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "indn", strDeliveryNo };
            procParams[18] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "indnline", strDNLineNo };
            procParams[19] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ingoodstype", strGoodsType };
            procParams[20] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inmesline", strMesLine };
            procParams[21] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insaptransfer", strSAPTransfer };
            procParams[22] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inforceflag", strForceFlag };
            procParams[23] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incoo", srtCOO };
            procParams[24] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inmesplant", strMesPlant };
            procParams[25] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inmeswhno", strMesWhNo };
            procParams[26] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMSI_INSERTFGSN", procParams);
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

        public string RAWMINBySP(string strGUID, string strCartonNO, string strPalletNO, string strICTPartNo, string strSnQty, string strTransferDN, string strProduct, string strForceFlag, string strPlant, string strSloc, out string RetMsg)
        {
            object[][] procParams = new object[11][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strGUID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incartonno", strCartonNO };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNO };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inictpartno", strICTPartNo };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insnqty", strSnQty };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "intransferdn", strTransferDN };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inproduct", strProduct };
            procParams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inforceflag", strForceFlag };
            procParams[8] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inplant", strPlant };
            procParams[9] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insloc", strSloc };
            procParams[10] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMSI_INSERTRAWMSN", procParams);
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

        public string WmsiPalletTransINBySP(string strPalletNO,string strLocationNo, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNO };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationno", strLocationNo };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMSI_PALLETTRANSIN", procParams);
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

        public string WmsiPalletTransINBySP(string strPalletNO, string strLocationNo, string strplant_sloc, out string RetMsg)
        {
            string[] ss = strplant_sloc.Split('-');
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNO };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationno", strLocationNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inplant", ss[0] };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insloc", ss[1] };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMSI_PALLETTRANSIN", procParams);
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
                               and a.part_no not in (select b.part_no from sajet.sys_part b where b.part_no is not null)
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

        public DataSet GetNoExistPartNODataTableBySQL2(string strPalletNO)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                  select distinct b.part_no, a.product_name , b.batchtype
                                   from ppsuser.mes_pallet_info a
                                   join ppsuser.mes_sn_status b
                                     on a.in_guid = b.in_guid
                                    and a.pallet_no = b.pallet_no
                                  where a.pallet_no = '{0}'
                                    and b.part_no not in (select c.part_no from sajet.sys_part c where c.part_no is not null)
                                     ", strPalletNO);

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

        public DataSet GetPalletNODataTableBySQL(string strPalletNO)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                select b.customer_sn,
                                       b.work_order,
                                       b.batchtype,
                                       b.load_id,
                                       b.carton_no,
                                       b.pallet_no,
                                       b.part_no,
                                       b.model,
                                       b.region,
                                       b.mpn,
                                       b.hold_flag,
                                       b.trolley_no,
                                       b.trolley_line_no,
                                       b.pps_checkpoint as point_no,
                                       b.delivery_no,
                                       b.line_item
                                  from ppsuser.mes_pallet_info a
                                  join ppsuser.mes_sn_status b
                                    on a.in_guid = b.in_guid
                                   and a.pallet_no = b.pallet_no
                                 where a.pallet_no = '{0}'
                                union
                                select b.customer_sn,
                                       b.work_order,
                                       b.batchtype,
                                       b.load_id,
                                       b.carton_no,
                                       b.pallet_no,
                                       b.part_no,
                                       b.model,
                                       b.region,
                                       b.mpn,
                                       b.hold_flag,
                                       b.trolley_no,
                                       b.trolley_line_no,
                                       b.pps_checkpoint as point_no,
                                       b.delivery_no,
                                       b.line_item
                                  from ppsuser.mes_pallet_info a
                                  join ppsuser.mes_sn_status b
                                    on a.in_guid = b.in_guid
                                   and a.pallet_no = b.pallet_no
                                   and a.trolley_no = '{0}'
                                   and a.cdt > trunc(sysdate)
                                 order by carton_no asc, customer_sn asc
                                     ", strPalletNO);

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
                                select a.region  from sajet.wms_location a where a.location_id ='{0}'
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
                               select a.sap_wh_no ||'-'||a.sap_wh_name sapwhno  from sajet.wms_warehouse a where a.warehouse_id ='{0}'
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

       
        public Boolean WMSIBackUpWebServieLogBySQL(string strguid , string serverip, string url, string insn, string result,string strempno,string strinterfacename,out string RetMsg)
        {
            object[][] sqlparams = new object[7][];
            string sql = string.Empty;

            sql = string.Format(@"
                               insert into ppsuser.t_wmsi_meswebservice
                                  (msg_id, strserverip, strurl, pallet_no, strresult, emp_no, interface_name)
                                values
                                  (:inguid, :inserverip, :inurl, :insn,:inresult, :inempno , :interfacename)
                                     ");

            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strguid };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inserverip", serverip };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inurl", url };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            sqlparams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inresult", result };
            sqlparams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", strempno };
            sqlparams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "interfacename", strinterfacename };
            DataSet dataSet = new DataSet();
            try
            {
                //dataSet = ClientUtils.ExecuteSQL(sql.Replace(";", ""));
                dataSet = ClientUtils.ExecuteSQL(sql, sqlparams);
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return false;
            }
            RetMsg = "";
            return true;
        }


        public DataSet GetWmsiPalletDataTableBySQL(string strPALLET, string strSTATUS, string strStime, string strEtime)
        {
            string sql = string.Empty;
            object[][] sqlparams = new object[0][];
            int iPara = 0;
            sql = @"select a.pallet_no,
                           a.trolley_no,
                           a.product_name,
                           a.region,
                           a.goodstype,
                           a.cdt,
                           a.pallet_status,
                           a.check_status,
                           a.trans_pps_status,
                           a.fb_mes_status,
                           a.upload_sap_status,
                           a.edi_flag,
                           a.marina_status,
                           a.plant,
                           a.sloc
                      from ppsuser.mes_pallet_info a
                         where 1=1 ";
            //SAP出货单号查询条件
            if (strPALLET != "" && strPALLET != "ALL")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sql += " and a.pallet_no = :strpallet";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strpallet", strPALLET };
                iPara = iPara + 1;
            }


            if (strSTATUS != "" && strSTATUS != "ALL")
            {
                if (strSTATUS.Equals("ER"))
                {
                    sql += " and (a.pallet_status in ('IN','ER')  and a.trans_pps_status ='WP' ) ";
                }
                else if (strSTATUS.Equals("WP"))
                {
                    sql += " and a.pallet_status ='WP' and a.trans_pps_status ='WP' ";
                }
                else if (strSTATUS.Equals("IP"))
                {
                    sql += " and  a.trans_pps_status ='FP'  and (a.fb_mes_status ='WP' or a.upload_sap_status ='WP'  ) ";
                }
                else if (strSTATUS.Equals("FP"))
                {
                    sql += " and  a.trans_pps_status ='FP'  and (a.fb_mes_status in ('FP','NA') or a.upload_sap_status in ('FP','NA')  ) ";
                }
                else
                {
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    sql += " and a.pallet_status = :status";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "status", strSTATUS };
                    iPara = iPara + 1;
                }
            }

            //开始日期
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.cdt >= to_date(:starttime ,'yyyy-mm-dd hh24:mi')";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "starttime", strStime };
            iPara = iPara + 1;

            //结束日期
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.cdt <= to_date(:endtime ,'yyyy-mm-dd  hh24:mi')";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "endtime", strEtime };
            iPara = iPara + 1;

            sql += " order by a.cdt desc";


            DataSet dataSet = new DataSet();
            try
            {
                //dataSet = ClientUtils.ExecuteSQL(sql);
                dataSet = ClientUtils.ExecuteSQL(sql, sqlparams);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public DataSet GetWmsiGUIDPALLETBySQL(string strGUID)
        {
            string sql = string.Empty;
            object[][] sqlparams = new object[0][];
            int iPara = 0;
            sql = @"select a.pallet_no,
                           a.trolley_no,
                           a.product_name,
                           a.region,
                           a.goodstype,
                           a.cdt,
                           a.pallet_status,
                           a.check_status,
                           a.trans_pps_status,
                           a.fb_mes_status,
                           a.upload_sap_status,
                           a.edi_flag,
                           a.marina_status,
                           a.plant,
                           a.sloc
                      from ppsuser.mes_pallet_info a
                         where 1=1 ";
            //SAP出货单号查询条件
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.in_guid = :strguid";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strguid", strGUID };
            iPara = iPara + 1;


            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql, sqlparams);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }


        //ppsuser.sp_wmsi_checkpalletstatus(inpalletno   in varchar2,
        //                                                      inchecktype  in varchar2,
        //                                                      computername in varchar2,
        //                                                      errmsg       out varchar2


        public string WmsiPalletCheckBySP(string strPalletNO, string strCheckType,string strComputerName, out string RetMsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNO };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inchecktype", strCheckType };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "computername", strComputerName };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_wmsi_checkpalletstatus", procParams);
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
        public string WmsiTrolleyCheckinBySP(string strPalletNO, string strTrolleyLine, string strCarton, out string RetMsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNO };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "introlleyline", strTrolleyLine };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarton", strCarton };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_wmsi_trolleycheckin", procParams);
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

        
        public DataSet GetTrolleyNODataTableBySQL(string strPalletNO)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 select aaa.trolley_line_no,
                                       aaa.point_no,
                                       aaa.delivery_no,
                                       aaa.line_item,
                                       aaa.carton_no,
                                       aaa.customer_sn
                                  from (select aa.*, rownum seqnum, 'A' seqnum2
                                          from (select b.trolley_line_no trolley_line_no,
                                                       b.pps_checkpoint  as point_no,
                                                       b.delivery_no,
                                                       b.line_item,
                                                       b.carton_no,
                                                       b.customer_sn
                                                  from ppsuser.mes_pallet_info a
                                                  join ppsuser.mes_sn_status b
                                                    on a.in_guid = b.in_guid
                                                   and a.pallet_no = b.pallet_no
                                                 where a.pallet_no = '{0}'
                                                   and b.pps_checkpoint = '0'
                                                 order by b.trolley_line_no asc, b.delivery_no asc) aa
                                        union
                                        select bb.*, rownum seqnum, 'B' seqnum2
                                          from (select b1.trolley_line_no trolley_line_no,
                                                       b1.pps_checkpoint  as point_no,
                                                       b1.delivery_no,
                                                       b1.line_item,
                                                       b1.carton_no,
                                                       b1.customer_sn
                                                  from ppsuser.mes_pallet_info a1
                                                  join ppsuser.mes_sn_status b1
                                                    on a1.in_guid = b1.in_guid
                                                   and a1.pallet_no = b1.pallet_no
                                                 where a1.pallet_no = '{0}'
                                                   and b1.pps_checkpoint <> '0'
                                                 order by b1.trolley_line_no asc, b1.pps_checkpoint asc) bb) aaa
                                 order by seqnum2 asc, seqnum asc
                                     ", strPalletNO);

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

        public DataSet GetTrolleyNODataTableBySQL(string strPalletNO,string strTrolleyLine)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(strTrolleyLine)) 
            {
                sql = string.Format(@"
                                  select aaa.trolley_line_no,
                                       aaa.point_no,
                                       aaa.delivery_no,
                                       aaa.line_item,
                                       aaa.carton_no,
                                       aaa.customer_sn
                                  from (select aa.*, rownum seqnum, 'A' seqnum2
                                          from (select b.trolley_line_no trolley_line_no,
                                                       b.pps_checkpoint  as point_no,
                                                       b.delivery_no,
                                                       b.line_item,
                                                       b.carton_no,
                                                       b.customer_sn
                                                  from ppsuser.mes_pallet_info a
                                                  join ppsuser.mes_sn_status b
                                                    on a.in_guid = b.in_guid
                                                   and a.pallet_no = b.pallet_no
                                                 where a.pallet_no = '{0}'
                                                   and b.pps_checkpoint = '0'
                                                 order by b.trolley_line_no asc, b.delivery_no asc) aa
                                        union
                                        select bb.*, rownum seqnum, 'B' seqnum2
                                          from (select b1.trolley_line_no trolley_line_no,
                                                       b1.pps_checkpoint  as point_no,
                                                       b1.delivery_no,
                                                       b1.line_item,
                                                       b1.carton_no,
                                                       b1.customer_sn
                                                  from ppsuser.mes_pallet_info a1
                                                  join ppsuser.mes_sn_status b1
                                                    on a1.in_guid = b1.in_guid
                                                   and a1.pallet_no = b1.pallet_no
                                                 where a1.pallet_no = '{0}'
                                                   and b1.pps_checkpoint <> '0'
                                                 order by b1.trolley_line_no asc, b1.pps_checkpoint asc) bb) aaa
                                 order by seqnum2 asc, seqnum asc
                                     ", strPalletNO);
            }
            else
            {
                sql = string.Format(@"
                                 select b.trolley_line_no trolley_line_no,
                                        b.pps_checkpoint  as point_no,
                                        b.delivery_no,
                                        b.line_item,
                                        b.carton_no,
                                        b.customer_sn
                                   from ppsuser.mes_pallet_info a
                                   join ppsuser.mes_sn_status b
                                     on a.in_guid = b.in_guid
                                    and a.pallet_no = b.pallet_no
                                  where a.pallet_no = '{0}'
                                    and b.trolley_line_no = '{1}' 
                                  order by b.pps_checkpoint asc,b.trolley_line_no asc
                                     ", strPalletNO, strTrolleyLine);

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

        public DataSet GetPalletCartonInfoDataTableBySQL(string strPalletNO)
        {
            string sql = string.Empty;
            
            sql = string.Format(@"
                                select b.carton_no,
                                        b.part_no,
                                        b.batchtype,
                                        b.mpn,
                                        b.model,
                                        b.delivery_no,
                                        b.line_item,
                                        b.trolley_line_no,
                                        b.pps_checkpoint as point_no
                                   from ppsuser.mes_pallet_info a
                                   join ppsuser.mes_sn_status b
                                     on a.in_guid = b.in_guid
                                    and a.pallet_no = b.pallet_no
                                  where a.pallet_no = '{0}'
                                  order by b.trolley_line_no asc,b.pps_checkpoint asc, b.mpn asc, b.carton_no asc
 
                                    ", strPalletNO);

            

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
        //栈板和原材
        public DataSet GetPalletCartonInfoDataTableBySQL2(string strPalletNO)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                               select a.pallet_no, b.carton_no, b.part_no, count(b.customer_sn) sn_qty
                                  from ppsuser.mes_pallet_info a
                                  join ppsuser.mes_sn_status b
                                    on a.in_guid = b.in_guid
                                   and a.pallet_no = b.pallet_no
                                 where a.pallet_no = '{0}'
                                 group by a.pallet_no, b.carton_no, b.part_no
                                 order by a.pallet_no asc, b.carton_no asc, b.part_no asc
 
                                    ", strPalletNO);



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

        public string PPSGetbasicparameterBySP(string strParaType, out string strParaValue, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inparatype", strParaType };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outparavalue", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.sp_pps_getbasicparameter", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                strParaValue = "";
                RetMsg = e1.ToString();
                return "NG";
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            strParaValue = dt.Rows[0]["outparavalue"].ToString();
            if (RetMsg.Equals("OK"))
            {

                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public DataSet GetLocationNoInfoBySQL(string strLocationNo, string strPalletEDIflag)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                                 select a.location_no, a.qty, a.cartonqty, part_no
                                      from nonedipps.t_location a
                                     where a.location_no = '{0}'
                                 union
                                select a.location_no, a.qty, a.cartonqty, part_no
                                    from ppsuser.t_location a
                                    where a.location_no = '{0}'
                                     ", strLocationNo);
            
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


        public DataSet GetMesPalletInfoLogBySQL(string strPalletNO)
        {
            string sql = string.Empty;

            sql = string.Format(@"
	                        select count(*) as recordnum 
                                from ppsuser.mes_pallet_info a
                               where a.pallet_no ='{0}'
                                     ", strPalletNO);

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

        public DataSet PPSGetWHNO(string strPalletNO)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                select WHNO from ppsuser.mes_pallet_info a
                               where a.pallet_no ='{0}'
                                     ", strPalletNO);

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

        public Boolean WMSIUpdatePalletStatusBySQL(string strPalletNO, out string RetMsg)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 update ppsuser.mes_pallet_info a
                                 set a.fb_mes_status='FP', a.udt=sysdate
                                 where a.pallet_no ='{0}'
                                 and a.fb_mes_status='WP'
                                     ", strPalletNO);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql.Replace(";", ""));
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return false;
            }
            RetMsg = "";
            return true;
        }
        public Boolean WMSIUpdatePalletStatusBySQL2(string strPalletNO, out string RetMsg)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 update ppsuser.mes_pallet_info a
                                 set a.upload_sap_status='FP' , a.udt=sysdate
                                 where a.pallet_no ='{0}'
                                 and a.upload_sap_status='WP'
                                     ", strPalletNO);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql.Replace(";", ""));
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return false;
            }
            RetMsg = "";
            return true;
        }


        public DataSet GetSNSAPInfoDataTableBySQL2(string strPalletNO)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                    select d.sap_wh_no,
                                            b.work_order,
                                            c.batch_no,
                                            b.part_no,
                                            b.mes_line, 
                                            count(distinct b.customer_sn) sncount
                                       from ppsuser.mes_pallet_info a
                                       join ppsuser.mes_sn_status b
                                         on a.in_guid = b.in_guid
                                        and a.pallet_no = b.pallet_no
                                       join ppsuser.g_sn_status c
                                         on to_char(b.customer_sn) = to_char(c.customer_sn)
                                       join sajet.wms_warehouse d
                                         on c.container = d.warehouse_id
                                      where a.pallet_no = '{0}'
                                      group by d.sap_wh_no, b.work_order, c.batch_no, b.part_no,mes_line
                                     ", strPalletNO);

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


        public Boolean InsertSapWebLogBySQL(string strGUID, string strServerIP, string strSAPWcf, string strPalletNO, string  strSAPNO, string  strReqjson , string  strResjson,string strUserNo, string strSapRestatus, string strSapRemsg, out string RetMsg)
        {
            RetMsg = "";

            //insert tabel 
            //insert into ppsuser.t_wmsi_sapwebservice
            //  (msg_id, strserverip, strurl, pallet_no, sap_no, req_json, res_json, emp_no, cdt, status, errmsg)
            //values
            //  (v_msg_id, v_strserverip, v_strurl, v_pallet_no, v_sap_no, v_req_json, v_res_json, v_emp_no, v_cdt, v_status, v_errmsg);

            //sql = string.Format(@"
            //insert into ppsuser.t_wmsi_sapwebservice
            //  (msg_id, strserverip, strurl, pallet_no, sap_no, req_json, res_json, emp_no,  status, errmsg)
            //values
            //  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')
            //  ", strGUID, strServerIP, strSAPWcf, strPalletNO, strSAPNO, strReqjson, strResjson, strUserNo, strSapRestatus, strSapRemsg);


            object[][] sqlparams = new object[10][];
            string sql = string.Empty;

            sql = string.Format(@"
            insert into ppsuser.t_wmsi_sapwebservice
              (msg_id, strserverip, strurl, pallet_no, sap_no, req_json, res_json, emp_no,  status, errmsg)
            values
              (:inguid, :inserverip, :inurl, :inpalletno, :insapno, :inreqjson, :inresjson,:inempno,:instatus, :inerrmsg)
              ", strGUID, strServerIP, strSAPWcf, strPalletNO, strSAPNO, strReqjson, strResjson, strUserNo, strSapRestatus, strSapRemsg);


            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strGUID };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inserverip", strServerIP };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inurl", strSAPWcf };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNO };
            sqlparams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insapno", strSAPNO };
            sqlparams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inreqjson", strReqjson };
            sqlparams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inresjson", strResjson };
            sqlparams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", strUserNo };
            sqlparams[8] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "instatus", strSapRestatus };
            sqlparams[9] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inerrmsg", strSapRemsg };
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql, sqlparams);
            }
            catch (Exception e)
            {
                RetMsg=e.ToString();
                return false;
            }
            return true;

        }

        public DataSet GetPalletNoStatusInfoBySQL(string strPalletNo)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 select a.pallet_no,
                                       a.trolley_no,
                                       a.product_name,
                                       a.region,
                                       a.goodstype,
                                       a.cdt,
                                       a.pallet_status,
                                       a.check_status,
                                       a.trans_pps_status,
                                       a.fb_mes_status,
                                       a.upload_sap_status,
                                       a.edi_flag,
                                       a.marina_status
                                  from ppsuser.mes_pallet_info a
                                     where a.pallet_no = '{0}'
                                     ", strPalletNo);

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

        public string GetDBTypeBySP(string inparatype, out string outparavalue, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inparatype", inparatype };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outparavalue", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_GETBASICPARAMETER", procParams);
            //create or replace procedure SP_PPS_GETBASICPARAMETER(inparatype   in varchar2,
            //                                                 outparavalue  out varchar2,
            //                                                 errmsg out varchar2) as
            outparavalue = ds.Tables[0].Rows[0]["outparavalue"].ToString();
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }


        public DataSet GetSNInfoDataTableDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select distinct customer_sn ,a.product_name
                                  from ppsuser.mes_pallet_info a
                                  join ppsuser.mes_sn_status b
                                    on a.in_guid = b.in_guid
                                   and a.pallet_no = b.pallet_no
                                 where a.pallet_no = '{0}'
                                 order by customer_sn asc", inputSno);

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

        public Boolean CheckMarinaServerUrlLogBySQL(string strguid, string strserverip, string strurl, string strSN, string strresult, string strempno, string strrequest, out string RetMsg)
        {
            object[][] sqlparams = new object[7][];
            string sql = string.Empty;

            //sql = string.Format(@"
            //                   insert into ppsuser.t_pick_marinawebservice
            //                   (msg_id, strserverip, strurl, pallet_no,  req_json, res_json, emp_no)
            //                 values
            //                   ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')
            //                         ",strguid, strserverip, strurl, strSN, strresult, strrequest, strempno);
            sql = string.Format(@"
                               insert into ppsuser.t_pick_marinawebservice
                               (msg_id, strserverip, strurl, pallet_no,  req_json, res_json, emp_no,createby)
                             values
                               (:inguid, :inserverip, :inurl, :insn, :inrequest, :inresult, :inempno,'WMSI')
                                     ");
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strguid };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inserverip", strserverip };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inurl", strurl };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSN };
            sqlparams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inrequest", strrequest };
            sqlparams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inresult", strresult };
            sqlparams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", "" };
            DataSet dataSet = new DataSet();
            try
            {
                //dataSet = ClientUtils.ExecuteSQL(sql.Replace(";", ""));
                dataSet = ClientUtils.ExecuteSQL(sql, sqlparams);
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return false;
            }
            RetMsg = "";
            return true;
        }


        public Boolean WMSIUpdatePalletStatusBySQL3(string strPalletNO, out string RetMsg)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 update ppsuser.mes_pallet_info a
                                 set a.marina_status='FP'
                                 where a.pallet_no ='{0}'
                                 and a.marina_status='WP'
                                     ", strPalletNO);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql.Replace(";", ""));
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return false;
            }
            RetMsg = "";
            return true;
        }



        public Boolean GetMESPalletOKUpdateStatusBySQL(string strGuid, out string RetMsg)
        {
            string sql = string.Empty;

            sql = string.Format(@"update ppsuser.mes_pallet_info a 
                                set a.pallet_status ='WP'
                                  where a.in_guid ='{0}' 
                                   and a.pallet_status ='IN'
                               ", strGuid);


            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql.Replace(";", ""));
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return false;
            }
            RetMsg = "";
            return true;
        }



        public string GetMarinaPackoutFlagBySP(string strSN,string strStation, out string strMarinaFlag, out string strPackoutFlag, out string errmsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSN };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "instation", strStation };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outmarinaflag", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outpackoutflag", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_pps_marinapackoutcheck", procParams);
            //create or replace procedure ppsuser.sp_pps_marinapackoutcheck(insn           in varchar2,
            //                                                  instation      in varchar2,
            //                                                  outmarinaflag  out varchar2,
            //                                                  outpackoutflag out varchar2,
            //                                                  errmsg         out varchar2) as
            strMarinaFlag = ds.Tables[0].Rows[0]["outmarinaflag"].ToString();
            strPackoutFlag = ds.Tables[0].Rows[0]["outpackoutflag"].ToString();
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public DataSet GetWMSNListBySQL(Int32 ichecktotalcount)
        {
            string sql = string.Empty;
            if (ichecktotalcount == 0 || string.IsNullOrEmpty(ichecktotalcount.ToString()))
            {
                sql= string.Format(@"select a.customer_sn
                                  from ppsuser.t_sn_status a
                                 where a.wc = 'W0'
                                   and a.customer_sn not in
                                       (select b.customer_sn
                                          from ppsuser.t_wms_marina_sn_info b
                                         where b.cdt > trunc(sysdate))
                                ");
            }
            else
            {
                sql=  string.Format(@"select aa.customer_sn
                              from (select a.customer_sn
                                      from ppsuser.t_sn_status a
                                     where a.wc = 'W0'
                                       and a.customer_sn not in
                                           (select b.customer_sn
                                              from ppsuser.t_wms_marina_sn_info b
                                             where b.cdt > trunc(sysdate))) aa
                             where rownum <= {0}", ichecktotalcount);
            }


            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception)
            {
                return null;
            }
            return dataSet;

        }

        public DataSet GetAutoWMSIPalletNOBySQL()
        {
            string sql = string.Empty;
             sql = string.Format(@"select a.customer_sn
                                  from ppsuser.t_temp_csn a
                                  where status  =0
                                 
                                ");
            
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception)
            {
                return null;
            }
            return dataSet;

        }
        public DataSet GetAutoWMSILocationNOBySQL()
        {
            string sql = string.Empty;
            sql = string.Format(@"select location_no
                                      from (select a.location_no
                                              from ppsuser.wms_location a
                                             where a.location_no not in
                                                   (select location_no from ppsuser.t_location))
                                     where rownum <= 1
                                ");

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception)
            {
                return null;
            }
            return dataSet;

        }

        

    }
}
