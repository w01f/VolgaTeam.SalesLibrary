namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.LinkBundles.SingleBundle
{
	partial class CoverEditControl
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pictureEditImage = new DevExpress.XtraEditors.PictureEdit();
			this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
			this.pictureEditLogo = new DevExpress.XtraEditors.PictureEdit();
			this.layoutControlGroupRoot = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItemLogo = new DevExpress.XtraLayout.LayoutControlItem();
			this.simpleLabelItemTitle = new DevExpress.XtraLayout.SimpleLabelItem();
			this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
			this.layoutControlItemImage = new DevExpress.XtraLayout.LayoutControlItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditImage.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
			this.layoutControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemImage)).BeginInit();
			this.SuspendLayout();
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.ForeColor = System.Drawing.Color.Black;
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.Appearance.Options.UseForeColor = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Options.UseForeColor = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// pictureEditImage
			// 
			this.pictureEditImage.AllowDrop = true;
			this.pictureEditImage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureEditImage.Location = new System.Drawing.Point(2, 94);
			this.pictureEditImage.Name = "pictureEditImage";
			this.pictureEditImage.Properties.AllowFocused = false;
			this.pictureEditImage.Properties.NullText = "Click to add Image...";
			this.pictureEditImage.Properties.ReadOnly = true;
			this.pictureEditImage.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditImage.Properties.ShowMenu = false;
			this.pictureEditImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			this.pictureEditImage.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditImage.Size = new System.Drawing.Size(628, 411);
			this.pictureEditImage.StyleController = this.layoutControl;
			this.pictureEditImage.TabIndex = 12;
			this.pictureEditImage.Click += new System.EventHandler(this.OnImageEditClick);
			this.pictureEditImage.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnImageDragDrop);
			this.pictureEditImage.DragOver += new System.Windows.Forms.DragEventHandler(this.OnImageDragOver);
			// 
			// layoutControl
			// 
			this.layoutControl.AllowCustomization = false;
			this.layoutControl.Appearance.Control.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.layoutControl.Appearance.Control.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDisabled.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDown.Options.UseFont = true;
			this.layoutControl.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlDropDownHeader.Options.UseFont = true;
			this.layoutControl.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlFocused.Options.UseFont = true;
			this.layoutControl.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControl.Appearance.ControlReadOnly.Options.UseFont = true;
			this.layoutControl.Controls.Add(this.pictureEditImage);
			this.layoutControl.Controls.Add(this.pictureEditLogo);
			this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl.ForeColor = System.Drawing.Color.Black;
			this.layoutControl.Location = new System.Drawing.Point(0, 0);
			this.layoutControl.Name = "layoutControl";
			this.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(802, 383, 250, 350);
			this.layoutControl.Root = this.layoutControlGroupRoot;
			this.layoutControl.Size = new System.Drawing.Size(632, 507);
			this.layoutControl.StyleController = this.styleController;
			this.layoutControl.TabIndex = 64;
			this.layoutControl.Text = "layoutControl1";
			// 
			// pictureEditLogo
			// 
			this.pictureEditLogo.EditValue = global::SalesLibraries.FileManager.Properties.Resources.BundlesEditorCoverLogo;
			this.pictureEditLogo.Location = new System.Drawing.Point(4, 4);
			this.pictureEditLogo.Name = "pictureEditLogo";
			this.pictureEditLogo.Properties.AllowFocused = false;
			this.pictureEditLogo.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.pictureEditLogo.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.pictureEditLogo.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEditLogo.Properties.Appearance.Options.UseForeColor = true;
			this.pictureEditLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEditLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEditLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			this.pictureEditLogo.Properties.ZoomAccelerationFactor = 1D;
			this.pictureEditLogo.Size = new System.Drawing.Size(64, 64);
			this.pictureEditLogo.StyleController = this.layoutControl;
			this.pictureEditLogo.TabIndex = 64;
			// 
			// layoutControlGroupRoot
			// 
			this.layoutControlGroupRoot.AllowHtmlStringInCaption = true;
			this.layoutControlGroupRoot.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.Header.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderActive.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.layoutControlGroupRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
			this.layoutControlGroupRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroupRoot.GroupBordersVisible = false;
			this.layoutControlGroupRoot.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemLogo,
            this.simpleLabelItemTitle,
            this.emptySpaceItem1,
            this.layoutControlItemImage});
			this.layoutControlGroupRoot.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroupRoot.Name = "layoutControlGroupRoot";
			this.layoutControlGroupRoot.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroupRoot.Size = new System.Drawing.Size(632, 507);
			this.layoutControlGroupRoot.TextVisible = false;
			// 
			// layoutControlItemLogo
			// 
			this.layoutControlItemLogo.Control = this.pictureEditLogo;
			this.layoutControlItemLogo.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemLogo.FillControlToClientArea = false;
			this.layoutControlItemLogo.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItemLogo.MaxSize = new System.Drawing.Size(72, 72);
			this.layoutControlItemLogo.MinSize = new System.Drawing.Size(72, 72);
			this.layoutControlItemLogo.Name = "layoutControlItemLogo";
			this.layoutControlItemLogo.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
			this.layoutControlItemLogo.Size = new System.Drawing.Size(72, 72);
			this.layoutControlItemLogo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItemLogo.Text = "Logo";
			this.layoutControlItemLogo.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemLogo.TextVisible = false;
			this.layoutControlItemLogo.TrimClientAreaToControl = false;
			// 
			// simpleLabelItemTitle
			// 
			this.simpleLabelItemTitle.AllowHotTrack = false;
			this.simpleLabelItemTitle.AllowHtmlStringInCaption = true;
			this.simpleLabelItemTitle.AppearanceItemCaption.Options.UseTextOptions = true;
			this.simpleLabelItemTitle.AppearanceItemCaption.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.None;
			this.simpleLabelItemTitle.Location = new System.Drawing.Point(72, 0);
			this.simpleLabelItemTitle.MaxSize = new System.Drawing.Size(0, 72);
			this.simpleLabelItemTitle.MinSize = new System.Drawing.Size(226, 72);
			this.simpleLabelItemTitle.Name = "simpleLabelItemTitle";
			this.simpleLabelItemTitle.Size = new System.Drawing.Size(560, 72);
			this.simpleLabelItemTitle.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.simpleLabelItemTitle.Text = "<size=+2>Add a Cover Image to this Link Bundle…</size>";
			this.simpleLabelItemTitle.TextSize = new System.Drawing.Size(281, 18);
			// 
			// emptySpaceItem1
			// 
			this.emptySpaceItem1.AllowHotTrack = false;
			this.emptySpaceItem1.Location = new System.Drawing.Point(0, 72);
			this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 20);
			this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 20);
			this.emptySpaceItem1.Name = "emptySpaceItem1";
			this.emptySpaceItem1.Size = new System.Drawing.Size(632, 20);
			this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
			// 
			// layoutControlItemImage
			// 
			this.layoutControlItemImage.Control = this.pictureEditImage;
			this.layoutControlItemImage.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.layoutControlItemImage.FillControlToClientArea = false;
			this.layoutControlItemImage.Location = new System.Drawing.Point(0, 92);
			this.layoutControlItemImage.Name = "layoutControlItemImage";
			this.layoutControlItemImage.Size = new System.Drawing.Size(632, 415);
			this.layoutControlItemImage.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItemImage.TextVisible = false;
			this.layoutControlItemImage.TrimClientAreaToControl = false;
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "png";
			this.openFileDialog.Filter = "Png Files|*.png|Bitmap|*.bmp|Jpeg Files|*.jpg, *.jpeg";
			// 
			// CoverEditControl
			// 
			this.Controls.Add(this.layoutControl);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Size = new System.Drawing.Size(632, 507);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEditImage.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
			this.layoutControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureEditLogo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRoot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLogo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.simpleLabelItemTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItemImage)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.PictureEdit pictureEditImage;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private DevExpress.XtraLayout.LayoutControl layoutControl;
		private DevExpress.XtraEditors.PictureEdit pictureEditLogo;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRoot;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLogo;
		private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemTitle;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItemImage;
	}
}
