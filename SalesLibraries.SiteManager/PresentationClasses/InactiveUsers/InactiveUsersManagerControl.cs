using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using SalesLibraries.ServiceConnector.InactiveUsersService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ConfigurationClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.InactiveUsers
{
	[ToolboxItem(false)]
	public partial class InactiveUsersManagerControl : UserControl
	{
		private readonly List<UserModel> _records = new List<UserModel>();
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

			LoadDefaultEmailSettings();

			if (CreateGraphics().DpiX > 96)
			{
				splitContainerControlMain.Panel1.Width =
					RectangleHelper.ScaleVertical(splitContainerControlMain.Panel1.Width,
						splitContainerControlMain.ScaleFactor.Width);

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
			var filteredRecords = new List<UserModel>();
			_records.ForEach(x => x.Selected = false);
			filteredRecords.AddRange(_filterControl.EnableFilter ? _records.Where(x => x.GroupNameList.Any(y => _filterControl.SelectedGroups.Contains(y))) : _records);
			gridControlRecords.DataSource = filteredRecords;
			UpdateUsersCount();
		}

		private void ResetUsers()
		{
			var message = string.Empty;

			var userIds = _records.Where(x => x.Selected).Select(x => x.id.ToString()).ToArray();
			var onlyEmail = !_filterControl.EmailReset;
			var sender = textEditEmailResetSender.EditValue?.ToString() ?? string.Empty;
			var subject = textEditEmailResetSubject.EditValue?.ToString() ?? string.Empty;
			var body = memoEditEmailResetBody.EditValue?.ToString() ?? string.Empty;

			SettingsManager.Instance.InactiveUsersSettings.ResetEmailSender = sender;
			SettingsManager.Instance.InactiveUsersSettings.ResetEmailSubject = subject;
			SettingsManager.Instance.InactiveUsersSettings.ResetEmailBody = body;
			SettingsManager.Instance.InactiveUsersSettings.Save();

			if (userIds.Length == 0 || string.IsNullOrEmpty(sender)) return;

			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Sending Emails...";
				form.TopMost = true;

				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.ResetUsers(userIds, onlyEmail, sender, subject, body, out message));
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

		private void DeleteUsers()
		{
			var message = string.Empty;

			var userIds = _records.Where(x => x.Selected).Select(x => x.id.ToString()).ToArray();
			var onlyEmail = !_filterControl.EmailDelete;
			var sender = textEditEmailDeleteSender.EditValue != null ? textEditEmailDeleteSender.EditValue.ToString() : string.Empty;
			var subject = textEditEmailDeleteSubject.EditValue != null ? textEditEmailDeleteSubject.EditValue.ToString() : string.Empty;
			var body = memoEditEmailDeleteBody.EditValue != null ? memoEditEmailDeleteBody.EditValue.ToString() : string.Empty;

			SettingsManager.Instance.InactiveUsersSettings.DeleteEmailSender = sender;
			SettingsManager.Instance.InactiveUsersSettings.DeleteEmailSubject = subject;
			SettingsManager.Instance.InactiveUsersSettings.DeleteEmailBody = body;
			SettingsManager.Instance.InactiveUsersSettings.Save();

			if (userIds.Length == 0 || string.IsNullOrEmpty(sender)) return;

			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Sending Emails...";
				form.TopMost = true;

				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.DeleteUsers(userIds, onlyEmail, sender, subject, body, out message));
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

		private void LoadDefaultEmailSettings()
		{
			textEditEmailResetSender.EditValue = SettingsManager.Instance.InactiveUsersSettings.ResetEmailSender;
			textEditEmailResetSubject.EditValue = SettingsManager.Instance.InactiveUsersSettings.ResetEmailSubject;
			memoEditEmailResetBody.EditValue = SettingsManager.Instance.InactiveUsersSettings.ResetEmailBody;

			textEditEmailDeleteSender.EditValue = SettingsManager.Instance.InactiveUsersSettings.DeleteEmailSender;
			textEditEmailDeleteSubject.EditValue = SettingsManager.Instance.InactiveUsersSettings.DeleteEmailSubject;
			memoEditEmailDeleteBody.EditValue = SettingsManager.Instance.InactiveUsersSettings.DeleteEmailBody;
		}

		private void UpdateUsersCount()
		{
			var selecteUsersCount = _records.Count(x => x.Selected);
			labelControlEmailResetUserCount.Text = labelControlEmailDeleteUserCount.Text = selecteUsersCount > 0 ? String.Format("The Email will be sent to: {0} {1}", selecteUsersCount, selecteUsersCount > 1 ? "Users" : "User") : "Email will not be sent. There are no selected users";
		}

		public void ExportUsers()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("InactiveUsers({0}).xls", DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xls";
				dialog.Title = "Export Inactive Users";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					gridColumnUsersSelected.Visible = false;
					var options = new XlsExportOptions();
					options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
					options.TextExportMode = TextExportMode.Text;
					options.ExportHyperlinks = true;
					options.ShowGridLines = true;
					options.ExportMode = XlsExportMode.SingleFile;
					printableComponentLink.CreateDocument();
					printableComponentLink.PrintingSystem.ExportToXls(dialog.FileName, options);
					gridColumnUsersSelected.Visible = true;

					if (File.Exists(dialog.FileName))
						Process.Start(dialog.FileName);
				}
			}
		}

		private void buttonXLoadData_Click(object sender, EventArgs e)
		{
			RefreshData(true);
		}

		private void gridViewRecords_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column == gridColumnUsersSelected)
				UpdateUsersCount();
		}

		private void repositoryItemCheckEditUsers_CheckedChanged(object sender, EventArgs e)
		{
			gridViewRecords.CloseEditor();
		}

		private void buttonXEmailResetSend_Click(object sender, EventArgs e)
		{
			ResetUsers();
		}

		private void buttonXEmailDeleteSend_Click(object sender, EventArgs e)
		{
			DeleteUsers();
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