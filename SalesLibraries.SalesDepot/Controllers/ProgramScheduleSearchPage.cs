using System;
using System.Windows.Forms;
using ProgramManager.CoreObjects;
using SalesLibraries.Common.DataState;
using SalesLibraries.SalesDepot.Business.ProgramSchedule;
using SalesLibraries.SalesDepot.PresentationLayer.ProgramSchedule;

namespace SalesLibraries.SalesDepot.Controllers
{
	class ProgramScheduleSearchPage : ScheduleSearchControl, IPageController
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

		public ProgramScheduleSearchPage()
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

			MainController.Instance.MainForm.comboBoxEditProgramSearchStation.EditValueChanged += OnStationSelectorEditValueChanged;
			MainController.Instance.MainForm.comboBoxEditProgramSearchPrograms.KeyDown += OnProgramSearchProgramsKeyDown;
			MainController.Instance.MainForm.dateEditProgramSearchDateStart.EditValueChanged += OnProgramSearchDateEditValueChanged;
			MainController.Instance.MainForm.dateEditProgramSearchDateEnd.EditValueChanged += OnProgramSearchDateEditValueChanged;
			MainController.Instance.MainForm.buttonItemProgramSearchRun.Click += OnSearchRunClick;
			MainController.Instance.MainForm.buttonItemProgramSearchOutputExcel.Click += OnSearchOutputExcelClick;
			MainController.Instance.MainForm.buttonItemProgramSearchOutputPDF.Click += OnSearchOutputPDFClick;
			MainController.Instance.MainForm.buttonItemProgramSearchHelp.Click += OnHelpClick;
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
			MainController.Instance.ActivityManager.AddUserActivity("Program Schedule selected");
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			MainController.Instance.MainForm.ribbonTabItemProgramSearch.Enabled = ActiveScheduleContext.HasData;

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
			ActiveScheduleContext.SetActiveStation(MainController.Instance.MainForm.comboBoxEditProgramSearchStation.EditValue as Station);
		}

		private void OnProgramSearchProgramsKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				RunSearch();
		}

		private void OnProgramSearchDateEditValueChanged(object sender, EventArgs e)
		{
			gridControlPrograms.Focus();
		}

		private void OnSearchRunClick(object sender, EventArgs e)
		{
			RunSearch();
		}

		private void OnSearchOutputExcelClick(object sender, EventArgs e)
		{
			GenerateActivityListReport(false);
		}

		private void OnSearchOutputPDFClick(object sender, EventArgs e)
		{
			GenerateActivityListReport(true);
		}

		private void OnHelpClick(object sender, EventArgs e)
		{
			MainController.Instance.HelpManager.OpenHelpLink("programsearch");
		}
		#endregion
	}
}
