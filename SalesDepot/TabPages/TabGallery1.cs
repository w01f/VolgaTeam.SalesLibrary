using System;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.PresentationClasses.Gallery;

namespace SalesDepot.TabPages
{
	public class TabGallery1 : GalleryControl, IController
	{
		#region IController
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.buttonItemGallery1Help.Click += ButtonItemGallery1HelpOnClick;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			AppManager.Instance.ActivityManager.AddUserActivity("Gallery selected");
		}
		#endregion

		#region GalleryControl
		private readonly GalleryManager _galleryManager = new GalleryManager(SettingsManager.Instance.Gallery1FilePath);
		public override GalleryManager Manager
		{
			get { return _galleryManager; }
		}

		public override RibbonPanel Panel
		{
			get { return FormMain.Instance.ribbonPanelGallery1; }
		}

		public override RibbonBar BrowseBar
		{
			get { return FormMain.Instance.ribbonBarGallery1Browse; }
		}

		public override RibbonBar ImageBar
		{
			get { return FormMain.Instance.ribbonBarGallery1Image; }
		}

		public override RibbonBar ZoomBar
		{
			get { return FormMain.Instance.ribbonBarGallery1Zoom; }
		}

		public override RibbonBar CopyBar
		{
			get { return FormMain.Instance.ribbonBarGallery1Copy; }
		}

		public override ItemContainer BrowseModeContainer
		{
			get { return FormMain.Instance.itemContainerGallery1BrowseContentType; }
		}

		public override ButtonItem ViewMode
		{
			get { return FormMain.Instance.buttonItemGallery1View; }
		}

		public override ButtonItem EditMode
		{
			get { return FormMain.Instance.buttonItemGallery1Edit; }
		}

		public override ButtonItem ImageSelect
		{
			get { return FormMain.Instance.buttonItemGallery1ImageSelect; }
		}

		public override ButtonItem ImageCrop
		{
			get { return FormMain.Instance.buttonItemGallery1ImageCrop; }
		}

		public override ButtonItem ZoomIn
		{
			get { return FormMain.Instance.buttonItemGallery1ZoomIn; }
		}

		public override ButtonItem ZoomOut
		{
			get { return FormMain.Instance.buttonItemGallery1ZoomOut; }
		}

		public override ButtonItem Copy
		{
			get { return FormMain.Instance.buttonItemGallery1Copy; }
		}

		public override ComboBoxEdit SectionsList
		{
			get { return FormMain.Instance.comboBoxEditGallery1Sections; }
		}

		public override ComboBoxEdit GroupsList
		{
			get { return FormMain.Instance.comboBoxEditGallery1Groups; }
		}
		#endregion

		private void ButtonItemGallery1HelpOnClick(object sender, EventArgs eventArgs)
		{
		}
	}
}
