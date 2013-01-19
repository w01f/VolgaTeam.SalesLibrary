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
        public BusinessClasses.LibraryLink File { get; private set; }

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

        public ExcelViewer(BusinessClasses.LibraryLink file)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Visible = false;

            this.File = file;

            if (InteropClasses.ExcelHelper.Instance.Connect())
            {
                Guid g = Guid.NewGuid();
                string newFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, g.ToString() + ".html");
                InteropClasses.ExcelHelper.Instance.ConvertToHtml(this.File.LocalPath, newFileName);
                InteropClasses.ExcelHelper.Instance.Disconnect();
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
