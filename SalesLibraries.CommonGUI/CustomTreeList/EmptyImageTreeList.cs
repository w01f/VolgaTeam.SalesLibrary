using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;

namespace SalesLibraries.CommonGUI.CustomTreeList
{
	public class EmptyImageTreeList : TreeList
	{
		protected override TreeListViewInfo CreateViewInfo()
		{
			return new EmptyImageTreeListInfo(this);
		}

		protected override CheckNodeEventArgs RaiseBeforeCheckNode(TreeListNode node, System.Windows.Forms.CheckState prevState, System.Windows.Forms.CheckState state)
		{
			var e = base.RaiseBeforeCheckNode(node, prevState, state);
			e.CanCheck = CanCheckNode(e.Node);
			return e;
		}

		protected override void RaiseCustomDrawNodeCheckBox(CustomDrawNodeCheckBoxEventArgs e)
		{
			var canCheckNode = CanCheckNode(e.Node);
			if (canCheckNode)
				return;
			e.ObjectArgs.State = DevExpress.Utils.Drawing.ObjectState.Disabled;
			e.Handled = true;

			base.RaiseCustomDrawNodeCheckBox(e);
		}

		public bool CanCheckNode(TreeListNode node)
		{
			return node.Tag != null;
		}
	}
}
