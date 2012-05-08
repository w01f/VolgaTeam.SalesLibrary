namespace SalesDepot.ConfigurationClasses
{
    partial class FormEmailSettings
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
            this.btSave = new System.Windows.Forms.Button();
            this.groupBoxEmail = new System.Windows.Forms.GroupBox();
            this.rbEmailButtonsDisableAll = new System.Windows.Forms.RadioButton();
            this.ckEnableViewOptions = new System.Windows.Forms.CheckBox();
            this.ckEnableQuickView = new System.Windows.Forms.CheckBox();
            this.ckEnableEmailBin = new System.Windows.Forms.CheckBox();
            this.rbEmailButtonsEnableAll = new System.Windows.Forms.RadioButton();
            this.rbEmailButtonsEnaiblePartial = new System.Windows.Forms.RadioButton();
            this.btCancel = new System.Windows.Forms.Button();
            this.rbLaunchPPTAtStartup = new System.Windows.Forms.RadioButton();
            this.rbNotLaunchPPTAtStartUp = new System.Windows.Forms.RadioButton();
            this.groupBoxEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btSave.Location = new System.Drawing.Point(335, 12);
            this.btSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(107, 33);
            this.btSave.TabIndex = 18;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btOK_Click);
            // 
            // groupBoxEmail
            // 
            this.groupBoxEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEmail.Controls.Add(this.rbEmailButtonsDisableAll);
            this.groupBoxEmail.Controls.Add(this.ckEnableViewOptions);
            this.groupBoxEmail.Controls.Add(this.ckEnableQuickView);
            this.groupBoxEmail.Controls.Add(this.ckEnableEmailBin);
            this.groupBoxEmail.Controls.Add(this.rbEmailButtonsEnableAll);
            this.groupBoxEmail.Controls.Add(this.rbEmailButtonsEnaiblePartial);
            this.groupBoxEmail.Location = new System.Drawing.Point(5, 5);
            this.groupBoxEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxEmail.Name = "groupBoxEmail";
            this.groupBoxEmail.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxEmail.Size = new System.Drawing.Size(312, 204);
            this.groupBoxEmail.TabIndex = 22;
            this.groupBoxEmail.TabStop = false;
            // 
            // rbEmailButtonsDisableAll
            // 
            this.rbEmailButtonsDisableAll.AutoSize = true;
            this.rbEmailButtonsDisableAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbEmailButtonsDisableAll.Location = new System.Drawing.Point(7, 176);
            this.rbEmailButtonsDisableAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbEmailButtonsDisableAll.Name = "rbEmailButtonsDisableAll";
            this.rbEmailButtonsDisableAll.Size = new System.Drawing.Size(173, 20);
            this.rbEmailButtonsDisableAll.TabIndex = 24;
            this.rbEmailButtonsDisableAll.Text = "Disable All Email Buttons";
            this.rbEmailButtonsDisableAll.UseVisualStyleBackColor = true;
            // 
            // ckEnableViewOptions
            // 
            this.ckEnableViewOptions.AutoSize = true;
            this.ckEnableViewOptions.Location = new System.Drawing.Point(28, 144);
            this.ckEnableViewOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckEnableViewOptions.Name = "ckEnableViewOptions";
            this.ckEnableViewOptions.Size = new System.Drawing.Size(104, 20);
            this.ckEnableViewOptions.TabIndex = 23;
            this.ckEnableViewOptions.Text = "View Options";
            this.ckEnableViewOptions.UseVisualStyleBackColor = true;
            // 
            // ckEnableQuickView
            // 
            this.ckEnableQuickView.AutoSize = true;
            this.ckEnableQuickView.Location = new System.Drawing.Point(28, 112);
            this.ckEnableQuickView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckEnableQuickView.Name = "ckEnableQuickView";
            this.ckEnableQuickView.Size = new System.Drawing.Size(89, 20);
            this.ckEnableQuickView.TabIndex = 22;
            this.ckEnableQuickView.Text = "QuickView";
            this.ckEnableQuickView.UseVisualStyleBackColor = true;
            // 
            // ckEnableEmailBin
            // 
            this.ckEnableEmailBin.AutoSize = true;
            this.ckEnableEmailBin.Location = new System.Drawing.Point(28, 80);
            this.ckEnableEmailBin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckEnableEmailBin.Name = "ckEnableEmailBin";
            this.ckEnableEmailBin.Size = new System.Drawing.Size(83, 20);
            this.ckEnableEmailBin.TabIndex = 21;
            this.ckEnableEmailBin.Text = "Email Bin";
            this.ckEnableEmailBin.UseVisualStyleBackColor = true;
            // 
            // rbEmailButtonsEnableAll
            // 
            this.rbEmailButtonsEnableAll.AutoSize = true;
            this.rbEmailButtonsEnableAll.Checked = true;
            this.rbEmailButtonsEnableAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbEmailButtonsEnableAll.Location = new System.Drawing.Point(7, 16);
            this.rbEmailButtonsEnableAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbEmailButtonsEnableAll.Name = "rbEmailButtonsEnableAll";
            this.rbEmailButtonsEnableAll.Size = new System.Drawing.Size(170, 20);
            this.rbEmailButtonsEnableAll.TabIndex = 19;
            this.rbEmailButtonsEnableAll.TabStop = true;
            this.rbEmailButtonsEnableAll.Text = "Enable All Email Buttons";
            this.rbEmailButtonsEnableAll.UseVisualStyleBackColor = true;
            // 
            // rbEmailButtonsEnaiblePartial
            // 
            this.rbEmailButtonsEnaiblePartial.AutoSize = true;
            this.rbEmailButtonsEnaiblePartial.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbEmailButtonsEnaiblePartial.Location = new System.Drawing.Point(7, 48);
            this.rbEmailButtonsEnaiblePartial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbEmailButtonsEnaiblePartial.Name = "rbEmailButtonsEnaiblePartial";
            this.rbEmailButtonsEnaiblePartial.Size = new System.Drawing.Size(226, 20);
            this.rbEmailButtonsEnaiblePartial.TabIndex = 20;
            this.rbEmailButtonsEnaiblePartial.Text = "Enable Only These Email Buttons:";
            this.rbEmailButtonsEnaiblePartial.UseVisualStyleBackColor = true;
            this.rbEmailButtonsEnaiblePartial.CheckedChanged += new System.EventHandler(this.rbEmailButtons_CheckedChanged);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btCancel.Location = new System.Drawing.Point(335, 53);
            this.btCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(107, 33);
            this.btCancel.TabIndex = 22;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // rbLaunchPPTAtStartup
            // 
            this.rbLaunchPPTAtStartup.AutoSize = true;
            this.rbLaunchPPTAtStartup.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbLaunchPPTAtStartup.Location = new System.Drawing.Point(6, 13);
            this.rbLaunchPPTAtStartup.Name = "rbLaunchPPTAtStartup";
            this.rbLaunchPPTAtStartup.Size = new System.Drawing.Size(199, 20);
            this.rbLaunchPPTAtStartup.TabIndex = 19;
            this.rbLaunchPPTAtStartup.Text = "Launch PowerPoint at Startup";
            this.rbLaunchPPTAtStartup.UseVisualStyleBackColor = true;
            // 
            // rbNotLaunchPPTAtStartUp
            // 
            this.rbNotLaunchPPTAtStartUp.AutoSize = true;
            this.rbNotLaunchPPTAtStartUp.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbNotLaunchPPTAtStartUp.Location = new System.Drawing.Point(6, 39);
            this.rbNotLaunchPPTAtStartUp.Name = "rbNotLaunchPPTAtStartUp";
            this.rbNotLaunchPPTAtStartUp.Size = new System.Drawing.Size(249, 20);
            this.rbNotLaunchPPTAtStartUp.TabIndex = 20;
            this.rbNotLaunchPPTAtStartUp.Text = "Do NOT Launch PowerPoint at Startup";
            this.rbNotLaunchPPTAtStartUp.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(454, 222);
            this.Controls.Add(this.groupBoxEmail);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBoxEmail.ResumeLayout(false);
            this.groupBoxEmail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.RadioButton rbLaunchPPTAtStartup;
        private System.Windows.Forms.RadioButton rbNotLaunchPPTAtStartUp;
        private System.Windows.Forms.GroupBox groupBoxEmail;
        private System.Windows.Forms.RadioButton rbEmailButtonsEnableAll;
        private System.Windows.Forms.RadioButton rbEmailButtonsEnaiblePartial;
        private System.Windows.Forms.RadioButton rbEmailButtonsDisableAll;
        private System.Windows.Forms.CheckBox ckEnableViewOptions;
        private System.Windows.Forms.CheckBox ckEnableQuickView;
        private System.Windows.Forms.CheckBox ckEnableEmailBin;
    }
}