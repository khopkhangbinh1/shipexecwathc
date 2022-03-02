using ClientUtilsDll;
using EDIWarehouseIN.JSMESWebReference;
using EDIWarehouseIN.WCF;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OperationWCF;
using Oracle.ManagedDataAccess.Client;
using SajetClass;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EDIWarehouseIN.Model.MarinaModel;
using static EDIWarehouseIN.Model.PackoutLogicModel;
using static EDIWarehouseIN.WCF.CommonModel;

namespace EDIWarehouseIN
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            dgvnoa = this.dgvNo;
        }
        public static DataGridView dgvnoa;
        string StrData;
        //保存查询出来的PickList用于过滤
        string StrIniFile = Application.StartupPath + "\\sajet.ini";
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sUserID = ClientUtils.UserPara1;
        private string g_ServerIP = ClientUtils.url;
        public int H = 0;

        string strSTATUS = string.Empty;
        string strStime = string.Empty;
        string strEtime = string.Empty;
        //获得电脑名
        string localHostname = "";
        private Int32 g_curRow = -1;


        private string g_DBTYPE;
        private string g_MarinaUrl;
        private string g_MarinaSite;

        private string g_WMSIMARINA;
        private string g_WMSIPACKOUT;

        //当前选择的栈板号
        string strPALLET = string.Empty;
        //当前选择的车号
        string strTROLLEY = string.Empty;
        //当前选择的栈板料号地区（取得第一笔）
        string strPalletRegion = string.Empty;
        //当前选择的栈板 NPI 还是MP
        string strGoodsType = string.Empty;
        //当前栈板的产品
        string strPalletProduct = string.Empty;
        //当前栈板 料号属于EDI 还是NONEDI
        string strPalletEDIflag = string.Empty;
        //当前栈板是否为转仓 --不需要调用SAP 界面上用不到
        //string strTransferFlag = string.Empty;
        //栈板反馈MES状态
        string strPalletFBMESflag = string.Empty;
        //栈板反馈SAP状态
        string strPalletUPLOADSAPflag = string.Empty;

        //栈板Marina状态
        string strPalletMarinaflag = string.Empty;

        string strPlant = string.Empty;
        string strSloc = string.Empty;
        //当前选择栈板箱数
        int strPalletCartonCount = 0;

        //当前选择栈板 已经check箱数
        int strPalletCartonCheckCount = 0;

        EDIWarehouseINBLL wb = new EDIWarehouseINBLL();
        internal static object paf;
        string strFGinMESWcf = string.Empty;
        string strProduct = string.Empty;
        string strInSnType = string.Empty;


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
        private void fMain2_Load(object sender, EventArgs e)
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
                select 'ER' id ,'ER-ERROR' name from dual
                ");
            fillCmb(strSql2, "", cmbSTATUS);

            //填充仓库信息
            //string strSql = string.Format(@"
            // select a.warehouse_id id, a.db_type|| a.warehouse_no name
            //   from sajet.wms_warehouse a
            //  where a.enabled = 'Y'
            //    and a.warehouse_no <>'SYS'
            //  order by a.warehouse_no
            //    ");
            //  fillCmb(strSql, "warehouse_No", cmbWH);

            try
            {
                localHostname = System.Environment.MachineName;
            }
            catch (Exception ex)
            {
                ShowMsg("获取电脑名异常" + ex.ToString(), 0);
                return;
            }

            //填充产品信息
            string strSql3 = string.Format(@"
                 select a.para_value id, a.para_type name
                   from ppsuser.t_basicparameter_info a
                  where a.enabled = 'Y'
                    and a.remark in ('入库产品')
                    and a.para_type in ('WATCH','AIRPOD')
                  order by a.para_type desc
                ");
            fillCmb(strSql3, "para_type", cmbFGName);

            string strResult = string.Empty;
            string strOutparavalue = string.Empty;
            string strResulterrmsg = string.Empty;
            EDIWarehouseINBLL eb = new EDIWarehouseINBLL();
            strResult = eb.GetDBType("DB_TYPE", out strOutparavalue, out strResulterrmsg);
            g_DBTYPE = strOutparavalue;

            if (!g_DBTYPE.Equals("TEST") )
            {
                //如果是正式库，需要检查Marina Server Url
                string strResulta = string.Empty;
                strResulta = eb.PPSGetbasicparameter("MARINA_URL", out g_MarinaUrl, out strResulterrmsg);
                strResulta = eb.PPSGetbasicparameter("MARINA_SITE", out g_MarinaSite, out strResulterrmsg);
                if (string.IsNullOrEmpty(g_MarinaUrl) || string.IsNullOrEmpty(g_MarinaSite))
                {
                    ShowMsg("MARINASERVER无配置资料，请联系IT", 0);
                }
            }
            //string strWMSIMarinaCheck = string.Empty;
            //strResult = eb.GetDBType("WMSI_MARINA", out strWMSIMarinaCheck, out strResulterrmsg);
            //g_WMSIMARINA = strWMSIMarinaCheck;
            //string strWMSIPackOutCheck = string.Empty;
            //strResult = eb.GetDBType("WMSI_PACKOUT", out strWMSIPackOutCheck, out strResulterrmsg);
            //g_WMSIPACKOUT = strWMSIPackOutCheck;


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
            string strPALLET = string.Empty;
            strPALLET = cmbPallet.Text;
            strSTATUS = cmbSTATUS.SelectedValue.ToString();
            strStime = dt_start.Value.ToString("yyyy-MM-dd HH:mm");
            string strEtime = dt_end.Value.ToString("yyyy-MM-dd HH:mm");
            btnSearch.Enabled = false;

            dgvNo.DataSource = null;
            dgvNo.Rows.Clear();
            DataTable dtSAPIDList = wb.GetWmsiPalletDataTable(strPALLET, strSTATUS, strStime, strEtime);
            dgvNo.DataSource = dtSAPIDList;
            refreshCmbbox();
            ShowMsg("",0);
            btnSearch.Enabled = true;
        }

        private void refreshCmbbox()
        {
            cmbPallet.Items.Clear();
            cmbPallet.Items.Add("ALL");
            //cmbRegion.Items.Clear();
            //cmbRegion.Items.Add("-ALL-");

            for (int i = 0; i < dgvNo.RowCount; i++)
            {
                string strSIDStatus = dgvNo.Rows[i].Cells["pallet_status"].Value.ToString();
                string strSID = dgvNo.Rows[i].Cells["pallet_no"].Value.ToString();
                if (strSIDStatus.Contains("WP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else if (strSIDStatus.Contains("IP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (strSIDStatus.Contains("FP"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                else if (strSIDStatus.Contains("ER"))
                {
                    dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                if (!cmbPallet.Items.Contains(strSID))
                {
                    cmbPallet.Items.Add(strSID);
                }
            }

        }


        private void fillCmb(string strSQL, string colName, ComboBox cmb)
        {
            cmb.DataSource = null; cmb.Items.Clear();
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
                if (cmb.Items.Count>0) 
                {
                    cmb.DataSource = null; cmb.Items.Clear();
                }
               
            }
            //selecttxtCarton();
        }

        private void dgvNo_SelectionChanged(object sender, EventArgs e)
        {
            dgvPickNO.DataSource = null;
            dgvStock.DataSource = null;

            strPalletCartonCount = 0;
            strPalletCartonCheckCount = 0;

            //集货清单选中的事件
            //1. 更新库存清单显示逻辑
            //2. 更新需作业集货单号、栈板号等显示内容
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvNo.CurrentRow.Index;
                //rowIndex = dgvNo.CurrentCell.RowIndex;
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

                strPALLET = dgvNo.Rows[g_curRow].Cells["pallet_no"].Value.ToString();
                strTROLLEY = dgvNo.Rows[g_curRow].Cells["trolley_no"].Value?.ToString();
                strPalletRegion = dgvNo.Rows[g_curRow].Cells["region"].Value?.ToString();
                strGoodsType = dgvNo.Rows[g_curRow].Cells["goodstype"].Value?.ToString();
                strPalletProduct = dgvNo.Rows[g_curRow].Cells["product_name"].Value?.ToString();
                strPalletEDIflag = dgvNo.Rows[g_curRow].Cells["edi_flag"].Value?.ToString();
                strPalletFBMESflag = dgvNo.Rows[g_curRow].Cells["fb_mes_status"].Value?.ToString();
                strPalletUPLOADSAPflag = dgvNo.Rows[g_curRow].Cells["upload_sap_status"].Value?.ToString();
                strPalletMarinaflag= dgvNo.Rows[g_curRow].Cells["marina_status"].Value?.ToString();
                strPlant = dgvNo.Rows[g_curRow].Cells["plant"].Value.ToString();
                strSloc = dgvNo.Rows[g_curRow].Cells["sloc"].Value.ToString();
                txtPalletNO.Text = strPALLET;
                txtTrolleyNO.Text = strTROLLEY;
                txtPalletRegion.Text = strPalletRegion;
                lblGoodsType.Text = strGoodsType;

                wb.ShowpalletCartonInfo(strPALLET,dgvPickNO, strGoodsType);
                lblCheckResult.Text = "0/" + dgvPickNO.Rows.Count.ToString();
                ShowMsg("",0);
                //2 更新作业显示内容
                //pb.ShowStockInfo(setPart, dgvStock);
                if (strPlant != "" && strSloc != "")
                    txtPlantSloc.Text = strPlant + "-" + strSloc;
                else
                    txtPlantSloc.Text = "";
                cmbLocationRegion.SelectedIndex = -1;
                cmbLocation.DataSource = null;
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
            if (string.IsNullOrEmpty(txtPalletNO.Text.Trim()))
            {
                ShowMsg("请选择一个需作业的SAP单号。", 0);
                return;
            }

            string strSID = txtPalletNO.Text;
            string strResultOUT = string.Empty;
            string strRB = "";//wb.NPICheckSID(strSID, localHostname, out strResultOUT);
            if (strRB.Equals("OK"))
            {
                //txtCarton.Enabled = false;
                //txtCarton.Focus();
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
            refresh_dgvPickNew(strSID);

            /// 2.锁定程式界面上半部分的功能>> 按钮失效;datagriadview 选择index 事件失效;...
            btnSearch.Enabled = false;
            btnClsFace.Enabled = false;
            btnGetPallet.Enabled = false;
            this.dgvNo.SelectionChanged -= new System.EventHandler(this.dgvNo_SelectionChanged);

            //txtCarton.Enabled = true;
            //txtCarton.Text = "";
            //txtCarton.SelectAll();
            //txtCarton.Focus();
            /// 3.【结束作业】 按钮启用。
            //btnStart.Enabled = false;
            //btnEnd.Enabled = true;



        }

        private void refresh_dgvPickNew(string strSID )
        {
            dgvPickNO.DataSource = null;
            dgvPickNO.Rows.Clear();
            DataTable pickdt = null;// wb.GetSIDLINEINFO(strSID);
            int sumsapqty = 0;
            int sumpickqty = 0;
            if (pickdt.Rows.Count > 0)
            {
                for (int i = 0; i < pickdt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPickNO.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = pickdt.Rows[i]["shipment_id"].ToString();
                    dr.Cells[1].Value = pickdt.Rows[i]["delivery_no"].ToString();
                    dr.Cells[2].Value = pickdt.Rows[i]["line_item"].ToString();
                    dr.Cells[3].Value = pickdt.Rows[i]["mpn"].ToString();
                    dr.Cells[4].Value = pickdt.Rows[i]["ictpn"].ToString();
                    dr.Cells[5].Value = pickdt.Rows[i]["status"].ToString();
                    dr.Cells[6].Value = pickdt.Rows[i]["qty"].ToString();
                    dr.Cells[7].Value = pickdt.Rows[i]["carton_qty"].ToString();
                    dr.Cells[8].Value = pickdt.Rows[i]["pack_qty"].ToString();
                    dr.Cells[9].Value = pickdt.Rows[i]["pack_carton_qty"].ToString();
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
                    sumpickqty += Convert.ToInt32(dr.Cells["pack_qty"].Value);
                }
                //labqty.Text = sumpickqty.ToString() + "/" + sumsapqty.ToString();
                //labqty.Refresh();
            }
            else
            {
                //labqty.Text = "00/00";
            }
        }



        

        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {

            string strCarton = txtCarton.Text.Trim();
            strCarton = wb.DelPrefixCartonSN(strCarton);

            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的/PalletID/SN/Carton不能为空！", 0);
                txtCarton.SelectAll();
                txtCarton.Focus();
                return;
            }
            if (dgvPickNO.Rows.Count>0) 
            {
                bool Isexist = false;
                for (int j = 0; j < dgvPickNO.Rows.Count; j++)
                {
                    string dgvCarton = dgvPickNO.Rows[j].Cells["CARTON_NO"].Value.ToString();
                    dgvCarton = dgvCarton.Trim();
                    if (dgvCarton.Equals(strCarton))
                    {
                        if (this.dgvPickNO.Rows[j].DefaultCellStyle.BackColor != Color.Yellow)
                        {
                            strPalletCartonCheckCount += 1;
                            lblCheckResult.Text = strPalletCartonCheckCount.ToString() + "/" + dgvPickNO.Rows.Count.ToString();
                            this.dgvPickNO.Rows[j].DefaultCellStyle.BackColor = Color.Yellow;
                            Isexist = true;
                            LibHelper.MediasHelper.PlaySoundAsyncByOk();
                            break;
                        }
                        else
                        {
                            ShowMsg("重复刷入",0);
                            Isexist = true;
                            LibHelper.MediasHelper.PlaySoundAsyncByRe();
                            break;
                        }
                    }
                }
                if(!Isexist)
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.ShowMsg("输入的Carton不属于此入库清单", 0);
                }
                txtCarton.SelectAll();
                
            }
            
        }
        private void endprintaction()
        {
            ///// 3.锁定程式界面上半部分的功能>> 按钮失效;datagriadview 选择index 事件失效;...
            //btnSearch.Enabled = true;
            //btnClsFace.Enabled = true;
            //btnGetPallet.Enabled = true;
            //this.dgvNo.SelectionChanged += new System.EventHandler(this.dgvNo_SelectionChanged);
            //txtCarton.Enabled = false;
            //txtCarton.Text = "";
            ///// 4.【开始作业】 按钮启用。
            //btnStart.Enabled = true;
            //btnEnd.Enabled = false;
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
                //btnStart.Enabled = true;
                btnGetPallet.Enabled = true;
                clearCmb();
                clearDgv();
                clearTxt();
                //labqty.Text = "00/00";
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
            cmbPallet.Text = "ALL";
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
            dgvNo.Rows.Clear();
            dgvStock.Rows.Clear();
            dgvPickNO.Rows.Clear();
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
            txtPalletNO.Text = string.Empty;
            //txtCarton.Text = string.Empty;
            txtTrolleyNO.Text = string.Empty;
            txtPalletRegion.Text = string.Empty;
            txtPalletNO.ReadOnly = true;
            //txtRegion.ReadOnly = true;
            //txtCountry.ReadOnly = true;
            //txtPoe.ReadOnly = true;
            txtPalletNO.BackColor = System.Drawing.Color.Silver;
            //txtRegion.BackColor = System.Drawing.Color.Silver;
            //txtCountry.BackColor = System.Drawing.Color.Silver;
            //txtPoe.BackColor = System.Drawing.Color.Silver;
            //txtCarton.BackColor = System.Drawing.SystemColors.Window;
            //txtLOCATION_NO.BackColor = System.Drawing.SystemColors.Window;
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
                //if (g_curRow == rowIndex)
                //    return;
                 g_curRow = rowIndex;
                //dgvStock.DataSource = null;
                //dgvStock.Rows.Clear();
                //string strSID = dgvPickNO.Rows[g_curRow].Cells["pallet_no"].Value.ToString();
                //string strPartNo= dgvPickNO.Rows[g_curRow].Cells["ictpn"].Value.ToString();
                //2 更新作业显示内容
               // dgvStock.DataSource=  wb.ShowStockInfo(strPartNo);
            }
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddPalletWeight_Click(object sender, EventArgs e)
        {
            //addPalletWeight pr = new addPalletWeight();
            //pr.ShowDialog();
        }

        
        private void btnGetPallet_Click(object sender, EventArgs e)
        {
            fGetMesPallet pr = new fGetMesPallet();
           pr.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
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

        private void btnCheckIN_Click(object sender, EventArgs e)
        {

            btnCheckIN.Enabled = false;
            //check 是否为金刚车 状态是否不为FP

            string strPalletNO = string.Empty;
            strPalletNO = txtPalletNO.Text.Trim();
            if (string.IsNullOrEmpty(strPalletNO)) 
            {
                ShowMsg("栈板号不能为空",0);

                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnCheckIN.Enabled = true;
                return;
            }
            string strCheckType = "CHECKIN";
            string strResult = string.Empty;
            string strResultOUT = string.Empty;
            //调用金刚车的checkin
            
            strResult = wb.WmsiPalletCheck(strPalletNO, strCheckType, localHostname, out strResultOUT);
            if (!strResult.Equals("OK")) 
            {
                ShowMsg(strResultOUT, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnCheckIN.Enabled = true;
                return;
            }

            fWmsiCheckin fw = new fWmsiCheckin();
            fw.PALLETNO = txtPalletNO.Text;
            fw.TROLLEYNO = txtTrolleyNO.Text;
            fw.ShowDialog();

            btnCheckIN.Enabled = true;
        }

        private void btnTransPPS_Click(object sender, EventArgs e)
        {
            
            btnTransPPS.Enabled = false;
            //check 是否为金刚车 状态是否不为FP

            string strPalletNO = string.Empty;
            strPalletNO = txtPalletNO.Text.Trim();
            if (string.IsNullOrEmpty(strPalletNO))
            {
                ShowMsg("栈板号不能为空", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnTransPPS.Enabled = true;
                return;
            }

            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS Start", "PALLETNO:" + strPalletNO);
            string strCheckType = "TRANSPPS";
            string strResult = string.Empty;
            string strResultOUT = string.Empty;
            //调用金刚车的checkin

            strResult = wb.WmsiPalletCheck(strPalletNO, strCheckType, localHostname, out strResultOUT);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(strResultOUT, 0);

                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnTransPPS.Enabled = true;
                return;
            }
            if (strResultOUT.Contains("HOLD") &&  chkHold.Checked) 
            {
                DialogResult strResultA = MessageBox.Show("栈板号:" + strPalletNO + ",存在HOLD，是否入库？", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (strResultA == DialogResult.No)
                {
                    btnTransPPS.Enabled = true;
                    return;
                }
            }
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS HOLD检查End", "PALLETNO:" + strPalletNO);
            //序号的region和 储位region的region检查

            string strPlant_sloc = txtPlantSloc.Text;
            if (string.IsNullOrEmpty(strPlant_sloc) || strPlant_sloc == "-")
            {
                ShowMsg("请核对厂别-库别", 0);

                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnTransPPS.Enabled = true;
                return;
            }
            string strLocationRegion = cmbLocationRegion.Text;
            string strLocationNo = cmbLocation.Text;
            if (string.IsNullOrEmpty(strLocationRegion) || string.IsNullOrEmpty(strLocationNo))
            {
                ShowMsg("请先筛选目的储位地区", 0);

                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnTransPPS.Enabled = true;
                return;
            }

            string strPalletRegion = txtPalletRegion.Text;
            //if (!strLocationRegion.Equals(strPalletRegion)) 
            //{
            //     DialogResult msgResult= MessageBox.Show("栈板地区和储位地区不一致是否继续？", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //    if (msgResult== DialogResult.Cancel) 
            //    {
            //        LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //        btnTransPPS.Enabled = true;
            //        return;
            //    }
            //    //如果选择地区就直接入库。
            //}


            string strOutMsg = string.Empty;
            //处理是否有新料号
            if (!InertNewPartNoToPPS2(strPalletNO, out strOutMsg))
            {
                ShowMsg(strOutMsg, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnTransPPS.Enabled = true;
                return;
            }
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS InertNewPartNoToPPS2End", "PALLETNO:" + strPalletNO);

            DataTable dtPallet = wb.GetPalletNoStatusInfo(strPalletNO);
            if (dtPallet == null)
            {
                ShowMsg("栈板资料为空,不得入库", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnTransPPS.Enabled = true;
                return;
            }
            if (dtPallet.Rows.Count > 0)
            {
                strPalletMarinaflag = dtPallet.Rows[0]["marina_status"]?.ToString();
                strPalletFBMESflag = dtPallet.Rows[0]["fb_mes_status"]?.ToString();
                strPalletUPLOADSAPflag = dtPallet.Rows[0]["upload_sap_status"]?.ToString();
                //string strpalletstatus = dtPallet.Rows[0]["pallet_status"]?.ToString();
                //string strpalletcheckstatus = dtPallet.Rows[0]["check_status"]?.ToString();
                //string strpallettransppsstatus = dtPallet.Rows[0]["trans_pps_status"]?.ToString();
                //string strpalletfbmesstatus = dtPallet.Rows[0]["fb_mes_status"]?.ToString();
                //string strpalletuploadsapstatus = dtPallet.Rows[0]["upload_sap_status"]?.ToString();
                //string strpalletmarinastatus = dtPallet.Rows[0]["marina_status"]?.ToString();
            }

            if (strPalletMarinaflag.Equals("WP") &&  (!g_DBTYPE.Equals("TEST")))
            {
                //执行Marina Server Check
                //g_WMSIMARINA = strWMSIMarinaCheck;
                //g_WMSIPACKOUT = strWMSIPackOutCheck;
                string strMarinaFlag = string.Empty;
                string strPackoutFlag = string.Empty;
                string strMarinaPackoutResultMsg = string.Empty;

                string strMarinaPackoutResult = wb.GetMarinaPackoutFlag(strPalletNO, "WMSI", out strMarinaFlag, out strPackoutFlag, out strMarinaPackoutResultMsg);
                if (!strMarinaPackoutResult.Equals("OK"))
                {
                    ShowMsg(strMarinaPackoutResultMsg, 0);

                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    btnTransPPS.Enabled = true;
                    return;
                }
                //if (strMarinaFlag.Equals("Y")) 
                if (strMarinaFlag.Equals("Y")) 
                { 
                    string strErrmsg = string.Empty;
                    if (!CheckMarinaServer(strPalletNO, out strErrmsg))
                    {
                        LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS MarinaServerCheckEnd", "PALLETNO:" + strPalletNO);
                        ShowMsg("MarinaServer:" + strErrmsg, 0);
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        btnTransPPS.Enabled = true;
                        return;
                    }
                }
                //执行Packout Logic Check
                //if (g_WMSIPACKOUT.Equals("Y"))
                if (strPackoutFlag.Equals("Y"))
                {
                    string strErrmsg2 = string.Empty;
                    if (!CheckPackoutLogic(strPalletNO, out strErrmsg2))
                    {
                        LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS PackOutCheckEnd", "PALLETNO:" + strPalletNO);
                        ShowMsg("SF_PackoutLogic:" + strErrmsg2, 0);
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        btnTransPPS.Enabled = true;
                        return;
                    }
                }
                //更新状态
                string strResultOut4 = string.Empty;
                Boolean isReusltOK3 = wb.WMSIUpdatePalletMarinaStatus(strPalletNO, out strResultOut4);
                if (!isReusltOK3)
                {
                    ShowMsg("Marina&PackoutLogic CheckOK，但是更新DB异常:" + strResultOut4, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    btnTransPPS.Enabled = true;
                    return ;
                }

            }

            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS ExecuteWmsiPalletTransIN Start", "PALLETNO:" + strPalletNO);
            //执行入库
            string strOuterrmsg = string.Empty;
            //string strReulstFGIN = wb.ExecuteWmsiPalletTransIN(strPalletNO, strLocationNo,  out strOuterrmsg);
            string strReulstFGIN = wb.ExecuteWmsiPalletTransIN(strPalletNO, strLocationNo, strPlant_sloc, out strOuterrmsg);
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS ExecuteWmsiPalletTransIN End", "PALLETNO:" + strPalletNO);
            if (!strReulstFGIN.Equals("OK"))
            {
                ShowMsg(strOuterrmsg, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                wb.UpdateDgvPalletStatus(strPalletNO, dgvNo);
                btnTransPPS.Enabled = true;
                return;
            }
            if (strPalletFBMESflag.Equals("WP"))
            {
                //执行反馈MES
                LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS 执行反馈MES Start", "PALLETNO:" + strPalletNO);
                strResult = wb.WmsiFBMESWebService(strPalletNO, strPalletProduct, g_sUserNo, g_ServerIP, out strResultOUT);
                LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS 执行反馈MES End", "PALLETNO:" + strPalletNO);
                if (!strResult.Equals("OK"))
                {
                    ShowMsg(strResultOUT, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    wb.UpdateDgvPalletStatus(strPalletNO, dgvNo);
                    btnTransPPS.Enabled = true;
                    return;
                }
            }

            if (strPalletUPLOADSAPflag.Equals("WP"))
            {
                //执行反馈SAP
                LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS 执行反馈SAP Start", "PALLETNO:" + strPalletNO);
                strResult = wb.WmsiUplodSapWebService(strPalletNO, g_sUserNo, g_ServerIP, out strResultOUT);
                LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS 执行反馈SAP End", "PALLETNO:" + strPalletNO);
                if (!strResult.Equals("OK"))
                {
                    ShowMsg(strResultOUT, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    wb.UpdateDgvPalletStatus(strPalletNO, dgvNo);
                    btnTransPPS.Enabled = true;
                    return;
                }
            }
            ShowMsg("入库OK", 0);
            LibHelper.MediasHelper.PlaySoundAsyncByOk();
            wb.UpdateDgvPalletStatus(strPalletNO, dgvNo);
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS End", "PALLETNO:" + strPalletNO); 
            btnTransPPS.Enabled = true;



        }
        private bool InertNewPartNoToPPS2(string strPalletNO, out string strOutmsg)
        {
            DataTable dtNoExistPartNO = wb.GetNoExistPartNODataTable2(strPalletNO);
            if (dtNoExistPartNO != null)
            {
                string strProduct = dtNoExistPartNO.Rows[0]["product_name"].ToString().ToUpper();
                //WATCH or AIRPOD
                string strResult = string.Empty;
                string strResulterrmsg = string.Empty;
                string strFGinMESWcf = string.Empty;
                strResult=wb.PPSGetbasicparameter(strProduct, out strFGinMESWcf, out strResulterrmsg);
                if (!strResult.Equals("OK")) 
                {
                    strOutmsg = strResulterrmsg;
                    return false;
                }

                // 有记录说明为新料号
                for (int i = 0; i < dtNoExistPartNO.Rows.Count; i++)
                {
                    if (strProduct.Equals("WATCH"))
                    {
                        string strPNReturnJson = string.Empty;
                        string partNo = dtNoExistPartNO.Rows[i]["PART_NO"].ToString();
                        string strBatchtype = dtNoExistPartNO.Rows[i]["batchtype"].ToString().ToUpper();
                        if (!string.IsNullOrEmpty(strBatchtype)) 
                        { 
                            partNo = partNo.Substring(0, partNo.Length - strBatchtype.Length - 1);
                        }
                        
                        //partNo = batchType == "-S01" || batchType == "-M04" ?partNo.Substring(0, partNo.Length - 4) : partNo;
                        try
                        {
                            //strResultGetSN = wb.GetMesAPI(strProduct, strFGinMESWcf, strMESFuncName, strCarton);
                            //JSMESWebReference.MesApi ws = new JSMESWebReference.MesApi(strFGinMESWcf);
                            //strPNReturnJson = ws.GetMESPNInfo(partNo);
                            strPNReturnJson = wb.GetMesAPI(strProduct, strFGinMESWcf, "GetMESPNInfo", partNo);
                            Boolean isIsertLogOK = true;
                            string strRsgMsg0 = string.Empty;
                            string strGUID2 = System.Guid.NewGuid().ToString().ToUpper();
                            isIsertLogOK = wb.WMSIBackUpWebServieLog(strGUID2, g_ServerIP, strFGinMESWcf, partNo, strPNReturnJson, g_sUserNo, "GetMESPNInfo", out strRsgMsg0);
                            if (!isIsertLogOK)
                            {
                                strOutmsg= strRsgMsg0;
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            strOutmsg = ex.Message;
                            return false;
                        }

                        PNRETURNMODEL PNResultModel = new PNRETURNMODEL();
                        PNResultModel = JsonConvert.DeserializeObject<PNRETURNMODEL>(strPNReturnJson);
                        string strPNResultModel = PNResultModel.RESULT;
                        string strPNErrmsg = PNResultModel.RESULT;

                        if (!strPNErrmsg.ToUpper().Equals("TRUE"))
                        {
                            strOutmsg = "获取MES 料号资料异常:" + PNResultModel.MSG;
                            return false; ;
                        }

                        PNLIST[] TeturnPNList = PNResultModel.PNLIST;
                        for (int j = 0; j < TeturnPNList.Count(); j++)
                        {
                            PNLIST pninfo = TeturnPNList[j];
                            string stroutPNerrmsg = string.Empty;
                            if (!string.IsNullOrEmpty(strBatchtype)) 
                            {
                                pninfo.PN = pninfo.PN + "-" + strBatchtype;
                            }
                            //pninfo.PN = batchType == "-S01" || batchType == "-M04" ? pninfo.PN + batchType : pninfo.PN;
                            string strPNResult = wb.ExecutePNIN(pninfo, out stroutPNerrmsg);
                            if (!strPNResult.Equals("OK"))
                            {
                                strOutmsg = stroutPNerrmsg;
                                return false; ;
                            }
                        }
                    }
                    else if (strProduct.Equals("AIRPOD"))
                    {
                        strOutmsg = "暂时不支持" + strProduct + "此产品入库";
                        return false;
                    }
                    else
                    {
                        strOutmsg = "暂时不支持" + strProduct + "此产品入库";
                        return false;
                    }
                }
            }
            strOutmsg = "";
            return true;
        }
        private void btnFeedbackMES_Click(object sender, EventArgs e)
        {
             btnFeedbackMES.Enabled = false;
            //check 是否为金刚车 状态是否不为FP

            string strPalletNO = string.Empty;
            strPalletNO = txtPalletNO.Text.Trim();
            if (string.IsNullOrEmpty(strPalletNO))
            {
                ShowMsg("栈板号不能为空", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnFeedbackMES.Enabled = true;
                return;
            }
            string strCheckType = "FBMES";
            string strResult = string.Empty;
            string strResultOUT = string.Empty;
            //调用金刚车的checkin

            strResult = wb.WmsiPalletCheck(strPalletNO, strCheckType, localHostname, out strResultOUT);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(strResultOUT, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnFeedbackMES.Enabled = true;
                return;
            }

            strResult = wb.WmsiFBMESWebService( strPalletNO,  strPalletProduct, g_sUserNo, g_ServerIP, out strResultOUT);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(strResultOUT, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnFeedbackMES.Enabled = true;
                return;
            }
            #region 2020324old 
            //string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            ////获得序号
            //string strResultGetSN = string.Empty;
            //if (strPalletProduct.Equals("WATCH"))
            //{
            //    string strResulta = string.Empty;
            //    string strResulterrmsg = string.Empty;
            //    string strFGinMESWcf = string.Empty;
            //    strResulta = wb.PPSGetbasicparameter(strPalletProduct, out strFGinMESWcf, out strResulterrmsg);
            //    if (!strResulta.Equals("OK"))
            //    {
            //        LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //        this.ShowMsg(strResulterrmsg, 0);
            //        return;
            //    }

            //    #region 备份获取记录 先只看watch
            //    string strMESFuncName = "UpdateStockINStatus";
            //    strResultGetSN = wb.GetMesAPI(strPalletProduct, strFGinMESWcf, strMESFuncName, strPalletNO);
            //    Boolean isIsertLogOK = true;
            //    string strRsgMsg0 = string.Empty;
            //    isIsertLogOK = wb.WMSIBackUpWebServieLog(strGUID, g_ServerIP, strFGinMESWcf, strPalletNO, strResultGetSN, g_sUserNo, strMESFuncName, out strRsgMsg0);
            //    if (!isIsertLogOK)
            //    {
            //        LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //        this.ShowMsg(strRsgMsg0, 0);
            //        btnFeedbackMES.Enabled = true;
            //        return;
            //    }
            //    #endregion
            //}
            //else if (strPalletProduct.Equals("AIRPOD"))
            //{
            //    LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //    this.ShowMsg("暂时不支持" + strPalletProduct + "此产品入库", 0);
            //    btnFeedbackMES.Enabled = true;
            //    return;
            //}
            //else
            //{
            //    LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //    this.ShowMsg("暂时不支持" + strPalletProduct + "此产品入库", 0);
            //    btnFeedbackMES.Enabled = true;
            //    return;
            //}

            //List<FBMESRETURNMODEL> ResultModelList = new List<FBMESRETURNMODEL>();
            //try
            //{
            //    ResultModelList = JsonConvert.DeserializeObject< List<FBMESRETURNMODEL> >(strResultGetSN);
            //}
            //catch (Exception e1)
            //{
            //    LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //    this.ShowMsg("MES反馈信息格式异常:" + e1.ToString(), 0);
            //    btnFeedbackMES.Enabled = true;
            //    return;
            //}
            //string strMESReturnResult = "PASS";
            //for (int i = 0; i < ResultModelList.Count(); i++)
            //{
            //    FBMESRETURNMODEL ResultModel = ResultModelList[i];
            //    if (ResultModel.Result.Contains("FAIL")) 
            //    {
            //        strMESReturnResult = ResultModel.Result;
            //    }
            //}
            //if (strMESReturnResult.Equals("PASS"))
            //{
            //    string strUpdateResult = string.Empty;
            //    Boolean isUpdateOK = wb.WMSIUpdatePalletStatus(strPalletNO, out strUpdateResult);
            //    if (!isUpdateOK)
            //    {
            //        LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //        this.ShowMsg("调用MES接口OK，MES反馈信息:"+strUpdateResult, 0);
            //        btnFeedbackMES.Enabled = true;
            //        return;
            //    }
            //}
            //else 
            //{
            //    LibHelper.MediasHelper.PlaySoundAsyncByNg();
            //    this.ShowMsg("调用MES接口OK，MES反馈信息:" + strMESReturnResult, 0);
            //    btnFeedbackMES.Enabled = true;
            //    return;
            //}
            #endregion

            LibHelper.MediasHelper.PlaySoundAsyncByOk();
            wb.UpdateDgvPalletStatus(strPalletNO, dgvNo);
            ShowMsg("FEEDBACK MES OK", 0);
            btnFeedbackMES.Enabled = true;

        }

        private void btnUploadSAP_Click(object sender, EventArgs e)
        {
            btnUploadSAP.Enabled = false;
            //check 是否为金刚车 状态是否不为FP

            string strPalletNO = string.Empty;
            strPalletNO = txtPalletNO.Text.Trim();
            if (string.IsNullOrEmpty(strPalletNO))
            {
                ShowMsg("栈板号不能为空", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnUploadSAP.Enabled = true;
                return;
            }
            string strCheckType = "UPLOADSAP";
            string strResult = string.Empty;
            string strResultOUT = string.Empty;
            //调用金刚车的checkin

            strResult = wb.WmsiPalletCheck(strPalletNO, strCheckType, localHostname, out strResultOUT);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(strResultOUT, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnUploadSAP.Enabled = true;
                return;
            }
            //UPLOADSAP
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS 执行反馈SAP Start", "PALLETNO:" + strPalletNO);
            strResult = wb.WmsiUplodSapWebService(strPalletNO, g_sUserNo, g_ServerIP, out strResultOUT);
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "入库PPS 执行反馈SAP End", "PALLETNO:" + strPalletNO);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(strResultOUT, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                btnUploadSAP.Enabled = true;
                return;
            }
            LibHelper.MediasHelper.PlaySoundAsyncByOk();
            wb.UpdateDgvPalletStatus(strPalletNO, dgvNo);
            ShowMsg("UPLOAD SAP OK", 0);
            btnUploadSAP.Enabled = true;
        }
        private void cmbPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlant.SelectedValue == null ||
                            cmbPlant.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }
            ShowcmbWH(cmbPlant.SelectedValue?.ToString());
        }
        private void cmbWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSAPWH.Text = "";
            if (cmbWH.SelectedValue == null
                || cmbWH.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }
            string strLocationRegion = cmbLocationRegion.Text;
            ShowcmbLocation(cmbWH.SelectedValue?.ToString(), strLocationRegion, checkOnlyinLocation.Checked);
            lblSAPWH.Text = wb.GetSAPWH(cmbWH.SelectedValue.ToString());
        }

        private void cmbLocationRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbWH.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            //{ return; }
            if (txtPlantSloc.Text.Trim() == "" || txtPlantSloc.Text.Trim() == "-")
            {
                ShowMsg("厂别库别不允许为空", 0);
                return;
            }
            string strLocationRegion = cmbLocationRegion.Text;
            ShowcmbLocation(txtPlantSloc.Text, strLocationRegion, checkOnlyinLocation.Checked);
            //ShowcmbLocation(cmbWH.SelectedValue?.ToString() , strLocationRegion, checkOnlyinLocation.Checked);
        }

        private void ShowcmbWH(string strFactory)
        {
            string strSql = string.Format(@"
                select a.warehouse_id id, a.db_type|| a.sap_wh_no name
                  from sajet.wms_warehouse a
                 where a.enabled = 'Y'
                   and a.warehouse_no <>'SYS' and a.plant='{0}'
                 order by a.warehouse_no
                ", strFactory);
            fillCmb(strSql, "warehouse_No", cmbWH);
        }
        private void ShowcmbLocation(string strWHID, string strRegion, bool isOnlyinLocation)
        {
            string strSql = string.Empty;
            if (isOnlyinLocation)
            {
                if (strRegion.Equals("AMR") || strRegion.Equals("EMEIA") || strRegion.Equals("PAC"))
                {
                    strSql = string.Format(@"
                 select distinct a.location_id id, location_no name, region region
                             from sajet.wms_location a join sajet.wms_warehouse b
                             on a.warehouse_id=b.warehouse_id
                            where b.plant||'-'||b.sap_wh_no='{0}'
                               and b.enabled='Y'
                               and b.warehouse_no!='SYS'
                               and a.region ='{1}'
                               and a.enabled ='Y'
							   and a.location_no not like '%SYS%'
                               and a.location_id not in (select location_id from ppsuser.t_location)
                               and a.location_id not in (select location_id from nonedipps.t_location)
                            order by a.location_no"
                             , strWHID, strRegion);
                }
                else
                {
                    strSql = string.Format(@"
                 select distinct a.location_id id, location_no name, region region
                             from sajet.wms_location a join sajet.wms_warehouse b
                             on a.warehouse_id=b.warehouse_id
                            where b.plant||'-'||b.sap_wh_no='{0}'
                               and b.enabled='Y'
                               and b.warehouse_no!='SYS'
							  and a.location_no not like '%SYS%' 
                              and a.enabled ='Y'
                              and a.location_id not in (select location_id from ppsuser.t_location)
                              and a.location_id not in (select location_id from nonedipps.t_location)
                            order by a.location_no"
                             , strWHID);
                }
            }
            else
            {
                if (strRegion.Equals("AMR") || strRegion.Equals("EMEIA") || strRegion.Equals("PAC"))
                {
                    strSql = string.Format(@"
                 select distinct a.location_id id, location_no name, region region
                             from sajet.wms_location a join sajet.wms_warehouse b
                             on a.warehouse_id=b.warehouse_id
                            where b.plant||'-'||b.sap_wh_no='{0}'
                               and b.enabled='Y'
                               and b.warehouse_no!='SYS'
                               and a.region ='{1}'
			       and a.enabled ='Y' 
			       and a.location_no not like '%SYS%' 
                            order by a.location_no"
                             , strWHID, strRegion);
                }
                else
                {
                    strSql = string.Format(@"
                 select distinct a.location_id id, location_no name, region region
                             from sajet.wms_location ajoin sajet.wms_warehouse b
                             on a.warehouse_id=b.warehouse_id
                            where b.plant||'-'||b.sap_wh_no='{0}'
                               and b.enabled='Y'
							   and b.warehouse_no!='SYS'   
							  and a.location_no not like '%SYS%'                          
                              and a.enabled ='Y'
                            order by a.location_no"
                             , strWHID);
                }
            }
            fillCmb(strSql, "location_name", cmbLocation);
        }


        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLocation.SelectedValue?.GetType().ToString() == "System.Data.DataRowView")
                {
                    cmbLocation.Text = "";
                    dgvStock.DataSource = null;
                    dgvStock.Rows.Clear();
                    return; 
                }
            }
            catch (Exception) 
            {
               
                return;
            }
            string strLocationNo = cmbLocation.Text;
            dgvStock.DataSource = null;
            dgvStock.Rows.Clear();
            DataTable dtSAPIDList = wb.GetLocationNoInfo(strLocationNo, strPalletEDIflag);
            dgvStock.DataSource = dtSAPIDList;

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //1.取4个库存的SN


            
            //1.自动获取栈板号
            //2.栈板号自动获取序号
            //3.自动获取一个储位
            //4.自动入库
            EDIWarehouseINBLL pb = new EDIWarehouseINBLL();
            DataTable palletdt = pb.GetAutoWMSIPalletNO();
            if (palletdt.Rows.Count > 0)
            {
                int iTotalrow = palletdt.Rows.Count;
                for (int i = 0; i < iTotalrow; i++)
                {
                    string strGUID = System.Guid.NewGuid().ToString().ToUpper();

                    string strPallet_no = palletdt.Rows[i]["customer_sn"].ToString().Trim();

                    string strMESFuncName = string.Empty;
                    strMESFuncName = "GetMESStockInfo";
                    string strResultGetSN = wb.GetMesAPI(strProduct, strFGinMESWcf, strMESFuncName, strPallet_no);
                   
                    Boolean isIsertLogOK = true;
                    string strRsgMsg0 = string.Empty;
                    isIsertLogOK = wb.WMSIBackUpWebServieLog(strGUID, g_ServerIP, strFGinMESWcf, strPallet_no, strResultGetSN, g_sUserNo, strMESFuncName, out strRsgMsg0);

                    FGINRETURNMODEL ResultModel = new FGINRETURNMODEL();
                    try
                    {
                        ResultModel = JsonConvert.DeserializeObject<FGINRETURNMODEL>(strResultGetSN);
                    }
                    catch (Exception e1)
                    {
                        this.ShowMsg("MES反馈信息格式异常:" + e1.ToString(), 0);
                    }


                    //显示序号 +保存序号
                    string strOutMsg = string.Empty;
                    if (!ShowMesSnOnDGV(ResultModel, strGUID, "N", out strOutMsg))
                    {
                        ShowMsg(strOutMsg, 0);
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        selecttxtCarton();
                        return;
                    }


                    string strlocationno = pb.GetAutoWMSILocationNO();

                    string strCheckType = "TRANSPPS";
                    string strResult = string.Empty;
                    string strResultOUT = string.Empty;
                    //调用金刚车的checkin

                    strResult = wb.WmsiPalletCheck(strPallet_no, strCheckType, localHostname, out strResultOUT);
                    if (!strResult.Equals("OK"))
                    {
                        ShowMsg(strResultOUT, 0);
                    }
                    
                    string strOutMsg0 = string.Empty;
                    //处理是否有新料号
                    bool isok=InertNewPartNoToPPS2(strPallet_no, out strOutMsg0);
                    
                    //执行入库
                    string strOuterrmsg = string.Empty;
                    string strReulstFGIN = wb.ExecuteWmsiPalletTransIN(strPallet_no, strlocationno, out strOuterrmsg);
                    if (strReulstFGIN.Equals("OK"))
                    {
                        strResult = wb.WmsiFBMESWebService(strPallet_no, strPalletProduct, g_sUserNo, g_ServerIP, out strResultOUT);
                        strResult = wb.WmsiUplodSapWebService(strPallet_no, g_sUserNo, g_ServerIP, out strResultOUT);
                    }
                    wb.UpdateDgvPalletStatus(strPallet_no, dgvNo);

                }

                //string  strA= AutoWMSMarinaCheck(2, 10);
                //ShowMsg( strA, 0);

                //fWmsiCheckin fw = new fWmsiCheckin();
                //fw.PALLETNO = txtPalletNO.Text;
                //fw.TROLLEYNO = txtTrolleyNO.Text;
                //fw.ShowDialog()
                //执行Packout Logic Check
                //string strpallet=txtPalletNO.Text;
                //string strErrmsg2 = string.Empty;
                //if (!CheckMarinaServer(strpallet, out strErrmsg2))
                //{
                //    ShowMsg("Mrina:" + strErrmsg2, 0);
                //    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                //    return;
                //}


            }
        }
        public string AutoWMSMarinaCheck(Int32 iprecount, Int32 ichecktotalcount)
        {
            //一次循环检查数量 iprecount 1~400
            //一次检查库存总数 0&空 就是全检查
            string strMsgOut = string.Empty;
            //
            EDIWarehouseINBLL pb = new EDIWarehouseINBLL();
            string strResulta = string.Empty;
            string strResulterrmsg = string.Empty;
            strResulta = pb.PPSGetbasicparameter("MARINA_URL", out g_MarinaUrl, out strResulterrmsg);
            if (string.IsNullOrEmpty(g_MarinaUrl))
            {
                strMsgOut = "NG-MarinaServerUrl地址为空异常" + strResulterrmsg;
                return strMsgOut;
            };
            strResulta = pb.PPSGetbasicparameter("MARINA_SITE", out g_MarinaSite, out strResulterrmsg);

            if (string.IsNullOrEmpty(g_MarinaSite))
            {
                strMsgOut = "NG-MarinaSite为空异常" + strResulterrmsg;
                return strMsgOut;
            };

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            if (iprecount == 0 || string.IsNullOrEmpty(iprecount.ToString()))
            {
                iprecount = 1;
            }
            DataTable sndt = pb.GetWMSNList(ichecktotalcount);
            if (sndt.Rows.Count > 0)
            {
                int iTotalrow = sndt.Rows.Count;
                int iTotalPalletCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(iTotalrow) / Convert.ToDouble(iprecount)));
                for (int i = 0; i < iTotalPalletCount; i++)
                {
                    MarinaRequestModel inmodel = new MarinaRequestModel();
                    inmodel.STATION_TYPE = "WMS";
                    inmodel.SITE = g_MarinaSite;
                    List<Request> snList = new List<Request>();
                    for (int j = i * iprecount; j < (i + 1) * iprecount; j++)
                    {
                        Request sn = new Request();
                        sn.SerialNumber = sndt.Rows[j]["customer_sn"].ToString().Trim();
                        snList.Add(sn);
                        if (j == sndt.Rows.Count)
                        {
                            break;
                        }
                    }
                    inmodel.request = snList.ToArray();
                    string strRequest = JsonConvert.SerializeObject(inmodel);

                    string strRsgMsg = string.Empty;
                    string strMarinaResult = string.Empty;

                   // strMarinaResult = pb.MarinaWebService(g_MarinaUrl, strRequest, out strRsgMsg);

                    Boolean isIsertLogOK = true;
                    string strRsgMsg0 = string.Empty;
                    isIsertLogOK = pb.CheckMarinaServerUrlLog(strGUID, g_ServerIP, g_MarinaUrl, (i + 1).ToString(), strRsgMsg, "10086", strRequest, out strRsgMsg0);
                    if (!isIsertLogOK)
                    {
                        strMsgOut = strRsgMsg0;
                        return strMsgOut;
                    };
                    MarinaReturnModel outmodel = new MarinaReturnModel();
                    try
                    {
                        outmodel = JsonConvert.DeserializeObject<MarinaReturnModel>(strRsgMsg);
                    }
                    catch (Exception e)
                    {
                        strMsgOut = "序号对应MarinaServerCheck 返回异常，" + e.ToString(); ;
                       // return strMsgOut;
                    }

                    var SNList = new List<string>();
                    //foreach (var item in outmodel.response)
                    //{
                    //    PPSMarinaSN marinaSNInfo = new PPSMarinaSN();
                    //    marinaSNInfo.CUSTOMER_SN = item.SerialNumber;
                    //    marinaSNInfo.OKTOSHIP = item.OKtoShipwithInstalledOS;
                    //    marinaSNInfo.ERRORCODE = item.CurrentInstalledOS.ErrorCode;
                    //    marinaSNInfo.ERRORMESSAGE = item.CurrentInstalledOS.ErrorMessage;
                    //    SNList.Add(marinaSNInfo);
                    //}

                    var marinaSNInfo = new PPSMarinaSN();
                    marinaSNInfo.msgid = strGUID;
                    marinaSNInfo.palletno = (i + 1).ToString();
                    marinaSNInfo.sn = "TEST";
                    marinaSNInfo.oktoship = "N";
                    marinaSNInfo.errcode = "123123";
                    marinaSNInfo.errmsg = "N";
                    SNList.Add(JsonConvert.SerializeObject(marinaSNInfo));
                    //PPSMarinaSNList aa = new PPSMarinaSNList();
                    //aa.SNRESULTLIST= SNList.ToArray();


                    string insertSql = @"INSERT INTO PPSUSER.T_WMS_MARINA_SN_INFO (MSG_ID, PALLET_NO, CUSTOMER_SN, OKTOSHIP, ERRORCODE, ERRORMESSAGE) VALUES (:msgid,:palletno ,:sn ,:oktoship, :errcode , :errmsg )"; 
                    Dictionary<string, object> trans = new Dictionary<string, object>();
                    trans.Add(insertSql, SNList);
                    ClientUtils.DoExtremeSpeedTransaction(trans);

                }

                return null;
            }
            else
            {
                return null;
            }

        }

        private bool CheckMarinaServer(string strSN, out string OutRetmsg)
        {
            OutRetmsg = "";
            EDIWarehouseINBLL pb = new EDIWarehouseINBLL();
            MarinaRequestModel inmodel = new MarinaRequestModel();
            inmodel.STATION_TYPE = "PPS";
            //inmodel.STATION_TYPE = "PICK"; by wenxing 2021-2-1
            inmodel.SITE = g_MarinaSite;

            DataTable dt0 = pb.GetSNInfoDataTableBLL(strSN);
            if (dt0 == null)
            {
                OutRetmsg = "输入非法无效的序号或者箱号，不做统计";
                return false;
            }
            List<Request> snList = new List<Request>();
            foreach (DataRow csnlist in dt0.Rows)
            {
                Request sn = new Request();
                sn.SerialNumber = csnlist["customer_sn"]?.ToString();
                snList.Add(sn);
            }
            inmodel.request = snList.ToArray();

            string strRequest = JsonConvert.SerializeObject(inmodel);

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            string strRsgMsg = string.Empty;
            string strMarinaResult = string.Empty;
            if (string.IsNullOrEmpty(g_MarinaUrl))
            {
                OutRetmsg = "MarinaServerUrl地址为空异常";
                return false;
            };
            strMarinaResult = pb.MarinaWebService(g_MarinaUrl, strRequest, out strRsgMsg);

            Boolean isIsertLogOK = true;
            string strRsgMsg0 = string.Empty;
            isIsertLogOK = pb.CheckMarinaServerUrlLog(strGUID, g_ServerIP, g_MarinaUrl, strSN, strRsgMsg, g_sUserNo, strRequest, out strRsgMsg0);
            if (!isIsertLogOK)
            {
                OutRetmsg = strRsgMsg0;
                return false;
            };
            MarinaReturnModel outmodel = new MarinaReturnModel();
            try
            {
                outmodel = JsonConvert.DeserializeObject<MarinaReturnModel>(strRsgMsg);
            }
            catch (Exception e)
            {
                OutRetmsg = "序号对应MarinaServerCheck 返回异常，" + e.ToString(); ;
                return false;
            }
            foreach (var item in outmodel.response)
            {
                if (!item.OKtoShipwithInstalledOS.Equals("Y"))
                {
                    OutRetmsg = "序号对应MarinaServerCheck Fail,不得入库";
                    return false;
                }

            }
            ////更新状态  --
            //string strResultOut4 = string.Empty;
            //Boolean isReusltOK3 = pb.WMSIUpdatePalletMarinaStatus(strSN, out strResultOut4);
            //if (!isReusltOK3) 
            //{
            //    OutRetmsg = "MarinaCheckOK，但是更新DB异常"+ strResultOut4;
            //    return false;
            //}
            return true;

        }

        private bool CheckPackoutLogic(string strSN, out string OutRetmsg)
        {
            OutRetmsg = "";
            EDIWarehouseINBLL pb = new EDIWarehouseINBLL();
            PackoutLogicRequestModel inmodel = new PackoutLogicRequestModel();

            DataTable dt0 = pb.GetSNInfoDataTableBLL(strSN);
            if (dt0 == null)
            {
                OutRetmsg = "输入非法无效的序号或者箱号，不做统计";
                return false;
            }
            string strProduct = dt0.Rows[0]["PRODUCT_NAME"].ToString();
            List<requestsn> snList = new List<requestsn>();
            foreach (DataRow csnlist in dt0.Rows)
            {
                requestsn sn = new requestsn();
                sn.SN = csnlist["customer_sn"]?.ToString();
                snList.Add(sn);
            }
            inmodel.SN = snList.ToArray();

            string strRequest = JsonConvert.SerializeObject(inmodel);

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            
            

            string strResult0 = string.Empty;
            string strFGinMESWcf = string.Empty;
            string strResulterrmsg = string.Empty;
            strResult0 = pb.GetDBType(strProduct, out strFGinMESWcf, out strResulterrmsg);

            string strMESFuncName = "CheckPackOutLogic";
            string strResult = wb.GetMesAPI(strProduct, strFGinMESWcf, strMESFuncName, strRequest);
            Boolean isIsertLogOK = true;
            string strRsgMsg0 = string.Empty;
            isIsertLogOK = wb.WMSIBackUpWebServieLog(strGUID, g_ServerIP, strFGinMESWcf, strSN, strResult, g_sUserNo, strMESFuncName, out strRsgMsg0);
            if (!isIsertLogOK)
            {
                OutRetmsg =strRsgMsg0;
                return false;
            }

            try
            {
                var outmodel = JsonConvert.DeserializeObject<List<PackoutLogicReturnModel>>(strResult);

                foreach (var item in outmodel)
                {
                    if (!item.RESULT.Equals("OK"))
                    {
                        OutRetmsg = "序号对应PackoutLogicCheck Fail,不得入库";
                        return false;
                    }

                }
            }
            catch (Exception e)
            {
                OutRetmsg = "序号对应PackoutLogicCheck 返回异常，" + e.ToString(); ;
                return false;
            }
            return true;

        }

        private void cmbFGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFGName.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }
            strProduct = cmbFGName.Text; ;
            strFGinMESWcf = cmbFGName.SelectedValue.ToString();

        }
        private void rdoPallet_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPallet.Checked)
            {
                txtSFPalletNo.Text = "";
                strInSnType = "PALLET";
            }
            else
            {
                txtSFPalletNo.Text = "";
                strInSnType = "CARTON";
            }

        }

        private void txtSFPalletNo_KeyDown(object sender, KeyEventArgs e)
        {

            //可扫描序号/箱号
            string strCarton = txtSFPalletNo.Text.Trim();
            strCarton = wb.DelPrefixCartonSN(strCarton);
            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            lblHold.Visible = false;
            if (string.IsNullOrEmpty(strCarton))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的SN/Carton不能为空！", 0);
                selecttxtCarton();
                return;
            }
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "获取序号 Start", "PALLETNO:" + strCarton);

            //是否强制获取新的栈板信息
            //不勾选的 提示已经存在。
            string strIsForceInertPalletFlag = string.Empty;

            if (chkUpdatePallet.Checked)
            {
                strIsForceInertPalletFlag = "Y";
            }
            else
            {
                strIsForceInertPalletFlag = "N";
                //如果栈板号已经再表里面就直接为错误。
                DataTable dt = wb.GetMesPalletInfoLog(strCarton);
                if (!(dt == null))
                {
                    string strPalletRecordnum = dt.Rows[0]["recordnum"].ToString();
                    if (!strPalletRecordnum.Equals("0"))
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        this.ShowMsg("栈板号已经存在记录", 0);
                        selecttxtCarton();
                        return;
                    }
                }
            }

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            //获得序号
            string strResultGetSN = string.Empty;


            if (strProduct.Equals("WATCH"))
            {
                #region 备份获取记录 先只看watch
                string strMESFuncName = string.Empty;
                if (rdoCarton.Checked)
                {
                    strMESFuncName = "GetMaterialTransferInfo";
                }
                else
                {
                    strMESFuncName = "GetMESStockInfo";
                }
                LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "GetMesAPI Start", strProduct + "*" + strFGinMESWcf + "*" + strMESFuncName + "*" + strCarton);
                strResultGetSN = wb.GetMesAPI(strProduct, strFGinMESWcf, strMESFuncName, strCarton);
                LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "GetMesAPI End", strProduct + "*" + strFGinMESWcf + "*" + strMESFuncName + "*" + strCarton);

                Boolean isIsertLogOK = true;
                string strRsgMsg0 = string.Empty;
                isIsertLogOK = wb.WMSIBackUpWebServieLog(strGUID, g_ServerIP, strFGinMESWcf, strCarton, strResultGetSN, g_sUserNo, strMESFuncName, out strRsgMsg0);
                if (!isIsertLogOK)
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.ShowMsg(strRsgMsg0, 0);
                    selecttxtCarton();
                    return;
                }
                #endregion
            }
            else if (strProduct.Equals("AIRPOD"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("暂时不支持" + strProduct + "此产品入库", 0);
                selecttxtCarton();
                return;
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("暂时不支持" + strProduct + "此产品入库", 0);
                selecttxtCarton();
                return;
            }


            if (string.IsNullOrEmpty(strResultGetSN))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("获得序号为空", 0);
                selecttxtCarton();
                return;
            }

            if (rdoCarton.Checked)
            {
                //原材料入库
                RAWINRETURNMODEL ResultModel = new RAWINRETURNMODEL();
                try
                {
                    ResultModel = JsonConvert.DeserializeObject<RAWINRETURNMODEL>(strResultGetSN);
                }
                catch (Exception e1)
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.ShowMsg("MES反馈信息格式异常:" + e1.ToString(), 0);
                    selecttxtCarton();
                    return;
                }


                //显示序号 +保存序号
                string strOutMsg = string.Empty;
                if (!ShowMesSnOnDGV(ResultModel, strGUID, strIsForceInertPalletFlag, out strOutMsg))
                {
                    ShowMsg(strOutMsg, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    selecttxtCarton();
                    return;
                }
            }
            else //栈板入库非原材
            {
                FGINRETURNMODEL ResultModel = new FGINRETURNMODEL();
                try
                {
                    ResultModel = JsonConvert.DeserializeObject<FGINRETURNMODEL>(strResultGetSN);
                }
                catch (Exception e1)
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.ShowMsg("MES反馈信息格式异常:" + e1.ToString(), 0);
                    selecttxtCarton();
                    return;
                }


                //显示序号 +保存序号
                string strOutMsg = string.Empty;
                if (!ShowMesSnOnDGV(ResultModel, strGUID, strIsForceInertPalletFlag, out strOutMsg))
                {
                    ShowMsg(strOutMsg, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    selecttxtCarton();
                    return;
                }
            }
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "获取序号 End", "PALLETNO:" + strCarton);
            dgvNo.DataSource = null;
            dgvNo.Rows.Clear();
            DataTable dtSAPIDList = wb.GetWmsiGUIDPALLETBySQL(strGUID);
            dgvNo.DataSource = dtSAPIDList;
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "显示栈板信息", "PALLETNO:" + strCarton);
            LibHelper.MediasHelper.PlaySoundAsyncByOk();
            ShowMsg("OK", -1);
            selecttxtCarton();


        }

        private void selecttxtCarton()
        {
            txtSFPalletNo.SelectAll();
            txtSFPalletNo.Focus();
        }
        #region //HYQ  ShowMesSnOnDGV OLD 写法
        private bool ShowMesSnOnDGV(FGINRETURNMODEL MesSnModel, string strGUID, string strForceFlag, out string outMsg)
        {

            //FGINRETURNMODEL MesSnModel = new FGINRETURNMODEL();
            //ResultModel = JsonConvert.DeserializeObject<FGINRETURNMODEL>(strMODEL);
            string strINSN = MesSnModel.INSN;
            string strResultModel = MesSnModel.RESULT;
            string strErrmsg = MesSnModel.MSG;
            if (!strResultModel.ToUpper().Equals("TRUE"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                outMsg = "MES反馈序号信息失败:" + strErrmsg;
                selecttxtCarton();
                return false;
            }
            SNLIST[] TeturnSNList = MesSnModel.SNLIST;

            for (int i = 0; i < TeturnSNList.Count(); i++)
            {
                SNLIST sninfo = TeturnSNList[i];
                if (string.IsNullOrEmpty(sninfo.PALLETID?.ToString()))
                {
                    outMsg = "MES反馈的资料清单中栈板号为空，异常";
                    return false;
                }

                string strHoldflag = sninfo.QHOLDFLAG?.ToString();
                if (strHoldflag.Equals("Y")) 
                {
                    lblHold.Visible = true;
                }
                //insert
                //SP内检查PPS是否序号重复， 如果OK，
                string strerrmsg = string.Empty;
                string strResult = wb.ExecuteFGIN(strGUID, sninfo, strProduct, strForceFlag, out strerrmsg);
                if (!strResult.Equals("OK"))
                {
                    outMsg = strerrmsg;
                    return false;
                }
               // LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSI", "ExecuteFGIN", sninfo.BOXID);

            }
            //HYQ 20200619 增加逻辑; MES_PALLET_INFO只有执行到这里才能改成IN-->WP;
            string strerrmsg2 = string.Empty;
            if (!wb.GetMESPalletOKUpdateStatus(strGUID, out strerrmsg2))
            {
                outMsg = strerrmsg2;
                return false;
            };
            
            outMsg = "";
            return true;
        }
        #endregion

        private bool ShowMesSnOnDGV(RAWINRETURNMODEL MesSnModel, string strGUID, string strForceFlag, out string outMsg)
        {

            string strINSN = MesSnModel.INSN;
            string strResultModel = MesSnModel.RESULT;
            string strErrmsg = MesSnModel.MSG;

            if (!strResultModel.ToUpper().Equals("TRUE"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                outMsg = "MES反馈序号信息失败:" + strErrmsg;
                selecttxtCarton();
                return false;
            }
            MATERIALQTY[] TeturnSNList = MesSnModel.MATERIALQTY;

            for (int i = 0; i < TeturnSNList.Count(); i++)
            {
                MATERIALQTY sninfo = TeturnSNList[i];
                DataGridViewRow dr = new DataGridViewRow();
                //累加序号
                if (string.IsNullOrEmpty(sninfo.PALLETID?.ToString()))
                {
                    outMsg = "MES反馈的资料清单中栈板号为空，异常";
                    return false;
                }
                //20200610改为正常不显示;遇到异常异常才会显示，正常要查询才会显示
                //dgvCarton.Rows.Add(dr);
                //insert
                string strerrmsg = string.Empty;
                string strResult = wb.ExecuteRAWMIN(strGUID, sninfo, strProduct, strForceFlag, out strerrmsg);
                if (!strResult.Equals("OK"))
                {

                    outMsg = strerrmsg;
                    return false;
                }
            }
            //HYQ 20200619 增加逻辑; MES_PALLET_INFO只有执行到这里才能改成IN-->WP;
            string strerrmsg2 = string.Empty;
            if (!wb.GetMESPalletOKUpdateStatus(strGUID, out strerrmsg2))
            {
                outMsg = strerrmsg2;
                return false;
            }
            outMsg = "";
            return true;
        }

        private void dgvNo_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvNo.Rows.Count; i++)
            {
                this.dgvNo.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
                this.dgvNo.Rows[i].Cells["fb_mes_status"].Style.BackColor = Color.White;
                this.dgvNo.Rows[i].Cells["upload_sap_status"].Style.BackColor = Color.White;
                if (dgvNo.Rows[i].Cells["trans_pps_status"].Value.ToString().Equals("FP"))
                {
                    if (dgvNo.Rows[i].Cells["fb_mes_status"].Value.ToString().Equals("WP"))
                    {
                        this.dgvNo.Rows[i].Cells["fb_mes_status"].Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        this.dgvNo.Rows[i].Cells["fb_mes_status"].Style.BackColor = Color.White;
                    }

                    if (dgvNo.Rows[i].Cells["upload_sap_status"].Value.ToString().Equals("WP"))
                    {
                        this.dgvNo.Rows[i].Cells["upload_sap_status"].Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        this.dgvNo.Rows[i].Cells["upload_sap_status"].Style.BackColor = Color.White;
                    }
                }



            }
        }
    
    }
}
