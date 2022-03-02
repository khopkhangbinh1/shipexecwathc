using iMWS.Api.Models.Bean;
using iMWS.Api.Models.DBTool;
using iMWS.Api.Models.PPSUSER;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using UniDb;
using Dapper;
using System.Data.SqlClient;

namespace iMWS.Api.Models.DataGateWay
{
    public class datagetway
    {
        private IDb _idb;
        public datagetway()
        {
            _idb = new OracleDb(ConfigurationManager.AppSettings["PPSUser"]);
        }

        ExecutionResult exeRes = new ExecutionResult();
        public List<T_TROLLEY_LINE_INFO> CheckTrolley(string strTrolley)
        {
            string sql = @"select * from ppsuser.t_trolley_line_info where  trolley_no= :trolley";
            return _idb.DbCon.Query<T_TROLLEY_LINE_INFO>(sql, new { trolley = strTrolley }).ToList();
        }

        public List<T_TROLLEY_LINE_INFO> CheckTrolleyLine(baseinfo bean)
        {
            string sql = @"select * from ppsuser.t_trolley_line_info where trolley_no=:trolley and  trolley_line_no=:trolleyline";
            return _idb.DbCon.Query<T_TROLLEY_LINE_INFO>(sql, new { trolley = bean.Trolley_no, trolleyline = bean.Trolley_Line_No }).ToList();
        }

        public List<T_LOCATION> CheckTrolleyUsed(string trolley_no)
        {
            string sql = @"
select *
from ppsuser.t_location
where pallet_no in (
		select pallet_no
		from ppsuser.t_trolley_sn_status
		where trolley_no = :trolley
		
		union
		
		select pallet_no
		from ppsuser.t_trolley_mpart_sn
		where trolley_no = :trolley
		)
";
            return this._idb.DbCon.Query<T_LOCATION>(sql, new { trolley = trolley_no }).ToList();
        }

        public List<SnInfoData> GetSnInfo(baseinfo bean)
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
            string sql = @"
SELECT *
FROM (
	SELECT customer_sn
		,carton_no
		,part_no
		,Pallet_no
		,packqty
		,wc
		,B.PARTTYPE
	FROM ppsuser.t_sn_status a
		,pptest.oms_partmapping b
	WHERE a.customer_sn = :strSN
		AND a.part_no = b.part(+)
	
	UNION
	
	SELECT customer_sn
		,carton_no
		,part_no
		,Pallet_no
		,packqty
		,wc
		,B.PARTTYPE
	FROM ppsuser.t_sn_status a
		,pptest.oms_partmapping b
	WHERE a.carton_no = :strSN
		AND a.part_no = b.part(+)
	) T1
	,(
		SELECT sum(a.total) AS total
		FROM (
			SELECT count(CUSTOMER_SN) AS total
			FROM ppsuser.t_sn_status
			WHERE CARTON_NO IN (
					SELECT TO_CHAR(CARTON_NO)
					FROM ppsuser.t_sn_status
					WHERE customer_sn = :strSN
					
					UNION
					
					SELECT TO_CHAR(:strSN) AS CARTON_NO
					FROM DUAL
					)
			) a
		) T2
";

            return this._idb.DbCon.Query<SnInfoData>(sql, new { strSN = bean.Customer_SN }).ToList();
        }

        public List<T_TROLLEY_MPART_SN> CheckTrolleyLineIsUsed(baseinfo bean)
        {
            string sql = @"select * from ppsuser.T_TROLLEY_MPART_SN 
                                    where trolley_no =:trolley  and trolley_line_no =:trolleyLine";

            return this._idb.DbCon.Query<T_TROLLEY_MPART_SN>(sql, new { trolley = bean.Trolley_no, trolleyLine = bean.Trolley_Line_No }).ToList();
        }

        internal ExecutionResult GetDBConnAddr()
        {
            string sql = @"select value2 from ppsuser.c_configsetting   where function_name='DBAddress' 
                                    and value1='PPSTEST'and enableflag='Y'";
            exeRes.Anything = ClientUtils.ExecuteSQL(sql);
            return exeRes;
        }

