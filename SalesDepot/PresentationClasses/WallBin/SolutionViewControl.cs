﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SalesDepot.PresentationClasses.WallBin
{
    public partial class SolutionViewControl : UserControl, IWallBinView
    {
        private List<Viewers.IFileViewer> _fileViewers = new List<Viewers.IFileViewer>();
        private Viewers.IFileViewer _selectedFileViewer = null;
        private bool _searchSplitterPositionChanged = false;

        private DevComponents.DotNetBar.SuperTooltipInfo _targetToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by qualified target criteria", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _titleToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by title or file name", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _dateToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me search for files by date range", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

        public SolutionViewControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            navBarControlSearchTags.View = new CustomNavPaneViewInfoRegistrator();
            gridControlFiles.DataSource = new BindingList<Viewers.IFileViewer>(_fileViewers);
            LoadKeyWordFilterSet();
        }

        #region Methods
        public void ApplyView()
        {
            FormMain.Instance.ribbonBarHomeView.Enabled = true;
            FormMain.Instance.buttonItemSettingsMultitab.Enabled = true;
            FormMain.Instance.buttonItemSettingsQuickViewImages.Enabled = true;

            FormMain.Instance.ribbonBarEmailBin.Enabled = false;
            FormMain.Instance.ribbonBarViewSettings.Enabled = false;
            FormMain.Instance.ribbonBarHomeSearchMode.Enabled = !ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection;

            FormMain.Instance.comboBoxItemStations.Visible = false;
            FormMain.Instance.comboBoxItemPages.Visible = false;
            FormMain.Instance.ribbonBarStations.RecalcLayout();

            if (ConfigurationClasses.SettingsManager.Instance.SolutionTitleView)
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _titleToolTip);
            else if (ConfigurationClasses.SettingsManager.Instance.SolutionTagsView)
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _targetToolTip);
            else if (ConfigurationClasses.SettingsManager.Instance.SolutionDateView)
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _dateToolTip);
        }

        public void ApplySearchCriteria(BusinessClasses.LibraryFile[] files)
        {
            _selectedFileViewer = null;
            pnPreviewArea.Controls.Clear();
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Searching Library...";
                form.TopMost = true;
                if (files.Length > 0)
                    FormMain.Instance.ribbonControl.Enabled = false;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {

                    FormMain.Instance.Invoke((MethodInvoker)delegate()
                    {
                        foreach (Viewers.IFileViewer file in _fileViewers)
                            file.ReleaseResources();
                        _fileViewers.Clear();
                    });

                    foreach (BusinessClasses.LibraryFile file in files)
                    {
                        Viewers.IFileViewer viewer = null;
                        switch (file.Type)
                        {
                            case BusinessClasses.FileTypes.BuggyPresentation:
                            case BusinessClasses.FileTypes.FriendlyPresentation:
                            case BusinessClasses.FileTypes.Presentation:
                                if (file.PreviewContainer != null)
                                    file.PreviewContainer.GetPreviewImages();
                                FormMain.Instance.Invoke((MethodInvoker)delegate() { viewer = new Viewers.PowerPointViewer(file); });
                                break;
                            case BusinessClasses.FileTypes.Excel:
                                FormMain.Instance.Invoke((MethodInvoker)delegate()
                                {
                                    try
                                    { viewer = new Viewers.ExcelViewer(file); }
                                    catch { viewer = new Viewers.DefaultViewer(file); }
                                });
                                break;
                            case BusinessClasses.FileTypes.Word:
                                FormMain.Instance.Invoke((MethodInvoker)delegate()
                                {
                                    try
                                    { viewer = new Viewers.WordViewer(file); }
                                    catch { viewer = new Viewers.DefaultViewer(file); }
                                });
                                break;
                            case BusinessClasses.FileTypes.PDF:
                                FormMain.Instance.Invoke((MethodInvoker)delegate()
                                {
                                    try
                                    { viewer = new Viewers.PDFViewer(file); }
                                    catch { viewer = new Viewers.DefaultViewer(file); }
                                });
                                break;
                            case BusinessClasses.FileTypes.MediaPlayerVideo:
                                FormMain.Instance.Invoke((MethodInvoker)delegate()
                                {
                                    try
                                    { viewer = new Viewers.VideoViewer(file); }
                                    catch { viewer = new Viewers.DefaultViewer(file); }
                                });
                                break;
                            case BusinessClasses.FileTypes.Url:
                                FormMain.Instance.Invoke((MethodInvoker)delegate()
                                {
                                    try
                                    { viewer = new Viewers.WebViewer(file); }
                                    catch { viewer = new Viewers.DefaultViewer(file); }
                                });
                                break;
                            default:
                                FormMain.Instance.Invoke((MethodInvoker)delegate() { viewer = new Viewers.DefaultViewer(file); });
                                break;
                        }
                        Application.DoEvents();
                        if (viewer != null)
                            _fileViewers.Add(viewer);
                    }
                }));

                if (files.Length > 0)
                    form.Show();
                Application.DoEvents();

                thread.Start();

                while (thread.IsAlive)
                    Application.DoEvents();

                FormMain.Instance.ribbonControl.Enabled = true;
                form.Close();
            }
            pnPreviewArea.Controls.AddRange(_fileViewers.Select(x => x as Control).ToArray());
            if (!_searchSplitterPositionChanged)
            {
                splitContainerControlSerachResults.SplitterPosition = splitContainerControlSerachResults.Height / 2;
                _searchSplitterPositionChanged = true;
            }

            ((BindingList<Viewers.IFileViewer>)gridControlFiles.DataSource).ResetBindings();
            gridViewFiles.ExpandAllGroups();
            gridViewFiles.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewFiles_FocusedRowChanged);
            if (_fileViewers.Count > 0)
                gridViewFiles.FocusedRowHandle = 0;
            UpdatePreviewArea(0);
            barStaticItemFileNumber.Caption = _fileViewers.Count > 0 ? string.Format("{0} files", _fileViewers.Count.ToString("#,##0")) : string.Empty;
            barStaticItemFileNumber.Visibility = _fileViewers.Count > 0 ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
            gridViewFiles.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewFiles_FocusedRowChanged);
        }

        private void UpdateViewAccordingFileType(BusinessClasses.LibraryFile file)
        {
            barButtonItemOpenLink.Enabled = false;
            barButtonItemSave.Enabled = false;
            barButtonItemSaveAsPDF.Enabled = false;
            barButtonItemEmailLink.Enabled = false;
            barButtonItemAddSlide.Enabled = false;
            barButtonItemOpenQuickView.Enabled = false;
            barButtonItemPrintLink.Enabled = false;
            splitContainerControlSerachResults.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            if (file != null)
            {
                barButtonItemOpenLink.Enabled = true;
                switch (file.Type)
                {
                    case BusinessClasses.FileTypes.BuggyPresentation:
                    case BusinessClasses.FileTypes.FriendlyPresentation:
                    case BusinessClasses.FileTypes.Presentation:
                        barButtonItemSave.Enabled = true;
                        barButtonItemSaveAsPDF.Enabled = !InteropClasses.PowerPointHelper.Instance.Is2003 & true;
                        barButtonItemEmailLink.Enabled = true;
                        barButtonItemAddSlide.Enabled = true;
                        barButtonItemOpenQuickView.Enabled = true;
                        barButtonItemPrintLink.Enabled = true;
                        if (file.PreviewContainer != null)
                            if (file.PreviewContainer.Slides.Count > 0)
                                splitContainerControlSerachResults.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                        break;
                    case BusinessClasses.FileTypes.Excel:
                    case BusinessClasses.FileTypes.PDF:
                    case BusinessClasses.FileTypes.Word:
                        barButtonItemSave.Enabled = true;
                        barButtonItemEmailLink.Enabled = true;
                        barButtonItemPrintLink.Enabled = true;
                        splitContainerControlSerachResults.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                        break;
                    case BusinessClasses.FileTypes.Other:
                        barButtonItemSave.Enabled = true;
                        barButtonItemEmailLink.Enabled = true;
                        barButtonItemPrintLink.Enabled = true;
                        break;
                    case BusinessClasses.FileTypes.MediaPlayerVideo:
                    case BusinessClasses.FileTypes.QuickTimeVideo:
                        barButtonItemEmailLink.Enabled = true;
                        splitContainerControlSerachResults.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                        break;
                    case BusinessClasses.FileTypes.Url:
                        splitContainerControlSerachResults.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                        break;
                }
            }
            gridViewFiles.MakeRowVisible(gridViewFiles.FocusedRowHandle, true);
        }

        private void UpdatePreviewArea(int rowHandele)
        {
            pnEmptyPreview.BringToFront();
            if (_selectedFileViewer != null)
            {
                (_selectedFileViewer as Control).Visible = false;

                Viewers.VideoViewer viewer = _selectedFileViewer as Viewers.VideoViewer;
                if (viewer != null)
                    viewer.Stop();
            }
            _selectedFileViewer = rowHandele < _fileViewers.Count && rowHandele >= 0 ? _fileViewers[gridViewFiles.GetDataSourceRowIndex(rowHandele)] : null;
            if (_selectedFileViewer != null)
            {
                UpdateViewAccordingFileType(_selectedFileViewer.File);
                (_selectedFileViewer as Control).Visible = true;
                (_selectedFileViewer as Control).BringToFront();

                Viewers.VideoViewer viewer = _selectedFileViewer as Viewers.VideoViewer;
                if (viewer != null)
                {
                    viewer.Play();
                }
            }
            pnPreviewArea.BringToFront();
        }

        private void Search()
        {
            if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer != null)
            {
                BusinessClasses.LibraryFile[] files = new BusinessClasses.LibraryFile[] { };
                if (FormMain.Instance.buttonItemHomeSearchByTags.Checked)
                {
                    files = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByTags(GetSearhTags());
                    if (files.Where(x => x.Type == BusinessClasses.FileTypes.BuggyPresentation || x.Type == BusinessClasses.FileTypes.FriendlyPresentation || x.Type == BusinessClasses.FileTypes.Presentation).Count() > 25)
                    {
                        AppManager.Instance.ShowWarning("Only the first 25 Results will be displayed.\nNarrow your Search Criteria to display a more qualified list of files...");
                        files = files.Take(25).ToArray();
                    }
                }
                else if (FormMain.Instance.buttonItemHomeSearchByFileName.Checked)
                {
                    string criteria = textEditSearchByFiles.EditValue != null ? textEditSearchByFiles.EditValue.ToString().ToLower() : string.Empty;
                    if (checkEditAllFiles.Checked)
                    {
                        files = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.Other);
                    }
                    else
                    {
                        List<BusinessClasses.LibraryFile> filesByName = new List<BusinessClasses.LibraryFile>();
                        if (checkEditExcel.Checked)
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.Excel));
                        if (checkEditFolders.Checked)
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.Folder));
                        if (checkEditNetwork.Checked)
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.Network));
                        if (checkEditPDF.Checked)
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.PDF));
                        if (checkEditPowerPoint.Checked)
                        {
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.BuggyPresentation));
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.FriendlyPresentation));
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.Presentation));
                        }
                        if (checkEditVideo.Checked)
                        {
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.MediaPlayerVideo));
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.QuickTimeVideo));
                        }
                        if (checkEditWeb.Checked)
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.Url));
                        if (checkEditWord.Checked)
                            filesByName.AddRange(PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, BusinessClasses.FileTypes.Word));
                        files = filesByName.ToArray();
                    }
                    if (files.Where(x => x.Type == BusinessClasses.FileTypes.BuggyPresentation || x.Type == BusinessClasses.FileTypes.FriendlyPresentation || x.Type == BusinessClasses.FileTypes.Presentation).Count() > 25)
                    {
                        AppManager.Instance.ShowWarning("Only the first 25 Results will be displayed.\nNarrow your Search Criteria to display a more qualified list of files...");
                        files = files.Take(25).ToArray();
                    }
                    else if (files.Length == 0)
                    {
                        AppManager.Instance.ShowWarning("No files meet this search criteria...");
                    }
                }
                else if (FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked)
                {
                    DateTime startDate = DateTime.Now;
                    DateTime endDate = DateTime.Now;
                    string messageText = string.Empty;
                    if (rbLastDay.Checked)
                    {
                        startDate = DateTime.Now.AddDays(-1);
                        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                        endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                        messageText = "Too many New files have been added in the past day.\nOnly the first 25 results will be displayed...";
                    }
                    else if (rbLastHalfMonth.Checked)
                    {
                        startDate = DateTime.Now.AddDays(-15);
                        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                        endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                        messageText = "Too many New files have been added in the past 15 days.\nOnly the first 25 results will be displayed...";
                    }
                    else if (rbLastMonth.Checked)
                    {
                        startDate = DateTime.Now.AddMonths(-1);
                        startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                        endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                        messageText = "Too many New files have been added in the past 30 days.\nOnly the first 25 results will be displayed...";
                    }
                    else if (rbDateRange.Checked)
                    {
                        startDate = new DateTime(dateEditDateRangeStart.DateTime.Year, dateEditDateRangeStart.DateTime.Month, dateEditDateRangeStart.DateTime.Day, 0, 0, 0);
                        endDate = new DateTime(dateEditDateRangeEnd.DateTime.Year, dateEditDateRangeEnd.DateTime.Month, dateEditDateRangeEnd.DateTime.Day, 23, 59, 59);
                        messageText = "There are many new files added for this date range.\nOnly the first 25 results will be displayed...";
                    }
                    files = PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.Package.SearchByDate(startDate, endDate);
                    if (files.Where(x => x.Type == BusinessClasses.FileTypes.BuggyPresentation || x.Type == BusinessClasses.FileTypes.FriendlyPresentation || x.Type == BusinessClasses.FileTypes.Presentation).Count() > 25)
                    {
                        AppManager.Instance.ShowWarning(messageText);
                        files = files.Take(25).ToArray();
                    }
                    else if (files.Length == 0)
                    {
                        AppManager.Instance.ShowWarning("There are no new files available for this date range...");
                    }
                }
                ApplySearchCriteria(files);
            }
        }

        public void ClearSolutionControl()
        {
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup1.Items)
                item.CheckState = CheckState.Unchecked;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup2.Items)
                item.CheckState = CheckState.Unchecked;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup3.Items)
                item.CheckState = CheckState.Unchecked;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup4.Items)
                item.CheckState = CheckState.Unchecked;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup5.Items)
                item.CheckState = CheckState.Unchecked;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup6.Items)
                item.CheckState = CheckState.Unchecked;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup7.Items)
                item.CheckState = CheckState.Unchecked;

            rbLastDay.Checked = true;
            rbLastHalfMonth.Checked = false;
            rbLastMonth.Checked = false;
            rbDateRange.Checked = false;
            dateEditDateRangeStart.DateTime = DateTime.Now;
            dateEditDateRangeEnd.DateTime = DateTime.Now;

            textEditSearchByFiles.EditValue = null;

            LoadKeyWordFilterSet();

            ApplySearchCriteria(new BusinessClasses.LibraryFile[] { });
            UpdateSearchButtonStatus();
            navBarControlSearchTags.View = new CustomNavPaneViewInfoRegistrator();
        }

        public void UpdateSearchButtonStatus()
        {
            bool enableButton = false;
            if (FormMain.Instance.buttonItemHomeSearchByTags.Checked)
            {
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup1.Items)
                    if (item.CheckState == CheckState.Checked)
                    {
                        enableButton = true;
                        break;
                    }
                if (!enableButton)
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup2.Items)
                        if (item.CheckState == CheckState.Checked)
                        {
                            enableButton = true;
                            break;
                        }
                if (!enableButton)
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup3.Items)
                        if (item.CheckState == CheckState.Checked)
                        {
                            enableButton = true;
                            break;
                        }
                if (!enableButton)
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup4.Items)
                        if (item.CheckState == CheckState.Checked)
                        {
                            enableButton = true;
                            break;
                        }
                if (!enableButton)
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup5.Items)
                        if (item.CheckState == CheckState.Checked)
                        {
                            enableButton = true;
                            break;
                        }
                if (!enableButton)
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup6.Items)
                        if (item.CheckState == CheckState.Checked)
                        {
                            enableButton = true;
                            break;
                        }
                if (!enableButton)
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup7.Items)
                        if (item.CheckState == CheckState.Checked)
                        {
                            enableButton = true;
                            break;
                        }
            }
            else if (FormMain.Instance.buttonItemHomeSearchByFileName.Checked)
                enableButton = textEditSearchByFiles.EditValue != null;
            else
                enableButton = true;
            barButtonItemSearch.Enabled = enableButton;
            barButtonItemClear.Enabled = enableButton;
        }

        public void InsertSlide()
        {
            Viewers.PowerPointViewer viewer = _selectedFileViewer as Viewers.PowerPointViewer;
            if (viewer != null)
                viewer.InsertSlide();
        }
        #endregion

        #region Search Tags Methods and Event Handlers
        private void InitSearchTagsGroups()
        {
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 0)
            {
                navBarGroup1.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0];
                navBarGroup1.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0].Description;
                navBarGroup1.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0].Logo;
                checkedListBoxControlGroup1.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[0].Tags.ToArray());
            }
            else
                navBarGroup1.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 1)
            {
                navBarGroup2.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1];
                navBarGroup2.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1].Description;
                navBarGroup2.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1].Logo;
                checkedListBoxControlGroup2.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[1].Tags.ToArray());
            }
            else
                navBarGroup2.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 2)
            {
                navBarGroup3.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2];
                navBarGroup3.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2].Description;
                navBarGroup3.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2].Logo;
                checkedListBoxControlGroup3.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[2].Tags.ToArray());
            }
            else
                navBarGroup3.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 3)
            {
                navBarGroup4.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3];
                navBarGroup4.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3].Description;
                navBarGroup4.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3].Logo;
                checkedListBoxControlGroup4.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[3].Tags.ToArray());
            }
            else
                navBarGroup4.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 4)
            {
                navBarGroup5.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4];
                navBarGroup5.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4].Description;
                navBarGroup5.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4].Logo;
                checkedListBoxControlGroup5.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[4].Tags.ToArray());
            }
            else
                navBarGroup5.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 5)
            {
                navBarGroup6.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5];
                navBarGroup6.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5].Description;
                navBarGroup6.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5].Logo;
                checkedListBoxControlGroup6.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[5].Tags.ToArray());
            }
            else
                navBarGroup6.Visible = false;
            if (ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups.Count > 6)
            {
                navBarGroup7.Tag = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6];
                navBarGroup7.Caption = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6].Description;
                navBarGroup7.SmallImage = ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6].Logo;
                checkedListBoxControlGroup7.Items.AddRange(ConfigurationClasses.ListManager.Instance.SearchTags.SearchGroups[6].Tags.ToArray());
            }
            else
                navBarGroup7.Visible = false;
        }

        private BusinessClasses.LibraryFileSearchTags GetSearhTags()
        {
            BusinessClasses.LibraryFileSearchTags searchTags = new BusinessClasses.LibraryFileSearchTags();

            if (checkedListBoxControlGroup1.CheckedItemsCount > 0)
            {
                ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                group.Name = (navBarGroup1.Tag as ConfigurationClasses.SearchGroup).Name;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup1.Items)
                    if (item.CheckState == CheckState.Checked)
                        group.Tags.Add(item.Value.ToString());
                searchTags.SearchGroups.Add(group);
            }
            if (checkedListBoxControlGroup2.CheckedItemsCount > 0)
            {
                ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                group.Name = (navBarGroup2.Tag as ConfigurationClasses.SearchGroup).Name; group.Name = navBarGroup2.Caption;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup2.Items)
                    if (item.CheckState == CheckState.Checked)
                        group.Tags.Add(item.Value.ToString());
                searchTags.SearchGroups.Add(group);
            }
            if (checkedListBoxControlGroup3.CheckedItemsCount > 0)
            {
                ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                group.Name = (navBarGroup3.Tag as ConfigurationClasses.SearchGroup).Name;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup3.Items)
                    if (item.CheckState == CheckState.Checked)
                        group.Tags.Add(item.Value.ToString());
                searchTags.SearchGroups.Add(group);
            }
            if (checkedListBoxControlGroup4.CheckedItemsCount > 0)
            {
                ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                group.Name = (navBarGroup4.Tag as ConfigurationClasses.SearchGroup).Name;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup4.Items)
                    if (item.CheckState == CheckState.Checked)
                        group.Tags.Add(item.Value.ToString());
                searchTags.SearchGroups.Add(group);
            }
            if (checkedListBoxControlGroup5.CheckedItemsCount > 0)
            {
                ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                group.Name = (navBarGroup5.Tag as ConfigurationClasses.SearchGroup).Name;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup5.Items)
                    if (item.CheckState == CheckState.Checked)
                        group.Tags.Add(item.Value.ToString());
                searchTags.SearchGroups.Add(group);
            }
            if (checkedListBoxControlGroup6.CheckedItemsCount > 0)
            {
                ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                group.Name = (navBarGroup6.Tag as ConfigurationClasses.SearchGroup).Name;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup6.Items)
                    if (item.CheckState == CheckState.Checked)
                        group.Tags.Add(item.Value.ToString());
                searchTags.SearchGroups.Add(group);
            }
            if (checkedListBoxControlGroup7.CheckedItemsCount > 0)
            {
                ConfigurationClasses.SearchGroup group = new ConfigurationClasses.SearchGroup();
                group.Name = (navBarGroup7.Tag as ConfigurationClasses.SearchGroup).Name;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlGroup7.Items)
                    if (item.CheckState == CheckState.Checked)
                        group.Tags.Add(item.Value.ToString());
                searchTags.SearchGroups.Add(group);
            }
            return searchTags;
        }

        private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            UpdateSearchButtonStatus();
        }
        #endregion

        #region Key Word Methods and Event Handlers
        private bool _allowToSaveKeyWordFilters = false;
        private void LoadKeyWordFilterSet()
        {
            _allowToSaveKeyWordFilters = false;
            checkEditAllFiles.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.AllFiles;
            checkEditPowerPoint.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.PowerPoint;
            checkEditPDF.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.PDF;
            checkEditExcel.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Excel;
            checkEditWord.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Word;
            checkEditVideo.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Video;
            checkEditWeb.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Url;
            checkEditNetwork.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Network;
            checkEditFolders.Checked = ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Folder;
            _allowToSaveKeyWordFilters = true;
        }

        private void SaveKeyWordFilterSet()
        {
            if (ConfigurationClasses.SettingsManager.Instance.LastViewed)
            {
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.AllFiles = checkEditAllFiles.Checked;
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.PowerPoint = checkEditPowerPoint.Checked;
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.PDF = checkEditPDF.Checked;
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Excel = checkEditExcel.Checked;
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Word = checkEditWord.Checked;
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Video = checkEditVideo.Checked;
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Url = checkEditWeb.Checked;
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Network = checkEditNetwork.Checked;
                ConfigurationClasses.SettingsManager.Instance.KeyWordFilters.Folder = checkEditFolders.Checked;
                ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            }
        }

        private void checkEditKeyWord_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSaveKeyWordFilters)
                SaveKeyWordFilterSet();
        }

        private void checkEditAllFiles_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditAllFiles.Checked)
            {
                bool allowToSaveKeyWordFiltersOld = _allowToSaveKeyWordFilters;
                _allowToSaveKeyWordFilters = false;
                checkEditExcel.Checked = true;
                checkEditFolders.Checked = true;
                checkEditNetwork.Checked = true;
                checkEditPDF.Checked = true;
                checkEditPowerPoint.Checked = true;
                checkEditVideo.Checked = true;
                checkEditWeb.Checked = true;
                checkEditWord.Checked = true;
                checkEditExcel.Enabled = false;
                checkEditFolders.Enabled = false;
                checkEditNetwork.Enabled = false;
                checkEditPDF.Enabled = false;
                checkEditPowerPoint.Enabled = false;
                checkEditVideo.Enabled = false;
                checkEditWeb.Enabled = false;
                checkEditWord.Enabled = false;
                _allowToSaveKeyWordFilters = allowToSaveKeyWordFiltersOld;
            }
            else
            {
                checkEditExcel.Enabled = true;
                checkEditFolders.Enabled = true;
                checkEditNetwork.Enabled = true;
                checkEditPDF.Enabled = true;
                checkEditPowerPoint.Enabled = true;
                checkEditVideo.Enabled = true;
                checkEditWeb.Enabled = true;
                checkEditWord.Enabled = true;
            }
            checkEditKeyWord_CheckedChanged(null, null);
        }

        private void textEditSearchByFiles_EditValueChanged(object sender, EventArgs e)
        {
            UpdateSearchButtonStatus();
        }

        private void textEditSearchByFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textEditSearchByFiles.EditValue != null)
                Search();
        }
        #endregion

        #region Date Range Methods and Event Handlers
        private void radioButtonDateRange_CheckedChanged(object sender, EventArgs e)
        {
            dateEditDateRangeStart.Enabled = rbDateRange.Checked;
            dateEditDateRangeEnd.Enabled = rbDateRange.Checked;
        }
        #endregion

        #region Preview Area Methods and Event Handlers
        #region Toolbar Buttons Clicks
        private void barButtonItemOpenLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Open();
        }

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Save();
        }

        private void barButtonItemSaveAsPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Viewers.PowerPointViewer viewer = _selectedFileViewer as Viewers.PowerPointViewer;
            if (viewer != null)
                viewer.SaveAsPDF();
        }

        private void barButtonItemEmailLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Email();
        }

        private void barButtonItemPrintLink_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_selectedFileViewer != null)
                _selectedFileViewer.Print();
        }

        private void barButtonItemAddSlide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertSlide();
        }

        private void barButtonItemOpenQuickView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Viewers.PowerPointViewer viewer = _selectedFileViewer as Viewers.PowerPointViewer;
            if (viewer != null)
                viewer.OpenInQuickView();
        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Search();
        }

        private void pbSearchByFiles_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void barButtonItemClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearSolutionControl();
        }
        #endregion

        #region Grid Event Handlers
        private void gridViewFiles_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0 && _fileViewers.Count > e.RowHandle)
            {
                BusinessClasses.LibraryFile file = _fileViewers[gridViewFiles.GetDataSourceRowIndex(e.RowHandle)].File;
                switch (file.CriteriaOverlap)
                {
                    case "meet ALL of your Search Criteria":
                        e.Appearance.BackColor = Color.FromArgb(223, 253, 234);
                        break;
                    case "meet SOME of your Search Criteria":
                        e.Appearance.BackColor = Color.FromArgb(255, 223, 217);
                        break;
                }
            }
        }

        private void gridViewFiles_MouseMove(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = gridViewFiles.CalcHitInfo(e.X, e.Y);
            if (hi.InRowCell)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
        }

        private void gridViewFiles_MouseLeave(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void gridViewFiles_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int rowHandele = e.FocusedRowHandle;
            UpdatePreviewArea(rowHandele);
        }

        private void gridViewFiles_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (_selectedFileViewer != null && e.Clicks == 2)
            {
                _selectedFileViewer.Open();
            }
        }

        #endregion
        #endregion

        #region Control Event Handlers
        private void SolutionViewControl_Load(object sender, EventArgs e)
        {
            InitSearchTagsGroups();
        }

        private void toolTipController_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControlFiles)
                return;
            DevExpress.Utils.ToolTipControlInfo info = e.Info;
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = gridControlFiles.GetViewAt(e.ControlMousePosition) as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null)
                    return;
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.InRowCell)
                {
                    if (hi.RowHandle >= 0 && _fileViewers.Count > hi.RowHandle)
                    {
                        BusinessClasses.LibraryFile file = _fileViewers[gridViewFiles.GetDataSourceRowIndex(hi.RowHandle)].File;
                        if (file != null)
                        {
                            List<string> toolTip = new List<string>();
                            toolTip.Add(file.NameWithExtension);
                            toolTip.Add("Added: " + file.AddDate.ToString("M/dd/yy h:mm:ss tt"));
                            if (file.ExpirationDateOptions.EnableExpirationDate && file.ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
                                toolTip.Add("Expires: " + file.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt"));
                            else
                                toolTip.Add("Expires: No Expiration Date");
                            info = new DevExpress.Utils.ToolTipControlInfo(new DevExpress.XtraGrid.Views.Base.CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), string.Join(Environment.NewLine, toolTip.ToArray()));
                        }
                    }
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        public void buttonItemHomeSearchMode_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem buttonItem = sender as DevComponents.DotNetBar.ButtonItem;
            if (buttonItem != null)
            {
                FormMain.Instance.buttonItemHomeSearchByTags.Checked = false;
                FormMain.Instance.buttonItemHomeSearchByFileName.Checked = false;
                FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked = false;
                buttonItem.Checked = true;
            }
        }

        public void buttonItemHomeSearchMode_CheckedChanged(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem buttonItem = sender as DevComponents.DotNetBar.ButtonItem;
            if (buttonItem != null)
            {
                if (buttonItem.Checked)
                {
                    ClearSolutionControl();
                    if (buttonItem == FormMain.Instance.buttonItemHomeSearchByTags)
                    {
                        xtraTabControlSolutionModes.SelectedTabPage = xtraTabPageSearchTags;
                        FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _targetToolTip);
                    }
                    else if (buttonItem == FormMain.Instance.buttonItemHomeSearchByFileName)
                    {
                        xtraTabControlSolutionModes.SelectedTabPage = xtraTabPageKeyWords;
                        FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _titleToolTip);
                    }
                    else if (buttonItem == FormMain.Instance.buttonItemHomeSearchRecentFiles)
                    {
                        xtraTabControlSolutionModes.SelectedTabPage = xtraTabPageAddDate;
                        FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _dateToolTip);
                    }
                    ConfigurationClasses.SettingsManager.Instance.SolutionTitleView = FormMain.Instance.buttonItemHomeSearchByFileName.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionTagsView = FormMain.Instance.buttonItemHomeSearchByTags.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SolutionDateView = FormMain.Instance.buttonItemHomeSearchRecentFiles.Checked;
                    ConfigurationClasses.SettingsManager.Instance.SaveSettings();
                }
            }
        }
        #endregion
    }
}
