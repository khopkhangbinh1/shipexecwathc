using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIWarehouseTools
{
    class EDIWarehouseToolsDAL
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

        public DataSet GetStockInfoDataTable2(string strlocationno)
        {
            string sql = string.Empty;
            sql = string.Format(@" 
                select a.location_no, a.pallet_no, a.part_no, a.qty, a.cartonqty
                  from ppsuser.t_location a
                 where location_no = '{0}'
                ", strlocationno);
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
        //sp_wmst_checktrolleyinfo(incarno in varchar2,
        //                                             errmsg  out varchar2)
        public string WmstCheckTrolleyInfoBySP(string strTrolleyNo ,out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarno", strTrolleyNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_wmst_checktrolleyinfo", procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        //sp_wmst_pallet2cartrans(incartonno    in varchar2,
        //                                            incarlinenoto in varchar2,
        //                                            errmsg        out varchar2)
        public string WmstPalletToCarTransBySP(string strCartonNo,string strCarLineNo, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incartonno", strCartonNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlinenoto", strCarLineNo };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_wmst_pallet2cartrans", procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

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

        public string WmstCarToPalletTransBySP(string strCartonNo, string strLocationNoTo, out string RetMsg)
        {
            //ppsuser.sp_wmst_car2pallettrans(insn      in varchar2,
            //                                                inlocnoto in varchar2,
            //                                                errmsg    out varchar2) as
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strCartonNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocnoto", strLocationNoTo };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_wmst_car2pallettrans", procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public DataSet GetPalletListBySQL(string strStartTime, string strEndTime)
        {
            string sql = string.Format(@"
                                      select a.shipment_id,
                                       b.pallet_no,
                                       c.car_no,
                                       c.isload,
                                       a.carrier_name  carrier,
                                       a.shipment_type sidtype,
                                       a.type,
                                       a.region
                                  from ppsuser.t_shipment_info a
                                  join ppsuser.t_shipment_pallet b
                                    on a.shipment_id = b.shipment_id
                                  left join pptest.oms_load_car c
                                    on b.pallet_no = c.pallet_no
                                 where a.shipping_time >= to_date('{0}', 'yyyy-mm-dd')
                                   and a.shipping_time < to_date('{1}', 'yyyy-mm-dd')
                                   and a.status not in ('WS','IN','SF','HO','CP')
                                 order by c.car_no asc, a.shipment_id asc, b.pallet_no asc
                                                        ", strStartTime, strEndTime);
            // and a.shipping_time < to_date('{1}', 'yyyy-mm-dd')
            DataSet dataSet = new DataSet();
            try
            {
                //
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }


        public DataSet GetDockLoactionListBySQL()
        {
            string sql = @"select a.location_no,
                           a.type,
                           a.priority,
                           a.palletcount,
                           a.region,
                           a.carriers,
                           a.enabled
                      from ppsuser.t_dock_location_info a
                    ";

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
        public DataSet GetDockLoactionInfoBySQL(string strLocationNo)
        {
            string sql = string.Format(@"select a.location_no, a.pallet_no
                              from ppsuser.t_dock_location_info a
                             where a.enabled = 'Y'
                               and a.location_no = '{0}'
                    ", strLocationNo);

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

        public DataSet GetPalletDockLoactionBySQL(string strPalletNO)
        {
            string sql = string.Format(@"select a.location_no, a.pallet_no
                              from ppsuser.t_dock_location_info a
                             where a.enabled = 'Y'
                               and a.pallet_no = '{0}'
                    ", strPalletNO);

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

        public DataSet GetDockLoactionInfoBySQL2(string strLocationNo,string strPalletNo)
        {
            string sql = string.Format(@"select a.location_no, a.pallet_no
                              from ppsuser.t_dock_location_info a
                             where a.enabled = 'Y'
                               and a.location_no = '{0}'
                               and a.pallet_no = '{1}'
                    ", strLocationNo, strPalletNo);

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

        public DataSet GetAssignLocationListBySQL(string strCARNO, string strSID)
        {
            string sql = string.Empty;
            if (strCARNO.Equals("-ALL-") && strSID.Equals("-ALL-"))
            {
                sql = @"
                         select a.shipment_id,
                                 b.pallet_no,
                                 d.location_no,
                                 c.car_no,
                                 c.isload,
                                 a.carrier_name  carrier,
                                 a.shipment_type sidtype,
                                 a.type,
                                 a.region
                            from ppsuser.t_shipment_info a
                            join ppsuser.t_shipment_pallet b
                              on a.shipment_id = b.shipment_id
                            left join pptest.oms_load_car c
                              on b.pallet_no = c.pallet_no
                            left join (select*from ppsuser.t_dock_location_info where ENABLED ='Y') d
                              on b.pallet_no = d.pallet_no
                           where a.shipping_time >= trunc(sysdate)
                             
                             and a.status not in ('WS', 'IN', 'SF', 'HO', 'CP')
                             and (b.shipment_flag is null)
                             and (b.weight is not null)
                           order by c.car_no asc, a.shipment_id asc, b.pallet_no asc
                        ";
            }
            else if (strCARNO.Equals("-ALL-") && !strSID.Equals("-ALL-")) 
            {
                sql = string.Format(@"
                         select a.shipment_id,
                                 b.pallet_no,
                                 d.location_no,
                                 c.car_no,
                                 c.isload,
                                 a.carrier_name  carrier,
                                 a.shipment_type sidtype,
                                 a.type,
                                 a.region
                            from ppsuser.t_shipment_info a
                            join ppsuser.t_shipment_pallet b
                              on a.shipment_id = b.shipment_id
                            left join pptest.oms_load_car c
                              on b.pallet_no = c.pallet_no
                            left join (select*from ppsuser.t_dock_location_info where ENABLED ='Y') d
                              on b.pallet_no = d.pallet_no
                           where a.shipping_time >= trunc(sysdate)
                             and a.shipment_id ='{0}'
                             and a.status not in ('WS', 'IN', 'SF', 'HO', 'CP')
                             and (b.shipment_flag is null)
                             and (b.weight is not null)
                           order by c.car_no asc, a.shipment_id asc, b.pallet_no asc
                        ", strSID);
            }
            else if (!strCARNO.Equals("-ALL-") && strSID.Equals("-ALL-")) 
            {
                sql = string.Format(@"
                         select a.shipment_id,
                                 b.pallet_no,
                                 d.location_no,
                                 c.car_no,
                                 c.isload,
                                 a.carrier_name  carrier,
                                 a.shipment_type sidtype,
                                 a.type,
                                 a.region
                            from ppsuser.t_shipment_info a
                            join ppsuser.t_shipment_pallet b
                              on a.shipment_id = b.shipment_id
                            join pptest.oms_load_car c
                              on b.pallet_no = c.pallet_no
                            left join (select*from ppsuser.t_dock_location_info where ENABLED ='Y') d
                              on b.pallet_no = d.pallet_no
                           where a.shipping_time >= trunc(sysdate)
                             and c.car_no ='{0}'
                             and a.status not in ('WS', 'IN', 'SF', 'HO', 'CP')
                             and (b.shipment_flag is null)
                             and (b.weight is not null)
                           order by c.car_no asc, a.shipment_id asc, b.pallet_no asc
                        ", strCARNO);
            }
            else 
            {
                sql = string.Format(@"
                         select a.shipment_id,
                                 b.pallet_no,
                                 d.location_no,
                                 c.car_no,
                                 c.isload,
                                 a.carrier_name  carrier,
                                 a.shipment_type sidtype,
                                 a.type,
                                 a.region
                            from ppsuser.t_shipment_info a
                            join ppsuser.t_shipment_pallet b
                              on a.shipment_id = b.shipment_id
                            join pptest.oms_load_car c
                              on b.pallet_no = c.pallet_no
                            left join (select*from ppsuser.t_dock_location_info where ENABLED ='Y') d
                              on b.pallet_no = d.pallet_no
                           where a.shipping_time >= trunc(sysdate)
                             and c.car_no ='{0}'
                             and a.shipment_id ='{1}'
                             and a.status not in ('WS', 'IN', 'SF', 'HO', 'CP')
                             and (b.shipment_flag is null)
                             and (b.weight is not null)
                           order by c.car_no asc, a.shipment_id asc, b.pallet_no asc
                        ", strCARNO,strSID);
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
        public DataSet GetAssignLocationListBySQL2()
        {
            string sql = @"
                        select bb.shipment_id,
                               bb.pallet_no,
                               aa.location_no,
                               bb.car_no,
                               bb.isload,
                               bb.carrier,
                               bb.sidtype,
                               bb.type,
                               bb.region
                          from ppsuser.t_dock_location_info aa
                          left join (select a.shipment_id,
                                       b.pallet_no,
                                       c.car_no,
                                       c.isload,
                                       a.carrier_name  carrier,
                                       a.shipment_type sidtype,
                                       a.type,
                                       a.region
                                  from ppsuser.t_shipment_info a
                                  join ppsuser.t_shipment_pallet b
                                    on a.shipment_id = b.shipment_id
                                  left join pptest.oms_load_car c
                                    on b.pallet_no = c.pallet_no
                                 where b.pallet_no in (select pallet_no from ppsuser.t_dock_location_info )
                                   and a.status not in ('WS', 'IN', 'SF', 'HO', 'CP')
                                   and (b.shipment_flag is null)
                                   and (b.weight is not null)) bb
                            on aa.pallet_no = bb.pallet_no
                        where  aa.ENABLED ='Y'
                         order by bb.car_no asc,aa.location_no asc, bb.shipment_id asc, bb.pallet_no asc
                        ";

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
        public DataSet GetAssignLocationListBySQL3()
        {
            string sql = @"
                        select bb.shipment_id,
                               bb.pallet_no,
                               aa.location_no,
                               bb.car_no,
                               bb.isload,
                               bb.carrier,
                               bb.sidtype,
                               bb.type,
                               bb.region
                          from ppsuser.t_dock_location_info aa
                           join (select a.shipment_id,
                                       b.pallet_no,
                                       c.car_no,
                                       c.isload,
                                       a.carrier_name  carrier,
                                       a.shipment_type sidtype,
                                       a.type,
                                       a.region
                                  from ppsuser.t_shipment_info a
                                  join ppsuser.t_shipment_pallet b
                                    on a.shipment_id = b.shipment_id
                                  left join pptest.oms_load_car c
                                    on b.pallet_no = c.pallet_no
                                 where  a.status not in ('WS', 'IN', 'SF', 'HO', 'CP')
                                   and (b.shipment_flag is null)
                                   and (b.weight is not null)) bb
                            on aa.pallet_no = bb.pallet_no 
                        where   aa.ENABLED ='Y'
                         order by bb.car_no asc,aa.location_no asc, bb.shipment_id asc, bb.pallet_no asc
                        ";

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

        public string UpdateDockLocationPalletBySP(string strLocation, string strPalletNO, string strNewPalletNO,  out string RetMsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocation", strLocation };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incurrpalletno", strPalletNO };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "innewpalletno", strNewPalletNO };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_WMST_UPDATEDOCKLOCATION", procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
    }
}
