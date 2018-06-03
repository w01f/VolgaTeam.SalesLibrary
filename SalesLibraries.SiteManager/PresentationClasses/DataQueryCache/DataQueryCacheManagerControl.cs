using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.ServiceConnector.ShortcutsDataQueryCacheService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.DataQueryCache
{
	[ToolboxItem(false)]
	public partial class DataQueryCacheManagerControl : UserControl
	{
		public DataQueryCacheManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void RefreshData(bool showMessages)
		{
			var records = new List<SoapShortcutModel>();
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => records.AddRange(WebSiteManager.Instance.SelectedSite.GetLandingPages(out message)));
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
					AppManager.Instance.PopupMessages.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() => records.AddRange(WebSiteManager.Instance.SelectedSite.GetLandingPages(out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			gridControlRecords.DataSource = records;
		}

		public void ResetDataQueryCache()
		{
			var message = string.Empty;

			var shortcutModels = gridViewRecords.GetSelectedRows()
				.Select(rowIndex => gridViewRecords.GetRow(rowIndex))
				.OfType<SoapShortcutModel>()
				.ToList();

			if (!shortcutModels.Any()) return;

			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Refreshing Snapshots...";
				form.TopMost = true;

				var thread = new Thread(() =>
				{
					foreach (var shortcutModel in shortcutModels)
					{
						Console.WriteLine(shortcutModel.id);
						WebSiteManager.Instance.SelectedSite.ResetDataQueryCache(shortcutModel.id, out message);
						if (!String.IsNullOrEmpty(message))
							break;
						Console.WriteLine(String.Format("Success {0:MM/dd/yyyy hh:mm}", DateTime.Now));
						Console.WriteLine();
					}
				});
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

			if (!String.IsNullOrEmpty(message))
				AppManager.Instance.PopupMessages.ShowWarning(message);
			else
				AppManager.Instance.PopupMessages.ShowInfo("Snapshots Updated");
		}
	}
}