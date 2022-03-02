using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.PPSPick
{
    public class baseinfo
    {
        public string Trolley_no { get; set; }
        public string Trolley_Line_No { get; set; }
        public string Sides_No { get; set; }//面
        public string Level_No { get; set; }//层
        public string Point_No { get; set; }//点
        public string Customer_SN { get; set; }
        public string Carton_No { get; set; }
        public string Pallet_No { get; set; }
        public string Emp_ID { get; set; }
        public string KeyPart { get; set; }
        public static int MaxQtyByLine { get; set; } = 0;
        public int Check_Index { get; set; } = 1;
        public string OriginPallet_no { get; set; }
        public bool isSingle { get; set; }
    }
}