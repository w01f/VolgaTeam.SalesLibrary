using System;
using System.Windows.Forms;

namespace FileManager.TabPages
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabClipartControl : UserControl
    {
        public TabClipartControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void UpdateView()
        {
            PresentationClasses.Cliparts.SalesGalleryControl.Instance.InitTreeList();
            PresentationClasses.Cliparts.ClientLogosControl.Instance.InitTreeList();
            PresentationClasses.Cliparts.WebArtControl.Instance.InitTreeList();
        }

        public void buttonItemClipart_Click(object sender, EventArgs e)
        {
            FormMain.Instance.buttonItemClipartClientLogos.Checked = false;
            FormMain.Instance.buttonItemClipartSalesGallery.Checked = false;
            FormMain.Instance.buttonItemClipartWebArt.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemClipart_CheckedChanged(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem button = sender as DevComponents.DotNetBar.ButtonItem;
            if (button.Checked)
            {
                Control parent = pnMain.Parent;
                pnMain.Parent = null;
                if (button == FormMain.Instance.buttonItemClipartSalesGallery)
                {
                    if (!pnMain.Controls.Contains(PresentationClasses.Cliparts.SalesGalleryControl.Instance))
                        pnMain.Controls.Add(PresentationClasses.Cliparts.SalesGalleryControl.Instance);
                    PresentationClasses.Cliparts.SalesGalleryControl.Instance.BringToFront();
                    PresentationClasses.Cliparts.SalesGalleryControl.Instance.InitTreeList();
                }
                else  if (button == FormMain.Instance.buttonItemClipartClientLogos)
                {
                    if (!pnMain.Controls.Contains(PresentationClasses.Cliparts.ClientLogosControl.Instance))
                        pnMain.Controls.Add(PresentationClasses.Cliparts.ClientLogosControl.Instance);
                    PresentationClasses.Cliparts.ClientLogosControl.Instance.BringToFront();
                    PresentationClasses.Cliparts.ClientLogosControl.Instance.InitTreeList();
                }
                else if (button == FormMain.Instance.buttonItemClipartWebArt)
                {
                    if (!pnMain.Controls.Contains(PresentationClasses.Cliparts.WebArtControl.Instance))
                        pnMain.Controls.Add(PresentationClasses.Cliparts.WebArtControl.Instance);
                    PresentationClasses.Cliparts.WebArtControl.Instance.BringToFront();
                    PresentationClasses.Cliparts.WebArtControl.Instance.InitTreeList();
                }
                pnMain.Parent = parent;
            }
        }

        private void TabClipartControl_Load(object sender, EventArgs e)
        {
            FormMain.Instance.buttonItemClipartSalesGallery.Checked = true;
            clipartTreeListControl.Init();
        }
    }
}
