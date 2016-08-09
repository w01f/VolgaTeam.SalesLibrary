using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public sealed partial class TabPage : XtraTabPage, IPageView
	{
		private bool _readyToUse;
		public LibraryPage Page { get; private set; }
		public PageContent Content { get; private set; }
		public bool IsActive => MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == this;

		public TabPage(LibraryPage page)
		{
			InitializeComponent();
			Page = page;
			Text = Page.Name;
			Controls.Add(pnContainer);
			Controls.Add(pnEmpty);
			Content = new PageContent(this);
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
			_readyToUse = true;
			MainController.Instance.WallbinViews.Selection.Resume();
		}

		public void DisposePage()
		{
			pnContainer.Controls.Remove(Content);
			Content.DisposeContent();
		}

		public void ShowPage()
		{
			UpdateView();
			Content.Refresh();
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
