using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.IPad
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class IPadManagerControl : UserControl
    {
        private List<BusinessClasses.VideoInfo> _videoFiles = new List<BusinessClasses.VideoInfo>();

        public WallBin.Decorators.LibraryDecorator ParentDecorator { get; private set; }

        public IPadManagerControl(WallBin.Decorators.LibraryDecorator parent)
        {
            InitializeComponent();
            this.ParentDecorator = parent;
            this.Dock = DockStyle.Fill;
        }

        public void UpdateVideoFiles()
        {
            int focussedRow = gridViewVideo.FocusedRowHandle;

            gridControlVideo.DataSource = null;
            _videoFiles.Clear();

            _videoFiles.AddRange(this.ParentDecorator.Library.IPadManager.VideoFiles);
            gridControlVideo.DataSource = _videoFiles;
            laVideoTitle.Text = string.Format("Your Library has {0} Video File{1}", _videoFiles.Count.ToString(), (_videoFiles.Count > 1 ? "s" : string.Empty));

            if (focussedRow >= 0 && focussedRow < gridViewVideo.RowCount)
                gridViewVideo.FocusedRowHandle = focussedRow;
        }

        private void gridViewVideo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int videoIndex = gridViewVideo.GetDataSourceRowIndex(e.RowHandle);
            if (videoIndex >= 0 && videoIndex < _videoFiles.Count)
            {
                BusinessClasses.VideoInfo videoInfo = _videoFiles[videoIndex];
                if (e.Column == gridColumnVideoMp4FileName)
                {
                    if (string.IsNullOrEmpty(videoInfo.Mp4FilePath))
                        e.Appearance.ForeColor = Color.Red;
                    else
                        e.Appearance.ForeColor = Color.Black;
                }
                else if (e.Column == gridColumnVideoOgvFileName)
                {
                    if (string.IsNullOrEmpty(videoInfo.OgvFilePath))
                        e.Appearance.ForeColor = Color.Red;
                    else
                        e.Appearance.ForeColor = Color.Black;
                }
                else if (e.Column == gridColumnVideoIPadCompatible)
                {
                    if (string.IsNullOrEmpty(videoInfo.Mp4FilePath))
                        e.Appearance.ForeColor = Color.Red;
                    else
                        e.Appearance.ForeColor = Color.Green;
                }
            }
        }

        private void repositoryItemButtonEditVideoWmv_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewVideo.FocusedRowHandle >= 0)
            {
                BusinessClasses.VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(gridViewVideo.FocusedRowHandle)];
                if (File.Exists(videoInfo.SourceFilePath))
                {
                    ToolClasses.VideoHelper.Instance.OpenMediaPlayer(videoInfo.SourceFilePath);
                }
                else
                    AppManager.Instance.ShowWarning("You need to convert this video first!");
            }
        }


        private void repositoryItemButtonEditVideoMp4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewVideo.FocusedRowHandle >= 0)
            {
                BusinessClasses.VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(gridViewVideo.FocusedRowHandle)];
                string filePath = string.Empty;
                if (gridViewVideo.FocusedColumn == gridColumnVideoMp4FileName)
                    filePath = videoInfo.Mp4FilePath;
                else
                    filePath = videoInfo.SourceFilePath;
                if (File.Exists(filePath))
                {
                    ToolClasses.VideoHelper.Instance.OpenQuickTime(filePath);
                }
                else
                    AppManager.Instance.ShowWarning("You need to convert this video first!");
            }
        }

        private void repositoryItemButtonEditVideoOgv_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewVideo.FocusedRowHandle >= 0)
            {
                BusinessClasses.VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(gridViewVideo.FocusedRowHandle)];
                if (File.Exists(videoInfo.OgvFilePath))
                {
                    ToolClasses.VideoHelper.Instance.OpenFirefox(videoInfo.OgvFilePath);
                }
                else
                    AppManager.Instance.ShowWarning("You need to convert this video first!");
            }
        }

        private void repositoryItemButtonEditVideoFolder_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewVideo.FocusedRowHandle >= 0)
            {
                BusinessClasses.VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(gridViewVideo.FocusedRowHandle)];
                if (e.Button.Index == 0)
                {
                    if (Directory.Exists(videoInfo.IPadFolderPath))
                        Process.Start(videoInfo.IPadFolderPath);
                    else
                        AppManager.Instance.ShowWarning("You need to convert this video first!");
                }
                else if (e.Button.Index == 1)
                {
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        this.Enabled = false;
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                        form.laProgress.Text = "Converting Video for the iPad..." + Environment.NewLine + "This might take a few minutes...";
                        form.TopMost = true;
                        Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            if (videoInfo.Parent.UniversalPreviewContainer == null)
                                videoInfo.Parent.UniversalPreviewContainer = new BusinessClasses.UniversalPreviewContainer(videoInfo.Parent);
                            videoInfo.Parent.UniversalPreviewContainer.UpdateContent();
                            this.ParentDecorator.Library.Save();
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
                    UpdateVideoFiles();
                }
            }
        }

        private void gridViewVideo_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnVideoSourceFileName)
            {
                BusinessClasses.VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(e.RowHandle)];
                if (videoInfo.Parent.Type == BusinessClasses.FileTypes.QuickTimeVideo)
                    e.RepositoryItem = repositoryItemButtonEditVideoMp4;
                else
                    e.RepositoryItem = repositoryItemButtonEditVideoWmv;
            }
        }

        private void pbIE_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Website))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "iexplore.exe";
                    process.StartInfo.Arguments = this.ParentDecorator.Library.IPadManager.Website;
                    process.Start();
                }
                catch
                {
                    AppManager.Instance.ShowWarning("Couldn't open the website");
                }
            }
            else
                AppManager.Instance.ShowWarning("Website is no set");
        }

        private void pbChrome_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Website))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "chrome.exe";
                    process.StartInfo.Arguments = this.ParentDecorator.Library.IPadManager.Website;
                    process.Start();
                }
                catch
                {
                    AppManager.Instance.ShowWarning("Couldn't open the website");
                }
            }
            else
                AppManager.Instance.ShowWarning("Website is no set");
        }

        private void pbFirefox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Website))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "firefox.exe";
                    process.StartInfo.Arguments = this.ParentDecorator.Library.IPadManager.Website;
                    process.Start();
                }
                catch
                {
                    AppManager.Instance.ShowWarning("Couldn't open the website");
                }
            }
            else
                AppManager.Instance.ShowWarning("Website is no set");
        }

        private void pbSafari_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Website))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "safari.exe";
                    process.StartInfo.Arguments = this.ParentDecorator.Library.IPadManager.Website;
                    process.Start();
                }
                catch
                {
                    AppManager.Instance.ShowWarning("Couldn't open the website");
                }
            }
            else
                AppManager.Instance.ShowWarning("Website is no set");
        }

        private void pbOpera_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Website))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "opera.exe";
                    process.StartInfo.Arguments = this.ParentDecorator.Library.IPadManager.Website;
                    process.Start();
                }
                catch
                {
                    AppManager.Instance.ShowWarning("Couldn't open the website");
                }
            }
            else
                AppManager.Instance.ShowWarning("Website is no set");

        }

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion
    }
}
