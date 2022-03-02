using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIWarehouseOUT.WCF
{
   public class CommonModel
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

        /// <summary>
        /// Watch 的栈板入库
        /// </summary>
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
            public string LINE { get; set; }
            public string TRANSFER { get; set; }

            public string in_guid { get; set; }
            public string work_order { get { return WO; } }
            public string serial_number { get { return SN + "01"; } }
            public string customer_sn { get { return SN; } }
            public string carton_no { get { return BOXID; } }
            public string pallet_no { get { return PALLETID; } }
            // 加入包装方式
            public string part_no { get { return PN + "-" + BATCHTYPE; } }
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

        /// <summary>
        /// Watch 原材入库
        /// </summary>
        public class RAWINRETURNMODEL
        {
            public string INSN { get; set; }
            public string RESULT { get; set; }
            public string MSG { get; set; }
            public MATERIALQTY[] MATERIALQTY { get; set; }
        }

        public class MATERIALQTY
        {
            public string BOXID { get; set; }
            public string PALLETID { get; set; }
            public string PN { get; set; }
            public int QTY { get; set; }
            public string TRANSFERDN { get; set; }
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

            /// <summary>线别   </summary>
            public string ZZLINE { get; set; }

        }

        public class ERPResModel
        {

            //     数据记录号
            public string ZRECORD_NO { get; set; }
            //     消息类型
            public string ZZMSGTYPE { get; set; }
            //     消息内容
            public string ZZMSG { get; set; }
            public bool IsSuccess
            {
                get
                {
                    return ZZMSGTYPE == "S";
                }
            }
        }

        /// <summary>
        /// 反馈MES入库OK的返回值list
        /// </summary>
        public class FBMESRETURNMODEL
        {
            public string Result { get; set; }
        }

        /// <summary>
        /// 入库 SAP的反馈
        /// </summary>
        public class WMSISAPRETURNMODEL
        {
            public string ZRECORD_NO { get; set; }
            public string ZZMSGTYPE { get; set; }
            public string ZZMSG { get; set; }
            public string XBLNR { get; set; }
            public string VKORG { get; set; }
            public string KSCHL { get; set; }
            public string KUNNR { get; set; }
            public DateTime JBDATE { get; set; }
            public DateTime LAEDA { get; set; }
            public string VBELN { get; set; }
            public string EBELN { get; set; }
            public bool IsSuccess { get; set; }
            public string PACK { get; set; }
            public string PACK_CONT { get; set; }
        }


    }
}
