using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EDIWarehouseTools
{
    public partial class fWMSCarToPallet : Form
    {
        public int H = 0;
        public int W = 0;
        public fWMSCarToPallet()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void fWMSCarToPallet_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            initGroupBox();
            //initLocationFrom();
            //initLocationTo();

        }
        private void initGroupBox()
        {
            W = Screen.PrimaryScreen.Bounds.Width;
          
               W = Convert.ToInt32(W * 0.5);
            this.grpFrom.Width =W;
        }

        private void initLocationFrom()
        {
            string strSql = @"select distinct a.trolley_line_no id, a.trolley_line_no name "
                        + " from ppsuser.t_trolley_line_info a "
                        + "  where(trolley_no, sides_no, level_no, seq_no) in "
                        + "        (select distinct trolley_no, sides_no, level_no, seq_no "
                        + "           from ppsuser.t_trolley_sn_status  "
                        + "              where trolley_no <> 'ICT-00-00-000' ) "
                        + " order by id asc";
            EDIWarehouseToolsBLL wb = new EDIWarehouseToolsBLL();
            wb.fillCmb(strSql, "", cmbLocationFrom);
        }

        private void initLocationFrom(string strCarNo)
        {
            string strSql = string.Format(@"
                                    select distinct a.trolley_line_no id, a.trolley_line_no name
                                      from ppsuser.t_trolley_line_info a
                                     where trolley_no ='{0}'
                                       and trolley_no not in ('ICT-00-00-000', 'ICT-00-00-SYS')
                                     order by id asc", strCarNo);
            EDIWarehouseToolsBLL wb = new EDIWarehouseToolsBLL();
            wb.fillCmb(strSql, "", cmbLocationFrom);
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

        private void cmbLocationFrom_SelectedIndexChanged(object sender, EventArgs e)
        {

            EDIWarehouseToolsBLL wb = new EDIWarehouseToolsBLL();
            wb.ShowStockInfo(cmbLocationFrom.Text, dgvFindResultFrom);
        }

        private void cmbLocationTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            EDIWarehouseToolsBLL wb = new EDIWarehouseToolsBLL();
            //wb.ShowStockInfo(cmbLocationTo.Text, dgvFindResultTo);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ShowMsg("", 0);
            if (dgvFindResultFrom.RowCount == 0)
            {
                ShowMsg("先选择车行号，点击查询", 0);
                return;
            }
            //if (cmbLocationFrom.Text.Equals(cmbLocationTo.Text))
            //{
            //    ShowMsg("相同位置不需要转移", 0);
            //    return;
            //}
            cmbLocationFrom.Enabled = false;
            //cmbLocationTo.Enabled = false;
            btnStart.Enabled = false;
            btnSearch.Enabled = false;
            btnEnd.Enabled = true;

            txtSN.Enabled = true;
            txtSN.Focus();
            txtSN.SelectAll();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            cmbLocationFrom.Enabled = true;
            //cmbLocationTo.Enabled = true;
            btnStart.Enabled = true;
            btnSearch.Enabled = true;
            btnEnd.Enabled = false;

            txtSN.Text = "";
            txtSN.Enabled = false;
            ShowMsg("", -1);
        }
        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            string strCSN = txtSN.Text;
            string strLocationNoTo = txtLocationTo.Text;
           
            if (e.KeyCode != Keys.Enter)
            { return; }
            EDIWarehouseToolsBLL plb = new EDIWarehouseToolsBLL();
            strCSN = plb.DelPrefixCartonSN(strCSN);

            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCSN))
            {
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }
            if (string.IsNullOrEmpty(strLocationNoTo))
            {
                ShowMsg("储位号不能为空", -1);
                return;
            }

            #region
            string strResult = string.Empty;
            string strCarlineno = string.Empty;
            string strResulterrmsg = string.Empty;
            EDIWarehouseToolsBLL wb = new EDIWarehouseToolsBLL();
            strResult = wb.GetCarlinenoByCSN(strCSN, out strCarlineno, out strResulterrmsg);
            if (strResult.Equals("OK"))
            {
                if (chkCarLineNo.Checked ) 
                {
                    
                    if (!strCarlineno.Equals(cmbLocationFrom.Text)) 
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        ShowMsg("箱号不属于车行号", 0);
                        return;
                    }
                }
                cmbLocationFrom.Text = strCarlineno;
                    
                //wb.ShowStockInfo2(cmbLocationFrom.Text, dgvFindResultFrom, strCSN);
                //ShowdgvNoColor(strCSN, dgvFindResultFrom);

                string strResult2 = string.Empty;
                string strResulterrmsg2 = string.Empty;
                strResult2 = wb.WmstCarToPalletTrans(strCSN,strLocationNoTo , out strResulterrmsg2);
               
                if (strResult2.Equals("OK"))
                {
                    wb.ShowStockInfo(cmbLocationFrom.Text, dgvFindResultFrom);
                    wb.ShowStockInfo2(txtLocationTo.Text, dgvFindResultTo);
                    //showdgvNoColor(strCSN, dgvFindResultTo);
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    ShowMsg("OK", -1);
                }
                else
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    ShowMsg(strResulterrmsg2, 0);
                }
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg(strResulterrmsg, 0);
            }
            txtSN.Text = "";
            txtSN.Focus();
            txtSN.SelectAll();
            #endregion
            
            
        }

        private void showdgvNoColor(string incsn,DataGridView dgv)
        {
            #region
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                //dgv.Rows[i].HeaderCell.Value = (i + 1).ToString();
                ////按条件设置每一行的颜色属性
                //this.dgvNo.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                if (dgv.Rows[i].Cells["CUSTOM_SN"].Value.ToString().Contains(incsn))
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    return;
                }

            }
            #endregion
        }

        public DialogResult ShowMsg(string strTxt, int strType)
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
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
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
        public void PartError()
        {
            LibHelper.MediasHelper.PlaySoundAsyncByPartError();
        }

        private void txtLocationTo_KeyDown(object sender, KeyEventArgs e)
        {
            string strLocationNoTo = txtLocationTo.Text;
            if (e.KeyCode != Keys.Enter)
            { return; }
            if (string.IsNullOrEmpty(strLocationNoTo))
            {
                ShowMsg("储位号不能为空", -1);
                return;
            }
            EDIWarehouseToolsBLL wb = new EDIWarehouseToolsBLL();
            wb.ShowStockInfo2(txtLocationTo.Text, dgvFindResultTo);
        }

        private void txtCarNo_KeyDown(object sender, KeyEventArgs e)
        {
            string strCarNo = txtCarNo.Text;
            if (e.KeyCode != Keys.Enter)
            { return; }
            if (string.IsNullOrEmpty(strCarNo))
            {
                return;
            }

            EDIWarehouseToolsBLL eb = new EDIWarehouseToolsBLL();
            string strResultMsg = string.Empty;
            string strResult = eb.WmstCheckTrolleyInfo(strCarNo, out strResultMsg);
            if (!strResult.Equals("OK"))
            {
                Ng();
                ShowMsg(strResultMsg, 0);
                return;
            }

            initLocationFrom(strCarNo);

        }
    }


}
