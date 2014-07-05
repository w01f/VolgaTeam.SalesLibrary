using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using FileManager.ConfigurationClasses;
using FileManager.PresentationClasses.TabPages;
using FileManager.ToolForms;
using FileManager.ToolForms.Settings;

namespace FileManager.Controllers
{
	public class CalendarController : IPageController
	{
		private FormCalendarSettings _formSettings;

		private bool _initialization;
		private TabOvernightsCalendarControl _tabPage;

		public CalendarController()
		{
			FormMain.Instance.buttonItemCalendarSyncStatusDisabled.Click += buttonItemCalendarSyncStatus_Click;
			FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Click += buttonItemCalendarSyncStatus_Click;
			FormMain.Instance.buttonItemCalendarSyncStatusDisabled.CheckedChanged += buttonItemCalendarSyncStatus_CheckedChanged;
			FormMain.Instance.buttonItemCalendarSyncStatusEnabled.CheckedChanged += buttonItemCalendarSyncStatus_CheckedChanged;
			FormMain.Instance.buttonEditCalendarLocation.EditValueChanged += buttonEditCalendarLocation_EditValueChanged;
			FormMain.Instance.buttonEditCalendarLocation.ButtonClick += buttonEditCalendarLocation_ButtonClick;
			FormMain.Instance.buttonItemCalendarSettings.Click += buttonItemCalendarSettings_Click;
			FormMain.Instance.buttonItemCalendarFontUp.Click += buttonItemCalendarFontUp_Click;
			FormMain.Instance.buttonItemCalendarFontDown.Click += buttonItemCalendarFontDown_Click;
		}

		#region IPageController Members
		public void InitController()
		{
			_initialization = true;

			_tabPage = new TabOvernightsCalendarControl();
			ApplyOvernightsCalendar();
			if (!FormMain.Instance.pnMain.Controls.Contains(_tabPage))
				FormMain.Instance.pnMain.Controls.Add(_tabPage);

			_formSettings = new FormCalendarSettings();

			MainController.Instance.LibraryChanged += (sender, args) =>
														  {
															  _initialization = true;
															  ApplyOvernightsCalendar();
															  _initialization = false;
														  };

			_initialization = false;
		}

		public void PrepareTab(TabPageEnum tabPage) { }

		public void ShowTab()
		{
			_initialization = true;
			ApplyOvernightsCalendar();
			_initialization = false;
			_tabPage.BringToFront();
		}
		#endregion

		private void ApplyOvernightsCalendar()
		{
			var activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator == null || !activeDecorator.Library.IsConfigured || !SettingsManager.Instance.EnableOvernightsCalendarTab) return;
			FormMain.Instance.buttonItemCalendarSyncStatusDisabled.Checked = !activeDecorator.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Checked = activeDecorator.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.buttonEditCalendarLocation.EditValue = activeDecorator.Library.OvernightsCalendar.RootFolder.FullName;
			FormMain.Instance.ribbonBarCalendarLocation.Enabled = activeDecorator.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.ribbonBarCalendarParts.Enabled = activeDecorator.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.ribbonBarCalendarSettings.Enabled = activeDecorator.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.ribbonBarCalendarFont.Enabled = activeDecorator.Library.OvernightsCalendar.Enabled;
			_tabPage.Enabled = activeDecorator.Library.OvernightsCalendar.Enabled;

			UpdateCalendarFontButtonsStatus();

			FormMain.Instance.ribbonBarCalendarParts.Items.Clear();
			FormMain.Instance.ribbonBarCalendarParts.Items.AddRange(activeDecorator.OvernightsCalendar.PartToggles.ToArray());
		
			if (!_tabPage.Controls.Contains(activeDecorator.OvernightsCalendar))
				_tabPage.Controls.Add(activeDecorator.OvernightsCalendar);
			activeDecorator.OvernightsCalendar.BringToFront();
		}

		private void UpdateCalendarFontButtonsStatus()
		{
			FormMain.Instance.buttonItemCalendarFontUp.Enabled = SettingsManager.Instance.CalendarFontSize < 14;
			FormMain.Instance.buttonItemCalendarFontDown.Enabled = SettingsManager.Instance.CalendarFontSize > 10;
		}

		private void buttonItemCalendarSyncStatus_Click(object sender, EventArgs e)
		{
			FormMain.Instance.buttonItemCalendarSyncStatusDisabled.Checked = false;
			FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Checked = false;
			(sender as ButtonItem).Checked = true;
		}

		private void buttonItemCalendarSyncStatus_CheckedChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null || _initialization) return;
			MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled = FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Checked;
			FormMain.Instance.ribbonBarCalendarLocation.Enabled = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.ribbonBarCalendarParts.Enabled = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.ribbonBarCalendarSettings.Enabled = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
			FormMain.Instance.ribbonBarCalendarFont.Enabled = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
			_tabPage.Enabled = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
			if (MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled && SettingsManager.Instance.EnableOvernightsCalendarTab)
				using (var formProgress = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					formProgress.TopMost = true;
					formProgress.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
					var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate
					{
						MainController.Instance.ActiveDecorator.BuildOvernightsCalendar(true);
						ApplyOvernightsCalendar();
					}));
					formProgress.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					formProgress.Close();
					FormMain.Instance.ribbonControl.Enabled = true;
				}
			MainController.Instance.ActiveDecorator.Library.Save();
		}

		private void buttonEditCalendarLocation_EditValueChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null || _initialization) return;
			var path = FormMain.Instance.buttonEditCalendarLocation.EditValue != null ? FormMain.Instance.buttonEditCalendarLocation.EditValue.ToString() : string.Empty;
			if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
			{
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.RootFolder = new DirectoryInfo(path);
				using (var formProgress = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					formProgress.TopMost = true;
					formProgress.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
					var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate
					{
						MainController.Instance.ActiveDecorator.BuildOvernightsCalendar(true);
						ApplyOvernightsCalendar();
					}));
					formProgress.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					formProgress.Close();
					FormMain.Instance.ribbonControl.Enabled = true;
				}
			}
			MainController.Instance.ActiveDecorator.Library.Save();
		}

		private void buttonEditCalendarLocation_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.RootFolder.FullName;
				if (dialog.ShowDialog() != DialogResult.OK) return;
				if (Directory.Exists(dialog.SelectedPath))
					FormMain.Instance.buttonEditCalendarLocation.EditValue = dialog.SelectedPath;
			}
		}

		private void buttonItemCalendarSettings_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
			{
				if (_formSettings == null) _formSettings = new FormCalendarSettings();
				if (_formSettings.ShowDialog() == DialogResult.OK)
					MainController.Instance.ActiveDecorator.OvernightsCalendar.RefreshColors();
			}
		}

		private void buttonItemCalendarFontUp_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.CalendarFontSize++;
			SettingsManager.Instance.Save();
			if (MainController.Instance.ActiveDecorator != null)
				MainController.Instance.ActiveDecorator.OvernightsCalendar.RefreshFont();
			UpdateCalendarFontButtonsStatus();
		}

		private void buttonItemCalendarFontDown_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.CalendarFontSize--;
			SettingsManager.Instance.Save();
			if (MainController.Instance.ActiveDecorator != null)
				MainController.Instance.ActiveDecorator.OvernightsCalendar.RefreshFont();
			UpdateCalendarFontButtonsStatus();
		}
	}
}