namespace EDIWareHouseOUT
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvWaitFBMESCarton = new System.Windows.Forms.DataGridView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnCheck = new System.Windows.Forms.Button();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.chkHold = new System.Windows.Forms.CheckBox();
            this.btnFBMESCarton = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPickSapID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSapId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rdoPart = new System.Windows.Forms.RadioButton();
            this.rdoPallet = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvNo = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.labqty = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.btnClsFace = new System.Windows.Forms.Button();
            this.cmbSAPid = new System.Windows.Forms.ComboBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTEST = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCarton2 = new System.Windows.Forms.TextBox();
            this.btnExport2 = new System.Windows.Forms.Button();
            this.cmbWHOutType = new System.Windows.Forms.ComboBox();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaitFBMESCarton)).BeginInit();
            this.panel7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.panel6.SuspendLayout();
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
            this.dgvPickNO.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPickNO.MultiSelect = false;
            this.dgvPickNO.Name = "dgvPickNO";
            this.dgvPickNO.ReadOnly = true;
            this.dgvPickNO.RowHeadersWidth = 60;
            this.dgvPickNO.RowTemplate.Height = 23;
            this.dgvPickNO.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPickNO.Size = new System.Drawing.Size(788, 105);
            this.dgvPickNO.TabIndex = 125;
            this.dgvPickNO.SelectionChanged += new System.EventHandler(this.dgvPickNO_SelectionChanged);
            // 
            // SAP_NO
            // 
            this.SAP_NO.HeaderText = "SAP_NO";
            this.SAP_NO.MinimumWidth = 6;
            this.SAP_NO.Name = "SAP_NO";
            this.SAP_NO.ReadOnly = true;
            this.SAP_NO.Width = 66;
            // 
            // LINE_ITEM
            // 
            this.LINE_ITEM.HeaderText = "LINE_ITEM";
            this.LINE_ITEM.MinimumWidth = 6;
            this.LINE_ITEM.Name = "LINE_ITEM";
            this.LINE_ITEM.ReadOnly = true;
            this.LINE_ITEM.Width = 84;
            // 
            // PART_NO
            // 
            this.PART_NO.HeaderText = "PART_NO";
            this.PART_NO.MinimumWidth = 6;
            this.PART_NO.Name = "PART_NO";
            this.PART_NO.ReadOnly = true;
            this.PART_NO.Width = 72;
            // 
            // PART_DESC
            // 
            this.PART_DESC.HeaderText = "PART_DESC";
            this.PART_DESC.MinimumWidth = 6;
            this.PART_DESC.Name = "PART_DESC";
            this.PART_DESC.ReadOnly = true;
            this.PART_DESC.Width = 84;
            // 
            // QTY
            // 
            this.QTY.HeaderText = "QTY";
            this.QTY.MinimumWidth = 6;
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 48;
            // 
            // PICK_QTY
            // 
            this.PICK_QTY.HeaderText = "PICK_QTY";
            this.PICK_QTY.MinimumWidth = 6;
            this.PICK_QTY.Name = "PICK_QTY";
            this.PICK_QTY.ReadOnly = true;
            this.PICK_QTY.Width = 78;
            // 
            // STATUS
            // 
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.MinimumWidth = 6;
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Width = 66;
            // 
            // LOCATION_NO
            // 
            this.LOCATION_NO.HeaderText = "LOCATION_NO";
            this.LOCATION_NO.MinimumWidth = 6;
            this.LOCATION_NO.Name = "LOCATION_NO";
            this.LOCATION_NO.ReadOnly = true;
            this.LOCATION_NO.Width = 96;
            // 
            // PART_BATCH
            // 
            this.PART_BATCH.HeaderText = "PART_BATCH";
            this.PART_BATCH.MinimumWidth = 6;
            this.PART_BATCH.Name = "PART_BATCH";
            this.PART_BATCH.ReadOnly = true;
            this.PART_BATCH.Width = 90;
            // 
            // PART_VERSION
            // 
            this.PART_VERSION.HeaderText = "PART_VERSION";
            this.PART_VERSION.MinimumWidth = 6;
            this.PART_VERSION.Name = "PART_VERSION";
            this.PART_VERSION.ReadOnly = true;
            this.PART_VERSION.Width = 102;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvPickNO);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 105);
            this.panel1.TabIndex = 118;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 321);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(788, 295);
            this.panel3.TabIndex = 133;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 207);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(788, 88);
            this.panel4.TabIndex = 128;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvWaitFBMESCarton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(788, 88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "未反馈MES的箱号List";
            // 
            // dgvWaitFBMESCarton
            // 
            this.dgvWaitFBMESCarton.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvWaitFBMESCarton.BackgroundColor = System.Drawing.Color.White;
            this.dgvWaitFBMESCarton.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWaitFBMESCarton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWaitFBMESCarton.Location = new System.Drawing.Point(2, 16);
            this.dgvWaitFBMESCarton.Margin = new System.Windows.Forms.Padding(2);
            this.dgvWaitFBMESCarton.Name = "dgvWaitFBMESCarton";
            this.dgvWaitFBMESCarton.RowHeadersWidth = 51;
            this.dgvWaitFBMESCarton.RowTemplate.Height = 27;
            this.dgvWaitFBMESCarton.Size = new System.Drawing.Size(784, 70);
            this.dgvWaitFBMESCarton.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel7.Controls.Add(this.btnCheck);
            this.panel7.Controls.Add(this.chkPrint);
            this.panel7.Controls.Add(this.chkHold);
            this.panel7.Controls.Add(this.btnFBMESCarton);
            this.panel7.Controls.Add(this.btnUpload);
            this.panel7.Controls.Add(this.btnExport);
            this.panel7.Controls.Add(this.btnEnd);
            this.panel7.Controls.Add(this.btnStart);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.txtPickSapID);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.txtSapId);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.txtCarton);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(788, 102);
            this.panel7.TabIndex = 127;
            // 
            // btnCheck
            // 
            this.btnCheck.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheck.Location = new System.Drawing.Point(618, 9);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 27);
            this.btnCheck.TabIndex = 138;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Location = new System.Drawing.Point(241, 80);
            this.chkPrint.Margin = new System.Windows.Forms.Padding(2);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(72, 16);
            this.chkPrint.TabIndex = 137;
            this.chkPrint.Text = "自动打印";
            this.chkPrint.UseVisualStyleBackColor = true;
            // 
            // chkHold
            // 
            this.chkHold.AutoSize = true;
            this.chkHold.Checked = true;
            this.chkHold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHold.Location = new System.Drawing.Point(102, 80);
            this.chkHold.Margin = new System.Windows.Forms.Padding(2);
            this.chkHold.Name = "chkHold";
            this.chkHold.Size = new System.Drawing.Size(72, 16);
            this.chkHold.TabIndex = 136;
            this.chkHold.Text = "检查HOLD";
            this.chkHold.UseVisualStyleBackColor = true;
            // 
            // btnFBMESCarton
            // 
            this.btnFBMESCarton.Location = new System.Drawing.Point(534, 51);
            this.btnFBMESCarton.Margin = new System.Windows.Forms.Padding(2);
            this.btnFBMESCarton.Name = "btnFBMESCarton";
            this.btnFBMESCarton.Size = new System.Drawing.Size(94, 27);
            this.btnFBMESCarton.TabIndex = 123;
            this.btnFBMESCarton.Text = "反馈MES";
            this.btnFBMESCarton.UseVisualStyleBackColor = true;
            this.btnFBMESCarton.Click += new System.EventHandler(this.btnFBMESCarton_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(632, 52);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(94, 27);
            this.btnUpload.TabIndex = 122;
            this.btnUpload.Text = "UPLOAD SAP";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExport.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(708, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(74, 26);
            this.btnExport.TabIndex = 105;
            this.btnExport.Text = "导出excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Enabled = false;
            this.btnEnd.Location = new System.Drawing.Point(454, 51);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 28);
            this.btnEnd.TabIndex = 105;
            this.btnEnd.Text = "结束作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnStart.Location = new System.Drawing.Point(348, 52);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 27);
            this.btnStart.TabIndex = 104;
            this.btnStart.Text = "开始作业";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(270, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(98, 14);
            this.label16.TabIndex = 102;
            this.label16.Text = "批次出货单号:";
            // 
            // txtPickSapID
            // 
            this.txtPickSapID.Enabled = false;
            this.txtPickSapID.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPickSapID.ForeColor = System.Drawing.Color.Blue;
            this.txtPickSapID.Location = new System.Drawing.Point(385, 9);
            this.txtPickSapID.Margin = new System.Windows.Forms.Padding(4);
            this.txtPickSapID.Name = "txtPickSapID";
            this.txtPickSapID.ReadOnly = true;
            this.txtPickSapID.Size = new System.Drawing.Size(221, 32);
            this.txtPickSapID.TabIndex = 103;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(4, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 72;
            this.label4.Text = "SAP出库单号:";
            // 
            // txtSapId
            // 
            this.txtSapId.BackColor = System.Drawing.SystemColors.Control;
            this.txtSapId.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSapId.ForeColor = System.Drawing.Color.Green;
            this.txtSapId.Location = new System.Drawing.Point(124, 14);
            this.txtSapId.Margin = new System.Windows.Forms.Padding(4);
            this.txtSapId.Name = "txtSapId";
            this.txtSapId.Size = new System.Drawing.Size(138, 26);
            this.txtSapId.TabIndex = 73;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(6, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
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
            this.txtCarton.Location = new System.Drawing.Point(124, 46);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(211, 32);
            this.txtCarton.TabIndex = 78;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvStock);
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Font = new System.Drawing.Font("NSimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(788, 321);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 295);
            this.groupBox2.TabIndex = 132;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "库位信息";
            // 
            // dgvStock
            // 
            this.dgvStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvStock.BackgroundColor = System.Drawing.Color.White;
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStock.Location = new System.Drawing.Point(3, 52);
            this.dgvStock.Margin = new System.Windows.Forms.Padding(2);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.RowHeadersWidth = 51;
            this.dgvStock.RowTemplate.Height = 27;
            this.dgvStock.Size = new System.Drawing.Size(489, 240);
            this.dgvStock.TabIndex = 101;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.rdoPart);
            this.panel6.Controls.Add(this.rdoPallet);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 21);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(489, 31);
            this.panel6.TabIndex = 100;
            // 
            // rdoPart
            // 
            this.rdoPart.AutoSize = true;
            this.rdoPart.Font = new System.Drawing.Font("NSimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoPart.Location = new System.Drawing.Point(158, 6);
            this.rdoPart.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPart.Name = "rdoPart";
            this.rdoPart.Size = new System.Drawing.Size(88, 16);
            this.rdoPart.TabIndex = 1;
            this.rdoPart.Text = "按料号显示";
            this.rdoPart.UseVisualStyleBackColor = true;
            // 
            // rdoPallet
            // 
            this.rdoPallet.AutoSize = true;
            this.rdoPallet.Checked = true;
            this.rdoPallet.Font = new System.Drawing.Font("NSimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoPallet.Location = new System.Drawing.Point(20, 6);
            this.rdoPallet.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPallet.Name = "rdoPallet";
            this.rdoPallet.Size = new System.Drawing.Size(88, 16);
            this.rdoPallet.TabIndex = 0;
            this.rdoPallet.TabStop = true;
            this.rdoPallet.Text = "按单号显示";
            this.rdoPallet.UseVisualStyleBackColor = true;
            this.rdoPallet.CheckedChanged += new System.EventHandler(this.rdoPallet_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 152);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1283, 169);
            this.panel2.TabIndex = 131;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox5.Controls.Add(this.dgvNo);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("NSimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(1283, 169);
            this.groupBox5.TabIndex = 114;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SAP出库单号列表";
            // 
            // dgvNo
            // 
            this.dgvNo.AllowUserToAddRows = false;
            this.dgvNo.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F);
            this.dgvNo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvNo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNo.ColumnHeadersHeight = 29;
            this.dgvNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNo.Location = new System.Drawing.Point(2, 20);
            this.dgvNo.Margin = new System.Windows.Forms.Padding(1);
            this.dgvNo.MultiSelect = false;
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("NSimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNo.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvNo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.dgvNo.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvNo.RowTemplate.Height = 27;
            this.dgvNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNo.Size = new System.Drawing.Size(1279, 147);
            this.dgvNo.TabIndex = 97;
            this.dgvNo.SelectionChanged += new System.EventHandler(this.dgvNo_SelectionChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.labqty);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(985, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(296, 93);
            this.panel5.TabIndex = 118;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("SimSun", 16F);
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(64, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(197, 22);
            this.label12.TabIndex = 89;
            this.label12.Text = "已刷箱数/需要箱数";
            // 
            // labqty
            // 
            this.labqty.Font = new System.Drawing.Font("SimSun", 32F, System.Drawing.FontStyle.Bold);
            this.labqty.ForeColor = System.Drawing.Color.Blue;
            this.labqty.Location = new System.Drawing.Point(3, 42);
            this.labqty.Name = "labqty";
            this.labqty.Size = new System.Drawing.Size(286, 43);
            this.labqty.TabIndex = 90;
            this.labqty.Text = "00/00";
            this.labqty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(562, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 27);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 107;
            this.label14.Text = "出库类型:";
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(290, 72);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(96, 21);
            this.dt_end.TabIndex = 10;
            // 
            // btnClsFace
            // 
            this.btnClsFace.Location = new System.Drawing.Point(562, 47);
            this.btnClsFace.Name = "btnClsFace";
            this.btnClsFace.Size = new System.Drawing.Size(94, 27);
            this.btnClsFace.TabIndex = 85;
            this.btnClsFace.Text = "重置";
            this.btnClsFace.UseVisualStyleBackColor = true;
            this.btnClsFace.Click += new System.EventHandler(this.btnClsFace_Click);
            // 
            // cmbSAPid
            // 
            this.cmbSAPid.FormattingEnabled = true;
            this.cmbSAPid.Location = new System.Drawing.Point(120, 44);
            this.cmbSAPid.Margin = new System.Windows.Forms.Padding(1);
            this.cmbSAPid.Name = "cmbSAPid";
            this.cmbSAPid.Size = new System.Drawing.Size(194, 20);
            this.cmbSAPid.TabIndex = 104;
            this.cmbSAPid.Text = "ALL";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(256, 78);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(23, 12);
            this.lblEnd.TabIndex = 92;
            this.lblEnd.Text = "至:";
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(120, 72);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(102, 21);
            this.dt_start.TabIndex = 93;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.btnTEST);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtCarton2);
            this.groupBox3.Controls.Add(this.btnExport2);
            this.groupBox3.Controls.Add(this.cmbWHOutType);
            this.groupBox3.Controls.Add(this.panel5);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.label14);
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
            this.groupBox3.Location = new System.Drawing.Point(0, 41);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1283, 111);
            this.groupBox3.TabIndex = 129;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "筛选";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // btnTEST
            // 
            this.btnTEST.Enabled = false;
            this.btnTEST.Location = new System.Drawing.Point(821, 19);
            this.btnTEST.Name = "btnTEST";
            this.btnTEST.Size = new System.Drawing.Size(94, 27);
            this.btnTEST.TabIndex = 125;
            this.btnTEST.Text = "TEST";
            this.btnTEST.UseVisualStyleBackColor = true;
            this.btnTEST.Visible = false;
            this.btnTEST.Click += new System.EventHandler(this.btnTEST_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(689, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 14);
            this.label1.TabIndex = 124;
            this.label1.Text = "补列印Pick栈板号:";
            // 
            // txtCarton2
            // 
            this.txtCarton2.BackColor = System.Drawing.Color.White;
            this.txtCarton2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton2.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton2.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton2.Location = new System.Drawing.Point(692, 65);
            this.txtCarton2.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarton2.Name = "txtCarton2";
            this.txtCarton2.Size = new System.Drawing.Size(211, 32);
            this.txtCarton2.TabIndex = 123;
            this.txtCarton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton2_KeyDown);
            // 
            // btnExport2
            // 
            this.btnExport2.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnExport2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport2.Location = new System.Drawing.Point(562, 75);
            this.btnExport2.Name = "btnExport2";
            this.btnExport2.Size = new System.Drawing.Size(94, 26);
            this.btnExport2.TabIndex = 122;
            this.btnExport2.Text = "导出excel";
            this.btnExport2.UseVisualStyleBackColor = true;
            this.btnExport2.Click += new System.EventHandler(this.btnExport2_Click);
            // 
            // cmbWHOutType
            // 
            this.cmbWHOutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWHOutType.FormattingEnabled = true;
            this.cmbWHOutType.Items.AddRange(new object[] {
            "工单领退出库",
            "杂收发出库",
            "转仓出库"});
            this.cmbWHOutType.Location = new System.Drawing.Point(120, 17);
            this.cmbWHOutType.Margin = new System.Windows.Forms.Padding(1);
            this.cmbWHOutType.Name = "cmbWHOutType";
            this.cmbWHOutType.Size = new System.Drawing.Size(194, 20);
            this.cmbWHOutType.TabIndex = 121;
            this.cmbWHOutType.SelectedIndexChanged += new System.EventHandler(this.cmbWHOutType_SelectedIndexChanged);
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
            this.cmbSTATUS.Location = new System.Drawing.Point(448, 44);
            this.cmbSTATUS.Name = "cmbSTATUS";
            this.cmbSTATUS.Size = new System.Drawing.Size(87, 20);
            this.cmbSTATUS.TabIndex = 101;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(7, 76);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(59, 12);
            this.lblStart.TabIndex = 9;
            this.lblStart.Text = "开始日期:";
            // 
            // labShipmentID
            // 
            this.labShipmentID.AutoSize = true;
            this.labShipmentID.Location = new System.Drawing.Point(4, 46);
            this.labShipmentID.Name = "labShipmentID";
            this.labShipmentID.Size = new System.Drawing.Size(77, 12);
            this.labShipmentID.TabIndex = 13;
            this.labShipmentID.Text = "SAP出库单号:";
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(348, 47);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(59, 12);
            this.labStatus.TabIndex = 11;
            this.labStatus.Text = "单号状态:";
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
            this.labShow.Size = new System.Drawing.Size(1283, 41);
            this.labShow.TabIndex = 128;
            this.labShow.Text = "出库";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextMsg
            // 
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 616);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1283, 38);
            this.TextMsg.TabIndex = 130;
            this.TextMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextMsg_KeyDown);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 654);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labShow);
            this.Controls.Add(this.TextMsg);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fMain";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickNO)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaitFBMESCarton)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
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
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPickSapID;
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
        private System.Windows.Forms.Label label14;
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
        private System.Windows.Forms.ComboBox cmbWHOutType;
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
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvWaitFBMESCarton;
        private System.Windows.Forms.Button btnFBMESCarton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkHold;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rdoPart;
        private System.Windows.Forms.RadioButton rdoPallet;
        private System.Windows.Forms.Button btnExport2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCarton2;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.Button btnTEST;
        private System.Windows.Forms.Button btnCheck;
    }
}

