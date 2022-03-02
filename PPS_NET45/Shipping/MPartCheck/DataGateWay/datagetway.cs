using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using DBTools;
using DBTools.Connection;
using MPartCheck.Bean;

namespace MPartCheck.DataGateWay
{
    class datagetway
    {
        ExecutionResult exeRes = new ExecutionResult();
        internal ExecutionResult CheckTrolley(string strTrolley)
        {
            string sql = @"select * from ppsuser.t_trolley_line_info where  trolley_no=:trolley";
            object[][] param = new object[1][];
            param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, ":trolley", strTrolley };
            exeRes.Anything = ClientUtils.ExecuteSQL(sql, param);
            return exeRes;
        }

        internal ExecutionResult CheckTrolleyLine(baseinfo bean)
        {
            string sql = @"select * from ppsuser.t_trolley_line_info where trolley_no=:trolley and  trolley_line_no=:trolleyline";
            object[][] param = new object[2][];
            param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, ":trolleyline", bean.Trolley_Line_No };
            param[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, ":trolley", bean.Trolley_no };
            exeRes.Anything = ClientUtils.ExecuteSQL(sql, param);
            return exeRes;
        }

        internal ExecutionResult GetSnInfo(baseinfo bean)
        {
            //v 1.0
            //string sql = @"select  customer_sn,carton_no,part_no,Pallet_no from 
            //                        ppsuser.t_sn_status where customer_sn =:strSN
            //                        union
            //                        select  customer_sn,carton_no,part_no,Pallet_no from
            //                        ppsuser.t_sn_status where carton_no =:strSN";
            //v2.0
            //string sql = @"select  customer_sn,carton_no,part_no,Pallet_no,packqty,wc,B.PARTTYPE
            //                     from ppsuser.t_sn_status a, pptest.oms_partmapping b where a.customer_sn =:strSN
            //                    and a.part_no=b.part(+)
            //                    union
            //                    select customer_sn,carton_no,part_no,Pallet_no,packqty,wc,B.PARTTYPE
            //                     from ppsuser.t_sn_status a ,pptest.oms_partmapping b where a.carton_no =:strSN
            //                    and a.part_no=b.part(+)";
            //v3.0
            string sql = @"select *from (select customer_sn,
                                   carton_no,  part_no, Pallet_no,  packqty,    wc,   B.PARTTYPE
                                 from ppsuser.t_sn_status a, pptest.oms_partmapping b
                                 where a.customer_sn =:strSN   and a.part_no = b.part(+)
                              union
                                   select customer_sn,   carton_no,  part_no,
                                   Pallet_no,  packqty,     wc, B.PARTTYPE
                                 from ppsuser.t_sn_status a, pptest.oms_partmapping b
                                 where a.carton_no =:strSN and a.part_no = b.part(+)) T1,
                                      (select sum(a.total) as total from ( selecT count(CUSTOMER_SN ) as total
                                  from ppsuser.t_sn_status where  CARTON_NO IN (selecT TO_CHAR(CARTON_NO)
                                  from ppsuser.t_sn_status   where customer_sn =:strSN
                                 UNION   SELECT TO_CHAR(:strSN) AS CARTON_NO FROM DUAL)) a) T2";
            object[][] param = new object[1][];
            param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, ":strSN", bean.Customer_SN };
            exeRes.Anything = ClientUtils.ExecuteSQL(sql, param);
            return exeRes;
        }

        internal ExecutionResult CheckTrolleyLineIsUsed(baseinfo bean)
        {
            string sql = @"select*from ppsuser.T_TROLLEY_MPART_SN 
                                    where trolley_no =:strTrolley  and trolley_line_no =:strTrolleyLine";
            object[][] param = new object[2][];
            param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, ":strTrolley", bean.Trolley_no };
            param[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, ":strTrolleyLine", bean.Trolley_Line_No };
            exeRes.Anything = ClientUtils.ExecuteSQL(sql, param);
            return exeRes;
        }

        internal ExecutionResult GetDBConnAddr()
        {
            string sql = @"select value2 from ppsuser.c_configsetting   where function_name='DBAddress' 
                                    and value1='PPSTEST'and enableflag='Y'";
            exeRes.Anything = ClientUtils.ExecuteSQL(sql);
            return exeRes;
        }

        internal DataTable GetPalletByTrolley(string trolley)
        {
            string sql = @"select distinct pallet_no from ppsuser.T_TROLLEY_MPART_SN 
                                    where trolley_no =:strTrolley";
            object[][] param = new object[1][];
            param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, ":strTrolley", trolley };
            return ClientUtils.ExecuteSQL(sql, param).Tables[0];
        }

        internal string GetNewPallet()
        {
            string sql = @"select ppsuser.t_create_palletNo from dual";
            return ClientUtils.ExecuteSQL(sql).Tables[0].Rows[0][0].ToString();
        }

        internal ExecutionResult InsertMPartTrolley(baseinfo bean, DBTransaction dbTransaction)
        {
            string sql = @"insert into ppsuser.t_trolley_mpart_sn 
            (custom_sn,trolley_no,pallet_no,sides_no,level_no,pointno,emp_id,ictpartno,carton_no,trolley_line_no)
            values
            (:strCustSN,:strTrolley,:strPallet,:strSides,:strLevel,:strPoint,:strEmp,:strKeyPart,:strCarton,:strTrolleyLine)";
            DBParameter dbparam = new DBParameter();
            dbparam.Add(":strCustSN", OracleType.VarChar, bean.Customer_SN);
            dbparam.Add(":strTrolley", OracleType.VarChar, bean.Trolley_no);
            dbparam.Add(":strPallet", OracleType.VarChar, bean.Pallet_No);
            dbparam.Add(":strSides", OracleType.VarChar, bean.Sides_No);
            dbparam.Add(":strLevel", OracleType.VarChar, bean.Level_No);
            dbparam.Add(":strPoint", OracleType.VarChar, bean.Point_No);
            dbparam.Add(":strEmp", OracleType.VarChar, bean.Emp_ID);
            dbparam.Add(":strKeyPart", OracleType.VarChar, bean.KeyPart);
            dbparam.Add(":strCarton", OracleType.VarChar, bean.Carton_No);
            dbparam.Add(":strTrolleyLine", OracleType.VarChar, bean.Trolley_Line_No);
            return dbTransaction.ExecuteUpdate(sql, dbparam.GetParameters());
        }

        internal ExecutionResult UpdateLocation(baseinfo bean, DBTransaction dbTransaction)
        {
            string sql = @"update ppsuser.t_location set qty=qty-1,cartonqty=cartonqty-1 where pallet_no=:strPallet";
            DBParameter dbparam = new DBParameter();
            dbparam.Add(":strPallet", OracleType.VarChar, bean.OriginPallet_no);
            return dbTransaction.ExecuteUpdate(sql, dbparam.GetParameters());
        }

        internal ExecutionResult UpdateSnPallet(baseinfo bean, DBTransaction dbTransaction)
        {
            string sql = @"update ppsuser.t_sn_status set pallet_no=:strPallet where customer_sn =:strCustSN";
            DBParameter dbparam = new DBParameter();
            dbparam.Add(":strPallet", OracleType.VarChar, bean.Pallet_No);
            dbparam.Add(":strCustSN", OracleType.VarChar, bean.Customer_SN);
            return dbTransaction.ExecuteUpdate(sql, dbparam.GetParameters());
        }

        internal ExecutionResult CheckSn(string customer_SN)
        {
            string sql = @"select*from ppsuser.T_TROLLEY_MPART_SN  where custom_sn=:strSN";
            object[][] param = new object[1][];
            param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, ":strSN", customer_SN };
            exeRes.Anything = ClientUtils.ExecuteSQL(sql, param);
            return exeRes;
        }
    }
}
