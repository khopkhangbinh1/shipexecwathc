namespace PickList
{
    partial class fUPSShipExecCheck
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
            this.prgTitle = new System.Windows.Forms.TextBox();
            this.prgMSG = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnResend1 = new System.Windows.Forms.Button();
            this.btnSearch1 = new System.Windows.Forms.Button();
            this.txtBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtResult1 = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtResult1)).BeginInit();
            this.SuspendLayout();
            // 
            // prgTitle
            // 
            this.prgTitle.BackColor = System.Drawing.SystemColors.HotTrack;
            this.prgTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prgTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.prgTitle.Font = new System.Drawing.Font("SimSun", 29F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prgTitle.ForeColor = System.Drawing.SystemColors.Info;
            this.prgTitle.Location = new System.Drawing.Point(0, 0);
            this.prgTitle.Margin = new System.Windows.Forms.Padding(2);
            this.prgTitle.Multiline = true;
            this.prgTitle.Name = "prgTitle";
            this.prgTitle.ReadOnly = true;
            this.prgTitle.Size = new System.Drawing.Size(850, 40);
            this.prgTitle.TabIndex = 67;
            this.prgTitle.Text = "Title";
            this.prgTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // prgMSG
            // 
            this.prgMSG.AutoEllipsis = true;
            this.prgMSG.BackColor = System.Drawing.SystemColors.Control;
            this.prgMSG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.prgMSG.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.prgMSG.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold);
            this.prgMSG.ForeColor = System.Drawing.Color.Blue;
            this.prgMSG.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.prgMSG.Location = new System.Drawing.Point(0, 418);
            this.prgMSG.Name = "prgMSG";
            this.prgMSG.Size = new System.Drawing.Size(850, 51);
            this.prgMSG.TabIndex = 74;
            this.prgMSG.Text = "Message";
            this.prgMSG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnResend1);
            this.panel2.Controls.Add(this.btnSearch1);
            this.panel2.Controls.Add(this.txtBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 91);
            this.panel2.TabIndex = 75;
            // 
            // btnResend1
            // 
            this.btnResend1.Location = new System.Drawing.Point(531, 37);
            this.btnResend1.Name = "btnResend1";
            this.btnResend1.Size = new System.Drawing.Size(105, 39);
            this.btnResend1.TabIndex = 2;
            this.btnResend1.Text = "Resend";
            this.btnResend1.UseVisualStyleBackColor = true;
            this.btnResend1.Click += new System.EventHandler(this.btnResend1_Click);
            // 
            // btnSearch1
            // 
            this.btnSearch1.Location = new System.Drawing.Point(353, 37);
            this.btnSearch1.Name = "btnSearch1";
            this.btnSearch1.Size = new System.Drawing.Size(121, 39);
            this.btnSearch1.TabIndex = 1;
            this.btnSearch1.Text = "Search";
            this.btnSearch1.UseVisualStyleBackColor = true;
            this.btnSearch1.Click += new System.EventHandler(this.btnSearch1_Click);
            // 
            // txtBox1
            // 
            this.txtBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBox1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold);
            this.txtBox1.Location = new System.Drawing.Point(22, 51);
            this.txtBox1.Name = "txtBox1";
            this.txtBox1.Size = new System.Drawing.Size(238, 26);
            this.txtBox1.TabIndex = 76;
            this.txtBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 16);
            this.label1.TabIndex = 71;
            this.label1.Text = "CSN/CARTONNO/PICKPALLETNO:";
            // 
            // dtResult1
            // 
            this.dtResult1.AllowUserToAddRows = false;
            this.dtResult1.AllowUserToDeleteRows = false;
            this.dtResult1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtResult1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtResult1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtResult1.Location = new System.Drawing.Point(0, 131);
            this.dtResult1.Name = "dtResult1";
            this.dtResult1.RowTemplate.Height = 23;
            this.dtResult1.Size = new System.Drawing.Size(850, 287);
            this.dtResult1.TabIndex = 76;
            this.dtResult1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dtResult_CellFormatting);
            // 
            // fUPSShipExecCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 469);
            this.Controls.Add(this.dtResult1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.prgMSG);
            this.Controls.Add(this.prgTitle);
            this.Name = "fUPSShipExecCheck";
            this.Text = "fUPSShipExecCheck";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtResult1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnResend;
        private System.Windows.Forms.DataGridView dtResult;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox prgTitle;
        private System.Windows.Forms.Label prgMSG;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResend1;
        private System.Windows.Forms.Button btnSearch1;
        private System.Windows.Forms.TextBox txtBox1;
        private System.Windows.Forms.DataGridView dtResult1;
    }
}