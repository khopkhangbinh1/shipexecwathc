using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Interface
{
    [ServiceContract]
    public interface IICTToGeTech
    {
        /// <summary>
        /// 工单发料
        /// </summary>
        [OperationContract]
        string PPSBOMRelease(string data);

        /// <summary>
        /// 发货请求
        /// </summary>
        [OperationContract]
        string PPSShippingNotice(string data);

        /// <summary>
        /// PPS 拣选完成状态更新
        /// </summary>
        [OperationContract]
        string PPSPickComplete(string data);

        /// <summary>
        /// 系统盘点依据Log
        /// </summary>
        [OperationContract]
        string PPSStockTakeByLog(string data);

        /// <summary>
        /// 系统盘点依据料号
        /// </summary>
        [OperationContract]
        string PPSStockTakeByICTPN(string data);

        /// <summary>
        /// Hold unHold 通知
        /// </summary>
        [OperationContract]
        string PPSQHoldUndo(string data);

        /// <summary>
        /// 产线Packout 到CheckIn
        /// </summary>
        [OperationContract]
        string PackoutNotify(string data);

        /// <summary>
        /// CheckIn 完成入库通知
        /// </summary>
        [OperationContract]
        string StockInNotify(string data);

        /// <summary>
        /// 金刚车拣选完通知回库
        /// </summary>
        [OperationContract]
        string TrolleyBackToStock(string data);

        /// <summary>
        /// 强制清除库存(测试用)
        /// </summary>
        [OperationContract]
        string CleanStock(string data);

        /// <summary>
        /// 同步料号
        /// </summary>
        [OperationContract]
        string Sync_ICT_Part(string data);

        /// <summary>
        /// 合并栈板
        /// </summary>
        [OperationContract]
        string MultiPalletCombine(string data);

    }
}
