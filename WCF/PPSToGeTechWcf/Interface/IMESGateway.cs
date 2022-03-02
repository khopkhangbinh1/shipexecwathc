using Newtonsoft.Json;
using OperationWCF;
using System;
using System.Linq;
using System.ServiceModel;
using WcfICTGeTech.Model;
using static EDIWarehouseIN.WCF.CommonModel;

namespace WcfICTGeTech.Interface
{
    public class MESGateway
    {
        private string mesUrl { get; set; }
        private IMESGateway mesWS { get; set; }
        public MESGateway() {
            string sql = @"select a.para_value id
                   from ppsuser.t_basicparameter_info a
                  where a.enabled = 'Y'
                    and a.remark in ('入库产品')
                    and a.para_type in ('WATCH')";
            mesUrl = ClientUtils.Query<string>(sql,null).FirstOrDefault();
            //
            mesWS = HttpChannel.Get<IMESGateway>(mesUrl);
        }
        public FGINRETURNMODEL GetMESStockInfo(string palletNo) {
            FGINRETURNMODEL ResultModel = new FGINRETURNMODEL();
            try 
            {
                string ret = mesWS.GetMESStockInfo(palletNo);
                ResultModel = JsonConvert.DeserializeObject<FGINRETURNMODEL>(ret);
            }
            catch (Exception e) 
            {
                throw new Exception(e.ToString());
            }
            return ResultModel;
        }
    }

    [ServiceContractAttribute(Namespace = "http://tempuri.org.bill/")]
    public interface IMESGateway
    {
        [OperationContractAttribute(Action = "http://tempuri.org.bill/GetMESStockInfo", ReplyAction = "*")]
        string GetMESStockInfo(string Box);
    }

}
