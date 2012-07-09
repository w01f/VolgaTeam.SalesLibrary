using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.ProgramManager
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ProgramSearchControl : UserControl
    {
        public WallBin.Decorators.LibraryDecorator ParentDecorator { get; private set; }
        public global::ProgramManager.CoreObjects.ProgramActivity[] SearchResult { get; private set; }

        public ProgramSearchControl(WallBin.Decorators.LibraryDecorator parent)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.ParentDecorator = parent;

            this.ParentDecorator.Library.ProgramManager.StationChanged += new EventHandler<EventArgs>((sender, e) =>
            {
                if (sender != this)
                {
                    FormMain.Instance.comboBoxEditProgramSearchStation.EditValue = this.ParentDecorator.Library.ProgramManager.SelectedStation.Name;
                    FormMain.Instance.labelItemProgramSearchStationLogo.Image = this.ParentDecorator.Library.ProgramManager.SelectedStation.Logo;
                    FormMain.Instance.ribbonBarProgramSearchStation.RecalcLayout();
                    FormMain.Instance.ribbonPanelProgramSearch.PerformLayout();
                }
            });
        }

        public void LoadStation()
        {
            this.ParentDecorator.Library.ProgramManager.LoadStation(this, FormMain.Instance.comboBoxEditProgramSearchStation.EditValue != null ? FormMain.Instance.comboBoxEditProgramSearchStation.EditValue.ToString() : string.Empty);
            if (this.ParentDecorator.Library.ProgramManager.SelectedStation != null)
            {
                FormMain.Instance.labelItemProgramSearchStationLogo.Image = this.ParentDecorator.Library.ProgramManager.SelectedStation.Logo;
                FormMain.Instance.ribbonBarProgramSearchStation.RecalcLayout();
                FormMain.Instance.ribbonPanelProgramSearch.PerformLayout();
            }
        }

        public void LoadProgramsList()
        {
            FormMain.Instance.comboBoxEditProgramSearchPrograms.Properties.Items.Clear();
            FormMain.Instance.comboBoxEditProgramSearchPrograms.Properties.Items.AddRange(this.ParentDecorator.Library.ProgramManager.GetProgramList());
        }

        public void ClearSearchParameters()
        {
            FormMain.Instance.comboBoxEditProgramSearchPrograms.EditValue = null;
            FormMain.Instance.dateEditProgramSearchDateStart.DateTime = DateTime.Now;
            FormMain.Instance.dateEditProgramSearchDateEnd.DateTime = FormMain.Instance.dateEditProgramSearchDateStart.DateTime;
            FormMain.Instance.buttonItemProgramSearchRun.Enabled = true;
            FormMain.Instance.ribbonBarProgramSearchOutput.Enabled = false;
            gridControlPrograms.DataSource = null;
            this.SearchResult = null;
        }

        public void RunSearch()
        {
            if (this.ParentDecorator.Library.ProgramManager.SelectedStation != null)
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    form.laProgress.Text = "Searching Programs...";
                    form.TopMost = true;
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        this.SearchResult = this.ParentDecorator.Library.ProgramManager.SelectedStation.Search(FormMain.Instance.dateEditProgramSearchDateStart.DateTime, FormMain.Instance.dateEditProgramSearchDateEnd.DateTime, FormMain.Instance.comboBoxEditProgramSearchPrograms.EditValue != null ? FormMain.Instance.comboBoxEditProgramSearchPrograms.EditValue.ToString() : null);
                        this.Invoke((MethodInvoker)delegate()
                        {
                            gridControlPrograms.DataSource = new BindingList<global::ProgramManager.CoreObjects.ProgramActivity>(this.SearchResult);
                            FormMain.Instance.ribbonBarProgramSearchOutput.Enabled = this.SearchResult.Length > 0;
                            gridControlPrograms.Focus();
                        });
                    }));
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
