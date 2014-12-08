using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using SalesDepot.BusinessClasses;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.InteropClasses;
using SalesDepot.PresentationClasses.Viewers;
using SalesDepot.PresentationClasses.WallBin.Decorators;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;

namespace SalesDepot.TabPages
{
	public partial class TabSearchControl : UserControl, IController
	{
		private readonly SuperTooltipInfo _dateToolTip = new SuperTooltipInfo("HELP", "", "Help me search for files by date range", null, null, eTooltipColor.Gray);
		private readonly List<IFileViewer> _fileViewers = new List<IFileViewer>();

		private readonly SuperTooltipInfo _targetToolTip = new SuperTooltipInfo("HELP", "", "Help me search for files by qualified target criteria", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _titleToolTip = new SuperTooltipInfo("HELP", "", "Help me search for files by title or file name", null, null, eTooltipColor.Gray);
		private bool _searchSplitterPositionChanged;
		private IFileViewer _selectedFileViewer;

		public TabSearchControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			gridControlFiles.DataSource = new BindingList<IFileViewer>(_fileViewers);
			LoadKeyWordFilterSet();
			NeedToUpdate = true;
		}

		#region IController Members
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public void InitController()
		{
			FormMain.Instance.buttonItemSearchHelp.Click += buttonItemSearchHelp_Click;
			if (SettingsManager.Instance.UseRemoteConnection)
			{
				xtraTabControlSolutionModes.ShowTabHeader = DefaultBoolean.False;
				xtraTabControlSolutionModes.SelectedTabPage = xtraTabPageKeyWords;
			}
			else
			{
				xtraTabControlSolutionModes.ShowTabHeader = DefaultBoolean.True;
				if (SettingsManager.Instance.SolutionTagsView)
					xtraTabControlSolutionModes.SelectedTabPage = xtraTabPageSearchTags;
				else if (SettingsManager.Instance.SolutionTitleView)
					xtraTabControlSolutionModes.SelectedTabPage = xtraTabPageKeyWords;
				else if (SettingsManager.Instance.SolutionDateView)
					xtraTabControlSolutionModes.SelectedTabPage = xtraTabPageAddDate;
				else
					xtraTabControlSolutionModes.SelectedTabPage = xtraTabPageSearchTags;
			}

			if (SettingsManager.Instance.SolutionTitleView)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _titleToolTip);
			else if (SettingsManager.Instance.SolutionTagsView)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _targetToolTip);
			else if (SettingsManager.Instance.SolutionDateView)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _dateToolTip);

			InitSearchTagsGroups();

			xtraTabControlSolutionModes.SelectedPageChanged += xtraTabControlSolutionModes_SelectedPageChanged;
			barButtonItemQuickSiteAdd.Visibility = barButtonItemQuickSiteEmail.Visibility = SettingsManager.Instance.QBuilderSettings.AvailableHosts.Count > 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			AppManager.Instance.ActivityManager.AddUserActivity("Search selected");
			SettingsManager.Instance.SearchView = true;
			SettingsManager.Instance.SaveSettings();
		}
		#endregion

		#region Ribbon Button's Click Event Handlers
		public void buttonItemSearchHelp_Click(object sender, EventArgs e)
		{
			if (SettingsManager.Instance.SolutionTitleView)
				AppManager.Instance.HelpManager.OpenHelpLink("title");
			else if (SettingsManager.Instance.SolutionTagsView)
				AppManager.Instance.HelpManager.OpenHelpLink("target");
			else if (SettingsManager.Instance.SolutionDateView)
				AppManager.Instance.HelpManager.OpenHelpLink("date");
		}
		#endregion

		#region Methods
		public void ApplySearchCriteria(ILibraryLink[] files)
		{
			_selectedFileViewer = null;
			pnPreviewArea.Controls.Clear();
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Searching Library...";
				form.TopMost = true;
				if (files.Length > 0)
					FormMain.Instance.ribbonControl.Enabled = false;
				var thread = new Thread(delegate()
											{
												FormMain.Instance.Invoke((MethodInvoker)delegate
																							{
																								foreach (IFileViewer file in _fileViewers)
																									file.ReleaseResources();
																								_fileViewers.Clear();
																							});

												foreach (LibraryLink file in files)
												{
													IFileViewer viewer = null;
													switch (file.Type)
													{
														case FileTypes.Presentation:
															if (file.PreviewContainer != null)
																file.PreviewContainer.GetPreviewImages();
															FormMain.Instance.Invoke((MethodInvoker)delegate { viewer = new PowerPointViewer(file); });
															break;
														case FileTypes.Excel:
															FormMain.Instance.Invoke((MethodInvoker)delegate
																										{
																											try
																											{
																												viewer = new ExcelViewer(file);
																											}
																											catch
																											{
																												viewer = new DefaultViewer(file);
																											}
																										});
															break;
														case FileTypes.Word:
															FormMain.Instance.Invoke((MethodInvoker)delegate
																										{
																											try
																											{
																												viewer = new WordViewer(file);
																											}
																											catch
																											{
																												viewer = new DefaultViewer(file);
																											}
																										});
															break;
														case FileTypes.PDF:
															FormMain.Instance.Invoke((MethodInvoker)delegate
																										{
																											try
																											{
																												viewer = new PDFViewer(file);
																											}
																											catch
																											{
																												viewer = new DefaultViewer(file);
																											}
																										});
															break;
														case FileTypes.MediaPlayerVideo:
															FormMain.Instance.Invoke((MethodInvoker)delegate
																										{
																											try
																											{
																												viewer = new VideoViewer(file);
																											}
																											catch
																											{
																												viewer = new DefaultViewer(file);
																											}
																										});
															break;
														case FileTypes.Url:
															FormMain.Instance.Invoke((MethodInvoker)delegate
																										{
																											try
																											{
																												viewer = new WebViewer(file);
																											}
																											catch
																											{
																												viewer = new DefaultViewer(file);
																											}
																										});
															break;
														default:
															FormMain.Instance.Invoke((MethodInvoker)delegate { viewer = new DefaultViewer(file); });
															break;
													}
													Application.DoEvents();
													if (viewer != null)
														_fileViewers.Add(viewer);
												}
											});

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

			((BindingList<IFileViewer>)gridControlFiles.DataSource).ResetBindings();
			gridViewFiles.ExpandAllGroups();
			gridViewFiles.FocusedRowChanged -= gridViewFiles_FocusedRowChanged;
			if (_fileViewers.Count > 0)
				gridViewFiles.FocusedRowHandle = 0;
			UpdatePreviewArea(0);
			barStaticItemFileNumber.Caption = _fileViewers.Count > 0 ? string.Format("{0} files", _fileViewers.Count.ToString("#,##0")) : string.Empty;
			barStaticItemFileNumber.Visibility = _fileViewers.Count > 0 ? BarItemVisibility.Always : BarItemVisibility.Never;
			gridViewFiles.FocusedRowChanged += gridViewFiles_FocusedRowChanged;
		}

		private void UpdateViewAccordingFileType(LibraryLink file)
		{
			barButtonItemOpenLink.Enabled = false;
			barButtonItemSave.Enabled = false;
			barButtonItemSaveAsPDF.Enabled = false;
			barButtonItemEmailLink.Enabled = false;
			barButtonItemAddSlide.Enabled = false;
			barButtonItemOpenQuickView.Enabled = false;
			barButtonItemPrintLink.Enabled = false;
			splitContainerControlSerachResults.PanelVisibility = SplitPanelVisibility.Panel1;
			if (file != null)
			{
				barButtonItemOpenLink.Enabled = true;
				switch (file.Type)
				{
					case FileTypes.Presentation:
						barButtonItemSave.Enabled = true;
						barButtonItemSaveAsPDF.Enabled = !PowerPointHelper.Instance.Is2003 & true;
						barButtonItemEmailLink.Enabled = true;
						barButtonItemAddSlide.Enabled = true;
						barButtonItemOpenQuickView.Enabled = true;
						barButtonItemPrintLink.Enabled = true;
						if (file.PreviewContainer != null)
							if (file.PreviewContainer.Slides.Count > 0)
								splitContainerControlSerachResults.PanelVisibility = SplitPanelVisibility.Both;
						break;
					case FileTypes.Excel:
					case FileTypes.PDF:
					case FileTypes.Word:
						barButtonItemSave.Enabled = true;
						barButtonItemEmailLink.Enabled = true;
						barButtonItemPrintLink.Enabled = true;
						splitContainerControlSerachResults.PanelVisibility = SplitPanelVisibility.Both;
						break;
					case FileTypes.Other:
						barButtonItemSave.Enabled = true;
						barButtonItemEmailLink.Enabled = true;
						barButtonItemPrintLink.Enabled = true;
						break;
					case FileTypes.MediaPlayerVideo:
					case FileTypes.QuickTimeVideo:
						barButtonItemEmailLink.Enabled = true;
						splitContainerControlSerachResults.PanelVisibility = SplitPanelVisibility.Both;
						break;
					case FileTypes.Url:
						splitContainerControlSerachResults.PanelVisibility = SplitPanelVisibility.Both;
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

				var viewer = _selectedFileViewer as VideoViewer;
				if (viewer != null)
					viewer.Stop();
			}
			_selectedFileViewer = rowHandele < _fileViewers.Count && rowHandele >= 0 ? _fileViewers[gridViewFiles.GetDataSourceRowIndex(rowHandele)] : null;
			if (_selectedFileViewer != null)
			{
				UpdateViewAccordingFileType(_selectedFileViewer.File);
				(_selectedFileViewer as Control).Visible = true;
				(_selectedFileViewer as Control).BringToFront();

				var viewer = _selectedFileViewer as VideoViewer;
				if (viewer != null)
				{
					viewer.Play();
				}
			}
			pnPreviewArea.BringToFront();
		}

		private void Search()
		{
			if (DecoratorManager.Instance.ActivePackageViewer != null)
			{
				var files = new ILibraryLink[] { };
				if (xtraTabControlSolutionModes.SelectedTabPage == xtraTabPageSearchTags)
				{
					files = DecoratorManager.Instance.ActivePackageViewer.Package.SearchByTags(GetSearhTags());
					if (files.Count(x => x.Type == FileTypes.Presentation) > 25)
					{
						AppManager.Instance.ShowWarning("Only the first 25 Results will be displayed.\nNarrow your Search Criteria to display a more qualified list of files...");
						files = files.Take(25).ToArray();
					}
				}
				else if (xtraTabControlSolutionModes.SelectedTabPage == xtraTabPageKeyWords)
				{
					string criteria = textEditSearchByFiles.EditValue != null ? textEditSearchByFiles.EditValue.ToString().ToLower() : string.Empty;
					if (checkEditAllFiles.Checked)
					{
						files = DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.Other);
					}
					else
					{
						var filesByName = new List<ILibraryLink>();
						if (checkEditExcel.Checked)
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.Excel));
						if (checkEditFolders.Checked)
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.Folder));
						if (checkEditNetwork.Checked)
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.Network));
						if (checkEditPDF.Checked)
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.PDF));
						if (checkEditPowerPoint.Checked)
						{
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.Presentation));
						}
						if (checkEditVideo.Checked)
						{
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.MediaPlayerVideo));
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.QuickTimeVideo));
						}
						if (checkEditWeb.Checked)
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.Url));
						if (checkEditWord.Checked)
							filesByName.AddRange(DecoratorManager.Instance.ActivePackageViewer.Package.SearchByName(criteria, checkEditSearchByFilesExactMatch.Checked, FileTypes.Word));
						files = filesByName.ToArray();
					}
					if (files.Count(x => x.Type == FileTypes.Presentation) > 25)
					{
						AppManager.Instance.ShowWarning("Only the first 25 Results will be displayed.\nNarrow your Search Criteria to display a more qualified list of files...");
						files = files.Take(25).ToArray();
					}
					else if (files.Length == 0)
					{
						AppManager.Instance.ShowWarning("No files meet this search criteria...");
					}
				}
				else if (xtraTabControlSolutionModes.SelectedTabPage == xtraTabPageAddDate)
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
					files = DecoratorManager.Instance.ActivePackageViewer.Package.SearchByDate(startDate, endDate);
					if (files.Count(x => x.Type == FileTypes.Presentation) > 25)
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
			ListManager.Instance.SearchTags.SearchGroups.ForEach(g => g.ListBox.UnCheckAll());
			rbLastDay.Checked = true;
			rbLastHalfMonth.Checked = false;
			rbLastMonth.Checked = false;
			rbDateRange.Checked = false;
			dateEditDateRangeStart.DateTime = DateTime.Now;
			dateEditDateRangeEnd.DateTime = DateTime.Now;

			textEditSearchByFiles.EditValue = null;

			LoadKeyWordFilterSet();

			ApplySearchCriteria(new ILibraryLink[] { });
			UpdateSearchButtonStatus();
		}

		public void UpdateSearchButtonStatus()
		{
			bool enableButton = false;
			if (xtraTabControlSolutionModes.SelectedTabPage == xtraTabPageSearchTags)
			{
				foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
				{
					enableButton = searchGroup.ListBox.CheckedItemsCount > 0;
					if (enableButton)
						break;
				}
			}
			else if (xtraTabControlSolutionModes.SelectedTabPage == xtraTabPageKeyWords)
				enableButton = textEditSearchByFiles.EditValue != null;
			else
				enableButton = true;
			barButtonItemSearch.Enabled = enableButton;
			barButtonItemClear.Enabled = enableButton;
		}

		public void InsertSlide()
		{
			var viewer = _selectedFileViewer as PowerPointViewer;
			if (viewer != null)
				viewer.InsertSlide();
		}
		#endregion

		#region Search Tags Methods and Event Handlers
		private void InitSearchTagsGroups()
		{
			ListManager.Instance.SearchTags.SearchGroups.ForEach(g=>g.InitGroupControls());
			var totalHeight = ListManager.Instance.SearchTags.SearchGroups.Sum(g => g.ToggleButton.Height);
			xtraScrollableControlSearchTags.Height = totalHeight < 400 ? totalHeight : 400;
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
			{
				searchGroup.ToggleButton.Dock = DockStyle.Top;
				searchGroup.ToggleButton.Click += CategoriesGroup_Click;
				searchGroup.ToggleButton.CheckedChanged += CategoriesGroup_CheckedChanged;
				xtraScrollableControlSearchTags.Controls.Add(searchGroup.ToggleButton);
				searchGroup.ToggleButton.BringToFront();
				searchGroup.ListBox.ItemCheck += GroupListBox_ItemCheck;
				pnSearchTagsListContainer.Controls.Add(searchGroup.ListBox);
				searchGroup.ListBox.BringToFront();
			}
			var firstGroup = ListManager.Instance.SearchTags.SearchGroups.FirstOrDefault();
			if (firstGroup != null)
				CategoriesGroup_Click(firstGroup.ToggleButton, new EventArgs());
		}

		private LibraryFileSearchTags GetSearhTags()
		{
			var searchTags = new LibraryFileSearchTags();
			foreach (var searchGroup in ListManager.Instance.SearchTags.SearchGroups)
			{
				if (searchGroup.ListBox.CheckedItemsCount > 0)
				{
					var group = new SearchGroup();
					group.Name = searchGroup.Name;
					foreach (CheckedListBoxItem item in searchGroup.ListBox.Items)
						if (item.CheckState == CheckState.Checked)
							group.Tags.Add(new SearchTag(group.Name) { Name = item.Value.ToString() });
					searchTags.SearchGroups.Add(group);
				}
			}
			return searchTags;
		}

		private void CategoriesGroup_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null || button.Checked) return;
			ListManager.Instance.SearchTags.SearchGroups.ForEach(g => g.ToggleButton.Checked = false);
			button.Checked = true;
		}

		private void CategoriesGroup_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null || !button.Checked) return;
			var assignedControl = button.Tag as Control;
			if (assignedControl == null) return;
			assignedControl.Dock = DockStyle.Fill;
			assignedControl.BringToFront();
		}

		private void GroupListBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			UpdateSearchButtonStatus();
		}
		#endregion

		#region Key Word Methods and Event Handlers
		private bool _allowToSaveKeyWordFilters;

		private void LoadKeyWordFilterSet()
		{
			_allowToSaveKeyWordFilters = false;
			checkEditAllFiles.Checked = SettingsManager.Instance.KeyWordFilters.AllFiles;
			checkEditPowerPoint.Checked = SettingsManager.Instance.KeyWordFilters.PowerPoint;
			checkEditPDF.Checked = SettingsManager.Instance.KeyWordFilters.PDF;
			checkEditExcel.Checked = SettingsManager.Instance.KeyWordFilters.Excel;
			checkEditWord.Checked = SettingsManager.Instance.KeyWordFilters.Word;
			checkEditVideo.Checked = SettingsManager.Instance.KeyWordFilters.Video;
			checkEditWeb.Checked = SettingsManager.Instance.KeyWordFilters.Url;
			checkEditNetwork.Checked = SettingsManager.Instance.KeyWordFilters.Network;
			checkEditFolders.Checked = SettingsManager.Instance.KeyWordFilters.Folder;
			_allowToSaveKeyWordFilters = true;
		}

		private void SaveKeyWordFilterSet()
		{
			if (SettingsManager.Instance.LastViewed)
			{
				SettingsManager.Instance.KeyWordFilters.AllFiles = checkEditAllFiles.Checked;
				SettingsManager.Instance.KeyWordFilters.PowerPoint = checkEditPowerPoint.Checked;
				SettingsManager.Instance.KeyWordFilters.PDF = checkEditPDF.Checked;
				SettingsManager.Instance.KeyWordFilters.Excel = checkEditExcel.Checked;
				SettingsManager.Instance.KeyWordFilters.Word = checkEditWord.Checked;
				SettingsManager.Instance.KeyWordFilters.Video = checkEditVideo.Checked;
				SettingsManager.Instance.KeyWordFilters.Url = checkEditWeb.Checked;
				SettingsManager.Instance.KeyWordFilters.Network = checkEditNetwork.Checked;
				SettingsManager.Instance.KeyWordFilters.Folder = checkEditFolders.Checked;
				SettingsManager.Instance.SaveSettings();
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
		private void barButtonItemOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.Open();
		}

		private void barButtonItemSave_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.Save();
		}

		private void barButtonItemSaveAsPDF_ItemClick(object sender, ItemClickEventArgs e)
		{
			var viewer = _selectedFileViewer as PowerPointViewer;
			if (viewer != null)
				viewer.SaveAsPDF();
		}

		private void barButtonItemEmailLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.Email();
		}

		private void barButtonItemPrintLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.Print();
		}

		private void barButtonItemQuickSiteEmail_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.EmailLinkToQuickSite();
		}

		private void barButtonItemQuickSiteAdd_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (_selectedFileViewer != null)
				_selectedFileViewer.AddLinkToQuickSite();
		}

		private void barButtonItemAddSlide_ItemClick(object sender, ItemClickEventArgs e)
		{
			InsertSlide();
		}

		private void barButtonItemOpenQuickView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var viewer = _selectedFileViewer as PowerPointViewer;
			if (viewer != null)
				viewer.OpenInQuickView();
		}

		private void barButtonItemSearch_ItemClick(object sender, ItemClickEventArgs e)
		{
			Search();
		}

		private void pbSearchByFiles_Click(object sender, EventArgs e)
		{
			Search();
		}

		private void barButtonItemClear_ItemClick(object sender, ItemClickEventArgs e)
		{
			ClearSolutionControl();
		}
		#endregion

		#region Grid Event Handlers
		private void gridViewFiles_RowStyle(object sender, RowStyleEventArgs e)
		{
			if (e.RowHandle >= 0 && _fileViewers.Count > e.RowHandle)
			{
				LibraryLink file = _fileViewers[gridViewFiles.GetDataSourceRowIndex(e.RowHandle)].File;
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
			GridHitInfo hi = gridViewFiles.CalcHitInfo(e.X, e.Y);
			if (hi.InRowCell)
				Cursor = Cursors.Hand;
			else
				Cursor = Cursors.Default;
		}

		private void gridViewFiles_MouseLeave(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void gridViewFiles_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			int rowHandele = e.FocusedRowHandle;
			UpdatePreviewArea(rowHandele);
		}

		private void gridViewFiles_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (_selectedFileViewer != null && e.Clicks == 2)
			{
				_selectedFileViewer.Open();
			}
		}
		#endregion

		#endregion

		#region Control Event Handlers
		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControlFiles)
				return;
			ToolTipControlInfo info = e.Info;
			try
			{
				var view = gridControlFiles.GetViewAt(e.ControlMousePosition) as GridView;
				if (view == null)
					return;
				GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
				if (hi.InRowCell)
				{
					if (hi.RowHandle >= 0 && _fileViewers.Count > hi.RowHandle)
					{
						LibraryLink file = _fileViewers[gridViewFiles.GetDataSourceRowIndex(hi.RowHandle)].File;
						if (file != null)
						{
							var toolTip = new List<string>();
							toolTip.Add(file.NameWithExtension);
							toolTip.Add("Added: " + file.AddDate.ToString("M/dd/yy h:mm:ss tt"));
							if (file.ExpirationDateOptions.EnableExpirationDate && file.ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
								toolTip.Add("Expires: " + file.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt"));
							else
								toolTip.Add("Expires: No Expiration Date");
							info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), string.Join(Environment.NewLine, toolTip.ToArray()));
						}
					}
				}
			}
			finally
			{
				e.Info = info;
			}
		}

		private void xtraTabControlSolutionModes_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			ClearSolutionControl();
			if (e.Page == xtraTabPageSearchTags)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _targetToolTip);
			else if (e.Page == xtraTabPageKeyWords)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _titleToolTip);
			else if (e.Page == xtraTabPageAddDate)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _dateToolTip);
			SettingsManager.Instance.SolutionTitleView = e.Page == xtraTabPageKeyWords;
			SettingsManager.Instance.SolutionTagsView = e.Page == xtraTabPageSearchTags;
			SettingsManager.Instance.SolutionDateView = e.Page == xtraTabPageAddDate;
			SettingsManager.Instance.SaveSettings();
		}
		#endregion
	}
}