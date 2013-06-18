using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;

namespace SalesDepot.PresentationClasses.WallBin
{
	[ToolboxItem(false)]
	public partial class FolderBoxControl : UserControl
	{
		private const int ImageWidthMargin = 6;
		private const int ImageHeightMargin = 6;
		private const int DefaultImageWidth = 26;
		private const int DefaultImageHeight = 26;
		private readonly RichTextBox _richTextControl = new RichTextBox();
		private bool _containsWidgets;
		private Rectangle _dragBox;

		private LibraryFolder _folder;
		private DataGridView.HitTestInfo _hitTest;
		private Font _noteFont;
		private Font _textFont;
		private Cursor storedCursor;
		private int _contantHeight;

		#region Public Properties
		public event EventHandler<EventArgs> ContentVisibilityChanged;
		public PageDecorator Decorator { get; set; }

		private bool _contentExpanded;
		public bool ContentExpanded
		{
			get { return _contentExpanded; }
			set
			{
				_contentExpanded = value;
				buttonXHeader.Checked = _contentExpanded;
			}
		}

		public LibraryFolder Folder
		{
			set
			{
				_folder = value;
				var linksCount = _folder.Files.Count(x => x.Type != FileTypes.LineBreak);
				var linksText = linksCount > 0 ? string.Format(" <font color=\"#727272\">({0})</font>", linksCount > 1 ? string.Format("{0} links", linksCount.ToString()) : "1 link") : string.Empty;
				laIndex.Text = (_folder.AbsoluteRowOrder + 1).ToString();

				if (_folder.BannerProperties.Enable && _folder.BannerProperties.Image != null)
				{
					pbImage.Visible = true;
					pbImage.Image = _folder.BannerProperties.Image;
					if (_folder.BannerProperties.ShowText && !string.IsNullOrEmpty(_folder.BannerProperties.Text))
					{
						labelControlText.Visible = true;
						pbImage.Dock = DockStyle.Left;
						pbImage.SizeMode = PictureBoxSizeMode.Normal;
						buttonXHeader.Text = _folder.BannerProperties.Text + linksText;
						labelControlText.Text = _folder.BannerProperties.Text;
						labelControlText.Font = _folder.BannerProperties.Font;
						labelControlText.ForeColor = _folder.BannerProperties.ForeColor;
						switch (_folder.HeaderAlignment)
						{
							case Alignment.Left:
								labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
								break;
							case Alignment.Center:
								labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
								break;
							case Alignment.Right:
								labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
								break;
						}
					}
					else
					{
						labelControlText.Visible = false;
						switch (_folder.BannerProperties.ImageAlignement)
						{
							case Alignment.Left:
								pbImage.Dock = DockStyle.Left;
								pbImage.SizeMode = PictureBoxSizeMode.Normal;
								break;
							case Alignment.Center:
								pbImage.Dock = DockStyle.Fill;
								pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
								break;
							case Alignment.Right:
								pbImage.Dock = DockStyle.Right;
								pbImage.SizeMode = PictureBoxSizeMode.Normal;
								break;
						}
						pnHeaderBorder.Height = _folder.BannerProperties.Image.Height;
						buttonXHeader.Text = _folder.Name + linksText;
					}
				}
				else
				{
					pbImage.Visible = false;
					if (_folder.EnableWidget && _folder.Widget != null)
						labelControlText.Appearance.Image = _folder.Widget;
					buttonXHeader.Text = _folder.Name + linksText;
					labelControlText.Text = _folder.Name;
					labelControlText.Font = _folder.HeaderFont;
					labelControlText.ForeColor = _folder.ForeHeaderColor;
					switch (_folder.HeaderAlignment)
					{
						case Alignment.Left:
							labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
							break;
						case Alignment.Center:
							labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
							break;
						case Alignment.Right:
							labelControlText.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
							break;
					}
				}

				pnHeader.BackColor = _folder.BackgroundHeaderColor;
				pnHeaderBorder.BackColor = _folder.BackgroundHeaderColor;
				pbImage.BackColor = _folder.BackgroundHeaderColor;
				labelControlText.BackColor = _folder.BackgroundHeaderColor;

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
			get { return _folder.ColumnOrder >= 0 ? (SettingsManager.Instance.ClassicView || SettingsManager.Instance.AccordionView ? _folder.ColumnOrder : 2) : 0; }
			set { _folder.ColumnOrder = value; }
		}

		public double RowOrder
		{
			get { return SettingsManager.Instance.ClassicView ? _folder.RowOrder : _folder.AbsoluteRowOrder; }
			set { _folder.RowOrder = value; }
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

		private void pnHeader_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				LinkManager.Instance.OpenLibraryFolder(_folder);
			}
		}

