using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.Viewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class EmptyViewer : UserControl, IFileViewer
    {
        #region Properties
        public BusinessClasses.LibraryLink File { get; private set; }

        public string DisplayName
        {
            get
            {
                return string.Empty;
            }
        }

        public string CriteriaOverlap
        {
            get
            {
                return string.Empty;
            }
        }

        public Image Widget
        {
            get
            {
                return null;
            }
        }
        #endregion

        public EmptyViewer(BusinessClasses.LibraryLink file)
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
