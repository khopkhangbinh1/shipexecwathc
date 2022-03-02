using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace Weight
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
            string sql = "SELECT shipment_id FROM ppsuser.g_ds_pallet_t WHERE END_PALLETNO =:palletNo";
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
            string sql = "SELECT sum(qty) FROM ppsuser.g_ds_pallet_t WHERE shipment_id =:shipmentId";
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
            string sql = "SELECT nvl(SUM(qty),0) FROM ppsuser.g_ds_scandata_t WHERE shipment_id = :shipmentId AND check_flag = 'Y'";
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

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_INSERTWORKLOG", procParams);
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

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_CHECKSHIPMENTHOLD", procParams);
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

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WEIGHT_CHECKPALLETSAWB2", procParams);
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
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WEIGHT_CHECKPALLETSAWB", procParams);
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

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WEIGHT_CHECKSHIPMENTSTATUS2", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            strregion = ds.Tables[0].Rows[0]["outregion"].ToString();
            return errmsg;


        }

        public DataSet ShowShipmentPalletInfoDataTable(string inputSno)
        {
            string sql = string.Empty;
            #region 20200329bk
            //sql = string.Format(@"
            //                    select a.shipment_id  as 集货单号,
            //                                    a.pallet_no    as 栈板号,
            //                                    a.carton_qty   as 箱数,
            //                                    a.weight       as 重量,
            //                                    a.pick_carton  as PICK箱数,
            //                                    a.pack_carton  as PACK箱数,
            //                                    a.check_result as CHECK结果,
            //                                    a.SECURITY SECURITY
            //                               from ppsuser.t_shipment_pallet a
            //                              where a.shipment_id in
            //                                    (select shipment_id
            //                                       from ppsuser.t_shipment_pallet
            //                                      where pallet_no = '{0}')
            //                        ", inputSno);
            #endregion
            #region 20200329new EMEIA DHL WPX
            sql = string.Format(@"
            select a.shipment_id as 集货单号,
                   a.pallet_no as 栈板号,
                   a.carton_qty as 箱数,
                   a.weight as 重量,
                   a.pick_carton as PICK箱数,
                   a.pack_carton as PACK箱数,
                   a.check_result as CHECK结果,
                   a.SECURITY SECURITY,
                   case
                     when c.region = 'EMEIA' and
                          (c.carrier_code like '%DHL%' or
                          c.carrier_name like '%DHL%') and
                          c.service_level = 'WPX' then
                      d.remark || 'WPX'
                     else
                      d.remark
                   end as 栈板规格
              from ppsuser.t_shipment_pallet a
             inner
              join ppsuser.t_shipment_info c

           on a.shipment_id = c.shipment_id

         left
              join (select e.packcode, min(e.remark) remark
                           from(select distinct packcode,
                                                 palletlengthcm || '*' ||
                                                 palletwidthcm as remark
                                   from ppsuser.vw_mpn_info) e
                          group by packcode) d
                on a.pack_code = d.packcode
             where a.shipment_id in
                   (select shipment_id
                      from ppsuser.t_shipment_pallet
                     where pallet_no = '{0}')", inputSno);
            #endregion

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


        public DataSet GetPalletLabelDataTableDAL(string inpalletno,string  isSAWB,string  inregion)
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
                    #region SAWB 20201007bk
                    //sql= string.Format(@"select DISTINCT bb.pallet_no,
                    //                              bb.real_pallet_no,
                    //                              case  
                    //                                  when aa.shipment_type = 'DS' then
                    //                                    aa.carrier_code 
                    //                                else  
                    //                            (SELECT distinct SCACCODE
                    //                         FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D
                    //                        where trim(D.carriercode) = aa.carrier_code
                    //                          and D.ShipMode = aa.transport
                    //                          and D.isdisabled = '0'
                    //                          and D.type = 'HAWB') end carrier_code,
                    //                              aa.hawb,aa.poe gateway,
                    //                              bb.weight,  
                    //                              bb.empty_carton + bb.carton_qty fullcartonqty,  
                    //                              bb.empty_carton,  
                    //                              bb.qty totalqty,
                    //                              to_char(aa.shipping_time, 'dd/mm/yyyy') cdt,  
                    //                              ppsuser.t_pallet_bottomdesc(bb.pallet_no) mix_desc,  
                    //                              aa.shipment_id,  
                    //                              '' itemcustpo,  
                    //                              '' itemcustpoline,  
                    //                              '' delivery_no,  
                    //                              '' line_item,  
                    //                              decode(aa.shipment_type, 'FD', 'HUB', aa.shipment_type) shipmenttype,  
                    //                              cc.mpn,cc.ictpn,  
                    //                              cc.assign_qty,  
                    //                              cc.gccn,  
                    //                              cc.poe
                    //                from ppsuser.t_shipment_info aa
                    //                join ppsuser.t_shipment_pallet bb
                    //                  on aa.shipment_id = bb.shipment_id
                    //                join(select a.pallet_no,
                    //                             b.hawb as gccn,
                    //                             b.mpn,
                    //                             b.poe, b.ictpn,
                    //                             sum(a.assign_qty) as assign_qty
                    //                        from ppsuser.t_pallet_order a
                    //                        join ppsuser.t_order_info b
                    //                     on a.delivery_no = b.delivery_no
                    //                    and a.line_item = b.line_item
                    //                         and a.ictpn = b.ictpn
                    //                        join ppsuser.t_shipment_sawb c
                    //                          on b.shipment_id = c.shipment_id
                    //                         and a.shipment_id = c.sawb_shipment_id
                    //                       where a.pallet_no = '{0}'
                    //                       group by a.pallet_no, b.hawb, b.mpn, b.poe, b.ictpn) cc
                    //                  on bb.pallet_no = cc.pallet_no
                    //                join(select *
                    //                        from(select pallet_no, weight, cdt
                    //                                from ppsuser.t_pallet_weight_log
                    //                              where pallet_no = '{1}'
                    //                                 AND PASS = '1'
                    //                               order by cdt desc)
                    //                       where rownum = 1) dd
                    //                  on bb.pallet_no = dd.pallet_no
                                   
                    //                 order by cc.ictpn asc,
                    //                  cc.gccn asc,
                    //                  cc.poe asc
                    //            ", inpalletno, inpalletno);
                    #endregion
                    #region SAWB 20201007new
                    sql = string.Format(@"select DISTINCT bb.pallet_no,
                                                  bb.real_pallet_no,
                                                  case  
                                                      when aa.shipment_type = 'DS' then
                                                        aa.carrier_code 
                                                    else  
                                                (SELECT distinct SCACCODE
                                             FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D
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
                                                  ppsuser.t_pallet_bottomdesc(bb.pallet_no) mix_desc,  
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
                                    from ppsuser.t_shipment_info aa
                                    join ppsuser.t_shipment_pallet bb
                                      on aa.shipment_id = bb.shipment_id
                                    join(select a.pallet_no,
                                                 b.hawb as gccn,
                                                 b.mpn,
                                                 b.poe, '' ictpn,
                                                 sum(a.assign_qty) as assign_qty
                                            from ppsuser.t_pallet_order a
                                            join ppsuser.t_order_info b
                                         on a.delivery_no = b.delivery_no
                                        and a.line_item = b.line_item
                                             and a.ictpn = b.ictpn
                                            join ppsuser.t_shipment_sawb c
                                              on b.shipment_id = c.shipment_id
                                             and a.shipment_id = c.sawb_shipment_id
                                           where a.pallet_no = '{0}'
                                           group by a.pallet_no, b.hawb, b.mpn, b.poe) cc
                                      on bb.pallet_no = cc.pallet_no
                                    join(select *
                                            from(select pallet_no, weight, cdt
                                                    from ppsuser.t_pallet_weight_log
                                                  where pallet_no = '{1}'
                                                     AND PASS = '1'
                                                   order by cdt desc)
                                           where rownum = 1) dd
                                      on bb.pallet_no = dd.pallet_no
                                   
                                     order by cc.ictpn asc,
                                      cc.gccn asc,
                                      cc.poe asc
                                ", inpalletno, inpalletno);
                    #endregion
                }
                else
                {
                    #region NOSAWB 简约版 20200317BK
                    //sql = string.Format(@"select pallet_no,real_pallet_no, carrier_code,hawb,weight,
                    //                            fullcartonqty, empty_carton,totalqty,cdt,mix_desc,shipment_id,
                    //                            itemcustpo,itemcustpoline,delivery_no, line_item,
                    //                            mpn,ictpn,poe,gateway,gccn,shipmenttype,
                    //                            sum(assign_qty) assign_qty
                    //                       from (select  b.pallet_no,
                    //                                             b.real_pallet_no,
                    //                                             case
                    //                                               when a.shipment_type = 'DS' then
                    //                                                a.carrier_code
                    //                                               else
                    //                                                (SELECT distinct SCACCODE
                    //                                                   FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D
                    //                                                  where trim(D.carriercode) = a.carrier_code
                    //                                                    and D.ShipMode = a.transport
                    //                                                    and D.isdisabled = '0'
                    //                                                    and D.type = 'HAWB')
                    //                                             end carrier_code,
                    //                                             a.hawb,
                    //                                             b.weight,
                    //                                             b.empty_carton + b.carton_qty fullcartonqty,
                    //                                             b.empty_carton,
                    //                                             b.qty totalqty,
                    //                                             to_char(a.shipping_time, 'dd/mm/yyyy') cdt,
                    //                                             case
                    //                                               when a.region = 'EMEIA' and
                    //                                                    UPPER(e.SHIPPLANT) LIKE 'MIT%' then
                    //                                                'MIT'
                    //                                               when pallet_type = '001' then
                    //                                                'DO NOT BREAK BULK'
                    //                                               when pallet_type = '999' then
                    //                                                'CONSOLIDATED'
                    //                                             end mix_desc,
                    //                                             c.shipment_id,
                    //                                             decode(a.shipment_type, 'DS', '', e.itemcustpo) itemcustpo,
                    //                                             decode(a.shipment_type, 'DS', '', e.itemcustpoline) itemcustpoline,
                    //                                             decode(a.shipment_type, 'DS', '', c.delivery_no) delivery_no,
                    //                                             decode(a.shipment_type, 'DS', '', c.line_item) line_item,
                    //                                             c.mpn,
                    //                                             c.ictpn,
                    //                                             c.assign_qty,
                    //                                             case
                    //                                               when a.shipment_type = 'DS' and a.region = 'EMEIA' and
                    //                                                    a.poe = 'SA' then
                    //                                                e.PORTOFENTRY
                    //                                               else
                    //                                                a.poe
                    //                                             end poe,
                    //                                             '' gateway,
                    //                                             '' gccn,
                    //                                             decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) shipmenttype
                    //                               from ppsuser.t_shipment_info a
                    //                               join ppsuser.t_shipment_pallet b
                    //                                 on a.shipment_id = b.shipment_id
                    //                               join ppsuser.t_pallet_order c
                    //                                 on b.pallet_no = c.pallet_no
                    //                               join (select *
                    //                                      from (select pallet_no, weight, cdt
                    //                                              from ppsuser.t_pallet_weight_log
                    //                                             where pallet_no = '{0}'
                    //                                               AND PASS = '1'
                    //                                             order by cdt desc)
                    //                                     where rownum = 1) d
                    //                                 on c.pallet_no = d.pallet_no
                    //                               left join (select decode(itemcustpo,
                    //                                                       '',
                    //                                                       weborderno,
                    //                                                       null,
                    //                                                       weborderno,
                    //                                                       itemcustpo) itemcustpo,
                    //                                                itemcustpoline,
                    //                                                PORTOFENTRY,
                    //                                                deliveryno,
                    //                                                custdelitem,
                    //                                                SHIPPLANT
                    //                                           from ppsuser.t_940_unicode) e
                    //                                 on c.delivery_no = e.deliveryno
                    //                                and c.line_item = e.custdelitem
                    //                              where b.pallet_no = '{1}') aa
                    //                      group by pallet_no,real_pallet_no, carrier_code,hawb,weight,
                    //                            fullcartonqty, empty_carton,totalqty,cdt,mix_desc,shipment_id,
                    //                            itemcustpo,itemcustpoline,delivery_no, line_item,
                    //                            mpn,ictpn,poe,gateway,gccn,shipmenttype 
                    //                      order by ictpn asc
                                                                
                    //                      ", inpalletno, inpalletno);
                    #endregion
                    #region NOSAWB 简约版 20200339bk ICTPN=''
                    //sql = string.Format(@"select pallet_no,real_pallet_no, carrier_code,hawb,weight,
                    //                            fullcartonqty, empty_carton,totalqty,cdt,mix_desc,shipment_id,
                    //                            itemcustpo,itemcustpoline,delivery_no, line_item,
                    //                            mpn,'' ictpn,poe,gateway,gccn,shipmenttype,
                    //                            sum(assign_qty) assign_qty
                    //                       from (select  b.pallet_no,
                    //                                             b.real_pallet_no,
                    //                                             case
                    //                                               when a.shipment_type = 'DS' then
                    //                                                a.carrier_code
                    //                                               else
                    //                                                (SELECT distinct SCACCODE
                    //                                                   FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D
                    //                                                  where trim(D.carriercode) = a.carrier_code
                    //                                                    and D.ShipMode = a.transport
                    //                                                    and D.isdisabled = '0'
                    //                                                    and D.type = 'HAWB')
                    //                                             end carrier_code,
                    //                                             a.hawb,
                    //                                             b.weight,
                    //                                             b.empty_carton + b.carton_qty fullcartonqty,
                    //                                             b.empty_carton,
                    //                                             b.qty totalqty,
                    //                                             to_char(a.shipping_time, 'dd/mm/yyyy') cdt,
                    //                                             case
                    //                                               when a.region = 'EMEIA' and
                    //                                                    UPPER(e.SHIPPLANT) LIKE 'MIT%' then
                    //                                                'MIT'
                    //                                               when pallet_type = '001' then
                    //                                                'DO NOT BREAK BULK'
                    //                                               when pallet_type = '999' then
                    //                                                'CONSOLIDATED'
                    //                                             end mix_desc,
                    //                                             c.shipment_id,
                    //                                             decode(a.shipment_type, 'DS', '', e.itemcustpo) itemcustpo,
                    //                                             decode(a.shipment_type, 'DS', '', e.itemcustpoline) itemcustpoline,
                    //                                             decode(a.shipment_type, 'DS', '', c.delivery_no) delivery_no,
                    //                                             decode(a.shipment_type, 'DS', '', c.line_item) line_item,
                    //                                             c.mpn,
                    //                                             c.ictpn,
                    //                                             c.assign_qty,
                    //                                             case
                    //                                               when a.shipment_type = 'DS' and a.region = 'EMEIA' and
                    //                                                    a.poe = 'SA' then
                    //                                                e.PORTOFENTRY
                    //                                               else
                    //                                                a.poe
                    //                                             end poe,
                    //                                             '' gateway,
                    //                                             '' gccn,
                    //                                             decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) shipmenttype
                    //                               from ppsuser.t_shipment_info a
                    //                               join ppsuser.t_shipment_pallet b
                    //                                 on a.shipment_id = b.shipment_id
                    //                               join ppsuser.t_pallet_order c
                    //                                 on b.pallet_no = c.pallet_no
                    //                               join (select *
                    //                                      from (select pallet_no, weight, cdt
                    //                                              from ppsuser.t_pallet_weight_log
                    //                                             where pallet_no = '{0}'
                    //                                               AND PASS = '1'
                    //                                             order by cdt desc)
                    //                                     where rownum = 1) d
                    //                                 on c.pallet_no = d.pallet_no
                    //                               left join (select decode(itemcustpo,
                    //                                                       '',
                    //                                                       weborderno,
                    //                                                       null,
                    //                                                       weborderno,
                    //                                                       itemcustpo) itemcustpo,
                    //                                                itemcustpoline,
                    //                                                PORTOFENTRY,
                    //                                                deliveryno,
                    //                                                custdelitem,
                    //                                                SHIPPLANT
                    //                                           from ppsuser.t_940_unicode) e
                    //                                 on c.delivery_no = e.deliveryno
                    //                                and c.line_item = e.custdelitem
                    //                              where b.pallet_no = '{1}') aa
                    //                      group by pallet_no,real_pallet_no, carrier_code,hawb,weight,
                    //                            fullcartonqty, empty_carton,totalqty,cdt,mix_desc,shipment_id,
                    //                            itemcustpo,itemcustpoline,delivery_no, line_item,
                    //                            mpn,poe,gateway,gccn,shipmenttype 
                    //                      order by ictpn asc
                                                                
                    //                      ", inpalletno, inpalletno);
                    #endregion
                    #region NOSAWB 简约版20200329new EMEIA DHL WPX
                    sql = string.Format(@"
                                     select pallet_no,
                                           real_pallet_no,
                                           carrier_code,
                                           hawb,
                                           weight,
                                           fullcartonqty,
                                           empty_carton,
                                           totalqty,
                                           cdt,
                                           mix_desc,
                                           shipment_id,
                                           itemcustpo,
                                           itemcustpoline,
                                           delivery_no,
                                           line_item,
                                           mpn,
                                           '' ictpn,
                                           poe,
                                           gateway,
                                           gccn,
                                           shipmenttype,
                                           sum(assign_qty) assign_qty
                                      from (select b.pallet_no,
                                                   b.real_pallet_no,
                                                   case
                                                     when a.shipment_type = 'DS' then
                                                      a.carrier_code
                                                     else
                                                      (select distinct scaccode
                                                         from pptest.oms_carrier_tracking_prefix d
                                                        where trim(d.carriercode) = a.carrier_code
                                                          and d.shipmode = a.transport
                                                          and d.isdisabled = '0'
                                                          and d.type = 'HAWB')
                                                   end carrier_code,
                                                   a.hawb,
                                                   b.weight,
                                                   case
                                                     when a.region = 'EMEIA' and (a.carrier_code like '%DHL%' or
                                                          a.carrier_name like '%DHL%') and
                                                          a.service_level = 'WPX' then
                                                      b.carton_qty
                                                     else
                                                      b.empty_carton + b.carton_qty
                                                   end fullcartonqty,
                                                   case
                                                     when a.region = 'EMEIA' and (a.carrier_code like '%DHL%' or
                                                          a.carrier_name like '%DHL%') and
                                                          a.service_level = 'WPX' then
                                                      '0'
                                                     else
                                                      to_char(b.empty_carton)
                                                   end empty_carton,
                                                   b.qty totalqty,
                                                   to_char(a.shipping_time, 'dd/mm/yyyy') cdt,
                                                   case
                                                     when a.region = 'EMEIA' and upper(e.shipplant) like 'MIT%' then
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
                                                      e.portofentry
                                                     else
                                                      a.poe
                                                   end poe,
                                                   '' gateway,
                                                   '' gccn,
                                                   decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) shipmenttype
                                              from ppsuser.t_shipment_info a
                                              join ppsuser.t_shipment_pallet b
                                                on a.shipment_id = b.shipment_id
                                              join ppsuser.t_pallet_order c
                                                on b.pallet_no = c.pallet_no
                                              join (select *
                                                     from (select pallet_no, weight, cdt
                                                             from ppsuser.t_pallet_weight_log
                                                            where pallet_no = '{0}'
                                                              and pass = '1'
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
                                                               portofentry,
                                                               deliveryno,
                                                               custdelitem,
                                                               shipplant
                                                          from ppsuser.t_940_unicode) e
                                                on c.delivery_no = e.deliveryno
                                               and c.line_item = e.custdelitem
                                             where b.pallet_no = '{1}') aa
                                     group by pallet_no,
                                              real_pallet_no,
                                              carrier_code,
                                              hawb,
                                              weight,
                                              fullcartonqty,
                                              empty_carton,
                                              totalqty,
                                              cdt,
                                              mix_desc,
                                              shipment_id,
                                              itemcustpo,
                                              itemcustpoline,
                                              delivery_no,
                                              line_item,
                                              mpn,
                                              poe,
                                              gateway,
                                              gccn,
                                              shipmenttype
                                     order by ictpn asc
                          
                                          ", inpalletno, inpalletno);
                    #endregion
                }
            }
            else
            {
                #region PAC 复杂版
                sql = string.Format(@"
                                select distinct b.pallet_no,
                                                b.real_pallet_no,
                                                case
                                                  when a.shipment_type = 'DS' then
                                                   a.carrier_code
                                                  else
                                                   (select distinct scaccode
                                                      from pptest.oms_carrier_tracking_prefix d
                                                     where trim(d.carriercode) = a.carrier_code
                                                       and d.shipmode = a.transport
                                                       and d.isdisabled = '0'
                                                       and d.type = 'HAWB')
                                                end carrier_code,
                                                a.hawb,
                                                b.weight,
                                                b.empty_carton + b.carton_qty fullcartonqty,
                                                b.empty_carton,
                                                b.qty totalqty,
                                                to_char(a.shipping_time, 'dd/mm/yyyy') cdt,
                                                ppsuser.t_pallet_bottomdesc(b.pallet_no) mix_desc,
                                                c.shipment_id,
                                                e.itemcustpo itemcustpo,
                                                e.itemcustpoline itemcustpoline,
                                                c.delivery_no delivery_no,
                                                c.line_item line_item,
                                                c.mpn,
                                                c.ictpn,
                                                c.assign_qty,
                                                case
                                                  when a.shipment_type = 'DS' and a.region = 'EMEIA' and
                                                       a.poe = 'SA' then
                                                   e.portofentry
                                                  else
                                                   a.poe
                                                end poe,
                                                '' gateway,
                                                '' gccn,
                                                decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) shipmenttype
                                  from ppsuser.t_shipment_info a
                                  join ppsuser.t_shipment_pallet b
                                    on a.shipment_id = b.shipment_id
                                  join ppsuser.t_pallet_order c
                                    on b.pallet_no = c.pallet_no
                                  join (select *
                                          from (select pallet_no, weight, cdt
                                                  from ppsuser.t_pallet_weight_log
                                                 where pallet_no = '{0}'
                                                   and pass = '1'
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
                                                    portofentry,
                                                    deliveryno,
                                                    custdelitem,
                                                    shipplant
                                               from ppsuser.t_940_unicode) e
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


        public DataSet CheckExistSSCC18LabelBySQL(string strPalletNo)
        {
            string qtyStr = string.Empty;
            string sql = @"
                         select b.pallet_no, a.shiptype, b.gs1flag, b.pallet_type
                           from ppsuser.t_shipment_info a
                           join ppsuser.t_shipment_pallet b
                             on a.shipment_id = b.shipment_id
                          where b.pallet_no = :insn
                            and a.type = 'BULK'
                            and ((b.pallet_type = '001' and
                                a.shipment_id not in
                                (select f.shipment_id
                                     from ppsuser.t_shipment_info f
                                    where f.region = 'PAC'
                                      and f.shipment_type = 'DS'
                                      and nvl(f.shiptype, ' ') = 'STO')) or b.gs1flag = 'Y')";
            object[][] paramArry = new object[1][];
            paramArry[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strPalletNo };
            DataSet dataSet = ClientUtils.ExecuteSQL(sql, paramArry);
            if (dataSet != null)
            {
                return dataSet;
            }
            else
            {
                return null;
            }
        }

        public DataSet CheckExistSSCC18LabelBySQL(string strPalletNo, string strSSCC)
        {
            string qtyStr = string.Empty;
            string sql = @"
                         select b.pallet_no, a.shiptype, b.gs1flag, b.pallet_type
                           from ppsuser.t_shipment_info a
                           join ppsuser.t_shipment_pallet b
                             on a.shipment_id = b.shipment_id
                          where b.pallet_no = :insn 
                            and b.sscc = :insscc
                            and a.type = 'BULK'
                            and ((b.pallet_type = '001' and
                                a.shipment_id not in
                                (select f.shipment_id
                                     from ppsuser.t_shipment_info f
                                    where f.region = 'PAC'
                                      and f.shipment_type = 'DS'
                                      and nvl(f.shiptype, ' ') = 'STO')) or b.gs1flag = 'Y')";
            object[][] paramArry = new object[2][];
            paramArry[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strPalletNo };
            paramArry[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insscc", strSSCC };
            DataSet dataSet = ClientUtils.ExecuteSQL(sql, paramArry);
            if (dataSet != null)
            {
                return dataSet;
            }
            else
            {
                return null;
            }
        }
        public string PPSGetbasicparameterBySP(string strParaType, out string strParaValue, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inparatype", strParaType };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outparavalue", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.sp_pps_getbasicparameter", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                strParaValue = "";
                RetMsg = e1.ToString();
                return "NG";
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            strParaValue = dt.Rows[0]["outparavalue"].ToString();
            if (RetMsg.Equals("OK"))
            {

                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public string  PPSCheckReprintRoleBySP(string inempno, string inwc, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", inempno };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwc", inwc };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_CHECKREPRINTROLE", procParams).Tables[0]; 
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return "NG";
            }
            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {

                return "OK";
            }
            else
            {
                return "NG";
            }
        }

       
        public string PPInsertRePrintLogBySP(string strstation, string strlabelname, string strsn, string strppsempno, string strppsempname, string strreprintempno, string strreprintempname, string strreprintlogintime, string strcomputername, string strmac, string strremark, out string RetMsg)
        {
            object[][] procParams = new object[12][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "instation", strstation };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlabelname", strlabelname };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strsn };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inppsempno", strppsempno };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inppsempname", strppsempname };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inreprintempno", strreprintempno };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inreprintempname", strreprintempname };
            procParams[7] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inreprintlogintime", strreprintlogintime };
            procParams[8] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incomputername", strcomputername };
            procParams[9] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inmac", strmac };
            procParams[10] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inremark", strremark };
            procParams[11] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("PPSUSER.sp_pps_inreparintlog", procParams).Tables[0];
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return "NG";
            }
            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {

                return "OK";
            }
            else
            {
                return "NG";
            }
        }

        //ppsuser.sp_pack_checkreprintcarton(inempno  in varchar2,
        //                                                       inwc     in varchar2,
        //                                                       incarton in varchar2,
        //                                                       errmsg   out varchar2) as

        //public string  CheckReprintCartonBySP(string strEmpNo, string strWC, string strCartonNo, out string RetMsg)
        //{
        //    object[][] procParams = new object[4][];
        //    procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", strEmpNo };
        //    procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwc", strWC };
        //    procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarton", strCartonNo };
        //    procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };


        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = ClientUtils.ExecuteProc("PPSUSER.sp_pack_checkreprintcarton", procParams).Tables[0];
        //    }
        //    catch (Exception e)
        //    {
        //        RetMsg = e.ToString();
        //        return "NG";
        //    }
        //    RetMsg = dt.Rows[0]["errmsg"].ToString();
        //    if (RetMsg.Equals("OK"))
        //    {

        //        return "OK";
        //    }
        //    else
        //    {
        //        return "NG";
        //    }
        //}
    }
}
