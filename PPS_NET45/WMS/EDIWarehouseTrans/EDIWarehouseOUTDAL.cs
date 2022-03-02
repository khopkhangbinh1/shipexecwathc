﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIWareHouseTrans
{
    class EDIWarehouseOUTDAL
    {

        public DataSet GetSAPIDDataTableBySQL(string strWHOutType, string strSAPid, string strSTATUS, string strStime, string strEtime)
        {
            string sql = string.Empty;
            object[][] sqlparams = new object[0][];
            int iPara = 0;
        if (strWHOutType.Equals("ZTL"))
            {
                sql = @"select a.zdbnum as sap_no,
                               a.cdt,
                               a.status,
                               a.computer_name,
                               a.close_time,
                               a.bwart,
                               a.btext,
                               a.rqdatet,
                               a.usnam,
                               a.erdatt,
                               a.del_flag,
                               a.zbz,
                               a.werks,
                               a.name1,
                               a.bukrs,
                               b.werks_bc,
                               b.lgort_bc,
                               b.werks_br,
                               b.lgort_br
                               from ppsuser.sap_ztl a  join ppsuser.sap_ztl_line b
                               on a.zdbnum=b.zdbnum
                         where 1 = 1  and  exists
                        (select distinct LGORT_BR,WERKS_BR from ppsuser.sap_ztl_line   where  ZDBNUM=a.ZDBNUM and   (LGORT_BR ,WERKS_BR) in(
                         select SAP_WH_NO,plant from ppsuser.wms_warehouse  where warehouse_no <>'SYS' and  enabled = 'Y'
                         union 
                        select SAP_WH_NO,plant from nonedipps.wms_warehouse  where warehouse_no <>'SYS' and  enabled = 'Y' )) ";
                //SAP出货单号查询条件
                if (strSAPid != "" && strSAPid != "ALL")
                {
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    sql += " and a.zdbnum = :strsapid";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strsapid", strSAPid };
                    iPara = iPara + 1;
                }
            }
            else
            {
                return null;
            }

            if (strSTATUS != "" && strSTATUS != "ALL")
            {
                
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sql += " and a.status = :status";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "status", strSTATUS };
                iPara = iPara + 1;
            }
            //开始日期
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.cdt >= to_date(:starttime ,'yyyy-mm-dd')";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "starttime", strStime };
            iPara = iPara + 1;

            //结束日期
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.cdt <= to_date(:endtime ,'yyyy-mm-dd')";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "endtime", strEtime };
            iPara = iPara + 1;

            sql += "  and rownum <=1000  order by a.cdt asc";


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


        public string WmsOCheckSapNoBySP(string strSapNo,string  strWHOutType, string localHostname, out string RetMsg)
        {
            object[][] procParams = new object[4][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insapno", strSapNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwhouttype", strWHOutType };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "computername", localHostname };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.SP_WMSO_CHECKSAPNOSTATUS", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                return "NG";
            }
            
            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else if (RetMsg.Contains("WA"))
            {
                return "WA";
            }
            else
            {
                return "NG";
            }

        }
        public string WmsOCheckCartonFBMESBySP(string strCartonNo,out string strProduct, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incartonno", strCartonNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "outproduct", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.SP_WMSO_CHECKCARTONFBMES", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                strProduct = "";
                return "NG";
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            strProduct = dt.Rows[0]["outproduct"].ToString();
            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            
            else
            {
                return "NG";
            }

        }

        public DataSet GetSAPNOLINEINFOBySQL(string strSapNo,string strWHOutType)
        {
            string sql = string.Empty;


            if (strWHOutType.Equals("ZSF"))
            {
                sql = string.Format(@"select a.lddnum sap_no,
                               a.sortid line_item,
                               a.matnr part_no,
                               a.maktx part_desc,
                               a.qty qty,
                               a.deal_qty pick_qty,
                               a.status,
                               a.lgort location_no,
                               a.charg part_batch,
                               a.zzstage part_version
                          from ppsuser.sap_zsf_line a
                         where a.lddnum = '{0}'
                         order by a.sortid asc", strSapNo);
                
            }
            else if (strWHOutType.Equals("ZTL"))
            {
                sql = string.Format(@"
                                    select a.zdbnum     sap_no,
                                           a.sortid     line_item,
                                           a.matnr      part_no,
                                           a.maktx      part_desc,
                                           a.qty        qty,
                                           a.deal_qty   pick_qty,
                                           a.status,
                                           a.lgort_bc   location_no,
                                           a.charg_bc   part_batch,
                                           a.zzstage_bc part_version
                                      from ppsuser.sap_ztl_line a
                                     where a.zdbnum = '{0}'
                                     order by a.sortid asc", strSapNo);
                
            }
            else if (strWHOutType.Equals("ZBOMR"))
            {
                sql = string.Format(@"
                        select a.aufnr sap_no,
                               a.posnr line_item,
                               a.matnr part_no,
                               '' part_desc,
                               a.qty qty,
                               a.deal_qty pick_qty,
                               a.status,
                               a.lgort location_no,
                               a.charg part_batch,
                               a.zzstage part_version
                          from ppsuser.sap_zbomr_line a
                         where a.aufnr = '{0}'
                         order by a.posnr asc", strSapNo);
            }
            else
            {
                return null;
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
        public DataSet GetSNInfoDataTableDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select distinct customer_sn ,carton_no ,hold_flag 
                                              from ppsuser.g_sn_status 
                                             where customer_sn = '{0}' 
                                                or carton_no = '{1}'
                                 ", inputSno, inputSno);

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

        public DataSet GetEDINONEDITypeDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select distinct carton_no 
                                              from ppsuser.t_sn_status 
                                             where carton_no = '{0}' ; 
                                              select distinct carton_no 
                                              from nonedipps.t_sn_status 
                                             where carton_no = '{1}'", inputSno, inputSno);
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


        public DataSet GetLocationDataTableDAL(string inputSno,string sap_no)
        {
            string sql = string.Empty;
            sql = string.Format(@"select location_id,location_no 
                                   from ppsuser.t_sn_status 
                                   where carton_no = '{0}'  and  (pps_wh_no ,pps_plant) in (select distinct LGORT_BC,WERKS_BC from  
                                 ppsuser.sap_ztl_line   where  ZDBNUM='{1}' )", inputSno, sap_no);

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
        public DataSet GetWaitFBMESCartonDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select distinct  carton_no
                                  from (select distinct carton_no
                                          from ppsuser.t_sn_status a
                                         where a.shipment_id='{0}'
                                           and wc = 'WP'
                                        union
                                        select distinct carton_no
                                          from nonedipps.t_sn_status b
                                         where b.shipment_id='{0}'
                                           and wc = 'WP') aa", inputSno);

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

        public string WmsOInsertCartonBySP(  string strSapNo, string strCarton ,
                                             string strUserNo, string strOriPlant, string strOriSloc,
                 string strTarPlant, string strTarSloc,string strTarLocationNo, out string RetMsg,out string strLBL,  out string newPalletNo)
        {
            object[][] procParams = new object[11][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insapno", strSapNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "snorcartonno", strCarton };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "oriPlant", strOriPlant };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "oriSloc", strOriSloc };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "tarPlant", strTarPlant };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "tarSloc", strTarSloc };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "tarLocationNo", strTarLocationNo };
            procParams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "empno", strUserNo };
            procParams[8] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            procParams[9] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "strlbl", "" };
            procParams[10] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "newPalletNo", "" };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.sp_wmstrance_insertcarton", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                strLBL = "00/00";
                newPalletNo = "";
                return "NG";
            }
            RetMsg = dt.Rows[0]["errmsg"].ToString();
            strLBL = dt.Rows[0]["strlbl"].ToString();
            newPalletNo= dt.Rows[0]["newPalletNo"].ToString();
            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else if (RetMsg.StartsWith("FINISH"))
            {
                return "FINISH";
            }
            else
            {
                return "NG";
            }

        }

        public string WmsOCheckCartonBySP(string strWHOutType, string strPickSapNOA, out string strPickSapNO, string strSapNo, string strCarton, string strUserNo,out string outorgLocationNo, out string RetMsg, out string strLBL)
        {
            object[][] procParams = new object[9][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwhouttype", strWHOutType };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpicksapno", strPickSapNOA };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "picksapno", "" };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insapno", strSapNo };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "snOrCartonno", strCarton };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "empno", strUserNo };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outlocation", "" };
            procParams[7] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            procParams[8] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "strlbl", "" };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.sp_wmstrance_check", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                strLBL = "00/00";
                strPickSapNO = "";
                outorgLocationNo = "";
                return "NG";
            }
            RetMsg = dt.Rows[0]["errmsg"].ToString();
            strLBL = dt.Rows[0]["strlbl"].ToString();
            strPickSapNO = dt.Rows[0]["picksapno"].ToString();
            outorgLocationNo= dt.Rows[0]["outlocation"].ToString();
            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else if (RetMsg.StartsWith("FINISH"))
            {
                return "FINISH";
            }
            else
            {
                return "NG";
            }

        }

        public string WmsOCheckTransferEDI(string strCarton, string orgLocationNo, string tarLocationNo, out string RetMsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "INPUTDATA", strCarton };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "orgLocationNo", orgLocationNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "tarLocationNo", tarLocationNo };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.t_check_is_transfer", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                return "NG";
            }
            RetMsg = dt.Rows[0]["TMES"].ToString();
            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public string WmsOTransferEDI(string strCarton, string orgLocationNo, string tarLocationNo, out string RetMsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", strCarton };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "orgLocationNo", orgLocationNo };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "tarLocationNo", tarLocationNo };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.t_change_location", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                return "NG";
            }
            RetMsg = dt.Rows[0]["TMES"].ToString();
            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }


        public string WmsOUnlockComputerBySP(string strSapId, string strWHOutType,out string RetMsg)
        {
            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insapno", strSapId };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwhouttype", strWHOutType };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.SP_WMSO_UNLOCKCOMPUTERNAME", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                return "NG";
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        public Boolean WMSOBackUpWebServieLogBySQL(string strguid, string serverip, string url, string insn, string result, string strempno, string strinterfacename, out string RetMsg)
        {
            object[][] sqlparams = new object[7][];
            string sql = string.Empty;

            sql = string.Format(@"
                               insert into ppsuser.t_wmso_meswebservice
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
        public Boolean WMSOUpdateCartonStatusBySP(string strPalletNO, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incartonno", strPalletNO };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.sp_wmso_fbmesupdatecarton", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                
                RetMsg = e1.ToString();
                return false;
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        public Boolean WMSODELETESN2BySP(string strSN, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSN };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "retmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.sp_wmso_deletesn2", procParams).Tables[0];
            }
            catch (Exception e1)
            {

                RetMsg = e1.ToString();
                return false;
            }

            RetMsg = dt.Rows[0]["retmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        public string PPSGetbasicparameterBySP(string strParaType,out string strParaValue, out string RetMsg)
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


        public Boolean InsertSapWebLogBySQL(string strGUID, string strServerIP, string strParaValue, string strSapNo, string strPickSapNo, string strRequestModel, string strUserNo ,out string RetMsg)
        {
         //   string sql = string.Empty;
         //   sql = string.Format(@"
         //   insert into ppsuser.t_wmso_sapwebservice
         //     (msg_id, strserverip, strurl, sap_no, pick_sap_no, req_json, emp_no)
         //   values
         //     ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}' )
         //", strGUID, strServerIP, strParaValue, strSapNo, strPickSapNo, strRequestModel, strUserNo);


            object[][] sqlparams = new object[7][];
            string sql = string.Empty;

            sql = string.Format(@"
                               insert into ppsuser.t_wmso_sapwebservice
                                  (msg_id, strserverip, strurl, sap_no, pick_sap_no, req_json, emp_no)
                                values
                                  (:inguid, :inserverip, :inurl,:insapno,:inpicksapno, :inreqjson , :inempno)
                                     ");

            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strGUID };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inserverip", strServerIP };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inurl", strParaValue };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insapno", strSapNo };
            sqlparams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpicksapno", strPickSapNo };
            sqlparams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inreqjson", strRequestModel };
            sqlparams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", strUserNo };
            DataSet dataSet = new DataSet();
            try
            {
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

        public DataSet GetRequestSapModelBySQL(string strWHOutType,string strSapNo, string strPickSapNo)
        {
            string sql = string.Empty;
            if (strWHOutType.Equals("ZTL"))
            {
                #region 20200422new  //2020918charg_bc =  charg_br   // a.charg_br charg_br,
                sql = string.Format(@"select a.zdbnum zdbnum,
                                           a.sortid sortid,
                                           a.matnr matnr,
                                           a.lgort_bc lgort_bc,
                                           decode(c.batch_no, null, '', c.batch_no) charg_bc,
                                           a.zzstage_bc zzstage_bc,
                                           a.lgort_br lgort_br,
                                           decode(c.batch_no, null, '', c.batch_no) charg_br,
                                           a.zzstage_br zzstage_br,
                                           '2' zsyflg,
                                           count(distinct c.serial_number) menge
                                      from ppsuser.sap_ztl_line a
                                      left join ppsuser.t_sn_status_ztl c
                                        on a.zdbnum = c.delivery_no
                                       and a.matnr = c.part_no
                                     where a.zdbnum = '{0}'
                                     group by a.zdbnum,
                                              a.sortid,
                                              a.matnr,
                                              a.lgort_bc,
                                              decode(c.batch_no, null, '', c.batch_no),
                                              a.zzstage_bc,
                                              a.lgort_br,
                                              a.charg_br,
                                              a.zzstage_br
                                     order by a.sortid asc
                                    ", strSapNo);
                #endregion
            }
            else
            {
                return null;
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


        public DataSet GetStockInfoBySQL(string strPartNo,string strBatchNo,string strSapLocationNo)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(strBatchNo))
            {
                sql = string.Format(@"select a.part_no, 
                                 decode(b.custom_sn ,null , a.location_no ,'' , a.location_no,a.location_no ||'*'|| b.trolley_no||'*'|| b.level_no ||'*'|| b.pointno )  location_no,
                                    count(distinct a.carton_no) cartoncount
                                  from ppsuser.t_sn_status a
                                  left join ppsuser.t_trolley_sn_status b
                                   on a.customer_sn =b.custom_sn
                                 where a.part_no = '{0}'
                                   and wc = 'W0'
                                   and location_id in
                                       (select location_id
                                          from ppsuser.wms_location
                                         where warehouse_id in (select warehouse_id
                                                                  from ppsuser.wms_warehouse
                                                                 where sap_wh_no = '{1}'))
                                 group by part_no, decode(b.custom_sn ,null , a.location_no ,'' , a.location_no,a.location_no ||'*'|| b.trolley_no||'*'|| b.level_no ||'*'|| b.pointno)
                                union
                                select part_no, location_no, count(distinct a.carton_no) cartoncount
                                  from nonedipps.t_sn_status a
                                 where a.part_no = '{0}'
                                   and wc = 'W0'
                                   and location_id in
                                       (select location_id
                                          from nonedipps.wms_location
                                         where warehouse_id in (select warehouse_id
                                                                  from nonedipps.wms_warehouse
                                                                 where sap_wh_no = '{1}'))
                                 group by part_no, location_no
                                 order by location_no asc, part_no asc , batch_no asc
                    ", strPartNo, strSapLocationNo);
            }
            else
            {
                sql = string.Format(@"select part_no,
                                           batch_no, 
                                        decode(b.custom_sn ,null , a.location_no ,'', a.location_no ,a.location_no ||'*'|| b.trolley_no||'*'|| b.level_no ||'*'|| b.pointno) location_no,
                                           count(distinct a.carton_no) cartoncount
                                      from ppsuser.t_sn_status a
                                      left join ppsuser.t_trolley_sn_status b
                                   on a.customer_sn =b.custom_sn
                                     where a.part_no = '{0}'
                                       and a.batch_no = '{1}'
                                       and wc = 'W0'
                                       and location_id in
                                           (select location_id
                                              from ppsuser.wms_location
                                             where warehouse_id in (select warehouse_id
                                                                      from ppsuser.wms_warehouse
                                                                     where sap_wh_no = '{2}'))
                                     group by part_no, batch_no, decode(b.custom_sn ,null , a.location_no ,'', a.location_no ,a.location_no ||'*'|| b.trolley_no||'*'|| b.level_no ||'*'|| b.pointno)
                                    union
                                    select part_no,
                                           batch_no, location_no,
                                           count(distinct a.carton_no) cartoncount
                                      from nonedipps.t_sn_status a
                                     where a.part_no = '{0}'
                                       and a.batch_no = '{1}'
                                       and wc = 'W0'
                                       and location_id in
                                           (select location_id
                                              from nonedipps.wms_location
                                             where warehouse_id in (select warehouse_id
                                                                      from nonedipps.wms_warehouse
                                                                     where sap_wh_no = '{2}'))
                                     group by part_no, batch_no, location_no
                                      order by location_no asc, part_no asc , batch_no asc
                                    ", strPartNo,  strBatchNo,  strSapLocationNo);
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


        public DataSet GetOriPalletStockInfoBySQL(string strSapId, string strWarehousrID)
        {
            string[] ss = strWarehousrID.Split('^');
            string strPlantOri = ss[0].ToString();
            string strWhOri = ss[1].ToString();
            string sql = string.Empty;

            sql = string.Format(@"
                                    select a.part_no,
                                           a.batch_no,
                                           a.location_no ,
                                           count(distinct a.carton_no) cartoncount
                                      from ppsuser.t_sn_status a
                                     where a.part_no in
                                           (select part_no
                                              from (
                                                    select b.zdbnum   as sap_no,
                                                           b.matnr    as part_no,
                                                           b.charg_bc charg
                                                      from ppsuser.sap_ztl_line b
                                                     where status <> 'FP') aa
                                             where aa.sap_no = '{0}')
                                       and wc = 'W0'
                                       and a.plant='{1}' and a.sloc='{2}'
                                     group by a.part_no, a.batch_no,  a.location_no
                                    union
                                    select part_no,
                                           batch_no,
                                           location_no,
                                           count(distinct a.carton_no) cartoncount
                                      from nonedipps.t_sn_status a
                                     where a.part_no in
                                           (select part_no
                                              from (
                                                    select b.zdbnum   as sap_no,
                                                           b.matnr    as part_no,
                                                           b.charg_bc charg
                                                      from ppsuser.sap_ztl_line b
                                                     where status <> 'FP'
                                                    ) aa
                                             where aa.sap_no = '{0}')
                                       and wc = 'W0'
                                       and a.plant='{1}' and a.sloc='{2}'
                                     group by part_no, batch_no, location_no
                                     order by location_no asc, part_no asc , batch_no asc
                                    ", strSapId,strPlantOri,strWhOri);

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


        public DataSet GetPalletStockInfoBySQL(string strSapId,string strWarehousrID)
        {
            string[] ss = strWarehousrID.Split('^');
            string strPlantTar = ss[0].ToString();
            string strSlocTar = ss[1].ToString();
            string sql = string.Format(@"
                                    select part_no,batch_no,location_no ,count(distinct carton_no) cartoncount
from  ppsuser.t_sn_status  where plant='{1}'  and sloc='{2}'  and carton_no in (select carton_no from ppsuser.t_sn_status_ztl where shipment_id='{0}')
group by part_no, batch_no, location_no
union
select part_no,batch_no,location_no ,count(distinct carton_no) cartoncount
from  nonedipps.t_sn_status  where plant='{1}'  and sloc='{2}'  and carton_no in (select carton_no from ppsuser.t_sn_status_ztl where shipment_id='{0}')
group by part_no, batch_no, location_no
                                     order by location_no asc, part_no asc , batch_no asc
                                    ", strSapId,strPlantTar, strSlocTar);
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


        public Boolean UpdateSapWebResultBySQL(string strGUID, string strResult, out string RetMsg)
        {
            object[][] sqlparams = new object[2][];
            string sql = string.Empty;
            sql = string.Format(@"
                        update ppsuser.t_wmso_sapwebservice a
                           set a.strresult = :inresult
                         where a.msg_id = :inguid ");
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strGUID };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inresult", strResult };

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

        

        public DataSet GetUnPICKSAPINFOBySQL(string strStime, string strEtime)
        {
            string sql = string.Empty;
            sql = string.Format(@" select * from (
            select  (select max(UDT) from ppsuser.sap_ztl_line where zdbnum=a.zdbnum ) as picktime, a.zdbnum as sap_no,a.status
                                                 from  ppsuser.sap_ztl a
                                                 where a.zdbnum not in
                                                (select cc.sap_no
                                                  from ppsuser.t_wmso_sapwebservice cc
                                                 where cc.status = 'S')
                                                 and  exists
                       (select distinct LGORT_BR from ppsuser.sap_ztl_line   where ZDBNUM=a.ZDBNUM and LGORT_BR in(select SAP_WH_NO
                       from sajet.wms_warehouse a
                       where a.enabled = 'Y'
                       and a.warehouse_no <>'SYS' ))
                                                 )
                                                 where picktime > to_date('{0}', 'yyyy-mm-dd')
                                                 and picktime <= to_date('{1}', 'yyyy-mm-dd')
                                       ", strStime, strEtime);

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
        public DataSet GetUnPICKSAPALLINFOBySQL(string strStime, string strEtime)
        {
            string sql = string.Empty;
            sql = string.Format(@" select * from (
 select   (select max(UDT) from ppsuser.sap_ztl_line where zdbnum=a.zdbnum ) as picktime, 'PPSZTLPosting' whouttype，a.zdbnum as sap_no,'P1'||a.zdbnum as pick_sap_no,'Y' fb_mes_status
                                                 from  ppsuser.sap_ztl a
                                                 where  exists
                       (select distinct LGORT_BR from ppsuser.sap_ztl_line   where ZDBNUM=a.ZDBNUM and LGORT_BR in(select SAP_WH_NO
                       from sajet.wms_warehouse a
                       where a.enabled = 'Y'
                       and a.warehouse_no <>'SYS' ))
                                                 )
                                                 where picktime > to_date('{0}', 'yyyy-mm-dd')
                                                 and picktime <= to_date('{1}', 'yyyy-mm-dd')
                            ", strStime, strEtime);

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
        public DataSet GetComputerNameOfSAPNOBySQL(string strSAPNO)
        {
            string sql = string.Empty;
            sql = string.Format(@" 
                                select a.computer_name
                                  from ppsuser.sap_zbomr a
                                 where a.aufnr = '{0}'
                                union
                                select b.computer_name
                                  from ppsuser.sap_ztl b
                                 where b.zdbnum = '{0}'
                                union
                                select c.computer_name
                                  from ppsuser.sap_zsf c
                                 where c.lddnum = '{0}'
                            ", strSAPNO);

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


        public DataSet GetPickPrintInfoBySQL(string strPickSAPNO)
        {
            string sql = string.Empty;
            sql = string.Format(@" 
                                select aa.sap_no sapno,
                                       aa.pick_sap_no picksapno,
                                       aa.qty || '/' || aa.cartonqty pickqty,
                                       bb.qty totalqty
                                  from (select a.sap_no,
                                               a.pick_sap_no,
                                               sum(a.qty) qty,
                                               sum(a.carton) cartonqty
                                          from ppsuser.t_wmso_sapno_pick a
                                         where a.pick_sap_no = '{0}'
                                         group by a.sap_no, a.pick_sap_no) aa
                                  join (select aufnr sap_no, qty
                                          from ppsuser.sap_zbomr
                                        union
                                        select zdbnum sap_no, sum(qty) qty
                                          from ppsuser.sap_ztl_line
                                         group by zdbnum
                                        union
                                        select lddnum sap_no, sum(qty) qty
                                          from ppsuser.sap_zsf_line
                                         group by lddnum) bb
                                    on aa.sap_no = bb.sap_no
                            ", strPickSAPNO);

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

        public DataSet GetPickPrintInfo2BySQL(string strPickSAPNO)
        {
            string sql = string.Empty;
            sql = string.Format(@" 
                                select pick_sap_no,
                                   part_no ictpn,
                                   bb.custpart mpn,
                                   to_char(count(distinct customer_sn)) || '/' || to_char(count(distinct carton_no)) partpickqty
                              from (select customer_sn,
                                           carton_no,
                                           part_no,
                                           work_order,
                                           batch_no,
                                           shipment_id    sap_no,
                                           pick_pallet_no pick_sap_no
                                      from ppsuser.t_sn_status a
                                     where a.pick_pallet_no = '{0}'
                                    union
                                    select customer_sn,
                                           carton_no,
                                           part_no,
                                           work_order,
                                           batch_no,
                                           shipment_id    sap_no,
                                           pick_pallet_no pick_sap_no
                                      from ppsuser.t_sn_status_zc a
                                     where a.pick_pallet_no = '{0}'
                                       and create_by = 'WMSO'
                                    union
                                    select customer_sn,
                                           carton_no,
                                           part_no,
                                           work_order,
                                           batch_no,
                                           shipment_id    sap_no,
                                           pick_pallet_no pick_sap_no
                                      from nonedipps.t_sn_status a
                                     where a.pick_pallet_no = '{0}'
                                    union
                                    select customer_sn,
                                           carton_no,
                                           part_no,
                                           work_order,
                                           batch_no,
                                           shipment_id    sap_no,
                                           pick_pallet_no pick_sap_no
                                      from nonedipps.t_sn_status_zc a
                                     where a.pick_pallet_no = '{0}'
                                       and create_by = 'WMSO') aa
                              join (select part, custpart
                                      from pptest.oms_partmapping
                                    union
                                    select part, custpart
                                      from nonedioms.oms_partmapping) bb
                                on aa.part_no = bb.part
                             where aa.sap_no in (select a.aufnr sap_no
                                                   from ppsuser.sap_zbomr a
                                                  where a.aufnr = substr('{0}',3)
                                                 union
                                                 select a.zdbnum sap_no
                                                   from ppsuser.sap_ztl a
                                                  where a.zdbnum = substr('{0}',3)
                                                 union
                                                 select a.lddnum sap_no
                                                   from ppsuser.sap_zsf a
                                                  where a.lddnum = substr('{0}',3))
                             group by pick_sap_no, part_no, bb.custpart
                             order by pick_sap_no, part_no, bb.custpart
                            ", strPickSAPNO);

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

        public DataSet GetPickSAPNOBySQL(string strSN)
        {
            string sql = string.Empty;
            sql = string.Format(@"select pick_pallet_no picksapno
                                      from ppsuser.t_sn_status
                                     where (customer_sn = '{0}' or carton_no = '{0}')
                                    union
                                    select pick_pallet_no picksapno
                                      from ppsuser.t_sn_status_zc
                                     where idt in (select max(idt) idt
                                                     from ppsuser.t_sn_status_zc
                                                    where (customer_sn = '{0}' or carton_no = '{0}'))
                                       and (customer_sn = '{0}' or carton_no = '{0}')

                                    union
                                    select pick_pallet_no picksapno
                                      from nonedipps.t_sn_status
                                     where (customer_sn = '{0}' or carton_no = '{0}')
                                    union
                                    select pick_pallet_no picksapno
                                      from nonedipps.t_sn_status_zc
                                     where idt in (select max(idt) idt
                                                     from nonedipps.t_sn_status_zc
                                                    where (customer_sn = '{0}' or carton_no = '{0}'))
                                       and (customer_sn = '{0}' or carton_no = '{0}')
                                 ", strSN);

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


        public DataSet GetCartonPartInfoBySQL(string strCartonNo)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 select min(a.part_no ) as part_no
                                  from ppsuser.t_sn_status a
                                     where a.carton_no = '{0}'
                                 union
                                select min(a.part_no ) as part_no
                                  from nonedipps.t_sn_status a
                                     where a.carton_no = '{0}'
                                     ", strCartonNo);

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

        public DataSet GetSAPNOandPICKSAPNOListBySQL(string strCartno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status a
                                         where a.carton_no  = '{0}'
                                         
                                         union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status_zc a
                                         where a.carton_no  = '{0}'
                                           and create_by = 'WMSO'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status a
                                         where a.carton_no  = '{0}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status_zc a
                                         where a.carton_no  = '{0}'
                                           and create_by = 'WMSO'" , strCartno);

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


        public DataSet GetSAPNOCartonListBySQL(string strSAPNO,string strPICKSAPNO)
        {
            string sql = string.Empty;
            if (strPICKSAPNO.Equals(" - ALL-")) 
            {
                sql = string.Format(@"
                               select *
                                  from (select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status a
                                         where a.shipment_id  = '{0}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status_zc a
                                         where a.shipment_id  = '{0}'
                                           and create_by = 'WMSO'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status a
                                         where a.shipment_id  = '{0}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status_zc a
                                         where a.shipment_id  = '{0}'
                                           and create_by = 'WMSO') aa
                                 where aa.sap_no in (select a.aufnr sap_no
                                                       from ppsuser.sap_zbomr a
                                                      where a.aufnr  = '{0}'
                                                     union
                                                     select a.zdbnum sap_no
                                                       from ppsuser.sap_ztl a
                                                      where a.zdbnum  = '{0}'
                                                     union
                                                     select a.lddnum sap_no
                                                       from ppsuser.sap_zsf a
                                                      where a.lddnum  = '{0}')
                                 order by sap_no, pick_sap_no, carton_no
                                     ", strSAPNO);

            }
            else 
            {
                sql = string.Format(@"
                                 select *
                                  from (select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status_zc a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}'
                                           and create_by = 'WMSO'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status_zc a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}'
                                           and create_by = 'WMSO') aa
                                 where aa.sap_no in (select a.aufnr sap_no
                                                       from ppsuser.sap_zbomr a
                                                      where a.aufnr  = '{0}'
                                                     union
                                                     select a.zdbnum sap_no
                                                       from ppsuser.sap_ztl a
                                                      where a.zdbnum  = '{0}'
                                                     union
                                                     select a.lddnum sap_no
                                                       from ppsuser.sap_zsf a
                                                      where a.lddnum  = '{0}')
                                 order by sap_no, pick_sap_no, carton_no
                                     ", strSAPNO, strPICKSAPNO);

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

        public DataSet ChangeCSNtoCartonBySQL(string strSN)
        {
            string strSQL = string.Format(@"
                                    select customer_sn ,carton_no ,wc
                                      from ppsuser.t_sn_status a
                                     where a.customer_sn = '{0}' 
                                    union
                                    select customer_sn ,carton_no ,wc
                                      from ppsuser.t_sn_status_zc a
                                     where a.customer_sn = '{0}' 
                                       and create_by = 'WMSO'
                                    union
                                    select customer_sn ,carton_no ,wc
                                      from nonedipps.t_sn_status a 
                                     where a.customer_sn = '{0}' 
                                    union
                                    select customer_sn ,carton_no ,wc
                                      from nonedipps.t_sn_status_zc a 
                                     where a.customer_sn = '{0}' 
                                       and create_by = 'WMSO'
                                     ", strSN);


            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(strSQL);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
        public DataSet CheckCartonInSAPNOBySQL(string strSAPNO, string strPICKSAPNO, string strSN)
        {
            string sql = string.Empty;
            if (strPICKSAPNO.Equals("-ALL-"))
            {
                sql = string.Format(@"
                               select *
                                  from (select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status a
                                         where a.shipment_id  = '{0}' and a.carton_no ='{1}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status_zc a
                                         where a.shipment_id  = '{0}'  and a.carton_no ='{1}'
                                           and create_by = 'WMSO'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status a  
                                         where a.shipment_id  = '{0}' and a.carton_no ='{1}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status_zc a  
                                         where a.shipment_id  = '{0}' and a.carton_no ='{1}'
                                           and create_by = 'WMSO') aa
                                 where aa.sap_no in (select a.aufnr sap_no
                                                       from ppsuser.sap_zbomr a
                                                      where a.aufnr  = '{0}'
                                                     union
                                                     select a.zdbnum sap_no
                                                       from ppsuser.sap_ztl a
                                                      where a.zdbnum  = '{0}'
                                                     union
                                                     select a.lddnum sap_no
                                                       from ppsuser.sap_zsf a
                                                      where a.lddnum  = '{0}')
                                 order by sap_no, pick_sap_no, carton_no
                                     ", strSAPNO, strSN) ;

            }
            else
            {
                sql = string.Format(@"
                                 select *
                                  from (select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}' and  a.carton_no ='{2}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from ppsuser.t_sn_status_zc a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}' and  a.carton_no ='{2}'
                                           and create_by = 'WMSO'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}' and  a.carton_no ='{2}'
                                        union
                                        select carton_no, shipment_id sap_no, pick_pallet_no pick_sap_no
                                          from nonedipps.t_sn_status_zc a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}' and  a.carton_no ='{2}'
                                           and create_by = 'WMSO') aa
                                 where aa.sap_no in (select a.aufnr sap_no
                                                       from ppsuser.sap_zbomr a
                                                      where a.aufnr  = '{0}'
                                                     union
                                                     select a.zdbnum sap_no
                                                       from ppsuser.sap_ztl a
                                                      where a.zdbnum  = '{0}'
                                                     union
                                                     select a.lddnum sap_no
                                                       from ppsuser.sap_zsf a
                                                      where a.lddnum  = '{0}')
                                 order by sap_no, pick_sap_no, carton_no
                                     ", strSAPNO, strPICKSAPNO, strSN);

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

        public DataSet GetWarehouseNOTraBySQL(string strSapId,string strDBtype)
        {
            string sql = string.Empty;
            if(strDBtype== "EDI")
            sql = string.Format(@"select * from   ppsuser.wms_warehouse   where (sap_wh_no,plant) 
                                 in ( select distinct LGORT_BR,WERKS_BC from  
                                 ppsuser.sap_ztl_line   where  ZDBNUM='{0}')", strSapId);
            else
                sql = string.Format(@"select * from   nonedipps.wms_warehouse   where (sap_wh_no,plant) 
                                 in ( select distinct LGORT_BR,WERKS_BC from  
                                 ppsuser.sap_ztl_line   where  ZDBNUM='{0}')", strSapId);
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
        public DataSet GetDBTypeBySQL(string strPartNo1)
        {
            string sql = string.Empty;

            sql = string.Format(@"select *  from pptest.oms_partmapping   where part = '{0}'", strPartNo1);

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
        public DataSet GetPartNoBySQL(string strSapId)
        {
            string sql = string.Empty;

            sql = string.Format(@"select distinct matnr from  ppsuser.sap_ztl_line   where  ZDBNUM='{0}'", strSapId);

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
