using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;
using SalesLibraries.FileManager.Properties;
using HorizontalAlignment = SalesLibraries.Business.Entities.Wallbin.Common.Enums.HorizontalAlignment;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkBanner : MetroForm, ILinkSettingsEditForm
	{
		private const string ImageTitleFormat = "<size=+4><b>{0}</b></size><br><color=gray>{1}</color>";
		private const int BannerThumbnailHeight = 64;

		private bool _allowHandleEvents;
		private string _tempBannerText;
		private readonly BaseLibraryLink _sourceLink;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Banner,
		};

		public FormEditLinkBanner(BaseLibraryLink sourceLink)
		{
			_sourceLink = sourceLink;
			InitializeComponent();

			labelControlTitle.Text = String.Format(ImageTitleFormat, _sourceLink.LinkInfoDisplayName, String.Empty);

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
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
				buttonXEnable.Font = new Font(buttonXEnable.Font.FontFamily, buttonXEnable.Font.Size - 2, buttonXEnable.Font.Style);
				buttonXDisable.Font = new Font(buttonXDisable.Font.FontFamily, buttonXDisable.Font.Size - 2, buttonXDisable.Font.Style);
				buttonXShowTextNone.Font = new Font(buttonXShowTextNone.Font.FontFamily, buttonXShowTextNone.Font.Size - 2, buttonXShowTextNone.Font.Style);
				buttonXShowTextLinkName.Font = new Font(buttonXShowTextLinkName.Font.FontFamily, buttonXShowTextLinkName.Font.Size - 2, buttonXShowTextLinkName.Font.Style);
				buttonXShowTextCustom.Font = new Font(buttonXShowTextCustom.Font.FontFamily, buttonXShowTextCustom.Font.Size - 2, buttonXShowTextCustom.Font.Style);
			}
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			Width = 990;
			Height = 670;
			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder, "Site Admin-Link-Banner", false, false);
			Text = string.Format(Text, _sourceLink.Name);
			LoadData();
		}

		private void LoadData()
		{
			_allowHandleEvents = false;

			xtraTabControlGallery.TabPages.Clear();
			xtraTabControlGallery.TabPages.AddRange(
				MainController.Instance.Lists.Banners.Items.Select(imageGroup =>
				{
					var tabPage = BaseLinkImagesContainer.Create(imageGroup);
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

			buttonXEnable.Enabled = MainController.Instance.Lists.Banners.MainFolder.ExistsLocal();
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

		private BannerSettings GetEmptyBanner()
		{
			var banner = SettingsContainer.CreateInstance<BannerSettings>(_sourceLink);
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
				labelControlTitle.Tag = banner.Image;
			else
				labelControlTitle.Tag = null;
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
			if (buttonXEnable.Checked)
			{
				_sourceLink.Banner.Enable = true;

				_sourceLink.Banner.Inverted = checkEditInvert.Checked;
				_sourceLink.Banner.InversionColor = colorEditInversionColor.Color != GraphicObjectExtensions.DefaultInversionColor
					? colorEditInversionColor.Color
					: GraphicObjectExtensions.DefaultInversionColor;
				_sourceLink.Banner.ImageVerticalAlignement = checkEditVerticalAlignmentTop1.Checked
					? VerticalAlignment.Top
					: VerticalAlignment.Middle;
				_sourceLink.Banner.Image = labelControlTitle.Tag as Image;
				if (checkEditHorizontalAlignmentLeft.Checked)
					_sourceLink.Banner.ImageAlignement = HorizontalAlignment.Left;
				else if (checkEditHorizontalAlignmentCenter.Checked)
					_sourceLink.Banner.ImageAlignement = HorizontalAlignment.Center;
				else if (checkEditHorizontalAlignmentRight.Checked)
					_sourceLink.Banner.ImageAlignement = HorizontalAlignment.Right;

				_sourceLink.Banner.TextMode = BannerTextMode.NoText;
				if (_sourceLink.Banner.ImageAlignement == HorizontalAlignment.Left)
				{
					if (buttonXShowTextLinkName.Checked)
						_sourceLink.Banner.TextMode = BannerTextMode.LinkName;
					else if (buttonXShowTextCustom.Checked)
						_sourceLink.Banner.TextMode = BannerTextMode.CustomText;
				}
				_sourceLink.Banner.Font = buttonEditBannerTextFont.Tag as Font;
				_sourceLink.Banner.ForeColor = colorEditBannerTextColor.Color;

				if (checkEditSaveAsTemplate.Checked)
					MainController.Instance.Settings.DefaultBannerSettingsEncoded =
						_sourceLink.Banner.SaveAsTemplate().Serialize();
			}
			else
			{
				_sourceLink.Banner = GetEmptyBanner();
			}
			_sourceLink.Banner.Text = _tempBannerText;
			_sourceLink.Settings.TextWordWrap = checkEditTextWordWrap.Checked;
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
				displayImage = colorEditInversionColor.Color != GraphicObjectExtensions.DefaultInversionColor
					? imageClone.ReplaceColor(colorEditInversionColor.Color)
					: imageClone.Invert();
			}
			labelControlTitle.Appearance.Image = displayImage;
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
			xtraTabControlSettings.Enabled = buttonXEnable.Checked;
			if (_allowHandleEvents)
			{
				_allowHandleEvents = false;
				var banner = buttonXEnable.Checked ? GetDefaultBanner() : GetEmptyBanner();
				banner.Enable = buttonXEnable.Checked;
				LoadBanner(banner);
				_allowHandleEvents = true;
			}
		}

		private void OnSelectedBannerChanged(object sender, LinkImageEventArgs e)
		{
			labelControlTitle.Tag = e.Image;
			labelControlTitle.Text = String.Format(ImageTitleFormat, _sourceLink.LinkInfoDisplayName, e.Text);
			UpdateCustomDisplayImage();
		}

		private void OnInvertCheckedChanged(object sender, EventArgs e)
		{
			colorEditInversionColor.Enabled = checkEditInvert.Checked;
			if(_allowHandleEvents)
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
			xtraTabPageText.PageEnabled = enableText;
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
			labelControlTextFont.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			labelControlTextColor.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			memoEditBannerText.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			memoEditBannerText.ReadOnly = buttonXShowTextLinkName.Checked;
			buttonEditBannerTextFont.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			colorEditBannerTextColor.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;
			checkEditTextWordWrap.Enabled = buttonXShowTextLinkName.Checked || buttonXShowTextCustom.Checked;

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
	}
}