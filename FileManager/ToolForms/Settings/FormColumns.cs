using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormColumns : Form
    {
        public BusinessClasses.Library Library { get; set; }
        private BusinessClasses.LibraryPage _currentPage;
        private BusinessClasses.LibraryFolder _currentFolder;
        private bool _stateChanges = false;
        private bool _changesDone = false;
        private bool _allowToSave = false;

        bool _column1FirstSelect = true;
        bool _column2FirstSelect = true;
        bool _column3FirstSelect = true;

        public FormColumns()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laWindowHeaderBackColor.Font = new System.Drawing.Font(laWindowHeaderBackColor.Font.FontFamily, laWindowHeaderBackColor.Font.Size - 2, laWindowHeaderBackColor.Font.Style);
                laWindowHeaderFont.Font = new System.Drawing.Font(laWindowHeaderFont.Font.FontFamily, laWindowHeaderFont.Font.Size - 2, laWindowHeaderFont.Font.Style);
                laWindowHeaderForeColor.Font = new System.Drawing.Font(laWindowHeaderForeColor.Font.FontFamily, laWindowHeaderForeColor.Font.Size - 2, laWindowHeaderForeColor.Font.Style);
                laLocationHeader.Font = new System.Drawing.Font(laLocationHeader.Font.FontFamily, laLocationHeader.Font.Size - 2, laLocationHeader.Font.Style);
                laLocationValue.Font = new System.Drawing.Font(laLocationValue.Font.FontFamily, laLocationValue.Font.Size - 2, laLocationValue.Font.Style);
                laWindowBorderColor.Font = new System.Drawing.Font(laWindowBorderColor.Font.FontFamily, laWindowBorderColor.Font.Size - 2, laWindowBorderColor.Font.Style);
                laPages.Font = new System.Drawing.Font(laPages.Font.FontFamily, laPages.Font.Size - 2, laPages.Font.Style);
                laWindow.Font = new System.Drawing.Font(laWindow.Font.FontFamily, laWindow.Font.Size - 2, laWindow.Font.Style);
                laWindowBackColor.Font = new System.Drawing.Font(laWindowBackColor.Font.FontFamily, laWindowBackColor.Font.Size - 2, laWindowBackColor.Font.Style);
                laWindowForeColor.Font = new System.Drawing.Font(laWindowForeColor.Font.FontFamily, laWindowForeColor.Font.Size - 2, laWindowForeColor.Font.Style);
                laWindowHeaderAlignment.Font = new System.Drawing.Font(laWindowHeaderAlignment.Font.FontFamily, laWindowHeaderAlignment.Font.Size - 2, laWindowHeaderAlignment.Font.Style);
                laWindowAvailableBanners.Font = new System.Drawing.Font(laWindowAvailableBanners.Font.FontFamily, laWindowAvailableBanners.Font.Size - 2, laWindowAvailableBanners.Font.Style);
                laWindowBannerAligment.Font = new System.Drawing.Font(laWindowBannerAligment.Font.FontFamily, laWindowBannerAligment.Font.Size - 2, laWindowBannerAligment.Font.Style);
                laWindowSelectedBanner.Font = new System.Drawing.Font(laWindowSelectedBanner.Font.FontFamily, laWindowSelectedBanner.Font.Size - 2, laWindowSelectedBanner.Font.Style);
                laWindowAvailableWidgets.Font = new System.Drawing.Font(laWindowAvailableWidgets.Font.FontFamily, laWindowAvailableWidgets.Font.Size - 2, laWindowAvailableWidgets.Font.Style);
                laWindowSelectedWidget.Font = new System.Drawing.Font(laWindowSelectedWidget.Font.FontFamily, laWindowSelectedWidget.Font.Size - 2, laWindowSelectedWidget.Font.Style);
                ckApllyForAllWindowsAppearance.Font = new System.Drawing.Font(ckApllyForAllWindowsAppearance.Font.FontFamily, ckApllyForAllWindowsAppearance.Font.Size - 2, ckApllyForAllWindowsAppearance.Font.Style);
                ckApplyForAllColumnTitles.Font = new System.Drawing.Font(ckApplyForAllColumnTitles.Font.FontFamily, ckApplyForAllColumnTitles.Font.Size - 2, ckApplyForAllColumnTitles.Font.Style);
                ckApllyForAllWindowsBanner.Font = new System.Drawing.Font(ckApllyForAllWindowsBanner.Font.FontFamily, ckApllyForAllWindowsBanner.Font.Size - 2, ckApllyForAllWindowsBanner.Font.Style);
                ckWindowEnableBanner.Font = new System.Drawing.Font(ckWindowEnableBanner.Font.FontFamily, ckWindowEnableBanner.Font.Size - 2, ckWindowEnableBanner.Font.Style);
                ckWindowBannerShowText.Font = new System.Drawing.Font(ckWindowBannerShowText.Font.FontFamily, ckWindowBannerShowText.Font.Size - 2, ckWindowBannerShowText.Font.Style);
                ckApllyForAllWindowsWidget.Font = new System.Drawing.Font(ckApllyForAllWindowsWidget.Font.FontFamily, ckApllyForAllWindowsWidget.Font.Size - 2, ckApllyForAllWindowsWidget.Font.Style);
                ckWindowEnableWidget.Font = new System.Drawing.Font(ckWindowEnableWidget.Font.FontFamily, ckWindowEnableWidget.Font.Size - 2, ckWindowEnableWidget.Font.Style);
                rbWindowHeaderAlignmentCenter.Font = new System.Drawing.Font(rbWindowHeaderAlignmentCenter.Font.FontFamily, rbWindowHeaderAlignmentCenter.Font.Size - 2, rbWindowHeaderAlignmentCenter.Font.Style);
                rbWindowHeaderAlignmentLeft.Font = new System.Drawing.Font(rbWindowHeaderAlignmentLeft.Font.FontFamily, rbWindowHeaderAlignmentLeft.Font.Size - 2, rbWindowHeaderAlignmentLeft.Font.Style);
                rbWindowHeaderAlignmentRight.Font = new System.Drawing.Font(rbWindowHeaderAlignmentRight.Font.FontFamily, rbWindowHeaderAlignmentRight.Font.Size - 2, rbWindowHeaderAlignmentRight.Font.Style);
                rbWindowBannerAlignmentCenter.Font = new System.Drawing.Font(rbWindowBannerAlignmentCenter.Font.FontFamily, rbWindowBannerAlignmentCenter.Font.Size - 2, rbWindowBannerAlignmentCenter.Font.Style);
                rbWindowBannerAlignmentLeft.Font = new System.Drawing.Font(rbWindowBannerAlignmentLeft.Font.FontFamily, rbWindowBannerAlignmentLeft.Font.Size - 2, rbWindowBannerAlignmentLeft.Font.Style);
                rbWindowBannerAlignmentRight.Font = new System.Drawing.Font(rbWindowBannerAlignmentRight.Font.FontFamily, rbWindowBannerAlignmentRight.Font.Size - 2, rbWindowBannerAlignmentRight.Font.Style);
                ckEnableColumnTitles.Font = new System.Drawing.Font(ckEnableColumnTitles.Font.FontFamily, ckEnableColumnTitles.Font.Size - 2, ckEnableColumnTitles.Font.Style);
                xtraTabControlWindowProperties.Appearance.Font = new System.Drawing.Font(xtraTabControlWindowProperties.Appearance.Font.FontFamily, xtraTabControlWindowProperties.Appearance.Font.Size - 2, xtraTabControlWindowProperties.Appearance.Font.Style);
                xtraTabControlWindowProperties.AppearancePage.Header.Font = new System.Drawing.Font(xtraTabControlWindowProperties.AppearancePage.Header.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.Header.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.Header.Font.Style);
                xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font = new System.Drawing.Font(xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderActive.Font.Style);
                xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font(xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderDisabled.Font.Style);
                xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font(xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControlWindowProperties.AppearancePage.HeaderHotTracked.Font.Style);
                xtraTabControlColumnTitles.Appearance.Font = new System.Drawing.Font(xtraTabControlColumnTitles.Appearance.Font.FontFamily, xtraTabControlColumnTitles.Appearance.Font.Size - 2, xtraTabControlColumnTitles.Appearance.Font.Style);
                xtraTabControlColumnTitles.AppearancePage.Header.Font = new System.Drawing.Font(xtraTabControlColumnTitles.AppearancePage.Header.Font.FontFamily, xtraTabControlColumnTitles.AppearancePage.Header.Font.Size - 2, xtraTabControlColumnTitles.AppearancePage.Header.Font.Style);
                xtraTabControlColumnTitles.AppearancePage.HeaderActive.Font = new System.Drawing.Font(xtraTabControlColumnTitles.AppearancePage.HeaderActive.Font.FontFamily, xtraTabControlColumnTitles.AppearancePage.HeaderActive.Font.Size - 2, xtraTabControlColumnTitles.AppearancePage.HeaderActive.Font.Style);
                xtraTabControlColumnTitles.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font(xtraTabControlColumnTitles.AppearancePage.HeaderDisabled.Font.FontFamily, xtraTabControlColumnTitles.AppearancePage.HeaderDisabled.Font.Size - 2, xtraTabControlColumnTitles.AppearancePage.HeaderDisabled.Font.Style);
                xtraTabControlColumnTitles.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font(xtraTabControlColumnTitles.AppearancePage.HeaderHotTracked.Font.FontFamily, xtraTabControlColumnTitles.AppearancePage.HeaderHotTracked.Font.Size - 2, xtraTabControlColumnTitles.AppearancePage.HeaderHotTracked.Font.Style);

                laColumn1BackColor.Font = new System.Drawing.Font(laColumn1BackColor.Font.FontFamily, laColumn1BackColor.Font.Size - 2, laColumn1BackColor.Font.Style);
                laColumn1ForeColor.Font = new System.Drawing.Font(laColumn1ForeColor.Font.FontFamily, laColumn1ForeColor.Font.Size - 2, laColumn1ForeColor.Font.Style);
                laColumn1Font.Font = new System.Drawing.Font(laColumn1Font.Font.FontFamily, laColumn1Font.Font.Size - 2, laColumn1Font.Font.Style);
                laColumn1Alignment.Font = new System.Drawing.Font(laColumn1Alignment.Font.FontFamily, laColumn1Alignment.Font.Size - 2, laColumn1Alignment.Font.Style);
                ckColumn1EnableText.Font = new System.Drawing.Font(ckColumn1EnableText.Font.FontFamily, ckColumn1EnableText.Font.Size - 2, ckColumn1EnableText.Font.Style);
                ckColumn1EnableBanner.Font = new System.Drawing.Font(ckColumn1EnableBanner.Font.FontFamily, ckColumn1EnableBanner.Font.Size - 2, ckColumn1EnableBanner.Font.Style);
                ckColumn1EnableWidget.Font = new System.Drawing.Font(ckColumn1EnableWidget.Font.FontFamily, ckColumn1EnableWidget.Font.Size - 2, ckColumn1EnableWidget.Font.Style);
                rbColumn1AlignmentCenter.Font = new System.Drawing.Font(rbColumn1AlignmentCenter.Font.FontFamily, rbColumn1AlignmentCenter.Font.Size - 2, rbColumn1AlignmentCenter.Font.Style);
                rbColumn1AlignmentLeft.Font = new System.Drawing.Font(rbColumn1AlignmentLeft.Font.FontFamily, rbColumn1AlignmentLeft.Font.Size - 2, rbColumn1AlignmentLeft.Font.Style);
                rbColumn1AlignmentRight.Font = new System.Drawing.Font(rbColumn1AlignmentRight.Font.FontFamily, rbColumn1AlignmentRight.Font.Size - 2, rbColumn1AlignmentRight.Font.Style);

                laColumn2BackColor.Font = new System.Drawing.Font(laColumn2BackColor.Font.FontFamily, laColumn2BackColor.Font.Size - 2, laColumn2BackColor.Font.Style);
                laColumn2ForeColor.Font = new System.Drawing.Font(laColumn2ForeColor.Font.FontFamily, laColumn2ForeColor.Font.Size - 2, laColumn2ForeColor.Font.Style);
                laColumn2Font.Font = new System.Drawing.Font(laColumn2Font.Font.FontFamily, laColumn2Font.Font.Size - 2, laColumn2Font.Font.Style);
                laColumn2Alignment.Font = new System.Drawing.Font(laColumn2Alignment.Font.FontFamily, laColumn2Alignment.Font.Size - 2, laColumn2Alignment.Font.Style);
                ckColumn2EnableBanner.Font = new System.Drawing.Font(ckColumn2EnableBanner.Font.FontFamily, ckColumn2EnableBanner.Font.Size - 2, ckColumn2EnableBanner.Font.Style);
                ckColumn2EnableWidget.Font = new System.Drawing.Font(ckColumn2EnableWidget.Font.FontFamily, ckColumn2EnableWidget.Font.Size - 2, ckColumn2EnableWidget.Font.Style);
                ckColumn2EnableText.Font = new System.Drawing.Font(ckColumn2EnableText.Font.FontFamily, ckColumn2EnableText.Font.Size - 2, ckColumn2EnableText.Font.Style);
                rbColumn2AlignmentCenter.Font = new System.Drawing.Font(rbColumn2AlignmentCenter.Font.FontFamily, rbColumn2AlignmentCenter.Font.Size - 2, rbColumn2AlignmentCenter.Font.Style);
                rbColumn2AlignmentLeft.Font = new System.Drawing.Font(rbColumn2AlignmentLeft.Font.FontFamily, rbColumn2AlignmentLeft.Font.Size - 2, rbColumn2AlignmentLeft.Font.Style);
                rbColumn2AlignmentRight.Font = new System.Drawing.Font(rbColumn2AlignmentRight.Font.FontFamily, rbColumn2AlignmentRight.Font.Size - 2, rbColumn2AlignmentRight.Font.Style);

                laColumn3BackColor.Font = new System.Drawing.Font(laColumn3BackColor.Font.FontFamily, laColumn3BackColor.Font.Size - 2, laColumn3BackColor.Font.Style);
                laColumn3ForeColor.Font = new System.Drawing.Font(laColumn3ForeColor.Font.FontFamily, laColumn3ForeColor.Font.Size - 2, laColumn3ForeColor.Font.Style);
                laColumn3Font.Font = new System.Drawing.Font(laColumn3Font.Font.FontFamily, laColumn3Font.Font.Size - 2, laColumn3Font.Font.Style);
                laColumn3Alignment.Font = new System.Drawing.Font(laColumn3Alignment.Font.FontFamily, laColumn3Alignment.Font.Size - 2, laColumn3Alignment.Font.Style);
                ckColumn3EnableBanner.Font = new System.Drawing.Font(ckColumn3EnableBanner.Font.FontFamily, ckColumn3EnableBanner.Font.Size - 2, ckColumn3EnableBanner.Font.Style);
                ckColumn3EnableWidget.Font = new System.Drawing.Font(ckColumn3EnableWidget.Font.FontFamily, ckColumn3EnableWidget.Font.Size - 2, ckColumn3EnableWidget.Font.Style);
                ckColumn3EnableText.Font = new System.Drawing.Font(ckColumn3EnableText.Font.FontFamily, ckColumn3EnableText.Font.Size - 2, ckColumn3EnableText.Font.Style);
                rbColumn3AlignmentCenter.Font = new System.Drawing.Font(rbColumn3AlignmentCenter.Font.FontFamily, rbColumn3AlignmentCenter.Font.Size - 2, rbColumn3AlignmentCenter.Font.Style);
                rbColumn3AlignmentLeft.Font = new System.Drawing.Font(rbColumn3AlignmentLeft.Font.FontFamily, rbColumn3AlignmentLeft.Font.Size - 2, rbColumn3AlignmentLeft.Font.Style);
                rbColumn3AlignmentRight.Font = new System.Drawing.Font(rbColumn3AlignmentRight.Font.FontFamily, rbColumn3AlignmentRight.Font.Size - 2, rbColumn3AlignmentRight.Font.Style);
            }
            AssignCloseActiveEditorsonOutSideClick(this);
        }

        #region Base Methods
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
                && controlType != typeof(DevExpress.XtraEditors.MemoEdit))
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
            laPages.Focus();
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

        private void GetColumnSettings()
        {
            _allowToSave = false;
            grColumn1.Rows.Clear();
            grColumn2.Rows.Clear();
            grColumn3.Rows.Clear();

            if (_currentPage != null)
            {
                foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders.OrderBy(x => x.RowOrder))
                {
                    switch (folder.ColumnOrder)
                    {
                        case 0:
                            grColumn1.Rows.Add(folder.Name, folder.Identifier);
                            break;
                        case 1:
                            grColumn2.Rows.Add(folder.Name, folder.Identifier);
                            break;
                        case 2:
                            grColumn3.Rows.Add(folder.Name, folder.Identifier);
                            break;
                    }
                }
                grColumn1.ClearSelection();
                grColumn2.ClearSelection();
                grColumn3.ClearSelection();
            }
            _allowToSave = true;
        }

        private void SaveColumnSettings()
        {
            if (_currentPage != null)
            {
                List<Guid> existedFolderIDs = new List<Guid>();
                BusinessClasses.LibraryFolder templateFolder = null;

                if (this.Library.ApplyAppearanceForAllWindows)
                    templateFolder = _currentPage.Folders.FirstOrDefault();

                foreach (DataGridViewRow row in grColumn1.Rows)
                    existedFolderIDs.Add((Guid)row.Cells[1].Value);
                foreach (DataGridViewRow row in grColumn2.Rows)
                    existedFolderIDs.Add((Guid)row.Cells[1].Value);
                foreach (DataGridViewRow row in grColumn3.Rows)
                    existedFolderIDs.Add((Guid)row.Cells[1].Value);

                foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders.Where(x => !existedFolderIDs.Contains(x.Identifier)).ToArray())
                    folder.RemoveFromParent();

                foreach (DataGridViewRow row in grColumn1.Rows)
                {
                    BusinessClasses.LibraryFolder folder = _currentPage.Folders.Where(x => x.Identifier.Equals(row.Cells[1].Value)).FirstOrDefault();
                    if (folder == null)
                    {
                        folder = new BusinessClasses.LibraryFolder(_currentPage);
                        folder.Identifier = (Guid)row.Cells[1].Value;
                        _currentPage.Folders.Add(folder);
                    }
                    folder.Name = row.Cells[0].Value.ToString();
                    folder.ColumnOrder = 0;
                    folder.RowOrder = row.Index;
                    if (templateFolder != null)
                    {
                        folder.BackgroundHeaderColor = templateFolder.BackgroundHeaderColor;
                        folder.ForeHeaderColor = templateFolder.ForeHeaderColor;
                        folder.BackgroundWindowColor = templateFolder.BackgroundWindowColor;
                        folder.ForeWindowColor = templateFolder.ForeWindowColor;
                    }
                }

                foreach (DataGridViewRow row in grColumn2.Rows)
                {
                    BusinessClasses.LibraryFolder folder = _currentPage.Folders.Where(x => x.Identifier.Equals(row.Cells[1].Value)).FirstOrDefault();
                    if (folder == null)
                    {
                        folder = new BusinessClasses.LibraryFolder(_currentPage);
                        folder.Identifier = (Guid)row.Cells[1].Value;
                        _currentPage.Folders.Add(folder);
                    }
                    folder.Name = row.Cells[0].Value.ToString();
                    folder.ColumnOrder = 1;
                    folder.RowOrder = row.Index;
                    if (templateFolder != null)
                    {
                        folder.BackgroundHeaderColor = templateFolder.BackgroundHeaderColor;
                        folder.ForeHeaderColor = templateFolder.ForeHeaderColor;
                        folder.BackgroundWindowColor = templateFolder.BackgroundWindowColor;
                        folder.ForeWindowColor = templateFolder.ForeWindowColor;
                    }
                }

                foreach (DataGridViewRow row in grColumn3.Rows)
                {
                    BusinessClasses.LibraryFolder folder = _currentPage.Folders.Where(x => x.Identifier.Equals(row.Cells[1].Value)).FirstOrDefault();
                    if (folder == null)
                    {
                        folder = new BusinessClasses.LibraryFolder(_currentPage);
                        folder.Identifier = (Guid)row.Cells[1].Value;
                        _currentPage.Folders.Add(folder);
                    }
                    folder.Name = row.Cells[0].Value.ToString();
                    folder.ColumnOrder = 2;
                    folder.RowOrder = row.Index;
                    if (templateFolder != null)
                    {
                        folder.BackgroundHeaderColor = templateFolder.BackgroundHeaderColor;
                        folder.ForeHeaderColor = templateFolder.ForeHeaderColor;
                        folder.BackgroundWindowColor = templateFolder.BackgroundWindowColor;
                        folder.ForeWindowColor = templateFolder.ForeWindowColor;
                    }
                }
            }
        }

        private void GetWindowSettings()
        {
            if (_currentFolder != null)
            {
                pnWindows.Enabled = true;
                laLocationValue.Text = "Window " + (_currentFolder.RowOrder + 1).ToString() + " - Column " + (_currentFolder.ColumnOrder + 1).ToString();
                _allowToSave = false;
                BusinessClasses.LibraryFolder currentPageFolder = _currentPage.Folders.FirstOrDefault();
                if (ckApllyForAllWindowsAppearance.Checked)
                {
                    if (currentPageFolder != null)
                    {
                        colorEditWindowHeaderBackColor.Color = currentPageFolder.BackgroundHeaderColor;
                        colorEditWindowHeaderForeColor.Color = currentPageFolder.ForeHeaderColor;
                        colorEditWindowBackColor.Color = currentPageFolder.BackgroundWindowColor;
                        colorEditWindowForeColor.Color = currentPageFolder.ForeWindowColor;
                        colorEditWindowBorderColor.Color = currentPageFolder.BorderColor;
                        if (currentPageFolder.HeaderFont != null)
                        {
                            buttonEditWindowHeaderFont.Tag = currentPageFolder.HeaderFont;
                            buttonEditWindowHeaderFont.EditValue = FontToString(currentPageFolder.HeaderFont);
                        }
                        else
                            buttonEditWindowHeaderFont.EditValue = string.Empty;
                        switch (currentPageFolder.HeaderAlignment)
                        {
                            case BusinessClasses.Alignment.Left:
                                rbWindowHeaderAlignmentLeft.Checked = true;
                                rbWindowHeaderAlignmentCenter.Checked = false;
                                rbWindowHeaderAlignmentRight.Checked = false;
                                break;
                            case BusinessClasses.Alignment.Center:
                                rbWindowHeaderAlignmentLeft.Checked = false;
                                rbWindowHeaderAlignmentCenter.Checked = true;
                                rbWindowHeaderAlignmentRight.Checked = false;
                                break;
                            case BusinessClasses.Alignment.Right:
                                rbWindowHeaderAlignmentLeft.Checked = false;
                                rbWindowHeaderAlignmentCenter.Checked = false;
                                rbWindowHeaderAlignmentRight.Checked = true;
                                break;
                        }
                    }
                }
                else
                {
                    colorEditWindowHeaderBackColor.Color = _currentFolder.BackgroundHeaderColor;
                    colorEditWindowHeaderForeColor.Color = _currentFolder.ForeHeaderColor;
                    colorEditWindowBackColor.Color = _currentFolder.BackgroundWindowColor;
                    colorEditWindowForeColor.Color = _currentFolder.ForeWindowColor;
                    colorEditWindowBorderColor.Color = _currentFolder.BorderColor;
                    if (_currentFolder.HeaderFont != null)
                    {
                        buttonEditWindowHeaderFont.Tag = _currentFolder.HeaderFont;
                        buttonEditWindowHeaderFont.EditValue = FontToString(_currentFolder.HeaderFont);
                    }
                    else
                        buttonEditWindowHeaderFont.EditValue = string.Empty;
                    switch (_currentFolder.HeaderAlignment)
                    {
                        case BusinessClasses.Alignment.Left:
                            rbWindowHeaderAlignmentLeft.Checked = true;
                            rbWindowHeaderAlignmentCenter.Checked = false;
                            rbWindowHeaderAlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Center:
                            rbWindowHeaderAlignmentLeft.Checked = false;
                            rbWindowHeaderAlignmentCenter.Checked = true;
                            rbWindowHeaderAlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Right:
                            rbWindowHeaderAlignmentLeft.Checked = false;
                            rbWindowHeaderAlignmentCenter.Checked = false;
                            rbWindowHeaderAlignmentRight.Checked = true;
                            break;
                    }
                }

                if (ckApllyForAllWindowsWidget.Checked)
                {
                    if (currentPageFolder != null)
                    {
                        pbWindowSelectedWidget.Image = currentPageFolder.EnableWidget ? currentPageFolder.Widget : null;
                        ckWindowEnableWidget.Checked = currentPageFolder.EnableWidget;
                    }
                }
                else
                {
                    pbWindowSelectedWidget.Image = _currentFolder.EnableWidget ? _currentFolder.Widget : null;
                    ckWindowEnableWidget.Checked = _currentFolder.EnableWidget;
                }

                if (ckApllyForAllWindowsBanner.Checked)
                {
                    if (currentPageFolder != null)
                    {
                        ckWindowEnableBanner.Checked = currentPageFolder.BannerProperties.Enable;
                        pbWindowSelectedBanner.Image = currentPageFolder.BannerProperties.Enable ? currentPageFolder.BannerProperties.Image : null;
                        switch (currentPageFolder.BannerProperties.ImageAlignement)
                        {
                            case BusinessClasses.Alignment.Left:
                                rbWindowBannerAlignmentLeft.Checked = true;
                                rbWindowBannerAlignmentCenter.Checked = false;
                                rbWindowBannerAlignmentRight.Checked = false;
                                break;
                            case BusinessClasses.Alignment.Center:
                                rbWindowBannerAlignmentLeft.Checked = false;
                                rbWindowBannerAlignmentCenter.Checked = true;
                                rbWindowBannerAlignmentRight.Checked = false;
                                break;
                            case BusinessClasses.Alignment.Right:
                                rbWindowBannerAlignmentLeft.Checked = false;
                                rbWindowBannerAlignmentCenter.Checked = false;
                                rbWindowBannerAlignmentRight.Checked = true;
                                break;
                        }
                        ckWindowBannerShowText.Checked = currentPageFolder.BannerProperties.ShowText;
                        memoEditWindowBannerText.EditValue = currentPageFolder.BannerProperties.Text;
                        buttonEditWindowBannerTextFont.Tag = currentPageFolder.BannerProperties.Font;
                        buttonEditWindowBannerTextFont.EditValue = FontToString(currentPageFolder.BannerProperties.Font);
                        colorEditWindowBannerTextColor.Color = currentPageFolder.BannerProperties.ForeColor;
                    }
                }
                else
                {
                    ckWindowEnableBanner.Checked = _currentFolder.BannerProperties.Enable;
                    pbWindowSelectedBanner.Image = _currentFolder.BannerProperties.Enable ? _currentFolder.BannerProperties.Image : null;
                    switch (_currentFolder.BannerProperties.ImageAlignement)
                    {
                        case BusinessClasses.Alignment.Left:
                            rbWindowBannerAlignmentLeft.Checked = true;
                            rbWindowBannerAlignmentCenter.Checked = false;
                            rbWindowBannerAlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Center:
                            rbWindowBannerAlignmentLeft.Checked = false;
                            rbWindowBannerAlignmentCenter.Checked = true;
                            rbWindowBannerAlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Right:
                            rbWindowBannerAlignmentLeft.Checked = false;
                            rbWindowBannerAlignmentCenter.Checked = false;
                            rbWindowBannerAlignmentRight.Checked = true;
                            break;
                    }
                    ckWindowBannerShowText.Checked = _currentFolder.BannerProperties.ShowText;
                    memoEditWindowBannerText.EditValue = _currentFolder.BannerProperties.Text;
                    buttonEditWindowBannerTextFont.Tag = _currentFolder.BannerProperties.Font;
                    buttonEditWindowBannerTextFont.EditValue = FontToString(_currentFolder.BannerProperties.Font);
                    colorEditWindowBannerTextColor.Color = _currentFolder.BannerProperties.ForeColor;
                }
                memoEditWindowBannerText.ForeColor = colorEditWindowBannerTextColor.Color;
                memoEditWindowBannerText.Font = buttonEditWindowBannerTextFont.Tag as Font;
                memoEditWindowBannerText.Properties.Appearance.Font = memoEditWindowBannerText.Font;
                memoEditWindowBannerText.Properties.AppearanceDisabled.Font = memoEditWindowBannerText.Font;
                memoEditWindowBannerText.Properties.AppearanceFocused.Font = memoEditWindowBannerText.Font;
                memoEditWindowBannerText.Properties.AppearanceReadOnly.Font = memoEditWindowBannerText.Font;
                _allowToSave = true;
            }
        }

        private void ClearWindowSettings()
        {
            _allowToSave = false;
            pnWindows.Enabled = false;
            laLocationValue.Text = string.Empty;
            colorEditWindowBorderColor.Color = Color.Black;
            colorEditWindowHeaderBackColor.Color = Color.White;
            colorEditWindowHeaderForeColor.Color = Color.White;
            colorEditWindowBackColor.Color = Color.White;
            colorEditWindowForeColor.Color = Color.White;
            buttonEditWindowHeaderFont.EditValue = string.Empty;
            _currentFolder = null;
            _allowToSave = true;
        }

        private void SaveWindowSettings()
        {
            if (_currentFolder != null)
            {
                if (ckApllyForAllWindowsAppearance.Checked && _currentPage.Folders.FirstOrDefault() == _currentFolder)
                {
                    foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders)
                    {
                        folder.BackgroundHeaderColor = colorEditWindowHeaderBackColor.Color;
                        folder.ForeHeaderColor = colorEditWindowHeaderForeColor.Color;
                        folder.BackgroundWindowColor = colorEditWindowBackColor.Color;
                        folder.ForeWindowColor = colorEditWindowForeColor.Color;
                        folder.BorderColor = colorEditWindowBorderColor.Color;
                        folder.HeaderFont = buttonEditWindowHeaderFont.Tag as Font;
                        if (rbWindowHeaderAlignmentLeft.Checked)
                            folder.HeaderAlignment = BusinessClasses.Alignment.Left;
                        else if (rbWindowHeaderAlignmentCenter.Checked)
                            folder.HeaderAlignment = BusinessClasses.Alignment.Center;
                        else if (rbWindowHeaderAlignmentRight.Checked)
                            folder.HeaderAlignment = BusinessClasses.Alignment.Right;
                    }
                    this.Library.ApplyAppearanceForAllWindows = true;
                }
                else
                {
                    if (_currentFolder != null)
                    {
                        _currentFolder.BackgroundHeaderColor = colorEditWindowHeaderBackColor.Color;
                        _currentFolder.ForeHeaderColor = colorEditWindowHeaderForeColor.Color;
                        _currentFolder.BackgroundWindowColor = colorEditWindowBackColor.Color;
                        _currentFolder.ForeWindowColor = colorEditWindowForeColor.Color;
                        _currentFolder.BorderColor = colorEditWindowBorderColor.Color;
                        _currentFolder.HeaderFont = buttonEditWindowHeaderFont.Tag as Font;
                        if (rbWindowHeaderAlignmentLeft.Checked)
                            _currentFolder.HeaderAlignment = BusinessClasses.Alignment.Left;
                        else if (rbWindowHeaderAlignmentCenter.Checked)
                            _currentFolder.HeaderAlignment = BusinessClasses.Alignment.Center;
                        else if (rbWindowHeaderAlignmentRight.Checked)
                            _currentFolder.HeaderAlignment = BusinessClasses.Alignment.Right;
                        this.Library.ApplyAppearanceForAllWindows = false;
                    }
                }

                if (ckApllyForAllWindowsWidget.Checked && _currentPage.Folders.FirstOrDefault() == _currentFolder)
                {
                    foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders)
                    {
                        folder.EnableWidget = ckWindowEnableWidget.Checked;
                        folder.Widget = pbWindowSelectedWidget.Image;
                    }
                    this.Library.ApplyWidgetForAllWindows = true;
                }
                else
                {
                    if (_currentFolder != null)
                    {
                        _currentFolder.EnableWidget = ckWindowEnableWidget.Checked;
                        _currentFolder.Widget = pbWindowSelectedWidget.Image;
                    }
                }

                if (ckApllyForAllWindowsBanner.Checked && _currentPage.Folders.FirstOrDefault() == _currentFolder)
                {
                    foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders)
                    {
                        folder.BannerProperties.Enable = ckWindowEnableBanner.Checked;
                        folder.BannerProperties.Image = pbWindowSelectedBanner.Image;
                        if (rbWindowBannerAlignmentLeft.Checked)
                            folder.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Left;
                        else if (rbWindowBannerAlignmentCenter.Checked)
                            folder.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Center;
                        else if (rbWindowBannerAlignmentRight.Checked)
                            folder.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Right;
                        folder.BannerProperties.ShowText = ckWindowBannerShowText.Checked;
                        folder.BannerProperties.Text = memoEditWindowBannerText.EditValue != null ? memoEditWindowBannerText.EditValue.ToString() : string.Empty;
                        folder.BannerProperties.Font = buttonEditWindowBannerTextFont.Tag as Font;
                        folder.BannerProperties.ForeColor = colorEditWindowBannerTextColor.Color;
                        folder.BannerProperties.Configured = true;
                    }
                    this.Library.ApplyBannerForAllWindows = true;
                }
                else
                {
                    if (_currentFolder != null)
                    {
                        _currentFolder.BannerProperties.Enable = ckWindowEnableBanner.Checked;
                        _currentFolder.BannerProperties.Image = pbWindowSelectedBanner.Image;
                        if (rbWindowBannerAlignmentLeft.Checked)
                            _currentFolder.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Left;
                        else if (rbWindowBannerAlignmentCenter.Checked)
                            _currentFolder.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Center;
                        else if (rbWindowBannerAlignmentRight.Checked)
                            _currentFolder.BannerProperties.ImageAlignement = BusinessClasses.Alignment.Right;
                        _currentFolder.BannerProperties.ShowText = ckWindowBannerShowText.Checked;
                        _currentFolder.BannerProperties.Text = memoEditWindowBannerText.EditValue != null ? memoEditWindowBannerText.EditValue.ToString() : string.Empty;
                        _currentFolder.BannerProperties.Font = buttonEditWindowBannerTextFont.Tag as Font;
                        _currentFolder.BannerProperties.ForeColor = colorEditWindowBannerTextColor.Color;
                        _currentFolder.BannerProperties.Configured = true;
                        this.Library.ApplyBannerForAllWindows = false;
                    }
                }
            }
        }

        private void GetColumnTitles()
        {
            if (_currentPage != null)
            {
                if (_currentPage.ColumnTitles.Count == 3)
                {
                    _allowToSave = false;
                    ckEnableColumnTitles.Checked = _currentPage.EnableColumnTitles;
                    ckApplyForAllColumnTitles.Checked = _currentPage.ApplyForAllColumnTitles;

                    colorEditColumn1BackColor.Color = _currentPage.ColumnTitles[0].BackgroundColor;
                    switch (_currentPage.ColumnTitles[0].HeaderAlignment)
                    {
                        case BusinessClasses.Alignment.Left:
                            rbColumn1AlignmentLeft.Checked = true;
                            rbColumn1AlignmentCenter.Checked = false;
                            rbColumn1AlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Center:
                            rbColumn1AlignmentLeft.Checked = false;
                            rbColumn1AlignmentCenter.Checked = true;
                            rbColumn1AlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Right:
                            rbColumn1AlignmentLeft.Checked = false;
                            rbColumn1AlignmentCenter.Checked = false;
                            rbColumn1AlignmentRight.Checked = true;
                            break;
                    }
                    ckColumn1EnableText.Checked = _currentPage.ColumnTitles[0].EnableText;
                    memoEditColumn1Title.EditValue = _currentPage.ColumnTitles[0].Name;
                    colorEditColumn1ForeColor.Color = _currentPage.ColumnTitles[0].ForeColor;
                    buttonEditColumn1Font.Tag = _currentPage.ColumnTitles[0].HeaderFont;
                    buttonEditColumn1Font.EditValue = FontToString(_currentPage.ColumnTitles[0].HeaderFont);
                    memoEditColumn1Title.ForeColor = colorEditColumn1ForeColor.Color;
                    memoEditColumn1Title.Font = buttonEditColumn1Font.Tag as Font; ;
                    memoEditColumn1Title.Properties.Appearance.Font = memoEditColumn1Title.Font;
                    memoEditColumn1Title.Properties.AppearanceDisabled.Font = memoEditColumn1Title.Font;
                    memoEditColumn1Title.Properties.AppearanceFocused.Font = memoEditColumn1Title.Font;
                    memoEditColumn1Title.Properties.AppearanceReadOnly.Font = memoEditColumn1Title.Font;
                    ckColumn1EnableBanner.Checked = _currentPage.ColumnTitles[0].BannerProperties.Enable;
                    pbColumn1SelectedBanner.Image = _currentPage.ColumnTitles[0].BannerProperties.Enable ? _currentPage.ColumnTitles[0].BannerProperties.Image : null;
                    ckColumn1EnableWidget.Checked = _currentPage.ColumnTitles[0].EnableWidget;
                    pbColumn1SelectedWidget.Image = _currentPage.ColumnTitles[0].EnableWidget ? _currentPage.ColumnTitles[0].Widget : null;

                    colorEditColumn2BackColor.Color = _currentPage.ColumnTitles[1].BackgroundColor;
                    switch (_currentPage.ColumnTitles[1].HeaderAlignment)
                    {
                        case BusinessClasses.Alignment.Left:
                            rbColumn2AlignmentLeft.Checked = true;
                            rbColumn2AlignmentCenter.Checked = false;
                            rbColumn2AlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Center:
                            rbColumn2AlignmentLeft.Checked = false;
                            rbColumn2AlignmentCenter.Checked = true;
                            rbColumn2AlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Right:
                            rbColumn2AlignmentLeft.Checked = false;
                            rbColumn2AlignmentCenter.Checked = false;
                            rbColumn2AlignmentRight.Checked = true;
                            break;
                    }
                    ckColumn2EnableText.Checked = _currentPage.ColumnTitles[1].EnableText;
                    memoEditColumn2Title.EditValue = _currentPage.ColumnTitles[1].Name;
                    colorEditColumn2ForeColor.Color = _currentPage.ColumnTitles[1].ForeColor;
                    buttonEditColumn2Font.Tag = _currentPage.ColumnTitles[1].HeaderFont;
                    buttonEditColumn2Font.EditValue = FontToString(_currentPage.ColumnTitles[1].HeaderFont);
                    memoEditColumn2Title.ForeColor = colorEditColumn2ForeColor.Color;
                    memoEditColumn2Title.Font = buttonEditColumn2Font.Tag as Font; ;
                    memoEditColumn2Title.Properties.Appearance.Font = memoEditColumn2Title.Font;
                    memoEditColumn2Title.Properties.AppearanceDisabled.Font = memoEditColumn2Title.Font;
                    memoEditColumn2Title.Properties.AppearanceFocused.Font = memoEditColumn2Title.Font;
                    memoEditColumn2Title.Properties.AppearanceReadOnly.Font = memoEditColumn2Title.Font;
                    ckColumn2EnableBanner.Checked = _currentPage.ColumnTitles[1].BannerProperties.Enable;
                    pbColumn2SelectedBanner.Image = _currentPage.ColumnTitles[1].BannerProperties.Enable ? _currentPage.ColumnTitles[1].BannerProperties.Image : null;
                    ckColumn2EnableWidget.Checked = _currentPage.ColumnTitles[1].EnableWidget;
                    pbColumn2SelectedWidget.Image = _currentPage.ColumnTitles[1].EnableWidget ? _currentPage.ColumnTitles[1].Widget : null;

                    colorEditColumn3BackColor.Color = _currentPage.ColumnTitles[2].BackgroundColor;
                    switch (_currentPage.ColumnTitles[2].HeaderAlignment)
                    {
                        case BusinessClasses.Alignment.Left:
                            rbColumn3AlignmentLeft.Checked = true;
                            rbColumn3AlignmentCenter.Checked = false;
                            rbColumn3AlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Center:
                            rbColumn3AlignmentLeft.Checked = false;
                            rbColumn3AlignmentCenter.Checked = true;
                            rbColumn3AlignmentRight.Checked = false;
                            break;
                        case BusinessClasses.Alignment.Right:
                            rbColumn3AlignmentLeft.Checked = false;
                            rbColumn3AlignmentCenter.Checked = false;
                            rbColumn3AlignmentRight.Checked = true;
                            break;
                    }
                    ckColumn3EnableText.Checked = _currentPage.ColumnTitles[2].EnableText;
                    memoEditColumn3Title.EditValue = _currentPage.ColumnTitles[2].Name;
                    colorEditColumn3ForeColor.Color = _currentPage.ColumnTitles[2].ForeColor;
                    buttonEditColumn3Font.Tag = _currentPage.ColumnTitles[2].HeaderFont;
                    buttonEditColumn3Font.EditValue = FontToString(_currentPage.ColumnTitles[2].HeaderFont);
                    memoEditColumn3Title.ForeColor = colorEditColumn3ForeColor.Color;
                    memoEditColumn3Title.Font = buttonEditColumn3Font.Tag as Font; ;
                    memoEditColumn3Title.Properties.Appearance.Font = memoEditColumn3Title.Font;
                    memoEditColumn3Title.Properties.AppearanceDisabled.Font = memoEditColumn3Title.Font;
                    memoEditColumn3Title.Properties.AppearanceFocused.Font = memoEditColumn3Title.Font;
                    memoEditColumn3Title.Properties.AppearanceReadOnly.Font = memoEditColumn3Title.Font;
                    ckColumn3EnableBanner.Checked = _currentPage.ColumnTitles[2].BannerProperties.Enable;
                    pbColumn3SelectedBanner.Image = _currentPage.ColumnTitles[2].BannerProperties.Enable ? _currentPage.ColumnTitles[2].BannerProperties.Image : null;
                    ckColumn3EnableWidget.Checked = _currentPage.ColumnTitles[2].EnableWidget;
                    pbColumn3SelectedWidget.Image = _currentPage.ColumnTitles[2].EnableWidget ? _currentPage.ColumnTitles[2].Widget : null;

                    _allowToSave = true;
                }
            }
        }

        private void SaveColumnTitles()
        {
            if (_currentPage != null)
            {
                _currentPage.ColumnTitles.Clear();

                BusinessClasses.ColumnTitle column = new BusinessClasses.ColumnTitle(_currentPage);
                column.ColumnOrder = 0;
                column.BackgroundColor = colorEditColumn1BackColor.Color;
                if (rbColumn1AlignmentLeft.Checked)
                    column.HeaderAlignment = BusinessClasses.Alignment.Left;
                else if (rbColumn1AlignmentCenter.Checked)
                    column.HeaderAlignment = BusinessClasses.Alignment.Center;
                else if (rbColumn1AlignmentRight.Checked)
                    column.HeaderAlignment = BusinessClasses.Alignment.Right;
                column.Name = ckColumn1EnableText.Checked & memoEditColumn1Title.EditValue != null ? memoEditColumn1Title.EditValue.ToString() : string.Empty;
                column.EnableText = ckColumn1EnableText.Checked & !string.IsNullOrEmpty(column.Name);
                column.ForeColor = colorEditColumn1ForeColor.Color;
                column.HeaderFont = buttonEditColumn1Font.Tag as Font;
                column.BannerProperties.Enable = ckColumn1EnableBanner.Checked & pbColumn1SelectedBanner.Image != null;
                column.BannerProperties.Image = ckColumn1EnableBanner.Checked ? pbColumn1SelectedBanner.Image : null;
                column.EnableWidget = ckColumn1EnableWidget.Checked & pbColumn1SelectedWidget.Image != null;
                column.Widget = ckColumn1EnableWidget.Checked ? pbColumn1SelectedWidget.Image : null;
                _currentPage.ColumnTitles.Add(column);

                if (ckApplyForAllColumnTitles.Checked)
                {
                    column = new BusinessClasses.ColumnTitle(_currentPage);
                    column.ColumnOrder = 1;
                    column.BackgroundColor = colorEditColumn1BackColor.Color;
                    if (rbColumn1AlignmentLeft.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Left;
                    else if (rbColumn1AlignmentCenter.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Center;
                    else if (rbColumn1AlignmentRight.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Right;
                    column.Name = ckColumn1EnableText.Checked & memoEditColumn1Title.EditValue != null ? memoEditColumn1Title.EditValue.ToString() : string.Empty;
                    column.EnableText = ckColumn1EnableText.Checked & !string.IsNullOrEmpty(column.Name);
                    column.ForeColor = colorEditColumn1ForeColor.Color;
                    column.HeaderFont = buttonEditColumn1Font.Tag as Font;
                    column.BannerProperties.Enable = ckColumn1EnableBanner.Checked & pbColumn1SelectedBanner.Image != null;
                    column.BannerProperties.Image = ckColumn1EnableBanner.Checked ? pbColumn1SelectedBanner.Image : null;
                    column.EnableWidget = ckColumn1EnableWidget.Checked & pbColumn1SelectedWidget.Image != null;
                    column.Widget = ckColumn1EnableWidget.Checked ? pbColumn1SelectedWidget.Image : null;
                    _currentPage.ColumnTitles.Add(column);


                    column = new BusinessClasses.ColumnTitle(_currentPage);
                    column.ColumnOrder = 2;
                    column.BackgroundColor = colorEditColumn1BackColor.Color;
                    if (rbColumn1AlignmentLeft.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Left;
                    else if (rbColumn1AlignmentCenter.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Center;
                    else if (rbColumn1AlignmentRight.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Right;
                    column.Name = ckColumn1EnableText.Checked & memoEditColumn1Title.EditValue != null ? memoEditColumn1Title.EditValue.ToString() : string.Empty;
                    column.EnableText = ckColumn1EnableText.Checked & !string.IsNullOrEmpty(column.Name);
                    column.ForeColor = colorEditColumn1ForeColor.Color;
                    column.HeaderFont = buttonEditColumn1Font.Tag as Font;
                    column.BannerProperties.Enable = ckColumn1EnableBanner.Checked & pbColumn1SelectedBanner.Image != null;
                    column.BannerProperties.Image = ckColumn1EnableBanner.Checked ? pbColumn1SelectedBanner.Image : null;
                    column.EnableWidget = ckColumn1EnableWidget.Checked & pbColumn1SelectedWidget.Image != null;
                    column.Widget = ckColumn1EnableWidget.Checked ? pbColumn1SelectedWidget.Image : null;
                    _currentPage.ColumnTitles.Add(column);
                }
                else
                {
                    column = new BusinessClasses.ColumnTitle(_currentPage);
                    column.ColumnOrder = 1;
                    column.BackgroundColor = colorEditColumn2BackColor.Color;
                    if (rbColumn2AlignmentLeft.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Left;
                    else if (rbColumn2AlignmentCenter.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Center;
                    else if (rbColumn2AlignmentRight.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Right;
                    column.Name = ckColumn2EnableText.Checked & memoEditColumn2Title.EditValue != null ? memoEditColumn2Title.EditValue.ToString() : string.Empty;
                    column.EnableText = ckColumn2EnableText.Checked & !string.IsNullOrEmpty(column.Name);
                    column.ForeColor = colorEditColumn2ForeColor.Color;
                    column.HeaderFont = buttonEditColumn2Font.Tag as Font;
                    column.BannerProperties.Enable = ckColumn2EnableBanner.Checked & pbColumn2SelectedBanner.Image != null;
                    column.BannerProperties.Image = ckColumn2EnableBanner.Checked ? pbColumn2SelectedBanner.Image : null;
                    column.EnableWidget = ckColumn2EnableWidget.Checked & pbColumn2SelectedWidget.Image != null;
                    column.Widget = ckColumn2EnableWidget.Checked ? pbColumn2SelectedWidget.Image : null;
                    _currentPage.ColumnTitles.Add(column);

                    column = new BusinessClasses.ColumnTitle(_currentPage);
                    column.ColumnOrder = 2;
                    column.BackgroundColor = colorEditColumn3BackColor.Color;
                    if (rbColumn3AlignmentLeft.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Left;
                    else if (rbColumn3AlignmentCenter.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Center;
                    else if (rbColumn3AlignmentRight.Checked)
                        column.HeaderAlignment = BusinessClasses.Alignment.Right;
                    column.Name = ckColumn3EnableText.Checked & memoEditColumn3Title.EditValue != null ? memoEditColumn3Title.EditValue.ToString() : string.Empty;
                    column.EnableText = ckColumn3EnableText.Checked & !string.IsNullOrEmpty(column.Name);
                    column.ForeColor = colorEditColumn3ForeColor.Color;
                    column.HeaderFont = buttonEditColumn3Font.Tag as Font;
                    column.BannerProperties.Enable = ckColumn3EnableBanner.Checked & pbColumn3SelectedBanner.Image != null;
                    column.BannerProperties.Image = ckColumn3EnableBanner.Checked ? pbColumn3SelectedBanner.Image : null;
                    column.EnableWidget = ckColumn3EnableWidget.Checked & pbColumn3SelectedWidget.Image != null;
                    column.Widget = ckColumn3EnableWidget.Checked ? pbColumn3SelectedWidget.Image : null;
                    _currentPage.ColumnTitles.Add(column);
                }

                _currentPage.EnableColumnTitles = ckEnableColumnTitles.Checked;
                _currentPage.ApplyForAllColumnTitles = ckApplyForAllColumnTitles.Checked;
            }
        }

        private void PopulatePagesList()
        {
            _allowToSave = false;
            comboBoxEditPages.Properties.Items.Clear();

            comboBoxEditPages.Properties.Items.AddRange(this.Library.Pages.Select(y => y.Name).ToArray());
            if (comboBoxEditPages.Properties.Items.Count > 0)
                comboBoxEditPages.SelectedIndex = 0;
            if (comboBoxEditPages.Properties.Items.Count > 1)
                pnPages.Visible = true;
            else
                pnPages.Visible = false;
            _allowToSave = true;
        }

        private void PopulateWindowsList()
        {
            comboBoxEditWindows.Properties.Items.Clear();
            ClearWindowSettings();
            if (_currentPage != null)
            {
                comboBoxEditWindows.Properties.Items.AddRange(_currentPage.Folders.Select(x => x.Name).ToArray());
                comboBoxEditWindows.SelectedIndex = -1;
                if (comboBoxEditWindows.Properties.Items.Count > 0)
                    comboBoxEditWindows.SelectedIndex = 0;
            }
        }
        #endregion

        #region Base GUI
        private void Form_Load(object sender, EventArgs e)
        {
            _currentFolder = null;
            _currentPage = null;
            grColumn1.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
            grColumn2.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
            grColumn3.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
            xtraTabControlSettings.SelectedTabPage = xtraTabPageColumns;
            xtraTabControlWindows.SelectedTabPage = xtraTabPageColumn1;
            xtraTabControlWindowProperties.SelectedTabPage = xtraTabPageWindowPropertiesAppearance;
            xtraTabControlColumnTitles.SelectedTabPage = xtraTabPageColumnTitles1;
            ckApllyForAllWindowsAppearance.Checked = this.Library.ApplyAppearanceForAllWindows;
            ckApllyForAllWindowsWidget.Checked = this.Library.ApplyWidgetForAllWindows;
            ckApllyForAllWindowsBanner.Checked = this.Library.ApplyBannerForAllWindows;
            xtraTabPageWindowPropertiesBanner.PageEnabled = System.IO.Directory.Exists(ConfigurationClasses.ListManager.Instance.BannerFolder);
            gridControlWindowBanners.DataSource = new BindingList<ConfigurationClasses.Banner>(ConfigurationClasses.ListManager.Instance.Banners);
            xtraTabPageWindowPropertiesWidget.PageEnabled = System.IO.Directory.Exists(ConfigurationClasses.ListManager.Instance.WidgetFolder);
            gridControlWindowWidgets.DataSource = new BindingList<ConfigurationClasses.Widget>(ConfigurationClasses.ListManager.Instance.Widgets);
            gridControlColumn1Banners.DataSource = new BindingList<ConfigurationClasses.Banner>(ConfigurationClasses.ListManager.Instance.Banners);
            gridControlColumn2Banners.DataSource = new BindingList<ConfigurationClasses.Banner>(ConfigurationClasses.ListManager.Instance.Banners);
            gridControlColumn3Banners.DataSource = new BindingList<ConfigurationClasses.Banner>(ConfigurationClasses.ListManager.Instance.Banners);
            gridControlColumn1Widgets.DataSource = new BindingList<ConfigurationClasses.Widget>(ConfigurationClasses.ListManager.Instance.Widgets);
            gridControlColumn2Widgets.DataSource = new BindingList<ConfigurationClasses.Widget>(ConfigurationClasses.ListManager.Instance.Widgets);
            gridControlColumn3Widgets.DataSource = new BindingList<ConfigurationClasses.Widget>(ConfigurationClasses.ListManager.Instance.Widgets);
            PopulatePagesList();
            comboBoxEditPages_SelectedIndexChanged(null, null);
            _stateChanges = false;
        }


        private void StateChanges_TextChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                _stateChanges = true;
        }

        private void StateChanges_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                _stateChanges = true;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveColumnSettings();
            SaveColumnTitles();
            if (_currentFolder == null)
            {
                if (this.Library.ApplyAppearanceForAllWindows)
                {
                    BusinessClasses.LibraryFolder currentPageFolder = _currentPage.Folders.FirstOrDefault();
                    if (currentPageFolder != null)
                    {
                        foreach (BusinessClasses.LibraryFolder folder in _currentPage.Folders)
                        {
                            folder.BackgroundHeaderColor = currentPageFolder.BackgroundHeaderColor;
                            folder.ForeHeaderColor = currentPageFolder.ForeHeaderColor;
                            folder.BackgroundWindowColor = currentPageFolder.BackgroundWindowColor;
                            folder.ForeWindowColor = currentPageFolder.ForeWindowColor;
                            folder.HeaderFont = currentPageFolder.HeaderFont;
                        }
                    }
                }
            }
            else
                SaveWindowSettings();
            this.Library.Save();
            AppManager.Instance.ShowInfo("Wall Bin Settings are saved");
            _changesDone = true;
            _stateChanges = false;
        }

        private void DWBSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_stateChanges)
            {
                if (AppManager.Instance.ShowQuestion("Before you EXIT, do you want to save the changes you made?") == DialogResult.Yes)
                {
                    btSave_Click(null, null);
                }
                else
                {
                    if (MessageBox.Show("You are about to lose your changes.\nThe changes will be LOST FOREVER & EVER & EVER!", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        e.Cancel = true;
                }
            }
            if (_changesDone)
                this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Columns Tab GUI
        private void comboBoxEditPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                BusinessClasses.FolderCopier.PasteReady = false;
                SaveWindowSettings();
                SaveColumnSettings();
                SaveColumnTitles();
                _currentFolder = null;
                if (comboBoxEditPages.EditValue != null)
                {
                    _currentPage = this.Library.Pages.Where(x => x.Name.Equals(comboBoxEditPages.EditValue.ToString())).FirstOrDefault();
                    GetColumnSettings();
                    GetColumnTitles();
                    PopulateWindowsList();
                }
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            grColumn1.EndEdit();
            grColumn1.ClearSelection();
            grColumn2.EndEdit();
            grColumn2.ClearSelection();
            grColumn3.EndEdit();
            grColumn3.ClearSelection();
        }

        private void grColumn_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hitTestInfo = ((DataGridView)sender).HitTest(e.X, e.Y);
            if (hitTestInfo.Type != DataGridViewHitTestType.Cell)
            {
                grColumn1.EndEdit();
                grColumn1.ClearSelection();
                grColumn2.EndEdit();
                grColumn2.ClearSelection();
                grColumn3.EndEdit();
                grColumn3.ClearSelection();
            }
        }

        private void grColumn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tmiCut.Enabled = false;
                tmiPaste.Enabled = BusinessClasses.FolderCopier.PasteReady;
                contextMenuStrip.Show(((DataGridView)sender), new Point(e.X, e.Y));
            }
        }

        private void grColumn_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ((DataGridView)sender).Rows[e.RowIndex].Selected = true;
                if (((DataGridView)sender).SelectedRows.Count > 0)
                    tmiCut.Enabled = true;
                else
                    tmiCut.Enabled = false;
                tmiPaste.Enabled = BusinessClasses.FolderCopier.PasteReady;
                contextMenuStrip.Show(((DataGridView)sender), new Point(e.X, e.Y));
            }
        }

        private void grColumn1_SelectionChanged(object sender, EventArgs e)
        {
            if (_column1FirstSelect)
                _column1FirstSelect = false;
            else
                grColumn1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        }

        private void grColumn2_SelectionChanged(object sender, EventArgs e)
        {
            if (_column2FirstSelect)
                _column2FirstSelect = false;
            else
                grColumn2.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        }

        private void grColumn3_SelectionChanged(object sender, EventArgs e)
        {
            if (_column3FirstSelect)
                _column3FirstSelect = false;
            else
                grColumn3.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        }

        private void tmiCut_Click(object sender, EventArgs e)
        {
            DataGridView workingGrid = null;

            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    workingGrid = grColumn1;
                    break;
                case 1:
                    workingGrid = grColumn2;
                    break;
                case 2:
                    workingGrid = grColumn3;
                    break;
            }

            BusinessClasses.LibraryFolder selectedFolder = _currentPage.Folders.Where(x => x.Identifier.Equals(workingGrid.SelectedRows[0].Cells[1].Value)).FirstOrDefault();
            if (selectedFolder != null)
            {
                BusinessClasses.FolderCopier.CopiedFolder = selectedFolder;
                BusinessClasses.FolderCopier.Copy(workingGrid, workingGrid.SelectedRows[0].Index);
            }
        }

        private void tmiPaste_Click(object sender, EventArgs e)
        {
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    BusinessClasses.FolderCopier.Paste(grColumn1);
                    break;
                case 1:
                    BusinessClasses.FolderCopier.Paste(grColumn2);
                    break;
                case 2:
                    BusinessClasses.FolderCopier.Paste(grColumn3);
                    break;
            }
            _stateChanges = true;
        }

        private void btAddWindow_Click(object sender, EventArgs e)
        {
            int rowNumber;

            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    rowNumber = grColumn1.Rows.Add("Window " + (grColumn1.Rows.Count + 1).ToString(), Guid.NewGuid());
                    grColumn1.CurrentCell = grColumn1.Rows[rowNumber].Cells[0];
                    grColumn1.Focus();
                    grColumn1.BeginEdit(true);
                    break;
                case 1:
                    rowNumber = grColumn2.Rows.Add("Window " + (grColumn2.Rows.Count + 1).ToString(), Guid.NewGuid());
                    grColumn2.CurrentCell = grColumn2.Rows[rowNumber].Cells[0];
                    grColumn2.Focus();
                    grColumn2.BeginEdit(true);
                    break;
                case 2:
                    rowNumber = grColumn3.Rows.Add("Window " + (grColumn3.Rows.Count + 1).ToString(), Guid.NewGuid());
                    grColumn3.CurrentCell = grColumn3.Rows[rowNumber].Cells[0];
                    grColumn3.Focus();
                    grColumn3.BeginEdit(true);
                    break;
            }
            _stateChanges = true;
        }

        private void btRemoveWindow_Click(object sender, EventArgs e)
        {
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    foreach (DataGridViewRow row in grColumn1.SelectedRows)
                        grColumn1.Rows.Remove(row);
                    break;
                case 1:
                    foreach (DataGridViewRow row in grColumn2.SelectedRows)
                        grColumn2.Rows.Remove(row);
                    break;
                case 2:
                    foreach (DataGridViewRow row in grColumn3.SelectedRows)
                        grColumn3.Rows.Remove(row);
                    break;
            }
            _stateChanges = true;
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            DataGridView columnGrid = null;
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    columnGrid = grColumn1;
                    break;
                case 1:
                    columnGrid = grColumn2;
                    break;
                case 2:
                    columnGrid = grColumn3;
                    break;
            }

            if (columnGrid.SelectedRows.Count > 0)
            {
                if (columnGrid.SelectedRows[0].Index > 0)
                {
                    string windowName = "";
                    Guid windowIdentifier = Guid.Empty;

                    columnGrid.SuspendLayout();

                    windowName = columnGrid.SelectedRows[0].Cells[0].Value.ToString();
                    windowIdentifier = (Guid)columnGrid.SelectedRows[0].Cells[1].Value;

                    columnGrid.SelectedRows[0].Cells[0].Value = columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Cells[0].Value;
                    columnGrid.SelectedRows[0].Cells[1].Value = columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Cells[1].Value;

                    columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Cells[0].Value = windowName;
                    columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Cells[1].Value = windowIdentifier;

                    columnGrid.Rows[columnGrid.SelectedRows[0].Index - 1].Selected = true;

                    columnGrid.ResumeLayout();
                }
            }
            _stateChanges = true;
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            DataGridView columnGrid = null;
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    columnGrid = grColumn1;
                    break;
                case 1:
                    columnGrid = grColumn2;
                    break;
                case 2:
                    columnGrid = grColumn3;
                    break;
            }

            if (columnGrid.SelectedRows.Count > 0)
            {
                if (columnGrid.SelectedRows[0].Index < columnGrid.Rows.Count - 1)
                {
                    string windowName = "";
                    Guid windowIdentifier = Guid.Empty;

                    columnGrid.SuspendLayout();

                    windowName = columnGrid.SelectedRows[0].Cells[0].Value.ToString();
                    windowIdentifier = (Guid)columnGrid.SelectedRows[0].Cells[1].Value;

                    columnGrid.SelectedRows[0].Cells[0].Value = columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Cells[0].Value;
                    columnGrid.SelectedRows[0].Cells[1].Value = columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Cells[1].Value;

                    columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Cells[0].Value = windowName;
                    columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Cells[1].Value = windowIdentifier;

                    columnGrid.Rows[columnGrid.SelectedRows[0].Index + 1].Selected = true;

                    columnGrid.ResumeLayout();
                }
            }
            _stateChanges = true;
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            DataGridView sourceGrid = null;
            DataGridView destGrid = null;

            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    sourceGrid = grColumn1;
                    destGrid = grColumn2;
                    break;
                case 1:
                    sourceGrid = grColumn3;
                    destGrid = grColumn3;
                    break;
            }
            if (sourceGrid.SelectedRows.Count > 0)
            {
                BusinessClasses.LibraryFolder selectedFolder = _currentPage.Folders.Where(x => x.Identifier.Equals(sourceGrid.SelectedRows[0].Cells[1].Value)).FirstOrDefault();
                if (selectedFolder != null)
                {
                    BusinessClasses.FolderCopier.CopiedFolder = selectedFolder;
                    BusinessClasses.FolderCopier.Copy(sourceGrid, sourceGrid.SelectedRows[0].Index);
                    BusinessClasses.FolderCopier.Paste(destGrid);
                }
            }
            _stateChanges = true;
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            DataGridView sourceGrid = null;
            DataGridView destGrid = null;

            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 1:
                    sourceGrid = grColumn2;
                    destGrid = grColumn1;
                    break;
                case 2:
                    sourceGrid = grColumn3;
                    destGrid = grColumn2;
                    break;
            }
            if (sourceGrid.SelectedRows.Count > 0)
            {
                BusinessClasses.LibraryFolder selectedFolder = _currentPage.Folders.Where(x => x.Identifier.Equals(sourceGrid.SelectedRows[0].Cells[1].Value)).FirstOrDefault();
                if (selectedFolder != null)
                {
                    BusinessClasses.FolderCopier.CopiedFolder = selectedFolder;
                    BusinessClasses.FolderCopier.Copy(sourceGrid, sourceGrid.SelectedRows[0].Index);
                    BusinessClasses.FolderCopier.Paste(destGrid);
                }
            }
            _stateChanges = true;
        }

        private void xtraTabControlSettings_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.PrevPage == xtraTabPageColumns)
            {
                SaveColumnSettings();
                PopulateWindowsList();
            }
        }

        private void xtraTabControlWindows_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xtraTabControlWindows.SelectedTabPageIndex)
            {
                case 0:
                    buttonXRight.Enabled = true;
                    buttonXLeft.Enabled = false;
                    break;
                case 1:
                    buttonXRight.Enabled = true;
                    buttonXLeft.Enabled = true;
                    break;
                case 2:
                    buttonXRight.Enabled = false;
                    buttonXLeft.Enabled = true;
                    break;
            }
        }
        #endregion

        #region Windows Tab GUI
        private void cbWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentFolder != null)
                SaveWindowSettings();
            if (comboBoxEditWindows.SelectedIndex >= 0)
                _currentFolder = _currentPage.Folders[comboBoxEditWindows.SelectedIndex];
            else
                _currentFolder = null;
            GetWindowSettings();
            if (comboBoxEditWindows.SelectedIndex == 0)
            {
                ckApllyForAllWindowsAppearance.Visible = true;
                colorEditWindowHeaderBackColor.Enabled = true;
                colorEditWindowHeaderForeColor.Enabled = true;
                colorEditWindowBackColor.Enabled = true;
                colorEditWindowForeColor.Enabled = true;
                buttonEditWindowHeaderFont.Enabled = true;
                rbWindowHeaderAlignmentCenter.Enabled = true;
                rbWindowHeaderAlignmentLeft.Enabled = true;
                rbWindowHeaderAlignmentRight.Enabled = true;

                ckApllyForAllWindowsBanner.Visible = true;
                ckWindowEnableBanner.Enabled = true;
                gbWindowBanners.Enabled = ckWindowEnableBanner.Checked;

                ckApllyForAllWindowsWidget.Visible = true;
                ckWindowEnableWidget.Enabled = true;
                gbWindowWidgets.Enabled = ckWindowEnableWidget.Checked;
            }
            else
            {
                ckApllyForAllWindowsAppearance.Visible = false;
                colorEditWindowHeaderBackColor.Enabled = !ckApllyForAllWindowsAppearance.Checked;
                colorEditWindowHeaderForeColor.Enabled = !ckApllyForAllWindowsAppearance.Checked;
                colorEditWindowBackColor.Enabled = !ckApllyForAllWindowsAppearance.Checked;
                colorEditWindowForeColor.Enabled = !ckApllyForAllWindowsAppearance.Checked;
                buttonEditWindowHeaderFont.Enabled = !ckApllyForAllWindowsAppearance.Checked;
                rbWindowHeaderAlignmentCenter.Enabled = !ckApllyForAllWindowsAppearance.Checked;
                rbWindowHeaderAlignmentLeft.Enabled = !ckApllyForAllWindowsAppearance.Checked;
                rbWindowHeaderAlignmentRight.Enabled = !ckApllyForAllWindowsAppearance.Checked;

                ckApllyForAllWindowsBanner.Visible = false;
                ckWindowEnableBanner.Enabled = !ckApllyForAllWindowsBanner.Checked;
                gbWindowBanners.Enabled = !ckApllyForAllWindowsBanner.Checked & ckWindowEnableBanner.Checked;

                ckApllyForAllWindowsWidget.Visible = false;
                ckWindowEnableWidget.Enabled = !ckApllyForAllWindowsWidget.Checked;
                gbWindowWidgets.Enabled = !ckApllyForAllWindowsWidget.Checked & ckWindowEnableWidget.Checked;
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
                    if (_allowToSave)
                        _stateChanges = true;
                }
            }
        }

        private void FontEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FontEdit_Click(this, null);
        }

        #region Window Banner
        private void ckWindowEnableBanner_CheckedChanged(object sender, EventArgs e)
        {
            gbWindowBanners.Enabled = ckWindowEnableBanner.Checked;
            if (ckWindowEnableBanner.Checked)
                ckWindowEnableWidget.Checked = false;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void ckWindowBannerShowText_CheckedChanged(object sender, EventArgs e)
        {
            memoEditWindowBannerText.Enabled = ckWindowBannerShowText.Checked;
            buttonEditWindowBannerTextFont.Enabled = ckWindowBannerShowText.Checked;
            colorEditWindowBannerTextColor.Enabled = ckWindowBannerShowText.Checked;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void gridViewBanners_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Banner selectedBanner = null;
            if (gridViewWindowBanners.FocusedRowHandle >= 0)
                selectedBanner = ConfigurationClasses.ListManager.Instance.Banners[gridViewWindowBanners.GetDataSourceRowIndex(gridViewWindowBanners.FocusedRowHandle)];
            pbWindowSelectedBanner.Image = selectedBanner != null ? selectedBanner.Image : null;
        }

        private void gridViewBanners_Click(object sender, EventArgs e)
        {
            Point pt = gridControlWindowBanners.PointToClient(Control.MousePosition);

            if (gridViewWindowBanners.CalcHitInfo(pt).RowHandle == gridViewWindowBanners.FocusedRowHandle)
                gridViewBanners_FocusedRowChanged(null, null);
        }

        private void colorEditWindowBannerTextColor_EditValueChanged(object sender, EventArgs e)
        {
            memoEditWindowBannerText.ForeColor = colorEditWindowBannerTextColor.Color;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void buttonEditWindowBannerTextFont_EditValueChanged(object sender, EventArgs e)
        {
            memoEditWindowBannerText.Font = buttonEditWindowBannerTextFont.Tag as Font; ;
            memoEditWindowBannerText.Properties.Appearance.Font = memoEditWindowBannerText.Font;
            memoEditWindowBannerText.Properties.AppearanceDisabled.Font = memoEditWindowBannerText.Font;
            memoEditWindowBannerText.Properties.AppearanceFocused.Font = memoEditWindowBannerText.Font;
            memoEditWindowBannerText.Properties.AppearanceReadOnly.Font = memoEditWindowBannerText.Font;
            if (_allowToSave)
                _stateChanges = true;
        }
        #endregion

        #region Window Widget
        private void ckWindowEnableWidget_CheckedChanged(object sender, EventArgs e)
        {
            gbWindowWidgets.Enabled = ckWindowEnableWidget.Checked;
            if (ckWindowEnableWidget.Checked)
                ckWindowEnableBanner.Checked = false;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void layoutViewWindowWidgets_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Widget selectedWidget = null;
            if (layoutViewWindowWidgets.FocusedRowHandle >= 0)
            {
                selectedWidget = ConfigurationClasses.ListManager.Instance.Widgets[layoutViewWindowWidgets.GetDataSourceRowIndex(layoutViewWindowWidgets.FocusedRowHandle)];
            }
            pbWindowSelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void layoutViewWindowWidgets_Click(object sender, EventArgs e)
        {
            Point pt = gridControlWindowWidgets.PointToClient(Control.MousePosition);

            if (layoutViewWindowWidgets.CalcHitInfo(pt).RowHandle == layoutViewWindowWidgets.FocusedRowHandle)
                layoutViewWindowWidgets_FocusedRowChanged(null, null);
        }
        #endregion
        #endregion

        #region Column Titles Tab GUI
        private void ckEnableColumnTitles_CheckedChanged(object sender, EventArgs e)
        {
            xtraTabControlColumnTitles.Enabled = ckEnableColumnTitles.Checked;
            ckApplyForAllColumnTitles.Enabled = ckEnableColumnTitles.Checked;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void xtraTabControlColumnTitles_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControlColumnTitles.SelectedTabPageIndex == 0)
                ckApplyForAllColumnTitles.Visible = true;
            else
                ckApplyForAllColumnTitles.Visible = false;
        }

        private void ckApplyForAllColumnTitles_CheckedChanged(object sender, EventArgs e)
        {
            xtraTabPageColumnTitles2.PageEnabled = !ckApplyForAllColumnTitles.Checked;
            xtraTabPageColumnTitles3.PageEnabled = !ckApplyForAllColumnTitles.Checked;
            if (_allowToSave)
                _stateChanges = true;
        }

        #region Column 1
        private void ckColumn1EnableText_CheckedChanged(object sender, EventArgs e)
        {
            laColumn1Font.Enabled = ckColumn1EnableText.Checked;
            laColumn1ForeColor.Enabled = ckColumn1EnableText.Checked;
            memoEditColumn1Title.Enabled = ckColumn1EnableText.Checked;
            colorEditColumn1ForeColor.Enabled = ckColumn1EnableText.Checked;
            buttonEditColumn1Font.Enabled = ckColumn1EnableText.Checked;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void colorEditColumn1ForeColor_EditValueChanged(object sender, EventArgs e)
        {
            memoEditColumn1Title.ForeColor = colorEditColumn1ForeColor.Color;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void buttonEditColumn1Font_EditValueChanged(object sender, EventArgs e)
        {
            memoEditColumn1Title.Font = buttonEditColumn1Font.Tag as Font; ;
            memoEditColumn1Title.Properties.Appearance.Font = memoEditColumn1Title.Font;
            memoEditColumn1Title.Properties.AppearanceDisabled.Font = memoEditColumn1Title.Font;
            memoEditColumn1Title.Properties.AppearanceFocused.Font = memoEditColumn1Title.Font;
            memoEditColumn1Title.Properties.AppearanceReadOnly.Font = memoEditColumn1Title.Font;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void ckColumn1EnableBanner_CheckedChanged(object sender, EventArgs e)
        {
            pbColumn1SelectedBanner.Enabled = ckColumn1EnableBanner.Checked;
            gridControlColumn1Banners.Enabled = ckColumn1EnableBanner.Checked;
            if (ckColumn1EnableBanner.Checked)
                ckColumn1EnableWidget.Checked = false;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void gridViewColumn1Banners_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Banner selectedBanner = null;
            if (gridViewColumn1Banners.FocusedRowHandle >= 0)
                selectedBanner = ConfigurationClasses.ListManager.Instance.Banners[gridViewColumn1Banners.GetDataSourceRowIndex(gridViewColumn1Banners.FocusedRowHandle)];
            pbColumn1SelectedBanner.Image = selectedBanner != null ? selectedBanner.Image : null;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void gridViewColumn1Banners_Click(object sender, EventArgs e)
        {
            Point pt = gridControlColumn1Banners.PointToClient(Control.MousePosition);

            if (gridViewColumn1Banners.CalcHitInfo(pt).RowHandle == gridViewColumn1Banners.FocusedRowHandle)
                gridViewColumn1Banners_FocusedRowChanged(null, null);
        }

        private void ckColumn1EnableWidget_CheckedChanged(object sender, EventArgs e)
        {
            pbColumn1SelectedWidget.Enabled = ckColumn1EnableWidget.Checked;
            gridControlColumn1Widgets.Enabled = ckColumn1EnableWidget.Checked;
            if (ckColumn1EnableWidget.Checked)
                ckColumn1EnableBanner.Checked = false;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void layoutViewColumn1Widgets_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Widget selectedWidget = null;
            if (layoutViewColumn1Widgets.FocusedRowHandle >= 0)
            {
                selectedWidget = ConfigurationClasses.ListManager.Instance.Widgets[layoutViewColumn1Widgets.GetDataSourceRowIndex(layoutViewColumn1Widgets.FocusedRowHandle)];
            }
            pbColumn1SelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void layoutViewColumn1Widgets_Click(object sender, EventArgs e)
        {
            Point pt = gridControlColumn1Widgets.PointToClient(Control.MousePosition);

            if (layoutViewColumn1Widgets.CalcHitInfo(pt).RowHandle == layoutViewColumn1Widgets.FocusedRowHandle)
                layoutViewColumn1Widgets_FocusedRowChanged(null, null);
        }
        #endregion

        #region Column 2
        private void ckColumn2EnableText_CheckedChanged(object sender, EventArgs e)
        {
            laColumn2Font.Enabled = ckColumn2EnableText.Checked;
            laColumn2ForeColor.Enabled = ckColumn2EnableText.Checked;
            memoEditColumn2Title.Enabled = ckColumn2EnableText.Checked;
            colorEditColumn2ForeColor.Enabled = ckColumn2EnableText.Checked;
            buttonEditColumn2Font.Enabled = ckColumn2EnableText.Checked;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void buttonEditColumn2Font_EditValueChanged(object sender, EventArgs e)
        {
            memoEditColumn2Title.Font = buttonEditColumn2Font.Tag as Font; ;
            memoEditColumn2Title.Properties.Appearance.Font = memoEditColumn2Title.Font;
            memoEditColumn2Title.Properties.AppearanceDisabled.Font = memoEditColumn2Title.Font;
            memoEditColumn2Title.Properties.AppearanceFocused.Font = memoEditColumn2Title.Font;
            memoEditColumn2Title.Properties.AppearanceReadOnly.Font = memoEditColumn2Title.Font;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void colorEditColumn2ForeColor_EditValueChanged(object sender, EventArgs e)
        {
            memoEditColumn2Title.ForeColor = colorEditColumn2ForeColor.Color;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void ckColumn2EnableBanner_CheckedChanged(object sender, EventArgs e)
        {
            pbColumn2SelectedBanner.Enabled = ckColumn2EnableBanner.Checked;
            gridControlColumn2Banners.Enabled = ckColumn2EnableBanner.Checked;
            if (ckColumn2EnableBanner.Checked)
                ckColumn2EnableWidget.Checked = false;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void gridViewColumn2Banners_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Banner selectedBanner = null;
            if (gridViewColumn2Banners.FocusedRowHandle >= 0)
                selectedBanner = ConfigurationClasses.ListManager.Instance.Banners[gridViewColumn2Banners.GetDataSourceRowIndex(gridViewColumn2Banners.FocusedRowHandle)];
            pbColumn2SelectedBanner.Image = selectedBanner != null ? selectedBanner.Image : null;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void gridViewColumn2Banners_Click(object sender, EventArgs e)
        {
            Point pt = gridControlColumn2Banners.PointToClient(Control.MousePosition);

            if (gridViewColumn2Banners.CalcHitInfo(pt).RowHandle == gridViewColumn2Banners.FocusedRowHandle)
                gridViewColumn2Banners_FocusedRowChanged(null, null);
        }

        private void ckColumn2EnableWidget_CheckedChanged(object sender, EventArgs e)
        {
            pbColumn2SelectedWidget.Enabled = ckColumn2EnableWidget.Checked;
            gridControlColumn2Widgets.Enabled = ckColumn2EnableWidget.Checked;
            if (ckColumn2EnableWidget.Checked)
                ckColumn2EnableBanner.Checked = false;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void layoutViewColumn2Widgets_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Widget selectedWidget = null;
            if (layoutViewColumn2Widgets.FocusedRowHandle >= 0)
            {
                selectedWidget = ConfigurationClasses.ListManager.Instance.Widgets[layoutViewColumn2Widgets.GetDataSourceRowIndex(layoutViewColumn2Widgets.FocusedRowHandle)];
            }
            pbColumn2SelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void layoutViewColumn2Widgets_Click(object sender, EventArgs e)
        {
            Point pt = gridControlColumn2Widgets.PointToClient(Control.MousePosition);

            if (layoutViewColumn2Widgets.CalcHitInfo(pt).RowHandle == layoutViewColumn2Widgets.FocusedRowHandle)
                layoutViewColumn2Widgets_FocusedRowChanged(null, null);
        }
        #endregion

        #region Column 3
        private void ckColumn3EnableText_CheckedChanged(object sender, EventArgs e)
        {
            laColumn3Font.Enabled = ckColumn3EnableText.Checked;
            laColumn3ForeColor.Enabled = ckColumn3EnableText.Checked;
            memoEditColumn3Title.Enabled = ckColumn3EnableText.Checked;
            colorEditColumn3ForeColor.Enabled = ckColumn3EnableText.Checked;
            buttonEditColumn3Font.Enabled = ckColumn3EnableText.Checked;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void colorEditColumn3ForeColor_EditValueChanged(object sender, EventArgs e)
        {
            memoEditColumn3Title.Font = buttonEditColumn3Font.Tag as Font; ;
            memoEditColumn3Title.Properties.Appearance.Font = memoEditColumn3Title.Font;
            memoEditColumn3Title.Properties.AppearanceDisabled.Font = memoEditColumn3Title.Font;
            memoEditColumn3Title.Properties.AppearanceFocused.Font = memoEditColumn3Title.Font;
            memoEditColumn3Title.Properties.AppearanceReadOnly.Font = memoEditColumn3Title.Font;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void buttonEditColumn3Font_EditValueChanged(object sender, EventArgs e)
        {
            memoEditColumn3Title.ForeColor = colorEditColumn3ForeColor.Color;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void ckColumn3EnableBanner_CheckedChanged(object sender, EventArgs e)
        {
            pbColumn3SelectedBanner.Enabled = ckColumn3EnableBanner.Checked;
            gridControlColumn3Banners.Enabled = ckColumn3EnableBanner.Checked;
            if (ckColumn3EnableBanner.Checked)
                ckColumn3EnableWidget.Checked = false;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void gridViewColumn3Banners_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Banner selectedBanner = null;
            if (gridViewColumn3Banners.FocusedRowHandle >= 0)
                selectedBanner = ConfigurationClasses.ListManager.Instance.Banners[gridViewColumn3Banners.GetDataSourceRowIndex(gridViewColumn3Banners.FocusedRowHandle)];
            pbColumn3SelectedBanner.Image = selectedBanner != null ? selectedBanner.Image : null;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void gridViewColumn3Banners_Click(object sender, EventArgs e)
        {
            Point pt = gridControlColumn3Banners.PointToClient(Control.MousePosition);

            if (gridViewColumn3Banners.CalcHitInfo(pt).RowHandle == gridViewColumn3Banners.FocusedRowHandle)
                gridViewColumn3Banners_FocusedRowChanged(null, null);
        }

        private void ckColumn3EnableWidget_CheckedChanged(object sender, EventArgs e)
        {
            pbColumn3SelectedWidget.Enabled = ckColumn3EnableWidget.Checked;
            gridControlColumn3Widgets.Enabled = ckColumn3EnableWidget.Checked;
            if (ckColumn3EnableWidget.Checked)
                ckColumn3EnableBanner.Checked = false;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void layoutViewColumn3Widgets_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ConfigurationClasses.Widget selectedWidget = null;
            if (layoutViewColumn3Widgets.FocusedRowHandle >= 0)
            {
                selectedWidget = ConfigurationClasses.ListManager.Instance.Widgets[layoutViewColumn3Widgets.GetDataSourceRowIndex(layoutViewColumn3Widgets.FocusedRowHandle)];
            }
            pbColumn3SelectedWidget.Image = selectedWidget != null ? selectedWidget.Image : null;
            if (_allowToSave)
                _stateChanges = true;
        }

        private void layoutViewColumn3Widgets_Click(object sender, EventArgs e)
        {
            Point pt = gridControlColumn3Widgets.PointToClient(Control.MousePosition);

            if (layoutViewColumn3Widgets.CalcHitInfo(pt).RowHandle == layoutViewColumn3Widgets.FocusedRowHandle)
                layoutViewColumn3Widgets_FocusedRowChanged(null, null);
        }
        #endregion
        #endregion
    }
}
