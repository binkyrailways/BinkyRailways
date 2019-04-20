using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.Edit;

namespace BinkyRailways.WinApp.Forms
{
    partial class ChooseComPortForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseComPortForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.lbPortNames = new System.Windows.Forms.ListBox();
            this.flpInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.lbInfo = new System.Windows.Forms.Label();
            this.cbChangeEntity = new System.Windows.Forms.CheckBox();
            this.tlpMain.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.flpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMain.Controls.Add(this.lbPortNames, 0, 1);
            this.tlpMain.Controls.Add(this.flpInfo, 0, 0);
            this.tlpMain.Controls.Add(this.cbChangeEntity, 0, 2);
            this.tlpMain.Name = "tlpMain";
            // 
            // tlpButtons
            // 
            resources.ApplyResources(this.tlpButtons, "tlpButtons");
            this.tlpButtons.Controls.Add(this.cmdCancel, 2, 0);
            this.tlpButtons.Controls.Add(this.cmdOk, 1, 0);
            this.tlpButtons.Name = "tlpButtons";
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // lbPortNames
            // 
            resources.ApplyResources(this.lbPortNames, "lbPortNames");
            this.lbPortNames.FormattingEnabled = true;
            this.lbPortNames.Name = "lbPortNames";
            this.lbPortNames.SelectedIndexChanged += new System.EventHandler(this.lbPortNames_SelectedIndexChanged);
            // 
            // flpInfo
            // 
            resources.ApplyResources(this.flpInfo, "flpInfo");
            this.flpInfo.Controls.Add(this.lbInfo);
            this.flpInfo.MaximumSize = new System.Drawing.Size(390, 0);
            this.flpInfo.Name = "flpInfo";
            // 
            // lbInfo
            // 
            resources.ApplyResources(this.lbInfo, "lbInfo");
            this.lbInfo.Name = "lbInfo";
            // 
            // cbChangeEntity
            // 
            resources.ApplyResources(this.cbChangeEntity, "cbChangeEntity");
            this.cbChangeEntity.Name = "cbChangeEntity";
            this.cbChangeEntity.UseVisualStyleBackColor = true;
            // 
            // ChooseComPortForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseComPortForm";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.flpInfo.ResumeLayout(false);
            this.flpInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.ListBox lbPortNames;
        private System.Windows.Forms.FlowLayoutPanel flpInfo;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.CheckBox cbChangeEntity;
    }
}