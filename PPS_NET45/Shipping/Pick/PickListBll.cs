using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;
using PickList.Wcf;
using PickList.Entitys;
using Newtonsoft.Json;

namespace PickList
{
    class PickListBll
    {
        
        //public void ShowStockInfo(string ictpartno, DataGridView dtStock)
        //{
        //    if (string.IsNullOrEmpty(ictpartno)) { return; }
        //    dtStock.DataSource = null;
        //    dtStock.Rows.Clear();
        //    PickListDal PickDal = new PickListDal();
        //    DataTable dataSet = PickDal.GetStockInfoDataTable(ictpartno).Tables[0];
        //    if (dataSet.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dataSet.Rows.Count; i++)
        //        {
        //            //创建行
        //            DataGridViewRow dr = new DataGridViewRow();
        //            foreach (DataGridViewColumn c in dtStock.Columns)
        //            {
        //                dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
        //            }
        //            //累加序号
        //            dr.HeaderCell.Value = (i + 1).ToString();
        //            dr.Cells[0].Value = dataSet.Rows[i]["料号"].ToString();
        //            dr.Cells[1].Value = dataSet.Rows[i]["箱数"].ToString();
        //            dr.Cells[2].Value = dataSet.Rows[i]["库位"].ToString();

        //            try
        //            {
        //                dtStock.Invoke((MethodInvoker)delegate ()
        //                {
        //                    dtStock.Rows.Add(dr);
        //                });
        //            }
        //            catch (Exception)
        //            {
        //                return;
        //            }
        //        }
        //    }

