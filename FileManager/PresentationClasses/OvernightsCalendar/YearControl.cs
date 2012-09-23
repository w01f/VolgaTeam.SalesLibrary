using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.OvernightsCalendar
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class YearControl : DevExpress.XtraTab.XtraTabPage
    //public partial class YearControl : UserControl
    {
        private const int MonthsPadding = 10;
        private const int ColumnsCount = 4;
        private const int DaysInWeek = 7;

        public CalendarYear Data { get; private set; }
        public List<MonthControl> Months { get; private set; }
        public List<Panel> WeekDaysPanels { get; private set; }
        public bool ViewBuilded { get; set; }

        public YearControl(CalendarYear data)
        {
            InitializeComponent();
            this.Data = data;
            this.Months = new List<MonthControl>();
            this.WeekDaysPanels = new List<Panel>();
            this.Text = this.Data.Year.ToString();
            this.Resize += new System.EventHandler(YearControl_Resize);
        }

        public void BuildControls()
        {
            pnEmpty.BringToFront();

            pnMonths.Controls.Clear();
            this.Months.Clear();
            foreach (CalendarMonth month in this.Data.Months)
            {
                MonthControl monthControl = new MonthControl(month);
                this.Months.Add(monthControl);
                Application.DoEvents();
            }
            pnMonths.Controls.AddRange(this.Months.ToArray());

            pnWeekDays.Controls.Clear();
            this.WeekDaysPanels.Clear();
            for (int i = 0; i < ColumnsCount; i++)
            {
                Panel weekDayPanel = new Panel();
                for (int j = 0; j < DaysInWeek; j++)
                {
                    WeekDayHeaderControl weekDay = new WeekDayHeaderControl();
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
                this.WeekDaysPanels.Add(weekDayPanel);
            }
            pnWeekDays.Controls.AddRange(this.WeekDaysPanels.ToArray());

            RefreshColors(false);

            FitControls(false);

            pnMain.BringToFront();

            this.ViewBuilded = true;
        }

        public void FitControls(bool showEmptyPanel = true)
        {
            if (showEmptyPanel)
                pnEmpty.BringToFront();
            int rowsCount = this.Months.Count / ColumnsCount;
            int rowHeight = pnMonths.Height / rowsCount;
            int columnWidth = pnMonths.Width / ColumnsCount;
            for (int i = 0; i < ColumnsCount; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    MonthControl month = this.Months[(i * rowsCount) + j];
                    month.Top = (rowHeight * j) + MonthsPadding;
                    month.Left = (columnWidth * i) + MonthsPadding;
                    month.Height = rowHeight - (MonthsPadding * 2);
                    month.Width = columnWidth - (MonthsPadding * 2);
                    Application.DoEvents();
                }

                Panel weekDayPanel = this.WeekDaysPanels[i];
                weekDayPanel.Top = 0;
                weekDayPanel.Left = (columnWidth * i) + MonthsPadding + 1;
                weekDayPanel.Width = (columnWidth - (MonthsPadding * 2)) - 2;
                weekDayPanel.Height = weekDayPanel.Parent.Height;
                int weekDayHeaderWidht = weekDayPanel.Width / DaysInWeek;
                for (int j = 0; j < DaysInWeek; j++)
                {
                    WeekDayHeaderControl weekDayHeader = weekDayPanel.Controls[j] as WeekDayHeaderControl;
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
            foreach (Panel weekDayPanel in this.WeekDaysPanels)
            {
                weekDayPanel.BackColor = this.Data.Parent.CalendarHeaderBackColor;
                foreach (Control weekDayHeader in weekDayPanel.Controls)
                {
                    weekDayHeader.BackColor = this.Data.Parent.CalendarHeaderBackColor;
                    weekDayHeader.ForeColor = this.Data.Parent.CalendarHeaderForeColor;
                    weekDayHeader.Refresh();
                    Application.DoEvents();
                }
                weekDayPanel.Refresh();
            }
            pnWeekDays.BackColor = this.Data.Parent.CalendarHeaderBackColor;
            pnWeekDays.Refresh();

            pnMonths.BackColor = this.Data.Parent.CalendarBackColor;
            foreach (MonthControl month in this.Months)
            {
                month.RefreshColors();
                Application.DoEvents();
            }
            pnMonths.Refresh();
            this.Refresh();
            if (showEmptyPanel)
                pnMain.BringToFront();
        }

        public void RefreshFont()
        {
            foreach (Panel weekDayPanel in this.WeekDaysPanels)
                foreach (Control weekDayHeader in weekDayPanel.Controls)
                    weekDayHeader.Font = new System.Drawing.Font(weekDayHeader.Font.Name, ConfigurationClasses.SettingsManager.Instance.CalendarFontSize, weekDayHeader.Font.Style);

            foreach (MonthControl month in this.Months)
                month.RefreshFont();
        }

        private void YearControl_Resize(object sender, System.EventArgs e)
        {
            if (ViewBuilded)
                FitControls();
        }
    }
}
