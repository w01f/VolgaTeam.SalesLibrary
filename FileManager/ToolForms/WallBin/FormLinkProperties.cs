using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.XtraNavBar;
using FileManager.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.Services.IPadAdminService;
using Banner = FileManager.ConfigurationClasses.Banner;
using FileCard = SalesDepot.CoreObjects.BusinessClasses.FileCard;
using Font = System.Drawing.Font;
using Library = SalesDepot.CoreObjects.BusinessClasses.Library;

namespace FileManager.ToolForms.WallBin
{
	public partial class FormLinkProperties : MetroForm
	{
		private bool _closeEventAssigned;
		private bool _isBold;
		private string _note = string.Empty;
		private LayoutViewHitInfo _hitInfo;
		private readonly List<GroupModel> _securityGroups = new List<GroupModel>();
		private readonly Library _library;

		public bool IsLoading { get; set; }

		public FormLinkProperties(Library library)
		{
			InitializeComponent();
			_library = library;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
				laAddDateTitle.Font = new Font(laAddDateTitle.Font.FontFamily, laAddDateTitle.Font.Size - 2, laAddDateTitle.Font.Style);
				laAddDateValue.Font = new Font(laAddDateValue.Font.FontFamily, laAddDateValue.Font.Size - 2, laAddDateValue.Font.Style);
				laAvailableWidgets.Font = new Font(laAvailableWidgets.Font.FontFamily, laAvailableWidgets.Font.Size - 2, laAvailableWidgets.Font.Style);
				laAvailableBanners.Font = new Font(laAvailableBanners.Font.FontFamily, laAvailableBanners.Font.Size - 2, laAvailableBanners.Font.Style);
				laExpirationDateTitle.Font = new Font(laExpirationDateTitle.Font.FontFamily, laExpirationDateTitle.Font.Size - 2, laExpirationDateTitle.Font.Style);
				laExpireddateActions.Font = new Font(laExpireddateActions.Font.FontFamily, laExpireddateActions.Font.Size - 2, laExpireddateActions.Font.Style);
				laSelectedWidget.Font = new Font(laSelectedWidget.Font.FontFamily, laSelectedWidget.Font.Size - 2, laSelectedWidget.Font.Style);
				laWidgetHint.Font = new Font(laWidgetHint.Font.FontFamily, laWidgetHint.Font.Size - 4, laWidgetHint.Font.Style);
				laSelectedBanner.Font = new Font(laSelectedBanner.Font.FontFamily, laSelectedBanner.Font.Size - 2, laSelectedBanner.Font.Style);
				laBannerAligment.Font = new Font(laBannerAligment.Font.FontFamily, laBannerAligment.Font.Size - 2, laBannerAligment.Font.Style);
				laBannerHint.Font = new Font(laBannerHint.Font.FontFamily, laBannerHint.Font.Size - 4, laBannerHint.Font.Style);
				checkBoxEnableExpiredLinks.Font = new Font(checkBoxEnableExpiredLinks.Font.FontFamily, checkBoxEnableExpiredLinks.Font.Size - 2, checkBoxEnableExpiredLinks.Font.Style);
				checkBoxEnableWidget.Font = new Font(checkBoxEnableWidget.Font.FontFamily, checkBoxEnableWidget.Font.Size - 2, checkBoxEnableWidget.Font.Style);
				checkBoxEnableBanner.Font = new Font(checkBoxEnableBanner.Font.FontFamily, checkBoxEnableBanner.Font.Size - 2, checkBoxEnableBanner.Font.Style);
				checkBoxLabelLink.Font = new Font(checkBoxLabelLink.Font.FontFamily, checkBoxLabelLink.Font.Size - 2, checkBoxLabelLink.Font.Style);
				checkBoxSendEmailWhenDelete.Font = new Font(checkBoxSendEmailWhenDelete.Font.FontFamily, checkBoxSendEmailWhenDelete.Font.Size - 2, checkBoxSendEmailWhenDelete.Font.Style);
				checkBoxBannerShowText.Font = new Font(checkBoxBannerShowText.Font.FontFamily, checkBoxBannerShowText.Font.Size - 2, checkBoxBannerShowText.Font.Style);
				rbAttention.Font = new Font(rbAttention.Font.FontFamily, rbAttention.Font.Size - 2, rbAttention.Font.Style);
				rbBold.Font = new Font(rbBold.Font.FontFamily, rbBold.Font.Size - 2, rbBold.Font.Style);
				rbCustomNote.Font = new Font(rbCustomNote.Font.FontFamily, rbCustomNote.Font.Size - 2, rbCustomNote.Font.Style);
				rbNew.Font = new Font(rbNew.Font.FontFamily, rbNew.Font.Size - 2, rbNew.Font.Style);
				rbNone.Font = new Font(rbNone.Font.FontFamily, rbNone.Font.Size - 2, rbNone.Font.Style);
				rbRegular.Font = new Font(rbRegular.Font.FontFamily, rbRegular.Font.Size - 2, rbRegular.Font.Style);
				rbSell.Font = new Font(rbSell.Font.FontFamily, rbSell.Font.Size - 2, rbSell.Font.Style);
				rbUpdated.Font = new Font(rbUpdated.Font.FontFamily, rbUpdated.Font.Size - 2, rbUpdated.Font.Style);
				rbBannerAligmentCenter.Font = new Font(rbBannerAligmentCenter.Font.FontFamily, rbBannerAligmentCenter.Font.Size - 2, rbBannerAligmentCenter.Font.Style);
				rbBannerAligmentLeft.Font = new Font(rbBannerAligmentLeft.Font.FontFamily, rbBannerAligmentLeft.Font.Size - 2, rbBannerAligmentLeft.Font.Style);
				rbBannerAligmentRight.Font = new Font(rbBannerAligmentRight.Font.FontFamily, rbBannerAligmentRight.Font.Size - 2, rbBannerAligmentRight.Font.Style);
				xtraTabControl.AppearancePage.HeaderActive.Font = new Font(xtraTabControl.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControl.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControl.AppearancePage.HeaderActive.Font.Style);
				xtraTabControl.AppearancePage.Header.Font = new Font(xtraTabControl.AppearancePage.Header.Font.FontFamily, xtraTabControl.AppearancePage.Header.Font.Size - 2, xtraTabControl.AppearancePage.Header.Font.Style);
				xtraTabControl.AppearancePage.HeaderDisabled.Font = new Font(xtraTabControl.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControl.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControl.AppearancePage.HeaderDisabled.Font.Style);
				xtraTabControl.AppearancePage.HeaderHotTracked.Font = new Font(xtraTabControl.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControl.AppearancePage.HeaderHotTracked.Font.Style);
				buttonXAddKeyWord.Font = new Font(buttonXAddKeyWord.Font.FontFamily, buttonXAddKeyWord.Font.Size - 2, buttonXAddKeyWord.Font.Style);
				checkBoxEnableAttachmnets.Font = new Font(checkBoxEnableAttachmnets.Font.FontFamily, checkBoxEnableAttachmnets.Font.Size - 2, checkBoxEnableAttachmnets.Font.Style);
				buttonXAttachmentsFilesAdd.Font = new Font(buttonXAttachmentsFilesAdd.Font.FontFamily, buttonXAttachmentsFilesAdd.Font.Size - 2, buttonXAttachmentsFilesAdd.Font.Style);
				checkBoxEnableFileCard.Font = new Font(checkBoxEnableFileCard.Font.FontFamily, checkBoxEnableFileCard.Font.Size - 2, checkBoxEnableFileCard.Font.Style);
				checkBoxFileCardAdvertiser.Font = new Font(checkBoxFileCardAdvertiser.Font.FontFamily, checkBoxFileCardAdvertiser.Font.Size - 2, checkBoxFileCardAdvertiser.Font.Style);
				checkBoxFileCardDateSold.Font = new Font(checkBoxFileCardDateSold.Font.FontFamily, checkBoxFileCardDateSold.Font.Size - 2, checkBoxFileCardDateSold.Font.Style);
				checkBoxFileCardBroadcastClosed.Font = new Font(checkBoxFileCardBroadcastClosed.Font.FontFamily, checkBoxFileCardBroadcastClosed.Font.Size - 2, checkBoxFileCardBroadcastClosed.Font.Style);
				checkBoxFileCardDigitalClosed.Font = new Font(checkBoxFileCardDigitalClosed.Font.FontFamily, checkBoxFileCardDigitalClosed.Font.Size - 2, checkBoxFileCardDigitalClosed.Font.Style);
				checkBoxFileCardPublishingClosed.Font = new Font(checkBoxFileCardPublishingClosed.Font.FontFamily, checkBoxFileCardPublishingClosed.Font.Size - 2, checkBoxFileCardPublishingClosed.Font.Style);
				checkBoxFileCardSalesInfo.Font = new Font(checkBoxFileCardSalesInfo.Font.FontFamily, checkBoxFileCardSalesInfo.Font.Size - 2, checkBoxFileCardSalesInfo.Font.Style);
				checkBoxFileCardImportantInfo.Font = new Font(checkBoxFileCardImportantInfo.Font.FontFamily, checkBoxFileCardImportantInfo.Font.Size - 2, checkBoxFileCardImportantInfo.Font.Style);
				buttonXFileCardImportantInfoAdd.Font = new Font(buttonXFileCardImportantInfoAdd.Font.FontFamily, buttonXFileCardImportantInfoAdd.Font.Size - 2, buttonXFileCardImportantInfoAdd.Font.Style);

				rbSecurityAllowed.Font = new Font(rbSecurityAllowed.Font.FontFamily, rbSecurityAllowed.Font.Size - 2, rbSecurityAllowed.Font.Style);
				rbSecurityForbidden.Font = new Font(rbSecurityForbidden.Font.FontFamily, rbSecurityForbidden.Font.Size - 2, rbSecurityForbidden.Font.Style);
				rbSecurityDenied.Font = new Font(rbSecurityDenied.Font.FontFamily, rbSecurityDenied.Font.Size - 2, rbSecurityDenied.Font.Style);
				rbSecurityWhiteList.Font = new Font(rbSecurityWhiteList.Font.FontFamily, rbSecurityWhiteList.Font.Size - 2, rbSecurityWhiteList.Font.Style);
				rbSecurityBlackList.Font = new Font(rbSecurityBlackList.Font.FontFamily, rbSecurityBlackList.Font.Size - 2, rbSecurityBlackList.Font.Style);
				ckSecurityShareLink.Font = new Font(ckSecurityShareLink.Font.FontFamily, ckSecurityShareLink.Font.Size - 2, ckSecurityShareLink.Font.Style);
			}

