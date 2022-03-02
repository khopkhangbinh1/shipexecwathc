using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSPick
{

    public class PPSPickProcModel
    {
        public string PickPalletNo { get; set; }
        public string ErrMsg { get; set; }
        public string Strlbl { get; set; }
    }

    public class PPSbasicparameterModel
    {
        public string ErrMsg { get; set; }
        public string outparavalue { get; set; }
    }

    public class PPSMarinaFlagModel
    {
        public string ErrMsg { get; set; }
        public string outmarinaflag { get; set; }
        public string outpackoutflag { get; set; }
    }

    public class PPSSNModel
    {
        public string SN { get; set; }
        public string CartonNO { get; set; }
    }

    public class MarinaLogModel
    {
        public string inguid { get; set; }
        public string inserverip { get; set; }
        public string inurl { get; set; }
        public string insn { get; set; }
        public string inrequest { get; set; }
        public string inresult { get; set; }
        public string inempno { get; set; }
    }

    public class PPSSNOutModel
    {
        public string customer_sn { get; set; }
        public string carton_no { get; set; }
    }

    public class PPSSNOutModel2
    {
        public string customer_sn { get; set; }
        public string product_name { get; set; }
    }

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