using System.Windows.Forms;
using OvernightsCalendarViewer.BusinessClasses;
using OvernightsCalendarViewer.ConfigurationClasses;
using OvernightsCalendarViewer.PresentationClasses.OvernightsCalendar;

namespace OvernightsCalendarViewer.PresentationClasses.Decorators
{
	public class LibraryDecorator
	{
		private int _selectedPageIndex = -1;

		public LibraryDecorator(PackageDecorator parent, Library library)
		{
			Parent = parent;
			Library = library;
			OvernightsCalendar = new OvernightsCalendarControl(this);
		}

		public PackageDecorator Parent { get; private set; }
		public Library Library { get; set; }
		public OvernightsCalendarControl OvernightsCalendar { get; private set; }

		public void ApplyDecorator(bool firstRun = false)
		{
			if (Library.OvernightsCalendar.Enabled)
			{
				FormMain.Instance.ribbonTabItemCalendar.Visible = true;
				UpdateCalendarFontButtonsStatus();
				if (!FormMain.Instance.TabOvernightsCalendar.Controls.Contains(OvernightsCalendar))
					FormMain.Instance.TabOvernightsCalendar.Controls.Add(OvernightsCalendar);
				OvernightsCalendar.BringToFront();
			}
			else
				FormMain.Instance.ribbonTabItemCalendar.Visible = false;
			FormMain.Instance.ribbonControl.RecalcLayout();
		}

		#region Overnights Calendar Stuff
		public void BuildOvernightsCalendar()
		{
			Library.OvernightsCalendar.LoadYears();
			Application.DoEvents();
			if (!Library.OvernightsCalendar.Enabled) return;
			OvernightsCalendar.Build();
			Application.DoEvents();
		}

		public void UpdateCalendarFontButtonsStatus()
		{
			FormMain.Instance.buttonItemCalendarFontSizeLarger.Enabled = SettingsManager.Instance.CalendarFontSize < 14;
			FormMain.Instance.buttonItemCalendarFontSizeSmaler.Enabled = SettingsManager.Instance.CalendarFontSize > 10;
		}
		#endregion
	}
}