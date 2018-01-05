using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.Wallbin.Views;

namespace SalesLibraries.CommonGUI.Wallbin.Folders
{
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	[ToolboxItem(false)]
	public partial class BaseFolderBox : UserControl
	{
		protected bool _outsideChangesInProgress;

		#region Public Properties
		public LibraryFolder DataSource { get; private set; }

		public Font RegularRowFont { get; private set; }

		public Font BoldRowFont { get; private set; }
		public Font BoldItalicRowFont { get; private set; }
		public Font BoldUnderlineRowFont { get; private set; }

		public Font ItalicRowFont { get; private set; }
		public Font ItalicUnderlineRowFont { get; private set; }

		public Font UnderlineRowFont { get; private set; }

		public Font BoldItalicUndrerlineRowFont { get; private set; }


		public RichTextBox RichTextControl { get; }

		public event EventHandler<EventArgs> BoxSizeChanged;

		public virtual IWallbinViewFormat FormatState
		{
			get { throw new NotImplementedException(); }
		}

		public virtual Color SelectedRowBackColor => DataSource.Settings.BackgroundWindowColor;
		#endregion

		public BaseFolderBox()
		{
			InitializeComponent();

			var scaleFactor = pnSeparator.Height * Utils.GetScaleFactor(CreateGraphics().DpiX).Height;
			if (scaleFactor > 1)
				pnSeparator.Height = (Int32) Math.Ceiling(5 * scaleFactor);
			else
				pnSeparator.Height = 0;
		}

		public BaseFolderBox(LibraryFolder dataSource)
		{
			InitializeComponent();

			grFiles.CellPainting += OnGridCellPainting;
			grFiles.CellFormatting += OnGridCellFormatting;
			Disposed += OnDispose;

			DataSource = dataSource;

			RichTextControl = new RichTextBox();
			RichTextControl.WordWrap = true;
			RichTextControl.ScrollBars = RichTextBoxScrollBars.None;

			UpdateFont();

			grFiles.RowTemplate = new LinkRow();
			grFiles.RowTemplate.CreateCells(grFiles);
			SetupView();
			LoadData();
		}

		#region Public Methods
		public virtual void UpdateContent(bool handleEvents)
		{
			SetupView();

			UpdateFont();

			_outsideChangesInProgress = !handleEvents;
			foreach (var linkRow in grFiles.Rows.OfType<LinkRow>())
				linkRow.Info.Recalc();
			UpdateGridSize();
			_outsideChangesInProgress = false;
		}

		public virtual void ReleaseControl()
		{
			_outsideChangesInProgress = true;
			grFiles.Rows.Clear();
			BoxSizeChanged = null;
			DataSource = null;
			_outsideChangesInProgress = false;
		}
		#endregion

		#region Protected Methods
		protected void OnDispose(object sender, EventArgs e)
		{
			ReleaseControl();
		}

		protected void LoadData()
		{
			_outsideChangesInProgress = true;
			foreach (var libraryLink in DataSource.Links.OrderBy(l => l.Order))
			{
				InsertLinkRow(libraryLink);
				Application.DoEvents();
			}
			_outsideChangesInProgress = false;
		}

		protected LinkRow InsertLinkRow(BaseLibraryLink source, int position = -1)
		{
			if (position == -1)
				position = grFiles.Rows.Add();
			else
				grFiles.Rows.Insert(position);
			var row = (LinkRow)grFiles.Rows[position];
			row.Init(source, this);
			row.Info.Recalc();
			row.InfoChanged += OnLinkRowInfoChanged;
			return row;
		}

		protected virtual void SetupView()
		{
			pnHeaderBorder.BackColor = DataSource.Settings.BorderColor;
			pnBorders.BackColor = DataSource.Settings.BorderColor;
			pnHeader.BackColor = DataSource.Settings.BackgroundHeaderColor;
			pnSeparator.BackColor = DataSource.Settings.BackgroundWindowColor;
			grFiles.BackgroundColor = DataSource.Settings.BackgroundWindowColor;
			grFiles.DefaultCellStyle.BackColor = DataSource.Settings.BackgroundWindowColor;
			grFiles.DefaultCellStyle.SelectionBackColor = SelectedRowBackColor;
		}

		public virtual void UpdateHeaderSize()
		{
			UpdateControlHeight();
		}

