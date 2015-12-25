using DevComponents.DotNetBar;
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
			laTitle.Text = "How will PowerPoint files open?";
			pbLogo.Image = Resources.SettingsPowerPoint;
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
			laTitle.Text = "How will PDF files open?";
			pbLogo.Image = Resources.SettingsPDF;
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
			laTitle.Text = "How will Word files open?";
			pbLogo.Image = Resources.SettingsWord;
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
			laTitle.Text = "How will Excel files open?";
			pbLogo.Image = Resources.SettingsExcel;
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
			laTitle.Text = "How will Video files open?";
			pbLogo.Image = Resources.SettingsVideo;
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
			laTitle.Text = "How will Folders open?";
			pbLogo.Image = Resources.SettingsFolder;
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
