using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPSSelfJobWcf
{
    [ServiceContract]
    public interface IPPSSelfJobService
    {
        [OperationContract]
        //一次循环检查数量 iprecount 1~400
        //一次检查库存总数 0&空 就是全检查
        //
        //void AutoWMSMarinaCheck(string siprecount, string sichecktotalcount, Delegate wlog);
        string AutoWMSMarinaCheck(Int32 iprecount, Int32 ichecktotalcount);

    }

}
