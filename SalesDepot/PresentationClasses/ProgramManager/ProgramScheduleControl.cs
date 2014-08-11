using System.ComponentModel;
using System.Windows.Forms;
using ProgramManager.CoreObjects;
using SalesDepot.PresentationClasses.WallBin.Decorators;

namespace SalesDepot.PresentationClasses.ProgramManager
{
	[ToolboxItem(false)]
	public partial class ProgramScheduleControl : UserControl
	{
		public ProgramScheduleControl(LibraryDecorator parent)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			ParentDecorator = parent;

			ParentDecorator.Library.ProgramManager.StationChanged += (sender, e) =>
			{
				if (sender != this)
				{
					FormMain.Instance.TabProgramSchedule.AllowToSave = false;
					FormMain.Instance.comboBoxEditProgramScheduleStation.EditValue = ParentDecorator.Library.ProgramManager.SelectedStation.Name;
					FormMain.Instance.labelItemProgramScheduleStationLogo.Image = ParentDecorator.Library.ProgramManager.SelectedStation.Logo;
					FormMain.Instance.ribbonBarProgramScheduleStation.RecalcLayout();
					FormMain.Instance.ribbonPanelProgramSchedule.PerformLayout();
					FormMain.Instance.TabProgramSchedule.AllowToSave = true;

					LoadDay();
				}
			};
		}

		public LibraryDecorator ParentDecorator { get; private set; }

		public void LoadStation()
		{
			ParentDecorator.Library.ProgramManager.LoadStation(this, FormMain.Instance.comboBoxEditProgramScheduleStation.EditValue != null ? FormMain.Instance.comboBoxEditProgramScheduleStation.EditValue.ToString() : string.Empty);
			if (ParentDecorator.Library.ProgramManager.SelectedStation == null) return;
			FormMain.Instance.labelItemProgramScheduleStationLogo.Image = ParentDecorator.Library.ProgramManager.SelectedStation.Logo;
			FormMain.Instance.ribbonBarProgramScheduleStation.RecalcLayout();
			FormMain.Instance.ribbonPanelProgramSchedule.PerformLayout();
		}

		public void LoadDay()
		{
			ParentDecorator.Library.ProgramManager.LoadDay(FormMain.Instance.dateEditProgramScheduleDay.DateTime);
			if (ParentDecorator.Library.ProgramManager.SelectedDay != null)
				gridControlPrograms.DataSource = new BindingList<ProgramActivity>(ParentDecorator.Library.ProgramManager.SelectedDay.ProgramActivities);
			else
				gridControlPrograms.DataSource = null;
			gridControlPrograms.Focus();
		}
	}
}