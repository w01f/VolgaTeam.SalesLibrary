using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.VideoLinkData
{
	[ToolboxItem(false)]
	public sealed partial class ContainerControl : UserControl, IActivitiesView
	{
		private readonly List<VideoLinkInfo> _records = new List<VideoLinkInfo>();
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		private bool _active;
		public bool Active
		{
			get { return _active; }
			set
			{
				_active = value;
				Visible = _active;
				_filterControl.Visible = _active;
			}
		}

		private readonly Filter _filterControl;
		public IEnumerable<Control> FilterControls => new[] { _filterControl };

		public ContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_filterControl = new Filter();
			_filterControl.FilterChanged += (o, e) => ApplyData();
		}

		public void ShowView()
		{
			Active = true;
			BringToFront();
			_filterControl.BringToFront();
		}

		public void UpdateData(bool showMessages, ref string updateMessage)
		{
			ClearData();
			if (!Active) return;
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetVideoLinkInfo(out message)));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetVideoLinkInfo(out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			_filterControl.UpdateDataSource(_records.OrderBy(g => g.station).Select(x => x.station).Where(x => !String.IsNullOrEmpty(x)).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			xtraTabControlGroups.TabPages.Clear();
			_records.Clear();
		}

		public void ExportData()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("VideoLinksSummary({0}).csv", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "CSV files|*.csv";
				dialog.Title = "Video Link Summary";
				if (dialog.ShowDialog() != DialogResult.OK) return;
				var content = String.Join(Environment.NewLine, xtraTabControlGroups
					.TabPages
					.OfType<GroupControl>()
					.First()
					.Records
					.Select(r => String.Join(";", new[] { r.fileName, r.linkName, r.categoryGroups, r.categoryTags, r.keywords, r.linkNote, r.hoverNote, r.mp4Url, r.thumbUrl, r.station, r.DateModify.HasValue ? r.DateModify.Value.ToString("MM/dd/yyyy") : String.Empty }.Select(s => String.Format("\"{0}\"", s)))));
				File.WriteAllText(dialog.FileName, content);
				if (File.Exists(dialog.FileName))
					Process.Start(dialog.FileName);
			}
		}

		private void ApplyData()
		{
			xtraTabControlGroups.TabPages.Clear();
			var filteredRecords = new List<VideoLinkInfo>();
			filteredRecords.AddRange(_filterControl.EnableFilter ?
				_records.Where(g => _filterControl.SelectedGroups.Contains(g.station)) :
				_records);

			var totalRecords = filteredRecords.OrderBy(r => r.station).ThenBy(r => r.fileName).ToList();
			var totalPage = new GroupControl(totalRecords) { GroupName = "Total Summary" };
			xtraTabControlGroups.TabPages.Add(totalPage);

			foreach (var group in filteredRecords.OrderBy(r => r.station).Select(g => g.station).Distinct())
			{
				var groupPage = new GroupControl(filteredRecords.Where(r => r.station == group).OrderBy(r => r.fileName).ToList()) { GroupName = group };
				xtraTabControlGroups.TabPages.Add(groupPage);
			}
		}
	}
}