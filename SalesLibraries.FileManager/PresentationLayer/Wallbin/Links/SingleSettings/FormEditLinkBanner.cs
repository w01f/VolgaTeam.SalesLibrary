using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;
using SalesLibraries.FileManager.Properties;
using Font = System.Drawing.Font;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkBanner : MetroForm, ILinkSetSettingsEditForm
	{
		private const string ImageTitleFormat = "<size=+4>{0}</size><br><color=lightgray>{1}</color>";
		private const int BannerThumbnailHeight = 64;

		private bool _allowHandleEvents;
		private string _tempBannerText;
		private string _originalImageName;
		private readonly BaseLibraryLink _sourceLink;
		private readonly ILinksGroup _sourceLinkGroup;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Banner,
		};

		private string BannerTitle
			=>
				_sourceLinkGroup != null
					? String.Format("Library links ({0})", _sourceLinkGroup.AllGroupLinks.Count())
					: _sourceLink.LinkInfoDisplayName;

		private FormEditLinkBanner()
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

			retractableBarGallery.ContentSize = retractableBarGallery.Width;

			layoutControlGroupGallery.Enabled = false;
			layoutControlGroupImageSettings.Enabled = false;
			layoutControlGroupTextSettings.Enabled = false;
			layoutControlGroupTextFont.Enabled = false;

			layoutControlItemTitle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTitle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemTitle.MinSize = RectangleHelper.ScaleSize(layoutControlItemTitle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemToggleEnable.MaxSize = RectangleHelper.ScaleSize(layoutControlItemToggleEnable.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemToggleEnable.MinSize = RectangleHelper.ScaleSize(layoutControlItemToggleEnable.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDisableToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDisableToggle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDisableToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDisableToggle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSearchButton.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSearchButton.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSearchButton.MinSize = RectangleHelper.ScaleSize(layoutControlItemSearchButton.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemGalleryDescription.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemGalleryDescription.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemGalleryDescription.MinSize = RectangleHelper.ScaleSize(simpleLabelItemGalleryDescription.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemImageDescription.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemImageDescription.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemImageDescription.MinSize = RectangleHelper.ScaleSize(simpleLabelItemImageDescription.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTextDescription.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTextDescription.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTextDescription.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTextDescription.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemTextToggleNone.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTextToggleNone.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemTextToggleNone.MinSize = RectangleHelper.ScaleSize(layoutControlItemTextToggleNone.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemTextToggleCustom.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTextToggleCustom.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemTextToggleCustom.MinSize = RectangleHelper.ScaleSize(layoutControlItemTextToggleCustom.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemTextToggleLinkName.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTextToggleLinkName.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemTextToggleLinkName.MinSize = RectangleHelper.ScaleSize(layoutControlItemTextToggleLinkName.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemButtonOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemButtonOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemButtonOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemButtonOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemButtonCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemButtonCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemButtonCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemButtonCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public FormEditLinkBanner(BaseLibraryLink sourceLink) : this()
		{
			_sourceLink = sourceLink;
			labelControlTitle.Text = String.Format(ImageTitleFormat, BannerTitle, String.Empty);
		}

		public FormEditLinkBanner(ILinksGroup linkGroup, LinkType? defaultLinkType = null) : this()
		{
			_sourceLinkGroup = linkGroup;
			_sourceLink = _sourceLinkGroup.AllGroupLinks
				.FirstOrDefault(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value);
			labelControlTitle.Text = String.Format(ImageTitleFormat, BannerTitle, String.Empty);
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder, "Site Admin-Link-Banner", false, false);
			Text = String.Format(Text,
				_sourceLinkGroup != null ?
					String.Format("{0} links", _sourceLinkGroup.AllGroupLinks.Count()) :
					_sourceLink.ToString());
			StartPosition = FormStartPosition.CenterParent;
			LoadData();
		}

		private void LoadData()
		{
			_allowHandleEvents = false;

			xtraTabControlGallery.TabPages.Clear();
			xtraTabControlGallery.TabPages.AddRange(
				MainController.Instance.Lists.Banners.Items.Select(imageGroup =>
				{
					var tabPage = BaseLinkImagesContainer.Create<Banner>(imageGroup);
					tabPage.SelectedImageChanged += OnSelectedBannerChanged;
					tabPage.OnImageDoubleClick += OnImageDoubleClick;
					return (XtraTabPage)tabPage;
				}).ToArray());
			xtraTabControlGallery.SelectedPageChanged += (o, e) =>
			{
				((BaseLinkImagesContainer)e.Page).Init();
				var galleryNode = e.Page.Tag as TreeNode;
				if (galleryNode == null)
				{
					galleryNode = treeViewGallery.Nodes.Insert(xtraTabControlGallery.TabPages.IndexOf(e.Page), e.Page.Text);
					galleryNode.Tag = e.Page;
					e.Page.Tag = galleryNode;
					_allowHandleEvents = false;
					treeViewGallery.SelectedNode = galleryNode;
					_allowHandleEvents = true;
				}
			};
			((BaseLinkImagesContainer)xtraTabControlGallery.SelectedTabPage).Init();
			foreach (var galleryPage in xtraTabControlGallery.TabPages.OfType<BaseLinkImagesContainer>().ToList())
			{
				if (!galleryPage.PageVisible) continue;
				var galleryNode = treeViewGallery.Nodes.Add(galleryPage.Text);
				galleryNode.Tag = galleryPage;
				galleryPage.Tag = galleryNode;
				if (xtraTabControlGallery.SelectedTabPage == galleryPage)
				{
					treeViewGallery.SelectedNode = galleryNode;
					labelControlSelectedGalleryName.Text = galleryPage.Text;
				}
			}
			treeViewGallery.AfterSelect += (o, e) =>
			{
				var galleryPage = treeViewGallery.SelectedNode?.Tag as XtraTabPage;
				labelControlSelectedGalleryName.Text = galleryPage?.Text;
				if (!_allowHandleEvents) return;
				xtraTabControlGallery.SelectedTabPage = galleryPage;
			};

			layoutControlItemToggleEnable.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			memoEditBannerText.BackColor = _sourceLink.BannerBackColor;

			LoadBanner(_sourceLink.Banner);
			_tempBannerText = _sourceLink.Banner.Text;
			checkEditTextWordWrap.Checked = _sourceLink.Settings.TextWordWrap;

			_allowHandleEvents = true;
		}

		private BannerSettings GetDefaultBanner()
		{
			var banner = SettingsContainer.CreateInstance<BannerSettings>(_sourceLink,
				MainController.Instance.Settings.DefaultBannerSettingsEncoded);
			banner.Text = _tempBannerText;
			return banner;
		}

		private BannerSettings GetEmptyBanner(BaseLibraryLink parentLink)
		{
			var banner = SettingsContainer.CreateInstance<BannerSettings>(parentLink);
			banner.Text = _tempBannerText;
			return banner;
		}

		private void LoadBanner(BannerSettings banner)
		{
			buttonXEnable.Checked = banner.Enable && MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
			buttonXDisable.Checked = !buttonXEnable.Checked;
			checkEditInvert.Checked = banner.Inverted;
			colorEditInversionColor.EditValue = banner.InversionColor;
			checkEditVerticalAlignmentTop1.Checked = banner.ImageVerticalAlignement == VerticalAlignment.Top;
			checkEditVerticalAlignmentTop2.Checked = banner.ImageVerticalAlignement == VerticalAlignment.Top;
			if (banner.Enable && banner.Image != null)
			{
				labelControlTitle.Tag = banner.Image;
				_originalImageName = banner.ImageName;
			}
			else
			{
				labelControlTitle.Tag = null;
				_originalImageName = null;
			}
			switch (banner.ImageAlignement)
			{
				case HorizontalAlignment.Left:
					checkEditHorizontalAlignmentLeft.Checked = true;
					checkEditHorizontalAlignmentCenter.Checked = false;
					checkEditHorizontalAlignmentRight.Checked = false;
					break;
				case HorizontalAlignment.Center:
					checkEditHorizontalAlignmentLeft.Checked = false;
					checkEditHorizontalAlignmentCenter.Checked = true;
					checkEditHorizontalAlignmentRight.Checked = false;
					break;
				case HorizontalAlignment.Right:
					checkEditHorizontalAlignmentLeft.Checked = false;
					checkEditHorizontalAlignmentCenter.Checked = false;
					checkEditHorizontalAlignmentRight.Checked = true;
					break;
			}
			buttonXShowTextNone.Checked = banner.TextMode == BannerTextMode.NoText || banner.ImageAlignement != HorizontalAlignment.Left;
			buttonXShowTextLinkName.Checked = banner.ImageAlignement == HorizontalAlignment.Left && banner.TextMode == BannerTextMode.LinkName;
			buttonXShowTextCustom.Checked = banner.ImageAlignement == HorizontalAlignment.Left && banner.TextMode == BannerTextMode.CustomText;
			buttonEditBannerTextFont.Tag = banner.Font;
			buttonEditBannerTextFont.EditValue = Utils.FontToString(banner.Font);
			colorEditBannerTextColor.Color = banner.ForeColor;

			switch (banner.ImagePaddingLeft)
			{
				case 0:
					checkEditPaddingLeftNone.Checked = true;
					break;
				case 2:
					checkEditPaddingLeft2.Checked = true;
					break;
				case 6:
					checkEditPaddingLeft6.Checked = true;
					break;
				case 10:
					checkEditPaddingLeft10.Checked = true;
					break;
				default:
					checkEditPaddingLeft2.Checked = true;
					break;
			}
			switch (banner.ImagePaddingTop)
			{
				case 0:
					checkEditPaddingTopNone.Checked = true;
					break;
				case 2:
					checkEditPaddingTop2.Checked = true;
					break;
				case 6:
					checkEditPaddingTop6.Checked = true;
					break;
				case 10:
					checkEditPaddingTop10.Checked = true;
					break;
				default:
					checkEditPaddingTop2.Checked = true;
					break;
			}
			switch (banner.ImagePaddingRight)
			{
				case 0:
					checkEditPaddingRightNone.Checked = true;
					break;
				case 2:
					checkEditPaddingRight2.Checked = true;
					break;
				case 6:
					checkEditPaddingRight6.Checked = true;
					break;
				case 10:
					checkEditPaddingRight10.Checked = true;
					break;
				default:
					checkEditPaddingRight2.Checked = true;
					break;
			}
			switch (banner.ImagePaddingBottom)
			{
				case 0:
					checkEditPaddingBottomNone.Checked = true;
					break;
				case 2:
					checkEditPaddingBottom2.Checked = true;
					break;
				case 6:
					checkEditPaddingBottom6.Checked = true;
					break;
				case 10:
					checkEditPaddingBottom10.Checked = true;
					break;
				default:
					checkEditPaddingBottom2.Checked = true;
					break;
			}

			switch (banner.TextMode)
			{
				case BannerTextMode.LinkName:
					memoEditBannerText.EditValue = _sourceLink.Name;
					break;
				case BannerTextMode.CustomText:
					memoEditBannerText.EditValue = banner.Text;
					break;
				default:
					memoEditBannerText.EditValue = null;
					break;
			}
			memoEditBannerText.Font = banner.Font;
			memoEditBannerText.Properties.Appearance.Font = banner.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = banner.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = banner.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = banner.Font;
			memoEditBannerText.ForeColor = banner.ForeColor;

			UpdateCustomDisplayImage();
		}

		private void SaveData()
		{
			foreach (var link in (_sourceLinkGroup?.AllGroupLinks ?? new[] { _sourceLink }).ToList())
			{
				if (buttonXEnable.Checked)
				{
					link.Banner.Enable = true;

					link.Banner.Inverted = checkEditInvert.Checked;
					link.Banner.InversionColor = checkEditInvert.Checked ? colorEditInversionColor.Color : GraphicObjectExtensions.DefaultReplaceColor;
					link.Banner.ImageVerticalAlignement = checkEditVerticalAlignmentTop1.Checked
						? VerticalAlignment.Top
						: VerticalAlignment.Middle;
					link.Banner.Image = labelControlTitle.Tag as Image;
					link.Banner.ImageName = _originalImageName;
					if (checkEditHorizontalAlignmentLeft.Checked)
						link.Banner.ImageAlignement = HorizontalAlignment.Left;
					else if (checkEditHorizontalAlignmentCenter.Checked)
						link.Banner.ImageAlignement = HorizontalAlignment.Center;
					else if (checkEditHorizontalAlignmentRight.Checked)
						link.Banner.ImageAlignement = HorizontalAlignment.Right;

					if (checkEditPaddingLeftNone.Checked)
						_sourceLink.Banner.ImagePaddingLeft = 0;
					else if (checkEditPaddingLeft2.Checked)
						_sourceLink.Banner.ImagePaddingLeft = 2;
					else if (checkEditPaddingLeft6.Checked)
						_sourceLink.Banner.ImagePaddingLeft = 6;
					else if (checkEditPaddingLeft10.Checked)
						_sourceLink.Banner.ImagePaddingLeft = 10;
					else
						_sourceLink.Banner.ImagePaddingLeft = 2;
					if (checkEditPaddingTopNone.Checked)
						_sourceLink.Banner.ImagePaddingTop = 0;
					else if (checkEditPaddingTop2.Checked)
						_sourceLink.Banner.ImagePaddingTop = 2;
					else if (checkEditPaddingTop6.Checked)
						_sourceLink.Banner.ImagePaddingTop = 6;
					else if (checkEditPaddingTop10.Checked)
						_sourceLink.Banner.ImagePaddingTop = 10;
					else
						_sourceLink.Banner.ImagePaddingTop = 2;
					if (checkEditPaddingRightNone.Checked)
						_sourceLink.Banner.ImagePaddingRight = 0;
					else if (checkEditPaddingRight2.Checked)
						_sourceLink.Banner.ImagePaddingRight = 2;
					else if (checkEditPaddingRight6.Checked)
						_sourceLink.Banner.ImagePaddingRight = 6;
					else if (checkEditPaddingRight10.Checked)
						_sourceLink.Banner.ImagePaddingRight = 10;
					else
						_sourceLink.Banner.ImagePaddingRight = 2;
					if (checkEditPaddingBottomNone.Checked)
						_sourceLink.Banner.ImagePaddingBottom = 0;
					else if (checkEditPaddingBottom2.Checked)
						_sourceLink.Banner.ImagePaddingBottom = 2;
					else if (checkEditPaddingBottom6.Checked)
						_sourceLink.Banner.ImagePaddingBottom = 6;
					else if (checkEditPaddingBottom10.Checked)
						_sourceLink.Banner.ImagePaddingBottom = 10;
					else
						_sourceLink.Banner.ImagePaddingBottom = 2;


					link.Banner.TextMode = BannerTextMode.NoText;
					if (link.Banner.ImageAlignement == HorizontalAlignment.Left)
					{
						if (buttonXShowTextLinkName.Checked)
							link.Banner.TextMode = BannerTextMode.LinkName;
						else if (buttonXShowTextCustom.Checked)
							link.Banner.TextMode = BannerTextMode.CustomText;
					}
					link.Banner.Font = buttonEditBannerTextFont.Tag as Font;
					link.Banner.ForeColor = colorEditBannerTextColor.Color;


				}
				else
				{
					link.Banner = GetEmptyBanner(link);
				}
				link.Banner.Text = _tempBannerText;
				link.Settings.TextWordWrap = checkEditTextWordWrap.Checked;
				link.Widget.WidgetType = _sourceLink.Banner.Enable ? link.Widget.DefaultWidgetType : link.Widget.WidgetType;
				link.Thumbnail.Enable = !link.Banner.Enable && link.Thumbnail.Enable;
			}

			if (buttonXEnable.Checked && checkEditSaveAsTemplate.Checked)
				MainController.Instance.Settings.DefaultBannerSettingsEncoded =
					_sourceLink.Banner.SaveAsTemplate().Serialize();
		}

		private void UpdateCustomDisplayImage()
		{
			var originalImage = labelControlTitle.Tag as Image;
			var displayImage = originalImage != null && originalImage.Height > BannerThumbnailHeight ?
				originalImage.Resize(new Size(originalImage.Width, BannerThumbnailHeight)) :
				originalImage; ;
			if (originalImage != null && checkEditInvert.Checked)
			{
				var imageClone = (Image)displayImage.Clone();
				displayImage = imageClone.ReplaceColor(colorEditInversionColor.Color);
			}
			labelControlTitle.Appearance.Image = displayImage;
			labelControlTitle.Text = String.Format(ImageTitleFormat, BannerTitle, _originalImageName);
			labelControlTitle.ForeColor = displayImage != null && checkEditInvert.Checked ? colorEditInversionColor.Color : Color.Black;
		}

		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				SaveData();
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
			layoutControlGroupGallery.Enabled = buttonXEnable.Checked;
			layoutControlGroupImageSettings.Enabled = buttonXEnable.Checked;
			layoutControlGroupTextSettings.Enabled = buttonXEnable.Checked;
			if (_allowHandleEvents)
			{
				_allowHandleEvents = false;
				var banner = buttonXEnable.Checked ? GetDefaultBanner() : GetEmptyBanner(_sourceLink);
				banner.Enable = buttonXEnable.Checked;
				LoadBanner(banner);
				_allowHandleEvents = true;
			}
		}

		private void OnSelectedBannerChanged(object sender, LinkImageEventArgs e)
		{
			labelControlTitle.Tag = e.Image;
			_originalImageName = e.Text;
			UpdateCustomDisplayImage();
		}

		private void OnInvertCheckedChanged(object sender, EventArgs e)
		{
			colorEditInversionColor.Enabled = checkEditInvert.Checked;
			if (_allowHandleEvents)
				UpdateCustomDisplayImage();
		}

		private void OnInversionColorEditValueChanged(object sender, EventArgs e)
		{
			if (_allowHandleEvents)
				UpdateCustomDisplayImage();
		}

		private void OnBannerHorizontalChanged(object sender, EventArgs e)
		{
			var enableText = checkEditHorizontalAlignmentLeft.Checked;
			layoutControlGroupTextSettings.PageEnabled = enableText;
			if (!enableText)
				OnTextModeButtonClick(buttonXShowTextNone, EventArgs.Empty);
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
			layoutControlGroupTextFont.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;

			layoutControlItemTextColorTitle.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			layoutControlItemTextColorEditor.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;

			layoutControlItemTextEditor.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			memoEditBannerText.ReadOnly = buttonXShowTextLinkName.Checked;

			layoutControlItemTextWordWrap.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;

			if (buttonXShowTextNone.Checked)
				memoEditBannerText.EditValue = null;
			else if (buttonXShowTextLinkName.Checked)
				memoEditBannerText.EditValue = _sourceLink.Name;
			else if (buttonXShowTextCustom.Checked && !String.IsNullOrEmpty(_tempBannerText))
				memoEditBannerText.EditValue = _tempBannerText;
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
				_tempBannerText = memoEditBannerText.EditValue as String ?? _tempBannerText;
		}

		private void OnVerticalAlignmentTopCheckedChanged(object sender, EventArgs e)
		{
			if (!_allowHandleEvents) return;

			_allowHandleEvents = false;

			var checkBox = (CheckEdit)sender;
			checkEditVerticalAlignmentTop1.Checked = checkBox.Checked;
			checkEditVerticalAlignmentTop2.Checked = checkBox.Checked;
			_allowHandleEvents = true;
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void OnSearchButtonClick(object sender, EventArgs e)
		{
			var keyword = textEditSearch.EditValue as String;
			if (String.IsNullOrEmpty(keyword)) return;
			MainController.Instance.Lists.Banners.SearchResults.LoadImages(keyword);
		}

		private void OnSearchEditValueChanged(object sender, EventArgs e)
		{
			buttonXSearch.Enabled = !String.IsNullOrEmpty(textEditSearch.EditValue as String);
		}

		private void OnSearchKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OnSearchButtonClick(sender, EventArgs.Empty);
		}

		private void OnFormClick(object sender, EventArgs e)
		{
			buttonXOK.Focus();
		}

		private void labelControlTitle_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			var viewInfo = labelControlTitle.GetViewInfo() as LabelControlViewInfo;
			if (viewInfo == null) return;
			if (viewInfo.ImageBounds.Contains(e.Location) && checkEditInvert.Checked && labelControlTitle.Appearance.Image != null && colorEditInversionColor.Color != Color.White)
				contextMenuStripImage.Show(Cursor.Position);
		}

		private void toolStripMenuItemImageAddToFavorites_Click(object sender, EventArgs e)
		{
			if (labelControlTitle.Appearance.Image == null) return;
			var favoritesContainer = xtraTabControlGallery.TabPages.OfType<FavoritesImagesContainer>().FirstOrDefault();
			((FavoriteImageGroup)favoritesContainer?.ParentImageGroup)?.AddImage<Banner>(labelControlTitle.Appearance.Image, String.Format("{0}_{1}", _originalImageName, colorEditInversionColor.Color.ToHex()));
		}

		private void OnTabControlSettingsSelectedPageChanging(object sender, DevExpress.XtraLayout.LayoutTabPageChangingEventArgs e)
		{
			e.Cancel = !buttonXEnable.Checked;
		}
	}
}