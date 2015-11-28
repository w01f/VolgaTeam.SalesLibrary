using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Calendars;

namespace SalesLibraries.CommonGUI.Calendars
{
	[ToolboxItem(false)]
	public partial class MonthControl : UserControl
	{
		private const int WeeksCount = 5;
		private const int DaysInWeek = 7;

		public MonthControl(CalendarMonth data)
		{
			InitializeComponent();
			Data = data;
			Days = new List<DayControl>();
			laMonthName.Text = Data.Name;
			Resize += MonthControl_Resize;
			BuildControls();
		}

		public CalendarMonth Data { get; private set; }
		public List<DayControl> Days { get; private set; }

		private void BuildControls()
		{
			pnDaysInternalContainer.Controls.Clear();
			Days.Clear();
			foreach (var day in Data.Days)
			{
				var dayControl = new DayControl(day);
				Days.Add(dayControl);
				Application.DoEvents();
			}
			pnDaysInternalContainer.Controls.AddRange(Days.ToArray());
		}

		private void FitControls()
		{
			var rowHeight = pnDaysInternalContainer.Height / WeeksCount;
			var columnWidth = pnDaysInternalContainer.Width / DaysInWeek;
			for (var i = 0; i < DaysInWeek; i++)
			{
				for (var j = 0; j < WeeksCount; j++)
				{
					var dayIndex = (j * DaysInWeek) + i;
					if (dayIndex < Days.Count)
					{
						DayControl day = Days[dayIndex];
						day.Top = rowHeight * j;
						day.Left = columnWidth * i;
						day.Height = (j + 1) == WeeksCount ? (pnDaysInternalContainer.Height - (rowHeight * j)) : rowHeight;
						day.Width = (i + 1) == DaysInWeek ? (pnDaysInternalContainer.Width - (columnWidth * i)) : columnWidth;
						Application.DoEvents();
					}
					else
						break;
				}
			}
		}

		public void RefreshView()
		{
			laMonthName.BackColor = Data.Parent.Parent.Parent.Settings.MonthHeaderBackColor;
			laMonthName.ForeColor = Data.Parent.Parent.Parent.Settings.MonthHeaderForeColor;
			laMonthName.Refresh();

			pnDaysInternalContainer.BackColor = Data.Parent.Parent.Parent.Settings.MonthBodyBackColor;
			foreach (var day in Days)
				day.RefreshView();
			pnDaysInternalContainer.Refresh();
			Refresh();
		}

		public void RefreshFont(int fontSize)
		{
			laMonthName.Font = new Font(laMonthName.Font.Name, fontSize, laMonthName.Font.Style);
			foreach (var day in Days)
				day.RefreshFont(fontSize);
		}

		private void Control_Paint(object sender, PaintEventArgs e)
		{
			Rectangle rect;
			if (e.ClipRectangle.Top == 0)
				rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height);
			else
				rect = new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
			for (var i = 0; i < 1; i++)
			{
				ControlPaint.DrawBorder(e.Graphics, rect, Data.Parent.Parent.Parent.Settings.CalendarBorderColor, ButtonBorderStyle.Solid);
				rect.X = rect.X + 1;
				rect.Y = rect.Y + 1;
				rect.Width = rect.Width - 2;
				rect.Height = rect.Height - 2;
			}
		}

		private void MonthControl_Resize(object sender, EventArgs e)
		{
			FitControls();
		}
	}
}