namespace UnitTest
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnATSStockInCheck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnSendMailNotify = new System.Windows.Forms.Button();
            this.btnSync_Trolley = new System.Windows.Forms.Button();
            this.btnSync_Location = new System.Windows.Forms.Button();
            this.btnATSStockIn = new System.Windows.Forms.Button();
            this.btnPPSBOMReleaseResponse = new System.Windows.Forms.Button();
            this.btnATSPickComplete = new System.Windows.Forms.Button();
            this.btnStockInConfirm = new System.Windows.Forms.Button();
            this.btnTrolleyMoveNotice = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnATSStockInCheck
            // 
            this.btnATSStockInCheck.Location = new System.Drawing.Point(240, 12);
            this.btnATSStockInCheck.Name = "btnATSStockInCheck";
            this.btnATSStockInCheck.Size = new System.Drawing.Size(130, 23);
            this.btnATSStockInCheck.TabIndex = 0;
            this.btnATSStockInCheck.Text = "ATSStockInCheck";
            this.btnATSStockInCheck.UseVisualStyleBackColor = true;
            this.btnATSStockInCheck.Click += new System.EventHandler(this.btnATSStockInCheck_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(4, 24);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(230, 96);
            this.txtInput.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(4, 149);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(230, 110);
            this.txtResult.TabIndex = 4;
            // 
            // btnSendMailNotify
            // 
            this.btnSendMailNotify.Location = new System.Drawing.Point(240, 244);
            this.btnSendMailNotify.Name = "btnSendMailNotify";
            this.btnSendMailNotify.Size = new System.Drawing.Size(130, 23);
            this.btnSendMailNotify.TabIndex = 5;
            this.btnSendMailNotify.Text = "SendMailNotify";
            this.btnSendMailNotify.UseVisualStyleBackColor = true;
            this.btnSendMailNotify.Click += new System.EventHandler(this.btnSendMailNotify_Click);
            // 
            // btnSync_Trolley
            // 
            this.btnSync_Trolley.Location = new System.Drawing.Point(240, 215);
            this.btnSync_Trolley.Name = "btnSync_Trolley";
            this.btnSync_Trolley.Size = new System.Drawing.Size(130, 23);
            this.btnSync_Trolley.TabIndex = 6;
            this.btnSync_Trolley.Text = "Sync_Trolley";
            this.btnSync_Trolley.UseVisualStyleBackColor = true;
            this.btnSync_Trolley.Click += new System.EventHandler(this.btnSync_Trolley_Click);
            // 
            // btnSync_Location
            // 
            this.btnSync_Location.Location = new System.Drawing.Point(240, 186);
            this.btnSync_Location.Name = "btnSync_Location";
            this.btnSync_Location.Size = new System.Drawing.Size(130, 23);
            this.btnSync_Location.TabIndex = 7;
            this.btnSync_Location.Text = "Sync_Location";
            this.btnSync_Location.UseVisualStyleBackColor = true;
            this.btnSync_Location.Click += new System.EventHandler(this.btnSync_Location_Click);
            // 
            // btnATSStockIn
            // 
            this.btnATSStockIn.Location = new System.Drawing.Point(240, 41);
            this.btnATSStockIn.Name = "btnATSStockIn";
            this.btnATSStockIn.Size = new System.Drawing.Size(130, 23);
            this.btnATSStockIn.TabIndex = 8;
            this.btnATSStockIn.Text = "ATSStockIn";
            this.btnATSStockIn.UseVisualStyleBackColor = true;
            this.btnATSStockIn.Click += new System.EventHandler(this.btnATSStockIn_Click);
            // 
            // btnPPSBOMReleaseResponse
            // 
            this.btnPPSBOMReleaseResponse.Location = new System.Drawing.Point(240, 70);
            this.btnPPSBOMReleaseResponse.Name = "btnPPSBOMReleaseResponse";
            this.btnPPSBOMReleaseResponse.Size = new System.Drawing.Size(130, 23);
            this.btnPPSBOMReleaseResponse.TabIndex = 9;
            this.btnPPSBOMReleaseResponse.Text = "PPSBOMReleaseResponse";
            this.btnPPSBOMReleaseResponse.UseVisualStyleBackColor = true;
            this.btnPPSBOMReleaseResponse.Click += new System.EventHandler(this.btnPPSBOMReleaseResponse_Click);
            // 
            // btnATSPickComplete
            // 
            this.btnATSPickComplete.Location = new System.Drawing.Point(240, 99);
            this.btnATSPickComplete.Name = "btnATSPickComplete";
            this.btnATSPickComplete.Size = new System.Drawing.Size(130, 23);
            this.btnATSPickComplete.TabIndex = 10;
            this.btnATSPickComplete.Text = "ATSPickComplete";
            this.btnATSPickComplete.UseVisualStyleBackColor = true;
            this.btnATSPickComplete.Click += new System.EventHandler(this.btnATSPickComplete_Click);
            // 
            // btnStockInConfirm
            // 
            this.btnStockInConfirm.Location = new System.Drawing.Point(240, 128);
            this.btnStockInConfirm.Name = "btnStockInConfirm";
            this.btnStockInConfirm.Size = new System.Drawing.Size(130, 23);
            this.btnStockInConfirm.TabIndex = 11;
            this.btnStockInConfirm.Text = "StockInConfirm";
            this.btnStockInConfirm.UseVisualStyleBackColor = true;
            this.btnStockInConfirm.Click += new System.EventHandler(this.btnStockInConfirm_Click);
            // 
            // btnTrolleyMoveNotice
            // 
            this.btnTrolleyMoveNotice.Location = new System.Drawing.Point(240, 157);
            this.btnTrolleyMoveNotice.Name = "btnTrolleyMoveNotice";
            this.btnTrolleyMoveNotice.Size = new System.Drawing.Size(130, 23);
            this.btnTrolleyMoveNotice.TabIndex = 12;
            this.btnTrolleyMoveNotice.Text = "TrolleyMoveNotice";
            this.btnTrolleyMoveNotice.UseVisualStyleBackColor = true;
            this.btnTrolleyMoveNotice.Click += new System.EventHandler(this.btnTrolleyMoveNotice_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(523, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "TEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 304);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTrolleyMoveNotice);
            this.Controls.Add(this.btnStockInConfirm);
            this.Controls.Add(this.btnATSPickComplete);
            this.Controls.Add(this.btnPPSBOMReleaseResponse);
            this.Controls.Add(this.btnATSStockIn);
            this.Controls.Add(this.btnSync_Location);
            this.Controls.Add(this.btnSync_Trolley);
            this.Controls.Add(this.btnSendMailNotify);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnATSStockInCheck);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnATSStockInCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnSendMailNotify;
        private System.Windows.Forms.Button btnSync_Trolley;
        private System.Windows.Forms.Button btnSync_Location;
        private System.Windows.Forms.Button btnATSStockIn;
        private System.Windows.Forms.Button btnPPSBOMReleaseResponse;
        private System.Windows.Forms.Button btnATSPickComplete;
        private System.Windows.Forms.Button btnStockInConfirm;
        private System.Windows.Forms.Button btnTrolleyMoveNotice;
        private System.Windows.Forms.Button button1;
    }
}

