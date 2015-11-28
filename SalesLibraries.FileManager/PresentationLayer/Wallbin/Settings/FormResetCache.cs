using System;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
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
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to delete all QV files?") != DialogResult.Yes) return;
			var qvFolderPath = Path.Combine(_libraryPath, Constants.RegularPreviewContainersRootFolderName);
			if (!Directory.Exists(qvFolderPath)) return;
			MainController.Instance.ProcessManager.Run("Deleting Files...", cancelationToken =>
			{
				Utils.DeleteFolder(qvFolderPath);
				try
				{
					Directory.Delete(qvFolderPath);
				}
				catch { }
			});
		}

		private void buttonXResetWV_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to delete all WV files?") != DialogResult.Yes) return;
			var wvFolderPath = Path.Combine(_libraryPath, Constants.WebPreviewContainersRootFolderName);
			if (!Directory.Exists(wvFolderPath)) return;
			MainController.Instance.ProcessManager.Run("Deleting Files...", cancelationToken =>
			{
				Utils.DeleteFolder(wvFolderPath);
				try
				{
					Directory.Delete(wvFolderPath);
				}
				catch { }
			});
		}
	}
}