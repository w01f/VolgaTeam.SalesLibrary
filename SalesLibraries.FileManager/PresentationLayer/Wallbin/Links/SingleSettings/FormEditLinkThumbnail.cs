using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using Manina.Windows.Forms;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
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
	public partial class FormEditLinkThumbnail : MetroForm, ILinkSettingsEditForm
	{
		private string _tempThumbnailText;
		private readonly IThumbnailSettingsHolder _sourceLink;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Thumbnail
		};

		private FormEditLinkThumbnail()
		{
			InitializeComponent();

			buttonEditBannerTextFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditBannerTextFont.Click += EditorHelper.FontEdit_Click;

			retractableBarGallery.AddButtons(new[]
				{
					new ButtonInfo
					{
						Logo = Resources.RetractableLogoGallery,
						Tooltip = "Expand gallery"
					}
				});

			spinEditImageSize.EnableSelectAll();
			spinEditImagePadding.EnableSelectAll();

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXPreviewImage.Font = new Font(buttonXPreviewImage.Font.FontFamily, buttonXPreviewImage.Font.Size - 2, buttonXPreviewImage.Font.Style);
				buttonXEnable.Font = new Font(buttonXEnable.Font.FontFamily, buttonXEnable.Font.Size - 2, buttonXEnable.Font.Style);
				buttonXDisable.Font = new Font(buttonXDisable.Font.FontFamily, buttonXDisable.Font.Size - 2, buttonXDisable.Font.Style);
				buttonXShowTextNone.Font = new Font(buttonXShowTextNone.Font.FontFamily, buttonXShowTextNone.Font.Size - 2, buttonXShowTextNone.Font.Style);
				buttonXShowTextLinkName.Font = new Font(buttonXShowTextLinkName.Font.FontFamily, buttonXShowTextLinkName.Font.Size - 2, buttonXShowTextLinkName.Font.Style);
				buttonXShowTextCustom.Font = new Font(buttonXShowTextCustom.Font.FontFamily, buttonXShowTextCustom.Font.Size - 2, buttonXShowTextCustom.Font.Style);
			}
		}

		public FormEditLinkThumbnail(BaseLibraryLink sourceLink) : this()
		{
			_sourceLink = sourceLink as IThumbnailSettingsHolder;
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			Width = 990;
			Height = 670;
			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder, "Site Admin-Link-Thumbnail", false, false);
			Text = String.Format(Text, _sourceLink);
			StartPosition = FormStartPosition.CenterParent;

			LoadSourceImages();
			LoadData();
		}

		private void LoadData()
		{
			retractableBarGallery.Visible = _sourceLink.ShowSourceFilesList;

			buttonXEnable.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();

			buttonXEnable.Checked = _sourceLink.Thumbnail.Enable;
			buttonXDisable.Checked = !buttonXEnable.Checked;

			pictureEditImage.Image = _sourceLink.Thumbnail.Image ?? pictureEditImage.Image;

			switch (_sourceLink.Thumbnail.ImageWidth)
			{
				case 200:
					checkEditImageSize200.Checked = true;
					break;
				case 300:
					checkEditImageSize300.Checked = true;
					break;
				case 400:
					checkEditImageSize400.Checked = true;
					break;
				default:
					checkEditImageSizeCustom.Checked = true;
					spinEditImageSize.EditValue = _sourceLink.Thumbnail.ImageWidth;
					break;
			}

			switch (_sourceLink.Thumbnail.ImagePadding)
			{
				case 0:
					checkEditImagePaddingNone.Checked = true;
					break;
				case 6:
					checkEditImagePadding6.Checked = true;
					break;
				case 8:
					checkEditImagePadding8.Checked = true;
					break;
				case 10:
					checkEditImagePadding10.Checked = true;
					break;
				default:
					checkEditImagePaddingCustom.Checked = true;
					spinEditImagePadding.EditValue = _sourceLink.Thumbnail.ImagePadding;
					break;
			}

			switch (_sourceLink.Thumbnail.ImageAlignement)
			{
				case HorizontalAlignment.Left:
					checkEditImageAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					checkEditImageAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					checkEditImageAlignmentRight.Checked = true;
					break;
			}

			switch (_sourceLink.Thumbnail.BorderSize)
			{
				case 0:
					checkEditBorderSizeNone.Checked = true;
					break;
				case 2:
					checkEditBorderSize2.Checked = true;
					break;
				case 10:
					checkEditBorderSize10.Checked = true;
					break;
			}

			if (_sourceLink.Thumbnail.BorderColor == Color.Black)
				checkEditBorderColorBlack.Checked = true;
			else
				checkEditBorderColorCustom.Checked = true;
			colorEditBorderColor.Color = _sourceLink.Thumbnail.BorderColor;

			if (_sourceLink.Thumbnail.ShadowColor == Color.White)
				checkEditShadowColorNone.Checked = true;
			else if (_sourceLink.Thumbnail.ShadowColor == Color.Black)
				checkEditShadowColorBlack.Checked = true;
			else
				checkEditShadowColorCustom.Checked = true;
			colorEditShadowColor.Color = _sourceLink.Thumbnail.ShadowColor;


			memoEditBannerText.BackColor = _sourceLink.ThumbnailBackColor;
			buttonXShowTextNone.Checked = _sourceLink.Thumbnail.TextMode == ThumbnailTextMode.NoText;
			buttonXShowTextLinkName.Checked = _sourceLink.Thumbnail.TextMode == ThumbnailTextMode.LinkName;
			buttonXShowTextCustom.Checked = _sourceLink.Thumbnail.TextMode == ThumbnailTextMode.CustomText;
			buttonEditBannerTextFont.Tag = _sourceLink.Thumbnail.Font;
			buttonEditBannerTextFont.EditValue = Utils.FontToString(_sourceLink.Thumbnail.Font);
			colorEditBannerTextColor.Color = _sourceLink.Thumbnail.ForeColor;
			switch (_sourceLink.Thumbnail.TextMode)
			{
				case ThumbnailTextMode.LinkName:
					memoEditBannerText.EditValue = _sourceLink.Name;
					break;
				case ThumbnailTextMode.CustomText:
					memoEditBannerText.EditValue = _sourceLink.Thumbnail.Text;
					break;
				default:
					memoEditBannerText.EditValue = null;
					break;
			}
			memoEditBannerText.Font = _sourceLink.Thumbnail.Font;
			memoEditBannerText.Properties.Appearance.Font = _sourceLink.Thumbnail.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = _sourceLink.Thumbnail.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = _sourceLink.Thumbnail.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = _sourceLink.Thumbnail.Font;
			memoEditBannerText.ForeColor = _sourceLink.Thumbnail.ForeColor;
			switch (_sourceLink.Thumbnail.TextPosition)
			{
				case ThumbnailTextPosition.Top:
					checkEditTextPositionTop.Checked = true;
					break;
				case ThumbnailTextPosition.Bottom:
					checkEditTextPositionBottom.Checked = true;
					break;
			}
			switch (_sourceLink.Thumbnail.TextAlignement)
			{
				case HorizontalAlignment.Left:
					checkEditTextAlignmentLeft.Checked = true;
					break;
				case HorizontalAlignment.Center:
					checkEditTextAlignmentCenter.Checked = true;
					break;
				case HorizontalAlignment.Right:
					checkEditTextAlignmentRight.Checked = true;
					break;
			}

			_tempThumbnailText = _sourceLink.Thumbnail.Text;

			var defaultItem = imageListView.Items.FirstOrDefault(item => item.FileName == _sourceLink.Thumbnail.SourcePath) ??
				imageListView.Items.FirstOrDefault();
			if (defaultItem != null)
				defaultItem.Selected = true;
			if (pictureEditImage.Image == null)
				GeneratePreview();
			imageListView.Focus();
		}

		private void SaveData()
		{
			var libraryLink = (BaseLibraryLink)_sourceLink;

			if (buttonXEnable.Checked)
			{
				_sourceLink.Thumbnail.Enable = true;

				_sourceLink.Thumbnail.Image = (Image)pictureEditImage.Image.Clone();
				_sourceLink.Thumbnail.SourcePath = imageListView.SelectedItems.Select(item => item.FileName).FirstOrDefault();

				if (checkEditImageSize200.Checked)
					_sourceLink.Thumbnail.ImageWidth = 200;
				else if (checkEditImageSize300.Checked)
					_sourceLink.Thumbnail.ImageWidth = 300;
				else if (checkEditImageSize400.Checked)
					_sourceLink.Thumbnail.ImageWidth = 400;
				else
					_sourceLink.Thumbnail.ImageWidth = (Int32)spinEditImageSize.Value;

				if (checkEditImagePaddingNone.Checked)
					_sourceLink.Thumbnail.ImagePadding = 0;
				else if (checkEditImagePadding6.Checked)
					_sourceLink.Thumbnail.ImagePadding = 6;
				else if (checkEditImagePadding8.Checked)
					_sourceLink.Thumbnail.ImagePadding = 8;
				else if (checkEditImagePadding10.Checked)
					_sourceLink.Thumbnail.ImagePadding = 10;
				else
					_sourceLink.Thumbnail.ImagePadding = (Int32)spinEditImagePadding.Value;

				if (checkEditImageAlignmentLeft.Checked)
					_sourceLink.Thumbnail.ImageAlignement = HorizontalAlignment.Left;
				else if (checkEditImageAlignmentCenter.Checked)
					_sourceLink.Thumbnail.ImageAlignement = HorizontalAlignment.Center;
				else if (checkEditImageAlignmentRight.Checked)
					_sourceLink.Thumbnail.ImageAlignement = HorizontalAlignment.Right;

				if (checkEditBorderSizeNone.Checked)
					_sourceLink.Thumbnail.BorderSize = 0;
				else if (checkEditBorderSize2.Checked)
					_sourceLink.Thumbnail.BorderSize = 2;
				else if (checkEditBorderSize10.Checked)
					_sourceLink.Thumbnail.BorderSize = 10;

				if (!checkEditBorderSizeNone.Checked && checkEditBorderColorBlack.Checked)
					_sourceLink.Thumbnail.BorderColor = Color.Black;
				else if (!checkEditBorderSizeNone.Checked && checkEditBorderColorCustom.Checked)
					_sourceLink.Thumbnail.BorderColor = colorEditBorderColor.Color;
				else
					_sourceLink.Thumbnail.BorderColor = Color.Black;

				if (checkEditShadowColorNone.Checked)
					_sourceLink.Thumbnail.ShadowColor = Color.White;
				else if (checkEditShadowColorBlack.Checked)
					_sourceLink.Thumbnail.ShadowColor = Color.Black;
				else if (checkEditShadowColorCustom.Checked)
					_sourceLink.Thumbnail.ShadowColor = colorEditShadowColor.Color;
				else
					_sourceLink.Thumbnail.ShadowColor = Color.White;

				if (buttonXShowTextLinkName.Checked)
					_sourceLink.Thumbnail.TextMode = ThumbnailTextMode.LinkName;
				else if (buttonXShowTextCustom.Checked)
				{
					_sourceLink.Thumbnail.TextMode = ThumbnailTextMode.CustomText;
					_sourceLink.Thumbnail.Text = _tempThumbnailText;
				}
				else
					_sourceLink.Thumbnail.TextMode = ThumbnailTextMode.NoText;
				_sourceLink.Thumbnail.Font = buttonEditBannerTextFont.Tag as Font;
				_sourceLink.Thumbnail.ForeColor = colorEditBannerTextColor.Color;
				if (checkEditTextPositionTop.Checked)
					_sourceLink.Thumbnail.TextPosition = ThumbnailTextPosition.Top;
				else if (checkEditTextPositionBottom.Checked)
					_sourceLink.Thumbnail.TextPosition = ThumbnailTextPosition.Bottom;
				if (checkEditTextAlignmentLeft.Checked)
					_sourceLink.Thumbnail.TextAlignement = HorizontalAlignment.Left;
				else if (checkEditTextAlignmentCenter.Checked)
					_sourceLink.Thumbnail.TextAlignement = HorizontalAlignment.Center;
				else if (checkEditTextAlignmentRight.Checked)
					_sourceLink.Thumbnail.TextAlignement = HorizontalAlignment.Right;
			}
			else
				_sourceLink.Thumbnail = SettingsContainer.CreateInstance<ThumbnailSettings>(libraryLink);

			libraryLink.Widget.WidgetType = _sourceLink.Thumbnail.Enable ? libraryLink.Widget.DefaultWidgetType : libraryLink.Widget.WidgetType;
			libraryLink.Banner.Enable = !_sourceLink.Thumbnail.Enable && libraryLink.Banner.Enable;
		}

		private void LoadSourceImages()
		{
			imageListView.Items.Clear();
			var sourceImageFiles = _sourceLink.GetThumbnailSourceFiles();
			if (!sourceImageFiles.Any()) return;
			imageListView.Items.AddRange(sourceImageFiles
				.Select(filePath => new ImageListViewItem(
					filePath,
					Path.GetFileName(filePath))
				{
					Tag = filePath,
					Text = Regex.Match(Path.GetFileNameWithoutExtension(filePath), @"\d+$").Value,
				}).ToArray());
			imageListView.SelectionChanged -= OnSourceImagesSelectionChanged;
			imageListView.SelectionChanged += OnSourceImagesSelectionChanged;
			buttonXPreviewImage.Enabled = buttonXEnable.Checked && imageListView.SelectedItems.Any();
		}

		private void GeneratePreview()
		{
			var selectedImageFilePath = imageListView.SelectedItems.Select(item => item.FileName).FirstOrDefault();
			if (selectedImageFilePath == null) return;

			using (var tempImage = Image.FromFile(selectedImageFilePath))
			{
				var tranformAction = new Func<Image, Image>(image =>
				{
					image = image.Resize(new Size(_sourceLink.Thumbnail.ImageWidth, tempImage.Height));
					if (_sourceLink.Thumbnail.BorderSize > 0)
						image = image.DrawBorder(_sourceLink.Thumbnail.BorderSize, _sourceLink.Thumbnail.BorderColor);
					if (_sourceLink.Thumbnail.ShadowColor != Color.White)
						image = image.DrawShadow(ThumbnailSettings.DefaultShadowSize, _sourceLink.Thumbnail.ShadowColor);
					if (_sourceLink.Thumbnail.ImagePadding > 0)
						image = image.DrawPadding(_sourceLink.Thumbnail.TextEnabled ?
						new Padding(
							_sourceLink.Thumbnail.ImagePadding,
							_sourceLink.Thumbnail.TextPosition == ThumbnailTextPosition.Top ? 0 : _sourceLink.Thumbnail.ImagePadding,
							_sourceLink.Thumbnail.ImagePadding,
							_sourceLink.Thumbnail.TextPosition == ThumbnailTextPosition.Bottom ? 0 : _sourceLink.Thumbnail.ImagePadding) :
						new Padding(_sourceLink.Thumbnail.ImagePadding));
					return image;
				});
				using (var changedImage = tranformAction(tempImage))
					pictureEditImage.Image = (Image)changedImage.Clone();
			}
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				SaveData();

				GeneratePreview();

				SaveData();
			}
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
			xtraTabControlSettings.Enabled = buttonXEnable.Checked;
			buttonXPreviewImage.Enabled = buttonXEnable.Checked && imageListView.SelectedItems.Any();
			if (buttonXEnable.Checked && !imageListView.Items.Any())
				OnRefreshSourceFilesClick(sender, EventArgs.Empty);
		}

		private void OnImageSizeCheckedChanged(object sender, EventArgs e)
		{
			spinEditImageSize.Enabled = checkEditImageSizeCustom.Checked;
		}

		private void OnImagePaddingCheckedChanged(object sender, EventArgs e)
		{
			spinEditImagePadding.Enabled = checkEditImagePaddingCustom.Checked;
		}

		private void OnBorderSizeCheckedChanged(object sender, EventArgs e)
		{
			labelControlBorderColor.Enabled =
				checkEditBorderColorBlack.Enabled =
					checkEditBorderColorCustom.Enabled =
						!checkEditBorderSizeNone.Checked;
			colorEditBorderColor.Enabled = !checkEditBorderSizeNone.Checked && checkEditBorderColorCustom.Checked;
		}

		private void OnBorderColorCheckedChanged(object sender, EventArgs e)
		{
			colorEditBorderColor.Enabled = checkEditBorderColorCustom.Checked;
		}

		private void OnShadowColorCheckedChanged(object sender, EventArgs e)
		{
			colorEditShadowColor.Enabled = checkEditShadowColorCustom.Checked;
		}

		private void OnSourceImagesSelectionChanged(object sender, EventArgs e)
		{
			buttonXPreviewImage.Enabled = buttonXEnable.Checked && imageListView.SelectedItems.Any();
		}

		private void OnPreviewImageClick(object sender, EventArgs e)
		{
			SaveData();
			GeneratePreview();
		}

		private void OnTextModeButtonClick(object sender, EventArgs e)
		{
			var button = (ButtonX)sender;
			if (button.Checked) return;
			buttonXShowTextNone.Checked = false;
			buttonXShowTextLinkName.Checked = false;
			buttonXShowTextCustom.Checked = false;
			button.Checked = true;
		}

		private void OnTextModeButtonCheckedChanged(object sender, EventArgs e)
		{
			labelControlTextFont.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			labelControlTextColor.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			labelControlTextPosition.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			labelControlTextAlignment.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			memoEditBannerText.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			memoEditBannerText.ReadOnly = buttonXShowTextLinkName.Checked;
			buttonEditBannerTextFont.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			colorEditBannerTextColor.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			checkEditTextPositionTop.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			checkEditTextPositionBottom.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			checkEditTextAlignmentLeft.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			checkEditTextAlignmentCenter.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			checkEditTextAlignmentRight.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;

			if (buttonXShowTextNone.Checked)
				memoEditBannerText.EditValue = null;
			else if (buttonXShowTextLinkName.Checked)
				memoEditBannerText.EditValue = _sourceLink.Name;
			else if (buttonXShowTextCustom.Checked && !String.IsNullOrEmpty(_tempThumbnailText))
				memoEditBannerText.EditValue = _tempThumbnailText;
		}

		private void OnTextColorEditValueChanged(object sender, EventArgs e)
		{
			memoEditBannerText.ForeColor = colorEditBannerTextColor.Color;
		}

		private void OnTextFontEditValueChanged(object sender, EventArgs e)
		{
			memoEditBannerText.Font = (Font)buttonEditBannerTextFont.Tag;
			memoEditBannerText.Properties.Appearance.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = memoEditBannerText.Font;
		}

		private void OnBannerTextEditValueChanged(object sender, EventArgs e)
		{
			if (buttonXShowTextCustom.Checked)
				_tempThumbnailText = memoEditBannerText.EditValue as String ?? _tempThumbnailText;
		}

		private void OnFormClick(object sender, EventArgs e)
		{
			buttonXOK.Focus();
		}

		private void OnRefreshSourceFilesClick(object sender, EventArgs e)
		{
			MainController.Instance.ProcessManager.Run("Updating Images...", (cancelationToken, formProgess) =>
			{
				var previewableLink = (PreviewableLink)_sourceLink;
				previewableLink.ClearPreviewContainer();
				var previewContainer = previewableLink.GetPreviewContainer();
				var previewGenerator = previewContainer.GetPreviewGenerator();
				previewContainer.UpdateContent(previewGenerator, cancelationToken);
			});

			LoadSourceImages();

			if (pictureEditImage.Image == null)
			{
				var defaultItem = imageListView.Items.First();
				defaultItem.Selected = true;
				GeneratePreview();
			}
		}

		private void OnTabControlSettingsSelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			buttonXPreviewImage.Visible = xtraTabControlSettings.SelectedTabPage == xtraTabPageImage;
		}
	}
}