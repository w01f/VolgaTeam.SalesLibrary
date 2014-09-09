using System.Drawing;
using System.Windows.Forms;

namespace FileManager.PresentationClasses.WallBin
{
	[System.ComponentModel.ToolboxItem(false)]
	class ColumnPanel : Panel
	{
		public int Order { get; set; }
		public int DropHintLineHeight { get; set; }
		public Pen Pen { get; set; }
		public int RealPanelHeight { get; set; }

		public ColumnPanel()
		{
			this.DropHintLineHeight = -1;
			this.Pen = new Pen(Color.Black, 8);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.DropHintLineHeight != -1)
				e.Graphics.DrawLine(this.Pen, 0, this.DropHintLineHeight, this.Width, this.DropHintLineHeight);
		}

		public void ResizePanel()
		{
			FolderBoxControl box;
			int borderWidth = 0;
			int panelWidth = 0;

			panelWidth = this.Parent.Width / 3;

			this.Width = panelWidth;
			for (int i = 0; i < this.Controls.Count; i++)
			{
				box = (FolderBoxControl)this.Controls[i];
				box.Width = panelWidth - borderWidth * 2;
				box.Left = borderWidth;
				box.Top = i > 0 ? this.Controls[i - 1].Bottom + borderWidth : borderWidth;
			}
		}
	}
}
