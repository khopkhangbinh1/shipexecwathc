﻿using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace WeightAC
{
    public class WeightDal
    {
        /// <summary>
        /// 通过栈板号获取shipmentId
        /// </summary>
        /// <param name="strPalletNo">栈板号</param>
        /// <returns>shipmentId</returns>
        public string getShipmentIdByPalletNo(string strPalletNo)
        {
            string shipmentId = string.Empty;
            string sql = "SELECT shipment_id FROM NONEDIPPS.g_ds_pallet_t WHERE END_PALLETNO =:palletNo";
            object[][] paramArry = new object[1][];
            paramArry[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", strPalletNo };
            DataSet dataSet = ClientUtils.ExecuteSQL(sql, paramArry);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                shipmentId = dataSet.Tables[0].Rows[0][0].ToString();
            }
            return shipmentId;
        }

        /// <summary>
        /// 获取shipmentID的pallet数量
        /// </summary>
        /// <param name="strShipmentId"></param>
        /// <returns></returns>
        public float getPalletQtyByShipmentId(string strShipmentId)
        {
            string qtyStr = string.Empty;
            float qty = 0;
            string sql = "SELECT sum(qty) FROM NONEDIPPS.g_ds_pallet_t WHERE shipment_id =:shipmentId";
            object[][] paramArry = new object[1][];
            paramArry[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentId", strShipmentId };
            DataSet dataSet = ClientUtils.ExecuteSQL(sql, paramArry);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                qtyStr = dataSet.Tables[0].Rows[0][0].ToString();
                if (!float.TryParse(qtyStr, out qty))
                {
                    throw new Exception("转换数据类型失败");
                }
            }
            return qty;
        }

        /// <summary>
        /// 获取check scan数量
        /// </summary>
        /// <param name="strShipmentId"></param>
        /// <returns></returns>
        public DataTable getScanQtyByShipmentId(string strShipmentId)
        {
            string qtyStr = string.Empty;
            //float qty = 0;
            string sql = "SELECT nvl(SUM(qty),0) FROM NONEDIPPS.g_ds_scandata_t WHERE shipment_id = :shipmentId AND check_flag = 'Y'";
            object[][] paramArry = new object[1][];
            paramArry[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentId", strShipmentId };
            DataSet dataSet = ClientUtils.ExecuteSQL(sql, paramArry);
            if (dataSet != null)
            {
                return dataSet.Tables[0];
            }
            else
            {
                return null;
            } 
        }

        public string PPSInsertWorkLogByProcedure(string insn, string inwc, string macaddress, out string errmsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwc", inwc };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "macaddress", macaddress };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_PPS_INSERTWORKLOG", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        public string CheckShipmentIDHoldByProcedure(string insn, out string errmsg)
        {
            //insn 是PACKPALLETNO
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_PPS_CHECKSHIPMENTHOLD", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        //SP_WEIGHT_CHECKPALLETSAWB

        public string CheckPalletNoSAWBByProcedure(string insn, out string outregion, out string errmsg)
        {
            //insn 是PACKPALLETNO
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "packpalletno", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outregion", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_WEIGHT_CHECKPALLETSAWB2", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            outregion = ds.Tables[0].Rows[0]["outregion"].ToString();
            return errmsg;


        }
        public string CheckPalletNoSAWBByProcedure(string insn, out string errmsg)
        {
            //insn 是PACKPALLETNO
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "packpalletno", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_WEIGHT_CHECKPALLETSAWB", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;
           

        }

        public string CheckShipmentWeightStatusSP(string insn, out string strregion, out string errmsg)
        {
            //insn 是PACKPALLETNO
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "packpalletno", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outregion", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_WEIGHT_CHECKSHIPMENTSTATUS2", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            strregion = ds.Tables[0].Rows[0]["outregion"].ToString();
            return errmsg;


        }

        public DataSet ShowShipmentPalletInfoDataTable(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                                select a.shipment_id  as 集货单号,
                                                a.pallet_no    as 栈板号,
                                                a.carton_qty   as 箱数,
                                                a.weight       as 重量,
                                                a.pick_carton  as PICK箱数,
                                                a.pack_carton  as PACK箱数,
                                                a.check_result as CHECK结果,
                                                a.SECURITY SECURITY
                                           from NONEDIPPS.t_shipment_pallet a
                                          where a.shipment_id in
                                                (select shipment_id
                                                   from NONEDIPPS.t_shipment_pallet
                                                  where pallet_no = '{0}')
                                    ", inputSno);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public DataTable GetPalletAppleCareInfo(string strPalletNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PALLET_NO", strPalletNo };
            return ClientUtils.ExecuteSQL(@"SELECT bb.PALLET_NO,
aa.SHIPMENT_ID,
to_char(aa.SHIPPING_TIME,'yyyy-MM-dd') as DELIVERY_DATE,
aa.CARRIER_NAME,
aa.HAWB,
bb.QTY,
bb.WEIGHT,
nvl(bb.CARTON_QTY,0)+ nvl(bb.EMPTY_CARTON,0) AS FULL_CARTON_QTY,
bb.CARTON_QTY,
bb.EMPTY_CARTON,
cc.DELIVERY_NO,
cc.LINE_ITEM,
cc.MPN,
sum(cc.ASSIGN_QTY) AS DN_QTY
from NONEDIPPS.t_shipment_info aa
INNER join NONEDIPPS.t_shipment_pallet bb
on aa.shipment_id = bb.shipment_id
INNER JOIN NONEDIPPS.T_PALLET_ORDER cc ON aa.shipment_id=cc.SHIPMENT_ID AND bb.pallet_no=cc.PALLET_NO
WHERE bb.pallet_no=:PALLET_NO
GROUP BY bb.PALLET_NO,
aa.SHIPMENT_ID,
aa.SHIPPING_TIME,
aa.CARRIER_NAME,
aa.HAWB,
bb.QTY,
bb.WEIGHT,
bb.FULL_CARTON_QTY,
bb.CARTON_QTY,
bb.EMPTY_CARTON,
cc.DELIVERY_NO,
cc.LINE_ITEM,
cc.MPN
ORDER BY 
cc.DELIVERY_NO,
cc.LINE_ITEM,
cc.MPN ", sqlparams).Tables[0];
        }

        public DataSet GetPalletLabelDataTableDAL(string inpalletno, string isSAWB, string inregion)
        {
            string sql = string.Empty;
            //20190827
            //1如果是SAWB继续用SAWB的模板
            //2.1 剩余的如果是AMR 和EMEIA 用简约版
            //2.2 剩余的如果是PAC则保持不变
            if (inregion.Equals("AMR") || inregion.Equals("EMEIA"))
            {
                if (isSAWB.Equals("SAWB"))
                {
                    #region SAWB
                    sql = string.Format(@"select DISTINCT bb.pallet_no,
                                                  bb.real_pallet_no,
                                                  case  
                                                      when aa.shipment_type = 'DS' then
                                                        aa.carrier_code 
                                                    else  
                                                (SELECT distinct SCACCODE
                                             FROM NONEDIOMS.OMS_CARRIER_TRACKING_PREFIX D
                                            where trim(D.carriercode) = aa.carrier_code
                                              and D.ShipMode = aa.transport
                                              and D.isdisabled = '0'
                                              and D.type = 'HAWB') end carrier_code,
                                                  aa.hawb,aa.poe gateway,
                                                  bb.weight,  
                                                  bb.empty_carton + bb.carton_qty fullcartonqty,  
                                                  bb.empty_carton,  
                                                  bb.qty totalqty,
                                                  to_char(aa.shipping_time, 'dd/mm/yyyy') cdt,  
                                                  NONEDIPPS.t_pallet_bottomdesc(bb.pallet_no) mix_desc,  
                                                  aa.shipment_id,  
                                                  '' itemcustpo,  
                                                  '' itemcustpoline,  
                                                  '' delivery_no,  
                                                  '' line_item,  
                                                  decode(aa.shipment_type, 'FD', 'HUB', aa.shipment_type) shipmenttype,  
                                                  cc.mpn,cc.ictpn,  
                                                  cc.assign_qty,  
                                                  cc.gccn,  
                                                  cc.poe
                                    from NONEDIPPS.t_shipment_info aa
                                    join NONEDIPPS.t_shipment_pallet bb
                                      on aa.shipment_id = bb.shipment_id
                                    join(select a.pallet_no,
                                                 b.hawb as gccn,
                                                 b.mpn,
                                                 b.poe, b.ictpn,
                                                 sum(a.assign_qty) as assign_qty
                                            from NONEDIPPS.t_pallet_order a
                                            join NONEDIPPS.t_order_info b
                                         on a.delivery_no = b.delivery_no
                                        and a.line_item = b.line_item
                                             and a.ictpn = b.ictpn
                                            join NONEDIPPS.t_shipment_sawb c
                                              on b.shipment_id = c.shipment_id
                                             and a.shipment_id = c.sawb_shipment_id
                                           where a.pallet_no = '{0}'
                                           group by a.pallet_no, b.hawb, b.mpn, b.poe, b.ictpn) cc
                                      on bb.pallet_no = cc.pallet_no
                                    join(select *
                                            from(select pallet_no, weight, cdt
                                                    from NONEDIPPS.t_pallet_weight_log
                                                  where pallet_no = '{1}'
                                                     AND PASS = '1'
                                                   order by cdt desc)
                                           where rownum = 1) dd
                                      on bb.pallet_no = dd.pallet_no
                                    join NONEDIPPS.t_pallet_order ee
                                      on dd.pallet_no = ee.pallet_no
                                    left join NONEDIPPS.t_940_unicode ff
                                      on ee.delivery_no = ff.deliveryno
                                     and ee.line_item = ff.custdelitem  
                                     order by cc.ictpn asc,
                                      cc.gccn asc,
                                      cc.poe asc
                                     ", inpalletno, inpalletno);
                    #endregion
                }
                else
                {
                    #region NOSAWB 简约版
                    sql = string.Format(@"select pallet_no,real_pallet_no, carrier_code,hawb,weight,
                                                fullcartonqty, empty_carton,totalqty,cdt,mix_desc,shipment_id,
                                                itemcustpo,itemcustpoline,delivery_no, line_item,
                                                mpn,ictpn,poe,gateway,gccn,shipmenttype,
                                                sum(assign_qty) assign_qty
                                           from (select  b.pallet_no,
                                                                 b.real_pallet_no,
                                                                 case
                                                                   when a.shipment_type = 'DS' then
                                                                    a.carrier_code
                                                                   else
                                                                    (SELECT distinct SCACCODE
                                                                       FROM NONEDIOMS.OMS_CARRIER_TRACKING_PREFIX D
                                                                      where trim(D.carriercode) = a.carrier_code
                                                                        and D.ShipMode = a.transport
                                                                        and D.isdisabled = '0'
                                                                        and D.type = 'HAWB')
                                                                 end carrier_code,
                                                                 a.hawb,
                                                                 b.weight,
                                                                 b.empty_carton + b.carton_qty fullcartonqty,
                                                                 b.empty_carton,
                                                                 b.qty totalqty,
                                                                 to_char(a.shipping_time, 'dd/mm/yyyy') cdt,
                                                                 case
                                                                   when a.region = 'EMEIA' and
                                                                        UPPER(e.SHIPPLANT) LIKE 'MIT%' then
                                                                    'MIT'
                                                                   when pallet_type = '001' then
                                                                    'DO NOT BREAK BULK'
                                                                   when pallet_type = '999' then
                                                                    'CONSOLIDATED'
                                                                 end mix_desc,
                                                                 c.shipment_id,
                                                                 decode(a.shipment_type, 'DS', '', e.itemcustpo) itemcustpo,
                                                                 decode(a.shipment_type, 'DS', '', e.itemcustpoline) itemcustpoline,
                                                                 decode(a.shipment_type, 'DS', '', c.delivery_no) delivery_no,
                                                                 decode(a.shipment_type, 'DS', '', c.line_item) line_item,
                                                                 c.mpn,
                                                                 c.ictpn,
                                                                 c.assign_qty,
                                                                 case
                                                                   when a.shipment_type = 'DS' and a.region = 'EMEIA' and
                                                                        a.poe = 'SA' then
                                                                    e.PORTOFENTRY
                                                                   else
                                                                    a.poe
                                                                 end poe,
                                                                 '' gateway,
                                                                 '' gccn,
                                                                 decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) shipmenttype
                                                   from NONEDIPPS.t_shipment_info a
                                                   join NONEDIPPS.t_shipment_pallet b
                                                     on a.shipment_id = b.shipment_id
                                                   join NONEDIPPS.t_pallet_order c
                                                     on b.pallet_no = c.pallet_no
                                                   join (select *
                                                          from (select pallet_no, weight, cdt
                                                                  from NONEDIPPS.t_pallet_weight_log
                                                                 where pallet_no = '{0}'
                                                                   AND PASS = '1'
                                                                 order by cdt desc)
                                                         where rownum = 1) d
                                                     on c.pallet_no = d.pallet_no
                                                   left join (select decode(itemcustpo,
                                                                           '',
                                                                           weborderno,
                                                                           null,
                                                                           weborderno,
                                                                           itemcustpo) itemcustpo,
                                                                    itemcustpoline,
                                                                    PORTOFENTRY,
                                                                    deliveryno,
                                                                    custdelitem,
                                                                    SHIPPLANT
                                                               from NONEDIPPS.t_940_unicode) e
                                                     on c.delivery_no = e.deliveryno
                                                    and c.line_item = e.custdelitem
                                                  where b.pallet_no = '{1}') aa
                                          group by pallet_no,real_pallet_no, carrier_code,hawb,weight,
                                                fullcartonqty, empty_carton,totalqty,cdt,mix_desc,shipment_id,
                                                itemcustpo,itemcustpoline,delivery_no, line_item,
                                                mpn,ictpn,poe,gateway,gccn,shipmenttype 
                                                order by ictpn asc
                                                ", inpalletno, inpalletno);
                    #endregion
                }
            }
            else
            {
                #region PAC 复杂版
                sql = string.Format(@"select DISTINCT b.pallet_no,
                                                 b.real_pallet_no,
                                                     case  
                                                       when a.shipment_type = 'DS' then
                                                         a.carrier_code 
                                                     else  
                                                 (SELECT distinct SCACCODE
                                              FROM NONEDIOMS.OMS_CARRIER_TRACKING_PREFIX D
                                             where trim(D.carriercode) = a.carrier_code
                                               and D.ShipMode = a.transport
                                               and D.isdisabled = '0'
                                               and D.type = 'HAWB') end carrier_code,
                                                 a.hawb,  
                                                 b.weight,  
                                                 b.empty_carton + b.carton_qty fullcartonqty,  
                                                 b.empty_carton,  
                                                 b.qty totalqty,
                                                 to_char(a.shipping_time, 'dd/mm/yyyy') cdt,  
                                                 NONEDIPPS.t_pallet_bottomdesc(b.pallet_no) mix_desc,  
                                                 c.shipment_id,  
                                                 e.itemcustpo itemcustpo,
                                                 e.itemcustpoline itemcustpoline,
                                                 c.delivery_no delivery_no,
                                                 c.line_item line_item,
                                                 c.mpn,c.ictpn,  
                                                 c.assign_qty,  
                                                case when a.shipment_type = 'DS' and a.region = 'EMEIA' and a.poe = 'SA' then
                                                           e.PORTOFENTRY
                                                           else  a.poe end poe,'' gateway,'' gccn , 
                                                 decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) shipmenttype
                                      from NONEDIPPS.t_shipment_info a
                                   join NONEDIPPS.t_shipment_pallet b
                                     on a.shipment_id = b.shipment_id
                                   join NONEDIPPS.t_pallet_order c
                                     on b.pallet_no = c.pallet_no
                                   join(select *
                                           from(select pallet_no, weight, cdt
                                                   from NONEDIPPS.t_pallet_weight_log
                                                  where pallet_no = '{0}'
                                                    AND PASS = '1'
                                                  order by cdt desc)
                                          where rownum = 1) d
                                     on c.pallet_no = d.pallet_no
                                   left join (select  decode(itemcustpo, '', weborderno, null, weborderno, itemcustpo) itemcustpo, itemcustpoline, PORTOFENTRY, deliveryno, custdelitem, SHIPPLANT  from NONEDIPPS.t_940_unicode) e
                                              on c.delivery_no = e.deliveryno
                                    and c.line_item = e.custdelitem
                                   where b.pallet_no = '{1}'
                                   order by c.delivery_no asc, c.line_item asc 
                                   ", inpalletno, inpalletno);
                #endregion
            }


            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
    }
}
