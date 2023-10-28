namespace BinkyRailways.WinApp.Forms
{
    partial class RestoreBlockAssignmentsForm
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
            this.lvLocs = new System.Windows.Forms.ListView();
            this.chLoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAssignTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.cmdAssign = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.chDirection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvLocs
            // 
            this.lvLocs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLocs.CheckBoxes = true;
            this.lvLocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLoc,
            this.chAssignTo,
            this.chDirection});
            this.lvLocs.FullRowSelect = true;
            this.lvLocs.HideSelection = false;
            this.lvLocs.Location = new System.Drawing.Point(8, 24);
            this.lvLocs.MultiSelect = false;
            this.lvLocs.Name = "lvLocs";
            this.lvLocs.Size = new System.Drawing.Size(437, 255);
            this.lvLocs.TabIndex = 1;
            this.lvLocs.UseCompatibleStateImageBehavior = false;
            this.lvLocs.View = System.Windows.Forms.View.Details;
            this.lvLocs.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.OnItemChecked);
            // 
            // chLoc
            // 
            this.chLoc.Text = "Loc";
            this.chLoc.Width = 125;
            // 
            // chAssignTo
            // 
            this.chAssignTo.Text = "Assign to";
            this.chAssignTo.Width = 213;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Restore all checked block assignments.";
            // 
            // cmdAssign
            // 
            this.cmdAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAssign.Location = new System.Drawing.Point(208, 288);
            this.cmdAssign.Name = "cmdAssign";
            this.cmdAssign.Size = new System.Drawing.Size(112, 32);
            this.cmdAssign.TabIndex = 2;
            this.cmdAssign.Text = "&Assign";
            this.cmdAssign.UseVisualStyleBackColor = true;
            this.cmdAssign.Click += new System.EventHandler(this.OnAssignClick);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(328, 288);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(112, 32);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.OnCancelClick);
            // 
            // chDirection
            // 
            this.chDirection.Text = "Loc direction";
            this.chDirection.Width = 89;
            // 
            // RestoreBlockAssignmentsForm
            // 
            this.AcceptButton = this.cmdAssign;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(452, 326);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAssign);
            this.Controls.Add(this.lvLocs);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RestoreBlockAssignmentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Restore block assignments";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvLocs;
        private System.Windows.Forms.ColumnHeader chLoc;
        private System.Windows.Forms.ColumnHeader chAssignTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdAssign;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ColumnHeader chDirection;
    }
}