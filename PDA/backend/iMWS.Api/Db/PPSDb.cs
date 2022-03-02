using Dapper;
using iMWS.Api.Db;
using iMWS.Api.Models.PPSPick;
using System;
using System.Collections.Generic;
using System.Configuration;
using iMWS.Api.Models.Bean;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDb;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Web;
using ClientUtilsDll;

namespace iMWS.Api.Db
{
    public class PPSDb : DbDapperExtension
    {
        public PPSDb()
           : base(String.Format("User Id={0};password={1};{2}", "ppsuser", Encoding.UTF8.GetString(Convert.FromBase64String(ConfigurationManager.AppSettings["DBPwd"].ToString())),
               ConfigurationManager.AppSettings["CONNECTION_STR"].ToString()))
        //: base(ConfigurationManager.AppSettings["PPSUser"].ToString())
        {
        }


        public PPSPickProcModel InsertPalletPick(bool IsEntirety, string inPickPalletno, string Palletno, string SNOrCartonno, string Empno, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@inpickpalletno", inPickPalletno);
                p.Add("@pickpalletno", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                p.Add("@palletno", Palletno);
                p.Add("@snOrCartonno", SNOrCartonno);
                p.Add("@empno", Empno);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                p.Add("@strlbl", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute(IsEntirety ? "sp_pick_entirety_pick" : "SP_PICK_INSERTPALLETPICK2", p, commandType: CommandType.StoredProcedure, transaction: tra);
                PPSPickProcModel model = new PPSPickProcModel
                {
                    ErrMsg = p.Get<string>("@errmsg"),
                    PickPalletNo = p.Get<string>("@pickpalletno"),
                    Strlbl = p.Get<string>("@strlbl"),
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }

        public PPSbasicparameterModel PPSGetbasicparameterBySP(string strDBType, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@inparatype", strDBType);
                p.Add("@outparavalue", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute("ppsuser.sp_pps_getbasicparameter", p, commandType: CommandType.StoredProcedure, transaction: tra);
                PPSbasicparameterModel model = new PPSbasicparameterModel
                {
                    outparavalue = p.Get<string>("@outparavalue") ?? "",
                    ErrMsg = p.Get<string>("@errmsg")
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }

        public PPSMarinaFlagModel GetMarinaPackoutFlag(string strSN, string strStation, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@insn", strSN);
                p.Add("@instation", strStation);
                p.Add("@outmarinaflag", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);
                p.Add("@outpackoutflag", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                int i = DbCon.Execute("PPSUSER.sp_pps_marinapackoutcheck", p, commandType: CommandType.StoredProcedure, transaction: tra);
                PPSMarinaFlagModel model = new PPSMarinaFlagModel
                {
                    ErrMsg = p.Get<string>("@errmsg"),
                    outpackoutflag = p.Get<string>("@outpackoutflag"),
                    outmarinaflag = p.Get<string>("@outmarinaflag")
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }

        public bool CheckMarinaServer(string strSN, string g_MarinaSite, string g_MarinaUrl, out string OutRetmsg)
        {
            OutRetmsg = "";
            MarinaRequestModel inmodel = new MarinaRequestModel();
            inmodel.STATION_TYPE = "PPS";
            inmodel.SITE = g_MarinaSite;
            PPSSNModel vSN = new PPSSNModel();
            vSN.SN = strSN;

            string sql = @"select to_char(customer_sn) customer_sn, to_char(carton_no) carton_no
                                      from ppsuser.t_sn_status
                                     where customer_sn =:SN
                                        or carton_no =:SN
                                        or pallet_no = :SN
                                    union
                                    select custom_sn customer_sn, carton_no
                                      from ppsuser.t_trolley_sn_status
                                     where trolley_no = :SN";
            var p = Query<PPSSNOutModel>(sql, vSN);

            if (p == null || p.Count() == 0)
            {
                OutRetmsg = "输入非法无效的序号或者箱号，不做统计";
                return false;
            }
            List<Request> snList = new List<Request>();
            foreach (PPSSNOutModel snmodel in p)
            {
                Request sn = new Request();
                sn.SerialNumber = snmodel.customer_sn;
                snList.Add(sn);
            }
            inmodel.request = snList.ToArray();

            string strRequest = JsonConvert.SerializeObject(inmodel);

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            string strRsgMsg = string.Empty;
            string strMarinaResult = string.Empty;
            strMarinaResult = MarinaWebService(g_MarinaUrl, strRequest, out strRsgMsg);

            Boolean isIsertLogOK = true;
            string strRsgMsg0 = string.Empty;
            isIsertLogOK = CheckMarinaServerUrlLog(strGUID, ClientUtils.ServerUrl.Replace("http://", "").Replace(":8090/WCF_RemoteObject", ""), g_MarinaUrl, strSN, strRsgMsg, "PDA", strRequest, out strRsgMsg0);
            if (!isIsertLogOK)
            {
                OutRetmsg = strRsgMsg0;
                return false;
            };
            MarinaReturnModel outmodel = new MarinaReturnModel();
            try
            {
                outmodel = JsonConvert.DeserializeObject<MarinaReturnModel>(strRsgMsg);
            }
            catch (Exception e)
            {
                OutRetmsg = "序号对应MarinaServerCheck 返回异常，" + e.ToString(); ;
                return false;
            }
            foreach (var item in outmodel.response)
            {
                if (!item.OKtoShipwithInstalledOS.Equals("Y"))
                {
                    OutRetmsg = "序号对应MarinaServerCheck Fail,不得出货";
                    return false;
                }

            }


            return true;

        }

        public bool CheckPackoutLogic(string strSN, out string OutRetmsg)
        {
            OutRetmsg = "";
            PackoutLogicRequestModel inmodel = new PackoutLogicRequestModel();

            PPSSNModel vSN = new PPSSNModel();
            vSN.SN = strSN;
            string sql = @"select distinct customer_sn, a.product_name
                                      from ppsuser.mes_pallet_info a
                                      join ppsuser.mes_sn_status b
                                        on a.in_guid = b.in_guid
                                       and a.pallet_no = b.pallet_no
                                     where b.customer_sn in (select distinct customer_sn
                                                               from ppsuser.t_sn_status
                                                              where customer_sn =:SN
                                                                or carton_no = :SN
                                                                or pallet_no = :SN)
                                     order by customer_sn asc";
            var p = Query<PPSSNOutModel2>(sql, vSN);
            if (p == null || p.Count() == 0)
            {
                OutRetmsg = "输入非法无效的序号或者箱号，不做统计";
                return false;
            }
            string strProduct = p.ToArray()[0].product_name.ToString();
            List<requestsn> snList = new List<requestsn>();
            foreach (PPSSNOutModel2 pl in p)
            {
                requestsn sn = new requestsn();
                sn.SN = pl.customer_sn.ToString();
                snList.Add(sn);
            }
            inmodel.SN = snList.ToArray();

            string strRequest = JsonConvert.SerializeObject(inmodel);

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();



            string strResult0 = string.Empty;
            string strFGinMESWcf = string.Empty;
            string strResulterrmsg = string.Empty;
            var vba = PPSGetbasicparameterBySP(strProduct, null);
            strFGinMESWcf = vba.outparavalue;
            string strMESFuncName = "CheckPackOutLogic";
            string strResult = GetMesAPI(strProduct, strFGinMESWcf, strMESFuncName, strRequest);
            Boolean isIsertLogOK = true;
            string strRsgMsg0 = string.Empty;
            string strRequestUrl = HttpContext.Current.Request.Url.ToString();
            isIsertLogOK = CheckMarinaServerUrlLogBySQL(strGUID, strRequestUrl, strFGinMESWcf + strMESFuncName, strSN, strResult, "500", strRequest, out strRsgMsg0);
            if (!isIsertLogOK)
            {
                OutRetmsg = strRsgMsg0;
                return false;
            }

            try
            {
                var outmodel = JsonConvert.DeserializeObject<List<PackoutLogicReturnModel>>(strResult);

                foreach (var item in outmodel)
                {
                    if (!item.RESULT.Equals("OK"))
                    {
                        OutRetmsg = "序号对应PackoutLogicCheck Fail,不得出货";
                        return false;
                    }

                }
            }
            catch (Exception e)
            {
                OutRetmsg = "序号对应PackoutLogicCheck 返回异常，" + e.ToString(); ;
                return false;
            }
            return true;

        }
        public string GetMesAPI(string strProduct, string strUrl, string strMESFuncName, string strCarton)
        {

            try
            {
                if (strProduct.Equals("WATCH"))
                {
                    //MesApi ws = HttpChannel.Get<MesApi>(serviceUrl);
                    JSMESReference.MesApi ws = new JSMESReference.MesApi(strUrl);
                    if (strMESFuncName.Equals("GetMESStockInfo"))
                    {
                        return ws.GetMESStockInfo(strCarton);
                    }
                    else if (strMESFuncName.Equals("UpdateStockINStatus"))
                    {
                        return ws.UpdateStockINStatus(strCarton);
                    }
                    else if (strMESFuncName.Equals("GetMESPNInfo"))
                    {
                        return ws.GetMESPNInfo(strCarton);
                    }
                    else if (strMESFuncName.Equals("GetMaterialTransferInfo"))
                    {
                        return ws.GetMaterialTransferInfo(strCarton);
                    }
                    else if (strMESFuncName.Equals("CheckPackOutLogic"))
                    {
                        return ws.CheckPackOutLogic(strCarton);
                    }
                    else
                    {
                        //MessageBox.Show("暂时不支持" + strMESFuncName + "此方法");
                        return null;
                    }

                }
                else if (strProduct.Equals("AIRPOD"))
                {
                    //MessageBox.Show("暂时不支持" + strProduct + "此产品入库");
                    return null;
                }
                else
                {
                    //MessageBox.Show("暂时不支持" + strProduct + "此产品入库");
                    return null;
                }



            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }


        }
        public Boolean CheckMarinaServerUrlLogBySQL(string strguid, string strserverip, string strurl, string strSN, string strresult, string strempno, string strrequest, out string RetMsg)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                               insert into ppsuser.t_pick_marinawebservice
                               (msg_id, strserverip, strurl, pallet_no,  req_json, res_json, emp_no,createby)
                             values
                               (:inguid, :inserverip, :inurl, :insn, :inrequest, :inresult, :inempno,'PICK')
                                     ");
            MarinaLogModel vm = new MarinaLogModel();
            vm.inguid = strguid;
            vm.inserverip = strserverip;
            vm.inurl = strurl;
            vm.insn = strSN;
            vm.inrequest = strrequest;
            vm.inresult = strresult;
            vm.inempno = strempno;
            try
            {
                var p = Query<string>(sql, vm);
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return false;
            }
            RetMsg = "";
            return true;
        }

        public string MarinaWebService(string url, string strCSNList, out string strResultOut)
        {
            string errmsg = string.Empty;
            string result = string.Empty;
            string strRB = string.Empty;
            string param = string.Empty;
            byte[] bytes = null;

            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //param = HttpUtility.UrlEncode("packoutvalidation") + "=" + HttpUtility.UrlEncode(strCSNList) ;
            bytes = Encoding.UTF8.GetBytes(strCSNList);

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            try
            {
                writer = request.GetRequestStream();        //获取用于写入请求数据的Stream对象
                writer.Write(bytes, 0, bytes.Length);       //把参数数据写入请求数据流
                writer.Close();
                response = (HttpWebResponse)request.GetResponse();      //获得响应
            }
            catch (WebException e1)
            {
                strResultOut = e1.ToString();
                return "NG";
            }

            #region 这种方式读取到的是一个Xml格式的字符串
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            result = reader.ReadToEnd();
            response.Dispose();
            response.Close();
            reader.Close();
            reader.Dispose();
            #endregion
            strResultOut = result;

            return "OK";
        }
        public Boolean CheckMarinaServerUrlLog(string strguid, string serverip, string url, string insn, string result, string strempno, string strrequest, out string RetMsg)
        {
            return CheckMarinaServerUrlLogBySQL(strguid, serverip, url, insn, result, strempno, strrequest, out RetMsg);

        }
        public PPSAutoGetPalletProcModel AutoGetAssignPackPalletno(string inshippingdate, string inregion, string inshiptype, string inempno, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@inregion", inregion);
                p.Add("@inshippingdate", inshippingdate);
                p.Add("@inshiptype", inshiptype);
                p.Add("@inempno", inempno);
                p.Add("@outpackpalletno", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);

                int i = DbCon.Execute("sp_pick_assignpalletno1", p, commandType: CommandType.StoredProcedure, transaction: tra);

                PPSAutoGetPalletProcModel model = new PPSAutoGetPalletProcModel
                {
                    ErrMsg = p.Get<string>("@errmsg"),
                    PackPalletno = p.Get<string>("@outpackpalletno")
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }




        public PPSPickCheckSnModel CheckSN(string Palletno, string SNOrCartonno, bool IsEntirety, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@insn", SNOrCartonno);
                p.Add("@inpackpalletno", Palletno);
                p.Add("@outsn", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute(IsEntirety ? "sp_pick_checkentirety" : "sp_pick_checksn", p, commandType: CommandType.StoredProcedure, transaction: tra);
                PPSPickCheckSnModel model = new PPSPickCheckSnModel
                {
                    ErrMsg = p.Get<string>("@errmsg"),
                    Outsn = p.Get<string>("@outsn"),
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }








        public PPSPickHoldSnModel CheckHold(string SNOrCartonno, string Type, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@InputSno", SNOrCartonno);
                p.Add("@InputType", Type);
                p.Add("@RetMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);


                int i = DbCon.Execute("T_Check_Hold", p, commandType: CommandType.StoredProcedure, transaction: tra);

                PPSPickHoldSnModel model = new PPSPickHoldSnModel
                {
                    RetMsg = p.Get<string>("@RetMsg"),
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }




        public PPSPickHoldSnModel PickCartonEnd(string Pid, string Type, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@palletno", Pid);
                p.Add("@timelimit", "N");
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);


                int i = DbCon.Execute("SP_PICK_UNLOCKCOMPUTERNAME", p, commandType: CommandType.StoredProcedure, transaction: tra);

                PPSPickHoldSnModel model = new PPSPickHoldSnModel
                {
                    RetMsg = p.Get<string>("@errmsg"),
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }




        public PPSPickHoldSnModel PPartPreAssinPalletNO(string strSID, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@insid", strSID);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute("SP_PICK_PREASSIGN", p, commandType: CommandType.StoredProcedure, transaction: tra);
                PPSPickHoldSnModel model = new PPSPickHoldSnModel
                {
                    RetMsg = p.Get<string>("@errmsg"),
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }




        public PPSPickHoldSnModel CheckPalletStatus(string shipmentId, string palletId, string uuid, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@shipmentid", shipmentId);
                p.Add("@palletno", palletId);
                p.Add("@computername", uuid);
                // p.Add("@mac_address", 1);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);

                int i = DbCon.Execute("SP_PICK_CHECKPALLETSTATUS", p, commandType: CommandType.StoredProcedure, transaction: tra);

                PPSPickHoldSnModel model = new PPSPickHoldSnModel
                {
                    errmsg = p.Get<string>("@errmsg"),
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }


        public PPSPickHoldSnModel CheckTime(string ShipmentId, string palletno, string strTime, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@ShipmentId", ShipmentId);
                p.Add("@palletno", palletno);
                p.Add("@strTime", strTime);
                p.Add("@RetMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute("sp_pick_addlog", p, commandType: CommandType.StoredProcedure, transaction: tra);

                PPSPickHoldSnModel model = new PPSPickHoldSnModel
                {
                    RetMsg = p.Get<string>("@RetMsg"),
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }



        public PPSPickHoldSnModel PPSInsertWorkLogBy(string SNOrCartonno, string Type, string macaddress, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@insn", SNOrCartonno);
                p.Add("@inwc", Type);
                p.Add("@macaddress", macaddress);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);


                int i = DbCon.Execute("SP_PPS_INSERTWORKLOG", p, commandType: CommandType.StoredProcedure, transaction: tra);

                PPSPickHoldSnModel model = new PPSPickHoldSnModel
                {
                    errmsg = p.Get<string>("@errmsg"),
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }


        public PPSPickProcModel InsertCarton(TrolleyBean tbean, IDbTransaction tra = null)
        {
            try
            {
                DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@csn", tbean.CartonNo);
                p.Add("@tno", tbean.trolley_no);
                p.Add("@tlno", tbean.trolley_line_no);
                p.Add("@sno", tbean.sides_no);
                p.Add("@lno", tbean.level_no);
                p.Add("@eid", tbean.UUID);
                p.Add("@part", tbean.ictpartno);
                p.Add("@pno", tbean.pallet_no);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute("SP_Trolley_Insert", p, commandType: CommandType.StoredProcedure, transaction: tra);
                PPSPickProcModel model = new PPSPickProcModel
                {
                    ErrMsg = p.Get<string>("@errmsg"),
                };
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }



        public PPSPickProcModel Commit(string custom_sn, string pallet_no, string trolley_line_no, string pointno, string part, string eid, IDbTransaction tra = null)
        {
            try
            {
                DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@vCustomer_SN", custom_sn);
                p.Add("@vNewPallet", pallet_no);
                p.Add("@vTrolleyLine", trolley_line_no);
                p.Add("@vPoint", pointno);
                p.Add("@vKeyPart", pointno);
                p.Add("@vEmp", eid);
                p.Add("@vOriginPallet", "");
                p.Add("@vReturnMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute("SP_MPartCheckIN", p, commandType: CommandType.StoredProcedure, transaction: tra);
                PPSPickProcModel model = new PPSPickProcModel
                {
                    ErrMsg = p.Get<string>("@vReturnMsg"),
                };
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }


        public string CheckUser(string username, string password, out string tempname, IDbTransaction tra = null)
        {
            tempname = "";
            try
            {
                DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@TEMPNO", username);
                p.Add("@TEMPPWD", password);
                p.Add("@TRES", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                p.Add("@TEMPID", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                p.Add("@TEMPNAME", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute("sajet.login", p, commandType: CommandType.StoredProcedure, transaction: tra);

                string res = p.Get<string>("@TRES");
                tempname = p.Get<string>("@TEMPNAME");
                return res;
            }
            catch (Exception ex)
            {
                return "NG-server error!";
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }

        public string changeSNtoPickPalletno(string SNtoPickPalletno)
        {

            string sql = string.Format("Select pick_pallet_no from ppsuser.t_sn_status where customer_sn='{0}' or carton_no='{1}' or pick_pallet_no='{2}'", SNtoPickPalletno, SNtoPickPalletno, SNtoPickPalletno);
            DataTable dt_change = new DataTable();
            try
            {
                dt_change = ClientUtils.ExecuteSQL(sql).Tables[0];
            }
            catch (Exception ex)
            {
                return "";
            }


            if (dt_change.Rows.Count > 0)
            {
                //如果输入的时real_pallet_no 或者时print_pallet_no 
                //转换位pallet_no 来处理
                SNtoPickPalletno = dt_change.Rows[0]["pick_pallet_no"].ToString();
                return SNtoPickPalletno;
            }
            else
            {
                return "";
            }

        }

        public ExecutionResult GetPickLabelInfo(string strPickpalletno)
        {
            ExecutionResult res = new ExecutionResult();
            res.Status = false;

            string strHead = "";
            string strpackcodedesc = "";
            string strSIDType = "";

            string SQL = string.Format(@"
                               select c.shipment_id,
                               c.pick_pallet_no,
                               c.carrname,
                               c.pallettype,
                               c.remark,
                               c.type,
                               sum(qty) as sumqty
                          from (select a.shipment_id,
                                       e.type,
                                       b.pick_pallet_no,
                                       (select distinct scaccode
                                          from pptest.oms_carrier_tracking_prefix d
                                         where trim(d.carriercode) = e.carrier_code
                                           and d.shipmode = e.transport
                                           and d.region = e.region
                                           and d.isdisabled = '0'
                                           and d.type = 'HAWB') as carrname,
                                       case
                                         when a.pallet_type = '001' and a.gs1flag = 'N' then
                                          'NO MIX / NO GS1'
                                         when a.pallet_type = '001' and a.gs1flag = 'Y' then
                                          '  NO MIX / GS1'
                                         when a.pallet_type = '999' and a.gs1flag = 'N' then
                                          '  MIX / NO GS1'
                                         when a.pallet_type = '999' and a.gs1flag = 'Y' then
                                          '   MIX / GS1'
                                         when a.pallet_type = '999' and a.gs1flag is null then
                                          '      MIX'
                                         when a.pallet_type = '001' and a.gs1flag is null then
                                          '     NO MIX'
                                         else
                                          a.pallet_type
                                       end pallettype,
                                       case
                                         when e.region = 'EMEIA' and (e.carrier_code like '%DHL%' or
                                              e.carrier_name like '%DHL%') and
                                              e.service_level = 'WPX' then
                                          palletlengthcm || '*' || palletwidthcm || 'WPX'
                                         else
                                          palletlengthcm || '*' || palletwidthcm
                                       end as remark,
                                       b.qty
                                  from ppsuser.t_shipment_pallet a
                                  join ppsuser.t_pallet_pick b
                                    on a.pallet_no = b.pallet_no
                                  join ppsuser.t_shipment_info e
                                    on a.shipment_id = e.shipment_id
                                  join pptest.oms_carton_info oci1
                                    on a.pack_code = oci1.packcode
                                  join pptest.oms_pallet_info opi1
                                    on oci1.palletcode = opi1.palletcode
                                 where b.pick_pallet_no = '{0}') c
                         group by c.shipment_id,
                                  c.pick_pallet_no,
                                  c.carrname,
                                  c.pallettype,
                                  c.remark,
                                  c.type
                        ", strPickpalletno);
            DataTable dt = new DataTable();

            try
            {
                dt = ClientUtils.ExecuteSQL(SQL).Tables[0];
            }

            catch (Exception ex)
            {
                res.Message = ex.Message;
                return res;
            }


            if (dt.Rows.Count == 1)
            {
                string str1 = dt.Rows[0]["shipment_id"].ToString();
                string str2 = dt.Rows[0]["pick_pallet_no"].ToString();
                string str3 = dt.Rows[0]["CARRNAME"].ToString();
                string str4 = dt.Rows[0]["sumqty"].ToString();
                // HYQ：20181126 如果sumqty=0 直接return false 不用打印
                if (Convert.ToInt32(str4) == 0)
                {
                    return res;
                }
                string str5 = dt.Rows[0]["pallettype"].ToString();
                //HYQ: 前面补空格无效， .lst文件是有空格的， 但是打印程序被屏蔽掉了。
                str5 = str5.PadRight((15 - str5.Length) / 2, ' ');

                strpackcodedesc = dt.Rows[0]["remark"].ToString();
                strSIDType = dt.Rows[0]["type"].ToString();
                strHead = str1 + "," + str2 + "," + str3 + "," + str4 + "," + str5 + ",";

            }
            else
            {
                return res;
            }

            //HYQ：后面的QTY1 QTY2 
            string strSQL2 = string.Empty;
            string totalPalletqty = "";
            string str7 = "";
            strSQL2 = string.Format("select a.shipment_id, a.pallet_no, a.carton_qty, b.pick_pallet_no,b.pallet_number "
                                  + "from(select shipment_id, pallet_no, carton_qty "
                                  + "        from ppsuser.t_shipment_pallet "
                                  + "       where shipment_id in "
                                  + "             (select shipment_id "
                                  + "                from ppsuser.t_shipment_pallet "
                                  + "               where pallet_no in "
                                  + "                     (select pallet_no "
                                  + "                        from ppsuser.t_pallet_pick "
                                  + "                       where pick_pallet_no = '{0}'))) a "
                                  + " left join(select distinct pallet_no, pick_pallet_no,pallet_number "
                                  + "             from ppsuser.t_pallet_pick "
                                  + "            where pick_pallet_no = '{1}') b "
                                  + "  on a.pallet_no = b.pallet_no "
                                  + " order by a.pallet_no asc ", strPickpalletno, strPickpalletno);
            DataTable dt2 = new DataTable();

            try
            {
                dt2 = ClientUtils.ExecuteSQL(strSQL2).Tables[0];
            }

            catch (Exception ex2)
            {

            }
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["pick_pallet_no"].ToString().Contains(strPickpalletno))
                    {
                        totalPalletqty = dt2.Rows[i]["CARTON_QTY"].ToString();
                        str7 = dt2.Rows[i]["pallet_number"].ToString() + "/" + dt2.Rows.Count.ToString();
                        break;
                    }
                }

            }
            else
            {
                return res;
            }


            string strSQL3 = string.Empty;
            string str6 = "";
            int endQTY = 0;
            strSQL3 = string.Format("select pallet_no, pick_pallet_no , sum(CARTON) as CARTON "
                                    + "  from ppsuser.t_pallet_pick "
                                    + " where pallet_no in "
                                    + "       (select pallet_no "
                                    + "          from ppsuser.t_pallet_pick "
                                    + "         where pick_pallet_no = '{0}') "
                                    + " group by pallet_no, pick_pallet_no "
                                    + " order by pick_pallet_no asc", strPickpalletno);
            DataTable dt3 = new DataTable();

            try
            {
                dt3 = ClientUtils.ExecuteSQL(strSQL3).Tables[0];
            }

            catch (Exception ex3)
            {
            }
            if (dt3.Rows.Count > 0)
            {
                int startQTY = 1;

                for (int i = 0; i < dt3.Rows.Count; i++)
                {

                    if (i > 0)
                    {
                        startQTY = startQTY + Convert.ToInt32(dt3.Rows[i - 1]["CARTON"]);
                    }

                    endQTY = endQTY + Convert.ToInt32(dt3.Rows[i]["CARTON"]);
                    if (dt3.Rows[i]["pick_pallet_no"].ToString().Contains(strPickpalletno))
                    {
                        break;
                    }
                }

                str6 = startQTY.ToString() + "-" + endQTY.ToString() + "/" + totalPalletqty;
            }
            else
            {
                //return false;
            }

            if (strPickpalletno.Substring(1, 1).Equals("9"))
            {
                if (!endQTY.ToString().Equals(totalPalletqty))
                {
                    //这么写不好，再改改。
                    res.Message = "9号pickpallet,必须拣货完成自动打印";
                    return res;
                }

            }
            strHead = strHead + str6 + "," + str7 + "," + strpackcodedesc + "," + strSIDType + ",";
            res.Status = true;
            res.Anything = strHead;
            return res;
        }
        public string ChangeCSNtoCarton(string strSN)
        {
            ExecutionResult res = new ExecutionResult();
            res.Status = false;

            string strHead = "";
            string strpackcodedesc = "";
            string strSIDType = "";

            string strSQL = string.Format("select customer_sn ,carton_no ,wc "
                                         + "    from ppsuser.t_sn_status "
                                         + "   where customer_sn = '{0}' ", strSN);
            DataTable dt = new DataTable();

            try
            {
                dt = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            }

            catch (Exception ex)
            {
                res.Message = ex.Message;
                return res.Message;
            }


            if (dt.Rows.Count == 1)
            {

                strSN = dt.Rows[0]["carton_no"].ToString();
                return strSN;

            }


            return strSN;
        }
        public String WMSStockCheckBySP(String strLoctionId, string strSn, string strIsFirst, string strEmpNo, out string errmsg, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@inlocationid", strLoctionId);
                p.Add("@insn", strSn);
                p.Add("@inisfirst", strIsFirst);
                p.Add("@inempid", strEmpNo);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute("ppsuser.sp_wms_stockcheck", p, commandType: CommandType.StoredProcedure, transaction: tra);

                errmsg = p.Get<string>("@errmsg");



                return errmsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }

        public string CheckPalletCarton(string strSN, string strSN2)
        {
            ExecutionResult res = new ExecutionResult();
            res.Status = false;

            string strHead = "";
            string strpackcodedesc = "";
            string strSIDType = "";

            string strSQL = string.Format(@"select distinct a.carton_no , a.pallet_no
                                 from ppsuser.t_sn_status a
                                where a.pallet_no = '{0}'
                                  and a.carton_no = '{1}'
                                  and a.wc = 'W0'
                                ", strSN, strSN2);
            DataTable dt = new DataTable();

            try
            {
                dt = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            }

            catch (Exception ex)
            {
                res.Message = ex.Message;
                return res.Message;
            }


            if (dt.Rows.Count > 0)
            {

                strSN = dt.Rows[0]["carton_no"].ToString();
                return strSN;

            }
            else
            {
                return "No Data!";
            }



        }
        public string WMSStockCheckBySP2(string strLoctionId, string strSn, string strSn2, string strQTY, string strIsFirst, string strEmpNo, out string errmsg, IDbTransaction tra = null)
        {
            try
            {
                if (tra == null)
                    DbCon.Open();
                var p = new DynamicParameters();
                p.Add("@inlocationid", strLoctionId);
                p.Add("@insn", strSn);
                p.Add("@insn2", strSn2);
                p.Add("@inqty", strQTY);
                p.Add("@inisfirst", strIsFirst);
                p.Add("@inempid", strEmpNo);
                p.Add("@errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
                int i = DbCon.Execute("ppsuser.sp_wms_stockcheck2", p, commandType: CommandType.StoredProcedure, transaction: tra);

                errmsg = p.Get<string>("@errmsg");

                return errmsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (tra == null)
                    DbCon.Close();
            }
        }

    }


}
