using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using SalesDepot.Services.QBuilderService;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.QBuilder
{
	[ToolboxItem(false)]
	public partial class QPagesManagerControl : UserControl
	{
		private readonly List<QPageModel> _records = new List<QPageModel>();
		private readonly QPagesFilter _filterControl;

		public QPagesManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			_filterControl = new QPagesFilter();
			_filterControl.FilterChanged += (o, e) => ApplyData();
			pnCustomFilter.Controls.Add(_filterControl);
		}

		public void RefreshData(bool showMessages)
		{
			ClearData();
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetAllPages(out message)));
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
				var thread = new Thread(() => _records.AddRange(BusinessClasses.WebSiteManager.Instance.SelectedSite.GetAllPages(out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			_filterControl.UpdateDataSource(_records.SelectMany(x => x.GroupNameList).OrderBy(g => g).Distinct().ToArray());
			ApplyData();
		}

		public void ClearData()
		{
			gridControlRecords.DataSource = null;
			_records.Clear();
		}

		private void ApplyData()
		{
			var filteredRecords = new List<QPageModel>();
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => x.GroupNameList.Any(y => _filterControl.SelectedGroups.Contains(y))) : _records);
			gridControlRecords.DataSource = filteredRecords;
		}

		public void DeletePage()
		{
			var pageRecord = gridViewRecords.GetFocusedRow() as QPageModel;
			if (pageRecord == null || AppManager.Instance.ShowWarningQuestion(string.Format("Are you sure want to delete this {0}?", pageRecord.Type)) != DialogResult.Yes) return;
			string message = string.Empty;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = string.Format("Deleting {0}...", pageRecord.Type);
				form.TopMost = true;
				var thread = new Thread(() => BusinessClasses.WebSiteManager.Instance.SelectedSite.DeletePages(new[] { pageRecord.id }, out message));
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
			{
				AppManager.Instance.ShowWarning(message);
				return;
			}
			RefreshData(false);
		}

		private void repositoryItemHyperLinkEditPages_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (gridViewRecords.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				if (e.Button.Index == 0)
				{
					var pageRecord = gridViewRecords.GetFocusedRow() as QPageModel;
					if (pageRecord == null) return;
					Clipboard.SetText(pageRecord.url);
				}
			}
		}

		private void repositoryItemButtonEditPagesActions_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (gridViewRecords.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				if (e.Button.Index == 0)
				{
					DeletePage();
				}
			}
		}
	}
}