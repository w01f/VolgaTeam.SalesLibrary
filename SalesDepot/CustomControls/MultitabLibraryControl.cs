using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MultitabLibraryControl : UserControl
    {
        private List<BusinessClasses.PageDecorator> _pages = new List<BusinessClasses.PageDecorator>();

        public MultitabLibraryControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Resize += new System.EventHandler(MultitabLibraryControl_Resize);
        }

        private void FillPages()
        {
            pnEmpty.BringToFront();
            xtraTabControl.SelectedPageChanged -= new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl_SelectedPageChanged);
            xtraTabControl.TabPages.Clear();
            xtraTabControl.TabPages.AddRange(_pages.Select(x => x.TabPage).ToArray());
            xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl_SelectedPageChanged);
            DevExpress.XtraTab.XtraTabPage selectedPage = xtraTabControl.TabPages.Where(x => x.Text.Equals(ConfigurationClasses.SettingsManager.Instance.SelectedPage.Replace("&","&&"))).FirstOrDefault();
            if (selectedPage != null)
                xtraTabControl.SelectedTabPage = selectedPage;
            pnEmpty.SendToBack();
        }

        private void MultitabLibraryControl_Resize(object sender, System.EventArgs e)
        {
            FillPages();
        }

        void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page != null)
            {
                BusinessClasses.PageDecorator pageDecorator = e.Page.Tag as BusinessClasses.PageDecorator;
                if (pageDecorator != null)
                {
                    pageDecorator.ApplyPageLogo();
                    ConfigurationClasses.SettingsManager.Instance.SelectedPage = pageDecorator.Page.Name;
                    ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                }
            }
        }

        public void AddPages(BusinessClasses.PageDecorator[] pages)
        {
            _pages.Clear();
            _pages.AddRange(pages);
            FillPages();
        }
    }
}
