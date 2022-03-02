using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.IO;

namespace wmsReport
{
    public partial class fWMSCheck2 : Form
    {
        private List<string> _lstCheckDuplicate = new List<string>();
        public fWMSCheck2()
        {
            InitializeComponent();
        }

        private string g_sUserID = ClientUtils.UserPara1;
        private string strCurrLocationID = "";     //保存查询时Loc id
        private string strCurrLocationNO = "";   //保存location_no 为后面查询条件
        private string strCurrWHID = "";            //保存查询时的WHID

        public int W = 0;
        private Int32 g_curRow = -1;    //当前选中行号
        WMSBLL wb = new WMSBLL();
       
        private void dgvInit()
        {
            if (dgvLocation.Rows.Count>0) 
            {
                dgvLocation.DataSource = null;
                dgvLocation.Rows.Clear();
            }
            if (dgvCheckSum.Rows.Count > 0) 
            {
                dgvCheckSum.DataSource = null;
                dgvCheckSum.Rows.Clear();
            }
                
        }

        private void fWMSCheck2_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //填充仓库信息
            string strSql = @"select warehouse_id id,warehouse_No name 
                                   from ppsuser.WMS_WAREHOUSE 
                                   where enabled = 'Y' and warehouse_No <> 'SYS' ORDER BY WAREHOUSE_NO";
            //WMSBLL wb = new WMSBLL();
            wb.fillCmb(strSql, "warehouse_No", cmbWHID);

            dgvInit();

            initDgv();

            if (rdoSN.Checked)
            {
                lblQTY.Visible = false;
                txtQTY.Visible = false;
                lblSN2.Visible = false;
                txtSN2.Visible = false;
                btnQTYCheck.Visible = false;
            }
            else
            {
                lblQTY.Visible = true;
                txtQTY.Visible = true;
                lblSN2.Visible = true;
                txtSN2.Visible = true;
                btnQTYCheck.Visible = true;
            }

        }

        private void initDgv()
        {
            W = Screen.PrimaryScreen.Bounds.Width;

            W = Convert.ToInt32(W * 0.5);


            this.grpLocationInfo.Width = W;
        }
        private void cmbWHID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWHID.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
                return;
            
            //填充储位信息
            // string strSql = "SELECT location_id id,location_no name FROM PPSUSER.WMS_LOCATION WHERE enabled = 'Y' AND warehouse_id = " + cmbWHID.SelectedValue + " order by location_no";

            string strSql = string .Format("SELECT location_id id, location_no name "
                             + "     FROM PPSUSER.WMS_LOCATION "
                             + "    WHERE LOCATION_NO IN "
                             + "          (SELECT location_no FROM ppsuser.t_location where qty > 0) "
                             + "      and enabled = 'Y' "
                             + "      AND warehouse_id = '{0}'"  
                             + "    order by location_no", cmbWHID.SelectedValue);
            WMSBLL wb =new WMSBLL();
            wb.fillCmb(strSql, "location_no", cmbLocation);

