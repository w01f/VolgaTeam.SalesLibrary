using System;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.PresentationClasses.Gallery;

namespace SalesDepot.TabPages
{
	public class TabGallery2 : GalleryControl, IController
	{
		#region IController
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.buttonItemGallery2Help.Click += ButtonItemGallery2HelpOnClick;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			AppManager.Instance.ActivityManager.AddUserActivity("Gallery selected");
		}
		#endregion

		#region GalleryControl
		private readonly GalleryManager _galleryManager = new GalleryManager(SettingsManager.Instance.Gallery2FilePath);
		public override GalleryManager Manager
		{
			get { return _galleryManager; }
		}

		public override RibbonPanel Panel
		{
			get { return FormMain.Instance.ribbonPanelGallery2; }
		}

		public override RibbonBar BrowseBar
		{
			get { return FormMain.Instance.ribbonBarGallery2Browse; }
		}

		public override RibbonBar ImageBar
		{
			get { return FormMain.Instance.ribbonBarGallery2Image; }
		}

		public override RibbonBar ZoomBar
		{
			get { return FormMain.Instance.ribbonBarGallery2Zoom; }
		}

		public override RibbonBar CopyBar
		{
			get { return FormMain.Instance.ribbonBarGallery2Copy; }
		}

		public override ItemContainer BrowseModeContainer
		{
			get { return FormMain.Instance.itemContainerGallery2BrowseContentType; }
		}

		public override ButtonItem ViewMode
		{
			get { return FormMain.Instance.buttonItemGallery2View; }
		}

		public override ButtonItem EditMode
		{
			get { return FormMain.Instance.buttonItemGallery2Edit; }
		}

		public override ButtonItem ImageSelect
		{
			get { return FormMain.Instance.buttonItemGallery2ImageSelect; }
		}

		public override ButtonItem ImageCrop
		{
			get { return FormMain.Instance.buttonItemGallery2ImageCrop; }
		}

		public override ButtonItem ZoomIn
		{
			get { return FormMain.Instance.buttonItemGallery2ZoomIn; }
		}

		public override ButtonItem ZoomOut
		{
			get { return FormMain.Instance.buttonItemGallery2ZoomOut; }
		}

		public override ButtonItem Copy
		{
			get { return FormMain.Instance.buttonItemGallery2Copy; }
		}

		public override ComboBoxEdit SectionsList
		{
			get { return FormMain.Instance.comboBoxEditGallery2Sections; }
		}

		public override ComboBoxEdit GroupsList
		{
			get { return FormMain.Instance.comboBoxEditGallery2Groups; }
		}
		#endregion

		private void ButtonItemGallery2HelpOnClick(object sender, EventArgs eventArgs)
		{
		}
	}
}
