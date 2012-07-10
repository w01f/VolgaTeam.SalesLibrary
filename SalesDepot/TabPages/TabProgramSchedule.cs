using System;
using System.Windows.Forms;

namespace SalesDepot.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabProgramSchedule : UserControl
    {
        public bool AllowToSave { get; set; }

        public TabProgramSchedule()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        #region Navigation Event Handlers
        public void dateEditScheduleDay_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSchedule.LoadDay();
        }

        public void comboBoxEditScheduleStation_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSchedule.LoadStation();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSchedule.LoadDay();
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleSelectedStation = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.SelectedStation.Name;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        #region Browse Event Handlers
        public void buttonItemScheduleBrowseType_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem button = sender as DevComponents.DotNetBar.ButtonItem;
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
            if (this.AllowToSave)
            {
                if (FormMain.Instance.buttonItemProgramScheduleBrowseDay.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ProgramScheduleBrowseType = ConfigurationClasses.BrowseType.Day;
                else if (FormMain.Instance.buttonItemProgramScheduleBrowseWeek.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ProgramScheduleBrowseType = ConfigurationClasses.BrowseType.Week;
                else if (FormMain.Instance.buttonItemProgramScheduleBrowseMonth.Checked)
                    ConfigurationClasses.SettingsManager.Instance.ProgramScheduleBrowseType = ConfigurationClasses.BrowseType.Month;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        public void buttonItemScheduleBrowseButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = FormMain.Instance.dateEditProgramScheduleDay.DateTime;

            DevComponents.DotNetBar.ButtonItem button = sender as DevComponents.DotNetBar.ButtonItem;

            int directionFactor = 0;
            if (button == FormMain.Instance.buttonItemProgramScheduleBrowseForward)
                directionFactor = 1;
            else if (button == FormMain.Instance.buttonItemProgramScheduleBrowseBackward)
                directionFactor = -1;

            switch (ConfigurationClasses.SettingsManager.Instance.ProgramScheduleBrowseType)
            {
                case ConfigurationClasses.BrowseType.Day:
                    selectedDate = selectedDate.AddDays(1 * directionFactor);
                    break;
                case ConfigurationClasses.BrowseType.Week:
                    selectedDate = selectedDate.AddDays(7 * directionFactor);
                    break;
                case ConfigurationClasses.BrowseType.Month:
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
