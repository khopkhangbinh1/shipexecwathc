using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBTools;
using DBTools.Connection;
using MPartCheck.Bean;
using MPartCheck.DataGateWay;

namespace MPartCheck.Core
{
    class corebridge
    {
        ExecutionResult exeRes;
        datagetway dgw;
        baseinfo bean;
        public corebridge()
        {
            dgw = new datagetway();
            bean = new baseinfo();
        }
        /// <summary>
        /// 检查金刚车是否存在
        /// </summary>
        /// <param name="strTrolley">金刚车号</param>
        /// <returns>检查状态</returns>
        internal ExecutionResult CheckTrolley(baseinfo bean)
        {
            exeRes = new ExecutionResult();
            try
            {
                exeRes = dgw.CheckTrolley(bean.Trolley_no);
                if ((DataSet)exeRes.Anything == null || ((DataSet)exeRes.Anything).Tables[0].Rows.Count < 1)
                {
                    exeRes.Status = false;
                    exeRes.Message = "Check Trolley No Error ! pls check.";
                    return exeRes;
                }
                exeRes.Status = true;
                exeRes.Message = "pls Input Trolley Line No.";
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        /// <summary>
        /// 检查车行号是否存在
        /// </summary>
        /// <param name="strTrolleyLine">车行号&车号</param>
        /// <returns></returns>
        internal ExecutionResult CheckTrolleyLine(baseinfo bean)
        {
            exeRes = new ExecutionResult();
            try
            {
                if (string.IsNullOrEmpty(bean.Trolley_no))
                {
                    exeRes.Status = false;
                    exeRes.Message = "Input Trolley No First and Click Enter pls";
                    return exeRes;
                }
                exeRes = dgw.CheckTrolleyLine(bean);
                if ((DataSet)exeRes.Anything == null || ((DataSet)exeRes.Anything).Tables[0].Rows.Count < 1)
                {
                    exeRes.Status = false;
                    exeRes.Message = "Check Trolley Line No Error ! pls check.";
                }
                else
                {
                    exeRes.Status = true;
                    //获取输入该车行最大容量
                    baseinfo.MaxQtyByLine = int.Parse(((DataSet)exeRes.Anything).Tables[0].Rows[0]["MAXQTY"].ToString());
                    exeRes.Message = "pls Input  SN or Carton No.";
                    //检查该行是否被使用
                    exeRes = dgw.CheckTrolleyLineIsUsed(bean);
                    if (((DataSet)exeRes.Anything).Tables[0].Rows.Count > 0)
                    {
                        exeRes.Status = false;
                        exeRes.Message = "Trolley Line has be Used ! pls check.";
                    }
                }
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        /// <summary>
        /// 获取并检查序列号信息
        /// </summary>
        /// <param name="bean">输入序列号信息</param>
        /// <returns>SN关联信息</returns>
        internal ExecutionResult GetSnInfo(baseinfo bean)
        {
            exeRes = new ExecutionResult();
            try
            {
                if (string.IsNullOrEmpty(bean.Trolley_no) || string.IsNullOrEmpty(bean.Trolley_Line_No))
                {
                    exeRes.Status = false;
                    exeRes.Message = "Input Trolley No or Trolley Line No First pls.";
                    return exeRes;
                }
                if (bean.Check_Index > baseinfo.MaxQtyByLine)
                {
                    exeRes.Status = false;
                    exeRes.Message = "已扫入产品数量不能超过该行最大存储数[" + baseinfo.MaxQtyByLine + "]";
                    return exeRes;
                }

                exeRes = dgw.GetSnInfo(bean);
                //可以更新检查逻辑为 customer sn 是否与箱号相同
                if ((DataSet)exeRes.Anything == null || ((DataSet)exeRes.Anything).Tables[0].Rows.Count < 1)
                {
                    exeRes.Status = false;
                    exeRes.Message = "Invalid SN ,pls check";
                }
                else if (!((DataSet)exeRes.Anything).Tables[0].Rows[0]["PACKQTY"].ToString().Equals("1") ||
                    !((DataSet)exeRes.Anything).Tables[0].Rows[0]["PARTTYPE"].ToString().Equals("M") ||
                    !((DataSet)exeRes.Anything).Tables[0].Rows[0]["TOTAL"].ToString().Equals("1"))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("SN:{0} not M-Part single or Cannot found keypart Data,pls check", bean.Customer_SN);
                }
                else if (!((DataSet)exeRes.Anything).Tables[0].Rows[0]["WC"].ToString().Equals("W0"))
                {
                    exeRes.Status = false;
                    exeRes.Message = string.Format("SN:{0} Route Error,pls check", bean.Customer_SN);
                }
                else
                {
                    exeRes.Status = true;
                    bean.Carton_No = ((DataSet)exeRes.Anything).Tables[0].Rows[0]["CARTON_NO"].ToString();
                    bean.Customer_SN = ((DataSet)exeRes.Anything).Tables[0].Rows[0]["customer_sn"].ToString();
                    bean.OriginPallet_no = ((DataSet)exeRes.Anything).Tables[0].Rows[0]["Pallet_no"].ToString();
                    bean.KeyPart = ((DataSet)exeRes.Anything).Tables[0].Rows[0]["part_no"].ToString();
                    bean.Emp_ID = ClientUtils.UserPara1;
                    bean.isSingle = true;
                }
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        internal ExecutionResult ExecuteTrolleyCheckIn(baseinfo bean, DataTable dataSource)
        {
            exeRes = new ExecutionResult();
            try
            {
                exeRes = dgw.GetDBConnAddr();//获取配置DB连线
                if ((DataSet)exeRes.Anything == null || ((DataSet)exeRes.Anything).Tables[0].Rows.Count < 1)
                {
                    exeRes.Status = false;
                    exeRes.Message = "Get DB Connect Address Error. pls call pps team to check Table:c_configsetting";
                    return exeRes;
                }
                //获取判断新栈板号/沿用旧栈板号
                bean.Pallet_No = GetPalletNo(bean);
                //创建连线
                DBTransaction dbTransaction = new DBTransaction(((DataSet)exeRes.Anything).Tables[0].Rows[0][0].ToString());
                dbTransaction.BeginTransaction();//开启事务
                try
                {
                    foreach (DataRow dr in dataSource.Rows)//遍历datatable
                    {
                        bean.Trolley_no = dr["TrolleyNo"].ToString();
                        bean.Trolley_Line_No = dr["TrolleyLineNo"].ToString();
                        bean.Point_No = dr["Check_Index"].ToString();
                        bean.Customer_SN = dr["Customer_SN"].ToString();
                        bean.KeyPart = dr["KeyPartNo"].ToString();
                        bean.OriginPallet_no = dr["OriginPalletNo"].ToString();
                        //写入暂存记录表 ppsuser.T_TROLLEY_MPART_SN
                        exeRes = dgw.InsertMPartTrolley(bean, dbTransaction);
                        //更新库存表中数量ppsuser.t_location single qty-1
                        if (exeRes.Status)
                            exeRes = dgw.UpdateLocation(bean, dbTransaction);
                        //更新SN旧栈板号 ppsuser.t_sn_status 
                        if (exeRes.Status)
                            exeRes = dgw.UpdateSnPallet(bean, dbTransaction);
                        if (!exeRes.Status)
                            break;
                    }
                    if (exeRes.Status)
                        dbTransaction.Commit();//提交事务
                    else
                        dbTransaction.Rollback();//回滚事务
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();//回滚事务
                    exeRes.Status = false;
                    exeRes.Message = ex.Message;
                }
                finally
                {
                    dbTransaction.EndTransaction();//结束事务
                }
            }
            catch (Exception e)
            {
                exeRes.Status = false;
                exeRes.Message = e.Message;
            }
            return exeRes;
        }

        internal ExecutionResult CheckSN(baseinfo bean)
        {
            //可以写入GetSNInfo方法中
            ExecutionResult res = new ExecutionResult();
            res = dgw.CheckSn(bean.Customer_SN);
            if (((DataSet)res.Anything).Tables[0].Rows.Count > 0)
            {
                res.Status = false;
                res.Message = string.Format("SN:{0} has been scan in Line :{1},pls change other sn.", bean.Customer_SN, ((DataSet)res.Anything).Tables[0].Rows[0]["Trolley_Line_no"].ToString());
            }
            return res;
        }

        private string GetPalletNo(baseinfo bean)
        {
            string strPallet = "";
            DataTable dataTable = dgw.GetPalletByTrolley(bean.Trolley_no);
            strPallet = dataTable.Rows.Count > 0 ? dataTable.Rows[0][0].ToString() : dgw.GetNewPallet();
            return strPallet;
        }
    }
}
