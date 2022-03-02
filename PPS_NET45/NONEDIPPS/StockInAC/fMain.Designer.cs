namespace StockInAC
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
            this.gbScan = new System.Windows.Forms.GroupBox();
            this.radNoSN = new System.Windows.Forms.RadioButton();
            this.radSN = new System.Windows.Forms.RadioButton();
            this.labPart = new System.Windows.Forms.Label();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.labLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.Label();
            this.labShow = new System.Windows.Forms.TextBox();
            this.gbDNInfo = new System.Windows.Forms.GroupBox();
            this.txtPre = new System.Windows.Forms.TextBox();
            this.chkCarton = new System.Windows.Forms.CheckBox();
            this.gbScan.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbScan
            // 
            this.gbScan.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbScan.Controls.Add(this.chkCarton);
            this.gbScan.Controls.Add(this.txtPre);
            this.gbScan.Controls.Add(this.radNoSN);
            this.gbScan.Controls.Add(this.radSN);
            this.gbScan.Controls.Add(this.labPart);
            this.gbScan.Controls.Add(this.txtPart);
            this.gbScan.Controls.Add(this.labLocation);
            this.gbScan.Controls.Add(this.txtLocation);
            this.gbScan.Controls.Add(this.label3);
            this.gbScan.Controls.Add(this.txtCarton);
            this.gbScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbScan.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbScan.Location = new System.Drawing.Point(0, 41);
            this.gbScan.Margin = new System.Windows.Forms.Padding(2);
            this.gbScan.Name = "gbScan";
            this.gbScan.Padding = new System.Windows.Forms.Padding(2);
            this.gbScan.Size = new System.Drawing.Size(1096, 331);
            this.gbScan.TabIndex = 74;
            this.gbScan.TabStop = false;
            this.gbScan.Text = "信息扫入";
            // 
            // radNoSN
            // 
            this.radNoSN.AutoSize = true;
            this.radNoSN.Location = new System.Drawing.Point(530, 41);
            this.radNoSN.Name = "radNoSN";
            this.radNoSN.Size = new System.Drawing.Size(56, 19);
            this.radNoSN.TabIndex = 129;
            this.radNoSN.Text = "无SN";
            this.radNoSN.UseVisualStyleBackColor = true;
            this.radNoSN.CheckedChanged += new System.EventHandler(this.radSN_CheckedChanged);
            // 
            // radSN
            // 
            this.radSN.AutoSize = true;
            this.radSN.Checked = true;
            this.radSN.Location = new System.Drawing.Point(302, 42);
            this.radSN.Name = "radSN";
            this.radSN.Size = new System.Drawing.Size(71, 19);
            this.radSN.TabIndex = 128;
            this.radSN.TabStop = true;
            this.radSN.Text = "有SN号";
            this.radSN.UseVisualStyleBackColor = true;
            this.radSN.CheckedChanged += new System.EventHandler(this.radSN_CheckedChanged);
            // 
            // labPart
            // 
            this.labPart.AutoSize = true;
            this.labPart.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.labPart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labPart.Location = new System.Drawing.Point(759, 105);
            this.labPart.Name = "labPart";
            this.labPart.Size = new System.Drawing.Size(42, 14);
            this.labPart.TabIndex = 126;
            this.labPart.Text = "料号:";
            this.labPart.Visible = false;
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.White;
            this.txtPart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPart.Enabled = false;
            this.txtPart.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPart.ForeColor = System.Drawing.Color.Blue;
            this.txtPart.Location = new System.Drawing.Point(804, 96);
            this.txtPart.Margin = new System.Windows.Forms.Padding(4);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(225, 32);
            this.txtPart.TabIndex = 127;
            this.txtPart.Visible = false;
            this.txtPart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPart_KeyDown);
            // 
            // labLocation
            // 
            this.labLocation.AutoSize = true;
            this.labLocation.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.labLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labLocation.Location = new System.Drawing.Point(34, 105);
            this.labLocation.Name = "labLocation";
            this.labLocation.Size = new System.Drawing.Size(56, 14);
            this.labLocation.TabIndex = 124;
            this.labLocation.Text = "储位号:";
            // 
            // txtLocation
            // 
            this.txtLocation.BackColor = System.Drawing.Color.White;
            this.txtLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocation.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLocation.ForeColor = System.Drawing.Color.Blue;
            this.txtLocation.Location = new System.Drawing.Point(94, 96);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(225, 32);
            this.txtLocation.TabIndex = 125;
            this.txtLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocation_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新宋体", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(398, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 122;
            this.label3.Text = "中箱号:";
            // 
            // txtCarton
            // 
            this.txtCarton.BackColor = System.Drawing.Color.White;
            this.txtCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCarton.Enabled = false;
            this.txtCarton.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtCarton.Location = new System.Drawing.Point(458, 96);
            this.txtCarton.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(225, 32);
            this.txtCarton.TabIndex = 123;
            this.txtCarton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCarton_KeyDown);
            // 
            // txtMsg
            // 
            this.txtMsg.AutoEllipsis = true;
            this.txtMsg.BackColor = System.Drawing.Color.Blue;
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.txtMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMsg.Location = new System.Drawing.Point(0, 372);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(1096, 41);
            this.txtMsg.TabIndex = 73;
            this.txtMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.Color.White;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1096, 41);
            this.labShow.TabIndex = 72;
            this.labShow.Text = "AppleCare StockIn";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbDNInfo
            // 
            this.gbDNInfo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbDNInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDNInfo.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbDNInfo.Location = new System.Drawing.Point(0, 0);
            this.gbDNInfo.Margin = new System.Windows.Forms.Padding(2);
            this.gbDNInfo.Name = "gbDNInfo";
            this.gbDNInfo.Padding = new System.Windows.Forms.Padding(2);
            this.gbDNInfo.Size = new System.Drawing.Size(1096, 413);
            this.gbDNInfo.TabIndex = 75;
            this.gbDNInfo.TabStop = false;
            this.gbDNInfo.Text = "DN信息";
            // 
            // txtPre
            // 
            this.txtPre.BackColor = System.Drawing.Color.White;
            this.txtPre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPre.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPre.ForeColor = System.Drawing.Color.Blue;
            this.txtPre.Location = new System.Drawing.Point(804, 35);
            this.txtPre.Margin = new System.Windows.Forms.Padding(4);
            this.txtPre.Name = "txtPre";
            this.txtPre.Size = new System.Drawing.Size(225, 32);
            this.txtPre.TabIndex = 131;
            this.txtPre.Text = "CTN";
            this.txtPre.Visible = false;
            // 
            // chkCarton
            // 
            this.chkCarton.AutoSize = true;
            this.chkCarton.Checked = true;
            this.chkCarton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCarton.Location = new System.Drawing.Point(670, 42);
            this.chkCarton.Name = "chkCarton";
            this.chkCarton.Size = new System.Drawing.Size(131, 19);
            this.chkCarton.TabIndex = 132;
            this.chkCarton.Text = "检查箱号前缀：";
            this.chkCarton.UseVisualStyleBackColor = true;
            this.chkCarton.Visible = false;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 413);
            this.Controls.Add(this.gbScan);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.labShow);
            this.Controls.Add(this.gbDNInfo);
            this.Name = "fMain";
            this.Text = "fMain";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.gbScan.ResumeLayout(false);
            this.gbScan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbScan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.Label txtMsg;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.GroupBox gbDNInfo;
        private System.Windows.Forms.Label labLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label labPart;
        private System.Windows.Forms.TextBox txtPart;
        private System.Windows.Forms.RadioButton radNoSN;
        private System.Windows.Forms.RadioButton radSN;
        private System.Windows.Forms.CheckBox chkCarton;
        private System.Windows.Forms.TextBox txtPre;
    }
}

