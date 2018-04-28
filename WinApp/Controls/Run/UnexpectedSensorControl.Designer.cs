namespace BinkyRailways.WinApp.Controls.Run
{
    partial class UnexpectedSensorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnexpectedSensorControl));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmdAssignBack = new System.Windows.Forms.Button();
            this.lvOptions = new System.Windows.Forms.ListView();
            this.chBlock = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdAssignFront = new System.Windows.Forms.Button();
            this.lbInfo = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.cmdAssignBack, 0, 2);
            this.tlpMain.Controls.Add(this.lvOptions, 0, 1);
            this.tlpMain.Controls.Add(this.cmdAssignFront, 1, 2);
            this.tlpMain.Controls.Add(this.lbInfo, 0, 0);
            this.tlpMain.Name = "tlpMain";
            // 
            // cmdAssignBack
            // 
            resources.ApplyResources(this.cmdAssignBack, "cmdAssignBack");
            this.cmdAssignBack.Name = "cmdAssignBack";
            this.cmdAssignBack.UseVisualStyleBackColor = true;
            this.cmdAssignBack.Click += new System.EventHandler(this.cmdAssignBack_Click);
            // 
            // lvOptions
            // 
            this.lvOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBlock});
            this.tlpMain.SetColumnSpan(this.lvOptions, 2);
            resources.ApplyResources(this.lvOptions, "lvOptions");
            this.lvOptions.HideSelection = false;
            this.lvOptions.MultiSelect = false;
            this.lvOptions.Name = "lvOptions";
            this.lvOptions.UseCompatibleStateImageBehavior = false;
            this.lvOptions.View = System.Windows.Forms.View.List;
            this.lvOptions.SelectedIndexChanged += new System.EventHandler(this.lvOptions_SelectedIndexChanged);
            // 
            // chBlock
            // 
            resources.ApplyResources(this.chBlock, "chBlock");
            // 
            // cmdAssignFront
            // 
            resources.ApplyResources(this.cmdAssignFront, "cmdAssignFront");
            this.cmdAssignFront.Name = "cmdAssignFront";
            this.cmdAssignFront.UseVisualStyleBackColor = true;
            this.cmdAssignFront.Click += new System.EventHandler(this.cmdAssignFront_Click);
            // 
            // lbInfo
            // 
            resources.ApplyResources(this.lbInfo, "lbInfo");
            this.lbInfo.Name = "lbInfo";
            // 
            // UnexpectedSensorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "UnexpectedSensorControl";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ListView lvOptions;
        private System.Windows.Forms.ColumnHeader chBlock;
        private System.Windows.Forms.Button cmdAssignFront;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button cmdAssignBack;
    }
}
