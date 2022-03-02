using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPSToMESWcf
{
    class PPSToMESServiceDAL
    {

        public string ExecuteQHoldSNBySP(string strsn, string strqholdflag, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strsn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inqholdflag", strqholdflag };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = new DataSet();
            try
            {
                //ClientUtils.ServerUrl = "http://10.54.10.14:8090/WCF_RemoteObject";
                ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_QHOLDSN", procParams);
            }
            catch (Exception e)
            {
                RetMsg ="DB-ERROR:"+ e.ToString();
                return "NG";
            }
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
        public string ExecuteQHoldSNBySP2(string strguid,  out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strguid };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = new DataSet();
            try
            {
                //ClientUtils.ServerUrl = "http://10.54.10.14:8090/WCF_RemoteObject";
                ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_QHOLDSN2", procParams);
            }
            catch (Exception e)
            {
                RetMsg = "DB-ERROR:" + e.ToString();
                return "NG";
            }
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

        public string ExecuteUpdateSNDNBySP(string strsn, string strdn, string strdnline, string strworkorder, out string RetMsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strsn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "indn", strdn };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "indnline", strdnline };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inworkorder", strworkorder };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = new DataSet();
            try
            {
                //ClientUtils.ServerUrl = "http://10.54.10.14:8090/WCF_RemoteObject";
                ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_UPDATESNDN", procParams);
            }
            catch (Exception e)
            { 
                RetMsg = "DB-ERROR:" + e.ToString();
                return "NG";
            }
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

        public string ExecuteCheckTrolleyNoStatusBySP(string strTrolleyNo, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strTrolleyNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = new DataSet();
            try
            {
                //ClientUtils.ServerUrl = "http://10.54.10.14:8090/WCF_RemoteObject";
                ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_MESCHECKCARSTATUS", procParams);
            }
            catch (Exception e)
            {
                RetMsg = "DB-ERROR:" + e.ToString();
                return "NG";
            }
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



    }
}
