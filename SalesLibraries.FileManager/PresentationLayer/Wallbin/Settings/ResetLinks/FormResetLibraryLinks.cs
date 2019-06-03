using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;
using Padding = System.Windows.Forms.Padding;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	public partial class FormResetLibraryLinks : MetroForm
	{
		private IResetLibraryContentControl SelectedControl => xtraTabControl.SelectedTabPage as IResetLibraryContentControl;

		public FormResetLibraryLinks()
		{
			InitializeComponent();

			var resetImagesControl = new ResetImagesControl();
			resetImagesControl.SelectionChanged += OnControlSelectionChanged;
			resetImagesControl.ThumbnailRefreshRequested += OnThumbnailRefresh;
			xtraTabControl.TabPages.Add(resetImagesControl);

			var resetInfoControl = new ResetInfoControl();
			resetInfoControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetInfoControl);

			var resetFontControl = new ResetFontControl(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library);
			resetFontControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetFontControl);

			if (MainController.Instance.Settings.EditorSettings.EnableTagsEdit)
			{
				var searchTagsControl = new ResetTagsControl();
				searchTagsControl.SelectionChanged += OnControlSelectionChanged;
				xtraTabControl.TabPages.Add(searchTagsControl);
			}

			var resetLineBreaksControl = new ResetLineBreaksControl();
			resetLineBreaksControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetLineBreaksControl);

			var resetPreviewContentControl = new ResetPreviewContentControl();
			resetPreviewContentControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetPreviewContentControl);

			if (MainController.Instance.Settings.OneDriveSettings.Enabled)
			{
				var resetOneDriveLinksControl = new ResetOneDriveLinksControl();
				resetOneDriveLinksControl.SelectionChanged += OnControlSelectionChanged;
				xtraTabControl.TabPages.Add(resetOneDriveLinksControl);
			}

			var resetAllControl = new ResetAllControl();
			resetAllControl.SelectionChanged += OnControlSelectionChanged;
			xtraTabControl.TabPages.Add(resetAllControl);

			layoutControlItemToggleEnable.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggleEnable.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemToggleEnable.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggleEnable.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDisableToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDisableToggle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDisableToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDisableToggle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnEnableClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXEnable.Checked = false;
			buttonXDisable.Checked = false;
			button.Checked = true;
		}

		private void OnEnableCheckedChanged(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (!button.Checked) return;
			layoutControlItemTabControl.Enabled = buttonXEnable.Checked;
			layoutControlItemReset.Enabled = buttonXEnable.Checked && SelectedControl != null && SelectedControl.SelectionMade;
		}

		private void OnControlSelectionChanged(object sender, EventArgs e)
		{
			layoutControlItemReset.Enabled = buttonXEnable.Checked && SelectedControl != null && SelectedControl.SelectionMade;
		}

		private void OnSelectedControlTabChanged(object sender, TabPageChangedEventArgs e)
		{
			OnControlSelectionChanged(sender, e);
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (SelectedControl == null) return;
			if (SelectedControl is IResetLibraryLinkSettingsControl)
			{
				var resetLinkSettingsControl = SelectedControl as IResetLibraryLinkSettingsControl;
				using (var confirmationForm = new FormResetLibraryLinksConfirmation(resetLinkSettingsControl.GetSelectedSettingsGroups()))
				{
					if (confirmationForm.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
					SelectedControl.ResetContent(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library);
					MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
					MainController.Instance.ProcessChanges();
					MainController.Instance.ProcessManager.RunInQueue("Loading Library...",
						() =>
							MainController.Instance.MainForm.ActiveForm.Invoke(
								new MethodInvoker(MainController.Instance.TabWallbin.UpdateWallbin)));
				}
			}
			else
			{
				SelectedControl.ResetContent(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library);
				MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
				MainController.Instance.ProcessChanges();
			}
		}

		private void OnThumbnailRefresh(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Visible = false;
			MainController.Instance.ProcessManager.Run("Updating Thumbnails...", (cancelationToken, formProgess) =>
			{
				var thumbnailSessionKey = Guid.NewGuid().ToString();

				foreach (var thumbnailLink in MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library.Pages
					.SelectMany(page => page.TopLevelLinks)
					.OfType<IThumbnailSettingsHolder>()
					.Where(thumbnailHolder => thumbnailHolder.Thumbnail.Enable)
					.ToList())
				{
					if (cancelationToken.IsCancellationRequested) break;
					var defaultImageFilePath = thumbnailLink.GetThumbnailSourceFiles(thumbnailSessionKey).FirstOrDefault();
					if (defaultImageFilePath != null)
						using (var tempImage = Image.FromFile(defaultImageFilePath))
						{
							var tranformAction = new Func<Image, Image>(image =>
							{
								image = image.Resize(new Size(thumbnailLink.Thumbnail.ImageWidth, tempImage.Height));
								if (thumbnailLink.Thumbnail.BorderSize > 0)
								{
									image = image.DrawBorder(thumbnailLink.Thumbnail.BorderSize, thumbnailLink.Thumbnail.BorderColor);
									if (thumbnailLink.Thumbnail.ShadowColor != Color.White)
										image = image.DrawShadow(ThumbnailSettings.DefaultShadowSize, thumbnailLink.Thumbnail.ShadowColor);
								}

								if (thumbnailLink.Thumbnail.ImagePadding > 0)
									image = image.DrawPadding(thumbnailLink.Thumbnail.TextEnabled
										? new Padding(
											thumbnailLink.Thumbnail.ImagePadding,
											thumbnailLink.Thumbnail.TextPosition == ThumbnailTextPosition.Top ? 0 : thumbnailLink.Thumbnail.ImagePadding,
											thumbnailLink.Thumbnail.ImagePadding,
											thumbnailLink.Thumbnail.TextPosition == ThumbnailTextPosition.Bottom ? 0 : thumbnailLink.Thumbnail.ImagePadding)
										: new Padding(thumbnailLink.Thumbnail.ImagePadding));
								return image;
							});
							using (var changedImage = tranformAction(tempImage))
								thumbnailLink.Thumbnail.Image = (Image)changedImage.Clone();
						}

					thumbnailLink.Thumbnail.SourcePath = defaultImageFilePath;
				}
			});

			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			MainController.Instance.ProcessChanges();

			MainController.Instance.ProcessManager.Run("Loading Library...",
				(cancelationToken, formProgess) =>
					MainController.Instance.MainForm.ActiveForm.Invoke(
						new MethodInvoker(() =>
						{
							MainController.Instance.TabWallbin.UpdateWallbin();
						})));

			MainController.Instance.PopupMessages.ShowInfo("All Thumbnails are Updated");
		}
	}
}