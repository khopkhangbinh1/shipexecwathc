namespace NPIPickListAC
{
    partial class addPalletWeight
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
            this.TextMsg = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmptyCarton = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPalletWeight = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPalletHeight = new System.Windows.Forms.TextBox();
            this.cmbPalletSize = new System.Windows.Forms.ComboBox();
            this.labStatus = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPalletNo = new System.Windows.Forms.TextBox();
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
            this.txtTitle.Size = new System.Drawing.Size(1241, 53);
            this.txtTitle.TabIndex = 66;
            this.txtTitle.Text = "NPI称重维护";
            this.txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextMsg
            // 
            this.TextMsg.AutoEllipsis = true;
            this.TextMsg.BackColor = System.Drawing.Color.Blue;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TextMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.TextMsg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TextMsg.Location = new System.Drawing.Point(0, 522);
            this.TextMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(1241, 53);
            this.TextMsg.TabIndex = 67;
            this.TextMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEmptyCarton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPalletWeight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPalletHeight);
            this.groupBox1.Controls.Add(this.cmbPalletSize);
            this.groupBox1.Controls.Add(this.labStatus);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPalletNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1241, 155);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "更新";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(618, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 108;
            this.label2.Text = "实际空箱数:";
            // 
            // txtEmptyCarton
            // 
            this.txtEmptyCarton.BackColor = System.Drawing.Color.White;
            this.txtEmptyCarton.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmptyCarton.Font = new System.Drawing.Font("SimSun", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEmptyCarton.ForeColor = System.Drawing.Color.Blue;
            this.txtEmptyCarton.Location = new System.Drawing.Point(807, 69);
            this.txtEmptyCarton.Margin = new System.Windows.Forms.Padding(5);
            this.txtEmptyCarton.Name = "txtEmptyCarton";
            this.txtEmptyCarton.Size = new System.Drawing.Size(148, 34);
            this.txtEmptyCarton.TabIndex = 109;
            this.txtEmptyCarton.Text = "0";
            this.txtEmptyCarton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmptyCarton_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(604, 121);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 18);
            this.label1.TabIndex = 106;
            this.label1.Text = "实际栈板重量[kg]:";
            // 
            // txtPalletWeight
            // 
            this.txtPalletWeight.BackColor = System.Drawing.Color.White;
            this.txtPalletWeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPalletWeight.Font = new System.Drawing.Font("SimSun", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPalletWeight.ForeColor = System.Drawing.Color.Blue;
            this.txtPalletWeight.Location = new System.Drawing.Point(807, 112);
            this.txtPalletWeight.Margin = new System.Windows.Forms.Padding(5);
            this.txtPalletWeight.Name = "txtPalletWeight";
            this.txtPalletWeight.Size = new System.Drawing.Size(148, 34);
            this.txtPalletWeight.TabIndex = 107;
            this.txtPalletWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPalletWeight_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("NSimSun", 10.5F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(208, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 18);
            this.label3.TabIndex = 104;
            this.label3.Text = "实际栈板高[cm]:";
            // 
            // txtPalletHeight
            // 
            this.txtPalletHeight.BackColor = System.Drawing.Color.White;
            this.txtPalletHeight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPalletHeight.Font = new System.Drawing.Font("SimSun", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPalletHeight.ForeColor = System.Drawing.Color.Blue;
            this.txtPalletHeight.Location = new System.Drawing.Point(413, 112);
            this.txtPalletHeight.Margin = new System.Windows.Forms.Padding(5);
            this.txtPalletHeight.Name = "txtPalletHeight";
            this.txtPalletHeight.Size = new System.Drawing.Size(148, 34);
            this.txtPalletHeight.TabIndex = 105;
            this.txtPalletHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPalletHeight_KeyPress);
            // 
            // cmbPalletSize
            // 
            this.cmbPalletSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPalletSize.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPalletSize.FormattingEnabled = true;
            this.cmbPalletSize.Items.AddRange(new object[] {
            "107*90",
            "115*80",
            "112*89",
            "120*100",
            "80*67",
            "89*71"});
            this.cmbPalletSize.Location = new System.Drawing.Point(413, 73);
            this.cmbPalletSize.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPalletSize.Name = "cmbPalletSize";
            this.cmbPalletSize.Size = new System.Drawing.Size(139, 28);
            this.cmbPalletSize.TabIndex = 103;
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(251, 78);
            this.labStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(95, 19);
            this.labStatus.TabIndex = 102;
            this.labStatus.Text = "栈板规格:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(851, 27);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(125, 36);
            this.btnUpdate.TabIndex = 84;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(45, 43);
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
            this.label4.Location = new System.Drawing.Point(317, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 18);
            this.label4.TabIndex = 74;
            this.label4.Text = "栈板号:";
            // 
            // txtPalletNo
            // 
            this.txtPalletNo.BackColor = System.Drawing.SystemColors.Control;
            this.txtPalletNo.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPalletNo.ForeColor = System.Drawing.Color.Green;
            this.txtPalletNo.Location = new System.Drawing.Point(413, 27);
            this.txtPalletNo.Margin = new System.Windows.Forms.Padding(5);
            this.txtPalletNo.Name = "txtPalletNo";
            this.txtPalletNo.Size = new System.Drawing.Size(248, 30);
            this.txtPalletNo.TabIndex = 75;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox2.Controls.Add(this.dgvPallet);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1241, 314);
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
            this.dgvPallet.Size = new System.Drawing.Size(1235, 293);
            this.dgvPallet.TabIndex = 0;
            this.dgvPallet.SelectionChanged += new System.EventHandler(this.dgvPallet_SelectionChanged);
            // 
            // addPalletWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 575);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TextMsg);
            this.Controls.Add(this.txtTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "addPalletWeight";
            this.Text = "addPalletWeight";
            this.Load += new System.EventHandler(this.addPalletWeight_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPallet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label TextMsg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvPallet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPalletNo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cmbPalletSize;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPalletWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPalletHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmptyCarton;
    }
}