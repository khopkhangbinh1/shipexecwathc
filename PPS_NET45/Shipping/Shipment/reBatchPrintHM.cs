using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shipment
{
    public partial class reBatchPrintHM : Form
    {
        public reBatchPrintHM()
        {
            InitializeComponent();
        }

       

       
        private string changeSNtoShipmentID(string changeSNtoShipmentID,out string errmsg)
        {

            string sql = string.Format(@"Select distinct a.shipment_id 
                                        from ppsuser.t_shipment_pallet a  
                                        join ppsuser.t_shipment_info b 
                                         on a.shipment_id =b.shipment_id
                                        where b.status in('LF','UF')
                                    and  (a.pallet_no='{0}' or a.real_pallet_no='{1}') "
                                   , changeSNtoShipmentID, changeSNtoShipmentID);

            DataTable dt_change = new DataTable();
            try
            {
                dt_change = ClientUtils.ExecuteSQL(sql).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                errmsg = "NG";
                return "";
            }


            if (dt_change.Rows.Count > 0)
            {
                //如果输入的时real_pallet_no 或者时print_pallet_no 
                //转换位pallet_no 来处理
                changeSNtoShipmentID = dt_change.Rows[0]["shipment_id"].ToString();
                errmsg = "";
                return changeSNtoShipmentID;
            }
            else
            {
                errmsg = "NG";
                return "";
            }

        }

        private bool IsCarALLOver(string strShipment)
        {
            DataTable dtTemp = ClientUtils.ExecuteSQL(string.Format(@"
                SELECT count(*) as PACOUNT
FROM pptest.oms_load_car a
WHERE(a.car_no, TRUNC(a.expectedtime)) IN
(
SELECT DISTINCT b.car_no, TRUNC(c.SHIPPING_TIME)
FROM pptest.oms_load_car b INNER JOIN ppsuser.T_SHIPMENT_INFO c ON b.SHIPMENT_ID = c.SHIPMENT_ID
INNER JOIN ppsuser.T_SHIPMENT_PALLET c ON b.SHIPMENT_ID = c.SHIPMENT_ID
WHERE c.SHIPMENT_ID = '{0}' OR c.PALLET_NO = '{0}' OR c.REAL_PALLET_NO = '{0}'
)
AND nvl(a.isload, 0) <> 1
                ", strShipment)).Tables[0];
            if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
            {
                return false;
            }
            if (dtTemp.Rows[0]["PACOUNT"].ToString().Trim() == "0")
            {
                return true;
            }
            return false;
        }

        public DialogResult ShowMsg(string sText, int iType)
        {
            TextMsg.Text = sText;
            switch (iType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Yellow;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Green;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
        }
        public void getTruckNo(string starttime, string endtime)
        {
            string sql = string.Empty;
            #region 20200401new
            sql = string.Format(@"
                    select distinct t.car_no
                        from (select distinct case
                                                when b.car_no = '' then
                                                'notrack'
                                                when b.car_no is null then
                                                'notrack'
                                                else
                                                b.car_no
                                            end car_no,
                                            to_char(a.shipping_time, 'YYYYMMDD') strcdt
                                from ppsuser.t_shipment_info a
                                join pptest.oms_load_car b
                                  on a.shipment_id = b.shipment_id
                                where (to_date(a.shipping_time) >=
                                    to_date(:startdate, 'YYYY-MM-DD') and
                                    to_date(a.shipping_time) <= to_date(:enddate, 'YYYY-MM-DD'))
                                order by strcdt asc) t");
                #endregion
            

            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "startDate", starttime };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endDate", endtime };

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
            }
            catch (Exception e)
            {
                ShowMsg("连接数据错误" + e, 1);
                return;
            }
            cmbCarNo.Items.Clear();
            cmbCarNo.Text = "";
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    cmbCarNo.Items.Add(dataSet.Tables[0].Rows[i]["CAR_NO"].ToString());
                }
                return;
            }
            else
            {
                cmbCarNo.Items.Add("");
                return;
            }

        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            getTruckNo(dtpStartTime.Text, dtpStartTime.Text);
        }

        private void cmbCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            ShipmentBll sb = new ShipmentBll();
            dgvCarSIDList.DataSource = null;
            dgvCarSIDList.Rows.Clear();
            dgvCarSIDList.DataSource = sb.GetDateCarSID(dtpStartTime.Text, cmbCarNo.Text);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //ShowMsg(dgvCarSIDList.Rows.Count.ToString() , 0);
            ShowMsg("", -1);
            ShipmentBll sb = new ShipmentBll();
            dgvCarSIDList.DataSource = null;
            dgvCarSIDList.Rows.Clear();
            dgvCarSIDList.DataSource = sb.GetDateCarSID(dtpStartTime.Text, cmbCarNo.Text);
            if (dgvCarSIDList.Rows.Count>0) 
            {
                for (int i =0;i< dgvCarSIDList.Rows.Count-1;i++) 
                { 
                    string strSIDStatus = dgvCarSIDList.Rows[i].Cells["status"].Value.ToString();
                    if (!( strSIDStatus.Equals("FP") || strSIDStatus.Equals("LF") || strSIDStatus.Equals("UF")) )
                    {
                        ShowMsg("存在集货单异常", 0);
                        return;
                    }
                }

                for (int j = 0; j < dgvCarSIDList.Rows.Count - 1; j++)
                {
                    string strSID = dgvCarSIDList.Rows[j].Cells["shipment_id"].Value.ToString();
                    string strRegion = dgvCarSIDList.Rows[j].Cells["region"].Value.ToString();
                    CRReport.CRMain cr = new CRReport.CRMain();
                    if (strRegion.Equals("PAC"))
                    {
                        cr.HanDoveMan(strSID, true, true);
                    }
                    else
                    {
                        cr.HanDoveMan2(strSID, true, true, "WEIGHT");
                    }
                }
            }




            
        }
    }
}
