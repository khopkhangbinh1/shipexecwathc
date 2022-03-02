using ClientUtilsDll.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Packingparcel
{
    public partial class Printer : PPSForm
    {
        public Printer()
        {
            InitializeComponent();
        }

        private void Printer_Load(object sender, EventArgs e)
        {
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;//获取默认的打印机名

            foreach (string ss in PrinterSettings.InstalledPrinters)
            {
                ///在列表框中列出所有的打印机,
                this.cbPrinter.Items.Add(ss);
                if (ss == strDefaultPrinter)//把默认打印机设为缺省值
                {
                    cbPrinter.SelectedIndex = cbPrinter.Items.IndexOf(ss);
                }
                SuccessMSG("Chose Printer first pls.");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            fMain.Printer = cbPrinter.Text;
            this.Close();
        }
    }
}
