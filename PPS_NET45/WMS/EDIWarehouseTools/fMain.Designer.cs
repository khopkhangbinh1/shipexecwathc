namespace EDIWarehouseTools
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
            this.TextMsg = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPalletToCar = new System.Windows.Forms.Button();
            this.btnCarToPallet = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labShow = new System.Windows.Forms.TextBox();
            this.btnDockLocationAssign = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 818);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1293, 55);
            this.TextMsg.TabIndex = 136;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox3.Controls.Add(this.btnDockLocationAssign);
            this.groupBox3.Controls.Add(this.btnPalletToCar);
            this.groupBox3.Controls.Add(this.btnCarToPallet);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 49);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(1293, 824);
            this.groupBox3.TabIndex = 135;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "清单";
            // 
            // btnPalletToCar
            // 
            this.btnPalletToCar.Location = new System.Drawing.Point(309, 81);
            this.btnPalletToCar.Name = "btnPalletToCar";
            this.btnPalletToCar.Size = new System.Drawing.Size(147, 59);
            this.btnPalletToCar.TabIndex = 1;
            this.btnPalletToCar.Text = "栈板转精钢车";
            this.btnPalletToCar.UseVisualStyleBackColor = true;
            this.btnPalletToCar.Click += new System.EventHandler(this.btnPalletToCar_Click);
            // 
            // btnCarToPallet
            // 
            this.btnCarToPallet.Location = new System.Drawing.Point(126, 81);
            this.btnCarToPallet.Name = "btnCarToPallet";
            this.btnCarToPallet.Size = new System.Drawing.Size(147, 59);
            this.btnCarToPallet.TabIndex = 0;
            this.btnCarToPallet.Text = "精钢车转栈板";
            this.btnCarToPallet.UseVisualStyleBackColor = true;
            this.btnCarToPallet.Click += new System.EventHandler(this.btnCarToPallet_Click);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 49);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1293, 824);
            this.panel3.TabIndex = 139;
            // 
            // labShow
            // 
            this.labShow.BackColor = System.Drawing.SystemColors.HotTrack;
            this.labShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.labShow.Enabled = false;
            this.labShow.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labShow.ForeColor = System.Drawing.SystemColors.Info;
            this.labShow.Location = new System.Drawing.Point(0, 0);
            this.labShow.Margin = new System.Windows.Forms.Padding(4);
            this.labShow.Name = "labShow";
            this.labShow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labShow.Size = new System.Drawing.Size(1293, 49);
            this.labShow.TabIndex = 134;
            this.labShow.Text = "仓库工具";
            this.labShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDockLocationAssign
            // 
            this.btnDockLocationAssign.Location = new System.Drawing.Point(126, 326);
            this.btnDockLocationAssign.Name = "btnDockLocationAssign";
            this.btnDockLocationAssign.Size = new System.Drawing.Size(147, 59);
            this.btnDockLocationAssign.TabIndex = 2;
            this.btnDockLocationAssign.Text = "码头储位分配";
            this.btnDockLocationAssign.UseVisualStyleBackColor = true;
            this.btnDockLocationAssign.Click += new System.EventHandler(this.btnDockLocationAssign_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 873);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labShow);
            this.Name = "fMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox labShow;
        private System.Windows.Forms.Button btnPalletToCar;
        private System.Windows.Forms.Button btnCarToPallet;
        private System.Windows.Forms.Button btnDockLocationAssign;
    }
}

