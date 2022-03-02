namespace Shipment
{
    partial class reBatchPrintHM
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
            this.Shipment_T = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lbldate = new System.Windows.Forms.Label();
            this.lblchepai = new System.Windows.Forms.Label();
            this.cmbCarNo = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvCarSIDList = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarSIDList)).BeginInit();
            this.SuspendLayout();
            // 
            // Shipment_T
            // 
            this.Shipment_T.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Shipment_T.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Shipment_T.Dock = System.Windows.Forms.DockStyle.Top;
            this.Shipment_T.Font = new System.Drawing.Font("宋体", 29F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Shipment_T.ForeColor = System.Drawing.SystemColors.Info;
            this.Shipment_T.Location = new System.Drawing.Point(0, 0);
            this.Shipment_T.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Shipment_T.Multiline = true;
            this.Shipment_T.Name = "Shipment_T";
            this.Shipment_T.ReadOnly = true;
            this.Shipment_T.Size = new System.Drawing.Size(934, 50);
            this.Shipment_T.TabIndex = 66;
            this.Shipment_T.Text = "批量列印HandoverManifest";
            this.Shipment_T.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 401);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(934, 50);
            this.TextMsg.TabIndex = 67;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd ";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(224, 21);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(151, 25);
            this.dtpStartTime.TabIndex = 88;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // lbldate
            // 
            this.lbldate.AutoSize = true;
            this.lbldate.Location = new System.Drawing.Point(150, 25);
            this.lbldate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(53, 15);
            this.lbldate.TabIndex = 87;
            this.lbldate.Text = "日 期:";
            // 
            // lblchepai
            // 
            this.lblchepai.AutoSize = true;
            this.lblchepai.Location = new System.Drawing.Point(141, 71);
            this.lblchepai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblchepai.Name = "lblchepai";
            this.lblchepai.Size = new System.Drawing.Size(60, 15);
            this.lblchepai.TabIndex = 90;
            this.lblchepai.Text = "车牌号:";
            // 
            // cmbCarNo
            // 
            this.cmbCarNo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbCarNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCarNo.FormattingEnabled = true;
            this.cmbCarNo.Items.AddRange(new object[] {
            "卡车车牌号码"});
            this.cmbCarNo.Location = new System.Drawing.Point(224, 63);
            this.cmbCarNo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCarNo.Name = "cmbCarNo";
            this.cmbCarNo.Size = new System.Drawing.Size(206, 28);
            this.cmbCarNo.TabIndex = 89;
            this.cmbCarNo.SelectedIndexChanged += new System.EventHandler(this.cmbCarNo_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(403, 14);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(125, 34);
            this.btnSearch.TabIndex = 104;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.lbldate);
            this.groupBox1.Controls.Add(this.lblchepai);
            this.groupBox1.Controls.Add(this.dtpStartTime);
            this.groupBox1.Controls.Add(this.cmbCarNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(934, 100);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "筛选";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvCarSIDList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(934, 251);
            this.groupBox2.TabIndex = 106;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "集货单清单";
            // 
            // dgvCarSIDList
            // 
            this.dgvCarSIDList.BackgroundColor = System.Drawing.Color.White;
            this.dgvCarSIDList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarSIDList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCarSIDList.Location = new System.Drawing.Point(3, 21);
            this.dgvCarSIDList.Name = "dgvCarSIDList";
            this.dgvCarSIDList.RowHeadersWidth = 51;
            this.dgvCarSIDList.RowTemplate.Height = 27;
            this.dgvCarSIDList.Size = new System.Drawing.Size(928, 227);
            this.dgvCarSIDList.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Location = new System.Drawing.Point(471, 63);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(125, 32);
            this.btnPrint.TabIndex = 105;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // reBatchPrintHM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 451);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.Shipment_T);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "reBatchPrintHM";
            this.Text = "reBatchPrintHM";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarSIDList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Shipment_T;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label lbldate;
        private System.Windows.Forms.Label lblchepai;
        private System.Windows.Forms.ComboBox cmbCarNo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvCarSIDList;
        private System.Windows.Forms.Button btnPrint;
    }
}