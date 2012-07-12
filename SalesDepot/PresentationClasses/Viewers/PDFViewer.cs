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

            string tempName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(this.File.LocalPath));
            System.IO.File.Copy(this.File.LocalPath, tempName, true);
            axAcroPDF.LoadFile(tempName);
            axAcroPDF.setView("Fit");
        }

        #region IFileViewer Methods
        public void ReleaseResources()
        {
            axAcroPDF.Dispose();
        }

        public void Open()
        {
            BusinessClasses.LinkManager.Instance.OpenCopyOfFile(this.File);
        }

        public void Save()
        {
            BusinessClasses.LinkManager.Instance.SaveFile("Save copy of the file as", this.File);
        }

        public void Email()
        {
            using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
            {
                form.link = this.File;
                form.ShowDialog();
            }
        }

        public void Print()
        {
            BusinessClasses.LinkManager.Instance.PrintFile(this.File);
        }
        #endregion
    }
}
