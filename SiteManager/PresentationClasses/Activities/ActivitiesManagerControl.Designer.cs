namespace SalesDepot.SiteManager.PresentationClasses.Activities
{
	sealed partial class ActivitiesManagerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.buttonXApplyFilter = new DevComponents.DotNetBar.ButtonX();
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.gbDate = new System.Windows.Forms.GroupBox();
			this.labelControlDateEnd = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.dateEditEnd = new DevExpress.XtraEditors.DateEdit();
			this.labelControlDateStart = new DevExpress.XtraEditors.LabelControl();
			this.dateEditStart = new DevExpress.XtraEditors.DateEdit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			this.gbDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Lilian";
			// 
			// buttonXApplyFilter
			// 
			this.buttonXApplyFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXApplyFilter.CausesValidation = false;
			this.buttonXApplyFilter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXApplyFilter.Location = new System.Drawing.Point(14, 438);
			this.buttonXApplyFilter.Name = "buttonXApplyFilter";
			this.buttonXApplyFilter.Size = new System.Drawing.Size(222, 33);
			this.buttonXApplyFilter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXApplyFilter.TabIndex = 16;
			this.buttonXApplyFilter.Text = "Apply Filter";
			this.buttonXApplyFilter.TextColor = System.Drawing.Color.Black;
			this.buttonXApplyFilter.Click += new System.EventHandler(this.buttonXApplyFilter_Click);
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.gbDate);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXApplyFilter);
			this.splitContainerControl.Panel1.MinSize = 250;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(898, 483);
			this.splitContainerControl.SplitterPosition = 250;
			this.splitContainerControl.TabIndex = 17;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// gbDate
			// 
			this.gbDate.Controls.Add(this.labelControlDateEnd);
			this.gbDate.Controls.Add(this.dateEditEnd);
			this.gbDate.Controls.Add(this.labelControlDateStart);
			this.gbDate.Controls.Add(this.dateEditStart);
			this.gbDate.Location = new System.Drawing.Point(4, 8);
			this.gbDate.Name = "gbDate";
			this.gbDate.Size = new System.Drawing.Size(242, 98);
			this.gbDate.TabIndex = 17;
			this.gbDate.TabStop = false;
			this.gbDate.Text = "Date range";
			// 
			// labelControlDateEnd
			// 
			this.labelControlDateEnd.Location = new System.Drawing.Point(10, 63);
			this.labelControlDateEnd.Name = "labelControlDateEnd";
			this.labelControlDateEnd.Size = new System.Drawing.Size(58, 16);
			this.labelControlDateEnd.StyleController = this.styleController;
			this.labelControlDateEnd.TabIndex = 3;
			this.labelControlDateEnd.Text = "End Date:";
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
			// dateEditEnd
			// 
			this.dateEditEnd.EditValue = null;
			this.dateEditEnd.Location = new System.Drawing.Point(100, 60);
			this.dateEditEnd.Name = "dateEditEnd";
			this.dateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditEnd.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEnd.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditEnd.Size = new System.Drawing.Size(132, 22);
			this.dateEditEnd.StyleController = this.styleController;
			this.dateEditEnd.TabIndex = 2;
			// 
			// labelControlDateStart
			// 
			this.labelControlDateStart.Location = new System.Drawing.Point(10, 24);
			this.labelControlDateStart.Name = "labelControlDateStart";
			this.labelControlDateStart.Size = new System.Drawing.Size(63, 16);
			this.labelControlDateStart.StyleController = this.styleController;
			this.labelControlDateStart.TabIndex = 1;
			this.labelControlDateStart.Text = "Start Date:";
			// 
			// dateEditStart
			// 
			this.dateEditStart.EditValue = null;
			this.dateEditStart.Location = new System.Drawing.Point(100, 21);
			this.dateEditStart.Name = "dateEditStart";
			this.dateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.dateEditStart.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStart.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditStart.Size = new System.Drawing.Size(132, 22);
			this.dateEditStart.StyleController = this.styleController;
			this.dateEditStart.TabIndex = 0;
			// 
			// ActivitiesManagerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ActivitiesManagerControl";
			this.Size = new System.Drawing.Size(898, 483);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			this.gbDate.ResumeLayout(false);
			this.gbDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditEnd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditStart.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevComponents.DotNetBar.ButtonX buttonXApplyFilter;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private System.Windows.Forms.GroupBox gbDate;
		private DevExpress.XtraEditors.DateEdit dateEditStart;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlDateEnd;
		private DevExpress.XtraEditors.DateEdit dateEditEnd;
		private DevExpress.XtraEditors.LabelControl labelControlDateStart;
    }
}
