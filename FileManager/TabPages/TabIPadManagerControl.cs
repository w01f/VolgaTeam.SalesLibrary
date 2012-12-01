using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using SalesDepot.CoreObjects.ToolClasses;

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

		public void buttonItemIPadSyncStatus_Click(object sender, EventArgs e)
		{
			FormMain.Instance.buttonItemIPadSyncEnabled.Checked = false;
			FormMain.Instance.buttonItemIPadSyncDisabled.Checked = false;
			(sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
		}

		public void buttonItemIPadSyncStatus_CheckedChanged(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null && PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.AllowToSave)
			{
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.IPadManager.Enabled = FormMain.Instance.buttonItemIPadSyncEnabled.Checked;
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadManager.UpdateControlsState();
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.StateChanged = true;
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
				using (ToolForms.FormProgressSyncFilesIPad form = new ToolForms.FormProgressSyncFilesIPad())
				{
					form.CloseAfterSync = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.CloseAfterSync;
					form.ProcessAborted += new EventHandler<EventArgs>((progressSender, progressE) =>
					{
						Globals.ThreadAborted = true;
					});
					FormMain.Instance.ribbonControl.Enabled = false;
					this.Enabled = false;
					SaveIPadSettings();
					Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
					{
						Globals.ThreadActive = true;
						Globals.ThreadAborted = false;
						AppManager.Instance.KillAutoFM();
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.PrepareForIPadSynchronize();
						if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
							BusinessClasses.LibraryManager.Instance.SynchronizeLibraryForIpad(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library);
						AppManager.Instance.RunAutoFM();
					}));
					form.Show();
					FormWindowState savedState = FormMain.Instance.WindowState;
					if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Library.MinimizeOnSync)
						FormMain.Instance.WindowState = FormWindowState.Minimized;
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						System.Windows.Forms.Application.DoEvents();
					}
					Globals.ThreadActive = false;
					Globals.ThreadAborted = false;
					form.Close();
					this.Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;

					if (form.CloseAfterSync)
						Application.Exit();
					else
						FormMain.Instance.WindowState = savedState;
				}
			}
		}

		public void buttonItemIPadVideo_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadManager.ConvertSelectedVideoFiles();
		}
	}
}
