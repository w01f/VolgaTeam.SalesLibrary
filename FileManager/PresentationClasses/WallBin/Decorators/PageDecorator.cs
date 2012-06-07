using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.WallBin.Decorators
{
    public class PageDecorator
    {
        public BusinessClasses.LibraryPage Page { get; private set; }
        private List<PresentationClasses.WallBin.FolderBoxControl> _boxes = new List<PresentationClasses.WallBin.FolderBoxControl>();
        private Panel _headerPanel = null;
        private Panel _parentPanel = null;
        public DevExpress.XtraEditors.XtraScrollableControl Container { get; private set; }
        public DevExpress.XtraTab.XtraTabPage TabPage { get; private set; }
        private Timer _scrooTimer = new Timer();
        private bool _scroolDown = false;
        public LibraryDecorator Parent { get; set; }
        public PresentationClasses.WallBin.FolderBoxControl ActiveBox { get; set; }

        public PageDecorator(BusinessClasses.LibraryPage page)
        {
            this.Page = page;
            this.Container = new DevExpress.XtraEditors.XtraScrollableControl();
            this.TabPage = new DevExpress.XtraTab.XtraTabPage();
            this.TabPage.Tag = this;
            this.TabPage.Text = page.Name.Replace("&", "&&");
            _scrooTimer.Stop();
            _scrooTimer.Interval = 5;
            _scrooTimer.Tick += new EventHandler(scrooTimer_Tick);

            BuildPage();
            BuildDisplayBoxes();
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
                box.Decorator = this;
                _boxes.Add(box);
                Application.DoEvents();
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
            _headerPanel = new Panel();
            _headerPanel.BorderStyle = BorderStyle.None;
            _headerPanel.Height = 0;
            _headerPanel.Dock = DockStyle.Top;
            this.Container.Controls.Add(_headerPanel);
            _headerPanel.BringToFront();
            _headerPanel.Resize += new EventHandler(ColumnTitles_Resize);

            if (this.Page.EnableColumnTitles)
            {
                foreach (BusinessClasses.ColumnTitle columnTitle in this.Page.ColumnTitles)
                {
                    PresentationClasses.WallBin.ColumnTitleControl columnTitleControl = new PresentationClasses.WallBin.ColumnTitleControl(columnTitle);
                    columnTitleControl.Dock = columnTitle.ColumnOrder == 2 ? DockStyle.Fill : DockStyle.Left;
                    _headerPanel.Controls.Add(columnTitleControl);
                    columnTitleControl.BringToFront();
                    Application.DoEvents();
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

            PresentationClasses.WallBin.ColumnPanel panel = new PresentationClasses.WallBin.ColumnPanel();
            panel.BorderStyle = BorderStyle.None;
            panel.AllowDrop = true;
            panel.Dock = DockStyle.Left;
            panel.Order = 0;
            panel.MouseMove += new MouseEventHandler((sender, e) => { ((Control)sender).Parent.Parent.Focus(); });
            panel.DragEnter += new DragEventHandler(Column_DragEnter);
            panel.DragOver += new DragEventHandler(Column_DragOver);
            panel.DragLeave += new EventHandler(Column_DragLeave);
            panel.DragDrop += new DragEventHandler(Column_DragDrop);
            _parentPanel.Controls.Add(panel);
            panel.BringToFront();

            panel = new PresentationClasses.WallBin.ColumnPanel();
            panel.BorderStyle = BorderStyle.None;
            panel.AllowDrop = true;
            panel.Dock = DockStyle.Left;
            panel.Order = 1;
            panel.MouseMove += new MouseEventHandler((sender, e) => { ((Control)sender).Parent.Parent.Focus(); });
            panel.DragEnter += new DragEventHandler(Column_DragEnter);
            panel.DragOver += new DragEventHandler(Column_DragOver);
            panel.DragLeave += new EventHandler(Column_DragLeave);
            panel.DragDrop += new DragEventHandler(Column_DragDrop);
            _parentPanel.Controls.Add(panel);
            panel.BringToFront();

            panel = new PresentationClasses.WallBin.ColumnPanel();
            panel.BorderStyle = BorderStyle.None;
            panel.AllowDrop = true;
            panel.Dock = DockStyle.Fill;
            panel.Order = 2;
            panel.MouseMove += new MouseEventHandler((sender, e) => { ((Control)sender).Parent.Parent.Focus(); });
            panel.DragEnter += new DragEventHandler(Column_DragEnter);
            panel.DragOver += new DragEventHandler(Column_DragOver);
            panel.DragLeave += new EventHandler(Column_DragLeave);
            panel.DragDrop += new DragEventHandler(Column_DragDrop);
            _parentPanel.Controls.Add(panel);
            panel.BringToFront();

            Application.DoEvents();
        }

        private void LinkBoxesToColumns()
        {
            _boxes.Sort((x, y) => x.Column.CompareTo(y.Column) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.Column.CompareTo(y.Column));
            foreach (Control control in _parentPanel.Controls)
                control.Controls.Clear();
            foreach (PresentationClasses.WallBin.FolderBoxControl box in _boxes)
                _parentPanel.Controls[2 - box.Column].Controls.Add(box);
        }

        private void WallBin_Resize(object sender, EventArgs e)
        {
            FitObjectsToPage();
        }

        private void ColumnTitles_Resize(object sender, EventArgs e)
        {
            FitColumnsToPage();
        }

        private void ReorderBoxes()
        {
            int boxOrder = -1;
            int currentColumn = -1;

            _boxes.Sort((x, y) => x.Column.CompareTo(y.Column) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.Column.CompareTo(y.Column));
            for (int i = 0; i < _boxes.Count; i++)
            {
                if (currentColumn != _boxes[i].Column)
                {
                    currentColumn = _boxes[i].Column;
                    boxOrder = 0;
                }
                _boxes[i].RowOrder = boxOrder;
                boxOrder++;
            }
        }

        public void RefreshPanelHeight()
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

        private void FitColumnsToPage()
        {
            int panelWidth = _headerPanel.Width / 3;
            int panelHeight = 0;
            foreach (Control panel in _headerPanel.Controls)
            {
                panel.Width = panelWidth;
                int controlHeight = (panel as ColumnTitleControl).GetHeight();
                if (panelHeight < controlHeight)
                    panelHeight = controlHeight;
            }
            _headerPanel.Height = panelHeight;
        }

        public void FitObjectsToPage()
        {
            foreach (PresentationClasses.WallBin.FolderBoxControl box in _boxes)
                box.SetGridFont(ConfigurationClasses.SettingsManager.Instance.FontSize);


            RefreshPanelHeight();

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
        }

        public void FitPage()
        {
            LinkBoxesToColumns();
            FitColumnsToPage();
            FitObjectsToPage();
        }

        public void Apply()
        {
            this.Container.Parent = null;
            FormMain.Instance.TabHome.pnMain.Controls.Add(this.Container);
            FitPage();
        }

        public void Save()
        {
            foreach (PresentationClasses.WallBin.FolderBoxControl box in _boxes)
                box.Save();
        }

        #region Drag&Drop Event Handlers
        public void Column_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Serializable, true))
            {
                var data = e.Data.GetData(DataFormats.Serializable, true);
                if (data.GetType() == typeof(PresentationClasses.WallBin.FolderBoxControl))
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        public void Column_DragOver(object sender, DragEventArgs e)
        {
            Point point = this.Container.PointToClient(new Point(e.X, e.Y));

            if (point.Y < this.Container.Bottom && point.Y > this.Container.Bottom - 50)
            {
                _scroolDown = true;
                _scrooTimer.Start();
            }
            else if (point.Y > this.Container.Top && point.Y < this.Container.Top + 50)
            {
                _scroolDown = false;
                _scrooTimer.Start();
            }
            else
                _scrooTimer.Stop();
            if (e.Data.GetDataPresent(DataFormats.Serializable, true))
            {
                var data = e.Data.GetData(DataFormats.Serializable, true);
                if (data.GetType() == typeof(PresentationClasses.WallBin.FolderBoxControl))
                {
                    PresentationClasses.WallBin.ColumnPanel panel = (PresentationClasses.WallBin.ColumnPanel)sender;
                    Point pt = panel.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                    int bottomBoxBorder = 0;
                    foreach (Control control in panel.Controls)
                    {
                        if (control.Top <= pt.Y && control.Bottom >= pt.Y && control != data)
                        {
                            ((PresentationClasses.WallBin.FolderBoxControl)control).UnderlineBox = true;
                            if (panel.DropHintLineHeight != control.Top)
                            {
                                panel.DropHintLineHeight = control.Top;
                                panel.Refresh();
                            }
                        }
                        else
                            ((PresentationClasses.WallBin.FolderBoxControl)control).UnderlineBox = false;

                        if (bottomBoxBorder < control.Bottom)
                            bottomBoxBorder = control.Bottom;
                    }
                    if (bottomBoxBorder < pt.Y)
                    {
                        if (panel.Controls.Count > 0)
                            if (panel.Controls[panel.Controls.Count - 1] == data)
                                return;
                        if (panel.DropHintLineHeight != bottomBoxBorder)
                        {
                            panel.DropHintLineHeight = bottomBoxBorder;
                            panel.Refresh();
                        }
                    }
                }
            }
        }

        private void Column_DragLeave(object sender, EventArgs e)
        {
            _scrooTimer.Stop();
            ((PresentationClasses.WallBin.ColumnPanel)sender).DropHintLineHeight = -1;
            foreach (Control control in ((PresentationClasses.WallBin.ColumnPanel)sender).Controls)
                ((PresentationClasses.WallBin.FolderBoxControl)control).UnderlineBox = false;
            ((PresentationClasses.WallBin.ColumnPanel)sender).Refresh();
        }

        public void Column_DragDrop(object sender, DragEventArgs e)
        {
            _scrooTimer.Stop();
            double orderInColumn = -1;
            PresentationClasses.WallBin.ColumnPanel panel = (PresentationClasses.WallBin.ColumnPanel)sender;
            panel.DropHintLineHeight = -1;
            Point pt = panel.PointToClient(new Point(e.X + 1, e.Y + 1));
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                ((PresentationClasses.WallBin.FolderBoxControl)panel.Controls[i]).UnderlineBox = false;
                if (panel.Controls[i].Top <= pt.Y && panel.Controls[i].Bottom >= pt.Y)
                    orderInColumn = i;
            }
            if (orderInColumn == -1)
                orderInColumn = panel.Controls.Count;

            if (e.Data.GetDataPresent(DataFormats.Serializable, true))
            {
                var data = e.Data.GetData(DataFormats.Serializable, true);
                if (data.GetType() == typeof(PresentationClasses.WallBin.FolderBoxControl))
                {
                    PresentationClasses.WallBin.FolderBoxControl box = (PresentationClasses.WallBin.FolderBoxControl)data;
                    box.Column = panel.Order;
                    box.RowOrder = orderInColumn - 0.5;
                    this.ReorderBoxes();
                    this.LinkBoxesToColumns();
                    this.FitObjectsToPage();
                    panel.Refresh();
                    this.Parent.StateChanged = true;
                }
            }
        }

        private void scrooTimer_Tick(object sender, EventArgs e)
        {
            if (_scroolDown)
            {
                if (this.Container.VerticalScroll.Value < this.Container.VerticalScroll.Maximum - 10)
                    this.Container.VerticalScroll.Value += 10;
                else
                    this.Container.VerticalScroll.Value = this.Container.VerticalScroll.Maximum;
            }
            else
            {
                if (this.Container.VerticalScroll.Value > this.Container.VerticalScroll.Minimum + 10)
                    this.Container.VerticalScroll.Value -= 10;
                else
                    this.Container.VerticalScroll.Value = this.Container.VerticalScroll.Minimum;
            }
        }
        #endregion
    }
}
