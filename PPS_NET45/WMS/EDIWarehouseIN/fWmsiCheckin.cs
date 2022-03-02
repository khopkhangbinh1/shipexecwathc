using ClientUtilsDll;
using EDIWarehouseIN.JSMESWebReference;
using EDIWarehouseIN.WCF;
using Newtonsoft.Json;
using OperationWCF;
using Oracle.ManagedDataAccess.Client;
using SajetClass;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EDIWarehouseIN.WCF.CommonModel;

namespace EDIWarehouseIN
{
    public partial class fWmsiCheckin : Form
    {
        public fWmsiCheckin()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }
        public int H = 0;
        public int W = 0;
        private void initGroupBox()
        {
            W = Screen.PrimaryScreen.Bounds.Width;

            W = Convert.ToInt32(W * 0.5);


            this.grpPallet.Width = W;
        }
        public string PALLETNO
        {
            get
            {
                return this.txtPalletNO.Text;
            }
            set
            {
                this.txtPalletNO.Text = value;
            }
        }
        public string TROLLEYNO
        {
            get
            {
                return this.txtTrolleyNO.Text;
            }
            set
            {
                this.txtTrolleyNO.Text = value;
            }
        }
        string strFGinMESWcf = string.Empty;
        string strProduct = string.Empty;
        string strInSnType = string.Empty;
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sUserID = ClientUtils.UserPara1;
        private string g_ServerIP = ClientUtils.url;

        EDIWarehouseINBLL wb = new EDIWarehouseINBLL();

