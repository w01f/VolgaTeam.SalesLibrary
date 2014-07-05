using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OvernightsCalendarViewer.BusinessClasses;
using OvernightsCalendarViewer.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace OvernightsCalendarViewer.PresentationClasses.OvernightsCalendar
{
	[ToolboxItem(false)]
	public partial class DayControl : Label
	{
		private Cursor _storedCursor;

		public DayControl(CalendarDay data)
		{
			InitializeComponent();
			Data = data;
			Text = data.Date.ToString("dd");
			RefreshColors();
			RefreshFont();
		}

		public CalendarDay Data { get; private set; }

		public void RefreshColors()
		{
			if (Data.IsSweepDay)
			{
				BackColor = Data.Parent.Parent.Parent.Parent.SweepBackColor;
				if (Data.LinkedFile == null)
					ForeColor = Data.Parent.Parent.Parent.Parent.DeadLinksForeColor;
				else
					ForeColor = Data.Parent.Parent.Parent.Parent.SweepForeColor;
			}
			else
			{
				BackColor = Data.Parent.Parent.Parent.Parent.MonthBodyBackColor;
				if (Data.LinkedFile == null)
					ForeColor = Data.Parent.Parent.Parent.Parent.DeadLinksForeColor;
				else
					ForeColor = Data.Parent.Parent.Parent.Parent.MonthBodyForeColor;
			}
			Refresh();
		}

		public void RefreshFont()
		{
			Font = new Font(Font.Name, SettingsManager.Instance.CalendarFontSize, Font.Style);
		}

		private void DayControl_Click(object sender, EventArgs e)
		{
			if (Data.LinkedFile != null)
			{
				var libraryFile = new LibraryLink(new LibraryFolder(new LibraryPage(Data.Parent.Parent.Parent.Parent.Parent)));
				libraryFile.OriginalPath = Data.LinkedFile.FullName;
				libraryFile.Type = FileTypes.OvernightsLink;
				LinkManager.Instance.OpenLink(libraryFile);
			}
		}

		private void DayControl_MouseEnter(object sender, EventArgs e)
		{
			_storedCursor = Cursor;
			Cursor = Cursors.Hand;
		}

		private void DayControl_MouseLeave(object sender, EventArgs e)
		{
			Cursor = _storedCursor;
		}

		private void DayControl_MouseDown(object sender, MouseEventArgs e)
		{
			BackColor = Color.Blue;
			ForeColor = Color.White;
		}

		private void DayControl_MouseUp(object sender, MouseEventArgs e)
		{
			RefreshColors();
		}
	}
}