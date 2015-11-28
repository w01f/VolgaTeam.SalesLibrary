using System;
using System.ComponentModel;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class SimplePage : UserControl, IPageView
	{
		private bool _readyToUse;
		public LibraryPage Page { get; private set; }
		public PageContent Content { get; private set; }
		public bool IsActive
		{
			get { return MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == this; }
		}

		public SimplePage(LibraryPage page)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Page = page;
			Content = new PageContent(this);
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
			_readyToUse = true;
		}

		public void DisposePage()
		{
			Content.DisposeContent();
		}

		public void ShowPage()
		{
			UpdateView();
			BringToFront();
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
