using Newtonsoft.Json;
using OperationWCF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using static PPSToMESWcf.MODEL.CommonModel;

namespace PPSToMESWcf
{
    public class PPSToMESService : HttpHosting, IPPSToMESWcf
    {
        PPSToMESServiceBLL pb = new PPSToMESServiceBLL();
        private static string conn
        {
            //
            get
            {
                return
                    string.Format("User Id={0};password={1};{2}",
                    SysInfo.DBLogin.DBUser,
                    SysInfo.DBLogin.DBPwd,
                    ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            }
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public string PPSQHoldSN(string model)
        {
            PPSQHoldReturnModel returnModel = new PPSQHoldReturnModel();
            string strResult = string.Empty;
            string strSN = string.Empty;
            string strQflag = string.Empty;
            PPSQHoldModel inmodel=new PPSQHoldModel();
            try
            {
                inmodel = transformModel<PPSQHoldModel>(model);
                strSN = inmodel.INSN;
                strQflag = string.Empty;
                if (inmodel.QHOLDFLAG)
                {
                    strQflag = "Y";
                }
                else 
                {
                    strQflag = "N";
                }

            }
            catch (Exception e) 
            {
                returnModel.RESULT = false;
                returnModel.MSG = e.ToString()+"+PPS:无法解析["+ model+"]";
                strResult = JsonConvert.SerializeObject(returnModel);
                return strResult;
            }
            if (string.IsNullOrEmpty(strSN))
            {
                returnModel.RESULT = false;
                returnModel.MSG = "PPS:SN不能为空[" + model + "]";
                string strResult2 = JsonConvert.SerializeObject(returnModel);
                return strResult2;
            }

            strSN = pb.DelPrefixCartonSN(strSN);
            //调用SP进行QHOLD
            string strErrmsgOUT = string.Empty;
            string strResultSP = string.Empty;
            try 
            {
                strResultSP = pb.ExecuteQHoldSN(strSN, strQflag, out strErrmsgOUT);
                //strResultSP = "OK";
            }
            catch (Exception e1)
            {
                returnModel.RESULT = false;
                returnModel.MSG = e1.ToString();
                strResult = JsonConvert.SerializeObject(returnModel);
                return strResult;
            }
            if (strResultSP.Equals("NG"))
            {
                returnModel.RESULT = false;
            }
            else
            {
                returnModel.RESULT = true;
            }
            returnModel.MSG = strErrmsgOUT;
            returnModel.OUTSN = strSN;
            returnModel.QHOLDFLAG = inmodel.QHOLDFLAG;
            strResult = JsonConvert.SerializeObject(returnModel);
            return strResult;
        }
        public string PPSQHoldSNList(string model)
        {
            ResMsg returnModel = new ResMsg();
            string strResult = string.Empty;
            PPSQHoldModelList inmodel = new PPSQHoldModelList();
            try
            {
                inmodel = transformModel<PPSQHoldModelList>(model);
            }
            catch (Exception e)
            {
                returnModel.result = false;
                returnModel.msg = e.ToString() + "+PPS:无法解析[" + model + "]";
                strResult = JsonConvert.SerializeObject(returnModel);
                return strResult;
            }
            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            List<PPSQHoldModel> SNList = inmodel.SNFLAGLIST.ToList();


        
        //塞入 mes qhold log
        string insertSql = @"INSERT INTO PPSUSER.MES_QHOLDSN_LOG (IN_GUID,CUSTOMER_SN,HOLD_FLAG)
                values ('" + strGUID + "',:INSN,:HOLD_FLAG )";
            Dictionary<string, object> trans = new Dictionary<string, object>();
            trans.Add(insertSql, SNList);
            ClientUtils.DoExtremeSpeedTransaction(trans);

            
            //调用SP进行QHOLD
            string strErrmsgOUT = string.Empty;
            string strResultSP = string.Empty;
            try
            {
                strResultSP = pb.ExecuteQHoldSN2(strGUID, out strErrmsgOUT);
                //strResultSP = "OK";
            }
            catch (Exception e1)
            {
                returnModel.result = false;
                returnModel.msg = e1.ToString();
                strResult = JsonConvert.SerializeObject(returnModel);
                return strResult;
            }
            if (strResultSP.Equals("NG"))
            {
                returnModel.result = false;
            }
            else
            {
                returnModel.result = true;
            }
            returnModel.msg = strErrmsgOUT;
            strResult = JsonConvert.SerializeObject(returnModel);
            return strResult;
        }

        public string PPSTrolleyNoStatus(string strTrolleyNo)
        {
            PPSTrolleyNoReturnModel returnModel = new PPSTrolleyNoReturnModel();
            string strResult = string.Empty;
            if (string.IsNullOrEmpty(strTrolleyNo))
            {
                returnModel.RESULT = false;
                returnModel.MSG = "PPS:SN不能为空[" + strTrolleyNo + "]";
                string strResult2 = JsonConvert.SerializeObject(returnModel);
                return strResult2;
            }

            string strErrmsgOUT = string.Empty;
            string strResultSP = string.Empty;
            try
            {
                strResultSP = pb.ExecuteCheckTrolleyNoStatus(strTrolleyNo, out strErrmsgOUT);
                //strResultSP = "OK";
            }
            catch (Exception e1)
            {
                returnModel.RESULT = false;
                returnModel.MSG = e1.ToString();
                strResult = JsonConvert.SerializeObject(returnModel);
                return strResult;
            }
            if (strResultSP.Equals("NG"))
            {
                returnModel.RESULT = false;
            }
            else
            {
                returnModel.RESULT = true;
            }
            returnModel.MSG = strErrmsgOUT;
            returnModel.TROLLEYNO = strTrolleyNo;
            strResult = JsonConvert.SerializeObject(returnModel);
            return strResult;
        }

        public string PPSUpdateSNDN(string model)
        {
            PPSUpdateSNDNReturnModel returnModel = new PPSUpdateSNDNReturnModel();
            string strResult = string.Empty;
            string strSN = string.Empty;
            // DELIVERYNO;  DNLINE ;WORKORDER
            string strDN = string.Empty;
            string strDNLine = string.Empty;
            string strWorkOrder = string.Empty;
            PPSUpdateSNDNModel inmodel = new PPSUpdateSNDNModel();
            try
            {
                inmodel = transformModel<PPSUpdateSNDNModel>(model);
                strSN = inmodel.INSN;
                strDN = inmodel.DELIVERYNO;
                strDNLine = inmodel.DNLINE;
                strWorkOrder = inmodel.WORKORDER;

            }
            catch (Exception e)
            {
                returnModel.RESULT = false;
                returnModel.MSG = e.ToString() + "+PPS:无法解析[" + model + "]";
                strResult = JsonConvert.SerializeObject(returnModel);
                return strResult;
            }
            if (string.IsNullOrEmpty(strSN) || string.IsNullOrEmpty(strDN) || string.IsNullOrEmpty(strDNLine) || string.IsNullOrEmpty(strWorkOrder))
            {
                returnModel.RESULT = false;
                returnModel.MSG = "PPS:SN信息不能为空[" + model + "]";
                string strResult2 = JsonConvert.SerializeObject(returnModel);
                return strResult2;
            }


            strSN = pb.DelPrefixCartonSN(strSN);
            string strErrmsgOUT = string.Empty;
            string strResultSP = string.Empty;
            try
            {
                strResultSP = pb.ExecuteUpdateSNDN(strSN, strDN, strDNLine, strWorkOrder, out strErrmsgOUT);
                //strResultSP = "OK";
            }
            catch (Exception e1)
            {
                returnModel.RESULT = false;
                returnModel.MSG = e1.ToString();
                strResult = JsonConvert.SerializeObject(returnModel);
                return strResult;
            }
            if (strResultSP.Equals("NG"))
            {
                returnModel.RESULT = false;
            }
            else
            {
                returnModel.RESULT = true;
            }
            returnModel.MSG = strErrmsgOUT;
            returnModel.OUTSN = strSN;
            returnModel.DELIVERYNO = inmodel.DELIVERYNO;
            returnModel.DNLINE = inmodel.DNLINE;
            returnModel.WORKORDER = inmodel.WORKORDER;
            strResult = JsonConvert.SerializeObject(returnModel);
            return strResult;
        }

        #region test
        //public string TestFGIN(string model) 

        //{
        //UniDb.IDb ora = new UniDb.OracleDb(conn);
        //var s = ora.DbCon.Query<string>("select 'xtz' from dual").FirstOrDefault();
        //    return s;
        //    FGINRETURNMODEL ResultModel = new FGINRETURNMODEL();

        //    ResultModel.INSN = model;

        //    ResMsg resMsg = new ResMsg();
        //    resMsg.msg = "";
        //    resMsg.result = false;

        //    ResultModel.ResMsg = resMsg;


        //    List<SNMODEL> SNModelList = new List<SNMODEL>();

        //    for (int i = 0; i < 10; i++) 
        //    {
        //        SNMODEL SNinfo = new SNMODEL();
        //        SNinfo.WorkOrder = "WorkOrderA";
        //        SNinfo.SerialNumber = "SerialNumber" + i.ToString();
        //        SNinfo.CustomerSN = "CustomerSN" + i.ToString();
        //        SNinfo.CartonNO = "CartonNO" + i.ToString();
        //        SNinfo.PalletNO = "PalletNO" ;
        //        SNinfo.ICTPartNo = "ICTPartNoA" ;
        //        SNinfo.CustModel = "CustModelA" ;
        //        SNinfo.QHoldFlag = "N" ;
        //        SNinfo.TrolleyLineNo = "" ;
        //        SNinfo.TrolleyLineNoPoint = "" ;
        //        SNinfo.DeliveryNo = "";
        //        SNinfo.DNLineNO = "";
        //        SNModelList.Add(SNinfo);
        //    }
        //    ResultModel.SNList = SNModelList.ToArray();
        //    string strResult = JsonConvert.SerializeObject(ResultModel);

        //    return strResult;


        //}
        #endregion
        private T transformModel<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

    }
}
