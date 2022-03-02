using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.Bean
{
    public class TrolleyBean
    {
        public string custom_sn { get; set; }
        public string trolley_no { get; set; }
        public string pallet_no { get; set; }
        public string sides_no { get; set; }
        public string level_no { get; set; }
        public int pointno { get; set; }
        public string UUID { get; set; }
        public string ictpartno { get; set; }
        public string CartonNo { get; set; }
        public string trolley_line_no { get; set; }
        public  string emp_id { get; set; }

    }
}