using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShipmentAC
{
    public partial class rePrintReport : Form
    {
        public rePrintReport()
        {
            InitializeComponent();
        }
        ShipmentBll sb = new ShipmentBll();
        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            string StrPalletNo = txtBox.Text.Trim();
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (string.IsNullOrEmpty(StrPalletNo))
            {
                TextMsg.Text = "";
                TextMsg.BackColor = Color.Blue;
                return;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TextMsg.BackColor = Color.Blue;
                TextMsg.Text = "";


                string strPackPalletNo = txtBox.Text.Trim();
                string errormsg = string.Empty;
                string strShipmentID = sb.changeSNtoShipmentID(strPackPalletNo, out errormsg);
                if (errormsg.Contains("NG"))
                {
                    ShowMsg("刷入序号查询资料异常", 1);
                    return;
                }

                if (!sb.IsCarALLOver(strShipmentID))
                {
                    ShowMsg("刷入序号所在车未装车完毕,请检查!", 1);
                    return;
                }

                //HYQ： 打印水晶报表。
                ShipmentDal da = new ShipmentDal();
                //int remainNum = da.GetShipmentRemainNum(strsShipmentID);
                int remainNum = 0;
                if (remainNum == 0)
                {
                    //HYQ: 这里可以打印水晶报表了 handoverManifest

                    try
                    {
                      
                        CRReport.CRMain cr = new CRReport.CRMain();

                        cr.ACHanDoveMan2(strShipmentID, strPackPalletNo, true, false, "", "", "");
                        
                    }
                    catch(Exception ex)
                    {
                        ShowMsg(ex.Message, 1);
                        return;
                    }

                    txtBox.Text = "";
                    txtBox.SelectAll();
                    txtBox.Focus();

                    TextMsg.Text = "打印OK";
                    TextMsg.BackColor = Color.Blue;


                }
                else
                {
                    ShowMsg("shipemntid存在没有装车完的栈板", 1);
                }

            }


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
    }
}
