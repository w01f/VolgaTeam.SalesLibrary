using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.BusinessClasses
{
    public class PageDecorator
    {
        private List<CustomControls.FolderBoxControl> _boxes = new List<CustomControls.FolderBoxControl>();
        private Panel _headerPanel = null;
        private Panel _parentPanel = null;
        private Image _logo = null;
        public LibraryPage Page { get; private set; }
        public DevExpress.XtraEditors.XtraScrollableControl Container { get; private set; }
        public DevExpress.XtraTab.XtraTabPage TabPage { get; private set; }
        public LibraryDecorator Parent { get; private set; }
        public CustomControls.FolderBoxControl ActiveBox { get; set; }

        public PageDecorator(LibraryDecorator parent,LibraryPage page)
        {
            this.Page = page;
            this.Parent = parent;
            this.Container = new DevExpress.XtraEditors.XtraScrollableControl();
            this.TabPage = new DevExpress.XtraTab.XtraTabPage();
            this.TabPage.Tag = this;
            this.TabPage.Text = page.Name.Replace("&","&&");

            GetPageLogo();
            BuildPage();
            BuildDisplayBoxes();
        }

        private void GetPageLogo()
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.LibraryLogoFolder, this.Parent.Parent.Package.Folder.Name));
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles(string.Format(ConfigurationClasses.SettingsManager.PageLogoFileTemplate,(this.Page.Order + 1).ToString()));
                if (files.Length > 0)
                    _logo = new Bitmap(files[0].FullName);
            }
            if (_logo == null)
                _logo = this.Parent.Parent.Logo;
        }

        private void BuildDisplayBoxes()
        {
            foreach (CustomControls.FolderBoxControl box in _boxes)
                box.Dispose();
            _boxes.Clear();

            this.Page.Folders.Sort((x, y) => x.ColumnOrder.CompareTo(y.ColumnOrder) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.ColumnOrder.CompareTo(y.ColumnOrder));
            foreach (LibraryFolder folder in this.Page.Folders)
            {
                CustomControls.FolderBoxControl box = new CustomControls.FolderBoxControl();
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
                _headerPanel.Resize += new EventHandler(ColumnTitles_Resize);

                foreach (ColumnTitle columnTitle in this.Page.ColumnTitles)
                {
                    CustomControls.ColumnTitleControl columnTitleControl = new CustomControls.ColumnTitleControl();
                    columnTitleControl.laColumnTitle.Text = columnTitle.Name;
                    columnTitleControl.laColumnTitle.Font = columnTitle.HeaderFont;
                    columnTitleControl.laColumnTitle.BackColor = columnTitle.BackgroundColor;
                    columnTitleControl.laColumnTitle.ForeColor = columnTitle.ForeColor;
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
            _parentPanel.Resize += new EventHandler(WallBin_Resize);

            CustomControls.ColumnPanel panel = new CustomControls.ColumnPanel();
            panel.AllowDrop = true;
            panel.Dock = DockStyle.Left;
            panel.Order = 0;
            _parentPanel.Controls.Add(panel);
            panel.BringToFront();

            panel = new CustomControls.ColumnPanel();
            panel.AllowDrop = true;
            panel.Dock = DockStyle.Left;
            panel.Order = 1;
            _parentPanel.Controls.Add(panel);
            panel.BringToFront();

            panel = new CustomControls.ColumnPanel();
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
            foreach (CustomControls.FolderBoxControl box in _boxes)
            {
                _parentPanel.Controls[2 - box.Column].Controls.Add(box);
            }
        }

        private void WallBin_Resize(object sender, EventArgs e)
        {
            FitObjectsToPage();
        }

        private void ColumnTitles_Resize(object sender, EventArgs e)
        {
            FitColumnsToPage();
        }

        private void RefreshPanelHeight()
        {
            _parentPanel.Resize -= new EventHandler(WallBin_Resize);
            int maxHeight = this.Container.Height - (_headerPanel != null ? _headerPanel.Height : 0);

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

            if (realHeight < maxHeight)
                realHeight = maxHeight;

            _parentPanel.Height = realHeight;
            _parentPanel.Resize += new EventHandler(WallBin_Resize);
        }

        private void UpdateView()
        {
            foreach (Control control in _parentPanel.Controls)
            {
                CustomControls.ColumnPanel panel = control as CustomControls.ColumnPanel;
                panel.Visible = panel.Order == 2 || ConfigurationClasses.SettingsManager.Instance.ClassicView;
            }
            foreach (CustomControls.FolderBoxControl box in _boxes)
                box.UpdateView();
            if (_headerPanel != null)
                _headerPanel.Visible = ConfigurationClasses.SettingsManager.Instance.ClassicView;
        }

        private void FitColumnsToPage()
        {
            if (_headerPanel != null)
            {
                int panelWidth = _headerPanel.Width / 3;
                int panelHeight = 0;
                foreach (Control panel in _headerPanel.Controls)
                {
                    panel.Width = panelWidth;
                    int controlHeight = (panel as CustomControls.ColumnTitleControl).GetHeight();
                    if (panelHeight < controlHeight)
                        panelHeight = controlHeight;
                }
                _headerPanel.Height = panelHeight;
            }
        }

        public void FitObjectsToPage()
        {
            foreach (CustomControls.FolderBoxControl box in _boxes)
                box.SetGridFont(ConfigurationClasses.SettingsManager.Instance.FontSize);

            UpdateView();

            foreach (Control panel in _parentPanel.Controls)
            {
                ((CustomControls.ColumnPanel)panel).ResizePanel();
                if (((CustomControls.ColumnPanel)panel).Order < 2)
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
            this.Container.Parent = null;
            FormMain.Instance.ClassicViewControl.pnSalesDepotContainer.Controls.Add(this.Container);
            FitPage();
            ApplyPageLogo();
        }
    }
}
