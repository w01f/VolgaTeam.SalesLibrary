using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public sealed partial class TabPage : XtraTabPage, IPageView
	{
		private bool _readyToUse;
		public LibraryPage Page { get; }
		public PageContent Content { get; }
		public LibraryPageTagInfo TagInfoControl { get; }
		public bool IsActive => MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == this;

		public TabPage(LibraryPage page)
		{
			InitializeComponent();
			Page = page;
			Text = Page.Name;
			Controls.Add(pnContainer);
			Controls.Add(pnEmpty);
			Content = new PageContent(this);
			TagInfoControl = new LibraryPageTagInfo(Page);
			Suspend();
		}

		public override string ToString()
		{
			return Page != null ? Page.Name : String.Empty;
		}

		public void LoadPage(bool force = false)
		{
			if (_readyToUse && !force) return;
			MainController.Instance.WallbinViews.Selection.Suspend();
			if (force)
				DisposePage();
			Content.LoadContent();
			if (!pnContainer.Controls.Contains(Content))
				pnContainer.Controls.Add(Content);
			if (!MainController.Instance.TabWallbin.pnTagInfoContainer.Controls.Contains(TagInfoControl))
				MainController.Instance.TabWallbin.pnTagInfoContainer.Controls.Add(TagInfoControl);
			_readyToUse = true;
			MainController.Instance.WallbinViews.Selection.Resume();
		}

		public void DisposePage()
		{
			pnContainer.Controls.Remove(Content);
			Content.DisposeContent();
			TagInfoControl?.ReleaseControl();
		}

		public void ShowPage()
		{
			UpdateView();
			Content.Refresh();
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
