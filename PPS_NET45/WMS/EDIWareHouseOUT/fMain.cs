using ClientUtilsDll;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Oracle.ManagedDataAccess.Client;
using SajetClass;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EDIWareHouseOUT
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        string StrData;
        //保存查询出来的PickList用于过滤
        string StrIniFile = Application.StartupPath + "\\sajet.ini";
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sUserID = ClientUtils.UserPara1;
        private string g_ServerIP = ClientUtils.url;
        public int H = 0;

        //出库类型
        public string strWHOutType = "";
        string strSAPid = string.Empty;
        string strSTATUS = string.Empty;
        string strStime = string.Empty;
        string strEtime = string.Empty;

        private Int32 g_curRow = -1;
        private Int32 g_curRow2 = -1;

        EDIWarehouseOUTBLL wb = new EDIWarehouseOUTBLL();

        /// <summary>
        /// 设置文本框美式键盘
        /// </summary>
        private void SeachTxt()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    c.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
                if (c is ComboBox)
                {
                    c.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
            }
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            //HYQ:fLoad()  设定了panel2.Size
            fLoad();
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            SeachTxt();
            DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            dt_start.Value = dateTimeNow.AddDays(-1);
            dt_end.Value = dateTimeNow.AddDays(1);
            this.WindowState = FormWindowState.Maximized;
            SajetInifile sajetInifile1 = new SajetInifile();
            StrData = sajetInifile1.ReadIniFile(StrIniFile, "System", "Data", "Prod").ToLower();



            //填充出库信息
            string strSql = string.Format(@"
                  select a.para_type id, 
                        decode(a.para_type,'ZSF','杂收发出库','ZTL','转仓出库','ZBOMR','工单领退出库') name
                   from ppsuser.t_basicparameter_info a
                  where a.enabled = 'Y'
                    and a.remark in ('SAP出库')
                  order by a.para_type desc
                ");

            fillCmb(strSql, "", cmbWHOutType);

            //填充单号状态信息
            string strSql2 = string.Format(@"
                select 'ALL' id ,'ALL' name from dual
                union 
                select 'WP' id ,'WP-未开始' name from dual
                union 
                select 'IP' id ,'IP-作业中' name from dual
                union 
                select 'FP' id ,'FP-已完成' name from dual
                union 
                select 'CP' id ,'CP-CANCEL' name from dual
                union
                select 'HO' id ,'HO-HOLD' name from dual
                ");
            fillCmb(strSql2, "", cmbSTATUS);

        }

        private void fLoad()
        {
            H = Screen.PrimaryScreen.Bounds.Height;
            if (H >= 1080)
            {
                H = Convert.ToInt32(H * 0.30);
            }
            else
            {
                H = Convert.ToInt32(H * 0.22);

            }
            this.panel2.Size = new System.Drawing.Size(1300, H);
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            strSAPid = cmbSAPid.Text;
            strSTATUS = cmbSTATUS.SelectedValue.ToString();
            strStime = dt_start.Value.ToString("yyyy-MM-dd");
            string strEtime = dt_end.Value.ToString("yyyy-MM-dd");
            btnSearch.Enabled = false;

            dgvNo.DataSource = null;
            dgvNo.Rows.Clear();
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "SearchSAPNOSQL Start", strWHOutType+"*"+strSAPid + "*" + strSTATUS + "*" + strStime + "*" + strEtime);
            DataTable dtSAPIDList = wb.GetSAPIDDataTable(strWHOutType, strSAPid, strSTATUS, strStime, strEtime);
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "SearchSAPNOSQL End", strWHOutType + "*" + strSAPid + "*" + strSTATUS + "*" + strStime + "*" + strEtime);

            dgvNo.DataSource = dtSAPIDList;
            refreshCmbbox();
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "SearchSAPNO End", strWHOutType + "*" + strSAPid + "*" + strSTATUS + "*" + strStime + "*" + strEtime);

            ShowMsg("",0);
            btnSearch.Enabled = true;
        }

        private void refreshCmbbox()
        {
            cmbSAPid.Items.Clear();
            cmbSAPid.Items.Add("ALL");
            //cmbRegion.Items.Clear();
            //cmbRegion.Items.Add("-ALL-");

            for (int i = 0; i < dgvNo.RowCount; i++)
            {
                string strSapIdStatus = dgvNo.Rows[i].Cells["status"].Value.ToString();
                string strSapId = dgvNo.Rows[i].Cells["sap_no"].Value.ToString();
                if (strSapIdStatus.Contains("WP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else if (strSapIdStatus.Contains("IP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (strSapIdStatus.Contains("FP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                if (!cmbSAPid.Items.Contains(strSapId))
                {
                    cmbSAPid.Items.Add(strSapId);
                }
            }

        }



        private void cmbWHOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            strWHOutType = "";
            strWHOutType = cmbWHOutType.SelectedValue.ToString();

        }


        private void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            if (dts.Tables[0].Rows.Count > 0)
            {

                //return DtbPickList;
                //绑定单据号
                //List<string> shipmentList = (from d in dts.Tables[0].AsEnumerable()
                //                             select d.Field<string>(colName)
                //                           ).Distinct()
                //                           .ToList();
                //shipmentList.Add("");
                //shipmentList.Sort();
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";

            }
            else
            {
                cmb.Items.Clear();
            }
            //selecttxtCarton();
        }

        private void dgvNo_SelectionChanged(object sender, EventArgs e)
        {
            dgvPickNO.DataSource = null;
            dgvStock.DataSource = null;

            //集货清单选中的事件
            //1. 更新库存清单显示逻辑
            //2. 更新需作业集货单号、栈板号等显示内容
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvNo.CurrentRow.Index;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvNo.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                //if (g_curRow == rowIndex)
                //    return;
                g_curRow = rowIndex;
                

                string strSapId = dgvNo.Rows[g_curRow].Cells["sap_no"].Value.ToString();

                txtSapId.Text = strSapId;
                ShowMsg("",0);
                //2 更新作业显示内容
                //pb.ShowStockInfo(setPart, dgvStock);
                //给dgvPickNO写值, 刷新界面
                refresh_dgvPickNew(strSapId, strWHOutType);
                reflash_dgvWaitFBMESCarton(strSapId);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            TextMsg.Text = "";

            //1. 检查
            //获得电脑名
            string localHostname = "";
            try
            {
                localHostname = System.Environment.MachineName;
            }
            catch (Exception ex)
            {
                ShowMsg("获取电脑名异常" + ex.ToString(), 0);
                return;
            }
            //检查必须要选择一个集货单号
            if (string.IsNullOrEmpty(txtSapId.Text.Trim()))
            {
                ShowMsg("请选择一个需作业的SAP单号。", 0);
                return;
            }

            string strSapNo = txtSapId.Text;
            string strResultOUT = string.Empty;
            string strRB = wb.WmsOCheckSap(strSapNo, strWHOutType, localHostname, out strResultOUT);
            if (strRB.Equals("OK"))
            {
                txtCarton.Enabled = false;
                txtCarton.Focus();
            }
            else if (strRB.Equals("NG"))
            {

                ShowMsg(strResultOUT, 0);
                return;
            }
            else if (strRB.Equals("WA"))
            {
                DialogResult strResult = MessageBox.Show(strResultOUT.Substring(3) + ",是否继续作业？", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (strResult == DialogResult.No)
                {
                    return;
                }
            }



            //给dgvPickNO写值, 刷新界面
            refresh_dgvPickNew(strSapNo, strWHOutType);
            reflash_dgvWaitFBMESCarton(strSapNo);

            /// 2.锁定程式界面上半部分的功能>> 按钮失效;datagriadview 选择index 事件失效;...
            btnSearch.Enabled = false;
            btnClsFace.Enabled = false;
            this.dgvNo.SelectionChanged -= new System.EventHandler(this.dgvNo_SelectionChanged);

            txtPickSapID.Text = "";
            txtCarton.Enabled = true;
            txtCarton.Text = "";
            txtCarton.SelectAll();
            txtCarton.Focus();
            /// 3.【结束作业】 按钮启用。
            btnStart.Enabled = false;
            btnUpload.Enabled = false;
            btnEnd.Enabled = true;
            txtCarton2.Enabled = false;



        }

        private void refresh_dgvPickNew(string strSapNo ,string strWHOutType)
        {
            dgvPickNO.DataSource = null;
            dgvPickNO.Rows.Clear();
            DataTable pickdt = wb.GetSAPNOLINEINFO(strSapNo,strWHOutType);
            int sumsapqty = 0;
            int sumpickqty = 0;
            if (pickdt !=null)
            {
                bool isselectok = false;

                //dgvNo.Rows[0].Selected = true;
                for (int i = 0; i < pickdt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPickNO.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = pickdt.Rows[i]["SAP_NO"]?.ToString();
                    dr.Cells[1].Value = pickdt.Rows[i]["LINE_ITEM"]?.ToString();
                    dr.Cells[2].Value = pickdt.Rows[i]["PART_NO"]?.ToString();
                    dr.Cells[3].Value = pickdt.Rows[i]["PART_DESC"]?.ToString();
                    dr.Cells[4].Value = pickdt.Rows[i]["QTY"]?.ToString();
                    dr.Cells[5].Value = pickdt.Rows[i]["PICK_QTY"]?.ToString();
                    dr.Cells[6].Value = pickdt.Rows[i]["STATUS"]?.ToString();
                    dr.Cells[7].Value = pickdt.Rows[i]["LOCATION_NO"]?.ToString();
                    dr.Cells[8].Value = pickdt.Rows[i]["PART_BATCH"]?.ToString();
                    dr.Cells[9].Value = pickdt.Rows[i]["PART_VERSION"]?.ToString();

                    if (pickdt.Rows[i]["STATUS"].ToString().Contains("WP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.White;
                    }
                    else if (pickdt.Rows[i]["STATUS"].ToString().Contains("IP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (pickdt.Rows[i]["STATUS"].ToString().Contains("FP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Green;
                    }
                    try
                    {
                        dgvPickNO.Invoke((MethodInvoker)delegate ()
                        {
                            dgvPickNO.Rows.Add(dr);

                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                    sumsapqty += Convert.ToInt32(dr.Cells["QTY"].Value);
                    sumpickqty += Convert.ToInt32(dr.Cells["PICK_QTY"].Value);

                    if (!isselectok && !(pickdt.Rows[i]["STATUS"].ToString().Contains("FP")))
                    {
                        try
                        {
                            dgvPickNO.Rows[i].Selected = true;
                            dgvPickNO.FirstDisplayedScrollingRowIndex = i;
                            isselectok = true;
                            wb.ShowPalletStockInfo(strSapNo, dgvStock);
                        }
                        catch (Exception )
                        {
                            continue;
                        }
                    }
                }
                labqty.Text = sumpickqty.ToString() + "/" + sumsapqty.ToString();
                labqty.Refresh();
            }
            else
            {
                labqty.Text = "00/00";
            }

        }

        private void refresh_dgvPick2New(string strSapNo,string strCartonNo, string strWHOutType)
        {

            this.dgvPickNO.SelectionChanged -= new System.EventHandler(this.dgvPickNO_SelectionChanged);
            dgvPickNO.DataSource = null;
            dgvPickNO.Rows.Clear();
            DataTable pickdt = wb.GetSAPNOLINEINFO(strSapNo, strWHOutType);
            int sumsapqty = 0;
            int sumpickqty = 0;

            DataTable cartondt = wb.GetCartonPartInfo(strCartonNo);
            string strCartonPart = string.Empty;
            try
            {
                strCartonPart = cartondt.Rows[0]["part_no"].ToString();
            }
            catch (Exception e)
            {

            }
            if (pickdt != null)
            {
                bool isselectok = false;

                //dgvNo.Rows[0].Selected = true;
                for (int i = 0; i < pickdt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPickNO.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = pickdt.Rows[i]["SAP_NO"]?.ToString();
                    dr.Cells[1].Value = pickdt.Rows[i]["LINE_ITEM"]?.ToString();
                    dr.Cells[2].Value = pickdt.Rows[i]["PART_NO"]?.ToString();
                    dr.Cells[3].Value = pickdt.Rows[i]["PART_DESC"]?.ToString();
                    dr.Cells[4].Value = pickdt.Rows[i]["QTY"]?.ToString();
                    dr.Cells[5].Value = pickdt.Rows[i]["PICK_QTY"]?.ToString();
                    dr.Cells[6].Value = pickdt.Rows[i]["STATUS"]?.ToString();
                    dr.Cells[7].Value = pickdt.Rows[i]["LOCATION_NO"]?.ToString();
                    dr.Cells[8].Value = pickdt.Rows[i]["PART_BATCH"]?.ToString();
                    dr.Cells[9].Value = pickdt.Rows[i]["PART_VERSION"]?.ToString();

                    if (pickdt.Rows[i]["STATUS"].ToString().Contains("WP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.White;
                    }
                    else if (pickdt.Rows[i]["STATUS"].ToString().Contains("IP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (pickdt.Rows[i]["STATUS"].ToString().Contains("FP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Green;
                    }
                    try
                    {
                        dgvPickNO.Invoke((MethodInvoker)delegate ()
                        {
                            dgvPickNO.Rows.Add(dr);
                            //if (strCartonPart.Equals(dr.Cells["PART_NO"].Value.ToString()))
                            //{
                            //    dgvPickNO.Rows[i].Selected = true;
                            //    dgvPickNO.FirstDisplayedScrollingRowIndex = i;
                            //}

                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                    sumsapqty += Convert.ToInt32(dr.Cells["QTY"].Value);
                    sumpickqty += Convert.ToInt32(dr.Cells["PICK_QTY"].Value);

                   // if (!isselectok && !(pickdt.Rows[i]["STATUS"].ToString().Contains("FP")))
                    if (!isselectok && strCartonPart.Equals(dr.Cells["PART_NO"].Value.ToString()))
                    {
                        try
                        {
                            dgvPickNO.Rows[i].Selected = true;
                            dgvPickNO.FirstDisplayedScrollingRowIndex = i;
                            isselectok = true;
                            wb.ShowPalletStockInfo(strSapNo, dgvStock);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                labqty.Text = sumpickqty.ToString() + "/" + sumsapqty.ToString();
                labqty.Refresh();
            }
            else
            {
                labqty.Text = "00/00";
            }

        }






        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime dateStart = DateTime.Now;
            //可扫描序号/箱号/栈板号进行Pick
            string strCarton = txtCarton.Text.Trim();
            strCarton = wb.DelPrefixCartonSN(strCarton);

            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                //LibHelper.MediasHelper.PlaySoundAsyncByNg();
                //this.ShowMsg("输入的/PalletID/SN/Carton不能为空！", 0);
                txtCarton.SelectAll();
                txtCarton.Focus();
                return;
            }
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS Start", "CARTONNO:"+ strCarton);

            DataTable dt0 = wb.GetSNInfoDataTableBLL(strCarton);
            if (dt0 == null)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("输入非法无效的序号或者箱号，不做统计。", 0);
                txtCarton.Text = "";
                txtCarton.Focus();
                return;
            }

            strCarton = dt0.Rows[0]["carton_no"].ToString();
            bool IsHold = false;
            for (int i =0;i< dt0.Rows.Count;i++) 
            {
                string strHold = string.Empty;
                if(dt0.Rows[0]["hold_flag"] !=null)
                {
                    strHold = dt0.Rows[0]["hold_flag"]?.ToString();
                    if (strHold.Equals("Y"))
                    {
                        IsHold = true;
                    }
                }
                
            }
            if (IsHold && chkHold.Checked) 
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();

                DialogResult strResultA = MessageBox.Show("栈板号:" + strCarton + ",存在HOLD，是否出库？", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                if (strResultA == DialogResult.No)
                {
                    txtCarton.SelectAll();
                    label3.Focus();
                    return;
                }
                txtCarton.Focus();
            }
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS QHOLDCheck End", "CARTONNO:" + strCarton);

            TimeSpan ts0 = DateTime.Now.Subtract(dateStart).Duration();
            string strTest = dateStart.ToString("HHmmss.fff") + "执行insertpickpallet前" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts0.Seconds.ToString() + "*" + ts0.Milliseconds.ToString();

            string strSapNo = txtSapId.Text.Trim();
            string strPickSapNoA = txtPickSapID.Text.Trim();
            string strPickSapNO = "";
            string strLBL = "";
            string strResult = string.Empty;
            string strRetMsg = string.Empty;
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS WmsOInsertCarton Start", strWHOutType+"*"+strPickSapNoA + "*" +  strPickSapNO + "*" + strSapNo + "*" + strCarton + "*" + g_sUserNo + "*" + g_ServerIP);
            strResult =wb.WmsOInsertCarton(strWHOutType,strPickSapNoA, out strPickSapNO, strSapNo, strCarton, g_sUserNo, g_ServerIP, out strRetMsg, out strLBL);
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS WmsOInsertCarton End", strWHOutType + "*" + strPickSapNoA + "*" + strPickSapNO + "*" + strSapNo + "*" + strCarton + "*" + g_sUserNo + "*" + g_ServerIP);

            txtPickSapID.Text = strPickSapNO;
            txtPickSapID.Refresh();
            TimeSpan ts1 = DateTime.Now.Subtract(dateStart).Duration();
            strTest += "\r\n执行insertpickpallet后" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts1.Seconds.ToString() + "*" + ts1.Milliseconds.ToString();
            string strF = string.Empty;
            #region
            if (strResult.StartsWith("OK"))
            {
                this.labqty.Text = strLBL;
                labqty.Refresh();
                Ok();
                txtCarton.Focus();
                btnStart.Enabled = false;
            }
            else if (strResult.Contains("NG"))
            {
                ShowMsg(strRetMsg, 0);
                if (strRetMsg.Contains("重复")) 
                { Re(); }
                else { Ng(); }
                
                txtCarton.SelectAll();
                btnStart.Enabled = false;
                txtCarton.Focus();
                return;
            }
            else if (strResult.Contains("FINISH"))
            {
                strF = "F";
                labqty.Text = strLBL;
                labqty.Refresh();
                
                
                string strPickpalletno = txtPickSapID.Text.Trim();
                if (string.IsNullOrEmpty(strPickpalletno))
                {
                    ShowMsg("PickPalletNO 不能为空", 0);
                    return;
                }
                
                Ok();
                if (!string.IsNullOrEmpty(strPickpalletno)&& chkPrint.Checked)
                {
                    string strMsgOut = string.Empty;
                    if (!PrintPalletLabel(strPickpalletno, 14, out strMsgOut))
                    {
                        ShowMsg("打印失败" + strMsgOut, 0);
                    }
                    else
                    {
                        ShowMsg("打印OK", 0);
                    }

                    //DataTable dt = wb.GetPickPrintInfo(strPickpalletno);

                    //if (dt != null)
                    //{
                    //    string strPrintContext = string.Empty;

                    //    string strSAPNO2 = dt.Rows[0]["sapno"]?.ToString();
                    //    string strPickSapNo2 = dt.Rows[0]["picksapno"]?.ToString();
                    //    string strPickQty = dt.Rows[0]["pickqty"]?.ToString();
                    //    string strTotalQty = dt.Rows[0]["totalqty"]?.ToString();

                    //    strPrintContext = strSAPNO2 + "|" + strPickSapNo2 + "|" + strPickQty + "|" + strTotalQty;
                    //    PrintPickSAPNOLabel(strPrintContext);
                    //}

                }
                wb.ShowPalletStockInfo(strSapNo, dgvStock);
                //调用反馈SAP的接口
                //refresh_dgvPickNew(strSapNo, strWHOutType);
                #region

                //string strResult0 = string.Empty;
                //string strResultOut = string.Empty;
                //strResult0 = wb.WMSOUplodSapNoWebService(strSapNo, strPickSapNO, strWHOutType, g_sUserNo, g_ServerIP, out strResultOut);
                //if (strResult0.Equals("OK"))
                //{
                //    ShowMsg("出库OK，SAP处理OK", -1);
                //    endprintaction();
                //}
                //else
                //{
                //    ShowMsg(strResultOut, 0);
                //    txtCarton.Text = "";
                //    btnStart.Enabled = false;
                //    return;
                //}

                #endregion

            }
            else
            {
                ShowMsg("检查SN或者CARTONNO获得特殊异常", 0);
                txtCarton.Text = "";
                return;
            }
            #endregion
            //refresh_dgvPickNew(strSapNo, strWHOutType);
            refresh_dgvPick2New(strSapNo, strCarton, strWHOutType);
            reflash_dgvWaitFBMESCarton(strSapNo);
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS End", "CARTONNO:" + strCarton);
            txtCarton.Text = "";
            txtCarton.Focus();
            
        }
        private void endprintaction()
        {
            /// 3.锁定程式界面上半部分的功能>> 按钮失效;datagriadview 选择index 事件失效;...
            btnSearch.Enabled = true;
            btnClsFace.Enabled = true;

            this.dgvNo.SelectionChanged += new System.EventHandler(this.dgvNo_SelectionChanged);
            txtCarton.Enabled = false;
            txtCarton.Text = "";
            txtPickSapID.Text = "";
            /// 4.【开始作业】 按钮启用。
            btnStart.Enabled = true;
            btnUpload.Enabled = true;
            btnEnd.Enabled = false;
            txtCarton2.Enabled = true;
        }
        /// <summary>
        /// 错误声音
        /// </summary>
        public void Ng()
        {
            LibHelper.MediasHelper.PlaySoundAsyncByNg();
        }

        /// <summary>
        /// 正确声音
        /// </summary>
        public void Ok()
        {
            LibHelper.MediasHelper.PlaySoundAsyncByOk();
        }
        public void Re()
        {
            LibHelper.MediasHelper.PlaySoundAsyncByRe();
        }
        /// <summary>
        /// Message 信息
        /// </summary>
        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
            switch (strType)
            {
                case 0: //Error    
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.White;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void btnClsFace_Click(object sender, EventArgs e)
        {
            btnClsFace.Enabled = false;
            try
            {
                btnStart.Enabled = true;
                clearCmb();
                clearDgv();
                clearTxt();
                labqty.Text = "00/00";
                this.ShowMsg("清空成功", -1);
                Ok();
            }
            catch (Exception ex)
            {
                Ng();
                MessageBox.Show(ex.Message);
            }
            btnClsFace.Enabled = true;
        }

        private void clearCmb()
        {
            //this.dt_start.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            //this.dt_end.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cmbSAPid.Text = "ALL";
            //cmbCarrier2.Text = "";
        }
        /// <summary>
        /// dgv清空
        /// </summary>
        private void clearDgv()
        {
            this.dgvNo.DataSource = null;
            this.dgvStock.DataSource = null;
            this.dgvPickNO.DataSource = null;
            this.dgvWaitFBMESCarton.DataSource = null;
            dgvNo.Rows.Clear();
            dgvStock.Rows.Clear();
            dgvPickNO.Rows.Clear();
            dgvWaitFBMESCarton.Rows.Clear();
            //DataTable dt = (DataTable)dgvPick.DataSource;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    dt.Clear();
            //    dgvPick.DataSource = dt;
            //}
        }

        /// <summary>
        /// txt清空
        /// </summary>
        private void clearTxt()
        {
            //txtCountry.Text = string.Empty;
            //txtPoe.Text = string.Empty;
            //txtRegion.Text = string.Empty;
            txtSapId.Text = string.Empty;
            txtCarton.Text = string.Empty;
            txtPickSapID.Text = string.Empty;
            txtSapId.ReadOnly = true;
            //txtRegion.ReadOnly = true;
            //txtCountry.ReadOnly = true;
            //txtPoe.ReadOnly = true;
            txtSapId.BackColor = System.Drawing.Color.Silver;
            //txtRegion.BackColor = System.Drawing.Color.Silver;
            //txtCountry.BackColor = System.Drawing.Color.Silver;
            //txtPoe.BackColor = System.Drawing.Color.Silver;
            txtCarton.BackColor = System.Drawing.SystemColors.Window;
            //txtLOCATION_NO.BackColor = System.Drawing.SystemColors.Window;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            string strSapId =txtSapId.Text.Trim();
            string strPickSapNo = txtPickSapID.Text.Trim();

            if (!string.IsNullOrEmpty(strPickSapNo))
            {
                if (strPickSapNo.Substring(1, 1).Equals("9"))
                {
                    //这么写不好，再改改。
                    ShowMsg("9号pickpallet,必须拣货完成自动打印", 0);
                    return;
                }

                /// 2.解锁电脑名与栈板的绑定。 改成SP ppsuser.SP_PICK_UNLOCKCOMPUTERNAME(palletno  in varchar2,timelimit in varchar2,errmsg    out varchar2)
                //--timelimit 如果是N，就是没有时间限制，栈板号来了就能解除电脑， 
                //--          如果是Y，就是有时间限制， UDT 在当前时间点 前一天 或者一小时 才能解锁电脑。

                //string strResult0 = string.Empty;
                //string strResultOut = string.Empty;
                //strResult0 = wb.WMSOUplodSapNoWebService(strSapId, strPickSapNo, strWHOutType, g_sUserNo, g_ServerIP, out strResultOut);
                //if (strResult0.Equals("OK"))
                //{
                //    ShowMsg("出库OK，SAP处理OK", -1);

                //}
                //else
                //{
                //    ShowMsg(strResultOut, 0);
                //    return;
                //}
            }

            string strRetMsg = string.Empty;
            string strResult = wb.WmsOUnlockComputer(strSapId, strWHOutType, out strRetMsg);
            object[][] procParams = new object[3][];
            
            if (strResult.Contains("OK"))
            {
                txtCarton.Enabled = false;
                txtCarton.Focus();

            }
            else if (strResult.Contains("NG"))
            {
                ShowMsg(strRetMsg, 0);
                return;
            }
            else
            {
                ShowMsg("处理palletno遇到特殊异常", 0);
                return;
            }

            if (!string.IsNullOrEmpty(strPickSapNo)&& chkPrint.Checked) 
            {
                string strMsgOut = string.Empty;
                if (!PrintPalletLabel(strPickSapNo, 14, out strMsgOut))
                {
                    ShowMsg("打印失败" + strMsgOut, 0);
                }
                else
                {
                    ShowMsg("打印OK", 0);
                }
                //DataTable dt = wb.GetPickPrintInfo(strPickSapNo);

                //if (dt != null)
                //{
                //    string strPrintContext = string.Empty;

                //    string strSAPNO2 = dt.Rows[0]["sapno"]?.ToString();
                //    string strPickSapNo2 = dt.Rows[0]["picksapno"]?.ToString();
                //    string strPickQty = dt.Rows[0]["pickqty"]?.ToString();
                //    string strTotalQty = dt.Rows[0]["totalqty"]?.ToString();
                //    strPrintContext = strSAPNO2 + "|" + strPickSapNo2 + "|" + strPickQty + "|" + strTotalQty;
                //    PrintPickSAPNOLabel(strPrintContext);
                //}

            }
            endprintaction();
        }
        private void PrintPickSAPNOLabel(string strPrintContext) 
        {
            string strLabelName = @"WMSO_Pick_Label";
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\WMS\Label";
            //这部分是写.dat文件。
            string LabelParam = @"SAPNO|PICKSAPNO|PICKQTY|TOTALQTY|";
            string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".lst";
            if (File.Exists(str7))
            {
                File.Delete(str7);
            }
            //string strPrintContext = string.Empty;
            strPrintContext = LabelParam + "\r\n" + strPrintContext;
            this.WriteToPrintGo(str7, strPrintContext);

            
            string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
            
            using (Process p = new Process())
            {
                if (!File.Exists(strSampleFile))
                {
                    ShowMsg("Sample File Not exists-" + strSampleFile, 0);
                    return;
                }
                p.StartInfo.FileName = "bartend.exe";
                string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName  + ".lst" + '"').Replace("@QTY", "1");
                p.StartInfo.Arguments = sArguments;
                p.Start();
                p.WaitForExit();
            }

        }
        public bool PrintPalletLabel(string strPickSapNo, int listrows,out string sMessage)
        {
            //---------------------
            sMessage = "";
            string strLabelName = string.Empty;
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\WMS\Label";

            string strSAWBerrmsg = string.Empty;
            string strRegion = string.Empty;
           
            listrows = 14;
            strLabelName = @"WMSO_Pick_Label2";
            

            
            //CURPAGE  TOTALPAGE  一个栈板MIX多少，打印就有多少行，依据label能打印的最大行数分页，打印palletloadingsheet
            string mixTotalSelect = string.Empty;

            DataTable dt_mixTotal = new DataTable();

            DataTable dt = wb.GetPickPrintInfo(strPickSapNo);
            string strPrintContext = string.Empty;
            if (dt != null)
            {
                string strSAPNO2 = dt.Rows[0]["sapno"]?.ToString();
                string strPickSapNo2 = dt.Rows[0]["picksapno"]?.ToString();
                string strPickQty = dt.Rows[0]["pickqty"]?.ToString();
                string strTotalQty = dt.Rows[0]["totalqty"]?.ToString();
                strPrintContext = strSAPNO2 + "|" + strPickSapNo2 + "|" + strPickQty + "|" + strTotalQty ;

            }

            dt_mixTotal = wb.GetPickPrintInfo2(strPickSapNo);
            if (dt_mixTotal==null) 
            {
                sMessage = "箱号无对应出库单资料。";
                return false;
            }
            if (dt_mixTotal.Rows.Count > 0)
            {
                //确定打印的label的张数palletcount， 定义个list的行数datarows，
                //mix的所有行数
                int mixTotalrow = dt_mixTotal.Rows.Count;
                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));
                //HYQ： 这部分是写.dat文件。
                string LabelParam = @"SAPNO|PICKSAPNO|PICKQTY|TOTALQTY|CURPAGE|TOTALPAGE|ICTPN1|MPN1|QTY1|ICTPN2|MPN2|QTY2|ICTPN3|MPN3|QTY3|ICTPN4|MPN4|QTY4|ICTPN5|MPN5|QTY5|ICTPN6|MPN6|QTY6|ICTPN7|MPN7|QTY7|ICTPN8|MPN8|QTY8|ICTPN9|MPN9|QTY9|ICTPN10|MPN10|QTY10|ICTPN11|MPN11|QTY11|ICTPN12|MPN12|QTY12|ICTPN13|MPN13|QTY13|ICTPN14|MPN14|QTY14|";

                //label上唯一值的部分  REF 现在不定义 以后再补
                //   SAPNO|PICKSAPNO|PICKQTY|TOTALQTY|CURPAGE|TOTALPAGE 
                string strHead = "";

                //label上清单值的部分
                //   |ICTPN1|MPN1|QTY1|ICTPN2|MPN2|QTY2|ICTPN3|MPN3|QTY3|ICTPN4|MPN4|QTY4|ICTPN5|MPN5|QTY5|ICTPN6|MPN6|QTY6|ICTPN7|MPN7|QTY7|ICTPN8|MPN8|QTY8|ICTPN9|MPN9|QTY9|ICTPN10|MPN10|QTY10|ICTPN11|MPN11|QTY11|ICTPN12|MPN12|QTY12|ICTPN13|MPN13|QTY13|ICTPN14|MPN14|QTY14|
         
                string strLine = "";

                //HYQ：这循环里面只是产生.lst文件， 如果打印5张就产生5个文档。 
                //.lst 的内容是 一行栏位名 来源于.dat文件strItemno,加换行， 再加已将数据，数据部分是由strHead + strLine。
                //HYQ：20190801如果是ALL，就产生一个文档打印，快
                string strPalletList = string.Empty;
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    string[] strHeadArr = new string[TOTALPAGE];
                    string[] strLineArr = new string[TOTALPAGE];
                    string[] strAllArr = new string[TOTALPAGE];

                    //确定这些值
                    //    SAPNO|PICKSAPNO|PICKQTY|TOTALQTY|CURPAGE|TOTALPAGE 

                   
                    //因i从0开始，所以加1
                    string strcurpage = (i + 1).ToString();

                    strHead = "";
                    strHead = strPrintContext + "|"+strcurpage + "|" +TOTALPAGE + "|" ;
                    strHeadArr[i] = strHead;

                    //确定以下的部分 循环
                    //    |ICTPN1|MPN1|QTY1
                    strLine = "";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dt_mixTotal.Rows.Count)
                        {
                            break;
                        }

                        string strictpn = dt_mixTotal.Rows[j]["ictpn"].ToString();
                        string strmpn = dt_mixTotal.Rows[j]["mpn"].ToString();
                        string strqty = dt_mixTotal.Rows[j]["partpickqty"].ToString();
                       
                        strLine = strLine +strictpn + "|" +strmpn + "|" +strqty + "|" 
                                  ;
                    }
                    strLineArr[i] = strLine;
                  
                    strAllArr[i] = LabelParam + "\r\n" + strHeadArr[i] + strLineArr[i];

                    strPalletList = strPalletList + strHeadArr[i] + strLineArr[i] + "\r\n";

                    //HYQ： 以下3行不一定会用
                    //strHead = getPalletLabelHeadData(strpalletno);
                    //strLine = getPalletLabelLineData(strpalletno, i);
                    //strAll = LabelParam + "\r\n" + strHead + strLine;

                    string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i + ".lst";
                    if (File.Exists(str7))
                    {
                        File.Delete(str7);
                    }
                    this.WriteToPrintGo(str7, strAllArr[i]);
                }

                strPalletList = LabelParam + "\r\n" + strPalletList;
                string strPalletfile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + "ALL.lst";
                if (File.Exists(strPalletfile))
                {
                    File.Delete(strPalletfile);
                }
                this.WriteToPrintGo(strPalletfile, strPalletList);

                
                //一次打印所有的
                using (Process p = new Process())
                {
                    string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                    if (!File.Exists(strSampleFile))
                    {
                        sMessage = "Sample File Not exists-" + strSampleFile;
                        return false;
                    }
                    p.StartInfo.FileName = "bartend.exe";
                    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + "ALL.lst" + '"').Replace("@QTY", "1");
                    p.StartInfo.Arguments = sArguments;
                    p.Start();
                    p.WaitForExit();
                }
                return true;
                
                
            }
            else
            {
                return false;
            }
        }
        private void WriteToPrintGo(string sFile, string sData)
        {
            try
            {
                if (File.Exists(sFile))
                {
                    File.Delete(sFile);
                }
                File.AppendAllText(sFile, sData, Encoding.Default);
            }
            finally
            {
            }
        }
   

        private void dgvPickNO_SelectionChanged(object sender, EventArgs e)
        {
            
            //1. 更新库存清单显示逻辑
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvPickNO.CurrentRow.Index;
                //rowIndex = dgvNo.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvPickNO.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                //if (g_curRow2 == rowIndex)
                //    return;
                 g_curRow2 = rowIndex;
                string strSapId = dgvPickNO.Rows[g_curRow2].Cells["SAP_NO"].Value.ToString();
                string strPartNo= dgvPickNO.Rows[g_curRow2].Cells["PART_NO"].Value.ToString();
                string strSapLocationNo= dgvPickNO.Rows[g_curRow2].Cells["LOCATION_NO"].Value.ToString();
                string strBatchNo = dgvPickNO.Rows[g_curRow2].Cells["PART_BATCH"].Value.ToString();

                //2 更新作业显示内容
                if (rdoPart.Checked) { ShowStockInfo(strSapId, strPartNo, strBatchNo, strSapLocationNo, dgvStock); 
                }
                 

            }
        }
        private void ShowStockInfo(string strSapId ,string strPartNo, string strBatchNo, string strSapLocationNo, DataGridView dgv)
        {

            if (rdoPallet.Checked)
            { wb.ShowPalletStockInfo(strSapId,dgv); }
            else
            { wb.ShowPalletPartStockInfo( strPartNo, strBatchNo, strSapLocationNo, dgv); }
        }

        private void btnFBMESCarton_Click(object sender, EventArgs e)
        {
            EDIWarehouseOUTBLL eb = new EDIWarehouseOUTBLL();
            

            if (dgvWaitFBMESCarton.Rows.Count >0) 
            {
                for (int i=0;i< dgvWaitFBMESCarton.Rows.Count;i++) 
                {
                    string strCarton = string.Empty;
                    if (dgvWaitFBMESCarton.Rows[i].Cells["carton_no"]?.Value != null ) 
                    {
                        strCarton = dgvWaitFBMESCarton.Rows[i].Cells["carton_no"]?.Value.ToString();
                        //只有工单入库的 或者有反馈MES的 反馈SAP 才有；这原材没有检查
                        string strProduct = string.Empty;
                        string strResultOut0 = string.Empty;
                        string strResult0 = eb.WmsOCheckCartonFBMES(strCarton, out strProduct, out strResultOut0);
                        if (!strResult0.Equals("OK"))
                        {
                            ShowMsg( strResultOut0,0);
                            return ;
                        }

                        //调用MES接口
                        string strMESResultOUT = string.Empty;
                        LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS 反馈MES Start", strCarton+"*"+strProduct + "*" + g_sUserNo + "*" + g_ServerIP + "*" + strMESResultOUT);
                        string strMESResult = eb.WmsoFBMESWebService(strCarton, strProduct, g_sUserNo, g_ServerIP, out strMESResultOUT);
                        LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS 反馈MES Start", strCarton + "*" + strProduct + "*" + g_sUserNo + "*" + g_ServerIP + "*" + strMESResultOUT);
                        if (!strMESResult.Equals("OK"))
                        {
                            reflash_dgvWaitFBMESCarton(strSAPid);
                            ShowMsg(strMESResultOUT, 0);
                            return;
                        }
                       
                    }
                }
                reflash_dgvWaitFBMESCarton(strSAPid);
                ShowMsg("OK", 0);

            }
        }

        private void reflash_dgvWaitFBMESCarton(string strSapNo) 
        {
            dgvWaitFBMESCarton.DataSource = null;
            dgvWaitFBMESCarton.Rows.Clear();
            DataTable dt = wb.GetWaitFBMESCarton(strSapNo);
            dgvWaitFBMESCarton.DataSource = dt;
        }

      

        private void btnExport_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvPickNO.Rows.Count > 0)
                {
                    ExportExcel(dgvPickNO);
                }
                else
                {
                    this.ShowMsg("确认导出Excel有数据！", 0);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ExportExcel(DataGridView dt)
        {
            //获取导出路径
            string filePath = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "EXCEL 97-2007 工作簿(*.xls)|*.xls";//设置文件类型

            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string currdate = currentTime.ToString("yyyy-MM-dd-HH-mm-ss");
            //HH是24小时制,hh是12小时制

            //sfd.FileName = "wmsReport"+cmbTYPE.Text.Trim()+"_"+cmbLocation.Text.Trim()+"_"+ currdate;//设置默认文件名

            sfd.FileName = "_" + currdate;
            sfd.DefaultExt = "xlsx";//设置默认格式（可以不设）
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
            }
            else
            {
                this.ShowMsg("导出Excel失败！", 0);
            }

            IWorkbook workbook;
            string fileExt = Path.GetExtension(filePath).ToLower();
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                return;
            }
            ISheet sheet = string.IsNullOrEmpty("wmsReport") ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet("wmsReport");

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].HeaderText);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    if (dt.Rows[i].Cells[j].Value != null)
                    {
                        cell.SetCellValue(dt.Rows[i].Cells[j].Value.ToString());
                    }
                    else
                    {
                        cell.SetCellValue("");
                    }

                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
                this.ShowMsg("导出Excel成功！", 5);
            }
        }

       
        private void rdoPallet_CheckedChanged(object sender, EventArgs e)
        {

            if (dgvPickNO.Rows.Count > 0) 
            {
                string strSapNo = dgvPickNO.Rows[0].Cells["SAP_NO"].Value.ToString();
                if (rdoPallet.Checked) { wb.ShowPalletStockInfo(strSapNo, dgvStock); }
                
            }
            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnExport2_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvNo.Rows.Count > 0)
                {
                    ExportExcel(dgvNo);
                }
                else
                {
                    this.ShowMsg("确认导出Excel有数据！", 0);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                TextMsg.SelectAll();
        }

        private void txtCarton2_KeyDown(object sender, KeyEventArgs e)
        {
            string strCarton = txtCarton2.Text.Trim();
            strCarton = wb.DelPrefixCartonSN(strCarton);

            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                //LibHelper.MediasHelper.PlaySoundAsyncByNg();
                //this.ShowMsg("输入的/PalletID/SN/Carton不能为空！", 0);
                txtCarton2.SelectAll();
                txtCarton2.Focus();
                return;
            }
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS Start", "CARTONNO:" + strCarton);

            DataTable dt0 = wb.GetPickSAPNO(strCarton);
            if (dt0 == null)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("输入非法无效的序号或者箱号，不做统计。", 0);
                txtCarton2.Text = "";
                txtCarton2.Focus();
                return;
            }

            string strPickSapNo = dt0.Rows[0]["picksapno"].ToString();
            if (string.IsNullOrEmpty(strPickSapNo))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("箱号没有刷入出库单，对应出库单为空。", 0);
                txtCarton2.Text = "";
                txtCarton2.Focus();
                return;
            }

            string strSAPNO = strPickSapNo.Substring(3);

            if (!string.IsNullOrEmpty(strPickSapNo))
            {
                //获得电脑名
                string localHostname = "";
                try
                {
                    localHostname = System.Environment.MachineName;
                }
                catch (Exception ex)
                {
                    ShowMsg("获取电脑名异常" + ex.ToString(), 0);
                    return;
                }
                string strSAPNOComputerName = wb.GetComputerNameOfSAPNO(strSAPNO);
                if (!string.IsNullOrEmpty(strSAPNOComputerName) && !localHostname.Equals(strSAPNO))
                {
                    ShowMsg("此SAP单正在电脑名:" + strSAPNOComputerName + "上作业不得扣账", 0);
                    return;
                }
                string strMsgOut = string.Empty;
                if (!PrintPalletLabel(strPickSapNo, 14, out strMsgOut))
                {
                    ShowMsg("打印失败" + strMsgOut, 0);
                }
                else 
                {
                    ShowMsg("打印OK" , 0);
                }

                //DataTable dt = wb.GetPickPrintInfo(strPickSapNo);

                //if (dt != null)
                //{
                //    string strPrintContext = string.Empty;

                //    string strSAPNO2 = dt.Rows[0]["sapno"]?.ToString();
                //    string strPickSapNo2 = dt.Rows[0]["picksapno"]?.ToString();
                //    string strPickQty = dt.Rows[0]["pickqty"]?.ToString();
                //    string strTotalQty = dt.Rows[0]["totalqty"]?.ToString();
                //    strPrintContext = strSAPNO2 + "|" + strPickSapNo2 + "|" + strPickQty + "|" + strTotalQty;
                //    PrintPickSAPNOLabel(strPrintContext);
                //}

            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            fReUploadSAP pr = new fReUploadSAP();
            pr.ShowDialog();

        }

        private void btnTEST_Click(object sender, EventArgs e)
        {
            EDIWarehouseOUTBLL eb = new EDIWarehouseOUTBLL();

            //123456private string DateDiff(DateTime DateTime1, DateTime DateTime2) { string dateDiff = null; TimeSpan ts = DateTime1.Subtract(DateTime2).Duration(); dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒"; return dateDiff; }

            //DateTime startdt = DateTime.Now;
            //string strWebPostResult = eb.AfterEdiHttpPostWebService(@"http://10.54.10.15:93/OMSBgSAP/PPSRecZBOMRTest", "AAA");
            //DateTime enddt = DateTime.Now;
            //TimeSpan ts = enddt.Subtract(startdt).Duration();
            //string dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            //MessageBox.Show(strWebPostResult+"#"+ dateDiff);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            btnCheck.Enabled = false;
            #region  shifeng
            //string strSapNo = string.Empty;
            //strSapNo = txtSapId.Text.Trim();
            //if (string.IsNullOrEmpty(strSapNo))
            //{
            //    ShowMsg("SAP单号不能为空", 0);

            //    LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //    btnCheck.Enabled = true;
            //    return;
            //}
            #endregion
            fWmsoCheck fw = new fWmsoCheck();
            //fw.SAPNO = strSapNo;
            fw.ShowDialog();

            btnCheck.Enabled = true;
        }
    }
}
