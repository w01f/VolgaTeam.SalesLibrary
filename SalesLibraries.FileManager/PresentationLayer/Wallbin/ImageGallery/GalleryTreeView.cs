using System.Drawing;
using System.Windows.Forms;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery
{
	sealed class GalleryTreeView : TreeView
	{
		public GalleryTreeView()
		{
			DrawMode = TreeViewDrawMode.OwnerDrawText;
			ItemHeight = 25;
			HideSelection = false;
			Cursor = Cursors.Hand;
			ShowLines = false;
		}

		protected override void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			if (e.Node == null) return;
			var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
			var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
			if (selected && Enabled)
			{
				e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
				TextRenderer.DrawText(e.Graphics,
					e.Node.Text,
					font,
					new Rectangle(e.Bounds.X, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height),
					SystemColors.HighlightText,
					TextFormatFlags.GlyphOverhangPadding);
			}
			else
			{
				e.Graphics.FillRectangle(Brushes.White, e.Bounds);
				TextRenderer.DrawText(e.Graphics,
					e.Node.Text,
					font, new Rectangle(e.Bounds.X, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4),
					Enabled ? Color.Black : Color.LightGray,
					TextFormatFlags.GlyphOverhangPadding);
			}
		}
	}
}
