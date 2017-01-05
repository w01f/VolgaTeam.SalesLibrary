using System;
using System.ComponentModel;
using System.Windows.Forms;
using SalesLibraries.SalesDepot.Business.ProgramSchedule;
using SalesLibraries.SalesDepot.Controllers;
using Day = ProgramManager.CoreObjects.Day;

namespace SalesLibraries.SalesDepot.PresentationLayer.ProgramSchedule
{
	[ToolboxItem(false)]
	public partial class ScheduleViewControl : UserControl
	{
		public bool IsActive { get; set; }
		protected bool NeedToUpdateStation { get; set; }
		protected virtual ProgramScheduleContext ActiveScheduleContext
		{
			get { throw new NotImplementedException(); }
		}

		public ScheduleViewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		protected void LoadData()
		{
			ActiveScheduleContext.StationChanged -= OnStationChanged;
			ActiveScheduleContext.StationChanged += OnStationChanged;
			if (ActiveScheduleContext.Initialized || !ActiveScheduleContext.HasData) return;
			MainController.Instance.ProcessManager.Run("Loading Program Schedule...", (cancelletionToken, formProgress) => ActiveScheduleContext.LoadData());
		}

		protected void LoadControls()
		{
			if (!ActiveScheduleContext.HasData) return;
			MainController.Instance.MainForm.comboBoxEditProgramScheduleStation.Properties.Items.Clear();
			MainController.Instance.MainForm.comboBoxEditProgramScheduleStation.Properties.Items.AddRange(ActiveScheduleContext.Stations);

			MainController.Instance.MainForm.dateEditProgramScheduleDay.DateTime = DateTime.Today;

			LoadStation();
		}

		protected void OnStationChanged(object sender, EventArgs e)
		{
			if (!IsActive)
			{
				NeedToUpdateStation = true;
				return;
			}
			LoadStation();
		}

		private void LoadStation()
		{
			MainController.Instance.MainForm.comboBoxEditProgramScheduleStation.EditValue = ActiveScheduleContext.ActiveStation;

			MainController.Instance.MainForm.labelItemProgramScheduleStationLogo.Image = ActiveScheduleContext.ActiveStation.Logo;
			MainController.Instance.MainForm.ribbonBarProgramScheduleStation.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelProgramSchedule.PerformLayout();

			LoadDay();

			NeedToUpdateStation = false;
		}

		protected void LoadDay()
		{
			Day sheduleDay = null;
			MainController.Instance.ProcessManager.Run("Loading Records...", (cancelletionToken, formProgress) =>
			{
				sheduleDay = ActiveScheduleContext.ActiveStation.GetDay(MainController.Instance.MainForm.dateEditProgramScheduleDay.DateTime.Date);
			});
			gridControlPrograms.DataSource = sheduleDay != null ?
				sheduleDay.ProgramActivities :
				null;
			gridViewPrograms.RefreshData();
			gridControlPrograms.Focus();
		}

		protected void UpdateScheduleInfo()
		{
			gridViewPrograms.OptionsView.ShowPreview = MainController.Instance.Settings.ProgramScheduleSettings.ShowInfo;
			gridViewPrograms.RefreshData();
			gridControlPrograms.Focus();
		}

		protected void GenerateWeekScheduleReport(bool asPdf)
		{
			using (var form = new FormOutputParameters(ActiveScheduleContext, MainController.Instance.MainForm.dateEditProgramScheduleDay.DateTime.Date))
			{
				form.Text = "Output to Excel";
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var selectedStation = form.Station;
				var selectedWeeks = form.Weeks;
				var useLandscape = form.Landscape;
				MainController.Instance.ProcessManager.Run("Generating Program Schedule...", (cancelletionToken, formProgress) => 
					ActiveScheduleContext.GenerateWeekScheduleReport(selectedStation,selectedWeeks , asPdf, useLandscape));
			}
		}
	}
}