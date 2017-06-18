namespace LocoNetToolBox.WinApp.Controls
{
    partial class ServoTester
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServoTester));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdGoRight = new System.Windows.Forms.Button();
            this.lbAddress = new System.Windows.Forms.Label();
            this.udAddress = new System.Windows.Forms.NumericUpDown();
            this.cmdLeft = new System.Windows.Forms.Button();
            this.testWorker = new System.ComponentModel.BackgroundWorker();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.cmdStart, 0, 2);
            this.tlpMain.Controls.Add(this.cmdStop, 0, 2);
            this.tlpMain.Controls.Add(this.cmdGoRight, 1, 1);
            this.tlpMain.Controls.Add(this.lbAddress, 0, 0);
            this.tlpMain.Controls.Add(this.udAddress, 1, 0);
            this.tlpMain.Controls.Add(this.cmdLeft, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(267, 176);
            this.tlpMain.TabIndex = 0;
            // 
            // cmdStart
            // 
            this.cmdStart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdStart.Location = new System.Drawing.Point(14, 121);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(104, 51);
            this.cmdStart.TabIndex = 5;
            this.cmdStart.Text = "Start duration test";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.CmdStartClick);
            // 
            // cmdStop
            // 
            this.cmdStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdStop.Enabled = false;
            this.cmdStop.Location = new System.Drawing.Point(148, 121);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(104, 51);
            this.cmdStop.TabIndex = 4;
            this.cmdStop.Text = "Stop duration test";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.CmdStopClick);
            // 
            // cmdGoRight
            // 
            this.cmdGoRight.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdGoRight.Location = new System.Drawing.Point(148, 62);
            this.cmdGoRight.Name = "cmdGoRight";
            this.cmdGoRight.Size = new System.Drawing.Size(104, 53);
            this.cmdGoRight.TabIndex = 3;
            this.cmdGoRight.Text = "Go Right";
            this.cmdGoRight.UseVisualStyleBackColor = true;
            this.cmdGoRight.Click += new System.EventHandler(this.CmdRightClick);
            // 
            // lbAddress
            // 
            this.lbAddress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbAddress.AutoSize = true;
            this.lbAddress.Location = new System.Drawing.Point(85, 23);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(45, 13);
            this.lbAddress.TabIndex = 0;
            this.lbAddress.Text = "Address";
            // 
            // udAddress
            // 
            this.udAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.udAddress.Location = new System.Drawing.Point(136, 19);
            this.udAddress.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.udAddress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udAddress.Name = "udAddress";
            this.udAddress.Size = new System.Drawing.Size(69, 20);
            this.udAddress.TabIndex = 1;
            this.udAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmdLeft
            // 
            this.cmdLeft.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdLeft.Location = new System.Drawing.Point(14, 62);
            this.cmdLeft.Name = "cmdLeft";
            this.cmdLeft.Size = new System.Drawing.Size(104, 53);
            this.cmdLeft.TabIndex = 2;
            this.cmdLeft.Text = "Go Left";
            this.cmdLeft.UseVisualStyleBackColor = true;
            this.cmdLeft.Click += new System.EventHandler(this.CmdLeftClick);
            // 
            // testWorker
            // 
            this.testWorker.WorkerSupportsCancellation = true;
            this.testWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TestWorkerDoWork);
            this.testWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TestWorkerRunWorkerCompleted);
            // 
            // ServoTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 193);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServoTester";
            this.Text = "Servo Tester";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAddress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.Button cmdGoRight;
        private System.Windows.Forms.NumericUpDown udAddress;
        private System.Windows.Forms.Button cmdLeft;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.ComponentModel.BackgroundWorker testWorker;
    }
}