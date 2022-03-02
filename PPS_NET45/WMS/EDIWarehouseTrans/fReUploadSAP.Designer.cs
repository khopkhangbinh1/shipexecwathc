namespace EDIWareHouseTrans
{
    partial class fReUploadSAP
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
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.TextMsg = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbltrancef = new System.Windows.Forms.Label();
            this.txtTranceFlag = new System.Windows.Forms.TextBox();
            this.dt_end = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dt_start = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSAPNO = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPallet = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPallet)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.HotTrack;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.Font = new System.Drawing.Font("SimSun", 29F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTitle.ForeColor = System.Drawing.SystemColors.Info;
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(1208, 53);
            this.txtTitle.TabIndex = 66;
            this.txtTitle.Text = "补送SAP";
            this.txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextMsg
            // 
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 538);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1208, 36);
            this.TextMsg.TabIndex = 67;
            this.TextMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextMsg_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.lbltrancef);
            this.groupBox1.Controls.Add(this.txtTranceFlag);
            this.groupBox1.Controls.Add(this.dt_end);
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.dt_start);
            this.groupBox1.Controls.Add(this.lblStart);
            this.groupBox1.Controls.Add(this.btnUpload);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSAPNO);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1208, 155);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "更新";
            // 
            // lbltrancef
            // 
            this.lbltrancef.AutoSize = true;
            this.lbltrancef.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.lbltrancef.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbltrancef.Location = new System.Drawing.Point(485, 86);
            this.lbltrancef.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbltrancef.Name = "lbltrancef";
            this.lbltrancef.Size = new System.Drawing.Size(89, 18);
            this.lbltrancef.TabIndex = 98;
            this.lbltrancef.Text = "转仓完成:";
            // 
            // txtTranceFlag
            // 
            this.txtTranceFlag.BackColor = System.Drawing.SystemColors.Control;
            this.txtTranceFlag.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTranceFlag.ForeColor = System.Drawing.Color.Green;
            this.txtTranceFlag.Location = new System.Drawing.Point(598, 81);
            this.txtTranceFlag.Margin = new System.Windows.Forms.Padding(5);
            this.txtTranceFlag.Name = "txtTranceFlag";
            this.txtTranceFlag.ReadOnly = true;
            this.txtTranceFlag.Size = new System.Drawing.Size(95, 30);
            this.txtTranceFlag.TabIndex = 99;
            // 
            // dt_end
            // 
            this.dt_end.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_end.CustomFormat = "yyyy-MM-dd";
            this.dt_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_end.Location = new System.Drawing.Point(126, 79);
            this.dt_end.Margin = new System.Windows.Forms.Padding(4);
            this.dt_end.Name = "dt_end";
            this.dt_end.Size = new System.Drawing.Size(134, 28);
            this.dt_end.TabIndex = 95;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(70, 83);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(38, 19);
            this.lblEnd.TabIndex = 96;
            this.lblEnd.Text = "至:";
            // 
            // dt_start
            // 
            this.dt_start.CalendarMonthBackground = System.Drawing.SystemColors.ControlLightLight;
            this.dt_start.CustomFormat = "yyyy-MM-dd";
            this.dt_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_start.Location = new System.Drawing.Point(125, 33);
            this.dt_start.Margin = new System.Windows.Forms.Padding(4);
            this.dt_start.Name = "dt_start";
            this.dt_start.Size = new System.Drawing.Size(135, 28);
            this.dt_start.TabIndex = 97;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(9, 41);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(95, 19);
            this.lblStart.TabIndex = 94;
            this.lblStart.Text = "开始日期:";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(968, 25);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(125, 36);
            this.btnUpload.TabIndex = 84;
            this.btnUpload.Text = "UPLOAD SAP";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(291, 54);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(125, 36);
            this.btnSearch.TabIndex = 83;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(485, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 74;
            this.label4.Text = "SAPNO:";
            // 
            // txtSAPNO
            // 
            this.txtSAPNO.BackColor = System.Drawing.SystemColors.Control;
            this.txtSAPNO.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSAPNO.ForeColor = System.Drawing.Color.Green;
            this.txtSAPNO.Location = new System.Drawing.Point(598, 28);
            this.txtSAPNO.Margin = new System.Windows.Forms.Padding(5);
            this.txtSAPNO.Name = "txtSAPNO";
            this.txtSAPNO.ReadOnly = true;
            this.txtSAPNO.Size = new System.Drawing.Size(248, 30);
            this.txtSAPNO.TabIndex = 75;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvPallet);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1208, 330);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果清单";
            // 
            // dgvPallet
            // 
            this.dgvPallet.BackgroundColor = System.Drawing.Color.White;
            this.dgvPallet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPallet.Location = new System.Drawing.Point(3, 18);
            this.dgvPallet.Name = "dgvPallet";
            this.dgvPallet.RowHeadersWidth = 51;
            this.dgvPallet.RowTemplate.Height = 27;
            this.dgvPallet.Size = new System.Drawing.Size(1202, 309);
            this.dgvPallet.TabIndex = 0;
            this.dgvPallet.SelectionChanged += new System.EventHandler(this.dgvPallet_SelectionChanged);
            // 
            // fReUploadSAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 574);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.txtTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fReUploadSAP";
            this.Text = "fReUploadSAP";
            this.Load += new System.EventHandler(this.fReUploadSAP_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPallet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox TextMsg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvPallet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSAPNO;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.DateTimePicker dt_end;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dt_start;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lbltrancef;
        private System.Windows.Forms.TextBox txtTranceFlag;
    }
}