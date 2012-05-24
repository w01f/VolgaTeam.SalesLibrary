using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabOvernightsCalendarControl : UserControl
    {
        private ToolForms.Settings.FormCalendarSettings _formSettings = null;
        private ToolForms.Settings.FormEmailGrabber _formEmailGrabber = null;

        public TabOvernightsCalendarControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void TabOvernightsCalendarControl_Load(object sender, EventArgs e)
        {
            _formSettings = new ToolForms.Settings.FormCalendarSettings();
            _formEmailGrabber = new ToolForms.Settings.FormEmailGrabber();
        }

        public void buttonItemCalendarSyncStatus_Click(object sender, EventArgs e)
        {
            FormMain.Instance.buttonItemCalendarSyncStatusDisabled.Checked = false;
            FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemCalendarSyncStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null && PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.AllowToSave)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled = FormMain.Instance.buttonItemCalendarSyncStatusEnabled.Checked;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableEmailGrabber &= PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
                FormMain.Instance.ribbonBarCalendarLocation.Enabled = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
                FormMain.Instance.ribbonBarCalendarSettings.Enabled = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
                FormMain.Instance.ribbonBarCalendarFont.Enabled = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
                FormMain.Instance.ribbonBarCalendarEmailGrabber.Enabled = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
                this.Enabled = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled;
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.Enabled)
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        formProgress.TopMost = true;
                        formProgress.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.BuildOvernightsCalendar(true);
                            });
                        }));
                        formProgress.Show();
                        Application.DoEvents();
                        thread.Start();
                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();
                        formProgress.Close();
                        FormMain.Instance.ribbonControl.Enabled = true;
                    }
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
            }
        }

        public void buttonEditCalendarLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null && PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.AllowToSave)
            {
                string path = FormMain.Instance.buttonEditCalendarLocation.EditValue != null ? FormMain.Instance.buttonEditCalendarLocation.EditValue.ToString() : string.Empty;
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.RootFolder = new DirectoryInfo(path);
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        formProgress.TopMost = true;
                        formProgress.laProgress.Text = "Chill-Out for a few seconds....\nLoading Your Overnights Calendar";
                        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.BuildOvernightsCalendar(true);
                            });
                        }));
                        formProgress.Show();
                        Application.DoEvents();
                        thread.Start();
                        while (thread.IsAlive)
                            System.Windows.Forms.Application.DoEvents();
                        formProgress.Close();
                        FormMain.Instance.ribbonControl.Enabled = true;
                    }
                }
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.StateChanged = true;
            }
        }

        public void buttonEditCalendarLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.RootFolder.FullName;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        FormMain.Instance.buttonEditCalendarLocation.EditValue = dialog.SelectedPath;
                }
            }
        }

        public void buttonItemCalendarSettings_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                if (_formSettings.ShowDialog() == DialogResult.OK)
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.OvernightsCalendar.RefreshColors();
            }
        }

        public void buttonItemCalendarFontUp_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.CalendarFontSize++;
            ConfigurationClasses.SettingsManager.Instance.Save();
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.UpdateCalendarFontButtonsStatus();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.OvernightsCalendar.RefreshFont();
            }
        }

        public void buttonItemCalendarFontDown_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.CalendarFontSize--;
            ConfigurationClasses.SettingsManager.Instance.Save();
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.UpdateCalendarFontButtonsStatus();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.OvernightsCalendar.RefreshFont();
            }
        }

        public void buttonItemCalendarEmailGrabber_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                _formEmailGrabber.ShowDialog();
            }
        }
    }
}
