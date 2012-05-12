using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class FolderBoxControl : UserControl
    {
        private const int imageWidthMargin = 6;
        private const int imageHeightMargin = 6;
        private const int defaultImageWidth = 26;
        private const int defaultImageHeight = 26;

        private Cursor storedCursor;
        private BusinessClasses.LibraryFolder _folder;
        private DataGridView.HitTestInfo _hitTest;
        private Point _hitTestPoint;
        private Rectangle _dragBox;
        private Font _noteFont;
        private Font _textFont;
        private RichTextBox _richTextControl = new RichTextBox();
        private bool _containsWidgets = false;

        #region Public Properties
        public BusinessClasses.LibraryFolder Folder
        {
            set
            {
                _folder = value;

                laFolderName.Text = _folder.Name;
                laIndex.Text = (_folder.AbsoluteRowOrder + 1).ToString();

                laFolderName.Font = _folder.HeaderFont;
                laIndex.Font = new System.Drawing.Font(_folder.HeaderFont.Name, _folder.HeaderFont.Size + 2, _folder.HeaderFont.Style);

                laFolderName.BackColor = _folder.BackgroundHeaderColor;

                laFolderName.ForeColor = _folder.ForeHeaderColor;

                grFiles.BackgroundColor = _folder.BackgroundWindowColor;
                grFiles.DefaultCellStyle.BackColor = _folder.BackgroundWindowColor;
                grFiles.DefaultCellStyle.SelectionBackColor = _folder.BackgroundWindowColor;

                grFiles.DefaultCellStyle.ForeColor = _folder.ForeWindowColor;
                grFiles.DefaultCellStyle.SelectionForeColor = _folder.ForeWindowColor;

                Size labelSize = new Size(laFolderName.Width, Int32.MaxValue);
                laFolderName.Height = TextRenderer.MeasureText(laFolderName.Text, laFolderName.Font, labelSize, TextFormatFlags.WordBreak).Height + 10;
                laIndex.Height = laFolderName.Height;

                UpdateDataSource();

                grFiles.Refresh();
            }
        }

        public int Column
        {
            get
            {
                return _folder.ColumnOrder >= 0 ? (ConfigurationClasses.SettingsManager.Instance.ClassicView ? _folder.ColumnOrder : 2) : 0;
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
                return ConfigurationClasses.SettingsManager.Instance.ClassicView ? _folder.RowOrder : _folder.AbsoluteRowOrder;
            }
            set
            {
                _folder.RowOrder = value;
            }
        }
        #endregion

        #region GUI Routines
        public FolderBoxControl()
        {
            InitializeComponent();
        }

        private void FileBoxControl_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void grFiles_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var ht = grFiles.HitTest(e.X, e.Y);
                if (ht.Type == DataGridViewHitTestType.Cell)
                {
                    _hitTest = ht;
                    _hitTestPoint = e.Location;
                    _dragBox = new Rectangle(new Point(e.X - (SystemInformation.DragSize.Width / 2), e.Y - (SystemInformation.DragSize.Height / 2)),
                        SystemInformation.DragSize);
                }
                else
                    _hitTest = DataGridView.HitTestInfo.Nowhere;
            }
        }

        private void grFiles_MouseMove(object sender, MouseEventArgs e)
        {
            this.Parent.Parent.Parent.Focus();
            if (((e.Button & MouseButtons.Right) != MouseButtons.Right)
                || _hitTest == DataGridView.HitTestInfo.Nowhere
                || _dragBox.Contains(e.X, e.Y))
                return;
            BusinessClasses.LibraryFile file = grFiles.Rows[_hitTest.RowIndex].Tag as BusinessClasses.LibraryFile;
            if (file != null)
                switch (file.Type)
                {
                    case BusinessClasses.FileTypes.BuggyPresentation:
                    case BusinessClasses.FileTypes.FriendlyPresentation:
                    case BusinessClasses.FileTypes.MediaPlayerVideo:
                    case BusinessClasses.FileTypes.Other:
                    case BusinessClasses.FileTypes.Excel:
                    case BusinessClasses.FileTypes.PDF:
                    case BusinessClasses.FileTypes.Word:
                    case BusinessClasses.FileTypes.OtherPresentation:
                    case BusinessClasses.FileTypes.QuickTimeVideo:
                        grFiles.DoDragDrop(new DataObject(DataFormats.Serializable, file), DragDropEffects.Copy);
                        break;
                }
        }

        private void grFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grFiles.Rows[e.RowIndex].Tag != null)
            {
                BusinessClasses.LibraryFile file = grFiles.Rows[e.RowIndex].Tag as BusinessClasses.LibraryFile;
                ActivityRecorder.Instance.WriteActivity();
                BusinessClasses.LinkManager.OpenLink(file);
            }
        }

        private void laFolderName_Click(object sender, EventArgs e)
        {
            grFiles.Focus();
        }

        void grFiles_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            BusinessClasses.LibraryFile file = grFiles.Rows[e.RowIndex].Tag as BusinessClasses.LibraryFile;
            if (file != null)
                if (file.Type != BusinessClasses.FileTypes.LineBreak)
                    this.Cursor = Cursors.Hand;
        }

        private void grFiles_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = storedCursor;
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
                        toolTipText.Add(file.PropertiesName);
                        toolTipText.Add("Added: " + file.AddDate.ToString("M/dd/yy h:mm:ss tt"));
                        if (file.ExpirationDateOptions.EnableExpirationDate && file.ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
                            toolTipText.Add("Expires: " + file.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt"));
                        else
                            toolTipText.Add("Expires: No Expiration Date");
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
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region Methods
        private void Init()
        {
            grFiles.MouseMove += new MouseEventHandler(grFiles_MouseMove);
            grFiles.CellMouseEnter += new DataGridViewCellEventHandler(grFiles_CellMouseEnter);
            grFiles.CellMouseLeave += new DataGridViewCellEventHandler(grFiles_CellMouseLeave);
            grFiles.CellPainting += new DataGridViewCellPaintingEventHandler(grFiles_CellPainting);
            grFiles.CellFormatting += new DataGridViewCellFormattingEventHandler(grFiles_CellFormatting);
            storedCursor = this.Cursor;
            grFiles.DragOver += (s, eParameter) => eParameter.Effect = DragDropEffects.All;
            grFiles.ScrollBars = ScrollBars.Horizontal;
        }

        private void SetGridSize()
        {
            int height = 0;
            int maxColumnWidth = 0;

            pnMain.BorderStyle = System.Windows.Forms.BorderStyle.None;

            Size labelSize = new Size(laFolderName.Width, Int32.MaxValue);
            laFolderName.Height = TextRenderer.MeasureText(laFolderName.Text, laFolderName.Font, labelSize, TextFormatFlags.WordBreak).Height + 10;
            laIndex.Height = laFolderName.Height;

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
                height += row.Height;
            }
            height += +(int)(_noteFont.Size + 5);
            if (height < 90)
                height = 90;
            height = height + laFolderName.Height;

            this.Height = height + (ConfigurationClasses.SettingsManager.Instance.ListView ? (pnBottom.Height + pnTop.Height) : 0);
            colDisplayName.Width = maxColumnWidth > (grFiles.Width - 20) ? maxColumnWidth : (grFiles.Width - 20);

            pnMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        public void SetGridFont(int size)
        {
            _textFont = new Font(_folder.WindowFont.FontFamily, size, _folder.WindowFont.Style);
            _noteFont = new Font(_folder.WindowFont.FontFamily, size, FontStyle.Bold | _folder.WindowFont.Style);
            grFiles.DefaultCellStyle.Font = _noteFont;
            colDisplayName.DefaultCellStyle.Font = _noteFont;
            _containsWidgets = _folder.Files.Where(x => x.Widget != null).Count() > 0;
            SetGridSize();
        }

        public void UpdateView()
        {
            pnTop.Visible = ConfigurationClasses.SettingsManager.Instance.ListView;
            pnBottom.Visible = ConfigurationClasses.SettingsManager.Instance.ListView;
            pnRight.Visible = ConfigurationClasses.SettingsManager.Instance.ListView;
            pnIndex.Visible = ConfigurationClasses.SettingsManager.Instance.ListView;
            laFolderName.TextAlign = ConfigurationClasses.SettingsManager.Instance.ClassicView ? ContentAlignment.MiddleCenter : ContentAlignment.MiddleLeft;
            SetGridSize();
        }

        private void UpdateDataSource()
        {
            grFiles.Rows.Clear();
            foreach (BusinessClasses.LibraryFile libraryFile in _folder.Files)
            {
                DataGridViewRow row = grFiles.Rows[grFiles.Rows.Add(libraryFile.DisplayName + libraryFile.Note)];
                row.Tag = libraryFile;
            }
            _containsWidgets = _folder.Files.Where(x => x.Widget != null).Count() > 0;
            if (this.Parent != null)
                ((ColumnPanel)this.Parent).ResizePanel();
        }
        #endregion
    }
}
