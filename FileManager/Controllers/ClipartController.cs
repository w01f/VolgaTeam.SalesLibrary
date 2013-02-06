using System;
using DevComponents.DotNetBar;
using FileManager.PresentationClasses.TabPages;

namespace FileManager.Controllers
{
	public class ClipartController : IPageController
	{
		private bool _initialization;
		private TabClipartControl _tabPage;

		public ClipartController()
		{
			FormMain.Instance.buttonItemClipartClientLogos.Click += buttonItemClipart_Click;
			FormMain.Instance.buttonItemClipartSalesGallery.Click += buttonItemClipart_Click;
			FormMain.Instance.buttonItemClipartWebArt.Click += buttonItemClipart_Click;
			FormMain.Instance.buttonItemClipartClientLogos.CheckedChanged += buttonItemClipart_CheckedChanged;
			FormMain.Instance.buttonItemClipartSalesGallery.CheckedChanged += buttonItemClipart_CheckedChanged;
			FormMain.Instance.buttonItemClipartWebArt.CheckedChanged += buttonItemClipart_CheckedChanged;
		}

		#region IPageController Members
		public void InitController()
		{
			_initialization = true;
			_tabPage = new TabClipartControl();
			if (!FormMain.Instance.pnMain.Controls.Contains(_tabPage))
				FormMain.Instance.pnMain.Controls.Add(_tabPage);
			FormMain.Instance.buttonItemClipartSalesGallery.Checked = true;
			_tabPage.ShowSalesGallery();
			_initialization = false;
		}

		public void PrepareTab(TabPageEnum tabPage) { }

		public void ShowTab()
		{
			_tabPage.BringToFront();
		}
		#endregion

		private void buttonItemClipart_Click(object sender, EventArgs e)
		{
			FormMain.Instance.buttonItemClipartClientLogos.Checked = false;
			FormMain.Instance.buttonItemClipartSalesGallery.Checked = false;
			FormMain.Instance.buttonItemClipartWebArt.Checked = false;
			(sender as ButtonItem).Checked = true;
		}

		private void buttonItemClipart_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button.Checked && !_initialization)
			{
				if (button == FormMain.Instance.buttonItemClipartSalesGallery)
					_tabPage.ShowSalesGallery();
				else if (button == FormMain.Instance.buttonItemClipartClientLogos)
					_tabPage.ShowClientLogos();
				else if (button == FormMain.Instance.buttonItemClipartWebArt)
					_tabPage.ShowWebArt();
			}
		}
	}
}