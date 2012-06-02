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
        private const int ImageWidthMargin = 6;
        private const int ImageHeightMargin = 6;
        private const int DefaultImageWidth = 26;
        private const int DefaultImageHeight = 26;

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

                if (_folder.BannerProperties.Enable && _folder.BannerProperties.Image != null)
                {
                    pbImage.Visible = true;
                    pbImage.Image = _folder.BannerProperties.Image;
                    if (_folder.BannerProperties.ShowText && !string.IsNullOrEmpty(_folder.BannerProperties.Text))
                    {
                        laFolderName.Visible = true;
                        pbImage.Dock = DockStyle.Left;
                        pbImage.SizeMode = PictureBoxSizeMode.Normal;
                        laFolderName.Text = _folder.BannerProperties.Text;
                        laFolderName.Font = _folder.BannerProperties.Font;
                        laFolderName.ForeColor = _folder.BannerProperties.ForeColor;

                        if (ConfigurationClasses.SettingsManager.Instance.ClassicView)
                        {
                            switch (_folder.HeaderAlignment)
                            {
                                case BusinessClasses.Alignment.Left:
                                    laFolderName.TextAlign = ContentAlignment.MiddleLeft;
                                    break;
                                case BusinessClasses.Alignment.Center:
                                    laFolderName.TextAlign = ContentAlignment.MiddleCenter;
                                    break;
                                case BusinessClasses.Alignment.Right:
                                    laFolderName.TextAlign = ContentAlignment.MiddleRight;
                                    break;
                            }
                        }
                        else
                            laFolderName.TextAlign = ContentAlignment.MiddleLeft;
                    }
                    else
                    {
                        laFolderName.Visible = false;
                        switch (_folder.BannerProperties.ImageAlignement)
                        {
                            case BusinessClasses.Alignment.Left:
                                pbImage.Dock = DockStyle.Left;
                                pbImage.SizeMode = PictureBoxSizeMode.Normal;
                                break;
                            case BusinessClasses.Alignment.Center:
                                pbImage.Dock = DockStyle.Fill;
                                pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                                break;
                            case BusinessClasses.Alignment.Right:
                                pbImage.Dock = DockStyle.Right;
                                pbImage.SizeMode = PictureBoxSizeMode.Normal;
                                break;
                        }
                        pnHeader.Height = _folder.BannerProperties.Image.Height;
                    }
                }
                else
                {
                    if (_folder.EnableWidget && _folder.Widget != null)
                    {
                        pbImage.Visible = true;
                        pbImage.Dock = DockStyle.Left;
                        pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                        pbImage.Image = _folder.Widget;
                    }
                    else
                    {
                        pbImage.Visible = false;
                    }
                    laFolderName.Text = _folder.Name;
                    laFolderName.Font = _folder.HeaderFont;
                    laFolderName.ForeColor = _folder.ForeHeaderColor;
                    if (ConfigurationClasses.SettingsManager.Instance.ClassicView)
                    {
                        switch (_folder.HeaderAlignment)
                        {
                            case BusinessClasses.Alignment.Left:
                                laFolderName.TextAlign = ContentAlignment.MiddleLeft;
                                break;
                            case BusinessClasses.Alignment.Center:
                                laFolderName.TextAlign = ContentAlignment.MiddleCenter;
                                break;
                            case BusinessClasses.Alignment.Right:
                                laFolderName.TextAlign = ContentAlignment.MiddleRight;
                                break;
                        }
                    }
                    else
                        laFolderName.TextAlign = ContentAlignment.MiddleLeft;
                }

                pnHeader.BackColor = _folder.BackgroundHeaderColor;
                pbImage.BackColor = _folder.BackgroundHeaderColor;
                laFolderName.BackColor = _folder.BackgroundHeaderColor;

                grFiles.BackgroundColor = _folder.BackgroundWindowColor;
                grFiles.DefaultCellStyle.BackColor = _folder.BackgroundWindowColor;
                grFiles.DefaultCellStyle.SelectionBackColor = _folder.BackgroundWindowColor;

                grFiles.DefaultCellStyle.ForeColor = _folder.ForeWindowColor;
                grFiles.DefaultCellStyle.SelectionForeColor = _folder.ForeWindowColor;

                SetHeaderSize();

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
                ToolClasses.ActivityRecorder.Instance.WriteActivity();
                BusinessClasses.LinkManager.Instance.OpenLink(file);
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

        private void ControlBorders_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect;
            if (e.ClipRectangle.Top == 0)
                rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, this.Height);
            else
                rect = new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
            for (int i = 0; i < 1; i++)
            {
                ControlPaint.DrawBorder(e.Graphics, rect, _folder.BorderColor, ButtonBorderStyle.Solid);
                rect.X = rect.X + 2;
                rect.Y = rect.Y + 2;
                rect.Width = rect.Width - 4;
                rect.Height = rect.Height - 4;
            }
        }

        private void grFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grFiles.Rows[e.RowIndex].Tag != null)
            {
                BusinessClasses.LibraryFile file = grFiles.Rows[e.RowIndex].Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    List<string> toolTipText = new List<string>();
                    if (file.Type != BusinessClasses.FileTypes.LineBreak)
                    {
                        toolTipText.Add(file.NameWithExtension);
                        toolTipText.Add("Added: " + file.AddDate.ToString("M/dd/yy h:mm:ss tt"));
                        if (file.ExpirationDateOptions.EnableExpirationDate && file.ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
                            toolTipText.Add("Expires: " + file.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt"));
                        else
                            toolTipText.Add("Expires: No Expiration Date");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(file.LineBreakProperties.Note))
                            toolTipText.Add(file.LineBreakProperties.Note);
                    }
                    grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = string.Join(Environment.NewLine, toolTipText.ToArray());
                }
            }
        }

        private void grFiles_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                BusinessClasses.LibraryFile file = grFiles.Rows[e.RowIndex].Tag as BusinessClasses.LibraryFile;
                if (file != null)
                {
                    e.PaintBackground(e.CellBounds, true);

                    #region Calculate Options
                    Image image = null;
                    int imageLeft = 0;
                    int imageTop = 0;
                    int imageWidth = 0;
                    int imageHeight = 0;
                    string text = string.Empty;
                    int textLeft = 0;
                    int textTop = 0;
                    int textWidth = 0;
                    int textHeight = 0;
                    int columnWidth = 0;
                    int rowHeight = 0;
                    Color foreColor = Color.Black;
                    Font font = null;

                    GetLinkGUIValues(file
                        , ref image
                        , ref imageLeft
                        , ref imageTop
                        , ref imageWidth
                        , ref imageHeight
                        , ref text
                        , ref textLeft
                        , ref textTop
                        , ref textWidth
                        , ref textHeight
                        , ref columnWidth
                        , ref rowHeight
                        , ref foreColor
                        , ref font);

                    if (columnWidth > colDisplayName.Width)
                        colDisplayName.Width = columnWidth;

                    if (rowHeight > grFiles.Rows[e.RowIndex].Height)
                    {
                        grFiles.Rows[e.RowIndex].Height = rowHeight;
                        SetGridSize();
                        if (this.Parent != null)
                        {
                            ((ColumnPanel)this.Parent).ResizePanel();
                        }
                    }
                    #endregion

                    #region Build RichTextControl
                    _richTextControl.Text = text;
                    _richTextControl.Font = font;
                    _richTextControl.Height = textHeight;
                    _richTextControl.Width = textWidth;

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
                            _richTextControl.ForeColor = grFiles.DefaultCellStyle.SelectionForeColor;
                        }
                        else
                        {
                            _richTextControl.BackColor = grFiles.DefaultCellStyle.BackColor;
                            _richTextControl.ForeColor = foreColor;
                        }
                    }
                    else
                    {
                        _richTextControl.BackColor = grFiles.DefaultCellStyle.BackColor;
                        _richTextControl.ForeColor = foreColor;
                    }
                    #endregion

                    #region Custom Draw
                    if (image != null)
                        e.Graphics.DrawImage(image, new Rectangle(e.CellBounds.X + imageLeft, e.CellBounds.Y + imageTop, imageWidth, imageHeight));
                    if (!string.IsNullOrEmpty(text))
                        e.Graphics.DrawImage(RichTextBoxPrinter.Print(_richTextControl, textWidth, textHeight), new Rectangle(e.CellBounds.X + textLeft, e.CellBounds.Y + textTop, textWidth, textHeight));
                    #endregion
                }
                e.Handled = true;
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

        private void GetLinkGUIValues(BusinessClasses.LibraryFile file
                , ref Image image
                , ref int imageLeft
                , ref int imageTop
                , ref int imageWidth
                , ref int imageHeight
                , ref string text
                , ref int textLeft
                , ref int textTop
                , ref int textWidth
                , ref int textHeight
                , ref int columnWidth
                , ref int columnHeight
                , ref Color foreColor
                , ref Font font)
        {

            #region Image
            if (file.BannerProperties.Enable && file.BannerProperties.Image != null)
                image = file.BannerProperties.Image;
            else if (file.Widget != null)
                image = file.Widget;
            else
                image = null;
            #endregion

            #region Image Size and Coordinates
            imageLeft = 0;
            imageTop = 0;
            imageHeight = 0;
            imageWidth = 0;
            if (file.BannerProperties.Enable && file.BannerProperties.Image != null)
            {
                if (file.BannerProperties.ShowText)
                {
                    imageLeft = 0;
                }
                else
                {
                    switch (file.BannerProperties.ImageAlignement)
                    {
                        case BusinessClasses.Alignment.Left:
                            imageLeft = 0;
                            break;
                        case BusinessClasses.Alignment.Center:
                            imageLeft = (grFiles.Width - file.BannerProperties.Image.Width) / 2;
                            if (imageLeft < 0)
                                imageLeft = 0;
                            break;
                        case BusinessClasses.Alignment.Right:
                            imageLeft = grFiles.Width - file.BannerProperties.Image.Width;
                            if (imageLeft < 0)
                                imageLeft = 0;
                            break;
                        default:
                            imageLeft = 0;
                            break;
                    }
                }
                imageWidth = file.BannerProperties.Image.Width > DefaultImageWidth ? file.BannerProperties.Image.Width : DefaultImageWidth;
                imageHeight = file.BannerProperties.Image.Height > DefaultImageHeight ? file.BannerProperties.Image.Height : DefaultImageHeight;
            }
            else if (file.Widget != null)
            {
                imageLeft = 0;
                imageWidth = file.Widget.Width > DefaultImageWidth ? file.Widget.Width : DefaultImageWidth;
                imageHeight = file.Widget.Height > DefaultImageHeight ? file.Widget.Height : DefaultImageHeight;
            }
            else
            {
                imageLeft = 0;
                imageWidth = file.Type == BusinessClasses.FileTypes.LineBreak || !_containsWidgets ? 0 : DefaultImageWidth;
                imageHeight = DefaultImageHeight;
            }
            #endregion

            #region Text
            text = string.Empty;
            if (file.BannerProperties.Enable)
            {
                if (file.BannerProperties.ShowText && !string.IsNullOrEmpty(file.BannerProperties.Text))
                    text = file.BannerProperties.Text;
            }
            else
                text = file.DisplayName + file.Note;
            #endregion

            #region Font
            if (file.BannerProperties.Enable && file.BannerProperties.ShowText)
                font = file.BannerProperties.Font;
            else if (file.Type == BusinessClasses.FileTypes.LineBreak)
                font = file.LineBreakProperties.Font;
            else
                font = file.DisplayAsBold ? _noteFont : _textFont;
            #endregion

            #region Text Size and Coordinates
            Size textSize;
            if (file.BannerProperties.Enable && file.BannerProperties.ShowText && !string.IsNullOrEmpty(file.BannerProperties.Text))
            {
                textSize = new Size(Int32.MaxValue, Int32.MaxValue);
                textSize = TextRenderer.MeasureText(text, font, textSize, TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
            }
            else
            {
                textSize = new Size(Int32.MaxValue, Int32.MaxValue);
                textSize = TextRenderer.MeasureText(text, font, textSize, TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix);
            }

            textSize = new Size(textSize.Width, textSize.Height + 1);

            if (file.BannerProperties.Enable)
            {
                textLeft = imageLeft + imageWidth;
                textWidth = textSize.Width;
                textHeight = textSize.Height;
            }
            else if (file.Type == BusinessClasses.FileTypes.LineBreak)
            {
                textLeft = imageLeft + imageWidth + ImageWidthMargin;
                textWidth = textSize.Width;
                textHeight = textSize.Height;
            }
            else
            {
                textLeft = imageLeft + imageWidth + (_containsWidgets ? ImageWidthMargin : 0);
                textWidth = textSize.Width;
                textHeight = textSize.Height;
            }

            columnWidth = textLeft + textWidth + 10;
            #endregion

            #region Fore Color
            if (file.BannerProperties.Enable && file.BannerProperties.ShowText)
                foreColor = file.BannerProperties.ForeColor;
            else if (file.Type == BusinessClasses.FileTypes.LineBreak)
                foreColor = file.LineBreakProperties.ForeColor;
            else
                foreColor = grFiles.DefaultCellStyle.ForeColor;
            #endregion

            #region Correct Image and text coordinates
            if (textHeight > imageHeight)
            {
                textTop = 0;
                imageTop = (textHeight - imageHeight) / 2;
            }
            else if (textHeight == imageHeight)
            {
                textTop = 0;
                imageTop = 0;
            }
            else
            {
                textTop = (imageHeight - textHeight) / 2;
                imageTop = 0;
            }
            columnHeight = textHeight > imageHeight ? textHeight : imageHeight;
            #endregion
        }

        private void SetHeaderSize()
        {
            Size labelSize;
            int textHeight;
            if (_folder.BannerProperties.Enable && _folder.BannerProperties.Image != null)
            {
                pbImage.Width = _folder.BannerProperties.Image.Width;
                if (_folder.BannerProperties.ShowText && !string.IsNullOrEmpty(_folder.BannerProperties.Text))
                {
                    labelSize = new Size(laFolderName.Width, Int32.MaxValue);
                    textHeight = TextRenderer.MeasureText(laFolderName.Text, laFolderName.Font, labelSize, TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix).Height + 10;
                    pnHeader.Height = _folder.BannerProperties.Image.Height > textHeight ? _folder.BannerProperties.Image.Height : textHeight;
                }
                else
                {
                    pnHeader.Height = _folder.BannerProperties.Image.Height;
                }
            }
            else
            {
                if (_folder.EnableWidget)
                {
                    pbImage.Width = _folder.Widget.Width > DefaultImageWidth ? _folder.Widget.Width : DefaultImageWidth;
                    labelSize = new Size(laFolderName.Width, Int32.MaxValue);
                    textHeight = TextRenderer.MeasureText(laFolderName.Text, laFolderName.Font, labelSize, TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix).Height + 10;
                    pnHeader.Height = _folder.Widget.Height > textHeight ? _folder.Widget.Height : (textHeight > DefaultImageHeight ? textHeight : DefaultImageHeight);
                }
                else
                {
                    labelSize = new Size(laFolderName.Width, Int32.MaxValue);
                    textHeight = TextRenderer.MeasureText(laFolderName.Text, laFolderName.Font, labelSize, TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix).Height + 10;
                    pnHeader.Height = textHeight > DefaultImageHeight ? textHeight : DefaultImageHeight;
                }
            }
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
                    Image image = null;
                    int imageLeft = 0;
                    int imageTop = 0;
                    int imageWidth = 0;
                    int imageHeight = 0;
                    string text = string.Empty;
                    int textLeft = 0;
                    int textTop = 0;
                    int textWidth = 0;
                    int textHeight = 0;
                    int columnWidth = 0;
                    int rowHeight = 0;
                    Color foreColor = Color.Black;
                    Font font = null;

                    GetLinkGUIValues(file
                        , ref image
                        , ref imageLeft
                        , ref imageTop
                        , ref imageWidth
                        , ref imageHeight
                        , ref text
                        , ref textLeft
                        , ref textTop
                        , ref textWidth
                        , ref textHeight
                        , ref columnWidth
                        , ref rowHeight
                        , ref foreColor
                        , ref font);

                    row.Height = rowHeight;
                    if (maxColumnWidth < columnWidth)
                        maxColumnWidth = columnWidth;
                }
                height = height + row.Height;
            }
            height += +(int)(_noteFont.Size + 5);
            if (height < 90)
                height = 90;
            height = height + pnHeader.Height;

            this.Height = height + (ConfigurationClasses.SettingsManager.Instance.ListView ? (pnBottom.Height + pnTop.Height) : 0);
            colDisplayName.Width = maxColumnWidth > (grFiles.Width - 10) ? maxColumnWidth : (grFiles.Width - 10);
        }

        public void SetGridFont(int size)
        {
            _textFont = new Font(_folder.WindowFont.FontFamily, size, _folder.WindowFont.Style);
            _noteFont = new Font(_folder.WindowFont.FontFamily, size, FontStyle.Bold | _folder.WindowFont.Style);
            grFiles.DefaultCellStyle.Font = _noteFont;
            colDisplayName.DefaultCellStyle.Font = _noteFont;
            _containsWidgets = _folder.Files.Where(x => x.Widget != null).Count() > 0;
            SetHeaderSize();
            SetGridSize();
        }

        public void UpdateView()
        {
            pnTop.Visible = ConfigurationClasses.SettingsManager.Instance.ListView;
            pnBottom.Visible = ConfigurationClasses.SettingsManager.Instance.ListView;
            pnRight.Visible = ConfigurationClasses.SettingsManager.Instance.ListView;
            pnIndex.Visible = ConfigurationClasses.SettingsManager.Instance.ListView;
            SetHeaderSize();
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
