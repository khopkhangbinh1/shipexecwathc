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

namespace NPIPickListAC
{
    class NPIPickListACBLL
    {
        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("S"))
            { insn = insn.Substring(1); }

            return insn;

        }

        public DataTable GetNPISIDDataTable( string strSID, string strSTATUS, string strStime, string strEtime, string strPlant, string strWarehouse)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetNPISIDDataTableBySQL(strSID, strSTATUS, strStime, strEtime, strPlant, strWarehouse);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public string NPICheckSID(string strSID, string localHostname, out string RetMsg)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            string strRB = wd.NPICheckSIDBySP(strSID, localHostname, out  RetMsg);
            return strRB;
        }


        public DataTable GetSIDLINEINFO(string strSID)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetSIDLINEINFOBySQL(strSID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetSIDSNINFO(string strSID)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetSIDSNINFOBySQL(strSID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetSIDHOLDSNINFO(string strSID)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetSIDHOLDSNINFOBySQL(strSID);
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
            NPIPickListACDAL wd = new NPIPickListACDAL();
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

        public string NPIPICKInsertCarton( string strPickNOA, out string strPickNO, string strSID, string strCarton, string strUserNo, out string RetMsg, out string strLBL)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            string strRB = wd.NPIPICKInsertCartonBySP( strPickNOA, out  strPickNO,  strSID,  strCarton,  strUserNo, out  RetMsg, out  strLBL);
            return strRB;
        }

        public string NPIPICKUnlockComputer(string strSID, out string RetMsg)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            string strRB = wd.NPIPICKUnlockComputerBySP(strSID, out RetMsg);
            return strRB;
        }

        public DataTable ShowStockInfo(string strPartNo, string strPlant, string strSloc)
        {
            if (string.IsNullOrEmpty(strPartNo)) { return null; }
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetStockInfoBySQL(strPartNo, strPlant, strSloc);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }



        public string AfterEdiHttpPostWebService(string serverip, string url, string ShipmentID, out string strResultOut)
        {
            NPIPickListACDAL ud = new NPIPickListACDAL();

            string errmsg = string.Empty;
            string result = string.Empty;
            string strRB =string.Empty;
            string param = string.Empty;
            byte[] bytes = null;

            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            param = HttpUtility.UrlEncode("ShipmentID") + "=" + HttpUtility.UrlEncode(ShipmentID) + "&" + HttpUtility.UrlEncode("wdate") + "=" + HttpUtility.UrlEncode("");
            bytes = Encoding.UTF8.GetBytes(param);

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
                strRB = ud.PPSInsertWebServieByProcedure(ShipmentID, serverip, url, e1.ToString(), out errmsg);
                strResultOut = "INSERTWEBSERVICE:" + errmsg + " #WEBSERVICE_RESPONSE:" + result + e1.ToString();
                return "NG";
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

           
            strRB = ud.PPSInsertWebServieByProcedure(ShipmentID, serverip, url, result, out errmsg);

            strResultOut = "INSERTWEBSERVICE:" + errmsg + " #WEBSERVICE_RESPONSE:" + result;

            return "OK";
        }

        public string PPSCheckWEBLOG(string insn, out string tturl, out string errmsg)
        {

            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.PPSCheckWebServieByProcedure(insn, out tturl, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
        public string NPIPPSCheckWEBLOG(string insn, out string tturl, out string errmsg)
        {

            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.NPIPPSCheckWebServieByProcedure(insn, out tturl, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public DataTable GetPalletPrintInfo(string strPartNo)
        {
            if (string.IsNullOrEmpty(strPartNo)) { return null; }
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetPalletPrintInfoBySQL(strPartNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetPalletWeightINFO()
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetPalletWeightINFOBySQL();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string NPIUpdatePalletWeight(string strPalletNo, string strPalletSize, string strPalletHeight, string strPalletWeight, out string errmsg)
        {

            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.NPIUpdatePalletWeightBySP(strPalletNo, strPalletSize, strPalletHeight, strPalletWeight, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public string NPIUpdatePalletWeight2(string strPalletNo, string strPalletSize, string strPalletHeight, string strPalletWeight, string strPalletenmptycarton, out string errmsg)
        {

            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.NPIUpdatePalletWeight2BySP(strPalletNo, strPalletSize, strPalletHeight, strPalletWeight, strPalletenmptycarton, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public string NPIPPSAddSIDPallet(string insn,  out string errmsg)
        {

            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.NPIPPSAddSIDPalletByProcedure(insn,  out errmsg);
            return strRB;
        }

        public string PPSGetbasicparameter(string strParaType, out string strParaValue, out string RetMsg)
        {
            NPIPickListACDAL ud = new NPIPickListACDAL();
            string strRB = ud.PPSGetbasicparameterBySP(strParaType, out  strParaValue, out RetMsg);
            return strRB;

        }
        
        public void UpdateDgvSIDStatus(string strSID, DataGridView dgvNo)
        {
            DataTable dtPallet = GetSIDStatusInfo(strSID);
            if (dtPallet == null)
            {
                return;
            }
            if (dtPallet.Rows.Count > 0)
            {
                string strsidqty = dtPallet.Rows[0]["qty"]?.ToString();
                string strsidcartonqty = dtPallet.Rows[0]["carton_qty"]?.ToString();
                string strsidstatus = dtPallet.Rows[0]["status"]?.ToString();
                string strsidclosetime = dtPallet.Rows[0]["close_time"]?.ToString();
                string strsidcomputer = dtPallet.Rows[0]["computer_name"]?.ToString();

                if (dgvNo.Rows.Count > 0)
                {
                    for (int j = 0; j < dgvNo.Rows.Count; j++)
                    {
                        string dgvNoSID = dgvNo.Rows[j].Cells["shipment_id"].Value.ToString();
                        dgvNoSID = dgvNoSID.Trim();
                        if (dgvNoSID.Equals(strSID))
                        {
                            dgvNo.Rows[j].Cells["qty"].Value = strsidqty;
                            dgvNo.Rows[j].Cells["carton_qty"].Value = strsidcartonqty;
                            dgvNo.Rows[j].Cells["status"].Value = strsidstatus;
                            dgvNo.Rows[j].Cells["close_time"].Value = strsidclosetime;
                            dgvNo.Rows[j].Cells["computer_name"].Value = strsidcomputer;
                            break;
                        }
                    }
                }

            }
        }

        public DataTable GetSIDStatusInfo(string strSID)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetSIDStatusInfoBySQL(strSID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string GetDBType(string inparatype, out string outparavalue, out string errmsg)
        {

            errmsg = string.Empty;
            NPIPickListACDAL wd = new NPIPickListACDAL();
            string strRB = wd.GetDBTypeBySP(inparatype, out outparavalue, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public string GetMarinaPackoutFlag(string strSN, string strStation, out string strMarinaFlag, out string strPackoutFlag, out string errmsg)
        {

            errmsg = string.Empty;
            NPIPickListACDAL wd = new NPIPickListACDAL();
            string strResult = wd.GetMarinaPackoutFlagBySP(strSN, strStation, out strMarinaFlag, out strPackoutFlag, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
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
            strResultOut = result;

            return "OK";
        }

        public string GetMesAPI(string strProduct, string strUrl, string strMESFuncName, string strCarton)
        {

            try
            {
                if (strProduct.Equals("WATCH"))
                {
                    //MesApi ws = HttpChannel.Get<MesApi>(serviceUrl);
                    JSMESWebReference.MesApi ws = new JSMESWebReference.MesApi(strUrl);
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
                return null;
            }


        }

        public DataTable GetSNInfoDataTableBLL2(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetSNInfoDataTableDAL2(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public Boolean CheckMarinaServerUrlLog(string strguid, string serverip, string url, string insn, string result, string strempno, string strrequest, out string RetMsg)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            return wd.CheckMarinaServerUrlLogBySQL(strguid, serverip, url, insn, result, strempno, strrequest, out RetMsg);

        }

        public Int32 GetPickPalletCartonCount(string strPickPalletno)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataTable dt = new DataTable();
            DataSet dataSet = wd.GetPickPalletCartonCountBySQL( strPickPalletno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32( dataSet.Tables[0].Rows[0]["cartoncount"]);
            }
        }
        public DataTable GetSIDNOCartonList(string strSIDNO, string strPICKSIDNO)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.GetSIDNOCartonListBySQL(strSIDNO, strPICKSIDNO);
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
            NPIPickListACDAL wd = new NPIPickListACDAL();
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

        public DataTable CheckCartonInSIDNO(string strSIDNO, string strPICKSIDNO, string strSN)
        {
            NPIPickListACDAL wd = new NPIPickListACDAL();
            DataSet dataSet = wd.CheckCartonInSIDNOBySQL(strSIDNO, strPICKSIDNO, strSN);
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
            NPIPickListACDAL wd = new NPIPickListACDAL();
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
    }
}
