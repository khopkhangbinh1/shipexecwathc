using SajetClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDIWarehouseTools
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnCarToPallet_Click(object sender, EventArgs e)
        {
            fWMSCarToPallet a = new fWMSCarToPallet();
            a.Show();
        }

        private void btnPalletToCar_Click(object sender, EventArgs e)
        {
            fWMSPalletToCar a = new fWMSPalletToCar();
            a.Show();
        }

        private void btnDockLocationAssign_Click(object sender, EventArgs e)
        {
            fWMSDockLocationAssign a = new fWMSDockLocationAssign();
            a.Show();
        }
    }
}
