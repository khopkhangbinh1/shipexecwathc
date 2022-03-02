namespace MPartCheck
{
    partial class CreateSN
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCommit = new System.Windows.Forms.Button();
            this.lbTrolleyLine = new System.Windows.Forms.Label();
            this.tbDN = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OriginPalletNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer_SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrolleyLineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrolleyNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTrolleyInfo = new System.Windows.Forms.DataGridView();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrolleyInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // prgBanner
            // 
            this.prgBanner.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.prgBanner.Size = new System.Drawing.Size(913, 52);
            // 
            // prgMain
            // 
            this.prgMain.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.prgMain.Controls.Add(this.tableLayoutPanel1);
            this.prgMain.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.prgMain.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.prgMain.Size = new System.Drawing.Size(913, 399);
            // 
            // prgTitle
            // 
            this.prgTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.prgTitle.Size = new System.Drawing.Size(913, 52);
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 451);
            this.prgFooter.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.prgFooter.Size = new System.Drawing.Size(913, 48);
            // 
            // prgMSG
            // 
            this.prgMSG.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.prgMSG.Margin = new System.Windows.Forms.Padding(2, 1, 2, 0);
            this.prgMSG.Size = new System.Drawing.Size(913, 48);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 15);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.30809F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.69191F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 383);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.btnCommit);
            this.groupBox1.Controls.Add(this.lbTrolleyLine);
            this.groupBox1.Controls.Add(this.tbDN);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(907, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCommit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCommit.Location = new System.Drawing.Point(536, 83);
            this.btnCommit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(84, 39);
            this.btnCommit.TabIndex = 2;
            this.btnCommit.Text = "Create";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // lbTrolleyLine
            // 
            this.lbTrolleyLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTrolleyLine.AutoSize = true;
            this.lbTrolleyLine.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTrolleyLine.Location = new System.Drawing.Point(8, 16);
            this.lbTrolleyLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTrolleyLine.Name = "lbTrolleyLine";
            this.lbTrolleyLine.Size = new System.Drawing.Size(34, 20);
            this.lbTrolleyLine.TabIndex = 1;
            this.lbTrolleyLine.Text = "DN:";
            // 
            // tbDN
            // 
            this.tbDN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbDN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbDN.Location = new System.Drawing.Point(12, 39);
            this.tbDN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDN.Multiline = true;
            this.tbDN.Name = "tbDN";
            this.tbDN.Size = new System.Drawing.Size(444, 143);
            this.tbDN.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Controls.Add(this.dgvTrolleyInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(2, 210);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(907, 171);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // OriginPalletNo
            // 
            this.OriginPalletNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OriginPalletNo.HeaderText = "OriginPalletNo";
            this.OriginPalletNo.MinimumWidth = 8;
            this.OriginPalletNo.Name = "OriginPalletNo";
            this.OriginPalletNo.ReadOnly = true;
            this.OriginPalletNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KeyPartNo
            // 
            this.KeyPartNo.HeaderText = "KeyPartNo";
            this.KeyPartNo.MinimumWidth = 8;
            this.KeyPartNo.Name = "KeyPartNo";
            this.KeyPartNo.ReadOnly = true;
            this.KeyPartNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KeyPartNo.Width = 150;
            // 
            // Customer_SN
            // 
            this.Customer_SN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Customer_SN.HeaderText = "Customer_SN";
            this.Customer_SN.MinimumWidth = 8;
            this.Customer_SN.Name = "Customer_SN";
            this.Customer_SN.ReadOnly = true;
            this.Customer_SN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Check_Index
            // 
            this.Check_Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Check_Index.HeaderText = "Check_Index";
            this.Check_Index.MinimumWidth = 8;
            this.Check_Index.Name = "Check_Index";
            this.Check_Index.ReadOnly = true;
            this.Check_Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TrolleyLineNo
            // 
            this.TrolleyLineNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TrolleyLineNo.HeaderText = "TrolleyLineNo";
            this.TrolleyLineNo.MinimumWidth = 8;
            this.TrolleyLineNo.Name = "TrolleyLineNo";
            this.TrolleyLineNo.ReadOnly = true;
            this.TrolleyLineNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TrolleyNo
            // 
            this.TrolleyNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TrolleyNo.HeaderText = "TrolleyNo";
            this.TrolleyNo.MinimumWidth = 8;
            this.TrolleyNo.Name = "TrolleyNo";
            this.TrolleyNo.ReadOnly = true;
            this.TrolleyNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvTrolleyInfo
            // 
            this.dgvTrolleyInfo.AllowUserToAddRows = false;
            this.dgvTrolleyInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvTrolleyInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrolleyInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TrolleyNo,
            this.TrolleyLineNo,
            this.Check_Index,
            this.Customer_SN,
            this.KeyPartNo,
            this.OriginPalletNo});
            this.dgvTrolleyInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrolleyInfo.Location = new System.Drawing.Point(2, 16);
            this.dgvTrolleyInfo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTrolleyInfo.Name = "dgvTrolleyInfo";
            this.dgvTrolleyInfo.ReadOnly = true;
            this.dgvTrolleyInfo.RowHeadersWidth = 62;
            this.dgvTrolleyInfo.RowTemplate.Height = 30;
            this.dgvTrolleyInfo.Size = new System.Drawing.Size(903, 153);
            this.dgvTrolleyInfo.TabIndex = 0;
            // 
            // CreateSN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 499);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "CreateSN";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fMain_Load);
            this.prgBanner.ResumeLayout(false);
            this.prgMain.ResumeLayout(false);
            this.prgFooter.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrolleyInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbTrolleyLine;
        private System.Windows.Forms.TextBox tbDN;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.DataGridView dgvTrolleyInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrolleyNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrolleyLineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Check_Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer_SN;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyPartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginPalletNo;
    }
}

