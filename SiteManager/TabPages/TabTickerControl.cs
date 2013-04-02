using System.ComponentModel;
using System.Windows.Forms;

namespace SalesDepot.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabTickerControl : UserControl
	{
		public TabTickerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}