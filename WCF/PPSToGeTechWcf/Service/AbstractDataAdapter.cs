
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Interface;
using WcfICTGeTech.Model;

namespace WcfICTGeTech.Service
{
    public abstract class AbstractDataAdapter<T1, T2>: IMessageAction where T2 : ICTReturnModel
    {
        private DateTime startTime { get; set; }
        private DateTime endTime { get; set; }

        protected string _input { get; set; }
        protected string taskNo { get; set; }
        protected string key1 { get; set; }
        protected string key2 { get; set; }
        protected string key3 { get; set; }
        protected T1 inModel { get; set; }
        protected List<Db_I_TransactionTaskLog> transLog { get; set; }


        protected IICTToGeTech GetGeTechService {
            get {
                return OperationWCF.HttpChannel.Get<IICTToGeTech>(CommonHelper.GeTechUrl);
            }
        }

        protected  string callAction { get { return this.GetType().Name.Replace("Adapter", ""); }  }

        // 如果是 transactionMsg 先记录 避免错误重送 ,  Doaction , 回传结果
        protected abstract object CheckModel();
        protected abstract T2 DoAction(object bMsg);
        public AbstractDataAdapter(string data)
        {
            _input = data;
             inModel = JsonConvert.DeserializeObject<T1>(data);
        }
        public  string ProcessMsg()
        {
            T2 retModel;
            startTime = DateTime.Now;
            try
            {
                var nextModel = this. CheckModel();
                retModel = DoAction(nextModel);
            }
            catch (Exception ex)
            {
                retModel = Activator.CreateInstance<T2>();
                retModel.Result = false;
                retModel.Msg = ex.Message;
            }
           
            string _output = JsonConvert.SerializeObject(retModel);
            writeDbLog(_input, _output, retModel.Result, "ATSToICT");
            return _output;
        }

        private void writeDbLog(string input, string output, bool boolRes, string direction)
        {
            //
        string sql = @"
               INSERT INTO PPSUSER.I_INTERFACE_LOG(INTERFACE_NAME,STATUS,TASKNO,KEY1,KEY2,KEY3,STARTTIME,ENDTIME,OWNER,RESULT_MESSAGE,ORIGIN_DATA)
               VALUES (:INTERFACE_NAME,:STATUS,:TASKNO,:KEY1,:KEY2,:KEY3,:STARTTIME,:ENDTIME,:OWNER,:RESULT_MESSAGE,:ORIGIN_DATA)";
            Dictionary<string, object> trans = new Dictionary<string, object>();
            trans.Add(sql, new List<Db_I_INTERFACE_LOG> {
                new Db_I_INTERFACE_LOG{
                    INTERFACE_NAME = callAction,
                    STARTTIME = startTime,
                    ENDTIME = DateTime.Now,
                    TASKNO = taskNo,
                    KEY1 =key1,
                    KEY2 =key2,
                    KEY3 = key3,
                    STATUS = boolRes?"Y":"N",
                    OWNER = direction,
                    ORIGIN_DATA = input,
                    RESULT_MESSAGE = output
                }
            });

            #region transLog
            if (transLog != null && transLog.Count > 0) {
                string delSql = "DELETE PPSUSER.I_TRANSACTIONTASKLOG T WHERE T.TASKNO = :TASKNO";
                trans.Add(delSql, transLog.FirstOrDefault());
                transLog = transLog.Select(x => {
                    //IN , OUT , MOVE
                    int piv = x.DIRECTION == "OUT" ? -1 : x.OPTYPE == "MOVE" ? 0 : 1;
                    x.OPQTY = piv * x.QTY;
                    x.OPCARTONQTY = piv * x.CARTONQTY;
                    return x;
                }).ToList();
                string logsql = @"
                  INSERT INTO PPSUSER.I_TRANSACTIONTASKLOG (TASKNO,PALLETTROLLYNO,OPTYPE,DIRECTION,PART_NO,QTY,CARTONQTY,OPQTY,OPCARTONQTY,STATUS,CHKSTATUS,CDT,INDATA)
                 VALUES (:TASKNO,:PALLETTROLLYNO,:OPTYPE,:DIRECTION,:PART_NO,:QTY,:CARTONQTY,:OPQTY,:OPCARTONQTY,:STATUS,'N',SYSDATE,:INDATA)";
                trans.Add(logsql, transLog);
                trans.Add("UPDATE PPSUSER.I_TRANSACTIONTASKLOG T SET T.STATUS = :FLAG WHERE T.TASKNO = :TASKNO",
                    new { TASKNO = transLog.First().TASKNO, FLAG = boolRes?"S":"E" });
            }
            #endregion
            ClientUtils.DoExtremeSpeedTransaction(trans);
        }

    }
}
