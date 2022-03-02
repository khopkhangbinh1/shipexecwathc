namespace EDIWarehouseTools
{
    partial class fWMSPalletToCar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvFindResultFrom = new System.Windows.Forms.DataGridView();
            this.grpFrom = new System.Windows.Forms.GroupBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblSN = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCarNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLocationTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextMsg = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labShow = new System.Windows.Forms.TextBox();
            this.grpTo = new System.Windows.Forms.GroupBox();
            this.dgvFindResultTo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindResultFrom)).BeginInit();
            this.grpFrom.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindResultTo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFindResultFrom
            // 
            this.dgvFindResultFrom.AllowUserToAddRows = false;
            this.dgvFindResultFrom.AllowUserToDeleteRows = false;
            this.dgvFindResultFrom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFindResultFrom.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvFindResultFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFindResultFrom.ColumnHeadersHeight = 30;
            this.dgvFindResultFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFindResultFrom.Location = new System.Drawing.Point(3, 17);
            this.dgvFindResultFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFindResultFrom.MultiSelect = false;
            this.dgvFindResultFrom.Name = "dgvFindResultFrom";
            this.dgvFindResultFrom.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFindResultFrom.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFindResultFrom.RowHeadersWidth = 55;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvFindResultFrom.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFindResultFrom.RowTemplate.Height = 27;
            this.dgvFindResultFrom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFindResultFrom.Size = new System.Drawing.Size(514, 208);
            this.dgvFindResultFrom.TabIndex = 119;
            // 
            // grpFrom
            // 
            this.grpFrom.Controls.Add(this.dgvFindResultFrom);
            this.grpFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpFrom.Location = new System.Drawing.Point(0, 214);
            this.grpFrom.Name = "grpFrom";
            this.grpFrom.Size = new System.Drawing.Size(520, 228);
            this.grpFrom.TabIndex = 128;
            this.grpFrom.TabStop = false;
            this.grpFrom.Text = "箱号信息";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel8.Controls.Add(this.lblSN);
            this.panel8.Controls.Add(this.txtSN);
            this.panel8.Controls.Add(this.btnSearch);
            this.panel8.Controls.Add(this.btnEnd);
            this.panel8.Controls.Add(this.btnStart);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel8.Location = new System.Drawing.Point(0, 150);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(960, 64);
            this.panel8.TabIndex = 127;
            // 
            // lblSN
            // 
            this.lblSN.AutoSize = true;
            this.lblSN.Location = new System.Drawing.Point(128, 21);
            this.lblSN.Name = "lblSN";
            this.lblSN.Size = new System.Drawing.Size(89, 16);
            this.lblSN.TabIndex = 109;
            this.lblSN.Text = "CartonNo:";
            // 
            // txtSN
            // 
            this.txtSN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSN.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSN.ForeColor = System.Drawing.Color.Blue;
            this.txtSN.Location = new System.Drawing.Point(220, 15);
            this.txtSN.Margin = new System.Windows.Forms.Padding(4);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(258, 30);
            this.txtSN.TabIndex = 85;
            this.txtSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSN_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(806, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 30);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(757, 16);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(92, 30);
            this.btnEnd.TabIndex = 84;
            this.btnEnd.Text = "结束作业";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Visible = false;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(691, 16);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(92, 30);
            this.btnStart.TabIndex = 83;
            this.btnStart.Text = "开始作业";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.txtCarNo);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cmbLocationTo);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(0, 39);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(960, 111);
            this.groupBox3.TabIndex = 126;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // txtCarNo
            // 
            this.txtCarNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarNo.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarNo.ForeColor = System.Drawing.Color.Blue;
            this.txtCarNo.Location = new System.Drawing.Point(220, 24);
            this.txtCarNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarNo.Name = "txtCarNo";
            this.txtCarNo.Size = new System.Drawing.Size(188, 30);
            this.txtCarNo.TabIndex = 112;
            this.txtCarNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarNo_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 111;
            this.label2.Text = "目的车号:";
            // 
            // cmbLocationTo
            // 
            this.cmbLocationTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLocationTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLocationTo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbLocationTo.FormattingEnabled = true;
            this.cmbLocationTo.Location = new System.Drawing.Point(220, 67);
            this.cmbLocationTo.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLocationTo.Name = "cmbLocationTo";
            this.cmbLocationTo.Size = new System.Drawing.Size(258, 27);
            this.cmbLocationTo.TabIndex = 110;
            this.cmbLocationTo.SelectedIndexChanged += new System.EventHandler(this.cmbLocationTo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 16);
            this.label1.TabIndex = 109;
            this.label1.Text = "目的车行号:";
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.White;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(960, 44);
            this.TextMsg.TabIndex = 66;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(4, 446);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(105, 21);
            this.btnExport.TabIndex = 90;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TextMsg);
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(960, 44);
            this.panel3.TabIndex = 103;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 442);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 45);
            this.panel1.TabIndex = 125;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(960, 39);
            this.panel2.TabIndex = 124;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(960, 41);
            this.labShow.TabIndex = 61;
            this.labShow.Text = "栈板转精钢车";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpTo
            // 
            this.grpTo.Controls.Add(this.dgvFindResultTo);
            this.grpTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTo.Location = new System.Drawing.Point(520, 214);
            this.grpTo.Name = "grpTo";
            this.grpTo.Size = new System.Drawing.Size(440, 228);
            this.grpTo.TabIndex = 129;
            this.grpTo.TabStop = false;
            this.grpTo.Text = "目标储位信息";
            // 
            // dgvFindResultTo
            // 
            this.dgvFindResultTo.AllowUserToAddRows = false;
            this.dgvFindResultTo.AllowUserToDeleteRows = false;
            this.dgvFindResultTo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFindResultTo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvFindResultTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvFindResultTo.ColumnHeadersHeight = 30;
            this.dgvFindResultTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFindResultTo.Location = new System.Drawing.Point(3, 17);
            this.dgvFindResultTo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFindResultTo.MultiSelect = false;
            this.dgvFindResultTo.Name = "dgvFindResultTo";
            this.dgvFindResultTo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFindResultTo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFindResultTo.RowHeadersWidth = 55;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvFindResultTo.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFindResultTo.RowTemplate.Height = 27;
            this.dgvFindResultTo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFindResultTo.Size = new System.Drawing.Size(434, 208);
            this.dgvFindResultTo.TabIndex = 119;
            // 
            // fWMSPalletToCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 487);
            this.Controls.Add(this.grpTo);
            this.Controls.Add(this.grpFrom);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "fWMSPalletToCar";
            this.Text = "fWMSPalletToCar";
            this.Load += new System.EventHandler(this.fWMSPalletToCar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindResultFrom)).EndInit();
            this.grpFrom.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpTo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFindResultTo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvFindResultFrom;
        private System.Windows.Forms.GroupBox grpFrom;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblSN;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.ComboBox cmbLocationTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpTo;
        private System.Windows.Forms.DataGridView dgvFindResultTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCarNo;
    }
}