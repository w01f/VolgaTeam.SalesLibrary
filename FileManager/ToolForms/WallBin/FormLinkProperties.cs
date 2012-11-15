using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.WallBin
{
	public partial class FormLinkProperties : Form
	{
		private string _note = string.Empty;
		private bool _isBold = false;
		private bool _closeEventAssigned = false;

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


		public FormLinkProperties()
		{
			InitializeComponent();

			if ((base.CreateGraphics()).DpiX > 96)
			{
				Font styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
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
				laSelectedBanner.Font = new Font(laSelectedBanner.Font.FontFamily, laSelectedBanner.Font.Size - 2, laSelectedBanner.Font.Style);
				laBannerAligment.Font = new Font(laBannerAligment.Font.FontFamily, laBannerAligment.Font.Size - 2, laBannerAligment.Font.Style);
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

				repositoryItemButtonEditKeyword.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				repositoryItemButtonEditKeyword.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				repositoryItemButtonEditKeyword.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				repositoryItemButtonEditAttachmentsFiles.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				repositoryItemButtonEditAttachmentsFiles.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				repositoryItemButtonEditAttachmentsFiles.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				repositoryItemButtonEditAttachmentsWeb.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				repositoryItemButtonEditAttachmentsWeb.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				repositoryItemButtonEditAttachmentsWeb.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);

				textEditFileCardAdvertiser.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				textEditFileCardAdvertiser.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardAdvertiser.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				dateEditFileCardDateSold.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				dateEditFileCardDateSold.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				dateEditFileCardDateSold.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				spinEditFileCardBroadcastClosed.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				spinEditFileCardBroadcastClosed.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				spinEditFileCardBroadcastClosed.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				spinEditFileCardDigitalClosed.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				spinEditFileCardDigitalClosed.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				spinEditFileCardDigitalClosed.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				spinEditFileCardPublishingClosed.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				spinEditFileCardPublishingClosed.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				spinEditFileCardPublishingClosed.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardSalesName.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				textEditFileCardSalesName.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardSalesName.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardSalesEmail.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				textEditFileCardSalesEmail.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardSalesEmail.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardSalesPhone.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				textEditFileCardSalesPhone.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardSalesPhone.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardSalesStation.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				textEditFileCardSalesStation.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				textEditFileCardSalesStation.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				repositoryItemMemoEditFileCardImportantInfo.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
				repositoryItemMemoEditFileCardImportantInfo.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
				repositoryItemMemoEditFileCardImportantInfo.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
			}

			if (!this.IsLineBreak)
			{
				this.SearchTags = new LibraryFileSearchTags();
				this.ExpirationDateOptions = new ExpirationDateOptions();
				dateEditExpirationDate.Properties.NullDate = DateTime.MinValue;

				#region Search tags
				#region Categories
				if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 0)
				{
					navBarGroup1.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0].Name;
					checkedListBoxControlGroup1.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0].Tags.ToArray());
				}
				else
					navBarGroup1.Visible = false;
				if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 1)
				{
					navBarGroup2.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1].Name;
					checkedListBoxControlGroup2.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1].Tags.ToArray());
				}
				else
					navBarGroup2.Visible = false;
				if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 2)
				{
					navBarGroup3.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2].Name;
					checkedListBoxControlGroup3.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2].Tags.ToArray());
				}
				else
					navBarGroup3.Visible = false;
				if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 3)
				{
					navBarGroup4.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3].Name;
					checkedListBoxControlGroup4.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3].Tags.ToArray());
				}
				else
					navBarGroup4.Visible = false;
				if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 4)
				{
					navBarGroup5.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4].Name;
					checkedListBoxControlGroup5.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4].Tags.ToArray());
				}
				else
					navBarGroup5.Visible = false;
				if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 5)
				{
					navBarGroup6.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5].Name;
					checkedListBoxControlGroup6.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5].Tags.ToArray());
				}
				else
					navBarGroup6.Visible = false;
				if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 6)
				{
					navBarGroup7.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6].Name;
					checkedListBoxControlGroup7.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6].Tags.ToArray());
				}
				else
					navBarGroup7.Visible = false;
				#endregion

				this.Keywords = new List<StringDataSourceWrapper>();
				#endregion

				this.FileCardImportantInfo = new List<StringDataSourceWrapper>();
			}

			gridControlWidgets.DataSource = new BindingList<ConfigurationClasses.Widget>(ConfigurationClasses.ListManager.Instance.Widgets);
			gridControlBanners.DataSource = new BindingList<ConfigurationClasses.Banner>(ConfigurationClasses.ListManager.Instance.Banners);
		}

		public string CaptionName
		{
			set
			{
				this.Text = value;
			}
		}

		public string Note
		{
			get
			{
				return _note;
			}
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
			get
			{
				return _isBold;
			}
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
				&& controlType != typeof(DevExpress.XtraGrid.GridControl)
				&& controlType != typeof(DevExpress.XtraEditors.ButtonEdit)
				&& controlType != typeof(DevExpress.XtraEditors.CheckEdit)
				&& controlType != typeof(DevExpress.XtraEditors.CheckedListBoxControl)
				&& controlType != typeof(DevExpress.XtraEditors.ColorEdit)
				&& controlType != typeof(DevExpress.XtraEditors.ComboBoxEdit)
				&& controlType != typeof(DevExpress.XtraEditors.DateEdit)
				&& controlType != typeof(DevExpress.XtraEditors.TimeEdit)
				&& controlType != typeof(DevExpress.XtraEditors.MemoEdit)
				&& controlType != typeof(DevExpress.XtraEditors.SpinEdit)
				&& controlType != typeof(DevExpress.XtraEditors.TextEdit)
				&& controlType != typeof(DevExpress.XtraNavBar.NavBarControl))
			{
				control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
				foreach (Control childControl in control.Controls)
				{
					Application.DoEvents();
					AssignCloseActiveEditorsonOutSideClick(childControl);
				}
			}
		}

		private void FormProperties_Load(object sender, EventArgs e)
		{
			xtraTabPageExpiredLinks.PageVisible = !this.IsLineBreak;
			xtraTabPageNotes.PageVisible = !this.IsLineBreak;
			xtraTabPageSearchTags.PageVisible = !this.IsLineBreak;
			xtraTabPageAttachments.PageVisible = !this.IsLineBreak;
			xtraTabPageLineBrealProperties.PageVisible = this.IsLineBreak;
			if (!this.IsLineBreak)
			{
				#region Search tags
				#region Categories
				if (navBarGroup1.Visible)
				{
					checkedListBoxControlGroup1.UnCheckAll();
					SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup1.Caption)).FirstOrDefault();
					if (group != null)
						foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup1.Items)
							if (group.Tags.Contains(item.Value.ToString()))
								item.CheckState = CheckState.Checked;
				}
				if (navBarGroup2.Visible)
				{
					checkedListBoxControlGroup2.UnCheckAll();
					SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup2.Caption)).FirstOrDefault();
					if (group != null)
						foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup2.Items)
							if (group.Tags.Contains(item.Value.ToString()))
								item.CheckState = CheckState.Checked;
				}
				if (navBarGroup3.Visible)
				{
					checkedListBoxControlGroup3.UnCheckAll();
					SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup3.Caption)).FirstOrDefault();
					if (group != null)
						foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup3.Items)
							if (group.Tags.Contains(item.Value.ToString()))
								item.CheckState = CheckState.Checked;
				}
				if (navBarGroup4.Visible)
				{
					checkedListBoxControlGroup4.UnCheckAll();
					SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup4.Caption)).FirstOrDefault();
					if (group != null)
						foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup4.Items)
							if (group.Tags.Contains(item.Value.ToString()))
								item.CheckState = CheckState.Checked;
				}
				if (navBarGroup5.Visible)
				{
					checkedListBoxControlGroup5.UnCheckAll();
					SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup5.Caption)).FirstOrDefault();
					if (group != null)
						foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup5.Items)
							if (group.Tags.Contains(item.Value.ToString()))
								item.CheckState = CheckState.Checked;
				}
				if (navBarGroup6.Visible)
				{
					checkedListBoxControlGroup6.UnCheckAll();
					SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup6.Caption)).FirstOrDefault();
					if (group != null)
						foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup6.Items)
							if (group.Tags.Contains(item.Value.ToString()))
								item.CheckState = CheckState.Checked;
				}
				if (navBarGroup7.Visible)
				{
					checkedListBoxControlGroup7.UnCheckAll();
					SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup7.Caption)).FirstOrDefault();
					if (group != null)
						foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup7.Items)
							if (group.Tags.Contains(item.Value.ToString()))
								item.CheckState = CheckState.Checked;
				}
				#endregion

				#region Keywords
				UpdateKeywordsDataSource();
				#endregion
				#endregion

				#region Expiration date
				laAddDateValue.Text = this.AddDate.ToString("M/dd/yyyy h:mm:ss tt");
				dateEditExpirationDate.DateTime = this.ExpirationDateOptions.ExpirationDate;
				timeEditExpirationTime.Time = this.ExpirationDateOptions.ExpirationDate;
				checkBoxSendEmailWhenDelete.Checked = this.ExpirationDateOptions.SendEmailWhenSync;
				checkBoxLabelLink.Checked = this.ExpirationDateOptions.LabelLinkWhenExpired;
				checkBoxEnableExpiredLinks.Checked = this.ExpirationDateOptions.EnableExpirationDate;
				#endregion

				#region File Card
				checkBoxEnableFileCard.Checked = this.FileCard.Enable;
				textEditFileCardTitle.EditValue = this.FileCard.Title;
				checkBoxFileCardAdvertiser.Checked = !string.IsNullOrEmpty(this.FileCard.Advertiser);
				textEditFileCardAdvertiser.EditValue = this.FileCard.Advertiser;
				checkBoxFileCardDateSold.Checked = this.FileCard.DateSold.HasValue;
				dateEditFileCardDateSold.EditValue = this.FileCard.DateSold;
				checkBoxFileCardBroadcastClosed.Checked = this.FileCard.BroadcastClosed.HasValue;
				spinEditFileCardBroadcastClosed.EditValue = this.FileCard.BroadcastClosed;
				checkBoxFileCardDigitalClosed.Checked = this.FileCard.DigitalClosed.HasValue;
				spinEditFileCardDigitalClosed.EditValue = this.FileCard.DigitalClosed;
				checkBoxFileCardPublishingClosed.Checked = this.FileCard.PublishingClosed.HasValue;
				spinEditFileCardPublishingClosed.EditValue = this.FileCard.PublishingClosed;
				checkBoxFileCardSalesInfo.Checked = !string.IsNullOrEmpty(this.FileCard.SalesName) || !string.IsNullOrEmpty(this.FileCard.SalesEmail) || !string.IsNullOrEmpty(this.FileCard.SalesPhone) || !string.IsNullOrEmpty(this.FileCard.SalesStation);
				textEditFileCardSalesName.EditValue = this.FileCard.SalesName;
				textEditFileCardSalesEmail.EditValue = this.FileCard.SalesEmail;
				textEditFileCardSalesPhone.EditValue = this.FileCard.SalesPhone;
				textEditFileCardSalesStation.EditValue = this.FileCard.SalesStation;

				checkBoxFileCardImportantInfo.Checked = this.FileCardImportantInfo.Count > 0;
				gridControlFileCardImportantInfo.DataSource = this.FileCardImportantInfo;
				gridViewFileCardImportantInfo.RefreshData();
				#endregion

				#region Attachments
				checkBoxEnableAttachmnets.Checked = this.AttachmentProperties.Enable;
				gridControlAttachmentsFiles.DataSource = this.AttachmentProperties.FilesAttachments;
				gridControlAttachmentsWeb.DataSource = this.AttachmentProperties.WebAttachments;
				#endregion
			}
			else
			{
				#region Linebreak properties
				buttonEditLineBreakFont.Tag = this.LineBreakProperties.Font;
				buttonEditLineBreakFont.EditValue = FontToString(this.LineBreakProperties.Font);
				colorEditLineBreakFontColor.Color = this.LineBreakProperties.ForeColor;
				memoEditNote.EditValue = this.LineBreakProperties.Note;
				#endregion
			}

			pbSelectedWidget.Image = this.EnableWidget ? this.Widget : null;
			checkBoxEnableWidget.Checked = this.EnableWidget;

			#region Banner properties
			xtraTabPageBanner.PageEnabled = System.IO.Directory.Exists(ConfigurationClasses.ListManager.Instance.BannerFolder);
			checkBoxEnableBanner.Checked = this.BannerProperties.Enable;
			pbSelectedBanner.Image = this.BannerProperties.Enable ? this.BannerProperties.Image : null;
			switch (this.BannerProperties.ImageAlignement)
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
			checkBoxBannerShowText.Checked = this.BannerProperties.ShowText;
			buttonEditBannerTextFont.Tag = this.BannerProperties.Font;
			buttonEditBannerTextFont.EditValue = FontToString(this.BannerProperties.Font);
			colorEditBannerTextColor.Color = this.BannerProperties.ForeColor;
			memoEditBannerText.EditValue = this.BannerProperties.Text;
			memoEditBannerText.Font = this.BannerProperties.Font;
			memoEditBannerText.Properties.Appearance.Font = this.BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = this.BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = this.BannerProperties.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = this.BannerProperties.Font;
			memoEditBannerText.ForeColor = this.BannerProperties.ForeColor;
			#endregion

			if (!_closeEventAssigned)
			{
				AssignCloseActiveEditorsonOutSideClick(this);
				_closeEventAssigned = true;
			}
		}

		private void btOK_Click(object sender, EventArgs e)
		{
			if (!this.IsLineBreak)
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
				this.SearchTags.SearchGroups.Clear();
				if (checkedListBoxControlGroup1.CheckedItemsCount > 0)
				{
					SearchGroup group = new SearchGroup();
					group.Name = navBarGroup1.Caption;
					foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup1.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(item.Value.ToString());
					this.SearchTags.SearchGroups.Add(group);
				}
				if (checkedListBoxControlGroup2.CheckedItemsCount > 0)
				{
					SearchGroup group = new SearchGroup();
					group.Name = navBarGroup2.Caption;
					foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup2.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(item.Value.ToString());
					this.SearchTags.SearchGroups.Add(group);
				}
				if (checkedListBoxControlGroup3.CheckedItemsCount > 0)
				{
					SearchGroup group = new SearchGroup();
					group.Name = navBarGroup3.Caption;
					foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup3.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(item.Value.ToString());
					this.SearchTags.SearchGroups.Add(group);
				}
				if (checkedListBoxControlGroup4.CheckedItemsCount > 0)
				{
					SearchGroup group = new SearchGroup();
					group.Name = navBarGroup4.Caption;
					foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup4.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(item.Value.ToString());
					this.SearchTags.SearchGroups.Add(group);
				}
				if (checkedListBoxControlGroup5.CheckedItemsCount > 0)
				{
					SearchGroup group = new SearchGroup();
					group.Name = navBarGroup5.Caption;
					foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup5.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(item.Value.ToString());
					this.SearchTags.SearchGroups.Add(group);
				}
				if (checkedListBoxControlGroup6.CheckedItemsCount > 0)
				{
					SearchGroup group = new SearchGroup();
					group.Name = navBarGroup6.Caption;
					foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup6.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(item.Value.ToString());
					this.SearchTags.SearchGroups.Add(group);
				}
				if (checkedListBoxControlGroup7.CheckedItemsCount > 0)
				{
					SearchGroup group = new SearchGroup();
					group.Name = navBarGroup7.Caption;
					foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup7.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(item.Value.ToString());
					this.SearchTags.SearchGroups.Add(group);
				}
				#endregion
				#endregion

				#region Expiration date
				this.ExpirationDateOptions.ExpirationDate = new DateTime(dateEditExpirationDate.DateTime.Year, dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour, timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second);
				this.ExpirationDateOptions.SendEmailWhenSync = checkBoxSendEmailWhenDelete.Checked;
				this.ExpirationDateOptions.LabelLinkWhenExpired = checkBoxLabelLink.Checked;
				this.ExpirationDateOptions.EnableExpirationDate = checkBoxEnableExpiredLinks.Checked;
				#endregion

				#region File Card
				this.FileCard.Enable = checkBoxEnableFileCard.Checked;
				this.FileCard.Title = textEditFileCardTitle.EditValue != null ? textEditFileCardTitle.EditValue.ToString() : string.Empty;
				this.FileCard.Advertiser = this.FileCard.Enable && checkBoxFileCardAdvertiser.Checked && textEditFileCardAdvertiser.EditValue != null ? textEditFileCardAdvertiser.EditValue.ToString() : null;
				this.FileCard.DateSold = this.FileCard.Enable && checkBoxFileCardDateSold.Checked && dateEditFileCardDateSold.EditValue != null ? (DateTime?)dateEditFileCardDateSold.DateTime : null;
				this.FileCard.BroadcastClosed = this.FileCard.Enable && checkBoxFileCardBroadcastClosed.Checked && spinEditFileCardBroadcastClosed.EditValue != null ? (double?)spinEditFileCardBroadcastClosed.Value : null;
				this.FileCard.DigitalClosed = this.FileCard.Enable && checkBoxFileCardDigitalClosed.Checked && spinEditFileCardDigitalClosed.EditValue != null ? (double?)spinEditFileCardDigitalClosed.Value : null;
				this.FileCard.PublishingClosed = this.FileCard.Enable && checkBoxFileCardPublishingClosed.Checked && spinEditFileCardPublishingClosed.EditValue != null ? (double?)spinEditFileCardPublishingClosed.Value : null;
				this.FileCard.SalesName = this.FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesName.EditValue != null ? textEditFileCardSalesName.EditValue.ToString() : null;
				this.FileCard.SalesEmail = this.FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesEmail.EditValue != null ? textEditFileCardSalesEmail.EditValue.ToString() : null;
				this.FileCard.SalesPhone = this.FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesPhone.EditValue != null ? textEditFileCardSalesPhone.EditValue.ToString() : null;
				this.FileCard.SalesStation = this.FileCard.Enable && checkBoxFileCardSalesInfo.Checked && textEditFileCardSalesStation.EditValue != null ? textEditFileCardSalesStation.EditValue.ToString() : null;
				#endregion

				#region Attachments
				this.AttachmentProperties.Enable = checkBoxEnableAttachmnets.Checked;
				if (!this.AttachmentProperties.Enable)
				{
					this.AttachmentProperties.FilesAttachments.Clear();
					this.AttachmentProperties.WebAttachments.Clear();
				}
				#endregion
			}
			else
			{
				#region Linebreak properties
				this.LineBreakProperties.Font = buttonEditLineBreakFont.Tag as Font;
				this.LineBreakProperties.BoldFont = new Font(this.LineBreakProperties.Font.Name, this.LineBreakProperties.Font.Size, FontStyle.Bold);
				this.LineBreakProperties.ForeColor = colorEditLineBreakFontColor.Color;
				this.LineBreakProperties.Note = memoEditNote.EditValue != null ? memoEditNote.EditValue.ToString().Trim() : string.Empty;
				#endregion
			}

			this.EnableWidget = checkBoxEnableWidget.Checked;
			this.Widget = pbSelectedWidget.Image;

			#region Banner properties
			this.BannerProperties.Enable = checkBoxEnableBanner.Checked;
			this.BannerProperties.Image = pbSelectedBanner.Image;
			if (rbBannerAligmentLeft.Checked)
				this.BannerProperties.ImageAlignement = Alignment.Left;
			else if (rbBannerAligmentCenter.Checked)
				this.BannerProperties.ImageAlignement = Alignment.Center;
			else if (rbBannerAligmentRight.Checked)
				this.BannerProperties.ImageAlignement = Alignment.Right;
			this.BannerProperties.ShowText = checkBoxBannerShowText.Checked;
			this.BannerProperties.Text = memoEditBannerText.EditValue != null ? memoEditBannerText.EditValue.ToString() : string.Empty;
			this.BannerProperties.Font = buttonEditBannerTextFont.Tag as Font;
			this.BannerProperties.ForeColor = colorEditBannerTextColor.Color;
			this.BannerProperties.Configured = true;
			#endregion

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
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
				dateEditExpirationDate.DateTime = this.ExpirationDateOptions.ExpirationDate;
				timeEditExpirationTime.Time = this.ExpirationDateOptions.ExpirationDate;
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

		private void layoutViewWidgets_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			ConfigurationClasses.Widget selectedWidget = null;
			if (layoutViewWidgets.FocusedRowHandle >= 0)
			{
				selectedWidget = ConfigurationClasses.ListManager.Instance.Widgets[layoutViewWidgets.GetDataSourceRowIndex(layoutViewWidgets.FocusedRowHandle)];
			}
			pbSelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
		}

		private void layoutViewWidgets_Click(object sender, EventArgs e)
		{
			Point pt = gridControlWidgets.PointToClient(Control.MousePosition);

			if (layoutViewWidgets.CalcHitInfo(pt).RowHandle == layoutViewWidgets.FocusedRowHandle)
				layoutViewWidgets_FocusedRowChanged(null, null);
		}

		private void layoutViewWidgets_DoubleClick(object sender, EventArgs e)
		{
			Point pt = gridControlWidgets.PointToClient(Control.MousePosition);

			if (layoutViewWidgets.CalcHitInfo(pt).InField)
				btOK_Click(null, null);
		}
		#endregion

		#region Banner processing
		private void checkBoxEnableBanner_CheckedChanged(object sender, EventArgs e)
		{
			groupBoxBanners.Enabled = checkBoxEnableBanner.Checked;
			if (checkBoxEnableBanner.Checked)
				checkBoxEnableWidget.Checked = false;
		}

		private void gridViewBanners_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			ConfigurationClasses.Banner selectedBanner = null;
			if (gridViewBanners.FocusedRowHandle >= 0)
				selectedBanner = ConfigurationClasses.ListManager.Instance.Banners[gridViewBanners.GetDataSourceRowIndex(gridViewBanners.FocusedRowHandle)];
			pbSelectedBanner.Image = selectedBanner != null ? selectedBanner.Image : null;
		}

		private void gridViewBanners_Click(object sender, EventArgs e)
		{
			Point pt = gridControlBanners.PointToClient(Control.MousePosition);

			if (gridViewBanners.CalcHitInfo(pt).RowHandle == gridViewBanners.FocusedRowHandle)
				gridViewBanners_FocusedRowChanged(null, null);
		}

		private void gridViewBanners_DoubleClick(object sender, EventArgs e)
		{
			Point pt = gridControlBanners.PointToClient(Control.MousePosition);

			if (gridViewBanners.CalcHitInfo(pt).InRowCell)
				btOK_Click(null, null);
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
			memoEditBannerText.Font = buttonEditBannerTextFont.Tag as Font; ;
			memoEditBannerText.Properties.Appearance.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceDisabled.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceFocused.Font = memoEditBannerText.Font;
			memoEditBannerText.Properties.AppearanceReadOnly.Font = memoEditBannerText.Font;
		}
		#endregion

		#region Keyword processing
		private void UpdateKeywordsDataSource()
		{
			gridControlSearchTagsKeywords.DataSource = this.Keywords;
			gridViewSearchTagsKeywords.RefreshData();
		}

		private void SaveKeywordsDataSource()
		{
			gridViewSearchTagsKeywords.CloseEditor();
			this.Keywords.RemoveAll(x => string.IsNullOrEmpty(x.Value));
		}

		private void repositoryItemButtonEditKeyword_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			gridViewSearchTagsKeywords.CloseEditor();
			if (gridViewSearchTagsKeywords.FocusedRowHandle >= 0 && gridViewSearchTagsKeywords.FocusedRowHandle < gridViewSearchTagsKeywords.RowCount)
			{
				this.Keywords.RemoveAt(gridViewSearchTagsKeywords.GetDataSourceRowIndex(gridViewSearchTagsKeywords.FocusedRowHandle));
				gridViewSearchTagsKeywords.RefreshData();
			}
		}

		private void buttonXAddKeyWord_Click(object sender, EventArgs e)
		{
			SaveKeywordsDataSource();
			this.Keywords.Add(new StringDataSourceWrapper());
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
				this.FileCardImportantInfo.Clear();
				gridViewFileCardImportantInfo.RefreshData();
			}
		}

		private void buttonXFileCardImportantInfoAdd_Click(object sender, EventArgs e)
		{
			gridViewFileCardImportantInfo.CloseEditor();
			this.FileCardImportantInfo.RemoveAll(x => string.IsNullOrEmpty(x.Value));
			this.FileCardImportantInfo.Add(new StringDataSourceWrapper());
			gridViewFileCardImportantInfo.RefreshData();
			if (gridViewFileCardImportantInfo.RowCount > 0)
			{
				gridViewFileCardImportantInfo.FocusedRowHandle = gridViewFileCardImportantInfo.RowCount - 1;
				gridViewFileCardImportantInfo.MakeRowVisible(gridViewFileCardImportantInfo.FocusedRowHandle, true);
			}
		}

		private void repositoryItemButtonEditFileCardImportantInfo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			gridViewFileCardImportantInfo.CloseEditor();
			if (gridViewFileCardImportantInfo.FocusedRowHandle >= 0 && gridViewFileCardImportantInfo.FocusedRowHandle < gridViewFileCardImportantInfo.RowCount)
			{
				this.FileCardImportantInfo.RemoveAt(gridViewFileCardImportantInfo.GetDataSourceRowIndex(gridViewFileCardImportantInfo.FocusedRowHandle));
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
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Multiselect = true;
				dialog.Title = "Attach file";
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					foreach (string fileName in dialog.FileNames)
					{
						if (!this.AttachmentProperties.FilesAttachments.Any(x => x.OriginalPath.ToLower().Equals(fileName.ToLower())))
						{
							LinkAttachment attachment = new LinkAttachment(this.AttachmentProperties);
							attachment.Type = AttachmentType.File;
							attachment.OriginalPath = fileName;
							this.AttachmentProperties.FilesAttachments.Add(attachment);
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

		private void repositoryItemButtonEditAttachmentsFiles_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			gridViewAttachmentsFiles.CloseEditor();
			if (gridViewAttachmentsFiles.FocusedRowHandle >= 0 && gridViewAttachmentsFiles.FocusedRowHandle < gridViewAttachmentsFiles.RowCount)
			{
				if (e.Button.Index == 0)
				{
					LinkAttachment attachment = this.AttachmentProperties.FilesAttachments[gridViewAttachmentsFiles.GetDataSourceRowIndex(gridViewAttachmentsFiles.FocusedRowHandle)];
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
					this.AttachmentProperties.FilesAttachments.RemoveAt(gridViewAttachmentsFiles.GetDataSourceRowIndex(gridViewAttachmentsFiles.FocusedRowHandle));
					gridViewAttachmentsFiles.RefreshData();
				}
			}
		}

		private void gridViewAttachmentsFiles_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			int attachmentIndex = gridViewAttachmentsFiles.GetDataSourceRowIndex(e.RowHandle);
			if (attachmentIndex >= 0 && attachmentIndex < this.AttachmentProperties.FilesAttachments.Count)
			{
				LinkAttachment attachment = this.AttachmentProperties.FilesAttachments[attachmentIndex];
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
			this.AttachmentProperties.WebAttachments.RemoveAll(x => string.IsNullOrEmpty(x.OriginalPath));
			LinkAttachment attachment = new LinkAttachment(this.AttachmentProperties);
			attachment.Type = AttachmentType.Url;
			this.AttachmentProperties.WebAttachments.Add(attachment);
			gridViewAttachmentsWeb.RefreshData();
			if (gridViewAttachmentsWeb.RowCount > 0)
			{
				gridViewAttachmentsWeb.FocusedRowHandle = gridViewAttachmentsWeb.RowCount - 1;
				gridViewAttachmentsWeb.MakeRowVisible(gridViewAttachmentsWeb.FocusedRowHandle, true);
			}
		}

		private void repositoryItemButtonEditAttachmentsWeb_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			gridViewAttachmentsWeb.CloseEditor();
			if (gridViewAttachmentsWeb.FocusedRowHandle >= 0 && gridViewAttachmentsWeb.FocusedRowHandle < gridViewAttachmentsWeb.RowCount)
			{
				if (e.Button.Index == 0)
				{
					LinkAttachment attachment = this.AttachmentProperties.WebAttachments[gridViewAttachmentsWeb.GetDataSourceRowIndex(gridViewAttachmentsWeb.FocusedRowHandle)];
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
					this.AttachmentProperties.WebAttachments.RemoveAt(gridViewAttachmentsWeb.GetDataSourceRowIndex(gridViewAttachmentsWeb.FocusedRowHandle));
					gridViewAttachmentsWeb.RefreshData();
				}
			}
		}
		#endregion
		#endregion

		#region Shared methods
		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			xtraTabControl.Focus();
		}

		private void toolTipController_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl == gridControlWidgets)
			{
				DevExpress.Utils.ToolTipControlInfo info = null;
				try
				{
					DevExpress.XtraGrid.Views.Layout.LayoutView view = gridControlWidgets.GetViewAt(e.ControlMousePosition) as DevExpress.XtraGrid.Views.Layout.LayoutView;
					if (view == null)
						return;
					DevExpress.XtraGrid.Views.Layout.ViewInfo.LayoutViewHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
					if (hi.InFieldValue)
						info = new DevExpress.Utils.ToolTipControlInfo(new DevExpress.XtraGrid.Views.Base.CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), ConfigurationClasses.ListManager.Instance.Widgets[layoutViewWidgets.GetDataSourceRowIndex(hi.RowHandle)].FileName);
				}
				finally
				{
					e.Info = info;
				}
			}
			else if (e.SelectedControl == gridControlBanners)
			{
				DevExpress.Utils.ToolTipControlInfo info = null;
				try
				{
					DevExpress.XtraGrid.Views.Grid.GridView view = gridControlBanners.GetViewAt(e.ControlMousePosition) as DevExpress.XtraGrid.Views.Grid.GridView;
					if (view == null)
						return;
					DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
					if (hi.InRowCell)
						info = new DevExpress.Utils.ToolTipControlInfo(new DevExpress.XtraGrid.Views.Base.CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), ConfigurationClasses.ListManager.Instance.Widgets[gridViewBanners.GetDataSourceRowIndex(hi.RowHandle)].FileName);
				}
				finally
				{
					e.Info = info;
				}
			}
		}

		private void FontEdit_Click(object sender, EventArgs e)
		{
			DevExpress.XtraEditors.ButtonEdit fontEdit = sender as DevExpress.XtraEditors.ButtonEdit;
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

		private void FontEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			FontEdit_Click(this, null);
		}
		#endregion
	}
}
