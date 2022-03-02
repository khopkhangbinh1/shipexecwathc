using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wmsReportAC
{
    public partial class fWMSQuery : Form
    {
        public fWMSQuery()
        {
            InitializeComponent();
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
                LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }

            dgvDetail.Rows.Clear();

            WMSBLL plb = new WMSBLL();
            strSN = plb.DelPrefixCartonSN(strSN);
            
            string strSQL2;
            strSQL2 = "SELECT CUSTOMER_SN, CARTON_NO, PART_NO, LOCATION_NO, PALLET_NO  FROM NONEDIPPS.T_SN_STATUS " +
                     " WHERE (CUSTOMER_SN = '" + strSN.Replace("'", "''") + "' or Carton_NO ='" + strSN.Replace("'", "''") + "'"
                     +" or pallet_no = '" + strSN.Replace("'", "''") + "' ) and wc ='W0'";

            DataTable dt2 = new DataTable();

            try
            {
                dt2 = ClientUtils.ExecuteSQL(strSQL2).Tables[0];
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
            }

            if (dt2.Rows.Count == 0)
            {
                ShowMsg("查无资料", 0);
                txtSN.Text = "";
                txtSN.Focus();
                return;
            }
            else
            {
                //dgvDetail.DataSource = dt2;
              
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        //创建行
                        DataGridViewRow dr = new DataGridViewRow();
                        foreach (DataGridViewColumn c in dgvDetail.Columns)
                        {
                            dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        //累加序号
                        dr.HeaderCell.Value = (i + 1).ToString();
                        dr.Cells[0].Value = dt2.Rows[i]["CUSTOMER_SN"].ToString();
                        dr.Cells[1].Value = dt2.Rows[i]["CARTON_NO"].ToString();
                        dr.Cells[2].Value = dt2.Rows[i]["PART_NO"].ToString();
                        dr.Cells[3].Value = dt2.Rows[i]["LOCATION_NO"].ToString();
                        dr.Cells[4].Value = dt2.Rows[i]["PALLET_NO"].ToString();
                        try
                        {
                        dgvDetail.Invoke((MethodInvoker)delegate ()
                            {
                                dgvDetail.Rows.Add(dr);
                             
                            });
                        }
                        catch (Exception)
                        {
                           
                            return;
                        }
                    }
                  
                }
            
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
    }
}
