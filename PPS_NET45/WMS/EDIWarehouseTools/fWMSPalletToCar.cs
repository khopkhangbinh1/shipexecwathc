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
    public partial class fWMSPalletToCar : Form
    {
        public int H = 0;
        public int W = 0;
        public fWMSPalletToCar()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void fWMSPalletToCar_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            initGroupBox();
            //initLocationTo();

        }
        private void initGroupBox()
        {
            W = Screen.PrimaryScreen.Bounds.Width;
          
               W = Convert.ToInt32(W * 0.5);
            
        
            this.grpFrom.Width =W;
        }

       
        private void initLocationTo(string strCarNo)
        {
            string strSql = string.Format(@"
                                    select distinct a.trolley_line_no id, a.trolley_line_no name
                                      from ppsuser.t_trolley_line_info a
                                     where trolley_no ='{0}'
                                       and trolley_no not in ('ICT-00-00-000', 'ICT-00-00-SYS')
                                     order by id asc", strCarNo);
            EDIWarehouseToolsBLL wb = new EDIWarehouseToolsBLL();
            wb.fillCmb(strSql, "", cmbLocationTo);
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

       

        private void cmbLocationTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            EDIWarehouseToolsBLL wb = new EDIWarehouseToolsBLL();
            wb.ShowStockInfo(cmbLocationTo.Text, dgvFindResultTo);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ShowMsg("", 0);
            if (dgvFindResultFrom.RowCount == 0)
            {
                ShowMsg("先选择车行号，点击查询", 0);
                return;
            }
            
            cmbLocationTo.Enabled = false;
            btnStart.Enabled = false;
            btnSearch.Enabled = false;
            btnEnd.Enabled = true;

            txtSN.Enabled = true;
            txtSN.Focus();
            txtSN.SelectAll();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            cmbLocationTo.Enabled = true;
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
           
            if (e.KeyCode != Keys.Enter)
            { return; }
            EDIWarehouseToolsBLL eb = new EDIWarehouseToolsBLL();
            strCSN = eb.DelPrefixCartonSN(strCSN);
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCSN))
            {
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }

            string strCarLineNo = cmbLocationTo.Text;
            if (string.IsNullOrEmpty(strCarLineNo))
            {
                txtSN.SelectAll();
                txtSN.Focus();
                ShowMsg("车行号不能为空",0);
                return;
            }

            string strResultMsg = string.Empty;
            string strResult = eb.WmstPalletToCarTrans(strCSN, strCarLineNo, out  strResultMsg);
            if (!strResult.Equals("OK"))
            {
                Ng();
                txtSN.SelectAll();
                txtSN.Focus();
                ShowMsg(strResultMsg, 0);
                return;
            }
            Ok();
            txtSN.SelectAll();
            txtSN.Focus();
            eb.ShowStockInfo(strCarLineNo, dgvFindResultTo);
            showdgvNoColor( strCSN, dgvFindResultTo);

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

            initLocationTo(strCarNo);
        }

        
    }


}
