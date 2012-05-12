using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ExcelViewer : UserControl, IFileViewer
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

        public ExcelViewer(BusinessClasses.LibraryFile file)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Visible = false;

            this.File = file;

            if (InteropClasses.ExcelHelper.Instance.Open())
            {
                Guid g = Guid.NewGuid();
                string newFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, g.ToString() + ".html");
                InteropClasses.ExcelHelper.Instance.ConvertToHtml(this.File.FullPath, newFileName);
                InteropClasses.ExcelHelper.Instance.Close();
                webBrowser.Url = new Uri(newFileName);
            }
        }

        #region IFileViewer Methods
        public void ReleaseResources()
        {
            webBrowser.Navigate("about:blank");
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
            using (ToolForms.WallBin.FormEmailLink form = new ToolForms.WallBin.FormEmailLink())
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
