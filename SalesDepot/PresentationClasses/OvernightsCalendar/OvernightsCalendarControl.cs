using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.OvernightsCalendar
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OvernightsCalendarControl : UserControl
    {
        private bool _buildInProgress = false;
        public WallBin.Decorators.LibraryDecorator ParentDecorator { get; private set; }
        public List<YearControl> Years { get; private set; }
        public bool ViewBuilded { get; set; }

        public OvernightsCalendarControl(WallBin.Decorators.LibraryDecorator parent)
        {
            InitializeComponent();
            this.ParentDecorator = parent;
            this.Dock = DockStyle.Fill;
            this.Years = new List<YearControl>();

            xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl_SelectedPageChanged);
        }

        public void Build()
        {
            if (!this.ViewBuilded)
            {
                _buildInProgress = true;
                xtraTabControl.TabPages.Clear();
                this.Years.Clear();
                foreach (BusinessClasses.CalendarYear year in this.ParentDecorator.Library.OvernightsCalendar.Years)
                {
                    this.Years.Add(new YearControl(year));
                    Application.DoEvents();
                }
                xtraTabControl.TabPages.AddRange(this.Years.ToArray());

                YearControl selectedTab = this.Years.Where(x=>x.Data.Year.Equals(ConfigurationClasses.SettingsManager.Instance.SelectedCalendarYear)).FirstOrDefault();
                if (selectedTab == null)
                    selectedTab = this.Years.FirstOrDefault();
                if (selectedTab != null)
                    selectedTab.BuildControls();
                xtraTabControl.SelectedTabPage = selectedTab;

                _buildInProgress = false;
            }
            this.ViewBuilded = true;
        }

        public void RefreshColors()
        {
            foreach (YearControl year in this.Years)
                year.RefreshColors();
        }

        public void RefreshFont()
        {
            foreach (YearControl year in this.Years)
                year.RefreshFont();
        }

        private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (!_buildInProgress)
            {
                YearControl selectedYear = e.Page as YearControl;
                if (selectedYear != null && !selectedYear.ViewBuilded)
                {
                    ConfigurationClasses.SettingsManager.Instance.SelectedCalendarYear = selectedYear.Data.Year;
                    ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        formProgress.TopMost = true;
                        formProgress.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                selectedYear.BuildControls();
                            });
                        }));
                        formProgress.Show();
                        System.Windows.Forms.Application.DoEvents();
                        thread.Start();
                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();
                        formProgress.Close();
                        FormMain.Instance.ribbonControl.Enabled = true;
                    }
                }
            }
        }
    }
}
