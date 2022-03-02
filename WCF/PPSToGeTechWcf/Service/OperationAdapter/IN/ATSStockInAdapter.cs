
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Interface;
using WcfICTGeTech.Model;


namespace WcfICTGeTech.Service.OperationAdapter
{
    public class ATSStockInAdapter : AbstractDataAdapter<ATSStockInInModel, ATSStockInOutInModel>
    {
        //protected override string callAction { get { return "ATSStockInAdapter"; } set { } }

        public ATSStockInAdapter(string data) : base(data)
        {
            this.taskNo = inModel.TaskNo;
        }


        protected override object CheckModel()
        {
            MESGateway mes = new MESGateway();
            var mesInfo = mes.GetMESStockInfo(inModel.PalletNo);
            string mesString = JsonConvert.SerializeObject(
                JsonConvert.DeserializeObject<List<MESSNList>>(JsonConvert.SerializeObject(mesInfo.SNLIST)));
            string inString = JsonConvert.SerializeObject(inModel.SNList);

            if (inModel.SNList == null || inModel.SNList.Count() < 1)
                throw new Exception("无入库项次");

            if (mesString != inString)
                throw new Exception("传入资料与MES 不符");

            return null;
        }

        protected override ATSStockInOutInModel DoAction(object bMsg)
        {
            doTransLog();
            ATSStockInOutInModel ret = JsonConvert.DeserializeObject<ATSStockInOutInModel>(JsonConvert.SerializeObject(inModel));
            try
            {
                // 待补充 入过过账程序
                ret.Result = true;
                ret.Msg = "";
            }
            catch (Exception ex)
            {
                ret.Result = false;
                ret.Msg = ex.Message;
            }
            return ret;
        }


        private void doTransLog()
        {
            var list = inModel.SNList.GroupBy(x => x.PART_NO)
                .Select(x => new Db_I_TransactionTaskLog
                {
                    TASKNO = inModel.TaskNo,
                    DIRECTION = "IN",
                    PART_NO = x.Key,
                    STATUS = "N",
                    CARTONQTY = x.Select(y => y.CARTONID).Distinct().Count(),
                    QTY = x.Count(),
                    CDT = DateTime.Now,
                    OPTYPE = inModel.OPTYPE,
                    PALLETTROLLYNO = inModel.PalletNo,
                });
            transLog = list.Select(x => { x.INDATA = JsonConvert.SerializeObject(x); return x; }).ToList();
        }
    }
}
