using System.ComponentModel;
using System.Windows.Forms;

namespace SalesLibraries.SiteManager.TabPages
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