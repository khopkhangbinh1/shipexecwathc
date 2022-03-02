using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDIWarehouseTools
{
    class EDIWarehouseToolsBLL
    {
        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.Length == 13 && insn.StartsWith("S"))
            { insn = insn.Substring(1); }

            return insn;

        }
        public void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            cmb.Items.Clear();
            if (dts.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";
            }
        }

        public void ShowStockInfo(string carlineno, DataGridView dtStock)
        {
            if (string.IsNullOrEmpty(carlineno)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataTable dt = wd.GetStockInfoDataTable(carlineno).Tables[0];
            dtStock.DataSource = dt;
            //if (dataSet.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dataSet.Rows.Count; i++)
            //    {
            //        //创建行
            //        DataGridViewRow dr = new DataGridViewRow();
            //        foreach (DataGridViewColumn c in dtStock.Columns)
            //        {
            //            dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
            //        }
            //        //累加序号
            //        dr.HeaderCell.Value = (i + 1).ToString();
            //        dr.Cells[0].Value = dataSet.Rows[i]["location_no"].ToString();
            //        dr.Cells[1].Value = dataSet.Rows[i]["custom_sn"].ToString();
            //        dr.Cells[2].Value = dataSet.Rows[i]["trolley_line_no"].ToString();
            //        dr.Cells[3].Value = dataSet.Rows[i]["pointno"].ToString();
            //        dr.Cells[4].Value = dataSet.Rows[i]["group_code"].ToString();
            //        dr.Cells[5].Value = dataSet.Rows[i]["delivery_no"].ToString();
            //        dr.Cells[6].Value = dataSet.Rows[i]["line_item"].ToString();
            //        dr.Cells[7].Value = dataSet.Rows[i]["shipment_id"].ToString();
            //        try
            //        {
            //            dtStock.Invoke((MethodInvoker)delegate ()
            //            {
            //                dtStock.Rows.Add(dr);
            //            });
            //        }
            //        catch (Exception)
            //        {
            //            return;
            //        }
            //    }
            //}

        }
        public void ShowStockInfo2(string strlocationno, DataGridView dtStock)
        {
            if (string.IsNullOrEmpty(strlocationno)) { return; }
            dtStock.DataSource = null;
            dtStock.Rows.Clear();
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataTable dt = wd.GetStockInfoDataTable2(strlocationno).Tables[0];
            dtStock.DataSource = dt;
        }

        public string WmstCheckTrolleyInfo(string strTrolleyNo, out string RetMsg)
        {

            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            string strResult = wd.WmstCheckTrolleyInfoBySP( strTrolleyNo, out  RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string WmstPalletToCarTrans(string strCartonNo, string strCarLineNo, out string RetMsg)
        {

            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            string strResult = wd.WmstPalletToCarTransBySP( strCartonNo,  strCarLineNo, out  RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
        public string GetCarlinenoByCSN(string incsn, out string carlineno, out string errmsg)
        {

            errmsg = string.Empty;
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            string strRB = wd.GetCarlinenoByCSNBySP(incsn, out carlineno, out errmsg);
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }
        }

        public string WmstCarToPalletTrans(string strCartonNo, string strLocationNoTo, out string RetMsg)
        {

            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            string strResult = wd.WmstCarToPalletTransBySP(strCartonNo, strLocationNoTo, out RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public DataTable GetPalletListDataTable(string strStartTime, string strEndTime)
        {
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataSet dataSet = wd.GetPalletListBySQL(strStartTime, strEndTime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        
        public DataTable GetPalletDockLoaction(string strPalletNO)
        {
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataSet dataSet = wd.GetPalletDockLoactionBySQL(strPalletNO);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetDockLoactionListDataTable()
        {
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataSet dataSet = wd.GetDockLoactionListBySQL();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public DataTable GetDockLoactionInfo(string strLocationNo)
        {
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataSet dataSet = wd.GetDockLoactionInfoBySQL(strLocationNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetDockLoactionInfo2(string strLocationNo,string  strPalletNo)
        {
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataSet dataSet = wd.GetDockLoactionInfoBySQL2(strLocationNo, strPalletNo);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public DataTable GetAssignLocationListDataTable(string strCARNO, string strSID)
        {
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataSet dataSet = wd.GetAssignLocationListBySQL( strCARNO,  strSID);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetAssignLocationListDataTable2()
        {
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataSet dataSet = wd.GetAssignLocationListBySQL2();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetAssignLocationListDataTable3()
        {
            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            DataSet dataSet = wd.GetAssignLocationListBySQL3();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public string UpdateDockLocationPallet(string strLocation, string strPalletNO, string strNewPalletNO, out string RetMsg)
        {

            EDIWarehouseToolsDAL wd = new EDIWarehouseToolsDAL();
            string strResult = wd.UpdateDockLocationPalletBySP( strLocation,  strPalletNO,  strNewPalletNO, out  RetMsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

    }
}
