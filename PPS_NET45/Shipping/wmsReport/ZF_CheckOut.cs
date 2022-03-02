using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wmsReport.Core;
using wmsReport.Utils;

namespace wmsReport
{
    public partial class ZF_CheckOut : Form
    {
        private Controller controller;
        private string g_sUserNo = ClientUtils.fLoginUser;
        public ZF_CheckOut()
        {
            InitializeComponent();
            controller = new Controller();
        }

     
        private void Show_Message2(string msg, int type)
        {
            lblMessage.Text = msg;
            switch (type)
            {
                case 0: //error
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.BackColor = Color.Yellow;
                    break;
                case 1:
                    lblMessage.ForeColor = Color.Blue;
                    lblMessage.BackColor = Color.White;
                    break;
                default:
                    lblMessage.ForeColor = Color.Black;
                    lblMessage.BackColor = Color.White;
                    break;
            }
        }
     
        private void btnCommit_Click(object sender, EventArgs e)
        {
            int c_totalQty = 0;
            string strDN = this.txtDN.Text.ToUpper().Trim();
            string strPartNo = this.txtPartNo.Text.ToUpper().Trim();
            string strDNCount = this.txtDNCount.Text.ToUpper().Trim();
            string strEMP = this.txtEmp.Text.ToUpper().Trim();
            string strMark = this.txtMark.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(strDN))
            {
                Show_Message2("TT_订单号没有输入，不可以提交！", 0);
                return;
            }
            if (string.IsNullOrEmpty(strPartNo))
            {
                Show_Message2("料号没有输入，不可以提交！", 0);
                return;
            }
            if (string.IsNullOrEmpty(strDNCount))
            {
                Show_Message2("数量没有输入，不可以提交！", 0);
                return;
            }
            try
            {
                c_totalQty = Convert.ToInt32(strDNCount);
            }
            catch (Exception)
            {
                Show_Message2("数量输入的是非数字，不可以提交!", 0);
                return;
            }
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.insertTTOrderInfo(strDN, strPartNo, c_totalQty, strMark, strEMP);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                dt.Columns.RemoveAt(0);
                //dgvDN.DataSource = null;
                //dgvDN.Rows.Clear();
                this.dgvDN.DataSource = dt;


