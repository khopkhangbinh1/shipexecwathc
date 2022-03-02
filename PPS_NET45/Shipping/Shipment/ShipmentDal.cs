﻿using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment
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
                             FROM ppsuser.g_ds_weight_t 
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
                              FROM ppsuser.t_shipment_pallet  
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
                                    + " from ppsuser.t_shipment_pallet a  "
                                    + " left join ppsuser.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W4' "
                                    + "  join pptest.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 0 and (c.active = 0  or  c.active is null) "
                                    + "  join ppsuser.t_shipment_info d on a.shipment_id = d.shipment_id  "
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
                                    + " from ppsuser.t_shipment_pallet a  "
                                    + " left join ppsuser.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W4' "
                                    + "  join pptest.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 0 and (c.active = 0  or  c.active is null) and car_no='{1}' "
                                    + "  join ppsuser.t_shipment_info d on a.shipment_id = d.shipment_id  "
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
        public DataSet GetShipmentInfoDataTable(string carno, string shipmentid)
        {
            //string[] truckShipment = carNo.Split('-');
            //string truckno=truckShipment[0];
            //string shipmentid= truckShipment[1];
            //if (truckno.Equals("notrack")) { truckno = ""; }

            if (carno.Equals("notrack")) { carno = ""; }

            string sql = string.Empty;

            if (string.IsNullOrEmpty(carno.Trim()))
            {
                sql = string.Format(@"select rownum, t.*
                          from(select car_no,
                                       a.carrier_name,
                                       a.shipment_id,
                                       d.shipping_time,
                                       a.qty,
                                       a.pallet_no,
                                       a.shipment_flag,
                                       b.wc,
                                       count(serial_number) as sncount
                                  from ppsuser.t_shipment_pallet a
                                  left join ppsuser.t_sn_status b
                                    on a.pallet_no = b.pack_pallet_no
                                   and b.wc = 'W4'
                                  join pptest.oms_load_car c
                                    on a.shipment_id = c.shipment_id
                                   and a.pallet_no = c.pallet_no
                                   and isload = 0
                                   and(c.active = 0 or c.active is null)
                                  join ppsuser.t_shipment_info d
                                    on a.shipment_id = d.shipment_id
                                 where a.shipment_flag is null
                                   and a.shipment_id = '{0}'
                                 group by car_no,
                                          a.carrier_name,
                                          a.shipment_id,
                                          d.shipping_time,
                                          a.qty,
                                          a.pallet_no,
                                          a.shipment_flag,
                                          b.wc
                                 order by shipment_flag desc, pallet_no asc) t", shipmentid);
               
            }
            else
            {
                sql = string.Format("SELECT rownum, t.*  FROM ( "
                                    + " select car_no,a.carrier_name,a.shipment_id,d.shipping_time, "
                                    + " a.pallet_no,a.qty,a.shipment_flag,b.wc,count(serial_number) as sncount "
                                    + " from ppsuser.t_shipment_pallet a  "
                                    + " left join ppsuser.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W4' "
                                    + "  join pptest.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 0 and (c.active = 0  or  c.active is null) and car_no='{1}' "
                                    + "  join ppsuser.t_shipment_info d on a.shipment_id = d.shipment_id  "
                                    + " where a.shipment_flag is  null  "
                                    + " and a.shipment_id='{0}' "
                                    //+ " and car_no='{1}' "
                                    + " group by car_no,a.carrier_name,a.shipment_id,d.shipping_time,a.pallet_no,a.qty,a.shipment_flag,b.wc "
                                    + " order by shipment_flag desc ,a.pallet_no asc "
                                    + "  ) t", shipmentid, carno);

                if (shipmentid.Equals("ALL")) 
                {
                    sql = string.Format(@"SELECT rownum, t.*
                                          FROM (select car_no,
                                                       a.carrier_name,
                                                       a.shipment_id,
                                                       d.shipping_time,
                                                       a.pallet_no,
                                                       a.qty,
                                                       a.shipment_flag,
                                                       b.wc,
                                                       count(serial_number) as sncount
                                                  from ppsuser.t_shipment_pallet a
                                                  left join ppsuser.t_sn_status b
                                                    on a.pallet_no = b. pack_pallet_no
                                                   and b.wc = 'W4'
                                                  join pptest.oms_load_car c
                                                    on a.shipment_id = c.shipment_id
                                                   and a.pallet_no = c.pallet_no
                                                   and isload = 0
                                                   and (c.active = 0 or c.active is null)
                                                   and car_no = '{0}'
                                                  join ppsuser.t_shipment_info d
                                                    on a.shipment_id = d.shipment_id
                                                 where a.shipment_flag is null
                                                   and c.expectedtime > trunc(sysdate)
                                                   and c.expectedtime < trunc(sysdate+1)
                                                 group by car_no,
                                                          a.carrier_name,
                                                          a.shipment_id,
                                                          d.shipping_time,
                                                          a.pallet_no,
                                                          a.qty,
                                                          a.shipment_flag,
                                                          b.wc
                                                 order by shipment_flag desc, a.pallet_no asc) t", carno);
                }
            }

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
                                    + " from ppsuser.t_shipment_pallet a  "
                                    + " left join ppsuser.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W5' "
                                    + "  join pptest.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 1 and (c.active = 0  or  c.active is null) "

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
                                   + " from ppsuser.t_shipment_pallet a  "
                                   + " left join ppsuser.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W5' "
                                   + "  join pptest.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 1 and (c.active = 0  or  c.active is null) "

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
        public DataSet GetShipmentAlearyInfoDataTable(string carNo, string shipmentid)
        {
            //string[] truckShipment = carNo.Split('-');
            //string truckno = truckShipment[0];
            //string shipmentid = truckShipment[1];
            if (carNo.Equals("notrack")) { carNo = string.Empty; }

            string sql = string.Empty;
            if (string.IsNullOrEmpty(carNo))
            {
                sql = string.Format("SELECT rownum, t.*  FROM ( "
                                    + " select car_no,a.carrier_name,a.shipment_id,'' wharf_loc, "
                                    + " a.real_pallet_no,a.qty,a.pallet_no,a.shipment_flag,b.wc,count(serial_number) as sncount "
                                    + " from ppsuser.t_shipment_pallet a  "
                                    + " left join ppsuser.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W5' "
                                    + "  join pptest.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 1 and (c.active = 0  or  c.active is null) "

                                    + " where a.shipment_flag is not null  "
                                    + " and a.shipment_id='{0}' "
                                    + " group by car_no,a.carrier_name,a.shipment_id,a.real_pallet_no,a.qty,a.pallet_no,a.shipment_flag,b.wc "
                                    + " order by shipment_flag desc ,real_pallet_no asc "
                                    + "  ) t", shipmentid);

            }
            else
            {

                sql = string.Format("SELECT rownum, t.*  FROM ( "
                                   + " select car_no,a.carrier_name,a.shipment_id,'' wharf_loc, "
                                   + " a.real_pallet_no,a.qty,a.pallet_no,a.shipment_flag,b.wc,count(customer_sn) as sncount "
                                   + " from ppsuser.t_shipment_pallet a  "
                                   + " left join ppsuser.t_sn_status b on a.pallet_no =b. pack_pallet_no and b.wc='W5' "
                                   + "  join pptest.oms_load_car c on a.shipment_id = c.shipment_id and a.pallet_no=c.pallet_no and isload = 1 and (c.active = 0  or  c.active is null) "

                                   + " where a.shipment_flag is not null  "
                                   + " and a.shipment_id='{0}' "
                                   + " and car_no='{1}' "
                                   + " group by car_no,a.carrier_name,a.shipment_id,a.real_pallet_no,a.qty,a.pallet_no,a.shipment_flag,b.wc "
                                   + " order by shipment_flag desc ,real_pallet_no asc "
                                   + "  ) t", shipmentid, carNo);

                if (shipmentid.Equals("ALL")) 
                {
                    sql = string.Format(@"select rownum, t.*
                                          from (select car_no,
                                                       a.carrier_name,
                                                       a.shipment_id,
                                                       '' wharf_loc,
                                                       a.real_pallet_no,
                                                       a.qty,
                                                       a.pallet_no,
                                                       a.shipment_flag,
                                                       b.wc,
                                                       count(serial_number) as sncount
                                                  from ppsuser.t_shipment_pallet a
                                                  left join ppsuser.t_sn_status b
                                                    on a.pallet_no = b. pack_pallet_no
                                                   and b.wc = 'W5'
                                                  join pptest.oms_load_car c
                                                    on a.shipment_id = c.shipment_id
                                                   and a.pallet_no = c.pallet_no
                                                   and isload = 1
                                                   and (c.active = 0 or c.active is null)
                                                 where a.shipment_flag is not null
                                                   and c.expectedtime > trunc(sysdate)
                                                   and c.expectedtime < trunc(sysdate+1)
                                                   and car_no = '{0}'
                                                 group by car_no,
                                                          a.carrier_name,
                                                          a.shipment_id,
                                                          a.real_pallet_no,
                                                          a.qty,
                                                          a.pallet_no,
                                                          a.shipment_flag,
                                                          b.wc
                                                 order by shipment_flag desc, real_pallet_no asc) t", carNo);
                }
            }
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
        /// <summary>
        /// 判断pallet是否已经扫描
        /// </summary>
        /// <param name="palletNo">栈板号</param>
        /// <returns>true:已经扫描, false:未扫描</returns>
        public bool IsExistPalletFlagScan(string palletNo)
        {
            string sql = @"SELECT * 
                           FROM ppsuser.t_shipment_pallet a
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
                string sql = @"UPDATE ppsuser.t_shipment_pallet a
                               SET a.truck_no =
                                   (select b.car_no
                                      from pptest.oms_load_car b
                                     where a.shipment_id = b.shipment_id
                                       and a.pallet_no = b.pallet_no),
                                   a.shipment_flag = '1'
                             where  a.pallet_no =:palletNo  or  a.real_pallet_no=:palletNo";
                object[][] parameterArray = new object[1][];
                parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
               // parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
                ClientUtils.ExecuteSQL(sql, parameterArray);

                //update wc 
                string stringupdatewc = @"update ppsuser.t_sn_status set wc ='W5' where pack_pallet_no in (select pallet_no  from ppsuser.t_shipment_pallet where pallet_no =:palletNo  or  real_pallet_no=:palletNo )";
                object[][] Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
               // Param[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };

                ClientUtils.ExecuteSQL(stringupdatewc.ToString(), Param);

                //update oms_load_car
                string stringupdateoms = @"update pptest.oms_load_car
                                       set isload = 1, loadtime = sysdate
                                     where pallet_no in (select pallet_no
                                                           from ppsuser.t_shipment_pallet
                                                          where pallet_no = :palletNo
                                                             or real_pallet_no = :palletNo)";
                object[][] Param2 = new object[1][];
                Param2[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };
               // Param2[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "palletNo", palletNo };

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
            string sql = @"SELECT *  FROM ppsuser.t_shipment_pallet a WHERE a.shipment_flag IS NULL AND a.shipment_id = :shipmentid";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentId };
            return ClientUtils.RowCount(sql, parameterArray);
        }

        public int GetCarRemainNum(string strPalletNo)
        {
            string sql = @"SELECT a.pallet_no
                            FROM pptest.oms_load_car a 
                            WHERE (a.car_no,TRUNC(a.expectedtime)) IN
                            (
                            SELECT DISTINCT b.car_no,TRUNC(c.SHIPPING_TIME) 
                            FROM pptest.oms_load_car b INNER JOIN ppsuser.T_SHIPMENT_INFO c ON b.SHIPMENT_ID=c.SHIPMENT_ID
                            INNER JOIN ppsuser.T_SHIPMENT_PALLET c ON b.SHIPMENT_ID=c.SHIPMENT_ID
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
            string sql = @"UPDATE ppsuser.g_ds_shipment_ddline_t SET status = 'Handover'
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
            string sql = @"UPDATE ppsuser.t_shipment_info SET status = 'LF'
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
            string sql = @"UPDATE ppsuser.g_ds_shipment_dnline_t SET status = 'Handover'
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
            string sql = @"UPDATE ppsuser.g_ds_weightlist_t SET Flag = '1'
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
                         + "     FROM ppsuser.t_shipment_pallet a "
                         + "     join pptest.oms_load_car b "
                         + "       on a.shipment_id = b.shipment_id "
                         + "      and a.pallet_no = b.pallet_no "
                         + "      and(b.active = 0 or b.active is null) "
                         + "     join ppsuser.t_shipment_pallet_part e "
                         + "      on a.pallet_no = e.pallet_no "
                         + "     join ppsuser.t_shipment_info d  "
                         + "       on a.shipment_id = d.shipment_id "
                         + "     left join ppsuser.t_truck_confirm_log c "
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

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_SHIPMENT_GETTRUCKINFO", procParams);
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

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_SHIPMENT_TRUCKCONFIRM", procParams);
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


        public DataSet GetNoCarPalletCountbySQL(string strcarNo,string strsid)
        {
            string sql = string.Empty;
            sql = string.Format(@" 
                             select count(distinct a.pallet_no) palletcount
                               from ppsuser.t_shipment_pallet a
                              where a.shipment_id = '{0}'
                                and a.pallet_no not in (select b.pallet_no from pptest.oms_load_car b)
                            ", strsid);

            if (strsid.Equals("ALL")) 
            {
                sql = string.Format(@"
                        select count(distinct a.pallet_no) palletcount
                          from ppsuser.t_shipment_pallet a
                         where a.pallet_no not in
                               (select b.pallet_no
                                  from pptest.oms_load_car b
                                 where b.car_no = '{0}'
                                   and b.expectedtime > trunc(sysdate))
                           and a.shipment_id in
                               (select c.pallet_no
                                  from pptest.oms_load_car c
                                 where c.car_no = '{0}'
                                   and c.expectedtime > trunc(sysdate))
                         ", strcarNo);
            }

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
        public string UpdatePalletFlagandWCBySP(string palletNo, out string errmsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", palletNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_shipment_updatepalletstatus", procParams);
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
        public string ExcuteShipmentLoadCarBySP(string strCarNo,string strSID,string strPalletNo,string strLocalMAC, out string strIsneedcarlist, out string strIsneedhmlist, out string strRegion, out string errmsg)
        {
            //ppsuser.sp_shipment_insertpallet(incarno          in varchar2,
            //                                                insid            in varchar2,
            //                                                inpalletno       in varchar2,
            //                                                inlocalmac       in varchar2,
            //                                                outisneedcarlist out varchar2,
            //                                                outisneedhmlist  out varchar2,
            //                                                outregion        out varchar2,
            //                                                errmsg           out varchar2) as

           object[][] procParams = new object[8][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarno", strCarNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNo };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocalmac", strLocalMAC };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outisneedcarlist", "" };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outisneedhmlist", "" };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outregion", "" };
            procParams[7] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_shipment_insertpallet", procParams);
            strIsneedcarlist = ds.Tables[0].Rows[0]["outisneedcarlist"].ToString();
            strIsneedhmlist = ds.Tables[0].Rows[0]["outisneedhmlist"].ToString();
            strRegion = ds.Tables[0].Rows[0]["outregion"].ToString();
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

        public DataSet GetDateCarSIDbySQL(string strdate , string strcarNo)
        {
            string sql = string.Empty;
            sql = string.Format(@" 
                          select distinct car_no,
                a.shipment_id,a.region,
                a.status,
                to_char(a.shipping_time, 'YYYYMMDD') shippingtime
                  from ppsuser.t_shipment_info a
                  join pptest.oms_load_car b
                    on a.shipment_id = b.shipment_id
                 where (to_date(a.shipping_time) >= to_date('{0}', 'YYYY-MM-DD') 
                   and to_date(a.shipping_time) <= to_date('{0}', 'YYYY-MM-DD'))
                   and b.car_no ='{1}'               
                 order by shipment_id asc
                            ", strdate, strcarNo);

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

        public DataSet ChangPalletToSIDbySQL(string strPalletNo)
        {
            string sql = string.Empty;
            sql = string.Format(@" 
                         select a.shipment_id
                          from ppsuser.t_shipment_pallet a
                         where a.pallet_no = '{0}'
                            or a.real_pallet_no = '{0}'
                            ", strPalletNo);

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

        public DataSet GetACandEDISameCarNoBySQL(string carno, string shipmentid)
        {
            //string[] truckShipment = carNo.Split('-');
            //string truckno=truckShipment[0];
            //string shipmentid= truckShipment[1];
            //if (truckno.Equals("notrack")) { truckno = ""; }

            if (carno.Equals("notrack")) { carno = ""; }

            string sql = string.Empty;

            if (string.IsNullOrEmpty(carno.Trim()))
            {
                return null;
            }
            else
            {
                sql = string.Format(@"select distinct a.car_no
                                  from nonedioms.oms_load_car a
                                 where (a.car_no, trunc(a.expectedtime)) in
                                       (select b.car_no, trunc(b.expectedtime)
                                          from pptest.oms_load_car b
                                         where b.car_no = '{0}'and b.shipment_id = '{1}')", carno, shipmentid);

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

        public string changeSNtoShipmentIDBySQL(string changeSNtoShipmentID, out string errmsg)
        {

            string sql = string.Format(@"Select distinct a.shipment_id 
                                        from ppsuser.t_shipment_pallet a  
                                        join ppsuser.t_shipment_info b 
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
            string sql = string.Empty;
            sql = string.Format(@"
                    select count(*) as pacount
                      from pptest.oms_load_car a
                     where (a.car_no, trunc(a.expectedtime)) in
                           (select distinct b.car_no, trunc(c.shipping_time)
                              from pptest.oms_load_car b
                             inner join ppsuser.t_shipment_info c
                                on b.shipment_id = c.shipment_id
                             inner join ppsuser.t_shipment_pallet c
                                on b.shipment_id = c.shipment_id
                             where c.shipment_id = '{0}'
                                or c.pallet_no = '{0}')
                       and nvl(a.isload, 0) <> 1
                    ", strShipment);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return false;
            }

            if (dataSet.Tables[0].Rows[0]["PACOUNT"].ToString().Trim() == "0")
            {
                return true;
            }
            return false;

        }

        public DataSet GetSIDPalletWeightInfoBySQL(string strSID, string strCarNo)
        {
            string sql = string.Empty;
            sql = string.Format(@"select a.shipment_id,
                                   a.hawb,
                                   (select min(c.shiptocountry)
                                      from ppsuser.t_fd_order_detail c
                                      join ppsuser.t_order_info d
                                        on c.ac_po = d.delivery_no
                                       and c.ac_po_line = d.line_item
                                     where d.shipment_id = a.shipment_id) as shiptocountry,
                                   a.carrier_name,
                                   b.pallet_no,
                                   b.weight,
                                   olc.car_no,
                                   (select sum(to_number(tsp.weight))
                                      from ppsuser.t_shipment_pallet tsp
                                     where tsp.shipment_id = a.shipment_id) totalweight
                              from ppsuser.t_shipment_info a
                              join ppsuser.t_shipment_pallet b
                                on a.shipment_id = b.shipment_id
                              join pptest.oms_load_car olc
                                on b.shipment_id = olc.shipment_id
                               and b.pallet_no = olc.pallet_no
                             where a.shipment_id = '{0}'
                               and olc.car_no ='{1}'
                             order by b.pallet_no asc", strSID, strCarNo);

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

        public DataTable GetToDayShipmentByCar(string carNo)
        {       
            string strSql = @" select rownum 序号, a.* from (SELECT t.SORT 序列, t.CAR_NO 车牌号,t.SHIPMENT_ID 集货单 ,t.PALLET_NO 栈板号,
                             case 
                                when t.loadtime is not null and t.ISLOAD='1' then ''
                                else d.location_no 
                             end as 码头储位,
                              t.EXPECTEDTIME,
                            case 
                            when t.loadtime is not null and t.ISLOAD='1' then '已装车'
                            else  '未装车'
                            end  装车状态 
                              FROM PPTEST.OMS_LOAD_CAR t left join ppsuser.t_dock_location_info d on t.pallet_no = d.pallet_no
                             WHERE t.EXPECTEDTIME >= TRUNC (SYSDATE) AND t.CAR_NO = :carNo
                             order by t.SORT， t.PALLET_NO) a";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "carNo", carNo };
            return ClientUtils.ExecuteSQL(strSql, parameterArray).Tables[0];
        }
        public int GetPalletBeforeLoad(string palletNo)
        {
            string qry = @"SELECT COUNT(*) c
                          FROM PPTEST.OMS_LOAD_CAR
                         WHERE(CAR_NO, TRUNC(EXPECTEDTIME)) IN
                              (SELECT CAR_NO, TRUNC(EXPECTEDTIME)
                                 FROM PPTEST.OMS_LOAD_CAR
                                WHERE PALLET_NO = '{0}')
                           AND ISLOAD = 0
                           AND SORT != 0
                           AND SORT< (SELECT SORT
                                         FROM PPTEST.OMS_LOAD_CAR
                                        WHERE PALLET_NO = '{1}')";
            qry = String.Format(qry, palletNo, palletNo);
            var c = ClientUtils.ExecuteSQL(qry).Tables[0].Rows[0][0].ToString();
            return int.Parse(c);
        }
    }
}
