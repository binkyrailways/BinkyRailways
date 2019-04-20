namespace LocoNetToolBox.WinApp.Controls
{
    partial class CommandControl
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
            this.cmdQuery = new System.Windows.Forms.Button();
            this.lbFunctions = new System.Windows.Forms.Label();
            this.cmdAdvanced = new System.Windows.Forms.Button();
            this.cmdServoTester = new System.Windows.Forms.Button();
            this.cmdServoProgrammer = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.cmdQuery, 0, 5);
            this.tlpMain.Controls.Add(this.lbFunctions, 0, 3);
            this.tlpMain.Controls.Add(this.cmdAdvanced, 0, 11);
            this.tlpMain.Controls.Add(this.cmdServoTester, 0, 9);
            this.tlpMain.Controls.Add(this.cmdServoProgrammer, 0, 10);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 12;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(376, 223);
            this.tlpMain.TabIndex = 4;
            // 
            // cmdQuery
            // 
            this.cmdQuery.Location = new System.Drawing.Point(3, 22);
            this.cmdQuery.Name = "cmdQuery";
            this.cmdQuery.Size = new System.Drawing.Size(133, 45);
            this.cmdQuery.TabIndex = 11;
            this.cmdQuery.Text = "Find all MGV50\'s";
            this.cmdQuery.UseVisualStyleBackColor = true;
            this.cmdQuery.Click += new System.EventHandler(this.CmdQueryClick);
            // 
            // lbFunctions
            // 
            this.lbFunctions.AutoSize = true;
            this.lbFunctions.Location = new System.Drawing.Point(3, 3);
            this.lbFunctions.Margin = new System.Windows.Forms.Padding(3);
            this.lbFunctions.Name = "lbFunctions";
            this.lbFunctions.Size = new System.Drawing.Size(53, 13);
            this.lbFunctions.TabIndex = 18;
            this.lbFunctions.Text = "Functions";
            // 
            // cmdAdvanced
            // 
            this.cmdAdvanced.Location = new System.Drawing.Point(3, 175);
            this.cmdAdvanced.Name = "cmdAdvanced";
            this.cmdAdvanced.Size = new System.Drawing.Size(133, 45);
            this.cmdAdvanced.TabIndex = 19;
            this.cmdAdvanced.Text = "Advanced";
            this.cmdAdvanced.UseVisualStyleBackColor = true;
            this.cmdAdvanced.Click += new System.EventHandler(this.cmdAdvanced_Click);
            // 
            // cmdServoTester
            // 
            this.cmdServoTester.Location = new System.Drawing.Point(3, 73);
            this.cmdServoTester.Name = "cmdServoTester";
            this.cmdServoTester.Size = new System.Drawing.Size(133, 45);
            this.cmdServoTester.TabIndex = 20;
            this.cmdServoTester.Text = "Servo Tester";
            this.cmdServoTester.UseVisualStyleBackColor = true;
            this.cmdServoTester.Click += new System.EventHandler(this.CmdServoTesterClick);
            // 
            // cmdServoProgrammer
            // 
            this.cmdServoProgrammer.Location = new System.Drawing.Point(3, 124);
            this.cmdServoProgrammer.Name = "cmdServoProgrammer";
            this.cmdServoProgrammer.Size = new System.Drawing.Size(133, 45);
            this.cmdServoProgrammer.TabIndex = 17;
            this.cmdServoProgrammer.Text = "Servo Configurator";
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
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button cmdQuery;
        private System.Windows.Forms.Button cmdServoProgrammer;
        private System.Windows.Forms.Label lbFunctions;
        private System.Windows.Forms.Button cmdAdvanced;
        private System.Windows.Forms.Button cmdServoTester;
    }
}
