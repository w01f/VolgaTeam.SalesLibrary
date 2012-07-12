using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class QuickTimeViewer : UserControl, IFileViewer
    {
        private FileInfo _tempCopy = null;

        #region Properties
        public BusinessClasses.LibraryFile File { get; private set; }

        public string DisplayName
        {
            get
            {
                return this.File.DisplayName;
            }
        }

        public string CriteriaOverlap
        {
            get
            {
                return this.File.CriteriaOverlap;
            }
        }

        public Image Widget
        {
            get
            {
                return this.File.Widget;
            }
        }
        #endregion

        public QuickTimeViewer(BusinessClasses.LibraryFile file)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Visible = false;

            this.File = file;
            if (System.IO.File.Exists(this.File.LocalPath))
            {
                string tempPath = Path.Combine(AppManager.Instance.TempFolder.FullName, Path.GetFileName(this.File.LocalPath));
                System.IO.File.Copy(this.File.LocalPath, tempPath, true);
                _tempCopy = new FileInfo(tempPath);
            }
            //axWindowsMediaPlayer.URL = this.File.FullPath;
        }

        #region VideoViewer Methods
        public void Play()
        {
            //axWindowsMediaPlayer.Ctlcontrols.play();
        }

        public void Pause()
        {
            //axWindowsMediaPlayer.Ctlcontrols.pause();
        }

        public void Stop()
        {
            //axWindowsMediaPlayer.Ctlcontrols.stop();
        }

        public void InsertIntoPresentation()
        {
        }
        #endregion

        #region IFileViewer Methods
        public void ReleaseResources()
        {
        }

        public void Open()
        {
            BusinessClasses.LinkManager.Instance.OpenVideo(this.File);
        }

        public void Save()
        {
            BusinessClasses.LinkManager.Instance.SaveFile("Save copy of the file as", this.File);
        }

        public void Email()
        {
            BusinessClasses.LinkManager.Instance.EmailFile(this.File.LocalPath);
        }

        public void Print()
        {
            BusinessClasses.LinkManager.Instance.PrintFile(this.File);
        }
        #endregion
    }
}
