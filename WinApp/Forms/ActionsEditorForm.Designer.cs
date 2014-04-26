using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.Forms
{
    partial class ActionsEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionsEditorForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbAdd = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbRemove = new System.Windows.Forms.ToolStripButton();
            this.tvItems = new System.Windows.Forms.TreeView();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grid = new SettingsPropertyGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAdd,
            this.tbRemove});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tbAdd
            // 
            this.tbAdd.Image = global::BinkyRailways.Properties.Resources.add_22;
            resources.ApplyResources(this.tbAdd, "tbAdd");
            this.tbAdd.Name = "tbAdd";
            // 
            // tbRemove
            // 
            this.tbRemove.Image = global::BinkyRailways.Properties.Resources.remove_22;
            resources.ApplyResources(this.tbRemove, "tbRemove");
            this.tbRemove.Name = "tbRemove";
            this.tbRemove.Click += new System.EventHandler(this.tbRemove_Click);
            // 
            // tvItems
            // 
            resources.ApplyResources(this.tvItems, "tvItems");
            this.tvItems.Name = "tvItems";
            this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvItems_AfterSelect);
            // 
            // cmdOk
            // 
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // grid
            // 
            resources.ApplyResources(this.grid, "grid");
            this.grid.Name = "grid";
            this.grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.grid_PropertyValueChanged);
            // 
            // ActionsEditorForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.grid);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.tvItems);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActionsEditorForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton tbAdd;
        private System.Windows.Forms.TreeView tvItems;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolStripButton tbRemove;
        private SettingsPropertyGrid grid;
    }
}