using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.JsonConverters;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.CustomDialog;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.CommonGUI.Wallbin.Views;
using SalesLibraries.FileManager.Business.PreviewGenerators;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Clipboard;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.LinksGroup;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.ContextMenuEdit.SingleLink;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	[ToolboxItem(false)]
	public partial class ClassicFolderBox : FolderBoxWithHeader
	{
		private readonly Pen _folderBoxDraggedIndicatorPen = new Pen(Color.Black, 8);
		private readonly Pen _rowDraggedIndicatorPen = new Pen(Color.Black, 2);
		private FolderClipboardManager _folderClipboardManager;
		private readonly List<Links.ContextMenuEdit.SingleLink.BaseContextMenuEditor> _sigleLinkContextMenuEditors = new List<Links.ContextMenuEdit.SingleLink.BaseContextMenuEditor>();
		private readonly List<Links.ContextMenuEdit.LinksGroup.BaseContextMenuEditor> _linksGroupContextMenuEditors = new List<Links.ContextMenuEdit.LinksGroup.BaseContextMenuEditor>();

		#region Public Properties
		public override IWallbinViewFormat FormatState => MainController.Instance.WallbinViews.FormatState;

		protected SelectionManager SelectionManager => MainController.Instance.WallbinViews.Selection;

		protected PageContent FolderContainer => (PageContent)Parent;

		public bool IsActive => SelectionManager.SelectedFolder == this;

		public LinkRow SelectedLinkRow => grFiles.SelectedRows.OfType<LinkRow>().FirstOrDefault();

		public override Color SelectedRowBackColor
		{
			get
			{
				if (IsActive)
					return Color.Wheat;
				return base.SelectedRowBackColor;
			}
		}

		public event EventHandler<EventArgs> DataChanged;
		#endregion

		public ClassicFolderBox(LibraryFolder dataSource)
			: base(dataSource)
		{
			SelectionManager.SelectionChanged += OnSelectionChanged;
			DataStateObserver.Instance.DataChanged += OnLinksDeleted;

			barButtonItemFolderPropertiesDeleteLinkSecurity.Visibility = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
			barButtonItemFolderPropertiesDeleteLinkTags.Visibility = MainController.Instance.Settings.EditorSettings.EnableTagsEdit ? BarItemVisibility.Always : BarItemVisibility.Never;

			contextMenuStripSecurity.DefaultDropDownDirection = Screen.AllScreens.Length > 0 && DataSource.ColumnOrder > 1
				? ToolStripDropDownDirection.Left
				: ToolStripDropDownDirection.Default;

			_folderClipboardManager = new FolderClipboardManager(
				DataSource,
				barSubItemFolderPropertiesFolderCopy,
				barSubItemFolderPropertiesFolderMove);
			_folderClipboardManager.FolderMoved += OnFolderMoved;

			InitLinksGroupContextMenuEditors();
			InitSingleLinkContextMenuEditors();

			// 
			// grFiles
			// 
			grFiles.CellBeginEdit += (o, e) => { e.Cancel = true; };
			grFiles.CellMouseClick += OnGridCellMouseClick;
			grFiles.CellMouseDoubleClick += OnGridCellMouseDoubleClick;
			grFiles.CellMouseDown += OnGridCellMouseDown;
			grFiles.CellMouseLeave += OnGridCellMouseLeave;
			grFiles.CellMouseMove += OnGridCellMouseMove;
			grFiles.CellMouseUp += OnGridCellMouseUp;
			grFiles.SelectionChanged += OnGridSelectionChanged;
			grFiles.DragDrop += OnDragDrop;
			grFiles.DragOver += OnDragOver;
			grFiles.DragLeave += OnDragLeave;
			grFiles.MouseDown += OnGridMouseDown;
			grFiles.MouseClick += OnGridMouseClick;
			// 
			// labelControlText
			// 
			labelControlText.Click += OnHeaderClick;
			labelControlText.DoubleClick += OnHeaderDoubleClick;
			labelControlText.DragDrop += OnDragDrop;
			labelControlText.DragOver += OnDragOver;
			labelControlText.DragLeave += OnDragLeave;
			labelControlText.MouseDown += OnHeaderMouseDown;
			labelControlText.MouseMove += OnHeaderMouseMove;
		}

		#region Public Methods
		public override void ReleaseControl()
		{
			_folderClipboardManager.FolderMoved -= OnFolderMoved;
			_folderClipboardManager = null;

			base.ReleaseControl();
			DataChanged = null;
		}
		#endregion

		#region Link Data Processing
		public void AddHyperLink(BaseNetworkLinkInfo initialLinkInfo = null)
		{
			using (var form = new FormAddHyperLink(initialLinkInfo))
			{
				if (form.ShowDialog() != DialogResult.OK) return;

				var position = -1;
				var selectedLink = SelectedLinkRow;
				if (selectedLink != null)
					position = selectedLink.Index;

				_outsideChangesInProgress = true;

				LibraryObjectLink newLink;
				switch (form.SelectedEditorType)
				{
					case HyperLinkTypeEnum.Url:
						newLink = WebLink.Create(
							(UrlLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.YouTube:
						newLink = YouTubeLink.Create(
							(YouTubeLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.Network:
						newLink = NetworkLink.Create(
							(LanLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.QuickSite:
						newLink = QuickSiteLink.Create(
							(QuickSiteLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.App:
						newLink = AppLink.Create(
							(AppLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					case HyperLinkTypeEnum.Internal:
						var internalLinkInfo = (InternalLinkInfo)form.SelectedEditor.GetHyperLinkInfo();
						switch (internalLinkInfo.InternalLinkType)
						{
							case InternalLinkType.Wallbin:
								newLink = InternalWallbinLink.Create(
									(InternalWallbinLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							case InternalLinkType.LibraryPage:
								newLink = InternalLibraryPageLink.Create(
									(InternalLibraryPageLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							case InternalLinkType.LibraryFolder:
								newLink = InternalLibraryFolderLink.Create(
									(InternalLibraryFolderLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							case InternalLinkType.LibraryObject:
								newLink = InternalLibraryObjectLink.Create(
									(InternalLibraryObjectLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							case InternalLinkType.Shortcut:
								newLink = InternalShortcutLink.Create(
									(InternalShortcutLinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
									DataSource);
								break;
							default:
								throw new ArgumentOutOfRangeException("Link type not found");
						}
						break;
					case HyperLinkTypeEnum.Html5:
						newLink = Html5Link.Create(
							(Html5LinkInfo)form.SelectedEditor.GetHyperLinkInfo(),
							DataSource);
						break;
					default:
						throw new ArgumentOutOfRangeException("Link type not found");
				}

				if (position >= 0)
					((List<BaseLibraryLink>)DataSource.Links).InsertItem(newLink, position);
				else
					DataSource.Links.AddItem(newLink);
				var newRow = InsertLinkRow(newLink, position);
				_outsideChangesInProgress = false;

				UpdateGridSize();

				newRow.Selected = true;

				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public void AddLineBreak()
		{
			var position = -1;
			var selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				position = selectedLink.Index;

			_outsideChangesInProgress = true;
			var newLink = LineBreak.Create(DataSource);
			if (position >= 0)
				((List<BaseLibraryLink>)DataSource.Links).InsertItem(newLink, position);
			else
				DataSource.Links.AddItem(newLink);
			var newRow = InsertLinkRow(newLink, position);
			_outsideChangesInProgress = false;

			UpdateGridSize();

			newRow.Selected = true;

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void AddLinkBundle(LinkBundle bundle, int position = -1)
		{
			var selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				position = selectedLink.Index;

			_outsideChangesInProgress = true;
			var newLink = LinkBundleLink.Create(DataSource, bundle);
			if (position >= 0)
				((List<BaseLibraryLink>)DataSource.Links).InsertItem(newLink, position);
			else
				DataSource.Links.AddItem(newLink);
			var newRow = InsertLinkRow(newLink, position);
			_outsideChangesInProgress = false;

			UpdateGridSize();

			newRow.Selected = true;

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void EditLinkSettings(LinkSettingsType settingsType)
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			EditLinkSettings(new[] { selectedRow.Source }, settingsType);
		}

		public void EditLinkSettings(IList<BaseLibraryLink> links, LinkSettingsType settingsType)
		{
			var result = links.Count == 1 ?
				SettingsEditorFactory.Run(links.First(), settingsType) :
				SettingsEditorFactory.Run(links, settingsType, false);
			if (result != DialogResult.OK) return;

			grFiles.SuspendLayout();
			_outsideChangesInProgress = true;
			foreach (var linkRow in grFiles.Rows.OfType<LinkRow>())
				linkRow.Info.Recalc();
			_outsideChangesInProgress = false;
			UpdateGridSize();
			grFiles.ResumeLayout(true);
			grFiles.Refresh();

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void EditImageSettings()
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			if (!selectedRow.AllowEditImageSettings) return;
			if (selectedRow.Source.Widget.Enabled)
				EditLinkSettings(new[] { selectedRow.Source }, LinkSettingsType.Widget);
			else if (selectedRow.Source.Banner.Enable)
				EditLinkSettings(new[] { selectedRow.Source }, LinkSettingsType.Banner);
			else if (selectedRow.Source.Widget.HasAutoWidget)
				EditLinkSettings(new[] { selectedRow.Source }, LinkSettingsType.Widget);
		}

		public void ResetLinkSettings()
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;

			using (var form = new FormResetLinkSettings(selectedRow.Source))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var settingsGroupsForReset = form.SettingsGroups;
				using (var confirmation = new FormResetLinkSettingsConfirmation(settingsGroupsForReset))
				{
					if (confirmation.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
					selectedRow.Source.ResetToDefault(settingsGroupsForReset);
					selectedRow.Info.Recalc();
					grFiles.Refresh();
					DataChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public void ResetAllLinksInFolderSettings()
		{
			using (var form = new FormResetLinkSettings(DataSource))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var settingsGroupsForReset = form.SettingsGroups;
				using (var confirmation = new FormResetLinkSettingsConfirmation(settingsGroupsForReset))
				{
					if (confirmation.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
					DataSource.AllLinks.ResetToDefault(settingsGroupsForReset);

					grFiles.SuspendLayout();
					_outsideChangesInProgress = true;
					foreach (var linkRow in grFiles.Rows.OfType<LinkRow>())
						linkRow.Info.Recalc();
					_outsideChangesInProgress = false;
					UpdateGridSize();
					grFiles.ResumeLayout(true);

					DataChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public void OpenLink()
		{
			var selectedRow = SelectedLinkRow;
			var sourceLink = selectedRow?.Source as LibraryObjectLink;
			if (sourceLink == null) return;
			Utils.OpenFile(sourceLink.OpenPaths);
		}

		public void OpenLinkLocation()
		{
			var selectedRow = SelectedLinkRow;
			var sourceLink = selectedRow?.Source as LibraryFileLink;
			if (sourceLink == null) return;
			Utils.OpenFile(sourceLink.LocationPath);
			MainController.Instance.WallbinViews.ActiveWallbin.DataSourcesControl.ShowFileInTree(sourceLink.FullPath);
		}

		public void DeleteLink()
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			grFiles.SuspendLayout();
			var relatedLinks = selectedRow.SourceObject?.GetRelatedLinks();
			if (relatedLinks != null && relatedLinks.Any())
			{
				using (var form = new FormDeleteLink())
				{
					var result = form.ShowDialog(MainController.Instance.MainForm);
					switch (result)
					{
						case DialogResult.OK:
							selectedRow.DeleteWithSourceLink(true);
							break;
						case DialogResult.Yes:
							DataStateObserver.Instance.RaiseLinksDeleted(relatedLinks.Select(l => l.ExtId));
							selectedRow.SourceObject?.DeleteLinkAndRelatedLinks();
							break;
						default:
							grFiles.ResumeLayout();
							return;
					}
				}
			}
			else
			{
				if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove this link/line break?") !=
					DialogResult.Yes)
				{
					grFiles.ResumeLayout();
					return;
				}
				selectedRow.DeleteWithSourceLink(true);
			}
			grFiles.ResumeLayout();
			UpdateGridSize();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void DeleteLinks()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove links?") != DialogResult.Yes) return;
			foreach (var linkRow in grFiles.Rows.OfType<LinkRow>().ToList())
				linkRow.DeleteWithSourceLink(true);
			UpdateGridSize();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SelectAll(bool handleSelection = true)
		{
			_outsideChangesInProgress = !handleSelection;
			grFiles.SelectAll();
			_outsideChangesInProgress = false;
		}

		private void ResetSecurity()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to delete security settings?") != DialogResult.Yes) return;
			DataSource.AllLinks.ApplySecurity(new SecuritySettings());
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ResetTags()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to wipe tags?") != DialogResult.Yes) return;
			DataSource.AllLinks.ApplyCategories(new SearchGroup[] { });
			DataSource.AllLinks.ApplyKeywords(new SearchTag[] { });
			DataSource.AllLinks.ApplySuperFilters(new string[] { });
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ResetWidgets()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove widgets?") != DialogResult.Yes) return;
			DataSource.AllLinks.ResetWidgets();
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ResetBanners()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove banners?") != DialogResult.Yes) return;
			DataSource.AllLinks.ResetBanners();
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SortLinkByName()
		{
			grFiles.ClearSelection();
			DataSource.SortLinksByDisplayName();
			var rows = grFiles.Rows.OfType<LinkRow>().ToList();
			grFiles.Rows.Clear();
			grFiles.Rows.AddRange(rows.OrderBy(linkRow => linkRow.Source.Order).ToArray());
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SetLinkTextWordWrap()
		{
			DataSource.AllLinks.SetLinkTextWordWrap();
			UpdateContent(true);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnSingleLinkContextEditorValueChanged(object sender, EventArgs e)
		{
			var selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			selectedRow.Info.Recalc();
			grFiles.Refresh();
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnLinksGroupContextEditorValueChanged(object sender, EventArgs e)
		{
			grFiles.SuspendLayout();
			_outsideChangesInProgress = true;
			foreach (var linkRow in grFiles.Rows.OfType<LinkRow>())
				linkRow.Info.Recalc();
			_outsideChangesInProgress = false;
			UpdateGridSize();
			grFiles.ResumeLayout(true);

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void RefreshPreviewFiles()
		{
			var sourceLink = SelectedLinkRow?.Source as PreviewableLink;
			if (sourceLink == null) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure you want to refresh the server files for:{1}{0}?", sourceLink.NameWithExtension, Environment.NewLine)) != DialogResult.Yes) return;
			MainController.Instance.ProcessManager.Run("Updating Preview files...", cancelationToken =>
			{
				if (sourceLink.Type == FileTypes.PowerPoint)
				{
					((PowerPointLinkSettings)sourceLink.Settings).ClearQuickViewContent();
					using (var powerPointProcessor = new PowerPointHidden())
					{
						if (!powerPointProcessor.Connect(true)) return;
						((PowerPointLinkSettings)sourceLink.Settings).UpdateQuickViewContent(powerPointProcessor);
						((PowerPointLinkSettings)sourceLink.Settings).UpdatePresentationInfo(powerPointProcessor);
					}
				}
				sourceLink.ClearPreviewContainer();
				var previewContainer = sourceLink.GetPreviewContainer();
				var previewGenerator = previewContainer.GetPreviewGenerator();
				previewContainer.UpdateContent(previewGenerator, cancelationToken);
			});
			MainController.Instance.PopupMessages.ShowInfo(String.Format("{0}{1}Is now updated for the server!", sourceLink.NameWithExtension, Environment.NewLine));
		}

		private void CopyLinks()
		{
			if (!SelectionManager.SelectedLinks.Any()) return;
			MainController.Instance.WallbinViews.LinksClipboard.Copy(SelectionManager.SelectedLinks.Select(link => link.ExtId));
		}

		private void CutLinks()
		{
			if (!SelectionManager.SelectedLinks.Any()) return;
			MainController.Instance.WallbinViews.LinksClipboard.Cut(SelectionManager.SelectedLinks.Select(link => link.ExtId));
		}

		private void PasteLinks()
		{
			var position = -1;
			var selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				position = selectedLink.Index;

			var pastedLinks = MainController.Instance.WallbinViews.LinksClipboard.Paste();

			_outsideChangesInProgress = true;
			LinkRow row = null;
			foreach (var libraryLink in pastedLinks)
			{
				libraryLink.Folder = DataSource;
				if (position >= 0)
					((List<BaseLibraryLink>)DataSource.Links).InsertItem(libraryLink, position);
				else
					DataSource.Links.AddItem(libraryLink);
				row = InsertLinkRow(libraryLink, position);
				if (position != -1)
					position++;
			}
			_outsideChangesInProgress = false;

			if (row != null)
			{
				UpdateGridSize();
				row.Selected = false;
				row.Selected = true;
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		#endregion

		#region Folder Data Processing
		private void EditFolderSettings()
		{
			using (var form = new FormWindow(DataSource, new TitleFormParams()))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				UpdateFont();
				SetupView();
				UpdateContent(true);
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void EditFolderBanner()
		{
			using (var form = new FormWindow(DataSource, new BannerFormParams()))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				SetupView();
				UpdateGridSize();
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void EditFolderImageSettings()
		{
			if (DataSource.Widget.Enabled)
				EditFolderWidget();
			else if (DataSource.Banner.Enable)
				EditFolderBanner();
		}

		private void EditFolderWidget()
		{
			using (var form = new FormWindow(DataSource, new WidgetFormParams()))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				SetupView();
				UpdateGridSize();
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void DeleteFolder()
		{
			using (var form = new FormCustomDialog(
					String.Format("{0}{1}{2}",
						"<size=+4>Are you SURE you want to DELETE this Window?</size><br>",
						String.Format("<size=+2>{0}</size>", DataSource.Name),
						"<br><br>*All Links in this window will be removed from your site"
					),
					new[]
					{
						new CustomDialogButtonInfo {Title = "DELETE",DialogResult = DialogResult.OK,Width = 100},
						new CustomDialogButtonInfo {Title = "CANCEL",DialogResult = DialogResult.Cancel,Width = 100}
					}
				))
			{
				form.Width = 500;
				form.Height = 160;
				if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
					FolderContainer.DeleteFolder(this);
			}
		}

		private void OnFolderMoved(object sender, FolderMovingEventArgs e)
		{
			if (e.DeleteFromCurrent)
				FolderContainer.DeleteFolder(this);
			var targetPageView = MainController.Instance.WallbinViews.ActiveWallbin.Pages
				.FirstOrDefault(pageView => pageView.Page == e.TargetPage);
			var isSamePage = MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == targetPageView;
			if (isSamePage)
				MainController.Instance.ProcessManager.Run("Loading Page...",
				cancelationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					MainController.Instance.WallbinViews.ActiveWallbin.ActivePage?.LoadPage(true);
					MainController.Instance.WallbinViews.ActiveWallbin.ActivePage?.ShowPage();
				})));
			else
			{
				targetPageView?.LoadPage(true);
				MainController.Instance.WallbinViews.ActiveWallbin.SelectPage(targetPageView);
			}
		}
		#endregion

		#region Private Methods
		private BaseLibraryLink InsertFileLink(SourceLink sourceLink, int position = -1)
		{
			BaseLibraryLink link = null;
			MainController.Instance.ProcessManager.Run("Adding Link...", cancelationToken =>
			{
				link = LibraryFileLink.Create(sourceLink, DataSource);
				if (position >= 0)
					((List<BaseLibraryLink>)DataSource.Links).InsertItem(link, position);
				else
					DataSource.Links.AddItem(link);
			});
			return link;
		}

		private void InsertLinks(IEnumerable<SourceLink> sourceLinks, int position = -1)
		{
			if (!sourceLinks.Any()) return;

			_outsideChangesInProgress = true;
			LinkRow row = null;
			foreach (var sourceLink in sourceLinks)
			{
				row = InsertLinkRow(InsertFileLink(sourceLink, position), position);
				if (position != -1)
					position++;
			}
			_outsideChangesInProgress = false;

			UpdateGridSize();

			row.Selected = true;

			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ProcessRowMoving(LinkRow targetRow, int newPosition = -1)
		{
			int positionToInsert;
			if (targetRow.DataGridView == grFiles)
				positionToInsert = newPosition == -1 ?
					grFiles.RowCount - 1 :
					(targetRow.Index < newPosition ? newPosition - 1 : newPosition);
			else
				positionToInsert = newPosition == -1 ? grFiles.RowCount : newPosition;
			var targetLink = targetRow.Source;
			targetRow.DeleteWithSourceLink();
			targetRow.Dispose();

			grFiles.ClearSelection();
			targetLink.Folder = DataSource;
			((List<BaseLibraryLink>)DataSource.Links).InsertItem(targetLink, positionToInsert);
			var newRow = InsertLinkRow(targetLink, positionToInsert);
			UpdateGridSize();
			newRow.Selected = true;
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnSelectionChanged(object sender, SelectionEventArgs e)
		{
			if (IsDisposed) return;
			if (e.SelectionType == SelectionEventType.FolderSelected || e.SelectionType == SelectionEventType.SelectionReset)
			{
				if (IsActive)
				{
					ResetPadding();
					grFiles.ReadOnly = false;
					grFiles.MultiSelect = FormatState.AllowMultiSelect;
				}
				else
				{
					grFiles.ReadOnly = true;
					ResetPadding();
				}
			}

			if (!IsActive &&
				((e.SelectionType == SelectionEventType.FolderSelected && !FormatState.AllowMultiSelect) ||
				 e.SelectionType == SelectionEventType.SelectionReset)
				)
			{
				var storedValue = _outsideChangesInProgress;
				_outsideChangesInProgress = true;
				grFiles.ClearSelection();
				grFiles.CurrentCell = null;
				_outsideChangesInProgress = storedValue;
			}

			grFiles.DefaultCellStyle.SelectionBackColor = SelectedRowBackColor;
		}

		private void ResetPadding()
		{
			Padding newPadding;
			if (IsFolderBoxDragged)
				newPadding = new Padding(0, 2, 0, 0);
			else if (IsActive && !FormatState.AllowMultiSelect)
				newPadding = new Padding(2, 2, 2, 2);
			else
				newPadding = Padding.Empty;
			if (Padding.Left != newPadding.Left ||
				Padding.Right != newPadding.Right ||
				Padding.Top != newPadding.Top ||
				Padding.Bottom != newPadding.Bottom)
			{
				Padding = newPadding;
				UpdateGridSize();
				Refresh();
			}
		}

		private void OnLinksDeleted(object sender, DataChangeEventArgs e)
		{
			if (e.ChangeType != DataChangeType.LinksDeleted) return;
			var linksDeletedArgs = (LinksDeletedEventArgs)e;
			foreach (var linkRow in grFiles.Rows
				.OfType<LinkRow>()
				.Where(row => linksDeletedArgs.LinkIds.Any(id => id.Equals(row.Source.ExtId))))
				linkRow.RemoveFromGrid();
			UpdateGridSize();
		}
		#endregion

		#region Link Row Events (Editing, Selection, Sizing)
		private void OnGridCellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			if (!linkRow.AllowEditLinkText) return;
			using (var form = new FormEditLinkText())
			{
				form.Text = String.Format(form.Text, linkRow.Source is LineBreak ?
					"LINE BREAK" :
					(linkRow.Source as LibraryFileLink)?.NameWithExtension ??
						(linkRow.Source as LibraryObjectLink)?.RelativePath ??
						String.Empty
					);
				form.TextWordWrap = linkRow.Source.Settings.TextWordWrap;
				form.EditedText = linkRow.Source.DisplayNameWithoutNote;
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				linkRow.Source.DisplayNameWithoutNote = form.EditedText;
				linkRow.Source.Settings.TextWordWrap = form.TextWordWrap;
				linkRow.Info.Recalc();
				DataChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void OnGridSelectionChanged(object sender, EventArgs e)
		{
			if (_outsideChangesInProgress) return;
			var selectedLinks = grFiles.SelectedRows.OfType<LinkRow>().Select(row => row.Source);
			SelectionManager.SelectLinks(selectedLinks, ModifierKeys);
		}
		#endregion

		#region Formatting Event Handlers
		private void OnBordersPaint(object sender, PaintEventArgs e)
		{
			if (IsActive)
			{
				Rectangle rect = e.ClipRectangle.Top == 0 ?
					new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height) :
					new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
				const int borderWidth = 3;
				for (int i = 0; i < borderWidth; i++)
				{
					ControlPaint.DrawBorder(e.Graphics,
						rect,
						Color.Red,
						ButtonBorderStyle.Solid);
					rect.X++;
					rect.Y++;
					rect.Width -= 2;
					rect.Height -= 2;
				}
			}
			if (IsFolderBoxDragged)
				e.Graphics.DrawLine(_folderBoxDraggedIndicatorPen, 0, 0, Width, 0);
		}

		protected override void OnGridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex != 0) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			base.OnGridCellPainting(sender, e);
			if (DataSource.Links.Any() &&
				(IsSourceLinksDragged || IsLinkRowDragged || IsLinkBundleDragged))
			{
				if (_mouseDragOverHitInfo.Type == DataGridViewHitTestType.Cell && _mouseDragOverHitInfo.RowIndex == linkRow.Index)
					e.Graphics.DrawLine(_rowDraggedIndicatorPen, 0, e.CellBounds.Top + 2, grFiles.Width, e.CellBounds.Top + 2);
				else if (_mouseDragOverHitInfo.RowIndex == -1 && linkRow.Index == grFiles.RowCount - 1)
					e.Graphics.DrawLine(_rowDraggedIndicatorPen, 0, e.CellBounds.Height * grFiles.RowCount + 1, grFiles.Width, e.CellBounds.Height * grFiles.RowCount + 1);
			}
		}
		#endregion

		#region Drag&Drop Processing (Methods and Events)
		private IDataObject _dataDraggedOver;
		private Rectangle _dragStartBox;
		private DataGridView.HitTestInfo _mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
		private DataGridView.HitTestInfo _mouseDragOverHitInfo = DataGridView.HitTestInfo.Nowhere;

		protected IDataObject DataDraggedOver
		{
			get { return _dataDraggedOver; }
			set
			{
				_dataDraggedOver = value;
				ResetPadding();
			}
		}

		protected bool IsDataDragged => DataDraggedOver != null;

		protected bool IsSourceLinksDragged => IsDataDragged &&
			DataDraggedOver.GetDataPresent(DataFormats.Serializable, true) &&
			DataDraggedOver.GetData(DataFormats.Serializable, true) is SourceLink[];

		protected bool IsLinkRowDragged => IsDataDragged && DataDraggedOver.GetDataPresent(typeof(LinkRow));

		protected bool IsFolderBoxDragged
		{
			get
			{
				object data = IsDataDragged ? DataDraggedOver.GetData(typeof(ClassicFolderBox)) : null;
				return data != null && data != this;
			}
		}

		protected bool IsLinkBundleDragged => IsDataDragged &&
			DataDraggedOver.GetDataPresent(DataFormats.Serializable, true) &&
			EntitySettingsResolver.ExtractObjectTypeFromProxy(DataDraggedOver.GetData(DataFormats.Serializable, true).GetType()) == typeof(LinkBundle);

		private void ResetDragInfo()
		{
			bool needGridRefersh = IsSourceLinksDragged || IsLinkRowDragged || IsLinkBundleDragged;
			DataDraggedOver = null;
			_mouseDragOverHitInfo = DataGridView.HitTestInfo.Nowhere;
			if (needGridRefersh)
				grFiles.Refresh();
		}

		private void OnDragOver(object sender, DragEventArgs e)
		{
			FolderContainer.ProcessScrollOnDragOver(sender, e);
			if (!FormatState.AllowEdit) return;
			DataDraggedOver = e.Data;
			if (!IsDataDragged) return;
			if (IsSourceLinksDragged)
				e.Effect = DragDropEffects.Copy;
			else if (IsLinkRowDragged)
				e.Effect = DragDropEffects.Move;
			else if (IsFolderBoxDragged)
				e.Effect = DragDropEffects.Move;
			else if (IsLinkBundleDragged)
				e.Effect = DragDropEffects.Copy;
			else
			{
				e.Effect = DragDropEffects.None;
				ResetDragInfo();
				return;
			}

			var pt = grFiles.PointToClient(new Point(e.X, e.Y));
			var dragOverInfo = grFiles.HitTest(pt.X, pt.Y);
			if (!dragOverInfo.Equals(_mouseDragOverHitInfo))
			{
				_mouseDragOverHitInfo = dragOverInfo;
				grFiles.Refresh();
			}
		}

		private void OnDragLeave(object sender, EventArgs e)
		{
			FolderContainer.ProcessScrollOnDragLeave(sender, e);
			if (!FormatState.AllowEdit) return;
			if (DataDraggedOver == null) return;
			if (IsFolderBoxDragged && ClientRectangle.Contains(PointToClient(MousePosition)))
				return;
			ResetDragInfo();
		}

		private void OnDragDrop(object sender, DragEventArgs e)
		{
			FolderContainer.ProcessScrollOnDragLeave(sender, e);
			if (!FormatState.AllowEdit) return;
			SelectionManager.SelectFolder(this);
			if (IsSourceLinksDragged)
			{
				var droppedLinks = DataDraggedOver?.GetData(DataFormats.Serializable, true) as SourceLink[] ?? new SourceLink[] { };
				SelectionManager.SelectFolder(this);

				var fileLinks = droppedLinks.OfType<FileLink>().ToList();
				var folderLinks = droppedLinks.OfType<FolderLink>().ToList();

				var confirmDrop = true;
				if (!folderLinks.Any() && fileLinks.Count == 1 && FileFormatHelper.IsUrlFile(fileLinks.Single().Path))
				{
					AddHyperLink(UrlLinkInfo.FromFile(fileLinks.Single().Path));
					confirmDrop = false;
				}
				else
				if (folderLinks.Any(folderLink => fileLinks.Any(fileLink => fileLink.Path.Contains(folderLink.Path))))
				{
					using (var form = new FormCustomDialog(
						String.Format("{0}{1}",
							"<size=+4>Are you SURE you want to Drag the Folder AND all the Files into this Window?</size><br>",
							"<br>It might look kind of strange to have the entire folder in this window…"
							),
						new[]
						{
							new CustomDialogButtonInfo {Title = "Yep!", DialogResult = DialogResult.OK, Width = 100},
							new CustomDialogButtonInfo {Title = "CANCEL", DialogResult = DialogResult.Cancel, Width = 100}
						}
						))
					{
						form.Width = 500;
						form.Height = 160;
						confirmDrop = form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
					}
				}
				if (confirmDrop)
					InsertLinks(droppedLinks, _mouseDragOverHitInfo.RowIndex);
			}

			if (IsLinkBundleDragged)
			{
				var droppedLinkBundle = DataDraggedOver?.GetData(DataFormats.Serializable, true) as LinkBundle;
				AddLinkBundle(droppedLinkBundle, _mouseDragOverHitInfo.RowIndex);
			}

			var droppedRow = DataDraggedOver?.GetData(typeof(LinkRow)) as LinkRow;
			if (droppedRow != null)
				ProcessRowMoving(droppedRow, _mouseDragOverHitInfo.RowIndex);

			var droppedFolder = DataDraggedOver?.GetData(typeof(ClassicFolderBox)) as ClassicFolderBox;
			if (droppedFolder != null && droppedFolder != this)
				FolderContainer.ProcessFolderMoving(droppedFolder, DataSource.ColumnOrder, DataSource.RowOrder);

			ResetDragInfo();
		}

		private void OnGridCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			_dragStartBox = new Rectangle(
				new Point(
					e.X - (SystemInformation.DragSize.Width / 2),
					e.Y - (SystemInformation.DragSize.Height / 2)),
				SystemInformation.DragSize);
			_mouseDownHitInfo = grFiles.HitTest(e.X, e.Y);
		}

		private void OnGridCellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			_mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
		}

		private void OnGridCellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (DataSource.Links.Any())
				Cursor = Cursors.Hand;
			if (((e.Button & MouseButtons.Left) != MouseButtons.Left) ||
				Equals(_mouseDownHitInfo, DataGridView.HitTestInfo.Nowhere) ||
				_dragStartBox.Contains(e.X, e.Y))
				return;
			var selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				grFiles.DoDragDrop(selectedLink, DragDropEffects.All);
			_mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
		}

		private void OnHeaderMouseDown(object sender, MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					if (!FormatState.AllowEdit) return;
					_dragStartBox = new Rectangle(
						new Point(
							e.X - (SystemInformation.DragSize.Width / 2),
							e.Y - (SystemInformation.DragSize.Height / 2)),
						SystemInformation.DragSize);
					break;
				case MouseButtons.Right:
					if (FormatState.ShowSecurityTags)
						contextMenuStripSecurity.Show(
							(Control)sender,
							e.Location,
							Screen.AllScreens.Length > 0 && DataSource.ColumnOrder > 1 ?
								ToolStripDropDownDirection.Left :
								ToolStripDropDownDirection.Default);
					else
					{
						_folderClipboardManager.UpdateTargets();
						LoadLinksGroupContextMenuEditors();
						barButtonItemFolderPropertiesImageSettings.Visibility = DataSource.Widget.Enabled || DataSource.Banner.Enable ?
							BarItemVisibility.Always :
							BarItemVisibility.Never;
						barSubItemFolderPropertiesLinkAdminSetings.Visibility =
							barSubItemFolderPropertiesLinkPdfSetings.Visibility == BarItemVisibility.Always ||
							barSubItemFolderPropertiesLinkExcelSetings.Visibility == BarItemVisibility.Always ?
								BarItemVisibility.Always : BarItemVisibility.Never;
						popupMenuFolderProperties.ShowPopup(Cursor.Position);
					}
					break;
			}
		}

		private void OnHeaderMouseMove(object sender, MouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			Cursor = Cursors.Default;
			if (((e.Button & MouseButtons.Left) != MouseButtons.Left) || _dragStartBox.Contains(e.X, e.Y))
				return;
			grFiles.DoDragDrop(this, DragDropEffects.Move);
		}
		#endregion

		#region Other GUI Routines
		private void OnHeaderClick(object sender, EventArgs e)
		{
			if (IsActive) return;
			SelectionManager.SelectFolder(this);
			labelControlText.Focus();
		}

		private void OnHeaderDoubleClick(object sender, EventArgs e)
		{
			EditFolderSettings();
		}

		private void OnGridMouseDown(object sender, MouseEventArgs e)
		{
			if (IsActive) return;
			SelectionManager.SelectFolder(this);
			var hitTest = grFiles.HitTest(e.X, e.Y);
			if (hitTest.Type != DataGridViewHitTestType.Cell)
				labelControlText.Focus();
		}

		private void OnGridCellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void OnGridMouseClick(object sender, MouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (!IsActive) return;
			if (e.Button != MouseButtons.Right) return;
			var hitTest = grFiles.HitTest(e.X, e.Y);
			if (MainController.Instance.WallbinViews.LinksClipboard.LinkIds.Any() &&
				hitTest.Type == DataGridViewHitTestType.None)
			{
				barButtonItemLinkPropertiesCopy.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesCut.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesPaste.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesDelete.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesLinkSettings.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesImageSettings.Visibility = BarItemVisibility.Never;
				barSubItemLinkPropertiesNotes.Visibility = BarItemVisibility.Never;
				barSubItemLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesAdvancedSettings.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesTags.Visibility = BarItemVisibility.Never;
				barSubItemLinkPropertiesImages.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesResetSettings.Visibility = BarItemVisibility.Never;
				barSubItemLinkPropertiesAdvanced.Visibility = BarItemVisibility.Never;
				barSubItemLinkPropertiesTextFormat.Visibility = BarItemVisibility.Never;

				popupMenuLinkProperties.ShowPopup(Cursor.Position);
			}
			else
			{
				barButtonItemLinkPropertiesCopy.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesCut.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesPaste.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesOpenLink.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesDelete.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesLinkSettings.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesImageSettings.Visibility = BarItemVisibility.Always;
				barSubItemLinkPropertiesNotes.Visibility = BarItemVisibility.Always;
				barSubItemLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesAdvancedSettings.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesTags.Visibility = BarItemVisibility.Always;
				barSubItemLinkPropertiesImages.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesResetSettings.Visibility = BarItemVisibility.Always;
				barSubItemLinkPropertiesAdvanced.Visibility = BarItemVisibility.Always;
				barSubItemLinkPropertiesTextFormat.Visibility = BarItemVisibility.Always;
			}
		}

		private void OnGridCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (!IsActive) return;
			if (e.Button != MouseButtons.Right) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			linkRow.Selected = true;
			barButtonItemLinkPropertiesPaste.Visibility =
				MainController.Instance.WallbinViews.LinksClipboard.IsPasteAvailable(linkRow.Source.ExtId) ?
					BarItemVisibility.Always :
					BarItemVisibility.Never;
			barButtonItemLinkPropertiesImageSettings.Visibility = linkRow.AllowEditImageSettings ?
				BarItemVisibility.Always :
				BarItemVisibility.Never;
			barButtonItemLinkPropertiesSecurity.Visibility =
				MainController.Instance.Settings.EditorSettings.EnableSecurityEdit ?
					BarItemVisibility.Always :
					BarItemVisibility.Never;
			barButtonItemLinkPropertiesResetSettings.Visibility =
				linkRow.Source.GetCustomizedSettigsGroups().Any() ?
					BarItemVisibility.Always :
					BarItemVisibility.Never;
			if (linkRow.Source is LineBreak)
			{
				barButtonItemLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesFileLocation.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesAdvancedSettings.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesTags.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesExpirationDate.Visibility = BarItemVisibility.Never;
				barSubItemLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;

				barButtonItemLinkPropertiesLinkSettings.Caption = "Line Break Settings";
				barButtonItemLinkPropertiesDelete.Caption = "Delete this Line Break";
				barSubItemLinkPropertiesImages.Caption = "Line Break ART";
				barButtonItemLinkPropertiesResetSettings.Caption = "Reset this Line Break";
			}
			else if (linkRow.Source is LinkBundleLink)
			{
				barButtonItemLinkPropertiesOpenLink.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesFileLocation.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesAdvancedSettings.Visibility = BarItemVisibility.Never;
				barButtonItemLinkPropertiesRefreshPreview.Visibility = BarItemVisibility.Never;
				barSubItemLinkPropertiesAdminSettings.Visibility = BarItemVisibility.Never;

				barButtonItemLinkPropertiesLinkSettings.Caption = "Link Bundle Settings";
				barButtonItemLinkPropertiesDelete.Caption = "Delete this Link Bundle";
				barSubItemLinkPropertiesImages.Caption = "Link Bundle ART";
				barButtonItemLinkPropertiesResetSettings.Caption = "Reset this Link Bundle";
			}
			else
			{
				barButtonItemLinkPropertiesOpenLink.Visibility = BarItemVisibility.Always;
				barButtonItemLinkPropertiesFileLocation.Visibility = linkRow.Source is LibraryFileLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemLinkPropertiesAdvancedSettings.Visibility = linkRow.Source is LibraryFolderLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemLinkPropertiesRefreshPreview.Visibility = linkRow.Source is PreviewableLink
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemLinkPropertiesTags.Visibility = MainController.Instance.Settings.EditorSettings.EnableTagsEdit
					? BarItemVisibility.Always
					: BarItemVisibility.Never;
				barButtonItemLinkPropertiesExpirationDate.Visibility = BarItemVisibility.Always;
				barSubItemLinkPropertiesAdminSettings.Visibility = linkRow.Source is DocumentLink ||
					linkRow.Source is PowerPointLink ||
					linkRow.Source is ExcelLink ? BarItemVisibility.Always : BarItemVisibility.Never;

				barButtonItemLinkPropertiesLinkSettings.Caption = "Link Settings";
				barButtonItemLinkPropertiesDelete.Caption = "Delete this Link";
				barSubItemLinkPropertiesImages.Caption = "Link ART";
				barButtonItemLinkPropertiesResetSettings.Caption = "Reset this Link";
			}
			LoadSingleLinkContextMenuEditors(linkRow.Source);
			popupMenuLinkProperties.ShowPopup(Cursor.Position);
		}
		#endregion

		#region Context Menu

		#region Context Menu Editors
		private void InitSingleLinkContextMenuEditors()
		{
			var libraryObjectNotesEditor = new LibraryObjectNotesEditor(barSubItemLinkPropertiesNotes);
			libraryObjectNotesEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_sigleLinkContextMenuEditors.Add(libraryObjectNotesEditor);

			var lineBreakNotesEditor = new LineBreakNotesEditor(barSubItemLinkPropertiesNotes);
			lineBreakNotesEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_sigleLinkContextMenuEditors.Add(lineBreakNotesEditor);

			var libraryObjectFormatEditor = new LibraryObjectTextFormatEditor(barSubItemLinkPropertiesTextFormat);
			libraryObjectFormatEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_sigleLinkContextMenuEditors.Add(libraryObjectFormatEditor);

			var lineBreakFormatEditor = new LineBreakTextFormatEditor(barSubItemLinkPropertiesTextFormat);
			lineBreakFormatEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_sigleLinkContextMenuEditors.Add(lineBreakFormatEditor);

			var documentSettingsEditor = new Links.ContextMenuEdit.SingleLink.DocumentSettingsEditor(barSubItemLinkPropertiesAdminSettings);
			documentSettingsEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_sigleLinkContextMenuEditors.Add(documentSettingsEditor);

			var excelSettingsEditor = new Links.ContextMenuEdit.SingleLink.ExcelSettingsEditor(barSubItemLinkPropertiesAdminSettings);
			excelSettingsEditor.EditValueChanged += OnSingleLinkContextEditorValueChanged;
			_sigleLinkContextMenuEditors.Add(excelSettingsEditor);

			popupMenuLinkProperties.CloseUp += OnLinkPropertiesMenuCloseUp;
		}

		private void LoadSingleLinkContextMenuEditors(BaseLibraryLink link)
		{
			_sigleLinkContextMenuEditors.ForEach(e => e.LoadLink(link));
		}

		private void ApplySingleLinkContextMenuEditorChanges()
		{
			_sigleLinkContextMenuEditors.ForEach(e => e.ApplyChanges());
		}

		private void InitLinksGroupContextMenuEditors()
		{
			var pdfSettingsEditor = new PdfSettingsEditor(barSubItemFolderPropertiesLinkPdfSetings);
			pdfSettingsEditor.EditValueChanged += OnLinksGroupContextEditorValueChanged;
			_linksGroupContextMenuEditors.Add(pdfSettingsEditor);

			var excelSettingsEditor = new Links.ContextMenuEdit.LinksGroup.ExcelSettingsEditor(barSubItemFolderPropertiesLinkExcelSetings);
			excelSettingsEditor.EditValueChanged += OnLinksGroupContextEditorValueChanged;
			_linksGroupContextMenuEditors.Add(excelSettingsEditor);

			popupMenuFolderProperties.CloseUp += OnFolderPropertiesMenuCloseUp;
		}

		private void LoadLinksGroupContextMenuEditors()
		{
			_linksGroupContextMenuEditors.ForEach(e => e.LoadLinks(DataSource.AllLinks));
		}

		private void ApplyLinksGroupContextMenuEditorChanges()
		{
			_linksGroupContextMenuEditors.ForEach(e => e.ApplyChanges());
		}
		#endregion

		#region Link
		private void barButtonItemLinkPropertiesCopy_ItemClick(object sender, ItemClickEventArgs e)
		{
			CopyLinks();
		}

		private void barButtonItemLinkPropertiesCut_ItemClick(object sender, ItemClickEventArgs e)
		{
			CutLinks();
		}

		private void barButtonItemLinkPropertiesPaste_ItemClick(object sender, ItemClickEventArgs e)
		{
			PasteLinks();
		}

		private void barButtonItemLinkPropertiesOpenLink_ItemClick(object sender, ItemClickEventArgs e)
		{
			OpenLink();
		}

		private void barButtonItemLinkPropertiesFileLocation_ItemClick(object sender, ItemClickEventArgs e)
		{
			OpenLinkLocation();
		}

		private void barButtonItemLinkPropertiesDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteLink();
		}

		private void barButtonItemLinkPropertiesRefreshPreview_ItemClick(object sender, ItemClickEventArgs e)
		{
			RefreshPreviewFiles();
		}

		private void barButtonItemLinkPropertiesEditImageSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditImageSettings();
		}

		private void barButtonItemLinkPropertiesLinkSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Notes);
		}

		private void barButtonItemLinkPropertiesAdvancedSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditLinkSettings(LinkSettingsType.AdvancedSettings);
		}

		private void barButtonItemLinkPropertiesTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Tags);
		}

		private void barButtonItemLinkPropertiesExpirationDate_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditLinkSettings(LinkSettingsType.ExpirationDate);
		}

		private void barButtonItemLinkPropertiesSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Security);
		}

		private void barButtonItemLinkPropertiesWidget_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Widget);
		}

		private void barButtonItemLinkPropertiesBanner_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Banner);
		}

		private void barButtonItemLinkPropertiesResetSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetLinkSettings();
		}

		private void OnLinkPropertiesMenuCloseUp(object sender, EventArgs e)
		{
			ApplySingleLinkContextMenuEditorChanges();
		}
		#endregion

		#region Folder
		private void barButtonItemFolderPropertiesFolderSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditFolderSettings();
		}

		private void barButtonItemFolderPropertiesDeleteLinks_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteLinks();
		}

		private void barButtonItemFolderPropertiesDeleteFolder_ItemClick(object sender, ItemClickEventArgs e)
		{
			DeleteFolder();
		}

		private void barButtonItemFolderPropertiesResetLinkSetings_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetAllLinksInFolderSettings();
		}

		private void barButtonItemFolderPropertiesFolderWidget_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditFolderWidget();
		}

		private void barButtonItemFolderPropertiesFolderBanner_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditFolderBanner();
		}

		private void barButtonItemFolderPropertiesImageSettings_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditFolderImageSettings();
		}

		private void barButtonItemFolderPropertiesDeleteLinkWidgets_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetWidgets();
		}

		private void barButtonItemFolderPropertiesDeleteLinkBanners_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetBanners();
		}

		private void barButtonItemFolderPropertiesEditLinkTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditLinkSettings(DataSource.Links.ToList(), LinkSettingsType.Tags);
		}

		private void barButtonItemFolderPropertiesDeleteLinkSecurity_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetSecurity();
		}

		private void barButtonItemFolderPropertiesDeleteLinkTags_ItemClick(object sender, ItemClickEventArgs e)
		{
			ResetTags();
		}

		private void barButtonItemFolderPropertiesSortLinks_ItemClick(object sender, ItemClickEventArgs e)
		{
			SortLinkByName();
		}

		private void barButtonItemFolderPropertiesSetLinkTextWordWrap_ItemClick(object sender, ItemClickEventArgs e)
		{
			SetLinkTextWordWrap();
		}

		private void OnFolderPropertiesMenuCloseUp(object sender, EventArgs e)
		{
			ApplyLinksGroupContextMenuEditorChanges();
		}
		#endregion

		#region Security
		private void toolStripMenuItemSelectAll_Click(object sender, EventArgs e)
		{
			SelectAll();
		}

		private void toolStripMenuItemResetAll_Click(object sender, EventArgs e)
		{
			ResetSecurity();
		}
		#endregion

		#endregion
	}
}