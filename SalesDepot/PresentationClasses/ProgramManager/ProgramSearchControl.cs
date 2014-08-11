using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using ProgramManager.CoreObjects;
using SalesDepot.PresentationClasses.WallBin.Decorators;
using SalesDepot.ToolForms;

namespace SalesDepot.PresentationClasses.ProgramManager
{
	[ToolboxItem(false)]
	public partial class ProgramSearchControl : UserControl
	{
		public ProgramSearchControl(LibraryDecorator parent)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			ParentDecorator = parent;

			ParentDecorator.Library.ProgramManager.StationChanged += (sender, e) =>
			{
				if (sender != this)
				{
					FormMain.Instance.comboBoxEditProgramSearchStation.EditValue = ParentDecorator.Library.ProgramManager.SelectedStation.Name;
					FormMain.Instance.labelItemProgramSearchStationLogo.Image = ParentDecorator.Library.ProgramManager.SelectedStation.Logo;
					FormMain.Instance.ribbonBarProgramSearchStation.RecalcLayout();
					FormMain.Instance.ribbonPanelProgramSearch.PerformLayout();
				}
			};
		}

		public LibraryDecorator ParentDecorator { get; private set; }
		public ProgramActivity[] SearchResult { get; private set; }

		public void LoadStation()
		{
			ParentDecorator.Library.ProgramManager.LoadStation(this, FormMain.Instance.comboBoxEditProgramSearchStation.EditValue != null ? FormMain.Instance.comboBoxEditProgramSearchStation.EditValue.ToString() : string.Empty);
			if (ParentDecorator.Library.ProgramManager.SelectedStation != null)
			{
				FormMain.Instance.labelItemProgramSearchStationLogo.Image = ParentDecorator.Library.ProgramManager.SelectedStation.Logo;
				FormMain.Instance.ribbonBarProgramSearchStation.RecalcLayout();
				FormMain.Instance.ribbonPanelProgramSearch.PerformLayout();
			}
		}

		public void LoadProgramsList()
		{
			FormMain.Instance.comboBoxEditProgramSearchPrograms.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditProgramSearchPrograms.Properties.Items.AddRange(ParentDecorator.Library.ProgramManager.GetProgramList());
		}

		public void ClearSearchParameters()
		{
			FormMain.Instance.comboBoxEditProgramSearchPrograms.EditValue = null;
			FormMain.Instance.dateEditProgramSearchDateStart.DateTime = DateTime.Now;
			FormMain.Instance.dateEditProgramSearchDateEnd.DateTime = FormMain.Instance.dateEditProgramSearchDateStart.DateTime;
			FormMain.Instance.buttonItemProgramSearchRun.Enabled = true;
			FormMain.Instance.ribbonBarProgramSearchOutput.Enabled = false;
			gridControlPrograms.DataSource = null;
			SearchResult = null;
		}

		public void RunSearch()
		{
			if (ParentDecorator.Library.ProgramManager.SelectedStation != null)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Searching Programs...";
					form.TopMost = true;
					var thread = new Thread(delegate()
					{
						SearchResult = ParentDecorator.Library.ProgramManager.SelectedStation.Search(FormMain.Instance.dateEditProgramSearchDateStart.DateTime, FormMain.Instance.dateEditProgramSearchDateEnd.DateTime, FormMain.Instance.comboBoxEditProgramSearchPrograms.EditValue != null ? FormMain.Instance.comboBoxEditProgramSearchPrograms.EditValue.ToString() : null);
						Invoke((MethodInvoker)delegate
						{
							gridControlPrograms.DataSource = new BindingList<ProgramActivity>(SearchResult);
							FormMain.Instance.ribbonBarProgramSearchOutput.Enabled = SearchResult.Length > 0;
							gridControlPrograms.Focus();
						});
					});
					form.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
			}
		}
	}
}