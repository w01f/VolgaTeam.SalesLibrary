using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(PowerPointLink))]
	//public partial class LinkPowerPointOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkPowerPointOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly PowerPointLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkPowerPointOptions()
		{
			InitializeComponent();
			Text = "Admin";
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;

				ckDoNotGeneratePreview.Font = new Font(ckDoNotGeneratePreview.Font.FontFamily, ckDoNotGeneratePreview.Font.Size - 2, ckDoNotGeneratePreview.Font.Style);
				ckDoNotGenerateText.Font = new Font(ckDoNotGenerateText.Font.FontFamily, ckDoNotGenerateText.Font.Size - 2, ckDoNotGenerateText.Font.Style);

				buttonXOpenQV.Font = new Font(buttonXOpenQV.Font.FontFamily, buttonXOpenQV.Font.Size - 2, buttonXOpenQV.Font.Style);
				buttonXOpenWV.Font = new Font(buttonXOpenWV.Font.FontFamily, buttonXOpenWV.Font.Size - 2, buttonXOpenWV.Font.Style);
				buttonXRefreshPreview.Font = new Font(buttonXRefreshPreview.Font.FontFamily, buttonXRefreshPreview.Font.Size - 2, buttonXRefreshPreview.Font.Style);
			}
		}

		public LinkPowerPointOptions(PowerPointLink data) : this()
		{
			_data = data;
		}

		public void LoadData()
		{
			ckDoNotGeneratePreview.Checked = !((DocumentLinkSettings)_data.Settings).GeneratePreviewImages;
			ckDoNotGenerateText.Checked = !((DocumentLinkSettings)_data.Settings).GenerateContentText;

			if (Directory.Exists(((PowerPointLinkSettings)_data.Settings).ContainerPath))
			{
				buttonXOpenQV.Enabled = true;
				buttonXOpenQV.Text = String.Format("!QV Folder ({0})", ((PowerPointLinkSettings)_data.Settings).Id.ToString("D"));
			}
			else
				buttonXOpenQV.Enabled = false;


			if (Directory.Exists(_data.PreviewContainerPath))
			{
				buttonXOpenWV.Enabled = true;
				buttonXOpenWV.Text = String.Format("!WV Folder ({0})", _data.PreviewContainerName);
			}
			else
				buttonXOpenWV.Enabled = false;
		}

		public void SaveData()
		{
			((DocumentLinkSettings)_data.Settings).GeneratePreviewImages = !ckDoNotGeneratePreview.Checked;
			((DocumentLinkSettings)_data.Settings).GenerateContentText = !ckDoNotGenerateText.Checked;
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _data.NameWithExtension, Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", cancelationToken =>
			{
				((PowerPointLinkSettings)_data.Settings).ClearQuickViewContent();
				using (var powerPointProcessor = new PowerPointHidden())
				{
					if (!powerPointProcessor.Connect(true)) return;
					((PowerPointLinkSettings)_data.Settings).UpdateQuickViewContent(powerPointProcessor);
					((PowerPointLinkSettings)_data.Settings).UpdatePresentationInfo(powerPointProcessor);
				}

				_data.ClearPreviewContainer();
				var previewContainer = _data.GetPreviewContainer();
				//var previewGenerator = previewContainer.GetPreviewGenerator();
				//previewContainer.UpdateContent(previewGenerator, cancelationToken);
			});
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1}Is now updated for the server!", _data.NameWithExtension, Environment.NewLine));
		}

		private void buttonXOpenQV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(((PowerPointLinkSettings)_data.Settings).ContainerPath);
			}
			catch { }
		}

		private void buttonXOpenWV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_data.PreviewContainerPath);
			}
			catch { }
		}
	}
}
