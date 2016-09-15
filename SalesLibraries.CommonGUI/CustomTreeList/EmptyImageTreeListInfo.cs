using System.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;

namespace SalesLibraries.CommonGUI.CustomTreeList
{
	class EmptyImageTreeListInfo : TreeListViewInfo
	{
		public EmptyImageTreeListInfo(TreeList treeList) : base(treeList) { }

		protected override Point GetDataBoundsLocation(TreeListNode node, int top)
		{
			var result = base.GetDataBoundsLocation(node, top);
			if (!((EmptyImageTreeList)TreeList).CanCheckNode(node))
				result.X -= RC.CheckBoxSize.Width;
			if (Size.Empty != RC.SelectImageSize && -1 == node.SelectImageIndex)
				result.X -= RC.SelectImageSize.Width;
			if (Size.Empty != RC.StateImageSize && -1 == node.StateImageIndex)
				result.X -= RC.StateImageSize.Width;
			return result;
		}

		protected override void CalcStateImage(RowInfo ri)
		{
			base.CalcStateImage(ri);
			if (Size.Empty != RC.StateImageSize && !((EmptyImageTreeList)TreeList).CanCheckNode(ri.Node))
				ri.StateImageLocation.X -= RC.CheckBoxSize.Width;
			if (Size.Empty != RC.StateImageSize && -1 == ri.Node.StateImageIndex)
				ri.StateImageLocation.X -= RC.StateImageSize.Width;
		}

		protected override void CalcSelectImage(RowInfo ri)
		{
			base.CalcSelectImage(ri);
			if (-1 == ri.Node.SelectImageIndex)
				ri.SelectImageLocation = Point.Empty;
		}
	}
}
