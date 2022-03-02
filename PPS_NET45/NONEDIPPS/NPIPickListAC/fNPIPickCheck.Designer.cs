namespace NPIPickListAC
{
    partial class fNPIPickCheck
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
            this.grpPallet = new System.Windows.Forms.GroupBox();
            this.dgvSID = new System.Windows.Forms.DataGridView();
            this.lblQTY = new System.Windows.Forms.Label();
            this.cmbPickSID = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCheck = new System.Windows.Forms.DataGridView();
            this.CARTON_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NPISID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PICK_PALLET_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.labShow = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.grpPallet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheck)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPallet
            // 
            this.grpPallet.BackColor = System.Drawing.Color.LightSteelBlue;
            this.grpPallet.Controls.Add(this.dgvSID);
            this.grpPallet.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpPallet.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpPallet.Location = new System.Drawing.Point(0, 166);
            this.grpPallet.Margin = new System.Windows.Forms.Padding(2);
            this.grpPallet.Name = "grpPallet";
            this.grpPallet.Padding = new System.Windows.Forms.Padding(2);
            this.grpPallet.Size = new System.Drawing.Size(480, 327);
            this.grpPallet.TabIndex = 77;
            this.grpPallet.TabStop = false;
            this.grpPallet.Text = "集货单-SN信息";
            // 
            // dgvSID
            // 
            this.dgvSID.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSID.BackgroundColor = System.Drawing.Color.White;
            this.dgvSID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSID.Location = new System.Drawing.Point(2, 19);
            this.dgvSID.Name = "dgvSID";
            this.dgvSID.RowHeadersWidth = 55;
            this.dgvSID.RowTemplate.Height = 23;
            this.dgvSID.Size = new System.Drawing.Size(476, 306);
            this.dgvSID.TabIndex = 0;
            this.dgvSID.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvSID_RowsAdded);
            // 
            // lblQTY
            // 
            this.lblQTY.AutoSize = true;
            this.lblQTY.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.lblQTY.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQTY.Location = new System.Drawing.Point(724, 25);
            this.lblQTY.Name = "lblQTY";
            this.lblQTY.Size = new System.Drawing.Size(28, 14);
            this.lblQTY.TabIndex = 133;
            this.lblQTY.Text = "0/0";
            // 
            // cmbPickSID
            // 
            this.cmbPickSID.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPickSID.FormattingEnabled = true;
            this.cmbPickSID.Location = new System.Drawing.Point(151, 65);
            this.cmbPickSID.Margin = new System.Windows.Forms.Padding(1);
            this.cmbPickSID.Name = "cmbPickSID";
            this.cmbPickSID.Size = new System.Drawing.Size(194, 29);
            this.cmbPickSID.TabIndex = 132;
            this.cmbPickSID.Text = "ALL";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.grpPallet);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1031, 536);
            this.panel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.dgvCheck);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(480, 166);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(551, 327);
            this.groupBox1.TabIndex = 78;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CHECK-SN信息";
            // 
            // dgvCheck
            // 
            this.dgvCheck.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCheck.BackgroundColor = System.Drawing.Color.White;
            this.dgvCheck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCheck.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CARTON_NO,
            this.NPISID,
            this.PICK_PALLET_NO});
            this.dgvCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCheck.Location = new System.Drawing.Point(2, 19);
            this.dgvCheck.Name = "dgvCheck";
            this.dgvCheck.RowHeadersWidth = 55;
            this.dgvCheck.RowTemplate.Height = 23;
            this.dgvCheck.Size = new System.Drawing.Size(547, 306);
            this.dgvCheck.TabIndex = 0;
            this.dgvCheck.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvCheck_RowsAdded);
            // 
            // CARTON_NO
            // 
            this.CARTON_NO.HeaderText = "CARTON_NO";
            this.CARTON_NO.Name = "CARTON_NO";
            this.CARTON_NO.Width = 104;
            // 
            // NPISID
            // 
            this.NPISID.HeaderText = "NPISID";
            this.NPISID.Name = "NPISID";
            this.NPISID.Width = 80;
            // 
            // PICK_PALLET_NO
            // 
            this.PICK_PALLET_NO.HeaderText = "PICK_PALLET_NO";
            this.PICK_PALLET_NO.Name = "PICK_PALLET_NO";
            this.PICK_PALLET_NO.Width = 144;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.lblQTY);
            this.groupBox3.Controls.Add(this.cmbPickSID);
            this.groupBox3.Controls.Add(this.btnEnd);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtSID);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtCarton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 41);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1031, 125);
            this.groupBox3.TabIndex = 76;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "刷入序号";
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Location = new System.Drawing.Point(407, 62);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(94, 30);
            this.btnEnd.TabIndex = 131;
            this.btnEnd.Text = "结束";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(407, 23);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 31);
            this.btnStart.TabIndex = 130;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(18, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 14);
            this.label2.TabIndex = 128;
            this.label2.Text = "PICK_NPI集货单号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(46, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 14);
            this.label4.TabIndex = 124;
            this.label4.Text = "NPI-集货单号:";
            // 
            // txtSID
            // 
            this.txtSID.BackColor = System.Drawing.SystemColors.Control;
            this.txtSID.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSID.ForeColor = System.Drawing.Color.Green;
            this.txtSID.Location = new System.Drawing.Point(151, 25);
            this.txtSID.Margin = new System.Windows.Forms.Padding(4);
            this.txtSID.Name = "txtSID";
            this.txtSID.Size = new System.Drawing.Size(194, 29);
            this.txtSID.TabIndex = 125;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(567, 69);
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
            this.txtCarton.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(640, 60);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(256, 32);
            this.txtCarton.TabIndex = 123;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1031, 41);
            this.labShow.TabIndex = 64;
            this.labShow.Text = "NPI出货CHECK";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 493);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1031, 43);
            this.TextMsg.TabIndex = 63;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fNPIPickCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 536);
            this.Controls.Add(this.panel1);
            this.Name = "fNPIPickCheck";
            this.Text = "fNPIPickCheck";
            this.Load += new System.EventHandler(this.fNPIPickCheck_Load);
            this.grpPallet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheck)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPallet;
        private System.Windows.Forms.DataGridView dgvSID;
        private System.Windows.Forms.Label lblQTY;
        private System.Windows.Forms.ComboBox cmbPickSID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCheck;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARTON_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NPISID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PICK_PALLET_NO;
    }
}