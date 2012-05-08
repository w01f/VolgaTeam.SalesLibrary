namespace FileManager.ToolForms
{
    partial class FormSyncProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSyncProgress));
            this.laUpdate = new System.Windows.Forms.Label();
            this.pbUpdate = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // laUpdate
            // 
            this.laUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laUpdate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laUpdate.Location = new System.Drawing.Point(3, 6);
            this.laUpdate.Name = "laUpdate";
            this.laUpdate.Size = new System.Drawing.Size(281, 23);
            this.laUpdate.TabIndex = 0;
            this.laUpdate.Text = "Converting, Saving and Synchronizing Files…";
            this.laUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbUpdate
            // 
            this.pbUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbUpdate.Location = new System.Drawing.Point(6, 32);
            this.pbUpdate.Name = "pbUpdate";
            this.pbUpdate.Size = new System.Drawing.Size(278, 16);
            this.pbUpdate.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbUpdate.TabIndex = 1;
            // 
            // SyncProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 56);
            this.ControlBox = false;
            this.Controls.Add(this.pbUpdate);
            this.Controls.Add(this.laUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SyncProgressForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save And Sync";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laUpdate;
        private System.Windows.Forms.ProgressBar pbUpdate;
    }
}