using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.CustomControls.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class QuickTimeViewer : UserControl, BusinessClasses.IFileViewer
    {
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
            if(this.File.Type == BusinessClasses.FileTypes.MediaPlayerVideo)
                BusinessClasses.LinkManager.AddVideoIntoPresentation(new FileInfo(this.File.FullPath));
        }
        #endregion

        #region IFileViewer Methods
        public void ReleaseResources()
        {
        }

        public void Open()
        {
            BusinessClasses.LinkManager.OpenVideo(new FileInfo(this.File.FullPath));
        }

        public void Save()
        {
            ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.SaveFile("Save copy of the file as", new FileInfo(this.File.FullPath));
        }

        public void Email()
        {
            ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.EmailFile(this.File.FullPath);
        }

        public void Print()
        {
            ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.PrintFile(new FileInfo(this.File.FullPath));
        }
        #endregion
    }
}
