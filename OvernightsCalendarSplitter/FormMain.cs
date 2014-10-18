using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SalesDepot.CommonGUI.Forms;

namespace OvernightsCalendarSplitter
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		#region Grab Methods
		private void ProcessFiles()
		{
			List<FileInfo> files = new List<FileInfo>(GetFiles(new DirectoryInfo(SettingsManager.Instance.SourcePath)));
			foreach (FileInfo file in files)
				SplitFile(file);
		}

		private void SplitFile(FileInfo file)
		{
			string fileExtension = file.Extension;
			if (fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx"))
			{
				ExcelHelper excelHelper = new ExcelHelper();
				excelHelper.SplitFile(file.FullName);
			}
		}

		private FileInfo[] GetFiles(DirectoryInfo sourceFolder)
		{
			List<FileInfo> result = new List<FileInfo>();

			foreach (DirectoryInfo subFolder in sourceFolder.GetDirectories())
				result.AddRange(GetFiles(subFolder));

			foreach (FileInfo file in sourceFolder.GetFiles())
				result.Add(file);

			return result.ToArray();
		}
		#endregion

		private void FormMain_Load(object sender, EventArgs e)
		{
			buttonEditSource.EditValue = SettingsManager.Instance.SourcePath;
			buttonEditDestination.EditValue = SettingsManager.Instance.DestinationPath;
		}

		private void simpleButtonSplit_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.SourcePath = buttonEditSource.EditValue != null ? buttonEditSource.EditValue.ToString() : null;
			SettingsManager.Instance.DestinationPath = buttonEditDestination.EditValue != null ? buttonEditDestination.EditValue.ToString() : null;
			SettingsManager.Instance.SaveApplicationSettings();

			if (Directory.Exists(SettingsManager.Instance.SourcePath) && Directory.Exists(SettingsManager.Instance.DestinationPath))
			{
				using (FormProgress formProgress = new FormProgress())
				{
					formProgress.TopMost = true;
					formProgress.laProgress.Text = "Processing files...";
					System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
					{
						ProcessFiles();
					}));

					formProgress.Show();
					System.Windows.Forms.Application.DoEvents();

					thread.Start();

					while (thread.IsAlive)
						System.Windows.Forms.Application.DoEvents();
					formProgress.Close();
				}
			}
		}

		private void buttonEditSource_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				if (buttonEditSource.EditValue != null)
					dialog.SelectedPath = buttonEditSource.EditValue.ToString();
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					if (System.IO.Directory.Exists(dialog.SelectedPath))
						buttonEditSource.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void buttonEditDestination_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				if (buttonEditDestination.EditValue != null)
					dialog.SelectedPath = buttonEditDestination.EditValue.ToString();
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					if (System.IO.Directory.Exists(dialog.SelectedPath))
						buttonEditDestination.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void simpleButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
