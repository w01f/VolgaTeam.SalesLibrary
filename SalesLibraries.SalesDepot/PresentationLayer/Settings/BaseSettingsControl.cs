using System.Windows.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class BaseSettingsControl : UserControl
	{
		public BaseSettingsControl()
		{
			InitializeComponent();
		}

		public virtual void LoadData() {}
	}
}