using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesDepot.ConfigurationClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;

namespace SalesDepot.TabPages
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class TabProgramSchedule : UserControl, IController
	{
		public bool AllowToSave { get; set; }

		public TabProgramSchedule()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			NeedToUpdate = true;
		}

		#region IController Methods
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.comboBoxEditProgramScheduleStation.EditValueChanged += comboBoxEditScheduleStation_EditValueChanged;
			FormMain.Instance.dateEditProgramScheduleDay.EditValueChanged += dateEditScheduleDay_EditValueChanged;
			FormMain.Instance.buttonItemProgramScheduleInfo.CheckedChanged += buttonItemScheduleInfo_CheckedChanged;
			FormMain.Instance.buttonItemProgramScheduleBrowseDay.Click += buttonItemScheduleBrowseType_Click;
			FormMain.Instance.buttonItemProgramScheduleBrowseMonth.Click += buttonItemScheduleBrowseType_Click;
			FormMain.Instance.buttonItemProgramScheduleBrowseWeek.Click += buttonItemScheduleBrowseType_Click;
			FormMain.Instance.buttonItemProgramScheduleBrowseDay.CheckedChanged += buttonItemScheduleBrowseType_CheckedChanged;
			FormMain.Instance.buttonItemProgramScheduleBrowseMonth.CheckedChanged += buttonItemScheduleBrowseType_CheckedChanged;
			FormMain.Instance.buttonItemProgramScheduleBrowseWeek.CheckedChanged += buttonItemScheduleBrowseType_CheckedChanged;
			FormMain.Instance.buttonItemProgramScheduleBrowseForward.Click += buttonItemScheduleBrowseButton_Click;
			FormMain.Instance.buttonItemProgramScheduleBrowseBackward.Click += buttonItemScheduleBrowseButton_Click;
			FormMain.Instance.buttonItemProgramScheduleOutputExcel.Click += buttonItemScheduleOutputExcel_Click;
			FormMain.Instance.buttonItemProgramScheduleOutputPDF.Click += buttonItemScheduleOutputPDF_Click;
			FormMain.Instance.buttonItemProgramScheduleHelp.Click += buttonItemHelp_Click;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			Focus();
			AppManager.Instance.ActivityManager.AddUserActivity("Program Schedule selected");
		}
		#endregion

		#region Navigation Event Handlers
		public void dateEditScheduleDay_EditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
				DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSchedule.LoadDay();
		}

		public void comboBoxEditScheduleStation_EditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
			{
				DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSchedule.LoadStation();
				DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSchedule.LoadDay();
				SettingsManager.Instance.ProgramScheduleSelectedStation = DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.SelectedStation.Name;
				SettingsManager.Instance.SaveSettings();
			}
		}

		#region Browse Event Handlers
		public void buttonItemScheduleBrowseType_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button != null && !button.Checked)
			{
				FormMain.Instance.buttonItemProgramScheduleBrowseDay.Checked = false;
				FormMain.Instance.buttonItemProgramScheduleBrowseWeek.Checked = false;
				FormMain.Instance.buttonItemProgramScheduleBrowseMonth.Checked = false;
				button.Checked = true;
			}
		}

		public void buttonItemScheduleBrowseType_CheckedChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
			{
				if (FormMain.Instance.buttonItemProgramScheduleBrowseDay.Checked)
					SettingsManager.Instance.ProgramScheduleBrowseType = BrowseType.Day;
				else if (FormMain.Instance.buttonItemProgramScheduleBrowseWeek.Checked)
					SettingsManager.Instance.ProgramScheduleBrowseType = BrowseType.Week;
				else if (FormMain.Instance.buttonItemProgramScheduleBrowseMonth.Checked)
					SettingsManager.Instance.ProgramScheduleBrowseType = BrowseType.Month;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemScheduleBrowseButton_Click(object sender, EventArgs e)
		{
			DateTime selectedDate = FormMain.Instance.dateEditProgramScheduleDay.DateTime;

			var button = sender as ButtonItem;

			int directionFactor = 0;
			if (button == FormMain.Instance.buttonItemProgramScheduleBrowseForward)
				directionFactor = 1;
			else if (button == FormMain.Instance.buttonItemProgramScheduleBrowseBackward)
				directionFactor = -1;

			switch (SettingsManager.Instance.ProgramScheduleBrowseType)
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

			FormMain.Instance.dateEditProgramScheduleDay.DateTime = selectedDate;
		}
		#endregion

		#endregion

		#region Ribbon Buttons Clicks
		public void buttonItemScheduleInfo_CheckedChanged(object sender, EventArgs e)
		{
			if (this.AllowToSave)
			{
				ConfigurationClasses.SettingsManager.Instance.ProgramScheduleShowInfo = FormMain.Instance.buttonItemProgramScheduleInfo.Checked;
				ConfigurationClasses.SettingsManager.Instance.SaveSettings();

				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSchedule.gridViewPrograms.OptionsView.ShowPreview = ConfigurationClasses.SettingsManager.Instance.ProgramScheduleShowInfo;

				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSchedule.gridViewPrograms.Focus();
			}
		}


		public void buttonItemScheduleOutputExcel_Click(object sender, EventArgs e)
		{
			using (ToolForms.ProgramManager.FormOutputParameters form = new ToolForms.ProgramManager.FormOutputParameters())
			{
				form.Text = "Output to Excel";
				if (form.ShowDialog() == DialogResult.OK)
				{
					PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.ReportWeekSchedule(form.Station, form.Weeks, false, form.Landscape);
				}
			}
		}

		public void buttonItemScheduleOutputPDF_Click(object sender, EventArgs e)
		{
			using (ToolForms.ProgramManager.FormOutputParameters form = new ToolForms.ProgramManager.FormOutputParameters())
			{
				form.Text = "Output to PDF";
				if (form.ShowDialog() == DialogResult.OK)
				{
					PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.ReportWeekSchedule(form.Station, form.Weeks, true, form.Landscape);
				}
			}
		}

		public void buttonItemHelp_Click(object sender, EventArgs e)
		{
			BusinessClasses.HelpManager.Instance.OpenHelpLink("programschedule");
		}
		#endregion
	}
}
