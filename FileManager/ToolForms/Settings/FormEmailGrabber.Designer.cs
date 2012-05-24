namespace FileManager.ToolForms.Settings
{
    partial class FormEmailGrabber
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
            this.components = new System.ComponentModel.Container();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.buttonXDisable = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEnable = new DevComponents.DotNetBar.ButtonX();
            this.textEditInboxSubFolder = new DevExpress.XtraEditors.TextEdit();
            this.laInboxSubFolder = new System.Windows.Forms.Label();
            this.spinEditGrabInterval = new DevExpress.XtraEditors.SpinEdit();
            this.laGrabInterval = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInboxSubFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditGrabInterval.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(67, 164);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(93, 32);
            this.buttonXOK.TabIndex = 8;
            this.buttonXOK.Text = "OK";
            this.buttonXOK.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.Location = new System.Drawing.Point(204, 164);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
            this.buttonXCancel.TabIndex = 9;
            this.buttonXCancel.Text = "Cancel";
            this.buttonXCancel.TextColor = System.Drawing.Color.Black;
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDisabled.Options.UseFont = true;
            this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDown.Options.UseFont = true;
            this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
            this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceFocused.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceReadOnly.Options.UseFont = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // buttonXDisable
            // 
            this.buttonXDisable.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXDisable.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXDisable.Location = new System.Drawing.Point(205, 12);
            this.buttonXDisable.Name = "buttonXDisable";
            this.buttonXDisable.Size = new System.Drawing.Size(148, 32);
            this.buttonXDisable.TabIndex = 13;
            this.buttonXDisable.Text = "Disable";
            this.buttonXDisable.TextColor = System.Drawing.Color.Black;
            this.buttonXDisable.CheckedChanged += new System.EventHandler(this.buttonXEnable_CheckedChanged);
            this.buttonXDisable.Click += new System.EventHandler(this.buttonXEnable_Click);
            // 
            // buttonXEnable
            // 
            this.buttonXEnable.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXEnable.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXEnable.Location = new System.Drawing.Point(12, 12);
            this.buttonXEnable.Name = "buttonXEnable";
            this.buttonXEnable.Size = new System.Drawing.Size(148, 32);
            this.buttonXEnable.TabIndex = 12;
            this.buttonXEnable.Text = "Enable";
            this.buttonXEnable.TextColor = System.Drawing.Color.Black;
            this.buttonXEnable.CheckedChanged += new System.EventHandler(this.buttonXEnable_CheckedChanged);
            this.buttonXEnable.Click += new System.EventHandler(this.buttonXEnable_Click);
            // 
            // textEditInboxSubFolder
            // 
            this.textEditInboxSubFolder.Location = new System.Drawing.Point(168, 112);
            this.textEditInboxSubFolder.Name = "textEditInboxSubFolder";
            this.textEditInboxSubFolder.Size = new System.Drawing.Size(185, 22);
            this.textEditInboxSubFolder.StyleController = this.styleController;
            this.textEditInboxSubFolder.TabIndex = 3;
            // 
            // laInboxSubFolder
            // 
            this.laInboxSubFolder.AutoSize = true;
            this.laInboxSubFolder.Location = new System.Drawing.Point(12, 115);
            this.laInboxSubFolder.Name = "laInboxSubFolder";
            this.laInboxSubFolder.Size = new System.Drawing.Size(150, 16);
            this.laInboxSubFolder.TabIndex = 2;
            this.laInboxSubFolder.Text = "Outlook Inbox Subfolder:";
            // 
            // spinEditGrabInterval
            // 
            this.spinEditGrabInterval.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditGrabInterval.Location = new System.Drawing.Point(168, 64);
            this.spinEditGrabInterval.Name = "spinEditGrabInterval";
            this.spinEditGrabInterval.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditGrabInterval.Properties.IsFloatValue = false;
            this.spinEditGrabInterval.Properties.Mask.EditMask = "N00";
            this.spinEditGrabInterval.Properties.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.spinEditGrabInterval.Size = new System.Drawing.Size(100, 22);
            this.spinEditGrabInterval.StyleController = this.styleController;
            this.spinEditGrabInterval.TabIndex = 1;
            // 
            // laGrabInterval
            // 
            this.laGrabInterval.AutoSize = true;
            this.laGrabInterval.Location = new System.Drawing.Point(12, 67);
            this.laGrabInterval.Name = "laGrabInterval";
            this.laGrabInterval.Size = new System.Drawing.Size(150, 16);
            this.laGrabInterval.TabIndex = 0;
            this.laGrabInterval.Text = "Email Grab Interval, min:";
            // 
            // FormEmailGrabber
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(365, 205);
            this.Controls.Add(this.textEditInboxSubFolder);
            this.Controls.Add(this.buttonXDisable);
            this.Controls.Add(this.laInboxSubFolder);
            this.Controls.Add(this.spinEditGrabInterval);
            this.Controls.Add(this.buttonXEnable);
            this.Controls.Add(this.laGrabInterval);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXOK);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEmailGrabber";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Overnights Email Grabber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApplicationSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormApplicationSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInboxSubFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditGrabInterval.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.TextEdit textEditInboxSubFolder;
        private System.Windows.Forms.Label laInboxSubFolder;
        private DevExpress.XtraEditors.SpinEdit spinEditGrabInterval;
        private System.Windows.Forms.Label laGrabInterval;
        private DevComponents.DotNetBar.ButtonX buttonXDisable;
        private DevComponents.DotNetBar.ButtonX buttonXEnable;
    }
}