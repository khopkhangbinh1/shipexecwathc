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
    public partial class fGetMesPallet : Form
    {
        public fGetMesPallet()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        string strFGinMESWcf = string.Empty;
        string strProduct = string.Empty;
        string strInSnType = string.Empty;
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sUserID = ClientUtils.UserPara1;
        private string g_ServerIP = ClientUtils.url;

        EDIWarehouseINBLL wb = new EDIWarehouseINBLL();

        private void fGetMesPallet_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
           
            //填充产品信息
            string strSql2 = string.Format(@"
                 select a.para_value id, a.para_type name
                   from ppsuser.t_basicparameter_info a
                  where a.enabled = 'Y'
                    and a.remark in ('入库产品')
                    and a.para_type in ('WATCH','AIRPOD')
                  order by a.para_type desc
                ");
            fillCmb(strSql2, "para_type", cmbFGName);
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

        private void rdoPallet_CheckedChanged(object sender, EventArgs e)
        {
            dgvCarton.DataSource = null;
            dgvCarton.Rows.Clear();
            dgvCarton.Columns.Clear();
            if (rdoPallet.Checked) 
            {
                txtCarton.Text = "";
                strInSnType = "PALLET";
                dgvCarton.ColumnCount = 16;
                dgvCarton.ColumnHeadersVisible = true;
                // Set the column header style.
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                //columnHeaderStyle.BackColor = Color.Beige;
                //columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                //dgvCarton.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

                // Set the column header names.
                dgvCarton.Columns[0].Name = "SN";
                dgvCarton.Columns[1].Name = "WO";
                dgvCarton.Columns[2].Name = "BATCHTYPE";
                dgvCarton.Columns[3].Name = "LOADID";
                dgvCarton.Columns[4].Name = "BOX_ID";
                dgvCarton.Columns[5].Name = "PALLETID";
                dgvCarton.Columns[6].Name = "PN";
                dgvCarton.Columns[7].Name = "MODEL";
                dgvCarton.Columns[8].Name = "REGION";
                dgvCarton.Columns[9].Name = "CUSTPN";
                dgvCarton.Columns[10].Name = "QHOLDFLAG";
                dgvCarton.Columns[11].Name = "TROLLEYNO";
                dgvCarton.Columns[12].Name = "TROLLEYLINENO";
                dgvCarton.Columns[13].Name = "TROLLEYLINENOPOINT";
                dgvCarton.Columns[14].Name = "DN";
                dgvCarton.Columns[15].Name = "ITEMNO";
            }
            else 
            {
                txtCarton.Text = "";
                strInSnType = "CARTON";

                dgvCarton.ColumnCount = 5;
                dgvCarton.ColumnHeadersVisible = true;
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                
                dgvCarton.Columns[0].Name = "BOX_ID";
                dgvCarton.Columns[1].Name = "PALLETID";
                dgvCarton.Columns[2].Name = "PN";
                dgvCarton.Columns[3].Name = "SNQTY";
                dgvCarton.Columns[4].Name = "TRANSFERDN";
            }


           
        }
        //private void rdoCarton_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdoPallet.Checked)
        //    {
        //        strInSnType = "PALLET";
        //    }
        //    else
        //    {
        //        strInSnType = "CARTON";
        //    }


        //}
        private void cmbFGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFGName.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }
            strProduct = cmbFGName.Text; ;
            strFGinMESWcf = cmbFGName.SelectedValue.ToString();
            //MessageBox.Show(strProduct+"|"strFGinMESWcf);
        }
       

        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            //可扫描序号/箱号
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

            //是否强制获取新的栈板信息
            //不勾选的 提示已经存在。
            string strIsForceInertPalletFlag = string.Empty;

            if (chkUpdatePallet.Checked)
            {
                strIsForceInertPalletFlag = "Y";
            }
            else
            {
                strIsForceInertPalletFlag = "N";
                //如果栈板号已经再表里面就直接为错误。
                DataTable dt = wb.GetMesPalletInfoLog(strCarton);
                if (!(dt == null)) 
                {
                    string strPalletRecordnum = dt.Rows[0]["recordnum"].ToString();
                    if (!strPalletRecordnum.Equals("0")) 
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        this.ShowMsg("栈板号已经存在记录", 0);
                        selecttxtCarton();
                        return;
                    }
                }
            }

            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            //获得序号
            string strResultGetSN = string.Empty;

            
            if (strProduct.Equals("WATCH"))
            {
                #region 备份获取记录 先只看watch
                string strMESFuncName = string.Empty;
                if (rdoCarton.Checked)
                { 
                    strMESFuncName = "GetMaterialTransferInfo"; 
                }
                else 
                {
                    strMESFuncName = "GetMESStockInfo";
                }

                strResultGetSN = wb.GetMesAPI(strProduct, strFGinMESWcf, strMESFuncName, strCarton);
                Boolean isIsertLogOK = true;
                string strRsgMsg0 = string.Empty;
                isIsertLogOK= wb.WMSIBackUpWebServieLog(strGUID, g_ServerIP, strFGinMESWcf, strCarton, strResultGetSN, g_sUserNo, strMESFuncName, out strRsgMsg0);
                if (!isIsertLogOK) 
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.ShowMsg(strRsgMsg0 , 0);
                    selecttxtCarton();
                    return;
                }
                #endregion
            }
            else if (strProduct.Equals("AIRPOD"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("暂时不支持" + strProduct + "此产品入库", 0);
                selecttxtCarton();
                return;
            }
            else
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("暂时不支持" + strProduct + "此产品入库", 0);
                selecttxtCarton();
                return;
            }
            

            if (string.IsNullOrEmpty(strResultGetSN)) 
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("获得序号为空", 0);
                selecttxtCarton();
                return;
            }

            if (rdoCarton.Checked) 
            {
                //原材料入库
                RAWINRETURNMODEL ResultModel = new RAWINRETURNMODEL();
                try
                {
                    ResultModel = JsonConvert.DeserializeObject<RAWINRETURNMODEL>(strResultGetSN);
                }
                catch (Exception e1)
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.ShowMsg("MES反馈信息格式异常:" + e1.ToString(), 0);
                    selecttxtCarton();
                    return;
                }


                //显示序号 +保存序号
                string strOutMsg = string.Empty;
                if (!ShowMesSnOnDGV(ResultModel, strGUID, strIsForceInertPalletFlag, out strOutMsg))
                {
                    ShowMsg(strOutMsg, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    selecttxtCarton();
                    return;
                }
            }
            else //栈板入库非原材
            { 
                FGINRETURNMODEL ResultModel = new FGINRETURNMODEL();
                try 
                { 
                    ResultModel = JsonConvert.DeserializeObject<FGINRETURNMODEL>(strResultGetSN); 
                }
                catch (Exception e1) 
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    this.ShowMsg("MES反馈信息格式异常:"+e1.ToString(), 0);
                    selecttxtCarton();
                    return;
                }
            
           
                //显示序号 +保存序号
                string strOutMsg = string.Empty;
                if (!ShowMesSnOnDGV(ResultModel, strGUID, strIsForceInertPalletFlag , out strOutMsg))
                {
                    ShowMsg(strOutMsg, 0);
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    selecttxtCarton();
                    return;
                }
            }
            LibHelper.MediasHelper.PlaySoundAsyncByOk();
            ShowMsg("OK", -1);
            selecttxtCarton();
            
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

                dgvCarton.DataSource = TeturnSNList;
                dgvCarton.AutoResizeColumns(
                   DataGridViewAutoSizeColumnsMode.AllCells);
                int i = 1;
                foreach (DataGridViewRow dr in dgvCarton.Rows)
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

            bool IsHold = false;

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

            dgvCarton.DataSource = null;
            dgvCarton.Rows.Clear();
            for (int i = 0; i < TeturnSNList.Count(); i++)
            {
                SNLIST sninfo = TeturnSNList[i];
                DataGridViewRow dr = new DataGridViewRow();
                foreach (DataGridViewColumn c in dgvCarton.Columns)
                {
                    dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                }

                if (string.IsNullOrEmpty(sninfo.PALLETID?.ToString()))
                {
                    outMsg = "MES反馈的资料清单中栈板号为空，异常";
                    return false;
                }
                
                //累加序号
                dr.HeaderCell.Value = (i + 1).ToString();
                dr.Cells[0].Value = sninfo.SN?.ToString();
                dr.Cells[1].Value = sninfo.WO?.ToString();
                dr.Cells[2].Value = sninfo.BATCHTYPE?.ToString();
                dr.Cells[3].Value = sninfo.LOAD_ID?.ToString();
                dr.Cells[4].Value = sninfo.BOXID?.ToString();
                dr.Cells[5].Value = sninfo.PALLETID?.ToString();
                string strBatchtype = sninfo.BATCHTYPE?.ToString();
                if (!string.IsNullOrEmpty(strBatchtype))
                {
                    dr.Cells[6].Value = sninfo.PN?.ToString() + "-" + strBatchtype;
                    
                }
                else
                {
                    dr.Cells[6].Value = sninfo.PN?.ToString();
                }
                //if (!string.IsNullOrEmpty(strBatchtype))
                //{
                //    if (strBatchtype.Equals("M04") || strBatchtype.Equals("S01"))
                //    {
                //        dr.Cells[6].Value = sninfo.PN?.ToString() + "-" + strBatchtype;
                //    }
                //    else 
                //    {
                //        dr.Cells[6].Value = sninfo.PN?.ToString();
                //    }
                //}
                //else
                //{
                //    dr.Cells[6].Value = sninfo.PN?.ToString();
                //}
                dr.Cells[7].Value = sninfo.MODEL?.ToString();
                dr.Cells[8].Value = sninfo.REGION?.ToString();
                dr.Cells[9].Value = sninfo.CUSTPN?.ToString();
                dr.Cells[10].Value = sninfo.QHOLDFLAG?.ToString();
                if (dr.Cells[10].Value.ToString().Equals("Y")) 
                {
                    IsHold = true;
                }
                dr.Cells[11].Value = sninfo.TROLLEYNO?.ToString();
                dr.Cells[12].Value = sninfo.TROLLEYLINENO?.ToString();
                dr.Cells[13].Value = sninfo.TROLLEYLINENOPOINT?.ToString();
                dr.Cells[14].Value = sninfo.DN?.ToString();
                dr.Cells[15].Value = sninfo.ITEMNO?.ToString();


                //20200610改为正常不显示;遇到异常异常才会显示，正常要查询才会显示
                //dgvCarton.Rows.Add(dr);
                //insert
                //SP内检查PPS是否序号重复， 如果OK，
                string strerrmsg = string.Empty;
                string strResult = wb.ExecuteFGIN(strGUID, sninfo, strProduct, strForceFlag, out strerrmsg);
                if (!strResult.Equals("OK"))
                {
                    outMsg = strerrmsg;
                    return false;
                }
               
            }
            //HYQ 20200619 增加逻辑; MES_PALLET_INFO只有执行到这里才能改成IN-->WP;
            string strerrmsg2 = string.Empty;
            if (!wb.GetMESPalletOKUpdateStatus(strGUID, out strerrmsg2))
            {
                outMsg = strerrmsg2;
                return false;
            };
            if (IsHold) 
            {
                btnSearchFunc();
            }
            outMsg = "";
            return true;
        }
        #endregion

        private bool ShowMesSnOnDGV(RAWINRETURNMODEL MesSnModel, string strGUID, string strForceFlag, out string outMsg)
        {

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
            MATERIALQTY[] TeturnSNList = MesSnModel.MATERIALQTY;


            dgvCarton.DataSource = null;
            dgvCarton.Rows.Clear();
            for (int i = 0; i < TeturnSNList.Count(); i++)
            {
                MATERIALQTY sninfo = TeturnSNList[i];
                DataGridViewRow dr = new DataGridViewRow();
                foreach (DataGridViewColumn c in dgvCarton.Columns)
                {
                    dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                }
                //dgvCarton.Columns[0].Name = "BOX_ID";
                //dgvCarton.Columns[1].Name = "PALLETID";
                //dgvCarton.Columns[2].Name = "PN";
                //dgvCarton.Columns[3].Name = "SNQTY";
                //累加序号
                if (string.IsNullOrEmpty(sninfo.PALLETID?.ToString()))
                {
                    outMsg = "MES反馈的资料清单中栈板号为空，异常";
                    return false;
                }
                dr.HeaderCell.Value = (i + 1).ToString();
                dr.Cells[0].Value = sninfo.BOXID;
                dr.Cells[1].Value = sninfo.PALLETID;
                dr.Cells[2].Value = sninfo.PN;
                dr.Cells[3].Value = sninfo.QTY;
                dr.Cells[4].Value = sninfo.TRANSFERDN;
                //20200610改为正常不显示;遇到异常异常才会显示，正常要查询才会显示
                //dgvCarton.Rows.Add(dr);
                //insert
                string strerrmsg = string.Empty;
                string strResult = wb.ExecuteRAWMIN(strGUID, sninfo, strProduct, strForceFlag, out strerrmsg);
                if (!strResult.Equals("OK"))
                {
                    
                    outMsg = strerrmsg;
                    return false;
                }
            }
            //HYQ 20200619 增加逻辑; MES_PALLET_INFO只有执行到这里才能改成IN-->WP;
            string strerrmsg2 = string.Empty;
            if (!wb.GetMESPalletOKUpdateStatus(strGUID, out strerrmsg2)) 
            {
                outMsg = strerrmsg2;
                return false;
            };

            outMsg = "";
            return true;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearchFunc();
        }
        private void btnSearchFunc() 
        {
            btnSearch.Enabled = false;

            string strCarton = txtCarton.Text.Trim();
            strCarton = wb.DelPrefixCartonSN(strCarton);
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的SN/Carton不能为空！", 0);
                btnSearch.Enabled = true;
                return;
            }
            dgvCarton.DataSource = null;
            dgvCarton.Rows.Clear();
            dgvCarton.Columns.Clear();
            dgvCarton.ColumnCount = 16;
            dgvCarton.ColumnHeadersVisible = true;
            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            //columnHeaderStyle.BackColor = Color.Beige;
            //columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            //dgvCarton.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            dgvCarton.Columns[0].Name = "SN";
            dgvCarton.Columns[1].Name = "WO";
            dgvCarton.Columns[2].Name = "BATCHTYPE";
            dgvCarton.Columns[3].Name = "LOADID";
            dgvCarton.Columns[4].Name = "BOX_ID";
            dgvCarton.Columns[5].Name = "PALLETID";
            dgvCarton.Columns[6].Name = "PN";
            dgvCarton.Columns[7].Name = "MODEL";
            dgvCarton.Columns[8].Name = "REGION";
            dgvCarton.Columns[9].Name = "CUSTPN";
            dgvCarton.Columns[10].Name = "QHOLDFLAG";
            dgvCarton.Columns[11].Name = "TROLLEYNO";
            dgvCarton.Columns[12].Name = "TROLLEYLINENO";
            dgvCarton.Columns[13].Name = "TROLLEYLINENOPOINT";
            dgvCarton.Columns[14].Name = "DN";
            dgvCarton.Columns[15].Name = "ITEMNO";
            lblHold.Visible = false;

            DataTable db = wb.GetPalletNODataTable(strCarton);
            if (db == null)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("查无结果", 0);
                btnSearch.Enabled = true;
                return;
            }
            if (db.Rows.Count > 0)
            {
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvCarton.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号

                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = db.Rows[i]["customer_sn"].ToString();
                    dr.Cells[1].Value = db.Rows[i]["work_order"].ToString();
                    dr.Cells[2].Value = db.Rows[i]["batchtype"].ToString();
                    dr.Cells[3].Value = db.Rows[i]["load_id"].ToString();
                    dr.Cells[4].Value = db.Rows[i]["carton_no"].ToString();
                    dr.Cells[5].Value = db.Rows[i]["pallet_no"].ToString();
                    dr.Cells[6].Value = db.Rows[i]["part_no"].ToString();
                    dr.Cells[7].Value = db.Rows[i]["model"].ToString();
                    dr.Cells[8].Value = db.Rows[i]["region"].ToString();
                    dr.Cells[9].Value = db.Rows[i]["mpn"].ToString();
                    dr.Cells[10].Value = db.Rows[i]["hold_flag"].ToString();
                    dr.Cells[11].Value = db.Rows[i]["trolley_no"].ToString();
                    dr.Cells[12].Value = db.Rows[i]["trolley_line_no"].ToString();
                    dr.Cells[13].Value = db.Rows[i]["point_no"].ToString();
                    dr.Cells[14].Value = db.Rows[i]["delivery_no"].ToString();
                    dr.Cells[15].Value = db.Rows[i]["line_item"].ToString();

                    try
                    {
                        dgvCarton.Invoke((MethodInvoker)delegate ()
                        {
                            dgvCarton.Rows.Add(dr);
                            if (dr.Cells[10].Value.ToString().Equals("Y") && chkHold.Checked)
                            {
                                this.dgvCarton.Rows[i].Cells[10].Style.BackColor = System.Drawing.Color.Yellow;
                                lblHold.Visible = true;
                            }

                        });
                    }
                    catch (Exception e1)
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        ShowMsg(e1.ToString(), 0);
                        btnSearch.Enabled = true;
                        return;

                    }
                }
            }
            btnSearch.Enabled = true;
        }
       
    }
}
