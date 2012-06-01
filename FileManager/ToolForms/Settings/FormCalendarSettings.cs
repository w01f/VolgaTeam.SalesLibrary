using System;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormCalendarSettings : Form
    {
        public FormCalendarSettings()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laCalendarBackColor.Font = new System.Drawing.Font(laCalendarBackColor.Font.FontFamily, laCalendarBackColor.Font.Size - 2, laCalendarBackColor.Font.Style);
                laCalendarBorderColor.Font = new System.Drawing.Font(laCalendarBorderColor.Font.FontFamily, laCalendarBorderColor.Font.Size - 2, laCalendarBorderColor.Font.Style);
                laCalendarHeaderColor.Font = new System.Drawing.Font(laCalendarHeaderColor.Font.FontFamily, laCalendarHeaderColor.Font.Size - 2, laCalendarHeaderColor.Font.Style);
                laCalendarHeaderForeColor.Font = new System.Drawing.Font(laCalendarHeaderForeColor.Font.FontFamily, laCalendarHeaderForeColor.Font.Size - 2, laCalendarHeaderForeColor.Font.Style);
                laDeadLinksForeColor.Font = new System.Drawing.Font(laDeadLinksForeColor.Font.FontFamily, laDeadLinksForeColor.Font.Size - 2, laDeadLinksForeColor.Font.Style);
                laMonthBodyBackColor.Font = new System.Drawing.Font(laMonthBodyBackColor.Font.FontFamily, laMonthBodyBackColor.Font.Size - 2, laMonthBodyBackColor.Font.Style);
                laMonthBodyForeColor.Font = new System.Drawing.Font(laMonthBodyForeColor.Font.FontFamily, laMonthBodyForeColor.Font.Size - 2, laMonthBodyForeColor.Font.Style);
                laMonthHeaderBackColor.Font = new System.Drawing.Font(laMonthHeaderBackColor.Font.FontFamily, laMonthHeaderBackColor.Font.Size - 2, laMonthHeaderBackColor.Font.Style);
                laMonthHeaderForeColor.Font = new System.Drawing.Font(laMonthHeaderForeColor.Font.FontFamily, laMonthHeaderForeColor.Font.Size - 2, laMonthHeaderForeColor.Font.Style);
                laSweepBackColor.Font = new System.Drawing.Font(laSweepBackColor.Font.FontFamily, laSweepBackColor.Font.Size - 2, laSweepBackColor.Font.Style);
                laSweepForeColor.Font = new System.Drawing.Font(laSweepForeColor.Font.FontFamily, laSweepForeColor.Font.Size - 2, laSweepForeColor.Font.Style);
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = false;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarBackColor = colorEditCalendarBackColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarBorderColor = colorEditCalendarBorderColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarHeaderBackColor = colorEditCalendarHeaderBackColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarHeaderForeColor = colorEditCalendarHeaderForeColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthHeaderBackColor = colorEditMonthHeaderBackColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthHeaderForeColor = colorEditMonthHeaderForeColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthBodyBackColor = colorEditMonthBodyBackColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthBodyForeColor = colorEditMonthBodyForeColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.SweepBackColor = colorEditSweepBackColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.SweepForeColor = colorEditSweepForeColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.DeadLinksForeColor = colorEditDeadLinksForeColor.Color;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
            }
        }

        private void FormCalendarSettings_Load(object sender, EventArgs e)
        {
            colorEditCalendarBackColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarBackColor;
            colorEditCalendarBorderColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarBorderColor;
            colorEditCalendarHeaderBackColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarHeaderBackColor;
            colorEditCalendarHeaderForeColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.CalendarHeaderForeColor;
            colorEditMonthHeaderBackColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthHeaderBackColor;
            colorEditMonthHeaderForeColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthHeaderForeColor;
            colorEditMonthBodyBackColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthBodyBackColor;
            colorEditMonthBodyForeColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.MonthBodyForeColor;
            colorEditSweepBackColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.SweepBackColor;
            colorEditSweepForeColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.SweepForeColor;
            colorEditDeadLinksForeColor.Color = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.DeadLinksForeColor;
        }

        private void buttonXResetColors_Click(object sender, EventArgs e)
        {
            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.ResetColors();
            FormCalendarSettings_Load(null, null);
        }
    }
}
