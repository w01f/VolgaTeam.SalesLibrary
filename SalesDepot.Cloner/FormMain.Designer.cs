namespace SalesDepot.Cloner
{
    partial class FormMain
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
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.buttonEditSource = new DevExpress.XtraEditors.ButtonEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.laSource = new System.Windows.Forms.Label();
			this.laDestination = new System.Windows.Forms.Label();
			this.buttonEditDestination = new DevExpress.XtraEditors.ButtonEdit();
			this.simpleButtonSplit = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonExit = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditSource.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditDestination.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Lilian";
			// 
			// buttonEditSource
			// 
			this.buttonEditSource.Location = new System.Drawing.Point(15, 28);
			this.buttonEditSource.Name = "buttonEditSource";
			this.buttonEditSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditSource.Properties.NullText = "Select Folder...";
			this.buttonEditSource.Size = new System.Drawing.Size(395, 22);
			this.buttonEditSource.StyleController = this.styleController;
			this.buttonEditSource.TabIndex = 1;
			this.buttonEditSource.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditSource_ButtonClick);
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
			// laSource
			// 
			this.laSource.AutoSize = true;
			this.laSource.Location = new System.Drawing.Point(12, 9);
			this.laSource.Name = "laSource";
			this.laSource.Size = new System.Drawing.Size(171, 16);
			this.laSource.TabIndex = 1;
			this.laSource.Text = "Source  Library Folder Path:";
			// 
			// laDestination
			// 
			this.laDestination.AutoSize = true;
			this.laDestination.Location = new System.Drawing.Point(12, 66);
			this.laDestination.Name = "laDestination";
			this.laDestination.Size = new System.Drawing.Size(191, 16);
			this.laDestination.TabIndex = 3;
			this.laDestination.Text = "Destination Library Folder Path:";
			// 
			// buttonEditDestination
			// 
			this.buttonEditDestination.Location = new System.Drawing.Point(15, 85);
			this.buttonEditDestination.Name = "buttonEditDestination";
			this.buttonEditDestination.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditDestination.Properties.NullText = "Select Folder...";
			this.buttonEditDestination.Size = new System.Drawing.Size(395, 22);
			this.buttonEditDestination.StyleController = this.styleController;
			this.buttonEditDestination.TabIndex = 2;
			this.buttonEditDestination.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEditDestination_ButtonClick);
			// 
			// simpleButtonSplit
			// 
			this.simpleButtonSplit.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonSplit.Appearance.Options.UseFont = true;
			this.simpleButtonSplit.Location = new System.Drawing.Point(15, 126);
			this.simpleButtonSplit.Name = "simpleButtonSplit";
			this.simpleButtonSplit.Size = new System.Drawing.Size(122, 41);
			this.simpleButtonSplit.TabIndex = 0;
			this.simpleButtonSplit.Text = "Run process";
			this.simpleButtonSplit.Click += new System.EventHandler(this.simpleButtonSplit_Click);
			// 
			// simpleButtonExit
			// 
			this.simpleButtonExit.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonExit.Appearance.Options.UseFont = true;
			this.simpleButtonExit.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.simpleButtonExit.Location = new System.Drawing.Point(288, 126);
			this.simpleButtonExit.Name = "simpleButtonExit";
			this.simpleButtonExit.Size = new System.Drawing.Size(122, 41);
			this.simpleButtonExit.TabIndex = 3;
			this.simpleButtonExit.Text = "Exit";
			this.simpleButtonExit.Click += new System.EventHandler(this.simpleButtonExit_Click);
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(426, 179);
			this.Controls.Add(this.simpleButtonExit);
			this.Controls.Add(this.simpleButtonSplit);
			this.Controls.Add(this.laDestination);
			this.Controls.Add(this.buttonEditDestination);
			this.Controls.Add(this.laSource);
			this.Controls.Add(this.buttonEditSource);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SD Cloner";
			this.Load += new System.EventHandler(this.FormMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.buttonEditSource.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditDestination.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.ButtonEdit buttonEditSource;
        private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Label laSource;
        private System.Windows.Forms.Label laDestination;
        private DevExpress.XtraEditors.ButtonEdit buttonEditDestination;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSplit;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExit;
    }
}

