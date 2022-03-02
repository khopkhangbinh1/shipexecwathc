using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service.OperationAdapter
{
    public class Sync_TrolleyAdapter : AbstractDataAdapter<Sync_TrolleyInModel, Sync_TrolleyOutModel>
    {
        private IEnumerable<Db_T_Trolley_Line_Info> dbModels { get; set; }
        public Sync_TrolleyAdapter(string data)
            : base(data)
        {
        }

        //protected override string callAction { get { return "Sync_Trolley"; } set { } }

        protected override object CheckModel()
        {
            if (inModel.items == null || inModel.items.Count() < 1)
                throw new Exception("没有 items");

            // 转换 Model

            dbModels = inModel.items.Select(x => new Db_T_Trolley_Line_Info
            {
                TROLLEY_LINE_NO = $"{x.TROLLEY_ID}-{x.SIDES_NO}{x.LEVEL_NO.ToString("000")}",
                TROLLEY_NO = x.TROLLEY_ID,
                SIDES_NO = x.SIDES_NO,
                LEVEL_NO = x.LEVEL_NO,
                PACKQTY = x.PACKQTY,
                SEQ_NO = 1,
                MAXQTY = x.MAXSEQ,
                ISENABLED = x.ENABLED=="Y"?1:0,
                USEDQTY = 0,
                TROLLEY_TYPE = x.TROLLEY_TYPE,
                EMPNO = "GeTech"+x.UPDATE_USERID
            });

            if(dbModels.Where(x=>x.MAXQTY<1).Count()>0)
                throw new Exception("Max Qty 错误");


            return null;
        }

        protected override Sync_TrolleyOutModel DoAction(object bMsg)
        {
            string sql = @"
            MERGE INTO PPSUSER.T_TROLLEY_LINE_INFO A
                USING (SELECT '' FROM DUAL) C
                ON (A.TROLLEY_LINE_NO = :TROLLEY_LINE_NO)
            WHEN MATCHED THEN
                  UPDATE
                     SET A.UDT = SYSDATE, A.EMPNO= :EMPNO  ,A.TROLLEY_TYPE = :TROLLEY_TYPE,
                     A.ISENABLED = :ISENABLED,A.MAXQTY = :MAXQTY,A.PACKQTY=:PACKQTY
            WHEN NOT MATCHED THEN
              INSERT
               (TROLLEY_LINE_NO,TROLLEY_NO,SIDES_NO,LEVEL_NO,SEQ_NO,MAXQTY,ISENABLED,USEDQTY,CDT,TROLLEY_TYPE,EMPNO,UDT,PACKQTY)
              VALUES
               (:TROLLEY_LINE_NO,:TROLLEY_NO,:SIDES_NO,:LEVEL_NO,:SEQ_NO,:MAXQTY,:ISENABLED,:USEDQTY,SYSDATE,:TROLLEY_TYPE,:EMPNO,SYSDATE,:PACKQTY)";

            Dictionary<string, object> trans = new Dictionary<string, object>();
            trans.Add(sql, dbModels.ToList());
            ClientUtils.DoExtremeSpeedTransaction(trans);
            
            //写入系统
            return new Sync_TrolleyOutModel
            {
                Result = true,
            };
        }

    }
}
