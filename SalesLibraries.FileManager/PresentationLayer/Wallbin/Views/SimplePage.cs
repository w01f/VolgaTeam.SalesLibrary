using System;
using System.ComponentModel;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class SimplePage : UserControl, IPageView
	{
		private bool _readyToUse;
		public LibraryPage Page { get; }
		public PageContent Content { get; }
		public LibraryPageTagInfo TagInfoControl { get; }
		public bool IsActive => MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == this;

		public SimplePage(LibraryPage page)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Page = page;
			Content = new PageContent(this);
			TagInfoControl = new LibraryPageTagInfo(Page);
			pnContainer.Controls.Add(Content);
			Suspend();
		}

		public override string ToString()
		{
			return Page != null ? Page.Name : String.Empty;
		}

		public void LoadPage(bool force = false)
		{
			if (_readyToUse && !force) return;
			if (force)
				DisposePage();
			Content.LoadContent();
			if (!MainController.Instance.TabWallbin.pnTagInfoContainer.Controls.Contains(TagInfoControl))
				MainController.Instance.TabWallbin.pnTagInfoContainer.Controls.Add(TagInfoControl);
			_readyToUse = true;
		}

		public void DisposePage()
		{
			Content.DisposeContent();
			TagInfoControl?.ReleaseControl();
		}

		public void ShowPage()
		{
			UpdateView();
			BringToFront();
			TagInfoControl.BringToFront();
		}

		public void UpdateView()
		{
			Content.UpdateContent();
		}

		public void Suspend()
		{
			pnEmpty.BringToFront();
			Application.DoEvents();
		}

		public void Resume()
		{
			pnContainer.BringToFront();
			Application.DoEvents();
		}
	}
}
