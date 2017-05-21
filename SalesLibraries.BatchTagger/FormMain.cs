﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraTab;
using SalesLibraries.BatchTagger.PresentationLayer;
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

		}

		private readonly List<LibraryFilesModel> _records = new List<LibraryFilesModel>();

		private readonly TotalFilter _filterControlTotal;
		private readonly LibraryFilter _filterControlLibrary;

		private void OnFormShown(object sender, EventArgs e)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Loading data...";
				form.TopMost = true;

				var thread = new Thread(() =>
				{
					AppManager.Instance.LoadData();
				});
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}

				Text = String.Format("{0} ({1})", Text, AppManager.Instance.Connections.SoapConnection.Website);

				RefreshData(false);

				form.Close();
			}

			AppManager.Instance.ActivateMainForm();
		}

		public void RefreshData(bool showMessages)
		{
			ClearData();
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _records.AddRange(AppManager.Instance.Connections.SoapConnection.GetLibraryFiles(out message)));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() => _records.AddRange(AppManager.Instance.Connections.SoapConnection.GetLibraryFiles(out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}

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
			xtraTabControlLibraries.TabPages.Clear();
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
			var totalPage = new TotalControl(totalRecords);
			xtraTabControlLibraries.TabPages.Add(totalPage);

			foreach (var group in filteredRecords.OrderBy(r => r.library).Select(g => g.library).Distinct())
			{
				var libraryPage = new LibraryControl(filteredRecords.Where(r => r.library == group).OrderBy(r => r.linkName).ToList()) { GroupName = group };
				_filterControlLibrary.LinkTagFilterChanged += (o, e) => libraryPage.ApplyFilter(_filterControlLibrary);
				libraryPage.ApplyFilter(_filterControlLibrary);
				xtraTabControlLibraries.TabPages.Add(libraryPage);
			}
		}

		private void buttonXLoadData_Click(object sender, EventArgs e)
		{
			RefreshData(true);
			AppManager.Instance.ActivateMainForm();
		}

		private void OnSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
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