			repositoryItemButtonEditKeyword.Enter += FormMain.Instance.EditorEnter;
			repositoryItemButtonEditKeyword.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditKeyword.MouseDown += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditAttachmentsFiles.Enter += FormMain.Instance.EditorEnter;
			repositoryItemButtonEditAttachmentsFiles.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditAttachmentsFiles.MouseDown += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditAttachmentsWeb.Enter += FormMain.Instance.EditorEnter;
			repositoryItemButtonEditAttachmentsWeb.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemButtonEditAttachmentsWeb.MouseDown += FormMain.Instance.EditorMouseUp;

			textEditFileCardAdvertiser.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardAdvertiser.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardAdvertiser.MouseDown += FormMain.Instance.EditorMouseUp;
			dateEditFileCardDateSold.Enter += FormMain.Instance.EditorEnter;
			dateEditFileCardDateSold.MouseUp += FormMain.Instance.EditorMouseUp;
			dateEditFileCardDateSold.MouseDown += FormMain.Instance.EditorMouseUp;
			spinEditFileCardBroadcastClosed.Enter += FormMain.Instance.EditorEnter;
			spinEditFileCardBroadcastClosed.MouseUp += FormMain.Instance.EditorMouseUp;
			spinEditFileCardBroadcastClosed.MouseDown += FormMain.Instance.EditorMouseUp;
			spinEditFileCardDigitalClosed.Enter += FormMain.Instance.EditorEnter;
			spinEditFileCardDigitalClosed.MouseUp += FormMain.Instance.EditorMouseUp;
			spinEditFileCardDigitalClosed.MouseDown += FormMain.Instance.EditorMouseUp;
			spinEditFileCardPublishingClosed.Enter += FormMain.Instance.EditorEnter;
			spinEditFileCardPublishingClosed.MouseUp += FormMain.Instance.EditorMouseUp;
			spinEditFileCardPublishingClosed.MouseDown += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesName.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardSalesName.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesName.MouseDown += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesEmail.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardSalesEmail.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesEmail.MouseDown += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesPhone.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardSalesPhone.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesPhone.MouseDown += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesStation.Enter += FormMain.Instance.EditorEnter;
			textEditFileCardSalesStation.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditFileCardSalesStation.MouseDown += FormMain.Instance.EditorMouseUp;
			repositoryItemMemoEditFileCardImportantInfo.Enter += FormMain.Instance.EditorEnter;
			repositoryItemMemoEditFileCardImportantInfo.MouseUp += FormMain.Instance.EditorMouseUp;
			repositoryItemMemoEditFileCardImportantInfo.MouseDown += FormMain.Instance.EditorMouseUp;

			gridViewSecurityGroups.MasterRowEmpty += OnGroupChildListIsEmpty;
			gridViewSecurityGroups.MasterRowGetRelationCount += OnGetGroupRelationCount;
			gridViewSecurityGroups.MasterRowGetRelationName += OnGetGroupRelationName;
			gridViewSecurityGroups.MasterRowGetChildList += OnGetGroupChildList;

			SearchTags = new LibraryFileSearchTags();
			ExpirationDateOptions = new ExpirationDateOptions();
			dateEditExpirationDate.Properties.NullDate = DateTime.MinValue;

			#region Search tags
			Keywords = new List<StringDataSourceWrapper>();
			#endregion

			FileCardImportantInfo = new List<StringDataSourceWrapper>();

			gridControlWidgetsGallery.DataSource = new BindingList<Widget>(ListManager.Instance.Widgets);
			layoutViewWidgetsGallery.FocusedRowChanged += layoutViewWidgetsGallery_FocusedRowChanged;
			gridControlWidgetsFavs.DataSource = new BindingList<Widget>(ListManager.Instance.WidgetsFavs);
			layoutViewWidgetsFavs.FocusedRowChanged += layoutViewWidgetsFavs_FocusedRowChanged;
			gridControlBannersGallery.DataSource = new BindingList<Banner>(ListManager.Instance.Banners);
			gridControlBannersFavs.DataSource = new BindingList<Banner>(ListManager.Instance.BannersFavs);
			layoutViewBannersGallery.FocusedRowChanged += layoutViewBannersGalery_FocusedRowChanged;
			layoutViewBannersFavs.FocusedRowChanged += layoutViewBannersFavs_FocusedRowChanged;

			LoadSecurityGroups();
		}

		public bool IsLineBreak { get; set; }

		public DateTime AddDate { get; set; }
		public LibraryFileSearchTags SearchTags { get; set; }
		public List<StringDataSourceWrapper> Keywords { get; private set; }
		public ExpirationDateOptions ExpirationDateOptions { get; set; }
		public LineBreakProperties LineBreakProperties { get; set; }
		public BannerProperties BannerProperties { get; set; }
		public AttachmentProperties AttachmentProperties { get; set; }
		public bool EnableWidget { get; set; }
		public Image Widget { get; set; }
		public FileCard FileCard { get; set; }
		public List<StringDataSourceWrapper> FileCardImportantInfo { get; private set; }
		public Func<object> OpenQV { get; set; }
		public Func<object> OpenWV { get; set; }
		public Func<object> RefreshPreview { get; set; }

		private readonly List<string> _assignedUsers = new List<string>();
		public string AssignedUsers
		{
			get
			{
				_assignedUsers.Clear();
				if (rbSecurityWhiteList.Checked)
					_assignedUsers.AddRange(_securityGroups.Where(g => g.users != null).SelectMany(g => g.users).Where(u => u.selected).Select(u => u.login));
				return String.Join(",", _assignedUsers);
			}
			set
			{
				_assignedUsers.Clear();
				if (!String.IsNullOrEmpty(value))
					_assignedUsers.AddRange(value.Split(',').Select(item => item.Trim()));
				ApplyAssignedUsers();
			}
		}

