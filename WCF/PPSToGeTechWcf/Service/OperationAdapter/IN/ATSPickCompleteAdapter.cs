using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{

    public class ATSPickCompleteAdapter : AbstractDataAdapter<ATSPickCompleteModelInModel, ATSPickCompleteModelOutModel>
    {

        public ATSPickCompleteAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return this.GetType().Name.Replace("Adapter",""); } set { } }

        protected override object CheckModel()
        {
            List<string> op = new List<string> { "ATSPick" };
            if (!op.Contains(inModel.OPTYPE))
                throw new Exception("OpType 错误");

            if (inModel.CARTONS == null || inModel.CARTONS.Count < 1)
                throw new Exception("无Carton信息");


            return null;
        }

        protected override ATSPickCompleteModelOutModel DoAction(object bMsg)
        {
            // doTransLog();
            // 待补充 记录 Pick ?

            return new ATSPickCompleteModelOutModel { Result = true, Msg = "", TaskNo = inModel.TaskNo };
        }

    }



}