		protected void UpdateGridSize()
		{
			if(IsDisposed) return;
			var height = grFiles.Rows
				.OfType<LinkRow>()
				.Sum(row => row.Info.RowHeight);
			var maxColumnWidth = grFiles.Rows.Count > 0 ?
				grFiles.Rows
					.OfType<LinkRow>()
					.Max(row => row.Info.RowWidth) :
					0;

			height += (int)((BoldRowFont.Size + 5) * Utils.GetScaleFactor(CreateGraphics().DpiX).Height);
			if (height < 90) height = 90;
			grFiles.Height = height;
			colDisplayName.Width = maxColumnWidth > grFiles.Width ? maxColumnWidth : grFiles.Width;
			UpdateControlHeight();
			if (!_outsideChangesInProgress)
				BoxSizeChanged?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void UpdateControlHeight()
		{
			Height = pnHeaderBorder.Height +
				pnSeparator.Height +
				grFiles.Height +
				pnBorders.Padding.Top +
				pnBorders.Padding.Bottom +
				Padding.Top +
				Padding.Bottom;
		}

		protected void UpdateFont()
		{
			RegularRowFont = new Font(DataSource.Settings.WindowFont.FontFamily, FormatState.FontSize, DataSource.Settings.WindowFont.Style, GraphicsUnit.Point);
			BoldRowFont = new Font(DataSource.Settings.WindowFont.FontFamily, FormatState.FontSize, FontStyle.Bold, GraphicsUnit.Point);
			BoldItalicRowFont = new Font(DataSource.Settings.WindowFont.FontFamily, FormatState.FontSize, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
			BoldUnderlineRowFont = new Font(DataSource.Settings.WindowFont.FontFamily, FormatState.FontSize, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
			ItalicRowFont = new Font(DataSource.Settings.WindowFont.FontFamily, FormatState.FontSize, FontStyle.Italic, GraphicsUnit.Point);
			ItalicUnderlineRowFont = new Font(DataSource.Settings.WindowFont.FontFamily, FormatState.FontSize, FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point);
			UnderlineRowFont = new Font(DataSource.Settings.WindowFont.FontFamily, FormatState.FontSize, FontStyle.Underline, GraphicsUnit.Point);
			BoldItalicUndrerlineRowFont = new Font(DataSource.Settings.WindowFont.FontFamily, FormatState.FontSize, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Point);
			grFiles.DefaultCellStyle.Font = BoldItalicUndrerlineRowFont;
			grFiles.RowsDefaultCellStyle.Font = BoldItalicUndrerlineRowFont;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			var textWordWrappedRows = grFiles.Rows.OfType<LinkRow>().Where(r => r.Info.IsResponsible).ToList();
			if (textWordWrappedRows.Any())
			{
				grFiles.SuspendLayout();
				_outsideChangesInProgress = true;
				foreach (var linkRow in textWordWrappedRows)
					linkRow.Info.Recalc();
				_outsideChangesInProgress = false;
				UpdateGridSize();
				grFiles.ResumeLayout(true);
			}
		}
		#endregion

		#region Link Row Events
		protected void OnLinkRowInfoChanged(object sender, EventArgs e)
		{
			if (_outsideChangesInProgress) return;
			UpdateGridSize();
		}
		#endregion

		#region Formatting Event Handlers
		protected void OnGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			var hintText = linkRow.Source?.Hint;
			if (!String.IsNullOrEmpty(hintText) && !String.IsNullOrEmpty(hintText.Replace(Environment.NewLine, String.Empty)))
				grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = hintText;
			else
				grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = null;
			if (linkRow.Info.WordWrap)
			{
				e.Value = String.Empty;
				e.FormattingApplied = true;
			}
		}

		protected virtual void OnGridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex != 0) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			if (linkRow.Source == null) return;
			e.PaintBackground(e.CellBounds, true);

			#region Build RichTextControl
			RichTextControl.Text = linkRow.Info.Text;
			RichTextControl.Font = linkRow.Info.Font;
			RichTextControl.Height = linkRow.Info.TextBorder.Height;
			RichTextControl.Width = linkRow.Info.TextBorder.Width;

			var noteLength = !String.IsNullOrEmpty(linkRow.Source.DisplayName) ? linkRow.Source.DisplayName.Length - linkRow.Source.DisplayNameWithoutNote.Length : 0;
			if (noteLength > 0)
			{
				RichTextControl.SelectionStart = linkRow.Source.DisplayNameWithoutNote.Length;
				RichTextControl.SelectionLength = noteLength;
				RichTextControl.SelectionFont = BoldRowFont;
			}
			RichTextControl.ForeColor = linkRow.Info.ForeColor;
			RichTextControl.BackColor = linkRow.Info.BackColor;
			#endregion

			#region Custom Draw
			if (linkRow.Info.Image != null)
				e.Graphics.DrawImage(linkRow.Info.Image,
					new Rectangle(
						e.CellBounds.X + linkRow.Info.ImageBorder.Left,
						e.CellBounds.Y + linkRow.Info.ImageBorder.Top,
						linkRow.Info.ImageBorder.Width,
						linkRow.Info.ImageBorder.Height));
			if (!String.IsNullOrEmpty(linkRow.Info.Text))
				e.Graphics.DrawImage(RichTextBoxPrinter.Print(
					RichTextControl,
					linkRow.Info.TextBorder.Width,
					linkRow.Info.TextBorder.Height),
					new Rectangle(
						e.CellBounds.X + linkRow.Info.TextBorder.Left,
						e.CellBounds.Y + linkRow.Info.TextBorder.Top,
						linkRow.Info.TextBorder.Width,
						linkRow.Info.TextBorder.Height));
			#endregion

			e.Handled = true;
		}
		#endregion
	}
}