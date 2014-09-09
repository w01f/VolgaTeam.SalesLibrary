using System.ComponentModel;
using System.Windows.Forms;

namespace OutlookSalesDepotAddIn.Controls.Wallbin
{
	[ToolboxItem(false)]
	public class ColumnPanel : Panel
	{
		public ColumnPanel()
		{
			Order = -1;
			BorderStyle = BorderStyle.None;
			MouseMove += PanelMouseMove;
		}

		public int Order { get; set; }

		private void PanelMouseMove(object sender, MouseEventArgs e)
		{
			Parent.Parent.Focus();
		}

		public void ResizePanel()
		{
			var pnDWBHeight = 0;
			const int borderWidth = 0;
			var panelWidth = 0;

			panelWidth = Parent.Width / 3;

			Width = panelWidth;
			for (int i = 0; i < Controls.Count; i++)
			{
				var box = (FolderBoxControl)Controls[i];
				box.Width = panelWidth;
				box.Top = i > 0 ? Controls[i - 1].Bottom + borderWidth : borderWidth;
				if (pnDWBHeight < box.Bottom)
					pnDWBHeight = box.Bottom;
			}
			Height = pnDWBHeight + 15;
		}
	}
}