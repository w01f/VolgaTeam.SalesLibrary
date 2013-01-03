using System.ComponentModel;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.TabPages
{
	[ToolboxItem(false)]
	public partial class TabOvernightsCalendarControl : UserControl
	{
		public TabOvernightsCalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}