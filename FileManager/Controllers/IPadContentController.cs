using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using FileManager.BusinessClasses;
using FileManager.PresentationClasses.TabPages;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.ToolForms;
using FileManager.ToolForms.IPad;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.Controllers
{
	public class IPadContentController : IPageController
	{
		private bool _initialization;
		private TabIPadContentControl _tabPage;

		public IPadContentController()
		{
			FormMain.Instance.buttonItemIPadSyncDisabled.Click += buttonItemIPadSyncStatus_Click;
			FormMain.Instance.buttonItemIPadSyncEnabled.Click += buttonItemIPadSyncStatus_Click;
			FormMain.Instance.buttonItemIPadSyncDisabled.CheckedChanged += buttonItemIPadSyncStatus_CheckedChanged;
			FormMain.Instance.buttonItemIPadSyncEnabled.CheckedChanged += buttonItemIPadSyncStatus_CheckedChanged;
			FormMain.Instance.buttonEditIPadLocation.EditValueChanged += buttonEditIPadLocation_EditValueChanged;
			FormMain.Instance.buttonEditIPadLocation.ButtonClick += buttonEditIPadLocation_ButtonClick;
			FormMain.Instance.buttonEditIPadSite.EditValueChanged += buttonEditIPadSite_EditValueChanged;
			FormMain.Instance.buttonEditIPadLogin.EditValueChanged += buttonEditIPadSite_EditValueChanged;
			FormMain.Instance.buttonEditIPadPassword.EditValueChanged += buttonEditIPadSite_EditValueChanged;
			FormMain.Instance.buttonItemIPadVideoConvert.Click += buttonItemIPadVideo_Click;
			FormMain.Instance.buttonItemIPadSyncFiles.Click += buttonItemIPadSyncFiles_Click;
		}

		#region IPageController Members
		public void InitController()
		{
			_initialization = true;

			_tabPage = new TabIPadContentControl();
			ApplyIPadManager();
			if (!FormMain.Instance.pnMain.Controls.Contains(_tabPage))
				FormMain.Instance.pnMain.Controls.Add(_tabPage);

			MainController.Instance.LibraryChanged += (sender, args) =>
														  {
															  _initialization = true;
															  ApplyIPadManager();
															  _initialization = false;
														  };

			_initialization = false;

			ShowVideoWarning();
		}

		public void PrepareTab(TabPageEnum tabPage) { }

		public void ShowTab()
		{
			_tabPage.BringToFront();
		}
		#endregion

		private void ApplyIPadManager()
		{
			LibraryDecorator activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator == null || !activeDecorator.Library.IsConfigured || !ConfigurationClasses.SettingsManager.Instance.EnableIPadSettingsTab) return;

			activeDecorator.IPadContentManager.UpdateVideoFiles();

			FormMain.Instance.buttonItemIPadSyncDisabled.Checked = !activeDecorator.Library.IPadManager.Enabled;
			FormMain.Instance.buttonItemIPadSyncEnabled.Checked = activeDecorator.Library.IPadManager.Enabled;
			FormMain.Instance.buttonEditIPadLocation.EditValue = !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.SyncDestinationPath) ? activeDecorator.Library.IPadManager.SyncDestinationPath : null;
			FormMain.Instance.buttonEditIPadSite.EditValue = !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.Website) ? activeDecorator.Library.IPadManager.Website : null;
			FormMain.Instance.buttonEditIPadLogin.EditValue = !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.Login) ? activeDecorator.Library.IPadManager.Login : null;
			FormMain.Instance.buttonEditIPadPassword.EditValue = !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.Password) ? activeDecorator.Library.IPadManager.Password : null;

			UpdateControlsState();

			if (!_tabPage.Controls.Contains(activeDecorator.IPadContentManager))
				_tabPage.Controls.Add(activeDecorator.IPadContentManager);
			activeDecorator.IPadContentManager.BringToFront();
		}

		private void UpdateControlsState()
		{
			LibraryDecorator activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator == null || !activeDecorator.Library.IsConfigured) return;
			FormMain.Instance.ribbonBarIPadLocation.Enabled = activeDecorator.Library.IPadManager.Enabled;
			FormMain.Instance.ribbonBarIPadSite.Enabled = activeDecorator.Library.IPadManager.Enabled;
			bool settingsConfigured = activeDecorator.Library.IPadManager.Enabled && !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.SyncDestinationPath) && !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.Website.Replace("http://", string.Empty)) && !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.Login) && !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.Password);
			FormMain.Instance.buttonItemIPadVideoConvert.Enabled = settingsConfigured;
			FormMain.Instance.buttonItemIPadSyncFiles.Enabled = settingsConfigured;
			FormMain.Instance.ribbonTabItemIPadUsers.Enabled = settingsConfigured & ConfigurationClasses.SettingsManager.Instance.EnableIPadUsersTab;
			activeDecorator.IPadContentManager.UpdateControlsState();
		}

		private void ShowVideoWarning()
		{
			if (MainController.Instance.ActiveDecorator.Library.VideoConversionWarning
				&& MainController.Instance.ActiveDecorator.Library.PreviewContainers.Any(x => !x.Ready))
			{
				var form = new FormVideoWarning();
				form.Show();
			}
		}

		private void buttonItemIPadSyncStatus_Click(object sender, EventArgs e)
		{
			FormMain.Instance.buttonItemIPadSyncEnabled.Checked = false;
			FormMain.Instance.buttonItemIPadSyncDisabled.Checked = false;
			(sender as ButtonItem).Checked = true;
		}

		private void buttonItemIPadSyncStatus_CheckedChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null && !_initialization)
			{
				MainController.Instance.ActiveDecorator.Library.IPadManager.Enabled = FormMain.Instance.buttonItemIPadSyncEnabled.Checked;
				MainController.Instance.ActiveDecorator.Library.Save();
				ApplyIPadManager();
			}
		}

		private void buttonEditIPadLocation_EditValueChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null && !_initialization)
			{
				string path = FormMain.Instance.buttonEditIPadLocation.EditValue != null ? FormMain.Instance.buttonEditIPadLocation.EditValue.ToString() : string.Empty;
				if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
				{
					MainController.Instance.ActiveDecorator.Library.IPadManager.SyncDestinationPath = path;
					MainController.Instance.ActiveDecorator.IPadContentManager.UpdateControlsState();
				}
				MainController.Instance.ActiveDecorator.Library.Save();
			}
		}

		private void buttonEditIPadLocation_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = MainController.Instance.ActiveDecorator.Library.IPadManager.SyncDestinationPath;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						FormMain.Instance.buttonEditIPadLocation.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void buttonEditIPadSite_EditValueChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null && !_initialization)
			{
				string site = FormMain.Instance.buttonEditIPadSite.EditValue != null ? FormMain.Instance.buttonEditIPadSite.EditValue.ToString() : string.Empty;
				string login = FormMain.Instance.buttonEditIPadLogin.EditValue != null ? FormMain.Instance.buttonEditIPadLogin.EditValue.ToString() : string.Empty;
				string password = FormMain.Instance.buttonEditIPadPassword.EditValue != null ? FormMain.Instance.buttonEditIPadPassword.EditValue.ToString() : string.Empty;
				MainController.Instance.ActiveDecorator.Library.IPadManager.Website = site;
				MainController.Instance.ActiveDecorator.Library.IPadManager.Login = login;
				MainController.Instance.ActiveDecorator.Library.IPadManager.Password = password;
				MainController.Instance.ActiveDecorator.IPadContentManager.UpdateControlsState();
				MainController.Instance.ActiveDecorator.Library.Save();
			}
		}

		private void buttonItemIPadSyncFiles_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			if (MainController.Instance.ActiveDecorator.Library.VideoConversionWarning
				&& MainController.Instance.ActiveDecorator.Library.PreviewContainers.Any(x => !x.Ready))
				AppManager.Instance.ShowWarning("You still have Videos that are not converted");
			using (var form = new FormProgressSyncFilesIPad())
			{
				form.CloseAfterSync = MainController.Instance.ActiveDecorator.Library.CloseAfterSync;
				form.ProcessAborted += (progressSender, progressE) => { Globals.ThreadAborted = true; };
				FormMain.Instance.ribbonControl.Enabled = false;
				_tabPage.Enabled = false;
				MainController.Instance.ActiveDecorator.Library.Save();
				var thread = new Thread(delegate()
											{
												Globals.ThreadActive = true;
												Globals.ThreadAborted = false;
												AppManager.Instance.KillAutoFM();
												if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
													MainController.Instance.ActiveDecorator.Library.PrepareForIPadSynchronize();
												if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
													LibraryManager.Instance.SynchronizeLibraryForIpad(MainController.Instance.ActiveDecorator.Library);
												AppManager.Instance.RunAutoFM();
											});
				form.Show();
				FormWindowState savedState = FormMain.Instance.WindowState;
				if (MainController.Instance.ActiveDecorator.Library.MinimizeOnSync)
					FormMain.Instance.WindowState = FormWindowState.Minimized;
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				Globals.ThreadActive = false;
				Globals.ThreadAborted = false;
				form.Close();
				_tabPage.Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;

				if (form.CloseAfterSync)
					Application.Exit();
				else
					FormMain.Instance.WindowState = savedState;
			}
		}

		private void buttonItemIPadVideo_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				MainController.Instance.ActiveDecorator.IPadContentManager.ConvertSelectedVideoFiles();
		}
	}
}