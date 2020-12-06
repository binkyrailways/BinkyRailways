namespace LocoNetToolBox.WinApp.Controls
{
    partial class LocoBufferSettings
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
            this.rbUdp = new System.Windows.Forms.RadioButton();
            this.serialPortSettings = new LocoNetToolBox.WinApp.Controls.SerialPortLocoBufferSettings();
            this.rbSerialPort = new System.Windows.Forms.RadioButton();
            this.rbTcp = new System.Windows.Forms.RadioButton();
            this.tcpSettings = new LocoNetToolBox.WinApp.Controls.TcpLocoBufferSettings();
            this.udpSettings = new LocoNetToolBox.WinApp.Controls.UdpLocoBufferSettings();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.rbUdp, 1, 0);
            this.tlpMain.Controls.Add(this.serialPortSettings, 0, 1);
            this.tlpMain.Controls.Add(this.rbSerialPort, 0, 0);
            this.tlpMain.Controls.Add(this.rbTcp, 2, 0);
            this.tlpMain.Controls.Add(this.tcpSettings, 0, 2);
            this.tlpMain.Controls.Add(this.udpSettings, 0, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(299, 218);
            this.tlpMain.TabIndex = 0;
            // 
            // rbUdp
            // 
            this.rbUdp.AutoSize = true;
            this.rbUdp.Location = new System.Drawing.Point(81, 3);
            this.rbUdp.Name = "rbUdp";
            this.rbUdp.Size = new System.Drawing.Size(99, 17);
            this.rbUdp.TabIndex = 1;
            this.rbUdp.TabStop = true;
            this.rbUdp.Text = "UDP (MGV101)";
            this.rbUdp.UseVisualStyleBackColor = true;
            this.rbUdp.CheckedChanged += new System.EventHandler(this.OnChangeLocoBufferType);
            // 
            // serialPortSettings
            // 
            this.serialPortSettings.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.serialPortSettings, 3);
            this.serialPortSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.serialPortSettings.Location = new System.Drawing.Point(3, 26);
            this.serialPortSettings.Name = "serialPortSettings";
            this.serialPortSettings.Size = new System.Drawing.Size(293, 73);
            this.serialPortSettings.TabIndex = 3;
            // 
            // rbSerialPort
            // 
            this.rbSerialPort.AutoSize = true;
            this.rbSerialPort.Checked = true;
            this.rbSerialPort.Location = new System.Drawing.Point(3, 3);
            this.rbSerialPort.Name = "rbSerialPort";
            this.rbSerialPort.Size = new System.Drawing.Size(72, 17);
            this.rbSerialPort.TabIndex = 0;
            this.rbSerialPort.TabStop = true;
            this.rbSerialPort.Text = "Serial port";
            this.rbSerialPort.UseVisualStyleBackColor = true;
            this.rbSerialPort.CheckedChanged += new System.EventHandler(this.OnChangeLocoBufferType);
            // 
            // rbTcp
            // 
            this.rbTcp.AutoSize = true;
            this.rbTcp.Location = new System.Drawing.Point(186, 3);
            this.rbTcp.Name = "rbTcp";
            this.rbTcp.Size = new System.Drawing.Size(97, 17);
            this.rbTcp.TabIndex = 2;
            this.rbTcp.TabStop = true;
            this.rbTcp.Text = "TCP (MGV101)";
            this.rbTcp.UseVisualStyleBackColor = true;
            this.rbTcp.CheckedChanged += new System.EventHandler(this.OnChangeLocoBufferType);
            // 
            // tcpSettings
            // 
            this.tcpSettings.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.tcpSettings, 3);
            this.tcpSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcpSettings.Location = new System.Drawing.Point(3, 105);
            this.tcpSettings.Name = "tcpSettings";
            this.tcpSettings.Size = new System.Drawing.Size(293, 52);
            this.tcpSettings.TabIndex = 4;
            // 
            // udpSettings
            // 
            this.udpSettings.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.udpSettings, 3);
            this.udpSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.udpSettings.Location = new System.Drawing.Point(3, 163);
            this.udpSettings.Name = "udpSettings";
            this.udpSettings.Size = new System.Drawing.Size(293, 52);
            this.udpSettings.TabIndex = 5;
            // 
            // LocoBufferSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocoBufferSettings";
            this.Size = new System.Drawing.Size(299, 428);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private SerialPortLocoBufferSettings serialPortSettings;
        private System.Windows.Forms.RadioButton rbSerialPort;
        private System.Windows.Forms.RadioButton rbTcp;
        private TcpLocoBufferSettings tcpSettings;
        private System.Windows.Forms.RadioButton rbUdp;
        private UdpLocoBufferSettings udpSettings;

    }
}
