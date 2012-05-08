using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.SettingsForms
{
    public partial class FormSync : Form
    {
        public FormSync()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ckMinimizeOnSync.Checked = PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.MinimizeOnSync;
            ckCloseAfterSync.Checked = PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.CloseAfterSync;
            ckShowSyncStatus.Checked = PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.ShowProgressDuringSync;
        }

        private void FormSync_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.MinimizeOnSync = ckMinimizeOnSync.Checked;
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.CloseAfterSync = ckCloseAfterSync.Checked;
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.ShowProgressDuringSync = ckShowSyncStatus.Checked;
                PresentationClasses.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.Save();
            }
        }
    }
}
