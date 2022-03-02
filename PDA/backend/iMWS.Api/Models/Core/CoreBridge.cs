using iMWS.Api.Models.Bean;
using iMWS.Api.Models.DataGateWay;
using iMWS.Api.Models.DBTool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace iMWS.Api.Models.Core
{
    public class corebridge
    {
        ExecutionResult exeRes;
        datagetway dgw;
        baseinfo bean;
        public corebridge()
        {
            dgw = new datagetway();
            bean = new baseinfo();
        }

        //internal ExecutionResult ExecuteTrolleyCheckIn(baseinfo bean, DataTable dataSource)
        //{
        //    exeRes = new ExecutionResult();
        //    try
        //    {
        //        //exeRes = dgw.GetDBConnAddr();//获取配置DB连线
        //        //if ((DataSet)exeRes.Anything == null || ((DataSet)exeRes.Anything).Tables[0].Rows.Count < 1)
        //        //{
        //        //    exeRes.Status = false;
        //        //    exeRes.Message = "Get DB Connect Address Error. pls call pps team to check Table:c_configsetting";
        //        //    return exeRes;
        //        //}
        //        //获取判断新栈板号/沿用旧栈板号
        //        bean.Pallet_No = GetPalletNo(bean);
        //        //创建连线
        //        //DBTransaction dbTransaction = new DBTransaction(((DataSet)exeRes.Anything).Tables[0].Rows[0][0].ToString());
        //        //dbTransaction.BeginTransaction();//开启事务
        //        //  try
        //        //   {
        //        foreach (DataRow dr in dataSource.Rows)//遍历datatable
        //        {
        //            bean.Trolley_no = dr["TrolleyNo"].ToString();
        //            bean.Trolley_Line_No = dr["TrolleyLineNo"].ToString();
        //            bean.Point_No = dr["Check_Index"].ToString();
        //            bean.Customer_SN = dr["Customer_SN"].ToString();
        //            bean.KeyPart = dr["KeyPartNo"].ToString();
        //            bean.OriginPallet_no = dr["OriginPalletNo"].ToString();
        //            //存在风险，如果中途 报错则报错SN不会执行成功
        //            DataSet dataSet = dgw.ExcuteProcMPartTrolley(bean);
        //            if (dataSet.Tables[0].Rows[0]["vReturnMsg"].Equals("OK"))
        //            {
        //                exeRes.Message = "OK";
        //                exeRes.Status = true;
        //            }
        //            else
        //            {
        //                exeRes.Status = false;
        //                exeRes.Message += dataSet.Tables[0].Rows[0]["vReturnMsg"].ToString();
        //            }
        //            ////写入暂存记录表 ppsuser.T_TROLLEY_MPART_SN
        //            //exeRes = dgw.InsertMPartTrolley(bean, dbTransaction);
        //            ////更新库存表中数量ppsuser.t_location single qty-1
        //            //if (exeRes.Status)
        //            //    exeRes = dgw.UpdateLocation(bean, dbTransaction);
        //            ////更新SN旧栈板号 ppsuser.t_sn_status 
        //            //if (exeRes.Status)
        //            //    exeRes = dgw.UpdateSnPallet(bean, dbTransaction);
        //            //if (!exeRes.Status)
        //            //    break;
        //        }
        //        //if (exeRes.Status)
        //        //    dbTransaction.Commit();//提交事务
        //        //else
        //        //    dbTransaction.Rollback();//回滚事务
        //        // }
        //        //    catch (Exception ex)
        //        //    {
        //        //    dbTransaction.Rollback();//回滚事务
        //        //      exeRes.Status = false;
        //        //    exeRes.Message = ex.Message;
        //        //   }
        //        //   finally
        //        //  {
        //        //     dbTransaction.EndTransaction();//结束事务
        //        //  }
        //    }
        //    catch (Exception e)
        //    {
        //        exeRes.Status = false;
        //        exeRes.Message = e.Message;
        //    }
        //    return exeRes;
        //}

        private string GetPalletNo(baseinfo bean)
        {
            string strPallet = "";
            var list = dgw.GetPalletByTrolley(bean.Trolley_no);
            strPallet = list.Count > 0 ? list.FirstOrDefault() : dgw.GetNewPallet();
            return strPallet;
        }
    }
}