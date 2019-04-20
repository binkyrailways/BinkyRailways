using System.Windows.Forms;

namespace BinkyRailways.WinApp.Forms
{
    sealed partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lbProductName = new System.Windows.Forms.Label();
            this.cmdOk = new System.Windows.Forms.Button();
            this.upgradeCheckWorker = new System.ComponentModel.BackgroundWorker();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbChecking = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::BinkyRailways.Properties.Resources.train_128;
            resources.ApplyResources(this.pbLogo, "pbLogo");
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.TabStop = false;
            // 
            // lbProductName
            // 
            resources.ApplyResources(this.lbProductName, "lbProductName");
            this.lbProductName.Name = "lbProductName";
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // upgradeCheckWorker
            // 
            this.upgradeCheckWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnUpdateCheckDoWork);
            this.upgradeCheckWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnUpdateCheckCompleted);
            // 
            // lbVersion
            // 
            resources.ApplyResources(this.lbVersion, "lbVersion");
            this.lbVersion.Name = "lbVersion";
            // 
            // lbChecking
            // 
            resources.ApplyResources(this.lbChecking, "lbChecking");
            this.lbChecking.Name = "lbChecking";
            this.lbChecking.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbChecking_LinkClicked);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOk;
            this.Controls.Add(this.lbChecking);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.lbProductName);
            this.Controls.Add(this.pbLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lbProductName;
        private System.Windows.Forms.Button cmdOk;
        private System.ComponentModel.BackgroundWorker upgradeCheckWorker;
        private System.Windows.Forms.Label lbVersion;
        private LinkLabel lbChecking;
    }
}