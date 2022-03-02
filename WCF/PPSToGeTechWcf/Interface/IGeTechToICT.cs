using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Interface
{
    [ServiceContract]
    public interface IGeTechToICT
    {
        /// <summary>
        /// WMS发送pallet ID 取得栈板资讯
        /// </summary>
        [OperationContract]
        string ATSStockInCheck(string data);

        /// <summary>
        /// WMS发送pallet ID 入库请求
        /// </summary>
        [OperationContract]
        string ATSStockIn(string data);

        /// <summary>
        /// 工单发料结果回传
        /// </summary>
        [OperationContract]
        string PPSBOMReleaseResponse(string data);

        /// <summary>
        /// ATS 人工拣选出库回传
        /// </summary>
        [OperationContract]
        string ATSPickComplete(string data);

        /// <summary>
        /// 金刚车入库确认
        /// </summary>
        [OperationContract]
        string StockInConfirm(string data);

        /// <summary>
        /// 金刚车移动完成通知
        /// </summary>
        [OperationContract]
        string TrolleyMoveNotice(string data);

        /// <summary>
        /// 同步 储位
        /// </summary>
        [OperationContract]
        string Sync_Location(string data);

        /// <summary>
        /// 同步 金刚车
        /// </summary>
        [OperationContract]
        string Sync_Trolley(string data);

        /// <summary>
        /// 发送邮件
        /// </summary>
        [OperationContract]
        string SendMailNotify(string data);
    }
}
