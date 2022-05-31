using CarrierWCF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarrierWCF.Wcf
{
    public interface ICarrierToICTService
    {
    }
    [ServiceContract]
    [XmlSerializerFormat]
    public interface IICTToCarrierService
    {
        [OperationContract]
        string Ship(string data);
        [OperationContract]
        string Void(string data);

        [OperationContract]
        string GetGlbMSNByTrackingNo(ShipModel model, string GUID);

        [OperationContract]
        string RePrint(string data, string GUID);

        [OperationContract]
        string ShipDevTest(string data);

        [OperationContract]
        string SendMailAlert(string carton, string msg);
    }
}
