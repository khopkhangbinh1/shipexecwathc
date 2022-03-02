
using Newtonsoft.Json;
using OperationWCF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PPSSelfJobWcf.MODEL.MarinaModel;

namespace PPSSelfJobWcf
{
    public class PPSSelfJobService : HttpHosting, IPPSSelfJobService
    {

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        private string g_MarinaSite;
        private string g_MarinaUrl;
        private string g_ServerIP = ClientUtils.ServerUrl;

        private Delegate _log { get; set; }
        public void AutoWMSMarinaCheck(string siprecount, string sichecktotalcount, Delegate wlog)
        {
            try
            {
                _log = wlog;
                int iprecount = int.Parse(siprecount);
                int ichecktotalcount = int.Parse(sichecktotalcount);

                string g_MarinaSite;
                string g_MarinaUrl;
                string g_ServerIP = ClientUtils.ServerUrl;

                //一次循环检查数量 iprecount 1~400
                //一次检查库存总数 0&空 就是全检查
                string strMsgOut = string.Empty;
                //
                PPSSelfJobServiceBLL pb = new PPSSelfJobServiceBLL();
                string strResulta = string.Empty;
                string strResulterrmsg = string.Empty;
                strResulta = pb.PPSGetbasicparameter("MARINA_URL", out g_MarinaUrl, out strResulterrmsg);
                if (string.IsNullOrEmpty(g_MarinaUrl))
                {
                    strMsgOut = "NG-MarinaServerUrl地址为空异常" + strResulterrmsg;
                    writeLog(strMsgOut);
                };
                strResulta = pb.PPSGetbasicparameter("MARINA_SITE", out g_MarinaSite, out strResulterrmsg);

                if (string.IsNullOrEmpty(g_MarinaSite))
                {
                    strMsgOut = "NG-MarinaSite为空异常" + strResulterrmsg;
                    writeLog(strMsgOut);
                };

                string strGUID = System.Guid.NewGuid().ToString().ToUpper();
                if (iprecount == 0 || string.IsNullOrEmpty(iprecount.ToString()))
                {
                    iprecount = 1;
                }
                DataTable sndt = pb.GetWMSNList(ichecktotalcount);
                if (sndt.Rows.Count > 0)
                {
                    int iTotalrow = sndt.Rows.Count-2;
                    int iTotalPalletCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(iTotalrow) / Convert.ToDouble(iprecount)));
                    for (int i = 0; i < iTotalPalletCount; i++)
                    {
                        MarinaRequestModel inmodel = new MarinaRequestModel();
                        inmodel.STATION_TYPE = "WMS";
                        inmodel.SITE = g_MarinaSite;
                        List<Request> snList = new List<Request>();
                        for (int j = i * iprecount; j < (i + 1) * iprecount; j++)
                        {
                            Request sn = new Request();
                            sn.SerialNumber = sndt.Rows[j]["customer_sn"].ToString().Trim();
                            snList.Add(sn);
                            if (j == sndt.Rows.Count-1)
                            {
                                break;
                            }
                        }
                        inmodel.request = snList.ToArray();
                        string strRequest = JsonConvert.SerializeObject(inmodel);

                        string strRsgMsg = string.Empty;
                        string strMarinaResult = string.Empty;

                        strMarinaResult = pb.MarinaWebService(g_MarinaUrl, strRequest, out strRsgMsg);

                        Boolean isIsertLogOK = true;
                        string strRsgMsg0 = string.Empty;
                        isIsertLogOK = pb.CheckMarinaServerUrlLog(strGUID, g_ServerIP, g_MarinaUrl, (i + 1).ToString(), strRsgMsg, "10086", strRequest, out strRsgMsg0);
                        if (!isIsertLogOK)
                        {
                            strMsgOut = strRsgMsg0;
                            writeLog(strMsgOut);
                        };
                        MarinaReturnModel outmodel = new MarinaReturnModel();
                        try
                        {
                            outmodel = JsonConvert.DeserializeObject<MarinaReturnModel>(strRsgMsg);
                        }
                        catch (Exception e)
                        {
                            strMsgOut = "序号对应MarinaServerCheck 返回异常，" + e.ToString();
                            writeLog(strMsgOut);
                        }

                        List<PPSMarinaSN> SNList = new List<PPSMarinaSN>(); ;
                        foreach (var item in outmodel.response)
                        {
                            PPSMarinaSN marinaSNInfo = new PPSMarinaSN();
                            marinaSNInfo.CUSTOMER_SN = item.SerialNumber;
                            marinaSNInfo.OKTOSHIP = item.OKtoShipwithInstalledOS;
                            marinaSNInfo.ERRORCODE = item.CurrentInstalledOS.ErrorCode.ToString();
                            marinaSNInfo.ERRORMESSAGE = item.CurrentInstalledOS.ErrorMessage;
                            SNList.Add(marinaSNInfo);
                            string strRsgMsg2 = string.Empty;
                            //bool isIsertLogOK2 = pb.InsertMarinaSNLog(strGUID, (i + 1).ToString(), item.SerialNumber, item.OKtoShipwithInstalledOS, item.CurrentInstalledOS.ErrorCode.ToString(), item.CurrentInstalledOS.ErrorMessage, out strRsgMsg2);
                            //if (!isIsertLogOK2)
                            //{
                            //    strMsgOut = strRsgMsg2;
                            //    return strMsgOut;
                            //};
                        }



                        string insertSql = @"INSERT INTO PPSUSER.T_WMS_MARINA_SN_INFO
                                      (MSG_ID, PALLET_NO, CUSTOMER_SN, OKTOSHIP, ERRORCODE, ERRORMESSAGE)
                                    VALUES 
                                      ('" + strGUID + "','" + (i + 1).ToString() + "',:CUSTOMER_SN,:OKTOSHIP, :ERRORCODE, :ERRORMESSAGE )";
                        Dictionary<string, object> trans = new Dictionary<string, object>();
                        trans.Add(insertSql, SNList);
                        ClientUtils.DoExtremeSpeedTransaction(trans);



                    }

                    writeLog("Complete");
                }
                else
                {
                    writeLog("noData");
                }
            }
            catch (Exception ex)
            {
                writeLog(ex.Message);

            }

        }

        private void writeLog(string msg)
        {
            try
            {
                _log.DynamicInvoke(msg);
            }
            catch
            {
            }
        }
        public string AutoWMSMarinaCheck(Int32 iprecount, Int32 ichecktotalcount)
        {
            //一次循环检查数量 iprecount 1~400
            //一次检查库存总数 0&空 就是全检查
            string strMsgOut = string.Empty;
            //
            PPSSelfJobServiceBLL pb = new PPSSelfJobServiceBLL();
            string strResulta = string.Empty;
            string strResulterrmsg = string.Empty;
            strResulta = pb.PPSGetbasicparameter("MARINA_URL", out g_MarinaUrl, out strResulterrmsg);
            if (string.IsNullOrEmpty(g_MarinaUrl))
            {
                strMsgOut = "NG-MarinaServerUrl地址为空异常" + strResulterrmsg;
                return strMsgOut;
            };
            strResulta = pb.PPSGetbasicparameter("MARINA_SITE", out g_MarinaSite, out strResulterrmsg);

            if (string.IsNullOrEmpty(g_MarinaSite))
            {
                strMsgOut = "NG-MarinaSite为空异常" + strResulterrmsg;
                return strMsgOut;
            };

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            if (iprecount == 0 || string.IsNullOrEmpty(iprecount.ToString()))
            {
                iprecount = 1;
            }
            DataTable sndt = pb.GetWMSNList(ichecktotalcount);
            if (sndt.Rows.Count > 0)
            {
                int iTotalrow = sndt.Rows.Count;
                int iTotalPalletCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(iTotalrow) / Convert.ToDouble(iprecount)));
                for (int i = 0; i < iTotalPalletCount; i++)
                {
                    MarinaRequestModel inmodel = new MarinaRequestModel();
                    inmodel.STATION_TYPE = "WMS";
                    inmodel.SITE = g_MarinaSite;
                    List<Request> snList = new List<Request>();
                    for (int j = i * iprecount; j < (i + 1) * iprecount; j++)
                    {
                        Request sn = new Request();
                        sn.SerialNumber = sndt.Rows[j]["customer_sn"].ToString().Trim();
                        snList.Add(sn);
                        if (j == sndt.Rows.Count)
                        {
                            break;
                        }
                    }
                    inmodel.request = snList.ToArray();
                    string strRequest = JsonConvert.SerializeObject(inmodel);

                    string strRsgMsg = string.Empty;
                    string strMarinaResult = string.Empty;

                    strMarinaResult = pb.MarinaWebService(g_MarinaUrl, strRequest, out strRsgMsg);

                    Boolean isIsertLogOK = true;
                    string strRsgMsg0 = string.Empty;
                    isIsertLogOK = pb.CheckMarinaServerUrlLog(strGUID, g_ServerIP, g_MarinaUrl, (i + 1).ToString(), strRsgMsg, "10086", strRequest, out strRsgMsg0);
                    if (!isIsertLogOK)
                    {
                        strMsgOut = strRsgMsg0;
                        return strMsgOut;
                    };
                    MarinaReturnModel outmodel = new MarinaReturnModel();
                    try
                    {
                        outmodel = JsonConvert.DeserializeObject<MarinaReturnModel>(strRsgMsg);
                    }
                    catch (Exception e)
                    {
                        strMsgOut = "序号对应MarinaServerCheck 返回异常，" + e.ToString(); ;
                        return strMsgOut;
                    }

                    List<PPSMarinaSN> SNList = new List<PPSMarinaSN>(); ;
                    foreach (var item in outmodel.response)
                    {
                        PPSMarinaSN marinaSNInfo = new PPSMarinaSN();
                        marinaSNInfo.CUSTOMER_SN = item.SerialNumber;
                        marinaSNInfo.OKTOSHIP = item.OKtoShipwithInstalledOS;
                        marinaSNInfo.ERRORCODE = item.CurrentInstalledOS.ErrorCode.ToString();
                        marinaSNInfo.ERRORMESSAGE = item.CurrentInstalledOS.ErrorMessage;
                        SNList.Add(marinaSNInfo);
                        //string strRsgMsg2 = string.Empty;
                        //bool isIsertLogOK2 = pb.InsertMarinaSNLog(strGUID, (i + 1).ToString(), item.SerialNumber, item.OKtoShipwithInstalledOS, item.CurrentInstalledOS.ErrorCode.ToString(), item.CurrentInstalledOS.ErrorMessage, out strRsgMsg2);
                        //if (!isIsertLogOK2)
                        //{
                        //    strMsgOut = strRsgMsg2;
                        //    return strMsgOut;
                        //}
                    }



                    string insertSql = @"INSERT INTO PPSUSER.T_WMS_MARINA_SN_INFO
                                      (MSG_ID, PALLET_NO, CUSTOMER_SN, OKTOSHIP, ERRORCODE, ERRORMESSAGE)
                                    VALUES 
                                      ('" + strGUID + "','" + (i+1).ToString() + "',:CUSTOMER_SN,:OKTOSHIP, :ERRORCODE, :ERRORMESSAGE )";
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(insertSql, SNList);
                    ClientUtils.DoExtremeSpeedTransaction(trans);

                }

                return null;
            }
            else
            {
                return null;
            }

        }
    }
}
