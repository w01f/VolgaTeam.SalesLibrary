using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using FileManager.ConfigurationClasses;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.ToolForms;
using FileManager.ToolForms.WallBin;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;

namespace FileManager.PresentationClasses.WallBin
{
	[ToolboxItem(false)]
	public partial class FolderBoxControl : UserControl
	{
		private const int ImageWidthMargin = 6;
		private const int ImageHeightMargin = 6;
		private const int DefaultImageWidth = 26;
		private const int DefaultImageHeight = 26;

		private readonly Pen _boxDropHintPen = new Pen(Color.Black, 8);
		private readonly FormLinkProperties _formLinkProperties = new FormLinkProperties();
		private readonly RichTextBox _richTextControl = new RichTextBox();
		private readonly Pen _rowDropHintPen = new Pen(Color.Black, 2);
		private bool _containFiles;
		private bool _containsWidgets;
		private int _currentDragOverRow = -1;
		private Font _displayCellFont;
		private Rectangle _dragBox;
		private Font _editCellFont;
		private LibraryFolder _folder;

		private DataGridView.HitTestInfo _hitTest;
		private Font _noteFont;
		private Font _textFont;
		private bool _underlineBox;
		private bool _underlineRow;

		#region Public Properties
		public PageDecorator Decorator { get; set; }
		public WallBinOptions WallBinOptions { get; private set; }
		public bool IsActive { get; set; }

		public LibraryFolder Folder
		{
			get { return _folder; }
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
						pnHeaderBorder.Height = _folder.BannerProperties.Image.Height;
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
				grFiles.DefaultCellStyle.SelectionBackColor = IsActive ? Color.Wheat : _folder.BackgroundWindowColor;

				grFiles.DefaultCellStyle.ForeColor = _folder.ForeWindowColor;
				grFiles.DefaultCellStyle.SelectionForeColor = _folder.ForeWindowColor;

				SetHeaderSize();

				UpdateDataSource();

				grFiles.Refresh();
			}
		}

		public int Column
		{
			get { return _folder.ColumnOrder; }
			set { _folder.ColumnOrder = value; }
		}

		public double RowOrder
		{
			get { return _folder.RowOrder; }
			set { _folder.RowOrder = value; }
		}

		public bool UnderlineBox
		{
			get { return _underlineBox; }
			set
			{
				_underlineBox = value;
				if (_underlineBox)
					Padding = new Padding(0, 4, 0, 0);
				else if (IsActive)
					Padding = new Padding(2, 2, 2, 2);
				else
					Padding = new Padding(0, 0, 0, 0);
			}
		}
		#endregion

		public FolderBoxControl()
		{
			InitializeComponent();
			WallBinOptions = new WallBinOptions();
		}

