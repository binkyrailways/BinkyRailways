using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.Edit;

namespace BinkyRailways.WinApp.Forms
{
    partial class AddressEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressEditorForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbAddressType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.NumericUpDown();
            this.tbAddressSpace = new System.Windows.Forms.TextBox();
            this.cbType = new BinkyRailways.WinApp.Controls.Edit.AddressTypeCombo();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbValue)).BeginInit();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.lbAddressType, 0, 0);
            this.tlpMain.Controls.Add(this.label1, 0, 1);
            this.tlpMain.Controls.Add(this.label2, 0, 2);
            this.tlpMain.Controls.Add(this.tbValue, 1, 1);
            this.tlpMain.Controls.Add(this.tbAddressSpace, 1, 2);
            this.tlpMain.Controls.Add(this.cbType, 1, 0);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMain.Name = "tlpMain";
            // 
            // lbAddressType
            // 
            resources.ApplyResources(this.lbAddressType, "lbAddressType");
            this.lbAddressType.Name = "lbAddressType";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbValue
            // 
            resources.ApplyResources(this.tbValue, "tbValue");
            this.tbValue.Name = "tbValue";
            // 
            // tbAddressSpace
            // 
            resources.ApplyResources(this.tbAddressSpace, "tbAddressSpace");
            this.tbAddressSpace.Name = "tbAddressSpace";
            // 
            // cbType
            // 
            resources.ApplyResources(this.cbType, "cbType");
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Name = "cbType";
            this.cbType.SelectedItem = BinkyRailways.Core.Model.AddressType.Dcc;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // tlpButtons
            // 
            resources.ApplyResources(this.tlpButtons, "tlpButtons");
            this.tlpMain.SetColumnSpan(this.tlpButtons, 2);
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
            // 
            // AddressEditorForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddressEditorForm";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbValue)).EndInit();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbAddressType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown tbValue;
        private System.Windows.Forms.TextBox tbAddressSpace;
        private AddressTypeCombo cbType;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
    }
}