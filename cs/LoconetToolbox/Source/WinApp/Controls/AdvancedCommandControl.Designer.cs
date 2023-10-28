namespace LocoNetToolBox.WinApp.Controls
{
    partial class AdvancedCommandControl
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
            this.cmdDiscover = new System.Windows.Forms.Button();
            this.cmdRead = new System.Windows.Forms.Button();
            this.tbSvAddress = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmdSwitchRequest = new System.Windows.Forms.Button();
            this.cmdQuery = new System.Windows.Forms.Button();
            this.lbDstL = new System.Windows.Forms.Label();
            this.lbDstH = new System.Windows.Forms.Label();
            this.tbDstL = new System.Windows.Forms.NumericUpDown();
            this.tbDstH = new System.Windows.Forms.NumericUpDown();
            this.cmdGpOn = new System.Windows.Forms.Button();
            this.cmdGpOff = new System.Windows.Forms.Button();
            this.cmdBusy = new System.Windows.Forms.Button();
            this.lbAddress = new System.Windows.Forms.Label();
            this.tbAddress = new System.Windows.Forms.NumericUpDown();
            this.cbDirection = new System.Windows.Forms.CheckBox();
            this.cbOutput = new System.Windows.Forms.CheckBox();
            this.cmdServoProgrammer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbSvAddress)).BeginInit();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDstL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDstH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdDiscover
            // 
            this.cmdDiscover.Location = new System.Drawing.Point(3, 123);
            this.cmdDiscover.Name = "cmdDiscover";
            this.cmdDiscover.Size = new System.Drawing.Size(80, 24);
            this.cmdDiscover.TabIndex = 0;
            this.cmdDiscover.Text = "Discover";
            this.cmdDiscover.UseVisualStyleBackColor = true;
            this.cmdDiscover.Click += new System.EventHandler(this.CmdDiscoverClick);
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(3, 93);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(80, 24);
            this.cmdRead.TabIndex = 1;
            this.cmdRead.Text = "Read";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.CmdReadClick);
            // 
            // tbSvAddress
            // 
            this.tbSvAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbSvAddress.Location = new System.Drawing.Point(140, 125);
            this.tbSvAddress.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.tbSvAddress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbSvAddress.Name = "tbSvAddress";
            this.tbSvAddress.Size = new System.Drawing.Size(71, 20);
            this.tbSvAddress.TabIndex = 2;
            this.tbSvAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "SV";
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 263F));
            this.tlpMain.Controls.Add(this.cmdSwitchRequest, 0, 6);
            this.tlpMain.Controls.Add(this.cmdQuery, 0, 5);
            this.tlpMain.Controls.Add(this.tbSvAddress, 2, 4);
            this.tlpMain.Controls.Add(this.label1, 1, 4);
            this.tlpMain.Controls.Add(this.lbDstL, 1, 2);
            this.tlpMain.Controls.Add(this.lbDstH, 1, 3);
            this.tlpMain.Controls.Add(this.tbDstL, 2, 2);
            this.tlpMain.Controls.Add(this.tbDstH, 2, 3);
            this.tlpMain.Controls.Add(this.cmdGpOn, 0, 0);
            this.tlpMain.Controls.Add(this.cmdGpOff, 0, 1);
            this.tlpMain.Controls.Add(this.cmdDiscover, 0, 4);
            this.tlpMain.Controls.Add(this.cmdRead, 0, 3);
            this.tlpMain.Controls.Add(this.cmdBusy, 0, 2);
            this.tlpMain.Controls.Add(this.lbAddress, 1, 6);
            this.tlpMain.Controls.Add(this.tbAddress, 2, 6);
            this.tlpMain.Controls.Add(this.cbDirection, 2, 7);
            this.tlpMain.Controls.Add(this.cbOutput, 2, 8);
            this.tlpMain.Controls.Add(this.cmdServoProgrammer, 0, 9);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 10;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(376, 286);
            this.tlpMain.TabIndex = 4;
            // 
            // cmdSwitchRequest
            // 
            this.cmdSwitchRequest.Location = new System.Drawing.Point(3, 183);
            this.cmdSwitchRequest.Name = "cmdSwitchRequest";
            this.cmdSwitchRequest.Size = new System.Drawing.Size(80, 24);
            this.cmdSwitchRequest.TabIndex = 12;
            this.cmdSwitchRequest.Text = "SW_REQ";
            this.cmdSwitchRequest.UseVisualStyleBackColor = true;
            this.cmdSwitchRequest.Click += new System.EventHandler(this.CmdSwitchRequestClick);
            // 
            // cmdQuery
            // 
            this.cmdQuery.Location = new System.Drawing.Point(3, 153);
            this.cmdQuery.Name = "cmdQuery";
            this.cmdQuery.Size = new System.Drawing.Size(80, 24);
            this.cmdQuery.TabIndex = 11;
            this.cmdQuery.Text = "Query";
            this.cmdQuery.UseVisualStyleBackColor = true;
            this.cmdQuery.Click += new System.EventHandler(this.CmdQueryClick);
            // 
            // lbDstL
            // 
            this.lbDstL.AutoSize = true;
            this.lbDstL.Location = new System.Drawing.Point(89, 60);
            this.lbDstL.Name = "lbDstL";
            this.lbDstL.Size = new System.Drawing.Size(32, 13);
            this.lbDstL.TabIndex = 4;
            this.lbDstL.Text = "Dst L";
            // 
            // lbDstH
            // 
            this.lbDstH.AutoSize = true;
            this.lbDstH.Location = new System.Drawing.Point(89, 90);
            this.lbDstH.Name = "lbDstH";
            this.lbDstH.Size = new System.Drawing.Size(34, 13);
            this.lbDstH.TabIndex = 5;
            this.lbDstH.Text = "Dst H";
            // 
            // tbDstL
            // 
            this.tbDstL.Location = new System.Drawing.Point(140, 63);
            this.tbDstL.Name = "tbDstL";
            this.tbDstL.Size = new System.Drawing.Size(71, 20);
            this.tbDstL.TabIndex = 6;
            this.tbDstL.Value = new decimal(new int[] {
            81,
            0,
            0,
            0});
            // 
            // tbDstH
            // 
            this.tbDstH.Location = new System.Drawing.Point(140, 93);
            this.tbDstH.Name = "tbDstH";
            this.tbDstH.Size = new System.Drawing.Size(71, 20);
            this.tbDstH.TabIndex = 7;
            this.tbDstH.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmdGpOn
            // 
            this.cmdGpOn.Location = new System.Drawing.Point(3, 3);
            this.cmdGpOn.Name = "cmdGpOn";
            this.cmdGpOn.Size = new System.Drawing.Size(77, 24);
            this.cmdGpOn.TabIndex = 8;
            this.cmdGpOn.Text = "GPON";
            this.cmdGpOn.UseVisualStyleBackColor = true;
            this.cmdGpOn.Click += new System.EventHandler(this.CmdGpOnClick);
            // 
            // cmdGpOff
            // 
            this.cmdGpOff.Location = new System.Drawing.Point(3, 33);
            this.cmdGpOff.Name = "cmdGpOff";
            this.cmdGpOff.Size = new System.Drawing.Size(77, 24);
            this.cmdGpOff.TabIndex = 9;
            this.cmdGpOff.Text = "GPOFF";
            this.cmdGpOff.UseVisualStyleBackColor = true;
            this.cmdGpOff.Click += new System.EventHandler(this.CmdGpOffClick);
            // 
            // cmdBusy
            // 
            this.cmdBusy.Location = new System.Drawing.Point(3, 63);
            this.cmdBusy.Name = "cmdBusy";
            this.cmdBusy.Size = new System.Drawing.Size(77, 24);
            this.cmdBusy.TabIndex = 10;
            this.cmdBusy.Text = "Busy";
            this.cmdBusy.UseVisualStyleBackColor = true;
            this.cmdBusy.Click += new System.EventHandler(this.CmdBusyClick);
            // 
            // lbAddress
            // 
            this.lbAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbAddress.AutoSize = true;
            this.lbAddress.Location = new System.Drawing.Point(89, 188);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(45, 13);
            this.lbAddress.TabIndex = 13;
            this.lbAddress.Text = "Address";
            // 
            // tbAddress
            // 
            this.tbAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbAddress.Location = new System.Drawing.Point(140, 185);
            this.tbAddress.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.tbAddress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(71, 20);
            this.tbAddress.TabIndex = 14;
            this.tbAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbDirection
            // 
            this.cbDirection.AutoSize = true;
            this.cbDirection.Location = new System.Drawing.Point(140, 213);
            this.cbDirection.Name = "cbDirection";
            this.cbDirection.Size = new System.Drawing.Size(68, 17);
            this.cbDirection.TabIndex = 15;
            this.cbDirection.Text = "Direction";
            this.cbDirection.UseVisualStyleBackColor = true;
            // 
            // cbOutput
            // 
            this.cbOutput.AutoSize = true;
            this.cbOutput.Checked = true;
            this.cbOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOutput.Location = new System.Drawing.Point(140, 236);
            this.cbOutput.Name = "cbOutput";
            this.cbOutput.Size = new System.Drawing.Size(58, 17);
            this.cbOutput.TabIndex = 16;
            this.cbOutput.Text = "Output";
            this.cbOutput.UseVisualStyleBackColor = true;
            // 
            // cmdServoProgrammer
            // 
            this.cmdServoProgrammer.Location = new System.Drawing.Point(3, 259);
            this.cmdServoProgrammer.Name = "cmdServoProgrammer";
            this.cmdServoProgrammer.Size = new System.Drawing.Size(80, 24);
            this.cmdServoProgrammer.TabIndex = 17;
            this.cmdServoProgrammer.Text = "Servo Prog";
            this.cmdServoProgrammer.UseVisualStyleBackColor = true;
            this.cmdServoProgrammer.Click += new System.EventHandler(this.CmdServoProgrammerClick);
            // 
            // CommandControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "CommandControl";
            this.Size = new System.Drawing.Size(376, 359);
            ((System.ComponentModel.ISupportInitialize)(this.tbSvAddress)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDstL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDstH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAddress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdDiscover;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.NumericUpDown tbSvAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbDstL;
        private System.Windows.Forms.Label lbDstH;
        private System.Windows.Forms.NumericUpDown tbDstL;
        private System.Windows.Forms.NumericUpDown tbDstH;
        private System.Windows.Forms.Button cmdGpOn;
        private System.Windows.Forms.Button cmdGpOff;
        private System.Windows.Forms.Button cmdBusy;
        private System.Windows.Forms.Button cmdQuery;
        private System.Windows.Forms.Button cmdSwitchRequest;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.NumericUpDown tbAddress;
        private System.Windows.Forms.CheckBox cbDirection;
        private System.Windows.Forms.CheckBox cbOutput;
        private System.Windows.Forms.Button cmdServoProgrammer;
    }
}