		private readonly List<string> _deniedUsers = new List<string>();
		public string DeniedUsers
		{
			get
			{
				_deniedUsers.Clear();
				if (rbSecurityBlackList.Checked)
					_deniedUsers.AddRange(_securityGroups.Where(g => g.users != null).SelectMany(g => g.users).Where(u => u.selected).Select(u => u.login));
				return String.Join(",", _deniedUsers);
			}
			set
			{
				_deniedUsers.Clear();
				if (!String.IsNullOrEmpty(value))
					_deniedUsers.AddRange(value.Split(',').Select(item => item.Trim()));
				ApplyDeniedUsers();
			}
		}

		public string CaptionName
		{
			set { Text = value; }
		}

		public string Note
		{
			get { return _note; }
			set
			{
				_note = value;
				if (string.IsNullOrEmpty(_note))
					rbNone.Checked = true;
				else if (_note.Equals(rbNew.Text))
					rbNew.Checked = true;
				else if (_note.Equals(rbUpdated.Text))
					rbUpdated.Checked = true;
				else if (_note.Equals(rbSell.Text))
					rbSell.Checked = true;
				else if (_note.Equals(rbAttention.Text))
					rbAttention.Checked = true;
				else
				{
					rbCustomNote.Checked = true;
					edCustomNote.Text = _note;
				}
			}
		}

		public bool IsBold
		{
			get { return _isBold; }
			set
			{
				_isBold = value;
				rbRegular.Checked = !value;
				rbBold.Checked = value;
			}
		}

		private string FontToString(Font font)
		{
			string str = font.Name + ", " + font.Size.ToString("#0");
			if (font.Bold)
				str = str + ", Bold";
			if (font.Italic)
				str = str + ", Italic";
			if (font.Underline)
				str = str + ", Underline";
			if (font.Strikeout)
				str = str + ", Strikeout";
			return str;
		}

