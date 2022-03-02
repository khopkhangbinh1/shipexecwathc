
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Service
{
    public class CommonHelper
    {
        public static Dictionary<string, Delegate> _Delagates { get; set; }
        public static string _LogPath { get; set; }
        public const string _LogFile = "ICTGetechWCF.txt";


        public static string GeTechUrl { get; set; }

        public static void SendMail(string subject, string body, string[] emails)
        {
            _Delagates["SENDMAIL"].DynamicInvoke(subject, body, emails);
        }

        public static void WriteLog(string text)
        {
            _Delagates["WRITELOG"].DynamicInvoke(_LogPath, _LogFile, text);
        }
    }
}
