using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShipmentAC
{
    public class ShipmentDal
    {
        /// <summary>
        /// 获取shipmentId的称重栈板数量
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns></returns>
        public int GetWeightNumByShipmnetStat(string shipmentId)
        {
            string sql = @"SELECT *
                             FROM NONEDIPPS.g_ds_weight_t 
                            WHERE shipment_id = :shipmentId";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentId", shipmentId };
            int rowCount = ClientUtils.RowCount(sql, parameterArray);
            return rowCount;
        }

        /// <summary>
        /// 获取shiment单的所有车牌列表  //HYQ:其实没有什么用
        /// </summary>
        /// <returns></returns>
        public DataSet GetCarList(string startDate,string endDate)
        {
            string sql = @"select distinct 
                                case 
                                  when truck_no='' then 'notruck'||shipment_id
                                    when truck_no is null then 'notruck'||shipment_id
                                else truck_no||shipment_id
                                end    CAR_NO  
                              FROM NONEDIPPS.t_shipment_pallet  
                              WHERE to_date(cdt) >= to_date( :startDate ,'YYYY-MM-DD')
                              AND to_date(cdt) <= to_date( :endDate, 'YYYY-MM-DD')";
            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "startDate", startDate };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endDate", endDate };

            DataSet dataSet=new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
            }
            catch (Exception e)
            {
                MessageBox.Show("获取卡车信息失败"+e.ToString());
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
               // MessageBox.Show("有值");

                return dataSet;
            }
            else { return null; }

                //return dataSet;
        }

        /// <summary>
        /// 获取时间段、车牌号的shipment信息
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="carNo">车牌号</param>
        /// <returns></returns>
        /// //+" and to_date(a.cdt) >=  to_date( '{0}' ,'YYYY-MM-DD') "
        ///        //+ " and to_date(a.cdt) <=  to_date( '{1}','YYYY-MM-DD') "
        public DataSet GetShipmentInfoDataTable( string carNo)
        {
            string[] truckShipment = carNo.Split('-');
            string truckno=truckShipment[0];
            string shipmentid= truckShipment[1];
            if (truckno.Equals("notrack")) { truckno = ""; }

            string sql = string.Empty;
               
            if (string.IsNullOrEmpty(truckno.Trim()))
            {
                sql =string.Format("SELECT rownum, t.*  FROM ( "
                                    + " select car_no,a.carrier_name,a.shipment_id,d.shipping_time, "
                                    + " a.qty,a.pallet_no,a.shipment_flag,b.wc,count(seria_number) as sncount "
                                    + " from NONEDIPPS.t_shipment_pallet a  "
                                    + " left join NONEDIPPS.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W4' "
                                    + "  join NONEDIOMS.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 0 and (c.active = 0  or  c.active is null) "
                                    + "  join NONEDIPPS.t_shipment_info d on a.shipment_id = d.shipment_id  "
                                    + " where a.shipment_flag is  null  "
                                    + " and a.shipment_id='{0}' "
                                    + " group by car_no,a.carrier_name,a.shipment_id,d.shipping_time,a.qty,a.pallet_no,a.shipment_flag,b.wc "
                                    + " order by shipment_flag desc ,pallet_no asc "
                                    + "  ) t" , shipmentid);

                DataSet dataSet = new DataSet();
                try
                {
                    dataSet = ClientUtils.ExecuteSQL(sql);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }

                return dataSet;
            }
            else
            {
                sql = string.Format("SELECT rownum, t.*  FROM ( "
                                    + " select car_no,a.carrier_name,a.shipment_id,d.shipping_time, "
                                    + " a.pallet_no,a.qty,a.shipment_flag,b.wc,count(serial_number) as sncount "
                                    + " from NONEDIPPS.t_shipment_pallet a  "
                                    + " left join NONEDIPPS.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W4' "
                                    + "  join NONEDIOMS.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 0 and (c.active = 0  or  c.active is null) and car_no='{1}' "
                                    + "  join NONEDIPPS.t_shipment_info d on a.shipment_id = d.shipment_id  "
                                    + " where a.shipment_flag is  null  "
                                    + " and a.shipment_id='{0}' "
                                    //+ " and car_no='{1}' "
                                    + " group by car_no,a.carrier_name,a.shipment_id,d.shipping_time,a.pallet_no,a.qty,a.shipment_flag,b.wc "
                                    + " order by shipment_flag desc ,a.pallet_no asc "
                                    + "  ) t", shipmentid,truckno);
       
                DataSet dataSet = new DataSet();
                try
                {
                    dataSet = ClientUtils.ExecuteSQL(sql);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }

                return dataSet;
            } 
        }

        /// <summary>
        /// 获取时间段、车牌号的shipment信息
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="carNo">车牌号</param>
        /// <returns></returns>
        public DataSet GetShipmentAlearyInfoDataTable( string carNo)
        {
            string[] truckShipment = carNo.Split('-');
            string truckno = truckShipment[0];
            string shipmentid = truckShipment[1];
            if (truckno.Equals("notrack")) { truckno = string.Empty; }

            string sql = string.Empty;
            if (string.IsNullOrEmpty(truckno))
            {
                sql = string.Format("SELECT rownum, t.*  FROM ( "
                                    + " select car_no,a.carrier_name,a.shipment_id,'' wharf_loc, "
                                    + " a.real_pallet_no,a.qty,a.pallet_no,a.shipment_flag,b.wc,count(serial_number) as sncount "
                                    + " from NONEDIPPS.t_shipment_pallet a  "
                                    + " left join NONEDIPPS.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W5' "
                                    + "  join NONEDIOMS.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 1 and (c.active = 0  or  c.active is null) "

                                    + " where a.shipment_flag is not null  "
                                    + " and a.shipment_id='{0}' "
                                    + " group by car_no,a.carrier_name,a.shipment_id,a.real_pallet_no,a.qty,a.pallet_no,a.shipment_flag,b.wc "
                                    + " order by shipment_flag desc ,real_pallet_no asc "
                                    + "  ) t", shipmentid);

                DataSet dataSet = new DataSet();
                try
                {
                    dataSet = ClientUtils.ExecuteSQL(sql);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }
                 
                return dataSet;
            }
            else
            {


                
                sql = string.Format("SELECT rownum, t.*  FROM ( "
                                   + " select car_no,a.carrier_name,a.shipment_id,'' wharf_loc, "
                                   + " a.real_pallet_no,a.qty,a.pallet_no,a.shipment_flag,b.wc,count(customer_sn) as sncount "
                                   + " from NONEDIPPS.t_shipment_pallet a  "
                                   + " left join NONEDIPPS.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W5' "
                                   + "  join NONEDIOMS.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 1 and (c.active = 0  or  c.active is null) "

                                   + " where a.shipment_flag is not null  "
                                   + " and a.shipment_id='{0}' "
                                   + " and car_no='{1}' "
                                   + " group by car_no,a.carrier_name,a.shipment_id,a.real_pallet_no,a.qty,a.pallet_no,a.shipment_flag,b.wc "
                                   + " order by shipment_flag desc ,real_pallet_no asc "
                                   + "  ) t", shipmentid, truckno);
             
                DataSet dataSet = new DataSet();
                try
                {
                    dataSet = ClientUtils.ExecuteSQL(sql);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }

                return dataSet;
            }
        }

        /// <summary>
        /// 判断pallet是否已经扫描
        /// </summary>
        /// <param name="palletNo">栈板号</param>
        /// <returns>true:已经扫描, false:未扫描</returns>
        public bool IsExistPalletFlagScan(string palletNo)
        {
            string sql = @"SELECT * 
                           FROM NONEDIPPS.t_shipment_pallet a
                           WHERE a.shipment_flag is not null
                                 AND  (real_pallet_no = :palletNo or pallet_no = :palletNo)";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
            int count=0;
            try
            {
                count = ClientUtils.RowCount(sql, parameterArray);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      


        /// <summary>
        /// 更新pallet表标记为已经扫
        /// </summary>
        /// <param name="palletNo">栈板号</param>
        public void UpdatePalletFlagandWC(string palletNo)
        {
            try
            {
                //update flag
                string sql = @"UPDATE NONEDIPPS.t_shipment_pallet a
                               SET a.truck_no =
                                   (select b.car_no
                                      from NONEDIOMS.oms_load_car b
                                     where a.shipment_id = b.shipment_id
                                       and a.pallet_no = b.pallet_no),
                                   a.shipment_flag = '1'
                             where  a.pallet_no =:palletNo  or  a.real_pallet_no=:palletno";
                object[][] parameterArray = new object[2][];
                parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
                parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
                ClientUtils.ExecuteSQL(sql, parameterArray);

                //update wc 
                string stringupdatewc = @"update NONEDIPPS.t_sn_status set wc ='W5',udt=sysdate where pack_pallet_no in (select pallet_no  from NONEDIPPS.t_shipment_pallet where pallet_no =:palletNo  or  real_pallet_no=:palletno )";
                object[][] Param = new object[2][];
                Param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
                Param[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };

                ClientUtils.ExecuteSQL(stringupdatewc.ToString(), Param);

                //update oms_load_car
                string stringupdateoms = @"update NONEDIOMS.oms_load_car
                                       set isload = 1, loadtime = sysdate
                                     where pallet_no in (select pallet_no
                                                           from NONEDIPPS.t_shipment_pallet
                                                          where pallet_no = :palletNo
                                                             or real_pallet_no = :palletno)";
                object[][] Param2 = new object[2][];
                Param2[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
                Param2[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };

                ClientUtils.ExecuteSQL(stringupdateoms.ToString(), Param);
            }
            catch (Exception e)
            {
                MessageBox.Show("更新数据异常" + e.ToString());
            }


        }

        /// <summary>
        /// 获取shipment未扫描的栈板数
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns>未扫描栈板数</returns>
        public int GetShipmentRemainNum(string shipmentId)
        {
            string sql = @"SELECT *  FROM NONEDIPPS.t_shipment_pallet a WHERE a.shipment_flag IS NULL AND a.shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentId };
            return ClientUtils.RowCount(sql, parameterArray);
        }

        public int GetCarRemainNum(string strPalletNo)
        {
            string sql = @"SELECT a.pallet_no
                            FROM NONEDIOMS.oms_load_car a 
                            WHERE (a.car_no,TRUNC(a.expectedtime)) IN
                            (
                            SELECT DISTINCT b.car_no,TRUNC(c.SHIPPING_TIME) 
                            FROM NONEDIOMS.oms_load_car b INNER JOIN NONEDIPPS.T_SHIPMENT_INFO c ON b.SHIPMENT_ID=c.SHIPMENT_ID
                            INNER JOIN NONEDIPPS.T_SHIPMENT_PALLET c ON b.SHIPMENT_ID=c.SHIPMENT_ID
                            WHERE c.SHIPMENT_ID=:palletno OR c.PALLET_NO=:palletno OR c.REAL_PALLET_NO=:palletno
                            )
                            AND nvl(a.isload,0)<>1";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletno", strPalletNo };
            return ClientUtils.RowCount(sql, parameterArray);
        }

        /// <summary>
        /// 更新DDline状态为Handover
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateDDLineStatus(string shipmentId)
        {
            string sql = @"UPDATE NONEDIPPS.g_ds_shipment_ddline_t SET status = 'Handover'
                            WHERE shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentId };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }

        /// <summary>
        /// 更新shipment状态为LF loadcar finish
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateShipmentStatus(string shipmentId)
        {
            string sql = @"UPDATE NONEDIPPS.t_shipment_info SET status = 'LF'
                            WHERE shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentId };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }

        /// <summary>
        /// 更新DNline状态为Handover
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateDNLineStatus(string shipmentId)
        {
            string sql = @"UPDATE NONEDIPPS.g_ds_shipment_dnline_t SET status = 'Handover'
                            WHERE shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentId };
            ClientUtils.ExecuteSQL(sql, parameterArray);
        }

        /// <summary>
        /// 更新WeihtList标识
        /// </summary>
        /// <param name="shipmentId"></param>
        public void UpdateWeightListFlag(string shipmentId)
        {
            string sql = @"UPDATE NONEDIPPS.g_ds_weightlist_t SET Flag = '1'
                            WHERE shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentId };
            ClientUtils.ExecuteSQL(sql, parameterArray);
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


        public DataSet GetCarInfoDataTablebySQL(string starttime, string endtime, string truckno)
        {
            string sql = string.Empty;
            sql = string.Format("select a.shipment_id 集货单, "
                         + "          d.shipping_time 出货时间, "
                         + "          b.pallet_no 栈板号, "
                         + "          e.ictpn 料号, "
                         + "          e.carton_qty 箱数, "
                         + "          e.qty 数量, "
                         + "          b.car_no 车牌号, "
                         + "          decode(b.isload, 1, 'Y', 'N') 装车状态, "
                         + "          c.whconfirm_name 仓库确认人, "
                         + "          c.whconfirm_time 仓库确认时间, "
                         + "          c.securityconfirm_name 安保确认人, "
                         + "          c.securityconfirm_time 安保确认时间 "
                         + "     FROM NONEDIPPS.t_shipment_pallet a "
                         + "     join NONEDIOMS.oms_load_car b "
                         + "       on a.shipment_id = b.shipment_id "
                         + "      and a.pallet_no = b.pallet_no "
                         + "      and(b.active = 0 or b.active is null) "
                         + "     join NONEDIPPS.t_shipment_pallet_part e "
                         + "      on a.pallet_no = e.pallet_no "
                         + "     join NONEDIPPS.t_shipment_info d  "
                         + "       on a.shipment_id = d.shipment_id "
                         + "     left join NONEDIPPS.t_truck_confirm_log c "
                         + "       on a.shipment_id = c.shipment_id "
                         + "      and a.pallet_no = c.pallet_no "
                         + "      and c.car_no = b.car_no "
                         + "    where (to_date(a.cdt) >= to_date('{0}', 'YYYY-MM-DD')  "
                         + "      and  to_date(a.cdt) <= to_date('{1}', 'YYYY-MM-DD')) "
                         + "      and b.car_no = '{2}' "
                         + "      order by  d.shipping_time  desc ,b.pallet_no  asc ,e.ictpn  asc ", starttime,  endtime,  truckno);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;
        }

        public string getPalletsQtyByTruckByProcedure(string starttime, string endtime, string truckno,out string allreturnlist,out string errmsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "starttime", starttime };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endtime", endtime };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "truckno", truckno };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "allreturnlist", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_SHIPMENT_GETTRUCKINFO", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            allreturnlist = ds.Tables[0].Rows[0]["allreturnlist"].ToString();
            //return errmsg;
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public string TruckConfirmByProcedure(string starttime, string endtime, string truckno, string whoconfirm, string passw, out string errmsg)
        {
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "starttime", starttime };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endtime", endtime };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "truckno", truckno };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "whoconfirm", whoconfirm };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "passw", passw };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_SHIPMENT_TRUCKCONFIRM", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            //return errmsg;
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        public string UpdatePalletFlagandWCBySP(string palletNo, out string errmsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", palletNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("nonedipps.sp_npisid_updatepalletstatus", procParams);
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

        public string changeSNtoShipmentIDBySQL(string changeSNtoShipmentID, out string errmsg)
        {

            string sql = string.Format(@"Select distinct a.shipment_id 
                                        from NONEDIPPS.t_shipment_pallet a  
                                        join NONEDIPPS.t_shipment_info b 
                                         on a.shipment_id =b.shipment_id
                                        where b.status in('LF','UF')
                                    and  (a.pallet_no='{0}' or a.real_pallet_no='{0}' or a.shipment_id='{0}') "
                                    , changeSNtoShipmentID);

            DataTable dt_change = new DataTable();
            try
            {
                dt_change = ClientUtils.ExecuteSQL(sql).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                errmsg = "NG";
                return "";
            }


            if (dt_change.Rows.Count > 0)
            {
                //如果输入的时real_pallet_no 或者时print_pallet_no 
                //转换位pallet_no 来处理
                changeSNtoShipmentID = dt_change.Rows[0]["shipment_id"].ToString();
                errmsg = "";
                return changeSNtoShipmentID;
            }
            else
            {
                errmsg = "NG";
                return "";
            }

        }

        public bool IsCarALLOverBySQL(string strShipment)
        {
            DataTable dtTemp = ClientUtils.ExecuteSQL(string.Format(@"
                SELECT count(*) as PACOUNT
                        FROM NONEDIOMS.oms_load_car a
                        WHERE(a.car_no, TRUNC(a.expectedtime)) IN
                        (
                        SELECT DISTINCT b.car_no, TRUNC(c.SHIPPING_TIME)
                        FROM NONEDIOMS.oms_load_car b INNER JOIN NONEDIPPS.T_SHIPMENT_INFO c ON b.SHIPMENT_ID = c.SHIPMENT_ID
                        INNER JOIN NONEDIPPS.T_SHIPMENT_PALLET c ON b.SHIPMENT_ID = c.SHIPMENT_ID
                        WHERE c.SHIPMENT_ID = '{0}' OR c.PALLET_NO = '{0}' 
                        )
                        AND nvl(a.isload, 0) <> 1
                ", strShipment)).Tables[0];
            if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
            {
                return false;
            }
            if (dtTemp.Rows[0]["PACOUNT"].ToString().Trim() == "0")
            {
                return true;
            }
            return false;
        }


        public DataSet GetSIDPalletWeightInfoBySQL(string strSID,string strCarNo)
        {
            string sql = string.Empty;
            sql = string.Format(@"select a.shipment_id,
                                   a.hawb,
                                   (select min(c.shiptocountry)
                                      from nonedipps.t_fd_order_detail c
                                      join nonedipps.t_order_info d
                                        on c.ac_po = d.delivery_no
                                       and c.ac_po_line = d.line_item
                                     where d.shipment_id = a.shipment_id) as shiptocountry,
                                   a.carrier_name,
                                   b.pallet_no,
                                   b.weight,
                                   olc.car_no,
                                   (select sum(to_number(tsp.weight))
                                      from nonedipps.t_shipment_pallet tsp
                                     where tsp.shipment_id = a.shipment_id) totalweight
                              from nonedipps.t_shipment_info a
                              join nonedipps.t_shipment_pallet b
                                on a.shipment_id = b.shipment_id
                              join nonedioms.oms_load_car olc
                                on b.shipment_id = olc.shipment_id
                               and b.pallet_no = olc.pallet_no
                             where a.shipment_id = '{0}'
                               and olc.car_no ='{1}'
                             order by b.pallet_no asc", strSID,strCarNo);

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
        public DataSet GetACandEDISameCarNoBySQL(string carno)
        {
            string[] truckshipment = carno.Split('-');
            string truckno = truckshipment[0];
            string shipmentid = truckshipment[1];
            if (truckno.Equals("notrack")) { truckno = ""; }

            // if (carno.Equals("notrack")) { carno = ""; }

            string sql = string.Empty;

            if (string.IsNullOrEmpty(truckno.Trim()))
            {
                return null;
            }
            else
            {
                sql = string.Format(@"select distinct a.car_no
                                  from pptest.oms_load_car a
                                 where (a.car_no, trunc(a.expectedtime)) in
                                       (select b.car_no, trunc(b.expectedtime)
                                          from nonedioms.oms_load_car b
                                         where b.car_no = '{0}'and b.shipment_id = '{1}')", truckno, shipmentid);

                DataSet dataSet = new DataSet();
                try
                {
                    dataSet = ClientUtils.ExecuteSQL(sql);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }

                return dataSet;
            }
        }

    }
}
