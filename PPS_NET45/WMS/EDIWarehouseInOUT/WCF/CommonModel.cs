using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIWarehouseInOUT.WCF
{
    class CommonModel
    {
        public class ResMsg
        {
            public bool result { get; set; }
            public string msg { get; set; }
        }

        public class FGINMODEL
        {
            public string INSN { get; set; }
        }

        public class FGINRETURNMODEL
        {
            public string INSN { get; set; }
            public string RESULT { get; set; }
            public string MSG { get; set; }
            public SNLIST[] SNLIST { get; set; }
        }

        public class SNLIST
        {
            public string SN { get; set; }
            public string WO { get; set; }
            public string BATCHTYPE { get; set; }
            public string LOAD_ID { get; set; }
            public string BOXID { get; set; }
            public string PALLETID { get; set; }
            public string PN { get; set; }
            public string MODEL { get; set; }
            public string REGION { get; set; }
            public string CUSTPN { get; set; }
            public string QHOLDFLAG { get; set; }
            public string TROLLEYNO { get; set; }
            public string TROLLEYLINENO { get; set; }
            public string TROLLEYLINENOPOINT { get; set; }
            public string DN { get; set; }
            public string ITEMNO { get; set; }
            public string GOODSTYPE { get; set; }

            public string in_guid { get; set; }
            public string work_order { get { return WO; } }
            public string serial_number { get { return SN + "01"; } }
            public string customer_sn { get { return SN; } }
            public string carton_no { get { return BOXID; } }
            public string pallet_no { get { return PALLETID; } }
            // 加入包装方式
            public string part_no { get { return PN+"-"+BATCHTYPE; } }
            public string mpn { get { return CUSTPN; } }
            public string hold_flag { get { return QHOLDFLAG; } }
            public string trolley_line_no { get { return TROLLEYLINENO?.ToString(); } }
            public string point_no { get { return TROLLEYLINENOPOINT?.ToString(); } }
            public string delivery_no { get { return DN?.ToString(); } }
            public string line_item { get { return ITEMNO?.ToString(); } }
            public string trolley_no { get { return TROLLEYNO?.ToString(); } }
            public string product_name { get; set; }
            public string warehouse_id { get; set; }
            public string location_id { get; set; }

            
            
        }
        public class PNRETURNMODEL
        {
            public string INSN { get; set; }
            public string RESULT { get; set; }
            public string MSG { get; set; }
            public PNLIST[] PNLIST { get; set; }
        }

        public class PNLIST
        {
            public string PN { get; set; }
            public string CUST_MODEL_TYPE { get; set; }
            public string UPC_CODE { get; set; }
            public object JAN_CODE { get; set; }
            public string COUNTRY { get; set; }
            public string REGION { get; set; }
            public string CUST_PN { get; set; }
            public string SCC_CODE { get; set; }
        }
        public class ERPStockInModel
        {
            /// <summary>栈板号</summary>
            public string SPCQN { get; set; }
            /// <summary>生产日期入库日</summary>
            public string HSDAT { get; set; }
            public ERPStockInItemModel[] ITEMS { get; set; }
        }

        public class ERPStockInItemModel
        {
            /// <summary>检验日期  非必填</summary>
            public string QMDAT { get; set; }
            /// <summary>检验时间 非必填</summary>
            public string QMTIM { get; set; }
            /// <summary>工单号</summary>
            public string AUFNR { get; set; }
            /// <summary>物料编号</summary>
            public string MATNR { get; set; }
            /// <summary>基本计量单位  非必填</summary>
            public string MEINS { get; set; }
            /// <summary>入库量</summary>栈板下工单下SN 总数
            public string GAMNG { get; set; }
            /// <summary> 用户名 </summary>
            public string UNAME { get; set; }
            /// <summary>库存地点 非必填</summary>
            public string LGORT { get; set; }
            /// <summary>送检日期 非必填  </summary>
            public string ZSJRQ { get; set; }
            /// <summary>备注  非必填  </summary>
            public string REMARK { get; set; }

            /// <summary> 批次 =>工单号 </summary>
            public string CHARG { get; set; }
            /// <summary>单/多包  非必填  </summary>
            public string ZSSMB { get; set; }

        }

        public class ERPResModel
        {

            //     数据记录号
            public string ZRECORD_NO { get; set; }
            //     消息类型
            public string ZZMSGTYPE { get; set; }
            //     消息内容
            public string ZZMSG { get; set; }
            public bool IsSuccess { get {
                    return ZZMSGTYPE == "S";
                } }
        }
    }
}
