namespace EDIWareHouseTrans
{
    partial class fMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPickNO = new System.Windows.Forms.DataGridView();
            this.SAP_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LINE_ITEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PICK_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCATION_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_BATCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_VERSION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvOriStock = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtWarehouseNOOri = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWarehouseNOTra = new System.Windows.Forms.TextBox();
            this.checkOnlyinLocation = new System.Windows.Forms.CheckBox();
            this.lbWarehouseTarget = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.labEntrancePort = new System.Windows.Forms.Label();
            this.cmbLocationRegion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.chkHold = new System.Windows.Forms.CheckBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSapId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvNo = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.labqty = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.btnClsFace = new System.Windows.Forms.Button();
            this.cmbSAPid = new System.Windows.Forms.ComboBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbSTATUS = new System.Windows.Forms.ComboBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.labShipmentID = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickNO)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriStock)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPickNO
            // 
            this.dgvPickNO.AllowUserToAddRows = false;
            this.dgvPickNO.AllowUserToDeleteRows = false;
            this.dgvPickNO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPickNO.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPickNO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPickNO.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SAP_NO,
            this.LINE_ITEM,
            this.PART_NO,
            this.PART_DESC,
            this.QTY,
            this.PICK_QTY,
            this.STATUS,
            this.LOCATION_NO,
            this.PART_BATCH,
            this.PART_VERSION});
            this.dgvPickNO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPickNO.GridColor = System.Drawing.SystemColors.ControlText;
            this.dgvPickNO.Location = new System.Drawing.Point(0, 0);
            this.dgvPickNO.Margin = new System.Windows.Forms.Padding(5);
            this.dgvPickNO.MultiSelect = false;
            this.dgvPickNO.Name = "dgvPickNO";
            this.dgvPickNO.ReadOnly = true;
            this.dgvPickNO.RowHeadersWidth = 60;
            this.dgvPickNO.RowTemplate.Height = 23;
            this.dgvPickNO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPickNO.Size = new System.Drawing.Size(1605, 148);
            this.dgvPickNO.TabIndex = 125;
            this.dgvPickNO.SelectionChanged += new System.EventHandler(this.dgvPickNO_SelectionChanged);
            // 
            // SAP_NO
            // 
            this.SAP_NO.HeaderText = "SAP_NO";
            this.SAP_NO.MinimumWidth = 6;
            this.SAP_NO.Name = "SAP_NO";
            this.SAP_NO.ReadOnly = true;
            this.SAP_NO.Width = 93;
            // 
            // LINE_ITEM
            // 
            this.LINE_ITEM.HeaderText = "LINE_ITEM";
            this.LINE_ITEM.MinimumWidth = 6;
            this.LINE_ITEM.Name = "LINE_ITEM";
            this.LINE_ITEM.ReadOnly = true;
            this.LINE_ITEM.Width = 107;
            // 
            // PART_NO
            // 
            this.PART_NO.HeaderText = "PART_NO";
            this.PART_NO.MinimumWidth = 6;
            this.PART_NO.Name = "PART_NO";
            this.PART_NO.ReadOnly = true;
            this.PART_NO.Width = 103;
            // 
            // PART_DESC
            // 
            this.PART_DESC.HeaderText = "PART_DESC";
            this.PART_DESC.MinimumWidth = 6;
            this.PART_DESC.Name = "PART_DESC";
            this.PART_DESC.ReadOnly = true;
            this.PART_DESC.Width = 119;
            // 
            // QTY
            // 
            this.QTY.HeaderText = "QTY";
            this.QTY.MinimumWidth = 6;
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 66;
            // 
            // PICK_QTY
            // 
            this.PICK_QTY.HeaderText = "PICK_QTY";
            this.PICK_QTY.MinimumWidth = 6;
            this.PICK_QTY.Name = "PICK_QTY";
            this.PICK_QTY.ReadOnly = true;
            this.PICK_QTY.Width = 104;
            // 
            // STATUS
            // 
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.MinimumWidth = 6;
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Width = 92;
            // 
            // LOCATION_NO
            // 
            this.LOCATION_NO.HeaderText = "LOCATION_NO";
            this.LOCATION_NO.MinimumWidth = 6;
            this.LOCATION_NO.Name = "LOCATION_NO";
            this.LOCATION_NO.ReadOnly = true;
            this.LOCATION_NO.Width = 136;
            // 
            // PART_BATCH
            // 
            this.PART_BATCH.HeaderText = "PART_BATCH";
            this.PART_BATCH.MinimumWidth = 6;
            this.PART_BATCH.Name = "PART_BATCH";
            this.PART_BATCH.ReadOnly = true;
            this.PART_BATCH.Width = 128;
            // 
            // PART_VERSION
            // 
            this.PART_VERSION.HeaderText = "PART_VERSION";
            this.PART_VERSION.MinimumWidth = 6;
            this.PART_VERSION.Name = "PART_VERSION";
            this.PART_VERSION.ReadOnly = true;
            this.PART_VERSION.Width = 143;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvPickNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1605, 148);
            this.panel1.TabIndex = 118;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 299);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1605, 520);
            this.panel3.TabIndex = 133;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 243);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1605, 277);
            this.panel4.TabIndex = 126;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvStock);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Font = new System.Drawing.Font("NSimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(620, 29);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(620, 248);
            this.groupBox2.TabIndex = 132;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标库位信息";
            // 
            // dgvStock
            // 
            this.dgvStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvStock.BackgroundColor = System.Drawing.Color.White;
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStock.Location = new System.Drawing.Point(4, 21);
            this.dgvStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.RowHeadersWidth = 51;
            this.dgvStock.RowTemplate.Height = 27;
            this.dgvStock.Size = new System.Drawing.Size(612, 224);
            this.dgvStock.TabIndex = 101;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.dgvOriStock);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("NSimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(620, 248);
            this.groupBox1.TabIndex = 133;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原库位信息";
            // 
            // dgvOriStock
            // 
            this.dgvOriStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOriStock.BackgroundColor = System.Drawing.Color.White;
            this.dgvOriStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOriStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOriStock.Location = new System.Drawing.Point(4, 21);
            this.dgvOriStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvOriStock.Name = "dgvOriStock";
            this.dgvOriStock.RowHeadersWidth = 51;
            this.dgvOriStock.RowTemplate.Height = 27;
            this.dgvOriStock.Size = new System.Drawing.Size(612, 224);
            this.dgvOriStock.TabIndex = 101;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1605, 29);
            this.panel6.TabIndex = 134;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel7.Controls.Add(this.txtWarehouseNOOri);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.txtWarehouseNOTra);
            this.panel7.Controls.Add(this.checkOnlyinLocation);
            this.panel7.Controls.Add(this.lbWarehouseTarget);
            this.panel7.Controls.Add(this.cmbLocation);
            this.panel7.Controls.Add(this.labEntrancePort);
            this.panel7.Controls.Add(this.cmbLocationRegion);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.chkPrint);
            this.panel7.Controls.Add(this.chkHold);
            this.panel7.Controls.Add(this.btnUpload);
            this.panel7.Controls.Add(this.btnEnd);
            this.panel7.Controls.Add(this.btnStart);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.txtSapId);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.txtCarton);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1605, 95);
            this.panel7.TabIndex = 127;
            // 
            // txtWarehouseNOOri
            // 
            this.txtWarehouseNOOri.BackColor = System.Drawing.SystemColors.Control;
            this.txtWarehouseNOOri.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWarehouseNOOri.ForeColor = System.Drawing.Color.Black;
            this.txtWarehouseNOOri.Location = new System.Drawing.Point(441, 13);
            this.txtWarehouseNOOri.Margin = new System.Windows.Forms.Padding(5);
            this.txtWarehouseNOOri.Name = "txtWarehouseNOOri";
            this.txtWarehouseNOOri.ReadOnly = true;
            this.txtWarehouseNOOri.Size = new System.Drawing.Size(119, 30);
            this.txtWarehouseNOOri.TabIndex = 161;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label1.Location = new System.Drawing.Point(359, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 160;
            this.label1.Text = "原库:";
            // 
            // txtWarehouseNOTra
            // 
            this.txtWarehouseNOTra.BackColor = System.Drawing.SystemColors.Control;
            this.txtWarehouseNOTra.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWarehouseNOTra.ForeColor = System.Drawing.Color.Black;
            this.txtWarehouseNOTra.Location = new System.Drawing.Point(655, 13);
            this.txtWarehouseNOTra.Margin = new System.Windows.Forms.Padding(5);
            this.txtWarehouseNOTra.Name = "txtWarehouseNOTra";
            this.txtWarehouseNOTra.ReadOnly = true;
            this.txtWarehouseNOTra.Size = new System.Drawing.Size(119, 30);
            this.txtWarehouseNOTra.TabIndex = 159;
            // 
            // checkOnlyinLocation
            // 
            this.checkOnlyinLocation.AutoSize = true;
            this.checkOnlyinLocation.Checked = true;
            this.checkOnlyinLocation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkOnlyinLocation.Location = new System.Drawing.Point(1426, 20);
            this.checkOnlyinLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkOnlyinLocation.Name = "checkOnlyinLocation";
            this.checkOnlyinLocation.Size = new System.Drawing.Size(114, 21);
            this.checkOnlyinLocation.TabIndex = 158;
            this.checkOnlyinLocation.Text = "只显示空储位";
            this.checkOnlyinLocation.UseVisualStyleBackColor = true;
            this.checkOnlyinLocation.Visible = false;
            // 
            // lbWarehouseTarget
            // 
            this.lbWarehouseTarget.AutoSize = true;
            this.lbWarehouseTarget.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.lbWarehouseTarget.Location = new System.Drawing.Point(565, 18);
            this.lbWarehouseTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbWarehouseTarget.Name = "lbWarehouseTarget";
            this.lbWarehouseTarget.Size = new System.Drawing.Size(71, 18);
            this.lbWarehouseTarget.TabIndex = 152;
            this.lbWarehouseTarget.Text = "目标库:";
            // 
            // cmbLocation
            // 
            this.cmbLocation.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(1162, 13);
            this.cmbLocation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(258, 28);
            this.cmbLocation.Sorted = true;
            this.cmbLocation.TabIndex = 151;
            // 
            // labEntrancePort
            // 
            this.labEntrancePort.AutoSize = true;
            this.labEntrancePort.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.labEntrancePort.Location = new System.Drawing.Point(1061, 18);
            this.labEntrancePort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labEntrancePort.Name = "labEntrancePort";
            this.labEntrancePort.Size = new System.Drawing.Size(89, 18);
            this.labEntrancePort.TabIndex = 150;
            this.labEntrancePort.Text = "目标储位:";
            // 
            // cmbLocationRegion
            // 
            this.cmbLocationRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationRegion.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLocationRegion.FormattingEnabled = true;
            this.cmbLocationRegion.Items.AddRange(new object[] {
            "-ALL-",
            "AMR",
            "EMEIA",
            "PAC"});
            this.cmbLocationRegion.Location = new System.Drawing.Point(910, 13);
            this.cmbLocationRegion.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLocationRegion.Name = "cmbLocationRegion";
            this.cmbLocationRegion.Size = new System.Drawing.Size(138, 28);
            this.cmbLocationRegion.TabIndex = 149;
            this.cmbLocationRegion.SelectedIndexChanged += new System.EventHandler(this.cmbLocationRegion_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label2.Location = new System.Drawing.Point(786, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 148;
            this.label2.Text = "目标REGION:";
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Location = new System.Drawing.Point(828, 66);
            this.chkPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(86, 21);
            this.chkPrint.TabIndex = 137;
            this.chkPrint.Text = "自动打印";
            this.chkPrint.UseVisualStyleBackColor = true;
            this.chkPrint.Visible = false;
            // 
            // chkHold
            // 
            this.chkHold.AutoSize = true;
            this.chkHold.Checked = true;
            this.chkHold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHold.Location = new System.Drawing.Point(728, 65);
            this.chkHold.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkHold.Name = "chkHold";
            this.chkHold.Size = new System.Drawing.Size(97, 21);
            this.chkHold.TabIndex = 136;
            this.chkHold.Text = "检查HOLD";
            this.chkHold.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Location = new System.Drawing.Point(1432, 50);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(125, 35);
            this.btnUpload.TabIndex = 122;
            this.btnUpload.Text = "UPLOAD SAP";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Location = new System.Drawing.Point(133, 53);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(100, 37);
            this.btnEnd.TabIndex = 105;
            this.btnEnd.Text = "结束作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStart.Location = new System.Drawing.Point(7, 55);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 35);
            this.btnStart.TabIndex = 104;
            this.btnStart.Text = "开始作业";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(4, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 18);
            this.label4.TabIndex = 72;
            this.label4.Text = "SAP出库单号:";
            // 
            // txtSapId
            // 
            this.txtSapId.BackColor = System.Drawing.SystemColors.Control;
            this.txtSapId.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSapId.ForeColor = System.Drawing.Color.Green;
            this.txtSapId.Location = new System.Drawing.Point(169, 13);
            this.txtSapId.Margin = new System.Windows.Forms.Padding(5);
            this.txtSapId.Name = "txtSapId";
            this.txtSapId.ReadOnly = true;
            this.txtSapId.Size = new System.Drawing.Size(183, 30);
            this.txtSapId.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(286, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 77;
            this.label3.Text = "序号/箱号:";
            // 
            // txtCarton
            // 
            this.txtCarton.BackColor = System.Drawing.Color.White;
            this.txtCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton.Enabled = false;
            this.txtCarton.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(394, 52);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(5);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(327, 38);
            this.txtCarton.TabIndex = 78;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 142);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1605, 157);
            this.panel2.TabIndex = 131;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox5.Controls.Add(this.dgvNo);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(1605, 157);
            this.groupBox5.TabIndex = 114;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "转仓单列表";
            // 
            // dgvNo
            // 
            this.dgvNo.AllowUserToAddRows = false;
            this.dgvNo.AllowUserToDeleteRows = false;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("SimSun", 9F);
            this.dgvNo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvNo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNo.ColumnHeadersHeight = 29;
            this.dgvNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNo.Location = new System.Drawing.Point(3, 22);
            this.dgvNo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNo.MultiSelect = false;
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("NSimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNo.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvNo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            this.dgvNo.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvNo.RowTemplate.Height = 27;
            this.dgvNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNo.Size = new System.Drawing.Size(1599, 133);
            this.dgvNo.TabIndex = 97;
            this.dgvNo.SelectionChanged += new System.EventHandler(this.dgvNo_SelectionChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.labqty);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(1207, 17);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(395, 74);
            this.panel5.TabIndex = 118;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(85, 9);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(242, 27);
            this.label12.TabIndex = 89;
            this.label12.Text = "已刷箱数/需要箱数";
            // 
            // labqty
            // 
            this.labqty.Font = new System.Drawing.Font("SimSun", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labqty.ForeColor = System.Drawing.Color.Blue;
            this.labqty.Location = new System.Drawing.Point(7, 36);
            this.labqty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labqty.Name = "labqty";
            this.labqty.Size = new System.Drawing.Size(381, 33);
            this.labqty.TabIndex = 90;
            this.labqty.Text = "00/00";
            this.labqty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(749, 15);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(125, 35);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(331, 52);
            this.dt_end.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(127, 22);
            this.dt_end.TabIndex = 10;
            // 
            // btnClsFace
            // 
            this.btnClsFace.Location = new System.Drawing.Point(749, 53);
            this.btnClsFace.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClsFace.Name = "btnClsFace";
            this.btnClsFace.Size = new System.Drawing.Size(125, 35);
            this.btnClsFace.TabIndex = 85;
            this.btnClsFace.Text = "重置";
            this.btnClsFace.UseVisualStyleBackColor = true;
            this.btnClsFace.Click += new System.EventHandler(this.btnClsFace_Click);
            // 
            // cmbSAPid
            // 
            this.cmbSAPid.FormattingEnabled = true;
            this.cmbSAPid.Location = new System.Drawing.Point(136, 22);
            this.cmbSAPid.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSAPid.Name = "cmbSAPid";
            this.cmbSAPid.Size = new System.Drawing.Size(257, 24);
            this.cmbSAPid.TabIndex = 104;
            this.cmbSAPid.Text = "ALL";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(290, 59);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(26, 17);
            this.lblEnd.TabIndex = 92;
            this.lblEnd.Text = "至:";
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(136, 53);
            this.dt_start.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(135, 22);
            this.dt_start.TabIndex = 93;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.panel5);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.dt_end);
            this.groupBox3.Controls.Add(this.btnClsFace);
            this.groupBox3.Controls.Add(this.cmbSAPid);
            this.groupBox3.Controls.Add(this.lblEnd);
            this.groupBox3.Controls.Add(this.dt_start);
            this.groupBox3.Controls.Add(this.cmbSTATUS);
            this.groupBox3.Controls.Add(this.lblStart);
            this.groupBox3.Controls.Add(this.labShipmentID);
            this.groupBox3.Controls.Add(this.labStatus);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 49);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(1605, 93);
            this.groupBox3.TabIndex = 129;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // cmbSTATUS
            // 
            this.cmbSTATUS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSTATUS.FormattingEnabled = true;
            this.cmbSTATUS.Items.AddRange(new object[] {
            "ALL",
            "WP-未开始",
            "IP-作业中",
            "FP-已完成",
            "CP-CANCEL",
            "HO-HOLD"});
            this.cmbSTATUS.Location = new System.Drawing.Point(578, 55);
            this.cmbSTATUS.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbSTATUS.Name = "cmbSTATUS";
            this.cmbSTATUS.Size = new System.Drawing.Size(114, 24);
            this.cmbSTATUS.TabIndex = 101;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(12, 55);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(68, 17);
            this.lblStart.TabIndex = 9;
            this.lblStart.Text = "开始日期:";
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(12, 25);
            this.labShipmentID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(95, 17);
            this.labShipmentID.TabIndex = 13;
            this.labShipmentID.Text = "SAP转库单号:";
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(467, 59);
            this.labStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(68, 17);
            this.labStatus.TabIndex = 11;
            this.labStatus.Text = "单号状态:";
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Font = new System.Drawing.Font("SimSun", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.labShow.Name = "labShow";
            this.labShow.ReadOnly = true;
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1605, 49);
            this.labShow.TabIndex = 128;
            this.labShow.Text = "内部转仓";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextMsg
            // 
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 819);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1605, 46);
            this.TextMsg.TabIndex = 130;
            this.TextMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextMsg_KeyDown);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1605, 865);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labShow);
            this.Controls.Add(this.TextMsg);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "fMain";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickNO)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriStock)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNo)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvPickNO;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSapId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvNo;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labqty;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Button btnClsFace;
        private System.Windows.Forms.ComboBox cmbSAPid;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbSTATUS;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label labShipmentID;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.TextBox TextMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAP_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LINE_ITEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn PICK_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCATION_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_BATCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_VERSION;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.CheckBox chkHold;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.ComboBox cmbLocationRegion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label labEntrancePort;
        private System.Windows.Forms.Label lbWarehouseTarget;
        private System.Windows.Forms.CheckBox checkOnlyinLocation;
        private System.Windows.Forms.TextBox txtWarehouseNOTra;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvOriStock;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtWarehouseNOOri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
    }
}

