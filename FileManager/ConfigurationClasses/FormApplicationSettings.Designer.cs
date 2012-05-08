namespace FileManager.ConfigurationClasses
{
    partial class FormApplicationSettings
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
            this.laBackup = new System.Windows.Forms.Label();
            this.btBackup = new System.Windows.Forms.Button();
            this.tbBackup = new System.Windows.Forms.TextBox();
            this.tbNetwork = new System.Windows.Forms.TextBox();
            this.btNetwork = new System.Windows.Forms.Button();
            this.laNetwork = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // laBackup
            // 
            this.laBackup.AutoSize = true;
            this.laBackup.Location = new System.Drawing.Point(12, 9);
            this.laBackup.Name = "laBackup";
            this.laBackup.Size = new System.Drawing.Size(136, 16);
            this.laBackup.TabIndex = 0;
            this.laBackup.Text = "Primary Backup Root:";
            // 
            // btBackup
            // 
            this.btBackup.Location = new System.Drawing.Point(336, 27);
            this.btBackup.Name = "btBackup";
            this.btBackup.Size = new System.Drawing.Size(25, 23);
            this.btBackup.TabIndex = 1;
            this.btBackup.Text = "..";
            this.btBackup.UseVisualStyleBackColor = true;
            this.btBackup.Click += new System.EventHandler(this.btBackup_Click);
            // 
            // tbBackup
            // 
            this.tbBackup.Location = new System.Drawing.Point(12, 28);
            this.tbBackup.Name = "tbBackup";
            this.tbBackup.Size = new System.Drawing.Size(325, 22);
            this.tbBackup.TabIndex = 2;
            // 
            // tbNetwork
            // 
            this.tbNetwork.Location = new System.Drawing.Point(12, 82);
            this.tbNetwork.Name = "tbNetwork";
            this.tbNetwork.Size = new System.Drawing.Size(325, 22);
            this.tbNetwork.TabIndex = 5;
            // 
            // btNetwork
            // 
            this.btNetwork.Location = new System.Drawing.Point(336, 81);
            this.btNetwork.Name = "btNetwork";
            this.btNetwork.Size = new System.Drawing.Size(25, 23);
            this.btNetwork.TabIndex = 4;
            this.btNetwork.Text = "..";
            this.btNetwork.UseVisualStyleBackColor = true;
            this.btNetwork.Click += new System.EventHandler(this.btNetwork_Click);
            // 
            // laNetwork
            // 
            this.laNetwork.AutoSize = true;
            this.laNetwork.Location = new System.Drawing.Point(12, 63);
            this.laNetwork.Name = "laNetwork";
            this.laNetwork.Size = new System.Drawing.Size(133, 16);
            this.laNetwork.TabIndex = 3;
            this.laNetwork.Text = "Network Sync Folder:";
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(77, 110);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 32);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(212, 110);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 32);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // FormApplicationSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(365, 154);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tbNetwork);
            this.Controls.Add(this.btNetwork);
            this.Controls.Add(this.laNetwork);
            this.Controls.Add(this.tbBackup);
            this.Controls.Add(this.btBackup);
            this.Controls.Add(this.laBackup);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormApplicationSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApplicationSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormApplicationSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label laBackup;
        private System.Windows.Forms.Button btBackup;
        private System.Windows.Forms.TextBox tbBackup;
        private System.Windows.Forms.TextBox tbNetwork;
        private System.Windows.Forms.Button btNetwork;
        private System.Windows.Forms.Label laNetwork;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
    }
}