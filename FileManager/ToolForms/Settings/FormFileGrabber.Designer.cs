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
            this.components = new System.ComponentModel.Container();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.buttonXDisable = new DevComponents.DotNetBar.ButtonX();
            this.buttonXEnable = new DevComponents.DotNetBar.ButtonX();
            this.laSourceFolder = new System.Windows.Forms.Label();
            this.buttonEditSouceFolder = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditSouceFolder.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.Location = new System.Drawing.Point(113, 105);
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
            this.buttonXCancel.Location = new System.Drawing.Point(250, 105);
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
            this.laSourceFolder.Location = new System.Drawing.Point(12, 63);
            this.laSourceFolder.Name = "laSourceFolder";
            this.laSourceFolder.Size = new System.Drawing.Size(131, 16);
            this.laSourceFolder.TabIndex = 2;
            this.laSourceFolder.Text = "Source File Location:";
            // 
            // buttonEditSouceFolder
            // 
            this.buttonEditSouceFolder.Location = new System.Drawing.Point(168, 60);
            this.buttonEditSouceFolder.Name = "buttonEditSouceFolder";
            this.buttonEditSouceFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditSouceFolder.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.buttonEditSouceFolder.Size = new System.Drawing.Size(277, 22);
            this.buttonEditSouceFolder.StyleController = this.styleController;
            this.buttonEditSouceFolder.TabIndex = 14;
            this.buttonEditSouceFolder.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditSouceFolder_ButtonClick);
            // 
            // FormFileGrabber
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(457, 146);
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
            this.Text = "Overnights Email Grabber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormApplicationSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormApplicationSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditSouceFolder.Properties)).EndInit();
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
    }
}