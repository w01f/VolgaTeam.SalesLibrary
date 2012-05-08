using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.CustomControls.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class PDFViewer : UserControl, BusinessClasses.IFileViewer
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

            axAcroPDF.LoadFile(this.File.FullPath);
            axAcroPDF.setView("Fit");
        }

        #region IFileViewer Methods
        public void ReleaseResources()
        {
            axAcroPDF.Dispose();
        }

        public void Open()
        {
            BusinessClasses.LinkManager.OpenCopyOfFile(new FileInfo(this.File.FullPath));
        }

        public void Save()
        {
            ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.SaveFile("Save copy of the file as", new FileInfo(this.File.FullPath));
        }

        public void Email()
        {
            ActivityRecorder.Instance.WriteActivity();
            using (ToolForms.FormEmailLink form = new ToolForms.FormEmailLink())
            {
                form.SelectedFile = this.File;
                form.ShowDialog();
            }
        }

        public void Print()
        {
            ActivityRecorder.Instance.WriteActivity();
            BusinessClasses.LinkManager.PrintFile(new FileInfo(this.File.FullPath));
        }
        #endregion
    }
}
