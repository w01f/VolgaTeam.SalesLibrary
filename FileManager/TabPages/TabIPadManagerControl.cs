using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace FileManager.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabIPadManagerControl : UserControl
    {
        public TabIPadManagerControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void SaveIPadSettings()
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.StateChanged)
                {
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                }
            }
        }

        public void buttonEditIPadLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null && PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.AllowToSave)
            {
                string path = FormMain.Instance.buttonEditIPadLocation.EditValue != null ? FormMain.Instance.buttonEditIPadLocation.EditValue.ToString() : string.Empty;
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                {
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.IPadManager.SyncDestinationPath = path;
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadManager.UpdateControlsState();
                }
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.StateChanged = true;
            }
        }

        public void buttonEditIPadLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.IPadManager.SyncDestinationPath;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Directory.Exists(dialog.SelectedPath))
                        FormMain.Instance.buttonEditIPadLocation.EditValue = dialog.SelectedPath;
                }
            }
        }

        public void buttonEditIPadSite_EditValueChanged(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null && PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.AllowToSave)
            {
                string site = FormMain.Instance.buttonEditIPadSite.EditValue != null ? FormMain.Instance.buttonEditIPadSite.EditValue.ToString() : string.Empty;
                string login = FormMain.Instance.buttonEditIPadLogin.EditValue != null ? FormMain.Instance.buttonEditIPadLogin.EditValue.ToString() : string.Empty;
                string password = FormMain.Instance.buttonEditIPadPassword.EditValue != null ? FormMain.Instance.buttonEditIPadPassword.EditValue.ToString() : string.Empty;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.IPadManager.Website = site;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.IPadManager.Login = login;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.IPadManager.Password = password;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadManager.UpdateControlsState();
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.StateChanged = true;
            }
        }

        public void buttonItemIPadSyncFiles_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    FormMain.Instance.ribbonControl.Enabled = false;
                    this.Enabled = false;
                    SaveIPadSettings();
                    form.laProgress.Text = "Updating Files for the iPad..." + Environment.NewLine + "This might take a few minutes...";
                    form.TopMost = true;
                    Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        AppManager.Instance.KillAutoFM();
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.PrepareForIPadSynchronize();
                        BusinessClasses.LibraryManager.Instance.SynchronizeLibraryForIpad(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
                        AppManager.Instance.RunAutoFM();
                    }));
                    form.Show();
                    thread.Start();
                    while (thread.IsAlive)
                    {
                        Thread.Sleep(100);
                        System.Windows.Forms.Application.DoEvents();
                    }
                    form.Close();
                    this.Enabled = true;
                    FormMain.Instance.ribbonControl.Enabled = true;
                }

                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadManager.UpdateLibraryOnServer();
            }
        }

        public void buttonItemIPadVideo_Click(object sender, EventArgs e)
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadManager.ConvertSelectedVideoFiles();
        }
    }
}
