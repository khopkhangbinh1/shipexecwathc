using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NPIPickListAC
{
    public partial class addPalletWeight : Form
    {
        public addPalletWeight()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        
      

       
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

            NPIPickListACBLL nb = new NPIPickListACBLL();
            dgvPallet.DataSource = nb.GetPalletWeightINFO();
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

                string strPalletNo = dgvPallet.Rows[g_curRow].Cells["pallet_no"].Value.ToString();

                txtPalletNo.Text = strPalletNo;
                ShowMsg("", 0);
               
            }
        }

        private void txtPalletHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txtPalletHeight.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txtPalletHeight.Text, out oldf);
                    b2 = float.TryParse(txtPalletHeight.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void txtPalletWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txtPalletWeight.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txtPalletWeight.Text, out oldf);
                    b2 = float.TryParse(txtPalletWeight.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;

            string strPalletNo = txtPalletNo.Text;
            string strPalletSize = cmbPalletSize.Text;
            string strPalletHeight = txtPalletHeight.Text;
            string strPalletWeight = txtPalletWeight.Text;
            string strPalletEmptyCarton = txtEmptyCarton.Text;
            if (string.IsNullOrEmpty(strPalletEmptyCarton)) { strPalletEmptyCarton = "0"; }

            if (string.IsNullOrEmpty(strPalletNo) || string.IsNullOrEmpty(strPalletSize) || string.IsNullOrEmpty(strPalletHeight) || string.IsNullOrEmpty(strPalletWeight) ) 
            {
                ShowMsg("请全部维护值再做", 0);
                btnUpdate.Enabled = true;
                return;
            }
            string strResult = string.Empty;
            string strResultMsg = string.Empty;
            NPIPickListACBLL ub = new NPIPickListACBLL();
            //strResult = ub.NPIUpdatePalletWeight(strPalletNo, strPalletSize, strPalletHeight, strPalletWeight, out strResultMsg);
            strResult = ub.NPIUpdatePalletWeight2(strPalletNo, strPalletSize, strPalletHeight, strPalletWeight, strPalletEmptyCarton, out strResultMsg);

            if (strResult.StartsWith("NG"))
            {
                ShowMsg(strResultMsg, 0);
                btnUpdate.Enabled = true;
                return;
            }
            dgvPallet.DataSource = null;
            dgvPallet.Rows.Clear();

            NPIPickListACBLL nb = new NPIPickListACBLL();
            dgvPallet.DataSource = nb.GetPalletWeightINFO();
            txtPalletWeight.Text = "";
            txtPalletHeight.Text = "";
            btnUpdate.Enabled = true;
        }

        private void addPalletWeight_Load(object sender, EventArgs e)
        {
            NPIPickListACBLL nb = new NPIPickListACBLL();
            string strPalletSizeList =string.Empty;
            string strOutMsg = string.Empty;
            string strResult= nb.PPSGetbasicparameter("NPI_PALLETSIZE", out strPalletSizeList, out strOutMsg);

            string[] strPalletSize = strPalletSizeList.Split('#');
            cmbPalletSize.Items.Clear();
            foreach (var item in strPalletSize)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    cmbPalletSize.Items.Add(item);
                }
            }

        }

        private void txtEmptyCarton_KeyPress(object sender, KeyPressEventArgs e)
        {
            //阻止从键盘输入键
            e.Handled = true;

            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)8))
            {

                if ((e.KeyChar == (char)8)) { e.Handled = false; return; }
                else
                {
                    int len = txtEmptyCarton.Text.Length;
                    
                    if (len == 0 && e.KeyChar != '0')
                    {
                        e.Handled = false; return;
                    }
                    else if (len >0  && txtEmptyCarton.Text.Substring(0,1).Equals("0"))
                    {
                        MessageBox.Show("编号不能以0开头！"); return;
                    }
                    e.Handled = false; return;
                    
                }
            }
            else
            {
                MessageBox.Show("编号只能输入数字！");
            }


        }
    }
}
