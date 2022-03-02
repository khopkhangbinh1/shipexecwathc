using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace MixDN
{
 public abstract class DBHelper
    {
        //数据库连接字符串
  //    public  string conStr = @"data source=(DESCRIPTION =
  //  (ADDRESS_LIST =
  //    (ADDRESS = (PROTOCOL = TCP)(HOST = ppsscan.luxshare.com.cn)(PORT = 1521))
  //  )
  //  (CONNECT_DATA =
  //    (SERVICE_NAME = ksppsa)
  //  )
  //); user id=ppstemspro;password =sproppstem";
        public static CommandType cmdType = CommandType.Text;

        /// <summary>
        /// 用现有的数据库连接执行一个返回数据集的Oracle命令
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="tx">事务</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的数据集</returns>
        public static DataSet ExecuteSQL(OracleConnection conn, string cmdText,OracleTransaction tx, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.CommandTimeout = 0;
            PrepareCommand(cmd,conn, tx, cmdType, cmdText, commandParameters);
            OracleDataAdapter adt = new OracleDataAdapter();
            adt.SelectCommand = cmd;
            OracleCommandBuilder cbd = new OracleCommandBuilder(adt);
            DataSet ds = new DataSet();
            try
            {
                adt.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                string a = e.Message;
                return null;
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            
        }
        
        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">Oracle命令</param>
        /// <param name="conn">Oracle连接</param>
        /// <param name="trans">Oracle事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如：Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, 
            CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    }

}
