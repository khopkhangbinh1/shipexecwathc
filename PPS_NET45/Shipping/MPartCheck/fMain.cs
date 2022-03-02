using DBTools;
using DBTools.Forms;
using MPartCheck.Bean;
using MPartCheck.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MPartCheck
{
    public partial class fMain : SimpleForm
    {
        #region Field attribute
        baseinfo bean;
        corebridge core;
        ExecutionResult exeRes;
        #endregion

        #region Constructor
        public fMain()
        {
            bean = new baseinfo();
            core = new corebridge();
            exeRes = new ExecutionResult();
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void fMain_Load(object sender, EventArgs e)
        {
            fMain fmain = new fMain();
            prgTitle.Text = string.Format("{0}:{1}", fmain.ProductName, fmain.ProductVersion);
            //  SuccessMSG("ok");
        }

        private void tbTrolley_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || string.IsNullOrEmpty(tbTrolley.Text.Trim()))
                return;
            bean.Trolley_no = tbTrolley.Text.Trim();
            exeRes = core.CheckTrolley(bean);
            if (exeRes.Status)
            {
                SuccessMSG(exeRes.Message);
                tbTrolleyLine.Focus();
                tbTrolley.Enabled = false;
            }
            else
            {
                ErrorMSG(exeRes.Message);
                tbTrolley.SelectAll();
            }
        }

        private void tbTrolleyLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || string.IsNullOrEmpty(tbTrolleyLine.Text.Trim()))
                return;
            bean.Trolley_Line_No = tbTrolleyLine.Text.Trim();
            exeRes = core.CheckTrolleyLine(bean);
            if (exeRes.Status)
            {
                this.GetTrolleyLineInfo(bean);
                SuccessMSG(exeRes.Message);
                tbTrolleyLine.Enabled = false;
                tbSN.Focus();
            }
            else
            {
                ErrorMSG(exeRes.Message);
                tbTrolleyLine.SelectAll();
            }
        }

        private void tbSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || string.IsNullOrEmpty(tbSN.Text.Trim()))
                return;
            bean.Customer_SN = tbSN.Text.Trim();
            exeRes = core.GetSnInfo(bean);
            if (exeRes.Status)
            {
                exeRes = CheckDuplicate(bean, GetDgvToTable(dgvTrolleyInfo));
                if (!exeRes.Status)
                {
                    ErrorMSG(exeRes.Message);
                    tbSN.SelectAll();
                }
                else
                {
                    DataGridViewRow dgvNewRow = new DataGridViewRow();
                    dgvNewRow.CreateCells(dgvTrolleyInfo);
                    dgvNewRow.Cells[0].Value = bean.Trolley_no;
                    dgvNewRow.Cells[1].Value = bean.Trolley_Line_No;
                    dgvNewRow.Cells[2].Value = bean.Check_Index;
                    dgvNewRow.Cells[3].Value = bean.Customer_SN;
                    dgvNewRow.Cells[4].Value = bean.KeyPart;
                    dgvNewRow.Cells[5].Value = bean.OriginPallet_no;
                    dgvTrolleyInfo.Rows.Add(dgvNewRow);
                    bean.Check_Index++;
                    SuccessMSG("pls scan next Sn or Carton.");
                    tbSN.SelectAll();
                }
            }
            else
            {
                ErrorMSG(exeRes.Message);
                tbSN.SelectAll();
            }
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            //循环dgv更新DB 事务处理 需要获取DB Conn
            if (dgvTrolleyInfo.Rows.Count > 0)
            {
                exeRes = core.ExecuteTrolleyCheckIn(bean, GetDgvToTable(dgvTrolleyInfo));
                if (exeRes.Status)
                {
                    SuccessMSG(exeRes.Message);
                    tbTrolleyLine.Text = tbSN.Text = "";
                    tbTrolleyLine.Enabled = true;
                    tbTrolleyLine.Focus();
                    dgvTrolleyInfo.Rows.Clear();
                    bean.Check_Index = new baseinfo().Check_Index;
                }
                else
                {
                    ErrorMSG(exeRes.Message);
                    tbSN.SelectAll();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbTrolley.Text = tbTrolleyLine.Text = tbSN.Text = "";
            tbTrolley.Enabled = tbTrolleyLine.Enabled = true;
            tbTrolley.Focus();
            dgvTrolleyInfo.Rows.Clear();
            bean.Check_Index = new baseinfo().Check_Index;
        }
        #endregion

        #region Private method
        /// <summary>
        /// DataGridView 转换 DataTable
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <returns>DataTable</returns>
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private ExecutionResult CheckDuplicate(baseinfo bean, DataTable dataSource)
        {
            ExecutionResult res = new ExecutionResult();
            res.Status = true;
            //检查SN是否重复录入
            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataSource.Rows)
                {
                    if (bean.Customer_SN.Equals(dataRow["Customer_sn"].ToString()))
                    {
                        res.Message = string.Format("SN:{0} has been scan,pls change other sn.", bean.Customer_SN);
                        res.Status = false; break;
                    }
                }
            }
            //检查SN是否已录入进栈板中
            if (res.Status)
                res = core.CheckSN(bean);
            return res;
        }
        private void GetTrolleyLineInfo(baseinfo bean)
        {
            //090-A01 面：ABCD四面固定长度1，层：01-99 最大长度为2
            int iSplit = bean.Trolley_Line_No.IndexOf("-") + 1;
            bean.Sides_No = bean.Trolley_Line_No.Substring(iSplit, 1);
            bean.Level_No = bean.Trolley_Line_No.Substring(iSplit + 1, 2).TrimStart('0');
        }
        #endregion
    }
}
