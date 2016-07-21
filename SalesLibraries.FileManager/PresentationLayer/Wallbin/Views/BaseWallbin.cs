using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.DataSource;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Libraries;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public abstract partial class BaseWallbin : UserControl, IWallbinView
	{
		protected bool ReadyToUse { get; set; }
		public LibraryContext DataStorage { get; private set; }
		public IPageView ActivePage { get; private set; }
		public List<IPageView> Pages { get; private set; }
		public event EventHandler<EventArgs> PageChanging;
		public event EventHandler<EventArgs> PageChanged;
		public event EventHandler<EventArgs> DataChanged;
		public DataSourceTreeViewControl DataSourcesControl { get; private set; }
		public LibraryTagInfo TagInfoControl { get; private set; }

		private bool _isDataChanged;
		public bool IsDataChanged
		{
			get { return _isDataChanged; }
			set
			{
				_isDataChanged = value;
				if (!_isDataChanged) return;
				TagInfoControl.UpdateInfo();
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

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
			PageChanging += OnPageChanging;
			TagInfoControl = new LibraryTagInfo(DataStorage.Library);
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
			LoadPages(MainController.Instance.Settings.SelectedPage);
			InitControls();
			ReadyToUse = true;
		}

		public virtual void ShowView()
		{
			ActivePage.Suspend();
			ActivePage.ShowPage();
			ActivePage.Resume();

			pnContainer.BringToFront();

			TagInfoControl.BringToFront();
		}

		public virtual void DisposeView()
		{
			Pages.ForEach(p => p.DisposePage());
			Pages.Clear();
			ActivePage = null;
			if (DataSourcesControl != null)
			{
				DataSourcesControl.Parent = null;
				DataSourcesControl.Dispose();
				DataSourcesControl = null;
			}
			if (TagInfoControl != null)
				TagInfoControl.ReleaseControl();
		}

		public void SaveData()
		{
			MainController.Instance.WallbinViews.Selection.Reset();
			CorruptedLinksHelper.DeleteCorruptedLinks(DataStorage.Library);
			if (!IsDataChanged) return;
			MainController.Instance.ProcessManager.Run("Saving Changes...", cancelationToken =>
			{
				DataStorage.SaveChanges();
			});
			IsDataChanged = false;
		}

		protected virtual void InitControls()
		{
			LoadDataSource();
		}
		#endregion

		#region Pages Processing
		private void LoadPages(string activePageName)
		{
			var activePage = Pages.FirstOrDefault(v => v.Page.Name.Equals(activePageName)) ?? Pages.FirstOrDefault();
			SetActivePage(activePage);
		}

		protected void SetActivePage(IPageView pageView)
		{
			PageChanging?.Invoke(this, EventArgs.Empty);
			ActivePage = pageView;
			if (ActivePage == null) return;
			ActivePage.LoadPage();
			MainController.Instance.Settings.SelectedPage = ActivePage.Page.Name;
			MainController.Instance.Settings.Save();
			PageChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnPageChanging(object sender, EventArgs e)
		{
			SaveData();
		}

		public abstract void SelectPage(IPageView pageView);
		#endregion

		#region Data Sources
		public void LoadDataSource()
		{
			if (DataSourcesControl == null)
				DataSourcesControl = new DataSourceTreeViewControl();
			DataSourcesControl.LoadData(DataStorage.Library.GetDataSources());
		}
		#endregion
	}
}
