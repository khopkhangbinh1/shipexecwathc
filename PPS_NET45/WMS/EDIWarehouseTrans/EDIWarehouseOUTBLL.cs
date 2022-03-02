using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using static EDIWareHouseTrans.WCF.CommonModel;

namespace EDIWareHouseTrans
{
    class EDIWarehouseOUTBLL
    {
        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.Length == 13 && insn.StartsWith("S"))
            { insn = insn.Substring(1); }

            return insn;

        }

        public DataTable GetWarehouseNOTraDataTable(string strSAPid,string strDBtype)
        {
            if (string.IsNullOrEmpty(strSAPid)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetWarehouseNOTraBySQL(strSAPid, strDBtype);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetPartNoDataTable(string strSAPid)
        {
            if (string.IsNullOrEmpty(strSAPid)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetPartNoBySQL(strSAPid);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetDBTypeDataTable(string strPartNo)
        {
            if (string.IsNullOrEmpty(strPartNo)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetDBTypeBySQL(strPartNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetSAPIDDataTable(string strWHOutType, string strSAPid, string strSTATUS, string strStime, string strEtime)
        {
            if (string.IsNullOrEmpty(strWHOutType)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetSAPIDDataTableBySQL(strWHOutType, strSAPid, strSTATUS, strStime, strEtime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }



        public string WmsOCheckSap(string strSapNo,string strWHOutType, string localHostname, out string RetMsg)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strRB = wd.WmsOCheckSapNoBySP(strSapNo, strWHOutType, localHostname, out RetMsg);
            return strRB;
        }


        public DataTable GetSAPNOLINEINFO(string strSapNo, string strWHOutType)
        {
            if (string.IsNullOrEmpty(strWHOutType)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetSAPNOLINEINFOBySQL(strSapNo, strWHOutType);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetSNInfoDataTableBLL(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetSNInfoDataTableDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataSet GetEDINONEDITypeTableBLL(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetEDINONEDITypeDAL(inputsno);
            if (dataSet == null)
            {
                return null;
            }
            else
            {
                return dataSet;
            }
        }

        public DataTable GetLocationDataTableBLL(string inputsno,string sap_no)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetLocationDataTableDAL(inputsno, sap_no);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetWaitFBMESCarton(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetWaitFBMESCartonDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string WmsOTransferEDI(string strCarton, string orgLocationNo, string tarLocationNo, out string RetMsg)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strMESResult = string.Empty;
            string strMESResultOUT = string.Empty;
            string strRB = wd.WmsOTransferEDI(strCarton, orgLocationNo, tarLocationNo, out RetMsg);
            //LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS WmsOInsertCartonSP End", strWHOutType + "*" + strPickSapNOA + "*" + strPickSapNO + "*" + strSapNo + "*" + strCarton + "*" + strUserNo + "*" + strServerIP);
            return strRB;

        }

        public string WmsOCheckTransferEDI(string strCarton, string orgLocationNo, string tarLocationNo, out string RetMsg)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strMESResult = string.Empty;
            string strMESResultOUT = string.Empty;
            string strRB = wd.WmsOCheckTransferEDI(strCarton, orgLocationNo, tarLocationNo,out RetMsg);
            //LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS WmsOInsertCartonSP End", strWHOutType + "*" + strPickSapNOA + "*" + strPickSapNO + "*" + strSapNo + "*" + strCarton + "*" + strUserNo + "*" + strServerIP);
            return strRB;

        }

        public string WmsOCheckCarton(string strWHOutType, string strPickSapNOA, out string strPickSapNO, string strSapNo, string strCarton, string strUserNo, string strServerIP,out string orgLocationNo, out string RetMsg, out string strLBL)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strMESResult = string.Empty;
            string strMESResultOUT = string.Empty;
            string strRB = wd.WmsOCheckCartonBySP(strWHOutType, strPickSapNOA, out strPickSapNO, strSapNo, strCarton, strUserNo,out orgLocationNo, out RetMsg, out strLBL);
            //LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS WmsOInsertCartonSP End", strWHOutType + "*" + strPickSapNOA + "*" + strPickSapNO + "*" + strSapNo + "*" + strCarton + "*" + strUserNo + "*" + strServerIP);
            return strRB;

        }

        public string WmsOInsertCarton(string strSapNo, string strCarton, string strUserNo, string strServerIP, 
                                      string strOriPlant,string strOriSloc,string strTarPlant,string strTarSloc,
                                      string strTarLocationNo,
                                      out string RetMsg, out string strLBL,
                                      out string  newPalletNo)
        {
            try
            {
                EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
                string strMESResult = string.Empty;
                string strMESResultOUT = string.Empty;
                string strRB = wd.WmsOInsertCartonBySP(strSapNo, strCarton, strUserNo,
                                                       strOriPlant, strOriSloc, strTarPlant, strTarSloc, strTarLocationNo,
                                                     out RetMsg, out strLBL, out newPalletNo);
                //LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS WmsOInsertCartonSP End", strWHOutType + "*" + strPickSapNOA + "*" + strPickSapNO + "*" + strSapNo + "*" + strCarton + "*" + strUserNo + "*" + strServerIP);
                return strRB;
            }
            catch(Exception ex)
            {
                strLBL=newPalletNo = RetMsg = string.Empty;
                return ex.Message;
            }

        }
        public string WmsoFBMESWebService(string strPalletNO, string strPalletProduct, string strUserNo, string strServerIP, out string RetMsg)
        {

            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            //获得序号
            string strResultGetSN = string.Empty;
            if (strPalletProduct.Equals("WATCH"))
            {
                string strResulta = string.Empty;
                string strResulterrmsg = string.Empty;
                string strFGinMESWcf = string.Empty;
                strResulta = PPSGetbasicparameter(strPalletProduct, out strFGinMESWcf, out strResulterrmsg);
                if (!strResulta.Equals("OK"))
                {
                    RetMsg = strResulterrmsg;
                    return "NG";
                }

                #region 备份获取记录 先只看watch
                string strMESFuncName = "UpdateReturnLineStatus";
                strResultGetSN = GetMesAPI(strPalletProduct, strFGinMESWcf, strMESFuncName, strPalletNO);
                Boolean isIsertLogOK = true;
                string strRsgMsg0 = string.Empty;
                isIsertLogOK = WMSOBackUpWebServieLog(strGUID, strServerIP, strFGinMESWcf, strPalletNO, strResultGetSN, strUserNo, strMESFuncName, out strRsgMsg0);
                if (!isIsertLogOK)
                {
                    RetMsg = strRsgMsg0;
                    return "NG";
                }
                #endregion
            }
            else if (strPalletProduct.Equals("AIRPOD"))
            {
                RetMsg = "暂时不支持" + strPalletProduct + "此产品出库";
                return "NG";
            }
            else
            {
                RetMsg = "暂时不支持" + strPalletProduct + "此产品出库";
                return "NG";
            }

            List<FBMESRETURNMODEL> ResultModelList = new List<FBMESRETURNMODEL>();
            try
            {
                ResultModelList = JsonConvert.DeserializeObject<List<FBMESRETURNMODEL>>(strResultGetSN);
            }
            catch (Exception e1)
            {
                RetMsg = "MES反馈信息格式异常:" + e1.ToString();
                return "NG";
            }
            string strMESReturnResult = "PASS";
            for (int i = 0; i < ResultModelList.Count(); i++)
            {
                FBMESRETURNMODEL ResultModel = ResultModelList[i];
                if (ResultModel.Result.Contains("FAIL"))
                {
                    strMESReturnResult = ResultModel.Result;
                }
            }
            if (strMESReturnResult.Equals("PASS"))
            {
                RetMsg = "OK";
                return "OK";
            }
            else
            {
                RetMsg = "NG";
                return "NG";
            }
                
            //if (strMESReturnResult.Equals("PASS"))
            //{
            //    string strUpdateResult = string.Empty;
            //    Boolean isUpdateOK = WMSOUpdateCartonStatus(strPalletNO, out strUpdateResult);
            //    RetMsg = strUpdateResult;
            //    if (!isUpdateOK)
            //    {
            //        return "NG";
            //    }
            //    else
            //    {
            //        return "OK";
            //    }
            //}
            //else
            //{
            //    RetMsg =   strMESReturnResult;
            //    return "NG";
            //}
        }
        public Boolean WMSOUpdateCartonStatus(string strPalletNO, out string strUpdateResult)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            Boolean strRB = wd.WMSOUpdateCartonStatusBySP(strPalletNO, out strUpdateResult);
            return strRB;
        }

        public Boolean WMSODELETESN2(string strSN, out string strUpdateResult)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            Boolean strRB = wd.WMSODELETESN2BySP(strSN, out strUpdateResult);
            return strRB;
        }


        public Boolean WMSOBackUpWebServieLog(string strguid, string serverip, string url, string insn, string result, string strempno, string strinterfacename, out string RetMsg)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            return wd.WMSOBackUpWebServieLogBySQL(strguid, serverip, url, insn, result, strempno, strinterfacename, out RetMsg);

        }
        public string GetMesAPI(string strProduct, string strUrl, string strMESFuncName, string strCarton)
        {

            try
            {
                if (strProduct.Equals("WATCH"))
                {
                    //MesApi ws = HttpChannel.Get<MesApi>(serviceUrl);
                    JSMESWebReference.MesApi ws = new JSMESWebReference.MesApi(strUrl);
                    if (strMESFuncName.Equals("UpdateReturnLineStatus"))
                    {
                        return ws.UpdateReturnLineStatus(strCarton);
                    }
                    else
                    {
                        MessageBox.Show("暂时不支持" + strMESFuncName + "此方法");
                        return null;
                    }

                }
                else if (strProduct.Equals("AIRPOD"))
                {
                    MessageBox.Show("暂时不支持" + strProduct + "此产品入库");
                    return null;
                }
                else
                {
                    MessageBox.Show("暂时不支持" + strProduct + "此产品入库");
                    return null;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ex.ToString();
            }


        }
        public string PPSGetbasicparameter(string strParaType, out string strParaValue, out string RetMsg)
        {

            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strResult = wd.PPSGetbasicparameterBySP(strParaType, out strParaValue, out RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string WmsOCheckCartonFBMES(string strCartonNo, out string strProduct, out string RetMsg)
        {

            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strResult = wd.WmsOCheckCartonFBMESBySP(strCartonNo, out strProduct, out RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string WmsOUnlockComputer(string strSapId,string strWHOutType, out string RetMsg)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strRB = wd.WmsOUnlockComputerBySP(strSapId, strWHOutType, out  RetMsg);
            return strRB;
        }

        public string WMSOUplodSapNoWebService(string strSapNo,string strPickSapNo, string strWHOutType,string strUserNo,string strServerIP, out string RetMsg)
        {
            //            MSG_ID STRSERVERIP STRURL SAP_NO  PICK_SAP_NO REQ_JSON    STRRESULT EMP_NO  CDT STATUS  ERRMSG ROWID
            //1   TEST - TEST - TEST  http://10.54.10.14:8090	http://10.54.10.15:93/OMSBgSAP/PPSShipment	TESTTES	P1TESTTEST	<CLOB>		10086	2020/2/26 15:39:31			AAAEfMAAEAAABFrAAA

            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            string strReultOUT1 = string.Empty;
            string strParaValue = string.Empty;
            string strRB = wd.PPSGetbasicparameterBySP(strWHOutType, out strParaValue, out strReultOUT1);
            if (!strRB.Equals("OK")) 
            {
                RetMsg = strReultOUT1;
                return "NG";

            }

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();


            //先通过不同出货模式获取不同的表值，需要先写到表才能调用OMS的web POST ;OMS会更新表的信息栏位值。
            string strRequestModel = string.Empty;
            if (strWHOutType.Equals("ZTL"))
            {
                
                List<ZTLTOERPMODEL> modellist = new List<ZTLTOERPMODEL>();
                DataSet dataSet = wd.GetRequestSapModelBySQL(strWHOutType, strSapNo, strPickSapNo);
                DataTable dt = dataSet.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ZTLTOERPMODEL ztltosapmodel = new ZTLTOERPMODEL();
                        //ztltosapmodel.ZID = strGUID;  20200911 cancel
                        ztltosapmodel.ZID = strPickSapNo;
                        ztltosapmodel.ZDBNUM = dt.Rows[i]["ZDBNUM"].ToString();
                        ztltosapmodel.SORTID = dt.Rows[i]["SORTID"].ToString();
                        ztltosapmodel.MATNR = dt.Rows[i]["MATNR"].ToString();
                        ztltosapmodel.LGORT_BC = dt.Rows[i]["LGORT_BC"].ToString();
                        ztltosapmodel.CHARG_BC = dt.Rows[i]["CHARG_BC"].ToString();
                        ztltosapmodel.ZZSTAGE_BC = dt.Rows[i]["ZZSTAGE_BC"].ToString();
                        ztltosapmodel.MENGE = dt.Rows[i]["MENGE"].ToString();
                        ztltosapmodel.LGORT_BR = dt.Rows[i]["LGORT_BR"].ToString();
                        ztltosapmodel.CHARG_BR = dt.Rows[i]["CHARG_BR"].ToString();
                        ztltosapmodel.ZZSTAGE_BR = dt.Rows[i]["ZZSTAGE_BR"].ToString();
                        ztltosapmodel.ZSYFLG = dt.Rows[i]["ZSYFLG"].ToString();
                        ztltosapmodel.ZUSERS = strUserNo;
                        if (DateTime.Now.Day == 1 && DateTime.Now.Hour < 8)
                        {
                            ztltosapmodel.BUDAT = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                        }
                        else
                        {
                            ztltosapmodel.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                        }
                        modellist.Add(ztltosapmodel);
                    }
                }
                strRequestModel = JsonConvert.SerializeObject(modellist);

            }
            else
            {
                RetMsg = "NG-出库标识错误";
                return "NG";
            }


            //insert tabel 
            //insert into ppsuser.t_wmso_sapwebservice
            //  (msg_id, strserverip, strurl, sap_no, pick_sap_no, req_json, emp_no)
            //values
            //  (strGUID, strServerIP, strParaValue, strSapNo, strPickSapNo, strRequestModel, strUserNo )
            string strResultOut2 = string.Empty;
            Boolean isReusltOK = wd.InsertSapWebLogBySQL(strGUID, strServerIP, strParaValue, strSapNo, strPickSapNo, strRequestModel, strUserNo, out strResultOut2);
            if (!isReusltOK) 
            {
                RetMsg = "NG-"+strResultOut2;
                return "NG";
            }


            //调用POST
            string strWebPostResult=AfterEdiHttpPostWebService(strParaValue, strGUID);

            //回写结果到表
            string strResultOut3 = string.Empty;
            Boolean isUpdateOK= wd.UpdateSapWebResultBySQL(strGUID, strWebPostResult, out strResultOut3);
            if (!isUpdateOK)
            {
                RetMsg = "NG-" + strResultOut2;
                return "NG";
            }

            List<WMSOERPTOPPSMODEL> returnmodellist = new List<WMSOERPTOPPSMODEL>();
            string strErpReturnModelResult = string.Empty;
            strErpReturnModelResult = "TRUE";
            try { returnmodellist = JsonConvert.DeserializeObject<List<WMSOERPTOPPSMODEL>>(strWebPostResult); }
            catch (Exception e)
            {
                strErpReturnModelResult = "FASLE";
                RetMsg = "NG-调用API返回错误:" + e.ToString();
                //把结果写回去
                return "NG";
            }
            
            
            
            string strErpReturnModelResultMsg = string.Empty;
            for (int i = 0; i < returnmodellist.Count; i++)
            {
                WMSOERPTOPPSMODEL retrunmodel = new WMSOERPTOPPSMODEL();
                retrunmodel = returnmodellist[i];
                if (retrunmodel.IsSuccess.ToString().ToUpper().Equals("FALSE"))
                {
                    strErpReturnModelResult = "FASLE";
                }
                strErpReturnModelResultMsg = retrunmodel.OMSMSG?.ToString() + retrunmodel.ZZMSG?.ToString();
            }

            
             if (!strErpReturnModelResult.Equals("TRUE"))
            {
                RetMsg = "NG-调用API返回错误:" + strErpReturnModelResultMsg;
                //把结果写回去
                return "NG";
            }
            else
            {
                RetMsg = "OK-调用API返回:" + strErpReturnModelResultMsg; ;
                return "OK";
            }
        }


        public string AfterEdiHttpPostWebService(string url, string strin)
        {
            string result = string.Empty;
            string param = string.Empty;
            byte[] bytes = null;

            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            //param = HttpUtility.UrlEncode("PN") + "=" + HttpUtility.UrlEncode(ShipmentID);
            param = HttpUtility.UrlEncode("MsgId") + "=" + HttpUtility.UrlEncode(strin);
            bytes = Encoding.UTF8.GetBytes(param);

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            request.Timeout = 3 * 60 * 1000;
            //20200928add
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            try
            {
                writer = request.GetRequestStream();        //获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                return "exception_request";
            }

            writer.Write(bytes, 0, bytes.Length);       //把参数数据写入请求数据流
            writer.Close();

            try
            {
                response = (HttpWebResponse)request.GetResponse();      //获得响应
            }
            catch (WebException e)
            {
                return "exception_response" + e;
            }

            #region 这种方式读取到的是一个返回的结果字符串
            //Stream stream = response.GetResponseStream();        //获取响应流
            //XmlTextReader Reader = new XmlTextReader(stream);
            //Reader.MoveToContent();
            //result = Reader.ReadInnerXml();
            //response.Dispose();
            //response.Close();
            //Reader.Dispose();
            //Reader.Close();
            //stream.Dispose();
            //stream.Close();
            #endregion

            #region 这种方式读取到的是一个Xml格式的字符串
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            result = reader.ReadToEnd();
            response.Dispose();
            response.Close();
            reader.Close();
            reader.Dispose();
            #endregion


            return result;
        }


        public DataTable ShowStockInfo(string strPartNo,string strBatchNo, string strSapLocationNo)
        {
            if (string.IsNullOrEmpty(strPartNo)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetStockInfoBySQL(strPartNo, strBatchNo, strSapLocationNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public void ShowPalletPartStockInfo( string strPartNo, string strBatchNo, string strSapLocationNo, DataGridView dgv)
        {
            
            dgv.DataSource = null;
            dgv.Rows.Clear();
            if (string.IsNullOrEmpty(strPartNo)) { return; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetStockInfoBySQL(strPartNo, strBatchNo, strSapLocationNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                dgv.DataSource = null;
            }
            else
            {
                dgv.DataSource = dataSet.Tables[0];
            }
        }

        public void ShowOriPalletStockInfo(string strSapId, string strWarehousrID, DataGridView dgv, string strDBType)
        {
            dgv.DataSource = null;
            dgv.Rows.Clear();
            if (string.IsNullOrEmpty(strSapId)) { return; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetOriPalletStockInfoBySQL(strSapId, strWarehousrID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                dgv.DataSource = null;
            }
            else
            {
                dgv.DataSource = dataSet.Tables[0];
            }
        }

        public void ShowPalletStockInfo(string strSapId,string strWarehousrID, DataGridView dgv)
        {
            dgv.DataSource = null;
            dgv.Rows.Clear();
            if (string.IsNullOrEmpty(strSapId)) { return; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetPalletStockInfoBySQL(strSapId,strWarehousrID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                dgv.DataSource = null;
            }
            else
            {
                dgv.DataSource = dataSet.Tables[0];
            }
        }

        public DataTable GetUnPICKSAPINFO(string strStime, string strEtime)
        {
            if (string.IsNullOrEmpty(strStime)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetUnPICKSAPINFOBySQL( strStime,  strEtime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetUnPICKSAPALLINFO(string strStime, string strEtime)
        {
            if (string.IsNullOrEmpty(strStime)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetUnPICKSAPALLINFOBySQL(strStime, strEtime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public string GetComputerNameOfSAPNO(string strSAPNO)
        {
            if (string.IsNullOrEmpty(strSAPNO)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetComputerNameOfSAPNOBySQL( strSAPNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0].Rows[0]["computer_name"]?.ToString(); 
            }
        }

        public DataTable GetPickPrintInfo(string strPickSAPNO)
        {
            if (string.IsNullOrEmpty(strPickSAPNO)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetPickPrintInfoBySQL( strPickSAPNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetPickPrintInfo2(string strPickSAPNO)
        {
            if (string.IsNullOrEmpty(strPickSAPNO)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetPickPrintInfo2BySQL(strPickSAPNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetPickSAPNO(string strSN)
        {
            if (string.IsNullOrEmpty(strSN)) { return null; }
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetPickSAPNOBySQL( strSN);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetCartonPartInfo(string strCartonNo)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetCartonPartInfoBySQL(strCartonNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetSAPNOCartonList(string strSAPNO, string strPICKSAPNO)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetSAPNOCartonListBySQL( strSAPNO,  strPICKSAPNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetSAPNOandPICKSAPNOList(string strCartno)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.GetSAPNOandPICKSAPNOListBySQL(strCartno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string ChangeCSNtoCarton(string strSN)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataTable dt = new DataTable();
            DataSet dataSet = wd.ChangeCSNtoCartonBySQL(strSN);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return strSN;
            }
            else
            {
                return dataSet.Tables[0].Rows[0]["carton_no"].ToString();
            }
        }

        public DataTable CheckCartonInSAPNO(string strSAPNO, string strPICKSAPNO, string strSN)
        {
            EDIWarehouseOUTDAL wd = new EDIWarehouseOUTDAL();
            DataSet dataSet = wd.CheckCartonInSAPNOBySQL( strSAPNO,  strPICKSAPNO,  strSN);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
    }
}
