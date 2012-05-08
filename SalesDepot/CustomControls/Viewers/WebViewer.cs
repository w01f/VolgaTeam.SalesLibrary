using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.CustomControls.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class WebViewer : UserControl, BusinessClasses.IFileViewer
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

        public WebViewer(BusinessClasses.LibraryFile file)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.Visible = false;

            this.File = file;
            webBrowser.Navigate(this.File.FullPath);
        }

        #region IFileViewer Methods
        public void ReleaseResources()
        {
            webBrowser.Navigate("about:blank");
        }

        public void Open()
        {
            BusinessClasses.LinkManager.StartProcess(this.File.FullPath);
        }

        public void Save()
        {
        }

        public void Email()
        {
        }

        public void Print()
        {
        }
        #endregion
    }
}
