using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPSToMESWcf
{
    class PPSToMESServiceBLL
    {
        PPSToMESServiceDAL pd = new PPSToMESServiceDAL();
        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("S"))
            { insn = insn.Substring(1); }

            return insn;

        }

        public string ExecuteQHoldSN(string strsn, string strqholdflag , out string errmsg)
        {
            errmsg = string.Empty;
            string strResult = pd.ExecuteQHoldSNBySP(strsn, strqholdflag, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        public string ExecuteQHoldSN2(string strguid, out string errmsg)
        {
            errmsg = string.Empty;
            string strResult = pd.ExecuteQHoldSNBySP2(strguid, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        public string ExecuteCheckTrolleyNoStatus(string strTrolleyNo,  out string errmsg)
        {
            errmsg = string.Empty;
            string strResult = pd.ExecuteCheckTrolleyNoStatusBySP(strTrolleyNo, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        
        public string ExecuteUpdateSNDN(string strsn, string  strdn,string strdnline,string  strworkorder, out string errmsg)
        {
            errmsg = string.Empty;
            string strResult = pd.ExecuteUpdateSNDNBySP(strsn, strdn, strdnline, strworkorder, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        

    }
}
