﻿namespace BinkyRailways.WinApp.Forms
{
    partial class LocSetEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocSetEditorForm));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbAll = new System.Windows.Forms.ListBox();
            this.lbSet = new System.Windows.Forms.ListBox();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lbSetSensors = new System.Windows.Forms.Label();
            this.lbAllSensors = new System.Windows.Forms.Label();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            resources.ApplyResources(this.tlpMain, "tlpMain");
            this.tlpMain.Controls.Add(this.lbAll, 0, 1);
            this.tlpMain.Controls.Add(this.lbSet, 2, 1);
            this.tlpMain.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMain.Controls.Add(this.lbSetSensors, 2, 0);
            this.tlpMain.Controls.Add(this.lbAllSensors, 0, 0);
            this.tlpMain.Controls.Add(this.cmdAdd, 1, 1);
            this.tlpMain.Controls.Add(this.cmdRemove, 1, 2);
            this.tlpMain.Name = "tlpMain";
            // 
            // lbAll
            // 
            resources.ApplyResources(this.lbAll, "lbAll");
            this.lbAll.FormattingEnabled = true;
            this.lbAll.Name = "lbAll";
            this.tlpMain.SetRowSpan(this.lbAll, 2);
            this.lbAll.SelectedIndexChanged += new System.EventHandler(this.lbLocs_SelectedIndexChanged);
            this.lbAll.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbAll_MouseDoubleClick);
            // 
            // lbSet
            // 
            resources.ApplyResources(this.lbSet, "lbSet");
            this.lbSet.FormattingEnabled = true;
            this.lbSet.Name = "lbSet";
            this.tlpMain.SetRowSpan(this.lbSet, 2);
            this.lbSet.SelectedIndexChanged += new System.EventHandler(this.lbBlocks_SelectedIndexChanged);
            this.lbSet.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSet_MouseDoubleClick);
            // 
            // tlpButtons
            // 
            resources.ApplyResources(this.tlpButtons, "tlpButtons");
            this.tlpMain.SetColumnSpan(this.tlpButtons, 4);
            this.tlpButtons.Controls.Add(this.cmdOk, 1, 0);
            this.tlpButtons.Controls.Add(this.cmdCancel, 2, 0);
            this.tlpButtons.Name = "tlpButtons";
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
            // lbSetSensors
            // 
            resources.ApplyResources(this.lbSetSensors, "lbSetSensors");
            this.lbSetSensors.Name = "lbSetSensors";
            // 
            // lbAllSensors
            // 
            resources.ApplyResources(this.lbAllSensors, "lbAllSensors");
            this.lbAllSensors.Name = "lbAllSensors";
            // 
            // cmdAdd
            // 
            resources.ApplyResources(this.cmdAdd, "cmdAdd");
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdRemove
            // 
            resources.ApplyResources(this.cmdRemove, "cmdRemove");
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // LocSetEditorForm
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.tlpMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LocSetEditorForm";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbAllSensors;
        private System.Windows.Forms.ListBox lbAll;
        private System.Windows.Forms.ListBox lbSet;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lbSetSensors;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdRemove;
    }
}