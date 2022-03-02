using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPIPickListAC
{
    class NPIPickListACDAL
    {

        public DataSet GetNPISIDDataTableBySQL(string strSID, string strSTATUS, string strStime, string strEtime, string strPlant, string strWarehouse)
        {
            string sql = string.Empty;
            object[][] sqlparams = new object[0][];
            int iPara = 0;
            sql = @"select a.shipment_id,
                           a.shipping_time,
                           a.hawb,
                           a.qty,
                           a.carton_qty,
                           a.status,
                           a.close_time,
                           a.container as computer_name,
                           a.plant,
                           a.sloc
                      from nonedipps.t_shipment_info a
                         where 1=1 ";
            //SAP出货单号查询条件
            if (strSID != "" && strSID != "ALL")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sql += " and a.shipment_id = :strSID";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strSID", strSID };
                iPara = iPara + 1;
            }


            if (strSTATUS != "" && strSTATUS != "ALL")
            {

                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sql += " and a.status = :status";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "status", strSTATUS };
                iPara = iPara + 1;
            }
            if (strPlant != "" && strPlant != "ALL")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sql += " and a.plant = :plant";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "plant", strPlant };
                iPara = iPara + 1;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sql += " and a.sloc = :sloc";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "sloc", strWarehouse };
                iPara = iPara + 1;
            }
            //开始日期
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.shipping_time >= to_date(:starttime ,'yyyy-mm-dd')";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "starttime", strStime };
            iPara = iPara + 1;

            //结束日期
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.shipping_time <= to_date(:endtime ,'yyyy-mm-dd')";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "endtime", strEtime };
            iPara = iPara + 1;

            sql += " order by a.shipping_time asc";


            DataSet dataSet = new DataSet();
            try
            {
                //dataSet = ClientUtils.ExecuteSQL(sql);
                dataSet = ClientUtils.ExecuteSQL(sql, sqlparams);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
        public string NPICheckSIDBySP(string strSID,  string localHostname, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "computername", localHostname };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("nonedipps.SP_NPIPICK_CHECKSIDSTATUS", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                return "NG";
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else if (RetMsg.Contains("WA"))
            {
                return "WA";
            }
            else
            {
                return "NG";
            }

        }

        public DataSet GetSIDLINEINFOBySQL(string strSID)
        {
            string sql = string.Empty;


            sql = string.Format(@"select a.shipment_id,
                                         a.delivery_no,
                                         a.line_item,
                                         a.mpn,
                                         a.ictpn,
                                         a.status,
                                         a.qty,
                                         a.carton_qty,
                                         a.pack_qty,
                                         a.pack_carton_qty,
                                         b.plant,
                                         b.sloc
                                    from nonedipps.t_order_info a
                                    inner join nonedipps.t_shipment_info b on a.shipment_id=b.shipment_id
                                    where a.shipment_id='{0}'
                                    order by a.ictpn asc, a.delivery_no asc ,a.line_item asc", strSID);

            
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

        public DataSet GetSIDSNINFOBySQL(string strSID)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                                select a.*,
                                       b.part_no,
                                       decode(b.shipment_id,
                                              null,
                                              '未作业',
                                              a.shipmentid,
                                              '已经刷入',
                                              '异常') status
                                  from nonedioms.oms_shipment_sn a
                                  join nonedipps.t_sn_status b
                                    on a.sn = b.customer_sn
                                 where a.shipmentid = '{0}'", strSID);
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

        public DataSet GetSIDHOLDSNINFOBySQL(string strSID)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                                select distinct a.customer_sn
                                  from ppsuser.g_sn_status a
                                  join nonedipps.t_sn_status b
                                    on to_char(a.customer_sn) = to_char(b.customer_sn)
                                   and a.hold_flag='Y'
                                 where b.shipment_id = '{0}'", strSID);
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
            sql = string.Format("select distinct customer_sn ,carton_no "
                                          + "    from nonedipps.t_sn_status "
                                          + "   where customer_sn = '{0}' "
                                          + "      or carton_no = '{1}'", inputSno, inputSno);

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

        public string NPIPICKInsertCartonBySP(string strPickNOA, out string strPickNO, string strSID, string strCarton, string strUserNo, out string RetMsg, out string strLBL)
        {
            object[][] procParams = new object[7][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpickno", strPickNOA };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "pickno", "" };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "snOrCartonno", strCarton };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "empno", strUserNo };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "strlbl", "" };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("nonedipps.sp_npipick_insertcarton", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                strLBL = "00/00";
                strPickNO = "";
                return "NG";
            }
            RetMsg = dt.Rows[0]["errmsg"].ToString();
            strLBL = dt.Rows[0]["strlbl"].ToString();
            strPickNO = dt.Rows[0]["pickno"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else if (RetMsg.StartsWith("FINISH"))
            {
                return "FINISH";
            }
            else
            {
                return "NG";
            }

        }

        public string NPIPICKUnlockComputerBySP(string strSID, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("NONEDIPPS.SP_NPIPICK_UNLOCKCOMPUTERNAME", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
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

        public DataSet GetStockInfoBySQL(string strPartNo, string strPlant, string strSloc)
        {
            string sql = string.Empty;
            //sql = string.Format(@"Select b.part_no 料号,
            //                       b.location_no 库位,
            //                       '' 车行号,
            //                       b.CARTONQTY - b.QHCARTONQTY 箱数
            //                  from nonedipps.t_location b
            //                 where b.qty > 0
            //                   and b.
            //                 cartonqty > 0
            //                   and (b.part_no = '{0}' or
            //                       b.part_no in
            //                       (select distinct a.part_no
            //                           from ppsuser.t_sn_status a
            //                          where a.customer_sn = '{0}'
            //                             or a.carton_no = '{0}'))
            //                 order by b.Udt asc
            //        ", strPartNo);
            sql = string.Format(@"Select b.part_no 料号,
                                   b.location_no 库位,
                                   '' 车行号,
                                   b.CARTONQTY - b.QHCARTONQTY 箱数,b.udt
                              from nonedipps.t_location b
                              inner join nonedipps.wms_location c
                              on b.location_no=c.location_no
                              inner join nonedipps.wms_warehouse d
                              on c.warehouse_id=d.warehouse_id
                             where b.qty > 0
                               and b.
                             cartonqty > 0
                               and b.part_no = '{0}' 
                               and d.plant='{1}'
                               and d.sap_wh_no='{2}'
                             union
                             Select b.part_no 料号,
                                   b.location_no 库位,
                                   '' 车行号,
                                   b.CARTONQTY - b.QHCARTONQTY 箱数,b.udt
                              from ppsuser.t_location b
                              inner join ppsuser.wms_location c
                              on b.location_no=c.location_no
                              inner join ppsuser.wms_warehouse d
                              on c.warehouse_id=d.warehouse_id
                             where b.qty > 0
                               and b.
                             cartonqty > 0
                               and b.part_no = '{0}' 
                               and d.plant='{1}'
                               and d.sap_wh_no='{2}'
                             order by udt asc
                    ", strPartNo, strPlant, strSloc);

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

        public string PPSCheckWebServieByProcedure(string insn, out string tturl, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "tturl", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_UPLOAD_CHECKWEBSERVICELOG", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            tturl = ds.Tables[0].Rows[0]["tturl"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        public string NPIPPSCheckWebServieByProcedure(string insn, out string tturl, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "tturl", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_UPLOAD_CHECKACWEBSERVICELOG", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            tturl = ds.Tables[0].Rows[0]["tturl"].ToString();
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

        public string PPSInsertWebServieByProcedure(string insn, string serverip, string url, string result, out string errmsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strserverip", serverip };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strurl", url };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strresult", result };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("nonedipps.SP_UPLOAD_INSERTACWEBLOG", procParams);
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

        public DataSet GetPalletPrintInfoBySQL(string strPartNo)
        {
            string sql = string.Empty;
            #region  20200604bk
            //sql = string.Format(@"  select b.shipment_id shipmentid,
            //                                b.hawb hawb,
            //                                b.region region,
            //                                d.empty_carton  empty_carton,
            //                                d.weight  weight,
            //                                a.pick_pallet_no palletno,
            //                                a.pallet_number palletseq,
            //                                count(distinct c.customer_sn) snqty,
            //                                count(distinct c.carton_no) cartonqty
            //                           from nonedipps.t_pallet_pick a
            //                           join nonedipps.t_shipment_info b
            //                             on a.pallet_no = b.shipment_id
            //                           join nonedipps.t_sn_status c
            //                             on a.pick_pallet_no = c.pick_pallet_no
            //                      left join nonedipps.t_shipment_pallet d
            //                             on b.shipment_id =d.shipment_id 
            //                            and d.pallet_no = a.pick_pallet_no
            //                          where a.pick_pallet_no = '{0}'
            //                          group by b.shipment_id,
            //                                   b.hawb,
            //                                   b.region,
            //                                   d.empty_carton,
            //                                   d.weight,
            //                                   a.pick_pallet_no,
            //                                   a.pallet_number
            //                                            ", strPartNo);
            #endregion
            #region 20200604new
            sql = string.Format(@"  select b.shipment_id shipmentid,
                                            b.hawb hawb,
                                            (select min(shiptocountry) from nonedipps.t_fd_order_detail  tfod where tfod.freightorder = b.shipment_id  ) region,
                                            d.empty_carton  empty_carton,
                                            d.weight  weight,
                                            a.pick_pallet_no palletno,
                                            a.pallet_number palletseq,
                                            count(distinct c.customer_sn) snqty,
                                            count(distinct c.carton_no) cartonqty
                                       from nonedipps.t_pallet_pick a
                                       join nonedipps.t_shipment_info b
                                         on a.pallet_no = b.shipment_id
                                       join nonedipps.t_sn_status c
                                         on a.pick_pallet_no = c.pick_pallet_no
                                  left join nonedipps.t_shipment_pallet d
                                         on b.shipment_id =d.shipment_id 
                                        and d.pallet_no = a.pick_pallet_no
                                      where a.pick_pallet_no = '{0}'
                                      group by b.shipment_id,
                                               b.hawb,
                                               b.region,
                                               d.empty_carton,
                                               d.weight,
                                               a.pick_pallet_no,
                                               a.pallet_number
                                                        ", strPartNo);




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


        public DataSet GetPalletWeightINFOBySQL()
        {
            string sql = string.Empty;
            sql = string.Format(@"select a.shipment_id,
                                           b.pallet_no,
                                           b.weight,
                                           b.real_pallet_no palletvolume ,
                                           b.qty,
                                           b.carton_qty,
                                           b.empty_carton,
                                           c.delivery_no,
                                           c.line_item,
                                           c.pack_qty,
                                           c.pack_carton
                                      from nonedipps.t_shipment_info a
                                      join nonedipps.t_shipment_pallet b
                                        on a.shipment_id = b.shipment_id
                                      join nonedipps.t_pallet_order c
                                        on b.pallet_no = c.pallet_no
                                     where a.shiptype = 'NPI'
                                       and a.status in ('FP','UF')
                                       and b.cdt > trunc(sysdate - 10)
                                     order by b.cdt desc ,b.pallet_no asc");
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


     
        public string NPIUpdatePalletWeightBySP(string strPalletNo, string strPalletSize, string strPalletHeight, string strPalletWeight, out string errmsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletsize", strPalletSize };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletheight", strPalletHeight };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletweight", strPalletWeight };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("nonedipps.SP_NPI_UPDATEPALLETWEIGHT", procParams);
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

        public string NPIUpdatePalletWeight2BySP(string strPalletNo, string strPalletSize, string strPalletHeight, string strPalletWeight, string strPalletenmptycarton, out string errmsg)
        {
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletno", strPalletNo };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletsize", strPalletSize };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletheight", strPalletHeight };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletweight", strPalletWeight };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpalletemptycarton", strPalletenmptycarton };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("nonedipps.SP_NPI_UPDATEPALLETWEIGHT2", procParams);
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
        public string NPIPPSAddSIDPalletByProcedure(string insn, out string errmsg)
        {
            //nonedipps.sp_npi_insertomspallet(insn, errmsg)
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("nonedipps.sp_npi_insertomspallet", procParams);
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

        public DataSet GetSIDStatusInfoBySQL(string strSID)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 select a.shipment_id,
                                       a.shipping_time,
                                       a.hawb,
                                       a.qty,
                                       a.carton_qty,
                                       a.status,
                                       a.close_time,
                                       a.container as computer_name
                                  from nonedipps.t_shipment_info a
                                     where a.shipment_id = '{0}'
                                     ", strSID);

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
        public DataSet GetSNInfoDataTableDAL2(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select distinct customer_sn, a.product_name
                                      from ppsuser.mes_pallet_info a
                                      join ppsuser.mes_sn_status b
                                        on a.in_guid = b.in_guid
                                       and a.pallet_no = b.pallet_no
                                     where b.customer_sn in (select distinct customer_sn
                                                               from nonedipps.t_sn_status
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


        public DataSet GetPickPalletCartonCountBySQL(string strPickPalletno)
        {
            string sql = string.Empty;
            sql = string.Format(@"select  count(distinct  carton_no)  cartoncount 
                                         from nonedipps.t_sn_status 
                                        where pick_pallet_no  = '{0}'", strPickPalletno);

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




        public DataSet GetSIDNOCartonListBySQL(string strSIDNO, string strPICKSIDNO)
        {
            string sql = string.Empty;
            if (strPICKSIDNO.Equals("-ALL-"))
            {
                sql = string.Format(@"
                                 select distinct  carton_no, shipment_id npisid, pick_pallet_no
                                          from nonedipps.t_sn_status a
                                         where a.shipment_id  = '{0}'
                                 order by   shipment_id asc , pick_pallet_no asc ,carton_no asc
                                     ", strSIDNO);

            }
            else
            {
                sql = string.Format(@"
                                 select distinct  carton_no, shipment_id npisid, pick_pallet_no
                                          from nonedipps.t_sn_status a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}'
                                order by   shipment_id asc , pick_pallet_no asc ,carton_no asc
                                     ", strSIDNO, strPICKSIDNO);
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

        public DataSet ChangeCSNtoCartonBySQL(string strSN)
        {
            string strSQL = string.Format(@"
                                    select customer_sn ,carton_no ,wc
                                      from nonedipps.t_sn_status a 
                                     where a.customer_sn = '{0}' 
                                     ", strSN);

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
        public DataSet CheckCartonInSIDNOBySQL(string strSIDNO, string strPICKSIDNO, string strSN)
        {
            string sql = string.Empty;
            if (strPICKSIDNO.Equals("-ALL-"))
            {
                sql = string.Format(@"
                                     select distinct  carton_no, shipment_id npisid, pick_pallet_no
                                          from nonedipps.t_sn_status a
                                         where a.shipment_id  = '{0}'  and a.carton_no ='{1}'
                                     ", strSIDNO, strSN);

            }
            else
            {
                sql = string.Format(@"
                                select distinct  carton_no, shipment_id npisid, pick_pallet_no
                                          from nonedipps.t_sn_status a
                                         where a.shipment_id  = '{0}' and pick_pallet_no  ='{1}' and  a.carton_no ='{2}'
                                     ", strSIDNO, strPICKSIDNO, strSN);

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

        public DataSet GetCartonPartInfoBySQL(string strCartonNo)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 select min(a.part_no ) as part_no
                                  from nonedipps.t_sn_status a
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

    }
}
