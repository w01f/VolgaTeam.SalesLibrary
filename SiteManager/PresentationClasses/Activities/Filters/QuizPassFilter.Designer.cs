﻿namespace SalesDepot.SiteManager.PresentationClasses.Activities.Filters
{
	partial class QuizPassFilter
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
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.checkedListBoxControlGroups = new DevExpress.XtraEditors.CheckedListBoxControl();
			this.labelControlGroupsTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonXGroupsAll = new DevComponents.DotNetBar.ButtonX();
			this.buttonXGroupsNone = new DevComponents.DotNetBar.ButtonX();
			this.comboBoxTopLevel = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableFilter.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxTopLevel.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// checkEditEnableFilter
			// 
			this.checkEditEnableFilter.Location = new System.Drawing.Point(8, 8);
			this.checkEditEnableFilter.Name = "checkEditEnableFilter";
			this.checkEditEnableFilter.Properties.Caption = "Enable Filter";
			this.checkEditEnableFilter.Size = new System.Drawing.Size(222, 21);
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
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
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
			this.checkedListBoxControlGroups.Size = new System.Drawing.Size(222, 299);
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
			// comboBoxTopLevel
			// 
			this.comboBoxTopLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxTopLevel.Enabled = false;
			this.comboBoxTopLevel.Location = new System.Drawing.Point(8, 399);
			this.comboBoxTopLevel.Name = "comboBoxTopLevel";
			this.comboBoxTopLevel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxTopLevel.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxTopLevel.Size = new System.Drawing.Size(222, 22);
			this.comboBoxTopLevel.StyleController = this.styleController;
			this.comboBoxTopLevel.TabIndex = 22;
			this.comboBoxTopLevel.EditValueChanged += new System.EventHandler(this.comboBoxTopLevel_EditValueChanged);
			// 
			// QuizPassFilter
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.comboBoxTopLevel);
			this.Controls.Add(this.buttonXGroupsNone);
			this.Controls.Add(this.buttonXGroupsAll);
			this.Controls.Add(this.labelControlGroupsTitle);
			this.Controls.Add(this.checkedListBoxControlGroups);
			this.Controls.Add(this.checkEditEnableFilter);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "QuizPassFilter";
			this.Size = new System.Drawing.Size(238, 429);
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableFilter.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxTopLevel.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.CheckEdit checkEditEnableFilter;
		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControlGroups;
		private DevExpress.XtraEditors.LabelControl labelControlGroupsTitle;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsAll;
		private DevComponents.DotNetBar.ButtonX buttonXGroupsNone;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxTopLevel;
	}
}
