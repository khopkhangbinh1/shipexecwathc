using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientUtilsDll.Forms
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }


        private void frmmenu_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Normal;
            base.Activate();
        }

        public virtual void SuccessMSG(string successMSG)
        {
            this.prgMSG.ForeColor = Color.Blue;
            this.prgMSG.Text = successMSG.TL();
        }

        public virtual void SuccessMSG(string msgCode, string successMSG)
        {
            string msg ="";
            if (string.IsNullOrEmpty(msgCode))
            {
                msg = successMSG;
            }
            this.SuccessMSG(msgCode + ": " + msg.TL());
        }

        public virtual void ErrorMSG(string errorMSG)
        {
            this.prgMSG.ForeColor = Color.Red;
            this.prgMSG.Text = errorMSG.TL();
        }

        public virtual void ErrorMSG(string msgCode, string errorMSG)
        {
            string msg = "";
            if (string.IsNullOrEmpty(msgCode))
            {
                msg = errorMSG;
            }
            this.ErrorMSG(msgCode + ": " + msg.TL());
        }

        public virtual void WarningMSG(string warningMSG)
        {
            this.prgMSG.ForeColor = Color.Yellow;
            this.prgMSG.Text = warningMSG.TL();
        }

        public virtual void WarningMSG(string msgCode, string warningMSG)
        {
            string msg = "";
            if (string.IsNullOrEmpty(msgCode))
            {
                msg = warningMSG;
            }
            this.WarningMSG(msgCode + ": " + msg.TL());
        }
    }
}
