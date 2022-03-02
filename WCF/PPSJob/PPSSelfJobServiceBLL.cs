using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PPSSelfJob
{
    class PPSSelfJobServiceBLL
    {
        public string PPSGetbasicparameter(string strParaType, out string strParaValue, out string RetMsg)
        {

            PPSSelfJobServiceDAL pd = new PPSSelfJobServiceDAL();
            string strResult = pd.PPSGetbasicparameterBySP(strParaType, out strParaValue, out RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public DataTable GetWMSNList(Int32 ichecktotalcount)
        {
            PPSSelfJobServiceDAL pd = new PPSSelfJobServiceDAL();
            DataSet dataSet = pd.GetWMSNListBySQL( ichecktotalcount);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
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

        public Boolean CheckMarinaServerUrlLog(string strguid, string serverip, string url, string insn, string result, string strempno, string strrequest, out string RetMsg)
        {
            PPSSelfJobServiceDAL pd = new PPSSelfJobServiceDAL();
            return pd.CheckMarinaServerUrlLogBySQL(strguid, serverip, url, insn, result, strempno, strrequest, out RetMsg);

        }

        public Boolean InsertMarinaSNLog(string strguid, string strPallet, string strSN, string strOKTOSHIP, string strERRORCODE, string strERRORMESSAGE, out string RetMsg)
        {
            PPSSelfJobServiceDAL pd = new PPSSelfJobServiceDAL();
            return pd.InsertMarinaSNLogBySQL( strguid,  strPallet,  strSN,  strOKTOSHIP,  strERRORCODE,  strERRORMESSAGE, out  RetMsg);

        }

    }
}
