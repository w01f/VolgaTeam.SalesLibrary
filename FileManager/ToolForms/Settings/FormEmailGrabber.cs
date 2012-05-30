using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormEmailGrabber : Form
    {
        public FormEmailGrabber()
        {
            InitializeComponent();
            spinEditGrabInterval.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditGrabInterval.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditGrabInterval.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditInboxSubFolder.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditInboxSubFolder.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditInboxSubFolder.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
        }

        private void FormApplicationSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableEmailGrabber = buttonXEnable.Checked;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EmailGrabInterval = (int)spinEditGrabInterval.Value;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.InboxSubFolder = textEditInboxSubFolder.EditValue != null ? textEditInboxSubFolder.EditValue.ToString() : string.Empty;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
            }
        }

        private void FormApplicationSettings_Load(object sender, EventArgs e)
        {
            buttonXEnable.Checked = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableEmailGrabber;
            buttonXDisable.Checked = !PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EnableEmailGrabber;
            spinEditGrabInterval.Value = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.EmailGrabInterval;
            textEditInboxSubFolder.EditValue = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.OvernightsCalendar.InboxSubFolder;
        }


        private void buttonXEnable_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null && !button.Checked)
            {
                buttonXEnable.Checked = false;
                buttonXDisable.Checked = false;
                button.Checked = true;
            }
        }

        private void buttonXEnable_CheckedChanged(object sender, EventArgs e)
        {
            laGrabInterval.Enabled  = buttonXEnable.Checked;
            spinEditGrabInterval.Enabled = buttonXEnable.Checked;

            laInboxSubFolder.Enabled = buttonXEnable.Checked;
            textEditInboxSubFolder.Enabled = buttonXEnable.Checked;
        }
    }
}