            TextMsg.Text = "";
        }

        /// <summary>
        /// 执行成功后，调用OK声音文件
        /// </summary>
        public void Ok()
        {
            LibHelper.MediasHelper.PlaySoundAsyncByOk();
        }

        /// <summary>
        /// 执行失败后，调用Ng声音文件
        /// </summary>
        public void Ng()
        {
            LibHelper.MediasHelper.PlaySoundAsyncByNg();
        }

        /// <summary>
        /// 查询业务逻辑控制
        /// </summary>
        /// <returns></returns>
        private DataTable findData()
        {
            int iPara = 0;                              //变量项次
            string strSql;                              //SQL字符串变量
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            bool isInput = false;                       //是否有输入查询条件
            DataTable dt = new DataTable();              //按查询条件，查出数据源

            strSql = "SELECT Part_NO 料号,Pack_Code 包规,Pallet_NO,CartonQTY 箱数," +
                     "qty 数量, QHCARTONQTY QHold箱数,QHQTY QHold数量 FROM PPSUSER.T_LOCATION where 1 = 1";
                //组合输入查询条件，过滤数据源
                //仓库有输入值时，添加查询条件变量
                if (cmbWHID.Text.Trim() != "")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and warehouse_id = :warehouse";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "warehouse", cmbWHID.SelectedValue };
                    iPara = iPara + 1;
                }

                //储位有输入值时，添加查询条件变量
                if (cmbLocation.Text.Trim() != "")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and location_no = :location";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "location", cmbLocation.Text.Trim() };
                    iPara = iPara + 1;
                }

                

                //判断是否有输入查询条件，调用不同的类型
                if (isInput)
                {
                    dt = ClientUtils.ExecuteSQL(strSql, sqlparams).Tables[0];
                }
                else
                {
                    dt = ClientUtils.ExecuteSQL(strSql).Tables[0];
                }

            if ((dt.Rows.Count > 0))
            {
                this.ShowMsg("查询条件下，资料获取成功!", 5);
                Ok();
            }
            else
            {
                this.ShowMsg("查询条件下，资料获取不到资料，请确认!", 0);
                Ng();
            }
            return dt;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                EmptyData();
                dgvLocation.Columns.Clear();
                dgvLocation.DataSource = findData();

                strCurrLocationID = cmbLocation.SelectedValue.ToString();
                strCurrLocationNO = cmbLocation.Text.Trim();
                strCurrWHID = cmbWHID.SelectedValue.ToString();

                if (dgvLocation.Rows.Count > 0)
                {
                    //查询到数据，开启检验按纽功能
                    //butStart.Enabled = true;
                    //butEnd.Enabled = false;
                    //txtSN.ReadOnly = true;
                    //btnSearch.Enabled = true;

                    //开始按纽开始
                    dgvCheckSum.DataSource = null;
                    dgvCheckSum.Rows.Clear();
                    dgvCheckSum.DataSource = wb.GetLocationCheckLog(strCurrLocationNO);

                    //按储位名称，取出全部location_no
                    //HYQ: 加了一个限制， WC=W0
                    dgvDetail.DataSource = null;
                    dgvDetail.Rows.Clear();
                    dgvDetail.DataSource=wb.GetLocationSnInfo(strCurrLocationNO);
                    dgvDetail2.DataSource = null;
                    dgvDetail2.Rows.Clear();


                    lalFirst.Text = "Y";
                    butStart.Enabled = false;
                    butEnd.Enabled = true;
                    txtSN.ReadOnly = false;
                    txtSN.SelectAll();
                    txtSN.Focus();
                }
                else
                {
                    //查询没有记录时，禁用开始作业功能
                    butStart.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMsg("未获取到数据" + ex.Message, 1);
                Ng();
            }
        }

        private void dgvFindResult_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvLocation.Rows.Count; i++)
            {
                this.dgvLocation.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            }
        }

        private void insertCheck()
        {
            string strSQL;

            string strCheckTime, l_pallet_no, l_cartonqty;

            strCheckTime = get_CheckTime(strCurrLocationNO);
            l_pallet_no = dgvLocation.Rows[0].Cells["PALLET_NO"].Value.ToString();
            l_cartonqty = get_cartonQTYsum();


            //HYQ: 之前没有检查 上一笔记录是否是异常退出， pass and errorqty 是0 的情况。
            if (strCheckTime.Contains("NEW"))
            {
                strCheckTime = strCheckTime.Replace("NEW", "").Trim();
                strSQL = "Insert into PPSUSER.T_LOCATION_Check(daycode, checktime, location_id, location_no, pallet_no," +
                         "cartonqty, passcartonqty, errorcartonqty, result, cdt, emp_id) " +
                         "values(to_char(sysdate, 'yyyy-mm-dd'), " + strCheckTime + "," + strCurrLocationID + ",'" + strCurrLocationNO.Replace("'", "''") +
                         "','" + l_pallet_no.Replace("'", "''") + "'," + l_cartonqty + ",0,0,'IN PROCESS PASS',sysdate," + g_sUserID + ")";
                DataSet sDataSet = ClientUtils.ExecuteSQL(strSQL);
            }
            else if (strCheckTime.Contains("OLD"))
            {
                strCheckTime = strCheckTime.Replace("OLD", "").Trim();
            }
            else
            {
                ShowMsg("获取库位最大检查次数异常",0);
                return;
            }
            lalFirst.Text = "N";

        }

        private string get_cartonQTYsum()
        {
            //汇总查询结果中箱数显示
            int iRows = dgvLocation.Rows.Count;
            int iQTY = 0;

            for (int i = 0; i < iRows; i++)
            {
                iQTY += Convert.ToInt16(dgvLocation.Rows[i].Cells["箱数"].Value);
            }
            return iQTY.ToString();
        }

        private DataTable searchShow()
        {
            string strSql = "SELECT Part_NO 料号,Pack_Code 包规,Pallet_NO,CartonQTY 箱数," +
                     "qty 数量, QHCARTONQTY QHold箱数,QHQTY QHold数量 FROM PPSUSER.T_LOCATION where 1 = 0";
            return ClientUtils.ExecuteSQL(strSql).Tables[0];

        }

       
        private string get_CheckTime(string locationNo)
        {
            string strSQL = string.Empty;
            //按日期与当天检查次数，递增检查次数  //之前人写的。
            //strSQL = "select nvl(max(checktime) + 1, 1) from PPSUSER.T_LOCATION_Check " +
            //                "where daycode = to_char(sysdate, 'yyyy-mm-dd') and location_no = '" + locationNo.Replace("'","''")+"'";
            
            //HYQ: 更新下如果上次check是异常退出的且检查数量是0 0 ， checktime 还是保持原来的最大值。

            strSQL = string.Format("select case when passcartonqty=0 and errorcartonqty=0  then checktime "
                                  + "    else checktime + 1 end as checktime2, "
                                  + "    case when passcartonqty = 0 and errorcartonqty = 0  then 'OLD' "
                                  + "    else 'NEW' end as checktimedesc "
                                  + "    from ppsuser.T_LOCATION_Check "
                                  + "   where checktime in (select max(checktime) checktime "
                                  + "                         from ppsuser.T_LOCATION_Check "
                                  + "                        where daycode = to_char(sysdate, 'yyyy-mm-dd') "
                                  + "                          and location_no = '{0}') "
                                  + "     and daycode = to_char(sysdate, 'yyyy-mm-dd') "
                                  + "     and location_no = '{1}'", locationNo, locationNo);
            if (ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows.Count > 0)
            {
                return ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows[0]["checktime2"].ToString() + ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows[0]["checktimedesc"].ToString();
            }
            else
            {
                return "0NEW";
            }
        
        }

       
        private void butStart_Click(object sender, EventArgs e)
        {
            //HYQ： 补了前人写的一个BUG //insertcheck() 转到txtsn_keydown里面了
            //insertCheck();

            //开始按纽开始
            dgvCheckSum.DataSource  = wb.GetLocationCheckLog(strCurrLocationNO);

            //按储位名称，取出全部location_no
            //HYQ: 加了一个限制， WC=W0
            dgvDetail.DataSource = null;
            dgvDetail.Rows.Clear();
            dgvDetail.DataSource = wb.GetLocationSnInfo(strCurrLocationNO);
            dgvDetail2.DataSource = null;
            dgvDetail2.Rows.Clear();

            lalFirst.Text = "Y";

            butStart.Enabled = false;
            butEnd.Enabled = true;
            txtSN.ReadOnly = false;


        }

        private void dgvCheckSum_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvCheckSum.Rows.Count; i++)
            {
                this.dgvCheckSum.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            }
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            string strSN = txtSN.Text.Trim();
            TextMsg.Text = "";

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strSN))
            {
                this.ShowMsg("输入的 " + lblSN.Text + " 不能为空！", 0);
                Ng();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }
            WMSBLL plb = new WMSBLL();
            strSN = plb.DelPrefixCartonSN(strSN);
            strSN = plb.ChangeCSNtoCarton(strSN);

            if (this._lstCheckDuplicate.IndexOf(strSN) < 0)
                this._lstCheckDuplicate.Add(strSN);
            else
            {
                this.ShowMsg("NG-输入的箱号重复刷入！", 0);
                Ng();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }

            //需要把重复刷入的挡住 cartonno和palletno 在 dgvDetail2出现就报错 

            //if (dgvDetail2.Rows.Count > 0) 
            //{
            //    for (int i=0;i< dgvDetail2.Rows.Count;i++ ) 
            //    {
            //        if (dgvDetail2.Rows[i].Cells["CartonID"].Value.ToString() == strSN)
            //        {
            //            this.ShowMsg("输入的箱号已经刷入过，重复", 0);
            //            Ng();
            //            txtSN.SelectAll();
            //            txtSN.Focus();
            //            return;
            //        }
            //        if (dgvDetail2.Rows[i].Cells["PalletNO"].Value.ToString() == strSN)
            //        {
            //            this.ShowMsg("输入的栈板号已经刷入过，重复", 0);
            //            Ng();
            //            txtSN.SelectAll();
            //            txtSN.Focus();
            //            return;
            //        }

            //    }
            //}

            // HYQ：20181105
            //添加QHold 检查
            //ReverseBll.CheckHold(string Sno, string Type, out string errorMessage)
            //Type有: 'SHIPMENT', 'PICKPALLETNO', 'PACKPALLETNO', 'SERIALNUMBER'

            if (chkQHold.Checked) 
            {
                string errorMessage = "";
                bool CheckHoldOK = Reverse.ReverseBll.CheckHold(strSN, "SERIALNUMBER", out errorMessage);
                if (!CheckHoldOK)
                {
                    ShowMsg(errorMessage, 0);
                    Ng();
                    txtSN.SelectAll();
                    txtSN.Focus();
                    return;
                }
            }
            //开始按纽开始
            //dgvCheckSum.DataSource = wb.GetLocationCheckLog(strCurrLocationNO);
            if (rdoSN.Checked) 
            {
                string strResult = string.Empty;
                string strResulterrmsg = string.Empty;
                strResult = wb.WMSStockCheck(strCurrLocationID, strSN, lalFirst.Text, g_sUserID, out strResulterrmsg);
                if (strResult.Equals("OK"))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    ShowMsg("OK", -1);
                    //将箱号 货栈板好资料写到 dgvDetail

                    DataTable dt2 = wb.GetSnInfo(strSN);
                    foreach (DataRow dr in dt2.Rows)
                    {
                        object[] ds = dr.ItemArray;
                        dgvDetail2.Rows.Insert(0, ds);
                    }
                    lalFirst.Text = "N";

                    if (dgvDetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgvDetail.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt2.Rows.Count; j++)
                            {
                                if (dgvDetail.Rows[i].Cells["CARTON_NO"].Value.ToString() == dt2.Rows[j]["carton_no"]?.ToString())
                                {
                                    dgvDetail.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                }
                            }

                        }
                    }

                }
                else if (strResult.Equals("WA"))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg(strResulterrmsg, -1);

                    //将箱号 货栈板好资料写到 dgvDetail

                    DataTable dt2 = wb.GetSnInfo(strSN);
                    foreach (DataRow dr in dt2.Rows)
                    {
                        object[] ds = dr.ItemArray;
                        dgvDetail2.Rows.Insert(0, ds);
                    }
                    lalFirst.Text = "N";
                }
                else
                {
                    if (strResulterrmsg.Contains("重复"))
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByRe();
                    }
                    else
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    }

                    ShowMsg(strResulterrmsg, 0);

                }

                dgvCheckSum.DataSource = wb.GetLocationCheckLog(strCurrLocationNO);

                if (strResulterrmsg.Equals("OK-FINISH"))
                {
                    txtSN.Text = "";
                    cmbLocation.Focus();
                    cmbLocation.SelectAll();
                }
                else
                {
                    txtSN.SelectAll();
                    txtSN.Focus();
                }
            }
            else
            {
                txtSN2.SelectAll();
                txtSN2.Focus();
            }
            
        }
        private void txtSN2_KeyDown(object sender, KeyEventArgs e)
        {
            string strSN = txtSN.Text.Trim();
            string strSN2 = txtSN2.Text.Trim();
            TextMsg.Text = "";

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strSN))
            {
                this.ShowMsg("输入的 " + lblSN.Text + " 不能为空！", 0);
                Ng();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }
            if (string.IsNullOrEmpty(strSN2))
            {
                this.ShowMsg("输入的 " + lblSN2.Text + " 不能为空！", 0);
                Ng();
                txtSN2.SelectAll();
                txtSN2.Focus();
                return;
            }
            WMSBLL plb = new WMSBLL();
            strSN = plb.DelPrefixCartonSN(strSN);
            strSN = plb.ChangeCSNtoCarton(strSN);
            strSN2 = plb.DelPrefixCartonSN(strSN2);
            strSN2 = plb.ChangeCSNtoCarton(strSN2);

            if (!plb.CheckPalletCarton(strSN, strSN2)) 
            {
                this.ShowMsg("输入的箱号与栈板号不匹配", 0);
                Ng();
                txtSN2.SelectAll();
                txtSN2.Focus();
                return;
            }
            Ok();
            txtQTY.SelectAll();
            txtQTY.Focus();
        }
        private void btnQTYCheck_Click(object sender, EventArgs e)
        {
            string strSN = txtSN.Text.Trim();
            string strSN2 = txtSN2.Text.Trim();
            TextMsg.Text = "";
         
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strSN))
            {
                this.ShowMsg("输入的 " + lblSN.Text + " 不能为空！", 0);
                Ng();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }
            if (string.IsNullOrEmpty(strSN2))
            {
                this.ShowMsg("输入的 " + lblSN2.Text + " 不能为空！", 0);
                Ng();
                txtSN2.SelectAll();
                txtSN2.Focus();
                return;
            }
            WMSBLL plb = new WMSBLL();
            strSN = plb.DelPrefixCartonSN(strSN);
            strSN = plb.ChangeCSNtoCarton(strSN);
            strSN2 = plb.DelPrefixCartonSN(strSN2);
            strSN2 = plb.ChangeCSNtoCarton(strSN2);

            string strQTY = txtQTY.Text;
            if (string.IsNullOrEmpty(strQTY))
            {
                this.ShowMsg("输入的数量不能为空！", 0);
                Ng();
                txtQTY.SelectAll();
                txtQTY.Focus();
                return;
            }
            if (rdoQTY.Checked)
            {
                string strResult = string.Empty;
                string strResulterrmsg = string.Empty;
                strResult = wb.WMSStockCheck2(strCurrLocationID, strSN, strSN2, strQTY, lalFirst.Text, g_sUserID, out strResulterrmsg);
                if (strResult.Equals("OK"))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    ShowMsg("OK", -1);
                    //将箱号 货栈板好资料写到 dgvDetail

                    DataTable dt2 = wb.GetSnInfo(strSN);
                    foreach (DataRow dr in dt2.Rows)
                    {
                        object[] ds = dr.ItemArray;
                        dgvDetail2.Rows.Insert(0, ds);
                    }
                    lalFirst.Text = "N";

                    if (dgvDetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgvDetail.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt2.Rows.Count; j++)
                            {
                                if (dgvDetail.Rows[i].Cells["CARTON_NO"].Value.ToString() == dt2.Rows[j]["carton_no"]?.ToString())
                                {
                                    dgvDetail.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                }
                            }

                        }
                    }

                }
                else if (strResult.Equals("WA"))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg(strResulterrmsg, -1);

                    //将箱号 货栈板好资料写到 dgvDetail

                    DataTable dt2 = wb.GetSnInfo(strSN);
                    foreach (DataRow dr in dt2.Rows)
                    {
                        object[] ds = dr.ItemArray;
                        dgvDetail2.Rows.Insert(0, ds);
                    }
                    lalFirst.Text = "N";
                }
                else
                {
                    if (strResulterrmsg.Contains("重复"))
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByRe();
                    }
                    else
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    }
                    ShowMsg(strResulterrmsg, 0);

                }

                dgvCheckSum.DataSource = wb.GetLocationCheckLog(strCurrLocationNO);

                if (strResulterrmsg.Equals("OK-FINISH"))
                {
                    txtSN.Text = "";
                    cmbLocation.Focus();
                    cmbLocation.SelectAll();
                }
                else
                {
                    txtSN.SelectAll();
                    txtSN.Focus();
                  

                }
                txtSN2.Text = "";
                txtQTY.Text = "";
            }
        }

        //检查错误后，需查出正确资料显示，并提示此行信息不属于此储位
        private int errorDisp(string inputValue)
        {
            string strSQL;
            strSQL = "SELECT CUSTOMER_SN, Carton_NO, PART_NO, LOCATION_NO, PALLET_NO,SERIAL_NUMBER FROM PPSUSER.T_SN_STATUS " +
                     " WHERE CUSTOMER_SN = '" + inputValue.Replace("'", "''") + "' or Carton_NO ='" + inputValue.Replace("'", "''") + "'";

            int retValue = -1;

            foreach (DataRow dr in ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows)
            {
                //检测是否有重复
                retValue = snCheck(dr);

                //两个都重复，就返回
                if (retValue == 2)
                    continue;

                //返回值为0或1时，新增一行
                object[] ds = dr.ItemArray;
                dgvDetail.Rows.Insert(0, ds);
                dgvDetail.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                dgvDetail.Rows[0].DefaultCellStyle.Font = new Font("宋体", 10, FontStyle.Italic);
                dgvDetail.CurrentCell = dgvDetail.Rows[0].Cells[0];
            }

            return retValue;

        } 

        private void upType()
        {
            int i = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["匹配箱数"].Value);
            dgvCheckSum.Rows[0].Cells["匹配箱数"].Value = i + 1;
            dgvCheckSum.Rows[0].Cells["匹配箱数"].Style.ForeColor = Color.Blue;
            dgvCheckSum.Rows[0].Cells["匹配箱数"].Style.Font = new Font("宋体", 12,FontStyle.Bold);
        }

        private void upErrorQty()
        {
            int i = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value);
            dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value = i + 1;
            dgvCheckSum.Rows[0].Cells["ERROR箱数"].Style.ForeColor = Color.Red;
            dgvCheckSum.Rows[0].Cells["ERROR箱数"].Style.Font = new Font("宋体", 12, FontStyle.Bold);
        }

        private int snCheck(DataRow dr)
        {
            //扫描出Customer SN / CARTON NO是否已存在，并更查询汇总数据与显示内容
            //0: 该储位查询出的箱号，还未扫描
            //1: 该储位查询出的箱号，已扫描，但Customer SN 未扫描
            //2: 该储位查询出的箱号、Customer SN都已扫描
            int ds = dgvDetail.Rows.Count;

            int retValue = 0;

            for (int i = 0; i < ds; i++)
            {
                //比较箱号是否相同
                if (dgvDetail.Rows[i].Cells["cartonid"].Value.ToString() == dr["Carton_NO"].ToString())
                {
                    //比较Customer SN 是否相同
                    if (retValue == 0)
                        retValue = 1;
                }
                //比较Customer SN 是否相同
                if (dgvDetail.Rows[i].Cells["sn"].Value.ToString() == dr["CUSTOMER_SN"].ToString())
                {
                    //比较Customer SN 是否相同
                    if (retValue == 1)
                    {
                        //检测到Customer SN 与 Carton ID都相同时，退出
                        retValue = 2;
                        break;
                    }
                }
            }

            return retValue;
            
        }


        private void dgvDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rs = dgvDetail.Rows.Count;

            for (int i = 0; i < rs; i++)
            {
                this.dgvDetail.Rows[i].HeaderCell.Value = (rs-i).ToString().Trim();
            }
        }

        private void butEnd_Click(object sender, EventArgs e)
        {
            if (dgvCheckSum.Rows.Count>0)
            {
                //如果点了开始 什么都没有做，点结束的话， 删除 这笔 pass0 error0的记录

                string iPassQty = dgvCheckSum.Rows[0].Cells["匹配箱数"].Value.ToString();
                string iErrorQty = dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value.ToString();
                string sDayCode = dgvCheckSum.Rows[0].Cells["日期"].Value.ToString();
                string sCheckTime = dgvCheckSum.Rows[0].Cells["检查次数"].Value.ToString();

                //HYQ: 正式上线后没有DELETE权限。  这段注释

                if (iPassQty.Equals("0") && iErrorQty.Equals("0"))
                {
                    string strSQL = string.Format("delete from PPSUSER.T_LOCATION_Check "
                                                  + "   where daycode = to_char(sysdate, 'yyyy-mm-dd') "
                                                  + "     and location_no = '{0}' "
                                                  + "     and checktime = '{1}' "
                                                  + "     and passcartonqty = '0' "
                                                  + "     and errorcartonqty = '0'", cmbLocation.Text.ToString(), sCheckTime);


                    //string strSQL = "update PPSUSER.T_LOCATION_Check SET  passcartonqty =" + iPassQty + ",errorcartonqty = " + iErrorQty +
                    //         " ,UDT = sysdate where daycode = '" + sDayCode + "' and checktime =" + sCheckTime;
                    ClientUtils.ExecuteSQL(strSQL);
                }
            }

            lalFirst.Text = "Y";

            //
            btnSearch.Enabled = true;
            butStart.Enabled = false;
            butEnd.Enabled = false;
            txtSN.ReadOnly = true;
            txtSN.Text = "";
            dgvDetail.DataSource = null;
            dgvDetail2.DataSource = null;
            dgvInit();
            btnSearch.Focus();

        }

        private void cmbLocationChange() 
        {
            ShowMsg("", 0);
            EmptyData();
            try
            {
                dgvLocation.Columns.Clear();
                dgvLocation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvLocation.DataSource = findData();

                strCurrLocationID = cmbLocation.SelectedValue.ToString();
                strCurrLocationNO = cmbLocation.Text.Trim();
                strCurrWHID = cmbWHID.SelectedValue.ToString();

                if (dgvLocation.Rows.Count > 0)
                {
                    //查询到数据，开启检验按纽功能
                    //butStart.Enabled = true;
                    //butEnd.Enabled = false;
                    //txtSN.ReadOnly = true;
                    //btnSearch.Enabled = true;

                    //开始按纽开始
                    dgvCheckSum.DataSource = null;
                    dgvCheckSum.Rows.Clear();
                    dgvCheckSum.DataSource = wb.GetLocationCheckLog(strCurrLocationNO);

                    //按储位名称，取出全部location_no
                    //HYQ: 加了一个限制， WC=W0
                    dgvDetail.DataSource = null;
                    dgvDetail.Rows.Clear();
                    dgvDetail.DataSource = wb.GetLocationSnInfo(strCurrLocationNO);

                    dgvDetail2.DataSource = null;
                    dgvDetail2.Rows.Clear();

                    lalFirst.Text = "Y";

                    butStart.Enabled = false;
                    butEnd.Enabled = true;
                    txtSN.ReadOnly = false;
                    txtSN.SelectAll();
                    txtSN.Focus();
                }
                else
                {
                    //查询没有记录时，禁用开始作业功能
                    butStart.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMsg("未获取到数据" + ex.Message, 1);
                Ng();
            }
        }
        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLocationChange();
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);
            cmbLocationChange();
        }
        private bool saveData()
        {
            //保存数据
            string iPassQty = dgvCheckSum.Rows[0].Cells["匹配箱数"].Value.ToString();
            string iErrorQty = dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value.ToString();
            string sDayCode = dgvCheckSum.Rows[0].Cells["日期"].Value.ToString();
            string sCheckTime = dgvCheckSum.Rows[0].Cells["检查次数"].Value.ToString();
            //HYQ:补上result的逻辑
            //i)	ErrorCartonQty = 0,CARTONQTY = PASSCARTONQTY 为PASS
            //ii)	ErrorCartonQty != 0,CARTONQTY = PASSCARTONQTY 为PASS_HASCHECKFAIL
            //iii)	ErrorCartonQty = 0,CARTONQTY > PASSCARTONQTY 为INPROCESSPASS
            //iv)	ErrorCartonQty != 0,CARTONQTY > PASSCARTONQTY 为INPROCESSFAIL

            //保存数据
            Int32 valiPassQty = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["匹配箱数"].Value);
            Int32 valiErrorQty = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value.ToString());
            Int32 valiCartonQty = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["箱数"].Value.ToString());
            string dgvchecksumResult = "";
            if (valiErrorQty == 0 && valiCartonQty == valiPassQty) 
            { 
                dgvchecksumResult = "PASS";
                cmbLocation.Focus();
                cmbLocation.SelectAll();

            }
            else if (valiErrorQty != 0 && valiCartonQty == valiPassQty) { dgvchecksumResult = "PASS_HASCHECKFAIL"; }
            else if (valiErrorQty == 0 && valiCartonQty > valiPassQty) { dgvchecksumResult = "INPROCESSPASS"; }
            else if (valiErrorQty != 0 && valiCartonQty > valiPassQty) { dgvchecksumResult = "INPROCESSFAIL"; }
            else { dgvchecksumResult = "INPROCESS"; }

            dgvCheckSum.Rows[0].Cells["结果"].Value = dgvchecksumResult;


            //HYQ: 原来人写的有问题， 少了location_no 
            string strSQL = string.Format("update PPSUSER.T_LOCATION_Check "
                                          + "    SET passcartonqty = '{0}', errorcartonqty = '{1}',result='{5}', udt = sysdate "
                                          + "   where daycode = '{2}' "
                                          + "     and location_no = '{3}' "
                                          + "     and checktime = '{4}'", iPassQty, iErrorQty, sDayCode,cmbLocation.Text.Trim(), sCheckTime, dgvchecksumResult);

            //string strSQL = "update PPSUSER.T_LOCATION_Check SET  passcartonqty =" + iPassQty + ",errorcartonqty = " + iErrorQty +
            //                " ,UDT = sysdate where daycode = '" + sDayCode + "' and checktime =" + sCheckTime;
            ClientUtils.ExecuteSQL(strSQL);
            string sPallet_no, serial_number, customer_sn, carton_no, snolocation_id, snolocation_no,result;

            foreach (DataGridViewRow dr in dgvDetail.Rows)
            {
                sPallet_no = dr.Cells["palletno"].Value.ToString();
                serial_number = dr.Cells["SERIAL_NUMBER"].Value.ToString();
                customer_sn = dr.Cells["sn"].Value.ToString();
                carton_no = dr.Cells["cartonid"].Value.ToString();
                snolocation_id = "";
                snolocation_no = dr.Cells["location"].Value.ToString();

                if (strCurrLocationNO == snolocation_no)
                    result = "OK";
                else
                    result = "NG";

                strSQL = "Insert into PPSUSER.t_location_check_sno_log(daycode, checktime, location_id, location_no, pallet_no," +
                                  "serial_number, customer_sn, carton_no,snolocation_id,snolocation_name,result, cdt, emp_id) " +
                                  "values('" + sDayCode + "'," + sCheckTime + "," + strCurrLocationID.Replace("'", "''") + ",'" + strCurrLocationNO.Replace("'", "''") +
                                  "','" + sPallet_no + "','" + serial_number + "','" + customer_sn + "','" + carton_no + "','" + snolocation_id +
                                  "','" + snolocation_no +"','" + result + "',sysdate," + g_sUserID + ")";
              ClientUtils.ExecuteSQL(strSQL);
            }
            return true;
        }

        //需要结束作业时，
        private bool checkSave()
        {
            int checkSum = dgvCheckSum.Rows.Count;
            int checkDetail = dgvDetail.Rows.Count;
            return true;

        }


        /// <summary>
        /// 程式执行信息显示
        /// </summary>
        /// <param name="strTxt"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt;
            switch (strType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Silver;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Green;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(dgvDetail.Rows.Count.ToString());
        }

        private void dgvDetail2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
            int rs = dgvDetail2.Rows.Count;

            for (int i = 0; i < rs; i++)
            {
                this.dgvDetail2.Rows[i].HeaderCell.Value = (rs - i).ToString().Trim();
            }
            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void rdoSN_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSN.Checked)
            {
                lblQTY.Visible = false;
                txtQTY.Visible = false;
                lblSN2.Visible = false;
                txtSN2.Visible = false;
                btnQTYCheck.Visible = false;
            }
            else
            {
                lblQTY.Visible = true;
                txtQTY.Visible = true;
                lblSN2.Visible = true;
                txtSN2.Visible = true;
                btnQTYCheck.Visible = true;
            }
        }

        private void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void dgvLocation_SelectionChanged(object sender, EventArgs e)
        {
            if (rdoQTY.Checked)
            {
                Int32 rowIndex = 0;
                try
                {
                    rowIndex = dgvLocation.CurrentRow.Index;
                }
                catch (Exception)
                {
                    return;
                }
                if (dgvLocation.CurrentRow.Index >= 0)
                {
                    //if (g_curRow == rowIndex)
                    //    return;
                    g_curRow = rowIndex;
                    txtSN.Text = dgvLocation.Rows[rowIndex].Cells["pallet_no"].Value.ToString();
                }
            }
        }
        private void EmptyData()
        {
            this._lstCheckDuplicate = new List<string>();
        }
    }
}
