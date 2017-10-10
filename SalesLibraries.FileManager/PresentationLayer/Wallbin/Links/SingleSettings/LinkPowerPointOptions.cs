using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(PowerPointLink))]
	//public partial class LinkPowerPointOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkPowerPointOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private PowerPointLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkPowerPointOptions()
		{
			InitializeComponent();
			Text = "Admin";

			layoutControlItemRefreshPreview.MinSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRefreshPreview.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRefreshPreview.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenWV.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenWV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenWV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenWV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenQV.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpenQV.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpenQV.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpenQV.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public LinkPowerPointOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (PowerPointLink)sourceLink;

			checkEditDoNotGeneratePreview.Checked = !((DocumentLinkSettings)_data.Settings).GeneratePreviewImages;
			checkEditDoNotGenerateText.Checked = !((DocumentLinkSettings)_data.Settings).GenerateContentText;

			if (MainController.Instance.Settings.EnableLocalSync)
			{
				layoutControlItemOpenQV.Visibility = LayoutVisibility.Always;
				buttonXOpenQV.Text = String.Format("!QV Folder ({0})", ((PowerPointLinkSettings)_data.Settings).Id.ToString("D"));
			}
			else
				layoutControlItemOpenQV.Visibility = LayoutVisibility.Never;

			buttonXOpenWV.Text = String.Format("!WV Folder ({0})", _data.PreviewContainerName);
		}

		public void SaveData()
		{
			((DocumentLinkSettings)_data.Settings).GeneratePreviewImages = !checkEditDoNotGeneratePreview.Checked;
			((DocumentLinkSettings)_data.Settings).GenerateContentText = !checkEditDoNotGenerateText.Checked;
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", _data.NameWithExtension, Environment.NewLine)) != DialogResult.Yes) return;

			MainController.Instance.ProcessManager.Run("Updating Preview files...", (cancelationToken, formProgess) =>
			{
				((PowerPointLinkSettings)_data.Settings).ClearQuickViewContent();
				if (MainController.Instance.Settings.EnableLocalSync)
				{
					using (var powerPointProcessor = new PowerPointHidden())
					{
						if (!powerPointProcessor.Connect(true)) return;
						((PowerPointLinkSettings) _data.Settings).UpdateQuickViewContent(powerPointProcessor);
						((PowerPointLinkSettings) _data.Settings).UpdatePresentationInfo(powerPointProcessor);
					}
				}

				_data.ClearPreviewContainer();
				var previewContainer = _data.GetPreviewContainer();
				var previewGenerator = previewContainer.GetPreviewGenerator();
				try
				{
					previewContainer.UpdateContent(previewGenerator, cancelationToken);
				}
				catch { }
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
