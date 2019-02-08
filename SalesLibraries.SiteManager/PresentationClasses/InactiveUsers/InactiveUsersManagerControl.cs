using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Security;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using SalesLibraries.ServiceConnector.InactiveUsersService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ConfigurationClasses;
using SalesLibraries.SiteManager.ToolClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.InactiveUsers
{
	[ToolboxItem(false)]
	public partial class InactiveUsersManagerControl : UserControl
	{
		private readonly List<UserViewModel> _records = new List<UserViewModel>();
		private readonly InactiveUsersFilter _filterControl;

		public InactiveUsersManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			var now = DateTime.Now;
			dateEditStart.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
			now = now.AddDays(1);
			dateEditEnd.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

			_filterControl = new InactiveUsersFilter();
			_filterControl.FilterChanged += (o, e) => ApplyData();
			pnCustomFilter.Controls.Add(_filterControl);

			if (CreateGraphics().DpiX > 96)
			{
				splitContainerControlData.Panel2.Width =
					RectangleHelper.ScaleVertical(splitContainerControlData.Panel2.Width,
						splitContainerControlData.ScaleFactor.Width);
			}
		}

		public void RefreshData(bool showMessages)
		{
			ClearData();
			var startDate = dateEditStart.DateTime;
			var endDate = dateEditEnd.DateTime.AddDays(1);
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetInactiveUsers(startDate, endDate, out message)));
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
				var thread = new Thread(() => _records.AddRange(WebSiteManager.Instance.SelectedSite.GetInactiveUsers(startDate, endDate, out message)));
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
			var filteredRecords = new List<UserViewModel>();
			_records.ForEach(x => x.Selected = false);
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => x.GroupNameList.Any(y => _filterControl.SelectedGroups.Contains(y))) : _records);
			gridControlRecords.DataSource = filteredRecords;
			UpdateUsersCount();
		}

		public void ResetUsers()
		{
			var message = string.Empty;

			var userModels = gridViewRecords.GetSelectedRows()
				.Select(rowIndex => gridViewRecords.GetRow(rowIndex))
				.OfType<UserViewModel>()
				.ToList();

			if (!userModels.Any()) return;

			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Sending Emails...";
				form.TopMost = true;

				var thread = new Thread(() =>
				{
					var complexPassword = WebSiteManager.Instance.SelectedSite.IsUserPasswordComplex(out message);

					var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website) ??
										new UsersEmailSettings();
					var sendLocalMessage = emailSettings.SendLocalEmail && LocalUsersEmailManager.Instance.IsAvailable();
					foreach (var userModel in userModels)
					{
						var password = complexPassword ? Membership.GeneratePassword(10, 3) : new PasswordGenerator().Generate();
						WebSiteManager.Instance.SelectedSite.ResetInactiveUser(userModel.login, password, out message);
						if (sendLocalMessage)
							LocalUsersEmailManager.Instance.SendInactiveUserResetNotificationEmail(userModel.FullName, userModel.login, userModel.email, password, userModel.LastActivityDate);
					}
				});
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				if (string.IsNullOrEmpty(message))
					RefreshData(false);
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}

			if (!string.IsNullOrEmpty(message))
				RefreshData(true);
		}

		public void DeleteUsers()
		{
			var message = string.Empty;

			var userModels = gridViewRecords.GetSelectedRows()
				.Select(rowIndex => gridViewRecords.GetRow(rowIndex))
				.OfType<UserViewModel>()
				.ToList();

			if (!userModels.Any()) return;

			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Sending Emails...";
				form.TopMost = true;

				var thread = new Thread(() =>
				{
					var emailSettings = SettingsManager.Instance.UsersEmailSettingItems.FirstOrDefault(item => item.SiteUrl == WebSiteManager.Instance.SelectedSite.Website) ??
										new UsersEmailSettings();
					var sendLocalMessage = emailSettings.SendLocalEmail && LocalUsersEmailManager.Instance.IsAvailable();
					foreach (var userModel in userModels)
					{
						WebSiteManager.Instance.SelectedSite.DeleteInactiveUser(userModel.login, out message);
						if (sendLocalMessage)
							LocalUsersEmailManager.Instance.SendInactiveUserDeleteNotificationEmail(userModel.FullName, userModel.login, userModel.email, userModel.LastActivityDate);
					}
				});
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				if (string.IsNullOrEmpty(message))
					RefreshData(false);
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}

			if (!string.IsNullOrEmpty(message))
				RefreshData(true);
		}

		private void UpdateUsersCount()
		{
			var selectedRecords = gridViewRecords.GetSelectedRows()
				.Select(rowIndex => gridViewRecords.GetRow(rowIndex))
				.OfType<UserViewModel>()
				.ToList();
			var selecteUsersCount = selectedRecords.Count;
			_filterControl.UpdateUsersCount(_records.Count, selecteUsersCount);
		}

		public void ExportUsers()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("InactiveUsers({0:MMddyy-hmmtt}).xls", DateTime.Now);
				dialog.Filter = "Excel files|*.xls";
				dialog.Title = "Export Inactive Users";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					var options = new XlsExportOptions();
					options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
					options.TextExportMode = TextExportMode.Text;
					options.ExportHyperlinks = true;
					options.ShowGridLines = true;
					options.ExportMode = XlsExportMode.SingleFile;
					printableComponentLink.CreateDocument();
					printableComponentLink.PrintingSystem.ExportToXls(dialog.FileName, options);

					if (File.Exists(dialog.FileName))
						Process.Start(dialog.FileName);
				}
			}
		}

		private void OnGridViewSelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
			UpdateUsersCount();
		}

		private void buttonXLoadData_Click(object sender, EventArgs e)
		{
			RefreshData(true);
		}

		private void printableComponentLink_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var startDate = dateEditStart.DateTime;
			var endDate = dateEditEnd.DateTime.AddDays(1);
			var reportHeader = string.Format("{0} Users did not use the website during this period: {1} - {2}", gridViewRecords.RowCount, startDate.ToString("MMMM dd"), endDate.AddDays(-1).ToString("MMMM dd"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}