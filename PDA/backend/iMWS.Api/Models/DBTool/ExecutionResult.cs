using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.DBTool
{
    public class ExecutionResult
    {
        public DataSet Anything { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}