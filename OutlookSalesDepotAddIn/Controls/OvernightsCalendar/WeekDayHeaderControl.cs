using System.Drawing;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.OvernightsCalendar
{
	[System.ComponentModel.ToolboxItem(false)]
	public sealed partial class WeekDayHeaderControl : Label
	{
		public WeekDayHeaderControl()
		{
			InitializeComponent();
			Font = new Font(Font.Name, SettingsManager.CalendarFontSize, Font.Style);
		}
	}
}
