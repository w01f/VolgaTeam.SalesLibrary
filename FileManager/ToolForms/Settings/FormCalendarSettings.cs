using System;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormCalendarSettings : Form
    {
        public FormCalendarSettings()
        {
            InitializeComponent();
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
