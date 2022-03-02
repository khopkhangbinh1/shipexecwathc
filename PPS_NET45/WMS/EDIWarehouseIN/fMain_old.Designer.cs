namespace EDIWarehouseIN
{
    partial class fMain_old
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFGName = new System.Windows.Forms.ComboBox();
            this.rdoPallet = new System.Windows.Forms.RadioButton();
            this.rdoCarton = new System.Windows.Forms.RadioButton();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvLocation = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbLocationRegion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSAPWH = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.labEntrancePort = new System.Windows.Forms.Label();
            this.cmbWH = new System.Windows.Forms.ComboBox();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarton)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocation)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.labShow);
            this.panel1.Controls.Add(this.TextMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1339, 749);
            this.panel1.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvCarton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(0, 300);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(1339, 203);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SN信息";
            // 
            // dgvCarton
            // 
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
            this.dgvCarton.Location = new System.Drawing.Point(3, 23);
            this.dgvCarton.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCarton.Name = "dgvCarton";
            this.dgvCarton.RowHeadersWidth = 55;
            this.dgvCarton.RowTemplate.Height = 23;
            this.dgvCarton.Size = new System.Drawing.Size(1333, 178);
            this.dgvCarton.TabIndex = 0;
            // 
            // SN
            // 
            this.SN.DataPropertyName = "SN";
            this.SN.HeaderText = "SN";
            this.SN.MinimumWidth = 6;
            this.SN.Name = "SN";
            this.SN.ReadOnly = true;
            this.SN.Width = 125;
            // 
            // WO
            // 
            this.WO.DataPropertyName = "WO";
            this.WO.HeaderText = "WO";
            this.WO.MinimumWidth = 6;
            this.WO.Name = "WO";
            this.WO.ReadOnly = true;
            this.WO.Width = 125;
            // 
            // BATCHTYPE
            // 
            this.BATCHTYPE.DataPropertyName = "BATCHTYPE";
            this.BATCHTYPE.HeaderText = "BATCHTYPE";
            this.BATCHTYPE.MinimumWidth = 6;
            this.BATCHTYPE.Name = "BATCHTYPE";
            this.BATCHTYPE.ReadOnly = true;
            this.BATCHTYPE.Width = 125;
            // 
            // LOAD_ID
            // 
            this.LOAD_ID.DataPropertyName = "LOAD_ID";
            this.LOAD_ID.HeaderText = "LOAD_ID";
            this.LOAD_ID.MinimumWidth = 6;
            this.LOAD_ID.Name = "LOAD_ID";
            this.LOAD_ID.ReadOnly = true;
            this.LOAD_ID.Width = 125;
            // 
            // BOXID
            // 
            this.BOXID.DataPropertyName = "BOXID";
            this.BOXID.HeaderText = "BOXID";
            this.BOXID.MinimumWidth = 6;
            this.BOXID.Name = "BOXID";
            this.BOXID.ReadOnly = true;
            this.BOXID.Width = 125;
            // 
            // PALLETID
            // 
            this.PALLETID.DataPropertyName = "PALLETID";
            this.PALLETID.HeaderText = "PALLETID";
            this.PALLETID.MinimumWidth = 6;
            this.PALLETID.Name = "PALLETID";
            this.PALLETID.ReadOnly = true;
            this.PALLETID.Width = 125;
            // 
            // PN
            // 
            this.PN.DataPropertyName = "PN";
            this.PN.HeaderText = "PN";
            this.PN.MinimumWidth = 6;
            this.PN.Name = "PN";
            this.PN.ReadOnly = true;
            this.PN.Width = 125;
            // 
            // MODEL
            // 
            this.MODEL.DataPropertyName = "MODEL";
            this.MODEL.HeaderText = "MODEL";
            this.MODEL.MinimumWidth = 6;
            this.MODEL.Name = "MODEL";
            this.MODEL.ReadOnly = true;
            this.MODEL.Width = 125;
            // 
            // REGION
            // 
            this.REGION.DataPropertyName = "REGION";
            this.REGION.HeaderText = "REGION";
            this.REGION.MinimumWidth = 6;
            this.REGION.Name = "REGION";
            this.REGION.ReadOnly = true;
            this.REGION.Width = 125;
            // 
            // CUSTPN
            // 
            this.CUSTPN.DataPropertyName = "CUSTPN";
            this.CUSTPN.HeaderText = "CUSTPN";
            this.CUSTPN.MinimumWidth = 6;
            this.CUSTPN.Name = "CUSTPN";
            this.CUSTPN.ReadOnly = true;
            this.CUSTPN.Width = 125;
            // 
            // QHOLDFLAG
            // 
            this.QHOLDFLAG.DataPropertyName = "QHOLDFLAG";
            this.QHOLDFLAG.HeaderText = "QHOLDFLAG";
            this.QHOLDFLAG.MinimumWidth = 6;
            this.QHOLDFLAG.Name = "QHOLDFLAG";
            this.QHOLDFLAG.ReadOnly = true;
            this.QHOLDFLAG.Width = 125;
            // 
            // TROLLEYNO
            // 
            this.TROLLEYNO.DataPropertyName = "TROLLEYNO";
            this.TROLLEYNO.HeaderText = "TROLLEYNO";
            this.TROLLEYNO.MinimumWidth = 6;
            this.TROLLEYNO.Name = "TROLLEYNO";
            this.TROLLEYNO.ReadOnly = true;
            this.TROLLEYNO.Width = 125;
            // 
            // TROLLEYLINENO
            // 
            this.TROLLEYLINENO.DataPropertyName = "TROLLEYLINENO";
            this.TROLLEYLINENO.HeaderText = "TROLLEYLINENO";
            this.TROLLEYLINENO.MinimumWidth = 6;
            this.TROLLEYLINENO.Name = "TROLLEYLINENO";
            this.TROLLEYLINENO.ReadOnly = true;
            this.TROLLEYLINENO.Width = 125;
            // 
            // TROLLEYLINENOPOINT
            // 
            this.TROLLEYLINENOPOINT.DataPropertyName = "TROLLEYLINENOPOINT";
            this.TROLLEYLINENOPOINT.HeaderText = "TROLLEYLINENOPOINT";
            this.TROLLEYLINENOPOINT.MinimumWidth = 6;
            this.TROLLEYLINENOPOINT.Name = "TROLLEYLINENOPOINT";
            this.TROLLEYLINENOPOINT.ReadOnly = true;
            this.TROLLEYLINENOPOINT.Width = 125;
            // 
            // DN
            // 
            this.DN.DataPropertyName = "DN";
            this.DN.HeaderText = "DN";
            this.DN.MinimumWidth = 6;
            this.DN.Name = "DN";
            this.DN.ReadOnly = true;
            this.DN.Width = 125;
            // 
            // ITEMNO
            // 
            this.ITEMNO.DataPropertyName = "ITEMNO";
            this.ITEMNO.HeaderText = "ITEMNO";
            this.ITEMNO.MinimumWidth = 6;
            this.ITEMNO.Name = "ITEMNO";
            this.ITEMNO.ReadOnly = true;
            this.ITEMNO.Width = 125;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cmbFGName);
            this.groupBox3.Controls.Add(this.rdoPallet);
            this.groupBox3.Controls.Add(this.rdoCarton);
            this.groupBox3.Controls.Add(this.chkPrint);
            this.groupBox3.Controls.Add(this.btnEnd);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.txtCarton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 144);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(1339, 156);
            this.groupBox3.TabIndex = 76;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "刷入序号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 19);
            this.label1.TabIndex = 131;
            this.label1.Text = "产品:";
            // 
            // cmbFGName
            // 
            this.cmbFGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFGName.FormattingEnabled = true;
            this.cmbFGName.Location = new System.Drawing.Point(103, 24);
            this.cmbFGName.Margin = new System.Windows.Forms.Padding(1);
            this.cmbFGName.Name = "cmbFGName";
            this.cmbFGName.Size = new System.Drawing.Size(257, 26);
            this.cmbFGName.TabIndex = 130;
            // 
            // rdoPallet
            // 
            this.rdoPallet.AutoSize = true;
            this.rdoPallet.Checked = true;
            this.rdoPallet.Location = new System.Drawing.Point(273, 58);
            this.rdoPallet.Margin = new System.Windows.Forms.Padding(4);
            this.rdoPallet.Name = "rdoPallet";
            this.rdoPallet.Size = new System.Drawing.Size(87, 23);
            this.rdoPallet.TabIndex = 127;
            this.rdoPallet.TabStop = true;
            this.rdoPallet.Text = "栈板号";
            this.rdoPallet.UseVisualStyleBackColor = true;
            // 
            // rdoCarton
            // 
            this.rdoCarton.AutoSize = true;
            this.rdoCarton.Enabled = false;
            this.rdoCarton.Location = new System.Drawing.Point(133, 58);
            this.rdoCarton.Margin = new System.Windows.Forms.Padding(4);
            this.rdoCarton.Name = "rdoCarton";
            this.rdoCarton.Size = new System.Drawing.Size(68, 23);
            this.rdoCarton.TabIndex = 126;
            this.rdoCarton.Text = "箱号";
            this.rdoCarton.UseVisualStyleBackColor = true;
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Checked = true;
            this.chkPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrint.Location = new System.Drawing.Point(1052, 59);
            this.chkPrint.Margin = new System.Windows.Forms.Padding(4);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(183, 23);
            this.chkPrint.TabIndex = 125;
            this.chkPrint.Text = "自动打印栈板标签";
            this.chkPrint.UseVisualStyleBackColor = true;
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Location = new System.Drawing.Point(696, 28);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(125, 54);
            this.btnEnd.TabIndex = 129;
            this.btnEnd.Text = "结束";
            this.btnEnd.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(40, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 122;
            this.label3.Text = "栈板号/箱号:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(561, 28);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(125, 54);
            this.btnStart.TabIndex = 115;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // txtCarton
            // 
            this.txtCarton.BackColor = System.Drawing.Color.White;
            this.txtCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton.Enabled = false;
            this.txtCarton.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(176, 90);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(5);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(340, 38);
            this.txtCarton.TabIndex = 123;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox4.Controls.Add(this.dgvLocation);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(0, 503);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(1339, 192);
            this.groupBox4.TabIndex = 75;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "储位信息";
            // 
            // dgvLocation
            // 
            this.dgvLocation.AllowUserToAddRows = false;
            this.dgvLocation.AllowUserToDeleteRows = false;
            this.dgvLocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLocation.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvLocation.ColumnHeadersHeight = 29;
            this.dgvLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLocation.Location = new System.Drawing.Point(3, 23);
            this.dgvLocation.Margin = new System.Windows.Forms.Padding(1);
            this.dgvLocation.MultiSelect = false;
            this.dgvLocation.Name = "dgvLocation";
            this.dgvLocation.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLocation.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLocation.RowHeadersWidth = 30;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvLocation.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLocation.RowTemplate.Height = 27;
            this.dgvLocation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvLocation.Size = new System.Drawing.Size(1333, 167);
            this.dgvLocation.TabIndex = 98;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.cmbLocationRegion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblSAPWH);
            this.groupBox1.Controls.Add(this.lblRegion);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.labEntrancePort);
            this.groupBox1.Controls.Add(this.cmbWH);
            this.groupBox1.Controls.Add(this.labShipmentID);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 49);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1339, 95);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // cmbLocationRegion
            // 
            this.cmbLocationRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationRegion.FormattingEnabled = true;
            this.cmbLocationRegion.Items.AddRange(new object[] {
            "E3成品仓",
            "A8成品仓"});
            this.cmbLocationRegion.Location = new System.Drawing.Point(117, 56);
            this.cmbLocationRegion.Margin = new System.Windows.Forms.Padding(1);
            this.cmbLocationRegion.Name = "cmbLocationRegion";
            this.cmbLocationRegion.Size = new System.Drawing.Size(182, 26);
            this.cmbLocationRegion.TabIndex = 139;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 19);
            this.label4.TabIndex = 138;
            this.label4.Text = "REGION:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 19);
            this.label2.TabIndex = 137;
            this.label2.Text = "SAP仓:";
            // 
            // lblSAPWH
            // 
            this.lblSAPWH.AutoSize = true;
            this.lblSAPWH.Location = new System.Drawing.Point(411, 24);
            this.lblSAPWH.Name = "lblSAPWH";
            this.lblSAPWH.Size = new System.Drawing.Size(0, 19);
            this.lblSAPWH.TabIndex = 136;
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(799, 56);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(69, 19);
            this.lblRegion.TabIndex = 135;
            this.lblRegion.Text = "label4";
            // 
            // cmbLocation
            // 
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(415, 53);
            this.cmbLocation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(259, 26);
            this.cmbLocation.Sorted = true;
            this.cmbLocation.TabIndex = 134;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            this.cmbLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbLocation_KeyDown);
            // 
            // labEntrancePort
            // 
            this.labEntrancePort.AutoSize = true;
            this.labEntrancePort.Location = new System.Drawing.Point(342, 59);
            this.labEntrancePort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labEntrancePort.Name = "labEntrancePort";
            this.labEntrancePort.Size = new System.Drawing.Size(57, 19);
            this.labEntrancePort.TabIndex = 132;
            this.labEntrancePort.Text = "储位:";
            // 
            // cmbWH
            // 
            this.cmbWH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWH.FormattingEnabled = true;
            this.cmbWH.Items.AddRange(new object[] {
            "E3成品仓",
            "A8成品仓"});
            this.cmbWH.Location = new System.Drawing.Point(117, 20);
            this.cmbWH.Margin = new System.Windows.Forms.Padding(1);
            this.cmbWH.Name = "cmbWH";
            this.cmbWH.Size = new System.Drawing.Size(182, 26);
            this.cmbWH.TabIndex = 119;
            this.cmbWH.SelectedIndexChanged += new System.EventHandler(this.cmbWH_SelectedIndexChanged);
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(55, 27);
            this.labShipmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(57, 19);
            this.labShipmentID.TabIndex = 114;
            this.labShipmentID.Text = "仓库:";
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1339, 49);
            this.labShow.TabIndex = 64;
            this.labShow.Text = "入库_OLD";
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
            this.TextMsg.Location = new System.Drawing.Point(0, 695);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1339, 54);
            this.TextMsg.TabIndex = 63;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fMain_old
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 749);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "fMain_old";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarton)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocation)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label labEntrancePort;
        private System.Windows.Forms.ComboBox cmbWH;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvLocation;
        private System.Windows.Forms.Label lblSAPWH;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvCarton;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFGName;
        private System.Windows.Forms.RadioButton rdoPallet;
        private System.Windows.Forms.RadioButton rdoCarton;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.ComboBox cmbLocationRegion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}

