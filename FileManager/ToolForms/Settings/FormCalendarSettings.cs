using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using FileManager.Controllers;

namespace FileManager.ToolForms.Settings
{
	public partial class FormCalendarSettings : MetroForm
	{
		public FormCalendarSettings()
		{
			InitializeComponent();
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
			if (DialogResult == DialogResult.OK)
			{
				e.Cancel = false;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarBackColor = colorEditCalendarBackColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarBorderColor = colorEditCalendarBorderColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarHeaderBackColor = colorEditCalendarHeaderBackColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarHeaderForeColor = colorEditCalendarHeaderForeColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthHeaderBackColor = colorEditMonthHeaderBackColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthHeaderForeColor = colorEditMonthHeaderForeColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthBodyBackColor = colorEditMonthBodyBackColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthBodyForeColor = colorEditMonthBodyForeColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.SweepBackColor = colorEditSweepBackColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.SweepForeColor = colorEditSweepForeColor.Color;
				MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.DeadLinksForeColor = colorEditDeadLinksForeColor.Color;
				MainController.Instance.ActiveDecorator.Library.Save();
			}
		}

		private void FormCalendarSettings_Load(object sender, EventArgs e)
		{
			colorEditCalendarBackColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarBackColor;
			colorEditCalendarBorderColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarBorderColor;
			colorEditCalendarHeaderBackColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarHeaderBackColor;
			colorEditCalendarHeaderForeColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarHeaderForeColor;
			colorEditMonthHeaderBackColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthHeaderBackColor;
			colorEditMonthHeaderForeColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthHeaderForeColor;
			colorEditMonthBodyBackColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthBodyBackColor;
			colorEditMonthBodyForeColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthBodyForeColor;
			colorEditSweepBackColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.SweepBackColor;
			colorEditSweepForeColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.SweepForeColor;
			colorEditDeadLinksForeColor.Color = MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.DeadLinksForeColor;
		}

		private void buttonXResetColors_Click(object sender, EventArgs e)
		{
			MainController.Instance.ActiveDecorator.Library.OvernightsCalendar.ResetColors();
			FormCalendarSettings_Load(null, null);
		}
	}
}