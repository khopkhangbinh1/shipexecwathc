using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickList.Model
{
    class PackoutLogicModel
    {
        public class PackoutLogicRequestModel
        {
            public requestsn[] SN { get; set; }
        }

        public class requestsn
        {
            public string SN { get; set; }
        }


        public class PackoutLogicReturnModel
        {
            public string SN { get; set; }
            public string RESULT { get; set; }
        }

    }
}
