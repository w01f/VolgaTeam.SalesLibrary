using System;
using System.Windows.Forms;

namespace SalesDepot.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabProgramSearch : UserControl
    {
        public bool DataNotSaved { get; set; }
        public bool AllowToSave { get; set; }

        public TabProgramSearch()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        #region Navigation Event Handlers
        public void comboBoxEditSearchStation_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.ClearSearchParameters();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.LoadStation();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.LoadProgramsList();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.gridControlPrograms.Focus();
                ConfigurationClasses.SettingsManager.Instance.ProgramScheduleSelectedStation = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.SelectedStation.Name;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();

            }
        }

        public void dateEditProgramSearchDate_EditValueChanged(object sender, EventArgs e)
        {
            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.gridControlPrograms.Focus();
        }
        #endregion

        #region Ribbon Buttons Clicks
        public void comboBoxEditProgramSearchPrograms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.RunSearch();
        }

        public void buttonItemSearchRun_Click(object sender, EventArgs e)
        {
            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.RunSearch();
        }

        public void buttonItemSearchOutputExcel_Click(object sender, EventArgs e)
        {
            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.ReportActivityList(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.SearchResult, false);
        }

        public void buttonItemSearchOutputPDF_Click(object sender, EventArgs e)
        {
            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.ProgramManager.ReportActivityList(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.ProgramSearch.SearchResult, true);
        }
        #endregion
    }
}
