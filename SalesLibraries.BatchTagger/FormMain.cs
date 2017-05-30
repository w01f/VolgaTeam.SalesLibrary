using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraTab;
using SalesLibraries.BatchTagger.PresentationLayer;
using SalesLibraries.Common.Configuration;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.ServiceConnector.StatisticService;

namespace SalesLibraries.BatchTagger
{
	public partial class FormMain : MetroForm
	{
		public FormMain()
		{
			InitializeComponent();

			_filterControlLibrary = new LibraryFilter();
			pnCustomFilter.Controls.Add(_filterControlLibrary);
			_filterControlLibrary.FilterChanged += (o, e) =>
			{
				_filterControlTotal.EnableFilter = _filterControlLibrary.EnableFilter;
				_filterControlTotal.SelectedGroups.Clear();
				_filterControlTotal.SelectedGroups.AddRange(_filterControlLibrary.SelectedGroups);
				_filterControlTotal.UpdateDataSource(_filterControlLibrary.AllGroups.ToArray(), false);

				ApplyData();
			};

			_filterControlTotal = new TotalFilter();
			pnCustomFilter.Controls.Add(_filterControlTotal);
			_filterControlTotal.FilterChanged += (o, e) =>
			{
				_filterControlLibrary.EnableFilter = _filterControlTotal.EnableFilter;
				_filterControlLibrary.SelectedGroups.Clear();
				_filterControlLibrary.SelectedGroups.AddRange(_filterControlTotal.SelectedGroups);
				_filterControlLibrary.UpdateDataSource(_filterControlTotal.AllGroups.ToArray(), false);

				ApplyData();
			};

			FormStateHelper.Init(this, GlobalSettings.ApplicationRootPath, "Batch Tagger", false, true);
		}

		private readonly List<LibraryFilesModel> _records = new List<LibraryFilesModel>();

		private readonly TotalFilter _filterControlTotal;
		private readonly LibraryFilter _filterControlLibrary;

		private void OnFormShown(object sender, EventArgs e)
		{
			AppManager.Instance.ProcessManager.Run("Loading data...", (cancelationToken, formProgess) =>
			{
				AppManager.Instance.LoadData();
			});

			Text = String.Format("{0} ({1})", Text, AppManager.Instance.Connections.SoapConnection.Website);

			RefreshData();

			AppManager.Instance.ActivateForm(Handle, WindowState == FormWindowState.Maximized, false);
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			var totalControl = xtraTabControlLibraries.TabPages.OfType<TotalControl>().FirstOrDefault();
			totalControl?.SaveLayout();

			var libraryControl = xtraTabControlLibraries.SelectedTabPage as LibraryControl;
			libraryControl?.SaveLayout();
		}

		public void RefreshData()
		{
			ClearData();
			var message = string.Empty;
			AppManager.Instance.ProcessManager.Run("Loading data...", (cancelationToken, formProgess) =>
			{
				_records.AddRange(AppManager.Instance.Connections.SoapConnection.GetLibraryFiles(out message));
			});
			if (!string.IsNullOrEmpty(message))
				AppManager.Instance.ShowWarning(message);
			var filterDataSource =
				_records.OrderBy(g => g.library).Select(x => x.library).Where(x => !String.IsNullOrEmpty(x)).Distinct().ToArray();
			_filterControlTotal.UpdateDataSource(filterDataSource);
			_filterControlLibrary.UpdateDataSource(filterDataSource);
			ApplyData();
			OnSelectedPageChanged(xtraTabControlLibraries, new TabPageChangedEventArgs(null, xtraTabControlLibraries.SelectedTabPage));
		}

		public void ClearData()
		{
			xtraTabControlLibraries.TabPages.Clear();
			_records.Clear();
		}

		private void ApplyData()
		{
			var filteredRecords = new List<LibraryFilesModel>();
			filteredRecords.AddRange(_filterControlTotal.EnableFilter ?
				_records.Where(g => _filterControlTotal.SelectedGroups.Contains(g.library)) :
				_records);

			var totalRecords = filteredRecords
				.GroupBy(g => g.library)
				.Select(g => new LibraryFilesTotalModel()
				{
					Name = g.Key,
					FilesTotalCount = g.Count(),
					FilesTaggedCount = g.Count(r => r.HasCategories || r.HasKeywords),
					VideoTotalCount = g.Count(r => new[] { "video", "mp4", "wmv" }.Any(item => item.Equals(r.fileFormat, StringComparison.OrdinalIgnoreCase))),
					VideoTaggedCount = g.Count(r => (r.HasCategories || r.HasKeywords) && new[] { "video", "mp4", "wmv" }.Any(item => item.Equals(r.fileFormat, StringComparison.OrdinalIgnoreCase))),
					LibraryDate = g.Select(r => r.LibraryDate).FirstOrDefault()
				})
				.OrderBy(r => r.Name).ToList();
			var totalPage = xtraTabControlLibraries.TabPages.OfType<TotalControl>().FirstOrDefault();
			if (totalPage == null)
			{
				totalPage = new TotalControl(totalRecords);
				totalPage.LibraryOpen += (o, e) =>
				{
					_filterControlLibrary.ResetFilter();
					var targetLibraryControl = xtraTabControlLibraries.TabPages
						.OfType<LibraryControl>()
						.FirstOrDefault(lc => lc.GroupName == e.LibraryName);
					xtraTabControlLibraries.SelectedTabPage = targetLibraryControl;
				};
				xtraTabControlLibraries.TabPages.Add(totalPage);
			}
			else
				totalPage.ApplyData(totalRecords);

			foreach (var group in filteredRecords.OrderBy(r => r.library).Select(g => g.library).Distinct())
			{
				var libraryRecords = filteredRecords.Where(r => r.library == group).OrderBy(r => r.linkName).ToList();
				var libraryPage = xtraTabControlLibraries.TabPages
					.OfType<LibraryControl>()
					.FirstOrDefault(lc => lc.GroupName == group);
				if (libraryPage == null)
				{
					libraryPage = new LibraryControl(libraryRecords) { GroupName = group };
					_filterControlLibrary.LinkTagFilterChanged += (o, e) => libraryPage.ApplyFilter(_filterControlLibrary);
					xtraTabControlLibraries.TabPages.Add(libraryPage);
				}
				else
					libraryPage.ApplyData(libraryRecords);
				libraryPage.ApplyFilter(_filterControlLibrary);
			}
		}

		private void buttonXLoadData_Click(object sender, EventArgs e)
		{
			RefreshData();
			AppManager.Instance.ActivateForm(Handle, WindowState == FormWindowState.Maximized, false);
		}

		private void OnSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			var prevLibraryPage = e.PrevPage as LibraryControl;
			prevLibraryPage?.SaveLayout();

			if (e.Page is TotalControl)
				_filterControlTotal.BringToFront();
			else
			{
				var libraryPage = e.Page as LibraryControl;
				if (libraryPage != null)
					_filterControlLibrary.UpdateLiksInfo(
						libraryPage.Records.Count,
						libraryPage.Records.Count(r => !r.HasCategories),
						libraryPage.Records.Count(r => !r.HasKeywords)
						);
				_filterControlLibrary.BringToFront();
			}
		}
	}
}
