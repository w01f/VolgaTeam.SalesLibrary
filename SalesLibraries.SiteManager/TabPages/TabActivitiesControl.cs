using System.ComponentModel;
using System.Windows.Forms;

namespace SalesLibraries.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabActivitiesControl : UserControl
	{
		public TabActivitiesControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}