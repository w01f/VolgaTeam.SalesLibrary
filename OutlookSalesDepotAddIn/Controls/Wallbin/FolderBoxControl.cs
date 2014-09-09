using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using OutlookSalesDepotAddIn.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Wallbin
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

		private LibraryFolder _folder;
		private Font _noteFont;
		private Font _textFont;
		private Cursor _storedCursor;
		private int _contantHeight;

		#region Public Properties
		public PageDecorator Decorator { get; set; }

		public LibraryFolder Folder
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
						labelControlText.Visible = true;
						pbImage.Dock = DockStyle.Left;
						pbImage.SizeMode = PictureBoxSizeMode.Normal;
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
					}
				}
				else
				{
					pbImage.Visible = false;
					if (_folder.EnableWidget && _folder.Widget != null)
						labelControlText.Appearance.Image = _folder.Widget;
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
			get { return _folder.ColumnOrder >= 0 ? _folder.ColumnOrder : 0; }
			set { _folder.ColumnOrder = value; }
		}

		public double RowOrder
		{
			get { return _folder.RowOrder; }
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

		private void grFiles_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			switch (e.ColumnIndex)
			{
				case 0:
					var value = (Boolean)grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
					grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !value;
					break;
				case 1:
					if (grFiles.Rows[e.RowIndex].Tag != null)
					{
						var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
						LinkManager.Instance.OpenLink(file);
					}
					break;
			}

		}

		private void laFolderName_Click(object sender, EventArgs e)
		{
			Parent.Parent.Parent.Focus();
		}

		private void grFiles_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
			var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
			if (file == null) return;
			if (file.Type != FileTypes.LineBreak)
				Cursor = Cursors.Hand;
		}

		private void grFiles_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			Cursor = _storedCursor;
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
			var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
			if (file == null) return;
			switch (e.ColumnIndex)
			{
				case 0:
					if (file.Type == FileTypes.LineBreak)
					{
						e.PaintBackground(e.ClipBounds, true);
						e.Handled = true;
					}
					break;
				case 1:
					{
						e.Handled = true;
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
					break;
			}
		}

		private void xtraScrollableControlGrid_Resize(object sender, EventArgs e)
		{
			SetGridSize();
		}

		void FolderBoxControl_MouseMove(object sender, MouseEventArgs e)
		{
			Parent.Parent.Parent.Focus();
		}
		#endregion

		#region Methods
		private void Init()
		{
			grFiles.CellMouseEnter += grFiles_CellMouseEnter;
			grFiles.CellMouseLeave += grFiles_CellMouseLeave;
			grFiles.CellPainting += grFiles_CellPainting;
			grFiles.CellFormatting += grFiles_CellFormatting;
			_storedCursor = Cursor;
			grFiles.DragOver += (s, eParameter) => eParameter.Effect = DragDropEffects.All;
			grFiles.ScrollBars = ScrollBars.None;
			xtraScrollableControlGrid.Resize += xtraScrollableControlGrid_Resize;
			grFiles.MouseMove += FolderBoxControl_MouseMove;
			labelControlText.MouseMove += FolderBoxControl_MouseMove;
			pbImage.MouseMove += FolderBoxControl_MouseMove;
			MouseMove += FolderBoxControl_MouseMove;
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
							imageLeft = (xtraScrollableControlGrid.Width - file.BannerProperties.Image.Width) / 2;
							if (imageLeft < 0)
								imageLeft = 0;
							break;
						case Alignment.Right:
							imageLeft = xtraScrollableControlGrid.Width - file.BannerProperties.Image.Width;
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
			if (file.BannerProperties.Enable && file.BannerProperties.ShowText && !String.IsNullOrEmpty(file.BannerProperties.Text))
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
			if (Decorator == null) return;
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
			if (_textFont == null) return;
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
					var foreColor = Color.Black;
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
				_contantHeight += row.Height;
			}
			_contantHeight += (int)(_noteFont.Size + 5);
			if (_contantHeight < 90)
				_contantHeight = 90;

			var minWidth = xtraScrollableControlGrid.Width - colSelected.Width;
			colDisplayName.Width = maxColumnWidth > minWidth ? maxColumnWidth : minWidth;
			grFiles.Width = colDisplayName.Width + colSelected.Width;

			Height = _contantHeight + pnHeaderBorder.Height;
		}

		private void UpdateDataSource()
		{
			grFiles.Rows.Clear();
			foreach (LibraryLink libraryFile in _folder.Files.Where(f => !f.IsForbidden))
			{
				var row = grFiles.Rows[grFiles.Rows.Add(false, libraryFile.DisplayName + libraryFile.Note)];
				row.Tag = libraryFile;
			}
			_containsWidgets = _folder.Files.Any(x => !x.IsForbidden && x.Widget != null);
			if (Parent != null)
				((ColumnPanel)Parent).ResizePanel();
		}

		public void UpdateView()
		{
			pnMain.Padding = new Padding(1, 1, 1, 1);
			pnHeader.BringToFront();

			_textFont = new Font(_folder.WindowFont.FontFamily, 12, _folder.WindowFont.Style);
			_noteFont = new Font(_folder.WindowFont.FontFamily, 12, FontStyle.Bold | _folder.WindowFont.Style);
			grFiles.DefaultCellStyle.Font = _noteFont;
			colDisplayName.DefaultCellStyle.Font = _noteFont;
			_containsWidgets = _folder.Files.Any(x => !x.IsForbidden && x.Widget != null);

			SetHeaderSize();
			SetGridSize();
		}

		public IEnumerable<LibraryLink> GetSelectedLinks()
		{
			foreach (var row in grFiles.Rows.OfType<DataGridViewRow>())
			{
				var selected = (Boolean)row.Cells[colSelected.Index].Value;
				if (selected)
					yield return row.Tag as LibraryLink;
			}
		}

		public void ClearSelectedLinks()
		{
			foreach (var row in grFiles.Rows.OfType<DataGridViewRow>())
				row.Cells[colSelected.Index].Value = false;
		}
		#endregion
	}
}