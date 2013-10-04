using System.ComponentModel;
using System.Windows.Forms;

namespace SalesDepot.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabUtilitiesControl : UserControl
	{
		public TabUtilitiesControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}