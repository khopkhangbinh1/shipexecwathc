using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using NPOI.SS.Util;
using PickList.Entitys;

namespace PickList
{
    class PickListDal
    {
        public DataSet GetStockInfoDataTable(string ictpartno)
        {
            
            string sql = string.Empty;
            sql = string.Format("Select b.part_no 料号, b.location_no 库位, b.CARTONQTY - b.QHCARTONQTY 箱数 "
                                + "      from ppsuser.t_location b "
                                + "     where b.qty > 0 "
                                + "       and b. cartonqty > 0 "
                                + "       and(b.part_no = '{0}' or "
                                + "           b.part_no in (select distinct a.part_no "
                                + "                          from ppsuser.t_sn_status a "
                                + "                         where a.customer_sn = '{1}' "
                                + "                            or a.carton_no = '{2}' "
                                + "                            or a.pallet_no = '{3}')) "
                                + "     order by b.Udt asc", ictpartno, ictpartno, ictpartno, ictpartno);
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
        public DataSet GetStockCarInfoDataTable(string ictpartno,string palletno)
        {
            //是否为Person
            Int32 intPersonDNCount =0 ;
            string strsql = string.Empty;
            strsql  =string.Format("select count (delivery_no) as PPARTCOUNT "
                                   + "   from ppsuser.t_order_info "
                                   + "  where (delivery_no, line_item, ictpn) in "
                                   + "        (select delivery_no, line_item, ictpn "
                                   + "           from ppsuser.t_pallet_order "
                                   + "          where pallet_no = '{0}' "
                                   + "            and (ictpn = '{1}' or  "
                                    + "           ictpn in (select distinct a.part_no "
                                    + "                          from ppsuser.t_sn_status a "
                                    + "                         where a.customer_sn = '{2}' "
                                    + "                            or a.carton_no = '{3}' "
                                    + "                            or a.pallet_no = '{4}'))) "
                                   + "            and person_flag = 'Y'", palletno, ictpartno, ictpartno, ictpartno, ictpartno);
            
            try
            {
                DataTable db = ClientUtils.ExecuteSQL(strsql).Tables[0];
                if (db.Rows.Count > 0)
                {
                    intPersonDNCount = Convert.ToInt32(db.Rows[0]["PPARTCOUNT"].ToString());
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

            string sql = string.Empty;
            if (intPersonDNCount == 0)
            {
                //sql = string.Format("Select b.part_no 料号, b.location_no 库位,'' 车行号, b.CARTONQTY - b.QHCARTONQTY 箱数 "
                //                    + "      from ppsuser.t_location b "
                //                    + "     where b.qty > 0 "
                //                    + "       and b. cartonqty > 0 "
                //                    + "       and(b.part_no = '{0}' or "
                //                    + "           b.part_no in (select distinct a.part_no "
                //                    + "                          from ppsuser.t_sn_status a "
                //                    + "                         where a.customer_sn = '{1}' "
                //                    + "                            or a.carton_no = '{2}' "
                //                    + "                            or a.pallet_no = '{3}')) "
                //                    + "     order by b.Udt asc", ictpartno, ictpartno, ictpartno, ictpartno);
                sql = string.Format(@"Select t.part_no 料号, t.location_no 库位,'' 车行号, qty 箱数,TO_CHAR(udt,'YYYY/MM/DD') 入库时间 
                                      from(Select distinct b.part_no,
                                                            b.location_no,
                                                            (b.CARTONQTY - b.QHCARTONQTY) qty,
                                                            min(a.udt) as udt
                                              from ppsuser.t_location b, ppsuser.t_sn_status a
                                             where a.wc = 'W0'
                                               and A.LOCATION_ID(+) = B.LOCATION_ID
                                               AND b.qty > 0
                                               and b.cartonqty > 0
                                               and(b.part_no = '{0}' or
                                                   b.part_no in
                                                   (select distinct a.part_no
                                                       from ppsuser.t_sn_status a
                                                      where a.customer_sn = '{1}'
                                                         or a.carton_no = '{2}'
                                                         or a.pallet_no = '{3}')) and
                                                         a.part_no = b.part_no
                                             group by b.part_no,
                                                      b.location_no,b.CARTONQTY ,b.QHCARTONQTY
                                                     ) t
                                     where t.qty > 0
                                     order by t.Udt ", ictpartno, ictpartno, ictpartno, ictpartno);
            }
            else
            {
                #region
                //sql = string.Format("  select c.ict_partno 料号,e.location_no 库位, b.trolley_line_no 车行号, count(distinct custom_sn) 箱数"
                //                    + "     from ppsuser.t_trolley_sn_status a "
                //                    + "      join ppsuser.t_trolley_line_info b "
                //                    + "        on a.trolley_no = b.trolley_no "
                //                    + "       and a.sides_no = b.sides_no "
                //                    + "       and a.level_no = b.level_no "
                //                    + "       and a.seq_no = b.seq_no "
                //                    + "      join SAJET.SYS_PARKING_SEATING_GROUP c"
                //                    + "        on a.group_code = c.group_code "
                //                    + "     left join ppsuser.t_location_trolley d"
                //                    + "        on a.trolley_no = d.trolley_no "
                //                    + "      join (select location_id ,max(location_no) as location_no from ppsuser.t_location group by location_id ) e"
                //                    + "        on d.location_id = e.location_id "
                //                    + "    where (a.delivery_no, a.line_item, c.ict_partno) in "
                //                    + "          (select delivery_no, line_item, ictpn "
                //                    + "             from ppsuser.t_pallet_order d "
                //                    + "           where pallet_no = '{0}' "
                //                    + "              and ( ictpn = '{1}' or ictpn in (select distinct a.part_no "
                //                    + "                          from ppsuser.t_sn_status a "
                //                    + "                         where a.customer_sn = '{2}' "
                //                    + "                            or a.carton_no = '{3}' "
                //                    + "                            or a.pallet_no = '{4}'))) "
                //                    + "      and a.trolley_no <> 'ICT-00-00-000' "
                //                    + "    group by c.ict_partno ,e.location_no, b.trolley_line_no", palletno, ictpartno, ictpartno, ictpartno, ictpartno)
                //                    ;
                //sql = string.Format(@"   select aa.ictpartno 料号,
                //                                aa.location_no 库位,
                //                                aa.trolley_line_no || '(' || aa.pointno || ')' 车行号,
                //                                aa.csnqty 箱数
                //                           from (select a.ictpartno,
                //                                        e.location_no,
                //                                        b.trolley_line_no,
                //                                        count(distinct custom_sn) csnqty,
                //                                        listagg(decode(a.pointno, 0, null, a.pointno), ',') within group(order by a.pointno) as pointno
                //                                   from ppsuser.t_trolley_sn_status a
                //                                   join ppsuser.t_trolley_line_info b
                //                                     on a.trolley_no = b.trolley_no
                //                                    and a.sides_no = b.sides_no
                //                                    and a.level_no = b.level_no
                //                                    and a.seq_no = b.seq_no
                //                                   left join ppsuser.t_location_trolley d
                //                                     on a.trolley_no = d.trolley_no
                //                                   join (select location_id, max(location_no) as location_no
                //                                          from ppsuser.t_location
                //                                         group by location_id) e
                //                                     on d.location_id = e.location_id
                //                                  where (a.delivery_no, a.line_item, a.ictpartno) in
                //                                        (select d.delivery_no, d.line_item, d.ictpn
                //                                           from ppsuser.t_pallet_order d
                //                                           left join (select ttss.delivery_no,
                //                                                            ttss.line_item,
                //                                                            ttss.ictpartno,
                //                                                            count(distinct ttss.custom_sn) as customcount
                //                                                       from ppsuser.t_sn_status tss
                //                                                      inner join ppsuser.t_trolley_sn_status ttss
                //                                                         on tss.customer_sn = ttss.custom_sn
                //                                                      where substr(tss.pick_pallet_no, 3) = '{0}'
                //                                                      group by ttss.delivery_no,
                //                                                               ttss.line_item,
                //                                                               ttss.ictpartno) x
                //                                             on d.delivery_no = x.delivery_no
                //                                            and d.line_item = x.line_item
                //                                            and d.ictpn = x.ictpartno
                //                                          where d.pallet_no = '{0}'
                //                                            and nvl(x.customcount, 0) < d.assign_qty
                //                                            and (d.ictpn = '{1}' or
                //                                                d.ictpn in (select distinct a.part_no
                //                                                               from ppsuser.t_sn_status a
                //                                                              where a.customer_sn = '{2}'
                //                                                                 or a.carton_no = '{3}'
                //                                                                 or a.pallet_no = '{4}')))
                //                                    and a.trolley_no <> 'ICT-00-00-000'
                //                                  group by a.ictpartno, e.location_no, b.trolley_line_no) aa",
                //                                 palletno, ictpartno, ictpartno, ictpartno, ictpartno)
                #endregion
                sql = string.Format(@"  select to_char(aa.ictpartno) 料号,
                                             to_char(aa.location_no) 库位,
                                             to_char(aa.trolley_line_no || '(' || aa.pointno || ')') 车行号,
                                             aa.csnqty 箱数
                                        from (select a.ictpartno,
                                                     e.location_no,
                                                     b.trolley_line_no,
                                                     count(distinct a.custom_sn) csnqty,
                                                     LISTAGG(decode(a.pointno, 0, null, a.pointno), ',') WITHIN GROUP(ORDER BY a.pointno) as pointno
                                                from ppsuser.t_trolley_sn_status a
                                                join ppsuser.t_trolley_line_info b
                                                  on a.trolley_no = b.trolley_no
                                                 and a.sides_no = b.sides_no
                                                 and a.level_no = b.level_no
                                                 and a.seq_no = b.seq_no
                                                left join ppsuser.t_location_trolley d
                                                  on a.trolley_no = d.trolley_no
                                                join (select location_id, max(location_no) as location_no
                                                       from ppsuser.t_location
                                                      group by location_id) e
                                                  on d.location_id = e.location_id
                                               where (a.delivery_no, a.line_item, a.ictpartno) in
                                                     (select delivery_no, line_item, ictpn
                                                        from ppsuser.t_pallet_order d
                                                       where pallet_no = '{0}'
                                                         and (ictpn = '{1}' or
                                                             ictpn in (select distinct a.part_no
                                                                          from ppsuser.t_sn_status a
                                                                         where a.customer_sn = '{2}'
                                                                            or a.carton_no = '{3}'
                                                                            or a.pallet_no = '{4}')))
                                                 and a.trolley_no <> 'ICT-00-00-000'
                                                 and a.carton_no not in(
                                                   select carton_no from ppsuser.t_sn_ppart 
                                                   where pack_pallet_no <>'{5}')
                                               group by a.ictpartno, e.location_no, b.trolley_line_no) aa
                                            union
                                            select to_char(tss.part_no) 料号,
                                                   to_char(tss.location_no) 库位,
                                                   to_char(tss.pallet_no) 车行号,
                                                   count(distinct tss.carton_no) 箱数
                                              from ppsuser.t_sn_status tss
                                             where tss.wc = 'W0'
                                               and tss.customer_sn in
                                                   (select customer_sn
                                                      from ppsuser.g_sn_status a1
                                                     where a1.customer_sn not in
                                                           (select custom_sn from ppsuser.t_trolley_sn_status)
                                                       and (a1.delivery_no, a1.line_item, a1.part_no) in
                                                           (select delivery_no, line_item, ictpn
                                                              from ppsuser.t_pallet_order d
                                                             where pallet_no = '{0}'
                                                               and (ictpn = '{1}' or
                                                                   ictpn in (select distinct a.part_no
                                                                                from ppsuser.t_sn_status a
                                                                               where a.customer_sn = '{2}'
                                                                                  or a.carton_no = '{3}'
                                                                                  or a.pallet_no = '{4}'))))
                                             group by tss.part_no, tss.location_no, tss.pallet_no",
                                              palletno, ictpartno, ictpartno, ictpartno, ictpartno, palletno);
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


        public DataSet GetPalletStockInfoDataTable( string palletno)
        {
            #region 20200801 栈板取消料号
            //string sql = string.Format(@"select bb.part_no 料号,
            //                   bb.location_no 库位,
            //                   '' 车行号,
            //                   bb.cartonqty - bb.qhcartonqty 箱数
            //              from ppsuser.t_location bb
            //             where bb.qty > 0
            //               and bb.cartonqty > 0
            //               and bb.part_no in (select distinct a.ictpn
            //                                    from ppsuser.t_pallet_order a
            //                                    join ppsuser.t_order_info b
            //                                      on a.delivery_no = b.delivery_no
            //                                     and a.line_item = b.line_item
            //                                     and a.ictpn = b.ictpn
            //                                     and b.person_flag = 'N'
            //                                    join ppsuser.t_shipment_pallet_part c
            //                                      on a.pallet_no = c.pallet_no
            //                                     and a.ictpn = c.ictpn
            //                                     and c.qty > c.pick_qty
            //                                   where a.pallet_no = '{0}')
            //            union
            //            select to_char(aa.ictpartno) 料号,
            //                   to_char(aa.location_no) 库位,
            //                   to_char(aa.trolley_line_no || '(' || aa.pointno || ')') 车行号,
            //                   aa.csnqty 箱数
            //              from(select a.ictpartno,
            //                           e.location_no,
            //                           b.trolley_line_no,
            //                           count(distinct a.custom_sn) csnqty,
            //                           LISTAGG(decode(a.pointno, 0, null, a.pointno), ',') WITHIN GROUP(ORDER BY a.pointno) as pointno
            //                      from ppsuser.t_trolley_sn_status a
            //                      join ppsuser.t_trolley_line_info b
            //                        on a.trolley_no = b.trolley_no
            //                       and a.sides_no = b.sides_no
            //                       and a.level_no = b.level_no
            //                       and a.seq_no = b.seq_no
            //                      left join ppsuser.t_location_trolley d
            //                        on a.trolley_no = d.trolley_no
            //                      join(select location_id, max(location_no) as location_no
            //                             from ppsuser.t_location
            //                            group by location_id) e
            //                        on d.location_id = e.location_id
            //                     where(a.delivery_no, a.line_item, a.ictpartno) in
            //                           (select delivery_no, line_item, ictpn
            //                              from ppsuser.t_pallet_order d
            //                             where pallet_no = '{0}')
            //                       and a.trolley_no <> 'ICT-00-00-000'
            //                       and a.carton_no not in
            //                           (select carton_no
            //                              from ppsuser.t_sn_ppart
            //                             where pack_pallet_no <> '{0}')
            //                     group by a.ictpartno, e.location_no, b.trolley_line_no) aa
            //            union
            //            select to_char(tss.part_no) 料号,
            //                   to_char(tss.location_no) 库位,
            //                   to_char(tss.pallet_no) 车行号,
            //                   count(distinct tss.carton_no) 箱数
            //              from ppsuser.t_sn_status tss
            //             where tss.wc = 'W0'
            //               and tss.customer_sn in
            //                   (select customer_sn
            //                      from ppsuser.g_sn_status a1
            //                     where a1.customer_sn not in
            //                           (select custom_sn from ppsuser.t_trolley_sn_status)
            //                       and(a1.delivery_no, a1.line_item, a1.part_no) in
            //                           (select delivery_no, line_item, ictpn
            //                              from ppsuser.t_pallet_order d
            //                             where pallet_no = '{0}'))
            //             group by tss.part_no, tss.location_no, tss.pallet_no", palletno);
            #endregion
            //20200810new
            #region
            //string sql = string.Format(@"
            //                select bb.part_no 料号,
            //                       bb.location_no 库位,
            //                       '' 车行号,
            //                       to_char(bb.cartonqty - bb.qhcartonqty) 箱数
            //                  from ppsuser.t_location bb
            //                 where bb.qty > 0
            //                   and bb.cartonqty > 0
            //                   and bb.part_no in (select distinct a.ictpn
            //                                        from ppsuser.t_pallet_order a
            //                                        join ppsuser.t_order_info b
            //                                          on a.delivery_no = b.delivery_no
            //                                         and a.line_item = b.line_item
            //                                         and a.ictpn = b.ictpn
            //                                         and b.person_flag = 'N'
            //                                        join ppsuser.t_shipment_pallet_part c
            //                                          on a.pallet_no = c.pallet_no
            //                                         and a.ictpn = c.ictpn
            //                                         and c.qty > c.pick_qty
            //                                       where a.pallet_no = '{0}')
            //                union
            //                select '' 料号,
            //                       to_char(aa.location_no) 库位,
            //                        to_char(aa.trolley_line_no) 车行号,
            //                       listagg(aa.pointno, '*') within group(order by aa.pointno) as  箱数
            //                  from (select distinct a.shipment_id,
            //                                        b.shipping_time,
            //                                        a.region,
            //                                        decode(g.pack_pallet_no,
            //                                               null,
            //                                               c.pallet_no,
            //                                               g.pack_pallet_no) pallet_no,
            //                                        '' ictpn,
            //                                        '' mpn,
            //                                        d.location_no,
            //                                        e.sides_no,
            //                                        e.level_no,
            //                                        e.seq_no,
            //                                        e.pointno,
            //                                        f.trolley_line_no,
            //                                        decode(b.tdm,
            //                                               null,
            //                                               '',
            //                                               to_char(b.tdm, 'yyyy-mm-dd hh24:mi')) tdm
            //                          from ppsuser.t_order_info a
            //                         inner join ppsuser.t_shipment_info b
            //                            on a.shipment_id = b.shipment_id
            //                         inner join ppsuser.t_pallet_order c
            //                            on a.delivery_no = c.delivery_no
            //                           and a.line_item = c.line_item
            //                           and a.ictpn = c.ictpn
            //                           and a.shipment_id = c.shipment_id
            //                         inner join ppsuser.vw_person_log d
            //                            on a.delivery_no = d.delivery_no
            //                           and a.line_item = d.line_item
            //                           and a.ictpn = d.part_no
            //                         inner join ppsuser.t_trolley_sn_status e
            //                            on to_char(d.customer_sn) = to_char(e.custom_sn)
            //                          join ppsuser.t_trolley_line_info f
            //                            on e.trolley_no = f.trolley_no
            //                           and e.sides_no = f.sides_no
            //                           and e.level_no = f.level_no
            //                           and e.seq_no = f.seq_no
            //                          left join ppsuser.t_sn_ppart g
            //                            on e.carton_no = g.carton_no
            //                         where c.pallet_no = '{0}'
            //                           and f.trolley_no not in ('ICT-00-00-000')
            //                         order by d.location_no, f.trolley_line_no, e.pointno) aa
            //                 where aa.pallet_no = '{0}'
            //                 group by aa.shipment_id,
            //                          aa.shipping_time,
            //                          aa.region,
            //                          aa.pallet_no,
            //                          aa.location_no,
            //                          aa.trolley_line_no,
            //                          aa.tdm
            //                union
            //                select to_char(tss.part_no) 料号,
            //                       to_char(tss.location_no) 库位,
            //                       to_char(tss.pallet_no) 车行号,
            //                       to_char(count(distinct tss.carton_no)) 箱数
            //                  from ppsuser.t_sn_status tss
            //                 where tss.wc = 'W0'
            //                   and tss.customer_sn in
            //                       (select customer_sn
            //                          from ppsuser.g_sn_status a1
            //                         where a1.customer_sn not in
            //                               (select custom_sn from ppsuser.t_trolley_sn_status)
            //                           and (a1.delivery_no, a1.line_item, a1.part_no) in
            //                               (select delivery_no, line_item, ictpn
            //                                  from ppsuser.t_pallet_order d
            //                                 where pallet_no = '{0}'))
            //                 group by tss.part_no, tss.location_no, tss.pallet_no
            //                 order by 库位 ", palletno);
            #endregion

            //20200820  调整用
            string sql = string.Format(@"
                            select bb.part_no 料号,
                                   bb.location_no 库位,
                                   '' 车行号,
                                   to_char(bb.cartonqty - bb.qhcartonqty) 箱数
                              from ppsuser.t_location bb
                             where bb.qty > 0
                               and bb.cartonqty > 0
                               and bb.part_no in (select distinct a.ictpn
                                                    from ppsuser.t_pallet_order a
                                                    join ppsuser.t_order_info b
                                                      on a.delivery_no = b.delivery_no
                                                     and a.line_item = b.line_item
                                                     and a.ictpn = b.ictpn
                                                     and b.person_flag = 'N'
                                                    join ppsuser.t_shipment_pallet_part c
                                                      on a.pallet_no = c.pallet_no
                                                     and a.ictpn = c.ictpn
                                                     and c.qty > c.pick_qty
                                                   where a.pallet_no = '{0}')
                            union
                                  select '' 料号,
                                 to_char(aa.location_no) 库位,
                                 to_char(aa.trolley_line_no) 车行号,
                                 listagg(aa.pointno, '*') within group(order by aa.pointno) as 箱数
                            from (select distinct decode(g.pack_pallet_no,
                                                         null,
                                                         c.pallet_no,
                                                         g.pack_pallet_no) pallet_no,
                                                  '' ictpn,
                                                  '' mpn,
                                                  d.location_no,
                                                  d.sides_no,
                                                  d.level_no,
                                                  d.seq_no,
                                                  d.pointno,
                                                  f.trolley_line_no
                                    from ppsuser.t_pallet_order c
                                   inner join ppsuser.vw_person_log d
                                      on c.delivery_no = d.delivery_no
                                     and c.line_item = d.line_item
                                     and c.ictpn = d.part_no
                                    join ppsuser.t_trolley_line_info f
                                      on d.trolley_no = f.trolley_no
                                     and d.sides_no = f.sides_no
                                     and d.level_no = f.level_no
                                     and d.seq_no = f.seq_no
                                    left join ppsuser.t_sn_ppart g
                                      on d.carton_no = g.carton_no
                                   where c.pallet_no = '{0}'
                                     and f.trolley_no not in ('ICT-00-00-000')
                                   order by d.location_no, f.trolley_line_no, d.pointno) aa
                           where aa.pallet_no = '{0}'
                           group by aa.pallet_no, aa.location_no, aa.trolley_line_no
                            union
                            select to_char(tss.part_no) 料号,
                                   to_char(tss.location_no) 库位,
                                   to_char(tss.pallet_no) 车行号,
                                   to_char(count(distinct tss.carton_no)) 箱数
                              from ppsuser.t_sn_status tss
                             where tss.wc = 'W0'
                               and tss.customer_sn in
                                   (select customer_sn
                                      from ppsuser.g_sn_status a1
                                     where a1.customer_sn not in
                                           (select custom_sn from ppsuser.t_trolley_sn_status)
                                       and (a1.delivery_no, a1.line_item, a1.part_no) in
                                           (select delivery_no, line_item, ictpn
                                              from ppsuser.t_pallet_order d
                                             where pallet_no = '{0}'))
                             group by tss.part_no, tss.location_no, tss.pallet_no
                             order by 库位 ", palletno);

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


        public DataSet GetSNInfoDataTableDAL(string inputSno)
        {
            string sql = string.Empty;
            //sql = string.Format(@"select distinct customer_sn, a.product_name
            //                          from ppsuser.mes_pallet_info a
            //                          join ppsuser.mes_sn_status b
            //                            on a.in_guid = b.in_guid
            //                           and a.pallet_no = b.pallet_no
            //                         where a.customer_sn in (select distinct customer_sn
            //                                                   from ppsuser.t_sn_status
            //                                                  where customer_sn = '{0}'
            //                                                    and carton_no = '{0}'
            //                                                    and pallet_no = '{0}')
            //                         order by customer_sn asc", inputSno);

            sql = string.Format(@"select to_char(customer_sn) customer_sn, to_char(carton_no) carton_no
                                      from ppsuser.t_sn_status
                                     where customer_sn = '{0}'
                                        or carton_no = '{0}'
                                        or pallet_no = '{0}'
                                    union
                                    select custom_sn customer_sn, carton_no
                                      from ppsuser.t_trolley_sn_status
                                     where trolley_no = '{0}'
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

        public DataSet GetSNInfoDataTableDAL2(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select distinct customer_sn, a.product_name
                                      from ppsuser.mes_pallet_info a
                                      join ppsuser.mes_sn_status b
                                        on a.in_guid = b.in_guid
                                       and a.pallet_no = b.pallet_no
                                     where b.customer_sn in (select distinct customer_sn
                                                               from ppsuser.t_sn_status
                                                              where customer_sn = '{0}'
                                                                or carton_no = '{0}'
                                                                or pallet_no = '{0}')
                                     order by customer_sn asc", inputSno);

            //sql = string.Format("select distinct customer_sn ,carton_no "
            //                              + "    from ppsuser.t_sn_status "
            //                              + "   where customer_sn = '{0}' "
            //                              + " or carton_no = '{1}'or pallet_no = '{2}'", inputSno, inputSno, inputSno);

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

        public DataSet GetShipmentTypeDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format("select distinct  type "
                                          + "    from ppsuser.t_shipment_info "
                                          + "   where shipment_id = '{0}' ", inputSno);

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


        //ppsuser.SP_PPS_INSERTWORKLOG(insn       in varchar2,
        //                                                 inwc       in varchar2,
        //                                                 macaddress in varchar2,
        //                                                 errmsg     out varchar2)
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

        public DataSet GetDataTableDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select aa.pallet_no,
                                       aa.ictpn,
                                       aa.qty,
                                       aa.carton_qty,
                                       aa.pick_qty,
                                       aa.pick_carton,
                                       aa.computer_name,
                                       aa.pick_status
                                  from (select pallet_no,
                                               ictpn,
                                               qty,
                                               carton_qty,
                                               pick_qty,
                                               pick_carton,
                                               (case
                                                 when pick_status = 'WP' then
                                                  'WP-未拣货'
                                                 when pick_status = 'IP' then
                                                  'IP-拣货中'
                                                 when pick_status = 'FP' then
                                                  'FP-已拣货'
                                                 when pick_status = 'QH' then
                                                  'QH-QHold'
                                                 else
                                                  pick_status
                                               end) pick_status,
                                               computer_name,
                                               (case
                                                 when pick_status = 'WP' then
                                                  '1'
                                                 when pick_status = 'IP' then
                                                  '2'
                                                 when pick_status = 'FP' then
                                                  '3'
                                                 when pick_status = 'QH' then
                                                  '99'
                                                 else
                                                  pick_status
                                               end) showlevel
                                          from ppsuser.T_SHIPMENT_PALLET_PART
                                         where pallet_no = '{0}') aa
                                 order by aa.showlevel asc,aa.ictpn asc", inputSno);
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

        public string CreateSAWBSIDByProcedure( out string errmsg)
        {
            object[][] procParams = new object[1][];
            procParams[0] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "Tmes", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.T_HANDLE_SAWB_UPS", procParams);
            errmsg = ds.Tables[0].Rows[0]["Tmes"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        
        public string GetDBTypeBySP(string inparatype, out string outparavalue, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inparatype", inparatype };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outparavalue", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_GETBASICPARAMETER", procParams);
            //create or replace procedure SP_PPS_GETBASICPARAMETER(inparatype   in varchar2,
            //                                                 outparavalue  out varchar2,
            //                                                 errmsg out varchar2) as
            outparavalue = ds.Tables[0].Rows[0]["outparavalue"].ToString();
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
        
        //create or replace procedure SP_PICK_PREASSIGN(insid  in varchar2,
        //                                 errmsg out varchar2) as
        public string PPartPreAssinPalletNOBySP(string strSID, out string errmsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PICK_PREASSIGN", procParams);
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

        public Boolean CheckMarinaServerUrlLogBySQL(string strguid, string strserverip, string strurl, string strSN, string strresult, string strempno, string strrequest, out string RetMsg)
        {
            object[][] sqlparams = new object[7][];
            string sql = string.Empty;

            //sql = string.Format(@"
            //                   insert into ppsuser.t_pick_marinawebservice
            //                   (msg_id, strserverip, strurl, pallet_no,  req_json, res_json, emp_no)
            //                 values
            //                   ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')
            //                         ",strguid, strserverip, strurl, strSN, strresult, strrequest, strempno);
            sql = string.Format(@"
                               insert into ppsuser.t_pick_marinawebservice
                               (msg_id, strserverip, strurl, pallet_no,  req_json, res_json, emp_no,createby)
                             values
                               (:inguid, :inserverip, :inurl, :insn, :inrequest, :inresult, :inempno,'PICK')
                                     ");
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strguid };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inserverip", strserverip };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inurl", strurl };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSN };
            sqlparams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inrequest", strrequest };
            sqlparams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inresult", strresult };
            sqlparams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", "" };
            DataSet dataSet = new DataSet();
            try
            {
                //dataSet = ClientUtils.ExecuteSQL(sql.Replace(";", ""));
                dataSet = ClientUtils.ExecuteSQL(sql, sqlparams);
            }
            catch (Exception e)
            {
                RetMsg = e.ToString();
                return false;
            }
            RetMsg = "";
            return true;
        }

        public Boolean CheckMarinaServerUrlLogBySP(string strguid, string strserverip, string strurl, string strSN, string strresult, string strempno, string strrequest, out string RetMsg)
        {
            object[][] procParams = new object[8][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inguid", strguid };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inserverip", strserverip };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inurl", strurl };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSN };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Clob, "inrequest", strrequest };
            //勿用 参数会被截取掉
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Clob, "inresult", strresult };
            procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempno", "" };
            procParams[7] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.sp_pick_insertmarinalog", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                return  false;
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataSet GetSIDPickListINFOBySQL(string strSIDType, string strSID, string strSIDStatus, string strRegion, string strCarrier,
            string strPOE, DateTime dtimeStart, DateTime dtimeEnd, string strPlant, string strWarehouse)
        {
            #region
            //string strSQL = string.Format(@"
            //                         select a.shipment_id 集货单号,
            //                    c.shipping_time 出货时间,
            //                    (select distinct scaccode
            //                       from pptest.oms_carrier_tracking_prefix d
            //                      where trim(d.carriercode) = c.carrier_code
            //                        and d.shipmode = c.transport
            //                        and d.isdisabled = '0'
            //                        and d.type = 'HAWB') as carrier,
            //                    c.poe,
            //                    c.region 地区,
            //                    a.pallet_no 栈板号,
            //                    case
            //                      when a.pallet_type = '001' then
            //                       'NO MIX'
            //                      when a.pallet_type = '999' then
            //                       'MIX'
            //                      else
            //                       a.pallet_type
            //                    end 栈板类型,
            //                    (select distinct custpart
            //                       from pptest.oms_partmapping
            //                      where part = b.ictpn) mpn,
            //                    b.ictpn 料号,
            //                    b.qty 数量,
            //                    b.carton_qty 箱数,
            //                    b.pick_carton 已pick箱数,
            //                    case
            //                      when b.pick_status = 'WP' then
            //                       'WP-未拣货'
            //                      when b.pick_status = 'IP' then
            //                       'IP-拣货中'
            //                      when b.pick_status = 'FP' then
            //                       'FP-已拣货'
            //                      when b.pick_status = 'HO' then
            //                       'HO-QHold'
            //                      else
            //                       b.pick_status
            //                    end part_pick_status,
            //                    case
            //                      when a.pick_status = 'WP' then
            //                       'WP-未拣货'
            //                      when a.pick_status = 'IP' then
            //                       'IP-拣货中'
            //                      when a.pick_status = 'FP' then
            //                       'FP-已拣货'
            //                      when a.pick_status = 'HO' then
            //                       'HO-QHold'
            //                      else
            //                       a.pick_status
            //                    end pallet_pick_status,
            //                    a.pack_code,
            //                    case
            //                      when c.region = 'EMEIA' and
            //                           (c.carrier_code like '%DHL%' or c.carrier_name like '%DHL%') and
            //                           c.service_level = 'WPX' then
            //                       d.remark || 'WPX'
            //                      else
            //                       d.remark
            //                    end as packcodedesc,
            //                    c.priority priority
            //               from ppsuser.t_shipment_pallet a
            //              inner join ppsuser.t_shipment_pallet_part b
            //                 on a.pallet_no = b.pallet_no
            //              inner join ppsuser.t_shipment_info c
            //                 on a.shipment_id = c.shipment_id
            //               left join (select e.packcode, min(e.remark) remark
            //                            from (select distinct packcode,
            //                                                  palletlengthcm || '*' || palletwidthcm as remark
            //                                    from ppsuser.vw_mpn_info) e
            //                           group by packcode) d
            //                 on a.pack_code = d.packcode
            //              where c.status not in ('WS', 'SF')
            //               and 1 = 1
            //        ");
            #endregion
            #region //20200803 取消VW_MPN_INFO
            string strSQL = string.Format(@"
                                      select a.shipment_id 集货单号,
                                     c.shipping_time 出货时间,
                                     (select distinct scaccode
                                        from pptest.oms_carrier_tracking_prefix d
                                       where trim(d.carriercode) = c.carrier_code
                                         and d.shipmode = c.transport
                                         and d.isdisabled = '0'
                                         and d.type = 'HAWB') as carrier,
                                     c.poe,
                                     c.region 地区,
                                     a.pallet_no 栈板号,
                                     case
                                       when a.pallet_type = '001' then
                                        'NO MIX'
                                       when a.pallet_type = '999' then
                                        'MIX'
                                       else
                                        a.pallet_type
                                     end 栈板类型,
                                     (select distinct custpart
                                        from pptest.oms_partmapping
                                       where part = b.ictpn) mpn,
                                     b.ictpn 料号,
                                     b.qty 数量,
                                     b.carton_qty 箱数,
                                     b.pick_carton 已pick箱数,
                                     case
                                       when b.pick_status = 'WP' then
                                        'WP-未拣货'
                                       when b.pick_status = 'IP' then
                                        'IP-拣货中'
                                       when b.pick_status = 'FP' then
                                        'FP-已拣货'
                                       when b.pick_status = 'HO' then
                                        'HO-QHold'
                                       else
                                        b.pick_status
                                     end part_pick_status,
                                     case
                                       when a.pick_status = 'WP' then
                                        'WP-未拣货'
                                       when a.pick_status = 'IP' then
                                        'IP-拣货中'
                                       when a.pick_status = 'FP' then
                                        'FP-已拣货'
                                       when a.pick_status = 'HO' then
                                        'HO-QHold'
                                       else
                                        a.pick_status
                                     end pallet_pick_status,
                                     a.pack_code,
                                     case
                                       when c.region = 'EMEIA' and
                                            (c.carrier_code like '%DHL%' or c.carrier_name like '%DHL%') and
                                            c.service_level = 'WPX' then
                                        palletlengthcm || '*' || palletwidthcm || 'WPX'
                                       else
                                        palletlengthcm || '*' || palletwidthcm
                                     end as packcodedesc,
                                     c.priority priority,
                                     c.plant,
                                     c.sloc
                                from ppsuser.t_shipment_pallet a
                               inner join ppsuser.t_shipment_pallet_part b
                                  on a.pallet_no = b.pallet_no
                               inner join ppsuser.t_shipment_info c
                                  on a.shipment_id = c.shipment_id
                                join pptest.oms_carton_info oci1
                                  on a.pack_code = oci1.packcode
                                join pptest.oms_pallet_info opi1
                                  on oci1.palletcode = opi1.palletcode
                               where c.status not in ('WS', 'SF')
                                 and 1 = 1
                    ");
            #endregion
            //组合输入查询条件，过滤数据源

            object[][] sqlparams = new object[0][];
            Array.Resize(ref sqlparams, sqlparams.Length + 1);

            strSQL += " and c.shipment_type = :shipmentType ";

            int iPara = 0;                             
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentType", strSIDType };
            iPara = iPara + 1;

            //集货单号查询条件
            if (strSID.Trim() != "" && strSID.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSQL += " and a.Shipment_ID = :shipmentID";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentID", strSID };
                iPara = iPara + 1;
            }

            //货代查询条件
            if (strCarrier.Trim() != "" && strCarrier.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSQL += @"and c.carrier_code in 
                                ( select carriercode 
                                    from pptest.oms_carrier_tracking_prefix 
                                   where scaccode = :carrier )";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "carrier", strCarrier };
                iPara = iPara + 1;
            }

            //港口查询条件
            if (strPOE.Trim() != "" && strPOE.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSQL += " and c.poe = :poe ";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "poe", strPOE };
                iPara = iPara + 1;
            }

            //状态查询条件// 这里有个状态 查不到的 QH

            //"-ALL-",    --不用加调整 
            //"WP-未PICK",
            //"WP-未PACK",
            //"IP-PACK中",
            //"CP-CANCEL",
            //"HO-HOLD"，
            //"ALL-未PICK"  --显示所有时间段
            string strStatus = string.Empty;
            if (strSIDStatus.Equals("ALL-未PICK")) 
            {
                strSQL += " and b.pick_status in ('WP','IP')  ";
            }
            else
            { 
                if (strSIDStatus.Equals("WP-未PICK"))
                {
                    strSQL += " and b.pick_status in ('WP','IP') ";
                }
                else if (strSIDStatus.Equals("WP-未PACK"))
                {
                    strSQL += " and c.status in ('WP') ";
                }
                else if (strSIDStatus.Equals("IP-PACK中"))
                {
                    strSQL += " and c.status in ('IP') ";
                }
                else if (strSIDStatus.Equals("CP-CANCEL"))
                {
                    strSQL += " and c.status in ('CP') ";
                }
                else if (strSIDStatus.Equals("HO-HOLD"))
                {
                    strSQL += " and c.status in ('HO') ";
                }
                else
                { 
                }

                if (strPlant != "" && strPlant != "ALL")
                {
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSQL += " and c.plant = :plant";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "plant", strPlant };
                    iPara = iPara + 1;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSQL += " and c.sloc = :sloc";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "sloc", strWarehouse };
                    iPara = iPara + 1;
                }

                //出货开始日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSQL += " and c.shipping_time >= :shipmentTime";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "shipmentTime", dtimeStart};
                iPara = iPara + 1;

                //出货结束日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSQL += " and c.shipping_time < :shipmentTime2";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "shipmentTime2", dtimeEnd };
                iPara = iPara + 1;

            }

            //地区查询条件
            if (strRegion.Trim() != "" && strRegion.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSQL += " and c.region = :Region";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Region", strRegion };
                iPara = iPara + 1;
            }
            //添加排序
            strSQL += " order by c.priority asc, a.pallet_no asc ,mpn desc, b.ictpn asc ";

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(strSQL, sqlparams);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;
        }

        public DataSet GetCartonPartInfoBySQL(string strCartonNo)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 select min(a.part_no ) as part_no
                                  from ppsuser.t_sn_status a
                                     where a.carton_no = '{0}'
                                     ", strCartonNo);

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
        public string GetMarinaPackoutFlagBySP(string strSN, string strStation, out string strMarinaFlag, out string strPackoutFlag, out string errmsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSN };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "instation", strStation };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outmarinaflag", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outpackoutflag", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_pps_marinapackoutcheck", procParams);
            //create or replace procedure ppsuser.sp_pps_marinapackoutcheck(insn           in varchar2,
            //                                                  instation      in varchar2,
            //                                                  outmarinaflag  out varchar2,
            //                                                  outpackoutflag out varchar2,
            //                                                  errmsg         out varchar2) as
            strMarinaFlag = ds.Tables[0].Rows[0]["outmarinaflag"].ToString();
            strPackoutFlag = ds.Tables[0].Rows[0]["outpackoutflag"].ToString();
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

        public string PickCheckSNBySP(string strSN, string strPackPalletNo, out string strOutSN ,out string errmsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSN };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpackpalletno", strPackPalletNo };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outsn", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_pick_checksn", procParams);
            //ppsuser.sp_pick_checksn(insn           in varchar2,
            //                                        inpackpalletno in varchar2,
            //                                        outsn          out varchar2,
            //                                        errmsg         out varchar2) as
            strOutSN = ds.Tables[0].Rows[0]["outsn"].ToString();
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

        public string CreateTrackingNoBySP(string strSID, out string errmsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_pick_createtrackingno", procParams);
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
        public DataSet checkcarton(string inputSno)
        {
            string sql = string.Empty;

            sql = string.Format(@"select decode('{0}',carton_no,'1',customer_sn, '0',pallet_no,'2') CartonFlag, carton_no, pick_pallet_no, shipment_id 
                                    from ppsuser.t_sn_status where 
                                    (carton_no='{0}'
                                    or customer_sn='{0}'
                                    or pallet_no='{0}'
                                    or pick_pallet_no='{0}')
                                    and rownum=1", inputSno);
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
        public bool IsFinishShipExec(string pickPallet, out string errmsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", pickPallet };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_ups_api_finish", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable GetShipmentInfo(string shipmentID)
        {
            string sql = "SELECT REGION from T_SHIPMENT_INFO where SHIPMENT_ID='{0}'";
            sql = String.Format(sql, shipmentID);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable GetCartonTableDAL(string strPickpalletno)
        {
            string sql = String.Format(@"SELECT DISTINCT CARTON_NO FROM PPSUSER.T_SN_STATUS 
                WHERE CARTON_NO NOT IN(SELECT CARTON_NO FROM PPSUSER.T_UPS_RAWDATA WHERE CARTON_NO IN 
                (SELECT DISTINCT CARTON_NO FROM PPSUSER.T_SN_STATUS WHERE PICK_PALLET_NO = '{0}'))
                AND PICK_PALLET_NO = '{0}'", strPickpalletno);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getUpsInfoByCartonNo(string cartonNo, string region)
        {
            //修改逻辑 按MODEL来显示 AMR/PAC/EMEIA都要显示
            string instruction = Lithium_Batteries(cartonNo);
            string handleSql = @" select distinct (select FGWEIGHTKGP
                                       from pptest.oms_partmapping OPP,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where OPP.PART = T.PART_NO
                                         AND (OPP.SUBPACKCODE = T.PACK_CODE OR T.PACK_CODE = OPP.PACKCODE )) as WEIGHT_UNIT,
                                        ( select sum(GROSSWEIGHTKG * t.CARTON_QTY) total_DN
                                       from ppsuser.vw_mpn_info P_VMI,
                                        (select ictpn,PACK_CODE,sum(CARTON_QTY) CARTON_QTY 
                                        from(
                                                SELECT DISTINCT tpo.ictpn, TSP.PACK_CODE, tpo.line_item, tot.CARTON_QTY
                                                FROM PPSUSER.T_PALLET_ORDER       tpo,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP,
                                                   -- PPSUSER.T_SHIPMENT_SAWB tsw,
                                                    PPSUSER.T_ORDER_INFO tot,
                                                    PPSUSER.T_ALLO_TRACKINGNO tat
                                                WHERE tpo.PALLET_NO = TSP.PALLET_NO
                                                   -- and tpo.SHIPMENT_ID = tsw.SHIPMENT_ID
                                                    and tot.DELIVERY_NO=tpo.DELIVERY_NO
                                                    AND tpo.DELIVERY_NO = tat.DELIVERY_NO
                                                    and tpo.ICTPN = tot.ICTPN
                                                    and tat.CARTON_NO='{0}') 
                                               group by ictpn,PACK_CODE) T
                                      where P_VMI.ICTPARTNO = T.ictpn
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as TOTAL_WEIGHT,
                                        '' shipment_tracking,
                                        '' SAWB,
										t9u.SERVICELEVEL as SERVICELEVELID,
										(select coo from PPSUSER.T_SN_STATUS where CARTON_NO='{0}' and rownum=1) OriginCountry, 
										tsi.hawb,
                                        tsi.shipment_tracking as SHIPMENTREACKING,
                                        tat.tracking_no,
                                        to_char(tsi.shipping_time, 'yyyy/MM/dd') as shipdate,
                                        t9u.parcelaccountnumber,
                                        (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                        (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                        (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                        '' as SHIPER_ADDRESS3,
                                        (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                        (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                        (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                        (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                        '' as Consignee_UPS_Account_number,
                                        t9u.shiptoname,
                                        t9u.shiptocompany,
                                        t9u.shiptoconttel,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 1, 35)
                                          else
                                           t9u.shiptoaddress
                                        end as ST_ADDR1,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                          else
                                           t9u.shiptoaddress2
                                        end as ST_ADDR2,                     
                                        case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end       
                                         as ST_ADDR3,
                                        t9u.shiptocity,
                                        decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) as regiondesc, --RegionDesc  如果有=号码, 那么取=号之前的
                                        t9u.shiptozip,
                                        t9u.shipcntycode,
                                        tat.box_no as CARTON_SEQUNECE,
                                        (select sum(toi.carton_qty)
                                           from ppsuser.t_order_info toi
                                          where toi.delivery_no = tat.delivery_no
                                                ) as CARTON_COUNT,
                                        (  select GROSSWEIGHTKG
                                              from PPsuser.vw_mpn_info P_VMI,
                                                   (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                      FROM PPSUSER.T_SN_STATUS TSS, 
                                                           PPSUSER.T_SHIPMENT_PALLET TSP
                                                     WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
                                                       AND TSS.CARTON_NO = '{0}') T
                                             where P_VMI.ICTPARTNO = T.PART_NO
                                               AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,                 
                                        (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM * P_VMI.CARTONWIDTHCM) / 1000000/6000,2)
                                        from PPsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                FROM PPSUSER.T_SN_STATUS TSS, 
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                                WHERE substr(TSS.Pick_Pallet_No,3) = TSP.PALLET_NO
                                                  AND TSS.CARTON_NO = '{0}') T
                                        where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                        (select ttl.tracking_no from ppsuser.t_tracking_no_log ttl where ttl.carton_no = '{0}' ) SSCC,
                                        tat.delivery_no,
                                        t9u.custsono,
                                        t9u.custpono,
                                        t9u.weborderno,
                                        t9u.custdelitem,
                                        (SELECT DISTINCT TOI.MPN
                                       FROM PPSUSER.T_ORDER_INFO TOI
                                      WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
                                        and toi.ictpn in (select distinct tss.part_no from PPSUSER.T_SN_STATUS tss where tss.CARTON_NO= '{0}' and rownum = 1) ) AS AC_PN,  
                                        (select count(tss_.serial_number)
                                           from ppsuser.t_sn_status tss_
                                          where tss_.carton_no = '{0}') as perCartonQty,
                                        tat.carton_no,
                                        '{1}' as Delivery_Instruction,
                                        ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                        'USD',
                                        DECODE(SUBSTR(t9u.custshipinst, 1, 5),
                                               'ACDES',
                                               substr(t9u.custshipinst, 5),
                                               t9u.custshipinst) custshipinst,
                                        '' as HAWB_,
                                       (select distinct substr(pick_pallet_no,-4)from ppsuser.t_sn_status tss where tss.carton_no = '{0}')as PALLET_ID,
                                        '' as CARTON_ID
                          from ppsuser.t_shipment_info tsi,
                               ppsuser.t_allo_trackingno tat,
                               ppsuser.t_940_unicode   t9u
                         where tsi.shipment_id = tat.shipment_id
                           and tat.delivery_no = t9u.deliveryno
                           and tat.line_item = trim(t9u.custdelitem)
                           and tat.carton_no = '{0}'";
            #region
            /*string handleSql = @" select distinct tsi.hawb,
            tsi.shipment_tracking,
                                        tss.tracking_no,
                                        to_char(tsi.shipping_time, 'yyyy/MM/dd') as shipdate,
                                        t9u.parcelaccountnumber,
                                        (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                        (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                        (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                        '' as SHIPER_ADDRESS3,
                                        (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                        (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                        (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                        (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                        '' as Consignee_UPS_Account_number,
                                        t9u.shiptoname,
                                        t9u.shiptocompany,
                                        t9u.shiptoconttel,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 1, 35)
                                          else
                                           t9u.shiptoaddress
                                        end as ST_ADDR1,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                          else
                                           t9u.shiptoaddress2
                                        end as ST_ADDR2,                     
                                        case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end       
                                         as ST_ADDR3,
                                        t9u.shiptocity,
                                        decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) as regiondesc, --RegionDesc  如果有=号码, 那么取=号之前的
                                        t9u.shiptozip,
                                        t9u.shipcntycode,
                                        tss.box_no as CARTON_SEQUNECE,
                                        (select sum(toi.carton_qty)
                                           from ppsuser.t_order_info toi
                                          where toi.delivery_no =
                                                (select distinct tss.delivery_no
                                                   from ppsuser.t_sn_status tss
                                                  where tss.carton_no = '{0}')) as CARTON_COUNT,
                                        (  select GROSSWEIGHTKG
                                              from PPsuser.vw_mpn_info P_VMI,
                                                   (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                      FROM PPSUSER.T_SN_STATUS TSS, 
                                                           PPSUSER.T_SHIPMENT_PALLET TSP
                                                     WHERE TSS.pack_pallet_no = TSP.PALLET_NO
                                                       AND TSS.CARTON_NO = '{0}') T
                                             where P_VMI.ICTPARTNO = T.PART_NO
                                               AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,              
                                        (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM * P_VMI.CARTONWIDTHCM) / 1000000/6000,2)
                                        from PPsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                FROM PPSUSER.T_SN_STATUS TSS, 
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                                WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
                                                  AND TSS.CARTON_NO = '{0}') T
                                        where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                        tss.sscc,
                                        tss.delivery_no,
                                        t9u.custsono,
                                        t9u.custpono,
                                        t9u.weborderno,
                                        t9u.custdelitem,
                                        (SELECT DISTINCT TOI.MPN
                                           FROM PPSUSER.T_ORDER_INFO TOI
                                          WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO  and toi.ictpn  =   tss.part_no) AS AC_PN,
                                        (select count(tss_.serial_number)
                                           from ppsuser.t_sn_status tss_
                                          where tss_.carton_no = '{0}') as perCartonQty,
                                        tss.carton_no,
                                        '{1}' as Delivery_Instruction,
                                        ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                        'USD',
                                        DECODE(SUBSTR(t9u.custshipinst, 1, 5),
                                               'ACDES',
                                               substr(t9u.custshipinst, 5),
                                               t9u.custshipinst) custshipinst,
                                        '' as HAWB_,
                                        SUBSTR(TSS.PACK_PALLET_NO,-4) as PALLET_ID,
                                        '' as CARTON_ID
                          from ppsuser.t_shipment_info tsi,
                               ppsuser.t_sn_status     tss,
                               ppsuser.t_940_unicode   t9u
                         where tsi.shipment_id = tss.shipment_id
                           and tss.delivery_no = t9u.deliveryno
                           and tss.line_item = trim(t9u.custdelitem) 
                           and tss.carton_no = '{0}'";*/
            #endregion 
            if (region.Equals("AMR"))
            {
                handleSql = @"select distinct  (select FGWEIGHTKGP
                                       from pptest.oms_partmapping OPP,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where OPP.PART = T.PART_NO
                                        AND (OPP.SUBPACKCODE = T.PACK_CODE OR T.PACK_CODE = OPP.PACKCODE )) as WEIGHT_UNIT,
                             ( select sum(GROSSWEIGHTKG * t.CARTON_QTY) total_DN
                                       from ppsuser.vw_mpn_info P_VMI,
                                        (select ictpn,PACK_CODE,sum(CARTON_QTY) CARTON_QTY 
                                        from(
                                                SELECT DISTINCT tpo.ictpn, TSP.PACK_CODE, tpo.line_item, tot.CARTON_QTY
                                                FROM PPSUSER.T_PALLET_ORDER       tpo,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP,
                                                    PPSUSER.T_SHIPMENT_SAWB tsw,
                                                    PPSUSER.T_ORDER_INFO tot,
                                                    PPSUSER.T_ALLO_TRACKINGNO tat
                                                WHERE tpo.PALLET_NO = TSP.PALLET_NO
                                                    and tpo.SHIPMENT_ID = tsw.SHIPMENT_ID
                                                    and tot.DELIVERY_NO=tpo.DELIVERY_NO
                                                    AND tpo.DELIVERY_NO = tat.DELIVERY_NO
                                                    and tpo.ICTPN = tot.ICTPN
                                                    and tat.CARTON_NO='{0}') 
                                               group by ictpn,PACK_CODE) T
                                      where P_VMI.ICTPARTNO = T.ictpn
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as TOTAL_WEIGHT,
                            t9u.SERVICELEVELID,
                            (select coo from PPSUSER.T_SN_STATUS where CARTON_NO='{0}' and rownum=1) OriginCountry, 
                            (SELECT distinct   tsi.hawb
                              FROM PPSUSER.t_Order_Info    toi,
                                   ppsuser.t_allo_trackingno tat,
                                   ppsuser.t_shipment_info tsi
                             where toi.delivery_no = tat.delivery_no
                               and tsi.shipment_id = toi.shipment_id
                               and toi.shipment_id in
                                   (SELECT DISTINCT TSSA.SHIPMENT_ID
                                      FROM PPSUSER.t_allo_trackingno TAT, PPSUSER.T_SHIPMENT_SAWB TSSA
                                     WHERE TAT.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                       AND TAT.CARTON_NO = '{0}')
                               and tat.carton_no = '{0}'
                               ) AS HAWB,
                                   (SELECT distinct   tsi.shipment_tracking
                                  FROM PPSUSER.t_Order_Info    toi,
                                       ppsuser.t_allo_trackingno  tat,
                                       ppsuser.t_shipment_info tsi
                                 where toi.delivery_no = tat.delivery_no
                                   and tsi.shipment_id = toi.shipment_id
                                   and toi.shipment_id in
                                       (SELECT DISTINCT TSSA.SHIPMENT_ID
                                          FROM PPSUSER.t_allo_trackingno TAT, PPSUSER.T_SHIPMENT_SAWB TSSA
                                         WHERE TAT.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                           AND TAT.CARTON_NO = '{0}')
                                   and tat.carton_no = '{0}'
                                  ) AS SHIPMENTREACKING,
                                    tat.tracking_no,
                                    to_char(tsi.shipping_time, 'yyyy/MM/dd') SHIPDATE,
                                    t9u.parcelaccountnumber,
                                    (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                    (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                    (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                    '' as SHIPER_ADDRESS3,
                                    (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                    (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                    (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                    (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                    '' as Consignee_UPS_Account_number,
                                    t9u.shiptoname,
                                    t9u.shiptocompany,
                                    t9u.shiptoconttel,
                                    case
                                      when length(t9u.shiptoaddress) > 35 then
                                       substr(t9u.shiptoaddress, 1, 35)
                                      else
                                       t9u.shiptoaddress
                                    end as ST_ADDR1,
                                    case
                                      when length(t9u.shiptoaddress) > 35 then
                                       substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                      else
                                       t9u.shiptoaddress2
                                    end as ST_ADDR2,
                                    case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end        as ST_ADDR3,
                                    t9u.shiptocity,
                                    decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) AS  REGIONDESC, --RegionDesc  如果有 = 号码, 那么取 = 号之前的
                                    t9u.shiptozip,
                                    t9u.shipcntycode,
                                    tat.box_no as CARTON_SEQUNECE,
                                    (select sum(toi.carton_qty)
                                       from ppsuser.t_order_info toi
                                      where toi.delivery_no =
                                            (select distinct tat.delivery_no
                                               from ppsuser.t_allo_trackingno tat
                                              where tat.carton_no = '{0}')
                                         AND TOI.SHIPMENT_ID = Tat.SHIPMENT_ID) as CARTON_COUNT,        
                                   (select GROSSWEIGHTKG
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,
                                    (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM *
                                                  P_VMI.CARTONWIDTHCM) / 1000000 / 6000,
                                                  2)
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                    (select ttl.tracking_no from ppsuser.t_tracking_no_log ttl where ttl.carton_no = '{0}' ) SSCC,
                                    tat.delivery_no,
                                    t9u.custsono,
                                    t9u.custpono,
                                    t9u.weborderno,
                                    t9u.custdelitem,
                                    (SELECT DISTINCT TOI.MPN
                                       FROM PPSUSER.T_ORDER_INFO TOI
                                      WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
                                        and toi.ictpn in (select tss.part_no from PPSUSER.T_SN_STATUS tss where tss.CARTON_NO=tat.Carton_no and rownum=1)
                                      ) AS AC_PN,
                                    (select count(tss_.serial_number)
                                       from ppsuser.t_sn_status tss_
                                      where tss_.carton_no = '{0}') as perCartonQty,
                                    tat.carton_no,
                                   '{1}' as Delivery_Instruction,
                                    ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                        
                                    'USD',
                                    DECODE(SUBSTR(t9u.custshipinst, 1, 7),
                                           'ACDES--',
                                           substr(t9u.custshipinst, 8),
                                           t9u.custshipinst) custshipinst,
                                    '' as HAWB_,
                                    (select distinct substr( tss.pick_pallet_no,-4) from ppsuser.t_sn_status tss where tss.carton_no = '{0}')as PALLET_ID,
                                    '' as CARTON_ID,
                                    tsi.shipment_tracking,
                                    tsi.hawb as SAWB,
                                    tsi.carton_qty,
                                    tsi.poe
                      from ppsuser.t_shipment_info tsi,
                           ppsuser.t_allo_trackingno tat,
                           ppsuser.t_940_unicode t9u
                     where tsi.shipment_id = tat.shipment_id
                       and tat.delivery_no = t9u.deliveryno
                       and tat.line_item = trim(t9u.custdelitem)
                       and tat.carton_no = '{0}'";
            }
            string sql = string.Format(handleSql, cartonNo, instruction);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public string Lithium_Batteries(string strCarton)
        {
            string instruction = "";
            //add shipment parameter for dhl bbx mother file by franky 2021/5/14
            DataTable dtTemp = ClientUtils.ExecuteSQL(string.Format(@"SELECT DISTINCT c.HAZARDOUS, d.remark as PI9X
                           FROM PPSUSER.T_SN_STATUS     a,
                                ppsuser.VW_MPN_INFO     b,
                                PPTEST.OMS_MODEL        c,
                                pptest.oms_codemstr     d,
                                ppsuser.t_shipment_info e,
                                PPSUSER.T_ALLO_TRACKINGNO f
                          WHERE a.CARTON_NO = '{0}'
                            and a.CARTON_NO = f.CARTON_NO
                            and a.PART_NO = b.ICTPARTNO
                            and b.CUSTMODEL = c.CUSTMODEL
                            and f.shipment_id = e.shipment_id
                            and c.pi9x = d.value(+)
                            and instr(e.carrier_name, substr(d.code, instr(d.code, 'x') + 1)) > 0
                         union
                         SELECT DISTINCT c.HAZARDOUS, d.remark as PI9X
                           FROM PPSUSER.T_SN_STATUS     a,
                                ppsuser.VW_MPN_INFO     b,
                                PPTEST.OMS_MODEL        c,
                                pptest.oms_codemstr     d,
                                ppsuser.t_shipment_info e,
                                PPSUSER.T_ALLO_TRACKINGNO f
                          WHERE f.shipment_id = '{0}'
                          and a.CARTON_NO = f.CARTON_NO
                            and a.PART_NO = b.ICTPARTNO
                            and b.CUSTMODEL = c.CUSTMODEL
                            and f.shipment_id = e.shipment_id
                            and c.pi9x = d.value(+)
                             and instr(e.carrier_name, substr(d.code, instr(d.code, 'x') + 1)) > 0 ", strCarton)).Tables[0];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                if (dtTemp.Rows[0]["HAZARDOUS"].ToString().ToUpper() == "Y")
                    instruction = dtTemp.Rows[0]["PI9X"].ToString();
            }
            return instruction;
        }
        public DataSet GetUserContext(string paraType)
        {
            DataSet data = new DataSet();
            string sql = string.Empty;
            sql = string.Format(@"SELECT PARA_VALUE from T_BASICPARAMETER_INFO where PARA_TYPE = '{0}' and ENABLED = 'Y' and rownum=1", paraType);
            try
            {
                data = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return data;
        }
        public DataSet GetClientAccess(string paraType)
        {
            DataSet data = new DataSet();
            string sql = string.Empty;
            sql = string.Format(@"SELECT PARA_VALUE from T_BASICPARAMETER_INFO where PARA_TYPE = '{0}' and ENABLED = 'Y' and rownum=1", paraType);
            try
            {
                data = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return data;
        }

        public ExecuteResult GetWarningSNShipExec(string inputsn)
        {
            var res = new ExecuteResult();
            string sql = string.Format(@"SELECT DISTINCT tss.SHIPMENT_ID, tss.PICK_PALLET_NO, tss.CARTON_NO, DECODE(tur.CARTON_NO, null, '0','1') UPS_FLAG  
                                from PPSUSER.T_SN_STATUS tss
                                join PPSUSER.T_SHIPMENT_INFO tsi on tss.SHIPMENT_ID = tsi.SHIPMENT_ID
                                left join PPSUSER.T_UPS_RAWDATA tur on tss.CARTON_NO = tur.CARTON_NO
                                where (PICK_PALLET_NO='{0}' or tss.CARTON_NO='{1}')
                                and tsi.STATUS not in ('WS','SF') and tsi.CARRIER_CODE like '%UPS%'", inputsn, inputsn);
            try
            {
                res.Anything = ClientUtils.ExecuteSQL(sql).Tables[0];
                res.Status = true;
            }
            catch (Exception e)
            {
                res.Message = e.Message;
                res.Status = false;
            }
            return res;
        }


    }
}
