using System.ComponentModel;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.TabPages
{
	[ToolboxItem(false)]
	public partial class TabIPadUsersControl : UserControl
	{
		public TabIPadUsersControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}