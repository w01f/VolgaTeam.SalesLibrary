using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin
{
    [System.ComponentModel.ToolboxItem(false)]
    public class ColumnPanel : Panel
    {
        public int Order { get; set; }
        public ColumnPanel()
            : base()
        {
            this.Order = -1;
            this.BorderStyle = BorderStyle.None;
            this.MouseMove += new MouseEventHandler(PanelMouseMove);
        }

        private void PanelMouseMove(object sender, MouseEventArgs e)
        {
            this.Parent.Parent.Focus();
        }

        public void ResizePanel()
        {
            FolderBoxControl box;
            int pnDWBHeight = 0;
            int borderWidth = 0;
            int panelWidth = 0;

            panelWidth = this.Parent.Width / (ConfigurationClasses.SettingsManager.Instance.ClassicView ? 3 : 1);

            this.Width = panelWidth;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                box = (FolderBoxControl)this.Controls[i];
                box.Width = panelWidth;
                box.Top = i > 0 ? this.Controls[i - 1].Bottom + borderWidth : borderWidth;
                if (pnDWBHeight < box.Bottom)
                    pnDWBHeight = box.Bottom;
            }
            this.Height = pnDWBHeight + 15;
        }
    }
}
