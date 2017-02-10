using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class BaseWallbin : UserControl, IWallbinView
	{
		protected bool ReadyToUse { get; set; }
		public LibraryContext DataStorage { get; private set; }
		public IPageView ActivePage { get; private set; }
		public List<IPageView> Pages { get; private set; }
		public event EventHandler<EventArgs> PageChanged;

		public BaseWallbin()
		{
			InitializeComponent();
		}

		public BaseWallbin(LibraryContext dataStorage)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
			pnContainer.Dock = DockStyle.Fill;
			pnEmpty.BringToFront();
			DataStorage = dataStorage;
			Pages = new List<IPageView>();
		}

		public override string ToString()
		{
			return DataStorage != null ? DataStorage.LibraryName : String.Empty;
		}

		#region View Building
		public void LoadView(bool force = false)
		{
			if (ReadyToUse && !force) return;
			ReadyToUse = false;
			DisposeView();
			foreach (var libraryPage in DataStorage.Library.Pages.OrderBy(p => p.Order))
			{
				var pageView = PageViewFactory.Create(libraryPage);
				Pages.Add(pageView);
			}
			LoadPages(MainController.Instance.Settings.WallbinViewSettings.SelectedPage);
			InitControls();
			ReadyToUse = true;
		}

		public virtual void UnloadView() { }

		public virtual void ShowView()
		{
			ActivePage.Suspend();
			ActivePage.ShowPage();
			ActivePage.Resume();
			pnContainer.BringToFront();
		}

		public virtual void DisposeView()
		{
			UnloadView();
			Pages.ForEach(p => p.DisposePage());
			Pages.Clear();
			ActivePage = null;
		}

		protected virtual void InitControls() { }
		#endregion

		#region Pages Processing
		private void LoadPages(string activePageName)
		{
			var activePage = Pages.FirstOrDefault(v => v.Page.Name.Equals(activePageName)) ?? Pages.FirstOrDefault();
			SetActivePage(activePage);
		}

		protected void SetActivePage(IPageView pageView)
		{
			ActivePage = pageView;
			if (ActivePage == null) return;
			ActivePage.LoadPage();
			MainController.Instance.Settings.WallbinViewSettings.SelectedPage = ActivePage.Page.Name;
			MainController.Instance.Settings.SaveSettings();
			PageChanged?.Invoke(this, EventArgs.Empty);
		}
		#endregion
	}
}
