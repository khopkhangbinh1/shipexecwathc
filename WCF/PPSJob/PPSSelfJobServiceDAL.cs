using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPSSelfJob
{
    class PPSSelfJobServiceDAL
    {
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

        public DataSet GetWMSNListBySQL(Int32 ichecktotalcount)
        {
            string sql = string.Empty;
            if (ichecktotalcount == 0 || string.IsNullOrEmpty(ichecktotalcount.ToString()))
            {
                sql = string.Format(@"select a.customer_sn
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
                sql = string.Format(@"select aa.customer_sn
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
            catch (Exception )
            {
                return null;
            }
            return dataSet;

        }


        public Boolean CheckMarinaServerUrlLogBySQL(string strguid, string strserverip, string strurl, string strSN, string strresult, string strempno, string strrequest, out string RetMsg)
        {
            object[][] sqlparams = new object[7][];
            string sql = string.Empty;
            sql = string.Format(@"
                               insert into ppsuser.t_wms_marinawebservice
                               (msg_id, strserverip, strurl, pallet_no,  req_json, res_json, emp_no,createby)
                             values
                               (:inguid, :inserverip, :inurl, :insn, :inrequest, :inresult, :inempno,'WMS')
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

        public Boolean InsertMarinaSNLogBySQL(string strguid, string strPallet,string strSN, string strOKTOSHIP, string strERRORCODE, string strERRORMESSAGE, out string RetMsg)
        {
            //(MSG_ID, PALLET_NO, CUSTOMER_SN, OKTOSHIP, ERRORCODE, ERRORMESSAGE)
            object[][] sqlparams = new object[6][];
            string sql = string.Empty;
            sql = string.Format(@"
                              INSERT INTO PPSUSER.T_WMS_MARINA_SN_INFO
                                      (MSG_ID, PALLET_NO, CUSTOMER_SN, OKTOSHIP, ERRORCODE, ERRORMESSAGE)
                                    VALUES 
                                      (:inguid,:inpalletno,:insn,:inoktoship, :inerrcode, :inerrmsg )");
            
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strguid };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPallet };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSN };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inoktoship", strOKTOSHIP };
            sqlparams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inerrcode", strERRORCODE };
            sqlparams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inerrmsg", strERRORMESSAGE };
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
    }
}
