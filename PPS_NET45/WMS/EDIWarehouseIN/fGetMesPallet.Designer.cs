namespace EDIWarehouseIN
{
    partial class fGetMesPallet
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvCarton = new System.Windows.Forms.DataGridView();
            this.SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BATCHTYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOAD_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOXID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PALLETID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MODEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REGION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QHOLDFLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TROLLEYNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TROLLEYLINENO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TROLLEYLINENOPOINT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkHold = new System.Windows.Forms.CheckBox();
            this.lblHold = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkUpdatePallet = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFGName = new System.Windows.Forms.ComboBox();
            this.rdoPallet = new System.Windows.Forms.RadioButton();
            this.rdoCarton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.labShow = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarton)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 599);
            this.panel1.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvCarton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 153);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(1004, 403);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SN信息";
            // 
            // dgvCarton
            // 
            this.dgvCarton.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCarton.BackgroundColor = System.Drawing.Color.White;
            this.dgvCarton.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarton.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SN,
            this.WO,
            this.BATCHTYPE,
            this.LOAD_ID,
            this.BOXID,
            this.PALLETID,
            this.PN,
            this.MODEL,
            this.REGION,
            this.CUSTPN,
            this.QHOLDFLAG,
            this.TROLLEYNO,
            this.TROLLEYLINENO,
            this.TROLLEYLINENOPOINT,
            this.DN,
            this.ITEMNO});
            this.dgvCarton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCarton.Location = new System.Drawing.Point(2, 19);
            this.dgvCarton.Name = "dgvCarton";
            this.dgvCarton.RowHeadersWidth = 55;
            this.dgvCarton.RowTemplate.Height = 23;
            this.dgvCarton.Size = new System.Drawing.Size(1000, 382);
            this.dgvCarton.TabIndex = 0;
            // 
            // SN
            // 
            this.SN.HeaderText = "SN";
            this.SN.MinimumWidth = 6;
            this.SN.Name = "SN";
            this.SN.Width = 48;
            // 
            // WO
            // 
            this.WO.HeaderText = "WO";
            this.WO.MinimumWidth = 6;
            this.WO.Name = "WO";
            this.WO.Width = 48;
            // 
            // BATCHTYPE
            // 
            this.BATCHTYPE.HeaderText = "BATCHTYPE";
            this.BATCHTYPE.MinimumWidth = 6;
            this.BATCHTYPE.Name = "BATCHTYPE";
            this.BATCHTYPE.Width = 104;
            // 
            // LOAD_ID
            // 
            this.LOAD_ID.HeaderText = "LOAD_ID";
            this.LOAD_ID.MinimumWidth = 6;
            this.LOAD_ID.Name = "LOAD_ID";
            this.LOAD_ID.Width = 88;
            // 
            // BOXID
            // 
            this.BOXID.HeaderText = "BOXID";
            this.BOXID.MinimumWidth = 6;
            this.BOXID.Name = "BOXID";
            this.BOXID.Width = 72;
            // 
            // PALLETID
            // 
            this.PALLETID.HeaderText = "PALLETID";
            this.PALLETID.MinimumWidth = 6;
            this.PALLETID.Name = "PALLETID";
            this.PALLETID.Width = 96;
            // 
            // PN
            // 
            this.PN.HeaderText = "PN";
            this.PN.MinimumWidth = 6;
            this.PN.Name = "PN";
            this.PN.Width = 48;
            // 
            // MODEL
            // 
            this.MODEL.HeaderText = "MODEL";
            this.MODEL.MinimumWidth = 6;
            this.MODEL.Name = "MODEL";
            this.MODEL.Width = 72;
            // 
            // REGION
            // 
            this.REGION.HeaderText = "REGION";
            this.REGION.MinimumWidth = 6;
            this.REGION.Name = "REGION";
            this.REGION.Width = 80;
            // 
            // CUSTPN
            // 
            this.CUSTPN.HeaderText = "CUSTPN";
            this.CUSTPN.MinimumWidth = 6;
            this.CUSTPN.Name = "CUSTPN";
            this.CUSTPN.Width = 80;
            // 
            // QHOLDFLAG
            // 
            this.QHOLDFLAG.HeaderText = "QHOLDFLAG";
            this.QHOLDFLAG.MinimumWidth = 6;
            this.QHOLDFLAG.Name = "QHOLDFLAG";
            this.QHOLDFLAG.Width = 104;
            // 
            // TROLLEYNO
            // 
            this.TROLLEYNO.HeaderText = "TROLLEYNO";
            this.TROLLEYNO.MinimumWidth = 6;
            this.TROLLEYNO.Name = "TROLLEYNO";
            this.TROLLEYNO.Width = 104;
            // 
            // TROLLEYLINENO
            // 
            this.TROLLEYLINENO.HeaderText = "TROLLEYLINENO";
            this.TROLLEYLINENO.MinimumWidth = 6;
            this.TROLLEYLINENO.Name = "TROLLEYLINENO";
            this.TROLLEYLINENO.Width = 136;
            // 
            // TROLLEYLINENOPOINT
            // 
            this.TROLLEYLINENOPOINT.HeaderText = "TROLLEYLINENOPOINT";
            this.TROLLEYLINENOPOINT.MinimumWidth = 6;
            this.TROLLEYLINENOPOINT.Name = "TROLLEYLINENOPOINT";
            this.TROLLEYLINENOPOINT.Width = 176;
            // 
            // DN
            // 
            this.DN.HeaderText = "DN";
            this.DN.MinimumWidth = 6;
            this.DN.Name = "DN";
            this.DN.Width = 48;
            // 
            // ITEMNO
            // 
            this.ITEMNO.HeaderText = "ITEMNO";
            this.ITEMNO.MinimumWidth = 6;
            this.ITEMNO.Name = "ITEMNO";
            this.ITEMNO.Width = 80;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.chkHold);
            this.groupBox3.Controls.Add(this.lblHold);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.chkUpdatePallet);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cmbFGName);
            this.groupBox3.Controls.Add(this.rdoPallet);
            this.groupBox3.Controls.Add(this.rdoCarton);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtCarton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 41);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(1004, 112);
            this.groupBox3.TabIndex = 76;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "刷入序号";
            // 
            // chkHold
            // 
            this.chkHold.AutoSize = true;
            this.chkHold.Checked = true;
            this.chkHold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHold.Location = new System.Drawing.Point(595, 22);
            this.chkHold.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkHold.Name = "chkHold";
            this.chkHold.Size = new System.Drawing.Size(88, 19);
            this.chkHold.TabIndex = 135;
            this.chkHold.Text = "检查HOLD";
            this.chkHold.UseVisualStyleBackColor = true;
            // 
            // lblHold
            // 
            this.lblHold.AutoSize = true;
            this.lblHold.BackColor = System.Drawing.Color.ForestGreen;
            this.lblHold.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHold.Location = new System.Drawing.Point(501, 63);
            this.lblHold.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHold.Name = "lblHold";
            this.lblHold.Size = new System.Drawing.Size(54, 22);
            this.lblHold.TabIndex = 134;
            this.lblHold.Text = "HOLD";
            this.lblHold.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(643, 46);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 27);
            this.btnSearch.TabIndex = 133;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkUpdatePallet
            // 
            this.chkUpdatePallet.AutoSize = true;
            this.chkUpdatePallet.Location = new System.Drawing.Point(362, 23);
            this.chkUpdatePallet.Name = "chkUpdatePallet";
            this.chkUpdatePallet.Size = new System.Drawing.Size(176, 19);
            this.chkUpdatePallet.TabIndex = 132;
            this.chkUpdatePallet.Text = "强制重新获取栈板信息";
            this.chkUpdatePallet.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 131;
            this.label1.Text = "产品:";
            // 
            // cmbFGName
            // 
            this.cmbFGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFGName.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbFGName.FormattingEnabled = true;
            this.cmbFGName.Location = new System.Drawing.Point(132, 19);
            this.cmbFGName.Margin = new System.Windows.Forms.Padding(1);
            this.cmbFGName.Name = "cmbFGName";
            this.cmbFGName.Size = new System.Drawing.Size(194, 24);
            this.cmbFGName.TabIndex = 130;
            this.cmbFGName.SelectedIndexChanged += new System.EventHandler(this.cmbFGName_SelectedIndexChanged);
            // 
            // rdoPallet
            // 
            this.rdoPallet.AutoSize = true;
            this.rdoPallet.Checked = true;
            this.rdoPallet.Location = new System.Drawing.Point(260, 46);
            this.rdoPallet.Name = "rdoPallet";
            this.rdoPallet.Size = new System.Drawing.Size(145, 19);
            this.rdoPallet.TabIndex = 127;
            this.rdoPallet.TabStop = true;
            this.rdoPallet.Text = "栈板号（非原材）";
            this.rdoPallet.UseVisualStyleBackColor = true;
            this.rdoPallet.CheckedChanged += new System.EventHandler(this.rdoPallet_CheckedChanged);
            // 
            // rdoCarton
            // 
            this.rdoCarton.AutoSize = true;
            this.rdoCarton.Location = new System.Drawing.Point(132, 46);
            this.rdoCarton.Name = "rdoCarton";
            this.rdoCarton.Size = new System.Drawing.Size(115, 19);
            this.rdoCarton.TabIndex = 126;
            this.rdoCarton.Text = "箱号（原材）";
            this.rdoCarton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(18, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 122;
            this.label3.Text = "栈板号/箱号:";
            // 
            // txtCarton
            // 
            this.txtCarton.BackColor = System.Drawing.Color.White;
            this.txtCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(132, 72);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1004, 41);
            this.labShow.TabIndex = 64;
            this.labShow.Text = "获取MES栈板信息";
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
            this.TextMsg.Location = new System.Drawing.Point(0, 556);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1004, 43);
            this.TextMsg.TabIndex = 63;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fGetMesPallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 599);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "fGetMesPallet";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.fGetMesPallet_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarton)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvCarton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFGName;
        private System.Windows.Forms.RadioButton rdoPallet;
        private System.Windows.Forms.RadioButton rdoCarton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.CheckBox chkUpdatePallet;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn SN;
        private System.Windows.Forms.DataGridViewTextBoxColumn WO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BATCHTYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOAD_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOXID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PALLETID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PN;
        private System.Windows.Forms.DataGridViewTextBoxColumn MODEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn REGION;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTPN;
        private System.Windows.Forms.DataGridViewTextBoxColumn QHOLDFLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn TROLLEYNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TROLLEYLINENO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TROLLEYLINENOPOINT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNO;
        private System.Windows.Forms.Label lblHold;
        private System.Windows.Forms.CheckBox chkHold;
    }
}

