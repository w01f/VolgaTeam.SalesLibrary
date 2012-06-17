namespace FileManager.ToolForms.Settings
{
    partial class FormFileGrabber
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
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.styleController = new DevExpress.XtraEditors.StyleController();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.buttonXDisable = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEnable = new DevComponents.DotNetBar.ButtonX();
            this.laSourceFolder = new System.Windows.Forms.Label();
            this.buttonEditSouceFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.spinEditGrabInterval = new DevExpress.XtraEditors.SpinEdit();
            this.laGrabInterval = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditSouceFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditGrabInterval.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(113, 162);
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
            this.buttonXCancel.Location = new System.Drawing.Point(250, 162);
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
            this.buttonXDisable.Location = new System.Drawing.Point(297, 12);
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
            // laSourceFolder
            // 
            this.laSourceFolder.AutoSize = true;
            this.laSourceFolder.Location = new System.Drawing.Point(12, 115);
            this.laSourceFolder.Name = "laSourceFolder";
            this.laSourceFolder.Size = new System.Drawing.Size(131, 16);
            this.laSourceFolder.TabIndex = 2;
            this.laSourceFolder.Text = "Source File Location:";
            // 
            // buttonEditSouceFolder
            // 
            this.buttonEditSouceFolder.Location = new System.Drawing.Point(168, 112);
            this.buttonEditSouceFolder.Name = "buttonEditSouceFolder";
            this.buttonEditSouceFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditSouceFolder.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.buttonEditSouceFolder.Size = new System.Drawing.Size(277, 22);
            this.buttonEditSouceFolder.StyleController = this.styleController;
            this.buttonEditSouceFolder.TabIndex = 14;
            this.buttonEditSouceFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditSouceFolder_ButtonClick);
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
            this.spinEditGrabInterval.TabIndex = 16;
            // 
            // laGrabInterval
            // 
            this.laGrabInterval.AutoSize = true;
            this.laGrabInterval.Location = new System.Drawing.Point(12, 67);
            this.laGrabInterval.Name = "laGrabInterval";
            this.laGrabInterval.Size = new System.Drawing.Size(138, 16);
            this.laGrabInterval.TabIndex = 15;
            this.laGrabInterval.Text = "File Grab Interval, min:";
            // 
            // FormFileGrabber
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(457, 203);
            this.Controls.Add(this.spinEditGrabInterval);
            this.Controls.Add(this.laGrabInterval);
            this.Controls.Add(this.buttonEditSouceFolder);
            this.Controls.Add(this.buttonXDisable);
            this.Controls.Add(this.laSourceFolder);
            this.Controls.Add(this.buttonXEnable);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXOK);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFileGrabber";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Overnights File Grabber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApplicationSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormApplicationSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditSouceFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditGrabInterval.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXOK;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Label laSourceFolder;
        private DevComponents.DotNetBar.ButtonX buttonXDisable;
        private DevComponents.DotNetBar.ButtonX buttonXEnable;
        private DevExpress.XtraEditors.ButtonEdit buttonEditSouceFolder;
        private DevExpress.XtraEditors.SpinEdit spinEditGrabInterval;
        private System.Windows.Forms.Label laGrabInterval;
    }
}