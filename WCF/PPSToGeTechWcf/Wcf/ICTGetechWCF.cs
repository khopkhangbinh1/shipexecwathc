using Newtonsoft.Json;
using OperationWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfICTGeTech.Interface;
using WcfICTGeTech.Model;
using WcfICTGeTech.Service;
using WcfICTGeTech.Service.OperationAdapter;

namespace WcfICTGeTech.Wcf
{
    public class ICTGetechWCF : HttpHosting, IGeTechToICT
    {
        private Dictionary<string, string> _settings { get; set; }

        public ICTGetechWCF()
        {
            _settings = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(HostingSettings)) {
                _settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(HostingSettings);
                if (string.IsNullOrEmpty(CommonHelper.GeTechUrl) && _settings.ContainsKey("GeTechUrl")){
                    CommonHelper.GeTechUrl =  _settings["GeTechUrl"];
                }
            }
        }

        public override void OnStart()
        {
            CommonHelper._Delagates = Delegates;
            CommonHelper._LogPath = LogPath;


        }

        public override void OnStop()
        {
        }

        #region  GeTech 呼叫 Method

        public string ATSStockInCheck(string data)
        {
            return doAction<ATSStockInCheckAdapter>(data);
        }

        public string ATSStockIn(string data)
        {
            //
            return doAction<ATSStockInAdapter>(data);
        }

        public string PPSBOMReleaseResponse(string data)
        {
            return doAction<PPSBOMReleaseResponseAdapter>(data);
        }

        public string ATSPickComplete(string data)
        {
            return doAction<ATSPickCompleteAdapter>(data);
        }

        public string StockInConfirm(string data)
        {
            return doAction<StockInConfirmAdapter>(data);
        }

        public string TrolleyMoveNotice(string data)
        {
            return doAction<TrolleyMoveNoticeAdapter>(data);
        }

        public string Sync_Location(string data)
        {
            return doAction<Sync_LocationAdapter>(data);
        }

        public string Sync_Trolley(string data)
        {
            return doAction<Sync_TrolleyAdapter>(data);

        }

        public string SendMailNotify(string data)
        {
            return doAction<SendMailNotifyAdapter>(data);
        }

        #endregion

        #region 传送 到 GeTech
        public string PostSync_ICT_Part(string data) {
            return doAction<Sync_ICT_PartAdapter>(data);
        }

        public string PostCleanStock(string data)
        {
            return doAction<CleanStockAdapter>(data);
        }

        public string PostTrolleyBackToStock(string data)
        {
            return doAction<TrolleyBackToStockAdapter>(data);
        }

        public string PostStockInNotify(string data)
        {
            return doAction<StockInNotifyAdapter>(data);
        }
        public string PostPackoutNotify(string data)
        {
            return doAction<PackoutNotifyAdapter>(data);
        }

        public string PostPPSQHoldUndo(string data)
        {
            return doAction<PPSQHoldUndoAdapter>(data);
        }

        public string PostPPSStockTakeByICTPN(string data)
        {
            return doAction<PPSStockTakeByICTPNAdapter>(data);
        }

        public string PostPPSStockTakeByLog(string data)
        {
            return doAction<PPSStockTakeByLogAdapter>(data);
        }

        public string PostPPSPickComplete(string data)
        {
            return doAction<PPSPickCompleteAdapter>(data);
        }

        public string PostPPSShippingNotice(string data)
        {
            return doAction<PPSShippingNoticeAdapter>(data);
        }

        public string PostPPSBOMRelease(string data)
        {
            return doAction<PPSBOMReleaseAdapter>(data);
        }

        public string MultiPalletCombine(string data)
        {
            return doAction<MultiPalletCombineAdapter>(data);
        }
        #endregion


        #region private function
        private void writeLog(string msg) {
            WriteLog(CommonHelper._LogFile, msg);
        }

        private string  doAction<T>(string data) where T : IMessageAction {
            T t = (T)Activator.CreateInstance(typeof(T), data);
            return t.ProcessMsg();
        }

        #endregion
    }
}
