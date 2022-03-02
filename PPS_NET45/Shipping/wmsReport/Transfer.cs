using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using wmsReport.Core;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using ClientUtilsDll;
/*
add by dunyang_wang 20180917
*/
namespace wmsReport
{
    public partial class Transfer : Form
    {
        private PrintLabel printLabel = new PrintLabel();
        private Controller controller;
        public int H = 0;
        public int W = 0;
        public Transfer()
        {
            InitializeComponent();
            //固定位置
            controller = new Controller();
            this.Location = new Point(System.Windows.Forms.SystemInformation.WorkingArea.Width-this.Size.Width,0);
            ClientUtils.runBackground((Form)this);
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            this.chkQHold.Checked = true;//默认检查QAhold
            this.chkPrint.Checked = true;//默认打标签
            initGroupBox();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        private void initGroupBox()
        {
            W = Screen.PrimaryScreen.Bounds.Width;
            W = Convert.ToInt32(W * 0.5);

            H = Screen.PrimaryScreen.Bounds.Height;
            H = Convert.ToInt32(H * 0.4);
            this.groupBox1.Width = W;
            this.groupBox5.Width = Convert.ToInt32(W * 0.8);
            this.panel4.Width = W;
            this.panel1.Height = H;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.cmbLocationFrom.Enabled = false;
            this.cmbLocationTo.Enabled = false;
            this.txtCarton.Enabled = true;
            this.txtCarton.Focus();
            this.txtCarton.SelectAll();
        }
       
        private void resetAllStatus()
        {
            this.cmbLocationFrom.Enabled = true;
            this.cmbLocationFrom.Text = string.Empty;
            this.cmbLocationFrom.DataSource = null;
            this.dgvLocationFrom.DataSource = null;
            this.cmbLocationTo.Enabled = true;
            this.cmbLocationTo.Text = string.Empty;
            this.cmbLocationTo.DataSource = null;
            this.dgvLocationTo.DataSource = null;
            this.txtCarton.Enabled = false;
            this.txtCarton.Clear();
            this.dgvCarton.DataSource = null;
            this.Message_LB.Text = "Message";           
        }

        private void Show_Message(string msg, int type)
        {
            Message_LB.Text = msg.TP();
            switch (type)
            {
                case 0: //error
                    Message_LB.ForeColor = Color.Red;
                    Message_LB.BackColor = Color.Yellow;
                    break;
                case 1:
                    Message_LB.ForeColor = Color.Blue;
                    Message_LB.BackColor = Color.White;
                    break;
                default:
                    Message_LB.ForeColor = Color.Black;
                    Message_LB.BackColor = Color.White;
                    break;
            }
        }
        private void cmbLocationTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgLocationNo = this.cmbLocationTo.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(orgLocationNo))
            {
                Show_Message("储位的信息，不可为空！", 0);
                return;
            }
            string locationId = "";
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.locationNoTransformLocationIdByLocationNo(orgLocationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                locationId = dt.Rows[0]["location_id"].ToString();
                exeRes = controller.getLocationInformationBylocationId(locationId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvLocationTo.DataSource = dt;
                    Show_Message(exeRes.Message, 1);
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
            else
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
        }
        private void resetAll()
        {
            this.cmbLocationFrom.Enabled = true;
            this.cmbLocationFrom.Text = "";
            this.cmbLocationTo.Enabled = true;
            this.cmbLocationTo.Text = "";
            this.dgvLocationTo.DataSource = null;
            this.dgvLocationFrom.DataSource = null;
            this.dgvSamePartLocation.DataSource = null;
            this.txtOrgCarton.Text = "";
            this.txtCarton.Clear();
            this.txtCarton.Enabled = false;
            this.dgvCarton.DataSource = null;
        }
        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string orgLocationNo = this.cmbLocationFrom.Text.Trim().ToUpper();
                string tarLocationNo = this.cmbLocationTo.Text.Trim().ToUpper();
                string inputData = this.txtCarton.Text.Trim().ToUpper();
                if (inputData.StartsWith("3S"))
                {
                    inputData =  inputData.Substring(2);
                }
                if (inputData.StartsWith("S"))
                {
                    inputData = inputData.Substring(1);
                }
                if (inputData.Length == 20 && inputData.Substring(0, 2).Equals("00"))
                {
                    inputData = inputData.Substring(2);
                }
                if (string.IsNullOrEmpty(inputData) || string.IsNullOrEmpty(orgLocationNo) || string.IsNullOrEmpty(tarLocationNo))
                {
                    Show_Message("输入信息或原储位和目标储位没有输入信息，请检查！", 0);
                    return;
                }
                this.dgvCarton.DataSource = null;
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                #region  判断是否能够转移储位     
                exeRes = controller.checkIsTransfer(inputData, orgLocationNo, tarLocationNo);
                if (!exeRes.Status)
                {
                    this.txtCarton.Focus();
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.txtCarton.SelectAll();
                    Show_Message(exeRes.Message,0);
                    return;
                }
                #endregion
                #region //增加判断是否有QAhold机器，并且是否可以转移
                if (chkQHold.Checked)
                {
                    string errorMes = "";
                    if (!Reverse.ReverseBll.CheckHold(inputData, "SERIALNUMBER", out errorMes))
                    {
                        Show_Message("刷入:" + inputData + "中有被QAHOLD机器，不可转移，请检查！", 0);
                        LibHelper.MediasHelper.PlaySoundAsyncByHold();
                        this.txtCarton.Focus();
                        this.txtCarton.SelectAll();
                        return;
                    }                        
                }
                #endregion
                //做储位转移动作 
                exeRes =controller.changeLocation(inputData, orgLocationNo, tarLocationNo);
                if (!exeRes.Status)
                {
                    Show_Message(exeRes.Message,0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    return;
                }

                //将查到的信息，show到dgv中去
                exeRes = controller.getInformationByInputData(inputData);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvCarton.DataSource = dt;
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    Show_Message("储位已转移成功，请继续输入！", 1);
                    showAfterChangeLocationInformation(orgLocationNo, tarLocationNo);//将转移后location信息show出
                    return;
                }
                else
                {
                    this.txtCarton.Focus();
                    this.txtCarton.SelectAll();
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    Show_Message(exeRes.Message,0);
                    return;
                }                                   
            }      
        }
        private void txtOrgCarton_KeyDown(object sender, KeyEventArgs e)
        {
            string strSN = txtOrgCarton.Text.Trim();

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(strSN))
            {
                return;
            }

