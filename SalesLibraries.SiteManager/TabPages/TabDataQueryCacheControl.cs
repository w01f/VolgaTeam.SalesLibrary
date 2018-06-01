using System.ComponentModel;
using System.Windows.Forms;

namespace SalesLibraries.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabDataQueryCacheControl : UserControl
	{
		public TabDataQueryCacheControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}