        public List<string> GetPalletByTrolley(string trolley)
        {
            string sql = @"select distinct pallet_no from ppsuser.T_TROLLEY_MPART_SN 
                                    where trolley_no =:trolley";

            return this._idb.DbCon.Query<string>(sql, new { trolley = trolley }).ToList();
        }

        public string GetNewPallet()
        {
            string sql = @"select ppsuser.t_create_palletNo from dual";

            return this._idb.DbCon.Query<string>(sql).FirstOrDefault();
        }

        internal ExecutionResult InsertMPartTrolley(baseinfo bean, /*DBTransaction*/ dynamic dbTransaction)
        {
            string sql = @"insert into ppsuser.t_trolley_mpart_sn 
            (custom_sn,trolley_no,pallet_no,sides_no,level_no,pointno,emp_id,ictpartno,carton_no,trolley_line_no)
            values
            (:strCustSN,:strTrolley,:strPallet,:strSides,:strLevel,:strPoint,:strEmp,:strKeyPart,:strCarton,:strTrolleyLine)";
            dynamic dbparam = null;/* new DBParameter();*/
            dbparam.Add(":strCustSN", OracleType.VarChar, bean.Customer_SN);
            dbparam.Add(":strTrolley", OracleType.VarChar, bean.Trolley_no);
            dbparam.Add(":strPallet", OracleType.VarChar, bean.Pallet_No);
            dbparam.Add(":strSides", OracleType.VarChar, bean.Sides_No);
            dbparam.Add(":strLevel", OracleType.VarChar, bean.Level_No);
            dbparam.Add(":strPoint", OracleType.VarChar, bean.Point_No);
            dbparam.Add(":strEmp", OracleType.VarChar, bean.Emp_ID);
            dbparam.Add(":strKeyPart", OracleType.VarChar, bean.KeyPart);
            dbparam.Add(":strCarton", OracleType.VarChar, bean.Customer_SN);
            //custom sn = carton no edit by franky 19-11-27
            dbparam.Add(":strTrolleyLine", OracleType.VarChar, bean.Trolley_Line_No);
            return dbTransaction.ExecuteUpdate(sql, dbparam.GetParameters());
        }

        public List<StatusData> GetStatusByTrolley(string trolley_no)
        {
            string sql = @"select A.TROLLEY_LINE_NO, a.MaxQty, nvl(b.usedqty, 0) as UsedQty
                                      from (selecT distinct trolley_line_no, maxqty
                                              from t_trolley_line_info
                                             where trolley_no = :trolley) A,
                                           (select trolley_no || '-' || sides_no || lpad(level_no, 2, 0) as trolley_line_no,
                                                   count(custom_sn) as usedqty
                                              from t_trolley_mpart_sn
                                             where trolley_no = :trolley
                                             group by trolley_no || '-' || sides_no || lpad(level_no, 2, 0)) B
                                     where A.TROLLEY_LINE_NO = B.trolley_line_no(+)
                                     order by A.Trolley_Line_No
                                    ";

            return this._idb.DbCon.Query<StatusData>(sql, new { trolley = trolley_no }).ToList();
        }

        internal ExecutionResult UpdateLocation(baseinfo bean, /*DBTransaction*/ dynamic dbTransaction)
        {
            string sql = @"update ppsuser.t_location set qty=qty-1,cartonqty=cartonqty-1 where pallet_no=:strPallet";
            dynamic dbparam = null;/* new DBParameter();*/
            dbparam.Add(":strPallet", OracleType.VarChar, bean.OriginPallet_no);
            return dbTransaction.ExecuteUpdate(sql, dbparam.GetParameters());
        }

