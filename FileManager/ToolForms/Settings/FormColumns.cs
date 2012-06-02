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
                laColumn1.Font = new System.Drawing.Font(laColumn1.Font.FontFamily, laColumn1.Font.Size - 2, laColumn1.Font.Style);
                laColumn1Back.Font = new System.Drawing.Font(laColumn1Back.Font.FontFamily, laColumn1Back.Font.Size - 2, laColumn1Back.Font.Style);
                laColumn1Fore.Font = new System.Drawing.Font(laColumn1Fore.Font.FontFamily, laColumn1Fore.Font.Size - 2, laColumn1Fore.Font.Style);
                laColumn1HeaderFont.Font = new System.Drawing.Font(laColumn1HeaderFont.Font.FontFamily, laColumn1HeaderFont.Font.Size - 2, laColumn1HeaderFont.Font.Style);
                laColumn2.Font = new System.Drawing.Font(laColumn2.Font.FontFamily, laColumn2.Font.Size - 2, laColumn2.Font.Style);
                laColumn2Back.Font = new System.Drawing.Font(laColumn2Back.Font.FontFamily, laColumn2Back.Font.Size - 2, laColumn2Back.Font.Style);
                laColumn2Fore.Font = new System.Drawing.Font(laColumn2Fore.Font.FontFamily, laColumn2Fore.Font.Size - 2, laColumn2Fore.Font.Style);
                laColumn2HeaderFont.Font = new System.Drawing.Font(laColumn2HeaderFont.Font.FontFamily, laColumn2HeaderFont.Font.Size - 2, laColumn2HeaderFont.Font.Style);
                laColumn3.Font = new System.Drawing.Font(laColumn3.Font.FontFamily, laColumn3.Font.Size - 2, laColumn3.Font.Style);
                laColumn3Back.Font = new System.Drawing.Font(laColumn3Back.Font.FontFamily, laColumn3Back.Font.Size - 2, laColumn3Back.Font.Style);
                laColumn3Fore.Font = new System.Drawing.Font(laColumn3Fore.Font.FontFamily, laColumn3Fore.Font.Size - 2, laColumn3Fore.Font.Style);
                laColumn3HeaderFont.Font = new System.Drawing.Font(laColumn3HeaderFont.Font.FontFamily, laColumn3HeaderFont.Font.Size - 2, laColumn3HeaderFont.Font.Style);
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
            }
        }

        #region Base Methods
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
                    bool temp = _stateChanges;
                    tbColumn1.Text = _currentPage.ColumnTitles[0].Name;
                    colorEditColumn1Back.Color = _currentPage.ColumnTitles[0].BackgroundColor;
                    colorEditColumn1Fore.Color = _currentPage.ColumnTitles[0].ForeColor;
                    buttonEditColumn1HeaderFont.Tag = _currentPage.ColumnTitles[0].HeaderFont;
                    buttonEditColumn1HeaderFont.EditValue = FontToString(_currentPage.ColumnTitles[0].HeaderFont);
                    tbColumn2.Text = _currentPage.ColumnTitles[1].Name;
                    colorEditColumn2Back.Color = _currentPage.ColumnTitles[1].BackgroundColor;
                    colorEditColumn2Fore.Color = _currentPage.ColumnTitles[1].ForeColor;
                    buttonEditColumn2HeaderFont.Tag = _currentPage.ColumnTitles[1].HeaderFont;
                    buttonEditColumn2HeaderFont.EditValue = FontToString(_currentPage.ColumnTitles[1].HeaderFont);
                    tbColumn3.Text = _currentPage.ColumnTitles[2].Name;
                    colorEditColumn3Back.Color = _currentPage.ColumnTitles[2].BackgroundColor;
                    colorEditColumn3Fore.Color = _currentPage.ColumnTitles[2].ForeColor;
                    buttonEditColumn3HeaderFont.Tag = _currentPage.ColumnTitles[2].HeaderFont;
                    buttonEditColumn3HeaderFont.EditValue = FontToString(_currentPage.ColumnTitles[2].HeaderFont);
                    ckEnableColumnTitles.Checked = _currentPage.EnableColumnTitles;
                    ckApplyForAllColumnTitles.Checked = _currentPage.ApplyForAllColumnTitles;
                    _stateChanges = temp;
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
                column.Name = tbColumn1.Text;
                column.BackgroundColor = colorEditColumn1Back.Color;
                column.ForeColor = colorEditColumn1Fore.Color;
                column.HeaderFont = buttonEditColumn1HeaderFont.Tag as Font;
                _currentPage.ColumnTitles.Add(column);

                column = new BusinessClasses.ColumnTitle(_currentPage);
                column.ColumnOrder = 1;
                column.Name = tbColumn2.Text;
                column.BackgroundColor = colorEditColumn2Back.Color;
                column.ForeColor = colorEditColumn2Fore.Color;
                column.HeaderFont = buttonEditColumn2HeaderFont.Tag as Font;
                _currentPage.ColumnTitles.Add(column);

                column = new BusinessClasses.ColumnTitle(_currentPage);
                column.ColumnOrder = 2;
                column.Name = tbColumn3.Text;
                column.BackgroundColor = colorEditColumn3Back.Color;
                column.ForeColor = colorEditColumn3Fore.Color;
                column.HeaderFont = buttonEditColumn3HeaderFont.Tag as Font;
                _currentPage.ColumnTitles.Add(column);


                if (ckEnableColumnTitles.Checked && (string.IsNullOrEmpty(tbColumn1.Text.Trim()) || string.IsNullOrEmpty(tbColumn2.Text.Trim()) || string.IsNullOrEmpty(tbColumn3.Text.Trim())))
                {
                    AppManager.Instance.ShowWarning("You did not set all column titles and they will be disabled");
                    ckEnableColumnTitles.Checked = false;
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
            ckApllyForAllWindowsAppearance.Checked = this.Library.ApplyAppearanceForAllWindows;
            ckApllyForAllWindowsWidget.Checked = this.Library.ApplyWidgetForAllWindows;
            ckApllyForAllWindowsBanner.Checked = this.Library.ApplyBannerForAllWindows;
            xtraTabPageWindowPropertiesBanner.PageEnabled = System.IO.Directory.Exists(ConfigurationClasses.ListManager.Instance.BannerFolder);
            gridControlWindowBanners.DataSource = new BindingList<ConfigurationClasses.Banner>(ConfigurationClasses.ListManager.Instance.Banners);
            xtraTabPageWindowPropertiesWidget.PageEnabled = System.IO.Directory.Exists(ConfigurationClasses.ListManager.Instance.WidgetFolder);
            gridControlWindowWidgets.DataSource = new BindingList<ConfigurationClasses.Widget>(ConfigurationClasses.ListManager.Instance.Widgets);
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
                    sourceGrid = grColumn2;
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
            gbColumnTitle1.Enabled = ckEnableColumnTitles.Checked;
            gbColumnTitle2.Enabled = ckEnableColumnTitles.Checked;
            gbColumnTitle3.Enabled = ckEnableColumnTitles.Checked;
            ckApplyForAllColumnTitles.Enabled = ckEnableColumnTitles.Checked;
            _stateChanges = true;
        }

        private void ckApplyForAllColumnTitles_CheckedChanged(object sender, EventArgs e)
        {
            colorEditColumn2Back.Enabled = !ckApplyForAllColumnTitles.Checked;
            colorEditColumn2Fore.Enabled = !ckApplyForAllColumnTitles.Checked;
            buttonEditColumn2HeaderFont.Enabled = !ckApplyForAllColumnTitles.Checked;
            colorEditColumn3Back.Enabled = !ckApplyForAllColumnTitles.Checked;
            colorEditColumn3Fore.Enabled = !ckApplyForAllColumnTitles.Checked;
            buttonEditColumn3HeaderFont.Enabled = !ckApplyForAllColumnTitles.Checked;
            if (ckApplyForAllColumnTitles.Checked)
            {
                colorEditColumn2Back.Color = colorEditColumn1Back.Color;
                colorEditColumn2Fore.Color = colorEditColumn2Fore.Color;
                buttonEditColumn2HeaderFont.EditValue = buttonEditColumn1HeaderFont.EditValue;
                colorEditColumn3Back.Color = colorEditColumn1Back.Color;
                colorEditColumn3Fore.Color = colorEditColumn2Fore.Color;
                buttonEditColumn3HeaderFont.EditValue = buttonEditColumn1HeaderFont.EditValue;
            }
            _stateChanges = true;
        }

        private void colorEditColumn1Back_EditValueChanged(object sender, EventArgs e)
        {
            if (ckApplyForAllColumnTitles.Checked)
            {
                colorEditColumn2Back.Color = colorEditColumn1Back.Color;
                colorEditColumn3Back.Color = colorEditColumn1Back.Color;
            }
            _stateChanges = true;
        }

        private void colorEditColumn1Fore_EditValueChanged(object sender, EventArgs e)
        {
            if (ckApplyForAllColumnTitles.Checked)
            {
                colorEditColumn2Fore.Color = colorEditColumn1Fore.Color;
                colorEditColumn3Fore.Color = colorEditColumn1Fore.Color;
            }
            _stateChanges = true;
        }

        private void buttonEditColumn1HeaderFont_EditValueChanged(object sender, EventArgs e)
        {
            if (ckApplyForAllColumnTitles.Checked)
            {
                buttonEditColumn2HeaderFont.EditValue = buttonEditColumn1HeaderFont.EditValue;
                buttonEditColumn3HeaderFont.EditValue = buttonEditColumn1HeaderFont.EditValue;
            }
            _stateChanges = true;
        }
        #endregion
    }
}
