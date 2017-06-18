namespace LocoNetToolBox.WinApp.Controls
{
    partial class ServoProgrammerStep4
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.udSpeed = new System.Windows.Forms.NumericUpDown();
            this.cmdSetRight = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.udLeft = new System.Windows.Forms.NumericUpDown();
            this.udRight = new System.Windows.Forms.NumericUpDown();
            this.cmdSetLeft = new System.Windows.Forms.Button();
            this.lbTurnout = new System.Windows.Forms.Label();
            this.turnoutSelection = new LocoNetToolBox.WinApp.Controls.TurnoutSelectionControl();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.cmdSetSpeed = new System.Windows.Forms.Button();
            this.lbRelays = new System.Windows.Forms.Label();
            this.cbLeftLSB = new System.Windows.Forms.CheckBox();
            this.cmdSetRelay = new System.Windows.Forms.Button();
            this.lbIntro = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRight)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMain.ColumnCount = 5;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.00001F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.Controls.Add(this.udSpeed, 3, 2);
            this.tlpMain.Controls.Add(this.cmdSetRight, 2, 3);
            this.tlpMain.Controls.Add(this.label2, 1, 1);
            this.tlpMain.Controls.Add(this.label3, 2, 1);
            this.tlpMain.Controls.Add(this.udLeft, 1, 2);
            this.tlpMain.Controls.Add(this.udRight, 2, 2);
            this.tlpMain.Controls.Add(this.cmdSetLeft, 1, 3);
            this.tlpMain.Controls.Add(this.lbTurnout, 0, 1);
            this.tlpMain.Controls.Add(this.turnoutSelection, 0, 2);
            this.tlpMain.Controls.Add(this.lbSpeed, 3, 1);
            this.tlpMain.Controls.Add(this.cmdSetSpeed, 3, 3);
            this.tlpMain.Controls.Add(this.lbRelays, 4, 1);
            this.tlpMain.Controls.Add(this.cbLeftLSB, 4, 2);
            this.tlpMain.Controls.Add(this.cmdSetRelay, 4, 3);
            this.tlpMain.Controls.Add(this.lbIntro, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.MinimumSize = new System.Drawing.Size(400, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(572, 236);
            this.tlpMain.TabIndex = 0;
            // 
            // udSpeed
            // 
            this.udSpeed.Location = new System.Drawing.Point(345, 101);
            this.udSpeed.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.udSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udSpeed.Name = "udSpeed";
            this.udSpeed.Size = new System.Drawing.Size(75, 20);
            this.udSpeed.TabIndex = 14;
            this.udSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmdSetRight
            // 
            this.cmdSetRight.Location = new System.Drawing.Point(231, 199);
            this.cmdSetRight.Name = "cmdSetRight";
            this.cmdSetRight.Size = new System.Drawing.Size(92, 34);
            this.cmdSetRight.TabIndex = 7;
            this.cmdSetRight.Text = "Set right angle";
            this.cmdSetRight.UseVisualStyleBackColor = true;
            this.cmdSetRight.Click += new System.EventHandler(this.cmdSetRight_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Left angle";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Right angle";
            // 
            // udLeft
            // 
            this.udLeft.Location = new System.Drawing.Point(117, 101);
            this.udLeft.Name = "udLeft";
            this.udLeft.Size = new System.Drawing.Size(68, 20);
            this.udLeft.TabIndex = 4;
            this.udLeft.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // udRight
            // 
            this.udRight.Location = new System.Drawing.Point(231, 101);
            this.udRight.Name = "udRight";
            this.udRight.Size = new System.Drawing.Size(75, 20);
            this.udRight.TabIndex = 5;
            this.udRight.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // cmdSetLeft
            // 
            this.cmdSetLeft.Location = new System.Drawing.Point(117, 199);
            this.cmdSetLeft.Name = "cmdSetLeft";
            this.cmdSetLeft.Size = new System.Drawing.Size(92, 34);
            this.cmdSetLeft.TabIndex = 6;
            this.cmdSetLeft.Text = "Set left angle";
            this.cmdSetLeft.UseVisualStyleBackColor = true;
            this.cmdSetLeft.Click += new System.EventHandler(this.cmdSetLeft_Click);
            // 
            // lbTurnout
            // 
            this.lbTurnout.AutoSize = true;
            this.lbTurnout.Location = new System.Drawing.Point(3, 85);
            this.lbTurnout.Name = "lbTurnout";
            this.lbTurnout.Size = new System.Drawing.Size(80, 13);
            this.lbTurnout.TabIndex = 9;
            this.lbTurnout.Text = "Turnout (target)";
            // 
            // turnoutSelection
            // 
            this.turnoutSelection.AutoSize = true;
            this.turnoutSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.turnoutSelection.Location = new System.Drawing.Point(3, 101);
            this.turnoutSelection.Name = "turnoutSelection";
            this.turnoutSelection.Size = new System.Drawing.Size(108, 92);
            this.turnoutSelection.TabIndex = 10;
            this.turnoutSelection.TurnoutChanged += new System.EventHandler(this.turnoutSelection_TurnoutChanged);
            // 
            // lbSpeed
            // 
            this.lbSpeed.AutoSize = true;
            this.lbSpeed.Location = new System.Drawing.Point(345, 85);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(38, 13);
            this.lbSpeed.TabIndex = 13;
            this.lbSpeed.Text = "Speed";
            // 
            // cmdSetSpeed
            // 
            this.cmdSetSpeed.Location = new System.Drawing.Point(345, 199);
            this.cmdSetSpeed.Name = "cmdSetSpeed";
            this.cmdSetSpeed.Size = new System.Drawing.Size(90, 34);
            this.cmdSetSpeed.TabIndex = 15;
            this.cmdSetSpeed.Text = "Set speed";
            this.cmdSetSpeed.UseVisualStyleBackColor = true;
            this.cmdSetSpeed.Click += new System.EventHandler(this.cmdSetSpeed_Click);
            // 
            // lbRelays
            // 
            this.lbRelays.AutoSize = true;
            this.lbRelays.Location = new System.Drawing.Point(459, 85);
            this.lbRelays.Name = "lbRelays";
            this.lbRelays.Size = new System.Drawing.Size(39, 13);
            this.lbRelays.TabIndex = 16;
            this.lbRelays.Text = "Relays";
            // 
            // cbLeftLSB
            // 
            this.cbLeftLSB.AutoSize = true;
            this.cbLeftLSB.Checked = true;
            this.cbLeftLSB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLeftLSB.Location = new System.Drawing.Point(459, 101);
            this.cbLeftLSB.Name = "cbLeftLSB";
            this.cbLeftLSB.Size = new System.Drawing.Size(76, 17);
            this.cbLeftLSB.TabIndex = 17;
            this.cbLeftLSB.Text = "Left = LSB";
            this.cbLeftLSB.UseVisualStyleBackColor = true;
            // 
            // cmdSetRelay
            // 
            this.cmdSetRelay.Location = new System.Drawing.Point(459, 199);
            this.cmdSetRelay.Name = "cmdSetRelay";
            this.cmdSetRelay.Size = new System.Drawing.Size(90, 34);
            this.cmdSetRelay.TabIndex = 18;
            this.cmdSetRelay.Text = "Set relay position";
            this.cmdSetRelay.UseVisualStyleBackColor = true;
            this.cmdSetRelay.Click += new System.EventHandler(this.cmdSetRelay_Click);
            // 
            // lbIntro
            // 
            this.lbIntro.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.lbIntro, 5);
            this.lbIntro.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbIntro.Location = new System.Drawing.Point(3, 0);
            this.lbIntro.Margin = new System.Windows.Forms.Padding(3, 0, 3, 20);
            this.lbIntro.Name = "lbIntro";
            this.lbIntro.Size = new System.Drawing.Size(566, 65);
            this.lbIntro.TabIndex = 19;
            this.lbIntro.Text = "You\'re now in configuration mode.\r\n\r\nSelect the turnout you want to configure and" +
                " adjust the angles, speed and/or relay settings.\r\n\r\nTo leave configuration mode," +
                " close this window.";
            // 
            // ServoProgrammerStep4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.MinimumSize = new System.Drawing.Size(500, 38);
            this.Name = "ServoProgrammerStep4";
            this.Size = new System.Drawing.Size(572, 278);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udRight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown udLeft;
        private System.Windows.Forms.NumericUpDown udRight;
        private System.Windows.Forms.Button cmdSetRight;
        private System.Windows.Forms.Button cmdSetLeft;
        private System.Windows.Forms.Label lbTurnout;
        private TurnoutSelectionControl turnoutSelection;
        private System.Windows.Forms.NumericUpDown udSpeed;
        private System.Windows.Forms.Label lbSpeed;
        private System.Windows.Forms.Button cmdSetSpeed;
        private System.Windows.Forms.Label lbRelays;
        private System.Windows.Forms.CheckBox cbLeftLSB;
        private System.Windows.Forms.Button cmdSetRelay;
        private System.Windows.Forms.Label lbIntro;


    }
}