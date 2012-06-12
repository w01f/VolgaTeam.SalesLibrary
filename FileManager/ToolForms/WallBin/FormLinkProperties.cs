using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.ToolForms.WallBin
{
    public partial class FormLinkProperties : Form
    {
        private string _note = string.Empty;
        private bool _isBold = false;
        private bool _closeEventAssigned = false;

        public bool IsLineBreak { get; set; }

        public DateTime AddDate { get; set; }
        public BusinessClasses.LibraryFileSearchTags SearchTags { get; set; }
        public BusinessClasses.ExpirationDateOptions ExpirationDateOptions { get; set; }
        public BusinessClasses.LineBreakProperties LineBreakProperties { get; set; }
        public BusinessClasses.BannerProperties BannerProperties { get; set; }
        public bool EnableWidget { get; set; }
        public Image Widget { get; set; }


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
            }

            if (!this.IsLineBreak)
            {
                this.SearchTags = new BusinessClasses.LibraryFileSearchTags();
                this.ExpirationDateOptions = new BusinessClasses.ExpirationDateOptions();
                dateEditExpirationDate.Properties.NullDate = DateTime.MinValue;

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

        private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
        {
            xtraTabControl.Focus();
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

                this.SearchTags.SearchGroups.Clear();

                if (checkedListBoxControlGroup1.CheckedItemsCount > 0)
                {
                    ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                    group.Name = navBarGroup1.Caption;
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup1.Items)
                        if (item.CheckState == CheckState.Checked)
                            group.Tags.Add(item.Value.ToString());
                    this.SearchTags.SearchGroups.Add(group);
                }
                if (checkedListBoxControlGroup2.CheckedItemsCount > 0)
                {
                    ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                    group.Name = navBarGroup2.Caption;
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup2.Items)
                        if (item.CheckState == CheckState.Checked)
                            group.Tags.Add(item.Value.ToString());
                    this.SearchTags.SearchGroups.Add(group);
                }
                if (checkedListBoxControlGroup3.CheckedItemsCount > 0)
                {
                    ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                    group.Name = navBarGroup3.Caption;
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup3.Items)
                        if (item.CheckState == CheckState.Checked)
                            group.Tags.Add(item.Value.ToString());
                    this.SearchTags.SearchGroups.Add(group);
                }
                if (checkedListBoxControlGroup4.CheckedItemsCount > 0)
                {
                    ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                    group.Name = navBarGroup4.Caption;
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup4.Items)
                        if (item.CheckState == CheckState.Checked)
                            group.Tags.Add(item.Value.ToString());
                    this.SearchTags.SearchGroups.Add(group);
                }
                if (checkedListBoxControlGroup5.CheckedItemsCount > 0)
                {
                    ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                    group.Name = navBarGroup5.Caption;
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup5.Items)
                        if (item.CheckState == CheckState.Checked)
                            group.Tags.Add(item.Value.ToString());
                    this.SearchTags.SearchGroups.Add(group);
                }
                if (checkedListBoxControlGroup6.CheckedItemsCount > 0)
                {
                    ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                    group.Name = navBarGroup6.Caption;
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup6.Items)
                        if (item.CheckState == CheckState.Checked)
                            group.Tags.Add(item.Value.ToString());
                    this.SearchTags.SearchGroups.Add(group);
                }
                if (checkedListBoxControlGroup7.CheckedItemsCount > 0)
                {
                    ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                    group.Name = navBarGroup7.Caption;
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup7.Items)
                        if (item.CheckState == CheckState.Checked)
                            group.Tags.Add(item.Value.ToString());
                    this.SearchTags.SearchGroups.Add(group);
                }

                this.ExpirationDateOptions.ExpirationDate = new DateTime(dateEditExpirationDate.DateTime.Year, dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour, timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second);
                this.ExpirationDateOptions.SendEmailWhenSync = checkBoxSendEmailWhenDelete.Checked;
                this.ExpirationDateOptions.LabelLinkWhenExpired = checkBoxLabelLink.Checked;
                this.ExpirationDateOptions.EnableExpirationDate = checkBoxEnableExpiredLinks.Checked;
            }
            else
            {
                this.LineBreakProperties.Font = buttonEditLineBreakFont.Tag as Font;
                this.LineBreakProperties.ForeColor = colorEditLineBreakFontColor.Color;
                this.LineBreakProperties.Note = memoEditNote.EditValue != null ? memoEditNote.EditValue.ToString().Trim() : string.Empty;
            }

            this.EnableWidget = checkBoxEnableWidget.Checked;
            this.Widget = pbSelectedWidget.Image;

            this.BannerProperties.Enable = checkBoxEnableBanner.Checked;
            this.BannerProperties.Image = pbSelectedBanner.Image;
            if (rbBannerAligmentLeft.Checked)
                this.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Left;
            else if (rbBannerAligmentCenter.Checked)
                this.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Center;
            else if (rbBannerAligmentRight.Checked)
                this.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Right;
            this.BannerProperties.ShowText = checkBoxBannerShowText.Checked;
            this.BannerProperties.Text = memoEditBannerText.EditValue != null ? memoEditBannerText.EditValue.ToString() : string.Empty;
            this.BannerProperties.Font = buttonEditBannerTextFont.Tag as Font;
            this.BannerProperties.ForeColor = colorEditBannerTextColor.Color;
            this.BannerProperties.Configured = true;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void rbNew_CheckedChanged(object sender, EventArgs e)
        {
            edCustomNote.Enabled = rbCustomNote.Checked;
        }

        private void FormProperties_Load(object sender, EventArgs e)
        {
            xtraTabPageExpiredLinks.PageVisible = !this.IsLineBreak;
            xtraTabPageNotes.PageVisible = !this.IsLineBreak;
            xtraTabPageSearchTags.PageVisible = !this.IsLineBreak;
            xtraTabPageLineBrealProperties.PageVisible = this.IsLineBreak;
            if (!this.IsLineBreak)
            {
                if (navBarGroup1.Visible)
                {
                    ConfigurationClasses.SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup1.Caption)).FirstOrDefault();
                    if (group != null)
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup1.Items)
                            if (group.Tags.Contains(item.Value.ToString()))
                                item.CheckState = CheckState.Checked;
                }
                if (navBarGroup2.Visible)
                {
                    ConfigurationClasses.SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup2.Caption)).FirstOrDefault();
                    if (group != null)
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup2.Items)
                            if (group.Tags.Contains(item.Value.ToString()))
                                item.CheckState = CheckState.Checked;
                }
                if (navBarGroup3.Visible)
                {
                    ConfigurationClasses.SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup3.Caption)).FirstOrDefault();
                    if (group != null)
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup3.Items)
                            if (group.Tags.Contains(item.Value.ToString()))
                                item.CheckState = CheckState.Checked;
                }
                if (navBarGroup4.Visible)
                {
                    ConfigurationClasses.SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup4.Caption)).FirstOrDefault();
                    if (group != null)
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup4.Items)
                            if (group.Tags.Contains(item.Value.ToString()))
                                item.CheckState = CheckState.Checked;
                }
                if (navBarGroup5.Visible)
                {
                    ConfigurationClasses.SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup5.Caption)).FirstOrDefault();
                    if (group != null)
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup5.Items)
                            if (group.Tags.Contains(item.Value.ToString()))
                                item.CheckState = CheckState.Checked;
                }
                if (navBarGroup6.Visible)
                {
                    ConfigurationClasses.SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup6.Caption)).FirstOrDefault();
                    if (group != null)
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup6.Items)
                            if (group.Tags.Contains(item.Value.ToString()))
                                item.CheckState = CheckState.Checked;
                }
                if (navBarGroup7.Visible)
                {
                    ConfigurationClasses.SearchGroup group = this.SearchTags.SearchGroups.Where(x => x.Name.Equals(navBarGroup7.Caption)).FirstOrDefault();
                    if (group != null)
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup7.Items)
                            if (group.Tags.Contains(item.Value.ToString()))
                                item.CheckState = CheckState.Checked;
                }

                laAddDateValue.Text = this.AddDate.ToString("M/dd/yyyy h:mm:ss tt");
                dateEditExpirationDate.DateTime = this.ExpirationDateOptions.ExpirationDate;
                timeEditExpirationTime.Time = this.ExpirationDateOptions.ExpirationDate;
                checkBoxSendEmailWhenDelete.Checked = this.ExpirationDateOptions.SendEmailWhenSync;
                checkBoxLabelLink.Checked = this.ExpirationDateOptions.LabelLinkWhenExpired;
                checkBoxEnableExpiredLinks.Checked = this.ExpirationDateOptions.EnableExpirationDate;
            }
            else
            {
                buttonEditLineBreakFont.Tag = this.LineBreakProperties.Font;
                buttonEditLineBreakFont.EditValue = FontToString(this.LineBreakProperties.Font);
                colorEditLineBreakFontColor.Color = this.LineBreakProperties.ForeColor;
                memoEditNote.EditValue = this.LineBreakProperties.Note;
            }
            pbSelectedWidget.Image = this.EnableWidget ? this.Widget : null;
            checkBoxEnableWidget.Checked = this.EnableWidget;

            xtraTabPageBanner.PageEnabled = System.IO.Directory.Exists(ConfigurationClasses.ListManager.Instance.BannerFolder);
            checkBoxEnableBanner.Checked = this.BannerProperties.Enable;
            pbSelectedBanner.Image = this.BannerProperties.Enable ? this.BannerProperties.Image : null;
            switch (this.BannerProperties.ImageAlignement)
            {
                case BusinessClasses.Alignment.Left:
                    rbBannerAligmentLeft.Checked = true;
                    rbBannerAligmentCenter.Checked = false;
                    rbBannerAligmentRight.Checked = false;
                    break;
                case BusinessClasses.Alignment.Center:
                    rbBannerAligmentLeft.Checked = false;
                    rbBannerAligmentCenter.Checked = true;
                    rbBannerAligmentRight.Checked = false;
                    break;
                case BusinessClasses.Alignment.Right:
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

            if (!_closeEventAssigned)
            {
                AssignCloseActiveEditorsonOutSideClick(this);
                _closeEventAssigned = true;
            }
        }

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

        private void checkBoxEnableWidget_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxWidgets.Enabled = checkBoxEnableWidget.Checked;
            if (checkBoxEnableWidget.Checked)
                checkBoxEnableBanner.Checked = false;
        }

        private void checkBoxEnableBanner_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxBanners.Enabled = checkBoxEnableBanner.Checked;
            if (checkBoxEnableBanner.Checked)
                checkBoxEnableWidget.Checked = false;
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
    }
}
