using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Business.Entities.Wallbin.Persistent;

namespace SalesLibraries.FileManager.PresentationLayer.Calendars
{
	public partial class FormCalendarSettings : MetroForm
	{
		private readonly Library _library;
		public FormCalendarSettings(Library library)
		{
			InitializeComponent();
			_library = library;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laCalendarBackColor.Font = new Font(laCalendarBackColor.Font.FontFamily, laCalendarBackColor.Font.Size - 2, laCalendarBackColor.Font.Style);
				laCalendarBorderColor.Font = new Font(laCalendarBorderColor.Font.FontFamily, laCalendarBorderColor.Font.Size - 2, laCalendarBorderColor.Font.Style);
				laCalendarHeaderColor.Font = new Font(laCalendarHeaderColor.Font.FontFamily, laCalendarHeaderColor.Font.Size - 2, laCalendarHeaderColor.Font.Style);
				laCalendarHeaderForeColor.Font = new Font(laCalendarHeaderForeColor.Font.FontFamily, laCalendarHeaderForeColor.Font.Size - 2, laCalendarHeaderForeColor.Font.Style);
				laDeadLinksForeColor.Font = new Font(laDeadLinksForeColor.Font.FontFamily, laDeadLinksForeColor.Font.Size - 2, laDeadLinksForeColor.Font.Style);
				laMonthBodyBackColor.Font = new Font(laMonthBodyBackColor.Font.FontFamily, laMonthBodyBackColor.Font.Size - 2, laMonthBodyBackColor.Font.Style);
				laMonthBodyForeColor.Font = new Font(laMonthBodyForeColor.Font.FontFamily, laMonthBodyForeColor.Font.Size - 2, laMonthBodyForeColor.Font.Style);
				laMonthHeaderBackColor.Font = new Font(laMonthHeaderBackColor.Font.FontFamily, laMonthHeaderBackColor.Font.Size - 2, laMonthHeaderBackColor.Font.Style);
				laMonthHeaderForeColor.Font = new Font(laMonthHeaderForeColor.Font.FontFamily, laMonthHeaderForeColor.Font.Size - 2, laMonthHeaderForeColor.Font.Style);
				laSweepBackColor.Font = new Font(laSweepBackColor.Font.FontFamily, laSweepBackColor.Font.Size - 2, laSweepBackColor.Font.Style);
				laSweepForeColor.Font = new Font(laSweepForeColor.Font.FontFamily, laSweepForeColor.Font.Size - 2, laSweepForeColor.Font.Style);
			}
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = false;
			if (DialogResult != DialogResult.OK) return;
			e.Cancel = false;
			_library.Calendar.CalendarBackColor = colorEditCalendarBackColor.Color;
			_library.Calendar.CalendarBorderColor = colorEditCalendarBorderColor.Color;
			_library.Calendar.CalendarHeaderBackColor = colorEditCalendarHeaderBackColor.Color;
			_library.Calendar.CalendarHeaderForeColor = colorEditCalendarHeaderForeColor.Color;
			_library.Calendar.MonthHeaderBackColor = colorEditMonthHeaderBackColor.Color;
			_library.Calendar.MonthHeaderForeColor = colorEditMonthHeaderForeColor.Color;
			_library.Calendar.MonthBodyBackColor = colorEditMonthBodyBackColor.Color;
			_library.Calendar.MonthBodyForeColor = colorEditMonthBodyForeColor.Color;
			_library.Calendar.SweepBackColor = colorEditSweepBackColor.Color;
			_library.Calendar.SweepForeColor = colorEditSweepForeColor.Color;
			_library.Calendar.DeadLinksForeColor = colorEditDeadLinksForeColor.Color;
		}

		private void FormCalendarSettings_Load(object sender, EventArgs e)
		{
			colorEditCalendarBackColor.Color = _library.Calendar.CalendarBackColor;
			colorEditCalendarBorderColor.Color = _library.Calendar.CalendarBorderColor;
			colorEditCalendarHeaderBackColor.Color = _library.Calendar.CalendarHeaderBackColor;
			colorEditCalendarHeaderForeColor.Color = _library.Calendar.CalendarHeaderForeColor;
			colorEditMonthHeaderBackColor.Color = _library.Calendar.MonthHeaderBackColor;
			colorEditMonthHeaderForeColor.Color = _library.Calendar.MonthHeaderForeColor;
			colorEditMonthBodyBackColor.Color = _library.Calendar.MonthBodyBackColor;
			colorEditMonthBodyForeColor.Color = _library.Calendar.MonthBodyForeColor;
			colorEditSweepBackColor.Color = _library.Calendar.SweepBackColor;
			colorEditSweepForeColor.Color = _library.Calendar.SweepForeColor;
			colorEditDeadLinksForeColor.Color = _library.Calendar.DeadLinksForeColor;
		}

		private void buttonXResetColors_Click(object sender, EventArgs e)
		{
			_library.Calendar.ResetColors();
			FormCalendarSettings_Load(null, null);
		}
	}
}