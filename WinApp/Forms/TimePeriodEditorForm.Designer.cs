namespace BinkyRailways.WinApp.Forms
{
    partial class TimePeriodEditorForm
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbStart = new System.Windows.Forms.Label();
            this.lbEnd = new System.Windows.Forms.Label();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.tbEnd = new System.Windows.Forms.TextBox();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lbMessage = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lbStart, 0, 0);
            this.tlpMain.Controls.Add(this.lbEnd, 0, 1);
            this.tlpMain.Controls.Add(this.tbStart, 1, 0);
            this.tlpMain.Controls.Add(this.tbEnd, 1, 1);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMain.Controls.Add(this.lbMessage, 1, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(405, 121);
            this.tlpMain.TabIndex = 0;
            // 
            // lbStart
            // 
            this.lbStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbStart.AutoSize = true;
            this.lbStart.Location = new System.Drawing.Point(3, 6);
            this.lbStart.Name = "lbStart";
            this.lbStart.Size = new System.Drawing.Size(73, 13);
            this.lbStart.TabIndex = 0;
            this.lbStart.Text = "Start of period";
            // 
            // lbEnd
            // 
            this.lbEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbEnd.AutoSize = true;
            this.lbEnd.Location = new System.Drawing.Point(3, 32);
            this.lbEnd.Name = "lbEnd";
            this.lbEnd.Size = new System.Drawing.Size(70, 13);
            this.lbEnd.TabIndex = 1;
            this.lbEnd.Text = "End of period";
            // 
            // tbStart
            // 
            this.tbStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbStart.Location = new System.Drawing.Point(82, 3);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(320, 20);
            this.tbStart.TabIndex = 2;
            this.tbStart.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // tbEnd
            // 
            this.tbEnd.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbEnd.Location = new System.Drawing.Point(82, 29);
            this.tbEnd.Name = "tbEnd";
            this.tbEnd.Size = new System.Drawing.Size(320, 20);
            this.tbEnd.TabIndex = 3;
            this.tbEnd.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // tlpButtons
            // 
            this.tlpButtons.AutoSize = true;
            this.tlpButtons.ColumnCount = 3;
            this.tlpMain.SetColumnSpan(this.tlpButtons, 2);
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.Controls.Add(this.cmdOk, 1, 0);
            this.tlpButtons.Controls.Add(this.cmdCancel, 2, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpButtons.Location = new System.Drawing.Point(3, 75);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpButtons.Size = new System.Drawing.Size(399, 43);
            this.tlpButtons.TabIndex = 4;
            // 
            // cmdOk
            // 
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Location = new System.Drawing.Point(178, 3);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(106, 37);
            this.cmdOk.TabIndex = 0;
            this.cmdOk.Text = "&OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(290, 3);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(106, 37);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // lbMessage
            // 
            this.lbMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbMessage.AutoSize = true;
            this.lbMessage.Location = new System.Drawing.Point(82, 55);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(0, 13);
            this.lbMessage.TabIndex = 5;
            // 
            // TimePeriodEditorForm
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(405, 120);
            this.Controls.Add(this.tlpMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimePeriodEditorForm";
            this.Text = "Edit time period";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbStart;
        private System.Windows.Forms.Label lbEnd;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.TextBox tbEnd;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lbMessage;
    }
}