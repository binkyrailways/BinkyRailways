using BinkyRailways.WinApp.Controls.Edit;

namespace BinkyRailways.WinApp.Forms
{
    sealed partial class EditModuleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditModuleForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmdSave = new System.Windows.Forms.Button();
            this.editControl = new BinkyRailways.WinApp.Controls.Edit.EditModuleControl();
            this.cmdClose = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.cmdSave, 0, 1);
            this.tlpMain.Controls.Add(this.editControl, 0, 0);
            this.tlpMain.Controls.Add(this.cmdClose, 2, 1);
            this.tlpMain.Name = "tlpMain";
            // 
            // cmdSave
            // 
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // editControl
            // 
            this.tlpMain.SetColumnSpan(this.editControl, 3);
            resources.ApplyResources(this.editControl, "editControl");
            this.editControl.Name = "editControl";
            // 
            // cmdClose
            // 
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // EditModuleForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.Controls.Add(this.tlpMain);
            this.MinimizeBox = false;
            this.Name = "EditModuleForm";
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private EditModuleControl editControl;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdSave;
    }
}