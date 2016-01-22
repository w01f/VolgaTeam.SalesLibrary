using System;
using DevComponents.DotNetBar;
using ProgramManager.CoreObjects;
using SalesLibraries.Common.DataState;
using SalesLibraries.SalesDepot.Business.ProgramSchedule;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.PresentationLayer.ProgramSchedule;

namespace SalesLibraries.SalesDepot.Controllers
{
	class ProgramScheduleViewPage : ScheduleViewControl, IPageController
	{
		public bool NeedToUpdate { get; set; }

		protected override ProgramScheduleContext ActiveScheduleContext
		{
			get
			{
				var activeWallbin = MainController.Instance.WallbinViews.ActiveWallbin;
				return activeWallbin != null ?
					 activeWallbin.ProgramSchedule :
					null;
			}
		}

		public ProgramScheduleViewPage()
		{
			NeedToUpdate = true;
		}

		#region IPageController Methods
		public void InitController()
		{
			DataStateObserver.Instance.DataChanged += (o, e) =>
			{
				if (e.ChangeType != DataChangeType.LibrarySelected) return;
				OnLibraryChanged(o, e);
			};

			MainController.Instance.MainForm.buttonItemProgramScheduleInfo.Checked = MainController.Instance.Settings.ProgramScheduleSettings.ShowInfo;
			switch (MainController.Instance.Settings.ProgramScheduleSettings.BrowseType)
			{
				case BrowseType.Day:
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseDay.Checked = true;
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseMonth.Checked = false;
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseWeek.Checked = false;
					break;
				case BrowseType.Week:
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseDay.Checked = false;
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseMonth.Checked = false;
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseWeek.Checked = true;
					break;
				case BrowseType.Month:
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseDay.Checked = false;
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseMonth.Checked = true;
					MainController.Instance.MainForm.buttonItemProgramScheduleBrowseWeek.Checked = false;
					break;
			}
			UpdateScheduleInfo();

			MainController.Instance.MainForm.comboBoxEditProgramScheduleStation.EditValueChanged += OnStationSelectorEditValueChanged;
			MainController.Instance.MainForm.dateEditProgramScheduleDay.EditValueChanged += OnDateSelectorEditValueChanged;
			MainController.Instance.MainForm.buttonItemProgramScheduleInfo.CheckedChanged += OnScheduleInfoCheckedChanged;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseDay.Click += OnScheduleBrowseTypeClick;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseMonth.Click += OnScheduleBrowseTypeClick;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseWeek.Click += OnScheduleBrowseTypeClick;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseDay.CheckedChanged += OnScheduleBrowseTypeCheckedChanged;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseMonth.CheckedChanged += OnScheduleBrowseTypeCheckedChanged;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseWeek.CheckedChanged += OnScheduleBrowseTypeCheckedChanged;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseForward.Click += OnScheduleBrowseButtonClick;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseBackward.Click += OnScheduleBrowseButtonClick;
			MainController.Instance.MainForm.buttonItemProgramScheduleOutputExcel.Click += OnScheduleOutputExcelClick;
			MainController.Instance.MainForm.buttonItemProgramScheduleOutputPDF.Click += OnScheduleOutputPDFClick;
			MainController.Instance.MainForm.buttonItemProgramScheduleHelp.Click += OnHelpClick;
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;

			if (NeedToUpdate)
			{
				NeedToUpdate = false;
				OnLibraryChanged(this, EventArgs.Empty);
			}

			if (NeedToUpdateStation)
			{
				NeedToUpdateStation = false;
				OnStationChanged(this, EventArgs.Empty);
			}

			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(this))
				MainController.Instance.MainForm.pnContainer.Controls.Add(this);
			BringToFront();
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			MainController.Instance.MainForm.ribbonTabItemProgramSchedule.Enabled = ActiveScheduleContext.HasData;

			if (!IsActive)
			{
				NeedToUpdate = true;
				return;
			}
			if (ActiveScheduleContext == null) return;
			LoadData();
			LoadControls();
		}
		#endregion

		#region Control Event Handlers
		private void OnStationSelectorEditValueChanged(object sender, EventArgs e)
		{
			ActiveScheduleContext.SetActiveStation(MainController.Instance.MainForm.comboBoxEditProgramScheduleStation.EditValue as Station);
		}

		private void OnDateSelectorEditValueChanged(object sender, EventArgs e)
		{
			LoadDay();
		}

		private void OnScheduleInfoCheckedChanged(object sender, EventArgs e)
		{
			MainController.Instance.Settings.ProgramScheduleSettings.ShowInfo = MainController.Instance.MainForm.buttonItemProgramScheduleInfo.Checked;
			MainController.Instance.Settings.SaveSettings();
			UpdateScheduleInfo();
		}

		private void OnScheduleBrowseTypeClick(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null || button.Checked) return;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseDay.Checked = false;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseWeek.Checked = false;
			MainController.Instance.MainForm.buttonItemProgramScheduleBrowseMonth.Checked = false;
			button.Checked = true;
		}

		private void OnScheduleBrowseTypeCheckedChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.MainForm.buttonItemProgramScheduleBrowseDay.Checked)
				MainController.Instance.Settings.ProgramScheduleSettings.BrowseType = BrowseType.Day;
			else if (MainController.Instance.MainForm.buttonItemProgramScheduleBrowseWeek.Checked)
				MainController.Instance.Settings.ProgramScheduleSettings.BrowseType = BrowseType.Week;
			else if (MainController.Instance.MainForm.buttonItemProgramScheduleBrowseMonth.Checked)
				MainController.Instance.Settings.ProgramScheduleSettings.BrowseType = BrowseType.Month;
			MainController.Instance.Settings.SaveSettings();
		}

		private void OnScheduleBrowseButtonClick(object sender, EventArgs e)
		{
			var selectedDate = MainController.Instance.MainForm.dateEditProgramScheduleDay.DateTime.Date;

			var button = sender as ButtonItem;

			int directionFactor = 0;
			if (button == MainController.Instance.MainForm.buttonItemProgramScheduleBrowseForward)
				directionFactor = 1;
			else if (button == MainController.Instance.MainForm.buttonItemProgramScheduleBrowseBackward)
				directionFactor = -1;

			switch (MainController.Instance.Settings.ProgramScheduleSettings.BrowseType)
			{
				case BrowseType.Day:
					selectedDate = selectedDate.AddDays(1 * directionFactor);
					break;
				case BrowseType.Week:
					selectedDate = selectedDate.AddDays(7 * directionFactor);
					break;
				case BrowseType.Month:
					selectedDate = selectedDate.AddMonths(1 * directionFactor);
					break;
			}

			MainController.Instance.MainForm.dateEditProgramScheduleDay.DateTime = selectedDate;
		}

		private void OnScheduleOutputPDFClick(object sender, EventArgs e)
		{
			GenerateWeekScheduleReport(true);
		}

		private void OnScheduleOutputExcelClick(object sender, EventArgs e)
		{
			GenerateWeekScheduleReport(false);
		}

		private void OnHelpClick(object sender, EventArgs e)
		{
			MainController.Instance.HelpManager.OpenHelpLink("programschedule");
		}
		#endregion
	}
}
