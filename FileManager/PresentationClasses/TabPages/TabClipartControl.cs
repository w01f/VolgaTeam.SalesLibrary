using System.ComponentModel;
using System.Windows.Forms;
using FileManager.PresentationClasses.Cliparts;

namespace FileManager.PresentationClasses.TabPages
{
	[ToolboxItem(false)]
	public partial class TabClipartControl : UserControl
	{
		private readonly ClientLogosControl _clientLogos = new ClientLogosControl();
		private readonly SalesGalleryControl _salesGallery = new SalesGalleryControl();
		private readonly WebArtControl _webArt = new WebArtControl();

		public TabClipartControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			clipartTreeListControl.Init();
		}

		public void ShowSalesGallery()
		{
			if (!pnMain.Controls.Contains(_salesGallery))
				pnMain.Controls.Add(_salesGallery);
			_salesGallery.BringToFront();
			_salesGallery.InitTreeList();
		}

		public void ShowClientLogos()
		{
			if (!pnMain.Controls.Contains(_clientLogos))
				pnMain.Controls.Add(_clientLogos);
			_clientLogos.BringToFront();
			_clientLogos.InitTreeList();
		}

		public void ShowWebArt()
		{
			if (!pnMain.Controls.Contains(_webArt))
				pnMain.Controls.Add(_webArt);
			_webArt.BringToFront();
			_webArt.InitTreeList();
		}
	}
}