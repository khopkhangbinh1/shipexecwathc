using Newtonsoft.Json;
using OperationWCF;
using Oracle.ManagedDataAccess.Client;
using RollbackDN.Wcf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RollbackDN.UPSCancel
{
    class CheckUPS_shipment
    {

        public string UPSCheck(string shipmentId)
        {
            string msg = "";
            string sql = @"select * from t_shipment_info"
                           + "    where shipment_type='DS' " +
                           "and type='PARCEL' and carrier_name like '%UPS%' "
                           //+ " and shipment_id=:shipment_id";
                           + " and SHIPMENT_ID in (select DISTINCT SHIPMENT_ID from PPSUSER.T_SHIPMENT_PALLET where SHIPMENT_ID=:shipment_id and PICK_CARTON>0) ";//已经pick才要call void api
            object[][] para = new object[1][];
            para[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipment_id", shipmentId };
            DataTable dt = new DataTable();
            dt = ClientUtils.ExecuteSQL(sql, para).Tables[0];
            if (dt.Rows.Count > 0)
            {
                msg = "OK";
            }
            return msg;
        }
        public string SendShipmentCancel(string shipmentId)
        {
            string msg = "";
            try
            {
                string linksrv = "";
                ICTConnectionDB cndb = new ICTConnectionDB();
                linksrv = cndb.IctUrlFromDB();
                List<int> ls = new List<int>();
                DataTable dt1 = new DataTable();
                Credentials Credentials = new Credentials();
                string region = "";

                string sql1 = @"SELECT REGION FROM PPSUSER.T_SHIPMENT_INFO WHERE SHIPMENT_ID = :shipment_id";
                object[][] para1 = new object[1][];
                para1[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipment_id", shipmentId };
                dt1 = ClientUtils.ExecuteSQL(sql1, para1).Tables[0];

                if (dt1.Rows.Count == 0)
                {
                    msg = "Region Shipment 异常！";
                }
                else
                {
                    region = dt1.Rows[0]["REGION"].ToString();

                }
                DataSet Access = GetClientAccess("UPS_ACCESS");
                Credentials.ClientAccessCredentials = JsonConvert.DeserializeObject<ClientAccessCredentials>(Access.Tables[0].Rows[0]["PARA_VALUE"].ToString());

                if (region.Equals("PAC"))
                {
                    DataSet user = GetUserContext("UPS_CONTEXT_PAC");
                    Credentials.UserContext = JsonConvert.DeserializeObject<UserContext>(user.Tables[0].Rows[0]["PARA_VALUE"].ToString());
                }
                else
                {
                    DataSet user = GetUserContext("UPS_CONTEXT");
                    Credentials.UserContext = JsonConvert.DeserializeObject<UserContext>(user.Tables[0].Rows[0]["PARA_VALUE"].ToString());
                }

                string sql = @"SELECT DISTINCT globalmsn from PPSUSER.T_UPS_RAWDATA
                                where tracking_no in
                                (SELECT distinct T.TRACKING_NO trackingNo
                                                          FROM ppsuser.t_allo_trackingno t
                                                         WHERE     t.shipment_id IN (SELECT shipment_id
                                                                   FROM ppsuser.t_shipment_info
                                                              WHERE  carrier_name LIKE '%UPS%'
                                                                    AND TYPE = 'PARCEL')
                                AND shipment_id = :shipment_id)";
                object[][] para = new object[1][];
                para[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipment_id", shipmentId };
                DataTable dt = new DataTable();
                dt = ClientUtils.ExecuteSQL(sql, para).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    msg = "在Database未找到 此集货单之globalmsn";
                }
                else
                {
                    ls = dt.AsEnumerable().Select(x => int.Parse(x["globalmsn"].ToString())).ToList();
                    string data = JsonConvert.SerializeObject(new { GlobalMsns = ls, Credentials.ClientAccessCredentials, Credentials.UserContext });
                    if (ls.Count == 0)
                    {
                        msg = "未找到此集货单号之 GlobalMsns 号!";
                    }
                    else
                    {
                        CarrierWCF.Wcf.IICTToCarrierService WS = HttpChannel.Get<CarrierWCF.Wcf.IICTToCarrierService>(linksrv);
                        string dataout = WS.Void(data);
                        msg = dataout;
                    }
                }
            }
            catch (Exception ex1)
            {
                msg = ex1.Message;
            }
            return msg;
        }
        public string SendShipmentCancel(List<int> lstGlbMSN)
        {
            string msg = "";
            try
            {
                ICTConnectionDB cndb = new ICTConnectionDB();
                string linksrv = cndb.IctUrlFromDB();
                if (lstGlbMSN.Count == 0)
                    msg = "在Database未找到 此集货单之globalmsn";
                else
                {
                    CarrierWCF.Wcf.IICTToCarrierService WS = HttpChannel.Get<CarrierWCF.Wcf.IICTToCarrierService>(linksrv);
                    msg = WS.Void(JsonConvert.SerializeObject(new { GlobalMsns = lstGlbMSN }));
                }
            }
            catch (Exception ex1)
            {
                msg = ex1.Message;
            }
            return msg;
        }
        public bool CheckUPSEnable()
        {
            //string msg = "";
            string sql = @"select ENABLED from PPSUSER.T_BASICPARAMETER_INFO where para_type='UPS_URL'  and ENABLED='Y'";
            DataTable dt = new DataTable();
            dt = ClientUtils.ExecuteSQL(sql).Tables[0];
            //if (dt.Rows.Count > 0)
            //{ 
            //    msg = "尚未设定UPS Carrier开关";
            //}
            //else
            //{
            //    msg = dt.Rows[0]["ENABLED"].ToString();
            //}
            return (dt.Rows.Count > 0);
        }
        //public ExecuteResult getVoidRequest(string shipment_id, string region, out VoidRequeset VoidRequeset)
        //{
        //    ExecuteResult exeRes = new ExecuteResult();
        //    DataTable dt = new DataTable();
        //    VoidRequeset = new VoidRequeset();
        //    List<int> ls = new List<int>();
        //    string msg = "";

        //    DataSet Access = GetClientAccess("UPS_ACCESS");
        //    VoidRequeset.ClientAccessCredentials = JsonConvert.DeserializeObject<ClientAccessCredentials>(Access.Tables[0].Rows[0]["PARA_VALUE"].ToString());

        //    if (region.Equals("PAC"))
        //    {
        //        DataSet user = GetUserContext("UPS_CONTEXT_PAC");
        //        VoidRequeset.UserContext = JsonConvert.DeserializeObject<UserContext>(user.Tables[0].Rows[0]["PARA_VALUE"].ToString());
        //    }
        //    else
        //    {
        //        DataSet user = GetUserContext("UPS_CONTEXT");
        //        VoidRequeset.UserContext = JsonConvert.DeserializeObject<UserContext>(user.Tables[0].Rows[0]["PARA_VALUE"].ToString());
        //    }
        //    string sql = @"SELECT DISTINCT globalmsn from PPSUSER.T_UPS_RAWDATA
        //                        where tracking_no in
        //                        (SELECT distinct T.TRACKING_NO trackingNo
        //                                                  FROM ppsuser.t_allo_trackingno t
        //                                                 WHERE     t.shipment_id IN (SELECT shipment_id
        //                                                           FROM ppsuser.t_shipment_info
        //                                                      WHERE  carrier_name LIKE '%UPS%'
        //                                                            AND TYPE = 'PARCEL')
        //                        AND shipment_id = :shipment_id)";
        //    object[][] para = new object[1][];
        //    para[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipment_id", shipment_id };
        //    dt = ClientUtils.ExecuteSQL(sql, para).Tables[0];

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        ls = dt.AsEnumerable().Select(x => int.Parse(x["globalmsn"].ToString())).ToList();
        //        var data = JsonConvert.SerializeObject(new { GlobalMsns = ls, VoidRequeset.ClientAccessCredentials, VoidRequeset.UserContext });
        //        if (ls.Count == 0)
        //        {
        //            msg = "未找到此集货单号之 GlobalMsns 号!";
        //        }

        //    }
        //    else
        //    {
        //        exeRes.Status = false;
        //        exeRes.Message = "未能查询到Ups信息，请联系IT-PPS!";
        //    }
        //    return exeRes;
        //}
        public DataSet GetUserContext(string paraType)
        {
            DataSet data = new DataSet();
            string sql = string.Empty;
            sql = string.Format(@"SELECT PARA_VALUE from T_BASICPARAMETER_INFO where PARA_TYPE = '{0}' and ENABLED = 'Y' and rownum=1", paraType);
            try
            {
                data = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return data;
        }
        public DataSet GetClientAccess(string paraType)
        {
            DataSet data = new DataSet();
            string sql = string.Empty;
            sql = string.Format(@"SELECT PARA_VALUE from T_BASICPARAMETER_INFO where PARA_TYPE = '{0}' and ENABLED = 'Y' and rownum=1", paraType);
            try
            {
                data = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return data;
        }
    }
}
