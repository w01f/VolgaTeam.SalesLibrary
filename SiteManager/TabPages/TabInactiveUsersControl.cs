using System.ComponentModel;
using System.Windows.Forms;

namespace SalesDepot.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabInactiveUsersControl : UserControl
	{
		public TabInactiveUsersControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}