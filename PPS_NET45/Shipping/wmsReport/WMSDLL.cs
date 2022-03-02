using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace wmsReport
{
    class WMSDLL
    {
        public DataSet GetStockInfoDataTable(string carlineno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select aa.location_no,
                               aa.custom_sn,
                               aa.trolley_line_no,
                               aa.pointno,
                               aa.group_code,
                               aa.delivery_no,
                               aa.line_item,
                               LISTAGG(aa.shipment_id, ',') WITHIN GROUP(ORDER BY aa.shipment_id) as shipment_id
                          from (select distinct d.location_no,
                                                a.custom_sn,
                                                e.trolley_line_no,
                                                a.pointno,
                                                a.group_code,
                                                b.delivery_no,
                                                b.line_item,
                                                c.shipment_id
                                  from ppsuser.t_trolley_sn_status a
                                  join ppsuser.vw_person_log b
                                    on a.custom_sn = b.customer_sn
                                  left join ppsuser.t_order_info c
                                    on b.delivery_no = c.delivery_no
                                   and b.line_item = c.line_item
                                   and b.PART_NO = c.ictpn
                                  join ppsuser.t_sn_status d
                                    on a.custom_sn = d.customer_sn
                                  join ppsuser.t_trolley_line_info e
                                    on a.trolley_no = e.trolley_no
                                   and a.sides_no = e.sides_no
                                   and a.level_no = e.level_no
                                   and a.seq_no = e.seq_no
                                 where e.trolley_line_no = '{0}'
                                   and e.trolley_line_no <> 'ICT-00-00-000-0000'
                                   and rownum <500
                                   and (c.shipment_id not in
                                       (select shipment_id from ppsuser.t_shipment_sawb) or
                                       c.shipment_id is null)) aa
                         group by aa.location_no,
                                  aa.custom_sn,
                                  aa.trolley_line_no,
                                  aa.pointno,
                                  aa.group_code,
                                  aa.delivery_no,
                                  aa.line_item 
                         order by aa.pointno asc", carlineno);
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

        public string WMSPpartCheckBySP(string incarlineno, string incsn,  out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlineno", incarlineno };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_PPARTCHECK", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;
            //if (errmsg.Equals("OK"))
            //{
            //    return "OK";
            //}
            //else
            //{
            //    return "NG";
            //}

        }

        public string WMSPpartTransBySP(string incarlinenofrom, string incarlinenoto, string incsn, out string errmsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlinenofrom", incarlinenofrom };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlinenoto", incarlinenoto };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_PPARTTRANS", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;
        }


        public string GetCarlinenoByCSNBySP(string incsn, out string carlineno, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "carlineno", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_GETCARLINENOBYCSN", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            carlineno = ds.Tables[0].Rows[0]["carlineno"].ToString();
            return errmsg;
        }
        //P_PROPOSAL_TROLLEY(vCartonNo        in varchar2, vTROLLEY_LINE_NO out varchar2)

        public void GetCarlinenoByAdviseBySP(string incsn, out string carlineno)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "vCartonNo", incsn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "vTROLLEY_LINE_NO", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.P_PROPOSAL_TROLLEY", procParams);
            carlineno = ds.Tables[0].Rows[0]["vTROLLEY_LINE_NO"].ToString();
           
        }
        public string WMSTrolleyMoveBySP(string incar, string inlocationto, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incar", incar };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationto", inlocationto };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMS_MOVETROLLEY", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;
            
        }
        public DataSet GetCarInfoDataTable(string strcar)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                               select a.trolley_no,
                                a.trolley_line_no,
                                a.pointno,
                                a.pallet_no,
                                a.carton_no,
                                a.custom_sn,
                                a.delivery_no,
                                a.line_item,
                                b.location_no
                           from ppsuser.t_trolley_sn_status a
                           join ppsuser.t_sn_status b
                             on a.custom_sn = b.customer_sn
                          where a.trolley_no = '{0}'
                          order by a.trolley_line_no asc, a.pointno asc
                            ", strcar);
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

        public DataSet GetShipmentInfoDataTableBySQL()
        {
            string sql = string.Empty;
            //车牌号	集货单号	出货类型	地区	是否上传856	板数	状态	整车板数	TDM

            sql = string.Format(@"
                                select m.car_no         车牌号,
                                       m.shipment_id    集货单号,
                                       m.type           出货类型,
                                       m.region         地区,
                                       m.carrier_name   货代， m.uploadflag 是否上传856,
                                       m.palletcount    板数,
                                       m.status         状态,
                                       n.palletcountall 整车板数,
                                       n.time_s         TDM
                                  from (select nvl(g.car_no, ' ') as car_no,
                                               g.shipment_id,
                                               g.type,
                                               g.carrier_name,
                                               g.region,
                                               g.uploadflag,
                                               g.palletcount,
                                               case
                                                 when g.status = 'UF' or g.status = 'LF' then
                                                  '已装车'
                                                 when g.weightcount = 0 then
                                                  '已称重'
                                                 when g.pickcount > 0 then
                                                  '作业中'
                                                 else
                                                  '未Pick'
                                               end as status,
                                               g.time_s
                                          from (select c.car_no,
                                                       a.shipment_id,
                                                       a.type,
                                                       a.carrier_name,
                                                       a.region,
                                                       case
                                                         when a.status = 'UF' then
                                                          'Y'
                                                         else
                                                          'N'
                                                       end as uploadflag,
                                                       count(distinct b.pallet_no) as palletcount,
                                                       a.status,
                                                       sum(case
                                                             when b.pick_status = 'IP' or b.pick_status = 'FP' then
                                                              1
                                                             else
                                                              0
                                                           end) as pickcount,
                                                       sum(case
                                                             when nvl(b.weight, '0') = '0' then
                                                              1
                                                             else
                                                              0
                                                           end) as weightcount,
                                                       min(f.time_s) as time_s
                                                  from ppsuser.t_shipment_info a
                                                 inner join ppsuser.t_shipment_pallet b
                                                    on a.shipment_id = b.shipment_id
                                                 inner join ppsuser.t_shipment_pallet_part d
                                                    on b.pallet_no = d.pallet_no
                                                 inner join pptest.oms_partmapping e
                                                    on d.ictpn = e.part
                                                  left join pptest.oms_load_car c
                                                    on a.shipment_id = c.shipment_id
                                                   and b.pallet_no = c.pallet_no
                                                  left join (select distinct x.region, x.model, x.time_s
                                                              from pptest.oms_tdm x
                                                             where x.tdmtype = 'PGI') f
                                                    on a.region = f.region
                                                   and e.custmodel = f.model
                                                 where trunc(a.shipping_time) = trunc(sysdate)
                                                   and a.status not in ('WS', 'SF')
                                                 group by c.car_no,
                                                          a.shipment_id,
                                                          a.type,
                                                          a.carrier_name,
                                                          a.region,
                                                          a.status) g) m
                                 inner join (select nvl(g.car_no, ' ') as car_no,
                                                    sum(g.palletcount) as palletcountall,
                                                    min(g.time_s) as time_s
                                               from (select c.car_no,
                                                            a.shipment_id,
                                                            count(distinct b.pallet_no) as palletcount,
                                                            a.status,
                                                            sum(case
                                                                  when b.pick_status = 'IP' or
                                                                       b.pick_status = 'FP' then
                                                                   1
                                                                  else
                                                                   0
                                                                end) as pickcount,
                                                            sum(case
                                                                  when nvl(b.weight, '0') = '0' then
                                                                   1
                                                                  else
                                                                   0
                                                                end) as weightcount,
                                                            min(f.time_s) as time_s
                                                       from ppsuser.t_shipment_info a
                                                      inner join ppsuser.t_shipment_pallet b
                                                         on a.shipment_id = b.shipment_id
                                                      inner join ppsuser.t_shipment_pallet_part d
                                                         on b.pallet_no = d.pallet_no
                                                      inner join pptest.oms_partmapping e
                                                         on d.ictpn = e.part
                                                       left join pptest.oms_load_car c
                                                         on a.shipment_id = c.shipment_id
                                                        and b.pallet_no = c.pallet_no
                                                       left join (select distinct x.region,
                                                                                 x.model,
                                                                                 x.time_s
                                                                   from pptest.oms_tdm x
                                                                  where x.tdmtype = 'PGI') f
                                                         on a.region = f.region
                                                        and e.custmodel = f.model
                                                      where trunc(a.shipping_time) = trunc(sysdate)
                                                        and a.status not in ('WS', 'SF')
                                                      group by c.car_no, a.shipment_id, a.status) g
                                              group by g.car_no) n
                                    on m.car_no = n.car_no
                                 order by m.car_no,
                                          m.shipment_id,
                                          m.type,
                                          m.carrier_name,
                                          m.region,
                                          m.uploadflag asc


                            ");
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


        public DataSet GetLocationCheckLogBySQL(string locationNo)
        {
            string sql = string.Empty;
            sql = string.Format(@"select daycode        日期,
                                       checktime      检查次数,
                                       pallet_no      栈板号,
                                       cartonqty      箱数,
                                       passcartonqty  匹配箱数,
                                       errorcartonqty error箱数,
                                       result         结果
                                  from ppsuser.t_location_check
                                 where daycode = to_char(sysdate, 'yyyy-mm-dd')
                                   and location_no = '{0}'
                                 order by checktime desc
                                ", locationNo);

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

        public DataSet ChangeCSNtoCartonBySQL(string strSN)
        {
            string strSQL = string.Format("select customer_sn ,carton_no ,wc "
                                         + "    from ppsuser.t_sn_status "
                                         + "   where customer_sn = '{0}' ", strSN);


            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(strSQL);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }


        public DataSet GetLocationSnInfoBySQL(string locationNo)
        {
            string sql = string.Empty;
            sql = string.Format(@"select distinct a.pallet_no, a.carton_no, a.part_no, a.location_no
                                  from ppsuser.t_sn_status a
                                  join ppsuser.t_location b
                                    on a.location_id = b.location_id
                                 where a.wc = 'W0'
                                   and a.location_no = '{0}'
                                ", locationNo);

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

        public string WMSStockCheckBySP(string strLoctionId, string strSn, string strIsFirst, string strEmpNo, out string errmsg)
        {
            //ppsuser.sp_wms_stockcheck(inlocationid in varchar2,
            //                                          insn         in varchar2,
            //                                          inisfirst    in varchar2,
            //                                          inempid      in varchar2,
            //                                          errmsg       out varchar2)
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationid", strLoctionId };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSn };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inisfirst", strIsFirst };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempid", strEmpNo };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("ppsuser.sp_wms_stockcheck", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;

        }
        public string WMSStockCheckBySP2(string strLoctionId, string strSn, string strSn2,string strQTY, string strIsFirst, string strEmpNo, out string errmsg)
        {
            
            object[][] procParams = new object[7][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationid", strLoctionId };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strSn };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn2", strSn2 };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inqty", strQTY };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inisfirst", strIsFirst };
            procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inempid", strEmpNo };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("ppsuser.sp_wms_stockcheck2", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;

        }



        public DataSet GetSnInfoBySQL(string strSN)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                                select distinct pallet_no, carton_no, part_no, location_no ,location_id
                                  from ppsuser.t_sn_status a
                                 where wc = 'W0'
                                   and carton_no ='{0}'
                                union
                                select distinct pallet_no, carton_no, part_no, location_no ,location_id
                                  from ppsuser.t_sn_status a
                                 where wc = 'W0'
                                   and pallet_no ='{0}'
                                ", strSN);

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

        public DataSet GetCarLineLocationBySQL(string strCarLineNo)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                            select min(c.location_no) location_no
                              from ppsuser.t_location c
                             where c.location_id in
                                   (select b.location_id
                                      from ppsuser.t_location_trolley b
                                     where b.trolley_no in
                                           (select a.trolley_no
                                              from ppsuser.t_trolley_line_info a
                                             where a.trolley_line_no = '{0}'))
                                ", strCarLineNo);

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

        public DataSet GetSamePartLocationBySQL(string strLocation)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                              select b.location_no, b.pallet_no, b.part_no, b.qty, b.cartonqty
                                  from ppsuser.t_location b
                                 where b.part_no in (select a.part_no
                                                       from ppsuser.t_location a
                                                      where a.location_no = '{0}')
                                   and b.location_no not in ('{0}')
                                ", strLocation);

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


        public DataSet CheckPalletCartonBySQL(string strPallet,string strCarton)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                                 select distinct a.carton_no , a.pallet_no
                                 from ppsuser.t_sn_status a
                                where a.pallet_no ='{0}'
                                  and a.carton_no ='{1}'
                                  and a.wc='W0'
                                ", strPallet, strCarton);

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
