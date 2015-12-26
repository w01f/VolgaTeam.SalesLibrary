using System.ComponentModel;
using System.Windows.Forms;

namespace SalesLibraries.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabUsersControl : UserControl
	{
		public TabUsersControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}