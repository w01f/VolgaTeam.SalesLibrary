using System;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using SalesLibraries.SalesDepot.Business.Services;
using SalesLibraries.SalesDepot.PresentationLayer.Gallery;

namespace SalesLibraries.SalesDepot.Controllers
{
	class Gallery1Page : GalleryControl, IPageController
	{
		private GalleryManager _galleryManager;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		#region GalleryControl
		public override GalleryManager Manager => _galleryManager;

		public override RibbonPanel Panel => MainController.Instance.MainForm.ribbonPanelGallery1;

		public override RibbonBar BrowseBar => MainController.Instance.MainForm.ribbonBarGallery1Browse;

		public override RibbonBar ImageBar => MainController.Instance.MainForm.ribbonBarGallery1Image;

		public override RibbonBar ZoomBar => MainController.Instance.MainForm.ribbonBarGallery1Zoom;

		public override RibbonBar CopyBar => MainController.Instance.MainForm.ribbonBarGallery1Copy;

		public override ItemContainer BrowseModeContainer => MainController.Instance.MainForm.itemContainerGallery1BrowseContentType;

		public override ButtonItem ViewMode => MainController.Instance.MainForm.buttonItemGallery1View;

		public override ButtonItem EditMode => MainController.Instance.MainForm.buttonItemGallery1Edit;

		public override ButtonItem ImageSelect => MainController.Instance.MainForm.buttonItemGallery1ImageSelect;

		public override ButtonItem ImageCrop => MainController.Instance.MainForm.buttonItemGallery1ImageCrop;

		public override ButtonItem ZoomIn => MainController.Instance.MainForm.buttonItemGallery1ZoomIn;

		public override ButtonItem ZoomOut => MainController.Instance.MainForm.buttonItemGallery1ZoomOut;

		public override ButtonItem Copy => MainController.Instance.MainForm.buttonItemGallery1Copy;

		public override ComboBoxEdit SectionsList => MainController.Instance.MainForm.comboBoxEditGallery1Sections;

		public override ComboBoxEdit GroupsList => MainController.Instance.MainForm.comboBoxEditGallery1Groups;

		#endregion

		#region IPageController
		public void InitController()
		{
			_galleryManager = new GalleryManager(Configuration.RemoteResourceManager.Instance.Gallery1ConfigFile.LocalPath);
			MainController.Instance.MainForm.buttonItemGallery1Help.Click += OnHelpClick;
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;
			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(this))
			{
				MainController.Instance.MainForm.pnContainer.Controls.Add(this);
				InitControl();
			}
			BringToFront();
		}

		public void OnLibraryChanged(object sender, EventArgs e) { }
		#endregion

		private void OnHelpClick(object sender, EventArgs eventArgs)
		{
			MainController.Instance.HelpManager.OpenHelpLink("gallery1");
		}
	}
}
