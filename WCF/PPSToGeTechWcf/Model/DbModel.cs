using MESModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfICTGeTech.Model
{
    [TableName("PPSUSER.I_INTERFACE_LOG")]
    public class Db_I_INTERFACE_LOG
    {
        public string INTERFACE_NAME { get; set; }
        public string TASKNO { get; set; }
        public string STATUS { get; set; }
        public string KEY1 { get; set; }
        public string KEY2 { get; set; }
        public string KEY3 { get; set; }
        public DateTime STARTTIME { get; set; }
        public DateTime ENDTIME { get; set; }

        public string OWNER { get; set; }
        [DbLlob("CLOB")]
        public string RESULT_MESSAGE { get; set; }

        [DbLlob("CLOB")]
        public string ORIGIN_DATA { get; set; }
    }

    [TableName("PPSUSER.I_TransactionTaskLog")]
    public class Db_I_TransactionTaskLog
    {
        public string TASKNO { get; set; }
        public string PALLETTROLLYNO { get; set; }
        public string OPTYPE { get; set; }
        public string DIRECTION { get; set; }
        public string PART_NO { get; set; }
        public int QTY { get; set; }
        public int CARTONQTY { get; set; }
        public int OPQTY { get; set; }
        public int OPCARTONQTY { get; set; }
        public string STATUS { get; set; }
        public string CHKSTATUS { get; set; }
        public DateTime CDT { get; set; }
        [DbLlob("CLOB")]
        public string INDATA { get; set; }
    }

    [TableName("PPSUSER.T_Trolley_Line_Info")]
    public class Db_T_Trolley_Line_Info
    {
        public string TROLLEY_LINE_NO { get; set; }
        public string TROLLEY_NO { get; set; }
        public string SIDES_NO { get; set; }
        public int LEVEL_NO { get; set; }
        public int SEQ_NO { get; set; }
        public int MAXQTY { get; set; }
        public int ISENABLED { get; set; }
        public string LINE_NO { get; set; }
        public string GROUP_CODE { get; set; }
        public int USEDQTY { get; set; }
        public DateTime CDT { get; set; }
        public string TROLLEY_TYPE { get; set; }
        public string EMPNO { get; set; }
        public DateTime UDT { get; set; }
        public int PACKQTY { get; set; }
    }

    [TableName("PPSUSER.WMS_LOCATION")]
    public class Db_WMS_LOCATION
    {
        public string LOCATION_NO { get; set; }
        public string LOCATION_NAME { get; set; }
        public decimal WAREHOUSE_ID { get; set; }
        public string LOCATION_TYPE { get; set; }
        public decimal UPDATE_USERID { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string ENABLED { get; set; }
        public string REGION { get; set; }
    }

    [TableName("PPSUSER.WMS_WAREHOUSE")]
    public class Db_WMS_WAREHOUSE
    {
        public decimal WAREHOUSE_ID { get; set; }
        public string WAREHOUSE_NO { get; set; }
    }
}
