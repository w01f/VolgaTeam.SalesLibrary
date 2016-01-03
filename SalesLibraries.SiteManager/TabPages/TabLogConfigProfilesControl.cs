using System.ComponentModel;
using System.Windows.Forms;

namespace SalesLibraries.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabLinkConfigProfilesControl : UserControl
	{
		public TabLinkConfigProfilesControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}