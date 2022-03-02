using ClientUtilsDll;
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
using System.IO;
using System.Diagnostics;

namespace EDIWareHouseTrans
{
    public partial class fWmsoCheck : Form
    {
        public fWmsoCheck()
        {
            InitializeComponent();
        }
        public int H = 0;
        public int W = 0;

        EDIWarehouseOUTBLL eb = new EDIWarehouseOUTBLL();
        public string SAPNO
        {
            get
            {
                return this.txtSAPNO.Text;
            }
            set
            {
                this.txtSAPNO.Text = value;
            }
        }
        private void initGroupBox()
        {
            W = Screen.PrimaryScreen.Bounds.Width;

            W = Convert.ToInt32(W * 0.5);


            this.grpPallet.Width = W;
        }

        private void fWmsoCheck_Load(object sender, EventArgs e)
        {
            this.txtCartonStart.SelectAll();
            this.txtCartonStart.Focus();
            this.WindowState = FormWindowState.Maximized;
            initGroupBox();

            string strSAPNO = txtSAPNO.Text;
            
            string strSql2 = @"select '-ALL-' as picksapno
                                  from dual
                                union
                                select pick_sap_no as picksapno
                                  from (select distinct pick_sap_no
                                          from ppsuser.t_wmso_sapno_pick a
                                         where a.sap_no = :insapno
                                         order by pick_sap_no asc)";
            object[][] Param2 = new object[1][];
            Param2[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insapno", strSAPNO };
            DataSet dts2 = ClientUtils.ExecuteSQL(strSql2, Param2);
            if (dts2 != null && dts2.Tables[0].Rows.Count > 0)
            {
                List<string> picksaplist = (from d in dts2.Tables[0].AsEnumerable()
                                         select d.Field<string>("picksapno")).ToList();
                picksaplist.Sort();
                cmbPickSAPid.DataSource = picksaplist;
            }
            else
            {
                List<string> picksaplist = new List<string>();
                picksaplist.Add("-ALL-");
                cmbPickSAPid.DataSource = picksaplist;
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string strSAPNO = txtSAPNO.Text;
            string strPickSAPNO = cmbPickSAPid.Text;
            if (string.IsNullOrEmpty(strSAPNO)) 
            {
                ShowMsg("SAP单号不能为空",0);
                return;
            }
            btnStart.Enabled = false;

            //dgvSAP.Rows.Clear();
            dgvSAP.DataSource = eb.GetSAPNOCartonList( strSAPNO,  strPickSAPNO);

            if (dgvSAP.Rows.Count < 1)
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
            TextMsg.Text = strTxt.TP();
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


            dgvSAP.DataSource = null;
            dgvCheck.DataSource = null;
            if (dgvCheck.Rows.Count>1)
            {
                dgvCheck.Rows.Clear();
            }
            lblQTY.Text= "0/0";
            ShowMsg("",0);   
            txtCarton.Enabled = false;
            btnStart.Enabled = true;
        }

        private void dgvSAP_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvSAP.Rows.Count; i++)
            {
                this.dgvSAP.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            }
        }

