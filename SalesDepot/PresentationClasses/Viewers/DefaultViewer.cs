using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DefaultViewer : UserControl, IFileViewer
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

        public DefaultViewer(BusinessClasses.LibraryFile file)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Visible = false;

            this.File = file;

            switch (this.File.Type)
            {
                case BusinessClasses.FileTypes.BuggyPresentation:
                case BusinessClasses.FileTypes.FriendlyPresentation:
                case BusinessClasses.FileTypes.OtherPresentation:
                    laMessage.Text = "Double-Click PowerPoint files to preview";
                    break;
                default:
                    laMessage.Text = "Double-Click File to preview...";
                    break;
            }
        }

        #region IFileViewer Methods
        public void ReleaseResources()
        {
        }

        public void Open()
        {
            switch (this.File.Type)
            {
                case BusinessClasses.FileTypes.Other:
                case BusinessClasses.FileTypes.QuickTimeVideo:
                    BusinessClasses.LinkManager.Instance.OpenCopyOfFile(new FileInfo(this.File.LocalPath));
                    break;
                case BusinessClasses.FileTypes.Folder:
                    BusinessClasses.LinkManager.Instance.OpenFolder(this.File.LocalPath);
                    break;
                case BusinessClasses.FileTypes.Url:
                    BusinessClasses.LinkManager.Instance.StartProcess(this.File.LocalPath);
                    break;
                case BusinessClasses.FileTypes.Network:
                    BusinessClasses.LinkManager.Instance.StartProcess(this.File.LocalPath);
                    break;
                default:
                    BusinessClasses.LinkManager.Instance.OpenLink(this.File);
                    break;
            }
        }

        public void Save()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.SaveFile("Save copy of the file as", new FileInfo(this.File.LocalPath));
        }

        public void Email()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.EmailFile(this.File.LocalPath);
        }

        public void Print()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.PrintFile(new FileInfo(this.File.LocalPath));
        }
        #endregion

        private void laMessage_DoubleClick(object sender, System.EventArgs e)
        {
            BusinessClasses.LinkManager.Instance.OpenLink(this.File);
        }
    }
}
