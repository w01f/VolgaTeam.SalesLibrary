using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Calendars;

namespace SalesLibraries.CommonGUI.Calendars
{
	[ToolboxItem(false)]
	public sealed partial class YearControl : XtraTabPage
	//public partial class YearControl : UserControl
	{
		private const int MonthsPadding = 10;
		private const int ColumnsCount = 4;
		private const int DaysInWeek = 7;

		public YearControl(CalendarYear data)
		{
			InitializeComponent();
			Data = data;
			Months = new List<MonthControl>();
			WeekDaysPanels = new List<Panel>();
			Text = Data.Year.ToString();
			Resize += YearControl_Resize;
		}

		public CalendarYear Data { get; private set; }
		public List<MonthControl> Months { get; private set; }
		public List<Panel> WeekDaysPanels { get; private set; }
		public bool ViewBuilded { get; set; }

		public void BuildControls()
		{
			pnEmpty.BringToFront();

			pnMonths.Controls.Clear();
			Months.Clear();
			foreach (var month in Data.Months)
			{
				var monthControl = new MonthControl(month);
				Months.Add(monthControl);
				Application.DoEvents();
			}
			pnMonths.Controls.AddRange(Months.ToArray());

			pnWeekDays.Controls.Clear();
			WeekDaysPanels.Clear();
			for (int i = 0; i < ColumnsCount; i++)
			{
				var weekDayPanel = new Panel();
				for (int j = 0; j < DaysInWeek; j++)
				{
					var weekDay = new WeekDayHeaderControl();
					switch (j)
					{
						case 0:
							weekDay.Text = "M";
							break;
						case 1:
							weekDay.Text = "T";
							break;
						case 2:
							weekDay.Text = "W";
							break;
						case 3:
							weekDay.Text = "T";
							break;
						case 4:
							weekDay.Text = "F";
							break;
						case 5:
							weekDay.Text = "S";
							break;
						case 6:
							weekDay.Text = "S";
							break;
					}
					weekDayPanel.Controls.Add(weekDay);
					Application.DoEvents();
				}
				WeekDaysPanels.Add(weekDayPanel);
			}
			pnWeekDays.Controls.AddRange(WeekDaysPanels.ToArray());

			RefreshView(false);

			FitControls(false);

			pnMain.BringToFront();

			ViewBuilded = true;
		}

		public void FitControls(bool showEmptyPanel = true)
		{
			if (showEmptyPanel)
				pnEmpty.BringToFront();
			var rowsCount = Months.Count / ColumnsCount;
			var rowHeight = pnMonths.Height / rowsCount;
			var columnWidth = pnMonths.Width / ColumnsCount;
			for (var i = 0; i < ColumnsCount; i++)
			{
				for (var j = 0; j < rowsCount; j++)
				{
					var month = Months[(i * rowsCount) + j];
					month.Top = (rowHeight * j) + MonthsPadding;
					month.Left = (columnWidth * i) + MonthsPadding;
					month.Height = rowHeight - (MonthsPadding * 2);
					month.Width = columnWidth - (MonthsPadding * 2);
					Application.DoEvents();
				}

				var weekDayPanel = WeekDaysPanels[i];
				weekDayPanel.Top = 0;
				weekDayPanel.Left = (columnWidth * i) + MonthsPadding + 1;
				weekDayPanel.Width = (columnWidth - (MonthsPadding * 2)) - 2;
				weekDayPanel.Height = weekDayPanel.Parent.Height;
				var weekDayHeaderWidht = weekDayPanel.Width / DaysInWeek;
				for (var j = 0; j < DaysInWeek; j++)
				{
					var weekDayHeader = weekDayPanel.Controls[j];
					weekDayHeader.Top = 0;
					weekDayHeader.Left = weekDayHeaderWidht * j;
					weekDayHeader.Height = weekDayPanel.Height;
					weekDayHeader.Width = weekDayHeaderWidht;
					Application.DoEvents();
				}
			}
			if (showEmptyPanel)
				pnMain.BringToFront();
		}

		public void RefreshView(bool showEmptyPanel = true)
		{
			if (showEmptyPanel)
				pnEmpty.BringToFront();
			foreach (var weekDayPanel in WeekDaysPanels)
			{
				weekDayPanel.BackColor = Data.Parent.Parent.Settings.CalendarHeaderBackColor;
				foreach (Control weekDayHeader in weekDayPanel.Controls)
				{
					weekDayHeader.BackColor = Data.Parent.Parent.Settings.CalendarHeaderBackColor;
					weekDayHeader.ForeColor = Data.Parent.Parent.Settings.CalendarHeaderForeColor;
					weekDayHeader.Refresh();
					Application.DoEvents();
				}
				weekDayPanel.Refresh();
			}
			pnWeekDays.BackColor = Data.Parent.Parent.Settings.CalendarHeaderBackColor;
			pnWeekDays.Refresh();

			pnMonths.BackColor = Data.Parent.Parent.Settings.CalendarBackColor;
			foreach (var month in Months)
			{
				month.RefreshView();
				Application.DoEvents();
			}
			pnMonths.Refresh();
			Refresh();
			if (showEmptyPanel)
				pnMain.BringToFront();
		}

		public void RefreshFont(int fontSize)
		{
			foreach (var weekDayPanel in WeekDaysPanels)
				foreach (Control weekDayHeader in weekDayPanel.Controls)
					weekDayHeader.Font = new Font(weekDayHeader.Font.Name, fontSize, weekDayHeader.Font.Style);

			foreach (var month in Months)
				month.RefreshFont(fontSize);
		}

		private void YearControl_Resize(object sender, EventArgs e)
		{
			if (ViewBuilded)
				FitControls();
		}
	}
}