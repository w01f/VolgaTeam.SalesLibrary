using System.ComponentModel;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.TabPages
{
	[ToolboxItem(false)]
	public partial class TabIPadContentControl : UserControl
	{
		public TabIPadContentControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}