        private void dgvCheck_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvCheck.Rows.Count; i++)
            {
                this.dgvCheck.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            }
        }


        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sUserID = ClientUtils.UserPara1;
        private string g_ServerIP = ClientUtils.url;
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
            string strSAPNO = txtSAPNO.Text;
            string strPickSAPNO = cmbPickSAPid.Text;
            if (string.IsNullOrEmpty(strSAPNO))
            {
                ShowMsg("SAP单号不能为空", 0);
                return;
            }
            strSN = eb.DelPrefixCartonSN(strSN);
            strSN = eb.ChangeCSNtoCarton(strSN);

            DataTable dt = eb.CheckCartonInSAPNO(strSAPNO, strPickSAPNO, strSN);

            if (dt == null)
            {
                txtCarton.SelectAll();
                ShowMsg("此箱号"+ strSN +"不属于此SAP单", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                return;
            }
            if (dgvCheck.Rows.Count > 1)
            {
                for (int i = 0; i < dgvCheck.Rows.Count-1; i++)
                {
                    if (dgvCheck.Rows[i].Cells["carton_no"].Value.ToString() == strSN)
                    {
                        this.ShowMsg("输入的箱号已经刷入过，重复", 0);
                        dgvCheck.Rows[i].Selected = true;
                        dgvCheck.FirstDisplayedScrollingRowIndex = i;
                        LibHelper.MediasHelper.PlaySoundAsyncByRe();
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
                for (int i = 0; i < dgvCheck.Rows.Count-1; i++)
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
                lblQTY.Text = (dgvCheck.Rows.Count-1).ToString() + "/" + (dgvSAP.Rows.Count-1).ToString();
                ShowMsg("OK", 0);
            }

            if (dgvSAP.Rows.Count > 1)
            {
                for (int i = 0; i < dgvSAP.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dgvSAP.Rows[i].Cells["CARTON_NO"].Value.ToString() == dt.Rows[j]["carton_no"]?.ToString())
                        {
                            dgvSAP.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            dgvSAP.Rows[i].Selected = true;
                            dgvSAP.FirstDisplayedScrollingRowIndex = i;
                        }
                    }

                }
            }


            txtCarton.SelectAll();
            LibHelper.MediasHelper.PlaySoundAsyncByOk();
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "check end",  strSAPNO + "*" + strPickSAPNO + "*" + strSN + "*" + g_sUserNo + "*" + g_ServerIP);
            if (dgvSAP.Rows.Count== dgvCheck.Rows.Count)
            {
                string strPickpalletno = cmbPickSAPid.Text.Trim();
                if (string.IsNullOrEmpty(strPickpalletno))
                {
                    ShowMsg("PickPalletNO 不能为空", 0);
                    return;
                }
                if (!string.IsNullOrEmpty(strPickpalletno))
                {
                    string strMsgOut = string.Empty;
                    if (!PrintPalletLabel(strPickpalletno, 14, out strMsgOut))
                    {
                        ShowMsg("打印失败" + strMsgOut, 0);
                    }
                    else
                    {
                        ShowMsg("打印OK", 0);
                    }

                }
                this.txtCartonStart.Enabled = true;
                this.txtCartonStart.Text = "";
                this.txtCartonStart.SelectAll();
                this.txtCartonStart.Focus();
                this.txtCarton.Text = "";
                this.txtCarton.Enabled = false;

                txtSAPNO.Text = "";
                cmbPickSAPid.Text = "";
                lblQTY.Text = "0/0";
                dgvSAP.DataSource = null;
                dgvCheck.DataSource = null;
                if (dgvCheck.Rows.Count > 1)
                {
                    dgvCheck.Rows.Clear();
                }

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCarton_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCartonStart_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string strCartno = this.txtCartonStart.Text;

                TextMsg.Text = "";
                if (e.KeyCode != Keys.Enter)
                {
                    return;
                }
                ShowMsg("", -1);

                if (string.IsNullOrEmpty(strCartno.Trim()))
                {
                    ShowMsg("CSN/箱号不能为空", 0);
                    return;
                }
                if (strCartno.Trim().StartsWith("0000"))
                {
                    strCartno = strCartno.Substring(2, strCartno.Length - 2);
                    this.txtCartonStart.Text = strCartno;
                }
                DataTable dt = new DataTable();
                dt = eb.GetSAPNOandPICKSAPNOList(strCartno);
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txtSAPNO.Text = dt.Rows[0]["sap_no"].ToString();

                    List<string> picksaplist = new List<string>();
                    picksaplist.Add(dt.Rows[0]["pick_sap_no"].ToString());
                    cmbPickSAPid.DataSource = picksaplist;

                }
                else
                {
                    ShowMsg("CSN/箱号输入有误或者未Pick完毕", 0);
                    return;
                }
                this.txtCartonStart.Enabled = false;
                btnStart_Click(null, null);
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
            }
        }
        EDIWarehouseOUTBLL wb = new EDIWarehouseOUTBLL();
        private void txtCarton2_KeyDown(object sender, KeyEventArgs e)
        {
            string strCarton = txtCarton2.Text.Trim();
            strCarton = wb.DelPrefixCartonSN(strCarton);

            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                //LibHelper.MediasHelper.PlaySoundAsyncByNg();
                //this.ShowMsg("输入的/PalletID/SN/Carton不能为空！", 0);
                txtCarton2.SelectAll();
                txtCarton2.Focus();
                return;
            }
            LibHelper.LogHelper.InsertPPSExcuteSNLog("WMSO", "出库PPS Start", "CARTONNO:" + strCarton);

            DataTable dt0 = wb.GetPickSAPNO(strCarton);
            if (dt0 == null)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("输入非法无效的序号或者箱号，不做统计。", 0);
                txtCarton2.Text = "";
                txtCarton2.Focus();
                return;
            }

            string strPickSapNo = dt0.Rows[0]["picksapno"].ToString();
            if (string.IsNullOrEmpty(strPickSapNo))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("箱号没有刷入出库单，对应出库单为空。", 0);
                txtCarton2.Text = "";
                txtCarton2.Focus();
                return;
            }

            string strSAPNO = strPickSapNo.Substring(3);

            if (!string.IsNullOrEmpty(strPickSapNo))
            {
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
                string strSAPNOComputerName = wb.GetComputerNameOfSAPNO(strSAPNO);
                if (!string.IsNullOrEmpty(strSAPNOComputerName) && !localHostname.Equals(strSAPNO))
                {
                    ShowMsg("此SAP单正在电脑名:" + strSAPNOComputerName + "上作业不得扣账", 0);
                    return;
                }
                string strMsgOut = string.Empty;
                if (!PrintPalletLabel(strPickSapNo, 14, out strMsgOut))
                {
                    ShowMsg("打印失败" + strMsgOut, 0);
                }
                else
                {
                    ShowMsg("打印OK", 0);
                }

            }
        }

        public bool PrintPalletLabel(string strPickSapNo, int listrows, out string sMessage)
        {
            //---------------------
            sMessage = "";
            string strLabelName = string.Empty;
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\WMS\Label";

            string strSAWBerrmsg = string.Empty;
            string strRegion = string.Empty;

            listrows = 14;
            strLabelName = @"WMSO_Pick_Label2";



            //CURPAGE  TOTALPAGE  一个栈板MIX多少，打印就有多少行，依据label能打印的最大行数分页，打印palletloadingsheet
            string mixTotalSelect = string.Empty;

            DataTable dt_mixTotal = new DataTable();

            DataTable dt = wb.GetPickPrintInfo(strPickSapNo);
            string strPrintContext = string.Empty;
            if (dt != null)
            {
                string strSAPNO2 = dt.Rows[0]["sapno"]?.ToString();
                string strPickSapNo2 = dt.Rows[0]["picksapno"]?.ToString();
                string strPickQty = dt.Rows[0]["pickqty"]?.ToString();
                string strTotalQty = dt.Rows[0]["totalqty"]?.ToString();
                strPrintContext = strSAPNO2 + "|" + strPickSapNo2 + "|" + strPickQty + "|" + strTotalQty;

            }

            dt_mixTotal = wb.GetPickPrintInfo2(strPickSapNo);
            if (dt_mixTotal == null)
            {
                sMessage = "箱号无对应出库单资料。";
                return false;
            }
            if (dt_mixTotal.Rows.Count > 0)
            {
                //确定打印的label的张数palletcount， 定义个list的行数datarows，
                //mix的所有行数
                int mixTotalrow = dt_mixTotal.Rows.Count;
                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));
                //HYQ： 这部分是写.dat文件。
                string LabelParam = @"SAPNO|PICKSAPNO|PICKQTY|TOTALQTY|CURPAGE|TOTALPAGE|ICTPN1|MPN1|QTY1|ICTPN2|MPN2|QTY2|ICTPN3|MPN3|QTY3|ICTPN4|MPN4|QTY4|ICTPN5|MPN5|QTY5|ICTPN6|MPN6|QTY6|ICTPN7|MPN7|QTY7|ICTPN8|MPN8|QTY8|ICTPN9|MPN9|QTY9|ICTPN10|MPN10|QTY10|ICTPN11|MPN11|QTY11|ICTPN12|MPN12|QTY12|ICTPN13|MPN13|QTY13|ICTPN14|MPN14|QTY14|";

                //label上唯一值的部分  REF 现在不定义 以后再补
                //   SAPNO|PICKSAPNO|PICKQTY|TOTALQTY|CURPAGE|TOTALPAGE 
                string strHead = "";

                //label上清单值的部分
                //   |ICTPN1|MPN1|QTY1|ICTPN2|MPN2|QTY2|ICTPN3|MPN3|QTY3|ICTPN4|MPN4|QTY4|ICTPN5|MPN5|QTY5|ICTPN6|MPN6|QTY6|ICTPN7|MPN7|QTY7|ICTPN8|MPN8|QTY8|ICTPN9|MPN9|QTY9|ICTPN10|MPN10|QTY10|ICTPN11|MPN11|QTY11|ICTPN12|MPN12|QTY12|ICTPN13|MPN13|QTY13|ICTPN14|MPN14|QTY14|

                string strLine = "";

                //HYQ：这循环里面只是产生.lst文件， 如果打印5张就产生5个文档。 
                //.lst 的内容是 一行栏位名 来源于.dat文件strItemno,加换行， 再加已将数据，数据部分是由strHead + strLine。
                //HYQ：20190801如果是ALL，就产生一个文档打印，快
                string strPalletList = string.Empty;
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    string[] strHeadArr = new string[TOTALPAGE];
                    string[] strLineArr = new string[TOTALPAGE];
                    string[] strAllArr = new string[TOTALPAGE];

                    //确定这些值
                    //    SAPNO|PICKSAPNO|PICKQTY|TOTALQTY|CURPAGE|TOTALPAGE 


                    //因i从0开始，所以加1
                    string strcurpage = (i + 1).ToString();

                    strHead = "";
                    strHead = strPrintContext + "|" + strcurpage + "|" + TOTALPAGE + "|";
                    strHeadArr[i] = strHead;

                    //确定以下的部分 循环
                    //    |ICTPN1|MPN1|QTY1
                    strLine = "";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dt_mixTotal.Rows.Count)
                        {
                            break;
                        }

                        string strictpn = dt_mixTotal.Rows[j]["ictpn"].ToString();
                        string strmpn = dt_mixTotal.Rows[j]["mpn"].ToString();
                        string strqty = dt_mixTotal.Rows[j]["partpickqty"].ToString();

                        strLine = strLine + strictpn + "|" + strmpn + "|" + strqty + "|"
                                  ;
                    }
                    strLineArr[i] = strLine;

                    strAllArr[i] = LabelParam + "\r\n" + strHeadArr[i] + strLineArr[i];

                    strPalletList = strPalletList + strHeadArr[i] + strLineArr[i] + "\r\n";

                    //HYQ： 以下3行不一定会用
                    //strHead = getPalletLabelHeadData(strpalletno);
                    //strLine = getPalletLabelLineData(strpalletno, i);
                    //strAll = LabelParam + "\r\n" + strHead + strLine;

                    string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i + ".lst";
                    if (File.Exists(str7))
                    {
                        File.Delete(str7);
                    }
                    this.WriteToPrintGo(str7, strAllArr[i]);
                }

                strPalletList = LabelParam + "\r\n" + strPalletList;
                string strPalletfile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + "ALL.lst";
                if (File.Exists(strPalletfile))
                {
                    File.Delete(strPalletfile);
                }
                this.WriteToPrintGo(strPalletfile, strPalletList);


                //一次打印所有的
                using (Process p = new Process())
                {
                    string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                    if (!File.Exists(strSampleFile))
                    {
                        sMessage = "Sample File Not exists-" + strSampleFile;
                        return false;
                    }
                    p.StartInfo.FileName = "bartend.exe";
                    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + "ALL.lst" + '"').Replace("@QTY", "1");
                    p.StartInfo.Arguments = sArguments;
                    p.Start();
                    p.WaitForExit();
                }
                return true;


            }
            else
            {
                return false;
            }
        }

        private void WriteToPrintGo(string sFile, string sData)
        {
            try
            {
                if (File.Exists(sFile))
                {
                    File.Delete(sFile);
                }
                File.AppendAllText(sFile, sData, Encoding.Default);
            }
            finally
            {
            }
        }






    }
}