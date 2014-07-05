using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using FileManager.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.OvernightsCalendar
{
	[ToolboxItem(false)]
	public partial class YearControl : XtraTabPage
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

			RefreshFont();
			RefreshColors(false);

			FitControls(false);

			pnMain.BringToFront();

			ViewBuilded = true;
		}

		public void FitControls(bool showEmptyPanel = true)
		{
			if (showEmptyPanel)
				pnEmpty.BringToFront();
			int rowsCount = Months.Count / ColumnsCount;
			int rowHeight = pnMonths.Height / rowsCount;
			int columnWidth = pnMonths.Width / ColumnsCount;
			for (int i = 0; i < ColumnsCount; i++)
			{
				for (int j = 0; j < rowsCount; j++)
				{
					MonthControl month = Months[(i * rowsCount) + j];
					month.Top = (rowHeight * j) + MonthsPadding;
					month.Left = (columnWidth * i) + MonthsPadding;
					month.Height = rowHeight - (MonthsPadding * 2);
					month.Width = columnWidth - (MonthsPadding * 2);
					Application.DoEvents();
				}

				Panel weekDayPanel = WeekDaysPanels[i];
				weekDayPanel.Top = 0;
				weekDayPanel.Left = (columnWidth * i) + MonthsPadding + 1;
				weekDayPanel.Width = (columnWidth - (MonthsPadding * 2)) - 2;
				weekDayPanel.Height = weekDayPanel.Parent.Height;
				int weekDayHeaderWidht = weekDayPanel.Width / DaysInWeek;
				for (int j = 0; j < DaysInWeek; j++)
				{
					var weekDayHeader = weekDayPanel.Controls[j] as WeekDayHeaderControl;
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

		public void RefreshColors(bool showEmptyPanel = true)
		{
			if (showEmptyPanel)
				pnEmpty.BringToFront();
			foreach (Panel weekDayPanel in WeekDaysPanels)
			{
				weekDayPanel.BackColor = Data.Parent.Parent.CalendarHeaderBackColor;
				foreach (Control weekDayHeader in weekDayPanel.Controls)
				{
					weekDayHeader.BackColor = Data.Parent.Parent.CalendarHeaderBackColor;
					weekDayHeader.ForeColor = Data.Parent.Parent.CalendarHeaderForeColor;
					weekDayHeader.Refresh();
					Application.DoEvents();
				}
				weekDayPanel.Refresh();
			}
			pnWeekDays.BackColor = Data.Parent.Parent.CalendarHeaderBackColor;
			pnWeekDays.Refresh();

			pnMonths.BackColor = Data.Parent.Parent.CalendarBackColor;
			foreach (MonthControl month in Months)
			{
				month.RefreshColors();
				Application.DoEvents();
			}
			pnMonths.Refresh();
			Refresh();
			if (showEmptyPanel)
				pnMain.BringToFront();
		}

		public void RefreshFont()
		{
			foreach (var weekDayPanel in WeekDaysPanels)
				foreach (Control weekDayHeader in weekDayPanel.Controls)
					weekDayHeader.Font = new Font(weekDayHeader.Font.Name, SettingsManager.Instance.CalendarFontSize, weekDayHeader.Font.Style);

			foreach (var month in Months)
				month.RefreshFont();
		}

		private void YearControl_Resize(object sender, EventArgs e)
		{
			if (ViewBuilded)
				FitControls();
		}
	}
}