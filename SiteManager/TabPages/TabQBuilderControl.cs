using System.ComponentModel;
using System.Windows.Forms;

namespace SalesDepot.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabQBuilderControl : UserControl
	{
		public TabQBuilderControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}