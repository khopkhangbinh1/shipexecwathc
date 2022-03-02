using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace iMWS.Api.Models.WCF
{
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IEOSGateway
    {
        [OperationContract]
        string StockInConfirm(string data);
        [OperationContract]
        string TrolleyCombine(string data);
        [OperationContract]
        string MailNotice(string data);
        [OperationContract]
        string ManualStockInConfirm(string data);

        [OperationContract]
        string ManualStockOutConfirm(string data);

        [OperationContract]
        string GetShippingLabel(string data);

        [OperationContract]
        string PalletComplete(string data);

        [OperationContract]
        string GetPalletDoc(string data);

        [OperationContract]
        string EOSTransPickStatus(string data);

        [OperationContract]
        string EOSTransPackStatus(string data);

        [OperationContract]
        string AGVArrivedNotify(string data);

        [OperationContract]
        string GetCartonPrintDoc(string data);

        [OperationContract]
        string PostPackoutNotify(string data, string key);

        [OperationContract]
        string PostStockInNotify(string data, string key);

        [OperationContract]
        string PostShippingNotice(string data, string key);

        [OperationContract]
        string PostHoldCarton(string data, string key);
        [OperationContract]
        string PostAutoHold();
        [OperationContract]
        string PostBasicData(string data);

    }
}