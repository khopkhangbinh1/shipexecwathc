using iMWS.Api.Filters;
using iMWS.Api.Models.Bean;
using iMWS.Api.Models.DataGateWay;
using iMWS.Api.Models.PPSUSER;
using iMWS.Api.Models.Service;
using iMWS.Api.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using UniDb.Wcf;

namespace iMWS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/MPartCheck")]
    [JwtAuth]
    public class MPartCheckController : ApiController
    {
        private datagetway _DataGetway;

        public MPartCheckController()
        {
            this._DataGetway = new datagetway();
        }

        /// <summary>
        /// 检查金刚车是否存在
        /// </summary>
        /// <param name="strTrolley">金刚车号</param>
        /// <returns>检查状态</returns>
        [HttpPost]
        [Route("CheckTrolley")]
        public IHttpActionResult CheckTrolley(baseinfo bean)
        {
            try
            {
                var trolleyLineInfoList = this._DataGetway.CheckTrolley(bean.Trolley_no);
                if (!trolleyLineInfoList.Any())
                {
                    return Ok(Result.Create(false, "Check Trolley No Error ! pls check."));
                }

                var tLocationList = this._DataGetway.CheckTrolleyUsed(bean.Trolley_no);
                if (tLocationList.Count > 0)
                {
                    return Ok(Result.Create(false, $"Trolley:{bean.Trolley_no} used by Pallet:{ tLocationList.FirstOrDefault()?.PALLET_NO }"));
                }
                return Ok(Result.Create<List<T_LOCATION>>(true, "pls Input Trolley Line No.", tLocationList));
            }
            catch (Exception e)
            {
                return Ok(Result.Create(false, e.Message));
            }
        }


        /// <summary>
        /// 检查车行号是否存在
        /// </summary>
        /// <param name="strTrolleyLine">车行号&车号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckTrolleyLine")]
        public IHttpActionResult CheckTrolleyLine(baseinfo bean)
        {
            var result = new ResultViewModel(true, "");
            try
            {
                if (string.IsNullOrEmpty(bean.Trolley_no))
                {
                    return Ok(Result.Create(false, "Input Trolley No First and Click Enter pls"));
                }

                var trolleyLineList = this._DataGetway.CheckTrolleyLine(bean);
                if (!trolleyLineList.Any())
                {
                    result.Success = false;
                    result.Message = "Check Trolley Line No Error ! pls check.";
                }
                else
                {
                    // 获取输入该车行最大容量 // 靜態變數? 是需要取得再回傳到前端?
                    bean.MaxQtyByLine = int.Parse(trolleyLineList.FirstOrDefault().MAXQTY.ToString());
                    result.Success = true;
                    result.Message = "pls Input  SN or Carton No.";
                    result.Data = bean;

                    //检查该行是否被使用
                    var list = this._DataGetway.CheckTrolleyLineIsUsed(bean);

                    if (list.Count > 0)
                    {
                        result.Success = false;
                        result.Message = "Trolley Line has be Used ! pls check.";
                        result.Data = null;
                    }
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }
            return Ok(result);
        }


        /// <summary>
        /// 获取并检查序列号信息
        /// </summary>
        /// <param name="bean">输入序列号信息</param>
        /// <returns>SN关联信息</returns>
        [HttpPost]
        [Route("GetSnInfo")]
        public IHttpActionResult GetSnInfo(baseinfo bean)
        {
            var result = new ResultViewModel(true, "");
            try
            {
                if (string.IsNullOrEmpty(bean.Trolley_no) || string.IsNullOrEmpty(bean.Trolley_Line_No))
                {
                    return Ok(Result.Create(false, "Input Trolley No or Trolley Line No First pls."));
                }
                if (bean.Check_Index > bean.MaxQtyByLine)
                {
                    return Ok(Result.Create(false, $"已扫入产品数量不能超过该行最大存储数[{bean.MaxQtyByLine}]"));
                }

                var snInfoList = this._DataGetway.GetSnInfo(bean);
                var snInfo = snInfoList.FirstOrDefault();
                //可以更新检查逻辑为 customer sn 是否与箱号相同
                if (snInfo == null)
                {
                    result.Success = false;
                    result.Message = "Invalid SN ,pls check";
                }
                else if (!snInfo.PACKQTY.Equals(1) || !snInfo.PARTTYPE.Equals("M") || !snInfo.TOTAL.Equals(1))
                {
                    result.Success = false;
                    result.Message = $"SN:{ bean.Customer_SN} not M-Part single or Cannot found keypart Data,pls check";
                }
                else if (!snInfo.WC.Equals("W0"))
                {
                    result.Success = false;
                    result.Message = $"SN:{ bean.Customer_SN } Route Error,pls check";
                }
                else
                {
                    bean.Carton_No = snInfo.CARTON_NO;
                    bean.Customer_SN = snInfo.CUSTOMER_SN;
                    bean.OriginPallet_no = snInfo.PALLET_NO;
                    bean.KeyPart = snInfo.PART_NO;
                    //bean.Emp_ID = ClientUtils.UserPara1; // todo?
                    bean.isSingle = true;

                    result.Success = true;
                    result.Data = bean;
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }
            return Ok(result);
        }

        // api GetStatusByTrolley
        [HttpPost]
        [Route("GetStatusByTrolley")]
        public IHttpActionResult GetStatusByTrolley(string trolley_no)
        {
            List<StatusData> list = new List<StatusData>();
            try
            {
                list = this._DataGetway.GetStatusByTrolley(trolley_no);
            }
            catch { }
            return Ok(Result.Create(true, "", list));
        }

        [HttpPost]
        [Route("ExecuteTrolleyCheckIn")]
        public IHttpActionResult ExecuteTrolleyCheckIn(ExecuteTrolleyCheckInModel model)
        {
            var result = new ResultViewModel(true, "");
            try
            {
                //获取判断新栈板号/沿用旧栈板号
                model.bean.Pallet_No = GetPalletNo(model.bean);

                foreach (var row in model.dataSource)//遍历datatable
                {
                    //model.bean.Trolley_no = dr["TrolleyNo"].ToString();
                    //model.bean.Trolley_Line_No = dr["TrolleyLineNo"].ToString();
                    //model.bean.Point_No = dr["Check_Index"].ToString();
                    //model.bean.Customer_SN = dr["Customer_SN"].ToString();
                    //model.bean.KeyPart = dr["KeyPartNo"].ToString();
                    //model.bean.OriginPallet_no = dr["OriginPalletNo"].ToString();

                    model.bean.Trolley_no = row.Trolley_no;
                    model.bean.Trolley_Line_No = row.Trolley_Line_No;
                    model.bean.Point_No = row.Check_Index.ToString();
                    model.bean.Customer_SN = row.Customer_SN;
                    model.bean.KeyPart = row.KeyPart; // dr["KeyPartNo"].ToString();
                    model.bean.OriginPallet_no = row.OriginPallet_no;


                    //存在风险，如果中途 报错则报错SN不会执行成功
                    string vReturnMsg = this._DataGetway.ExcuteProcMPartTrolley(model.bean);
                    
                    if (vReturnMsg.Equals("OK"))
                    {
                        result.Success = true;
                        result.Message = "OK";
                    }
                    else
                    {
                        result.Success = false;
                        result.Message += vReturnMsg;
                    }
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }
            return Ok(result);
        }

        private string GetPalletNo(baseinfo bean)
        {
            string strPallet = "";
            var list = this._DataGetway.GetPalletByTrolley(bean.Trolley_no);
            strPallet = list.Count > 0 ? list.FirstOrDefault() : this._DataGetway.GetNewPallet();
            return strPallet;
        }

        // api GetStatusByTrolley
        [HttpPost]
        [Route("GetStatusByTrolley")]
        public IHttpActionResult GetStatusByTrolley(string trolley_no, string sides)
        {
            List<StatusData> list = new List<StatusData>();
            try
            {
                list = this._DataGetway.GetStatusByTrolley(trolley_no, sides);
            }
            catch { }
            return Ok(Result.Create(true, "", list));
        }

        // api CheckSN
        [HttpPost]
        [Route("CheckSN")]
        public IHttpActionResult CheckSN(baseinfo bean)
        {
            var result = new ResultViewModel(true, "");
            //可以写入GetSNInfo方法中
            var list = this._DataGetway.CheckSn(bean.Customer_SN);
            result.Data = list;

            if (list.Count > 0)
            {
                result.Success = false;
                result.Message = $"SN:{bean.Customer_SN} has been scan in Line :{list.FirstOrDefault()?.TROLLEY_LINE_NO},pls change other sn.";
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("GetWCFEOSGateway")]
        public IHttpActionResult GetWCFEOSGateway(string trolley_no)
        {
            string EosUrl = this._DataGetway.getEosUrl();

            try
            {
                // TODO: 我們沒有客戶的 WCF 相關的東西 無法使用
                // SajetMES.Wcfs.IEOSGateway ws = HttpChannel.Get<SajetMES.Wcfs.IEOSGateway>(EosUrl);
                dynamic ws = null;

                string data = "{\"WAREHOUSE_FROM_ID\": \"E3\",";
                data += "\"TROLLEYID\":\"" + trolley_no + "\",";
                data += "\"OPTYPE\": \"CHECKINSTOCKIN\"}";
                string res = ws.PostStockInNotify(data, "");

                return Ok(Result.Create(true, res));
            }
            catch(Exception ex)
            {
                return Ok(Result.Create(false, ex.Message));
            }
        }
    }
}
