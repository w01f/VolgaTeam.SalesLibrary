using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.ProgramManager
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ProgramScheduleControl : UserControl
    {
        public WallBin.Decorators.LibraryDecorator ParentDecorator { get; private set; }

        public ProgramScheduleControl(WallBin.Decorators.LibraryDecorator parent)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.ParentDecorator = parent;

            this.ParentDecorator.Library.ProgramManager.StationChanged += new EventHandler<EventArgs>((sender, e) =>
            {
                if (sender != this)
                {
                    FormMain.Instance.TabProgramSchedule.AllowToSave = false;
                    FormMain.Instance.comboBoxEditProgramScheduleStation.EditValue = this.ParentDecorator.Library.ProgramManager.SelectedStation.Name;
                    FormMain.Instance.labelItemProgramScheduleStationLogo.Image = this.ParentDecorator.Library.ProgramManager.SelectedStation.Logo;
                    FormMain.Instance.ribbonBarProgramScheduleStation.RecalcLayout();
                    FormMain.Instance.ribbonPanelProgramSchedule.PerformLayout();
                    FormMain.Instance.TabProgramSchedule.AllowToSave = true;

                    LoadDay();
                }
            });
        }

        public void LoadStation()
        {
            this.ParentDecorator.Library.ProgramManager.LoadStation(this, FormMain.Instance.comboBoxEditProgramScheduleStation.EditValue != null ? FormMain.Instance.comboBoxEditProgramScheduleStation.EditValue.ToString() : string.Empty);
            if (this.ParentDecorator.Library.ProgramManager.SelectedStation != null)
            {
                FormMain.Instance.labelItemProgramScheduleStationLogo.Image = this.ParentDecorator.Library.ProgramManager.SelectedStation.Logo;
                FormMain.Instance.ribbonBarProgramScheduleStation.RecalcLayout();
                FormMain.Instance.ribbonPanelProgramSchedule.PerformLayout();
            }
        }

        public void LoadDay()
        {
            this.ParentDecorator.Library.ProgramManager.LoadDay(FormMain.Instance.dateEditProgramScheduleDay.DateTime);
            if (this.ParentDecorator.Library.ProgramManager.SelectedDay != null)
                gridControlPrograms.DataSource = new BindingList<global::ProgramManager.CoreObjects.ProgramActivity>(this.ParentDecorator.Library.ProgramManager.SelectedDay.ProgramActivities);
            else
                gridControlPrograms.DataSource = null;
            gridControlPrograms.Focus();
        }
    }
}
