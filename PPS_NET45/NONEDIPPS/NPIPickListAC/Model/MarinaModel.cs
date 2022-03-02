using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPIPickListAC.Model
{
    class MarinaModel
    {

        public class MarinaRequestModel
        {
            public string STATION_TYPE { get; set; }
            public string SITE { get; set; }
            public Request[] request { get; set; }
        }

        public class Request
        {
            public string SerialNumber { get; set; }
        }

        public class MarinaReturnModel
        {
            public Response[] response { get; set; }
        }

        public class Response
        {
            public string SerialNumber { get; set; }
            public string ApChipID { get; set; }
            public string UniqueChipID { get; set; }
            public string OKtoShipwithInstalledOS { get; set; }
            public Currentinstalledos CurrentInstalledOS { get; set; }
        }

        public class Currentinstalledos
        {
            public string Variant { get; set; }
            public string DeviceClass { get; set; }
            public string BuildNumber { get; set; }
            public string BuildTrain { get; set; }
            public string uniqueBuildId { get; set; }
            public int tsaId { get; set; }
            public int ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }

    }
}
