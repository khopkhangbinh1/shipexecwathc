using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.DBTool
{
    public class ClientUtils
    {
        public static string UserPara1 { get; internal set; }

        internal static DataSet ExecuteProc(string procedurename, object[][] param)
        {
            throw new NotImplementedException();
        }

        internal static DataSet ExecuteSQL(string sql, object[][] param)
        {
            throw new NotImplementedException();
        }

        internal static DataSet ExecuteSQL(string sql)
        {
            throw new NotImplementedException();
        }
    }
}