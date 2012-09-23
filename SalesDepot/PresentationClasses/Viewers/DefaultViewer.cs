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
                case CoreObjects.BusinessClasses.FileTypes.BuggyPresentation:
                case CoreObjects.BusinessClasses.FileTypes.FriendlyPresentation:
                case CoreObjects.BusinessClasses.FileTypes.Presentation:
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
                case CoreObjects.BusinessClasses.FileTypes.Other:
                case CoreObjects.BusinessClasses.FileTypes.QuickTimeVideo:
                    BusinessClasses.LinkManager.Instance.OpenCopyOfFile(this.File);
                    break;
                case CoreObjects.BusinessClasses.FileTypes.Folder:
                    BusinessClasses.LinkManager.Instance.OpenFolder(this.File);
                    break;
                case CoreObjects.BusinessClasses.FileTypes.Url:
                    BusinessClasses.LinkManager.Instance.StartProcess(this.File);
                    break;
                case CoreObjects.BusinessClasses.FileTypes.Network:
                    BusinessClasses.LinkManager.Instance.StartProcess(this.File);
                    break;
                default:
                    BusinessClasses.LinkManager.Instance.OpenLink(this.File);
                    break;
            }
        }

        public void Save()
        {
            BusinessClasses.LinkManager.Instance.SaveFile("Save copy of the file as", this.File);
        }

        public void Email()
        {
            BusinessClasses.LinkManager.Instance.EmailFile(this.File);
        }

        public void Print()
        {
            BusinessClasses.LinkManager.Instance.PrintFile(this.File);
        }
        #endregion

        private void laMessage_DoubleClick(object sender, System.EventArgs e)
        {
            BusinessClasses.LinkManager.Instance.OpenLink(this.File);
        }
    }
}
