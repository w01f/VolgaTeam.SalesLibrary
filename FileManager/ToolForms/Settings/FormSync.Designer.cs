namespace FileManager.ToolForms.Settings
{
    partial class FormSync
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
			this.ckShowSyncStatus = new System.Windows.Forms.CheckBox();
			this.ckCloseAfterSync = new System.Windows.Forms.CheckBox();
			this.ckMinimizeOnSync = new System.Windows.Forms.CheckBox();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.ckFullSync = new System.Windows.Forms.CheckBox();
			this.ckVideoConversionWarning = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// ckShowSyncStatus
			// 
			this.ckShowSyncStatus.AutoSize = true;
			this.ckShowSyncStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckShowSyncStatus.Location = new System.Drawing.Point(12, 96);
			this.ckShowSyncStatus.Name = "ckShowSyncStatus";
			this.ckShowSyncStatus.Size = new System.Drawing.Size(198, 20);
			this.ckShowSyncStatus.TabIndex = 20;
			this.ckShowSyncStatus.Text = "Show Status Bar during Sync";
			this.ckShowSyncStatus.UseVisualStyleBackColor = true;
			// 
			// ckCloseAfterSync
			// 
			this.ckCloseAfterSync.AutoSize = true;
			this.ckCloseAfterSync.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckCloseAfterSync.Location = new System.Drawing.Point(12, 54);
			this.ckCloseAfterSync.Name = "ckCloseAfterSync";
			this.ckCloseAfterSync.Size = new System.Drawing.Size(123, 20);
			this.ckCloseAfterSync.TabIndex = 19;
			this.ckCloseAfterSync.Text = "Close after Sync";
			this.ckCloseAfterSync.UseVisualStyleBackColor = true;
			// 
			// ckMinimizeOnSync
			// 
			this.ckMinimizeOnSync.AutoSize = true;
			this.ckMinimizeOnSync.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckMinimizeOnSync.Location = new System.Drawing.Point(12, 12);
			this.ckMinimizeOnSync.Name = "ckMinimizeOnSync";
			this.ckMinimizeOnSync.Size = new System.Drawing.Size(131, 20);
			this.ckMinimizeOnSync.TabIndex = 18;
			this.ckMinimizeOnSync.Text = "Minimize on Sync";
			this.ckMinimizeOnSync.UseVisualStyleBackColor = true;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(151, 235);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 15;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(44, 235);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 14;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// ckFullSync
			// 
			this.ckFullSync.AutoSize = true;
			this.ckFullSync.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckFullSync.Location = new System.Drawing.Point(12, 134);
			this.ckFullSync.Name = "ckFullSync";
			this.ckFullSync.Size = new System.Drawing.Size(184, 36);
			this.ckFullSync.TabIndex = 21;
			this.ckFullSync.Text = "Sync Local AND iPad \r\n(Will take much more time)";
			this.ckFullSync.UseVisualStyleBackColor = true;
			// 
			// ckVideoConversionWarning
			// 
			this.ckVideoConversionWarning.AutoSize = true;
			this.ckVideoConversionWarning.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckVideoConversionWarning.Location = new System.Drawing.Point(12, 185);
			this.ckVideoConversionWarning.Name = "ckVideoConversionWarning";
			this.ckVideoConversionWarning.Size = new System.Drawing.Size(176, 36);
			this.ckVideoConversionWarning.TabIndex = 22;
			this.ckVideoConversionWarning.Text = "Warn me of ANY Needed \r\nVideo Conversions";
			this.ckVideoConversionWarning.UseVisualStyleBackColor = true;
			// 
			// FormSync
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(279, 279);
			this.Controls.Add(this.ckVideoConversionWarning);
			this.Controls.Add(this.ckFullSync);
			this.Controls.Add(this.ckShowSyncStatus);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.ckCloseAfterSync);
			this.Controls.Add(this.ckMinimizeOnSync);
			this.Controls.Add(this.buttonXOK);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSync";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Synchronizing";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSync_FormClosed);
			this.Load += new System.EventHandler(this.Form_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckShowSyncStatus;
        private System.Windows.Forms.CheckBox ckCloseAfterSync;
        private System.Windows.Forms.CheckBox ckMinimizeOnSync;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
		private System.Windows.Forms.CheckBox ckFullSync;
		private System.Windows.Forms.CheckBox ckVideoConversionWarning;
    }
}