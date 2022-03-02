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
    public partial class CreateSN : SimpleForm
    {
        #region Field attribute
        private baseinfo bean { get; set; }
        private corebridge core { get; set; }
        private ExecutionResult exeRes { get; set; }
        #endregion

        #region Constructor
        public CreateSN()
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
        }


        private void btnCommit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDN.Text)){
                var DNList = tbDN.Text.Split(
                                new[] { "\n", ",", "\n\r" },
                                StringSplitOptions.None
                            ).Where(x=>!string.IsNullOrEmpty(x)).Distinct().ToList();

                foreach (var DN in DNList) {
                    try
                    {
                        var list = getDNInfo(DN);
                    }
                    catch { }


                }
            }


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


        private IEnumerable<DNInfoModel> getDNInfo(string DN) {
            string sql =
                @"select b.ac_dn, b.ac_dn_line, b.ict_part, b.packqty, b.qty, c.mo_num,p.custpart
                  from pptest.oms_940_d_dnpart b
                  left join pptest.oms_ppart_dn_info c
                    on b.ac_dn = c.dn_no
                   and b.ac_dn_line = c.dn_line
                   and b.ict_part = c.ict_partno
                 inner join pptest.oms_partmapping p
                    on p.part = b.ict_part
                 where b.ac_dn = :DN
                   and b.qty <> 0";
            return ClientUtils.Query<DNInfoModel>(sql, new { DN = DN });
        }
        #endregion
    }

    public class DNInfoModel {
        //B.AC_DN, B.AC_DN_LINE, B.ICT_PART, B.PACKQTY, B.QTY, C.MO_NUM
        public string AC_DN { get; set; }
        public string AC_DN_LINE { get; set; }
        public string ICT_PART { get; set; }
        public decimal PACKQTY { get; set; }
        public decimal QTY { get; set; }
        public string MO_NUM { get; set; }
        public string CUSTPART { get; set; }
    }
}