        public string ExcuteProcMPartTrolley(baseinfo bean)
        {
            //object[][] param = new object[8][];
            //param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vCustomer_SN", bean.Customer_SN };
            //param[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vNewPallet", bean.Pallet_No };
            //param[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vTrolleyLine", bean.Trolley_Line_No };
            //param[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vPoint", bean.Point_No };
            //param[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vKeyPart", bean.KeyPart };
            //param[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vEmp", bean.Emp_ID };
            //param[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "vOriginPallet", bean.OriginPallet_no };
            //param[7] = new object[] { ParameterDirection.Output, OracleType.VarChar, "vReturnMsg", "" };
            //string procedurename = @"ppsuser.SP_MPartCheckIN";
            //DataSet ds = ClientUtils.ExecuteProc(procedurename, param);
            //return ds;

            // dapper 預存改寫
            var p = new DynamicParameters();
            p.Add("vCustomer_SN", bean.Customer_SN, DbType.String, ParameterDirection.Input);
            p.Add("vNewPallet", bean.Pallet_No, DbType.String, ParameterDirection.Input);
            p.Add("vTrolleyLine", bean.Trolley_Line_No, DbType.String, ParameterDirection.Input);
            p.Add("vPoint", bean.Point_No, DbType.String, ParameterDirection.Input);
            p.Add("vKeyPart", bean.KeyPart, DbType.String, ParameterDirection.Input);
            p.Add("vEmp", bean.Emp_ID, DbType.String, ParameterDirection.Input);
            p.Add("vOriginPallet", bean.OriginPallet_no, DbType.String, ParameterDirection.Input);
            p.Add("vReturnMsg", bean.OriginPallet_no, DbType.String, ParameterDirection.Output);
            
            this._idb.DbCon.Execute("ppsuser.SP_MPartCheckIN", p, null, null, CommandType.StoredProcedure);

            return p.Get<string>("vReturnMsg");
        }

        public string getEosUrl()
        {
            //string EosUrl = ClientUtils.ExecuteSQL("selecT  value2 from c_configsetting  where function_name ='EOSURL' and Value1='WCF'").Tables[0].Rows[0]["value2"].ToString();

            string sql = @"selecT  value2 from c_configsetting  where function_name ='EOSURL' and Value1='WCF'";
            return this._idb.DbCon.Query<string>(sql).FirstOrDefault();
        }

        public List<StatusData> GetStatusByTrolley(string trolley_no, string sides)
        {
            string sql = @"select A.TROLLEY_LINE_NO, a.MaxQty, nvl(b.usedqty, 0) as UsedQty
                                      from (selecT distinct trolley_line_no, maxqty
                                              from t_trolley_line_info
                                             where trolley_no = :trolley
                                                 and  sides_no=:trolleyLine) A,
                                           (select trolley_no || '-' || sides_no || lpad(level_no, 2, 0) as trolley_line_no,
                                                   count(custom_sn) as usedqty
                                              from t_trolley_mpart_sn
                                             where trolley_no = :trolley
                                             group by trolley_no || '-' || sides_no || lpad(level_no, 2, 0)) B
                                     where A.TROLLEY_LINE_NO = B.trolley_line_no(+)
                                     order by A.Trolley_Line_No
                                    ";

            return this._idb.DbCon.Query<StatusData>(sql, new { trolley = trolley_no, trolleyLine = sides }).ToList();
        }

        internal ExecutionResult UpdateSnPallet(baseinfo bean, /*DBTransaction*/ dynamic dbTransaction)
        {
            string sql = @"update ppsuser.t_sn_status set pallet_no=:strPallet, 
                                    location_id='10000005',
                                    location_no='SYS'
                                    where customer_sn =:strCustSN";
            dynamic dbparam = null;/* new DBParameter();*/
            dbparam.Add(":strPallet", OracleType.VarChar, bean.Pallet_No);
            dbparam.Add(":strCustSN", OracleType.VarChar, bean.Customer_SN);
            return dbTransaction.ExecuteUpdate(sql, dbparam.GetParameters());
        }

        public List<T_TROLLEY_MPART_SN> CheckSn(string customer_SN)
        {
            string sql = @"select*from ppsuser.T_TROLLEY_MPART_SN  where custom_sn=:strSN";
            return this._idb.DbCon.Query<T_TROLLEY_MPART_SN>(sql, new { strSN = customer_SN }).ToList();
        }
    }
}