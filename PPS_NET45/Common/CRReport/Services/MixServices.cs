using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace CRReport.Services
{
    class MixServices
    {
        public int SQTY, UROWS, QTY1, QTY2, QTY3, QTY4, QTY5, QTY6, QTY7, CURRENT_VALUE;
        public string TDN, TPACK_CODE, TDDLINE, TPALLET_NO, TRES;
        //建立连接池
        public string conStr = @"data source=(DESCRIPTION =
    (ADDRESS_LIST =
      (ADDRESS = (PROTOCOL = TCP)(HOST = ppsscan.luxshare.com.cn)(PORT = 1521))
    )
    (CONNECT_DATA =
      (SERVICE_NAME = ksppsa)
    )
  ); user id=ppstemspro;password =sproppstem";
        public OracleConnection conn;

        //创建事务
        public OracleTransaction tx;
        public void ExecuteSqlTran()
        {
            try
            {
                using (conn = new OracleConnection(conStr))
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    //创建事务
                    tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                     SJ_INSERT_PALLET_T();
                    //Test();
                    

                    try
                    {
                        if (TRES.Substring(0, 2) == "OK")
                        {
                            tx.Commit();//提交
                        }
                        else
                        {
                            tx.Rollback();//回滚
                        }
                    }
                    catch (Exception)
                    {
                        tx.Rollback();//回滚
                        throw;
                    }
                }

            }
            catch (OracleException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

        }


     //   public void Test()
     //   {

     //       string Oracle5 = @" INSERT INTO tt2pps.test
     // VALUES(:ID,:NAME,:SCOR,:NUB)";

     //       OracleParameter[] SP5 = {
     //             new OracleParameter(":ID", OracleDbType.Varchar2, 100),
     //               new OracleParameter(":NAME", OracleDbType.Varchar2, 100),
     //               new OracleParameter(":SCOR", OracleDbType.Varchar2, 100),
     //               new OracleParameter(":NUB", OracleDbType.Varchar2, 100)
     //           };
     //       SP5[0].Value = "5";
     //       SP5[1].Value = "ww";
     //       SP5[2].Value = "22";
     //       SP5[3].Value = "33";
     //       DBHelper.ExecuteSQL(conn, Oracle5, tx, SP5);


     //       string Oracle = @" select ID from  tt2pps.test  
     //WHERE ID=:ID";
     //       OracleParameter[] SP = {
     //               new OracleParameter(":ID", OracleDbType.Varchar2, 100)
     //           };
     //       SP[0].Value = 3;
     //       DataSet ds = DBHelper.ExecuteSQL(conn, Oracle, tx, SP);

     //       string a = ds.Tables[0].Rows[0][0].ToString();

     //       string Oracle9 = @" DELETE tt2pps.test  
     //WHERE ID=:ID";
     //       OracleParameter[] SP9 = {
     //               new OracleParameter(":ID", OracleDbType.Varchar2, 100)
     //           };
     //       SP9[0].Value = a;
     //       DBHelper.ExecuteSQL(conn, Oracle9, tx, SP9);


     //       //TRES = "2";

     //       TRES = "OK";

     //   }

        /// <summary>
        /// PPSUSER.SJ_INSERT_PALLET_T
        /// </summary>
        public void SJ_INSERT_PALLET_T()
        {
            string sql1 = @"  SELECT A.SHIPMENT_ID,B.PACK_CODE, B.DN, B.ICTPN,  B.DN_LINE, B.DD_LINE
      FROM PPSUSER.G_DS_SHIPMENT_DDLINE_T B, PPSUSER.G_DS_SHIMMENT_BASE_T A
     WHERE A.SHIPMENT_ID = B.SHIPMENT_ID and A.SHIPMENT_ID=:SHIPMENT_ID and B.DD_LINE=:DD_LINE
     --and  not exists(select c.* from ppsuser.g_ds_pallet_t c  where c.shipment_id = a.shipment_id)
    --AND A.STATUS = 'A'
    -- GROUP BY A.SHIPMENT_ID,
             -- B.PACK_CODE,
              --B.DN,
             -- B.ICTPN,
            --  B.DN_LINE,
          --    B.DD_LINE
  --   ORDER BY A.SHIPMENT_ID,B.PACK_CODE,B.DN ASC ";
            OracleParameter[] SP = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                      new OracleParameter(":DD_LINE", OracleDbType.Varchar2, 100),
                };
            SP[0].Value = "PDSS_20180614_00001";
            SP[1].Value = 1;
            DataSet ds1 = DBHelper.ExecuteSQL(conn, sql1, tx, SP);
            TRES = "OK";
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                CURRENT_VALUE = 0;
                string sql2 = @" SELECT B.TOTAL_QTY
                  FROM PPSUSER.G_DS_PARTINFO_T A, PPSUSER.G_DS_PACKINFO_T B
                 WHERE A.PACK_CODE = B.PACK_CODE
                   AND A.ICTPN =:ICTPN ";
                OracleParameter[] SP2 = {
                    new OracleParameter(":ICTPN", OracleDbType.Varchar2, 100)
                };
                SP2[0].Value = ds1.Tables[0].Rows[i]["ICTPN"];
                DataSet ds2 = DBHelper.ExecuteSQL(conn, sql2, tx, SP2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    SQTY = Convert.ToInt32(ds2.Tables[0].Rows[0]["TOTAL_QTY"].ToString());
                }

                string sql3 = @" SELECT A.QTY, FLOOR(A.QTY / :SQTY)
      FROM PPSUSER.G_DS_SHIPMENT_DDLINE_T A
     WHERE A.SHIPMENT_ID =:SHIPMENT_ID
       AND A.PACK_CODE =:PACK_CODE
       AND A.DD_LINE =:DD_LINE
       AND A.DN =:DN
       AND A.ICTPN =:ICTPN";
                OracleParameter[] SP3 = {
                     new OracleParameter(":SQTY", OracleDbType.Varchar2, 100),
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":PACK_CODE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DD_LINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":ICTPN", OracleDbType.Varchar2, 100),
                };
                SP3[0].Value = SQTY;
                SP3[1].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                SP3[2].Value = ds1.Tables[0].Rows[i]["PACK_CODE"];
                SP3[3].Value = ds1.Tables[0].Rows[i]["DD_LINE"];
                SP3[4].Value = ds1.Tables[0].Rows[i]["DN"];
                SP3[5].Value = ds1.Tables[0].Rows[i]["ICTPN"];
                DataSet ds3 = DBHelper.ExecuteSQL(conn, sql3, tx, SP3);
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    QTY3 = Convert.ToInt32(ds3.Tables[0].Rows[0]["QTY"].ToString());
                    UROWS = Convert.ToInt32(ds3.Tables[0].Rows[0][1].ToString());
                }
                 string sql4 = @"  SELECT COUNT(*)
                  FROM PPSUSER.G_DS_SHIPMENT_DDLINE_T A
                 WHERE A.SHIPMENT_ID =:SHIPMENT_ID
                   AND A.DN =:DN
                   AND A.ICTPN =:ICTPN
                   AND A.DD_LINE =:DD_LINE";

                OracleParameter[] SP4 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":ICTPN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DD_LINE", OracleDbType.Varchar2, 100),
                };
                SP4[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                SP4[1].Value = ds1.Tables[0].Rows[i]["DN"];
                SP4[2].Value = ds1.Tables[0].Rows[i]["ICTPN"];
                SP4[3].Value = ds1.Tables[0].Rows[i]["DD_LINE"];
                DataSet ds4 = DBHelper.ExecuteSQL(conn, sql4, tx, SP4);
                if (ds4.Tables[0].Rows.Count > 0)
                {
                    QTY1 = Convert.ToInt32(ds4.Tables[0].Rows[0][0].ToString());
                }

                if (QTY1 == 0)
                {
                    continue;
                }

                if (QTY3 > SQTY)
                {
                    while (CURRENT_VALUE < UROWS)
                    {
                        SJ_INSERT_PALLET_INFO_T(ds1.Tables[0].Rows[0]["ICTPN"].ToString(), ds1.Tables[0].Rows[0]["SHIPMENT_ID"].ToString(), SQTY, "B", out TRES);
                        TPALLET_NO = TRES;
                        SJ_INSERT_PICK(ds1.Tables[0].Rows[0]["ICTPN"].ToString(), TPALLET_NO, ds1.Tables[0].Rows[0]["SHIPMENT_ID"].ToString(), ds1.Tables[0].Rows[0]["DN"].ToString(),
                            ds1.Tables[0].Rows[0]["DD_LINE"].ToString(), ds1.Tables[0].Rows[0]["DN_LINE"].ToString(), SQTY, out TRES);
                        CURRENT_VALUE++;
                    }

                    QTY2 = QTY3 - SQTY * UROWS;
                    string sql5 = @" INSERT INTO PPSUSER.G_DS_PALLET_TEMP_T A
        (A.SHIPMENT_ID, A.DN, A.QTY, A.ICTPN, A.DN_LINE, A.DD_LINE)
      VALUES(:SHIPMENT_ID,:DN,:QTY,:ICTPN,:DN_LINE,:DD_LINE)";

                    OracleParameter[] SP5 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":QTY", OracleDbType.Varchar2, 100),
                    new OracleParameter(":ICTPN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN_LINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DD_LINE", OracleDbType.Varchar2, 100),
                };
                    SP5[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                    SP5[1].Value = ds1.Tables[0].Rows[i]["DN"];
                    SP5[2].Value = QTY2;
                    SP5[3].Value = ds1.Tables[0].Rows[i]["ICTPN"];
                    SP5[4].Value = ds1.Tables[0].Rows[i]["DN_LINE"];
                    SP5[5].Value = ds1.Tables[0].Rows[i]["DD_LINE"];
                    DBHelper.ExecuteSQL(conn, sql5, tx, SP5);
                }
                else
                {
                    string sql6 = @" INSERT INTO PPSUSER.G_DS_PALLET_TEMP_T A
        (A.SHIPMENT_ID, A.DN, A.QTY, A.DD_LINE, A.ICTPN, A.DN_LINE)
      VALUES(:SHIPMENT_ID,:DN,:QTY,:DD_LINE,:ICTPN,:DN_LINE)";
                    OracleParameter[] SP6 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":QTY", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DD_LINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":ICTPN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN_LINE", OracleDbType.Varchar2, 100),
                };
                    SP6[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                    SP6[1].Value = ds1.Tables[0].Rows[i]["DN"];
                    SP6[2].Value = QTY3;
                    SP6[3].Value = ds1.Tables[0].Rows[i]["DD_LINE"];
                    SP6[4].Value = ds1.Tables[0].Rows[i]["ICTPN"];
                    SP6[5].Value = ds1.Tables[0].Rows[i]["DN_LINE"];
                    DBHelper.ExecuteSQL(conn, sql6, tx, SP6);
                }

                string sql7 = @" SELECT B.TOTAL_QTY, A.PACK_CODE
      FROM PPSUSER.G_DS_PARTINFO_T A, PPSUSER.G_DS_PACKINFO_T B
     WHERE A.PACK_CODE = B.PACK_CODE
       AND A.ICTPN =:ICTPN";
                OracleParameter[] SP7 = {
                    new OracleParameter(":ICTPN", OracleDbType.Varchar2, 100),
                };
                SP7[0].Value = ds1.Tables[0].Rows[i]["ICTPN"];
                DataSet ds7 = DBHelper.ExecuteSQL(conn, sql7, tx, SP7);
                if (ds7.Tables[0].Rows.Count > 0)
                {
                    SQTY = Convert.ToInt32(ds7.Tables[0].Rows[0]["TOTAL_QTY"].ToString());
                    TPACK_CODE = ds7.Tables[0].Rows[0]["PACK_CODE"].ToString();

                }

                string sql8 = @" SELECT A.QTY, A.DN, A.DD_LINE
      FROM PPSUSER.G_DS_PALLET_TEMP_T A 
     WHERE A.SHIPMENT_ID =:SHIPMENT_ID
       AND A.ICTPN =:ICTPN
       AND A.DN_LINE =:DN_LINE 
       AND A.DD_LINE =:DD_LINE
       AND A.DN =:DN";

                OracleParameter[] SP8 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":ICTPN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN_LINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DD_LINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN", OracleDbType.Varchar2, 100),
                };
                SP8[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                SP8[1].Value = ds1.Tables[0].Rows[i]["ICTPN"];
                SP8[2].Value = ds1.Tables[0].Rows[i]["DN_LINE"];
                SP8[3].Value = ds1.Tables[0].Rows[i]["DD_LINE"];
                SP8[4].Value = ds1.Tables[0].Rows[i]["DN"];
                DataSet ds8 = DBHelper.ExecuteSQL(conn, sql8, tx, SP8);
                if (ds8.Tables[0].Rows.Count > 0)
                {
                    QTY4 = Convert.ToInt32(ds8.Tables[0].Rows[0]["QTY"].ToString());
                    TDN = ds8.Tables[0].Rows[0]["DN"].ToString();
                    TDDLINE = ds8.Tables[0].Rows[0]["DD_LINE"].ToString();
                }

                string sql9 = @" DELETE PPSUSER.G_DS_PALLET_TEMP_T A 
     WHERE A.SHIPMENT_ID =:SHIPMENT_ID
       AND A.ICTPN =:ICTPN
       AND A.DN_LINE =:DN_LINE
       AND A.DD_LINE =:DD_LINE
       AND A.DN =:DN";
                OracleParameter[] SP9 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":ICTPN", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN_LINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DD_LINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":DN", OracleDbType.Varchar2, 100),
                };
                SP9[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                SP9[1].Value = ds1.Tables[0].Rows[i]["ICTPN"];
                SP9[2].Value = ds1.Tables[0].Rows[i]["DN_LINE"];
                SP9[3].Value = ds1.Tables[0].Rows[i]["DD_LINE"];
                SP9[4].Value = ds1.Tables[0].Rows[i]["DN"];
                DBHelper.ExecuteSQL(conn, sql9, tx, SP9);

                string sql10 = @" SELECT COUNT(*)
      FROM PPSUSER.G_DS_PALLET_T A
     WHERE A.SHIPMENT_ID =:SHIPMENT_ID
       AND A.PACKING_CODE =:PACKING_CODE
       AND A.INSERT_FLAG = 'A'";

                OracleParameter[] SP10 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":PACKING_CODE", OracleDbType.Varchar2, 100),
                };
                SP10[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                SP10[1].Value = ds1.Tables[0].Rows[i]["PACK_CODE"];
                DataSet ds10 = DBHelper.ExecuteSQL(conn, sql10, tx, SP10);
                if (ds10.Tables[0].Rows.Count > 0)
                {
                    QTY5 = Convert.ToInt32(ds10.Tables[0].Rows[0][0].ToString());

                }



                if (QTY5 == 0)
                {
                    SJ_INSERT_PALLET_INFO_T(ds1.Tables[0].Rows[0]["ICTPN"].ToString(), ds1.Tables[0].Rows[0]["SHIPMENT_ID"].ToString(), QTY4, "A", out TRES);
                    TPALLET_NO = TRES;
                    SJ_INSERT_PICK(ds1.Tables[0].Rows[0]["ICTPN"].ToString(), TPALLET_NO, ds1.Tables[0].Rows[0]["SHIPMENT_ID"].ToString(), ds1.Tables[0].Rows[0]["DN"].ToString(),
                        ds1.Tables[0].Rows[0]["DD_LINE"].ToString(), ds1.Tables[0].Rows[0]["DN_LINE"].ToString(), QTY4, out TRES);
                }
                else
                {
                    string sql11 = string.Format(@"  SELECT A.QTY, A.PALLET_NO
        FROM PPSUSER.G_DS_PALLET_T A
       WHERE A.SHIPMENT_ID =:SHIPMENT_ID
         AND A.INSERT_FLAG = 'A'
         AND A.PACKING_CODE =:PACKING_CODE", ds1.Tables[0].Rows[i]["SHIPMENT_ID"], ds1.Tables[0].Rows[i]["PACK_CODE"]);
                    OracleParameter[] SP11 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":PACK_CODE", OracleDbType.Varchar2, 100),
                };
                    SP11[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                    SP11[1].Value = ds1.Tables[0].Rows[i]["PACK_CODE"];
                    DataSet ds11 = DBHelper.ExecuteSQL(conn, sql11, tx, SP11);
                    if (ds11.Tables[0].Rows.Count > 0)
                    {
                        QTY6 = Convert.ToInt32(ds11.Tables[0].Rows[0]["QTY"].ToString());
                        TPALLET_NO = ds8.Tables[0].Rows[0]["PALLET_NO"].ToString();

                    }


                    if (QTY6 + QTY4 > SQTY)
                    {
                        string sql12 = @"  UPDATE PPSUSER.G_DS_PALLET_T A
           SET A.QTY =:QTY, A.INSERT_FLAG = 'B'
         WHERE A.SHIPMENT_ID =:SHIPMENT_ID
           AND A.INSERT_FLAG = 'A'
           AND A.PALLET_NO =:PALLET_NO";
                        OracleParameter[] SP12 = {
                    new OracleParameter(":QTY", OracleDbType.Varchar2, 100),
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":PACK_CODE", OracleDbType.Varchar2, 100),
                };
                        SP12[0].Value = SQTY;
                        SP12[1].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                        SP12[2].Value = ds1.Tables[0].Rows[i]["PACK_CODE"];
                        DBHelper.ExecuteSQL(conn, sql12, tx, SP12);

                        SJ_INSERT_PICK(ds1.Tables[0].Rows[0]["ICTPN"].ToString(), TPALLET_NO, ds1.Tables[0].Rows[0]["SHIPMENT_ID"].ToString(), ds1.Tables[0].Rows[0]["DN"].ToString(),
                          ds1.Tables[0].Rows[0]["DD_LINE"].ToString(), ds1.Tables[0].Rows[0]["DN_LINE"].ToString(), SQTY - QTY6, out TRES);

                        SJ_INSERT_PALLET_INFO_T(ds1.Tables[0].Rows[0]["ICTPN"].ToString(), ds1.Tables[0].Rows[0]["SHIPMENT_ID"].ToString(), QTY6 + QTY4 - SQTY, "A", out TRES);

                        TPALLET_NO = TRES;

                        SJ_INSERT_PICK(ds1.Tables[0].Rows[0]["ICTPN"].ToString(), TPALLET_NO, ds1.Tables[0].Rows[0]["SHIPMENT_ID"].ToString(), ds1.Tables[0].Rows[0]["DN"].ToString(),
                            ds1.Tables[0].Rows[0]["DD_LINE"].ToString(), ds1.Tables[0].Rows[0]["DN_LINE"].ToString(), QTY6 + QTY4 - SQTY, out TRES);
                    }
                    else
                    {
                        string sql13 = @"  UPDATE PPSUSER.G_DS_PALLET_T A
           SET A.QTY =:QTY
         WHERE A.SHIPMENT_ID =:SHIPMENT_ID
           AND A.INSERT_FLAG = 'A'
           AND A.PALLET_NO =:PALLET_NO";
                        OracleParameter[] SP13 = {
                    new OracleParameter(":QTY", OracleDbType.Varchar2, 100),
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TPALLET_NO", OracleDbType.Varchar2, 100),
                };
                        SP13[0].Value = QTY6 + QTY4;
                        SP13[1].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                        SP13[2].Value = ds1.Tables[0].Rows[i]["PACK_CODE"];
                        DBHelper.ExecuteSQL(conn, sql13, tx, SP13);

                        SJ_INSERT_PICK(ds1.Tables[0].Rows[0]["ICTPN"].ToString(), TPALLET_NO, ds1.Tables[0].Rows[0]["SHIPMENT_ID"].ToString(), ds1.Tables[0].Rows[0]["DN"].ToString(),
                          ds1.Tables[0].Rows[0]["DD_LINE"].ToString(), ds1.Tables[0].Rows[0]["DN_LINE"].ToString(), SQTY - QTY6, out TRES);
                    }
                }


                string sql14 = @"  SELECT COUNT(*)
      FROM PPSUSER.G_DS_PALLET_TEMP_T A
     WHERE A.SHIPMENT_ID =:SHIPMENT_ID";
                OracleParameter[] SP14 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                };
                SP14[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                DataSet ds14 = DBHelper.ExecuteSQL(conn, sql14, tx, SP14);
                if (ds14.Tables[0].Rows.Count > 0)
                {
                    QTY7 = Convert.ToInt32(ds14.Tables[0].Rows[0][0].ToString());
                }


                if (QTY7 == 0)
                {
                    string sql15 = @"   UPDATE PPSUSER.G_DS_PALLET_T A
             SET A.INSERT_FLAG = 'D'
           WHERE A.SHIPMENT_ID =:SHIPMENT_ID";
                    OracleParameter[] SP15 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                };
                    SP15[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                    DBHelper.ExecuteSQL(conn, sql15, tx, SP15);


                    string sql16 = @"  UPDATE PPSUSER.G_DS_SHIMMENT_BASE_T A 
       SET A.STATUS ='NEW'
        WHERE A.SHIPMENT_ID =:SHIPMENT_ID";
                    OracleParameter[] SP16 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                };
                    SP16[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                    DBHelper.ExecuteSQL(conn, sql16, tx, SP16);

                    string sql17 = @"  update ppsuser.g_ds_pick_t a
       set a.pallet_type = (select b.pallet_type from ppsuser.g_ds_pallet_t b where b.pallet_no = a.pallet_no and a.shipment_id = b.shipment_id)
       where a.shipment_id =:SHIPMENT_ID";
                    OracleParameter[] SP17 = {
                    new OracleParameter(":SHIPMENT_ID", OracleDbType.Varchar2, 100),
                };
                    SP17[0].Value = ds1.Tables[0].Rows[i]["SHIPMENT_ID"];
                    DBHelper.ExecuteSQL(conn, sql17, tx, SP17);
                }
            }



        }

        /// <summary>
        /// PPSUSER.SJ_INSERT_PALLET_INFO_T
        /// </summary>
        public void SJ_INSERT_PALLET_INFO_T(string TDATA, string TSHIPMENT, int TQTY, string TFLAG, out string TRES)
        {
            string TPALLETNO = "";
            string sql1 = @"SELECT TO_CHAR(SYSDATE, 'yyyymm') ||
         LPAD(PPSUSER.G_DS_PALLET_NO.NEXTVAL, 9, 0) CODES  FROM DUAL";

            OracleParameter[] SP1 = {
                };
            DataSet ds = DBHelper.ExecuteSQL(conn, sql1, tx, SP1);
            if (ds.Tables[0].Rows.Count>0)
            {
                TPALLETNO = ds.Tables[0].Rows[0]["CODES"].ToString();
            }

            string sql2 = @"INSERT INTO PPSUSER.G_DS_PALLET_T A
    SELECT :TPALLETNO,
           B.PACK_CODE,
           B.TYPE,
           :TSHIPMENT,
           :TQTY,
           '0',
           B.EMPTY_PALLET_WEIGHT,
           '',
           :TFLAG,
           SYSDATE,
           '0',
           '',
           '',
           '',
           '',
           A.MODEL_NAME,
           '',
           '',
           '',
           '',
           '0','','0'
      FROM PPSUSER.G_DS_PARTINFO_T A, PPSUSER.G_DS_PACKINFO_T B
     WHERE A.PACK_CODE = B.PACK_CODE
       AND A.ICTPN = :TDATA";
            OracleParameter[] SP2 = {
                 new OracleParameter(":TPALLETNO", OracleDbType.Varchar2, 100),
                new OracleParameter(":TSHIPMENT", OracleDbType.Varchar2, 100),
                new OracleParameter(":TQTY", OracleDbType.Varchar2, 100),
                new OracleParameter(":TFLAG", OracleDbType.Varchar2, 100),
                new OracleParameter(":TDATA", OracleDbType.Varchar2, 100),
                };
            SP2[0].Value = TPALLETNO;
            SP2[1].Value = TSHIPMENT;
            SP2[2].Value = TQTY;
            SP2[3].Value = TFLAG;
            SP2[4].Value = TDATA;
            DBHelper.ExecuteSQL(conn, sql2, tx, SP2);

            TRES = "OK";
            if (TRES.Substring(0, 2) == "OK")
            {
                TRES = TPALLETNO;
            }
        }

        /// <summary>
        /// PPSUSER.SJ_INSERT_PICK                                                                                
        /// </summary>
        public void SJ_INSERT_PICK(string TDATA, string TPALLET_NO, string TSHIPMENT, string TDN, string TDDLINE, string TDNLINE, int TQTY, out string TRES)
        {
            int qty1 = 0, qty2 = 0;
            string LOCATION = "N/A";

            string sql1 = @"SELECT COUNT(*)
    FROM PPSUSER.G_SN_STATUS A, SAJET.SYS_PART B
   WHERE A.PART_ID = B.PART_ID
     AND B.PART_NO = :TDATA
     AND A.FLAG = 0
     AND ROWNUM <= :TQTY";
            OracleParameter[] SP1 = {
                    new OracleParameter(":TDATA", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TQTY", OracleDbType.Int32, 100),
                };
            SP1[0].Value = TDATA;
            SP1[1].Value = TQTY;
            DataSet ds1 = DBHelper.ExecuteSQL(conn, sql1, tx, SP1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                qty2 = Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString());
            }

            if (qty2 == TQTY)
            {
                string sql2 = @"SELECT DISTINCT (A.RC_NO)
      FROM PPSUSER.G_SN_STATUS A
     WHERE A.SERIAL_NUMBER IN
           (SELECT A.SERIAL_NUMBER
              FROM PPSUSER.G_SN_STATUS A, SAJET.SYS_PART B
             WHERE A.PART_ID = B.PART_ID
               AND B.PART_NO = :TDATA
               AND A.FLAG = 0
               AND ROWNUM <= :TQTY)
       AND ROWNUM = 1";
                OracleParameter[] SP2 = {
                    new OracleParameter(":TDATA", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TQTY", OracleDbType.Varchar2, 100),
                };
                SP2[0].Value = TDATA;
                SP2[1].Value = TQTY;
                DataSet ds2 = DBHelper.ExecuteSQL(conn, sql2, tx, SP2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    LOCATION = ds2.Tables[0].Rows[0][0].ToString();
                }

                string sql3 = @" UPDATE PPSUSER.G_SN_STATUS A
       SET A.FLAG = '1'
     WHERE A.SERIAL_NUMBER IN
           (SELECT A.SERIAL_NUMBER
              FROM PPSUSER.G_SN_STATUS A, SAJET.SYS_PART B
             WHERE A.PART_ID = B.PART_ID
               AND B.PART_NO = :TDATA
               AND A.FLAG = '0'
               AND ROWNUM <= :TQTY)";
                OracleParameter[] SP3 = {
                    new OracleParameter(":TDATA", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TQTY", OracleDbType.Varchar2, 100),
                };
                SP3[0].Value = TDATA;
                SP3[1].Value = TQTY;
                DBHelper.ExecuteSQL(conn, sql3, tx, SP3);
            }

            string sql4 = @"INSERT INTO PPSUSER.G_DS_PICK_T A
    (A.DN,
     A.DN_LINE,
     A.MPN,
     A.ICTPN,
     A.QTY,
     A.PALLET_NO,
     A.PACK_TYPE,
     A.PACK_CODE,
     A.REGION,
     A.CARRIER,
     A.POE,
     A.SHIPMENT_ID,
     A.SEQ,
     A.SUGGEST_STORE,
     A.SHIPPING_TIME)
    SELECT B.DN,
           B.DN_LINE,
           B.MPN,
           B.ICTPN,
           :TQTY,
           :TPALLET_NO,
           C.TYPE,
           C.PACK_CODE,
           A.REGION,
           A.CARRIER_NAME,
           A.POE,
           A.SHIPMENT_ID,
           (SELECT (SELECT DECODE(MAX(SEQ), '', 10000001, MAX(SEQ) + 1)
                      FROM PPSUSER.G_DS_PICK_T) AS A
              FROM DUAL),
           :LOCATION,SYSDATE
      FROM PPSUSER.G_DS_SHIPMENT_DDLINE_T B,
           PPSUSER.G_DS_SHIMMENT_BASE_T   A,
           PPSUSER.G_DS_PACKINFO_T        C,
           PPSUSER.G_DS_PALLET_T          D
     WHERE A.SHIPMENT_ID = B.SHIPMENT_ID
       AND C.PACK_CODE = B.PACK_CODE
       AND A.SHIPMENT_ID = D.SHIPMENT_ID
       AND D.PACKING_CODE = C.PACK_CODE
       AND B.ICTPN = :TDATA
       AND D.PALLET_NO = :TPALLET_NO
       AND B.DD_LINE = :TDDLINE
       AND B.DN_LINE = :TDNLINE
       AND B.SHIPMENT_ID = :TSHIPMENT
       AND B.DN = :TDN";
            OracleParameter[] SP4 = {
                    new OracleParameter(":TQTY", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TPALLET_NO", OracleDbType.Varchar2, 100),
                    new OracleParameter(":LOCATION", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TDATA", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TPALLET_NO", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TDDLINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TDNLINE", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TSHIPMENT", OracleDbType.Varchar2, 100),
                    new OracleParameter(":TDN", OracleDbType.Varchar2, 100),
                };
            SP4[0].Value = TQTY;
            SP4[1].Value = TPALLET_NO;
            SP4[2].Value = LOCATION;
            SP4[3].Value = TDATA;
            SP4[4].Value = TPALLET_NO;
            SP4[5].Value = TDDLINE;
            SP4[6].Value = TDNLINE;
            SP4[7].Value = TSHIPMENT;
            SP4[8].Value = TDN;
            DBHelper.ExecuteSQL(conn, sql4, tx, SP4);


            string sql5 = @" SELECT COUNT(DISTINCT(A.DN))
    FROM PPSUSER.G_DS_PICK_T A
   WHERE A.PALLET_NO = :TPALLET_NO";
            OracleParameter[] SP5 = {
                    new OracleParameter(":TPALLET_NO", OracleDbType.Varchar2, 100),
                };
            SP5[0].Value = TPALLET_NO;
            DataSet ds5 = DBHelper.ExecuteSQL(conn, sql5, tx, SP5);
            if (ds5.Tables[0].Rows.Count > 0)
            {
                qty1 = Convert.ToInt32(ds5.Tables[0].Rows[0][0].ToString());
            }

            if (qty1 > 1)
            {
                string sql6 = @"  UPDATE PPSUSER.G_DS_PALLET_T A
       SET A.PALLET_TYPE = '999'
     WHERE A.PALLET_NO = :TPALLET_NO";
                OracleParameter[] SP6 = {
                    new OracleParameter(":TPALLET_NO", OracleDbType.Varchar2, 100),
                };
                SP6[0].Value = TPALLET_NO;
                DBHelper.ExecuteSQL(conn, sql6, tx, SP6);
            }
            else
            {
                string sql7 = @"  UPDATE PPSUSER.G_DS_PALLET_T A
       SET A.PALLET_TYPE = '001'
     WHERE A.PALLET_NO = :TPALLET_NO";
                OracleParameter[] SP7 = {
                    new OracleParameter(":TPALLET_NO", OracleDbType.Varchar2, 100),
                };
                SP7[0].Value = TPALLET_NO;
                DBHelper.ExecuteSQL(conn, sql7, tx, SP7);
            }

            TRES = "OK";
        }
    }

}
