namespace BinkyRailways.WinApp.Forms
{
    partial class RenameEntitiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameEntitiesForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbIntro = new System.Windows.Forms.Label();
            this.tbExpression = new System.Windows.Forms.TextBox();
            this.tbPattern = new System.Windows.Forms.ToolStrip();
            this.tbInsertVariable = new System.Windows.Forms.ToolStripDropDownButton();
            this.lbEntities = new System.Windows.Forms.Label();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.chFrom = new System.Windows.Forms.ColumnHeader();
            this.chTo = new System.Windows.Forms.ColumnHeader();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.tbPattern.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.lbIntro, 0, 0);
            this.tlpMain.Controls.Add(this.tbExpression, 0, 1);
            this.tlpMain.Controls.Add(this.tbPattern, 0, 2);
            this.tlpMain.Controls.Add(this.lbEntities, 0, 3);
            this.tlpMain.Controls.Add(this.lvEntities, 0, 4);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 5);
            this.tlpMain.Name = "tlpMain";
            // 
            // lbIntro
            // 
            resources.ApplyResources(this.lbIntro, "lbIntro");
            this.lbIntro.Name = "lbIntro";
            // 
            // tbExpression
            // 
            resources.ApplyResources(this.tbExpression, "tbExpression");
            this.tbExpression.Name = "tbExpression";
            this.tbExpression.TextChanged += new System.EventHandler(this.tbExpression_TextChanged);
            // 
            // tbPattern
            // 
            this.tbPattern.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tbPattern.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbInsertVariable});
            resources.ApplyResources(this.tbPattern, "tbPattern");
            this.tbPattern.Name = "tbPattern";
            // 
            // tbInsertVariable
            // 
            this.tbInsertVariable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.tbInsertVariable, "tbInsertVariable");
            this.tbInsertVariable.Name = "tbInsertVariable";
            // 
            // lbEntities
            // 
            resources.ApplyResources(this.lbEntities, "lbEntities");
            this.lbEntities.Name = "lbEntities";
            // 
            // lvEntities
            // 
            this.lvEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFrom,
            this.chTo});
            resources.ApplyResources(this.lvEntities, "lvEntities");
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            // 
            // chFrom
            // 
            resources.ApplyResources(this.chFrom, "chFrom");
            // 
            // chTo
            // 
            resources.ApplyResources(this.chTo, "chTo");
            // 
            // tlpButtons
            // 
            resources.ApplyResources(this.tlpButtons, "tlpButtons");
            this.tlpButtons.Controls.Add(this.cmdOk, 1, 0);
            this.tlpButtons.Controls.Add(this.cmdCancel, 2, 0);
            this.tlpButtons.Name = "tlpButtons";
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // RenameEntitiesForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.tlpMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenameEntitiesForm";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tbPattern.ResumeLayout(false);
            this.tbPattern.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbIntro;
        private System.Windows.Forms.TextBox tbExpression;
        private System.Windows.Forms.ToolStrip tbPattern;
        private System.Windows.Forms.ToolStripDropDownButton tbInsertVariable;
        private System.Windows.Forms.Label lbEntities;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ColumnHeader chFrom;
        private System.Windows.Forms.ColumnHeader chTo;
    }
}