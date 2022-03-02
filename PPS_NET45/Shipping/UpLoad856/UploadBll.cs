﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace UpLoad856
{
    class UploadBll
    {
        public string PPSInsertWorkLog(string insn, string inwc, string macaddress, out string errmsg)
        {

            errmsg = string.Empty;
            UploadDAL ud = new UploadDAL();
            string strRB = ud.PPSInsertWorkLogByProcedure(insn, inwc, macaddress, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
        


        public string PPSCheckWEBLOG(string insn ,out string tturl,out string errmsg)
        {
            
            UploadDAL ud = new UploadDAL();
            string strRB = ud.PPSCheckWebServieByProcedure(insn, out tturl,out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public string AfterEdiHttpPostWebService(string serverip, string url, string ShipmentID, int itimeout)
        {
            UploadDAL ud = new UploadDAL();

            string errmsg = string.Empty;
            string result = string.Empty;
            string strRB = string.Empty;
            string param = string.Empty;
            byte[] bytes = null;

            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            param = HttpUtility.UrlEncode("ShipmentID") + "=" + HttpUtility.UrlEncode(ShipmentID) + "&" + HttpUtility.UrlEncode("wdate") + "=" + HttpUtility.UrlEncode("");
            bytes = Encoding.UTF8.GetBytes(param);
            //url += "?ShipmentID=" + ShipmentID;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            bool isSettingtimeout = (itimeout == 0) || string.IsNullOrEmpty(itimeout.ToString());
            if (!isSettingtimeout)
            {
                request.Timeout = itimeout * 60 * 1000;
            }
            //20200929add
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";

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

                result = "INSERTWEBSERVICE:" + errmsg + "#WEBSERVICE_RESPONSE:" + result + e1.ToString();
                return result;
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

            result = "INSERTWEBSERVICE:" + errmsg + "#WEBSERVICE_RESPONSE:" + result;

            return result;
        }


        public DataTable GetTodayCarNoList()
        {
            UploadDAL ud = new UploadDAL();
            DataSet dataSet = ud.GetTodayCarNoListBySQL();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetTodayCarNoSIDList( string strCarNO,string strSID)
        {
            UploadDAL ud = new UploadDAL();
            DataSet dataSet = ud.GetTodayCarNoSIDListBySQL(strCarNO,strSID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetTodayCarNoSIDList2(string strCarNO)
        {
            UploadDAL ud = new UploadDAL();
            DataSet dataSet = ud.GetTodayCarNoSIDList2BySQL(strCarNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string PPSBatchUpdate856(string incarno, string inempno, string macaddress, out string errmsg)
        {

            UploadDAL ud = new UploadDAL();
            string strRB = ud.PPSBatchUpdate856BySP( incarno,  inempno, macaddress, out  errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
    }
    
}

