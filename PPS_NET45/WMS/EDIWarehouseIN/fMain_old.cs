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
    public partial class fMain_old : Form
    {
        public fMain_old()
        {
            InitializeComponent();
        }

        string strFGinMESWcf = string.Empty;
        string strWH = string.Empty;
        string strLocation = string.Empty;
        string strProduct = string.Empty;
        string strInSnType = string.Empty;

        EDIWarehouseINBLL wb = new EDIWarehouseINBLL();

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.WindowState = FormWindowState.Maximized;
            //填充仓库信息
            string strSql = string.Format(@"
                select a.warehouse_id id, a.warehouse_no name
                  from sajet.wms_warehouse a
                 where a.enabled = 'Y'
                   and a.warehouse_id in ('2019010200317')
                 order by a.warehouse_no
                ");

            fillCmb(strSql, "warehouse_No", cmbWH);

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

        private void cmbWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSAPWH.Text = "";
            if (cmbWH.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }

            strWH = cmbWH.SelectedValue.ToString();
            string strSql = string.Format(@"
                 select distinct a.location_id id, location_no name, region region
                             from sajet.wms_location a
                            where a.warehouse_id = '{0}'
                              --and a.location_no like 'E3FG%' 
                            order by a.location_no"
                         , cmbWH.SelectedValue);
            fillCmb(strSql, "location_name", cmbLocation);

            lblSAPWH.Text = wb.GetSAPWH(cmbWH.SelectedValue.ToString());
            

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
            if (rdoPallet.Checked) 
            { 
                txtCarton.Enabled = true;
                strInSnType = "PALLET"; 
            }
            else 
            { 
                txtCarton.Enabled = false; 
                strInSnType = "CARTON"; 
            }
           
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            lblRegion.Text = "";
            if (cmbLocation.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }
            //MessageBox.Show(cmbLocation.SelectedValue.ToString());
            strLocation = cmbLocation.SelectedValue.ToString();
            dgvLocation.DataSource = null;
            dgvLocation.DataSource = findData();
            lblRegion.Text = wb.GetLocationRegion(cmbLocation.SelectedValue.ToString());

            selecttxtCarton();
        }
        private void cmbFGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFGName.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
            { return; }
            strProduct = cmbFGName.Text; ;
            strFGinMESWcf = cmbFGName.SelectedValue.ToString();
            //MessageBox.Show(strProduct+"|"strFGinMESWcf);

        }
        private DataTable findData()
        {

            int iPara = 0;                              //变量项次
            string strSql;                              //SQL字符串变量
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            bool isInput = false;                       //是否有输入查询条件
            DataTable dt = new DataTable();              //按查询条件，查出数据源


            #region
            strSql = "select location_no  Location,pallet_no 栈板号,part_no 料号, sum(cartonqty)箱数, sum(qty) 数量, sum(qhcartonqty) QHold影响的箱数, sum(qhqty) QHold数量 " +
                              " from ppsuser.t_location  where  1=1 ";


            //组合输入查询条件，过滤数据源
            //仓库有输入值时，添加查询条件变量
            if (cmbWH.Text.Trim() != "")
            {
                isInput = true;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSql += " and warehouse_id = :warehouse";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "warehouse", cmbWH.SelectedValue };
                iPara = iPara + 1;
            }

            //储位有输入值时，添加查询条件变量
            if (cmbLocation.Text.Trim() != "")
            {
                isInput = true;
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                strSql += " and location_no = :location";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "location", cmbLocation.Text.Trim() };
                iPara = iPara + 1;
            }

            strSql += " group by location_no,pallet_no, part_no having sum(qty) > 0 and sum(cartonqty)>0 order by location_no,pallet_no, part_no";


            //判断是否有输入查询条件，调用不同的类型
            if (isInput)
            {
                dt = ClientUtils.ExecuteSQL(strSql, sqlparams).Tables[0];
            }
            else
            {
                dt = ClientUtils.ExecuteSQL(strSql).Tables[0];
            }
            #endregion


            return dt;
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
            bool NojumpSAP = true;
            if (strCarton.Contains("%%"))
            {
                NojumpSAP = false;
                strCarton = strCarton.Replace("%%", "");
            }
            string strGUID = System.Guid.NewGuid().ToString().ToUpper();
            //获得序号
            string strResultGetSN = GetMesSnInfo( strProduct, strFGinMESWcf,  strCarton);
            if (string.IsNullOrEmpty(strResultGetSN)) 
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("获得序号为空", 0);
                selecttxtCarton();
                return;
            }
            FGINRETURNMODEL ResultModel = new FGINRETURNMODEL();
            ResultModel = JsonConvert.DeserializeObject<FGINRETURNMODEL>(strResultGetSN);
            // 单多包转换

            //显示序号
            string strOutMsg = string.Empty;
            if (!ShowMesSnOnDGV(ResultModel, strGUID,out strOutMsg))
            {
                ShowMsg(strOutMsg, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                selecttxtCarton();
                return;
            }


            


            //序号的region和 储位region的region检查
            string strLocationRegion = lblRegion.Text;
            if (!CheckLocationMatchToPPS(strGUID, strLocationRegion, out strOutMsg))
            {
                ShowMsg(strOutMsg, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                selecttxtCarton();
                return;
            }


            //处理是否有新料号
            if (!InertNewPartNoToPPS(strGUID, out strOutMsg))
            {
                ShowMsg(strOutMsg, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                selecttxtCarton();
                return;
            }

            //如果是金刚车 进行金刚车的checkin



            //这份给SAP的需要用查询语句获取栈板的 栈板号GroupBy
            DataTable getdt = wb.GetSNSAPInfoDataTable(strGUID);
            ERPStockInModel ToSAPModel = new ERPStockInModel();
            ToSAPModel.SPCQN = strCarton;
            ToSAPModel.HSDAT = DateTime.Now.ToString("yyyyMMdd");

            List<ERPStockInItemModel> ToSAPItemModelList = new List<ERPStockInItemModel>();

            for (int i=0;i< getdt.Rows.Count;i++) 
            {
                ERPStockInItemModel ToSAPItemModel = new ERPStockInItemModel();

                ToSAPItemModel.AUFNR = getdt.Rows[i]["work_order"].ToString();
                ToSAPItemModel.MATNR= getdt.Rows[i]["part_no"].ToString();
                ToSAPItemModel.GAMNG = getdt.Rows[i]["sncount"].ToString();
                ToSAPItemModel.UNAME = "10086";
                ToSAPItemModel.LGORT = lblSAPWH.Text.Split('-')[0];

                //???到底是什么值
                ToSAPItemModel.CHARG= getdt.Rows[i]["work_order"].ToString();
                ToSAPItemModelList.Add(ToSAPItemModel);

            }
            ToSAPModel.ITEMS = ToSAPItemModelList.ToArray();

            string strin = JsonConvert.SerializeObject(ToSAPModel);

            //后面会加入SAP的入库过账检查
            #region //TEST
            //string strin = @"
            //            {
            //             'SPCQN': 'TEST',
            //             'HSDAT': 'TEST',
            //             'ITEMS': [
            //              {
            //               'QMDAT': 'TEST',
            //               'QMTIM': 'TEST',
            //               'AUFNR': 'TEST',
            //               'MATNR': 'TEST',
            //               'MEINS': 'TEST',
            //               'GAMNG': 'TEST',
            //               'UNAME': 'TEST',
            //               'LGORT': 'TEST',
            //               'ZSJRQ': 'TEST',
            //               'REMARK': 'TEST',
            //               'CHARG': 'TEST',
            //               'ZSSMB': 'TEST'
            //              }
            //             ]
            //            }";

            //strin = strin.Replace("\'", "\"");
            #endregion
            if (NojumpSAP) {
                string strResultSAP = wb.AfterEdiHttpPostWebService(@"http://10.54.10.15:93/OMSBgSAP/ERPStockIn", strin);
                var erpResM = JsonConvert.DeserializeObject<ERPResModel>(strResultSAP);
                if (!erpResM.IsSuccess)
                {
                    ShowMsg("过账失败  :" + erpResM.ZZMSG, 0);
                    return;
                }
            }
    
        
            //开始入库
            string strOuterrmsg = string.Empty;
            string strQty = ResultModel.SNLIST.Count().ToString();
            string strReulstFGIN = wb.ExecuteFGWMSTransIN(strGUID, strQty, out strOuterrmsg);
            if (!strReulstFGIN.Equals("OK"))
            {
                ShowMsg(strOuterrmsg, 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                selecttxtCarton();
                return;
            }
            //反馈MES 入库OK
            FeedbackMesStockResult( strProduct, strFGinMESWcf,  strCarton);

            LibHelper.MediasHelper.PlaySoundAsyncByOk();
            ShowMsg("OK", -1);
            dgvLocation.DataSource = null;
            dgvLocation.DataSource = findData();
            dgvCarton.DataSource = null;
            txtCarton.Text = "";
            dgvCarton.DataSource = wb.GetSNInfoDataTable(strCarton, strInSnType);
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
                        x.warehouse_id = strWH;
                        x.location_id = strLocation;
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
        private bool ShowMesSnOnDGV(FGINRETURNMODEL MesSnModel, string strGUID, out string outMsg)
        {

            //FGINRETURNMODEL MesSnModel = new FGINRETURNMODEL();
            //ResultModel = JsonConvert.DeserializeObject<FGINRETURNMODEL>(strMODEL);
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
                //累加序号
                dr.HeaderCell.Value = (i + 1).ToString();
                dr.Cells[0].Value = sninfo.SN;
                dr.Cells[1].Value = sninfo.WO;
                dr.Cells[2].Value = sninfo.BATCHTYPE;
                dr.Cells[3].Value = sninfo.LOAD_ID;
                dr.Cells[4].Value = sninfo.BOXID;
                dr.Cells[5].Value = sninfo.PALLETID;
                dr.Cells[6].Value = sninfo.PN +"-" + sninfo.BATCHTYPE;
                dr.Cells[7].Value = sninfo.MODEL;
                dr.Cells[8].Value = sninfo.REGION;
                dr.Cells[9].Value = sninfo.CUSTPN;
                dr.Cells[10].Value = sninfo.QHOLDFLAG;
                dr.Cells[11].Value = sninfo.TROLLEYNO;
                dr.Cells[12].Value = sninfo.TROLLEYLINENO;
                dr.Cells[13].Value = sninfo.TROLLEYLINENOPOINT;
                dr.Cells[14].Value = sninfo.DN;
                dr.Cells[15].Value = sninfo.ITEMNO;


                //检查料号如果PPS没有则通过接口把PPS的sys_part 补一份，再入库  料号和料号ID
                try
                {
                    dgvCarton.Invoke((MethodInvoker)delegate ()
                    {
                        dgvCarton.Rows.Add(dr);
                        //insert
                        //SP内检查PPS是否序号重复， 如果OK，
                        string strerrmsg = string.Empty;
                        string strResult = wb.ExecuteFGIN(strGUID, sninfo, strWH, strLocation, strProduct, out strerrmsg);
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            string strLocation = cmbLocation.Text;
            if (string.IsNullOrEmpty(strLocation))
            {
                ShowMsg("不得输入空白储位", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                return;
            }
            if (string.IsNullOrEmpty(strProduct))
            {
                ShowMsg("请选择合适产品", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                return;
            }

            
            for (int i = 0; i < cmbLocation.Items.Count; i++)
            {

                if (strLocation == cmbLocation.GetItemText(cmbLocation.Items[i]))
                {
                    cmbLocation.SelectedIndex = i;
                    cmbWH.Enabled = false;
                    cmbLocation.Enabled = false;
                    txtCarton.Enabled = true;
                    btnStart.Enabled = false;
                    btnEnd.Enabled = true;
                    selecttxtCarton();
                    return;
                }

                if ((i == cmbLocation.Items.Count - 1) && !cmbLocation.GetItemText(cmbLocation.Items[i]).Equals(strLocation))
                {
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    cmbLocation.SelectAll();
                    cmbLocation.Focus();
                    ShowMsg("输入的储位无效", 0);
                    return;
                }

            }

        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            TextMsg.Text = "";
            TextMsg.BackColor = Color.Blue;
            rePrintLabel pr = new rePrintLabel();
            pr.ShowDialog();
            //fCheck fcheck = new fCheck();
            //if (fcheck.ShowDialog() != DialogResult.OK)
            //{
            //    ShowMsg("账号权限不正确", 0);
            //    return;
            //}
            //else
            //{
            //    rePrintLabel pr = new rePrintLabel();
            //    pr.ShowDialog();
            //}
        }


        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (dgvCarton.RowCount > 0)
            {
                string strSN = dgvCarton.Rows[0].Cells["BOXID"].Value?.ToString();
                WHPalletLabel wp = new WHPalletLabel();
                if (wp.PrintWHPalletLabel(strSN))
                {
                    TextMsg.Text = "打印OK";
                }
                else
                {
                    TextMsg.Text = "打印FAIL";
                }

            }
            cmbWH.Enabled = true;
            cmbLocation.Enabled = true;
            txtCarton.Enabled = false;
            btnStart.Enabled = true;
            btnEnd.Enabled = false;
        }

        private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strLocation = cmbLocation.Text;
                if (string.IsNullOrEmpty(strLocation))
                {
                    ShowMsg("不得输入空白储位", 0);
                    return;
                }
                //foreach (System.Data.DataRowView dr in cmbLocation.Items)
                //{
                //    //string id = dr["id"].ToString();
                //    if (strLocation == dr["name"].ToString())
                //    {
                //        MessageBox.Show(dr["id"].ToString());
                //    }
                //}

                for (int i = 0; i < cmbLocation.Items.Count; i++)
                {

                    if (strLocation == cmbLocation.GetItemText(cmbLocation.Items[i]))
                    {
                        cmbLocation.SelectedIndex = i;
                        //MessageBox.Show(cmbLocation.SelectedValue.ToString());
                        return;
                    }

                    if ((i == cmbLocation.Items.Count - 1) && !cmbLocation.GetItemText(cmbLocation.Items[i]).Equals(strLocation))
                    {
                        LibHelper.MediasHelper.PlaySoundAsyncByNg();
                        cmbLocation.SelectAll();
                        cmbLocation.Focus();
                        ShowMsg("输入的储位无效", 0);
                    }

                }

                //for (int i = 0; i < comboBox1.Items.Count; i++)
                //{
                //    comboBox1.SelectedIndex = i;
                //    string value = comboBox1.SelectedValue.ToString();
                //}

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

        

       
    }
}