            WMSBLL plb = new WMSBLL();
            strSN = plb.DelPrefixCartonSN(strSN);
            strSN = plb.ChangeCSNtoCarton(strSN);
            DataTable dt2 = plb.GetSnInfo( strSN);
            string strLocationno = string.Empty;
            string strLocationid = string.Empty;
            try
            {
                strLocationno = dt2.Rows[0]["location_no"]?.ToString();
                strLocationid = dt2.Rows[0]["location_id"]?.ToString();
            }
            catch 
            {
                Show_Message("箱号查无资料", 0);
                return;
            }
            cmbLocationFrom.Text = strLocationno;
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            dt = (DataTable)exeRes.Anything;
            exeRes = controller.getLocationInformationBylocationId(strLocationid);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                this.dgvLocationFrom.DataSource = dt;
                Show_Message(exeRes.Message, 1);
            }
            else
            {
                Show_Message(exeRes.Message, 0);
                return;
            }

            dgvSamePartLocation.DataSource = null;
            dgvSamePartLocation.Rows.Clear();
            WMSBLL wb = new WMSBLL();
            dgvSamePartLocation.DataSource = wb.GetSamePartLocation(strLocationno);

            

        }
        private void showAfterChangeLocationInformation(string orgLocationNo,string targetLocationNo)
        {
            string locationId = "";
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.locationNoTransformLocationIdByLocationNo(targetLocationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                locationId = dt.Rows[0]["location_id"].ToString();
                exeRes = controller.getLocationInformationBylocationId(locationId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvLocationTo.DataSource = dt;
                }
            }
            exeRes = controller.locationNoTransformLocationIdByLocationNo(orgLocationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                locationId = dt.Rows[0]["location_id"].ToString();
                exeRes = controller.getLocationInformationBylocationId(locationId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvLocationFrom.DataSource = dt;
                }
            }
        }
   
      
        private void resetInput()//将输入信息清除
        {
            this.txtCarton.Clear();
            this.txtCarton.Focus();
            this.txtCarton.SelectAll();
            this.dgvCarton.DataSource = null;
            Show_Message("重置成功！",1);         
        }

        private string getAllCount(string inputData)
        {
            string countAll = "";
            string sql = @"select count(*)  as qty  from  ppsuser.t_sn_status tss where   tss.pallet_no = :inputData";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", inputData } };
            DataSet ds = ClientUtils.ExecuteSQL(sql, sqlparams);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                countAll = ds.Tables[0].Rows[0]["qty"].ToString();
            }
            else
            {
                countAll = null;
            }
            return countAll;
        }
        /// <summary>
        /// 将A中数据添加到B中
        /// </summary>
        /// <param name="getDataDt">A dataTable</param>
        /// <param name="dgvDt">B dataTable</param>
        /// <returns></returns>
        private DataTable addRowData(DataTable getDataDt , DataTable dgvDt)
        {
            DataTable dt = new DataTable();
            if (getDataDt != null && getDataDt.Rows.Count>0)//将取到值添加到dgv中
            {
                DataRow drcalc;
                foreach (DataRow dr in getDataDt.Rows)
                {
                    drcalc = dgvDt.NewRow();
                    drcalc.ItemArray = dr.ItemArray;
                    dgvDt.Rows.Add(drcalc);
                }
            }           
            dt = dgvDt;
            return dt;
        }
      

        private DataTable getAllCartonNoBy_SN_cartonNo_PalletNo(string inputData)
        {            
            string sql = @"select distinct tss.carton_no
            from ppsuser.t_sn_status tss
            where (tss.serial_number = :inputData or tss.carton_no =:inputData or tss.pallet_no =:inputData)";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", inputData } };           
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0]; 
        }
        private DataTable queryDataByCartonNo(string cartonNo)
        {
            string sql = @"select tss.serial_number as  流水号,
                            tss.customer_sn  as  客户料号,
                            tss.pallet_no  as  栈板号,
                            tss.carton_no  as 箱号,
                            tss.location_name as 储位名,
                            tss.pallet_no as 料号,
                            '' as 目标储位
                             from  ppsuser.t_sn_status tss 
                            where   tss.carton_no = :inputData";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", cartonNo } };
            DataSet ds = ClientUtils.ExecuteSQL(sql, sqlparams);
            return ds.Tables[0];
        }
        //private bool checkQAHoldByCarton(string carton)
        //{
        //    bool flag = false;
        //    string sql = @"
        //                   select gss.hold_flag,tss.*
        //                   from ppsuser.g_sn_status gss, ppsuser.t_sn_status tss
        //                   where gss.serial_number = tss.serial_number
        //                     and gss.hold_flag = 'Y' 
        //                     and tss.carton_no = '{0}'
        //                   ";
        //    sql = string.Format(sql,carton);
        //   DataSet  ds =    ClientUtils.ExecuteSQL(sql);
        //    if (ds.Tables[0].Rows.Count>0)
        //    {
        //        flag = true;
        //    }
        //    return flag;
        //}
        private void cmbLocationFrom_DropDown(object sender, EventArgs e)
        {
            //获取所有储位信息
            this.cmbLocationFrom.DataSource = null;
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.getAllLocationInfo(false);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                if (dt.Rows.Count > 0 && dt != null)
                {
                    this.cmbLocationFrom.DataSource = dt;
                    this.cmbLocationFrom.DisplayMember = "LOCATION_No";
                    this.cmbLocationFrom.ValueMember = "LOCATION_ID";
                }
            }
            else
            {
                Show_Message(exeRes.Message,0);
                return;
            }          
        }

        private void cmbLocationTo_DropDown(object sender, EventArgs e)
        {
            //获取所有储位信息
            this.cmbLocationTo.DataSource = null;//cmbLocationTo
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.getAllLocationInfo(true);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                if (dt.Rows.Count > 0 && dt != null)
                {
                    this.cmbLocationTo.DataSource = dt;
                    this.cmbLocationTo.DisplayMember = "LOCATION_No";
                    this.cmbLocationTo.ValueMember = "LOCATION_ID";
                }
            }
            else
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
        }

   

       

        private void cmbLocationFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {           
                string originalLocationNo = this.cmbLocationFrom.Text.ToUpper().Trim();
                cmbLocationFrom.Text = originalLocationNo;
                if (string.IsNullOrEmpty(originalLocationNo))
                {
                    Show_Message("储位的信息，不可为空！", 0);
                    return;
                }
                string locationId = "";
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.locationNoTransformLocationIdByLocationNo(originalLocationNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    locationId = dt.Rows[0]["location_id"].ToString();
                    exeRes = controller.getLocationInformationBylocationId(locationId);
                    if (exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        this.dgvLocationFrom.DataSource = dt;
                        Show_Message(exeRes.Message, 1);
                    }
                    else
                    {
                        Show_Message(exeRes.Message, 0);
                        return;
                    }
                    dgvSamePartLocation.DataSource = null;
                    dgvSamePartLocation.Rows.Clear();
                    WMSBLL wb = new WMSBLL();
                    dgvSamePartLocation.DataSource = wb.GetSamePartLocation(originalLocationNo);

                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
        }

        private void cmbLocationTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string tarLocationNo = this.cmbLocationTo.Text.ToUpper().Trim();
                cmbLocationTo.Text = tarLocationNo;
                if (string.IsNullOrEmpty(tarLocationNo))
                {
                    Show_Message("储位的信息，不可为空！", 0);
                    return;
                }
                string locationId = "";
                ExecuteResult exeRes = new ExecuteResult();
                DataTable dt = new DataTable();
                exeRes = controller.locationNoTransformLocationIdByLocationNo(tarLocationNo);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    locationId = dt.Rows[0]["location_id"].ToString();
                    exeRes = controller.getLocationInformationBylocationId(locationId);
                    if (exeRes.Status)
                    {
                        dt = (DataTable)exeRes.Anything;
                        this.dgvLocationTo.DataSource = dt;
                        Show_Message(exeRes.Message, 1);
                    }
                    else
                    {
                        Show_Message(exeRes.Message, 0);
                        return;
                    }
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    return;
                }
            }
        }


        private void cmbLocationFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orglLocationNo = this.cmbLocationFrom.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(orglLocationNo))
            {
                Show_Message("储位的信息，不可为空！", 0);
                return;
            }
            string locationId = "";
            ExecuteResult exeRes = new ExecuteResult();
            DataTable dt = new DataTable();
            exeRes = controller.locationNoTransformLocationIdByLocationNo(orglLocationNo);
            if (exeRes.Status)
            {
                dt = (DataTable)exeRes.Anything;
                locationId = dt.Rows[0]["LOCATION_ID"].ToString();
                exeRes = controller.getLocationInformationBylocationId(locationId);
                if (exeRes.Status)
                {
                    dt = (DataTable)exeRes.Anything;
                    this.dgvLocationFrom.DataSource = dt;
                    Show_Message(exeRes.Message, 1);
                }
                else
                {
                    Show_Message(exeRes.Message, 0);
                    return;
                }

                dgvSamePartLocation.DataSource = null;
                dgvSamePartLocation.Rows.Clear();
                WMSBLL wb = new WMSBLL();
                dgvSamePartLocation.DataSource = wb.GetSamePartLocation( orglLocationNo);

            }
            else
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            string orgLocation = this.cmbLocationFrom.Text.ToUpper().Trim();
            string tarLocation = this.cmbLocationTo.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(orgLocation)|| string.IsNullOrEmpty(tarLocation))
            {
                Show_Message("原储位或目标储位没有输入信息！",0);
                return;
            }
            ExecuteResult exeRes = new ExecuteResult();
            List<string> labelContentList = new List<string>();
            exeRes = controller.getTransferLableContentStr(orgLocation);
            if (exeRes.Status)
            {
                labelContentList.Add((string)exeRes.Anything);
            }
            exeRes = printLabel.printLableForModifyVersion("Transfer_PalletPartLabel", labelContentList, 1);
            if (!exeRes.Status)
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
            exeRes = controller.getTransferLableContentStr(tarLocation);
            /////////////
            List<string> labelContentList2 = new List<string>();
            if (exeRes.Status)
            {
                labelContentList2.Add((string)exeRes.Anything);
            }
            exeRes =  printLabel.printLableForModifyVersion("Transfer_PalletPartLabel", labelContentList2,1);
            if (!exeRes.Status)
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
            else
            {
                Show_Message("储位转移结束，Label已经打印！", 0);
                return;
            }
        }

        private void chkQHold_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string orgLocation = this.cmbLocationFrom.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(orgLocation) )
            {
                Show_Message("原储位或目标储位没有输入信息！", 0);
                return;
            }
            ExecuteResult exeRes = new ExecuteResult();
            List<string> labelContentList = new List<string>();
            exeRes = controller.getTransferLableContentStr(orgLocation);
            if (exeRes.Status)
            {
                labelContentList.Add((string)exeRes.Anything);
            }
            exeRes = printLabel.printLableForModifyVersion("Transfer_PalletPartLabel", labelContentList, 1);
           
            if (!exeRes.Status)
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
            else
            {
                Show_Message("打印OK", 0);
                return;
            }
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            string tarLocation = this.cmbLocationTo.Text.ToUpper().Trim();
            if ( string.IsNullOrEmpty(tarLocation))
            {
                Show_Message("原储位或目标储位没有输入信息！", 0);
                return;
            }
            ExecuteResult exeRes = new ExecuteResult();
            exeRes = controller.getTransferLableContentStr(tarLocation);
            /////////////
            List<string> labelContentList2 = new List<string>();
            if (exeRes.Status)
            {
                labelContentList2.Add((string)exeRes.Anything);
            }
            exeRes = printLabel.printLableForModifyVersion("Transfer_PalletPartLabel", labelContentList2, 1);
            if (!exeRes.Status)
            {
                Show_Message(exeRes.Message, 0);
                return;
            }
            else
            {
                Show_Message("打印OK", 0);
                return;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvSamePartLocation.Rows.Count > 0)
                {
                    ExportExcel(dgvSamePartLocation);
                }
                else
                {
                    this.Show_Message("确认导出Excel有数据！", 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                }
            }
            catch (Exception ex)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
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

            sfd.FileName =  "Location_" + currdate;
            sfd.DefaultExt = "xlsx";//设置默认格式（可以不设）
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
            }
            else
            {
                this.Show_Message("导出Excel失败！", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
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
                this.Show_Message("导出Excel成功！", 5);
                LibHelper.MediasHelper.PlaySoundAsyncByOk();
            }
        }

       
    }
}