        private void fWmsiCheckin_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            initGroupBox();


        }

        
        private void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            if (dts.Tables[0].Rows.Count > 0)
            {

                //return DtbPickList;
                //绑定单据号
                //List<string> shipmentList = (from d in dts.Tables[0].AsEnumerable()
                //                             select d.Field<string>(colName)
                //                           ).Distinct()
                //                           .ToList();
                //shipmentList.Add("");
                //shipmentList.Sort();
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";

            }
            else
            {
                cmb.Items.Clear();
            }
            selecttxtCarton();
        }

      

        private void selecttxtCarton()
        {
            txtCarton.SelectAll();
            txtCarton.Focus();
        }
        private void selecttxttrolleyline()
        {
            txtTrolleyLine.SelectAll();
            txtTrolleyLine.Focus();
        }


        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            //可扫描序号/箱号
            string strPalletNO = txtPalletNO.Text.Trim();
            string strTrolleyLine = txtTrolleyLine.Text.Trim();
            string strCarton = txtCarton.Text.Trim();
            strCarton = wb.DelPrefixCartonSN(strCarton);
            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的SN/Carton不能为空！", 0);
                selecttxtCarton();
                return;
            }

            

            string strResult2 = string.Empty;
            string strResulterrmsg2 = string.Empty;

            //执行checkin SP 
            strResult2 = wb.WmsiTrolleyCheckin(strPalletNO, strTrolleyLine, strCarton, out strResulterrmsg2);

            if (strResult2.Equals("OK"))
            {
                wb.ShowStockInfo2(strPalletNO, strTrolleyLine, dgvLine, strCarton);

                if (strResulterrmsg2.Contains("FINISHLINE"))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    txtTrolleyLine.Enabled = true;
                    selecttxttrolleyline();
                    txtCarton.Enabled = false;
                    wb.ShowStockInfo2(strPalletNO, "", dgvPallet, strTrolleyLine);
                }
                else if (strResulterrmsg2.Contains("FINISHTROLLEY")) 
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByCheckOK();
                    txtTrolleyLine.Enabled = false;
                    txtCarton.Enabled = false;

                    wb.ShowStockInfo2(strPalletNO, "", dgvPallet, strTrolleyLine);
                }
                else 
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByOk();
                    selecttxtCarton();
                }
                ShowMsg(strResulterrmsg2, -1);


            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg(strResulterrmsg2, 0);
            }

            
        }

        private void FeedbackMesStockResult(string strProduct, string strUrl, string strCarton) 
        {
            try
            {

                if (strProduct.Equals("WATCH"))
                {
                    string ret = string.Empty;
                    JSMESWebReference.MesApi ws = new MesApi(strUrl);
                    ret = ws.UpdateStockINStatus(strCarton);
                }
                else if (strProduct.Equals("AIRPOD"))
                {
                    MessageBox.Show("暂时不支持" + strProduct + "此产品入库");
                    return;
                }
                else
                {
                    MessageBox.Show("暂时不支持" + strProduct + "此产品入库");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private bool CheckLocationMatchToPPS(string strGUID,string  strLocationRegion, out string strOutmsg)
        {
            DataTable dtRegionNoMatch = wb.GetRegionNoMatchDataTable(strGUID , strLocationRegion);
            if (dtRegionNoMatch != null)
            {
                // 有记录说明为新料号
                for (int i = 0; i < dtRegionNoMatch.Rows.Count; i++)
                {
                    if (strProduct.Equals("WATCH"))
                    {
                        strOutmsg = "产品对应的地区:"+ dtRegionNoMatch.Rows[i]["region"].ToString()+ "与储位对应的地区:"+strLocationRegion+"不一致";
                        return false;
                    }
                    else if (strProduct.Equals("AIRPOD"))
                    {
                        strOutmsg = "暂时不支持" + strProduct + "此产品入库";
                        return false;
                    }
                    else
                    {
                        strOutmsg = "暂时不支持" + strProduct + "此产品入库";
                        return false;
                    }
                }
            }
            strOutmsg = "";
            return true;
        }

        private bool  InertNewPartNoToPPS(string strGUID ,out string strOutmsg) 
        {
            DataTable dtNoExistPartNO = wb.GetNoExistPartNODataTable(strGUID);
            if (dtNoExistPartNO != null )
            {
                // 有记录说明为新料号
                for (int i = 0; i < dtNoExistPartNO.Rows.Count; i++)
                {
                    if (strProduct.Equals("WATCH"))
                    {
                        string strPNReturnJson = string.Empty;
                        string partNo = dtNoExistPartNO.Rows[i]["PART_NO"].ToString();
                        string batchType = partNo.Substring(partNo.Length - 4, 4);
                        partNo = batchType == "-S01" || batchType == "-M04" ?
                            partNo.Substring(0, partNo.Length - 4) : partNo;
                        try
                        {

                            JSMESWebReference.MesApi ws = new JSMESWebReference.MesApi(strFGinMESWcf);
                            strPNReturnJson = ws.GetMESPNInfo(partNo);
                        }
                        catch (Exception ex)
                        {
                            strOutmsg=ex.Message;
                            return false;
                        }

                        PNRETURNMODEL PNResultModel = new PNRETURNMODEL();
                        PNResultModel = JsonConvert.DeserializeObject<PNRETURNMODEL>(strPNReturnJson);
                        string strPNResultModel = PNResultModel.RESULT;
                        string strPNErrmsg = PNResultModel.RESULT;

                        if (!strPNErrmsg.ToUpper().Equals("TRUE"))
                        {
                            selecttxtCarton();
                            strOutmsg = "获取MES 料号资料异常:" + PNResultModel.MSG;
                            return false; ;
                        }

                        PNLIST[] TeturnPNList = PNResultModel.PNLIST;
                        for (int j = 0; j < TeturnPNList.Count(); j++)
                        {
                            PNLIST pninfo = TeturnPNList[j];
                            string stroutPNerrmsg = string.Empty;
                            pninfo.PN = batchType == "-S01" || batchType == "-M04" ?
                                pninfo.PN + batchType : pninfo.PN;
                            string strPNResult = wb.ExecutePNIN(pninfo, out stroutPNerrmsg);
                            if (!strPNResult.Equals("OK"))
                            {
                                strOutmsg = stroutPNerrmsg;
                                return false; ;
                            }
                        }
                    }
                    else if (strProduct.Equals("AIRPOD"))
                    {
                        strOutmsg = "暂时不支持" + strProduct + "此产品入库";
                        return false; 
                    }
                    else
                    {
                        strOutmsg = "暂时不支持" + strProduct + "此产品入库";
                        return false;
                    }
                }
            }
            strOutmsg = "";
            return true;
        }
      

        private bool ShowMesSnOnDGV2(FGINRETURNMODEL MesSnModel, string strGUID,out  string outMsg) 
        {
            try {
                string strINSN = MesSnModel.INSN;
                string strResultModel = MesSnModel.RESULT;
                string strErrmsg = MesSnModel.MSG;

                if (!strResultModel.ToUpper().Equals("TRUE"))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    outMsg = "获取MES资料异常:" + strErrmsg;
                    selecttxtCarton();
                    return false;
                }
                List<SNLIST> TeturnSNList = MesSnModel.SNLIST
                    .Select(x => {
                        x.in_guid = strGUID;
                        x.product_name = strProduct;
                        //x.warehouse_id = strWH;
                        //x.location_id = strLocation;
                        return x;
                    }).ToList();

                dgvPallet.DataSource = TeturnSNList;
                dgvPallet.AutoResizeColumns(
                   DataGridViewAutoSizeColumnsMode.AllCells);
                int i = 1;
                foreach (DataGridViewRow dr in dgvPallet.Rows)
                {
                    dr.HeaderCell.Value = (i++).ToString();
                }

                // Insert Data
                string sql = @"insert into ppsuser.mes_sn_status
                (in_guid,work_order,serial_number,customer_sn,carton_no,
                 pallet_no,part_no,mpn,hold_flag,trolley_line_no,point_no,
                 warehouse_id,location_id,product_name,delivery_no,line_item,
                 trolley_no,batchtype,load_id, model,region)
              values 
                (:in_guid, :work_order, :serial_number, :customer_sn, :carton_no, :pallet_no,
                 :part_no, :mpn, :hold_flag, :trolley_line_no, :point_no, :warehouse_id, :location_id,
                 :product_name, :delivery_no, :line_item, :trolley_no, :batchtype, :load_id, : model, :region)";

                ClientUtils.DoTransaction<SNLIST>(sql, TeturnSNList);


            }
            catch (Exception ex) {
                outMsg = ex.Message;
                return false;
            }

            outMsg = "";
            return true;
        }

        #region //HYQ  ShowMesSnOnDGV OLD 写法
        private bool ShowMesSnOnDGV(FGINRETURNMODEL MesSnModel, string strGUID, string strForceFlag, out string outMsg)
        {

            //FGINRETURNMODEL MesSnModel = new FGINRETURNMODEL();
            //ResultModel = JsonConvert.DeserializeObject<FGINRETURNMODEL>(strMODEL);
            string strINSN = MesSnModel.INSN;
            string strResultModel = MesSnModel.RESULT;
            string strErrmsg = MesSnModel.MSG;

            if (!strResultModel.ToUpper().Equals("TRUE"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                outMsg = "MES反馈序号信息失败:" + strErrmsg;
                selecttxtCarton();
                return false;
            }
            SNLIST[] TeturnSNList = MesSnModel.SNLIST;

            //ERPStockInModel ToSAPModel = new ERPStockInModel();
            //ToSAPModel.SPCQN = MesSnModel.INSN;
            //ToSAPModel.HSDAT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //List<ERPStockInItemModel> ToSAPItemModelList = new List<ERPStockInItemModel>();

            dgvPallet.DataSource = null;
            dgvPallet.Rows.Clear();
            for (int i = 0; i < TeturnSNList.Count(); i++)
            {
                SNLIST sninfo = TeturnSNList[i];
                DataGridViewRow dr = new DataGridViewRow();
                foreach (DataGridViewColumn c in dgvPallet.Columns)
                {
                    dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                }
                //累加序号
                dr.HeaderCell.Value = (i + 1).ToString();
                dr.Cells[0].Value = sninfo.SN;
                dr.Cells[1].Value = sninfo.WO;
                dr.Cells[2].Value = sninfo.BATCHTYPE;
                dr.Cells[3].Value = sninfo.LOAD_ID;
                dr.Cells[4].Value = sninfo.BOXID;
                dr.Cells[5].Value = sninfo.PALLETID;
                if (sninfo.BATCHTYPE.ToString().Equals("M04") || sninfo.BATCHTYPE.ToString().Equals("S01"))
                {
                    dr.Cells[6].Value = sninfo.PN + "-" + sninfo.BATCHTYPE;
                }
                else
                {
                    dr.Cells[6].Value = sninfo.PN;
                }
                dr.Cells[7].Value = sninfo.MODEL;
                dr.Cells[8].Value = sninfo.REGION;
                dr.Cells[9].Value = sninfo.CUSTPN;
                dr.Cells[10].Value = sninfo.QHOLDFLAG;
                dr.Cells[11].Value = sninfo.TROLLEYNO;
                dr.Cells[12].Value = sninfo.TROLLEYLINENO;
                dr.Cells[13].Value = sninfo.TROLLEYLINENOPOINT;
                dr.Cells[14].Value = sninfo.DN;
                dr.Cells[15].Value = sninfo.ITEMNO;

                try
                {
                    dgvPallet.Invoke((MethodInvoker)delegate ()
                    {
                        dgvPallet.Rows.Add(dr);
                        //insert
                        //SP内检查PPS是否序号重复， 如果OK，
                        string strerrmsg = string.Empty;
                        string strResult = wb.ExecuteFGIN(strGUID, sninfo, strProduct, strForceFlag, out strerrmsg);
                        if (!strResult.Equals("OK"))
                        {
                            MessageBox.Show(strerrmsg);
                            return;
                        }
                    });
                }
                catch (Exception e1)
                {
                    outMsg = e1.ToString();
                    return false;
                }
            }
            outMsg = "";
            return true;
        }
        #endregion
        private string GetMesSnInfo(string strProduct, string strUrl,string strCarton) 
        {
            try
            {
                if (strProduct.Equals("WATCH"))
                {
                    //MesApi ws = HttpChannel.Get<MesApi>(serviceUrl);
                    JSMESWebReference.MesApi ws = new JSMESWebReference.MesApi(strUrl);
                    return  ws.GetMESStockInfo(strCarton);


                }
                else if (strProduct.Equals("AIRPOD"))
                {
                    MessageBox.Show("暂时不支持" + strProduct + "此产品入库");
                    return null;
                }
                else
                {
                    MessageBox.Show("暂时不支持" + strProduct + "此产品入库");
                    return null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
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

        private void txtTrolleyLine_KeyDown(object sender, KeyEventArgs e)
        {
            string strPalletNO = txtPalletNO.Text.Trim();
            string strTrolleyLine = txtTrolleyLine.Text.Trim();
            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strTrolleyLine))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("车行号不存在", 0);
                selecttxttrolleyline();
                return;
            }
            //增加一个检查，如果行号满了就不用了


            dgvLine.DataSource = null;
            dgvLine.Rows.Clear();

            DataTable db = wb.GetTrolleyNODataTable(strPalletNO, strTrolleyLine);
            if (db == null)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("查无结果", 0);
                selecttxttrolleyline();
                btnStart.Enabled = true;
                return;
            }
            if (db.Rows.Count > 0)
            {
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvLine.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号

                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = db.Rows[i]["TROLLEY_LINE_NO"].ToString();
                    dr.Cells[1].Value = db.Rows[i]["POINT_NO"].ToString();
                    dr.Cells[2].Value = db.Rows[i]["DELIVERY_NO"].ToString();
                    dr.Cells[3].Value = db.Rows[i]["LINE_ITEM"].ToString();
                    dr.Cells[4].Value = db.Rows[i]["CARTON_NO"].ToString();
                    dr.Cells[5].Value = db.Rows[i]["CUSTOMER_SN"].ToString();

                    try
                    {
                        dgvLine.Invoke((MethodInvoker)delegate ()
                        {
                            dgvLine.Rows.Add(dr);
                            if (db.Rows[i]["POINT_NO"].ToString().Equals("0"))
                            {
                                dr.DefaultCellStyle.BackColor = Color.Green;
                            }

                        });
                    }
                    catch (Exception e1)
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        ShowMsg(e1.ToString(), 0);
                        selecttxttrolleyline();
                        btnStart.Enabled = true;
                        return;

                    }
                }
            }
            txtTrolleyLine.Enabled = false;
            txtCarton.Enabled = true;
            selecttxtCarton();
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            btnStart.Enabled = false;
            dgvPallet.DataSource = null;
            dgvPallet.Rows.Clear();

            string strPalletNO = txtPalletNO.Text.Trim();
            strPalletNO = wb.DelPrefixCartonSN(strPalletNO);
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strPalletNO))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("栈板号不能为空！", 0);
                btnStart.Enabled = true;
                return;
            }

            DataTable db = wb.GetTrolleyNODataTable(strPalletNO);
            if (db == null)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("查无结果", 0);
                btnStart.Enabled = true;
                return;
            }
            if (db.Rows.Count > 0)
            {
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPallet.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号

                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = db.Rows[i]["TROLLEY_LINE_NO"].ToString();
                    dr.Cells[1].Value = db.Rows[i]["POINT_NO"].ToString();
                    dr.Cells[2].Value = db.Rows[i]["DELIVERY_NO"].ToString();
                    dr.Cells[3].Value = db.Rows[i]["LINE_ITEM"].ToString();
                    dr.Cells[4].Value = db.Rows[i]["CARTON_NO"].ToString();
                    dr.Cells[5].Value = db.Rows[i]["CUSTOMER_SN"].ToString();

                    try
                    {
                        dgvPallet.Invoke((MethodInvoker)delegate ()
                        {
                            if (db.Rows[i]["POINT_NO"].ToString().Equals("0"))
                            {
                                dr.DefaultCellStyle.BackColor = Color.Green;
                            }
                            
                            dgvPallet.Rows.Add(dr);

                        });
                    }
                    catch (Exception e1)
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        ShowMsg(e1.ToString(), 0);
                        btnStart.Enabled = true;
                        return;

                    }

                }
            }
            btnStart.Enabled = true;


            txtTrolleyLine.Enabled = true;
            txtCarton.Enabled = false;
            selecttxttrolleyline();

            //show


        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            txtTrolleyLine.Text = "";
            txtCarton.Text = "";
            txtTrolleyLine.Enabled = false;
            txtCarton.Enabled = false;
        }

        private void fWmsiCheckin_FormClosing(object sender, FormClosingEventArgs e)
        {
            wb.UpdateDgvPalletStatus(txtPalletNO.Text,fMain.dgvnoa);
        }

        private void dgvLine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
