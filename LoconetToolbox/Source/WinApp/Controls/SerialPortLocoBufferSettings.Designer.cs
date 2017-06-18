namespace LocoNetToolBox.WinApp.Controls
{
    partial class SerialPortLocoBufferSettings
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
            this.lbPort = new System.Windows.Forms.Label();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.lbBaudRate = new System.Windows.Forms.Label();
            this.rbRate57K = new System.Windows.Forms.RadioButton();
            this.rbRate19K = new System.Windows.Forms.RadioButton();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.lbPort, 0, 0);
            this.tlpMain.Controls.Add(this.cbPort, 1, 0);
            this.tlpMain.Controls.Add(this.lbBaudRate, 0, 1);
            this.tlpMain.Controls.Add(this.rbRate57K, 1, 1);
            this.tlpMain.Controls.Add(this.rbRate19K, 1, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(299, 73);
            this.tlpMain.TabIndex = 0;
            // 
            // lbPort
            // 
            this.lbPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbPort.AutoSize = true;
            this.lbPort.Location = new System.Drawing.Point(3, 7);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(26, 13);
            this.lbPort.TabIndex = 0;
            this.lbPort.Text = "Port";
            // 
            // cbPort
            // 
            this.cbPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(59, 3);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(237, 21);
            this.cbPort.TabIndex = 1;
            this.cbPort.SelectedIndexChanged += new System.EventHandler(this.cbPort_SelectedIndexChanged);
            // 
            // lbBaudRate
            // 
            this.lbBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbBaudRate.AutoSize = true;
            this.lbBaudRate.Location = new System.Drawing.Point(3, 32);
            this.lbBaudRate.Name = "lbBaudRate";
            this.lbBaudRate.Size = new System.Drawing.Size(50, 13);
            this.lbBaudRate.TabIndex = 2;
            this.lbBaudRate.Text = "Baudrate";
            // 
            // rbRate57K
            // 
            this.rbRate57K.AutoSize = true;
            this.rbRate57K.Location = new System.Drawing.Point(59, 30);
            this.rbRate57K.Name = "rbRate57K";
            this.rbRate57K.Size = new System.Drawing.Size(50, 17);
            this.rbRate57K.TabIndex = 3;
            this.rbRate57K.TabStop = true;
            this.rbRate57K.Text = "57K6";
            this.rbRate57K.UseVisualStyleBackColor = true;
            this.rbRate57K.CheckedChanged += new System.EventHandler(this.rbRate_CheckedChanged);
            // 
            // rbRate19K
            // 
            this.rbRate19K.AutoSize = true;
            this.rbRate19K.Location = new System.Drawing.Point(59, 53);
            this.rbRate19K.Name = "rbRate19K";
            this.rbRate19K.Size = new System.Drawing.Size(50, 17);
            this.rbRate19K.TabIndex = 4;
            this.rbRate19K.TabStop = true;
            this.rbRate19K.Text = "19K2";
            this.rbRate19K.UseVisualStyleBackColor = true;
            this.rbRate19K.CheckedChanged += new System.EventHandler(this.rbRate_CheckedChanged);
            // 
            // LocoBufferSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocoBufferSettings";
            this.Size = new System.Drawing.Size(299, 193);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.Label lbBaudRate;
        private System.Windows.Forms.RadioButton rbRate57K;
        private System.Windows.Forms.RadioButton rbRate19K;
    }
}
