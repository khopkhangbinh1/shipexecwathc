using iMWS.Api.Models.Bean;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.ViewModel
{
    public class ExecuteTrolleyCheckInModel
    {
        public baseinfo bean { get; set; }

        public List<baseinfo> dataSource { get; set; }
    }
}