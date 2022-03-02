
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{
    public class TrolleyMoveNoticeAdapter : AbstractDataAdapter<TrolleyMoveNoticeInModel, TrolleyMoveNoticeOutModel>
    {
        public TrolleyMoveNoticeAdapter(string data)
            : base(data)
        {
            this.taskNo = inModel.TaskNo;
        }

        //protected override string callAction { get { return "TrolleyMoveNotice"; } set { } }

        protected override object CheckModel()
        {
            List<string> op = new List<string> { "PickComplete", "LocationChange" };
            if (!op.Contains(inModel.OPTYPE))
                throw new Exception("OpType 错误");

            if (inModel.CARS == null || inModel.CARS.Count<1)
                throw new Exception("无车辆信息");

            if (inModel.CARS.SelectMany(x=>x.CARTONS).Count()<1)
                throw new Exception("无Carton 信息");

            // 待补充 比对库存

            return null;
        }

        protected override TrolleyMoveNoticeOutModel DoAction(object bMsg)
        {
            doTransLog();

            // 待补充 库存移转

            return new TrolleyMoveNoticeOutModel { Result = true,Msg="",TaskNo=inModel.TaskNo};
        }



        private void doTransLog()
        {
            var list = inModel.CARS.SelectMany(x =>
                x.CARTONS.GroupBy(y=>y.ICT_PARTNO).Select(y => new Db_I_TransactionTaskLog
                {
                    TASKNO = inModel.TaskNo,
                    DIRECTION = "MOVE",
                    PART_NO =y.Key,
                    STATUS = "N",
                    CARTONQTY = y.Count(),
                    QTY = y.Sum(z=>z.SN.Count()),
                    CDT = DateTime.Now,
                    OPTYPE = inModel.OPTYPE,
                    PALLETTROLLYNO = x.TROLLEYID,
                }
              )
            );
            transLog = list.Select(x => { x.INDATA = JsonConvert.SerializeObject(x); return x; }).ToList();
        }
    }
}
