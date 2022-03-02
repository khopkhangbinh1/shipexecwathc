namespace MPartCheck
{
    partial class PpartCheckin
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
            this.prgBanner.Size = new System.Drawing.Size(1503, 78);
            // 
            // prgMain
            // 
            this.prgMain.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.prgMain.Controls.Add(this.tableLayoutPanel1);
            this.prgMain.Size = new System.Drawing.Size(1503, 646);
            // 
            // prgTitle
            // 
            this.prgTitle.Size = new System.Drawing.Size(1503, 78);
            // 
            // prgFooter
            // 
            this.prgFooter.Location = new System.Drawing.Point(0, 724);
            this.prgFooter.Size = new System.Drawing.Size(1503, 72);
            // 
            // prgMSG
            // 
            this.prgMSG.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.prgMSG.Size = new System.Drawing.Size(1503, 72);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1497, 619);
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
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1491, 179);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCommit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCommit.Location = new System.Drawing.Point(1118, 69);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(126, 59);
            this.btnCommit.TabIndex = 2;
            this.btnCommit.Text = "Commit";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnReset.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.Location = new System.Drawing.Point(1304, 69);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(126, 59);
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
            this.lbSN.Location = new System.Drawing.Point(603, 52);
            this.lbSN.Name = "lbSN";
            this.lbSN.Size = new System.Drawing.Size(153, 28);
            this.lbSN.TabIndex = 1;
            this.lbSN.Text = "SN/CartonNo:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(322, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "TrolleyLineNo:";
            // 
            // lbTrolleyLine
            // 
            this.lbTrolleyLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTrolleyLine.AutoSize = true;
            this.lbTrolleyLine.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTrolleyLine.Location = new System.Drawing.Point(51, 45);
            this.lbTrolleyLine.Name = "lbTrolleyLine";
            this.lbTrolleyLine.Size = new System.Drawing.Size(115, 28);
            this.lbTrolleyLine.TabIndex = 1;
            this.lbTrolleyLine.Text = "TrolleyNo:";
            // 
            // tbSN
            // 
            this.tbSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbSN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSN.Location = new System.Drawing.Point(606, 83);
            this.tbSN.Name = "tbSN";
            this.tbSN.Size = new System.Drawing.Size(365, 35);
            this.tbSN.TabIndex = 0;
            this.tbSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSN_KeyDown);
            // 
            // tbTrolleyLine
            // 
            this.tbTrolleyLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbTrolleyLine.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTrolleyLine.Location = new System.Drawing.Point(327, 83);
            this.tbTrolleyLine.Name = "tbTrolleyLine";
            this.tbTrolleyLine.Size = new System.Drawing.Size(261, 35);
            this.tbTrolleyLine.TabIndex = 0;
            this.tbTrolleyLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTrolleyLine_KeyDown);
            // 
            // tbTrolley
            // 
            this.tbTrolley.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbTrolley.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbTrolley.Location = new System.Drawing.Point(56, 83);
            this.tbTrolley.Name = "tbTrolley";
            this.tbTrolley.Size = new System.Drawing.Size(245, 35);
            this.tbTrolley.TabIndex = 0;
            this.tbTrolley.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTrolley_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Controls.Add(this.dgvTrolleyInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1491, 428);
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
            this.dgvTrolleyInfo.Location = new System.Drawing.Point(3, 24);
            this.dgvTrolleyInfo.Name = "dgvTrolleyInfo";
            this.dgvTrolleyInfo.ReadOnly = true;
            this.dgvTrolleyInfo.RowHeadersWidth = 62;
            this.dgvTrolleyInfo.RowTemplate.Height = 30;
            this.dgvTrolleyInfo.Size = new System.Drawing.Size(1485, 401);
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
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1503, 796);
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
    }
}

