using System.ComponentModel;
using DevComponents.DotNetBar;
using SalesDepot.ConfigurationClasses;
using SalesDepot.Properties;

namespace SalesDepot.PresentationClasses.Settings
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

		public void LoadData()
		{
			_allowToSave = false;
			switch (SettingsManager.Instance.PowerPointLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
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
				SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Viewer;
			else if (buttonXMenu.Checked)
				SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Menu;
			else if (buttonXLaunch.Checked)
				SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Launch;
			SettingsManager.Instance.SaveSettings();
		}
	}

	public class PDFSettingsControl : LinkSettingsControl
	{
		public PDFSettingsControl()
		{
			laTitle.Text = "How will PDF files open?";
			pbLogo.Image = Resources.SettingsPDF;
		}

		public void LoadData()
		{
			_allowToSave = false;
			switch (SettingsManager.Instance.PDFLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
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
				SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Viewer;
			else if (buttonXMenu.Checked)
				SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Menu;
			else if (buttonXLaunch.Checked)
				SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Launch;
			SettingsManager.Instance.SaveSettings();
		}
	}

	public class WordSettingsControl : LinkSettingsControl
	{
		public WordSettingsControl()
		{
			laTitle.Text = "How will Word files open?";
			pbLogo.Image = Resources.SettingsWord;
		}

		public void LoadData()
		{
			_allowToSave = false;
			switch (SettingsManager.Instance.WordLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
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
				SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Viewer;
			else if (buttonXMenu.Checked)
				SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Menu;
			else if (buttonXLaunch.Checked)
				SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Launch;
			SettingsManager.Instance.SaveSettings();
		}
	}

	public class ExcelSettingsControl : LinkSettingsControl
	{
		public ExcelSettingsControl()
		{
			laTitle.Text = "How will Excel files open?";
			pbLogo.Image = Resources.SettingsExcel;
		}

		public void LoadData()
		{
			_allowToSave = false;
			switch (SettingsManager.Instance.ExcelLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
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
				SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Viewer;
			else if (buttonXMenu.Checked)
				SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Menu;
			else if (buttonXLaunch.Checked)
				SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Launch;
			SettingsManager.Instance.SaveSettings();
		}
	}

	public class VideoSettingsControl : LinkSettingsControl
	{
		public VideoSettingsControl()
		{
			laTitle.Text = "How will Video files open?";
			pbLogo.Image = Resources.SettingsVideo;
		}

		public void LoadData()
		{
			_allowToSave = false;
			switch (SettingsManager.Instance.VideoLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
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
				SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Viewer;
			else if (buttonXMenu.Checked)
				SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Menu;
			else if (buttonXLaunch.Checked)
				SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Launch;
			SettingsManager.Instance.SaveSettings();
		}
	}

	public class FolderSettingsControl : LinkSettingsControl
	{
		public FolderSettingsControl()
		{
			laTitle.Text = "How will Folders open?";
			pbLogo.Image = Resources.SettingsFolder;
		}

		public void LoadData()
		{
			_allowToSave = false;
			switch (SettingsManager.Instance.FolderLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					buttonXViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					buttonXMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
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
				SettingsManager.Instance.FolderLaunchOptions = LinkLaunchOptions.Viewer;
			else if (buttonXMenu.Checked)
				SettingsManager.Instance.FolderLaunchOptions = LinkLaunchOptions.Menu;
			else if (buttonXLaunch.Checked)
				SettingsManager.Instance.FolderLaunchOptions = LinkLaunchOptions.Launch;
			SettingsManager.Instance.SaveSettings();
		}
	}
}
