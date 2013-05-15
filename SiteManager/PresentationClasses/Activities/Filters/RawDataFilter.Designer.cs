namespace SalesDepot.SiteManager.PresentationClasses.Activities.Filters
{
	partial class RawDataFilter
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
			this.labelControlColumnsTitle = new DevExpress.XtraEditors.LabelControl();
			this.checkEditShowDetails = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditShowActionGroup = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditShowAction = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableFilter.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowDetails.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowActionGroup.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowAction.Properties)).BeginInit();
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
			this.checkedListBoxControlGroups.Size = new System.Drawing.Size(222, 232);
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
			this.labelControlColumnsTitle.Location = new System.Drawing.Point(10, 329);
			this.labelControlColumnsTitle.Name = "labelControlColumnsTitle";
			this.labelControlColumnsTitle.Size = new System.Drawing.Size(55, 16);
			this.labelControlColumnsTitle.StyleController = this.styleController;
			this.labelControlColumnsTitle.TabIndex = 19;
			this.labelControlColumnsTitle.Text = "Columns:";
			// 
			// checkEditShowDetails
			// 
			this.checkEditShowDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditShowDetails.EditValue = true;
			this.checkEditShowDetails.Location = new System.Drawing.Point(8, 351);
			this.checkEditShowDetails.Name = "checkEditShowDetails";
			this.checkEditShowDetails.Properties.AutoWidth = true;
			this.checkEditShowDetails.Properties.Caption = "User Detail";
			this.checkEditShowDetails.Size = new System.Drawing.Size(87, 21);
			this.checkEditShowDetails.StyleController = this.styleController;
			this.checkEditShowDetails.TabIndex = 22;
			this.checkEditShowDetails.CheckedChanged += new System.EventHandler(this.checkEditShowColumns_CheckedChanged);
			// 
			// checkEditShowActionGroup
			// 
			this.checkEditShowActionGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditShowActionGroup.EditValue = true;
			this.checkEditShowActionGroup.Location = new System.Drawing.Point(8, 378);
			this.checkEditShowActionGroup.Name = "checkEditShowActionGroup";
			this.checkEditShowActionGroup.Properties.AutoWidth = true;
			this.checkEditShowActionGroup.Properties.Caption = "Action Group";
			this.checkEditShowActionGroup.Size = new System.Drawing.Size(99, 21);
			this.checkEditShowActionGroup.StyleController = this.styleController;
			this.checkEditShowActionGroup.TabIndex = 23;
			this.checkEditShowActionGroup.CheckedChanged += new System.EventHandler(this.checkEditShowColumns_CheckedChanged);
			// 
			// checkEditShowAction
			// 
			this.checkEditShowAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditShowAction.EditValue = true;
			this.checkEditShowAction.Location = new System.Drawing.Point(8, 405);
			this.checkEditShowAction.Name = "checkEditShowAction";
			this.checkEditShowAction.Properties.AutoWidth = true;
			this.checkEditShowAction.Properties.Caption = "Action";
			this.checkEditShowAction.Size = new System.Drawing.Size(60, 21);
			this.checkEditShowAction.StyleController = this.styleController;
			this.checkEditShowAction.TabIndex = 24;
			this.checkEditShowAction.CheckedChanged += new System.EventHandler(this.checkEditShowColumns_CheckedChanged);
			// 
			// RawDataFilter
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
			this.Controls.Add(this.checkEditShowAction);
			this.Controls.Add(this.checkEditShowActionGroup);
			this.Controls.Add(this.checkEditShowDetails);
			this.Controls.Add(this.labelControlColumnsTitle);
			this.Controls.Add(this.buttonXGroupsNone);
			this.Controls.Add(this.buttonXGroupsAll);
			this.Controls.Add(this.labelControlGroupsTitle);
			this.Controls.Add(this.checkedListBoxControlGroups);
			this.Controls.Add(this.checkEditEnableFilter);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "RawDataFilter";
			this.Size = new System.Drawing.Size(238, 429);
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnableFilter.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControlGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowDetails.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowActionGroup.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowAction.Properties)).EndInit();
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
		private DevExpress.XtraEditors.LabelControl labelControlColumnsTitle;
		private DevExpress.XtraEditors.CheckEdit checkEditShowDetails;
		private DevExpress.XtraEditors.CheckEdit checkEditShowActionGroup;
		private DevExpress.XtraEditors.CheckEdit checkEditShowAction;
	}
}
