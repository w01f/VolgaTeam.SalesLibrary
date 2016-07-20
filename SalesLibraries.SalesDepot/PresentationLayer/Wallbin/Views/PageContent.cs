using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Folders;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public abstract partial class PageContent : XtraScrollableControl
	{
		protected readonly List<BaseFolderBox> _folderBoxes = new List<BaseFolderBox>();
		public IPageView PageContainer { get; private set; }

		protected int InnerWidth => Width - SystemInformation.VerticalScrollBarWidth;

		protected PageContent(IPageView pageContainer)
		{
			InitializeComponent();
			PageContainer = pageContainer;
			Resize += OnResize;
		}

		public static PageContent Create(IPageView pageContainer)
		{
			if (MainController.Instance.WallbinViews.FormatState.ListView)
				return new ListPageContent(pageContainer);
			if (MainController.Instance.WallbinViews.FormatState.AccordionView)
				return new AccordionPageContent(pageContainer);
			return new ColumnsPageContent(pageContainer);
		}

		#region Public Methods
		public void LoadContent()
		{
			WinAPIHelper.SendMessage(Handle, 11, IntPtr.Zero, IntPtr.Zero);
			LoadContentParts();
			WinAPIHelper.SendMessage(Handle, 11, new IntPtr(1), IntPtr.Zero);
			Refresh();
		}

		public virtual void DisposeContent()
		{
			Resize -= OnResize;
			DisposeFolders();
			Resize += OnResize;
		}

		public void UpdateContent()
		{
			WinAPIHelper.SendMessage(Handle, 11, IntPtr.Zero, IntPtr.Zero);
			UpdateContentParts();
			WinAPIHelper.SendMessage(Handle, 11, new IntPtr(1), IntPtr.Zero);
			Refresh();
			UpdatePageLogo();
		}

		public void UpdatePageLogo()
		{
			MainController.Instance.MainForm.ribbonBarHomeWallbin.SuspendLayout();

			var pageLogoFolder = new StorageDirectory(RemoteResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge(new[]
			{
				Constants.LibraryLogoFolder,
				"libraries",
				PageContainer.Page.Library.Name,
			}));

			var libraryContainerVisibility = MainController.Instance.MainForm.itemContainerHomeWallbinLibrary.Visible;
			MainController.Instance.MainForm.itemContainerHomeWallbinLibrary.Visible = false;
			var pageLogoPath = Path.Combine(pageLogoFolder.LocalPath, String.Format("page{0}.png", PageContainer.Page.Order + 1));
			if (!File.Exists(pageLogoPath))
				pageLogoPath = Path.Combine(pageLogoFolder.LocalPath, "no_logo.png");
			if (File.Exists(pageLogoPath))
				MainController.Instance.MainForm.labelItemHomeWallbinLogo.Image = Image.FromFile(pageLogoPath);
			else
				MainController.Instance.MainForm.labelItemHomeWallbinLogo.Image = Properties.Resources.SettingsLogo;
			MainController.Instance.MainForm.itemContainerHomeWallbinLibrary.Visible = libraryContainerVisibility;
			MainController.Instance.MainForm.ribbonBarHomeWallbin.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelHome.PerformLayout();

			MainController.Instance.MainForm.ribbonBarHomeWallbin.ResumeLayout();
		}
		#endregion

		#region Protected Methods
		protected abstract void LoadContentParts();
		protected abstract void UpdateContentParts();
		protected abstract void UpdateContentPartsSizes();

		private void OnResize(object sender, EventArgs e)
		{
			if (IsDisposed) return;
			if (!PageContainer.IsActive) return;
			PageContainer.Suspend();
			UpdateContent();
			PageContainer.Resume();
		}

		private void OnLayout(object sender, LayoutEventArgs e)
		{
			HorizontalScroll.Visible = false;
			VerticalScroll.Visible = true;
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			Focus();
		}
		#endregion

		#region Folders Processing
		protected abstract void OnFolderSizeChanged(object sender, EventArgs e);
		protected abstract void UpdateFoldersSize();
		protected virtual void LoadFolders()
		{
			_folderBoxes.AddRange(
				PageContainer.Page.Folders
					.Select(libraryFolder =>
					{
						var folderBox = FolderBoxFactory.Create(libraryFolder);
						folderBox.BoxSizeChanged += OnFolderSizeChanged;
						Application.DoEvents();
						return folderBox;
					})
					.ToArray()
				);
			Controls.AddRange(_folderBoxes.Select(fb => (Control)fb).ToArray());
		}

		public void DisposeFolders()
		{
			_folderBoxes.ForEach(fb =>
			{
				Controls.Remove(fb);
				fb.ReleaseControl();
			});
			_folderBoxes.Clear();
		}

		public void UpdateFolders()
		{
			_folderBoxes.ForEach(fb => fb.UpdateContent(false));
			UpdateFoldersSize();
		}
		#endregion
	}
}
