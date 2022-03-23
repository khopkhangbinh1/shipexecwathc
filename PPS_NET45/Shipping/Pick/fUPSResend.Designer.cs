namespace PickList
{
    partial class fUPSResend
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.prgTitle = new System.Windows.Forms.TextBox();
            this.prgMSG = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBox1 = new System.Windows.Forms.TextBox();
            this.prgMain = new System.Windows.Forms.Panel();
            this.prgMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgTitle
            // 
            this.prgTitle.BackColor = System.Drawing.SystemColors.HotTrack;
            this.prgTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prgTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.prgTitle.Font = new System.Drawing.Font("SimSun", 29F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prgTitle.ForeColor = System.Drawing.SystemColors.Info;
            this.prgTitle.Location = new System.Drawing.Point(0, 0);
            this.prgTitle.Margin = new System.Windows.Forms.Padding(2);
            this.prgTitle.Multiline = true;
            this.prgTitle.Name = "prgTitle";
            this.prgTitle.ReadOnly = true;
            this.prgTitle.Size = new System.Drawing.Size(478, 40);
            this.prgTitle.TabIndex = 71;
            this.prgTitle.Text = "UPS ShipExec Resend";
            this.prgTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // prgMSG
            // 
            this.prgMSG.AutoEllipsis = true;
            this.prgMSG.BackColor = System.Drawing.SystemColors.Control;
            this.prgMSG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.prgMSG.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.prgMSG.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.prgMSG.ForeColor = System.Drawing.Color.Blue;
            this.prgMSG.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.prgMSG.Location = new System.Drawing.Point(0, 231);
            this.prgMSG.Name = "prgMSG";
            this.prgMSG.Size = new System.Drawing.Size(478, 51);
            this.prgMSG.TabIndex = 73;
            this.prgMSG.Text = "Message";
            this.prgMSG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(107, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(242, 16);
            this.label2.TabIndex = 74;
            this.label2.Text = "CSN/CARTONNO/PICKPALLETNO:";
            // 
            // txtBox1
            // 
            this.txtBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBox1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold);
            this.txtBox1.Location = new System.Drawing.Point(110, 66);
            this.txtBox1.Name = "txtBox1";
            this.txtBox1.Size = new System.Drawing.Size(238, 26);
            this.txtBox1.TabIndex = 75;
            this.txtBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox1_KeyDown);
            // 
            // prgMain
            // 
            this.prgMain.Controls.Add(this.label2);
            this.prgMain.Controls.Add(this.txtBox1);
            this.prgMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.prgMain.Location = new System.Drawing.Point(0, 40);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new System.Drawing.Size(478, 188);
            this.prgMain.TabIndex = 114;
            // 
            // fUPSResend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 282);
            this.Controls.Add(this.prgMain);
            this.Controls.Add(this.prgMSG);
            this.Controls.Add(this.prgTitle);
            this.Name = "fUPSResend";
            this.Text = "fUPSResend";
            this.prgMain.ResumeLayout(false);
            this.prgMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox prgTitle;
        private System.Windows.Forms.Label prgMSG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBox1;
        private System.Windows.Forms.Panel prgMain;
    }
}