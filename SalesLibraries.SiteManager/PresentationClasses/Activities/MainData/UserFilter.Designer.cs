﻿namespace SalesLibraries.SiteManager.PresentationClasses.Activities.MainData
{
	partial class UserFilter
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
			this.checkEditEnableFilter = new DevExpress.XtraEditors.CheckEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.checkedListBoxControlGroups = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.labelControlGroupsTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonXGroupsAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXGroupsNone = new DevComponents.DotNetBar.ButtonX();
			this.labelControlColumnsTitle = new DevExpress.XtraEditors.LabelControl();
			this.checkEditShowNumber = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditShowPercent = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditShowUsers = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditShowGroups = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableFilter.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowNumber.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowPercent.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowUsers.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowGroups.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// checkEditEnableFilter
			// 
			this.checkEditEnableFilter.Location = new System.Drawing.Point(8, 8);
			this.checkEditEnableFilter.Name = "checkEditEnableFilter";
			this.checkEditEnableFilter.Properties.Caption = "Enable Filter";
			this.checkEditEnableFilter.Size = new System.Drawing.Size(222, 20);
			this.checkEditEnableFilter.StyleController = this.styleController;
			this.checkEditEnableFilter.TabIndex = 0;
			this.checkEditEnableFilter.CheckedChanged += new System.EventHandler(this.checkEditFilterEnable_CheckedChanged);
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
			// checkedListBoxControlGroups
			// 
			this.checkedListBoxControlGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBoxControlGroups.CheckOnClick = true;
			this.checkedListBoxControlGroups.Enabled = false;
			this.checkedListBoxControlGroups.ItemHeight = 35;
			this.checkedListBoxControlGroups.Location = new System.Drawing.Point(8, 91);
			this.checkedListBoxControlGroups.Name = "checkedListBoxControlGroups";
			this.checkedListBoxControlGroups.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxControlGroups.Size = new System.Drawing.Size(222, 219);
			this.checkedListBoxControlGroups.StyleController = this.styleController;
			this.checkedListBoxControlGroups.TabIndex = 1;
			this.checkedListBoxControlGroups.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControlGroups_ItemCheck);
			// 
			// labelControlGroupsTitle
			// 
			this.labelControlGroupsTitle.Location = new System.Drawing.Point(10, 34);
			this.labelControlGroupsTitle.Name = "labelControlGroupsTitle";
			this.labelControlGroupsTitle.Size = new System.Drawing.Size(46, 16);
			this.labelControlGroupsTitle.StyleController = this.styleController;
			this.labelControlGroupsTitle.TabIndex = 2;
			this.labelControlGroupsTitle.Text = "Groups:";
			// 
			// buttonXGroupsAll
			// 
			this.buttonXGroupsAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGroupsAll.CausesValidation = false;
			this.buttonXGroupsAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGroupsAll.Enabled = false;
			this.buttonXGroupsAll.Location = new System.Drawing.Point(8, 59);
			this.buttonXGroupsAll.Name = "buttonXGroupsAll";
			this.buttonXGroupsAll.Size = new System.Drawing.Size(103, 23);
			this.buttonXGroupsAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGroupsAll.TabIndex = 17;
			this.buttonXGroupsAll.Text = "Select All";
			this.buttonXGroupsAll.TextColor = System.Drawing.Color.Black;
			this.buttonXGroupsAll.Click += new System.EventHandler(this.buttonXGroupsAll_Click);
			// 
			// buttonXGroupsNone
			// 
			this.buttonXGroupsNone.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGroupsNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXGroupsNone.CausesValidation = false;
			this.buttonXGroupsNone.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGroupsNone.Enabled = false;
			this.buttonXGroupsNone.Location = new System.Drawing.Point(127, 59);
			this.buttonXGroupsNone.Name = "buttonXGroupsNone";
			this.buttonXGroupsNone.Size = new System.Drawing.Size(103, 23);
			this.buttonXGroupsNone.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGroupsNone.TabIndex = 18;
			this.buttonXGroupsNone.Text = "Clear All";
			this.buttonXGroupsNone.TextColor = System.Drawing.Color.Black;
			this.buttonXGroupsNone.Click += new System.EventHandler(this.buttonXGroupsNone_Click);
			// 
			// labelControlColumnsTitle
			// 
			this.labelControlColumnsTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelControlColumnsTitle.Location = new System.Drawing.Point(10, 316);
			this.labelControlColumnsTitle.Name = "labelControlColumnsTitle";
			this.labelControlColumnsTitle.Size = new System.Drawing.Size(55, 16);
			this.labelControlColumnsTitle.StyleController = this.styleController;
			this.labelControlColumnsTitle.TabIndex = 19;
			this.labelControlColumnsTitle.Text = "Columns:";
			// 
			// checkEditShowNumber
			// 
			this.checkEditShowNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditShowNumber.EditValue = true;
			this.checkEditShowNumber.Location = new System.Drawing.Point(8, 336);
			this.checkEditShowNumber.Name = "checkEditShowNumber";
			this.checkEditShowNumber.Properties.AutoWidth = true;
			this.checkEditShowNumber.Properties.Caption = "Show #";
			this.checkEditShowNumber.Size = new System.Drawing.Size(65, 20);
			this.checkEditShowNumber.StyleController = this.styleController;
			this.checkEditShowNumber.TabIndex = 20;
			this.checkEditShowNumber.CheckedChanged += new System.EventHandler(this.checkEditShowColumns_CheckedChanged);
			// 
			// checkEditShowPercent
			// 
			this.checkEditShowPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditShowPercent.EditValue = true;
			this.checkEditShowPercent.Location = new System.Drawing.Point(103, 336);
			this.checkEditShowPercent.Name = "checkEditShowPercent";
			this.checkEditShowPercent.Properties.AutoWidth = true;
			this.checkEditShowPercent.Properties.Caption = "Show %";
			this.checkEditShowPercent.Size = new System.Drawing.Size(70, 20);
			this.checkEditShowPercent.StyleController = this.styleController;
			this.checkEditShowPercent.TabIndex = 21;
			this.checkEditShowPercent.CheckedChanged += new System.EventHandler(this.checkEditShowColumns_CheckedChanged);
			// 
			// checkEditShowUsers
			// 
			this.checkEditShowUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditShowUsers.EditValue = true;
			this.checkEditShowUsers.Location = new System.Drawing.Point(8, 368);
			this.checkEditShowUsers.Name = "checkEditShowUsers";
			this.checkEditShowUsers.Properties.AutoWidth = true;
			this.checkEditShowUsers.Properties.Caption = "Show Users Row";
			this.checkEditShowUsers.Size = new System.Drawing.Size(121, 20);
			this.checkEditShowUsers.StyleController = this.styleController;
			this.checkEditShowUsers.TabIndex = 22;
			this.checkEditShowUsers.CheckedChanged += new System.EventHandler(this.checkEditShowColumns_CheckedChanged);
			// 
			// checkEditShowGroups
			// 
			this.checkEditShowGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditShowGroups.EditValue = true;
			this.checkEditShowGroups.Location = new System.Drawing.Point(8, 400);
			this.checkEditShowGroups.Name = "checkEditShowGroups";
			this.checkEditShowGroups.Properties.AutoWidth = true;
			this.checkEditShowGroups.Properties.Caption = "Show Group Row";
			this.checkEditShowGroups.Size = new System.Drawing.Size(122, 20);
			this.checkEditShowGroups.StyleController = this.styleController;
			this.checkEditShowGroups.TabIndex = 23;
			this.checkEditShowGroups.CheckedChanged += new System.EventHandler(this.checkEditShowColumns_CheckedChanged);
			// 
			// UserFilter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.checkEditShowGroups);
			this.Controls.Add(this.checkEditShowUsers);
			this.Controls.Add(this.checkEditShowPercent);
			this.Controls.Add(this.checkEditShowNumber);
			this.Controls.Add(this.labelControlColumnsTitle);
			this.Controls.Add(this.buttonXGroupsNone);
			this.Controls.Add(this.buttonXGroupsAll);
			this.Controls.Add(this.labelControlGroupsTitle);
			this.Controls.Add(this.checkedListBoxControlGroups);
			this.Controls.Add(this.checkEditEnableFilter);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "UserFilter";
			this.Size = new System.Drawing.Size(238, 429);
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableFilter.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowNumber.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowPercent.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowUsers.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowGroups.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.CheckEdit checkEditEnableFilter;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlGroups;
		private DevExpress.XtraEditors.LabelControl labelControlGroupsTitle;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsAll;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsNone;
		private DevExpress.XtraEditors.LabelControl labelControlColumnsTitle;
		private DevExpress.XtraEditors.CheckEdit checkEditShowNumber;
		private DevExpress.XtraEditors.CheckEdit checkEditShowPercent;
		private DevExpress.XtraEditors.CheckEdit checkEditShowUsers;
		private DevExpress.XtraEditors.CheckEdit checkEditShowGroups;
	}
}
