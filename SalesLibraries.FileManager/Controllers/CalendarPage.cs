using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.DataState;
using SalesLibraries.FileManager.PresentationLayer.Calendars;

namespace SalesLibraries.FileManager.Controllers
{
	class CalendarPage : IPageController
	{
		private bool _isDataChanged;
		private bool _isDataLoading;
		private CalendarContainerControl _viewer;
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		protected LibraryContext ActiveLibraryContext
		{
			get
			{
				var activeWallbin = MainController.Instance.WallbinViews.ActiveWallbin;
				return activeWallbin != null ?
					 activeWallbin.DataStorage :
					null;
			}
		}

		public CalendarPage()
		{
			NeedToUpdate = true;
		}

		public void InitController()
		{
			DataStateObserver.Instance.DataChanged += (o, e) =>
			{
				if (e.ChangeType != DataChangeType.LibrarySelected) return;
				OnLibraryChanged(o, e);
			};

			MainController.Instance.MainForm.buttonItemCalendarSyncStatusDisabled.Click += buttonItemCalendarSyncStatus_Click;
			MainController.Instance.MainForm.buttonItemCalendarSyncStatusEnabled.Click += buttonItemCalendarSyncStatus_Click;
			MainController.Instance.MainForm.buttonItemCalendarSyncStatusDisabled.CheckedChanged += buttonItemCalendarSyncStatus_CheckedChanged;
			MainController.Instance.MainForm.buttonItemCalendarSyncStatusEnabled.CheckedChanged += buttonItemCalendarSyncStatus_CheckedChanged;
			MainController.Instance.MainForm.buttonEditCalendarLocation.EditValueChanged += buttonEditCalendarLocation_EditValueChanged;
			MainController.Instance.MainForm.buttonEditCalendarLocation.ButtonClick += buttonEditCalendarLocation_ButtonClick;
			MainController.Instance.MainForm.buttonItemCalendarSettings.Click += buttonItemCalendarSettings_Click;
			MainController.Instance.MainForm.buttonItemCalendarFontUp.Click += buttonItemCalendarFontUp_Click;
			MainController.Instance.MainForm.buttonItemCalendarFontDown.Click += buttonItemCalendarFontDown_Click;

			_viewer = new CalendarContainerControl();
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;

			if (NeedToUpdate)
			{
				NeedToUpdate = false;
				OnLibraryChanged(this, EventArgs.Empty);
			}

			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(_viewer))
				MainController.Instance.MainForm.pnContainer.Controls.Add(_viewer);
			_viewer.Visible = true;
			_viewer.BringToFront();
		}

		public void ProcessChanges()
		{
			if (!_isDataChanged) return;
			if (ActiveLibraryContext == null) return;
			MainController.Instance.ProcessManager.Run("Saving Changes...", (cancelationToken, formProgess) =>
			{
				ActiveLibraryContext.SaveChanges();
			});
			_isDataChanged = false;
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			if (!IsActive)
			{
				NeedToUpdate = true;
				return;
			}
			if (ActiveLibraryContext == null) return;
			ApplyCalendarSettings();
			LoadCalendarControls();
		}

		public void UpdateStatusBar()
		{
			MainController.Instance.UpdateCommonStatusBar();
		}

		private void ApplyCalendarSettings()
		{
			if (ActiveLibraryContext == null) return;

			_isDataLoading = true;

			var enableCalendar = ActiveLibraryContext.Library.Calendar.Enabled;
			MainController.Instance.MainForm.buttonItemCalendarSyncStatusDisabled.Checked = !enableCalendar;
			MainController.Instance.MainForm.buttonItemCalendarSyncStatusEnabled.Checked = enableCalendar;
			MainController.Instance.MainForm.ribbonBarCalendarLocation.Enabled = enableCalendar;
			MainController.Instance.MainForm.ribbonBarCalendarSettings.Enabled = enableCalendar;
			MainController.Instance.MainForm.ribbonBarCalendarFont.Enabled = enableCalendar;
			MainController.Instance.MainForm.buttonEditCalendarLocation.EditValue = enableCalendar ?
				ActiveLibraryContext.Library.Calendar.Path :
				null;
			UpdateFontButtonsState();

			_isDataLoading = false;
		}

		private void LoadCalendarControls()
		{
			_isDataLoading = true;

			_viewer.LoadData(ActiveLibraryContext.Library.Calendar);

			LoadCalendarPartToggles();

			_isDataLoading = false;
		}

