using System.Windows.Forms;

namespace BinkyRailways.WinApp.Forms
{
    partial class ImportMessagesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportMessagesForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbMessages = new System.Windows.Forms.ListView();
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.lbInfo = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.lbMessages, 0, 1);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMain.Controls.Add(this.lbInfo, 0, 0);
            this.tlpMain.Name = "tlpMain";
            // 
            // lbMessages
            // 
            this.lbMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMessage});
            resources.ApplyResources(this.lbMessages, "lbMessages");
            this.lbMessages.MultiSelect = false;
            this.lbMessages.Name = "lbMessages";
            this.tlpMain.SetRowSpan(this.lbMessages, 2);
            this.lbMessages.UseCompatibleStateImageBehavior = false;
            this.lbMessages.View = System.Windows.Forms.View.Details;
            // 
            // chMessage
            // 
            resources.ApplyResources(this.chMessage, "chMessage");
            // 
            // tlpButtons
            // 
            resources.ApplyResources(this.tlpButtons, "tlpButtons");
            this.tlpMain.SetColumnSpan(this.tlpButtons, 2);
            this.tlpButtons.Controls.Add(this.cmdOk, 3, 0);
            this.tlpButtons.Name = "tlpButtons";
            // 
            // cmdOk
            // 
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // lbInfo
            // 
            resources.ApplyResources(this.lbInfo, "lbInfo");
            this.lbInfo.Name = "lbInfo";
            // 
            // ImportMessagesForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOk;
            this.Controls.Add(this.tlpMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportMessagesForm";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.ListView lbMessages;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button cmdOk;
        private ColumnHeader chMessage;
    }
}