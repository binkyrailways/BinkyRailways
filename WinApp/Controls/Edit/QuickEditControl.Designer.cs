using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.Controls.Edit
{
    partial class QuickEditControl
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
            this.outerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tvItems = new EntityTreeView();
            this.nestedSplitContainer = new System.Windows.Forms.SplitContainer();
            this.viewEditor = new BinkyRailways.WinApp.Controls.Edit.ModuleViewEditorControl();
            this.grid = new BinkyRailways.WinApp.Controls.Edit.Settings.SettingsPropertyGrid();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outerSplitContainer)).BeginInit();
            this.outerSplitContainer.Panel1.SuspendLayout();
            this.outerSplitContainer.Panel2.SuspendLayout();
            this.outerSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nestedSplitContainer)).BeginInit();
            this.nestedSplitContainer.Panel1.SuspendLayout();
            this.nestedSplitContainer.Panel2.SuspendLayout();
            this.nestedSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.outerSplitContainer, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(697, 467);
            this.tlpMain.TabIndex = 0;
            // 
            // outerSplitContainer
            // 
            this.outerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.outerSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.outerSplitContainer.Name = "outerSplitContainer";
            // 
            // outerSplitContainer.Panel1
            // 
            this.outerSplitContainer.Panel1.Controls.Add(this.tvItems);
            this.outerSplitContainer.Panel1MinSize = 200;
            // 
            // outerSplitContainer.Panel2
            // 
            this.outerSplitContainer.Panel2.Controls.Add(this.nestedSplitContainer);
            this.outerSplitContainer.Size = new System.Drawing.Size(691, 461);
            this.outerSplitContainer.SplitterDistance = 200;
            this.outerSplitContainer.TabIndex = 2;
            // 
            // tvItems
            // 
            this.tvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvItems.Location = new System.Drawing.Point(0, 0);
            this.tvItems.Name = "tvItems";
            this.tvItems.Size = new System.Drawing.Size(200, 461);
            this.tvItems.TabIndex = 2;
            // 
            // nestedSplitContainer
            // 
            this.nestedSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nestedSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.nestedSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.nestedSplitContainer.Name = "nestedSplitContainer";
            // 
            // nestedSplitContainer.Panel1
            // 
            this.nestedSplitContainer.Panel1.Controls.Add(this.viewEditor);
            // 
            // nestedSplitContainer.Panel2
            // 
            this.nestedSplitContainer.Panel2.Controls.Add(this.grid);
            this.nestedSplitContainer.Panel2MinSize = 250;
            this.nestedSplitContainer.Size = new System.Drawing.Size(487, 461);
            this.nestedSplitContainer.SplitterDistance = 233;
            this.nestedSplitContainer.TabIndex = 1;
            // 
            // viewEditor
            // 
            this.viewEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewEditor.Location = new System.Drawing.Point(0, 0);
            this.viewEditor.Name = "viewEditor";
            this.viewEditor.Size = new System.Drawing.Size(233, 461);
            this.viewEditor.TabIndex = 1;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.InRunningState = false;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(250, 461);
            this.grid.TabIndex = 0;
            // 
            // QuickEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "QuickEditControl";
            this.Size = new System.Drawing.Size(697, 467);
            this.tlpMain.ResumeLayout(false);
            this.outerSplitContainer.Panel1.ResumeLayout(false);
            this.outerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outerSplitContainer)).EndInit();
            this.outerSplitContainer.ResumeLayout(false);
            this.nestedSplitContainer.Panel1.ResumeLayout(false);
            this.nestedSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nestedSplitContainer)).EndInit();
            this.nestedSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.SplitContainer outerSplitContainer;
        private EntityTreeView tvItems;
        private System.Windows.Forms.SplitContainer nestedSplitContainer;
        private ModuleViewEditorControl viewEditor;
        private SettingsPropertyGrid grid;

    }
}
