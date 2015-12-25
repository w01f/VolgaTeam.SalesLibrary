namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
    partial class FormDeadLinks
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
			this.ckProcessDeadLinks = new System.Windows.Forms.CheckBox();
			this.ckShowDeadLinksWarningDialog = new System.Windows.Forms.CheckBox();
			this.gbDeadLinksMarkup = new DevExpress.XtraEditors.GroupControl();
			this.laMarkupAsLineBreakDescription = new System.Windows.Forms.Label();
			this.rbMarkupAsLineBreak = new System.Windows.Forms.RadioButton();
			this.laMarkupAsBoldDescription = new System.Windows.Forms.Label();
			this.rbMarkupAsBold = new System.Windows.Forms.RadioButton();
			this.laAdminCaption = new System.Windows.Forms.Label();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.gbDeadLinksMarkup)).BeginInit();
			this.gbDeadLinksMarkup.SuspendLayout();
			this.SuspendLayout();
			// 
			// ckProcessDeadLinks
			// 
			this.ckProcessDeadLinks.BackColor = System.Drawing.Color.White;
			this.ckProcessDeadLinks.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckProcessDeadLinks.ForeColor = System.Drawing.Color.Black;
			this.ckProcessDeadLinks.Location = new System.Drawing.Point(15, 77);
			this.ckProcessDeadLinks.Name = "ckProcessDeadLinks";
			this.ckProcessDeadLinks.Size = new System.Drawing.Size(477, 24);
			this.ckProcessDeadLinks.TabIndex = 3;
			this.ckProcessDeadLinks.Text = "Enable Inactive Link Manager";
			this.ckProcessDeadLinks.UseVisualStyleBackColor = false;
			this.ckProcessDeadLinks.CheckedChanged += new System.EventHandler(this.ckProcessDeadLinks_CheckedChanged);
			// 
			// ckShowDeadLinksWarningDialog
			// 
			this.ckShowDeadLinksWarningDialog.BackColor = System.Drawing.Color.White;
			this.ckShowDeadLinksWarningDialog.Enabled = false;
			this.ckShowDeadLinksWarningDialog.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ckShowDeadLinksWarningDialog.ForeColor = System.Drawing.Color.Black;
			this.ckShowDeadLinksWarningDialog.Location = new System.Drawing.Point(15, 241);
			this.ckShowDeadLinksWarningDialog.Name = "ckShowDeadLinksWarningDialog";
			this.ckShowDeadLinksWarningDialog.Size = new System.Drawing.Size(477, 35);
			this.ckShowDeadLinksWarningDialog.TabIndex = 2;
			this.ckShowDeadLinksWarningDialog.Text = " Show Warning Dialogue on Bad Links at Startup?";
			this.ckShowDeadLinksWarningDialog.UseVisualStyleBackColor = false;
			// 
			// gbDeadLinksMarkup
			// 
			this.gbDeadLinksMarkup.Appearance.BackColor = System.Drawing.Color.White;
			this.gbDeadLinksMarkup.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gbDeadLinksMarkup.Appearance.Options.UseBackColor = true;
			this.gbDeadLinksMarkup.Appearance.Options.UseForeColor = true;
			this.gbDeadLinksMarkup.AppearanceCaption.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gbDeadLinksMarkup.AppearanceCaption.Options.UseFont = true;
			this.gbDeadLinksMarkup.Controls.Add(this.laMarkupAsLineBreakDescription);
			this.gbDeadLinksMarkup.Controls.Add(this.rbMarkupAsLineBreak);
			this.gbDeadLinksMarkup.Controls.Add(this.laMarkupAsBoldDescription);
			this.gbDeadLinksMarkup.Controls.Add(this.rbMarkupAsBold);
			this.gbDeadLinksMarkup.Enabled = false;
			this.gbDeadLinksMarkup.Location = new System.Drawing.Point(11, 103);
			this.gbDeadLinksMarkup.Name = "gbDeadLinksMarkup";
			this.gbDeadLinksMarkup.Size = new System.Drawing.Size(483, 132);
			this.gbDeadLinksMarkup.TabIndex = 1;
			this.gbDeadLinksMarkup.Text = "How do you want Inactive Links to be Displayed to the IPadUsers?";
			// 
			// laMarkupAsLineBreakDescription
			// 
			this.laMarkupAsLineBreakDescription.BackColor = System.Drawing.Color.Transparent;
			this.laMarkupAsLineBreakDescription.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laMarkupAsLineBreakDescription.ForeColor = System.Drawing.Color.Black;
			this.laMarkupAsLineBreakDescription.Location = new System.Drawing.Point(24, 103);
			this.laMarkupAsLineBreakDescription.Name = "laMarkupAsLineBreakDescription";
			this.laMarkupAsLineBreakDescription.Size = new System.Drawing.Size(453, 17);
			this.laMarkupAsLineBreakDescription.TabIndex = 3;
			this.laMarkupAsLineBreakDescription.Text = "The Inactive Link will will simply be deleted and replaced with a Line Break...";
			// 
			// rbMarkupAsLineBreak
			// 
			this.rbMarkupAsLineBreak.AutoSize = true;
			this.rbMarkupAsLineBreak.BackColor = System.Drawing.Color.Transparent;
			this.rbMarkupAsLineBreak.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbMarkupAsLineBreak.ForeColor = System.Drawing.Color.Black;
			this.rbMarkupAsLineBreak.Location = new System.Drawing.Point(6, 82);
			this.rbMarkupAsLineBreak.Name = "rbMarkupAsLineBreak";
			this.rbMarkupAsLineBreak.Size = new System.Drawing.Size(166, 20);
			this.rbMarkupAsLineBreak.TabIndex = 2;
			this.rbMarkupAsLineBreak.Text = "Replace with Line Break";
			this.rbMarkupAsLineBreak.UseVisualStyleBackColor = false;
			// 
			// laMarkupAsBoldDescription
			// 
			this.laMarkupAsBoldDescription.BackColor = System.Drawing.Color.Transparent;
			this.laMarkupAsBoldDescription.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laMarkupAsBoldDescription.ForeColor = System.Drawing.Color.Black;
			this.laMarkupAsBoldDescription.Location = new System.Drawing.Point(24, 58);
			this.laMarkupAsBoldDescription.Name = "laMarkupAsBoldDescription";
			this.laMarkupAsBoldDescription.Size = new System.Drawing.Size(453, 17);
			this.laMarkupAsBoldDescription.TabIndex = 1;
			this.laMarkupAsBoldDescription.Text = "Will appear before the link to remind you which link is no longer working.";
			// 
			// rbMarkupAsBold
			// 
			this.rbMarkupAsBold.AutoSize = true;
			this.rbMarkupAsBold.BackColor = System.Drawing.Color.Transparent;
			this.rbMarkupAsBold.Checked = true;
			this.rbMarkupAsBold.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbMarkupAsBold.ForeColor = System.Drawing.Color.Black;
			this.rbMarkupAsBold.Location = new System.Drawing.Point(6, 37);
			this.rbMarkupAsBold.Name = "rbMarkupAsBold";
			this.rbMarkupAsBold.Size = new System.Drawing.Size(173, 20);
			this.rbMarkupAsBold.TabIndex = 0;
			this.rbMarkupAsBold.TabStop = true;
			this.rbMarkupAsBold.Text = "Bold Warning  INACTIVE!";
			this.rbMarkupAsBold.UseVisualStyleBackColor = false;
			// 
			// laAdminCaption
			// 
			this.laAdminCaption.BackColor = System.Drawing.Color.White;
			this.laAdminCaption.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdminCaption.ForeColor = System.Drawing.Color.Black;
			this.laAdminCaption.Location = new System.Drawing.Point(9, 9);
			this.laAdminCaption.Name = "laAdminCaption";
			this.laAdminCaption.Size = new System.Drawing.Size(483, 65);
			this.laAdminCaption.TabIndex = 0;
			this.laAdminCaption.Text = "In the event you accidentally delete a source file, and disable a Link in your Sa" +
    "les Library, you need to choose how you want inactive Links to be displayed:";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(274, 284);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 11;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(137, 284);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 10;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// FormDeadLinks
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(504, 328);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.ckProcessDeadLinks);
			this.Controls.Add(this.ckShowDeadLinksWarningDialog);
			this.Controls.Add(this.laAdminCaption);
			this.Controls.Add(this.gbDeadLinksMarkup);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDeadLinks";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Dead Links";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
			this.Load += new System.EventHandler(this.Form_Load);
			((System.ComponentModel.ISupportInitialize)(this.gbDeadLinksMarkup)).EndInit();
			this.gbDeadLinksMarkup.ResumeLayout(false);
			this.gbDeadLinksMarkup.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laAdminCaption;
        private DevExpress.XtraEditors.GroupControl gbDeadLinksMarkup;
        private System.Windows.Forms.Label laMarkupAsLineBreakDescription;
        private System.Windows.Forms.RadioButton rbMarkupAsLineBreak;
        private System.Windows.Forms.Label laMarkupAsBoldDescription;
        private System.Windows.Forms.RadioButton rbMarkupAsBold;
        private System.Windows.Forms.CheckBox ckShowDeadLinksWarningDialog;
        private System.Windows.Forms.CheckBox ckProcessDeadLinks;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
    }
}