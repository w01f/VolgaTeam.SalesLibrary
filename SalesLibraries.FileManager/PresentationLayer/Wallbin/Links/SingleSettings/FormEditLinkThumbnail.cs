using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Manina.Windows.Forms;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.Properties;
using Font = System.Drawing.Font;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;
using Padding = System.Windows.Forms.Padding;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkThumbnail : MetroForm, ILinkSetSettingsEditForm
	{
		private bool _allowHandleEvents = false;
		private string _tempThumbnailText;
		private readonly List<IThumbnailSettingsHolder> _selectedLinks = new List<IThumbnailSettingsHolder>();
		private readonly ILinksGroup _linkGroup;
		private readonly LinkType? _defaultLinkType;

		private IThumbnailSettingsHolder SingleLink => _selectedLinks.FirstOrDefault();

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Thumbnail
		};

		private FormEditLinkThumbnail()
		{
			InitializeComponent();

			buttonEditSingleTextFont.ButtonClick += EditorHelper.OnFontEditButtonClick;
			buttonEditSingleTextFont.Click += EditorHelper.OnFontEditClick;
			buttonEditLinkSetTextFont.ButtonClick += EditorHelper.OnFontEditButtonClick;
			buttonEditLinkSetTextFont.Click += EditorHelper.OnFontEditClick;

			spinEditSingleImagePadding.EnableSelectAll();
			spinEditSingleImageSize.EnableSelectAll();
			spinEditLinkSetImagePadding.EnableSelectAll();
			spinEditLinkSetImageSize.EnableSelectAll();

			retractableBarSingleGallery.AddButtons(new[]
			{
				new ButtonInfo
				{
					Logo = Resources.RetractableLogoGallery,
					Tooltip = "Expand gallery"
				}
			});

			spinEditSingleImageSize.EnableSelectAll();
			spinEditSingleImagePadding.EnableSelectAll();

			layoutControlGroupSingleGallery.Enabled = false;
			layoutControlGroupSingleTextSettings.Enabled = false;
			layoutControlGroupSingleTextFont.Enabled = false;
			layoutControlGroupSingleTextColor.Enabled = false;
			layoutControlGroupSingleTextPosition.Enabled = false;
			layoutControlGroupSingleTextAlignment.Enabled = false;
			layoutControlGroupSingleTextSettings.Enabled = false;

			var scaleFactor = Utils.GetScaleFactor(CreateGraphics().DpiX);

			retractableBarSingleGallery.ContentSize = (Int32)(retractableBarSingleGallery.ContentSize * scaleFactor.Width);

			emptySpaceItemBorder.MinSize = RectangleHelper.ScaleSize(emptySpaceItemBorder.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));

			layoutControlItemLinksTree.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLinksTree.MaxSize, scaleFactor);
			layoutControlItemLinksTree.MinSize = RectangleHelper.ScaleSize(layoutControlItemLinksTree.MinSize, scaleFactor);
			layoutControlItemToggleEnable.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggleEnable.MaxSize, scaleFactor);
			layoutControlItemToggleEnable.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggleEnable.MinSize, scaleFactor);
			layoutControlItemToggleDisable.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggleDisable.MaxSize, scaleFactor);
			layoutControlItemToggleDisable.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggleDisable.MinSize, scaleFactor);
			simpleLabelItemSingleSettingsDescription.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemSingleSettingsDescription.MaxSize, scaleFactor);
			simpleLabelItemSingleSettingsDescription.MinSize = RectangleHelper.ScaleSize(simpleLabelItemSingleSettingsDescription.MinSize, scaleFactor);
			layoutControlItemSingleTextToggleNone.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSingleTextToggleNone.MaxSize, scaleFactor);
			layoutControlItemSingleTextToggleNone.MinSize = RectangleHelper.ScaleSize(layoutControlItemSingleTextToggleNone.MinSize, scaleFactor);
			layoutControlItemSingleTextToggleCustom.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSingleTextToggleCustom.MaxSize, scaleFactor);
			layoutControlItemSingleTextToggleCustom.MinSize = RectangleHelper.ScaleSize(layoutControlItemSingleTextToggleCustom.MinSize, scaleFactor);
			layoutControlItemSingleTextToggleLinkName.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSingleTextToggleLinkName.MaxSize, scaleFactor);
			layoutControlItemSingleTextToggleLinkName.MinSize = RectangleHelper.ScaleSize(layoutControlItemSingleTextToggleLinkName.MinSize, scaleFactor);
			layoutControlItemPreviewImage.MaxSize = RectangleHelper.ScaleSize(layoutControlItemPreviewImage.MaxSize, scaleFactor);
			layoutControlItemPreviewImage.MinSize = RectangleHelper.ScaleSize(layoutControlItemPreviewImage.MinSize, scaleFactor);
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		public FormEditLinkThumbnail(BaseLibraryLink sourceLink) : this()
		{
			_selectedLinks.Add(sourceLink as IThumbnailSettingsHolder);
			Width = Width - layoutControlItemLinksTree.Width;
			layoutControlItemLinksTree.Visibility = LayoutVisibility.Never;
		}

		public FormEditLinkThumbnail(ILinksGroup linkGroup, LinkType? defaultLinkType = null) : this()
		{
			_linkGroup = linkGroup;
			_defaultLinkType = defaultLinkType;
			layoutControlItemLinksTree.Visibility = LayoutVisibility.Always;
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder.LocalPath, "Site Admin-Link-Thumbnail", false, false);
			Text = String.Format(Text,
				_linkGroup != null ?
					String.Format("{0} links", _linkGroup.AllGroupLinks.Count()) :
					_selectedLinks.FirstOrDefault()?.ToString());
			StartPosition = FormStartPosition.CenterParent;

			if (_linkGroup != null)
			{
				linksTreeSelector.LinkSelected += (o, e) =>
				{
					if (_allowHandleEvents)
						SaveData();

					_selectedLinks.Clear();
					_selectedLinks.AddRange(linksTreeSelector.SelectedLinks.OfType<IThumbnailSettingsHolder>());

					LoadData();
				};
				linksTreeSelector.LoadData(_linkGroup, _defaultLinkType, new[] { LinkType.LineBreak });
			}
			else
				LoadData();
		}

		private void LoadData()
		{
			_allowHandleEvents = false;
			simpleLabelItemLinkName.Text = String.Format("<size=+4>{0}</size>",
				_selectedLinks.Count > 1 ?
					String.Format("{0} ({1})", linksTreeSelector.SelectedGroup?.Title, linksTreeSelector.SelectedGroup?.Links.OfType<LibraryObjectLink>().Count()) :
					_selectedLinks.FirstOrDefault()?.ToString());

			if (_selectedLinks.Count > 1)
				LoadLinkSetData();
			else
				LoadSingleLinkData();
			_allowHandleEvents = true;
		}

		private void SaveData()
		{
			if (_selectedLinks.Count > 1)
				SaveLinkSetData();
			else
			{
				SaveSingleLinkData();
				GeneratePreview();
				SaveSingleLinkData();
			}

			foreach (var selectedLink in _selectedLinks)
			{
				var libraryLink = (BaseLibraryLink)selectedLink;
				libraryLink.Widget.WidgetType = selectedLink.Thumbnail.Enable ? libraryLink.Widget.DefaultWidgetType : libraryLink.Widget.WidgetType;
				libraryLink.Banner.Enable = !selectedLink.Thumbnail.Enable && libraryLink.Banner.Enable;
			}
		}

		private void RefreshSourceFiles(IList<IThumbnailSettingsHolder> thubnailLinks)
		{
			if (!thubnailLinks.Any()) return;
			MainController.Instance.ProcessManager.Run("Updating Thumbnail Images...", (cancelationToken, formProgess) =>
			{
				foreach (var link in thubnailLinks)
				{
					var sourceLinks = new List<IPreviewableLink>();
					if (link is LinkBundleLink linkBundleLink)
					{
						if (linkBundleLink.Settings is LinkBundleLinkSettings settings)
						{
							sourceLinks.AddRange(settings.Bundle.Settings.Items
								.OfType<LibraryLinkItem>()
								.Select(item => item.TargetLink)
								.OfType<IPreviewableLink>());
						}
					}
					else if (link is IPreviewableLink previewableLink)
					{
						sourceLinks.Add(previewableLink);
					}

					foreach (var previewableLink in sourceLinks)
					{
						if (link is PreviewableFileLink previewableFileLink)
						{
							if (previewableFileLink.Settings is DocumentLinkSettings settings)
							{
								settings.IsArchiveResource = false;
								settings.GeneratePreviewImages = true;
							}
						}
						var previewGenerator = previewableLink.GetPreviewContainer().GetPreviewGenerator();
						previewableLink.UpdatePreviewContainer(previewGenerator, cancelationToken);
					}
				}
			});
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			SaveData();
		}

		private void OnFormClick(object sender, EventArgs e)
		{
			buttonXOK.Focus();
		}

		private void OnEnableButtonClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXEnable.Checked = false;
			buttonXDisable.Checked = false;
			button.Checked = true;
		}

		private void OnEnableButtonCheckedChanged(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (!button.Checked) return;
			layoutControlGroupSingleGallery.Enabled = buttonXEnable.Checked;
			layoutControlGroupSingleTextSettings.Enabled = buttonXEnable.Checked;
			layoutControlGroupLinkSetSettings.Enabled = buttonXEnable.Checked;
			layoutControlItemPreviewImage.Enabled = buttonXEnable.Checked && imageListViewSingle.SelectedItems.Any();
			layoutControlItemSingleGalleryContent.Enabled = imageListViewSingle.Visible = buttonXEnable.Checked;
			if (!(_selectedLinks.Count > 1) && buttonXEnable.Checked && !imageListViewSingle.Items.Any())
				OnRefreshSourceFilesClick(sender, EventArgs.Empty);
		}

		#region Single Settings Processing
		private void LoadSingleLinkData()
		{
			layoutControlGroupSingleGallery.Visibility = LayoutVisibility.Always;
			layoutControlGroupSingleTextSettings.Visibility = LayoutVisibility.Always;
			layoutControlGroupLinkSetSettings.Visibility = LayoutVisibility.Never;
			tabbedControlSingleSettings.SelectedTabPage = layoutControlGroupSingleGallery;

			if (SingleLink == null) return;

			LoadSourceImages();

			buttonXEnable.Checked = SingleLink.Thumbnail.Enable;
			buttonXDisable.Checked = !buttonXEnable.Checked;

			retractableBarSingleGallery.Visible = SingleLink.ShowSourceFilesList;

			pictureEditSingleImage.Image = SingleLink.Thumbnail.Image ?? pictureEditSingleImage.Image;

			switch (SingleLink.Thumbnail.ImageWidth)
			{
				case 200:
					checkEditSingleImageSize200.Checked = true;
					break;
				case 300:
					checkEditSingleImageSize300.Checked = true;
					break;
				default:
					checkEditSingleImageSizeCustom.Checked = true;
					spinEditSingleImageSize.EditValue = SingleLink.Thumbnail.ImageWidth;
					break;
			}

			switch (SingleLink.Thumbnail.ImagePadding)
			{
				case 0:
					checkEditSingleImagePaddingNone.Checked = true;
					break;
				case 8:
					checkEditSingleImagePadding8.Checked = true;
					break;
				case 10:
					checkEditSingleImagePadding10.Checked = true;
					break;
				default:
					checkEditSingleImagePaddingCustom.Checked = true;
					spinEditSingleImagePadding.EditValue = SingleLink.Thumbnail.ImagePadding;
					break;
			}

			switch (SingleLink.Thumbnail.ImageAlignement)
			{
				case HorizontalAlignment.Left:
					checkEditSingleImageAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					checkEditSingleImageAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					checkEditSingleImageAlignmentRight.Checked = true;
					break;
			}

			switch (SingleLink.Thumbnail.BorderSize)
			{
				case 0:
					checkEditSingleBorderSizeNone.Checked = true;
					break;
				case 2:
					checkEditSingleBorderSize2.Checked = true;
					break;
				case 10:
					checkEditSingleBorderSize10.Checked = true;
					break;
			}
			colorEditSingleBorderColor.Color = SingleLink.Thumbnail.BorderColor;

			if (SingleLink.Thumbnail.ShadowColor == Color.White)
			{
				checkEditSingleShadowColor.Checked = false;
				colorEditSingleShadowColor.Color = ThumbnailSettings.DefaultShadowColor;
			}
			else
			{
				checkEditSingleShadowColor.Checked = true;
				colorEditSingleShadowColor.Color = SingleLink.Thumbnail.ShadowColor;
			}

			memoEditBannerText.BackColor = SingleLink.ThumbnailBackColor;
			buttonXSingleShowTextNone.Checked = SingleLink.Thumbnail.TextMode == ThumbnailTextMode.NoText;
			buttonXSingleShowTextLinkName.Checked = SingleLink.Thumbnail.TextMode == ThumbnailTextMode.LinkName;
			buttonXSingleShowTextCustom.Checked = SingleLink.Thumbnail.TextMode == ThumbnailTextMode.CustomText;
			buttonEditSingleTextFont.Tag = SingleLink.Thumbnail.Font;
			buttonEditSingleTextFont.EditValue = Utils.FontToString(SingleLink.Thumbnail.Font);
			colorEditSingleTextColor.Color = SingleLink.Thumbnail.ForeColor;
			switch (SingleLink.Thumbnail.TextMode)
			{
				case ThumbnailTextMode.LinkName:
					memoEditBannerText.EditValue = SingleLink.Name;
					break;
				case ThumbnailTextMode.CustomText:
					memoEditBannerText.EditValue = SingleLink.Thumbnail.Text;
					break;
				default:
					memoEditBannerText.EditValue = null;
					break;
			}
			memoEditBannerText.Font = SingleLink.Thumbnail.Font;
			memoEditBannerText.Properties.Appearance.Font = SingleLink.Thumbnail.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = SingleLink.Thumbnail.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = SingleLink.Thumbnail.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = SingleLink.Thumbnail.Font;
			memoEditBannerText.ForeColor = SingleLink.Thumbnail.ForeColor;
			switch (SingleLink.Thumbnail.TextPosition)
			{
				case ThumbnailTextPosition.Top:
					checkEditSingleTextPositionTop.Checked = true;
					break;
				case ThumbnailTextPosition.Bottom:
					checkEditSingleTextPositionBottom.Checked = true;
					break;
			}
			switch (SingleLink.Thumbnail.TextAlignement)
			{
				case HorizontalAlignment.Left:
					checkEditSingleTextAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					checkEditSingleTextAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					checkEditSingleTextAlignmentRight.Checked = true;
					break;
			}

			_tempThumbnailText = SingleLink.Thumbnail.Text;

			var defaultItem = imageListViewSingle.Items.FirstOrDefault(item => item.FileName == SingleLink.Thumbnail.SourcePath) ??
				imageListViewSingle.Items.FirstOrDefault();
			if (defaultItem != null)
				defaultItem.Selected = true;
			if (pictureEditSingleImage.Image == null)
				GeneratePreview();
			imageListViewSingle.Focus();
		}

		private void SaveSingleLinkData()
		{
			if (SingleLink == null) return;
			if (buttonXEnable.Checked)
			{
				SingleLink.Thumbnail.Enable = true;
				SingleLink.Thumbnail.Image = (Image)pictureEditSingleImage.Image?.Clone();
				SingleLink.Thumbnail.SourcePath = imageListViewSingle.SelectedItems.Select(item => item.FileName).FirstOrDefault();

				if (checkEditSingleImageSize200.Checked)
					SingleLink.Thumbnail.ImageWidth = 200;
				else if (checkEditSingleImageSize300.Checked)
					SingleLink.Thumbnail.ImageWidth = 300;
				else
					SingleLink.Thumbnail.ImageWidth = (Int32)spinEditSingleImageSize.Value;

				if (checkEditSingleImagePaddingNone.Checked)
					SingleLink.Thumbnail.ImagePadding = 0;
				else if (checkEditSingleImagePadding8.Checked)
					SingleLink.Thumbnail.ImagePadding = 8;
				else if (checkEditSingleImagePadding10.Checked)
					SingleLink.Thumbnail.ImagePadding = 10;
				else
					SingleLink.Thumbnail.ImagePadding = (Int32)spinEditSingleImagePadding.Value;

				if (checkEditSingleImageAlignmentLeft.Checked)
					SingleLink.Thumbnail.ImageAlignement = HorizontalAlignment.Left;
				else if (checkEditSingleImageAlignmentCenter.Checked)
					SingleLink.Thumbnail.ImageAlignement = HorizontalAlignment.Center;
				else if (checkEditSingleImageAlignmentRight.Checked)
					SingleLink.Thumbnail.ImageAlignement = HorizontalAlignment.Right;

				if (checkEditSingleBorderSizeNone.Checked)
					SingleLink.Thumbnail.BorderSize = 0;
				else if (checkEditSingleBorderSize2.Checked)
					SingleLink.Thumbnail.BorderSize = 2;
				else if (checkEditSingleBorderSize10.Checked)
					SingleLink.Thumbnail.BorderSize = 10;

				SingleLink.Thumbnail.BorderColor = colorEditSingleBorderColor.Color;

				if (!checkEditSingleBorderSizeNone.Checked && checkEditSingleShadowColor.Checked)
					SingleLink.Thumbnail.ShadowColor = colorEditSingleShadowColor.Color;
				else
					SingleLink.Thumbnail.ShadowColor = Color.White;

				if (buttonXSingleShowTextLinkName.Checked)
					SingleLink.Thumbnail.TextMode = ThumbnailTextMode.LinkName;
				else if (buttonXSingleShowTextCustom.Checked)
				{
					SingleLink.Thumbnail.TextMode = ThumbnailTextMode.CustomText;
					SingleLink.Thumbnail.Text = _tempThumbnailText;
				}
				else
					SingleLink.Thumbnail.TextMode = ThumbnailTextMode.NoText;
				SingleLink.Thumbnail.Font = buttonEditSingleTextFont.Tag as Font;
				SingleLink.Thumbnail.ForeColor = colorEditSingleTextColor.Color;
				if (checkEditSingleTextPositionTop.Checked)
					SingleLink.Thumbnail.TextPosition = ThumbnailTextPosition.Top;
				else if (checkEditSingleTextPositionBottom.Checked)
					SingleLink.Thumbnail.TextPosition = ThumbnailTextPosition.Bottom;
				if (checkEditSingleTextAlignmentLeft.Checked)
					SingleLink.Thumbnail.TextAlignement = HorizontalAlignment.Left;
				else if (checkEditSingleTextAlignmentCenter.Checked)
					SingleLink.Thumbnail.TextAlignement = HorizontalAlignment.Center;
				else if (checkEditSingleTextAlignmentRight.Checked)
					SingleLink.Thumbnail.TextAlignement = HorizontalAlignment.Right;
			}
			else
				SingleLink.Thumbnail = SettingsContainer.CreateInstance<ThumbnailSettings>((BaseLibraryLink)SingleLink);
		}

		private void LoadSourceImages()
		{
			if (SingleLink == null) return;

			imageListViewSingle.Items.Clear();
			var sourceImageFiles = SingleLink.GetThumbnailSourceFiles();
			if (!sourceImageFiles.Any()) return;
			imageListViewSingle.Items.AddRange(sourceImageFiles
				.Select(filePath => new ImageListViewItem(
					filePath,
					Path.GetFileName(filePath))
				{
					Tag = filePath,
					Text = Regex.Match(Path.GetFileNameWithoutExtension(filePath), @"\d+$").Value,
				}).ToArray());
			imageListViewSingle.SelectionChanged -= OnSingleSourceImagesSelectionChanged;
			imageListViewSingle.SelectionChanged += OnSingleSourceImagesSelectionChanged;
			layoutControlItemPreviewImage.Enabled = buttonXEnable.Checked && imageListViewSingle.SelectedItems.Any();
		}

		private void GeneratePreview()
		{
			if (SingleLink == null) return;

			var selectedImageFilePath = imageListViewSingle.SelectedItems.Select(item => item.FileName).FirstOrDefault();
			if (selectedImageFilePath == null) return;

			using (var tempImage = Image.FromFile(selectedImageFilePath))
			{
				var tranformAction = new Func<Image, Image>(image =>
				{
					image = image.Resize(new Size(SingleLink.Thumbnail.ImageWidth, tempImage.Height));
					if (SingleLink.Thumbnail.BorderSize > 0)
					{
						image = image.DrawBorder(SingleLink.Thumbnail.BorderSize, SingleLink.Thumbnail.BorderColor);
						if (SingleLink.Thumbnail.ShadowColor != Color.White)
							image = image.DrawShadow(ThumbnailSettings.DefaultShadowSize, SingleLink.Thumbnail.ShadowColor);
					}
					if (SingleLink.Thumbnail.ImagePadding > 0)
						image = image.DrawPadding(SingleLink.Thumbnail.TextEnabled ?
						new Padding(
							SingleLink.Thumbnail.ImagePadding,
							SingleLink.Thumbnail.TextPosition == ThumbnailTextPosition.Top ? 0 : SingleLink.Thumbnail.ImagePadding,
							SingleLink.Thumbnail.ImagePadding,
							SingleLink.Thumbnail.TextPosition == ThumbnailTextPosition.Bottom ? 0 : SingleLink.Thumbnail.ImagePadding) :
						new Padding(SingleLink.Thumbnail.ImagePadding));
					return image;
				});
				using (var changedImage = tranformAction(tempImage))
					pictureEditSingleImage.Image = (Image)changedImage.Clone();
			}
		}

		private void OnRefreshSourceFilesClick(object sender, EventArgs e)
		{
			if (SingleLink == null) return;

			RefreshSourceFiles(new[] { SingleLink });

			LoadSourceImages();

			if (pictureEditSingleImage.Image == null)
			{
				var defaultItem = imageListViewSingle.Items.FirstOrDefault();
				if (defaultItem != null)
				{
					defaultItem.Selected = true;
					GeneratePreview();
				}
			}
		}

		private void OnSingleImageSizeCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemSingleImageSizeCustomEditor.Enabled = checkEditSingleImageSizeCustom.Checked;
		}

		private void OnSingleImagePaddingCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemSingleImagePaddingCustomEditor.Enabled = checkEditSingleImagePaddingCustom.Checked;
		}

		private void OnSingleBorderSizeCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemSingleBorderColorEditor.Visibility = !checkEditSingleBorderSizeNone.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlGroupSingleShadowColor.Visibility = !checkEditSingleBorderSizeNone.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void OnSingleShadowColorCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemSingleShadowColorCustomEditor.Enabled = checkEditSingleShadowColor.Checked;
		}

		private void OnSingleSourceImagesSelectionChanged(object sender, EventArgs e)
		{
			layoutControlItemPreviewImage.Enabled = buttonXEnable.Checked && imageListViewSingle.SelectedItems.Any();
			if (!_allowHandleEvents) return;
			OnSinglePreviewImageClick(sender, e);
		}

		private void OnSinglePreviewImageClick(object sender, EventArgs e)
		{
			SaveData();
			GeneratePreview();
		}

		private void OnSingleTextModeButtonClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXSingleShowTextNone.Checked = false;
			buttonXSingleShowTextLinkName.Checked = false;
			buttonXSingleShowTextCustom.Checked = false;
			button.Checked = true;
		}

		private void OnSingleTextModeButtonCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemSingleThumbnailText.Enabled = buttonXSingleShowTextLinkName.Checked || buttonXSingleShowTextCustom.Checked;
			memoEditBannerText.ReadOnly = buttonXSingleShowTextLinkName.Checked;

			layoutControlGroupSingleTextFont.Enabled = buttonXSingleShowTextLinkName.Checked || buttonXSingleShowTextCustom.Checked;
			layoutControlGroupSingleTextColor.Enabled = buttonXSingleShowTextLinkName.Checked || buttonXSingleShowTextCustom.Checked;
			layoutControlGroupSingleTextPosition.Enabled = buttonXSingleShowTextLinkName.Checked || buttonXSingleShowTextCustom.Checked;
			layoutControlGroupSingleTextAlignment.Enabled = buttonXSingleShowTextLinkName.Checked || buttonXSingleShowTextCustom.Checked;

			if (buttonXSingleShowTextNone.Checked)
				memoEditBannerText.EditValue = null;
			else if (buttonXSingleShowTextLinkName.Checked)
				memoEditBannerText.EditValue = SingleLink.Name;
			else if (buttonXSingleShowTextCustom.Checked && !String.IsNullOrEmpty(_tempThumbnailText))
				memoEditBannerText.EditValue = _tempThumbnailText;
		}

		private void OnSingleTextColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditBannerText.ForeColor = colorEditSingleTextColor.Color;
		}

		private void OnSingleTextFontEditValueChanged(object sender, EventArgs e)
		{
			memoEditBannerText.Font = (Font)buttonEditSingleTextFont.Tag;
			memoEditBannerText.Properties.Appearance.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = memoEditBannerText.Font;
		}

		private void OnSingleBannerTextEditValueChanged(object sender, EventArgs e)
		{
			if (buttonXSingleShowTextCustom.Checked)
				_tempThumbnailText = memoEditBannerText.EditValue as String ?? _tempThumbnailText;
		}

		private void OnTabControlSingleSettingsSelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
		{
			layoutControlItemPreviewImage.Visibility = tabbedControlSingleSettings.SelectedTabPage == layoutControlGroupSingleGallery ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void OnTabControlSingleSettingsSelectedPageChanging(object sender, LayoutTabPageChangingEventArgs e)
		{
			e.Cancel = !buttonXEnable.Checked;
		}
		#endregion

		#region Link Set Settings Processing
		private void LoadLinkSetData()
		{
			layoutControlGroupSingleGallery.Visibility = LayoutVisibility.Never;
			layoutControlGroupSingleTextSettings.Visibility = LayoutVisibility.Never;
			layoutControlGroupLinkSetSettings.Visibility = LayoutVisibility.Always;

			var defaultLink = _selectedLinks.First();

			buttonXEnable.Checked = defaultLink.Thumbnail.Enable;
			buttonXDisable.Checked = !buttonXEnable.Checked;

			switch (defaultLink.Thumbnail.ImageWidth)
			{
				case 200:
					checkEditLinkSetImageSize200.Checked = true;
					break;
				case 300:
					checkEditLinkSetImageSize300.Checked = true;
					break;
				default:
					checkEditLinkSetImageSizeCustom.Checked = true;
					spinEditLinkSetImageSize.EditValue = defaultLink.Thumbnail.ImageWidth;
					break;
			}

			switch (defaultLink.Thumbnail.ImagePadding)
			{
				case 0:
					checkEditLinkSetImagePaddingNone.Checked = true;
					break;
				case 8:
					checkEditLinkSetImagePadding8.Checked = true;
					break;
				case 10:
					checkEditLinkSetImagePadding10.Checked = true;
					break;
				default:
					checkEditLinkSetImagePaddingCustom.Checked = true;
					spinEditLinkSetImagePadding.EditValue = defaultLink.Thumbnail.ImagePadding;
					break;
			}

			switch (defaultLink.Thumbnail.ImageAlignement)
			{
				case HorizontalAlignment.Left:
					checkEditLinkSetImageAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					checkEditLinkSetImageAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					checkEditLinkSetImageAlignmentRight.Checked = true;
					break;
			}

			switch (defaultLink.Thumbnail.BorderSize)
			{
				case 0:
					checkEditLinkSetBorderNone.Checked = true;
					break;
				case 2:
					checkEditLinkSetBorder2.Checked = true;
					break;
				case 10:
					checkEditLinkSetBorder10.Checked = true;
					break;
			}
			colorEditLinkSetBorderColor.Color = defaultLink.Thumbnail.BorderColor;

			if (defaultLink.Thumbnail.ShadowColor == Color.White)
			{
				checkEditLinkSetShadowColor.Checked = false;
				colorEditLinkSetShadowColor.Color = ThumbnailSettings.DefaultShadowColor;
			}
			else
			{
				checkEditLinkSetShadowColor.Checked = true;
				colorEditLinkSetShadowColor.Color = defaultLink.Thumbnail.ShadowColor;
			}


			checkEditLinkSetShowText.Checked = defaultLink.Thumbnail.TextMode == ThumbnailTextMode.LinkName ||
											defaultLink.Thumbnail.TextMode == ThumbnailTextMode.CustomText;
			buttonEditLinkSetTextFont.Tag = defaultLink.Thumbnail.Font;
			buttonEditLinkSetTextFont.EditValue = Utils.FontToString(defaultLink.Thumbnail.Font);
			colorEditLinkSetTextColor.Color = defaultLink.Thumbnail.ForeColor;
			switch (defaultLink.Thumbnail.TextMode)
			{
				case ThumbnailTextMode.LinkName:
					memoEditBannerText.EditValue = defaultLink.Name;
					break;
				case ThumbnailTextMode.CustomText:
					memoEditBannerText.EditValue = defaultLink.Thumbnail.Text;
					break;
				default:
					memoEditBannerText.EditValue = null;
					break;
			}

			switch (defaultLink.Thumbnail.TextPosition)
			{
				case ThumbnailTextPosition.Top:
					checkEditLinkSetTextPositionTop.Checked = true;
					break;
				case ThumbnailTextPosition.Bottom:
					checkEditLinkSetTextPositionBottom.Checked = true;
					break;
			}
			switch (defaultLink.Thumbnail.TextAlignement)
			{
				case HorizontalAlignment.Left:
					checkEditLinkSetTextAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					checkEditLinkSetTextAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					checkEditLinkSetTextAlignmentRight.Checked = true;
					break;
			}
		}

		private void SaveLinkSetData()
		{
			if (buttonXEnable.Checked)
				RefreshSourceFiles(_selectedLinks.Where(link => !link.GetThumbnailSourceFiles().Any()).ToList());

			foreach (var selectedLink in _selectedLinks)
			{
				if (buttonXEnable.Checked)
				{
					selectedLink.Thumbnail.Enable = true;

					if (checkEditLinkSetImageSize200.Checked)
						selectedLink.Thumbnail.ImageWidth = 200;
					else if (checkEditLinkSetImageSize300.Checked)
						selectedLink.Thumbnail.ImageWidth = 300;
					else
						selectedLink.Thumbnail.ImageWidth = (Int32)spinEditLinkSetImageSize.Value;

					if (checkEditLinkSetImagePaddingNone.Checked)
						selectedLink.Thumbnail.ImagePadding = 0;
					else if (checkEditLinkSetImagePadding8.Checked)
						selectedLink.Thumbnail.ImagePadding = 8;
					else if (checkEditLinkSetImagePadding10.Checked)
						selectedLink.Thumbnail.ImagePadding = 10;
					else
						selectedLink.Thumbnail.ImagePadding = (Int32)spinEditLinkSetImagePadding.Value;

					if (checkEditLinkSetImageAlignmentLeft.Checked)
						selectedLink.Thumbnail.ImageAlignement = HorizontalAlignment.Left;
					else if (checkEditLinkSetImageAlignmentCenter.Checked)
						selectedLink.Thumbnail.ImageAlignement = HorizontalAlignment.Center;
					else if (checkEditLinkSetImageAlignmentRight.Checked)
						selectedLink.Thumbnail.ImageAlignement = HorizontalAlignment.Right;

					if (checkEditLinkSetBorderNone.Checked)
						selectedLink.Thumbnail.BorderSize = 0;
					else if (checkEditLinkSetBorder2.Checked)
						selectedLink.Thumbnail.BorderSize = 2;
					else if (checkEditLinkSetBorder10.Checked)
						selectedLink.Thumbnail.BorderSize = 10;

					selectedLink.Thumbnail.BorderColor = colorEditLinkSetBorderColor.Color;

					if (!checkEditLinkSetBorderNone.Checked && checkEditLinkSetShadowColor.Checked)
						selectedLink.Thumbnail.ShadowColor = colorEditLinkSetShadowColor.Color;
					else
						selectedLink.Thumbnail.ShadowColor = Color.White;

					selectedLink.Thumbnail.TextMode = checkEditLinkSetShowText.Checked ? ThumbnailTextMode.LinkName : ThumbnailTextMode.NoText;
					selectedLink.Thumbnail.Font = buttonEditLinkSetTextFont.Tag as Font;
					selectedLink.Thumbnail.ForeColor = colorEditLinkSetTextColor.Color;
					if (checkEditLinkSetTextPositionTop.Checked)
						selectedLink.Thumbnail.TextPosition = ThumbnailTextPosition.Top;
					else if (checkEditLinkSetTextPositionBottom.Checked)
						selectedLink.Thumbnail.TextPosition = ThumbnailTextPosition.Bottom;
					if (checkEditLinkSetTextAlignmentLeft.Checked)
						selectedLink.Thumbnail.TextAlignement = HorizontalAlignment.Left;
					else if (checkEditLinkSetTextAlignmentCenter.Checked)
						selectedLink.Thumbnail.TextAlignement = HorizontalAlignment.Center;
					else if (checkEditLinkSetTextAlignmentRight.Checked)
						selectedLink.Thumbnail.TextAlignement = HorizontalAlignment.Right;

					var defaultImageFilePath = selectedLink.GetThumbnailSourceFiles().FirstOrDefault();
					if (defaultImageFilePath != null)
						using (var tempImage = Image.FromFile(defaultImageFilePath))
						{
							var tranformAction = new Func<Image, Image>(image =>
							{
								image = image.Resize(new Size(selectedLink.Thumbnail.ImageWidth, tempImage.Height));
								if (selectedLink.Thumbnail.BorderSize > 0)
								{
									image = image.DrawBorder(selectedLink.Thumbnail.BorderSize, selectedLink.Thumbnail.BorderColor);
									if (selectedLink.Thumbnail.ShadowColor != Color.White)
										image = image.DrawShadow(ThumbnailSettings.DefaultShadowSize, selectedLink.Thumbnail.ShadowColor);
								}
								if (selectedLink.Thumbnail.ImagePadding > 0)
									image = image.DrawPadding(selectedLink.Thumbnail.TextEnabled ?
										new Padding(
											selectedLink.Thumbnail.ImagePadding,
											selectedLink.Thumbnail.TextPosition == ThumbnailTextPosition.Top ? 0 : selectedLink.Thumbnail.ImagePadding,
											selectedLink.Thumbnail.ImagePadding,
											selectedLink.Thumbnail.TextPosition == ThumbnailTextPosition.Bottom ? 0 : selectedLink.Thumbnail.ImagePadding) :
										new Padding(selectedLink.Thumbnail.ImagePadding));
								return image;
							});
							using (var changedImage = tranformAction(tempImage))
								selectedLink.Thumbnail.Image = (Image)changedImage.Clone();
						}
					selectedLink.Thumbnail.SourcePath = defaultImageFilePath;
				}
				else
					selectedLink.Thumbnail = SettingsContainer.CreateInstance<ThumbnailSettings>((BaseLibraryLink)selectedLink);
			}
		}

		private void OnLinkSetImageSizeCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemLinkSetImageSizeCustomEditor.Enabled = checkEditLinkSetImageSizeCustom.Checked;
		}

		private void OnLinkSetImagePaddingCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemLinkSetImagePaddingCustomEditor.Enabled = checkEditLinkSetImagePaddingCustom.Checked;
		}

		private void OnLinkSetBorderCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemLinkSetBorderColorEditor.Visibility = !checkEditLinkSetBorderNone.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlItemLinkSetShadowColorToggle.Visibility = !checkEditLinkSetBorderNone.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
			layoutControlItemLinkSetShadowColorEditor.Visibility = !checkEditLinkSetBorderNone.Checked ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void OnLinkSetShadowColorCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemLinkSetShadowColorEditor.Enabled = checkEditLinkSetShadowColor.Checked;
		}

		private void OnLinkSetShowTextCheckedChanged(object sender, EventArgs e)
		{
			layoutControlGroupLinkSetTextSettings.Enabled = checkEditLinkSetShowText.Checked;
		}
		#endregion
	}
}