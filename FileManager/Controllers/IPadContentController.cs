using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using FileManager.BusinessClasses;
using FileManager.ConfigurationClasses;
using FileManager.PresentationClasses.TabPages;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.ToolClasses;
using FileManager.ToolForms;
using FileManager.ToolForms.IPad;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.Controllers
{
	public class IPadContentController : IPageController
	{
		private TabIPadContentControl _tabPage;

		public IPadContentController()
		{
			FormMain.Instance.buttonEditIPadLocation.ButtonClick += buttonEditIPadLocation_ButtonClick;
			FormMain.Instance.buttonItemIPadVideoConvert.Click += buttonItemIPadVideo_Click;
			FormMain.Instance.buttonItemIPadSyncFiles.Click += buttonItemIPadSyncFiles_Click;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabIPadContentControl();
			ApplyIPadManager();
			if (!FormMain.Instance.pnMain.Controls.Contains(_tabPage))
				FormMain.Instance.pnMain.Controls.Add(_tabPage);

			MainController.Instance.LibraryChanged += (sender, args) => ApplyIPadManager();
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

			FormMain.Instance.buttonEditIPadLocation.EditValue = !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.SyncDestinationPath) ? activeDecorator.Library.IPadManager.SyncDestinationPath : null;

			UpdateControlsState();

			if (!_tabPage.Controls.Contains(activeDecorator.IPadContentManager))
				_tabPage.Controls.Add(activeDecorator.IPadContentManager);
			activeDecorator.IPadContentManager.BringToFront();
		}

		private void UpdateControlsState()
		{
			var activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator == null || !activeDecorator.Library.IsConfigured) return;
			var settingsConfigured = SettingsManager.Instance.WebServiceConnected && !string.IsNullOrEmpty(activeDecorator.Library.IPadManager.SyncDestinationPath);
			FormMain.Instance.buttonItemIPadVideoConvert.Enabled = settingsConfigured;
			FormMain.Instance.buttonItemIPadSyncFiles.Enabled = settingsConfigured;
			FormMain.Instance.ribbonTabItemIPadUsers.Enabled = settingsConfigured & SettingsManager.Instance.EnableIPadUsersTab;
			activeDecorator.IPadContentManager.UpdateControlsState();
		}

		private void ShowVideoWarning()
		{
			if (!MainController.Instance.ActiveDecorator.Library.VideoConversionWarning || MainController.Instance.ActiveDecorator.Library.PreviewContainers.All(x => x.Ready)) return;
			var form = new FormVideoWarning();
			form.Show();
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

		private void buttonItemIPadSyncFiles_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			if (MainController.Instance.ActiveDecorator.Library.IsSyncLocked()) return;
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