        //}
        public void ShowStockCarInfo(string ictpartno,string palletno, DataGridView dtStock)
        {
            if (string.IsNullOrEmpty(ictpartno)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            PickListDal PickDal = new PickListDal();
            DataTable dataSet = PickDal.GetStockCarInfoDataTable(ictpartno,palletno).Tables[0];
            if (dataSet.Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dataSet.Rows[i]["车行号"].ToString()))
                    {
                        dtStock.Columns[1].HeaderText = "箱数";
                    }
                    else
                    {
                        dtStock.Columns[1].HeaderText = "CSN数";
                    }
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dtStock.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    //dr.Cells[0].Value = dataSet.Rows[i]["料号"].ToString();
                    dr.Cells[0].Value = dataSet.Rows[i]["库位"].ToString();
                    dr.Cells[1].Value = dataSet.Rows[i]["箱数"].ToString();
                    dr.Cells[2].Value = dataSet.Rows[i]["车行号"].ToString();
                    dr.Cells[3].Value = dataSet.Rows[i]["料号"].ToString();
                    dr.Cells[4].Value = dataSet.Rows[i]["入库时间"].ToString();
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
        public void ShowPalletStockInfo(string ictpartno, string palletno, DataGridView dtStock)
        {
            if (string.IsNullOrEmpty(ictpartno)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            PickListDal PickDal = new PickListDal();
            DataTable dataSet = PickDal.GetPalletStockInfoDataTable(palletno).Tables[0];
            if (dataSet.Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dataSet.Rows[i]["车行号"].ToString()))
                    {
                        dtStock.Columns[1].HeaderText = "箱数";
                    }
                    else
                    {
                        dtStock.Columns[1].HeaderText = "点位";
                    }
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dtStock.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    //dr.Cells[0].Value = dataSet.Rows[i]["料号"].ToString();
                    dr.Cells[0].Value = dataSet.Rows[i]["库位"].ToString();
                    dr.Cells[1].Value = dataSet.Rows[i]["箱数"].ToString();
                    dr.Cells[2].Value = dataSet.Rows[i]["车行号"].ToString();
                    dr.Cells[3].Value = dataSet.Rows[i]["料号"].ToString();
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

        public DataTable GetStockInfoDataTable(string ictpartno)
        {
            if (string.IsNullOrEmpty(ictpartno)) { return null; }
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetStockInfoDataTable(ictpartno);
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
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetSNInfoDataTableDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetSNInfoDataTableBLL2(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetSNInfoDataTableDAL2(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetShipmentTypeBll(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetShipmentTypeDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetDataTableBLL(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetDataTableDAL(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public string PPSInsertWorkLogBy(string insn, string inwc, string macaddress, out string errmsg)
        {

            errmsg = string.Empty;
            PickListDal pl = new PickListDal();
            string strRB = pl.PPSInsertWorkLogByProcedure(insn, inwc, macaddress, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

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

        public string CreateSAWBSID( out string errmsg)
        {

            errmsg = string.Empty;
            PickListDal pl = new PickListDal();
            string strRB = pl.CreateSAWBSIDByProcedure(out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }


        public string GetDBType(string inparatype, out string outparavalue, out string errmsg)
        {

            errmsg = string.Empty;
            PickListDal pl = new PickListDal();
            string strRB = pl.GetDBTypeBySP( inparatype, out  outparavalue, out  errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
        //PPartPreAssinPalletNOBySP

        public string PPartPreAssinPalletNO(string strSID, out string errmsg)
        {

            errmsg = string.Empty;
            PickListDal pl = new PickListDal();
            string strRB = pl.PPartPreAssinPalletNOBySP(strSID,  out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public string PPSGetbasicparameter(string strParaType, out string strParaValue, out string RetMsg)
        {

            PickListDal pd = new PickListDal();
            string strResult = pd.PPSGetbasicparameterBySP(strParaType, out strParaValue, out RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public Boolean CheckMarinaServerUrlLog(string strguid, string serverip, string url, string insn, string result, string strempno, string strrequest, out string RetMsg)
        {
            PickListDal pd = new PickListDal();
            return pd.CheckMarinaServerUrlLogBySQL(strguid, serverip, url, insn, result, strempno, strrequest, out RetMsg);

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



        public DataTable GetSIDPickListINFO(string strSIDType, string strSID, string strSIDStatus, string strRegion, string strCarrier,string strPOE, DateTime dtimeStart, DateTime dtimeEnd, string strPlant, string strWarehouse)
        {
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetSIDPickListINFOBySQL( strSIDType,  strSID,  strSIDStatus,  strRegion,  strCarrier, strPOE,  dtimeStart,  dtimeEnd, strPlant, strWarehouse);
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
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.GetCartonPartInfoBySQL( strCartonNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string GetMarinaPackoutFlag(string strSN, string strStation, out string strMarinaFlag, out string strPackoutFlag, out string errmsg)
        {

            errmsg = string.Empty;
            PickListDal PickDal = new PickListDal();
            string strResult = PickDal.GetMarinaPackoutFlagBySP(strSN, strStation, out strMarinaFlag, out strPackoutFlag, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string PickCheckSN(string strSN, string strPackPalletNo, out string strOutSN, out string errmsg)
        {

            PickListDal PickDal = new PickListDal();
            string strResult = PickDal.PickCheckSNBySP( strSN,  strPackPalletNo, out  strOutSN, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        public string CreateTrackingNo(string strSID, out string errmsg)
        {

            errmsg = string.Empty;
            PickListDal pl = new PickListDal();
            string strRB = pl.CreateTrackingNoBySP(strSID, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
        public DataTable GetCheckCartonFlag(string inputsno)
        {
            if (string.IsNullOrEmpty(inputsno)) { return null; }
            PickListDal PickDal = new PickListDal();
            DataSet dataSet = PickDal.checkcarton(inputsno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public async void CallShipExecFinish(string pickPallet, string shipmentID, Action<string, string> ShowResult)//, out string msg
        {
            await System.Threading.Tasks.Task.Delay(10000);//wait for the last carton call api finish
            string msg = "";
            PickListBll pb1 = new PickListBll();
            if (pb1.IsFinishShipExec(pickPallet, out msg))
                return;

            DataTable cartondt = pb1.GetCartonTableBLL(pickPallet);
            List<string> lstCartonNo = cartondt.AsEnumerable().Select(x => x["carton_no"].ToString()).ToList();
            string ppsURL = "";
            var res = this.GetDBType("ICTSerivce_URL", out ppsURL, out msg);
            if (!String.IsNullOrWhiteSpace(ppsURL))
            {
                foreach (var cartonNo in lstCartonNo)
                {
                    CarrierWCF.Wcf.IICTToCarrierService WS = OperationWCF.HttpChannel.Get<CarrierWCF.Wcf.IICTToCarrierService>(ppsURL);
                    ShipRequestModel shipRequest = new ShipRequestModel();
                    PickListDal PickDal = new PickListDal();
                    string region = PickDal.GetShipmentInfo(shipmentID).Rows[0]["REGION"].ToString();
                    var exeRes = getRequestData(cartonNo, region, out shipRequest);
                    if (!exeRes.Status)
                    {
                        //send mail alert
                        await System.Threading.Tasks.Task.Run<string>(() => WS.SendMailAlert(cartonNo, exeRes.Message));
                        ShowResult(cartonNo, exeRes.Message);
                        return;
                    }
                    res = await System.Threading.Tasks.Task.Run<string>(() => WS.Ship(JsonConvert.SerializeObject(shipRequest)));
                    if (res != "OK")
                        ShowResult(cartonNo, res);
                }
            }
            else
                ShowResult("配置有异常", "UPS ShipExec配置有异常，请再检查！");
        }
        public bool IsFinishShipExec(string pickPallet, out string errmsg)
        {
            errmsg = string.Empty;
            PickListDal pl = new PickListDal();
            var res = pl.IsFinishShipExec(pickPallet, out errmsg);
            return res;
        }
        public DataTable GetCartonTableBLL(string strPickpalletno)
        {
            var dt = new DataTable();
            if (string.IsNullOrWhiteSpace(strPickpalletno)) { return dt; }
            PickListDal PickDal = new PickListDal();
            dt = PickDal.GetCartonTableDAL(strPickpalletno);
            return dt;
        }
        public ExecuteResult getRequestData(string cartonNo, string region, out ShipRequestModel shipRequest)
        {
            PickListDal PickDal = new PickListDal();
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            shipRequest = new ShipRequestModel();
            DataSet Access = PickDal.GetClientAccess("UPS_ACCESS");
            shipRequest.ClientAccessCredentials = JsonConvert.DeserializeObject<ClientAccessCredentials>(Access.Tables[0].Rows[0]["PARA_VALUE"].ToString());

            if (region.Equals("PAC"))
            {
                DataSet user = PickDal.GetUserContext("UPS_CONTEXT_PAC");
                shipRequest.UserContext = JsonConvert.DeserializeObject<UserContext>(user.Tables[0].Rows[0]["PARA_VALUE"].ToString());
            }
            else
            {
                DataSet user = PickDal.GetUserContext("UPS_CONTEXT");
                shipRequest.UserContext = JsonConvert.DeserializeObject<UserContext>(user.Tables[0].Rows[0]["PARA_VALUE"].ToString());
            }

            //shipRequest.UserContext = PickDal.GetUserContext();
            //仿照原本获取transinfile 查询逻辑 修改个别字段后查询sql
            dt = PickDal.getUpsInfoByCartonNo(cartonNo, region);
            if (dt != null && dt.Rows.Count > 0)
            {
                PackageDefaults packageDefaults = new PackageDefaults
                {
                    Service = new Service()
                    {
                        Symbol = dt.Rows[0]["SERVICELEVELID"].ToString()
                    },
                    Consignee = new Consignee //收件人
                    {
                        Address1 = dt.Rows[0]["ST_ADDR1"].ToString(),
                        City = dt.Rows[0]["SHIPTOCITY"].ToString(),
                        Company = dt.Rows[0]["SHIPTOCOMPANY"].ToString(),
                        Contact = dt.Rows[0]["SHIPTONAME"].ToString(),
                        Country = dt.Rows[0]["SHIPCNTYCODE"].ToString(),
                        Phone = dt.Rows[0]["SHIPTOCONTTEL"].ToString(),
                        PostalCode = dt.Rows[0]["SHIPTOZIP"].ToString(),
                        //RegionDesc  如果有 = 号码, 那么取 = 号之前的
                        StateProvince = dt.Rows[0]["REGIONDESC"].ToString().Contains("=") ? dt.Rows[0]["REGIONDESC"].ToString().Split('=')[0] : dt.Rows[0]["REGIONDESC"].ToString()
                    },
                    #region 待取消--测试时使用写死测试数据  UNKNOW
                    //comment by wenxing 2021-3-20
                    //ImporterOfRecord = new ImporterOfRecord
                    //{
                    //    Account = "X3332V",
                    //    Address1 = "1 Infinite Loop 3-IE",
                    //    City = "Cupertino",
                    //    Company = "Apple Computer Inc",
                    //    Contact = "c/o Customs Clearance",
                    //    Country = "US",
                    //    PostalCode = "95014",
                    //    StateProvince = "CA",
                    //    TaxId = "94-240411000"
                    //},
                    //ReturnAddress = new ReturnAddress
                    //{
                    //    Address1 = "558 ALD BLVD",
                    //    City = "Mt. Juliet",
                    //    Company = "A.I.",
                    //    Country = "US",
                    //    PostalCode = "37122",
                    //    StateProvince = "TN"
                    //},
                    //ThirdPartyBilling = "1",
                    //ThirdPartyBillingAddress = new ThirdPartyBillingAddress
                    //{
                    //    Account = "100Y00",
                    //    Address1 = "5505 W Parmer Lane, Bldg 4",
                    //    Address2 = "MS:186-LPM",
                    //    City = "Austin",
                    //    Company = "APPLE",
                    //    Contact = "C/O Freight Payment",
                    //    Country = "US",
                    //    PostalCode = "78727-4021",
                    //    StateProvince = "TX"
                    //},
                    #endregion
                    Shipdate = new Shipdate // SHIP TIME 
                    {
                        Year = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[0],
                        Month = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[1],
                        Day = dt.Rows[0]["SHIPDATE"].ToString().Split('/')[2]
                    },
                    //Shipper = "AAPL_ICT_KS_TEST",// dt.Rows[0]["SHIPER_CORP_NAME"].ToString(), 报错临时写定值
                    Shipper = "AAPL_" + dt.Rows[0]["parcelaccountnumber"].ToString(),
                    ShipperReference = dt.Rows[0]["DELIVERY_NO"].ToString(),//DN
                    ConsigneeReference = dt.Rows[0]["CUSTSONO"].ToString()
                };
                CommodityContents contents = new CommodityContents
                {
                    Description = "ELECTRONIC PRODUCTS",
                    OriginCountry = dt.Rows[0]["OriginCountry"].ToString(),//COO
                    ProductCode = dt.Rows[0]["AC_PN"].ToString(),//no 35 in transin-file
                    Quantity = dt.Rows[0]["PERCARTONQTY"].ToString(),
                    QuantityUnitMeasure = "PCS",
                    UnitValue = new UnitValue
                    {
                        Amount = dt.Rows[0]["SHIPMENT_TOTAL_VALUE"].ToString(),//总价钱
                        Currency = "USD"
                    },
                    UnitWeight = new UnitWeight
                    {
                        Amount = dt.Rows[0]["WEIGHT_UNIT"].ToString(),
                        Units = "KGS"
                    }
                };
                Packages packages = new Packages
                {
                    CarrierInstructions = dt.Rows[0]["Delivery_Instruction"].ToString(),
                    Description = "ACCESSORY",
                    CommodityContents = new CommodityContents[] { contents },
                    MiscReference1 = dt.Rows[0]["CUSTPONO"].ToString(),
                    MiscReference2 = dt.Rows[0]["WEBORDERNO"].ToString(),
                    MiscReference3 = dt.Rows[0]["CUSTDELITEM"].ToString(),
                    MiscReference4 = dt.Rows[0]["AC_PN"].ToString(),
                    MiscReference5 = dt.Rows[0]["CARTON_NO"].ToString(),
                    MiscReference6 = dt.Rows[0]["SSCC"].ToString(),
                    MiscReference7 = dt.Rows[0]["SHIPMENT_TRACKING"].ToString(),
                    MiscReference8 = dt.Rows[0]["SHIPMENTREACKING"].ToString(),
                    MiscReference10 = dt.Rows[0]["TOTAL_WEIGHT"].ToString() + " KG",//固定是KG
                    MiscReference11 = dt.Rows[0]["CARTON_SEQUNECE"].ToString(),
                    MiscReference12 = dt.Rows[0]["CARTON_COUNT"].ToString(),
                    MiscReference14 = dt.Rows[0]["SAWB"].ToString(),
                    MiscReference15 = dt.Rows[0]["HAWB"].ToString(),
                    TrackingNumber = dt.Rows[0]["TRACKING_NO"].ToString(),
                    Weight = new Weight
                    {
                        Amount = dt.Rows[0]["DN_TOTAL_WEIGHT"].ToString(),//单箱重量 no 27 in transinfile
                        //Amount = dt.Rows[0]["TOTAL_WEIGHT"].ToString(),//total weight of DN
                        Units = "KG"
                    },
                    WorldEaseCode = dt.Rows[0]["SHIPMENTREACKING"].ToString(),
                    WorldEaseFlag = "1"
                };
                shipRequest = new ShipRequestModel
                {
                    ClientAccessCredentials = shipRequest.ClientAccessCredentials,
                    UserContext = shipRequest.UserContext,
                    ShipmentRequest = new ShipmentRequest
                    {
                        PackageDefaults = packageDefaults,
                        Packages = new Packages[] { packages }
                    }
                };
            }
            else
            {
                exeRes.Status = false;
                exeRes.Message = "未能查询到Ups信息，请联系IT-PPS!";
            }
            return exeRes;
        }
        public async void CallShipExec(string cartonNo, string shipmentID, Action<string, string> ShowResult)//, out string msg
        {
            string ppsURL = "";
            string msg = "";
            var res = this.GetDBType("ICTSerivce_URL", out ppsURL, out msg);
            if (!String.IsNullOrWhiteSpace(ppsURL))
            {
                CarrierWCF.Wcf.IICTToCarrierService WS = OperationWCF.HttpChannel.Get<CarrierWCF.Wcf.IICTToCarrierService>(ppsURL);
                ShipRequestModel shipRequest = new ShipRequestModel();
                PickListDal PickDal = new PickListDal();
                string region = PickDal.GetShipmentInfo(shipmentID).Rows[0]["REGION"].ToString();
                var exeRes = getRequestData(cartonNo, region, out shipRequest);
                if (!exeRes.Status)
                {
                    //send mail alert
                    await System.Threading.Tasks.Task.Run<string>(() => WS.SendMailAlert(cartonNo, exeRes.Message));
                    ShowResult(cartonNo, exeRes.Message);
                    return;
                }
                res = await System.Threading.Tasks.Task.Run<string>(() => WS.Ship(JsonConvert.SerializeObject(shipRequest)));
                if (res != "OK")
                    ShowResult(cartonNo, res);
            }
            else
                ShowResult("配置有异常", "UPS ShipExec配置有异常，请再检查！");
        }
    }
}