                this.txtDN.Clear();
                this.txtDN.Focus();
                this.txtPartNo.Clear();
                this.txtDNCount.Clear();
                this.txtEmp.Clear();
                this.txtMark.Clear();
                Show_Message2(exeRes.Message, 1);
            }
            else
            {
                Show_Message2(exeRes.Message, 0);
                return;
            }
        }

        //private void s_query_BTN_Click(object sender, EventArgs e)
        //{
        //    string s_tt_order = this.s_tt_order_TB.Text.ToUpper().Trim();
        //    string s_ictPn = this.s_ictPn_TB.Text.ToUpper().Trim();
        //    string start_time = this.start_time_DTP.Value.ToString("yyyyMMdd");
        //    string end_time = this.end_time_DTP.Value.ToString("yyyyMMdd");
        //    ExecuteResult exeRes = new ExecuteResult();
        //    exeRes = controller.getTTOrderInfoByConditions(s_tt_order,s_ictPn, start_time,end_time);
        //    if (exeRes.Status)
        //    {
        //        this.s_tt_order_DGV.DataSource = (DataTable)exeRes.Anything;
        //        sec_msg_LB.Text = exeRes.Message;
        //        sec_msg_LB.ForeColor = Color.Blue;
        //        sec_msg_LB.BackColor = Color.White;
        //    }
        //    else
        //    {
        //        sec_msg_LB.Text = exeRes.Message;
        //        sec_msg_LB.ForeColor = Color.Red;
        //        sec_msg_LB.BackColor = Color.Yellow;
        //    }
        //}

      

        //private void tt_on_process_locationNo_TB_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        this.tt_t_location_DGV.DataSource = null;
        //        string ttOnProcessLocationNo = this.tt_on_process_locationNo_TB.Text.Trim().ToUpper();
        //        if (string.IsNullOrEmpty(ttOnProcessLocationNo))
        //        {
        //            sec_msg_LB.Text = "请输入储位号！";
        //            sec_msg_LB.ForeColor = Color.Red;
        //            sec_msg_LB.BackColor = Color.Yellow;
        //            return;
        //        }
        //        string ttOnProcessIctPn = this.tt_on_process_ictpn_TB.Text.Trim().ToUpper();
        //        ExecuteResult exeRes = new ExecuteResult();
        //        DataTable dt = new DataTable();
        //        exeRes = controller.checkIsExistIctPnOnLocationByLocationNoAndIctPn(ttOnProcessIctPn,ttOnProcessLocationNo);
        //        if (exeRes.Status)
        //        {
        //            dt = (DataTable)exeRes.Anything;
        //            this.tt_t_location_DGV.DataSource = TransferDataTable.RowsToCol(dt);
        //            this.tt_on_process_locationNo_TB.Enabled = false;
        //            this.tt_on_process_cartonNo_TB.Focus();
        //            this.tt_on_process_cartonNo_TB.SelectAll();
        //            sec_msg_LB.Text = exeRes.Message;
        //            sec_msg_LB.ForeColor = Color.Blue;
        //            sec_msg_LB.BackColor = Color.White;
        //        }
        //        else
        //        {
        //            sec_msg_LB.Text = exeRes.Message;
        //            sec_msg_LB.ForeColor = Color.Red;
        //            sec_msg_LB.BackColor = Color.Yellow;
        //            return;
        //        }
        //    }
        //}

        //private void tt_on_process_cartonNo_TB_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string cartonNo = this.tt_on_process_cartonNo_TB.Text.Trim().ToUpper();
        //        if (string.IsNullOrEmpty(cartonNo))
        //        {
        //            sec_msg_LB.Text ="请输入箱号！";
        //            this.tt_on_process_cartonNo_TB.Focus();
        //            this.tt_on_process_cartonNo_TB.SelectAll();
        //            sec_msg_LB.ForeColor = Color.Red;
        //            sec_msg_LB.BackColor = Color.Yellow;
        //            return;
        //        }
        //        string locationNo = this.tt_on_process_locationNo_TB.Text.Trim().ToUpper();
        //        string ictPn = this.tt_on_process_ictpn_TB.Text.Trim().ToUpper();
        //        ExecuteResult exeRes = new ExecuteResult();
        //        exeRes = controller.checkIsExistCartonNoIsLinkIctPn(cartonNo,ictPn);
        //        if (!exeRes.Status)
        //        {
        //            this.tt_on_process_cartonNo_TB.Focus();
        //            this.tt_on_process_cartonNo_TB.SelectAll();
        //            sec_msg_LB.Text = exeRes.Message;
        //            sec_msg_LB.ForeColor = Color.Red;
        //            sec_msg_LB.BackColor = Color.Yellow;
        //            return;
        //        }
        //        exeRes = controller.checkIsExistCartonNoIsLinkLocationNO(cartonNo,locationNo);
        //        if (!exeRes.Status)
        //        {
        //            this.tt_on_process_cartonNo_TB.Focus();
        //            this.tt_on_process_cartonNo_TB.SelectAll();
        //            sec_msg_LB.Text = exeRes.Message;
        //            sec_msg_LB.ForeColor = Color.Red;
        //            sec_msg_LB.BackColor = Color.Yellow;
        //            return;
        //        }
        //        string tt_order = this.s_tt_order_TB.Text.Trim().ToUpper();
        //        string tt_id =  this.tt_id.Text.Trim().ToUpper();
        //        DataTable dt = new DataTable();
        //        exeRes = controller.ZFPassStation(tt_id,tt_order,ictPn,locationNo,cartonNo);
        //        if (exeRes.Status)
        //        {
        //            cartons_listBox.Items.Add(cartonNo);
        //            dt = (DataTable)exeRes.Anything;
        //            this.account_LB.Text = dt.Rows[0]["C_MSG"].ToString();
        //            this.tt_on_process_cartonNo_TB.Focus();
        //            this.tt_on_process_cartonNo_TB.SelectAll();
        //            sec_msg_LB.Text = "此箱杂发出货成功，请刷下一箱！";
        //            sec_msg_LB.ForeColor = Color.Blue;
        //            sec_msg_LB.BackColor = Color.White;
        //        }
        //        else
        //        {
        //            this.tt_on_process_cartonNo_TB.Focus();
        //            this.tt_on_process_cartonNo_TB.SelectAll();
        //            sec_msg_LB.Text = exeRes.Message;
        //            sec_msg_LB.ForeColor = Color.Red;
        //            sec_msg_LB.BackColor = Color.Yellow;
        //            return;
        //        }
        //    }
        //}

      
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDN.Clear();
            this.txtDN.Focus();
            this.txtPartNo.Clear();
            this.txtDNCount.Clear();
            this.txtEmp.Clear();
            this.txtMark.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strDN = this.txtDN2.Text.ToUpper().Trim();
            string strPart = this.txtPartNO2.Text.ToUpper().Trim();
            string strStartTime = this.dtpStart.Value.ToString("yyyyMMdd");
            string strEndTime = this.dtpEnd.Value.ToString("yyyyMMdd");
            ExecuteResult exeRes = new ExecuteResult();
            exeRes = controller.getTTOrderInfoByConditions(strDN, strPart, strStartTime, strEndTime);
            if (exeRes.Status)
            {
                this.dgvDN2.DataSource = (DataTable)exeRes.Anything;
                lblMessage2.Text = exeRes.Message;
                lblMessage2.ForeColor = Color.Blue;
                lblMessage2.BackColor = Color.White;
            }
            else
            {
                lblMessage2.Text = exeRes.Message;
                lblMessage2.ForeColor = Color.Red;
                lblMessage2.BackColor = Color.Yellow;
            }
        }

        private void dgvDN2_DoubleClick(object sender, EventArgs e)
        {
            if(this.dgvDN2.DataSource != null)
            {
                string strDN = this.dgvDN2.Rows[dgvDN2.CurrentRow.Index].Cells[1].Value.ToString();
                string strQTY = this.dgvDN2.Rows[dgvDN2.CurrentRow.Index].Cells[4].Value.ToString();
                string strTotalQty = this.dgvDN2.Rows[dgvDN2.CurrentRow.Index].Cells[3].Value.ToString();
                this.txtProcessPart.Text = this.dgvDN2.Rows[dgvDN2.CurrentRow.Index].Cells[2].Value.ToString();
                this.lblID.Text = this.dgvDN2.Rows[dgvDN2.CurrentRow.Index].Cells[0].Value.ToString();
                this.txtProcessDN.Text = strDN;
                this.lblCurrentQTY.Text = strQTY + "/" + strTotalQty;
                this.lstCarton.Items.Clear();
            }
        }
        //private void s_tt_order_DGV_DoubleClick(object sender, EventArgs e)
        //{
        //    if (this.s_tt_order_DGV.DataSource != null)
        //    {
        //        string tt_orderNo = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[1].Value.ToString();
        //        string outQty = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[4].Value.ToString();
        //        string totalQty = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[3].Value.ToString();
        //        this.tt_on_process_ictpn_TB.Text = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[2].Value.ToString();
        //        this.tt_id.Text = this.s_tt_order_DGV.Rows[s_tt_order_DGV.CurrentRow.Index].Cells[0].Value.ToString();
        //        this.tt_on_process_ttOrder_TB.Text = tt_orderNo;
        //        this.account_LB.Text = outQty + "/" + totalQty;
        //        this.cartons_listBox.Items.Clear();
        //    }
        //}


        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dgvLocation.DataSource = null;
                string strLoactionNO = this.txtLocation.Text.Trim().ToUpper();
                if (string.IsNullOrEmpty(strLoactionNO))
                {
                    lblMessage2.Text = "请输入储位号！";
                    lblMessage2.ForeColor = Color.Red;
                    lblMessage2.BackColor = Color.Yellow;
                    return;
                }
                string strPartNO = this.txtProcessPart.Text.Trim().ToUpper();
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.checkIsExistIctPnOnLocationByLocationNoAndIctPn(strPartNO, strLoactionNO);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    //this.dgvLocation.DataSource = TransferDataTable.RowsToCol(dt);
                    this.dgvLocation.DataSource = dt;
                    this.txtLocation.Enabled = false;
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    lblMessage2.Text = exeRes.Message;
                    lblMessage2.ForeColor = Color.Blue;
                    lblMessage2.BackColor = Color.White;
                }
                else
                {
                    lblMessage2.Text = exeRes.Message;
                    lblMessage2.ForeColor = Color.Red;
                    lblMessage2.BackColor = Color.Yellow;
                    return;
                }
            }
        }

        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cartonNo = this.txtCarton.Text.Trim().ToUpper();
                if (string.IsNullOrEmpty(cartonNo))
                {
                    lblMessage2.Text = "请输入箱号！";
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    lblMessage2.ForeColor = Color.Red;
                    lblMessage2.BackColor = Color.Yellow;
                    return;
                }
                string locationNo = this.txtLocation.Text.Trim().ToUpper();
                string ictPn = this.txtProcessPart.Text.Trim().ToUpper();
                ExecuteResult exeRes = new ExecuteResult();
                exeRes = controller.checkIsExistCartonNoIsLinkIctPn(cartonNo, ictPn);
                if (!exeRes.Status)
                {
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    lblMessage2.Text = exeRes.Message;
                    lblMessage2.ForeColor = Color.Red;
                    lblMessage2.BackColor = Color.Yellow;
                    return;
                }
                exeRes = controller.checkIsExistCartonNoIsLinkLocationNO(cartonNo, locationNo);
                if (!exeRes.Status)
                {
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    lblMessage2.Text = exeRes.Message;
                    lblMessage2.ForeColor = Color.Red;
                    lblMessage2.BackColor = Color.Yellow;
                    return;
                }
                string tt_order = this.txtProcessDN.Text.Trim().ToUpper();
                string tt_id = this.lblID.Text.Trim().ToUpper();
                DataTable dt = new DataTable();
                exeRes = controller.ZFPassStation(tt_id, tt_order, ictPn, locationNo, cartonNo);
                if (exeRes.Status)
                {
                    lstCarton.Items.Add(cartonNo);
                    dt = (DataTable)exeRes.Anything;
                    this.lblCurrentQTY.Text = dt.Rows[0]["C_MSG"].ToString();
                    string[] strqty = dt.Rows[0]["C_MSG"].ToString().Split('/');
                  
                    string strqty1 = strqty[0];
                    string strqty2 = strqty[1];


                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();

                    if (strqty1.Equals(strqty2) && !strqty2.Equals("0"))
                    { lblMessage2.Text = "订单作业完成。"; }
                    else
                    { lblMessage2.Text = "此箱杂发出货成功，请刷下一箱！"; }
                    
                    lblMessage2.ForeColor = Color.Blue;
                    lblMessage2.BackColor = Color.White;
                }
                else
                {
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    lblMessage2.Text = exeRes.Message;
                    lblMessage2.ForeColor = Color.Red;
                    lblMessage2.BackColor = Color.Yellow;
                    return;
                }
            }
        }

        private void ZF_CheckOut_Load(object sender, EventArgs e)
        {
            DateTime dateTimeNow = DateTime.Now;
            dtpEnd.Value = dateTimeNow.AddDays(1);
            txtEmp.Text = g_sUserNo;
            initComboBox();
            this.WindowState = FormWindowState.Maximized;
        }
        private void initComboBox()
        {

            //MPN
            cmbPartno.DataSource = null;
            cmbPartno.Items.Clear();
            cmbPartno2.DataSource = null;
            cmbPartno2.Items.Clear();
            string strSql = @"select trim(ICTPARTNO||'*'||MPN||'*'||PACKUNIT||'*'||max(TOTALQTY))  as MPN1  "
                            + " from ppsuser.vw_mpn_info "
                            //where packcode <> subpackcode or subpackcode is null"
                            //+ " where  MPN like 'M%' and MPN like '%A' "
                            + " group by   ICTPARTNO ,MPN , PACKUNIT  "
                            + " order by MPN1 ";

            DataSet dts = ClientUtils.ExecuteSQL(strSql);
            if (dts != null && dts.Tables[0].Rows.Count > 0)
            {


                List<string> carrierList = (from d in dts.Tables[0].AsEnumerable()
                                            select d.Field<string>("MPN1")).ToList();
                carrierList.Sort();
                cmbPartno.DataSource = carrierList;
                cmbPartno2.DataSource = carrierList;

            }
            else
            {
                cmbPartno.DataSource = null;
                cmbPartno2.DataSource = null;
            }
        }

        private void cmbPartno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPartno.Text))
            {
                string ictpartnompn = cmbPartno.Text;
                string[] partmpn = ictpartnompn.Split('*');
                string strpartno = partmpn[0];
                string strmpn = partmpn[1];
                string strpackunit = partmpn[2];
                txtPartNo.Text = strpartno;
                //通过料号查upccode 
                string strUPC = string.Empty;
                string strJAN = string.Empty;
                string strCustModel = string.Empty;
                //txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
                //lblJAN.Text = strJAN;
                //lblUPC.Text = strUPC;
                //lblModel.Text = strCustModel;
            }
        }

        private void cmbPartno2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPartno.Text))
            {
                string ictpartnompn = cmbPartno.Text;
                string[] partmpn = ictpartnompn.Split('*');
                string strpartno = partmpn[0];
                string strmpn = partmpn[1];
                string strpackunit = partmpn[2];
                txtPartNO2.Text = strpartno;
                //通过料号查upccode 
                string strUPC = string.Empty;
                string strJAN = string.Empty;
                string strCustModel = string.Empty;
                //txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
                //lblJAN.Text = strJAN;
                //lblUPC.Text = strUPC;
                //lblModel.Text = strCustModel;
            }
        }

        private void txtDNCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
    }
}