		private void LoadCalendarPartToggles()
		{
			MainController.Instance.MainForm.ribbonPanelCalendar.SuspendLayout();
			MainController.Instance.MainForm.ribbonBarCalendarParts.Items.Clear();
			var partToggles = _viewer.GetPartToggleButtons().ToArray();
			if (partToggles.Any())
				MainController.Instance.MainForm.ribbonBarCalendarParts.Items.AddRange(partToggles);
			else
			{
				var emptyLabel = new LabelItem();
				emptyLabel.ForeColor = System.Drawing.Color.White;
				emptyLabel.Text = "No Calendars";
				MainController.Instance.MainForm.ribbonBarCalendarParts.Items.Add(emptyLabel);
			}
			MainController.Instance.MainForm.ribbonPanelCalendar.ResumeLayout();
			MainController.Instance.MainForm.ribbonBarCalendarParts.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelCalendar.PerformLayout();
		}

		private void UpdateCalendarView()
		{
			_viewer.LoadData(ActiveLibraryContext.Library.Calendar);
		}

		private void UpdateFontButtonsState()
		{
			MainController.Instance.MainForm.buttonItemCalendarFontUp.Enabled = MainController.Instance.Settings.CalendarFontSize < 14;
			MainController.Instance.MainForm.buttonItemCalendarFontDown.Enabled = MainController.Instance.Settings.CalendarFontSize > 10;
		}

		private void buttonItemCalendarSyncStatus_Click(object sender, EventArgs e)
		{
			var clickedButton = (ButtonItem)sender;
			if (clickedButton.Checked) return;
			MainController.Instance.MainForm.buttonItemCalendarSyncStatusDisabled.Checked = false;
			MainController.Instance.MainForm.buttonItemCalendarSyncStatusEnabled.Checked = false;
			clickedButton.Checked = true;
		}

		private void buttonItemCalendarSyncStatus_CheckedChanged(object sender, EventArgs e)
		{
			if (_isDataLoading) return;
			var checkedButton = (ButtonItem)sender;
			if (!checkedButton.Checked) return;
			if (ActiveLibraryContext == null) return;
			var enableCalendar = MainController.Instance.MainForm.buttonItemCalendarSyncStatusEnabled.Checked;
			ActiveLibraryContext.Library.Calendar.Enabled = enableCalendar;
			MainController.Instance.MainForm.ribbonBarCalendarLocation.Enabled = enableCalendar;
			MainController.Instance.MainForm.ribbonBarCalendarSettings.Enabled = enableCalendar;
			MainController.Instance.MainForm.ribbonBarCalendarFont.Enabled = enableCalendar;
			if (!enableCalendar)
			{
				_isDataLoading = true;
				MainController.Instance.MainForm.buttonEditCalendarLocation.EditValue = null;
				ActiveLibraryContext.Library.Calendar.Path = null;
				_isDataLoading = false;
			}
			LoadCalendarControls();
			_isDataChanged = true;
		}

		private void buttonEditCalendarLocation_EditValueChanged(object sender, EventArgs e)
		{
			if (_isDataLoading) return;
			if (ActiveLibraryContext == null) return;
			var path = MainController.Instance.MainForm.buttonEditCalendarLocation.EditValue as String;
			if (!String.IsNullOrEmpty(path) && Directory.Exists(path))
			{
				ActiveLibraryContext.Library.Calendar.Path = path;
				LoadCalendarControls();
			}
			_isDataChanged = true;
		}

		private void buttonEditCalendarLocation_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = !String.IsNullOrEmpty(ActiveLibraryContext.Library.Calendar.Path) ?
					ActiveLibraryContext.Library.Calendar.Path :
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				if (dialog.ShowDialog() != DialogResult.OK) return;
				if (Directory.Exists(dialog.SelectedPath))
					MainController.Instance.MainForm.buttonEditCalendarLocation.EditValue = dialog.SelectedPath;
			}
		}

		private void buttonItemCalendarSettings_Click(object sender, EventArgs e)
		{
			if (ActiveLibraryContext == null) return;
			var dilogResult = DialogResult.None;
			ActiveLibraryContext.Library.PerformTransaction(ActiveLibraryContext,
				libraryCopy =>
				{
					using (var form = new FormCalendarSettings(libraryCopy))
					{
						dilogResult = form.ShowDialog();
					}
					return dilogResult == DialogResult.OK;
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", (cancelationToken, formProgess) => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...",
					(cancelationToken, formProgess) =>
					{
						original.Calendar = current.Calendar.Clone<CalendarSettings>(original);
					}));
			if (dilogResult != DialogResult.OK) return;
			_isDataChanged = true;
			UpdateCalendarView();
		}

		private void buttonItemCalendarFontUp_Click(object sender, EventArgs e)
		{
			_viewer.UpFont();
			UpdateFontButtonsState();
		}

		private void buttonItemCalendarFontDown_Click(object sender, EventArgs e)
		{
			_viewer.DownFont();
			UpdateFontButtonsState();
		}
	}
}
