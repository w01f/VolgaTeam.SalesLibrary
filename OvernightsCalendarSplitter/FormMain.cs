using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
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
			var files = new List<FileInfo>(GetFiles(new DirectoryInfo(SettingsManager.Instance.SourcePath)));
			foreach (FileInfo file in files)
				SplitFile(file);
		}

		private void SplitFile(FileInfo file)
		{
			string fileExtension = file.Extension;
			if (fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx"))
			{
				var excelHelper = new ExcelHelper();
				excelHelper.SplitFile(file.FullName);
			}
		}

		private FileInfo[] GetFiles(DirectoryInfo sourceFolder)
		{
			var result = new List<FileInfo>();

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
				using (var formProgress = new FormProgress())
				{
					formProgress.TopMost = true;
					formProgress.laProgress.Text = "Processing files...";
					var thread = new Thread(delegate() { ProcessFiles(); });

					formProgress.Show();
					Application.DoEvents();

					thread.Start();

					while (thread.IsAlive)
						Application.DoEvents();
					formProgress.Close();
				}
			}
		}

		private void buttonEditSource_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditSource.EditValue != null)
					dialog.SelectedPath = buttonEditSource.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditSource.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void buttonEditDestination_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				if (buttonEditDestination.EditValue != null)
					dialog.SelectedPath = buttonEditDestination.EditValue.ToString();
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
						buttonEditDestination.EditValue = dialog.SelectedPath;
				}
			}
		}

		private void simpleButtonExit_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}