using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ProgramManager.CoreObjects;
using SalesLibraries.SalesDepot.Business.ProgramSchedule;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.ProgramSchedule
{
	[ToolboxItem(false)]
	public partial class ScheduleSearchControl : UserControl
	{
		public bool IsActive { get; set; }
		protected bool NeedToUpdateStation { get; set; }
		protected virtual ProgramScheduleContext ActiveScheduleContext
		{
			get { throw new NotImplementedException(); }
		}

		public ScheduleSearchControl()
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
			MainController.Instance.MainForm.comboBoxEditProgramSearchStation.Properties.Items.Clear();
			MainController.Instance.MainForm.comboBoxEditProgramSearchStation.Properties.Items.AddRange(ActiveScheduleContext.Stations);
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
			MainController.Instance.MainForm.comboBoxEditProgramSearchStation.EditValue = ActiveScheduleContext.ActiveStation;

			MainController.Instance.MainForm.labelItemProgramSearchStationLogo.Image = ActiveScheduleContext.ActiveStation.Logo;
			MainController.Instance.MainForm.ribbonBarProgramSearchStation.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelProgramSearch.PerformLayout();

			MainController.Instance.MainForm.comboBoxEditProgramSearchPrograms.Properties.Items.Clear();
			MainController.Instance.MainForm.comboBoxEditProgramSearchPrograms.Properties.Items.AddRange(ActiveScheduleContext.ActiveStation.ProgramNames);

			ClearSearchResult();

			NeedToUpdateStation = false;
		}

		protected void RunSearch()
		{
			IEnumerable<ProgramActivity> searchResult = null;
			var dateStart = MainController.Instance.MainForm.dateEditProgramSearchDateStart.DateTime;
			var dateEnd = MainController.Instance.MainForm.dateEditProgramSearchDateEnd.DateTime;
			var program = MainController.Instance.MainForm.comboBoxEditProgramSearchPrograms.EditValue as String;
			MainController.Instance.ProcessManager.Run("Searching Programs...", (cancelletionToken, formProgress) =>
			{
				searchResult = ActiveScheduleContext.ActiveStation.Search(dateStart, dateEnd, program);
			});
			LoadSearchResult(searchResult);
		}

		private void ClearSearchResult()
		{
			MainController.Instance.MainForm.comboBoxEditProgramSearchPrograms.EditValue = null;
			MainController.Instance.MainForm.dateEditProgramSearchDateStart.DateTime = DateTime.Now;
			MainController.Instance.MainForm.dateEditProgramSearchDateEnd.DateTime = MainController.Instance.MainForm.dateEditProgramSearchDateStart.DateTime;
			LoadSearchResult(new ProgramActivity[] { });
		}

		private void LoadSearchResult(IEnumerable<ProgramActivity> activities)
		{
			var dataSource = activities.ToArray();
			gridControlPrograms.DataSource = dataSource;
			gridViewPrograms.RefreshData();
			gridControlPrograms.Focus();
			MainController.Instance.MainForm.ribbonBarProgramSearchOutput.Enabled = dataSource.Any();
		}

		protected void GenerateActivityListReport(bool asPdf)
		{
			var activities = (ProgramActivity[])gridControlPrograms.DataSource;
			MainController.Instance.ProcessManager.Run("Generating Program List...", (cancelletionToken, formProgress) =>
				ActiveScheduleContext.GenerateActivityListReport(activities, asPdf));
		}
	}
}