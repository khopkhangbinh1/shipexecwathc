using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using static EDIWarehouseInOUT.WCF.CommonModel;

namespace EDIWarehouseInOUT
{
    class EDIWarehouseInOUTBLL
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

        public DataTable GetSNInfoDataTable(string sn, string sntype)
        {
            if (string.IsNullOrEmpty(sn)) { return null; }
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            DataSet dataSet = wd.GetSNInfoDataTable(sn, sntype);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        //old Sample
        public string ExecuteWMSTransIN(string strsn, string strLocationid, string sntype, out string errmsg)
        {

            errmsg = string.Empty;
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            string strResult = wd.WMSTransINBySP(strsn, strLocationid, sntype, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        public string ExecuteFGIN(string strGUID,SNLIST sninfo, string strWH, string strLocation, string strProduct, out string errmsg)
        {
            string strWorkOrder = sninfo.WO;
            string strSerialNumber = sninfo.SN + "01";
            string strCustomerSN = sninfo.SN;
            string strCartonNO = sninfo.BOXID;
            string strPalletNO = sninfo.PALLETID;
            string strBatchType = sninfo.BATCHTYPE;
            string strLoadID = sninfo.LOAD_ID?.ToString();
            string strICTPartNo = string.Empty;
            if (sninfo.BATCHTYPE.ToString().Equals("M04") || sninfo.BATCHTYPE.ToString().Equals("S01"))
            {
                strICTPartNo = sninfo.PN + "-" + sninfo.BATCHTYPE;
            }
            else
            {
                strICTPartNo = sninfo.PN ;
            }
            string strMODEL = sninfo.MODEL;
            string strCustModel = sninfo.CUSTPN;
            string strRegion = sninfo.REGION;
            string strQHoldFlag = sninfo.QHOLDFLAG;
            string strTrolleyNo = sninfo.TROLLEYNO?.ToString();
            string strTrolleyLineNo = sninfo.TROLLEYLINENO?.ToString();
            string strTrolleyLineNoPoint = sninfo.TROLLEYLINENOPOINT?.ToString();
            string strDeliveryNo = sninfo.DN?.ToString();
            string strDNLineNo = sninfo.ITEMNO?.ToString();
            string strGoodsType =  sninfo.GOODSTYPE.ToString();

            errmsg = string.Empty;
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            string strResult = wd.FGINBySP( strGUID,  strWorkOrder,  strSerialNumber,  strCustomerSN,  strCartonNO,  strPalletNO, strBatchType, strLoadID,
                                strICTPartNo, strMODEL,  strCustModel, strRegion, strQHoldFlag, strTrolleyNo, strTrolleyLineNo,  strTrolleyLineNoPoint,  strWH,  
                                strLocation,  strProduct, strDeliveryNo, strDNLineNo, strGoodsType, out errmsg)
        ;
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string ExecuteFGWMSTransIN(string strGUID, string strQty, out string errmsg)
        {

            errmsg = string.Empty;
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            string strResult = wd.FGWMSTransINBySP(strGUID, strQty,  out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        //GetNoExistPartNODataTable
        public DataTable GetNoExistPartNODataTable(string strGUID)
        {
            if (string.IsNullOrEmpty(strGUID)) { return null; }
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            DataSet dataSet = wd.GetNoExistPartNODataTableBySQL(strGUID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetRegionNoMatchDataTable(string strGUID ,string strLocationRegion)
        {
            if (string.IsNullOrEmpty(strGUID)) { return null; }
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            DataSet dataSet = wd.GetRegionNoMatchDataTableBySQL(strGUID, strLocationRegion);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string ExecutePNIN(PNLIST pninfo, out string errmsg)
        {
       
            string strPartNo = pninfo.PN?.ToString(); 
            string strCustModelType = pninfo.CUST_MODEL_TYPE?.ToString(); 
            string strUPC = pninfo.UPC_CODE?.ToString(); 
            string strJAN = pninfo.JAN_CODE?.ToString();
            string strCountry = pninfo.COUNTRY?.ToString(); 
            string strRegion = pninfo.REGION?.ToString(); 
            string strCustModel = pninfo.CUST_PN?.ToString();
            string strSCC = pninfo.SCC_CODE?.ToString(); 

            errmsg = string.Empty;
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            string strResult = wd.PNINBySP(strPartNo, strCustModelType, strUPC, strJAN, strCountry, strRegion, strCustModel, strSCC, out errmsg)
        ;
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
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
            param = HttpUtility.UrlEncode("model") + "=" + HttpUtility.UrlEncode(strin);
            bytes = Encoding.UTF8.GetBytes(param);

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

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
            #endregion

            #region 这种方式读取到的是一个Xml格式的字符串
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            result = reader.ReadToEnd();
            #endregion
            response.Dispose();
            response.Close();
            reader.Close();
            reader.Dispose();
            //Reader.Dispose();
            //Reader.Close();

            //stream.Dispose();
            //stream.Close();
            return result;
        }


        public DataTable GetSNSAPInfoDataTable(string strGUID)
        {
            if (string.IsNullOrEmpty(strGUID)) { return null; }
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            DataSet dataSet = wd.GetSNSAPInfoDataTableBySQL(strGUID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string GetLocationRegion(string strLocationID)
        {
            if (string.IsNullOrEmpty(strLocationID)) { return null; }
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            DataSet dataSet = wd.GetLocationRegionBySQL(strLocationID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0].Rows[0]["region"].ToString();
            }
        }

        public string GetSAPWH(string strWHID)
        {
            if (string.IsNullOrEmpty(strWHID)) { return null; }
            EDIWarehouseInOUTDAL wd = new EDIWarehouseInOUTDAL();
            DataSet dataSet = wd.GetSAPWHBySQL(strWHID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0].Rows[0]["sapwhno"].ToString();
            }
        }


    }
}
