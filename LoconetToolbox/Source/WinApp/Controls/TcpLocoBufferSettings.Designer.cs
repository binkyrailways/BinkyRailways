namespace LocoNetToolBox.WinApp.Controls
{
    partial class TcpLocoBufferSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbIpAddress = new System.Windows.Forms.Label();
            this.lbBaudRate = new System.Windows.Forms.Label();
            this.tbIpAddress = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.lbIpAddress, 0, 0);
            this.tlpMain.Controls.Add(this.lbBaudRate, 0, 1);
            this.tlpMain.Controls.Add(this.tbIpAddress, 1, 0);
            this.tlpMain.Controls.Add(this.tbPort, 1, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(299, 52);
            this.tlpMain.TabIndex = 0;
            // 
            // lbIpAddress
            // 
            this.lbIpAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbIpAddress.AutoSize = true;
            this.lbIpAddress.Location = new System.Drawing.Point(3, 6);
            this.lbIpAddress.Name = "lbIpAddress";
            this.lbIpAddress.Size = new System.Drawing.Size(57, 13);
            this.lbIpAddress.TabIndex = 0;
            this.lbIpAddress.Text = "IP address";
            // 
            // lbBaudRate
            // 
            this.lbBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbBaudRate.AutoSize = true;
            this.lbBaudRate.Location = new System.Drawing.Point(3, 32);
            this.lbBaudRate.Name = "lbBaudRate";
            this.lbBaudRate.Size = new System.Drawing.Size(26, 13);
            this.lbBaudRate.TabIndex = 2;
            this.lbBaudRate.Text = "Port";
            // 
            // tbIpAddress
            // 
            this.tbIpAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbIpAddress.Location = new System.Drawing.Point(66, 3);
            this.tbIpAddress.Name = "tbIpAddress";
            this.tbIpAddress.Size = new System.Drawing.Size(230, 20);
            this.tbIpAddress.TabIndex = 5;
            this.tbIpAddress.Validated += new System.EventHandler(this.tbIpAddress_Validated);
            this.tbIpAddress.Validating += new System.ComponentModel.CancelEventHandler(this.tbIpAddress_Validating);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(66, 29);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 20);
            this.tbPort.TabIndex = 6;
            this.tbPort.Validated += new System.EventHandler(this.tbPort_Validated);
            this.tbPort.Validating += new System.ComponentModel.CancelEventHandler(this.tbPort_Validating);
            // 
            // TcpLocoBufferSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tlpMain);
            this.Name = "TcpLocoBufferSettings";
            this.Size = new System.Drawing.Size(299, 193);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbIpAddress;
        private System.Windows.Forms.Label lbBaudRate;
        private System.Windows.Forms.TextBox tbIpAddress;
        private System.Windows.Forms.TextBox tbPort;
    }
}
