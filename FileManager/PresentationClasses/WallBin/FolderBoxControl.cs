﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace FileManager.PresentationClasses.WallBin
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class FolderBoxControl : UserControl
    {
        private const int imageWidthMargin = 6;
        private const int imageHeightMargin = 6;
        private const int defaultImageWidth = 26;
        private const int defaultImageHeight = 26;

        private BusinessClasses.LibraryFolder _folder;
        private Font _noteFont;
        private Font _textFont;
        private Pen _rowDropHintPen = new Pen(Color.Black, 2);
        private Pen _boxDropHintPen = new Pen(Color.Black, 8);
        private RichTextBox _richTextControl = new RichTextBox();
        private bool _containFiles = false;
        private bool _containsWidgets = false;
        private Font _displayCellFont;
        private Font _editCellFont;

        private DataGridView.HitTestInfo _hitTest;
        private Rectangle _dragBox;
        private bool _underlineRow = false;
        private bool _underlineBox = false;
        private int _currentDragOverRow = -1;

        #region Public Properties
        public Decorators.PageDecorator Decorator { get; set; }
        public bool IsActive { get; set; }

        public BusinessClasses.LibraryFolder Folder
        {
            set
            {
                _folder = value;

                laFolderName.Text = _folder.Name;

                laFolderName.Font = _folder.HeaderFont;

                laFolderName.Left = 0;
                laFolderName.BackColor = _folder.BackgroundHeaderColor;

                laFolderName.ForeColor = _folder.ForeHeaderColor;

                grFiles.BackgroundColor = _folder.BackgroundWindowColor;
                grFiles.DefaultCellStyle.BackColor = _folder.BackgroundWindowColor;
                grFiles.DefaultCellStyle.SelectionBackColor = this.IsActive ? Color.Wheat : _folder.BackgroundWindowColor;

                grFiles.DefaultCellStyle.ForeColor = _folder.ForeWindowColor;
                grFiles.DefaultCellStyle.SelectionForeColor = _folder.ForeWindowColor;

                Size labelSize = new Size(laFolderName.Width, Int32.MaxValue);
                laFolderName.Height = TextRenderer.MeasureText(laFolderName.Text, laFolderName.Font, labelSize, TextFormatFlags.WordBreak).Height + 10;

                UpdateDataSource();

                grFiles.Refresh();
            }
        }

        public int Column
        {
            get
            {
                return _folder.ColumnOrder;
            }
            set
            {
                _folder.ColumnOrder = value;
            }
        }

        public double RowOrder
        {
            get
            {
                return _folder.RowOrder;
            }
            set
            {
                _folder.RowOrder = value;
            }
        }

        public bool UnderlineBox
        {
            get
            {
                return _underlineBox;
            }
            set
            {
                _underlineBox = value;
                if (_underlineBox)
                    this.Padding = new Padding(0, 4, 0, 0);
                else
                    if (this.IsActive)
                        this.Padding = new Padding(2, 2, 2, 2);
                    else
                        this.Padding = new Padding(0, 0, 0, 0);
            }
        }
        #endregion

        public FolderBoxControl()
        {
            InitializeComponent();
        }

        #region Formatting Event Handlers
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.IsActive)
            {
                Rectangle rect;
                if (e.ClipRectangle.Top == 0)
                    rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, this.Height);
                else
                    rect = new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
                for (int i = 0; i < 3; i++)
                {
                    ControlPaint.DrawBorder(e.Graphics, rect, Color.Red, ButtonBorderStyle.Solid);
                    rect.X = rect.X + 1;
                    rect.Y = rect.Y + 1;
                    rect.Width = rect.Width - 2;
                    rect.Height = rect.Height - 2;
                }
            }
            if (_underlineBox)
                e.Graphics.DrawLine(_boxDropHintPen, 0, 0, this.Width, 0);
        }

        private void grFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grFiles.Rows[e.RowIndex].Tag != null)
            {
                BusinessClasses.LibraryFile file = grFiles.Rows[e.RowIndex].Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    List<string> toolTipText = new List<string>();
                    if (!string.IsNullOrEmpty(file.FullPath))
                    {
                        toolTipText.Add("Path: " + file.FullPath);
                        if (file.PresentationProperties != null)
                            toolTipText.Add(string.Format("Slide Size: {0} W = {1} H = {2}", new object[] { file.PresentationProperties.Orientation, file.PresentationProperties.Width.ToString("#.##"), file.PresentationProperties.Height.ToString("#.##") }));
                        toolTipText.Add("Added: " + file.AddDate.ToString("M/dd/yy h:mm:ss tt"));
                        if (file.ExpirationDateOptions.EnableExpirationDate && file.ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
                            toolTipText.Add("Expires: " + file.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt"));
                        else
                            toolTipText.Add("Expires: No Expiration Date");
                        if (!string.IsNullOrEmpty(file.SearchTags.AllTags))
                            toolTipText.Add("Search Tags: " + file.SearchTags.AllTags);
                        else
                            toolTipText.Add("No Search Tags Assigned");
                    }
                    else if (file.Type == BusinessClasses.FileTypes.LineBreak)
                    {
                        if (!string.IsNullOrEmpty(file.LineBreakProperties.Note))
                            toolTipText.Add(file.LineBreakProperties.Note);
                    }
                    grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = string.Join(Environment.NewLine, toolTipText.ToArray());
                }
            }
        }

        void grFiles_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                BusinessClasses.LibraryFile file = grFiles.Rows[e.RowIndex].Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    e.PaintBackground(e.CellBounds, true);
                    _richTextControl.Text = string.Empty;
                    _richTextControl.Font = file.Type == BusinessClasses.FileTypes.LineBreak ? file.LineBreakProperties.Font : (file.DisplayAsBold ? _noteFont : _textFont);
                    if (!string.IsNullOrEmpty(file.Name))
                    {
                        _richTextControl.Text = file.DisplayName + file.Note;
                        Size textSize = new Size(Int32.MaxValue, Int32.MaxValue);
                        textSize = TextRenderer.MeasureText(_richTextControl.Text, file.Type == BusinessClasses.FileTypes.LineBreak ? file.LineBreakProperties.Font : _noteFont, textSize, TextFormatFlags.SingleLine);
                        int columnWidth = textSize.Width + (_containsWidgets && (file.Type != BusinessClasses.FileTypes.LineBreak || file.Widget != null) ? ((file.Widget == null ? defaultImageWidth : file.Widget.Width) + imageWidthMargin) : 0);
                        if (columnWidth > colDisplayName.Width)
                        {
                            SetGridSize();
                            return;
                        }
                        _richTextControl.Height = textSize.Height;
                        _richTextControl.Width = textSize.Width + 10;

                        if (!string.IsNullOrEmpty(file.Note))
                        {
                            _richTextControl.SelectionStart = file.DisplayName.Length;
                            _richTextControl.SelectionLength = file.Note.Length;
                            _richTextControl.SelectionFont = _noteFont;
                        }
                        if (grFiles.SelectedRows.Count > 0)
                        {
                            if (grFiles.SelectedRows[0].Index == e.RowIndex)
                            {
                                _richTextControl.BackColor = grFiles.DefaultCellStyle.SelectionBackColor;
                                _richTextControl.ForeColor = file.Type == BusinessClasses.FileTypes.LineBreak ? file.LineBreakProperties.ForeColor : grFiles.DefaultCellStyle.SelectionForeColor;
                            }
                            else
                            {
                                _richTextControl.BackColor = grFiles.DefaultCellStyle.BackColor;
                                _richTextControl.ForeColor = file.Type == BusinessClasses.FileTypes.LineBreak ? file.LineBreakProperties.ForeColor : _richTextControl.ForeColor = grFiles.DefaultCellStyle.ForeColor;
                            }
                        }
                        else
                        {
                            _richTextControl.BackColor = grFiles.DefaultCellStyle.BackColor;
                            _richTextControl.ForeColor = file.Type == BusinessClasses.FileTypes.LineBreak ? file.LineBreakProperties.ForeColor : _richTextControl.ForeColor = grFiles.DefaultCellStyle.ForeColor;
                        }
                    }

                    if (file.EnableBanner && file.Banner != null)
                    {
                        e.Graphics.DrawImage(file.Banner,
                        new Rectangle(e.CellBounds.X, e.CellBounds.Y + ((e.CellBounds.Height - file.Banner.Height) / 2), file.Banner.Width, file.Banner.Height));
                    }
                    else
                    {
                        if (file.Widget != null)
                        {
                            e.Graphics.DrawImage(file.Widget,
                            new Rectangle(e.CellBounds.X, e.CellBounds.Y + ((e.CellBounds.Height - file.Widget.Height) / 2), file.Widget.Width, file.Widget.Height));
                        }
                        int textLeftCoordinate = e.CellBounds.X + (file.Type == BusinessClasses.FileTypes.LineBreak ?
                                (file.Widget != null ? file.Widget.Width + imageWidthMargin : 0) : 
                                (_containsWidgets ? ((file.Widget == null ? defaultImageWidth : file.Widget.Width) + imageWidthMargin) : 0));
                        e.Graphics.DrawImage(RichTextBoxPrinter.Print(
                            _richTextControl, _richTextControl.Width, _richTextControl.Height),
                            new Rectangle(textLeftCoordinate, e.CellBounds.Y + ((e.CellBounds.Height - _richTextControl.Height) / 2), _richTextControl.Width, _richTextControl.Height));
                    }
                }
                if (_containFiles && _underlineRow && (_currentDragOverRow == e.RowIndex || _currentDragOverRow == -1))
                {
                    if (_currentDragOverRow == -1)
                        e.Graphics.DrawLine(_rowDropHintPen, 0, e.CellBounds.Height * grFiles.RowCount + 1, grFiles.Width, e.CellBounds.Height * grFiles.RowCount + 1);
                    else
                    {
                        if (e.RowIndex == 0)
                            e.Graphics.DrawLine(_rowDropHintPen, 0, e.CellBounds.Top + 2, grFiles.Width, e.CellBounds.Top + 2);
                        else
                            e.Graphics.DrawLine(_rowDropHintPen, 0, e.CellBounds.Top - 1, grFiles.Width, e.CellBounds.Top - 1);
                    }
                }
                e.Handled = true;
            }
        }

        #endregion

        #region Drag&Drop Event Handlers
        private void grFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Serializable, true))
            {
                var data = e.Data.GetData(DataFormats.Serializable, true);
                if (data.GetType() == typeof(DataGridViewRow))
                {
                    _underlineRow = true;
                    e.Effect = DragDropEffects.Move;
                }
                else if (data.GetType() != typeof(FolderBoxControl))
                {
                    _underlineRow = true;
                    e.Effect = DragDropEffects.Copy;
                }
                else
                    this.Decorator.Column_DragEnter(this.Parent, e);
            }
        }

        private void grFiles_DragOver(object sender, DragEventArgs e)
        {
            if (_underlineRow)
            {
                Point pt = grFiles.PointToClient(new Point(e.X + 1, e.Y + 1));
                DataGridView.HitTestInfo ht = grFiles.HitTest(pt.X, pt.Y);
                if (_currentDragOverRow != ht.RowIndex)
                {
                    _currentDragOverRow = ht.RowIndex;
                    grFiles.Refresh();
                }
            }
            else
                this.Decorator.Column_DragOver(this.Parent, e);
        }

        private void grFiles_DragLeave(object sender, EventArgs e)
        {
            _underlineRow = false;
            _currentDragOverRow = -1;
            grFiles.Refresh();
        }

        private void grFiles_DragDrop(object sender, DragEventArgs e)
        {
            Point p = grFiles.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo ht = grFiles.HitTest(p.X, p.Y);
            if (e.Data.GetDataPresent(DataFormats.Serializable, true))
            {
                var data = e.Data.GetData(DataFormats.Serializable, true);
                if (data != null)
                {
                    if (!(data.GetType() == typeof(FolderBoxControl)))
                    {
                        MakeActive();
                        if (data.GetType() == typeof(DataGridViewRow))
                            MoveFile((DataGridViewRow)data, ht.RowIndex);
                        else
                        {
                            List<FileInfo> files = new List<FileInfo>();
                            List<DirectoryInfo> folders = new List<DirectoryInfo>();
                            foreach (object dragItem in (object[])data)
                                if (dragItem != null)
                                {
                                    if (dragItem.GetType() == typeof(FileInfo))
                                        files.Add((FileInfo)dragItem);
                                    else if (dragItem.GetType() == typeof(DirectoryInfo))
                                        folders.Add((DirectoryInfo)dragItem);
                                }
                            folders.Sort((x, y) => x.Name.CompareTo(y.Name));
                            files.Sort((x, y) => x.Name.CompareTo(y.Name));
                            foreach (DirectoryInfo folder in folders)
                                AddFolder(folder, ht.RowIndex);
                            foreach (FileInfo file in files)
                                AddFile(file, ht.RowIndex);
                        }
                        _containFiles = true;
                        SetGridFont(ConfigurationClasses.SettingsManager.Instance.FontSize);
                        SetGridSize();
                        if (this.Parent != null)
                        {
                            ((ColumnPanel)this.Parent).ResizePanel();
                            this.Decorator.RefreshPanelHeight();
                        }
                        this.Decorator.Parent.StateChanged = true;
                    }
                    else
                        this.Decorator.Column_DragDrop(this.Parent, e);
                }
            }
            grFiles_DragLeave(null, null);
        }

        private void grFiles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            _hitTest = grFiles.HitTest(e.X, e.Y);
            if (_hitTest.Type == DataGridViewHitTestType.Cell && _containFiles)
            {
                _dragBox = new Rectangle(new Point(e.X - (SystemInformation.DragSize.Width / 2), e.Y - (SystemInformation.DragSize.Height / 2)),
                    SystemInformation.DragSize);
            }
            else
                _hitTest = DataGridView.HitTestInfo.Nowhere;
        }

        void grFiles_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_containFiles)
                this.Cursor = Cursors.Hand;
            if (((e.Button & MouseButtons.Left) != MouseButtons.Left)
                || _hitTest == DataGridView.HitTestInfo.Nowhere
                || _dragBox.Contains(e.X, e.Y))
                return;

            object dragData = null;
            if (grFiles.SelectedRows.Count > 0)
                dragData = grFiles.SelectedRows[0];
            if (dragData != null)
                grFiles.DoDragDrop(new DataObject(DataFormats.Serializable, (object)dragData), DragDropEffects.Move);
            _hitTest = DataGridView.HitTestInfo.Nowhere;
        }

        private void laFolderName_MouseDown(object sender, MouseEventArgs e)
        {
            _dragBox = new Rectangle(new Point(e.X - (SystemInformation.DragSize.Width / 2), e.Y - (SystemInformation.DragSize.Height / 2)),
                SystemInformation.DragSize);
        }

        private void laFolderName_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            if (((e.Button & MouseButtons.Left) != MouseButtons.Left)
                || _dragBox.Contains(e.X, e.Y))
                return;
            grFiles.DoDragDrop(new DataObject(DataFormats.Serializable, (object)this), DragDropEffects.Move);
        }
        #endregion

        #region Other GUI Routines
        private void FileBoxControl_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void laFolderName_Click(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                MakeActive();
                laFolderName.Focus();
            }
        }

        private void grFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsActive)
            {
                MakeActive();
                laFolderName.Focus();
            }
            else
            {
                DataGridView.HitTestInfo hitTest = grFiles.HitTest(e.X, e.Y);
                if (hitTest.Type != DataGridViewHitTestType.Cell)
                    laFolderName.Focus();
            }
        }

        private void grFiles_Click(object sender, EventArgs e)
        {
        }

        private void grFiles_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void grWindowFiles_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
            if (e.ColumnIndex == 0)
            {
                BusinessClasses.LibraryFile file = grFiles.Rows[e.RowIndex].Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    if (file.EnableBanner)
                        return;
                    _displayCellFont = grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font;
                    grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = file.Type == BusinessClasses.FileTypes.LineBreak ? _noteFont : _editCellFont;
                    grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = file.Name;
                    e.Cancel = false;
                }
            }
        }

        private void grWindowFiles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                BusinessClasses.LibraryFile file = grFiles.Rows[e.RowIndex].Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    if (file.EnableBanner)
                        return;
                    if (grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        if (!grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals(file.Name))
                        {
                            file.Name = grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            this.Decorator.Parent.StateChanged = true;
                        }
                    }
                    else
                    {
                        file.Name = string.Empty;
                        this.Decorator.Parent.StateChanged = true;
                    }
                    grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = file.DisplayName + file.Note;
                    grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = _displayCellFont;
                    SetGridSize();
                }
            }
        }

        private void grFiles_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.IsActive)
            {
                grFiles.Rows[e.RowIndex].Selected = true;
                ShowLinkProperties(e.Location);
            }
        }

        private void grFiles_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsStatus();
        }

        private void grFiles_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateAfterDelete();
        }
        #endregion

        #region Button Click's Methods
        public void AddUrl()
        {
            using (ToolForms.WallBin.FormAddUrl form = new ToolForms.WallBin.FormAddUrl())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    BusinessClasses.LibraryFile file = new BusinessClasses.LibraryFile(_folder);
                    file.Name = form.LinkName;
                    file.RelativePath = form.LinkPath;
                    file.Type = BusinessClasses.FileTypes.Url;
                    if (grFiles.SelectedRows.Count > 0)
                    {
                        int rowIndex = grFiles.SelectedRows[0].Index;
                        grFiles.Rows.Insert(rowIndex, file.DisplayName + file.Note);
                        DataGridViewRow row = grFiles.Rows[rowIndex];
                        row.Tag = file;
                        grFiles.ClearSelection();
                    }
                    else
                    {
                        DataGridViewRow row = grFiles.Rows[grFiles.Rows.Add(file.DisplayName + file.Note)];
                        row.Tag = file;
                    }
                    _containFiles = true;
                    SetGridFont(ConfigurationClasses.SettingsManager.Instance.FontSize);
                    SetGridSize();
                    if (this.Parent != null)
                    {
                        ((ColumnPanel)this.Parent).ResizePanel();
                        this.Decorator.RefreshPanelHeight();
                    }
                    this.Decorator.Parent.StateChanged = true;
                    UpdateButtonsStatus();
                }
            }
        }

        public void AddNetworkFolder()
        {
            using (ToolForms.WallBin.FormAddNetworkFolder form = new ToolForms.WallBin.FormAddNetworkFolder())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    BusinessClasses.LibraryFile file = new BusinessClasses.LibraryFile(_folder);
                    file.Name = form.LinkName;
                    file.RelativePath = form.LinkPath;
                    file.Type = BusinessClasses.FileTypes.Network;
                    if (grFiles.SelectedRows.Count > 0)
                    {
                        int rowIndex = grFiles.SelectedRows[0].Index;
                        grFiles.Rows.Insert(rowIndex, file.DisplayName + file.Note);
                        DataGridViewRow row = grFiles.Rows[rowIndex];
                        row.Tag = file;
                        grFiles.ClearSelection();
                    }
                    else
                    {
                        DataGridViewRow row = grFiles.Rows[grFiles.Rows.Add(file.DisplayName + file.Note)];
                        row.Tag = file;
                    }
                    _containFiles = true;
                    SetGridFont(ConfigurationClasses.SettingsManager.Instance.FontSize);
                    SetGridSize();
                    if (this.Parent != null)
                    {
                        ((ColumnPanel)this.Parent).ResizePanel();
                        this.Decorator.RefreshPanelHeight();
                    }
                    this.Decorator.Parent.StateChanged = true;
                    UpdateButtonsStatus();
                }
            }
        }

        public void AddLineBreak()
        {
            if (grFiles.SelectedRows.Count > 0)
            {
                BusinessClasses.LibraryFile file = new BusinessClasses.LibraryFile(_folder);
                file.Type = BusinessClasses.FileTypes.LineBreak;
                file.LineBreakProperties = new BusinessClasses.LineBreakProperties();
                file.LineBreakProperties.Font = new Font(_textFont, FontStyle.Regular);
                file.IsBold = true;
                int rowIndex = grFiles.SelectedRows[0].Index;
                grFiles.Rows.Insert(rowIndex, file.DisplayName + file.Note);
                DataGridViewRow row = grFiles.Rows[rowIndex];
                row.Tag = file;
                grFiles.ClearSelection();
                SetGridSize();
                if (this.Parent != null)
                {
                    ((ColumnPanel)this.Parent).ResizePanel();
                    this.Decorator.RefreshPanelHeight();
                }
                this.Decorator.Parent.StateChanged = true;
                UpdateButtonsStatus();
            }
            else
                AppManager.Instance.ShowInfo("Select link above line break will be added");
        }

        public void DownLink()
        {
            BusinessClasses.LibraryFile file = grFiles.SelectedRows[0].Tag as BusinessClasses.LibraryFile;
            string tempFileDisplayName = grFiles.SelectedRows[0].Cells[0].Value.ToString();

            grFiles.SuspendLayout();

            grFiles.SelectedRows[0].Cells[0].Value = grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Cells[0].Value;
            grFiles.SelectedRows[0].Tag = grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Tag;

            grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Cells[0].Value = tempFileDisplayName;
            grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Tag = file;

            grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Selected = true;

            grFiles.ResumeLayout();

            UpdateButtonsStatus();
            this.Decorator.Parent.StateChanged = true;
        }

        public void UpLink()
        {
            BusinessClasses.LibraryFile file = grFiles.SelectedRows[0].Tag as BusinessClasses.LibraryFile;
            string tempFileDisplayName = grFiles.SelectedRows[0].Cells[0].Value.ToString();

            grFiles.SuspendLayout();

            grFiles.SelectedRows[0].Cells[0].Value = grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Cells[0].Value;
            grFiles.SelectedRows[0].Tag = grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Tag;

            grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Cells[0].Value = tempFileDisplayName;
            grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Tag = file;

            grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Selected = true;

            grFiles.ResumeLayout();

            UpdateButtonsStatus();
            this.Decorator.Parent.StateChanged = true;
        }

        public void ShowLinkProperties(Point cursorPosition)
        {
            if (grFiles.SelectedRows.Count > 0)
            {
                BusinessClasses.LibraryFile file = grFiles.SelectedRows[0].Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    using (ToolForms.WallBin.FormLinkProperties form = new ToolForms.WallBin.FormLinkProperties())
                    {
                        form.CaptionName = string.IsNullOrEmpty(file.PropertiesName) && file.Type == BusinessClasses.FileTypes.LineBreak ? "Line Break" : file.PropertiesName;
                        form.IsBold = file.IsBold;
                        form.EnableWidget = file.EnableWidget;
                        form.Widget = file.EnableWidget ? file.Widget : null;
                        form.EnableBanner = file.EnableBanner;
                        form.Banner = form.EnableBanner ? file.Banner : null;
                        form.IsLineBreak = file.Type == BusinessClasses.FileTypes.LineBreak;
                        if (file.Type != BusinessClasses.FileTypes.LineBreak)
                        {
                            form.Note = file.Note;
                            form.AddDate = file.AddDate;
                            form.ExpirationDateOptions = file.ExpirationDateOptions;
                            form.SearchTags = file.SearchTags;
                        }
                        else
                        {
                            form.LineBreakProperties = file.LineBreakProperties;
                        }
                        form.StartPosition = FormStartPosition.CenterScreen;
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            file.Widget = form.EnableWidget ? form.Widget : null;
                            file.EnableWidget = form.EnableWidget;
                            file.Banner = form.EnableBanner ? form.Banner : null;
                            file.EnableBanner = form.EnableBanner;
                            if (file.Type != BusinessClasses.FileTypes.LineBreak)
                            {
                                file.IsBold = form.IsBold;
                                file.Note = form.Note;
                                file.SearchTags = form.SearchTags;
                                file.ExpirationDateOptions = form.ExpirationDateOptions;
                            }
                            else
                            {
                                file.LineBreakProperties = form.LineBreakProperties;
                            }
                            grFiles.SelectedRows[0].Cells[0].Value = file.DisplayName + file.Note;

                            bool widgetColumnVisible = false;
                            foreach (DataGridViewRow row in grFiles.Rows)
                            {
                                BusinessClasses.LibraryFile libraryfile = row.Tag as BusinessClasses.LibraryFile;
                                if (libraryfile.Widget != null)
                                {
                                    widgetColumnVisible = true;
                                    break;
                                }
                            }
                            _containsWidgets = widgetColumnVisible;

                            SetGridSize();
                            grFiles.Refresh();
                            if (this.Parent != null)
                            {
                                ((ColumnPanel)this.Parent).ResizePanel();
                                this.Decorator.RefreshPanelHeight();
                            }
                            this.Decorator.Parent.StateChanged = true;
                        }
                    }
                }
            }
        }

        public void OpenLink()
        {
            if (grFiles.SelectedRows.Count > 0)
            {
                BusinessClasses.LibraryFile file = grFiles.SelectedRows[0].Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    try
                    {
                        Process.Start(file.FullPath);
                    }
                    catch
                    {
                        AppManager.Instance.ShowWarning("Couldn't open the link");
                    }
                }
            }
        }

        public void DeleteLink()
        {
            if (AppManager.Instance.ShowQuestion("Are You sure You want to remove this link/line break?") == DialogResult.Yes)
            {
                if (grFiles.SelectedRows.Count > 0)
                {
                    BusinessClasses.LibraryFile file = grFiles.SelectedRows[0].Tag as BusinessClasses.LibraryFile;
                    if (file != null)
                    {
                        if (file.Type == BusinessClasses.FileTypes.BuggyPresentation || file.Type == BusinessClasses.FileTypes.FriendlyPresentation || file.Type == BusinessClasses.FileTypes.OtherPresentation)
                        {
                            if (file.PreviewContainer == null)
                                file.PreviewContainer = new BusinessClasses.PresentationPreviewContainer(file);
                            file.PreviewContainer.ClearPreviewImages();
                        }
                    }
                    grFiles.Rows.Remove(grFiles.SelectedRows[0]);
                }
            }
        }

        public void UpdateAfterDelete()
        {
            if (!(grFiles.Rows.Count > 0))
                _containFiles = false;
            SetGridSize();
            if (this.Parent != null)
            {
                ((ColumnPanel)this.Parent).ResizePanel();
                this.Decorator.RefreshPanelHeight();
            }
            this.Decorator.Parent.StateChanged = true;
            UpdateButtonsStatus();
            grFiles.DefaultCellStyle.SelectionBackColor = this.IsActive ? Color.Wheat : _folder.BackgroundWindowColor;
        }

        public void Save()
        {
            _folder.Files.Clear();
            if (_containFiles)
            {
                foreach (DataGridViewRow row in grFiles.Rows)
                {
                    BusinessClasses.LibraryFile file = row.Tag as BusinessClasses.LibraryFile;
                    if (file != null)
                        _folder.Files.Add(file);
                }
            }
        }
        #endregion

        #region Other Methods
        protected void Init()
        {
            this.Resize += new EventHandler(FolderBoxControl_Resize);
            grFiles.CellMouseMove += new DataGridViewCellMouseEventHandler(grFiles_CellMouseMove);
            grFiles.CellMouseLeave += new DataGridViewCellEventHandler(grFiles_CellMouseLeave);
            grFiles.CellPainting += new DataGridViewCellPaintingEventHandler(grFiles_CellPainting);
            grFiles.CellFormatting += new DataGridViewCellFormattingEventHandler(grFiles_CellFormatting);
            grFiles.DragOver += (s, eParameter) => eParameter.Effect = DragDropEffects.All;
        }

        private void FolderBoxControl_Resize(object sender, EventArgs e)
        {
            Size labelSize = new Size(laFolderName.Width, Int32.MaxValue);
            laFolderName.Height = TextRenderer.MeasureText(laFolderName.Text, laFolderName.Font, labelSize, TextFormatFlags.WordBreak).Height + 10;
            SetGridSize();
        }

        private void SetGridSize()
        {
            int height = 0;
            int maxColumnWidth = 0;
            foreach (DataGridViewRow row in grFiles.Rows)
            {
                BusinessClasses.LibraryFile file = row.Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    string text = file.DisplayName + file.Note;
                    Size textSize = new Size(Int32.MaxValue, Int32.MaxValue);
                    textSize = TextRenderer.MeasureText(text, file.Type == BusinessClasses.FileTypes.LineBreak ? file.LineBreakProperties.Font : _noteFont, textSize, TextFormatFlags.SingleLine);
                    int columnWidth = textSize.Width + (_containsWidgets && (file.Type != BusinessClasses.FileTypes.LineBreak || file.Widget != null) ? ((file.Widget == null ? defaultImageWidth : file.Widget.Width) + imageWidthMargin) : 0);
                    int rowHeight = 0;
                    if (file.EnableBanner && file.Banner != null)
                        rowHeight = file.Banner.Height > (defaultImageHeight + imageHeightMargin) ? file.Banner.Height : (defaultImageHeight + imageHeightMargin);
                    else if (file.Type != BusinessClasses.FileTypes.LineBreak)
                    {
                        rowHeight = (textSize.Height > (defaultImageHeight + imageHeightMargin) ? textSize.Height : (defaultImageHeight + imageHeightMargin));
                    }
                    else
                    {
                        if (file.Widget != null)
                        {
                            rowHeight = file.Widget.Height > (defaultImageHeight + imageHeightMargin) ? file.Widget.Height : (defaultImageHeight + imageHeightMargin);
                            rowHeight = rowHeight > textSize.Height ? rowHeight : textSize.Height;
                        }
                        else
                            rowHeight = textSize.Height > (defaultImageHeight + imageHeightMargin) ? textSize.Height : (defaultImageHeight + imageHeightMargin);
                    }
                    row.Height = rowHeight;
                    if (maxColumnWidth < columnWidth)
                        maxColumnWidth = columnWidth;
                }
                height = height + row.Height;
            }
            height += +(int)(_noteFont.Size + 5);
            if (height < 90)
                height = 90;
            height = height + laFolderName.Height;

            this.Height = height;
            colDisplayName.Width = maxColumnWidth > (grFiles.Width - 10) ? maxColumnWidth : (grFiles.Width - 10);
        }

        public void SetGridFont(int size)
        {
            _textFont = new Font(_folder.WindowFont.FontFamily, size, _folder.WindowFont.Style);
            _noteFont = new Font(_folder.WindowFont.FontFamily, size, FontStyle.Bold | _folder.WindowFont.Style);
            grFiles.DefaultCellStyle.Font = _noteFont;
            colDisplayName.DefaultCellStyle.Font = _noteFont;
            _editCellFont = _textFont;
            SetGridSize();
        }

        private void UpdateDataSource()
        {
            grFiles.RowsRemoved -= new DataGridViewRowsRemovedEventHandler(grFiles_RowsRemoved);
            grFiles.Rows.Clear();
            grFiles.RowsRemoved += new DataGridViewRowsRemovedEventHandler(grFiles_RowsRemoved);
            if (_folder.Files.Count > 0)
            {
                _containFiles = true;
                foreach (BusinessClasses.LibraryFile libraryFile in _folder.Files)
                {
                    DataGridViewRow row = grFiles.Rows[grFiles.Rows.Add(libraryFile.DisplayName + libraryFile.Note)];
                    row.Tag = libraryFile;
                }
                _containsWidgets = _folder.Files.Where(x => x.Widget != null).Count() > 0;
            }
            else
                _containFiles = false;
            if (this.Parent != null)
            {
                ((ColumnPanel)this.Parent).ResizePanel();
                this.Decorator.RefreshPanelHeight();
            }
            grFiles.Refresh();
        }

        private void MakeActive()
        {
            this.IsActive = true;
            if (this.Decorator.ActiveBox != null && this.Decorator.ActiveBox != this)
                this.Decorator.ActiveBox.MakeInactive();
            this.Decorator.ActiveBox = this;
            this.Padding = new Padding(2, 2, 2, 2);
            grFiles.ReadOnly = false;
            grFiles.DefaultCellStyle.SelectionBackColor = Color.Wheat;
            this.Refresh();
            UpdateButtonsStatus();
        }

        public void MakeInactive()
        {
            this.IsActive = false;
            grFiles.ReadOnly = true;
            this.Padding = new Padding(0, 0, 0, 0);
            grFiles.DefaultCellStyle.SelectionBackColor = grFiles.DefaultCellStyle.BackColor;
            this.Decorator.ActiveBox.Refresh();
            UpdateButtonsStatus();
        }

        private int GetLeft(Control control)
        {
            return control.Left + (control.Parent != null ? GetLeft(control.Parent) : 0);
        }

        private int GetTop(Control control)
        {
            return control.Top + (control.Parent != null ? GetTop(control.Parent) : 0);
        }

        private void AddFile(FileInfo file, int rowIndex)
        {
            bool isExisted = false;
            foreach (DataGridViewRow row in grFiles.Rows)
            {
                BusinessClasses.LibraryFile libraryFile = row.Tag as BusinessClasses.LibraryFile;
                if (libraryFile != null)
                {
                    if (file.FullName.Equals(libraryFile.FullPath))
                    {
                        isExisted = true;
                        break;
                    }
                }
            }

            if (!isExisted)
            {
                BusinessClasses.LibraryFile libraryFile = new BusinessClasses.LibraryFile(_folder);
                libraryFile.Name = file.Name.Replace(file.Extension, string.Empty);
                libraryFile.RelativePath = (_folder.Parent.Parent.Name.Equals(ConfigurationClasses.SettingsManager.WholeDriveFilesStorage) ? @"\" : string.Empty) + file.FullName.Replace(_folder.Parent.Parent.Folder.FullName, string.Empty);
                libraryFile.SetProperties();

                int pathLength = libraryFile.RelativePath.Length;
                switch (libraryFile.Type)
                {
                    case BusinessClasses.FileTypes.BuggyPresentation:
                    case BusinessClasses.FileTypes.FriendlyPresentation:
                    case BusinessClasses.FileTypes.OtherPresentation:
                        using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                        {
                            FormMain.Instance.ribbonControl.Enabled = false;
                            form.laProgress.Text = "Get Presentation Properties...";
                            form.TopMost = true;

                            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                            {
                                if (InteropClasses.PowerPointHelper.Instance.Connect())
                                {
                                    libraryFile.GetPresentationPrperties();
                                    if (libraryFile.PreviewContainer == null)
                                        libraryFile.PreviewContainer = new BusinessClasses.PresentationPreviewContainer(libraryFile);
                                    InteropClasses.PowerPointHelper.Instance.Disconnect();
                                }
                            }));

                            form.Show();

                            thread.Start();

                            while (thread.IsAlive)
                                System.Windows.Forms.Application.DoEvents();

                            form.Close();
                            FormMain.Instance.ribbonControl.Enabled = true;
                        }
                        break;
                }

                if ((pathLength + ConfigurationClasses.SettingsManager.Instance.DestinationPathLength) < InteropClasses.WinAPIHelper.MAX_PATH)
                {
                    if (rowIndex >= 0 && rowIndex < grFiles.RowCount)
                    {
                        grFiles.Rows.Insert(rowIndex, libraryFile.DisplayName + libraryFile.Note);
                        DataGridViewRow row = grFiles.Rows[rowIndex];
                        row.Tag = libraryFile;
                        grFiles.Rows[rowIndex].Selected = true;
                    }
                    else
                    {
                        DataGridViewRow row = grFiles.Rows[grFiles.Rows.Add(libraryFile.DisplayName + libraryFile.Note)];
                        row.Tag = libraryFile;
                        grFiles.Rows[grFiles.RowCount - 1].Selected = true;
                    }
                    UpdateButtonsStatus();
                }
                else
                    AppManager.Instance.ShowInfo("This file path is too long.\nTry changing the file name to a shorter name, or move this file up a level in your network folders");
            }
            else
                AppManager.Instance.ShowInfo("The file " + file.Name + " is already in a window");
        }

        private void AddFolder(DirectoryInfo folder, int rowIndex)
        {
            bool isExisted = false;
            foreach (DataGridViewRow row in grFiles.Rows)
            {
                BusinessClasses.LibraryFile libraryFile = row.Tag as BusinessClasses.LibraryFile;
                if (libraryFile != null)
                {
                    if (folder.FullName.Equals(libraryFile.FullPath))
                    {
                        isExisted = true;
                        break;
                    }
                }
            }
            if (!isExisted)
            {
                BusinessClasses.LibraryFile libraryFile = new BusinessClasses.LibraryFile(_folder);
                libraryFile.Name = folder.Name;
                libraryFile.RelativePath = (_folder.Parent.Parent.Name.Equals(ConfigurationClasses.SettingsManager.WholeDriveFilesStorage) ? @"\" : string.Empty) + folder.FullName.Replace(_folder.Parent.Parent.Folder.FullName, string.Empty);
                libraryFile.Type = BusinessClasses.FileTypes.Folder;

                int pathLength = libraryFile.RelativePath.Length;
                if ((pathLength + ConfigurationClasses.SettingsManager.Instance.DestinationPathLength) < InteropClasses.WinAPIHelper.MAX_PATH)
                {
                    if (rowIndex >= 0 && rowIndex < grFiles.RowCount)
                    {
                        grFiles.Rows.Insert(rowIndex, libraryFile.DisplayName + libraryFile.Note);
                        DataGridViewRow row = grFiles.Rows[rowIndex];
                        row.Tag = libraryFile;
                        grFiles.Rows[rowIndex].Selected = true;
                    }
                    else
                    {
                        DataGridViewRow row = grFiles.Rows[grFiles.Rows.Add(libraryFile.DisplayName + libraryFile.Note)];
                        row.Tag = libraryFile;
                        grFiles.Rows[grFiles.RowCount - 1].Selected = true;
                    }
                    UpdateButtonsStatus();
                }
                else
                    AppManager.Instance.ShowInfo("This folder path is too long.\nTry changing the file name to a shorter name, or move this file up a level in your network folders");
            }
            else
                AppManager.Instance.ShowInfo("The folder " + folder.Name + " is already in a window");
        }

        private void MoveFile(DataGridViewRow row, int rowIndex)
        {
            grFiles.RowsRemoved -= new DataGridViewRowsRemovedEventHandler(grFiles_RowsRemoved);
            row.DataGridView.Rows.Remove(row);
            grFiles.RowsRemoved += new DataGridViewRowsRemovedEventHandler(grFiles_RowsRemoved);
            if (rowIndex >= 0 && rowIndex < grFiles.RowCount)
                grFiles.Rows.Insert(rowIndex, row);
            else
                grFiles.Rows.Add(row);
            row.Selected = true;
            UpdateButtonsStatus();
        }

        private void UpdateButtonsStatus()
        {
            if (this.IsActive)
            {
                if (grFiles.SelectedRows.Count > 0)
                {
                    try
                    {
                        FormMain.Instance.DeleteLinkButton = true;
                        FormMain.Instance.OpenLinkButton = true;
                        FormMain.Instance.LinkPropertiesButton = true;
                        FormMain.Instance.LineBreakButton = true;
                        if (grFiles.SelectedRows[0].Index > 0)
                            FormMain.Instance.UpLinkButton = true;
                        else
                            FormMain.Instance.UpLinkButton = false;
                        if (grFiles.SelectedRows[0].Index < grFiles.Rows.Count - 1)
                            FormMain.Instance.DownLinkButton = true;
                        else
                            FormMain.Instance.DownLinkButton = false;
                    }
                    catch
                    {
                        FormMain.Instance.UpLinkButton = false;
                        FormMain.Instance.DownLinkButton = false;
                        FormMain.Instance.DeleteLinkButton = false;
                        FormMain.Instance.OpenLinkButton = false;
                        FormMain.Instance.LinkPropertiesButton = false;
                    }
                }
                else
                {
                    FormMain.Instance.UpLinkButton = false;
                    FormMain.Instance.DownLinkButton = false;
                    FormMain.Instance.DeleteLinkButton = false;
                    FormMain.Instance.OpenLinkButton = false;
                    FormMain.Instance.LinkPropertiesButton = false;
                    FormMain.Instance.LineBreakButton = false;
                }
                FormMain.Instance.AddLinkButton = true;
            }
            else
            {
                FormMain.Instance.UpLinkButton = false;
                FormMain.Instance.DownLinkButton = false;
                FormMain.Instance.DeleteLinkButton = false;
                FormMain.Instance.OpenLinkButton = false;
                FormMain.Instance.LinkPropertiesButton = false;
                FormMain.Instance.LineBreakButton = false;
                FormMain.Instance.AddLinkButton = false;
            }
        }
        #endregion
    }
}
