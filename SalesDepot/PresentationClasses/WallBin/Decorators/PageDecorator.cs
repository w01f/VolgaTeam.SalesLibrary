using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin.Decorators
{
    public class PageDecorator
    {
        private List<PresentationClasses.WallBin.FolderBoxControl> _boxes = new List<PresentationClasses.WallBin.FolderBoxControl>();
        private Panel _headerPanel = null;
        private Panel _parentPanel = null;
        private Image _logo = null;
        public BusinessClasses.LibraryPage Page { get; private set; }
        public DevExpress.XtraEditors.XtraScrollableControl Container { get; private set; }
        public DevExpress.XtraTab.XtraTabPage TabPage { get; private set; }
        public LibraryDecorator Parent { get; private set; }
        public PresentationClasses.WallBin.FolderBoxControl ActiveBox { get; set; }

        public PageDecorator(LibraryDecorator parent, BusinessClasses.LibraryPage page)
        {
            this.Page = page;
            this.Parent = parent;
            this.Container = new DevExpress.XtraEditors.XtraScrollableControl();
            this.Container.Resize += new EventHandler(WallBin_Resize);
            this.TabPage = new DevExpress.XtraTab.XtraTabPage();
            this.TabPage.Tag = this;
            this.TabPage.Text = page.Name.Replace("&", "&&");

            GetPageLogo();
            BuildPage();
            BuildDisplayBoxes();
        }

        private void GetPageLogo()
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.LibraryLogoFolder, this.Parent.Parent.Package.Folder.Name));
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles(string.Format(ConfigurationClasses.SettingsManager.PageLogoFileTemplate, (this.Page.Order + 1).ToString()));
                if (files.Length > 0)
                    _logo = new Bitmap(files[0].FullName);
            }
            if (_logo == null)
                _logo = this.Parent.Parent.Logo;
        }

        private void BuildDisplayBoxes()
        {
            foreach (PresentationClasses.WallBin.FolderBoxControl box in _boxes)
                box.Dispose();
            _boxes.Clear();

            this.Page.Folders.Sort((x, y) => x.ColumnOrder.CompareTo(y.ColumnOrder) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.ColumnOrder.CompareTo(y.ColumnOrder));
            foreach (BusinessClasses.LibraryFolder folder in this.Page.Folders)
            {
                PresentationClasses.WallBin.FolderBoxControl box = new PresentationClasses.WallBin.FolderBoxControl();
                box.Folder = folder;
                _boxes.Add(box);
            }
        }

        private void BuildPage()
        {
            this.Container.Dock = DockStyle.Fill;
            this.Container.AlwaysScrollActiveControlIntoView = false;
            BuildColumnTitles();
            BuildColumns();
        }

        private void BuildColumnTitles()
        {
            if (this.Page.EnableColumnTitles)
            {
                _headerPanel = new Panel();
                _headerPanel.BorderStyle = BorderStyle.None;
                _headerPanel.Height = 0;
                _headerPanel.Dock = DockStyle.Top;
                this.Container.Controls.Add(_headerPanel);
                _headerPanel.BringToFront();

                foreach (BusinessClasses.ColumnTitle columnTitle in this.Page.ColumnTitles)
                {
                    PresentationClasses.WallBin.ColumnTitleControl columnTitleControl = new PresentationClasses.WallBin.ColumnTitleControl(columnTitle);
                    columnTitleControl.Dock = columnTitle.ColumnOrder == 2 ? DockStyle.Fill : DockStyle.Left;
                    _headerPanel.Controls.Add(columnTitleControl);
                    columnTitleControl.BringToFront();
                }
            }
        }

        private void BuildColumns()
        {
            _parentPanel = new Panel();
            _parentPanel.BorderStyle = BorderStyle.None;
            _parentPanel.Height = this.Container.Height;
            _parentPanel.Dock = DockStyle.Top;
            this.Container.Controls.Add(_parentPanel);
            _parentPanel.BringToFront();

            PresentationClasses.WallBin.ColumnPanel panel = new PresentationClasses.WallBin.ColumnPanel();
            panel.AllowDrop = true;
            panel.Dock = DockStyle.Left;
            panel.Order = 0;
            _parentPanel.Controls.Add(panel);
            panel.BringToFront();

            panel = new PresentationClasses.WallBin.ColumnPanel();
            panel.AllowDrop = true;
            panel.Dock = DockStyle.Left;
            panel.Order = 1;
            _parentPanel.Controls.Add(panel);
            panel.BringToFront();

            panel = new PresentationClasses.WallBin.ColumnPanel();
            panel.AllowDrop = true;
            panel.Dock = DockStyle.Fill;
            panel.Order = 2;
            _parentPanel.Controls.Add(panel);
            panel.BringToFront();
        }

        public void LinkBoxesToColumns()
        {
            _boxes.Sort((x, y) => x.Column.CompareTo(y.Column) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.Column.CompareTo(y.Column));
            foreach (Control control in _parentPanel.Controls)
                control.Controls.Clear();
            foreach (PresentationClasses.WallBin.FolderBoxControl box in _boxes)
            {
                _parentPanel.Controls[2 - box.Column].Controls.Add(box);
            }
        }

        private void WallBin_Resize(object sender, EventArgs e)
        {
            this.Parent.EmptyPanel.BringToFront();
            FitColumnsToPage();
            FitObjectsToPage();
            this.Parent.EmptyPanel.SendToBack();
        }

        private void RefreshPanelHeight()
        {
            int realHeight = 0;
            for (int i = 0; i < 3; i++)
            {
                int columnHeight = 0;
                foreach (var box in _boxes)
                    if (box.Column == i)
                        columnHeight += box.Height;
                if (realHeight < columnHeight)
                    realHeight = columnHeight;
            }
            _parentPanel.Height = realHeight;
        }

        private void UpdateView()
        {
            foreach (Control control in _parentPanel.Controls)
            {
                PresentationClasses.WallBin.ColumnPanel panel = control as PresentationClasses.WallBin.ColumnPanel;
                panel.Visible = panel.Order == 2 || ConfigurationClasses.SettingsManager.Instance.ClassicView;
            }
            foreach (PresentationClasses.WallBin.FolderBoxControl box in _boxes)
                box.UpdateView();
            if (_headerPanel != null)
                _headerPanel.Visible = ConfigurationClasses.SettingsManager.Instance.ClassicView;
        }

        public void FitColumnsToPage()
        {
            if (_headerPanel != null)
            {
                int panelWidth = _headerPanel.Width / 3;
                int panelHeight = 0;
                foreach (Control panel in _headerPanel.Controls)
                {
                    panel.Width = panelWidth;
                    int controlHeight = (panel as PresentationClasses.WallBin.ColumnTitleControl).GetHeight();
                    if (panelHeight < controlHeight)
                        panelHeight = controlHeight;
                }
                _headerPanel.Height = panelHeight;
            }
        }

        public void FitObjectsToPage()
        {
            foreach (PresentationClasses.WallBin.FolderBoxControl box in _boxes)
                box.SetGridFont(ConfigurationClasses.SettingsManager.Instance.FontSize);

            UpdateView();

            foreach (Control panel in _parentPanel.Controls)
            {
                ((PresentationClasses.WallBin.ColumnPanel)panel).ResizePanel();
                if (((PresentationClasses.WallBin.ColumnPanel)panel).Order < 2)
                {
                    panel.Dock = DockStyle.Left;
                }
                else
                {
                    panel.Dock = DockStyle.Fill;
                    panel.BringToFront();
                }
            }

            RefreshPanelHeight();
        }

        public void FitPage()
        {
            LinkBoxesToColumns();
            FitColumnsToPage();
            FitObjectsToPage();
        }

        public void ApplyPageLogo()
        {
            FormMain.Instance.labelItemPackageLogo.Image = _logo;
            FormMain.Instance.ribbonBarStations.RecalcLayout();
            FormMain.Instance.ribbonPanelHome.PerformLayout();
        }

        public void Apply()
        {
            FitPage();
            ApplyPageLogo();
        }
    }
}
