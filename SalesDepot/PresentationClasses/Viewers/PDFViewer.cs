using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class PDFViewer : UserControl, IFileViewer
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

        public PDFViewer(BusinessClasses.LibraryFile file)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Visible = false;

            this.File = file;

            axAcroPDF.LoadFile(this.File.LocalPath);
            axAcroPDF.setView("Fit");
        }

        #region IFileViewer Methods
        public void ReleaseResources()
        {
            axAcroPDF.Dispose();
        }

        public void Open()
        {
            BusinessClasses.LinkManager.Instance.OpenCopyOfFile(new FileInfo(this.File.LocalPath));
        }

        public void Save()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.SaveFile("Save copy of the file as", new FileInfo(this.File.LocalPath));
        }

        public void Email()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
            {
                form.SelectedFile = this.File;
                form.ShowDialog();
            }
        }

        public void Print()
        {
            ToolClasses.ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.Instance.PrintFile(new FileInfo(this.File.LocalPath));
        }
        #endregion
    }
}
