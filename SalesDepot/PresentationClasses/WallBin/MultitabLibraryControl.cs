﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MultitabLibraryControl : UserControl
    {
        private List<Decorators.PageDecorator> _pages = new List<Decorators.PageDecorator>();

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
            foreach (Decorators.PageDecorator page in _pages)
                page.FitPage();
            DevExpress.XtraTab.XtraTabPage selectedPage = xtraTabControl.TabPages.Where(x => x.Text.Equals(ConfigurationClasses.SettingsManager.Instance.SelectedPage.Replace("&", "&&"))).FirstOrDefault();
            if (selectedPage != null)
                xtraTabControl.SelectedTabPage = selectedPage;
            xtraTabControl_SelectedPageChanged(null, new DevExpress.XtraTab.TabPageChangedEventArgs(null, xtraTabControl.SelectedTabPage));
            xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl_SelectedPageChanged);
            pnEmpty.SendToBack();
        }

        private void MultitabLibraryControl_Resize(object sender, System.EventArgs e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                Decorators.PageDecorator pageDecorator = xtraTabControl.SelectedTabPage.Tag as Decorators.PageDecorator;
                if (pageDecorator != null)
                    pageDecorator.FitPage();
            }
        }

        void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page != null)
            {
                Decorators.PageDecorator pageDecorator = e.Page.Tag as Decorators.PageDecorator;
                if (pageDecorator != null)
                {
                    pageDecorator.ApplyPageLogo();
                    pageDecorator.FitColumnsToPage();
                    pageDecorator.UpdatePageContent();
                    ConfigurationClasses.SettingsManager.Instance.SelectedPage = pageDecorator.Page.Name;
                    ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                }
            }
        }

        public void AddPages(Decorators.PageDecorator[] pages)
        {
            _pages.Clear();
            _pages.AddRange(pages);
            FillPages();
        }
    }
}
