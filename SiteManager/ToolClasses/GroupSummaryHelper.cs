using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using DevExpress.Utils.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SalesDepot.SiteManager.ToolClasses
{
	public class GroupSummaryHelper
	{
		public static ArrayList ExtractSummaryItems(GridView view)
		{
			var items = new ArrayList();
			foreach (GridSummaryItem si in view.GroupSummary.Cast<GridSummaryItem>().Where(si => si is GridGroupSummaryItem && si.SummaryType != DevExpress.Data.SummaryItemType.None))
				items.Add(si);
			return items;
		}

		public static void DrawBackground(RowObjectCustomDrawEventArgs e, GridView view)
		{
			var painter = e.Painter as GridGroupRowPainter;
			var info = e.Info as GridGroupRowInfo;
			int level = view.GetRowLevel(e.RowHandle);
			int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
			info.GroupText = string.Format("{0}: {1}", view.GroupedColumns[level].Caption,
				view.GetRowCellDisplayText(row, view.GroupedColumns[level]));
			e.Appearance.DrawBackground(e.Cache, info.Bounds);
			painter.ElementsPainter.GroupRow.DrawObject(info);
		}

		public static void DrawSummaryValues(RowObjectCustomDrawEventArgs e, GridView view, ArrayList items)
		{
			var values = view.GetGroupSummaryValues(e.RowHandle);
			foreach (GridGroupSummaryItem item in items)
			{
				var rect = GetColumnBounds(view, item);
				if (rect.IsEmpty) continue;
				var text = item.GetDisplayText(values[item], false);
				rect = CalcSummaryRect(text, e, view.Columns[item.FieldName]);
				e.Appearance.DrawString(e.Cache, text, rect);
			}
		}

		private static Rectangle GetColumnBounds(GridView view, GridGroupSummaryItem item)
		{
			var column = view.Columns[item.FieldName];
			return GetColumnBounds(column);
		}

		private static Rectangle GetColumnBounds(GridColumn column)
		{
			var gridInfo = column.View.GetViewInfo() as GridViewInfo;
			var colInfo = gridInfo.ColumnsInfo[column];
			return colInfo != null ? colInfo.Bounds : Rectangle.Empty;
		}

		private static Rectangle CalcSummaryRect(string text, RowObjectCustomDrawEventArgs e, GridColumn column)
		{
			var gridInfo = column.View.GetViewInfo() as GridViewInfo;
			var result = GetColumnBounds(column);
			SizeF sz = TextUtils.GetStringSize(e.Graphics, text, e.Appearance.Font);
			var width = Convert.ToInt32(sz.Width) + 1;
			if (!gridInfo.ViewRects.FixedLeft.IsEmpty)
			{
				var fixedLeftRight = gridInfo.ViewRects.FixedLeft.Right;
				var marginLeft = result.Right - width - fixedLeftRight;
				if (marginLeft < 0 && column.Fixed == FixedStyle.None)
					return Rectangle.Empty;
			}
			if (!gridInfo.ViewRects.FixedRight.IsEmpty)
			{
				var fixedRightLeft = gridInfo.ViewRects.FixedRight.Left;
				if (fixedRightLeft <= result.Right && column.Fixed == FixedStyle.None)
					return Rectangle.Empty;
			}
			result = FixLeftEdge(width, result);
			result.Width = result.Width;
			result.Y = e.Bounds.Y;
			result.Height = e.Bounds.Height - 2;

			return PreventSummaryTextOverlapping(e, result);
		}

		private static Rectangle FixLeftEdge(int width, Rectangle result)
		{
			var delta = (result.Width - width) / 2;
			if (delta <= 0) return result;
			result.X += delta;
			result.Width -= delta;
			return result;
		}

		private static Rectangle PreventSummaryTextOverlapping(RowObjectCustomDrawEventArgs e, Rectangle rect)
		{
			var gInfo = (GridGroupRowInfo)e.Info;
			var groupTextLocation = gInfo.ButtonBounds.Right + 10;
			var groupTextWidth = TextUtils.GetStringSize(e.Graphics, gInfo.GroupText, e.Appearance.Font).Width;
			var r = new Rectangle(groupTextLocation, 0, groupTextWidth, e.Info.Bounds.Height);
			if (r.Right <= rect.X) return rect;
			if (r.Right > rect.Right)
				rect.Width = 0;
			else
			{
				rect.Width -= r.Right - rect.X;
				rect.X = r.Right;
			}
			return rect;
		}
	}
}
