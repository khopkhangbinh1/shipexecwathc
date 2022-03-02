using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPIPickListAC
{
    public partial class fNPIPickCheck : Form
    {
        public fNPIPickCheck()
        {
            InitializeComponent();
        }
        public int H = 0;
        public int W = 0;

        NPIPickListACBLL eb = new NPIPickListACBLL();
        public string SID
        {
            get
            {
                return this.txtSID.Text;
            }
            set
            {
                this.txtSID.Text = value;
            }
        }
        private void initGroupBox()
        {
            W = Screen.PrimaryScreen.Bounds.Width;

            W = Convert.ToInt32(W * 0.5);


            this.grpPallet.Width = W;
        }

        private void fNPIPickCheck_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            initGroupBox();

            string strSID = txtSID.Text;

            string strSql2 = @"select '-ALL-' as pick_pallet_no
                                  from dual
                                union
                                select distinct pick_pallet_no 
                                  from (select distinct pick_pallet_no
                                          from nonedipps.t_sn_status a
                                         where a.shipment_id = :insid
                                         order by pick_pallet_no asc)";
            object[][] Param2 = new object[1][];
            Param2[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            DataSet dts2 = ClientUtils.ExecuteSQL(strSql2, Param2);
            if (dts2 != null && dts2.Tables[0].Rows.Count > 0)
            {
                List<string> pickpalletlist = (from d in dts2.Tables[0].AsEnumerable()
                                            select d.Field<string>("pick_pallet_no")).ToList();
                pickpalletlist.Sort();
                cmbPickSID.DataSource = pickpalletlist;
            }
            else
            {
                List<string> pickpalletlist = new List<string>();
                pickpalletlist.Add("-ALL-");
                cmbPickSID.DataSource = pickpalletlist;
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string strSID = txtSID.Text;
            string strPickSAPNO = cmbPickSID.Text;
            if (string.IsNullOrEmpty(strSID))
            {
                ShowMsg("SAP单号不能为空", 0);
                return;
            }
            btnStart.Enabled = false;

            dgvSID.Rows.Clear();
            dgvSID.DataSource = eb.GetSIDNOCartonList(strSID, strPickSAPNO);

            if (dgvSID.Rows.Count < 1)
            {
                ShowMsg("查无资料", 0);
                btnStart.Enabled = true;
                return;
            }
            txtCarton.Enabled = true;
            txtCarton.SelectAll();
            txtCarton.Focus();
            lblQTY.Text = "0/0";
            btnEnd.Enabled = true;

        }
        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt;
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

        private void btnEnd_Click(object sender, EventArgs e)
        {
            btnEnd.Enabled = false;


            dgvSID.DataSource = null;
            dgvCheck.DataSource = null;
            if (dgvCheck.Rows.Count > 1)
            {
                dgvCheck.Rows.Clear();
            }
            lblQTY.Text = "0/0";
            ShowMsg("", 0);
            txtCarton.Enabled = false;
            btnStart.Enabled = true;
        }

        private void dgvSID_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvSID.Rows.Count; i++)
            {
                this.dgvSID.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            }
        }

        private void dgvCheck_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvCheck.Rows.Count; i++)
            {
                this.dgvCheck.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            }
        }

        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            string strSN = txtCarton.Text.Trim();
            TextMsg.Text = "";

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strSN))
            {
                this.ShowMsg("输入的箱号不能为空！", 0);

                txtCarton.SelectAll();
                txtCarton.Focus();
                return;
            }
            string strSID = txtSID.Text;
            string strPickSAPNO = cmbPickSID.Text;
            if (string.IsNullOrEmpty(strSID))
            {
                ShowMsg("SAP单号不能为空", 0);
                return;
            }
            strSN = eb.DelPrefixCartonSN(strSN);
            strSN = eb.ChangeCSNtoCarton(strSN);

            DataTable dt = eb.CheckCartonInSIDNO(strSID, strPickSAPNO, strSN);

            if (dt == null)
            {
                txtCarton.SelectAll();
                ShowMsg("此箱号" + strSN + "不属于此SAP单", 0);
                LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                return;
            }
            if (dgvCheck.Rows.Count > 1)
            {
                for (int i = 0; i < dgvCheck.Rows.Count - 1; i++)
                {
                    if (dgvCheck.Rows[i].Cells["carton_no"].Value.ToString() == strSN)
                    {
                        this.ShowMsg("输入的箱号已经刷入过，重复", 0);
                        dgvCheck.Rows[i].Selected = true;
                        dgvCheck.FirstDisplayedScrollingRowIndex = i;
                        LibHelperAC.MediasHelper.PlaySoundAsyncByRe();
                        txtCarton.SelectAll();
                        txtCarton.Focus();
                        return;
                    }

                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                object[] ds = dr.ItemArray;
                dgvCheck.Rows.Insert(0, ds);
            }

            if (dgvCheck.Rows.Count > 1)
            {
                for (int i = 0; i < dgvCheck.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dgvCheck.Rows[i].Cells["CARTON_NO"].Value.ToString() == dt.Rows[j]["carton_no"]?.ToString())
                        {
                            dgvCheck.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            dgvCheck.Rows[i].Selected = true;
                            dgvCheck.FirstDisplayedScrollingRowIndex = i;
                        }
                    }

                }
                lblQTY.Text = (dgvCheck.Rows.Count - 1).ToString() + "/" + (dgvSID.Rows.Count - 1).ToString();
                ShowMsg("OK", 0);
            }

            if (dgvSID.Rows.Count > 1)
            {
                for (int i = 0; i < dgvSID.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dgvSID.Rows[i].Cells["CARTON_NO"].Value.ToString() == dt.Rows[j]["carton_no"]?.ToString())
                        {
                            dgvSID.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            dgvSID.Rows[i].Selected = true;
                            dgvSID.FirstDisplayedScrollingRowIndex = i;
                        }
                    }

                }
            }


            txtCarton.SelectAll();
            LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
        }

       
    }
}
