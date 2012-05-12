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
                    BusinessClasses.LinkManager.OpenCopyOfFile(new FileInfo(this.File.FullPath));
                    break;
                case BusinessClasses.FileTypes.Folder:
                    BusinessClasses.LinkManager.OpenFolder(this.File.FullPath);
                    break;
                case BusinessClasses.FileTypes.Url:
                    BusinessClasses.LinkManager.StartProcess(this.File.FullPath);
                    break;
                case BusinessClasses.FileTypes.Network:
                    BusinessClasses.LinkManager.StartProcess(this.File.FullPath);
                    break;
                default:
                    break;
            }
        }

        public void Save()
        {
            ActivityRecorder.Instance.WriteActivity();
            switch (this.File.Type)
            {
                case BusinessClasses.FileTypes.Other:
                case BusinessClasses.FileTypes.QuickTimeVideo:
                    BusinessClasses.LinkManager.SaveFile("Save copy of the file as", new FileInfo(this.File.FullPath));
                    break;
                default:
                    break;
            }
        }

        public void Email()
        {
            ActivityRecorder.Instance.WriteActivity();
            switch (this.File.Type)
            {
                case BusinessClasses.FileTypes.Other:
                case BusinessClasses.FileTypes.QuickTimeVideo:
                    BusinessClasses.LinkManager.EmailFile(this.File.FullPath);
                    break;
                default:
                    break;
            }
        }

        public void Print()
        {
            ActivityRecorder.Instance.WriteActivity();
            switch (this.File.Type)
            {
                case BusinessClasses.FileTypes.Other:
                case BusinessClasses.FileTypes.QuickTimeVideo:
                    BusinessClasses.LinkManager.PrintFile(new FileInfo(this.File.FullPath));
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
