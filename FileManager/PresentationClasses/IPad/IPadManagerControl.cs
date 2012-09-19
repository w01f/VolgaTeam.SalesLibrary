using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace FileManager.PresentationClasses.IPad
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class IPadManagerControl : UserControl
    {
        private List<BusinessClasses.VideoInfo> _videoFiles = new List<BusinessClasses.VideoInfo>();
        private List<IPadAdminService.UserRecord> _users = new List<IPadAdminService.UserRecord>();

        public WallBin.Decorators.LibraryDecorator ParentDecorator { get; private set; }

        public IPadManagerControl(WallBin.Decorators.LibraryDecorator parent)
        {
            InitializeComponent();
            this.ParentDecorator = parent;
            this.Dock = DockStyle.Fill;
        }

        public void UpdateControlsState()
        {
            xtraTabControl.Enabled = !string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.SyncDestinationPath) && !string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Website.Replace("http://", string.Empty)) && !string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Login) && !string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Password);
            FormMain.Instance.buttonItemIPadVideoConvert.Enabled = xtraTabControl.Enabled;
            FormMain.Instance.buttonItemIPadSyncFiles.Enabled = xtraTabControl.Enabled;
        }

        #region Video Tab
        public void UpdateVideoFiles()
        {
            int focussedRow = gridViewVideo.FocusedRowHandle;

            gridControlVideo.DataSource = null;
            _videoFiles.Clear();

            _videoFiles.AddRange(this.ParentDecorator.Library.IPadManager.VideoFiles);
            gridControlVideo.DataSource = new BindingList<BusinessClasses.VideoInfo>(_videoFiles.ToArray());
            laVideoTitle.Text = string.Format("Your Library has {0} Video File{1}", _videoFiles.Count.ToString(), (_videoFiles.Count > 1 ? "s" : string.Empty));

            if (focussedRow >= 0 && focussedRow < gridViewVideo.RowCount)
                gridViewVideo.FocusedRowHandle = focussedRow;
        }

        public void ConvertSelectedVideoFiles()
        {
            BusinessClasses.VideoInfo[] videoFiles = _videoFiles.Where(x => x.Selected).ToArray();
            if (videoFiles.Length > 0)
                ConvertVideoFiles(videoFiles);
            else
                AppManager.Instance.ShowWarning("Please select one or several videos in the list below");
        }

        private void ConvertVideoFiles(BusinessClasses.VideoInfo[] videoFiles)
        {
            using (ToolForms.FormProgressConverVideo form = new ToolForms.FormProgressConverVideo())
            {
                form.ProcessAborted += new EventHandler<EventArgs>((progressSender, progressE) =>
                {
                    AppManager.Instance.ThreadAborted = true;
                });
                FormMain.Instance.ribbonControl.Enabled = false;
                this.Enabled = false;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    AppManager.Instance.ThreadActive = true;
                    AppManager.Instance.ThreadAborted = false;
                    foreach (BusinessClasses.VideoInfo videoFile in videoFiles)
                    {
                        if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                        {
                            if (videoFile.Parent.UniversalPreviewContainer == null)
                                videoFile.Parent.UniversalPreviewContainer = new BusinessClasses.UniversalPreviewContainer(videoFile.Parent);
                            videoFile.Parent.UniversalPreviewContainer.UpdateContent();
                        }
                        else
                            break;
                    }
                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                        this.ParentDecorator.Library.Save();
                }));
                form.Show();
                FormWindowState savedState = FormMain.Instance.WindowState;
                if (this.ParentDecorator.Library.MinimizeOnSync)
                    FormMain.Instance.WindowState = FormWindowState.Minimized;
                thread.Start();
                while (thread.IsAlive)
                {
                    Thread.Sleep(100);
                    System.Windows.Forms.Application.DoEvents();
                }
                AppManager.Instance.ThreadActive = false;
                AppManager.Instance.ThreadAborted = false;
                form.Close();
                this.Enabled = true;
                FormMain.Instance.ribbonControl.Enabled = true;

                if (form.CloseAfterSync)
                    Application.Exit();
                else
                    FormMain.Instance.WindowState = savedState;
            }

            UpdateVideoFiles();
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

                if (videoInfo.Selected)
                    e.Appearance.BackColor = Color.LightGreen;
                else
                    e.Appearance.BackColor = Color.White;
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
                    ConvertVideoFiles(new BusinessClasses.VideoInfo[] { videoInfo });
                }
            }
        }

        private void gridViewVideo_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumnVideoSourceFileName)
            {
                BusinessClasses.VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(e.RowHandle)];
                if (videoInfo.Parent.Type == SalesDepot.CoreObjects.FileTypes.QuickTimeVideo)
                    e.RepositoryItem = repositoryItemButtonEditVideoMp4;
                else
                    e.RepositoryItem = repositoryItemButtonEditVideoWmv;
            }
        }

        private void gridViewVideo_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            GridViewInfo vi = gridViewVideo.GetViewInfo() as GridViewInfo;
            GridDataRowInfo ri = vi.RowsInfo.GetInfoByHandle(e.RowHandle) as GridDataRowInfo;
            if (ri == null)
                return;
            e.RepositoryItem.Appearance.ForeColor = ri.Cells[e.Column].Appearance.ForeColor;
            e.RepositoryItem.Appearance.BackColor = ri.Cells[e.Column].Appearance.BackColor;
        }

        private void buttonXSelectAll_Click(object sender, EventArgs e)
        {
            foreach (BusinessClasses.VideoInfo videoInfo in _videoFiles)
                videoInfo.Selected = true;
            gridViewVideo.RefreshData();
        }

        private void buttonXClearAll_Click(object sender, EventArgs e)
        {
            foreach (BusinessClasses.VideoInfo videoInfo in _videoFiles)
                videoInfo.Selected = false;
            gridViewVideo.RefreshData();
        }

        private void repositoryItemButtonEditVideoWmv_EditValueChanged(object sender, EventArgs e)
        {
            gridViewVideo.CloseEditor();
        }
        #endregion

        #region Users Tab
        public void UpdateUsers(bool showMessages)
        {
            gridControlUsers.DataSource = null;
            _users.Clear();

            string message = string.Empty;
            if (showMessages)
            {
                using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                {
                    FormMain.Instance.ribbonControl.Enabled = false;
                    this.Enabled = false;
                    PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                    form.laProgress.Text = "Loading user list...";
                    form.TopMost = true;
                    Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                    {
                        _users.AddRange(this.ParentDecorator.Library.IPadManager.GetUsers(out message));
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
                if (!string.IsNullOrEmpty(message))
                    AppManager.Instance.ShowWarning(message);
            }
            else
                _users.AddRange(this.ParentDecorator.Library.IPadManager.GetUsers(out message));

            gridControlUsers.DataSource = new BindingList<IPadAdminService.UserRecord>(_users.ToArray());
        }

        private void repositoryItemButtonEditUsersActions_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridViewUsers.FocusedRowHandle >= 0 && gridViewUsers.FocusedRowHandle < gridViewUsers.RowCount)
            {
                IPadAdminService.UserRecord userRecord = _users[gridViewUsers.GetDataSourceRowIndex(gridViewUsers.FocusedRowHandle)];
                if (e.Button.Index == 0)
                {
                    string message = string.Empty;
                    using (ToolForms.IPad.FormEditUser formEdit = new ToolForms.IPad.FormEditUser(false, _users.Select(x => x.login).ToArray()))
                    {
                        formEdit.textEditLogin.EditValue = userRecord.login;
                        formEdit.textEditFirstName.EditValue = userRecord.firstName;
                        formEdit.textEditLastName.EditValue = userRecord.lastName;
                        formEdit.textEditEmail.EditValue = userRecord.email;
                        if (formEdit.ShowDialog() == DialogResult.OK)
                        {
                            string login = formEdit.textEditLogin.EditValue != null ? formEdit.textEditLogin.EditValue.ToString() : string.Empty;
                            string password = formEdit.textEditPassword.EditValue != null ? formEdit.textEditPassword.EditValue.ToString() : string.Empty;
                            string firstName = formEdit.textEditFirstName.EditValue != null ? formEdit.textEditFirstName.EditValue.ToString() : string.Empty;
                            string lastName = formEdit.textEditLastName.EditValue != null ? formEdit.textEditLastName.EditValue.ToString() : string.Empty;
                            string email = formEdit.textEditEmail.EditValue != null ? formEdit.textEditEmail.EditValue.ToString() : string.Empty;
                            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                            {
                                FormMain.Instance.ribbonControl.Enabled = false;
                                this.Enabled = false;
                                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                                form.laProgress.Text = "Updating user...";
                                form.TopMost = true;
                                Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                                {
                                    this.ParentDecorator.Library.IPadManager.SetUser(login, password, firstName, lastName, email, out message);
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
                            UpdateUsers(true);
                        }
                    }
                    if (!string.IsNullOrEmpty(message))
                        AppManager.Instance.ShowWarning(message);
                }
                else if (e.Button.Index == 1)
                {
                    if (AppManager.Instance.ShowWarningQuestion(string.Format("Are you sure want to delete user {0}?", userRecord.FullName)) == DialogResult.Yes)
                    {
                        string message = string.Empty;
                        using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                        {
                            FormMain.Instance.ribbonControl.Enabled = false;
                            this.Enabled = false;
                            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                            form.laProgress.Text = "Deleting user...";
                            form.TopMost = true;
                            Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                            {
                                this.ParentDecorator.Library.IPadManager.DeleteUser(userRecord.login, out message);
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
                        UpdateUsers(true);
                        if (!string.IsNullOrEmpty(message))
                            AppManager.Instance.ShowWarning(message);
                    }
                }
            }
        }

        private void buttonXUsersRefresh_Click(object sender, EventArgs e)
        {
            UpdateUsers(true);
        }

        private void buttonXAddUser_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            using (ToolForms.IPad.FormEditUser formEdit = new ToolForms.IPad.FormEditUser(true, _users.Select(x => x.login).ToArray()))
            {
                if (formEdit.ShowDialog() == DialogResult.OK)
                {
                    string login = formEdit.textEditLogin.EditValue != null ? formEdit.textEditLogin.EditValue.ToString() : string.Empty;
                    string password = formEdit.textEditPassword.EditValue != null ? formEdit.textEditPassword.EditValue.ToString() : string.Empty;
                    string firstName = formEdit.textEditFirstName.EditValue != null ? formEdit.textEditFirstName.EditValue.ToString() : string.Empty;
                    string lastName = formEdit.textEditLastName.EditValue != null ? formEdit.textEditLastName.EditValue.ToString() : string.Empty;
                    string email = formEdit.textEditEmail.EditValue != null ? formEdit.textEditEmail.EditValue.ToString() : string.Empty;
                    using (ToolForms.FormProgress form = new ToolForms.FormProgress())
                    {
                        FormMain.Instance.ribbonControl.Enabled = false;
                        this.Enabled = false;
                        PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                        form.laProgress.Text = "Adding user...";
                        form.TopMost = true;
                        Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                        {
                            this.ParentDecorator.Library.IPadManager.SetUser(login, password, firstName, lastName, email, out message);
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
                    UpdateUsers(true);
                }
            }
            if (!string.IsNullOrEmpty(message))
                AppManager.Instance.ShowWarning(message);
        }
        #endregion

        #region Site Tab
        public void UpdateLibraryOnServer()
        {
            string message = string.Empty;
            using (ToolForms.FormProgressSyncData form = new ToolForms.FormProgressSyncData())
            {
                form.ProcessAborted += new EventHandler<EventArgs>((progressSender, progressE) =>
                {
                    AppManager.Instance.ThreadAborted = true;
                });
                FormMain.Instance.ribbonControl.Enabled = false;
                this.Enabled = false;
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
                Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    AppManager.Instance.ThreadActive = true;
                    AppManager.Instance.ThreadAborted = false;
                    this.ParentDecorator.Library.IPadManager.UpdateLibraryOnServer(out message);
                }));
                form.Show();
                FormWindowState savedState = FormMain.Instance.WindowState;
                if (this.ParentDecorator.Library.MinimizeOnSync)
                    FormMain.Instance.WindowState = FormWindowState.Minimized;
                thread.Start();
                while (thread.IsAlive)
                {
                    if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
                    {
                        Thread.Sleep(100);
                        System.Windows.Forms.Application.DoEvents();
                    }
                    else
                        thread.Abort();
                }
                AppManager.Instance.ThreadActive = false;
                AppManager.Instance.ThreadAborted = false;
                form.Close();
                this.Enabled = true;
                FormMain.Instance.ribbonControl.Enabled = true;

                if (!form.CloseAfterSync)
                    FormMain.Instance.WindowState = savedState;

                if (!string.IsNullOrEmpty(message))
                    AppManager.Instance.ShowWarning(message);
                else if (form.CloseAfterSync)
                    Application.Exit();
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
        #endregion

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
