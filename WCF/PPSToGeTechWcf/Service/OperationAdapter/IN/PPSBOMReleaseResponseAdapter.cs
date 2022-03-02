
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{
    public class PPSBOMReleaseResponseAdapter : AbstractDataAdapter<PPSBOMReleaseResponseInModel, PPSBOMReleaseResponseOutModel>
    {
        public PPSBOMReleaseResponseAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return "PPSBOMReleaseResponse"; } set { } }

        protected override object CheckModel()
        {
            List<string> op = new List<string> { "PPSBOMRelease" };
            if (!op.Contains(inModel.OPTYPE))
                throw new Exception("OpType 错误");

            if (inModel.CARTONS == null || inModel.CARTONS.Count < 1)
                throw new Exception("无Carton信息");

            // 待补充 检查发料单
            return null;
        }

        protected override PPSBOMReleaseResponseOutModel DoAction(object bMsg)
        {

            doTransLog();
            // 待补充 产线发料过账 , PPS 出库

            return new PPSBOMReleaseResponseOutModel { Result = true, Msg = "", TaskNo = inModel.TaskNo };
        }


        private void doTransLog()
        {
            var list = inModel.CARTONS.GroupBy(y => new { y.ICT_PARTNO, y.PalletNo }).Select(y => new Db_I_TransactionTaskLog
                {
                    TASKNO = inModel.TaskNo,
                    DIRECTION = "OUT",
                    PART_NO = y.Key.ICT_PARTNO,
                    STATUS = "N",
                    CARTONQTY = y.Count(),
                    QTY = y.Sum(z => z.SN.Count()),
                    CDT = DateTime.Now,
                    OPTYPE = inModel.OPTYPE,
                    PALLETTROLLYNO = y.Key.PalletNo,
                }
              );
        
            transLog = list.Select(x => { x.INDATA = JsonConvert.SerializeObject(x); return x; }).ToList();
        }
    }
}
