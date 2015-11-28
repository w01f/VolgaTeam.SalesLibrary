using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public sealed partial class TabPage : XtraTabPage, IPageView
	{
		private bool _readyToUse;
		public LibraryPage Page { get; private set; }
		public PageContent Content { get; private set; }
		public bool IsActive
		{
			get { return MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == this; }
		}

		public TabPage(LibraryPage page)
		{
			InitializeComponent();
			Page = page;
			Text = Page.Name.Replace("&", "&&");
			Controls.Add(pnContainer);
			Controls.Add(pnEmpty);
			Content = PageContent.Create(this);
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
			if (!pnContainer.Controls.Contains(Content))
				pnContainer.Controls.Add(Content);
			_readyToUse = true;
		}

		public void DisposePage()
		{
			pnContainer.Controls.Remove(Content);
			Content.DisposeContent();
		}

		public void ShowPage()
		{
			UpdateView();
			Content.UpdatePageLogo();
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
