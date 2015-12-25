using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Calendars;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CommonGUI.Calendars
{
	[ToolboxItem(false)]
	public sealed partial class DayControl : Label
	{
		private Cursor _storedCursor;

		public DayControl(CalendarDay data)
		{
			InitializeComponent();
			Data = data;
			Text = data.Date.ToString("dd");
		}

		public CalendarDay Data { get; private set; }

		public void RefreshView()
		{
			if (Data.IsSweepDay)
			{
				BackColor = Data.Parent.Parent.Parent.Parent.Settings.SweepBackColor;
				if (Data.LinkedFile == null)
					ForeColor = Data.Parent.Parent.Parent.Parent.Settings.DeadLinksForeColor;
				else
					ForeColor = Data.Parent.Parent.Parent.Parent.Settings.SweepForeColor;
			}
			else
			{
				BackColor = Data.Parent.Parent.Parent.Parent.Settings.MonthBodyBackColor;
				if (Data.LinkedFile == null)
					ForeColor = Data.Parent.Parent.Parent.Parent.Settings.DeadLinksForeColor;
				else
					ForeColor = Data.Parent.Parent.Parent.Parent.Settings.MonthBodyForeColor;
			}
			Refresh();
		}

		public void RefreshFont(int fontSize)
		{
			Font = new Font(Font.Name, fontSize, Font.Style);
		}

		private void DayControl_Click(object sender, EventArgs e)
		{
			Data.Reset();
			if (Data.LinkedFile != null)
				Utils.OpenFile(Data.LinkedFile.FullName);
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
			RefreshView();
		}
	}
}