using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OvernightsCalendarConverter
{
    public partial class FormMain : Form
    {
        private List<FileInfo> _files = new List<FileInfo>();

        public FormMain()
        {
            InitializeComponent();
        }

        #region Grab Methods
        private void GrabFiles()
        {
            List<FileInfo> newFiles = new List<FileInfo>(GetLatestFiles(new DirectoryInfo(SettingsManager.Instance.SourcePath)));
            FileInfo[] changedFiles = newFiles.Where(x => !_files.Select(y => y.FullName).Contains(x.FullName) || _files.Where(y => y.FullName.Equals(x.FullName) && y.LastWriteTime < x.LastWriteTime).Count() > 0).ToArray();
            foreach (FileInfo file in changedFiles)
                GrabFile(file);
            _files.Clear();
            _files.AddRange(newFiles);
        }

        private void GrabFile(FileInfo file)
        {
            DateTime fileDate = file.LastWriteTime;
            string fileExtension = file.Extension;
            if (fileExtension.Equals(".xls") || fileExtension.Equals(".xlsx"))
            {
                string tempFile = Path.GetTempFileName();
                file.CopyTo(tempFile, true);
                ExcelHelper excelHelper = new ExcelHelper();
                fileDate = excelHelper.GetOvernightsDate(tempFile);
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }

            string yearFolderPath = Path.Combine(SettingsManager.Instance.DestinationPath, string.Format("{0}", new string[] { fileDate.ToString("yyyy") }));
            if (!Directory.Exists(yearFolderPath))
                Directory.CreateDirectory(yearFolderPath);

            string destinationPath = Path.Combine(yearFolderPath, string.Format("{0}f{1}", new string[] { fileDate.ToString("MMddyy"), fileExtension }));
            if (File.Exists(destinationPath))
            {
                if (File.GetLastWriteTime(destinationPath) < file.LastWriteTime)
                    file.CopyTo(destinationPath, true);
            }
            else
                file.CopyTo(destinationPath, true);
        }

        private FileInfo[] GetLatestFiles(DirectoryInfo sourceFolder)
        {
            List<FileInfo> result = new List<FileInfo>();

            foreach (DirectoryInfo subFolder in sourceFolder.GetDirectories())
                result.AddRange(GetLatestFiles(subFolder));

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

        private void simpleButtonConver_Click(object sender, EventArgs e)
        {
            SettingsManager.Instance.SourcePath = buttonEditSource.EditValue != null ? buttonEditSource.EditValue.ToString() : null;
            SettingsManager.Instance.DestinationPath = buttonEditDestination.EditValue != null ? buttonEditDestination.EditValue.ToString() : null;
            SettingsManager.Instance.SaveApplicationSettings();

            if (Directory.Exists(SettingsManager.Instance.SourcePath) && Directory.Exists(SettingsManager.Instance.DestinationPath))
            {
                using (FormProgress formProgress = new FormProgress())
                {
                    formProgress.TopMost = true;
                    formProgress.laProgress.Text = "Converting files...";
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        GrabFiles();
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
