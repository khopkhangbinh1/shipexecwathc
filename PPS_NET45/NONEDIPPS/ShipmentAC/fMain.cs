using ReverseAC;
using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

namespace ShipmentAC
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();

        }
        //DataTable dt = new DataTable();
        //DataTable dtResult = new DataTable();
        ShipmentBll sb = new ShipmentBll();

        string strLocalMACADDRESS = string.Empty;
        #region Even/fMain_Load
        //窗体载入时初始化
        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.dtpStartTime.ValueChanged -= new System.EventHandler(this.dtpStartTime_ValueChanged);
            this.dtpEndTime.ValueChanged -= new System.EventHandler(this.dtpEndTime_ValueChanged);

            DateTime dateTimeNow = DateTime.Now;
            //dtpStartTime.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month-2, 1);
            dtpStartTime.Value = DateTime.Now.AddMonths(-1);
            //HYQ:以下两个第一次被赋值的时候，就会调用dtpStartTime_ValueChanged 和dtpEndTime_ValueChanged

            //dtpStartTime.Value = dateTimeNow;
            dtpEndTime.Value = DateTime.Now.AddDays(1);
            

            //关掉触发
            this.cboCarNo.SelectedIndexChanged -= new System.EventHandler(this.cboCarNo_SelectedIndexChanged);
            
            getTruckNo(dtpStartTime.Text,dtpEndTime.Text);


            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);

            //开启触发
            this.cboCarNo.SelectedIndexChanged += new System.EventHandler(this.cboCarNo_SelectedIndexChanged);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            strLocalMACADDRESS = LibHelperAC.LocalHelper.getMacAddr_Local();
        }
        #endregion

        #region Even/txtPalletNo_KeyDown
        /// <summary>
        /// 栈板号的扫描输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            string StrPalletNo = txtPalletNo.Text.Trim();
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (string.IsNullOrEmpty(StrPalletNo))
            {
                txtMessage.Text = "";
                txtMessage.BackColor = Color.Blue;
            }

            //换成PICK栈板号 HYQ 20200511 cancel
            //if ((StrPalletNo.Length > 2) && (StrPalletNo.Substring(0, 1).ToUpper() == "P"))
            //{
            //    StrPalletNo = StrPalletNo.Substring(2);
            //    this.txtPalletNo.Text = StrPalletNo;
            //}

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtMessage.BackColor = Color.Blue;
                txtMessage.Text = "";
                
                try
                {
                    ShowMsg("", -1);
                    ShipmentBll shipmentBll = new ShipmentBll();


                    DataTable dtShipmentInfo = (DataTable)dgvShipmentInfo.DataSource;

                    if (dtShipmentInfo == null)
                    {
                        ShowMsg(cboCarNo.Text+"无装车信息!", 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.Focus();
                        return;
                    }
                    // HYQ：20181120
                    //添加QHold 检查
                    //ReverseBll.CheckHold(string Sno, string Type, out string errorMessage)
                    //Type有: 'SHIPMENT', 'PICKPALLETNO', 'PACKPALLETNO', 'SERIALNUMBER'
                    #region
                    string errorMessage = "";
                    bool CheckHoldOK = ReverseBll.CheckHold(StrPalletNo, "PACKPALLETNO", out errorMessage);
                    if (!CheckHoldOK)
                    {
                        ShowMsg(errorMessage, 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.Focus();
                        return;
                    }
                    #endregion

                    // HYQ：20190329 新增检查HOLD
                    #region
                    bool CheckHoldOK2 = shipmentBll.CheckShipmentIDHold(StrPalletNo, out errorMessage);
                    if (!CheckHoldOK2)
                    {
                        ShowMsg(errorMessage, 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.Focus();
                        return;
                    }
                    #endregion



                    //扫描栈板号
                    //HYQ:检查栈板下所有序号的CSN的WC
                    //real_pallet_no , pallet_no
                    if (dgvShipmentInfoAlready.RowCount>0)
                    {
                        for (int i=0; i< dgvShipmentInfoAlready.RowCount;i++)
                        { if (dgvShipmentInfoAlready.Rows[i].Cells["real_pallet_no"].Value.ToString().Contains(StrPalletNo) )
                            {
                                ShowMsg("此序号已经刷入过", 0);
                                txtPalletNo.Text = "";
                                txtPalletNo.SelectAll();
                                txtPalletNo.Focus();
                                return ;
                            }
                            if (dgvShipmentInfoAlready.Rows[i].Cells["pallet_no"].Value.ToString().Contains(StrPalletNo))
                            {
                                ShowMsg("此序号已经刷入过", 0);
                                txtPalletNo.Text = "";
                                txtPalletNo.SelectAll();
                                txtPalletNo.Focus();
                                return;
                            }
                        }
                    }

                    if (!checkPalletWC(StrPalletNo))
                    {
                        ShowMsg("栈板内CSN 序号站别存在非[W4]，异常。", 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.SelectAll();
                        txtPalletNo.Focus();
                        return;
                    }


                    //HYQ:之前人写的检查逻辑
                    string strResultMessage = string.Empty;
                    int intResultRowNum = 0;

                    //
                    //HYQ： 都在这里做处理， 包括，shipment 结束 打印水晶报表 handover manifest
                    bool scanStatus = shipmentBll.ScanPalletNo(dtShipmentInfo, txtPalletNo.Text.Trim(), out strResultMessage, out intResultRowNum, strLocalMACADDRESS);



                    if (scanStatus)
                    {
                        ShowMsg("装车OK", -1);
                        FillDataGridViewByShipment();
                        //每扫描过栈板后，获取站点信息//HYQ : 有什么用？？
                        //if (intResultRowNum >= 1)
                        //{
                        //    int intPalletNum;
                        //    if (!int.TryParse(txtShallNum.Text, out intPalletNum))
                        //    {
                        //        intPalletNum = 0;
                        //    }
                        //    if (dgvShipmentInfo.Rows.Count > 0)
                        //    {
                        //        string strShipmentId = this.dgvShipmentInfo.Rows[intResultRowNum - 1].Cells["ShipmentID"].Value.ToString();
                        //        lbStatInfo.Text = ShipmentBll.GetWeightNumByShipmnetStat(intPalletNum, strShipmentId);
                        //    }
                        //    txtPalletNo.Text = string.Empty;
                        //}
                    }
                    else
                    {
                        ShowMsg(strResultMessage, 0);
                    }
                    FillDataGridViewByShipment();
                    GetShipNumInfo();
                    txtPalletNo.Text="";
                    txtPalletNo.SelectAll();
                    txtPalletNo.Focus();
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message, 0);
                }
            }
            else { }
 
        }
        #endregion

        #region Even/cboCarNo_SelectedIndexChanged
        private void cboCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGridViewByShipment();
            ShowMsg("", -1);
   
        }
        #endregion

        //HYQ: 检查栈板下CSN的站别
        private bool checkPalletWC(string palletno)
        {
            string strSQL = string.Format(@"select wc 
                                            from NONEDIPPS.t_sn_status 
                                           where pack_pallet_no in 
                                            (select distinct  pallet_no  
                                                from NONEDIPPS.t_shipment_pallet 
                                               where pallet_no ='{0}' or real_pallet_no='{0}')" ,palletno);

            DataTable sDataTab = ClientUtils.ExecuteSQL(strSQL).Tables[0];

            if (sDataTab.Rows.Count > 0)
            {
                string palletStationCheck = string.Empty;
                for (int i = 0; i < sDataTab.Rows.Count; i++)
                {
                    string csnStation = sDataTab.Rows[i]["wc"].ToString();
                    csnStation = csnStation.Trim();
                    if (!csnStation.Equals("W4"))
                    {
                        palletStationCheck = "存在异常站别";
                    }

                }
                if (palletStationCheck.Equals("存在异常站别"))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        #region Even/dtpStartTime_KeyPress
        private void dtpStartTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    GetComCarNo();
                    FillDataGridViewByShipment();
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message.ToString(), 0);
                }
            }
        }
        #endregion

        #region Even/dtpEndTime_KeyPress
        private void dtpEndTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    GetComCarNo();
                    FillDataGridViewByShipment();
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message.ToString(), 0);
                }
            }
        }
        #endregion

        #region Even/cboCarNo_KeyPress
        /// <summary>
        /// 手动输入车牌号-回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCarNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
                return;
            if (!string.IsNullOrEmpty(cboCarNo.Text))
            {
                FillDataGridViewByShipment();
            }
            else
            {
                ShowMsg("请扫入车牌号!", 0);
                return;
            }
        }
        #endregion


        #region Even/dtpStartTime_ValueChanged
        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetComCarNo();
                FillDataGridViewByShipment();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message.ToString(), 0);
            }
        }
        #endregion
        #region Even/dtpEndTime_ValueChanged
        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetComCarNo();
                FillDataGridViewByShipment();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message.ToString(), 0);
            }
        }
        #endregion
        #region Function/ShowMsg 消息显示
        /// <summary>
        /// 错误/警告/确认 消息显示
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public DialogResult ShowMsg(string strText, int intType)
        {
            txtMessage.Text = strText;
            switch (intType)
            {
                case 0: //Error     
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                    txtMessage.ForeColor = Color.Red;
                    txtMessage.BackColor = Color.Silver;
                    return DialogResult.None;
                case 1: //Warning                        
                    txtMessage.ForeColor = Color.Blue;
                    txtMessage.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    txtMessage.ForeColor = Color.White;
                    txtMessage.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }
        #endregion

        #region Function/填充dgvShipmentInfo内容
        /// <summary>
        /// 填充shipment Info Datagridview内容
        /// </summary>
        private void FillDataGridViewByShipment()
        {
            //清空dgvShipmentInfo
            ShipmentBll shipmentBll = new ShipmentBll();
            DataTable dtShipMentDataTalbe = (DataTable)dgvShipmentInfo.DataSource;
            if (dtShipMentDataTalbe != null)
            {
                dtShipMentDataTalbe.Rows.Clear();
                dgvShipmentInfo.DataSource = dtShipMentDataTalbe;
            }
            DataTable dtShipMentAlreadyDataTalbe = (DataTable)dgvShipmentInfoAlready.DataSource;
            if (dtShipMentAlreadyDataTalbe != null)
            {
                dtShipMentAlreadyDataTalbe.Rows.Clear();
                dgvShipmentInfoAlready.DataSource = dtShipMentAlreadyDataTalbe;
            }
            //重新获取数据 
            DataTable dataTable = shipmentBll.GetShipmentInfoDataTable(cboCarNo.Text.Trim());
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                dgvShipmentInfo.DataSource = dataTable;
            }
            DataTable dataTableAlready = shipmentBll.GetShipmentAlreadyInfoDataTable(cboCarNo.Text.Trim());
            if (dataTableAlready != null && dataTableAlready.Rows.Count > 0)
            {
                dgvShipmentInfoAlready.DataSource = dataTableAlready;
            }
            showACandEDISameCarNotice(cboCarNo.Text.Trim());
            GetShipNumInfo();
            txtPalletNo.SelectAll();
            txtPalletNo.Focus();
        }
        #endregion
        // <summary>
        /// 如果当天一个车即运EDI也运AC的栈板。
        /// </summary>
        private void showACandEDISameCarNotice(string strCarNo)
        {
            ShipmentBll sb = new ShipmentBll();
            bool IsACandEDISameCar = sb.GetACandEDISameCarNo(strCarNo);
            if (IsACandEDISameCar)
            {
                this.labInfo.Text = string.Format("此车:{0} 同时包含EDI和AC的货，请注意装车!", strCarNo);
                this.labInfo.Visible = true;
            }
            else
            {
                this.labInfo.Visible = false;
            }
        }
        /// <summary>
        /// 显示栈板的扫描统计信息
        /// </summary>
        private void GetShipNumInfo()
        {

            //应扫描的栈板的数目
            txtShallNum.Text = (dgvShipmentInfo.RowCount + dgvShipmentInfoAlready.RowCount).ToString();
            //已扫描的栈板的数目
            txtAlreadyNum.Text = dgvShipmentInfoAlready.RowCount.ToString();
            //未扫描的栈板的数目 
            txtRemainNum.Text = dgvShipmentInfo.RowCount.ToString();
            //}

        }

        private void GetComCarNo()
        {
            //this.dtpStartTime.ValueChanged -= new System.EventHandler(this.dtpStartTime_ValueChanged);
            //this.dtpEndTime.ValueChanged -= new System.EventHandler(this.dtpEndTime_ValueChanged);

            //DateTime dateTimeNow = DateTime.Now;
            //dtpStartTime.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            ////HYQ:以下两个第一次被赋值的时候，就会调用dtpStartTime_ValueChanged 和dtpEndTime_ValueChanged

            ////dtpStartTime.Value = dateTimeNow;
            //dtpEndTime.Value = dateTimeNow;



            //关掉触发
            this.cboCarNo.SelectedIndexChanged -= new System.EventHandler(this.cboCarNo_SelectedIndexChanged);

            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);


            //this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            //this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);

            //开启触发
            this.cboCarNo.SelectedIndexChanged += new System.EventHandler(this.cboCarNo_SelectedIndexChanged);

        }


        //HYQ:重写获得卡车信息  //会增加第3个参数只是显示未完成的卡车或是shipment
        public void getTruckNo(string starttime, string endtime)
        {
            string sql = string.Empty;
            if (chkAllRecord.Checked == false)
            {
                 sql = @"select distinct case
                                          when b.car_no = '' then
                                           'notrack-' || a.shipment_id
                                          when b.car_no is null then
                                           'notruck-' || a.shipment_id
                                          else
                                           b.car_no || '-' || a.shipment_id
                                        end CAR_NO,
                                        a.shipment_id,
                                        to_char(a.cdt,'YYYYMMDD') strcdt 
                          FROM NONEDIPPS.t_shipment_pallet a
                          join NONEDIOMS.oms_load_car b
                            on a.shipment_id = b.shipment_id
                           and isload = 0
                           and (b.active = 0  or  b.active is null)
                         WHERE (to_date(a.cdt) >= to_date(:startDate, 'YYYY-MM-DD')
                           AND to_date(a.cdt) <= to_date(:endDate, 'YYYY-MM-DD')) or a.cdt is null
                         order by strcdt asc";
            }
            else
            {
                sql = @"select distinct case
                                          when b.car_no = '' then
                                           'notrack-' || a.shipment_id
                                          when b.car_no is null then
                                           'notruck-' || a.shipment_id
                                          else
                                           b.car_no || '-' || a.shipment_id
                                        end CAR_NO,
                                        a.shipment_id,
                                         to_char(a.cdt,'YYYYMMDD') strcdt 
                          FROM NONEDIPPS.t_shipment_pallet a
                          join NONEDIOMS.oms_load_car b
                            on a.shipment_id = b.shipment_id
                           and (b.active = 0  or  b.active is null)
                         WHERE (to_date(a.cdt) >= to_date(:startDate, 'YYYY-MM-DD')
                           AND to_date(a.cdt) <= to_date(:endDate, 'YYYY-MM-DD')) or a.cdt is null
                         order by strcdt asc";
            }

            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "startDate", starttime };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endDate", endtime };

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
                //if (chkAllRecord.Checked == false)
                //{
                //    dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
                //}
                //else
                //{
                //    dataSet = ClientUtils.ExecuteSQL(sql);
                //}
            }
            catch (Exception e)
            {
                ShowMsg("连接数据错误" + e, 1);
                return;
            }
            cboCarNo.Items.Clear();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    cboCarNo.Items.Add(dataSet.Tables[0].Rows[i]["CAR_NO"].ToString());
                }
                return ;
            }
            else {
                cboCarNo.Items.Add("");
                return ; }
            
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "";
            txtMessage.BackColor = Color.Blue;
            fCheck fcheck = new fCheck();
            if (fcheck.ShowDialog() != DialogResult.OK)
            {
                ShowMsg("账号权限不正确", 0);
                return;
            }
            else
            {
                rePrintReport pr = new rePrintReport();
                pr.ShowDialog();
            }
        }

        private void chkAllRecord_CheckedChanged(object sender, EventArgs e)
        {
            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            fShipmentConfirm tsc = new fShipmentConfirm();
            tsc.ShowDialog();
        }

        private void btnSCSExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboCarNo.Text))
            {
                ShowMsg("请先选择合适的集货单", 0);
                return;
            }
            string[] truckShipment = cboCarNo.Text.Split('-');
            string strCarNo = truckShipment[0];
            string strSID = truckShipment[1];
            if (strCarNo.Equals("notrack")) 
            { 
                strCarNo = string.Empty;
                ShowMsg("先维护车牌", 0);
                return;
            }
            if (string.IsNullOrEmpty(strSID)) 
            {
                ShowMsg("集货单不能为空", 0);
                return;
            }

            string errormsg = string.Empty;
            strSID = sb.changeSNtoShipmentID(strSID, out errormsg);
            if (errormsg.Contains("NG"))
            {
                ShowMsg("集货单状态不是已经装车", 1);
                return;
            }

            if (!sb.IsCarALLOver(strSID))
            {
                ShowMsg("刷入序号所在车未装车完毕,请检查!", 1);
                return;
            }
            DataTable dtPallet = sb.GetSIDPalletWeightInfo(strSID, strCarNo);
            if (dtPallet==null)
            {
                ShowMsg("无法获取集货单栈板信息", 1);
                return;
            }
            SCSExcelExport( strSID, strCarNo, dtPallet);
            ShowMsg("导出EXCE完成", 1);
        }
        private void SCSExcelExport(string strSID,string strCaronNo,DataTable dtPallet) 
        {
            string strSampleName = @"SCS-SAMPLE.xlsx";
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\ShippingAC\Label";
            string strSamplePathfile= Path.Combine(strLabelPath, strSampleName);

            if (!File.Exists(strSamplePathfile))
            {
                ShowMsg("文件路径不存在:"+ strSampleName+"#"+strSamplePathfile, 1);
                return;
            }
            //关掉所有excel进程；
            killProcess();

            //保存文档
            //获取导出路径
            string filePath = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "EXCEL 97-2007 工作簿(*.xls)|*.xls";//设置文件类型
            sfd.Filter = "EXCEL 工作簿(*.xlsx)|*.xlsx";//设置文件类型
            sfd.FileName = strSID+ "#"+ strCaronNo + "_SCS";//设置默认文件名
            sfd.DefaultExt = "xlsx";//设置默认格式（可以不设）
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName)) { File.Delete(sfd.FileName); }
                filePath = sfd.FileName;
                File.Copy(strSamplePathfile, filePath);
            }
            else
            {
                this.ShowMsg("导出Excel失败！", 0);
                return;
            }
            

            //COPY 模板的内容到新的内容
            IWorkbook workbook;
            string fileExt = Path.GetExtension(filePath).ToLower();
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook(filePath);
            }
            else if (fileExt == ".xls")
            {
                FileStream  fs = File.OpenRead(filePath);
                workbook = new HSSFWorkbook(fs);
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                return;
            }
            //SCS - BULK Air

            ISheet sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  

           
            ICell icell = null;
            //icell=sheet.GetRow(0).GetCell(0);
            //1.更新HAWB
            var cr = new CellReference("D17");
            var row = sheet.GetRow(cr.Row);
            icell = row.GetCell(cr.Col);
            string strHAWB=dtPallet.Rows[0]["HAWB"]?.ToString();
            icell.SetCellValue(strHAWB);
            //2.更新栈板总数
            cr = new CellReference("I18");
            row = sheet.GetRow(cr.Row);
            icell = row.GetCell(cr.Col);
            string strPalletCount = dtPallet.Rows.Count.ToString();
            icell.SetCellValue(strPalletCount);
            //3.更新栈板总数
            //cr = new CellReference("I18");
            //row = sheet.GetRow(cr.Row);
            //icell = row.GetCell(cr.Col);
            //icell.SetCellValue("新栈板总数");

            //7.更新出货车牌号
            cr = new CellReference("F5");
            row = sheet.GetRow(cr.Row);
            icell = row.GetCell(cr.Col);
            string strCarNo = dtPallet.Rows[0]["car_no"]?.ToString();
            icell.SetCellValue(strCarNo);

            //8.更新出货栈板总重
            cr = new CellReference("E5");
            row = sheet.GetRow(cr.Row);
            icell = row.GetCell(cr.Col);
            string strTotalWeight = dtPallet.Rows[0]["totalweight"]?.ToString();
            icell.SetCellValue(strTotalWeight);


            //4.插入栈板需要的行数
            double dPalletCount = dtPallet.Rows.Count;
            int iShowPalletCount = (int)Math.Ceiling(dPalletCount / 2);

            int startRow = 21;//开始插入行索引
                              //excel sheet模板默认可填充4行数据

            //写第一行编号+栈板号+重量 
            icell = sheet.GetRow(startRow-1).GetCell(0);
            icell.SetCellValue(1.ToString());
            icell = sheet.GetRow(startRow-1).GetCell(1);
            icell.SetCellValue(dtPallet.Rows[0]["pallet_no"]?.ToString());
            icell = sheet.GetRow(startRow-1).GetCell(3);
            icell.SetCellValue(dtPallet.Rows[0]["weight"]?.ToString());

            //写第一行 第2列 编号+栈板号+重量 
            if(iShowPalletCount + 1 <= dtPallet.Rows.Count) 
            {
                icell = sheet.GetRow(startRow-1).GetCell(7);
                icell.SetCellValue((iShowPalletCount+1).ToString());
                icell = sheet.GetRow(startRow-1).GetCell(8);
                icell.SetCellValue(dtPallet.Rows[iShowPalletCount]["pallet_no"]?.ToString());
                icell = sheet.GetRow(startRow-1).GetCell(10);
                icell.SetCellValue(dtPallet.Rows[iShowPalletCount]["weight"]?.ToString());
            }


            if (iShowPalletCount > 1)
            {
                //插入行
                sheet.ShiftRows(startRow, sheet.LastRowNum, iShowPalletCount-1, true, false);
                var rowSource = sheet.GetRow(20);
                var rowStyle = rowSource.RowStyle;//获取当前行样式
                
                for (int i = startRow; i < startRow + iShowPalletCount - 1; i++)
                {
                    var rowInsert = sheet.CreateRow(i);
                    if (rowStyle != null)
                        rowInsert.RowStyle = rowStyle;
                        rowInsert.Height = rowSource.Height;

                    for (int col = 0; col < rowSource.LastCellNum; col++)
                    {
                        var cellsource = rowSource.GetCell(col);
                        var cellInsert = rowInsert.CreateCell(col);
                        var cellStyle = cellsource.CellStyle;
                        //设置单元格样式　　　　
                        if (cellStyle != null)
                            cellInsert.CellStyle = cellsource.CellStyle;

                    }
                    sheet.AddMergedRegion(new CellRangeAddress(i, i,1,2));
                    sheet.AddMergedRegion(new CellRangeAddress(i, i, 8, 9));
                    //写其它编号+栈板号+重量  最后一个注意了
                    //写第一行编号+栈板号+重量 
                    icell = sheet.GetRow(i ).GetCell(0);
                    icell.SetCellValue((i- startRow+2).ToString());
                    icell = sheet.GetRow(i ).GetCell(1);
                    icell.SetCellValue(dtPallet.Rows[i - startRow+1]["pallet_no"]?.ToString());
                    icell = sheet.GetRow(i ).GetCell(3);
                    icell.SetCellValue(dtPallet.Rows[i - startRow+1]["weight"]?.ToString());

                    //写第一行 第2列 编号+栈板号+重量 
                    if (iShowPalletCount + i - startRow+2 <= dtPallet.Rows.Count)
                    {
                        icell = sheet.GetRow(i ).GetCell(7);
                        icell.SetCellValue((iShowPalletCount + i - startRow+2).ToString());
                        icell = sheet.GetRow(i ).GetCell(8);
                        icell.SetCellValue(dtPallet.Rows[iShowPalletCount + i - startRow+1]["pallet_no"]?.ToString());
                        icell = sheet.GetRow(i ).GetCell(10);
                        icell.SetCellValue(dtPallet.Rows[iShowPalletCount + i - startRow+1]["weight"]?.ToString());
                    }
                }

            }

            //5.更新出货城市
            icell=sheet.GetRow(26+ iShowPalletCount - 1).GetCell(2);
            string strCountry = dtPallet.Rows[0]["shiptocountry"]?.ToString();
            icell.SetCellValue(strCountry);

            //6.更新出货货代
            icell = sheet.GetRow(24 + iShowPalletCount - 1).GetCell(9);
            string strCarrier = dtPallet.Rows[0]["carrier_name"]?.ToString();
            icell.SetCellValue(strCarrier);

         


            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();
            workbook.Close();
            
        }
        public void killProcess()
        {
            //得到所有打开的进程        
            foreach (Process thisproc in Process.GetProcessesByName("Excel"))
            {
                //找到程序进程,kill之。
                if (!thisproc.CloseMainWindow())
                {
                    thisproc.Kill();
                }
            }

        }


    }
}
