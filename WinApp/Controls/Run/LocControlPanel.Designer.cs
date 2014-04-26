namespace BinkyRailways.WinApp.Controls.Run
{
    partial class LocControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocControlPanel));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmdForward = new System.Windows.Forms.Button();
            this.tbSpeed = new System.Windows.Forms.TrackBar();
            this.cmdReverse = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbF8 = new System.Windows.Forms.CheckBox();
            this.cbF7 = new System.Windows.Forms.CheckBox();
            this.cbF6 = new System.Windows.Forms.CheckBox();
            this.cbF5 = new System.Windows.Forms.CheckBox();
            this.cbF4 = new System.Windows.Forms.CheckBox();
            this.cbF3 = new System.Windows.Forms.CheckBox();
            this.cbF2 = new System.Windows.Forms.CheckBox();
            this.cbF0 = new System.Windows.Forms.CheckBox();
            this.cbF1 = new System.Windows.Forms.CheckBox();
            this.tlpTop = new System.Windows.Forms.TableLayoutPanel();
            this.locImage = new BinkyRailways.WinApp.Controls.Run.LocImage();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.cmdForward, 2, 3);
            this.tlpMain.Controls.Add(this.tbSpeed, 0, 2);
            this.tlpMain.Controls.Add(this.cmdReverse, 0, 3);
            this.tlpMain.Controls.Add(this.cmdStop, 1, 3);
            this.tlpMain.Controls.Add(this.tableLayoutPanel1, 0, 5);
            this.tlpMain.Controls.Add(this.tlpTop, 0, 1);
            this.tlpMain.Name = "tlpMain";
            // 
            // cmdForward
            // 
            resources.ApplyResources(this.cmdForward, "cmdForward");
            this.cmdForward.Image = global::BinkyRailways.Properties.Resources.forward_22;
            this.cmdForward.Name = "cmdForward";
            this.cmdForward.UseVisualStyleBackColor = true;
            this.cmdForward.Click += new System.EventHandler(this.cmdForward_Click);
            // 
            // tbSpeed
            // 
            this.tlpMain.SetColumnSpan(this.tbSpeed, 3);
            resources.ApplyResources(this.tbSpeed, "tbSpeed");
            this.tbSpeed.LargeChange = 10;
            this.tbSpeed.Maximum = 100;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.TickFrequency = 5;
            this.tbSpeed.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbSpeed.Scroll += new System.EventHandler(this.tbSpeed_Scroll);
            // 
            // cmdReverse
            // 
            this.cmdReverse.Image = global::BinkyRailways.Properties.Resources.back_22;
            resources.ApplyResources(this.cmdReverse, "cmdReverse");
            this.cmdReverse.Name = "cmdReverse";
            this.cmdReverse.UseVisualStyleBackColor = true;
            this.cmdReverse.Click += new System.EventHandler(this.cmdReverse_Click);
            // 
            // cmdStop
            // 
            resources.ApplyResources(this.cmdStop, "cmdStop");
            this.cmdStop.Image = global::BinkyRailways.Properties.Resources.stop_22;
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tlpMain.SetColumnSpan(this.tableLayoutPanel1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cbF8, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbF7, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbF6, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbF5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cbF4, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbF3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbF2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbF0, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbF1, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // cbF8
            // 
            resources.ApplyResources(this.cbF8, "cbF8");
            this.cbF8.Name = "cbF8";
            this.cbF8.UseVisualStyleBackColor = true;
            this.cbF8.CheckedChanged += new System.EventHandler(this.cbF8_CheckedChanged);
            // 
            // cbF7
            // 
            resources.ApplyResources(this.cbF7, "cbF7");
            this.cbF7.Name = "cbF7";
            this.cbF7.UseVisualStyleBackColor = true;
            this.cbF7.CheckedChanged += new System.EventHandler(this.cbF7_CheckedChanged);
            // 
            // cbF6
            // 
            resources.ApplyResources(this.cbF6, "cbF6");
            this.cbF6.Name = "cbF6";
            this.cbF6.UseVisualStyleBackColor = true;
            this.cbF6.CheckedChanged += new System.EventHandler(this.cbF6_CheckedChanged);
            // 
            // cbF5
            // 
            resources.ApplyResources(this.cbF5, "cbF5");
            this.cbF5.Name = "cbF5";
            this.cbF5.UseVisualStyleBackColor = true;
            this.cbF5.CheckedChanged += new System.EventHandler(this.cbF5_CheckedChanged);
            // 
            // cbF4
            // 
            resources.ApplyResources(this.cbF4, "cbF4");
            this.cbF4.Name = "cbF4";
            this.cbF4.UseVisualStyleBackColor = true;
            this.cbF4.CheckedChanged += new System.EventHandler(this.cbF4_CheckedChanged);
            // 
            // cbF3
            // 
            resources.ApplyResources(this.cbF3, "cbF3");
            this.cbF3.Name = "cbF3";
            this.cbF3.UseVisualStyleBackColor = true;
            this.cbF3.CheckedChanged += new System.EventHandler(this.cbF3_CheckedChanged);
            // 
            // cbF2
            // 
            resources.ApplyResources(this.cbF2, "cbF2");
            this.cbF2.Name = "cbF2";
            this.cbF2.UseVisualStyleBackColor = true;
            this.cbF2.CheckedChanged += new System.EventHandler(this.cbF2_CheckedChanged);
            // 
            // cbF0
            // 
            resources.ApplyResources(this.cbF0, "cbF0");
            this.tableLayoutPanel1.SetColumnSpan(this.cbF0, 4);
            this.cbF0.Name = "cbF0";
            this.cbF0.UseVisualStyleBackColor = true;
            this.cbF0.CheckedChanged += new System.EventHandler(this.cbF0_CheckedChanged);
            // 
            // cbF1
            // 
            resources.ApplyResources(this.cbF1, "cbF1");
            this.cbF1.Name = "cbF1";
            this.cbF1.UseVisualStyleBackColor = true;
            this.cbF1.CheckedChanged += new System.EventHandler(this.cbF1_CheckedChanged);
            // 
            // tlpTop
            // 
            resources.ApplyResources(this.tlpTop, "tlpTop");
            this.tlpMain.SetColumnSpan(this.tlpTop, 3);
            this.tlpTop.Controls.Add(this.locImage, 0, 0);
            this.tlpTop.Name = "tlpTop";
            // 
            // locImage
            // 
            resources.ApplyResources(this.locImage, "locImage");
            this.locImage.Name = "locImage";
            this.tlpTop.SetRowSpan(this.locImage, 3);
            // 
            // LocControlPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "LocControlPanel";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TrackBar tbSpeed;
        private System.Windows.Forms.Button cmdReverse;
        private System.Windows.Forms.Button cmdForward;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox cbF8;
        private System.Windows.Forms.CheckBox cbF7;
        private System.Windows.Forms.CheckBox cbF6;
        private System.Windows.Forms.CheckBox cbF5;
        private System.Windows.Forms.CheckBox cbF4;
        private System.Windows.Forms.CheckBox cbF3;
        private System.Windows.Forms.CheckBox cbF2;
        private System.Windows.Forms.CheckBox cbF0;
        private System.Windows.Forms.CheckBox cbF1;
        private System.Windows.Forms.TableLayoutPanel tlpTop;
        private LocImage locImage;

    }
}
