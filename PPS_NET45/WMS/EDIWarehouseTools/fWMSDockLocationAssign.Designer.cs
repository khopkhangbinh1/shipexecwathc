namespace EDIWarehouseTools
{
    partial class fWMSDockLocationAssign
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
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dgvSID = new System.Windows.Forms.DataGridView();
            this.shipment_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pallet_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.car_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isload = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.carrier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sidtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.region = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEXCEL1 = new System.Windows.Forms.Button();
            this.btnSearchPallet = new System.Windows.Forms.Button();
            this.lblStart = new System.Windows.Forms.Label();
            this.dgvAssign = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkALL = new System.Windows.Forms.CheckBox();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.cmbCarNO = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSID = new System.Windows.Forms.ComboBox();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPallet = new System.Windows.Forms.TextBox();
            this.rdoLocation = new System.Windows.Forms.RadioButton();
            this.rdoPALLET = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNEWPalletNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDockLocation = new System.Windows.Forms.TextBox();
            this.btnEXCEL3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCurrPalletNo = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnResult = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDockLocation = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnEXCEL2 = new System.Windows.Forms.Button();
            this.btnSearchDockLocation = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtCheckPallet0 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCheckPallet = new System.Windows.Forms.TextBox();
            this.txtCheckLocation = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssign)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDockLocation)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtStart
            // 
            this.dtStart.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dtStart.CustomFormat = "yyyy-MM-dd";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(136, 5);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(101, 21);
            this.dtStart.TabIndex = 132;
            this.dtStart.Value = new System.DateTime(2019, 7, 12, 0, 0, 0, 0);
            // 
            // dgvSID
            // 
            this.dgvSID.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSID.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvSID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.shipment_id,
            this.pallet_no,
            this.car_no,
            this.isload,
            this.carrier,
            this.sidtype,
            this.type,
            this.region});
            this.dgvSID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSID.Location = new System.Drawing.Point(3, 48);
            this.dgvSID.Name = "dgvSID";
            this.dgvSID.RowHeadersWidth = 51;
            this.dgvSID.RowTemplate.Height = 23;
            this.dgvSID.Size = new System.Drawing.Size(521, 119);
            this.dgvSID.TabIndex = 111;
            // 
            // shipment_id
            // 
            this.shipment_id.HeaderText = "shipment_id";
            this.shipment_id.MinimumWidth = 6;
            this.shipment_id.Name = "shipment_id";
            this.shipment_id.Width = 96;
            // 
            // pallet_no
            // 
            this.pallet_no.HeaderText = "pallet_no";
            this.pallet_no.MinimumWidth = 6;
            this.pallet_no.Name = "pallet_no";
            this.pallet_no.Width = 84;
            // 
            // car_no
            // 
            this.car_no.HeaderText = "car_no";
            this.car_no.MinimumWidth = 6;
            this.car_no.Name = "car_no";
            this.car_no.Width = 66;
            // 
            // isload
            // 
            this.isload.HeaderText = "isload";
            this.isload.MinimumWidth = 6;
            this.isload.Name = "isload";
            this.isload.Width = 66;
            // 
            // carrier
            // 
            this.carrier.HeaderText = "carrier";
            this.carrier.MinimumWidth = 6;
            this.carrier.Name = "carrier";
            this.carrier.Width = 72;
            // 
            // sidtype
            // 
            this.sidtype.HeaderText = "sidtype";
            this.sidtype.MinimumWidth = 6;
            this.sidtype.Name = "sidtype";
            this.sidtype.Width = 72;
            // 
            // type
            // 
            this.type.HeaderText = "type";
            this.type.MinimumWidth = 6;
            this.type.Name = "type";
            this.type.Width = 54;
            // 
            // region
            // 
            this.region.HeaderText = "region";
            this.region.MinimumWidth = 6;
            this.region.Name = "region";
            this.region.Width = 66;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnEXCEL1);
            this.panel2.Controls.Add(this.btnSearchPallet);
            this.panel2.Controls.Add(this.dtStart);
            this.panel2.Controls.Add(this.lblStart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(521, 31);
            this.panel2.TabIndex = 110;
            // 
            // btnEXCEL1
            // 
            this.btnEXCEL1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXCEL1.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEXCEL1.Location = new System.Drawing.Point(460, 3);
            this.btnEXCEL1.Name = "btnEXCEL1";
            this.btnEXCEL1.Size = new System.Drawing.Size(58, 25);
            this.btnEXCEL1.TabIndex = 134;
            this.btnEXCEL1.Text = "EXCEL";
            this.btnEXCEL1.UseVisualStyleBackColor = true;
            this.btnEXCEL1.Click += new System.EventHandler(this.btnEXCEL1_Click);
            // 
            // btnSearchPallet
            // 
            this.btnSearchPallet.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearchPallet.Location = new System.Drawing.Point(257, 3);
            this.btnSearchPallet.Name = "btnSearchPallet";
            this.btnSearchPallet.Size = new System.Drawing.Size(75, 25);
            this.btnSearchPallet.TabIndex = 133;
            this.btnSearchPallet.Text = "查询";
            this.btnSearchPallet.UseVisualStyleBackColor = true;
            this.btnSearchPallet.Click += new System.EventHandler(this.btnSearchPallet_Click);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(4, 9);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(95, 12);
            this.lblStart.TabIndex = 129;
            this.lblStart.Text = "集货单出货日期:";
            // 
            // dgvAssign
            // 
            this.dgvAssign.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAssign.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvAssign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAssign.Location = new System.Drawing.Point(3, 93);
            this.dgvAssign.Name = "dgvAssign";
            this.dgvAssign.RowHeadersWidth = 51;
            this.dgvAssign.RowTemplate.Height = 23;
            this.dgvAssign.Size = new System.Drawing.Size(1076, 193);
            this.dgvAssign.TabIndex = 112;
            this.dgvAssign.SelectionChanged += new System.EventHandler(this.dgvAssign_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1082, 459);
            this.panel1.TabIndex = 78;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvAssign);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1082, 289);
            this.groupBox1.TabIndex = 112;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分单";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkALL);
            this.panel3.Controls.Add(this.chkPrint);
            this.panel3.Controls.Add(this.cmbCarNO);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.cmbSID);
            this.panel3.Controls.Add(this.labShipmentID);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtPallet);
            this.panel3.Controls.Add(this.rdoLocation);
            this.panel3.Controls.Add(this.rdoPALLET);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.btnResult);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1076, 76);
            this.panel3.TabIndex = 87;
            // 
            // chkALL
            // 
            this.chkALL.AutoSize = true;
            this.chkALL.Location = new System.Drawing.Point(263, 7);
            this.chkALL.Name = "chkALL";
            this.chkALL.Size = new System.Drawing.Size(42, 16);
            this.chkALL.TabIndex = 145;
            this.chkALL.Text = "ALL";
            this.chkALL.UseVisualStyleBackColor = true;
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Location = new System.Drawing.Point(481, 9);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(72, 16);
            this.chkPrint.TabIndex = 144;
            this.chkPrint.Text = "自动打印";
            this.chkPrint.UseVisualStyleBackColor = true;
            // 
            // cmbCarNO
            // 
            this.cmbCarNO.FormattingEnabled = true;
            this.cmbCarNO.Items.AddRange(new object[] {
            "-ALL-"});
            this.cmbCarNO.Location = new System.Drawing.Point(104, 27);
            this.cmbCarNO.Margin = new System.Windows.Forms.Padding(1);
            this.cmbCarNO.Name = "cmbCarNO";
            this.cmbCarNO.Size = new System.Drawing.Size(97, 20);
            this.cmbCarNO.TabIndex = 143;
            this.cmbCarNO.Text = "-ALL-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 142;
            this.label5.Text = "当车牌:";
            // 
            // cmbSID
            // 
            this.cmbSID.FormattingEnabled = true;
            this.cmbSID.Items.AddRange(new object[] {
            "-ALL-"});
            this.cmbSID.Location = new System.Drawing.Point(104, 51);
            this.cmbSID.Margin = new System.Windows.Forms.Padding(1);
            this.cmbSID.Name = "cmbSID";
            this.cmbSID.Size = new System.Drawing.Size(132, 20);
            this.cmbSID.TabIndex = 139;
            this.cmbSID.Text = "-ALL-";
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(2, 54);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(59, 12);
            this.labShipmentID.TabIndex = 138;
            this.labShipmentID.Text = "集货单号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(328, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 14);
            this.label3.TabIndex = 114;
            this.label3.Text = "刷入栈板号自动分储位";
            // 
            // txtPallet
            // 
            this.txtPallet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPallet.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPallet.ForeColor = System.Drawing.Color.Blue;
            this.txtPallet.Location = new System.Drawing.Point(331, 30);
            this.txtPallet.Margin = new System.Windows.Forms.Padding(4);
            this.txtPallet.Name = "txtPallet";
            this.txtPallet.Size = new System.Drawing.Size(218, 30);
            this.txtPallet.TabIndex = 113;
            this.txtPallet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPallet_KeyDown);
            // 
            // rdoLocation
            // 
            this.rdoLocation.AutoSize = true;
            this.rdoLocation.Checked = true;
            this.rdoLocation.Location = new System.Drawing.Point(138, 7);
            this.rdoLocation.Margin = new System.Windows.Forms.Padding(2);
            this.rdoLocation.Name = "rdoLocation";
            this.rdoLocation.Size = new System.Drawing.Size(83, 16);
            this.rdoLocation.TabIndex = 112;
            this.rdoLocation.TabStop = true;
            this.rdoLocation.Text = "按照储位查";
            this.rdoLocation.UseVisualStyleBackColor = true;
            this.rdoLocation.CheckedChanged += new System.EventHandler(this.rdoLocation_CheckedChanged);
            // 
            // rdoPALLET
            // 
            this.rdoPALLET.AutoSize = true;
            this.rdoPALLET.Location = new System.Drawing.Point(11, 7);
            this.rdoPALLET.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPALLET.Name = "rdoPALLET";
            this.rdoPALLET.Size = new System.Drawing.Size(83, 16);
            this.rdoPALLET.TabIndex = 111;
            this.rdoPALLET.Text = "按栈板号查";
            this.rdoPALLET.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnPrint);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.txtNEWPalletNo);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.txtDockLocation);
            this.panel6.Controls.Add(this.btnEXCEL3);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.txtCurrPalletNo);
            this.panel6.Controls.Add(this.btnUpdate);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(564, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(512, 76);
            this.panel6.TabIndex = 110;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(251, 41);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 25);
            this.btnPrint.TabIndex = 141;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(232, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 140;
            this.label2.Text = "指定栈板号:";
            // 
            // txtNEWPalletNo
            // 
            this.txtNEWPalletNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNEWPalletNo.BackColor = System.Drawing.SystemColors.Control;
            this.txtNEWPalletNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNEWPalletNo.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNEWPalletNo.ForeColor = System.Drawing.Color.Green;
            this.txtNEWPalletNo.Location = new System.Drawing.Point(338, 10);
            this.txtNEWPalletNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtNEWPalletNo.Name = "txtNEWPalletNo";
            this.txtNEWPalletNo.Size = new System.Drawing.Size(171, 26);
            this.txtNEWPalletNo.TabIndex = 139;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 137;
            this.label1.Text = "Location:";
            // 
            // txtDockLocation
            // 
            this.txtDockLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDockLocation.BackColor = System.Drawing.SystemColors.Control;
            this.txtDockLocation.Enabled = false;
            this.txtDockLocation.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDockLocation.ForeColor = System.Drawing.Color.Green;
            this.txtDockLocation.Location = new System.Drawing.Point(82, 10);
            this.txtDockLocation.Margin = new System.Windows.Forms.Padding(4);
            this.txtDockLocation.Name = "txtDockLocation";
            this.txtDockLocation.Size = new System.Drawing.Size(149, 26);
            this.txtDockLocation.TabIndex = 138;
            // 
            // btnEXCEL3
            // 
            this.btnEXCEL3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXCEL3.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEXCEL3.Location = new System.Drawing.Point(449, 40);
            this.btnEXCEL3.Name = "btnEXCEL3";
            this.btnEXCEL3.Size = new System.Drawing.Size(56, 26);
            this.btnEXCEL3.TabIndex = 136;
            this.btnEXCEL3.Text = "EXCEL";
            this.btnEXCEL3.UseVisualStyleBackColor = true;
            this.btnEXCEL3.Click += new System.EventHandler(this.btnEXCEL3_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 14);
            this.label4.TabIndex = 107;
            this.label4.Text = "栈板号:";
            // 
            // txtCurrPalletNo
            // 
            this.txtCurrPalletNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrPalletNo.BackColor = System.Drawing.SystemColors.Control;
            this.txtCurrPalletNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCurrPalletNo.Enabled = false;
            this.txtCurrPalletNo.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCurrPalletNo.ForeColor = System.Drawing.Color.Green;
            this.txtCurrPalletNo.Location = new System.Drawing.Point(82, 38);
            this.txtCurrPalletNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCurrPalletNo.Name = "txtCurrPalletNo";
            this.txtCurrPalletNo.Size = new System.Drawing.Size(149, 26);
            this.txtCurrPalletNo.TabIndex = 108;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(334, 41);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 25);
            this.btnUpdate.TabIndex = 106;
            this.btnUpdate.Text = "执行更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnResult
            // 
            this.btnResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnResult.Location = new System.Drawing.Point(238, 32);
            this.btnResult.Name = "btnResult";
            this.btnResult.Size = new System.Drawing.Size(75, 25);
            this.btnResult.TabIndex = 109;
            this.btnResult.Text = "查询";
            this.btnResult.UseVisualStyleBackColor = true;
            this.btnResult.Click += new System.EventHandler(this.btnResult_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox4);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1082, 170);
            this.panel4.TabIndex = 111;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDockLocation);
            this.groupBox2.Controls.Add(this.panel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(527, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 170);
            this.groupBox2.TabIndex = 113;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Location信息:";
            // 
            // dgvDockLocation
            // 
            this.dgvDockLocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDockLocation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDockLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDockLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDockLocation.Location = new System.Drawing.Point(3, 48);
            this.dgvDockLocation.Name = "dgvDockLocation";
            this.dgvDockLocation.RowHeadersWidth = 51;
            this.dgvDockLocation.RowTemplate.Height = 23;
            this.dgvDockLocation.Size = new System.Drawing.Size(323, 119);
            this.dgvDockLocation.TabIndex = 112;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnEXCEL2);
            this.panel5.Controls.Add(this.btnSearchDockLocation);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 17);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(323, 31);
            this.panel5.TabIndex = 111;
            // 
            // btnEXCEL2
            // 
            this.btnEXCEL2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEXCEL2.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEXCEL2.Location = new System.Drawing.Point(263, 3);
            this.btnEXCEL2.Name = "btnEXCEL2";
            this.btnEXCEL2.Size = new System.Drawing.Size(58, 25);
            this.btnEXCEL2.TabIndex = 135;
            this.btnEXCEL2.Text = "EXCEL";
            this.btnEXCEL2.UseVisualStyleBackColor = true;
            this.btnEXCEL2.Click += new System.EventHandler(this.btnEXCEL2_Click_1);
            // 
            // btnSearchDockLocation
            // 
            this.btnSearchDockLocation.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSearchDockLocation.Location = new System.Drawing.Point(39, 3);
            this.btnSearchDockLocation.Name = "btnSearchDockLocation";
            this.btnSearchDockLocation.Size = new System.Drawing.Size(75, 25);
            this.btnSearchDockLocation.TabIndex = 107;
            this.btnSearchDockLocation.Text = "查询";
            this.btnSearchDockLocation.UseVisualStyleBackColor = true;
            this.btnSearchDockLocation.Click += new System.EventHandler(this.btnSearchDockLocation_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCheckPallet0);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtCheckPallet);
            this.groupBox4.Controls.Add(this.txtCheckLocation);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Location = new System.Drawing.Point(856, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(226, 170);
            this.groupBox4.TabIndex = 112;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "储位检查:";
            // 
            // txtCheckPallet0
            // 
            this.txtCheckPallet0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCheckPallet0.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckPallet0.ForeColor = System.Drawing.Color.Blue;
            this.txtCheckPallet0.Location = new System.Drawing.Point(24, 92);
            this.txtCheckPallet0.Margin = new System.Windows.Forms.Padding(4);
            this.txtCheckPallet0.Name = "txtCheckPallet0";
            this.txtCheckPallet0.Size = new System.Drawing.Size(170, 30);
            this.txtCheckPallet0.TabIndex = 140;
            this.txtCheckPallet0.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckPallet0_KeyDown);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(21, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 14);
            this.label7.TabIndex = 139;
            this.label7.Text = "栈板号:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(21, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 138;
            this.label6.Text = "Location:";
            // 
            // txtCheckPallet
            // 
            this.txtCheckPallet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCheckPallet.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckPallet.ForeColor = System.Drawing.Color.Blue;
            this.txtCheckPallet.Location = new System.Drawing.Point(24, 130);
            this.txtCheckPallet.Margin = new System.Windows.Forms.Padding(4);
            this.txtCheckPallet.Name = "txtCheckPallet";
            this.txtCheckPallet.Size = new System.Drawing.Size(170, 30);
            this.txtCheckPallet.TabIndex = 115;
            this.txtCheckPallet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckPallet_KeyDown);
            // 
            // txtCheckLocation
            // 
            this.txtCheckLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCheckLocation.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCheckLocation.ForeColor = System.Drawing.Color.Blue;
            this.txtCheckLocation.Location = new System.Drawing.Point(23, 41);
            this.txtCheckLocation.Margin = new System.Windows.Forms.Padding(4);
            this.txtCheckLocation.Name = "txtCheckLocation";
            this.txtCheckLocation.Size = new System.Drawing.Size(170, 30);
            this.txtCheckLocation.TabIndex = 114;
            this.txtCheckLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheckLocation_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvSID);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(527, 170);
            this.groupBox3.TabIndex = 109;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "栈板信息";
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 500);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1082, 41);
            this.TextMsg.TabIndex = 77;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1082, 41);
            this.labShow.TabIndex = 76;
            this.labShow.Text = "码头储位分配";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fWMSDockLocationAssign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.labShow);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fWMSDockLocationAssign";
            this.Text = "fWMSDockLocationAssign";
            this.Load += new System.EventHandler(this.fWMSDockLocationAssign_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSID)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssign)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDockLocation)).EndInit();
            this.panel5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DataGridView dgvSID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnEXCEL1;
        private System.Windows.Forms.Button btnSearchPallet;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DataGridView dgvAssign;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNEWPalletNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDockLocation;
        private System.Windows.Forms.Button btnEXCEL3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCurrPalletNo;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnResult;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipment_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn pallet_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn car_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn isload;
        private System.Windows.Forms.DataGridViewTextBoxColumn carrier;
        private System.Windows.Forms.DataGridViewTextBoxColumn sidtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn region;
        private System.Windows.Forms.RadioButton rdoLocation;
        private System.Windows.Forms.RadioButton rdoPALLET;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPallet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSID;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.ComboBox cmbCarNO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDockLocation;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnEXCEL2;
        private System.Windows.Forms.Button btnSearchDockLocation;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCheckPallet;
        private System.Windows.Forms.TextBox txtCheckLocation;
        private System.Windows.Forms.TextBox txtCheckPallet0;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.CheckBox chkALL;
    }
}