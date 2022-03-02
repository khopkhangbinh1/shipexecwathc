namespace EDIWareHouseOUT
{
    partial class fWmsoCheck
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
            this.TextMsg = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSAPNO = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.dgvSAP = new System.Windows.Forms.DataGridView();
            this.grpPallet = new System.Windows.Forms.GroupBox();
            this.dgvCheck = new System.Windows.Forms.DataGridView();
            this.CARTON_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAP_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PICK_SAP_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCarton2 = new System.Windows.Forms.TextBox();
            this.lblQTY = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCartonStart = new System.Windows.Forms.TextBox();
            this.cmbPickSAPid = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSAP)).BeginInit();
            this.grpPallet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheck)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 509);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1213, 43);
            this.TextMsg.TabIndex = 63;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Location = new System.Drawing.Point(838, 6);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(94, 30);
            this.btnEnd.TabIndex = 131;
            this.btnEnd.Text = "结束";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Visible = false;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(719, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 31);
            this.btnStart.TabIndex = 130;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(81, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 124;
            this.label4.Text = "SAP单号:";
            // 
            // txtSAPNO
            // 
            this.txtSAPNO.BackColor = System.Drawing.SystemColors.Control;
            this.txtSAPNO.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSAPNO.ForeColor = System.Drawing.Color.Green;
            this.txtSAPNO.Location = new System.Drawing.Point(151, 69);
            this.txtSAPNO.Margin = new System.Windows.Forms.Padding(4);
            this.txtSAPNO.Name = "txtSAPNO";
            this.txtSAPNO.Size = new System.Drawing.Size(244, 29);
            this.txtSAPNO.TabIndex = 125;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(441, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 122;
            this.label3.Text = "CSN/箱号:";
            // 
            // txtCarton
            // 
            this.txtCarton.BackColor = System.Drawing.Color.White;
            this.txtCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton.Enabled = false;
            this.txtCarton.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(518, 110);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(256, 32);
            this.txtCarton.TabIndex = 2;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // dgvSAP
            // 
            this.dgvSAP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSAP.BackgroundColor = System.Drawing.Color.White;
            this.dgvSAP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSAP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSAP.Location = new System.Drawing.Point(2, 19);
            this.dgvSAP.Name = "dgvSAP";
            this.dgvSAP.RowHeadersWidth = 55;
            this.dgvSAP.RowTemplate.Height = 23;
            this.dgvSAP.Size = new System.Drawing.Size(476, 283);
            this.dgvSAP.TabIndex = 0;
            this.dgvSAP.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvSAP_RowsAdded);
            // 
            // grpPallet
            // 
            this.grpPallet.BackColor = System.Drawing.Color.LightSteelBlue;
            this.grpPallet.Controls.Add(this.dgvSAP);
            this.grpPallet.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpPallet.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPallet.Location = new System.Drawing.Point(0, 205);
            this.grpPallet.Margin = new System.Windows.Forms.Padding(2);
            this.grpPallet.Name = "grpPallet";
            this.grpPallet.Padding = new System.Windows.Forms.Padding(2);
            this.grpPallet.Size = new System.Drawing.Size(480, 304);
            this.grpPallet.TabIndex = 77;
            this.grpPallet.TabStop = false;
            this.grpPallet.Text = "SAP单-SN信息";
            // 
            // dgvCheck
            // 
            this.dgvCheck.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCheck.BackgroundColor = System.Drawing.Color.White;
            this.dgvCheck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheck.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CARTON_NO,
            this.SAP_NO,
            this.PICK_SAP_NO});
            this.dgvCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCheck.Location = new System.Drawing.Point(2, 19);
            this.dgvCheck.Name = "dgvCheck";
            this.dgvCheck.RowHeadersWidth = 55;
            this.dgvCheck.RowTemplate.Height = 23;
            this.dgvCheck.Size = new System.Drawing.Size(729, 283);
            this.dgvCheck.TabIndex = 0;
            this.dgvCheck.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvCheck_RowsAdded);
            // 
            // CARTON_NO
            // 
            this.CARTON_NO.HeaderText = "CARTON_NO";
            this.CARTON_NO.Name = "CARTON_NO";
            this.CARTON_NO.Width = 104;
            // 
            // SAP_NO
            // 
            this.SAP_NO.HeaderText = "SAP_NO";
            this.SAP_NO.Name = "SAP_NO";
            this.SAP_NO.Width = 80;
            // 
            // PICK_SAP_NO
            // 
            this.PICK_SAP_NO.HeaderText = "PICK_SAP_NO";
            this.PICK_SAP_NO.Name = "PICK_SAP_NO";
            this.PICK_SAP_NO.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.dgvCheck);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(480, 205);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(733, 304);
            this.groupBox1.TabIndex = 78;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CHECK-SN信息";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.grpPallet);
            this.panel1.Controls.Add(this.btnEnd);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1213, 552);
            this.panel1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtCarton2);
            this.groupBox3.Controls.Add(this.lblQTY);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtCartonStart);
            this.groupBox3.Controls.Add(this.cmbPickSAPid);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtSAPNO);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtCarton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 41);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1213, 164);
            this.groupBox3.TabIndex = 76;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "刷入序号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(799, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 14);
            this.label6.TabIndex = 138;
            this.label6.Text = "补列印Check栈板号:";
            // 
            // txtCarton2
            // 
            this.txtCarton2.BackColor = System.Drawing.Color.White;
            this.txtCarton2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton2.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton2.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton2.Location = new System.Drawing.Point(951, 113);
            this.txtCarton2.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarton2.Name = "txtCarton2";
            this.txtCarton2.Size = new System.Drawing.Size(256, 32);
            this.txtCarton2.TabIndex = 137;
            this.txtCarton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton2_KeyDown);
            // 
            // lblQTY
            // 
            this.lblQTY.AutoSize = true;
            this.lblQTY.Font = new System.Drawing.Font("NSimSun", 25F, System.Drawing.FontStyle.Bold);
            this.lblQTY.ForeColor = System.Drawing.Color.Blue;
            this.lblQTY.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQTY.Location = new System.Drawing.Point(598, 58);
            this.lblQTY.Name = "lblQTY";
            this.lblQTY.Size = new System.Drawing.Size(69, 34);
            this.lblQTY.TabIndex = 136;
            this.lblQTY.Text = "0/0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(74, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 135;
            this.label1.Text = "CSN/箱号:";
            // 
            // txtCartonStart
            // 
            this.txtCartonStart.BackColor = System.Drawing.Color.White;
            this.txtCartonStart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCartonStart.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCartonStart.ForeColor = System.Drawing.Color.Blue;
            this.txtCartonStart.Location = new System.Drawing.Point(151, 23);
            this.txtCartonStart.Margin = new System.Windows.Forms.Padding(4);
            this.txtCartonStart.Name = "txtCartonStart";
            this.txtCartonStart.Size = new System.Drawing.Size(244, 32);
            this.txtCartonStart.TabIndex = 1;
            this.txtCartonStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCartonStart_KeyDown);
            // 
            // cmbPickSAPid
            // 
            this.cmbPickSAPid.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPickSAPid.FormattingEnabled = true;
            this.cmbPickSAPid.Location = new System.Drawing.Point(151, 113);
            this.cmbPickSAPid.Margin = new System.Windows.Forms.Padding(1);
            this.cmbPickSAPid.Name = "cmbPickSAPid";
            this.cmbPickSAPid.Size = new System.Drawing.Size(244, 29);
            this.cmbPickSAPid.TabIndex = 132;
            this.cmbPickSAPid.Text = "ALL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(46, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 128;
            this.label2.Text = "PICK_SAP单号:";
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1213, 41);
            this.labShow.TabIndex = 64;
            this.labShow.Text = "出库CHECK";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fWmsoCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 552);
            this.Controls.Add(this.panel1);
            this.Name = "fWmsoCheck";
            this.Text = "fWMSOCheck";
            this.Load += new System.EventHandler(this.fWmsoCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSAP)).EndInit();
            this.grpPallet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheck)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSAPNO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.DataGridView dgvSAP;
        private System.Windows.Forms.GroupBox grpPallet;
        private System.Windows.Forms.DataGridView dgvCheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPickSAPid;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARTON_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAP_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PICK_SAP_NO;
        private System.Windows.Forms.TextBox txtCarton2;
        private System.Windows.Forms.Label lblQTY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCartonStart;
        private System.Windows.Forms.Label label6;
    }
}