		private void grFiles_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			var ht = grFiles.HitTest(e.X, e.Y);
			if (ht.Type == DataGridViewHitTestType.Cell)
			{
				_hitTest = ht;
				_dragBox = new Rectangle(new Point(e.X - (SystemInformation.DragSize.Width / 2), e.Y - (SystemInformation.DragSize.Height / 2)),
										 SystemInformation.DragSize);
			}
			else
				_hitTest = DataGridView.HitTestInfo.Nowhere;
		}

		private void grFiles_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			var ht = grFiles.HitTest(e.X, e.Y);
			if (ht.Type == DataGridViewHitTestType.Cell)
			{
				var file = grFiles.Rows[ht.RowIndex].Tag as LibraryLink;
				LinkManager.Instance.OpenLink(file, true);
			}
		}

		private void grFiles_MouseMove(object sender, MouseEventArgs e)
		{
			Parent.Parent.Parent.Focus();
			if (((e.Button & MouseButtons.Right) != MouseButtons.Right)
				|| _hitTest == DataGridView.HitTestInfo.Nowhere
				|| _dragBox.Contains(e.X, e.Y))
				return;
			var file = grFiles.Rows[_hitTest.RowIndex].Tag as LibraryLink;
			if (file != null)
				switch (file.Type)
				{
					case FileTypes.BuggyPresentation:
					case FileTypes.FriendlyPresentation:
					case FileTypes.MediaPlayerVideo:
					case FileTypes.Other:
					case FileTypes.Excel:
					case FileTypes.PDF:
					case FileTypes.Word:
					case FileTypes.Presentation:
					case FileTypes.QuickTimeVideo:
						grFiles.DoDragDrop(new DataObject(DataFormats.Serializable, file), DragDropEffects.Copy);
						break;
				}
		}

		private void grFiles_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (grFiles.Rows[e.RowIndex].Tag != null)
			{
				var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
				LinkManager.Instance.OpenLink(file);
			}
		}

		private void laFolderName_Click(object sender, EventArgs e)
		{
			grFiles.Focus();
		}

		private void grFiles_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
			if (file != null)
				if (file.Type != FileTypes.LineBreak)
					Cursor = Cursors.Hand;
		}

		private void grFiles_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			Cursor = storedCursor;
		}

		private void ControlBorders_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rect;
			if (e.ClipRectangle.Top == 0)
				rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height);
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

		private void pnBottom_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rect;
			if (e.ClipRectangle.Top == 0)
				rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height);
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
				var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
				if (file != null)
				{
					var toolTipText = new List<string>();
					if (file.Type != FileTypes.LineBreak)
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
				var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
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
						if (Parent != null)
							((ColumnPanel)Parent).ResizePanel();
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
					_richTextControl.BackColor = grFiles.DefaultCellStyle.BackColor;
					_richTextControl.ForeColor = foreColor;
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

		private void buttonXHeader_Click(object sender, EventArgs e)
		{
			ContentExpanded = !ContentExpanded;
			UpdateContent();
		}
		#endregion

		#region Methods
		private void Init()
		{
			grFiles.MouseMove += grFiles_MouseMove;
			grFiles.CellMouseEnter += grFiles_CellMouseEnter;
			grFiles.CellMouseLeave += grFiles_CellMouseLeave;
			grFiles.CellPainting += grFiles_CellPainting;
			grFiles.CellFormatting += grFiles_CellFormatting;
			storedCursor = Cursor;
			grFiles.DragOver += (s, eParameter) => eParameter.Effect = DragDropEffects.All;
			grFiles.ScrollBars = ScrollBars.Horizontal;
		}

		private void GetLinkGUIValues(LibraryLink file
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
									  , ref int rowHeight
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
						case Alignment.Left:
							imageLeft = 0;
							break;
						case Alignment.Center:
							imageLeft = (grFiles.Width - file.BannerProperties.Image.Width) / 2;
							if (imageLeft < 0)
								imageLeft = 0;
							break;
						case Alignment.Right:
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
				imageWidth = file.Type == FileTypes.LineBreak || !_containsWidgets ? 0 : DefaultImageWidth;
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
			Font fontForSizeCalculation;
			if (file.BannerProperties.Enable && file.BannerProperties.ShowText)
			{
				font = file.BannerProperties.Font;
				fontForSizeCalculation = font;
			}
			else if (file.Type == FileTypes.LineBreak)
			{
				font = file.DisplayAsBold ? file.LineBreakProperties.BoldFont : file.LineBreakProperties.Font;
				fontForSizeCalculation = file.LineBreakProperties.BoldFont;
			}
			else
			{
				font = file.DisplayAsBold ? _noteFont : _textFont;
				fontForSizeCalculation = _noteFont;
			}
			#endregion

			#region Text Size and Coordinates
			SizeF textSize;
			if (file.BannerProperties.Enable && file.BannerProperties.ShowText && !string.IsNullOrEmpty(file.BannerProperties.Text))
				using (var g = labelControlText.CreateGraphics())
					textSize = g.MeasureString(text, fontForSizeCalculation, Int32.MaxValue);
			else
				using (var g = labelControlText.CreateGraphics())
					textSize = g.MeasureString(text, fontForSizeCalculation, Int32.MaxValue);

			if (file.BannerProperties.Enable)
			{
				textLeft = imageLeft + imageWidth;
				textWidth = (int)textSize.Width;
				textHeight = (int)textSize.Height + 2;
			}
			else if (file.Type == FileTypes.LineBreak)
			{
				textLeft = imageLeft + imageWidth + ImageWidthMargin;
				textWidth = (int)textSize.Width;
				textHeight = (int)textSize.Height;
			}
			else
			{
				textLeft = imageLeft + imageWidth + (_containsWidgets ? ImageWidthMargin : 0);
				textWidth = (int)textSize.Width;
				textHeight = (int)textSize.Height;
			}

			if (SettingsManager.Instance.AccordionView)
				textWidth += 20;

			columnWidth = textLeft + textWidth + 10;
			#endregion

			#region Fore Color
			if (file.BannerProperties.Enable && file.BannerProperties.ShowText)
				foreColor = file.BannerProperties.ForeColor;
			else if (file.Type == FileTypes.LineBreak)
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
			rowHeight = textHeight > (imageHeight + ImageHeightMargin) ? textHeight : (imageHeight + ImageHeightMargin);
			#endregion
		}

		private void SetHeaderSize()
		{
			int textHeight;
			if (_folder.BannerProperties.Enable && _folder.BannerProperties.Image != null)
			{
				pbImage.Width = _folder.BannerProperties.Image.Width;
				if (_folder.BannerProperties.ShowText && !string.IsNullOrEmpty(_folder.BannerProperties.Text))
				{
					using (Graphics g = labelControlText.CreateGraphics())
						textHeight = (int)g.MeasureString(labelControlText.Text, labelControlText.Font, new Size(labelControlText.Width, Int32.MaxValue)).Height + 10;
					pnHeaderBorder.Height = _folder.BannerProperties.Image.Height > textHeight ? _folder.BannerProperties.Image.Height : textHeight;
				}
				else
				{
					pnHeaderBorder.Height = _folder.BannerProperties.Image.Height;
				}
			}
			else
			{
				if (_folder.EnableWidget && _folder.Widget != null)
				{
					using (Graphics g = labelControlText.CreateGraphics())
						textHeight = (int)g.MeasureString(labelControlText.Text, labelControlText.Font, new Size(labelControlText.Width - _folder.Widget.Width, Int32.MaxValue)).Height + 10;
					pnHeaderBorder.Height = _folder.Widget.Height > textHeight ? _folder.Widget.Height : (textHeight > DefaultImageHeight ? textHeight : DefaultImageHeight);
				}
				else
				{
					using (Graphics g = labelControlText.CreateGraphics())
						textHeight = (int)g.MeasureString(labelControlText.Text, labelControlText.Font, new Size(labelControlText.Width, Int32.MaxValue)).Height + 10;
					pnHeaderBorder.Height = textHeight > DefaultImageHeight ? textHeight : DefaultImageHeight;
				}
			}
		}

		private void SetGridSize()
		{
			_contantHeight = 0;
			int maxColumnWidth = 0;
			foreach (DataGridViewRow row in grFiles.Rows)
			{
				var file = row.Tag as LibraryLink;
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

					switch (SettingsManager.Instance.RowSpace)
					{
						case 2:
							rowHeight += 5;
							break;
						case 3:
							rowHeight += 10;
							break;
					}

					row.Height = rowHeight;
					if (maxColumnWidth < columnWidth)
						maxColumnWidth = columnWidth;
				}
				_contantHeight += row.Height;
			}
			_contantHeight += (int)(_noteFont.Size + 5);
			if (_contantHeight < 90)
				_contantHeight = 90;

			colDisplayName.Width = maxColumnWidth > (grFiles.Width - 10) ? maxColumnWidth : (grFiles.Width - 10);

			Height = (_contentExpanded || !SettingsManager.Instance.AccordionView ? _contantHeight : 0) + pnHeaderBorder.Height + (SettingsManager.Instance.ListView || SettingsManager.Instance.AccordionView ? (pnBottom.Height + pnTop.Height) : 0);
		}

		public void SetGridFont(int size)
		{
			_textFont = new Font(_folder.WindowFont.FontFamily, size, _folder.WindowFont.Style);
			_noteFont = new Font(_folder.WindowFont.FontFamily, size, FontStyle.Bold | _folder.WindowFont.Style);
			grFiles.DefaultCellStyle.Font = _noteFont;
			colDisplayName.DefaultCellStyle.Font = _noteFont;
			_containsWidgets = _folder.Files.Any(x => x.Widget != null);
			SetHeaderSize();
			SetGridSize();
		}

		public void UpdateView()
		{
			pnMain.Padding = SettingsManager.Instance.ListView || SettingsManager.Instance.AccordionView ? new Padding(1, 1, 1, 0) : new Padding(1, 1, 1, 1);
			pnTop.Visible = SettingsManager.Instance.ListView || SettingsManager.Instance.AccordionView;
			pnBottom.Visible = SettingsManager.Instance.ListView || SettingsManager.Instance.AccordionView;
			pnRight.Visible = SettingsManager.Instance.ListView || SettingsManager.Instance.AccordionView;
			pnIndex.Visible = SettingsManager.Instance.ListView || SettingsManager.Instance.AccordionView;
			laIndex.Visible = SettingsManager.Instance.ListView;
			if (SettingsManager.Instance.AccordionView)
			{
				pnRight.Width = pnIndex.Width = 25;
				buttonXHeader.BringToFront();
			}
			else
			{
				ContentExpanded = false;
				pnRight.Width = pnIndex.Width = 50;
				pnHeader.BringToFront();
			}
			SetHeaderSize();
			SetGridSize();
		}

		public void UpdateContent()
		{
			if (ContentVisibilityChanged != null)
				ContentVisibilityChanged(this, new EventArgs());
		}

		private void UpdateDataSource()
		{
			grFiles.Rows.Clear();
			foreach (LibraryLink libraryFile in _folder.Files)
			{
				var row = grFiles.Rows[grFiles.Rows.Add(libraryFile.DisplayName + libraryFile.Note)];
				row.Tag = libraryFile;
			}
			_containsWidgets = _folder.Files.Any(x => x.Widget != null);
			if (Parent != null)
				((ColumnPanel)Parent).ResizePanel();
		}
		#endregion
	}
}