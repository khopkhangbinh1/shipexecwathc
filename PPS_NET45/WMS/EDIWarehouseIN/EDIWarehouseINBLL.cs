using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using static EDIWarehouseIN.WCF.CommonModel;

namespace EDIWarehouseIN
{
   public class EDIWarehouseINBLL
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

        public DataTable GetSNInfoDataTable(string sn, string sntype)
        {
            if (string.IsNullOrEmpty(sn)) { return null; }
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
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
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = wd.WMSTransINBySP(strsn, strLocationid, sntype, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        public string ExecuteFGIN(string strGUID, SNLIST sninfo, string strWH, string strLocation, string strProduct, out string errmsg)
        {
            string strWorkOrder = sninfo.WO;
            string strSerialNumber = sninfo.SN + "01";
            string strCustomerSN = sninfo.SN;
            string strCartonNO = sninfo.BOXID;
            string strPalletNO = sninfo.PALLETID;
            string strBatchType = sninfo.BATCHTYPE;
            string strLoadID = sninfo.LOAD_ID?.ToString();
            string strICTPartNo = string.Empty;
            //if (sninfo.BATCHTYPE.ToString().Equals("M04") || sninfo.BATCHTYPE.ToString().Equals("S01"))
            //{
            //    strICTPartNo = sninfo.PN + "-" + sninfo.BATCHTYPE;
            //}
            //else
            //{
            //    strICTPartNo = sninfo.PN;
            //}
            if (!string.IsNullOrEmpty(strBatchType))
            {
                strICTPartNo = sninfo.PN?.ToString() + "-" + strBatchType;

            }
            else
            {
                strICTPartNo = sninfo.PN?.ToString();
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
            string strGoodsType = sninfo.GOODSTYPE.ToString();

            errmsg = string.Empty;
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = "";
            //        wd.FGINBySP( strGUID,  strWorkOrder,  strSerialNumber,  strCustomerSN,  strCartonNO,  strPalletNO, strBatchType, strLoadID,
            //                        strICTPartNo, strMODEL,  strCustModel, strRegion, strQHoldFlag, strTrolleyNo, strTrolleyLineNo,  strTrolleyLineNoPoint,  strWH,  
            //                        strLocation,  strProduct, strDeliveryNo, strDNLineNo, strGoodsType, out errmsg)
            //;
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        public string ExecuteFGIN(string strGUID, SNLIST sninfo, string strProduct, string strForceFlag, out string errmsg)
        {
            string strWorkOrder = sninfo.WO?.ToString();
            string strSerialNumber = sninfo.SN?.ToString() + "01";
            string strCustomerSN = sninfo.SN?.ToString();
            string strCartonNO = sninfo.BOXID?.ToString();
            string strPalletNO = sninfo.PALLETID?.ToString();
            string strBatchType = sninfo.BATCHTYPE?.ToString();
            string strLoadID = sninfo.LOAD_ID?.ToString();
            string strMesWhNo = sninfo.SAPWH?.ToString();
            string strMesPlant = sninfo.PLANT?.ToString();
            string strICTPartNo = string.Empty;
            if (!string.IsNullOrEmpty(strBatchType))
            {
                strICTPartNo = sninfo.PN?.ToString() + "-" + strBatchType;

            }
            else
            {
                strICTPartNo = sninfo.PN?.ToString();
            }
            string strMODEL = sninfo.MODEL?.ToString();
            string strCustModel = sninfo.CUSTPN?.ToString();
            string strRegion = sninfo.REGION?.ToString();
            string strQHoldFlag = sninfo.QHOLDFLAG?.ToString();
            string strTrolleyNo = sninfo.TROLLEYNO?.ToString();
            string strTrolleyLineNo = sninfo.TROLLEYLINENO?.ToString();
            string strTrolleyLineNoPoint = sninfo.TROLLEYLINENOPOINT?.ToString();
            string strDeliveryNo = sninfo.DN?.ToString();
            string strDNLineNo = sninfo.ITEMNO?.ToString();
            string strGoodsType = sninfo.GOODSTYPE.ToString();
            string strMesLine = sninfo.LINE?.ToString();
            string strSAPTransfer = sninfo.TRANSFER?.ToString();
            string coo = sninfo.SNCOO?.ToString();
            errmsg = string.Empty;
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = wd.FGINBySP(strGUID, strWorkOrder, strSerialNumber, strCustomerSN, strCartonNO, strPalletNO, strBatchType, strLoadID,
                                strICTPartNo, strMODEL, strCustModel, strRegion, strQHoldFlag, strTrolleyNo, strTrolleyLineNo, strTrolleyLineNoPoint,
                                strProduct, strDeliveryNo, strDNLineNo, strGoodsType, strMesLine, strSAPTransfer, strForceFlag, coo, strMesWhNo, strMesPlant, out errmsg);
            ;
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string ExecuteRAWMIN(string strGUID, MATERIALQTY sninfo, string strProduct, string strForceFlag, out string errmsg)
        {
            string strCartonNO = sninfo.BOXID;
            string strPalletNO = sninfo.PALLETID;
            string strICTPartNo = sninfo.PN;
            string strSnQty = sninfo.QTY.ToString();
            string strTransferDN = sninfo.TRANSFERDN.ToString();
            string strPlant = sninfo.PLANT.ToString();
            string strSloc = sninfo.SAPWH.ToString();
            if (string.IsNullOrEmpty(strPlant) || string.IsNullOrEmpty(strSloc))
            {
                errmsg = "厂别库别为空";
                return "NG";
            }
            errmsg = string.Empty;
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = wd.RAWMINBySP(strGUID, strCartonNO, strPalletNO, strICTPartNo, strSnQty, strTransferDN, strProduct, strForceFlag, strPlant, strSloc, out errmsg);
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
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = wd.FGWMSTransINBySP(strGUID, strQty, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string ExecuteWmsiPalletTransIN(string strPalletNO, string strLocationNo, out string errmsg)
        {

            errmsg = string.Empty;
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = wd.WmsiPalletTransINBySP(strPalletNO, strLocationNo, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string ExecuteWmsiPalletTransIN(string strPalletNO, string strLocationNo, string strplant_sloc, out string errmsg)
        {

            errmsg = string.Empty;
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = wd.WmsiPalletTransINBySP(strPalletNO, strLocationNo, strplant_sloc, out errmsg);
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
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
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

        public DataTable GetNoExistPartNODataTable2(string strPalletNO)
        {
            if (string.IsNullOrEmpty(strPalletNO)) { return null; }
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetNoExistPartNODataTableBySQL2(strPalletNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetPalletNODataTable(string strPalletNO)
        {
            if (string.IsNullOrEmpty(strPalletNO)) { return null; }
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetPalletNODataTableBySQL(strPalletNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetRegionNoMatchDataTable(string strGUID, string strLocationRegion)
        {
            if (string.IsNullOrEmpty(strGUID)) { return null; }
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
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
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
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
            //20200925add
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
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
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
        public DataTable GetSNSAPInfoDataTable2(string strPalletNO)
        {
            if (string.IsNullOrEmpty(strPalletNO)) { return null; }
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetSNSAPInfoDataTableBySQL2(strPalletNO);
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
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
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
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
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

        public Boolean WMSIBackUpWebServieLog(string strguid, string serverip, string url, string insn, string result, string strempno, string strinterfacename, out string RetMsg)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            return wd.WMSIBackUpWebServieLogBySQL(strguid, serverip, url, insn, result, strempno, strinterfacename, out RetMsg);

        }

        public DataTable GetWmsiPalletDataTable(string strPALLET, string strSTATUS, string strStime, string strEtime)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetWmsiPalletDataTableBySQL(strPALLET, strSTATUS, strStime, strEtime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetWmsiGUIDPALLETBySQL(string strGUID)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetWmsiGUIDPALLETBySQL( strGUID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public string WmsiPalletCheck(string strPalletNO, string strCheckType, string strComputerName, out string RetMsg)
        {

            EDIWarehouseINDAL ud = new EDIWarehouseINDAL();
            string strRB = ud.WmsiPalletCheckBySP(strPalletNO, strCheckType, strComputerName, out RetMsg);
            return strRB;
        }

        public string WmsiTrolleyCheckin(string strPalletNO, string strTrolleyLine, string strCarton, out string RetMsg)
        {

            EDIWarehouseINDAL ud = new EDIWarehouseINDAL();
            string strRB = ud.WmsiTrolleyCheckinBySP(strPalletNO, strTrolleyLine, strCarton, out RetMsg);
            return strRB;
        }




        public DataTable GetTrolleyNODataTable(string strPalletNO)
        {
            if (string.IsNullOrEmpty(strPalletNO)) { return null; }
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetTrolleyNODataTableBySQL(strPalletNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetTrolleyNODataTable(string strPalletNO, string strTrolleyLine)
        {
            if (string.IsNullOrEmpty(strPalletNO)) { return null; }
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetTrolleyNODataTableBySQL(strPalletNO, strTrolleyLine);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public void ShowStockInfo2(string strPalletNO, string strTrolleyLine, DataGridView dtStock, string incsn)
        {
            if (string.IsNullOrEmpty(strPalletNO)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataTable dt = wd.GetTrolleyNODataTableBySQL(strPalletNO, strTrolleyLine).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dtStock.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();

                    dr.Cells[0].Value = dt.Rows[i]["TROLLEY_LINE_NO"].ToString();
                    dr.Cells[1].Value = dt.Rows[i]["POINT_NO"].ToString();
                    dr.Cells[2].Value = dt.Rows[i]["DELIVERY_NO"].ToString();
                    dr.Cells[3].Value = dt.Rows[i]["LINE_ITEM"].ToString();
                    dr.Cells[4].Value = dt.Rows[i]["CARTON_NO"].ToString();
                    dr.Cells[5].Value = dt.Rows[i]["CUSTOMER_SN"].ToString();


                    if (dt.Rows[i]["POINT_NO"].ToString().Equals("0"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Green;
                    }
                    if (dt.Rows[i]["CUSTOMER_SN"].ToString().Contains(incsn) || dt.Rows[i]["CARTON_NO"].ToString().Contains(incsn) || dt.Rows[i]["TROLLEY_LINE_NO"].ToString().Contains(incsn))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    try
                    {
                        dtStock.Invoke((MethodInvoker)delegate ()
                        {
                            dtStock.Rows.Add(dr);
                        });
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }

        }


        public void ShowpalletCartonInfo(string strPalletNO, DataGridView dtStock)
        {
            if (string.IsNullOrEmpty(strPalletNO)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataTable dt = wd.GetPalletCartonInfoDataTableBySQL(strPalletNO).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dtStock.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dt.Rows[i]["CARTON_NO"].ToString();
                    dr.Cells[1].Value = dt.Rows[i]["PART_NO"].ToString();
                    dr.Cells[2].Value = dt.Rows[i]["BATCHTYPE"].ToString();
                    dr.Cells[3].Value = dt.Rows[i]["MPN"].ToString();
                    dr.Cells[4].Value = dt.Rows[i]["MODEL"].ToString();
                    dr.Cells[5].Value = dt.Rows[i]["DELIVERY_NO"].ToString();
                    dr.Cells[6].Value = dt.Rows[i]["LINE_ITEM"].ToString();
                    dr.Cells[7].Value = dt.Rows[i]["TROLLEY_LINE_NO"].ToString();
                    dr.Cells[8].Value = dt.Rows[i]["POINT_NO"].ToString();

                    try
                    {
                        dtStock.Invoke((MethodInvoker)delegate ()
                        {
                            dtStock.Rows.Add(dr);
                        });
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }

        }

        public void ShowpalletCartonInfo(string strPalletNO, DataGridView dtStock, string strGoodsType)
        {
            if (string.IsNullOrEmpty(strPalletNO)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataTable dt = wd.GetPalletCartonInfoDataTableBySQL(strPalletNO).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string strFirstCarNO = dt.Rows[0]["TROLLEY_LINE_NO"].ToString();
                if (string.IsNullOrEmpty(strFirstCarNO))
                {
                    //如果不是金刚车，就放简单点
                    DataTable dt2 = wd.GetPalletCartonInfoDataTableBySQL2(strPalletNO).Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        dtStock.Columns.Clear();
                        dtStock.ColumnCount = 4;
                        dtStock.ColumnHeadersVisible = true;
                        DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                        dtStock.Columns[0].Name = "PALLET_NO";
                        dtStock.Columns[1].Name = "CARTON_NO";
                        dtStock.Columns[2].Name = "PART_NO";
                        dtStock.Columns[3].Name = "SN_QTY";

                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            //创建行
                            DataGridViewRow dr = new DataGridViewRow();
                            foreach (DataGridViewColumn c in dtStock.Columns)
                            {
                                dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            //累加序号
                            dr.HeaderCell.Value = (i + 1).ToString();
                            dr.Cells[0].Value = dt2.Rows[i]["PALLET_NO"].ToString();
                            dr.Cells[1].Value = dt2.Rows[i]["CARTON_NO"].ToString();
                            dr.Cells[2].Value = dt2.Rows[i]["PART_NO"].ToString();
                            dr.Cells[3].Value = dt2.Rows[i]["SN_QTY"].ToString();
                            try
                            {
                                dtStock.Invoke((MethodInvoker)delegate ()
                                {
                                    dtStock.Rows.Add(dr);
                                });
                            }
                            catch (Exception)
                            {
                                return;
                            }
                        }

                    }


                }
                else
                {
                    dtStock.Columns.Clear();
                    dtStock.ColumnCount = 9;
                    dtStock.ColumnHeadersVisible = true;
                    DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                    dtStock.Columns[0].Name = "CARTON_NO";
                    dtStock.Columns[1].Name = "PART_NO";
                    dtStock.Columns[2].Name = "BATCHTYPE";
                    dtStock.Columns[3].Name = "MPN";
                    dtStock.Columns[4].Name = "MODEL";
                    dtStock.Columns[5].Name = "DELIVERY_NO";
                    dtStock.Columns[6].Name = "LINE_ITEM";
                    dtStock.Columns[7].Name = "TROLLEY_LINE_NO";
                    dtStock.Columns[8].Name = "POINT_NO";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //创建行
                        DataGridViewRow dr = new DataGridViewRow();
                        foreach (DataGridViewColumn c in dtStock.Columns)
                        {
                            dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        //累加序号
                        dr.HeaderCell.Value = (i + 1).ToString();
                        dr.Cells[0].Value = dt.Rows[i]["CARTON_NO"].ToString();
                        dr.Cells[1].Value = dt.Rows[i]["PART_NO"].ToString();
                        dr.Cells[2].Value = dt.Rows[i]["BATCHTYPE"].ToString();
                        dr.Cells[3].Value = dt.Rows[i]["MPN"].ToString();
                        dr.Cells[4].Value = dt.Rows[i]["MODEL"].ToString();
                        dr.Cells[5].Value = dt.Rows[i]["DELIVERY_NO"].ToString();
                        dr.Cells[6].Value = dt.Rows[i]["LINE_ITEM"].ToString();
                        dr.Cells[7].Value = dt.Rows[i]["TROLLEY_LINE_NO"].ToString();
                        dr.Cells[8].Value = dt.Rows[i]["POINT_NO"].ToString();

                        try
                        {
                            dtStock.Invoke((MethodInvoker)delegate ()
                            {
                                dtStock.Rows.Add(dr);
                            });
                        }
                        catch (Exception)
                        {
                            return;
                        }
                    }
                }
            }

        }

        public string PPSGetbasicparameter(string strParaType, out string strParaValue, out string RetMsg)
        {

            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = wd.PPSGetbasicparameterBySP(strParaType, out strParaValue, out RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public DataTable PPSGetWHNO(string strPalletNO)
        {

            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.PPSGetWHNO(strPalletNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetLocationNoInfo(string strLocationNo, string strPalletEDIflag)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetLocationNoInfoBySQL(strLocationNo, strPalletEDIflag);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetMesPalletInfoLog(string strPalletNO)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetMesPalletInfoLogBySQL(strPalletNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
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

        public Boolean WMSIUpdatePalletStatus(string strPalletNO, out string RetMsg)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            return wd.WMSIUpdatePalletStatusBySQL(strPalletNO, out RetMsg);

        }
        public string WmsiFBMESWebService(string strPalletNO, string strPalletProduct, string strUserNo, string strServerIP, out string RetMsg)
        {

            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
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
                string strMESFuncName = "UpdateStockINStatus";
                strResultGetSN = GetMesAPI(strPalletProduct, strFGinMESWcf, strMESFuncName, strPalletNO);
                Boolean isIsertLogOK = true;
                string strRsgMsg0 = string.Empty;
                isIsertLogOK = WMSIBackUpWebServieLog(strGUID, strServerIP, strFGinMESWcf, strPalletNO, strResultGetSN, strUserNo, strMESFuncName, out strRsgMsg0);
                if (!isIsertLogOK)
                {
                    RetMsg = strRsgMsg0;
                    return "NG";
                }
                #endregion
            }
            else if (strPalletProduct.Equals("AIRPOD"))
            {
                RetMsg = "暂时不支持" + strPalletProduct + "此产品入库";
                return "NG";
            }
            else
            {
                RetMsg = "暂时不支持" + strPalletProduct + "此产品入库";
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
                string strUpdateResult = string.Empty;
                Boolean isUpdateOK = WMSIUpdatePalletStatus(strPalletNO, out strUpdateResult);
                RetMsg = "调用MES接口OK，MES反馈信息:" + strUpdateResult;
                if (!isUpdateOK)
                {
                    return "NG";
                }
                else
                {
                    return "OK";
                }
            }
            else
            {
                RetMsg = "调用MES接口OK，MES反馈信息:" + strMESReturnResult;
                return "NG";
            }


        }
        public string WmsiUplodSapWebService(string strPalletNO, string strUserNo, string strServerIP, out string RetMsg)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResulta = string.Empty;
            string strResulterrmsg = string.Empty;
            string strSAPWcf = string.Empty;
            strResulta = PPSGetbasicparameter("WMSI_SAP", out strSAPWcf, out strResulterrmsg);
            if (!strResulta.Equals("OK"))
            {
                RetMsg = "获取SAP_URL错误:" + strResulterrmsg;
                return "NG";
            }
            // 这份给SAP的需要用查询语句获取栈板的 栈板号GroupBy
            DataTable getdt = GetSNSAPInfoDataTable2(strPalletNO);
            ERPStockInModel ToSAPModel = new ERPStockInModel();
            ToSAPModel.SPCQN = strPalletNO;
            ToSAPModel.HSDAT = DateTime.Now.ToString("yyyyMMdd");
            //每月1号 0点~8点(不含)，算前一天
            if (DateTime.Now.Day == 1 && DateTime.Now.Hour < 8)
            {
                ToSAPModel.BUDAT = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            }
            else 
            {
                ToSAPModel.BUDAT = DateTime.Now.ToString("yyyyMMdd");
            }

            List<ERPStockInItemModel> ToSAPItemModelList = new List<ERPStockInItemModel>();

            for (int i = 0; i < getdt.Rows.Count; i++)
            {
                ERPStockInItemModel ToSAPItemModel = new ERPStockInItemModel();

                ToSAPItemModel.AUFNR = getdt.Rows[i]["work_order"].ToString();
                ToSAPItemModel.MATNR = getdt.Rows[i]["part_no"].ToString();
                ToSAPItemModel.GAMNG = getdt.Rows[i]["sncount"].ToString();
                ToSAPItemModel.UNAME = "10086";
                //ToSAPItemModel.LGORT = lblSAPWH.Text.Split('-')[0];
                ToSAPItemModel.LGORT = getdt.Rows[i]["sap_wh_no"].ToString();
                //???到底是什么值
                ToSAPItemModel.CHARG = getdt.Rows[i]["batch_no"].ToString();
                ToSAPItemModel.ZZLINE = getdt.Rows[i]["mes_line"].ToString();
                ToSAPItemModelList.Add(ToSAPItemModel);

            }
            ToSAPModel.ITEMS = ToSAPItemModelList.ToArray();

            string strin = JsonConvert.SerializeObject(ToSAPModel);

            //后面会加入SAP的入库过账检查
            #region //TEST
            //string strin = @"
            //            {
            //             'SPCQN': 'TEST',
            //             'HSDAT': 'TEST',
            //             'ITEMS': [
            //              {
            //               'QMDAT': 'TEST',
            //               'QMTIM': 'TEST',
            //               'AUFNR': 'TEST',
            //               'MATNR': 'TEST',
            //               'MEINS': 'TEST',
            //               'GAMNG': 'TEST',
            //               'UNAME': 'TEST',
            //               'LGORT': 'TEST',
            //               'ZSJRQ': 'TEST',
            //               'REMARK': 'TEST',
            //               'CHARG': 'TEST',
            //               'ZSSMB': 'TEST'
            //              }
            //             ]
            //            }";

            //strin = strin.Replace("\'", "\"");
            #endregion

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();

            //调用POST
            string strResultSAP = AfterEdiHttpPostWebService(strSAPWcf, strin);
            Boolean isReusltOK = true;
            WMSISAPRETURNMODEL returnmodel = new WMSISAPRETURNMODEL();
            try
            {
                returnmodel = JsonConvert.DeserializeObject<WMSISAPRETURNMODEL>(strResultSAP);
            }
            catch (Exception e)
            {
                string strResultOut2 = string.Empty;
                isReusltOK = wd.InsertSapWebLogBySQL(strGUID, strServerIP, strSAPWcf, strPalletNO, "", strin, strResultSAP, strUserNo, "", e.ToString(), out strResultOut2);

                if (!isReusltOK)
                {
                    RetMsg = "NG-" + e.ToString() + "#NG-备份web记录失败:" + strResultOut2;
                }
                else
                {
                    RetMsg = "NG-" + e.ToString();
                }
                return "NG";
            }
            string strSAPReturnModelResultMsg = string.Empty;
            isReusltOK = returnmodel.IsSuccess;
            strSAPReturnModelResultMsg = returnmodel.ZZMSG?.ToString();
            if (strSAPReturnModelResultMsg.Length > 100)
            {
                strSAPReturnModelResultMsg = strSAPReturnModelResultMsg.Substring(0, 100);
            }
            string strResultOut3 = string.Empty;
            Boolean isReusltOK2 = wd.InsertSapWebLogBySQL(strGUID, strServerIP, strSAPWcf, strPalletNO, "", strin, strResultSAP, strUserNo, returnmodel.ZZMSGTYPE?.ToString(), strSAPReturnModelResultMsg, out strResultOut3);

            isReusltOK = isReusltOK && isReusltOK2;


            if (!isReusltOK)
            {
                RetMsg = "NG-调用API返回错误:" + strSAPReturnModelResultMsg + strResultOut3;
                return "NG";
            }
            else
            {
                string strResultOut4 = string.Empty;
                Boolean isReusltOK3 = wd.WMSIUpdatePalletStatusBySQL2(strPalletNO, out strResultOut4);
                if (!isReusltOK3)
                {
                    RetMsg = "NG-PPS-PALLE更新失败，转IT确认" + "OK-SAP状态:" + returnmodel.ZZMSGTYPE?.ToString() + strSAPReturnModelResultMsg;
                    return "NG";
                }
                RetMsg = "OK-SAP状态:" + returnmodel.ZZMSGTYPE?.ToString() + strSAPReturnModelResultMsg;
                return "OK";
            }
        }

        public DataTable GetPalletNoStatusInfo(string strPalletNo)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            DataSet dataSet = wd.GetPalletNoStatusInfoBySQL(strPalletNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public void UpdateDgvPalletStatus(string strPalletNo, DataGridView dgvNo)
        {
            DataTable dtPallet = GetPalletNoStatusInfo(strPalletNo);
            if (dtPallet == null) 
            {
                return;
            }
            if (dtPallet.Rows.Count > 0)
            {
                string strpalletstatus = dtPallet.Rows[0]["pallet_status"]?.ToString();
                string strpalletcheckstatus = dtPallet.Rows[0]["check_status"]?.ToString();
                string strpallettransppsstatus = dtPallet.Rows[0]["trans_pps_status"]?.ToString();
                string strpalletfbmesstatus = dtPallet.Rows[0]["fb_mes_status"]?.ToString();
                string strpalletuploadsapstatus = dtPallet.Rows[0]["upload_sap_status"]?.ToString();
                string strpalletmarinastatus = dtPallet.Rows[0]["marina_status"]?.ToString();

                if (dgvNo.Rows.Count > 0)
                {
                    for (int j = 0; j < dgvNo.Rows.Count; j++)
                    {
                        string dgvNopallet = dgvNo.Rows[j].Cells["pallet_no"].Value.ToString();
                        dgvNopallet = dgvNopallet.Trim();
                        if (dgvNopallet.Equals(strPalletNo))
                        {
                            dgvNo.Rows[j].Cells["pallet_status"].Value = strpalletstatus;
                            dgvNo.Rows[j].Cells["check_status"].Value = strpalletcheckstatus;
                            dgvNo.Rows[j].Cells["trans_pps_status"].Value = strpallettransppsstatus;
                            dgvNo.Rows[j].Cells["fb_mes_status"].Value = strpalletfbmesstatus;
                            dgvNo.Rows[j].Cells["upload_sap_status"].Value = strpalletuploadsapstatus;
                            dgvNo.Rows[j].Cells["marina_status"].Value = strpalletmarinastatus;
                            break;
                        }
                    }
                }

            }
        }

        public string GetDBType(string inparatype, out string outparavalue, out string errmsg)
        {

            errmsg = string.Empty;
            EDIWarehouseINDAL pl = new EDIWarehouseINDAL();
            string strRB = pl.GetDBTypeBySP(inparatype, out outparavalue, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }


        public DataTable GetSNInfoDataTableBLL(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            EDIWarehouseINDAL ed = new EDIWarehouseINDAL();
            DataSet dataSet = ed.GetSNInfoDataTableDAL(inputsno);
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
            EDIWarehouseINDAL pd = new EDIWarehouseINDAL();
            return pd.CheckMarinaServerUrlLogBySQL(strguid, serverip, url, insn, result, strempno, strrequest, out RetMsg);

        }
        public DataTable GetWMSNList(Int32 ichecktotalcount)
        {
            EDIWarehouseINDAL pd = new EDIWarehouseINDAL();
            DataSet dataSet = pd.GetWMSNListBySQL(ichecktotalcount);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public Boolean WMSIUpdatePalletMarinaStatus(string strPalletNO, out string RetMsg)
        {
            EDIWarehouseINDAL pd = new EDIWarehouseINDAL();
            return pd.WMSIUpdatePalletStatusBySQL3( strPalletNO, out  RetMsg);
        }

        public Boolean GetMESPalletOKUpdateStatus(string strGuid, out string RetMsg)
        {
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            return wd.GetMESPalletOKUpdateStatusBySQL(strGuid, out RetMsg);
        }

        public string GetMarinaPackoutFlag(string strSN, string strStation, out string strMarinaFlag, out string strPackoutFlag, out string errmsg)
        {

            errmsg = string.Empty;
            EDIWarehouseINDAL wd = new EDIWarehouseINDAL();
            string strResult = wd.GetMarinaPackoutFlagBySP( strSN,  strStation, out  strMarinaFlag, out  strPackoutFlag, out  errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public DataTable GetAutoWMSIPalletNO()
        {
            EDIWarehouseINDAL pd = new EDIWarehouseINDAL();
            DataSet dataSet = pd.GetAutoWMSIPalletNOBySQL();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string  GetAutoWMSILocationNO()
        {
            EDIWarehouseINDAL pd = new EDIWarehouseINDAL();
            DataSet dataSet = pd.GetAutoWMSILocationNOBySQL();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0].Rows[0]["location_no"].ToString().Trim();
            }
        }

    }
}
