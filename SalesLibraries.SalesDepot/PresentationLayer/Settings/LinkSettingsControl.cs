using DevComponents.DotNetBar;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.Properties;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class LinkSettingsControl : BaseSettingsControl
	{
		protected bool _allowToSave;

		public LinkSettingsControl()
		{
			InitializeComponent();
			layoutControlItemViewer.MaxSize = RectangleHelper.ScaleSize(layoutControlItemViewer.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemViewer.MinSize = RectangleHelper.ScaleSize(layoutControlItemViewer.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemMenu.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMenu.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemMenu.MinSize = RectangleHelper.ScaleSize(layoutControlItemMenu.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLaunch.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLaunch.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemLaunch.MinSize = RectangleHelper.ScaleSize(layoutControlItemLaunch.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void Button_Click(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked) return;
			_allowToSave = false;
			buttonXViewer.Checked = false;
			buttonXMenu.Checked = false;
			buttonXLaunch.Checked = false;
			_allowToSave = true;
			button.Checked = true;
		}

		protected virtual void ButtonX_CheckedChanged(object sender, System.EventArgs e) { }
	}

	public class PowerPointSettingsControl : LinkSettingsControl
	{
		public PowerPointSettingsControl()
		{
			simpleLabelItemTitle.Text = "<size=+2>How will PowerPoint files open?</size>";
			pictureEditLogo.Image = Resources.SettingsPowerPoint;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			switch (MainController.Instance.Settings.LinkLaunchSettings.PowerPoint)
			{
				case LinkLaunchOptionsEnum.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Launch:
					buttonXLaunch.Checked = true;
					break;
			}
			_allowToSave = true;
		}

		protected override void ButtonX_CheckedChanged(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			if (buttonXViewer.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.PowerPoint = LinkLaunchOptionsEnum.Viewer;
			else if (buttonXMenu.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.PowerPoint = LinkLaunchOptionsEnum.Menu;
			else if (buttonXLaunch.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.PowerPoint = LinkLaunchOptionsEnum.Launch;
			MainController.Instance.Settings.SaveSettings();
		}
	}

	public class PDFSettingsControl : LinkSettingsControl
	{
		public PDFSettingsControl()
		{
			simpleLabelItemTitle.Text = "<size=+2>How will PDF files open?</size>";
			pictureEditLogo.Image = Resources.SettingsPDF;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			switch (MainController.Instance.Settings.LinkLaunchSettings.PDF)
			{
				case LinkLaunchOptionsEnum.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Launch:
					buttonXLaunch.Checked = true;
					break;
			}
			_allowToSave = true;
		}

		protected override void ButtonX_CheckedChanged(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			if (buttonXViewer.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.PDF = LinkLaunchOptionsEnum.Viewer;
			else if (buttonXMenu.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.PDF = LinkLaunchOptionsEnum.Menu;
			else if (buttonXLaunch.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.PDF = LinkLaunchOptionsEnum.Launch;
			MainController.Instance.Settings.SaveSettings();
		}
	}

	public class WordSettingsControl : LinkSettingsControl
	{
		public WordSettingsControl()
		{
			simpleLabelItemTitle.Text = "<size=+2>How will Word files open?</size>";
			pictureEditLogo.Image = Resources.SettingsWord;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			switch (MainController.Instance.Settings.LinkLaunchSettings.Word)
			{
				case LinkLaunchOptionsEnum.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Launch:
					buttonXLaunch.Checked = true;
					break;
			}
			_allowToSave = true;
		}

		protected override void ButtonX_CheckedChanged(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			if (buttonXViewer.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Word = LinkLaunchOptionsEnum.Viewer;
			else if (buttonXMenu.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Word = LinkLaunchOptionsEnum.Menu;
			else if (buttonXLaunch.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Word = LinkLaunchOptionsEnum.Launch;
			MainController.Instance.Settings.SaveSettings();
		}
	}

	public class ExcelSettingsControl : LinkSettingsControl
	{
		public ExcelSettingsControl()
		{
			simpleLabelItemTitle.Text = "<size=+2>How will Excel files open?</size>";
			pictureEditLogo.Image = Resources.SettingsExcel;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			switch (MainController.Instance.Settings.LinkLaunchSettings.Excel)
			{
				case LinkLaunchOptionsEnum.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Launch:
					buttonXLaunch.Checked = true;
					break;
			}
			_allowToSave = true;
		}

		protected override void ButtonX_CheckedChanged(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			if (buttonXViewer.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Excel = LinkLaunchOptionsEnum.Viewer;
			else if (buttonXMenu.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Excel = LinkLaunchOptionsEnum.Menu;
			else if (buttonXLaunch.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Excel = LinkLaunchOptionsEnum.Launch;
			MainController.Instance.Settings.SaveSettings();
		}
	}

	public class VideoSettingsControl : LinkSettingsControl
	{
		public VideoSettingsControl()
		{
			simpleLabelItemTitle.Text = "<size=+2>How will Video files open?</size>";
			pictureEditLogo.Image = Resources.SettingsVideo;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			switch (MainController.Instance.Settings.LinkLaunchSettings.Video)
			{
				case LinkLaunchOptionsEnum.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Launch:
					buttonXLaunch.Checked = true;
					break;
			}
			_allowToSave = true;
		}

		protected override void ButtonX_CheckedChanged(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			if (buttonXViewer.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Video = LinkLaunchOptionsEnum.Viewer;
			else if (buttonXMenu.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Video = LinkLaunchOptionsEnum.Menu;
			else if (buttonXLaunch.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Video = LinkLaunchOptionsEnum.Launch;
			MainController.Instance.Settings.SaveSettings();
		}
	}

	public class FolderSettingsControl : LinkSettingsControl
	{
		public FolderSettingsControl()
		{
			simpleLabelItemTitle.Text = "<size=+2>How will Folders open?</size>";
			pictureEditLogo.Image = Resources.SettingsFolder;
		}

		public override void LoadData()
		{
			_allowToSave = false;
			switch (MainController.Instance.Settings.LinkLaunchSettings.Folder)
			{
				case LinkLaunchOptionsEnum.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptionsEnum.Launch:
					buttonXLaunch.Checked = true;
					break;
			}
			_allowToSave = true;
		}

		protected override void ButtonX_CheckedChanged(object sender, System.EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (!button.Checked) return;
			if (!_allowToSave) return;
			if (buttonXViewer.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Folder = LinkLaunchOptionsEnum.Viewer;
			else if (buttonXMenu.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Folder = LinkLaunchOptionsEnum.Menu;
			else if (buttonXLaunch.Checked)
				MainController.Instance.Settings.LinkLaunchSettings.Folder = LinkLaunchOptionsEnum.Launch;
			MainController.Instance.Settings.SaveSettings();
		}
	}
}
