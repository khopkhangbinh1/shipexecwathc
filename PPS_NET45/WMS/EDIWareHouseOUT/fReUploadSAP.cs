using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EDIWareHouseOUT
{
    public partial class fReUploadSAP : Form
    {
        public fReUploadSAP()
        {
            InitializeComponent();
        }

        private void fReUploadSAP_Load(object sender, EventArgs e)
        {
            DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            dt_start.Value = dateTimeNow.AddDays(-1);
            dt_end.Value = dateTimeNow.AddDays(1);
        }
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sUserID = ClientUtils.UserPara1;
        private string g_ServerIP = ClientUtils.url;



        private string changeSNtoPickPalletno(string SNtoPickPalletno)
        {
            
            string sql = string.Format("Select pack_pallet_no from nonedipps.t_sn_status where customer_sn='{0}' or carton_no='{1}' or pack_pallet_no='{2}'", SNtoPickPalletno, SNtoPickPalletno, SNtoPickPalletno);
            DataTable dt_change = new DataTable();
            try
            {
                dt_change = ClientUtils.ExecuteSQL(sql).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }


            if (dt_change.Rows.Count > 0)
            {
                //如果输入的时real_pallet_no 或者时print_pallet_no 
                //转换位pallet_no 来处理
                SNtoPickPalletno = dt_change.Rows[0]["pack_pallet_no"].ToString();
                return SNtoPickPalletno;
            }
            else
            {
                return "";
            }

        }


        public DialogResult ShowMsg(string sText, int iType)
        {
            TextMsg.Text = sText;
            switch (iType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Yellow;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Black;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvPallet.DataSource = null;
            dgvPallet.Rows.Clear();
            string strStime = dt_start.Value.ToString("yyyy-MM-dd");
            string strEtime = dt_end.Value.ToString("yyyy-MM-dd");
            EDIWarehouseOUTBLL nb = new EDIWarehouseOUTBLL();
            if (chkALL.Checked) 
            {
                dgvPallet.DataSource = nb.GetUnPICKSAPALLINFO(strStime, strEtime);
            }
            else 
            { 
                dgvPallet.DataSource = nb.GetUnPICKSAPINFO(strStime, strEtime);
            }
            
        }

        private void dgvPallet_SelectionChanged(object sender, EventArgs e)
        {
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvPallet.CurrentRow.Index;
                //rowIndex = dgvNo.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvPallet.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                //if (g_curRow == rowIndex)
                //    return;
                int g_curRow = rowIndex;

                string strWHOUTTYPE= dgvPallet.Rows[g_curRow].Cells["whouttype"].Value.ToString();
                string strSAPNO = dgvPallet.Rows[g_curRow].Cells["sap_no"].Value.ToString();
                string strPICKSAPNO = dgvPallet.Rows[g_curRow].Cells["pick_sap_no"].Value.ToString();
                string strFBMESStatus = dgvPallet.Rows[g_curRow].Cells["fb_mes_status"].Value.ToString();
                txtSAPNO.Text = strSAPNO;
                txtPickSAPNO.Text = strPICKSAPNO;
                txtWHOutType.Text = strWHOUTTYPE;
                txtFBMESStatus.Text = strFBMESStatus;
                ShowMsg("", 0);
               
            }
        }

        
       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpload.Enabled = false;

            string strFBMESStatus=txtFBMESStatus.Text  ;
            if (!strFBMESStatus.Equals("Y"))
            {
                ShowMsg("SAPNO对于箱号存在未反馈MES", 0);
                btnUpload.Enabled = true;
                return;
            }
            string strWHOutType = string.Empty;
            if (txtWHOutType.Text.Contains("ZSF"))
            {
                strWHOutType = "ZSF";
            }
            else if (txtWHOutType.Text.Contains("ZTL"))
            {
                strWHOutType = "ZTL";
            }
            else if(txtWHOutType.Text.Contains("ZBOMR"))
            {
                strWHOutType = "ZBOMR";
            }
            string strSAPNO = txtSAPNO.Text;
            string strPICKSAPNO = txtPickSAPNO.Text;
            if (string.IsNullOrEmpty(strSAPNO) ) 
            {
                ShowMsg("请全部维护值再做", 0);
                btnUpload.Enabled = true;
                return;
            }
            EDIWarehouseOUTBLL eb = new EDIWarehouseOUTBLL();
            //0. 检查
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
            string strSAPNOComputerName = eb.GetComputerNameOfSAPNO( strSAPNO);
            if (!string.IsNullOrEmpty(strSAPNOComputerName) ) 
            {
                ShowMsg("此SAP单正在电脑名:"+ strSAPNOComputerName + "上作业不得扣账", 0);
                return;
            }
            
            string strResult0 = string.Empty;
            string strResultOut = string.Empty;
            
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS 执行反馈SAP Start", strSAPNO+"*"+ strPICKSAPNO + "*" + strWHOutType + "*" + g_sUserNo + "*" + g_ServerIP);
            strResult0 = eb.WMSOUplodSapNoWebService(strSAPNO, strPICKSAPNO, strWHOutType, g_sUserNo, g_ServerIP, out strResultOut);
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS 执行反馈SAP End", strSAPNO + "*" + strPICKSAPNO + "*" + strWHOutType + "*" + g_sUserNo + "*" + g_ServerIP);

            if (strResult0.StartsWith("NG"))
            {
                ShowMsg(strResultOut, 0);
                btnUpload.Enabled = true;
                return;
            }
            else
            {
                //删除储位的资料
                string strDELETEResult = string.Empty;
                Boolean isDeleteOK = eb.WMSODELETESN2(strPICKSAPNO, out strDELETEResult);
                
                if (!isDeleteOK)
                {
                    strResultOut = strResultOut + "#清除PPS资料失败:" + strDELETEResult+",请联系IT处理";
                }
                else
                {
                    strResultOut = strResultOut + "#清除PPS资料:" + strDELETEResult;
                }
            }
            ShowMsg(strResultOut, 0);
            dgvPallet.DataSource = null;
            dgvPallet.Rows.Clear();
            string strStime = dt_start.Value.ToString("yyyy-MM-dd");
            string strEtime = dt_end.Value.ToString("yyyy-MM-dd");
            dgvPallet.DataSource = eb.GetUnPICKSAPINFO( strStime,  strEtime);
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS 清除PPS资料刷新界面 End", "PICKSAPNO:"+ strPICKSAPNO);

            btnUpload.Enabled = true;
        }

        private void TextMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                TextMsg.SelectAll();
        }
    }
}
