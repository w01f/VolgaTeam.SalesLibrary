using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.ConfigurationClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;

namespace SalesDepot.TabPages
{
	[ToolboxItem(false)]
	public partial class TabProgramSearch : UserControl, IController
	{
		public TabProgramSearch()
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
			FormMain.Instance.comboBoxEditProgramSearchStation.EditValueChanged += comboBoxEditSearchStation_EditValueChanged;
			FormMain.Instance.comboBoxEditProgramSearchPrograms.KeyDown += comboBoxEditProgramSearchPrograms_KeyDown;
			FormMain.Instance.dateEditProgramSearchDateStart.EditValueChanged += dateEditProgramSearchDate_EditValueChanged;
			FormMain.Instance.dateEditProgramSearchDateEnd.EditValueChanged += dateEditProgramSearchDate_EditValueChanged;
			FormMain.Instance.buttonItemProgramSearchRun.Click += buttonItemSearchRun_Click;
			FormMain.Instance.buttonItemProgramSearchOutputExcel.Click += buttonItemSearchOutputExcel_Click;
			FormMain.Instance.buttonItemProgramSearchOutputPDF.Click += buttonItemSearchOutputPDF_Click;
			FormMain.Instance.buttonItemProgramSearchHelp.Click += buttonItemHelp_Click;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			Focus();
			AppManager.Instance.ActivityManager.AddUserActivity("Program Search selected");

			if (DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary == null) return;
			if (DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.Enabled) return;

			using (var progressForm = new FormProgress())
			{
				progressForm.laProgress.Text = "Loading Program Schedule...";
				progressForm.TopMost = true;
				progressForm.Show();
				Application.DoEvents();
				var thread = new Thread(() =>
				{
					DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.BuildProgramManager();
					Invoke((MethodInvoker)(() => DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ApplyProgramManager()));
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				progressForm.Close();
			}
		}
		#endregion

		#region Navigation Event Handlers
		public void comboBoxEditSearchStation_EditValueChanged(object sender, EventArgs e)
		{
			if (AllowToSave)
			{
				DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.ClearSearchParameters();
				DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.LoadStation();
				DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.LoadProgramsList();
				DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.gridControlPrograms.Focus();
				SettingsManager.Instance.ProgramScheduleSelectedStation = DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.SelectedStation.Name;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void dateEditProgramSearchDate_EditValueChanged(object sender, EventArgs e)
		{
			DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.gridControlPrograms.Focus();
		}
		#endregion

		#region Ribbon Buttons Clicks
		public void comboBoxEditProgramSearchPrograms_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.RunSearch();
		}

		public void buttonItemSearchRun_Click(object sender, EventArgs e)
		{
			DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.RunSearch();
		}

		public void buttonItemSearchOutputExcel_Click(object sender, EventArgs e)
		{
			DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.ReportActivityList(DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.SearchResult, false);
		}

		public void buttonItemSearchOutputPDF_Click(object sender, EventArgs e)
		{
			DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.ReportActivityList(DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.SearchResult, true);
		}

		public void buttonItemHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("programsearch");
		}
		#endregion

		public bool DataNotSaved { get; set; }
		public bool AllowToSave { get; set; }
	}
}