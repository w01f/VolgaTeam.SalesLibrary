namespace FileManager.ToolForms.Settings
{
    partial class FormAutoSyncEdit
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.laTime = new System.Windows.Forms.Label();
			this.timeEditTime = new DevExpress.XtraEditors.TimeEdit();
			this.checkBoxMonday = new System.Windows.Forms.CheckBox();
			this.checkBoxTuesday = new System.Windows.Forms.CheckBox();
			this.checkBoxWednesday = new System.Windows.Forms.CheckBox();
			this.checkBoxThursday = new System.Windows.Forms.CheckBox();
			this.checkBoxSunday = new System.Windows.Forms.CheckBox();
			this.checkBoxSaturday = new System.Windows.Forms.CheckBox();
			this.checkBoxFriday = new System.Windows.Forms.CheckBox();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.groupControlWeekDays = new DevExpress.XtraEditors.GroupControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.timeEditTime.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlWeekDays)).BeginInit();
			this.groupControlWeekDays.SuspendLayout();
			this.SuspendLayout();
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
			// laTime
			// 
			this.laTime.AutoSize = true;
			this.laTime.Location = new System.Drawing.Point(19, 15);
			this.laTime.Name = "laTime";
			this.laTime.Size = new System.Drawing.Size(74, 16);
			this.laTime.TabIndex = 0;
			this.laTime.Text = "Sync Time:";
			// 
			// timeEditTime
			// 
			this.timeEditTime.EditValue = null;
			this.timeEditTime.Location = new System.Drawing.Point(122, 12);
			this.timeEditTime.Name = "timeEditTime";
			this.timeEditTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.timeEditTime.Properties.DisplayFormat.FormatString = "hh:mm tt";
			this.timeEditTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.timeEditTime.Properties.EditFormat.FormatString = "hh:mm tt";
			this.timeEditTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.timeEditTime.Properties.EditValueChangedDelay = 2000;
			this.timeEditTime.Properties.Mask.EditMask = "HH:mm tt";
			this.timeEditTime.Size = new System.Drawing.Size(104, 22);
			this.timeEditTime.StyleController = this.styleController;
			this.timeEditTime.TabIndex = 1;
			// 
			// checkBoxMonday
			// 
			this.checkBoxMonday.AutoSize = true;
			this.checkBoxMonday.Location = new System.Drawing.Point(10, 29);
			this.checkBoxMonday.Name = "checkBoxMonday";
			this.checkBoxMonday.Size = new System.Drawing.Size(73, 20);
			this.checkBoxMonday.TabIndex = 3;
			this.checkBoxMonday.Text = "Monday";
			this.checkBoxMonday.UseVisualStyleBackColor = true;
			// 
			// checkBoxTuesday
			// 
			this.checkBoxTuesday.AutoSize = true;
			this.checkBoxTuesday.Location = new System.Drawing.Point(110, 29);
			this.checkBoxTuesday.Name = "checkBoxTuesday";
			this.checkBoxTuesday.Size = new System.Drawing.Size(76, 20);
			this.checkBoxTuesday.TabIndex = 4;
			this.checkBoxTuesday.Text = "Tuesday";
			this.checkBoxTuesday.UseVisualStyleBackColor = true;
			// 
			// checkBoxWednesday
			// 
			this.checkBoxWednesday.AutoSize = true;
			this.checkBoxWednesday.Location = new System.Drawing.Point(214, 29);
			this.checkBoxWednesday.Name = "checkBoxWednesday";
			this.checkBoxWednesday.Size = new System.Drawing.Size(96, 20);
			this.checkBoxWednesday.TabIndex = 5;
			this.checkBoxWednesday.Text = "Wednesday";
			this.checkBoxWednesday.UseVisualStyleBackColor = true;
			// 
			// checkBoxThursday
			// 
			this.checkBoxThursday.AutoSize = true;
			this.checkBoxThursday.Location = new System.Drawing.Point(315, 29);
			this.checkBoxThursday.Name = "checkBoxThursday";
			this.checkBoxThursday.Size = new System.Drawing.Size(80, 20);
			this.checkBoxThursday.TabIndex = 6;
			this.checkBoxThursday.Text = "Thursday";
			this.checkBoxThursday.UseVisualStyleBackColor = true;
			// 
			// checkBoxSunday
			// 
			this.checkBoxSunday.AutoSize = true;
			this.checkBoxSunday.Location = new System.Drawing.Point(214, 67);
			this.checkBoxSunday.Name = "checkBoxSunday";
			this.checkBoxSunday.Size = new System.Drawing.Size(71, 20);
			this.checkBoxSunday.TabIndex = 9;
			this.checkBoxSunday.Text = "Sunday";
			this.checkBoxSunday.UseVisualStyleBackColor = true;
			// 
			// checkBoxSaturday
			// 
			this.checkBoxSaturday.AutoSize = true;
			this.checkBoxSaturday.Location = new System.Drawing.Point(110, 67);
			this.checkBoxSaturday.Name = "checkBoxSaturday";
			this.checkBoxSaturday.Size = new System.Drawing.Size(79, 20);
			this.checkBoxSaturday.TabIndex = 8;
			this.checkBoxSaturday.Text = "Saturday";
			this.checkBoxSaturday.UseVisualStyleBackColor = true;
			// 
			// checkBoxFriday
			// 
			this.checkBoxFriday.AutoSize = true;
			this.checkBoxFriday.Location = new System.Drawing.Point(10, 67);
			this.checkBoxFriday.Name = "checkBoxFriday";
			this.checkBoxFriday.Size = new System.Drawing.Size(63, 20);
			this.checkBoxFriday.TabIndex = 7;
			this.checkBoxFriday.Text = "Friday";
			this.checkBoxFriday.UseVisualStyleBackColor = true;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(235, 152);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(93, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 12;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(98, 152);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(93, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 11;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// groupControlWeekDays
			// 
			this.groupControlWeekDays.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupControlWeekDays.Appearance.Options.UseFont = true;
			this.groupControlWeekDays.AppearanceCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.groupControlWeekDays.AppearanceCaption.Options.UseFont = true;
			this.groupControlWeekDays.Controls.Add(this.checkBoxMonday);
			this.groupControlWeekDays.Controls.Add(this.checkBoxSunday);
			this.groupControlWeekDays.Controls.Add(this.checkBoxThursday);
			this.groupControlWeekDays.Controls.Add(this.checkBoxTuesday);
			this.groupControlWeekDays.Controls.Add(this.checkBoxFriday);
			this.groupControlWeekDays.Controls.Add(this.checkBoxSaturday);
			this.groupControlWeekDays.Controls.Add(this.checkBoxWednesday);
			this.groupControlWeekDays.Location = new System.Drawing.Point(12, 47);
			this.groupControlWeekDays.Name = "groupControlWeekDays";
			this.groupControlWeekDays.Size = new System.Drawing.Size(406, 93);
			this.groupControlWeekDays.TabIndex = 13;
			this.groupControlWeekDays.Text = "Week Days";
			// 
			// FormAutoSyncEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(427, 196);
			this.Controls.Add(this.groupControlWeekDays);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.timeEditTime);
			this.Controls.Add(this.laTime);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAutoSyncEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Sync Schedule Record";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAutoSyncEdit_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.timeEditTime.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControlWeekDays)).EndInit();
			this.groupControlWeekDays.ResumeLayout(false);
			this.groupControlWeekDays.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Label laTime;
        private DevExpress.XtraEditors.TimeEdit timeEditTime;
        private System.Windows.Forms.CheckBox checkBoxMonday;
        private System.Windows.Forms.CheckBox checkBoxTuesday;
        private System.Windows.Forms.CheckBox checkBoxWednesday;
        private System.Windows.Forms.CheckBox checkBoxThursday;
        private System.Windows.Forms.CheckBox checkBoxSunday;
        private System.Windows.Forms.CheckBox checkBoxSaturday;
		private System.Windows.Forms.CheckBox checkBoxFriday;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevExpress.XtraEditors.GroupControl groupControlWeekDays;
    }
}