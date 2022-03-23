using ClientUtilsDll;
using ClientUtilsDll.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PickList
{
    public partial class fUPSResend : Form
    {
        public fUPSResend()
        {
            InitializeComponent();
        }

        private void txtBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                DateTime dateTime = DateTime.Now;
                string sn = txtBox1.Text.Trim();
                ResendUPS(sn);
            }
        }
        private async void ResendUPS(string sn)
        {
            bool actionCallFlag = false;

            DisplayMsg("正在处理，请稍等", -1);
            #region resend pallet
            PickListBll pb1 = new PickListBll();
            sn = pb1.DelPrefixCartonSN(sn);
            string msg = "";
            Action<string, string> ShipExecShow = delegate (string carton, string mesg)
            {
                actionCallFlag = true;
                if (mesg != "OK")
                {
                    DisplayMsg(mesg, 0);
                }
                else
                {
                    DisplayMsg("OK");
                }
            };
            var dt = pb1.GetCheckCartonFlag(sn);
            string shipment = dt?.Rows[0]["shipment_id"].ToString();
            string palletNo = dt?.Rows[0]["pick_pallet_no"].ToString().Substring(2);
            if (dt == null || String.IsNullOrWhiteSpace(shipment))
            {
                DisplayMsg("SN/Pallet不存在或还没做pick", 0);
                return;
            }

            var carrier = ClientUtils.ExecuteSQL(string.Format("SELECT CARRIER_CODE FROM PPSUSER.T_SHIPMENT_INFO WHERE SHIPMENT_ID='{0}'",
                                shipment)).Tables[0].Rows[0]["CARRIER_CODE"].ToString().ToUpper();

            if (!carrier.Contains("UPS"))
            {
                DisplayMsg("Shipment不是UPS", 0);
                return;
            }
            bool cartonFlag = dt.Rows[0]["CartonFlag"].ToString() == "1";

            if (cartonFlag)
            {
                await pb1.CallShipExec(sn, shipment, ShipExecShow);
            }
            else
            {
                if (pb1.IsFinishShipExec(sn, out msg))
                {
                    DisplayMsg(msg, 0);
                    return;
                }
                var pickpalletFinish = String.IsNullOrEmpty(ClientUtils.ExecuteSQL(string.Format(@"SELECT pallet_no, computer_name FINISH_FLAG  from PPSUSER.T_SHIPMENT_PALLET_PART 
                                                    where pallet_no='{0}' and rownum=1", palletNo)).Tables[0].Rows[0]["FINISH_FLAG"].ToString());
                if (!pickpalletFinish)
                {
                    DisplayMsg("Pickpallet还没结束，请先结束栈板", 0);
                    return;
                }

                await pb1.ExecuteFinishShipExec(sn, shipment, ShipExecShow);
            }
            if (!actionCallFlag)
                DisplayMsg("成功");
            #endregion
        }
        private void DisplayMsg(string strTxt, int strType = -1)
        {
            //ShowMSG(strTxt, strType);
            prgMSG.Text = strTxt.TP();
            switch (strType)
            {
                case 0: //Error    
                    prgMSG.ForeColor = Color.Red;
                    prgMSG.BackColor = Color.Blue;
                    break;
                case 1: //Warning                        
                    prgMSG.ForeColor = Color.Blue;
                    prgMSG.BackColor = Color.FromArgb(255, 255, 128);
                    break;
                default:
                    prgMSG.ForeColor = Color.White;
                    prgMSG.BackColor = Color.Blue;
                    break;
            }
        }
    }
}
