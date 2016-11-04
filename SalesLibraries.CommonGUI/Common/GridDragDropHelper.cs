using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CommonGUI.Common
{
	public class GridDragDropHelper : GridPainter
	{
		private readonly GridView _view;
		private GridHitInfo _downHitInfo;
		private Cursor _dragRowCursor;
		private int _dropTargetRowHandle = -1;
		private readonly int _rowVerticalOffset;
		private readonly bool _underlineHoverRow;
		private bool _draggedOutOfGrid;
		private readonly List<GridColumn> _handledColumns = new List<GridColumn>();

		public event DragEventHandler AfterDrop;

		public GridDragDropHelper(GridView view, bool underlineHoverRow, int rowVerticalOffset = 0, GridColumn[] handledColumns = null)
			: base(view)
		{
			_view = view;
			_rowVerticalOffset = rowVerticalOffset;
			_underlineHoverRow = underlineHoverRow;
			if (handledColumns != null)
				_handledColumns.AddRange(handledColumns);
			SubscribeEvents(view);
		}

		private int DropTargetRowHandle
		{
			get { return _dropTargetRowHandle; }
			set
			{
				_dropTargetRowHandle = value;
				_view.Invalidate();
			}
		}

		private void SubscribeEvents(GridView view)
		{
			view.GridControl.AllowDrop = true;
			view.MouseDown += view_MouseDown;
			view.MouseMove += view_MouseMove;
			view.MouseUp += view_MouseUp;
			view.ShownEditor += view_ShownEditor;
			view.GridControl.GiveFeedback += GridControl_GiveFeedback;
			view.GridControl.DragOver += GridControl_DragOver;
			view.GridControl.DragDrop += GridControl_DragDrop;
			view.GridControl.DragLeave += GridControl_DragLeave;
			view.GridControl.DragEnter += GridControl_DragEnter;
			view.GridControl.Paint += GridControl_Paint;
		}

		private void GridControl_Paint(object sender, PaintEventArgs e)
		{
			if (!_underlineHoverRow || _downHitInfo == null || DropTargetRowHandle < 0 || _draggedOutOfGrid) return;
			var grid = (GridControl)sender;
			var view = (GridView)grid.MainView;

			var isBottomLine = DropTargetRowHandle == view.DataRowCount;

			var viewInfo = view.GetViewInfo() as GridViewInfo;
			var rowInfo = viewInfo?.GetGridRowInfo(isBottomLine ? DropTargetRowHandle - 1 : DropTargetRowHandle);

			if (rowInfo == null) return;

			Point p1, p2;
			if (isBottomLine)
			{
				p1 = new Point(rowInfo.Bounds.Left, rowInfo.Bounds.Bottom - 1);
				p2 = new Point(rowInfo.Bounds.Right, rowInfo.Bounds.Bottom - 1);
			}
			else
			{
				p1 = new Point(rowInfo.Bounds.Left, rowInfo.Bounds.Top - 1);
				p2 = new Point(rowInfo.Bounds.Right, rowInfo.Bounds.Top - 1);
			}

			var pen = new Pen(Color.FromArgb(254, 164, 0), 3);
			e.Graphics.DrawLine(pen, p1, p2);
		}

		private void GridControl_GiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			if (_downHitInfo == null) return;
			if (_draggedOutOfGrid) return;
			e.UseDefaultCursors = false;
			Cursor.Current = _dragRowCursor;
		}

		private void GridControl_DragDrop(object sender, DragEventArgs e)
		{
			_downHitInfo = null;
			DropTargetRowHandle = -1;
			AfterDrop?.Invoke(sender, e);
		}

		private void GridControl_DragOver(object sender, DragEventArgs e)
		{
			var grid = (GridControl)sender;

			var pt = new Point(e.X, e.Y);
			pt = grid.PointToClient(pt);
			var view = grid.GetViewAt(pt) as GridView;
			if (view == null) return;

			var hitInfo = view.CalcHitInfo(pt);
			DropTargetRowHandle = hitInfo.HitTest == GridHitTest.EmptyRow ?
				view.DataRowCount :
				hitInfo.RowHandle;

			e.Effect = DropTargetRowHandle >= 0 ? DragDropEffects.Move : DragDropEffects.None;
		}

		private void GridControl_DragLeave(object sender, EventArgs e)
		{
			_draggedOutOfGrid = true;
		}

		private void GridControl_DragEnter(Object sender, DragEventArgs e)
		{
			_draggedOutOfGrid = false;
		}

		private void view_MouseMove(object sender, MouseEventArgs e)
		{
			var point = new Point(e.X, e.Y);
			if (sender is Control)
			{
				point = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
				point = _view.GridControl.PointToClient(point);
			}
			if (e.Button != MouseButtons.Left || _downHitInfo == null) return;
			var dragSize = SystemInformation.DragSize;
			var dragRect = new Rectangle(new Point(_downHitInfo.HitPoint.X - dragSize.Width / 2,
				_downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

			if (dragRect.Contains(point)) return;
			_dragRowCursor = GetDragCursor(_downHitInfo.RowHandle, point);

			var data = new DataObject();
			data.SetData(_downHitInfo);
			data.SetData(DataFormats.Serializable, _view.GetRow(_downHitInfo.RowHandle));

			_view?.GridControl.DoDragDrop(data, DragDropEffects.All);
			_downHitInfo = null;
		}

		private void view_MouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as GridView;
			_downHitInfo = null;
			DropTargetRowHandle = -1;
			_draggedOutOfGrid = false;
			if (view == null) return;
			var hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
			if (Control.ModifierKeys != Keys.None)
				return;
			if (e.Button == MouseButtons.Left &&
				(hitInfo.InRow || hitInfo.InRowCell) &&
				hitInfo.RowHandle != GridControl.NewItemRowHandle &&
				(!_handledColumns.Any() || _handledColumns.Any(hc => hc == hitInfo.Column)))
				_downHitInfo = hitInfo;
		}

		private void view_MouseUp(object sender, MouseEventArgs e)
		{
			_downHitInfo = null;
			DropTargetRowHandle = -1;
		}

		private void view_ShownEditor(object sender, EventArgs eventArgs)
		{
			var view = sender as GridView;
			if (view == null) return;
			view.ActiveEditor.MouseMove -= view_MouseMove;
			view.ActiveEditor.MouseMove += view_MouseMove;
		}

		private Cursor GetDragCursor(int rowHandle, Point e)
		{
			var info = (GridViewInfo)_view.GetViewInfo();
			var rowInfo = info.GetGridRowInfo(rowHandle);
			var imageBounds = new Rectangle(new Point(0, 0), rowInfo.TotalBounds.Size);
			var totalBounds = new Rectangle(new Point(0, 0), info.Bounds.Size);
			//Grid
			using (var bitmap = new Bitmap(totalBounds.Width, totalBounds.Height))
			using (var gridBitmap = new Bitmap(totalBounds.Width, totalBounds.Height))
			using (var cache = new GraphicsCache(Graphics.FromImage(bitmap)))
			using (var result = new Bitmap(imageBounds.Width, imageBounds.Height))
			using (var resultGraphics = Graphics.FromImage(result))
			using (var imageAttributes = new ImageAttributes())
			{
				_view.GridControl.DrawToBitmap(gridBitmap, _view.GridControl.Bounds);
				var args = new GridViewDrawArgs(cache, info, totalBounds);
				DrawRow(args, rowInfo);

				float[][] matrixItems =
				{
					new float[] { 1, 0, 0, 0, 0 },
					new float[] { 0, 1, 0, 0, 0 },
					new float[] { 0, 0, 1, 0, 0 },
					new[] { 0, 0, 0, 0.7f, 0 },
					new float[] { 0, 0, 0, 0, 1 }
				};
				var colorMatrix = new ColorMatrix(matrixItems);
				imageAttributes.SetColorMatrix(
					colorMatrix,
					ColorMatrixFlag.Default,
					ColorAdjustType.Bitmap);
				resultGraphics.DrawImage(gridBitmap, imageBounds, rowInfo.TotalBounds.X, rowInfo.TotalBounds.Y + _rowVerticalOffset, rowInfo.TotalBounds.Width, rowInfo.TotalBounds.Height, GraphicsUnit.Pixel, imageAttributes);
				var offset = new Point(e.X - rowInfo.TotalBounds.X, e.Y - rowInfo.TotalBounds.Y);
				return CreateCursor(result, offset);
			}
		}

		public static Cursor CreateCursor(Bitmap bmp, Point hotspot)
		{
			if (bmp == null) return Cursors.Default;
			var ptr = bmp.GetHicon();
			var tmp = new IconInfo();
			WinAPIHelper.GetIconInfo(ptr, ref tmp);
			tmp.IsIcon = false;
			tmp.xHotspot = hotspot.X;
			tmp.yHotspot = hotspot.Y;
			ptr = WinAPIHelper.CreateIconIndirect(ref tmp);
			return ptr == IntPtr.Zero ? Cursors.Default : new Cursor(ptr);
		}
	}
}
