namespace MPartCheck
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCommit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lbSN = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTrolleyLine = new System.Windows.Forms.Label();
            this.tbSN = new System.Windows.Forms.TextBox();
            this.tbTrolleyLine = new System.Windows.Forms.TextBox();
            this.tbTrolley = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvTrolleyInfo = new System.Windows.Forms.DataGridView();
            this.TrolleyNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrolleyLineNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Customer_SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KeyPartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginPalletNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prgBanner.SuspendLayout();
            this.prgMain.SuspendLayout();
            this.prgFooter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrolleyInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(911, 383);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.btnCommit);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lbSN);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbTrolleyLine);
            this.groupBox1.Controls.Add(this.tbSN);
            this.groupBox1.Controls.Add(this.tbTrolleyLine);
            this.groupBox1.Controls.Add(this.tbTrolley);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(907, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCommit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCommit.Location = new System.Drawing.Point(659, 41);
            this.btnCommit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(84, 39);
            this.btnCommit.TabIndex = 2;
            this.btnCommit.Text = "Commit";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.Location = new System.Drawing.Point(783, 41);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(84, 39);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lbSN
            // 
            this.lbSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbSN.AutoSize = true;
            this.lbSN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSN.Location = new System.Drawing.Point(402, 30);
            this.lbSN.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSN.Name = "lbSN";
            this.lbSN.Size = new System.Drawing.Size(102, 20);
            this.lbSN.TabIndex = 1;
            this.lbSN.Text = "SN/CartonNo:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(215, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "TrolleyLineNo:";
            // 
            // lbTrolleyLine
            // 
            this.lbTrolleyLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTrolleyLine.AutoSize = true;
            this.lbTrolleyLine.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTrolleyLine.Location = new System.Drawing.Point(34, 25);
            this.lbTrolleyLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTrolleyLine.Name = "lbTrolleyLine";
            this.lbTrolleyLine.Size = new System.Drawing.Size(77, 20);
            this.lbTrolleyLine.TabIndex = 1;
            this.lbTrolleyLine.Text = "TrolleyNo:";
            // 
            // tbSN
            // 
            this.tbSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbSN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSN.Location = new System.Drawing.Point(404, 51);
            this.tbSN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbSN.Name = "tbSN";
            this.tbSN.Size = new System.Drawing.Size(245, 26);
            this.tbSN.TabIndex = 0;
            this.tbSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSN_KeyDown);
            // 
            // tbTrolleyLine
            // 
            this.tbTrolleyLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbTrolleyLine.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTrolleyLine.Location = new System.Drawing.Point(218, 51);
            this.tbTrolleyLine.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTrolleyLine.Name = "tbTrolleyLine";
            this.tbTrolleyLine.Size = new System.Drawing.Size(175, 26);
            this.tbTrolleyLine.TabIndex = 0;
            this.tbTrolleyLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTrolleyLine_KeyDown);
            // 
            // tbTrolley
            // 
            this.tbTrolley.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbTrolley.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTrolley.Location = new System.Drawing.Point(37, 51);
            this.tbTrolley.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTrolley.Name = "tbTrolley";
            this.tbTrolley.Size = new System.Drawing.Size(165, 26);
            this.tbTrolley.TabIndex = 0;
            this.tbTrolley.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTrolley_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.dgvTrolleyInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(2, 116);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(907, 265);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
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
            this.dgvTrolleyInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvTrolleyInfo.Name = "dgvTrolleyInfo";
            this.dgvTrolleyInfo.ReadOnly = true;
            this.dgvTrolleyInfo.RowHeadersWidth = 62;
            this.dgvTrolleyInfo.RowTemplate.Height = 30;
            this.dgvTrolleyInfo.Size = new System.Drawing.Size(903, 247);
            this.dgvTrolleyInfo.TabIndex = 0;
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
            // TrolleyLineNo
            // 
            this.TrolleyLineNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TrolleyLineNo.HeaderText = "TrolleyLineNo";
            this.TrolleyLineNo.MinimumWidth = 8;
            this.TrolleyLineNo.Name = "TrolleyLineNo";
            this.TrolleyLineNo.ReadOnly = true;
            this.TrolleyLineNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // Customer_SN
            // 
            this.Customer_SN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Customer_SN.HeaderText = "Customer_SN";
            this.Customer_SN.MinimumWidth = 8;
            this.Customer_SN.Name = "Customer_SN";
            this.Customer_SN.ReadOnly = true;
            this.Customer_SN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // OriginPalletNo
            // 
            this.OriginPalletNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OriginPalletNo.HeaderText = "OriginPalletNo";
            this.OriginPalletNo.MinimumWidth = 8;
            this.OriginPalletNo.Name = "OriginPalletNo";
            this.OriginPalletNo.ReadOnly = true;
            this.OriginPalletNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(2, 16);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(903, 247);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "TrolleyNo";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "TrolleyLineNo";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Check_Index";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Customer_SN";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "KeyPartNo";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "OriginPalletNo";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 499);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "fMain";
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTrolleyLine;
        private System.Windows.Forms.TextBox tbSN;
        private System.Windows.Forms.TextBox tbTrolleyLine;
        private System.Windows.Forms.TextBox tbTrolley;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lbSN;
        private System.Windows.Forms.DataGridView dgvTrolleyInfo;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrolleyNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrolleyLineNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Check_Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Customer_SN;
        private System.Windows.Forms.DataGridViewTextBoxColumn KeyPartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginPalletNo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}

