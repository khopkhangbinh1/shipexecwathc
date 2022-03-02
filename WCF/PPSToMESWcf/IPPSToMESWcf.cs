using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using static PPSToMESWcf.MODEL.CommonModel;

namespace PPSToMESWcf
{
    [ServiceContract]
    public interface IPPSToMESWcf
    {
        [OperationContract]
        string PPSQHoldSN(string model);
        [OperationContract]
        string PPSQHoldSNList(string model);

        [OperationContract]
        string PPSTrolleyNoStatus(string model);

        //[OperationContract]
        //string TestFGIN(string model);

        //20200323取消这个接口
        //[OperationContract]
        //string PPSUpdateSNDN(string model);

    }
}