		private void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			Type controlType = control.GetType();
			if (controlType != typeof(CheckBox)
				&& controlType != typeof(RadioButton)
				&& controlType != typeof(TextBox)
				&& controlType != typeof(DataGridView)
				&& controlType != typeof(GridControl)
				&& controlType != typeof(ButtonEdit)
				&& controlType != typeof(CheckEdit)
				&& controlType != typeof(CheckedListBoxControl)
				&& controlType != typeof(ColorEdit)
				&& controlType != typeof(ComboBoxEdit)
				&& controlType != typeof(DateEdit)
				&& controlType != typeof(TimeEdit)
				&& controlType != typeof(MemoEdit)
				&& controlType != typeof(SpinEdit)
				&& controlType != typeof(TextEdit)
				&& controlType != typeof(NavBarControl))
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
				{
					Application.DoEvents();
					AssignCloseActiveEditorsonOutSideClick(childControl);
				}
			}
		}

		private void FormProperties_Load(object sender, EventArgs e)
		{
			xtraTabPageExpiredLinks.PageVisible = !IsLineBreak;
			xtraTabPageNotes.PageVisible = !IsLineBreak;
			xtraTabPageSearchTags.PageVisible = !IsLineBreak;
			xtraTabPageAttachments.PageVisible = !IsLineBreak;
			xtraTabPageLineBrealProperties.PageVisible = IsLineBreak;

			#region Search tags
			#region Categories
			xtraScrollableControlSearchTagsCategories.Controls.Clear();
			splitContainerSearchTagsCategories.Panel2.Controls.Clear();
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
			{
				searchGroup.InitGroupControls();

				searchGroup.ToggleButton.Dock = DockStyle.Top;
				searchGroup.ToggleButton.Click += CategoriesGroup_Click;
				searchGroup.ToggleButton.CheckedChanged += CategoriesGroup_CheckedChanged;
				xtraScrollableControlSearchTagsCategories.Controls.Add(searchGroup.ToggleButton);
				searchGroup.ToggleButton.BringToFront();

				searchGroup.ListBox.ItemChecking += ListBox_ItemChecking;
				searchGroup.ListBox.ItemCheck += (o, ea) => UpdateCategoriesHeader();
				splitContainerSearchTagsCategories.Panel2.Controls.Add(searchGroup.ListBox);
				searchGroup.ListBox.BringToFront();
			}

			ListManager.Instance.SearchTags.SearchGroups.ForEach(g => g.ListBox.UnCheckAll());
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
			{
				var group = SearchTags.SearchGroups.FirstOrDefault(x => x.Name.Equals(searchGroup.Name));
				if (group != null)
					foreach (var item in searchGroup.ListBox.Items.Cast<CheckedListBoxItem>().Where(item => group.Tags.Select(x => x.Name).Contains(item.Value.ToString())))
						item.CheckState = CheckState.Checked;
			}
			var firstGroup = ListManager.Instance.SearchTags.SearchGroups.FirstOrDefault();
			if (firstGroup != null)
				CategoriesGroup_Click(firstGroup.ToggleButton, new EventArgs());
			UpdateCategoriesHeader();
			#endregion

			#region Keywords
			UpdateKeywordsDataSource();
			#endregion
			#endregion

			if (!IsLineBreak)
			{
				#region Expiration date
				laAddDateValue.Text = AddDate.ToString("M/dd/yyyy h:mm:ss tt");
				dateEditExpirationDate.DateTime = ExpirationDateOptions.ExpirationDate;
				timeEditExpirationTime.Time = ExpirationDateOptions.ExpirationDate;
				checkBoxSendEmailWhenDelete.Checked = ExpirationDateOptions.SendEmailWhenSync;
				checkBoxLabelLink.Checked = ExpirationDateOptions.LabelLinkWhenExpired;
				checkBoxEnableExpiredLinks.Checked = ExpirationDateOptions.EnableExpirationDate;
				#endregion

				#region File Card
				checkBoxEnableFileCard.Checked = FileCard.Enable;
				textEditFileCardTitle.EditValue = FileCard.Title;
				checkBoxFileCardAdvertiser.Checked = !string.IsNullOrEmpty(FileCard.Advertiser);
				textEditFileCardAdvertiser.EditValue = FileCard.Advertiser;
				checkBoxFileCardDateSold.Checked = FileCard.DateSold.HasValue;
				dateEditFileCardDateSold.EditValue = FileCard.DateSold;
				checkBoxFileCardBroadcastClosed.Checked = FileCard.BroadcastClosed.HasValue;
				spinEditFileCardBroadcastClosed.EditValue = FileCard.BroadcastClosed;
				checkBoxFileCardDigitalClosed.Checked = FileCard.DigitalClosed.HasValue;
				spinEditFileCardDigitalClosed.EditValue = FileCard.DigitalClosed;
				checkBoxFileCardPublishingClosed.Checked = FileCard.PublishingClosed.HasValue;
				spinEditFileCardPublishingClosed.EditValue = FileCard.PublishingClosed;
				checkBoxFileCardSalesInfo.Checked = !string.IsNullOrEmpty(FileCard.SalesName) || !string.IsNullOrEmpty(FileCard.SalesEmail) || !string.IsNullOrEmpty(FileCard.SalesPhone) || !string.IsNullOrEmpty(FileCard.SalesStation);
				textEditFileCardSalesName.EditValue = FileCard.SalesName;
				textEditFileCardSalesEmail.EditValue = FileCard.SalesEmail;
				textEditFileCardSalesPhone.EditValue = FileCard.SalesPhone;
				textEditFileCardSalesStation.EditValue = FileCard.SalesStation;

				checkBoxFileCardImportantInfo.Checked = FileCardImportantInfo.Count > 0;
				gridControlFileCardImportantInfo.DataSource = FileCardImportantInfo;
				gridViewFileCardImportantInfo.RefreshData();
				#endregion

				#region Attachments
				checkBoxEnableAttachmnets.Checked = AttachmentProperties.Enable;
				gridControlAttachmentsFiles.DataSource = AttachmentProperties.FilesAttachments;
				gridControlAttachmentsWeb.DataSource = AttachmentProperties.WebAttachments;
				#endregion
			}
			else
			{
				#region Linebreak properties
				buttonEditLineBreakFont.Tag = LineBreakProperties.Font;
				buttonEditLineBreakFont.EditValue = FontToString(LineBreakProperties.Font);
				colorEditLineBreakFontColor.Color = LineBreakProperties.ForeColor;
				memoEditNote.EditValue = LineBreakProperties.Note;
				#endregion
			}

			pbSelectedWidget.Image = EnableWidget ? Widget : null;
			laWidgetFileName.Text = string.Empty;
			checkBoxEnableWidget.Checked = EnableWidget;

			#region Banner properties
			xtraTabPageBanner.PageEnabled = Directory.Exists(ListManager.Instance.BannerFolder);
			checkBoxEnableBanner.Checked = BannerProperties.Enable;
			pbSelectedBanner.Image = BannerProperties.Enable ? BannerProperties.Image : null;
			laBannerFileName.Text = string.Empty;
			switch (BannerProperties.ImageAlignement)
			{
				case Alignment.Left:
					rbBannerAligmentLeft.Checked = true;
					rbBannerAligmentCenter.Checked = false;
					rbBannerAligmentRight.Checked = false;
					break;
				case Alignment.Center:
					rbBannerAligmentLeft.Checked = false;
					rbBannerAligmentCenter.Checked = true;
					rbBannerAligmentRight.Checked = false;
					break;
				case Alignment.Right:
					rbBannerAligmentLeft.Checked = false;
					rbBannerAligmentCenter.Checked = false;
					rbBannerAligmentRight.Checked = true;
					break;
			}
			checkBoxBannerShowText.Checked = BannerProperties.ShowText;
			buttonEditBannerTextFont.Tag = BannerProperties.Font;
			buttonEditBannerTextFont.EditValue = FontToString(BannerProperties.Font);
			colorEditBannerTextColor.Color = BannerProperties.ForeColor;
			memoEditBannerText.EditValue = BannerProperties.Text;
			memoEditBannerText.Font = BannerProperties.Font;
			memoEditBannerText.Properties.Appearance.Font = BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = BannerProperties.Font;
			memoEditBannerText.ForeColor = BannerProperties.ForeColor;
			#endregion

			if (!_closeEventAssigned)
			{
				AssignCloseActiveEditorsonOutSideClick(this);
				_closeEventAssigned = true;
			}
			IsLoading = false;
		}

		private void FormLinkProperties_FormClosed(object sender, FormClosedEventArgs e)
		{
			IsLoading = true;
		}

		private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			hyperLinkEditRequestNewCategories.Visible = e.Page == xtraTabPageSearchTags && !String.IsNullOrEmpty(SettingsManager.Instance.CategoryRequestRecipients) && !String.IsNullOrEmpty(SettingsManager.Instance.CategoryRequestSubject) && !String.IsNullOrEmpty(SettingsManager.Instance.CategoryRequestBody);
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			if (!IsLineBreak)
			{
				if (rbNew.Checked)
					_note = rbNew.Text;
				else if (rbUpdated.Checked)
					_note = rbUpdated.Text;
				else if (rbSell.Checked)
					_note = rbSell.Text;
				else if (rbAttention.Checked)
					_note = rbAttention.Text;
				else if (rbCustomNote.Checked)
					_note = edCustomNote.Text;
				else
					_note = string.Empty;

				_isBold = rbBold.Checked;

				#region Search tags

				#region Categories
				SearchTags.SearchGroups.Clear();
				foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
				{
					var group = new SearchGroup();
					group.Name = searchGroup.Name;
					foreach (CheckedListBoxItem item in searchGroup.ListBox.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(new SearchTag(group.Name) { Name = item.Value.ToString() });
					SearchTags.SearchGroups.Add(group);
				}
				#endregion

				#endregion

				#region Expiration date
				ExpirationDateOptions.ExpirationDate = new DateTime(dateEditExpirationDate.DateTime.Year, dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour, timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second);
				ExpirationDateOptions.SendEmailWhenSync = checkBoxSendEmailWhenDelete.Checked;
				ExpirationDateOptions.LabelLinkWhenExpired = checkBoxLabelLink.Checked;
				ExpirationDateOptions.EnableExpirationDate = checkBoxEnableExpiredLinks.Checked;
				#endregion

				#region File Card
				FileCard.Enable = checkBoxEnableFileCard.Checked;
				FileCard.Title = textEditFileCardTitle.EditValue != null ? textEditFileCardTitle.EditValue.ToString() : string.Empty;
				FileCard.Advertiser = FileCard.Enable && checkBoxFileCardAdvertiser.Checked && textEditFileCardAdvertiser.EditValue != null ? textEditFileCardAdvertiser.EditValue.ToString() : null;
				FileCard.DateSold = FileCard.Enable && checkBoxFileCardDateSold.Checked && dateEditFileCardDateSold.EditValue != null ? (DateTime?)dateEditFileCardDateSold.DateTime : null;
				FileCard.BroadcastClosed = FileCard.Enable && checkBoxFileCardBroadcastClosed.Checked && spinEditFileCardBroadcastClosed.EditValue != null ? (double?)spinEditFileCardBroadcastClosed.Value : null;
				FileCard.DigitalClosed = FileCard.Enable && checkBoxFileCardDigitalClosed.Checked && spinEditFileCardDigitalClosed.EditValue != null ? (double?)spinEditFileCardDigitalClosed.Value : null;
				FileCard.PublishingClosed = FileCard.Enable && checkBoxFileCardPublishingClosed.Checked && spinEditFileCardPublishingClosed.EditValue != null ? (double?)spinEditFileCardPublishingClosed.Value : null;
				FileCard.SalesName = FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesName.EditValue != null ? textEditFileCardSalesName.EditValue.ToString() : null;
				FileCard.SalesEmail = FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesEmail.EditValue != null ? textEditFileCardSalesEmail.EditValue.ToString() : null;
				FileCard.SalesPhone = FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesPhone.EditValue != null ? textEditFileCardSalesPhone.EditValue.ToString() : null;
				FileCard.SalesStation = FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesStation.EditValue != null ? textEditFileCardSalesStation.EditValue.ToString() : null;
				#endregion

				#region Attachments
				AttachmentProperties.Enable = checkBoxEnableAttachmnets.Checked;
				if (!AttachmentProperties.Enable)
				{
					AttachmentProperties.FilesAttachments.Clear();
					AttachmentProperties.WebAttachments.Clear();
				}
				#endregion
			}
			else
			{
				#region Linebreak properties
				LineBreakProperties.Font = buttonEditLineBreakFont.Tag as Font;
				LineBreakProperties.BoldFont = new Font(LineBreakProperties.Font.Name, LineBreakProperties.Font.Size, FontStyle.Bold);
				LineBreakProperties.ForeColor = colorEditLineBreakFontColor.Color;
				LineBreakProperties.Note = memoEditNote.EditValue != null ? memoEditNote.EditValue.ToString().Trim() : string.Empty;
				#endregion
			}

			EnableWidget = checkBoxEnableWidget.Checked;
			Widget = pbSelectedWidget.Image;

			#region Banner properties
			BannerProperties.Enable = checkBoxEnableBanner.Checked;
			BannerProperties.Image = pbSelectedBanner.Image;
			if (rbBannerAligmentLeft.Checked)
				BannerProperties.ImageAlignement = Alignment.Left;
			else if (rbBannerAligmentCenter.Checked)
				BannerProperties.ImageAlignement = Alignment.Center;
			else if (rbBannerAligmentRight.Checked)
				BannerProperties.ImageAlignement = Alignment.Right;
			BannerProperties.ShowText = checkBoxBannerShowText.Checked;
			BannerProperties.Text = memoEditBannerText.EditValue != null ? memoEditBannerText.EditValue.ToString() : string.Empty;
			BannerProperties.Font = buttonEditBannerTextFont.Tag as Font;
			BannerProperties.ForeColor = colorEditBannerTextColor.Color;
			BannerProperties.Configured = true;
			#endregion

			DialogResult = DialogResult.OK;
			Close();
		}

		private void rbNew_CheckedChanged(object sender, EventArgs e)
		{
			edCustomNote.Enabled = rbCustomNote.Checked;
		}

		#region Expiration date processing
		private void checkBoxEnableExpiredLinks_CheckedChanged(object sender, EventArgs e)
		{
			gbExpiredLinks.Enabled = checkBoxEnableExpiredLinks.Checked;
			if (checkBoxEnableExpiredLinks.Checked)
			{
				dateEditExpirationDate.DateTime = ExpirationDateOptions.ExpirationDate;
				timeEditExpirationTime.Time = ExpirationDateOptions.ExpirationDate;
			}
			else
			{
				dateEditExpirationDate.EditValue = DateTime.MinValue;
				timeEditExpirationTime.Time = DateTime.MinValue;
			}
		}
		#endregion

		#region Widget processing
		private void checkBoxEnableWidget_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxWidgets.Enabled = checkBoxEnableWidget.Checked;
			if (checkBoxEnableWidget.Checked)
				checkBoxEnableBanner.Checked = false;
		}

		private void xtraTabControlWidgets_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			laWidgetFileName.Text = string.Empty;
		}

		private void layoutViewWidgetsGallery_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			Widget selectedWidget = null;
			if (layoutViewWidgetsGallery.FocusedRowHandle != GridControl.InvalidRowHandle)
				selectedWidget = ListManager.Instance.Widgets[layoutViewWidgetsGallery.GetDataSourceRowIndex(layoutViewWidgetsGallery.FocusedRowHandle)];
			pbSelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
			laWidgetFileName.Text = selectedWidget != null ? selectedWidget.FileName : string.Empty;
		}

		private void layoutViewWidgetsFavs_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			Widget selectedWidget = null;
			if (layoutViewWidgetsFavs.FocusedRowHandle != GridControl.InvalidRowHandle)
				selectedWidget = ListManager.Instance.WidgetsFavs[layoutViewWidgetsFavs.GetDataSourceRowIndex(layoutViewWidgetsFavs.FocusedRowHandle)];
			pbSelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
			laWidgetFileName.Text = selectedWidget != null ? selectedWidget.FileName : string.Empty;
		}


		private void layoutViewWidgetsGallery_Click(object sender, EventArgs e)
		{
			var pt = gridControlWidgetsGallery.PointToClient(MousePosition);
			if (layoutViewWidgetsGallery.CalcHitInfo(pt).RowHandle == layoutViewWidgetsGallery.FocusedRowHandle)
				layoutViewWidgetsGallery_FocusedRowChanged(null, null);
		}

		private void layoutViewWidgetsFavs_Click(object sender, EventArgs e)
		{
			var pt = gridControlWidgetsFavs.PointToClient(MousePosition);
			if (layoutViewWidgetsFavs.CalcHitInfo(pt).RowHandle == layoutViewWidgetsFavs.FocusedRowHandle)
				layoutViewWidgetsFavs_FocusedRowChanged(null, null);
		}

		private void layoutViewWidgetsGallery_DoubleClick(object sender, EventArgs e)
		{
			var pt = gridControlWidgetsGallery.PointToClient(MousePosition);
			if (layoutViewWidgetsGallery.CalcHitInfo(pt).InField)
			{
				layoutViewWidgetsGallery_Click(sender, e);
				btOK_Click(null, null);
			}
		}

		private void layoutViewWidgetsFavs_DoubleClick(object sender, EventArgs e)
		{
			var pt = gridControlWidgetsFavs.PointToClient(MousePosition);
			if (layoutViewWidgetsFavs.CalcHitInfo(pt).InField)
			{
				layoutViewWidgetsFavs_Click(sender, e);
				btOK_Click(null, null);
			}
		}

		private void gridControlWidgetsGallery_MouseUp(object sender, MouseEventArgs e)
		{
			_hitInfo = layoutViewWidgetsGallery.CalcHitInfo(e.Location);
			if (_hitInfo.InField && e.Button == MouseButtons.Right)
				contextMenuStrip.Show(MousePosition);
		}
		#endregion

		#region Banner processing
		private void checkBoxEnableBanner_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxBanners.Enabled = checkBoxEnableBanner.Checked;
			if (checkBoxEnableBanner.Checked)
				checkBoxEnableWidget.Checked = false;
		}

		private void xtraTabControlBanners_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
			laBannerFileName.Text = string.Empty;
		}

		private void layoutViewBannersGalery_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			Banner selectedBanner = null;
			if (layoutViewBannersGallery.FocusedRowHandle != GridControl.InvalidRowHandle)
				selectedBanner = ListManager.Instance.Banners[layoutViewBannersGallery.GetDataSourceRowIndex(layoutViewBannersGallery.FocusedRowHandle)];
			pbSelectedBanner.Image = selectedBanner != null ? selectedBanner.Image : null;
			laBannerFileName.Text = selectedBanner != null ? selectedBanner.FileName : string.Empty;
		}

		private void layoutViewBannersFavs_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			Banner selectedBanner = null;
			if (layoutViewBannersFavs.FocusedRowHandle != GridControl.InvalidRowHandle)
				selectedBanner = ListManager.Instance.BannersFavs[layoutViewBannersFavs.GetDataSourceRowIndex(layoutViewBannersFavs.FocusedRowHandle)];
			pbSelectedBanner.Image = selectedBanner != null ? selectedBanner.Image : null;
			laBannerFileName.Text = selectedBanner != null ? selectedBanner.FileName : string.Empty;
		}

		private void gridViewBannersGallery_Click(object sender, EventArgs e)
		{
			var pt = gridControlBannersGallery.PointToClient(MousePosition);
			if (layoutViewBannersGallery.CalcHitInfo(pt).RowHandle == layoutViewBannersGallery.FocusedRowHandle)
				layoutViewBannersGalery_FocusedRowChanged(null, null);
		}

		private void gridViewBannersFavs_Click(object sender, EventArgs e)
		{
			var pt = gridControlBannersFavs.PointToClient(MousePosition);
			if (layoutViewBannersFavs.CalcHitInfo(pt).RowHandle == layoutViewBannersFavs.FocusedRowHandle)
				layoutViewBannersFavs_FocusedRowChanged(null, null);
		}

		private void gridViewBannersGallery_DoubleClick(object sender, EventArgs e)
		{
			var pt = gridControlBannersGallery.PointToClient(MousePosition);
			if (layoutViewBannersGallery.CalcHitInfo(pt).InField)
			{
				gridViewBannersGallery_Click(sender, e);
				btOK_Click(null, null);
			}
		}

		private void gridViewBannersFavs_DoubleClick(object sender, EventArgs e)
		{
			gridViewBannersFavs_Click(sender, e);
			var pt = gridControlBannersFavs.PointToClient(MousePosition);
			if (layoutViewBannersFavs.CalcHitInfo(pt).InField)
			{
				gridViewBannersFavs_Click(sender, e);
				btOK_Click(null, null);
			}
		}

		private void gridControlBannersGallery_MouseUp(object sender, MouseEventArgs e)
		{
			_hitInfo = layoutViewBannersGallery.CalcHitInfo(e.Location);
			if (_hitInfo.InField && e.Button == MouseButtons.Right)
				contextMenuStrip.Show(MousePosition);
		}

		private void checkBoxBannerShowText_CheckedChanged(object sender, EventArgs e)
		{
			memoEditBannerText.Enabled = checkBoxBannerShowText.Checked;
			buttonEditBannerTextFont.Enabled = checkBoxBannerShowText.Checked;
			colorEditBannerTextColor.Enabled = checkBoxBannerShowText.Checked;
		}

		private void colorEditBannerTextColor_EditValueChanged(object sender, EventArgs e)
		{
			memoEditBannerText.ForeColor = colorEditBannerTextColor.Color;
		}

		private void buttonEditBannerTextFont_EditValueChanged(object sender, EventArgs e)
		{
			memoEditBannerText.Font = buttonEditBannerTextFont.Tag as Font;
			;
			memoEditBannerText.Properties.Appearance.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = memoEditBannerText.Font;
		}
		#endregion

		#region Categories Processing
		private void UpdateCategoriesHeader()
		{
			var totalTags = ListManager.Instance.SearchTags.SearchGroups.Sum(g => g.ListBox.CheckedItemsCount);
			labelControlSearchTagsCategoriesHeader.Text = String.Format("{0}{1}",
				ListManager.Instance.SearchTags.MaxTags > 0 ? String.Format("Only {0} Search Tags are ALLOWED{1}", ListManager.Instance.SearchTags.MaxTags, Environment.NewLine) : String.Empty,
				totalTags > 0 ? String.Join(", ", ListManager.Instance.SearchTags.SearchGroups.SelectMany(g => g.ListBox.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => it.Value.ToString()))) : "No Tags Selected"
				);
		}

		private void ListBox_ItemChecking(object sender, ItemCheckingEventArgs e)
		{
			if (!IsLoading && ListManager.Instance.SearchTags.MaxTags > 0 && e.NewValue == CheckState.Checked)
			{
				var totalTags = ListManager.Instance.SearchTags.SearchGroups.Sum(g => g.ListBox.CheckedItemsCount);
				if (totalTags >= ListManager.Instance.SearchTags.MaxTags)
				{
					AppManager.Instance.ShowWarning(String.Format("Only {0} Search Tags are ALLOWED", ListManager.Instance.SearchTags.MaxTags));
					e.Cancel = true;
				}
			}
		}

		private void buttonXWipeTags_Click(object sender, EventArgs e)
		{
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
				searchGroup.ListBox.UnCheckAll();
		}

		private void CategoriesGroup_Click(object sender, EventArgs e)
		{
			var button = sender as DevComponents.DotNetBar.ButtonX;
			if (button == null || button.Checked) return;
			ListManager.Instance.SearchTags.SearchGroups.ForEach(g => g.ToggleButton.Checked = false);
			button.Checked = true;
		}

		private void CategoriesGroup_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as DevComponents.DotNetBar.ButtonX;
			if (button == null || !button.Checked) return;
			var assignedControl = button.Tag as Control;
			if (assignedControl == null) return;
			assignedControl.Dock = DockStyle.Fill;
			assignedControl.BringToFront();
		}

		private void hyperLinkEditRequestNewCategories_OpenLink(object sender, OpenLinkEventArgs e)
		{
			try
			{
				Process.Start(String.Format("mailto:{0}?subject={1}&body={2}", SettingsManager.Instance.CategoryRequestRecipients, SettingsManager.Instance.CategoryRequestSubject, SettingsManager.Instance.CategoryRequestBody));
			}
			catch { }
			e.Handled = true;
		}
		#endregion

		#region Keyword processing
		private void UpdateKeywordsDataSource()
		{
			gridControlSearchTagsKeywords.DataSource = Keywords;
			gridViewSearchTagsKeywords.RefreshData();
		}

		private void SaveKeywordsDataSource()
		{
			gridViewSearchTagsKeywords.CloseEditor();
			Keywords.RemoveAll(x => string.IsNullOrEmpty(x.Value));
		}

		private void repositoryItemButtonEditKeyword_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewSearchTagsKeywords.CloseEditor();
			if (gridViewSearchTagsKeywords.FocusedRowHandle >= 0 && gridViewSearchTagsKeywords.FocusedRowHandle < gridViewSearchTagsKeywords.RowCount)
			{
				Keywords.RemoveAt(gridViewSearchTagsKeywords.GetDataSourceRowIndex(gridViewSearchTagsKeywords.FocusedRowHandle));
				gridViewSearchTagsKeywords.RefreshData();
			}
		}

		private void buttonXAddKeyWord_Click(object sender, EventArgs e)
		{
			SaveKeywordsDataSource();
			Keywords.Add(new StringDataSourceWrapper());
			gridViewSearchTagsKeywords.RefreshData();
			if (gridViewSearchTagsKeywords.RowCount > 0)
			{
				gridViewSearchTagsKeywords.FocusedRowHandle = gridViewSearchTagsKeywords.RowCount - 1;
				gridViewSearchTagsKeywords.MakeRowVisible(gridViewSearchTagsKeywords.FocusedRowHandle, true);
			}
		}
		#endregion

		#region File Card processing
		private void checkBoxEnableFileCard_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxFileCard.Enabled = checkBoxEnableFileCard.Checked;
			textEditFileCardTitle.Enabled = checkBoxEnableFileCard.Checked;
			if (!checkBoxEnableFileCard.Checked)
			{
				checkBoxFileCardAdvertiser.Checked = false;
				checkBoxFileCardDateSold.Checked = false;
				checkBoxFileCardBroadcastClosed.Checked = false;
				checkBoxFileCardDigitalClosed.Checked = false;
				checkBoxFileCardPublishingClosed.Checked = false;
				checkBoxFileCardSalesInfo.Checked = false;
			}
		}

		private void checkBoxFileCardAdvertiser_CheckedChanged(object sender, EventArgs e)
		{
			textEditFileCardAdvertiser.Enabled = checkBoxFileCardAdvertiser.Checked;
			if (!checkBoxFileCardAdvertiser.Checked)
				textEditFileCardAdvertiser.EditValue = null;
		}

		private void checkBoxFileCardDateSold_CheckedChanged(object sender, EventArgs e)
		{
			dateEditFileCardDateSold.Enabled = checkBoxFileCardDateSold.Checked;
			if (!checkBoxFileCardDateSold.Checked)
				dateEditFileCardDateSold.EditValue = null;
		}

		private void checkBoxFileCardBroadcastClosed_CheckedChanged(object sender, EventArgs e)
		{
			spinEditFileCardBroadcastClosed.Enabled = checkBoxFileCardBroadcastClosed.Checked;
			if (!checkBoxFileCardBroadcastClosed.Checked)
				spinEditFileCardBroadcastClosed.EditValue = null;
		}

		private void checkBoxFileCardDigitalClosed_CheckedChanged(object sender, EventArgs e)
		{
			spinEditFileCardDigitalClosed.Enabled = checkBoxFileCardDigitalClosed.Checked;
			if (!checkBoxFileCardDigitalClosed.Checked)
				spinEditFileCardDigitalClosed.EditValue = null;
		}

		private void checkBoxFileCardPublishingClosed_CheckedChanged(object sender, EventArgs e)
		{
			spinEditFileCardPublishingClosed.Enabled = checkBoxFileCardPublishingClosed.Checked;
			if (!checkBoxFileCardPublishingClosed.Checked)
				spinEditFileCardPublishingClosed.EditValue = null;
		}

		private void checkBoxFileCardSalesInfo_CheckedChanged(object sender, EventArgs e)
		{
			textEditFileCardSalesName.Enabled = checkBoxFileCardSalesInfo.Checked;
			textEditFileCardSalesEmail.Enabled = checkBoxFileCardSalesInfo.Checked;
			textEditFileCardSalesPhone.Enabled = checkBoxFileCardSalesInfo.Checked;
			textEditFileCardSalesStation.Enabled = checkBoxFileCardSalesInfo.Checked;
			if (!checkBoxFileCardSalesInfo.Checked)
			{
				textEditFileCardSalesName.EditValue = null;
				textEditFileCardSalesEmail.EditValue = null;
				textEditFileCardSalesPhone.EditValue = null;
				textEditFileCardSalesStation.EditValue = null;
			}
		}

		private void checkBoxFileCardImportantInfo_CheckedChanged(object sender, EventArgs e)
		{
			buttonXFileCardImportantInfoAdd.Enabled = checkBoxFileCardImportantInfo.Checked;
			gridControlFileCardImportantInfo.Enabled = checkBoxFileCardImportantInfo.Checked;
			if (!checkBoxFileCardImportantInfo.Checked)
			{
				FileCardImportantInfo.Clear();
				gridViewFileCardImportantInfo.RefreshData();
			}
		}

		private void buttonXFileCardImportantInfoAdd_Click(object sender, EventArgs e)
		{
			gridViewFileCardImportantInfo.CloseEditor();
			FileCardImportantInfo.RemoveAll(x => string.IsNullOrEmpty(x.Value));
			FileCardImportantInfo.Add(new StringDataSourceWrapper());
			gridViewFileCardImportantInfo.RefreshData();
			if (gridViewFileCardImportantInfo.RowCount > 0)
			{
				gridViewFileCardImportantInfo.FocusedRowHandle = gridViewFileCardImportantInfo.RowCount - 1;
				gridViewFileCardImportantInfo.MakeRowVisible(gridViewFileCardImportantInfo.FocusedRowHandle, true);
			}
		}

		private void repositoryItemButtonEditFileCardImportantInfo_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewFileCardImportantInfo.CloseEditor();
			if (gridViewFileCardImportantInfo.FocusedRowHandle >= 0 && gridViewFileCardImportantInfo.FocusedRowHandle < gridViewFileCardImportantInfo.RowCount)
			{
				FileCardImportantInfo.RemoveAt(gridViewFileCardImportantInfo.GetDataSourceRowIndex(gridViewFileCardImportantInfo.FocusedRowHandle));
				gridViewFileCardImportantInfo.RefreshData();
			}
		}
		#endregion

		#region Attachments processing
		private void checkBoxEnableAttachments_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxAttachments.Enabled = checkBoxEnableAttachmnets.Checked;
		}

		#region Files
		private void buttonXAttachmentsFilesAdd_Click(object sender, EventArgs e)
		{
			using (var dialog = new OpenFileDialog())
			{
				dialog.Multiselect = true;
				dialog.Title = "Attach file";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					foreach (string fileName in dialog.FileNames)
					{
						if (!AttachmentProperties.FilesAttachments.Any(x => x.OriginalPath.ToLower().Equals(fileName.ToLower())))
						{
							var attachment = new LinkAttachment(AttachmentProperties);
							attachment.Type = AttachmentType.File;
							attachment.OriginalPath = fileName;
							AttachmentProperties.FilesAttachments.Add(attachment);
							gridViewAttachmentsFiles.RefreshData();
							if (gridViewAttachmentsFiles.RowCount > 0)
							{
								gridViewAttachmentsFiles.FocusedRowHandle = gridViewAttachmentsFiles.RowCount - 1;
								gridViewAttachmentsFiles.MakeRowVisible(gridViewAttachmentsFiles.FocusedRowHandle, true);
							}
						}
					}
				}
			}
		}

		private void repositoryItemButtonEditAttachmentsFiles_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewAttachmentsFiles.CloseEditor();
			if (gridViewAttachmentsFiles.FocusedRowHandle >= 0 && gridViewAttachmentsFiles.FocusedRowHandle < gridViewAttachmentsFiles.RowCount)
			{
				if (e.Button.Index == 0)
				{
					LinkAttachment attachment = AttachmentProperties.FilesAttachments[gridViewAttachmentsFiles.GetDataSourceRowIndex(gridViewAttachmentsFiles.FocusedRowHandle)];
					if (attachment.IsSourceAvailable)
					{
						try
						{
							Process.Start(attachment.OriginalPath);
						}
						catch
						{
							AppManager.Instance.ShowWarning("Attachment is not available");
						}
					}
					else
						AppManager.Instance.ShowWarning("Attachment is not available");
				}
				else if (e.Button.Index == 1)
				{
					AttachmentProperties.FilesAttachments.RemoveAt(gridViewAttachmentsFiles.GetDataSourceRowIndex(gridViewAttachmentsFiles.FocusedRowHandle));
					gridViewAttachmentsFiles.RefreshData();
				}
			}
		}

		private void gridViewAttachmentsFiles_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			int attachmentIndex = gridViewAttachmentsFiles.GetDataSourceRowIndex(e.RowHandle);
			if (attachmentIndex >= 0 && attachmentIndex < AttachmentProperties.FilesAttachments.Count)
			{
				LinkAttachment attachment = AttachmentProperties.FilesAttachments[attachmentIndex];
				if (attachment.IsSourceAvailable)
					e.Appearance.ForeColor = Color.Black;
				else
					e.Appearance.ForeColor = Color.Red;
			}
		}
		#endregion

		#region Websites
		private void buttonXAttachmentsWebAdd_Click(object sender, EventArgs e)
		{
			gridViewAttachmentsWeb.CloseEditor();
			AttachmentProperties.WebAttachments.RemoveAll(x => string.IsNullOrEmpty(x.OriginalPath));
			var attachment = new LinkAttachment(AttachmentProperties);
			attachment.Type = AttachmentType.Url;
			AttachmentProperties.WebAttachments.Add(attachment);
			gridViewAttachmentsWeb.RefreshData();
			if (gridViewAttachmentsWeb.RowCount > 0)
			{
				gridViewAttachmentsWeb.FocusedRowHandle = gridViewAttachmentsWeb.RowCount - 1;
				gridViewAttachmentsWeb.MakeRowVisible(gridViewAttachmentsWeb.FocusedRowHandle, true);
			}
		}

		private void repositoryItemButtonEditAttachmentsWeb_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			gridViewAttachmentsWeb.CloseEditor();
			if (gridViewAttachmentsWeb.FocusedRowHandle >= 0 && gridViewAttachmentsWeb.FocusedRowHandle < gridViewAttachmentsWeb.RowCount)
			{
				if (e.Button.Index == 0)
				{
					LinkAttachment attachment = AttachmentProperties.WebAttachments[gridViewAttachmentsWeb.GetDataSourceRowIndex(gridViewAttachmentsWeb.FocusedRowHandle)];
					try
					{
						Process.Start(attachment.OriginalPath);
					}
					catch
					{
						AppManager.Instance.ShowWarning("Website is not available");
					}
				}
				else if (e.Button.Index == 1)
				{
					AttachmentProperties.WebAttachments.RemoveAt(gridViewAttachmentsWeb.GetDataSourceRowIndex(gridViewAttachmentsWeb.FocusedRowHandle));
					gridViewAttachmentsWeb.RefreshData();
				}
			}
		}
		#endregion

		#endregion

		#region Security Processing
		private void LoadSecurityGroups()
		{
			rbSecurityWhiteList.Enabled = false;
			rbSecurityBlackList.Enabled = false;
			pnSecurityUserListGrid.Visible = false;
			gridControlSecurityUserList.DataSource = null;
			_securityGroups.Clear();
			laSecurityUserListInfo.Visible = true;
			laSecurityUserListInfo.BringToFront();
			if (!SettingsManager.Instance.WebServiceConnected)
				laSecurityUserListInfo.Text = String.Format("Service coonection is not configured");
			circularSecurityUserListProgress.Visible = true;
			circularSecurityUserListProgress.BringToFront();
			laSecurityUserListInfo.Text = String.Format("Loading user list from {0}...", SettingsManager.Instance.WebServiceSite);
			var message = String.Empty;
			var thread = new Thread(() =>
			{
				_securityGroups.AddRange(_library.IPadManager.GetGroupsByLibrary(out message));
				Invoke((MethodInvoker)delegate
				{
					circularSecurityUserListProgress.Visible = false;
					if (!String.IsNullOrEmpty(message))
						laSecurityUserListInfo.Text = String.Format("Couldn't load user list from {0}", SettingsManager.Instance.WebServiceSite);
					else if (!_securityGroups.Any())
						laSecurityUserListInfo.Text = String.Format("There is no users on {0}", SettingsManager.Instance.WebServiceSite);
					else
					{
						laSecurityUserListInfo.Visible = false;
						pnSecurityUserListGrid.Visible = true;
						gridControlSecurityUserList.DataSource = _securityGroups.Where(g => g.users != null).ToList();
						ApplyAssignedUsers();
						ApplyDeniedUsers();
						rbSecurityWhiteList.Enabled = true;
						rbSecurityBlackList.Enabled = true;
					}
				});
			});
			thread.Start();
		}

		private void ApplyAssignedUsers()
		{
			if (rbSecurityWhiteList.Checked)
			{
				foreach (var groupModel in _securityGroups.Where(g => g.users != null))
				{
					foreach (var userModel in groupModel.users)
						userModel.selected = _assignedUsers.Contains(userModel.login);
					groupModel.selected = groupModel.users.Any(u => u.selected);
				}
				gridControlSecurityUserList.RefreshDataSource();
			}
		}

		private void ApplyDeniedUsers()
		{
			if (rbSecurityBlackList.Checked)
			{
				foreach (var groupModel in _securityGroups.Where(g => g.users != null))
				{
					foreach (var userModel in groupModel.users)
						userModel.selected = _deniedUsers.Contains(userModel.login);
					groupModel.selected = groupModel.users.Any(u => u.selected);
				}
				gridControlSecurityUserList.RefreshDataSource();
			}
		}

		private void rbSecurityRestricted_CheckedChanged(object sender, EventArgs e)
		{
			pnSecurityUserListGrid.Enabled = rbSecurityWhiteList.Checked || rbSecurityBlackList.Checked;
			if (IsLoading) return;
			if (!rbSecurityWhiteList.Checked)
				_assignedUsers.Clear();
			if (!rbSecurityBlackList.Checked)
				_assignedUsers.Clear();
			ApplyAssignedUsers();
			ApplyDeniedUsers();
		}

		private void buttonXSecurityUserListSelectAll_Click(object sender, EventArgs e)
		{
			foreach (var groupModel in _securityGroups.Where(g => g.users != null))
			{
				foreach (var userModel in groupModel.users)
					userModel.selected = true;
				groupModel.selected = groupModel.users.Any(u => u.selected);
			}
			gridControlSecurityUserList.RefreshDataSource();
		}

		private void buttonXSecurityUserListClearAll_Click(object sender, EventArgs e)
		{
			foreach (var groupModel in _securityGroups.Where(g => g.users != null))
			{
				foreach (var userModel in groupModel.users)
					userModel.selected = false;
				groupModel.selected = groupModel.users.Any(u => u.selected);
			}
			gridControlSecurityUserList.RefreshDataSource();
		}

		private void OnGroupChildListIsEmpty(object sender, MasterRowEmptyEventArgs e)
		{
			e.IsEmpty = !(e.RowHandle != GridControl.InvalidRowHandle && _securityGroups[e.RowHandle].users != null && _securityGroups[e.RowHandle].users.Any());
		}

		private void OnGetGroupRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
		{
			e.RelationCount = 1;
		}

		private void OnGetGroupRelationName(object sender, MasterRowGetRelationNameEventArgs e)
		{
			e.RelationName = "Users";
		}

		private void OnGetGroupChildList(object sender, MasterRowGetChildListEventArgs e)
		{
			if (e.RowHandle != GridControl.InvalidRowHandle && _securityGroups[e.RowHandle].users != null)
				e.ChildList = _securityGroups[e.RowHandle].users.ToArray();
		}

		private void RepositoryItemCheckEditCheckedChanged(object sender, EventArgs e)
		{
			var focussedView = gridControlSecurityUserList.FocusedView as GridView;
			if (focussedView == null) return;
			focussedView.CloseEditor();
			if (focussedView == gridViewSecurityGroups)
			{
				if (focussedView.FocusedRowHandle == GridControl.InvalidRowHandle) return;
				var groupModel = focussedView.GetFocusedRow() as GroupModel;
				if (groupModel == null) return;
				if (groupModel.users == null) return;
				foreach (var userModel in groupModel.users)
					userModel.selected = groupModel.selected;
				var usersView = focussedView.GetDetailView(focussedView.FocusedRowHandle, 0) as GridView;
				if (usersView != null)
					usersView.RefreshData();
			}
			else
			{
				var groupModel = focussedView.SourceRow as GroupModel;
				var userModel = focussedView.GetFocusedRow() as UserModel;
				if (groupModel == null || userModel == null || !userModel.selected) return;
				groupModel.selected = userModel.selected;
				gridControlSecurityUserList.MainView.RefreshData();
			}
		}
		#endregion

		#region Shared methods
		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			xtraTabControl.Focus();
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			ToolTipControlInfo info = null;
			if (e.SelectedControl == gridControlWidgetsGallery)
			{
				try
				{
					var view = gridControlWidgetsGallery.GetViewAt(e.ControlMousePosition) as LayoutView;
					if (view == null)
						return;
					var hi = view.CalcHitInfo(e.ControlMousePosition);
					if (hi.InFieldValue)
						info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), ListManager.Instance.Widgets[layoutViewWidgetsGallery.GetDataSourceRowIndex(hi.RowHandle)].FileName);
				}
				finally
				{
					e.Info = info;
				}
			}
			else if (e.SelectedControl == gridControlWidgetsFavs)
			{
				try
				{
					var view = gridControlWidgetsFavs.GetViewAt(e.ControlMousePosition) as LayoutView;
					if (view == null)
						return;
					var hi = view.CalcHitInfo(e.ControlMousePosition);
					if (hi.InFieldValue)
						info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), ListManager.Instance.WidgetsFavs[layoutViewWidgetsFavs.GetDataSourceRowIndex(hi.RowHandle)].FileName);
				}
				finally
				{
					e.Info = info;
				}
			}
			else if (e.SelectedControl == gridControlBannersGallery)
			{
				try
				{
					var view = gridControlBannersGallery.GetViewAt(e.ControlMousePosition) as LayoutView;
					if (view == null)
						return;
					var hi = view.CalcHitInfo(e.ControlMousePosition);
					if (hi.InField)
						info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), ListManager.Instance.Banners[layoutViewBannersGallery.GetDataSourceRowIndex(hi.RowHandle)].FileName);
				}
				finally
				{
					e.Info = info;
				}
			}
			else if (e.SelectedControl == gridControlBannersFavs)
			{
				try
				{
					var view = gridControlBannersFavs.GetViewAt(e.ControlMousePosition) as LayoutView;
					if (view == null)
						return;
					var hi = view.CalcHitInfo(e.ControlMousePosition);
					if (hi.InField)
						info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), ListManager.Instance.BannersFavs[layoutViewBannersFavs.GetDataSourceRowIndex(hi.RowHandle)].FileName);
				}
				finally
				{
					e.Info = info;
				}
			}
		}

		private void FontEdit_Click(object sender, EventArgs e)
		{
			var fontEdit = sender as ButtonEdit;
			if (fontEdit != null)
			{
				dlgFont.Font = fontEdit.Tag as Font;
				if (dlgFont.ShowDialog() == DialogResult.OK)
				{
					fontEdit.Tag = dlgFont.Font;
					fontEdit.EditValue = FontToString(dlgFont.Font);
				}
			}
		}

		private void FontEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			FontEdit_Click(this, null);
		}

		private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_hitInfo == null) return;
			if (xtraTabControl.SelectedTabPage == xtraTabPageBanner)
			{
				var selectedBanner = ListManager.Instance.Banners[layoutViewBannersGallery.GetDataSourceRowIndex(_hitInfo.RowHandle)];
				selectedBanner.CopyToFavs();
				layoutViewBannersFavs.FocusedRowChanged -= layoutViewBannersFavs_FocusedRowChanged;
				gridControlBannersFavs.DataSource = new BindingList<Banner>(ListManager.Instance.BannersFavs);
				layoutViewBannersFavs.FocusedRowChanged += layoutViewBannersFavs_FocusedRowChanged;
			}
			else if (xtraTabControl.SelectedTabPage == xtraTabPageWidgets)
			{
				var selectedWidget = ListManager.Instance.Widgets[layoutViewWidgetsGallery.GetDataSourceRowIndex(_hitInfo.RowHandle)];
				selectedWidget.CopyToFavs();
				layoutViewWidgetsFavs.FocusedRowChanged -= layoutViewWidgetsFavs_FocusedRowChanged;
				gridControlWidgetsFavs.DataSource = new BindingList<Widget>(ListManager.Instance.WidgetsFavs);
				layoutViewWidgetsFavs.FocusedRowChanged += layoutViewWidgetsFavs_FocusedRowChanged;
			}
		}
		#endregion

		#region Admin Tools
		private void buttonXOpenQV_Click(object sender, EventArgs e)
		{
			OpenQV();
		}

		private void buttonXOpenWV_Click(object sender, EventArgs e)
		{
			OpenWV();
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("Are you sure want to delete preview files for the link?") == DialogResult.Yes)
			{
				RefreshPreview();
				AppManager.Instance.ShowInfo("Preview files for the link was deleted and will re-create during next Sync.");
			}
		}
		#endregion
	}
}