using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabOvernightsCalendarControl : UserControl
    {
        public TabOvernightsCalendarControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void buttonItemCalendarDisclaimer_Click(object sender, EventArgs e)
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.DisclaimerPath))
                Process.Start(ConfigurationClasses.SettingsManager.Instance.DisclaimerPath);
        }

        public void buttonItemCalendarFontLarger_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.CalendarFontSize++;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.FormatCalendar();
        }

        public void buttonItemCalendarFontSmaller_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.CalendarFontSize--;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.FormatCalendar();
        }

        public void buttonItemHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("oc");
        }
    }
}
