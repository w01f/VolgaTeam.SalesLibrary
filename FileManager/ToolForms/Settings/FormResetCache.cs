using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormResetCache : DevComponents.DotNetBar.Metro.MetroForm
	{
		private readonly string _libraryPath;
		public FormResetCache(string libraryPath)
		{
			InitializeComponent();
			_libraryPath = libraryPath;
		}

		private void buttonXResetQV_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to delete all QV files?") != DialogResult.Yes) return;
			var qvFolderPath = Path.Combine(_libraryPath, Constants.RegularPreviewContainersRootFolderName);
			if (!Directory.Exists(qvFolderPath)) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.TopMost = true;
				formProgress.laProgress.Text = "Deleting Files...";
				var thread = new Thread(() =>
				{
					SyncManager.DeleteFolder(new DirectoryInfo(qvFolderPath));
					try
					{
						Directory.Delete(qvFolderPath);
					}
					catch { }
				});
				formProgress.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
			}
			
		}

		private void buttonXResetWV_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to delete all WV files?") != DialogResult.Yes) return;
			var wvFolderPath = Path.Combine(_libraryPath, Constants.FtpPreviewContainersRootFolderName);
			if (!Directory.Exists(wvFolderPath)) return;
			using (var formProgress = new FormProgress())
			{
				formProgress.TopMost = true;
				formProgress.laProgress.Text = "Deleting Files...";
				var thread = new Thread(() =>
				{
					SyncManager.DeleteFolder(new DirectoryInfo(wvFolderPath));
					try
					{
						Directory.Delete(wvFolderPath);
					}
					catch { }
				});
				formProgress.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				formProgress.Close();
			}
		}
	}
}