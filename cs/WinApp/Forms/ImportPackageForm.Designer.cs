using System.Windows.Forms;

namespace BinkyRailways.WinApp.Forms
{
    partial class ImportPackageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportPackageForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbAll = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chType = new System.Windows.Forms.ColumnHeader();
            this.chRemarks = new System.Windows.Forms.ColumnHeader();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lbAllSensors = new System.Windows.Forms.Label();
            this.cmdCheckAll = new System.Windows.Forms.Button();
            this.cmdCheckNone = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.lbAll, 0, 1);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMain.Controls.Add(this.lbAllSensors, 0, 0);
            this.tlpMain.Name = "tlpMain";
            // 
            // lbAll
            // 
            this.lbAll.CheckBoxes = true;
            this.lbAll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chType,
            this.chRemarks});
            resources.ApplyResources(this.lbAll, "lbAll");
            this.lbAll.MultiSelect = false;
            this.lbAll.Name = "lbAll";
            this.tlpMain.SetRowSpan(this.lbAll, 2);
            this.lbAll.UseCompatibleStateImageBehavior = false;
            this.lbAll.View = System.Windows.Forms.View.Details;
            this.lbAll.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lbAll_ItemChecked);
            // 
            // chName
            // 
            resources.ApplyResources(this.chName, "chName");
            // 
            // chType
            // 
            resources.ApplyResources(this.chType, "chType");
            // 
            // chRemarks
            // 
            resources.ApplyResources(this.chRemarks, "chRemarks");
            // 
            // tlpButtons
            // 
            resources.ApplyResources(this.tlpButtons, "tlpButtons");
            this.tlpMain.SetColumnSpan(this.tlpButtons, 2);
            this.tlpButtons.Controls.Add(this.cmdOk, 3, 0);
            this.tlpButtons.Controls.Add(this.cmdCancel, 4, 0);
            this.tlpButtons.Controls.Add(this.cmdCheckAll, 0, 0);
            this.tlpButtons.Controls.Add(this.cmdCheckNone, 1, 0);
            this.tlpButtons.Name = "tlpButtons";
            // 
            // cmdOk
            // 
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // lbAllSensors
            // 
            resources.ApplyResources(this.lbAllSensors, "lbAllSensors");
            this.lbAllSensors.Name = "lbAllSensors";
            // 
            // cmdCheckAll
            // 
            resources.ApplyResources(this.cmdCheckAll, "cmdCheckAll");
            this.cmdCheckAll.Name = "cmdCheckAll";
            this.cmdCheckAll.UseVisualStyleBackColor = true;
            this.cmdCheckAll.Click += new System.EventHandler(this.cmdCheckAll_Click);
            // 
            // cmdCheckNone
            // 
            resources.ApplyResources(this.cmdCheckNone, "cmdCheckNone");
            this.cmdCheckNone.Name = "cmdCheckNone";
            this.cmdCheckNone.UseVisualStyleBackColor = true;
            this.cmdCheckNone.Click += new System.EventHandler(this.cmdCheckNone_Click);
            // 
            // ImportPackageForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.tlpMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportPackageForm";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbAllSensors;
        private System.Windows.Forms.ListView lbAll;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private ColumnHeader chName;
        private ColumnHeader chRemarks;
        private ColumnHeader chType;
        private Button cmdCheckAll;
        private Button cmdCheckNone;
    }
}