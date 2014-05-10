namespace BinkyRailways.WinApp.Forms
{
    partial class RouteInspectionForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.locListView = new BinkyRailways.WinApp.Controls.Run.LocListView();
            this.inspectionControl = new BinkyRailways.WinApp.Controls.Run.RouteInspectionControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.locListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.inspectionControl);
            this.splitContainer1.Size = new System.Drawing.Size(735, 365);
            this.splitContainer1.SplitterDistance = 245;
            this.splitContainer1.TabIndex = 2;
            // 
            // locListView
            // 
            this.locListView.AllowItemDrag = false;
            this.locListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.locListView.Location = new System.Drawing.Point(0, 0);
            this.locListView.Name = "locListView";
            this.locListView.ShowCheckboxes = false;
            this.locListView.Size = new System.Drawing.Size(245, 365);
            this.locListView.TabIndex = 0;
            this.locListView.UpdateTimerEnabled = false;
            this.locListView.SelectionChanged += new System.EventHandler(this.OnSelectionChanged);
            // 
            // inspectionControl
            // 
            this.inspectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inspectionControl.Loc = null;
            this.inspectionControl.Location = new System.Drawing.Point(0, 0);
            this.inspectionControl.Name = "inspectionControl";
            this.inspectionControl.Size = new System.Drawing.Size(486, 365);
            this.inspectionControl.TabIndex = 2;
            // 
            // RouteInspectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 365);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RouteInspectionForm";
            this.Text = "Route Inspector";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.Run.LocListView locListView;
        private Controls.Run.RouteInspectionControl inspectionControl;

    }
}