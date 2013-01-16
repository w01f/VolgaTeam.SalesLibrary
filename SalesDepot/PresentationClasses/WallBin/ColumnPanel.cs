using System.ComponentModel;
using System.Windows.Forms;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.PresentationClasses.WallBin
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
			FolderBoxControl box;
			int pnDWBHeight = 0;
			int borderWidth = 0;
			int panelWidth = 0;

			panelWidth = Parent.Width / (SettingsManager.Instance.ClassicView || SettingsManager.Instance.AccordionView ? 3 : 1);

			Width = panelWidth;
			for (int i = 0; i < Controls.Count; i++)
			{
				box = (FolderBoxControl)Controls[i];
				box.Width = panelWidth;
				box.Top = i > 0 ? Controls[i - 1].Bottom + borderWidth : borderWidth;
				if (pnDWBHeight < box.Bottom)
					pnDWBHeight = box.Bottom;
			}
			Height = pnDWBHeight + 15;
		}
	}
}