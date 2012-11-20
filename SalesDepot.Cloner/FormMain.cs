using System;
using System.IO;
using System.Windows.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.Cloner
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		#region Methods

		private void Convert(string sourcePath, string destinationPath)
		{
			if (!string.IsNullOrEmpty(sourcePath) && Directory.Exists(sourcePath) && !string.IsNullOrEmpty(destinationPath) && Directory.Exists(destinationPath))
			{
				DirectoryInfo sourceFolder = new DirectoryInfo(sourcePath);
				DirectoryInfo destinationFolder = new DirectoryInfo(destinationPath);
				CoreObjects.BusinessClasses.Library sourceLibrary = new Library(sourceFolder.Name, sourceFolder);
				sourceLibrary.Init();
				CoreObjects.BusinessClasses.Library destinationLibrary = sourceLibrary.Clone(destinationFolder.Name, destinationFolder) as Library;
				destinationLibrary.Save();
				destinationLibrary.SaveLight();
			}
			else
			{
				MessageBox.Show("Source or Destination folder are wrong", "SD Cloner", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
		#endregion

		private void FormMain_Load(object sender, EventArgs e)
		{
		}

		private void simpleButtonSplit_Click(object sender, EventArgs e)
		{
			using (FormProgress formProgress = new FormProgress())
			{
				formProgress.TopMost = true;
				formProgress.laProgress.Text = "Processing files...";
				System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
				{
					Convert(buttonEditSource.EditValue != null ? buttonEditSource.EditValue.ToString() : string.Empty, buttonEditDestination.EditValue != null ? buttonEditDestination.EditValue.ToString() : string.Empty);
				}));

				formProgress.Show();
				System.Windows.Forms.Application.DoEvents();

				thread.Start();

				while (thread.IsAlive)
					System.Windows.Forms.Application.DoEvents();
				formProgress.Close();
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
