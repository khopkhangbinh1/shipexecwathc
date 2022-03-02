using ClientUtilsDll;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDIWarehouseTools
{
    public partial class fWMSDockLocationAssign : Form
    {
        public fWMSDockLocationAssign()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }
        EDIWarehouseToolsBLL eb = new EDIWarehouseToolsBLL();
        public int H = 0;
        public int W = 0;
        private Int32 g_curRow = -1;    //当前选中行号
        private void fWMSDockLocationAssign_Load(object sender, EventArgs e)
        {
            DateTime dateTimeNow = DateTime.Now;
            dtStart.Value = dateTimeNow;
            this.WindowState = FormWindowState.Maximized;
            fLoad();
        }
        private void fLoad()
        {
            H = Screen.PrimaryScreen.Bounds.Height;
            if (H >= 1080)
            {
                H = Convert.ToInt32(H * 0.4);
            }
            else
            {
                H = Convert.ToInt32(H * 0.35);

            }
            W = Screen.PrimaryScreen.Bounds.Width;
            W= Convert.ToInt32(W * 0.4);
            this.groupBox3.Size = new System.Drawing.Size(W, this.groupBox3.Size.Height);
            this.panel4.Size = new System.Drawing.Size(this.groupBox3.Size.Width, H);
            //this.groupBox3.Size = new System.Drawing.Size(W, H);
        }
        private void btnSearchPallet_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnSearchPallet.Enabled = false;


            showPalletList();

            btnSearchPallet.Enabled = true;
        }

        private void showPalletList()
        {
            string strStartDay = dtStart.Value.ToString("yyyy-MM-dd");
            string strEndDay = dtStart.Value.AddDays(1).ToString("yyyy-MM-dd");
            dgvSID.Rows.Clear();
            dgvSID.DataSource = null;
            
            DataTable dtSIDList = eb.GetPalletListDataTable(strStartDay, strEndDay);
            if (dtSIDList == null || dtSIDList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                //dgvSID.DataSource = dtSIDList;
                for (int i = 0; i < dtSIDList.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvSID.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dtSIDList.Rows[i]["shipment_id"].ToString();
                    dr.Cells[1].Value = dtSIDList.Rows[i]["pallet_no"].ToString();
                    dr.Cells[2].Value = dtSIDList.Rows[i]["car_no"].ToString();
                    dr.Cells[3].Value = dtSIDList.Rows[i]["isload"].ToString();
                    dr.Cells[4].Value = dtSIDList.Rows[i]["carrier"].ToString();
                    dr.Cells[5].Value = dtSIDList.Rows[i]["sidtype"].ToString();
                    dr.Cells[6].Value = dtSIDList.Rows[i]["type"].ToString();
                    dr.Cells[6].Value = dtSIDList.Rows[i]["region"].ToString();
                    try
                    {
                        dgvSID.Invoke((MethodInvoker)delegate ()
                        {
                            dgvSID.Rows.Add(dr);
                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                }
            }
        }



        private DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
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
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void btnSearchDockLocation_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnSearchDockLocation.Enabled = false;

            showDockLocationList();

            btnSearchDockLocation.Enabled = true;
        }

        private void showDockLocationList()
        {

            dgvDockLocation.DataSource = null;
            dgvDockLocation.Rows.Clear();
            DataTable dtlineList = eb.GetDockLoactionListDataTable();
            if (dtlineList == null || dtlineList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvDockLocation.DataSource = dtlineList;
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnResult.Enabled = false;

            if (rdoPALLET.Checked)
            {
                string strCARNO = cmbCarNO.Text;
                string strSID = cmbSID.Text;
                showAssginLocationList( strCARNO, strSID);
            }
            else
            {
                if (chkALL.Checked)
                {
                    showAssginLocationList2();
                }
                else
                {
                    showAssginLocationList3();
                }
            }
            showcolor(txtPallet.Text);

            btnResult.Enabled = true;
        }
        private void showAssginLocationList(string strCARNO, string strSID)
        {

            dgvAssign.DataSource = null;
            dgvAssign.Rows.Clear();
            DataTable dtlineList = eb.GetAssignLocationListDataTable( strCARNO,  strSID);
            if (dtlineList == null || dtlineList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvAssign.DataSource = dtlineList;
            }
        }
        private void showAssginLocationList2()
        {

            dgvAssign.DataSource = null;
            dgvAssign.Rows.Clear();
            DataTable dtlineList = eb.GetAssignLocationListDataTable2();
            if (dtlineList == null || dtlineList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvAssign.DataSource = dtlineList;
            }
        }
        private void showAssginLocationList3()
        {

            dgvAssign.DataSource = null;
            dgvAssign.Rows.Clear();
            DataTable dtlineList = eb.GetAssignLocationListDataTable3();
            if (dtlineList == null || dtlineList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvAssign.DataSource = dtlineList;
            }
        }
        private void showcolor(string strPalletNO) 
        {
            initcmb();
            if (dgvAssign.Rows.Count <=1) { return; }

            for (int i=0; i< dgvAssign.Rows.Count;i++) 
            {

                if (dgvAssign.Rows[i].Cells["pallet_no"].Value != null) 
                {
                    if (strPalletNO.Equals(dgvAssign.Rows[i].Cells["pallet_no"].Value.ToString())) 
                    {
                        dgvAssign.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgvAssign.ClearSelection();
                        dgvAssign.Rows[i].Selected = true;
                        dgvAssign.FirstDisplayedScrollingRowIndex = i;
                    }
                }
                if (dgvAssign.Rows[i].Cells["shipment_id"].Value != null)
                {
                    string strSID = dgvAssign.Rows[i].Cells["shipment_id"].Value.ToString();
                    if (!cmbSID.Items.Contains(strSID) && !string.IsNullOrEmpty(strSID))
                    {
                        cmbSID.Items.Add(strSID);
                    }
                }
                if (dgvAssign.Rows[i].Cells["car_no"].Value != null)
                {
                    string strCARNO = dgvAssign.Rows[i].Cells["car_no"].Value.ToString();
                    if (!cmbCarNO.Items.Contains(strCARNO) && !string.IsNullOrEmpty(strCARNO)  )
                    {
                        cmbCarNO.Items.Add(dgvAssign.Rows[i].Cells["car_no"].Value.ToString());
                    }
                }

            }

        }


        private void btnEXCEL1_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvSID.Rows.Count > 1)
                {
                    ExportExcel(dgvSID);
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

        private void btnEXCEL2_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvDockLocation.Rows.Count > 1)
                {
                    ExportExcel(dgvDockLocation);
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

        private void btnEXCEL3_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvAssign.Rows.Count > 1)
                {
                    ExportExcel(dgvAssign);
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

        private void txtPallet_KeyDown(object sender, KeyEventArgs e)
        {
            string strNewPalletNO = string.Empty;
            strNewPalletNO = txtPallet.Text.Trim().ToUpper();
            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strNewPalletNO))
            {

                txtPallet.SelectAll();
                txtPallet.Focus();
                return;
            }
            string strLocation = string.Empty;
            string strPalletNO = string.Empty;
            string strResult = string.Empty;
            string strResulterrmsg = string.Empty;
            strResult = eb.UpdateDockLocationPallet(strLocation, strPalletNO, strNewPalletNO, out  strResulterrmsg);

            if (strResult.Equals("OK"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByOk();
                ShowMsg("OK", -1);
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg(strResulterrmsg, 0);
            }
            if (rdoPALLET.Checked)
            {
                showAssginLocationList("-ALL-", "-ALL-");
            }
            else
            {
                showAssginLocationList2();
            }
            showcolor(txtPallet.Text);

            if (chkPrint.Checked) 
            {

                string strPrintContext = string.Empty;
                DataTable dt = eb.GetPalletDockLoaction(strNewPalletNO);
                if (dt.Rows.Count > 0)
                {
                    strPrintContext = dt.Rows[0]["location_no"].ToString() + "," + dt.Rows[0]["pallet_no"].ToString() + ",";
                    PrintPickSAPNOLabel(strPrintContext);
                }
            }

            txtPallet.SelectAll();
            txtPallet.Focus();

        }

        private void dgvAssign_SelectionChanged(object sender, EventArgs e)
        {
            reflashlabel();
        }

        private void reflashlabel()
        {
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvAssign.CurrentRow.Index;
                //rowIndex = dgvDN.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvAssign.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                //if (g_curRow == rowIndex)
                //    return;
                g_curRow = rowIndex;

                txtDockLocation.Text = dgvAssign.Rows[rowIndex].Cells["location_no"].Value.ToString();
                txtCurrPalletNo.Text = dgvAssign.Rows[rowIndex].Cells["pallet_no"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //检查line是否为空 

            //集货单如果是空 就是删除

            //集货单是其它值这执行SP进行更新。

            ShowMsg("", -1);
            btnUpdate.Enabled = false;

            string strLocation = txtDockLocation.Text;
            string strCurrPalletNO = txtCurrPalletNo.Text;
            string strNewPalletNO = txtNEWPalletNo.Text.Trim().ToString() ;

            if (string.IsNullOrEmpty(strLocation))
            {
                ShowMsg("储位不能为空", -1);
                afterbtnUpdate();
                return;
            }

            if (string.IsNullOrEmpty(strCurrPalletNO) && string.IsNullOrEmpty(strNewPalletNO))
            {
                ShowMsg("新旧集货单不能都为空", -1);
                afterbtnUpdate();
                return;
            }

            if (string.IsNullOrEmpty(strNewPalletNO))
            {

                DialogResult result = ShowMsg("确定取消已经分配的集货单", 2);
                if (result == DialogResult.No)
                {
                    afterbtnUpdate();
                    ShowMsg("", -1);
                    return;
                }

            }
            ShowMsg("", -1);

            
            string strResult = string.Empty;
            string strResulterrmsg = string.Empty;
            strResult = eb.UpdateDockLocationPallet(strLocation, strCurrPalletNO, strNewPalletNO, out strResulterrmsg);

            if (strResult.Equals("OK"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByOk();
                ShowMsg("OK", -1);
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg(strResulterrmsg, 0);
            }

            if (rdoPALLET.Checked)
            {
                showAssginLocationList("-ALL-", "-ALL-");
            }
            else
            {
                showAssginLocationList2();
            }
            showcolor(txtPallet.Text);
            afterbtnUpdate();
        }
        private void afterbtnUpdate()
        {
            txtDockLocation.Text = "";
            txtCurrPalletNo.Text = "";
            txtNEWPalletNo.SelectAll();
            txtNEWPalletNo.Focus();
            btnUpdate.Enabled = true;
        }

        private void rdoLocation_CheckedChanged(object sender, EventArgs e)
        {
            initcmb();
        }
        private void initcmb() 
        {
            cmbSID.DataSource = null;
            cmbSID.Items.Clear();
            cmbSID.Items.Add("-ALL-");
            cmbSID.Text = "-ALL-";

            cmbCarNO.DataSource = null;
            cmbCarNO.Items.Clear();
            cmbCarNO.Items.Add("-ALL-");
            cmbCarNO.Text = "-ALL-";
        }

        private void txtCheckLocation_KeyDown(object sender, KeyEventArgs e)
        {
            string strLocationNo = string.Empty;
            strLocationNo = txtCheckLocation.Text.Trim().ToUpper();
            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strLocationNo))
            {
                txtCheckLocation.SelectAll();
                txtCheckLocation.Focus();
                return;
            }
            DataTable dt = eb.GetDockLoactionInfo(strLocationNo);
            if (dt == null)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("不是有效的储位", -1);
                txtCheckLocation.SelectAll();
                txtCheckLocation.Focus();
                return;
            }
            else
            {
                if (dt.Rows.Count == 0)
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg("储位记录为空", -1);
                    txtCheckLocation.SelectAll();
                    txtCheckLocation.Focus();
                    return;
                }
                else
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    txtCheckPallet0.SelectAll();
                    txtCheckPallet0.Focus();
                }
            }
            
        }

        private void txtCheckPallet_KeyDown(object sender, KeyEventArgs e)
        {

            string strPallet0 = string.Empty;
            strPallet0 = txtCheckPallet0.Text.Trim().ToUpper();

            string strPalletNO = string.Empty;
            strPalletNO = txtCheckPallet.Text.Trim().ToUpper();
            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strPallet0))
            {

                txtCheckPallet0.SelectAll();
                txtCheckPallet0.Focus();
                return;
            }
            if (string.IsNullOrEmpty(strPalletNO))
            {

                txtCheckPallet.SelectAll();
                txtCheckPallet.Focus();
                return;
            }
            if (!strPalletNO.Equals(strPallet0)) 
            {
                ShowMsg("栈板号不一致", -1);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                txtCheckPallet0.SelectAll();
                txtCheckPallet0.Focus();
                txtCheckPallet.Text = "";
                return;
            }


            string strLocationNo = string.Empty;
            strLocationNo = txtCheckLocation.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(strLocationNo))
            {
                txtCheckLocation.SelectAll();
                txtCheckLocation.Focus();
                return;
            }
            DataTable dt = eb.GetDockLoactionInfo2(strLocationNo, strPalletNO);
            if (dt == null)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("栈板与储位不匹配", -1);
                txtCheckPallet.SelectAll();
                txtCheckPallet.Focus();
                return;
            }
            else
            {
                if (dt.Rows.Count == 0)
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg("栈板与储位匹配记录为空", -1);
                    txtCheckPallet.SelectAll();
                    txtCheckPallet.Focus();
                    return;
                }
                else
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    ShowMsg("OK", -1);
                    txtCheckPallet.Text = "";
                    txtCheckLocation.Text = "";
                    txtCheckLocation.SelectAll();
                    txtCheckLocation.Focus();
                }
            }

        }

        private void txtCheckPallet0_KeyDown(object sender, KeyEventArgs e)
        {
            string strPalletNO = string.Empty;
            strPalletNO = txtCheckPallet0.Text.Trim().ToUpper();
            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strPalletNO))
            {

                txtCheckPallet0.SelectAll();
                txtCheckPallet0.Focus();
                return;
            }
            txtCheckPallet.SelectAll();
            txtCheckPallet.Focus();


        }

        private void btnEXCEL2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string strLocation = txtDockLocation.Text;
            string strPallet = txtCurrPalletNo.Text;
            if (string.IsNullOrEmpty(strLocation) || string.IsNullOrEmpty(strPallet)) { return; }
            string strPrintContext = strLocation + "," + strPallet + ",";
            PrintPickSAPNOLabel(strPrintContext);
        }

        private void PrintPickSAPNOLabel(string strPrintContext)
        {
            string strLabelName = @"DockLocationPallet";
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\WMS\Label";
            //这部分是写.dat文件。
            string LabelParam = @"LOCATION_NO,PALLET_NO,";
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
                sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".lst" + '"').Replace("@QTY", "1");
                p.StartInfo.Arguments = sArguments;
                p.Start();
                p.WaitForExit();
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


    }
}
