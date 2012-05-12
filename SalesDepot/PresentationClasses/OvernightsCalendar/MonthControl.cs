using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.OvernightsCalendar
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MonthControl : UserControl
    {
        private const int WeeksCount = 5;
        private const int DaysInWeek = 7;

        public BusinessClasses.CalendarMonth Data { get; private set; }
        public List<DayControl> Days { get; private set; }

        public MonthControl(BusinessClasses.CalendarMonth data)
        {
            InitializeComponent();
            this.Data = data;
            this.Days = new List<DayControl>();
            laMonthName.Text = this.Data.Name;
            this.Resize += new EventHandler(MonthControl_Resize);
            BuildControls();
        }

        private void BuildControls()
        {
            pnDaysInternalContainer.Controls.Clear();
            this.Days.Clear();
            foreach (BusinessClasses.CalendarDay day in this.Data.Days)
            {
                DayControl dayControl = new DayControl(day);
                this.Days.Add(dayControl);
                Application.DoEvents();
            }
            pnDaysInternalContainer.Controls.AddRange(this.Days.ToArray());
        }

        private void FitControls()
        {
            int rowHeight = pnDaysInternalContainer.Height / WeeksCount;
            int columnWidth = pnDaysInternalContainer.Width / DaysInWeek;
            for (int i = 0; i < DaysInWeek; i++)
            {
                for (int j = 0; j < WeeksCount; j++)
                {
                    int dayIndex = (j * DaysInWeek) + i;
                    if (dayIndex < this.Days.Count)
                    {
                        DayControl day = this.Days[dayIndex];
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

        public void RefreshColors()
        {
            laMonthName.BackColor = this.Data.Parent.Parent.MonthHeaderBackColor;
            laMonthName.ForeColor = this.Data.Parent.Parent.MonthHeaderForeColor;
            laMonthName.Refresh();

            pnDaysInternalContainer.BackColor = this.Data.Parent.Parent.MonthBodyBackColor;
            foreach (DayControl day in this.Days)
                day.RefreshColors();
            pnDaysInternalContainer.Refresh();
            this.Refresh();
        }

        public void RefreshFont()
        {
            laMonthName.Font = new System.Drawing.Font(laMonthName.Font.Name, ConfigurationClasses.SettingsManager.Instance.CalendarFontSize, laMonthName.Font.Style);
            foreach (DayControl day in this.Days)
                day.RefreshFont();
        }

        private void Control_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect;
            if (e.ClipRectangle.Top == 0)
                rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, this.Height);
            else
                rect = new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
            for (int i = 0; i < 1; i++)
            {
                ControlPaint.DrawBorder(e.Graphics, rect, this.Data.Parent.Parent.CalendarBorderColor, ButtonBorderStyle.Solid);
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