		#region Formatting Event Handlers
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (IsActive)
			{
				Rectangle rect;
				if (e.ClipRectangle.Top == 0)
					rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height);
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
				e.Graphics.DrawLine(_boxDropHintPen, 0, 0, Width, 0);
		}

		private void ControlBorders_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rect;
			if (e.ClipRectangle.Top == 0)
				rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height);
			else
				rect = new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
			for (var i = 0; i < 1; i++)
			{
				ControlPaint.DrawBorder(e.Graphics, rect, _folder.BorderColor, ButtonBorderStyle.Solid);
				rect.X = rect.X + 1;
				rect.Y = rect.Y + 1;
				rect.Width = rect.Width - 2;
				rect.Height = rect.Height - 2;
			}
		}

		private void grFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (grFiles.Rows[e.RowIndex].Tag == null) return;
			var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
			if (file == null) return;
			var toolTipText = new List<string>();
			if (!string.IsNullOrEmpty(file.OriginalPath))
			{
				toolTipText.Add("Path: " + file.OriginalPath);
				if (file.PresentationProperties != null)
					toolTipText.Add(string.Format("Slide Size: {0} W = {1} H = {2}", new object[] { file.PresentationProperties.Orientation, file.PresentationProperties.Width.ToString("#.##"), file.PresentationProperties.Height.ToString("#.##") }));
				toolTipText.Add("Added: " + file.AddDate.ToString("M/dd/yy h:mm:ss tt"));
				if (file.ExpirationDateOptions.EnableExpirationDate && file.ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
					toolTipText.Add("Expires: " + file.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt"));
				else
					toolTipText.Add("Expires: No Expiration Date");
				if (!string.IsNullOrEmpty(file.SearchTags.AllTags))
					toolTipText.Add("Category Tags: " + file.SearchTags.AllTags);
				else
					toolTipText.Add("No Category Tags Assigned");
				if (!string.IsNullOrEmpty(file.CustomKeywords.AllTags))
					toolTipText.Add("Keyword Tags: " + file.CustomKeywords.AllTags);
			}
			else if (file.Type == FileTypes.LineBreak)
			{
				if (!string.IsNullOrEmpty(file.LineBreakProperties.Note))
					toolTipText.Add(file.LineBreakProperties.Note);
			}
			grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = string.Join(Environment.NewLine, toolTipText.ToArray());
		}

		private void grFiles_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex != 0) return;
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
					{
						((ColumnPanel)Parent).ResizePanel();
						Decorator.RefreshPanelHeight();
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

				_richTextControl.ForeColor = foreColor;
				if (WallBinOptions.AllowEdit && grFiles.SelectedRows.Count > 0 && grFiles.SelectedRows[0].Index == e.RowIndex)
					_richTextControl.BackColor = grFiles.DefaultCellStyle.SelectionBackColor;
				else if (WallBinOptions.AllowMultiSelect && grFiles.Rows[e.RowIndex].Selected)
					_richTextControl.BackColor = grFiles.DefaultCellStyle.SelectionBackColor;
				else
					_richTextControl.BackColor = grFiles.DefaultCellStyle.BackColor;

				#endregion

				#region Custom Draw
				if (image != null)
					e.Graphics.DrawImage(image, new Rectangle(e.CellBounds.X + imageLeft, e.CellBounds.Y + imageTop, imageWidth, imageHeight));
				if (!string.IsNullOrEmpty(text))
					e.Graphics.DrawImage(RichTextBoxPrinter.Print(_richTextControl, textWidth, textHeight), new Rectangle(e.CellBounds.X + textLeft, e.CellBounds.Y + textTop, textWidth, textHeight));
				#endregion
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
		#endregion

		#region Drag&Drop Event Handlers
		private void grFiles_DragEnter(object sender, DragEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			if (!e.Data.GetDataPresent(DataFormats.Serializable, true)) return;
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
				Decorator.ColumnDragEnter(Parent, e);
		}

		private void grFiles_DragOver(object sender, DragEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			if (_underlineRow)
			{
				var pt = grFiles.PointToClient(new Point(e.X + 1, e.Y + 1));
				var ht = grFiles.HitTest(pt.X, pt.Y);
				if (_currentDragOverRow != ht.RowIndex)
				{
					_currentDragOverRow = ht.RowIndex;
					grFiles.Refresh();
				}
			}
			else
				Decorator.ColumnDragOver(Parent, e);
		}

		private void grFiles_DragLeave(object sender, EventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			_underlineRow = false;
			_currentDragOverRow = -1;
			grFiles.Refresh();
		}

		private void grFiles_DragDrop(object sender, DragEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			var p = grFiles.PointToClient(new Point(e.X, e.Y));
			var ht = grFiles.HitTest(p.X, p.Y);
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
							var files = new List<FileLink>();
							var folders = new List<FolderLink>();
							foreach (object dragItem in (object[])data)
								if (dragItem != null)
								{
									if (dragItem.GetType() == typeof(FileLink))
										files.Add((FileLink)dragItem);
									else if (dragItem.GetType() == typeof(FolderLink))
										folders.Add((FolderLink)dragItem);
								}
							folders.Sort((x, y) => x.Folder.Name.CompareTo(y.Folder.Name));
							files.Sort((x, y) => x.File.Name.CompareTo(y.File.Name));
							foreach (var folder in folders)
								AddFolder(folder, ht.RowIndex);
							foreach (var file in files)
								AddFile(file, ht.RowIndex);
						}
						_containFiles = true;
						SetGridFont(SettingsManager.Instance.FontSize);
						SetGridSize();
						if (Parent != null)
						{
							((ColumnPanel)Parent).ResizePanel();
							Decorator.RefreshPanelHeight();
						}
						Decorator.Parent.StateChanged = true;
					}
					else
						Decorator.ColumnDragDrop(Parent, e);
				}
			}
			grFiles_DragLeave(null, null);
		}

		private void grFiles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			_hitTest = grFiles.HitTest(e.X, e.Y);
			if (_hitTest.Type == DataGridViewHitTestType.Cell && _containFiles)
			{
				_dragBox = new Rectangle(new Point(e.X - (SystemInformation.DragSize.Width / 2), e.Y - (SystemInformation.DragSize.Height / 2)),
										 SystemInformation.DragSize);
			}
			else
				_hitTest = DataGridView.HitTestInfo.Nowhere;
		}

		private void grFiles_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			if (_containFiles)
				Cursor = Cursors.Hand;
			if (((e.Button & MouseButtons.Left) != MouseButtons.Left)
				|| _hitTest == DataGridView.HitTestInfo.Nowhere
				|| _dragBox.Contains(e.X, e.Y))
				return;

			object dragData = null;
			if (grFiles.SelectedRows.Count > 0)
				dragData = grFiles.SelectedRows[0];
			if (dragData != null)
				grFiles.DoDragDrop(new DataObject(DataFormats.Serializable, dragData), DragDropEffects.Move);
			_hitTest = DataGridView.HitTestInfo.Nowhere;
		}

		private void laFolderName_MouseDown(object sender, MouseEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			_dragBox = new Rectangle(new Point(e.X - (SystemInformation.DragSize.Width / 2), e.Y - (SystemInformation.DragSize.Height / 2)),
									 SystemInformation.DragSize);
		}

		private void laFolderName_MouseMove(object sender, MouseEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			Cursor = Cursors.Default;
			if (((e.Button & MouseButtons.Left) != MouseButtons.Left)
				|| _dragBox.Contains(e.X, e.Y))
				return;
			grFiles.DoDragDrop(new DataObject(DataFormats.Serializable, this), DragDropEffects.Move);
		}
		#endregion

		#region Other GUI Routines
		private void laFolderName_Click(object sender, EventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			if (IsActive) return;
			MakeActive();
			labelControlText.Focus();
		}

		private void grFiles_MouseDown(object sender, MouseEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			if (!IsActive)
			{
				MakeActive();
				labelControlText.Focus();
			}
			else
			{
				var hitTest = grFiles.HitTest(e.X, e.Y);
				if (hitTest.Type != DataGridViewHitTestType.Cell)
					labelControlText.Focus();
			}
		}

		private void grFiles_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void grWindowFiles_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			e.Cancel = true;
			if (!WallBinOptions.AllowEdit) return;
			if (e.ColumnIndex != 0) return;
			var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
			if (file != null)
			{
				if (file.BannerProperties.Enable)
					return;
				_displayCellFont = grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font;
				grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = file.Type == FileTypes.LineBreak ? _noteFont : _editCellFont;
				grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = file.Name;
				e.Cancel = false;
			}
		}

		private void grWindowFiles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (!WallBinOptions.AllowEdit) return;
			if (e.ColumnIndex != 0) return;
			var file = grFiles.Rows[e.RowIndex].Tag as LibraryLink;
			if (file == null) return;
			if (file.BannerProperties.Enable)
				return;
			if (grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
			{
				if (!grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals(file.Name))
				{
					file.Name = grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
					Decorator.Parent.StateChanged = true;
				}
			}
			else
			{
				file.Name = string.Empty;
				Decorator.Parent.StateChanged = true;
			}
			grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = file.DisplayName + file.Note;
			grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = _displayCellFont;
			SetGridSize();
		}

		private void grFiles_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (WallBinOptions.AllowEdit)
			{
				if (e.Button != MouseButtons.Right || !IsActive) return;
				grFiles.Rows[e.RowIndex].Selected = true;
				ShowLinkProperties(e.Location);
			}
		}

		private void grFiles_SelectionChanged(object sender, EventArgs e)
		{
			if (WallBinOptions.AllowMultiSelect)
				Decorator.SelectLink(_folder.Identifier, (from DataGridViewRow row in grFiles.SelectedRows select row.Tag).OfType<LibraryLink>().ToArray(), ModifierKeys);
			if (WallBinOptions.AllowEdit)
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
			using (var form = new FormAddUrl())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					var file = new LibraryLink(_folder);
					file.Name = form.LinkName;
					file.RelativePath = form.LinkPath;
					file.Type = FileTypes.Url;
					file.InitBannerProperties();
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
					SetGridFont(SettingsManager.Instance.FontSize);
					SetGridSize();
					if (Parent != null)
					{
						((ColumnPanel)Parent).ResizePanel();
						Decorator.RefreshPanelHeight();
					}
					Decorator.Parent.StateChanged = true;
					UpdateButtonsStatus();
				}
			}
		}

		public void AddNetworkFolder()
		{
			using (var form = new FormAddNetworkFolder())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					var file = new LibraryLink(_folder);
					file.Name = form.LinkName;
					file.RelativePath = form.LinkPath;
					file.Type = FileTypes.Network;
					file.InitBannerProperties();
					if (grFiles.SelectedRows.Count > 0)
					{
						var rowIndex = grFiles.SelectedRows[0].Index;
						grFiles.Rows.Insert(rowIndex, file.DisplayName + file.Note);
						var row = grFiles.Rows[rowIndex];
						row.Tag = file;
						grFiles.ClearSelection();
					}
					else
					{
						var row = grFiles.Rows[grFiles.Rows.Add(file.DisplayName + file.Note)];
						row.Tag = file;
					}
					_containFiles = true;
					SetGridFont(SettingsManager.Instance.FontSize);
					SetGridSize();
					if (Parent != null)
					{
						((ColumnPanel)Parent).ResizePanel();
						Decorator.RefreshPanelHeight();
					}
					Decorator.Parent.StateChanged = true;
					UpdateButtonsStatus();
				}
			}
		}

		public void AddLineBreak()
		{
			if (grFiles.SelectedRows.Count > 0)
			{
				var file = new LibraryLink(_folder);
				file.Type = FileTypes.LineBreak;
				file.LineBreakProperties = new LineBreakProperties(file);
				file.LineBreakProperties.Font = new Font(_textFont, FontStyle.Regular);
				file.LineBreakProperties.BoldFont = new Font(_textFont, FontStyle.Bold);
				file.IsBold = true;
				file.InitBannerProperties();
				int rowIndex = grFiles.SelectedRows[0].Index;
				grFiles.Rows.Insert(rowIndex, file.DisplayName + file.Note);
				DataGridViewRow row = grFiles.Rows[rowIndex];
				row.Tag = file;
				grFiles.ClearSelection();
				SetGridSize();
				if (Parent != null)
				{
					((ColumnPanel)Parent).ResizePanel();
					Decorator.RefreshPanelHeight();
				}
				Decorator.Parent.StateChanged = true;
				UpdateButtonsStatus();
			}
			else
				AppManager.Instance.ShowInfo("Select link above line break will be added");
		}

		public void DownLink()
		{
			var file = grFiles.SelectedRows[0].Tag as LibraryLink;
			string tempFileDisplayName = grFiles.SelectedRows[0].Cells[0].Value.ToString();

			grFiles.SuspendLayout();

			grFiles.SelectedRows[0].Cells[0].Value = grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Cells[0].Value;
			grFiles.SelectedRows[0].Tag = grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Tag;

			grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Cells[0].Value = tempFileDisplayName;
			grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Tag = file;

			grFiles.Rows[grFiles.SelectedRows[0].Index + 1].Selected = true;

			grFiles.ResumeLayout();

			UpdateButtonsStatus();
			Decorator.Parent.StateChanged = true;
		}

		public void UpLink()
		{
			var file = grFiles.SelectedRows[0].Tag as LibraryLink;
			var tempFileDisplayName = grFiles.SelectedRows[0].Cells[0].Value.ToString();

			grFiles.SuspendLayout();

			grFiles.SelectedRows[0].Cells[0].Value = grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Cells[0].Value;
			grFiles.SelectedRows[0].Tag = grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Tag;

			grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Cells[0].Value = tempFileDisplayName;
			grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Tag = file;

			grFiles.Rows[grFiles.SelectedRows[0].Index - 1].Selected = true;

			grFiles.ResumeLayout();

			UpdateButtonsStatus();
			Decorator.Parent.StateChanged = true;
		}

		public void ShowLinkProperties(Point cursorPosition)
		{
			if (grFiles.SelectedRows.Count <= 0) return;
			var file = grFiles.SelectedRows[0].Tag as LibraryLink;
			if (file == null) return;
			_formLinkProperties.CaptionName = string.IsNullOrEmpty(file.PropertiesName) && file.Type == FileTypes.LineBreak ? "Line Break" : file.PropertiesName;
			_formLinkProperties.IsBold = file.IsBold;
			_formLinkProperties.EnableWidget = file.EnableWidget;
			_formLinkProperties.Widget = file.EnableWidget ? file.Widget : null;
			_formLinkProperties.BannerProperties = file.BannerProperties;
			_formLinkProperties.IsLineBreak = file.Type == FileTypes.LineBreak;
			if (file.Type != FileTypes.LineBreak)
			{
				_formLinkProperties.Note = file.Note;
				_formLinkProperties.AddDate = file.AddDate;
				_formLinkProperties.ExpirationDateOptions = file.ExpirationDateOptions;
				_formLinkProperties.SearchTags = file.SearchTags;

				_formLinkProperties.Keywords.Clear();
				_formLinkProperties.Keywords.AddRange(file.CustomKeywords.Tags.Select(x => new StringDataSourceWrapper(x.Name)));

				_formLinkProperties.FileCard = file.FileCard;
				_formLinkProperties.FileCardImportantInfo.Clear();
				_formLinkProperties.FileCardImportantInfo.AddRange(file.FileCard.Notes.Select(x => new StringDataSourceWrapper(x)));

				_formLinkProperties.AttachmentProperties = file.AttachmentProperties;
			}
			else
			{
				_formLinkProperties.LineBreakProperties = file.LineBreakProperties;
			}

			_formLinkProperties.rbSecurityAllowed.Checked = !file.IsRestricted;
			_formLinkProperties.rbSecurityDenied.Checked = file.IsRestricted && string.IsNullOrEmpty(file.AssignedUsers);
			_formLinkProperties.rbSecurityRestricted.Checked = file.IsRestricted && !string.IsNullOrEmpty(file.AssignedUsers);
			_formLinkProperties.memoEditSecurityUsers.EditValue = file.IsRestricted && !string.IsNullOrEmpty(file.AssignedUsers) ? file.AssignedUsers : (!string.IsNullOrEmpty(SettingsManager.Instance.DefaultLinkUsers) ? SettingsManager.Instance.DefaultLinkUsers : null);

			if (file.Type != FileTypes.Folder && file.Type != FileTypes.LineBreak && file.Type != FileTypes.Network && file.Type != FileTypes.Url && file.Type != FileTypes.Excel && file.Type != FileTypes.MediaPlayerVideo && file.Type != FileTypes.QuickTimeVideo)
			{
				_formLinkProperties.ckDoNotGeneratePreview.Checked = file.DoNotGeneratePreview;
				_formLinkProperties.ckDoNotGeneratePreview.Visible = true;
			}
			else
			{
				_formLinkProperties.ckDoNotGeneratePreview.Checked = false;
				_formLinkProperties.ckDoNotGeneratePreview.Visible = false;
			}

			_formLinkProperties.StartPosition = FormStartPosition.CenterScreen;
			if (_formLinkProperties.ShowDialog() != DialogResult.OK) return;
			file.LastChanged = DateTime.Now;
			file.Widget = _formLinkProperties.EnableWidget ? _formLinkProperties.Widget : null;
			file.EnableWidget = _formLinkProperties.EnableWidget;
			file.BannerProperties = _formLinkProperties.BannerProperties;

			file.IsRestricted = _formLinkProperties.rbSecurityDenied.Checked || _formLinkProperties.rbSecurityRestricted.Checked;
			if (_formLinkProperties.rbSecurityRestricted.Checked && _formLinkProperties.memoEditSecurityUsers.EditValue != null && !string.IsNullOrEmpty(_formLinkProperties.memoEditSecurityUsers.EditValue.ToString().Trim()))
			{
				file.AssignedUsers = _formLinkProperties.memoEditSecurityUsers.EditValue.ToString().Trim();
				SettingsManager.Instance.DefaultLinkUsers = file.AssignedUsers;
				SettingsManager.Instance.Save();
			}
			else
				file.AssignedUsers = null;

			if (file.Type != FileTypes.LineBreak)
			{
				file.IsBold = _formLinkProperties.IsBold;
				file.Note = _formLinkProperties.Note;
				file.SearchTags = _formLinkProperties.SearchTags;

				file.CustomKeywords.Tags.Clear();
				file.CustomKeywords.Tags.AddRange(_formLinkProperties.Keywords.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => new SearchTag(file.CustomKeywords.Name) { Name = x.Value }));

				file.ExpirationDateOptions = _formLinkProperties.ExpirationDateOptions;

				file.FileCard = _formLinkProperties.FileCard;
				file.FileCard.Notes.Clear();
				file.FileCard.Notes.AddRange(_formLinkProperties.FileCardImportantInfo.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => x.Value));

				file.AttachmentProperties = _formLinkProperties.AttachmentProperties;
				foreach (var attachment in file.AttachmentProperties.FilesAttachments)
					file.Parent.Parent.Parent.GetPreviewContainer(attachment.OriginalPath);
			}
			else
			{
				file.LineBreakProperties = _formLinkProperties.LineBreakProperties;
			}
			file.DoNotGeneratePreview = _formLinkProperties.ckDoNotGeneratePreview.Checked;

			grFiles.SelectedRows[0].Cells[0].Value = file.DisplayName + file.Note;

			bool widgetColumnVisible = (from DataGridViewRow row in grFiles.Rows select row.Tag as LibraryLink).Any(x => x.Widget != null || (WallBinOptions.ShowCategoryTags && x.HasCategories) || (WallBinOptions.ShowKeywordTags && x.HasKeywords) || (WallBinOptions.ShowFileCardTags && x.HasFileCard) || (WallBinOptions.ShowAttachmentTags && (x.HasFileAttachments || x.HasWebAttachments)) || (WallBinOptions.ShowSecurityTags && x.IsRestricted));
			_containsWidgets = widgetColumnVisible;

			SetGridSize();
			grFiles.Refresh();
			if (Parent != null)
			{
				((ColumnPanel)Parent).ResizePanel();
				Decorator.RefreshPanelHeight();
			}
			Decorator.Parent.StateChanged = true;
		}

		public void OpenLink()
		{
			if (grFiles.SelectedRows.Count > 0)
			{
				var file = grFiles.SelectedRows[0].Tag as LibraryLink;
				if (file != null)
				{
					try
					{
						Process.Start(file.OriginalPath);
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
					var file = grFiles.SelectedRows[0].Tag as LibraryLink;
					if (file != null)
					{
						if (file.Type == FileTypes.BuggyPresentation || file.Type == FileTypes.FriendlyPresentation || file.Type == FileTypes.Presentation)
						{
							if (file.PreviewContainer == null)
								file.PreviewContainer = new PresentationPreviewContainer(file);
							file.PreviewContainer.ClearContent();
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
			if (Parent != null)
			{
				((ColumnPanel)Parent).ResizePanel();
				Decorator.RefreshPanelHeight();
			}
			Decorator.Parent.StateChanged = true;
			UpdateButtonsStatus();
			grFiles.DefaultCellStyle.SelectionBackColor = IsActive ? Color.Wheat : _folder.BackgroundWindowColor;
		}

		public void Save()
		{
			_folder.Files.Clear();
			if (!_containFiles) return;
			foreach (DataGridViewRow row in grFiles.Rows)
			{
				var file = row.Tag as LibraryLink;
				if (file == null) continue;
				if (file.LastChanged == DateTime.MinValue)
					_folder.LastChanged = DateTime.Now;
				file.Order = row.Index;
				_folder.Files.Add(file);
			}
		}
		#endregion

		#region Other Methods
		public void Init()
		{
			Resize += FolderBoxControl_Resize;
			grFiles.CellMouseMove += grFiles_CellMouseMove;
			grFiles.CellMouseLeave += grFiles_CellMouseLeave;
			grFiles.CellPainting += grFiles_CellPainting;
			grFiles.CellFormatting += grFiles_CellFormatting;
			grFiles.DragOver += (s, eParameter) => eParameter.Effect = DragDropEffects.All;
			Decorator.SelectionChanged += (sender, e) =>
											  {
												  if (grFiles.SelectedRows.Count > 0 && !e.SourceFolderId.Equals(_folder.Identifier))
												  {
													  grFiles.SelectionChanged -= grFiles_SelectionChanged;
													  foreach (DataGridViewRow row in grFiles.SelectedRows)
													  {
														  var file = row.Tag as LibraryLink;
														  row.Selected = Decorator.IsLinkSelected(file);
													  }
													  grFiles.SelectionChanged += grFiles_SelectionChanged;
												  }
											  };
		}

		private void FolderBoxControl_Resize(object sender, EventArgs e)
		{
			SetHeaderSize();
			SetGridSize();
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
			else if (WallBinOptions.ShowCategoryTags && file.HasCategories)
				image = Properties.Resources.TagsCategoriesWidget;
			else if (WallBinOptions.ShowKeywordTags && file.HasKeywords)
				image = Properties.Resources.TagsKeywordsWidget;
			else if (WallBinOptions.ShowFileCardTags && file.HasFileCard)
				image = Properties.Resources.TagsFileCardsWidget;
			else if (WallBinOptions.ShowAttachmentTags && file.HasFileAttachments)
				image = Properties.Resources.TagsFileAttachmentsWidget;
			else if (WallBinOptions.ShowAttachmentTags && file.HasWebAttachments)
				image = Properties.Resources.TagsWebAttachmentsWidget;
			else if (WallBinOptions.ShowSecurityTags && file.IsRestricted)
				image = Properties.Resources.TagsSecurityWidget;
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
			else if (WallBinOptions.ShowTagsEditor && file.HasTags && image != null)
			{
				imageLeft = 0;
				imageWidth = DefaultImageWidth;
				imageHeight = DefaultImageHeight;
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
			int height = 0;
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

					row.Height = rowHeight;
					if (maxColumnWidth < columnWidth)
						maxColumnWidth = columnWidth;
				}
				height += row.Height;
			}
			height += (int)(_noteFont.Size + 5);
			if (height < 90)
				height = 90;
			height = height + pnHeaderBorder.Height;

			Height = height;
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
			grFiles.RowsRemoved -= grFiles_RowsRemoved;
			grFiles.Rows.Clear();
			grFiles.RowsRemoved += grFiles_RowsRemoved;
			if (_folder.Files.Count > 0)
			{
				_containFiles = true;
				foreach (LibraryLink libraryFile in _folder.Files)
				{
					DataGridViewRow row = grFiles.Rows[grFiles.Rows.Add(libraryFile.DisplayName + libraryFile.Note)];
					row.Tag = libraryFile;
				}
				_containsWidgets = _folder.Files.OfType<LibraryLink>().Any(x => x.Widget != null || (WallBinOptions.ShowCategoryTags && x.HasCategories) || (WallBinOptions.ShowKeywordTags && x.HasKeywords) || (WallBinOptions.ShowFileCardTags && x.HasFileCard) || (WallBinOptions.ShowAttachmentTags && (x.HasFileAttachments || x.HasWebAttachments)) || (WallBinOptions.ShowSecurityTags && x.IsRestricted));
			}
			else
				_containFiles = false;
			if (Parent != null)
			{
				((ColumnPanel)Parent).ResizePanel();
				Decorator.RefreshPanelHeight();
			}
			grFiles.ClearSelection();
			grFiles.Refresh();
		}

		private void MakeActive()
		{
			IsActive = true;
			if (Decorator.ActiveBox != null && Decorator.ActiveBox != this)
				Decorator.ActiveBox.MakeInactive();
			Decorator.ActiveBox = this;
			Padding = new Padding(2, 2, 2, 2);
			grFiles.ReadOnly = false;
			grFiles.DefaultCellStyle.SelectionBackColor = Color.Wheat;
			Refresh();
			UpdateButtonsStatus();
		}

		public void MakeInactive()
		{
			IsActive = false;
			grFiles.ReadOnly = true;
			Padding = new Padding(0, 0, 0, 0);
			grFiles.DefaultCellStyle.SelectionBackColor = grFiles.DefaultCellStyle.BackColor;
			Decorator.ActiveBox.Refresh();
			UpdateButtonsStatus();
			Decorator.ActiveBox = null;
		}

		public void ApplyWallBinOptions(WallBinOptions options)
		{
			WallBinOptions.AllowEdit = options.AllowEdit;
			WallBinOptions.AllowMultiSelect = options.AllowMultiSelect;
			WallBinOptions.ShowFiles = options.ShowFiles;
			WallBinOptions.ShowTagsEditor = options.ShowTagsEditor;
			WallBinOptions.ShowCategoryTags = options.ShowCategoryTags;
			WallBinOptions.ShowKeywordTags = options.ShowKeywordTags;
			WallBinOptions.ShowFileCardTags = options.ShowFileCardTags;
			WallBinOptions.ShowAttachmentTags = options.ShowAttachmentTags;
			WallBinOptions.ShowSecurityTags = options.ShowSecurityTags;
			grFiles.MultiSelect = WallBinOptions.AllowMultiSelect && (WallBinOptions.ShowCategoryTags || WallBinOptions.ShowFileCardTags || WallBinOptions.ShowKeywordTags || WallBinOptions.ShowSecurityTags);
			grFiles.DefaultCellStyle.SelectionBackColor = WallBinOptions.AllowEdit ? grFiles.DefaultCellStyle.BackColor : Color.Wheat;
			grFiles.ClearSelection();
			_containsWidgets = (from DataGridViewRow row in grFiles.Rows select row.Tag as LibraryLink).Any(x => x.Widget != null || (WallBinOptions.ShowCategoryTags && x.HasCategories) || (WallBinOptions.ShowKeywordTags && x.HasKeywords) || (WallBinOptions.ShowFileCardTags && x.HasFileCard) || (WallBinOptions.ShowAttachmentTags && (x.HasFileAttachments || x.HasWebAttachments)) || (WallBinOptions.ShowSecurityTags && x.IsRestricted));
		}

		private void AddFile(FileLink file, int rowIndex)
		{
			var isExisted = (from DataGridViewRow row in grFiles.Rows select row.Tag).OfType<LibraryLink>().Any(libraryFile => file.File.FullName.Equals(libraryFile.OriginalPath));
			if (!isExisted)
			{
				var libraryFile = new LibraryLink(_folder);
				libraryFile.Name = file.File.Name.Replace(file.File.Extension, string.Empty);
				libraryFile.RootId = file.RootId;

				var rootFolder = _folder.Parent.Parent.GetRootFolder(file.RootId);
				libraryFile.RelativePath = (rootFolder.IsDrive ? @"\" : string.Empty) + file.File.FullName.Replace(rootFolder.Folder.FullName, string.Empty);

				libraryFile.SetProperties();
				libraryFile.InitBannerProperties();

				var pathLength = libraryFile.RelativePath.Length;

				switch (libraryFile.Type)
				{
					case FileTypes.BuggyPresentation:
					case FileTypes.FriendlyPresentation:
					case FileTypes.Presentation:
						using (var form = new FormProgress())
						{
							FormMain.Instance.ribbonControl.Enabled = false;
							form.laProgress.Text = "Get Presentation Properties...";
							form.TopMost = true;

							var thread = new Thread(delegate()
														{
															if (PowerPointHelper.Instance.Connect())
															{
																libraryFile.GetPresentationProperties();
																PowerPointHelper.Instance.Disconnect();
															}
														});

							form.Show();

							thread.Start();

							while (thread.IsAlive)
								Application.DoEvents();

							form.Close();
							FormMain.Instance.ribbonControl.Enabled = true;
						}

						#region Compatibility with Desktop Sales Depot
						if (libraryFile.PreviewContainer == null)
							libraryFile.PreviewContainer = new PresentationPreviewContainer(libraryFile);
						#endregion

						break;
					case FileTypes.Other:
					case FileTypes.MediaPlayerVideo:
					case FileTypes.QuickTimeVideo:
						break;
				}

				libraryFile.Parent.Parent.Parent.GetPreviewContainer(libraryFile.OriginalPath);


				if ((pathLength + SettingsManager.Instance.DestinationPathLength) < WinAPIHelper.MAX_PATH)
				{
					if (rowIndex >= 0 && rowIndex < grFiles.RowCount)
					{
						grFiles.Rows.Insert(rowIndex, libraryFile.DisplayName + libraryFile.Note);
						var row = grFiles.Rows[rowIndex];
						row.Tag = libraryFile;
						grFiles.Rows[rowIndex].Selected = true;
					}
					else
					{
						var row = grFiles.Rows[grFiles.Rows.Add(libraryFile.DisplayName + libraryFile.Note)];
						row.Tag = libraryFile;
						grFiles.Rows[grFiles.RowCount - 1].Selected = true;
					}
					UpdateButtonsStatus();
				}
				else
					AppManager.Instance.ShowInfo("This file path is too long.\nTry changing the file name to a shorter name, or move this file up a level in your network folders");
			}
			else
				AppManager.Instance.ShowInfo("The file " + file.File.Name + " is already in a window");
		}

		private void AddFolder(FolderLink folder, int rowIndex)
		{
			bool isExisted = false;
			foreach (DataGridViewRow row in grFiles.Rows)
			{
				var libraryFile = row.Tag as LibraryLink;
				if (libraryFile != null)
				{
					if (folder.Folder.FullName.Equals(libraryFile.OriginalPath))
					{
						isExisted = true;
						break;
					}
				}
			}
			if (!isExisted)
			{
				var libraryFile = new LibraryFolderLink(_folder);
				libraryFile.Name = folder.Folder.Name;
				libraryFile.RootId = folder.RootId;

				RootFolder rootFolder = _folder.Parent.Parent.GetRootFolder(folder.RootId);
				libraryFile.RelativePath = (rootFolder.IsDrive ? @"\" : string.Empty) + folder.Folder.FullName.Replace(rootFolder.Folder.FullName, string.Empty);

				libraryFile.Type = FileTypes.Folder;
				libraryFile.InitBannerProperties();

				int pathLength = libraryFile.RelativePath.Length;
				if ((pathLength + SettingsManager.Instance.DestinationPathLength) < WinAPIHelper.MAX_PATH)
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
				AppManager.Instance.ShowInfo("The folder " + folder.Folder.Name + " is already in a window");
		}

		private void MoveFile(DataGridViewRow row, int rowIndex)
		{
			grFiles.RowsRemoved -= grFiles_RowsRemoved;
			row.DataGridView.Rows.Remove(row);
			grFiles.RowsRemoved += grFiles_RowsRemoved;
			if (rowIndex >= 0 && rowIndex < grFiles.RowCount)
				grFiles.Rows.Insert(rowIndex, row);
			else
				grFiles.Rows.Add(row);
			row.Selected = true;
			UpdateButtonsStatus();
		}

		private void UpdateButtonsStatus()
		{
			if (IsActive)
			{
				if (grFiles.SelectedRows.Count > 0)
				{
					try
					{
						FormMain.Instance.DeleteLinkButton = true;
						FormMain.Instance.OpenLinkButton = true;
						FormMain.Instance.LinkPropertiesButton = true;
						FormMain.Instance.LineBreakButton = true;
						FormMain.Instance.UpLinkButton = grFiles.SelectedRows[0].Index > 0;
						FormMain.Instance.DownLinkButton = grFiles.SelectedRows[0].Index < grFiles.Rows.Count - 1;
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