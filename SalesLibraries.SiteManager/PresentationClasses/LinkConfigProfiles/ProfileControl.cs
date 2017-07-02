using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using SalesLibraries.ServiceConnector.LinkConfigProfileService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ToolForms;
using Application = System.Windows.Forms.Application;

namespace SalesLibraries.SiteManager.PresentationClasses.LinkConfigProfiles
{
	[ToolboxItem(false)]
	public partial class ProfileControl : UserControl
	{
		private bool _loading;
		private bool _dataLoaded;
		private readonly LinkConfigProfileModel _dataSource;

		public bool NeedToSave { get; private set; }

		public LinkConfigProfileModel Profile => _dataSource;

		public string ProfileName
		{
			get { return _dataSource.name; }
			set { _dataSource.name = value; }
		}

		public ProfileControl(LinkConfigProfileModel dataSource)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_dataSource = dataSource;
			NeedToSave = true;

			if (CreateGraphics().DpiX > 96)
			{
				gridColumnFilesSelected.Width =
					RectangleHelper.ScaleHorizontal(gridColumnFilesSelected.Width, gridControlFiles.ScaleFactor.Width);
			}
		}

		#region Common Methods
		public void LoadData()
		{
			_loading = true;

			checkEditDisablePreview.Checked = Profile.config.disablePreview;
			checkEditDisableDownload.Checked = Profile.config.disableDownload;
			checkEditDisableEmail.Checked = Profile.config.disableEmail;
			checkEditDisableFavorites.Checked = Profile.config.disableFavorites;
			checkEditDisablePdf.Checked = Profile.config.disablePdf;
			checkEditDisableQuickSite.Checked = Profile.config.disableQuickSite;
			checkEditDisableSave.Checked = Profile.config.disableSave;

			var libraries = LinkConfigProfilesDataHelper.Libraries.Select(l => l.Clone()).ToList();
			foreach (var libraryReference in libraries)
			{
				if (Profile.config.libraryReferences.Any(l => l.id == libraryReference.id))
					libraryReference.Selected = true;
			}
			gridControlLibraries.DataSource = libraries;
			UpdateLibrariesTotals();

			var securityGroups = LinkConfigProfilesDataHelper.SecurityGroups.Select(g => g.Clone()).ToList();
			foreach (var groupReference in securityGroups)
			{
				if (Profile.config.securityGroupReferences.Any(g => g.id == groupReference.id))
					groupReference.Selected = true;
			}
			gridControlSecurityGroups.DataSource = securityGroups;
			UpdateSecurityGroupsTotals();

			textEditFilesTags.EditValue = String.Join("; ", Profile.config.libraryLinkTags);

			_loading = false;

			LoadAffectedLinks();

			_dataLoaded = true;
			NeedToSave = false;
		}

		public void SaveData()
		{
			gridViewLibraries.CloseEditor();
			gridViewSecurityGroups.CloseEditor();
			gridViewFiles.CloseEditor();

			if (!NeedToSave) return;

			if (_dataLoaded)
			{
				Profile.config.disablePreview = checkEditDisablePreview.Checked;
				Profile.config.disableDownload = checkEditDisableDownload.Checked;
				Profile.config.disableEmail = checkEditDisableEmail.Checked;
				Profile.config.disableFavorites = checkEditDisableFavorites.Checked;
				Profile.config.disablePdf = checkEditDisablePdf.Checked;
				Profile.config.disableQuickSite = checkEditDisableQuickSite.Checked;
				Profile.config.disableSave = checkEditDisableSave.Checked;

				Profile.config.libraryReferences = ((List<LibraryReference>)gridControlLibraries.DataSource).Where(l => l.Selected).ToArray();
				Profile.config.securityGroupReferences = ((List<SecurityGroupReference>)gridControlSecurityGroups.DataSource).Where(l => l.Selected).ToArray();

				var linkTags = textEditFilesTags.EditValue as String;
				if (!String.IsNullOrEmpty(linkTags))
					Profile.config.libraryLinkTags = linkTags.Split(';').Where(item => !String.IsNullOrEmpty(item)).Select(item => item.Trim()).ToArray();
				else
					Profile.config.libraryLinkTags = new string[] { };

				Profile.config.ignoredLinkReferences = ((List<LibraryLinkReference>)gridControlFiles.DataSource).Where(linkReference => linkReference.Selected).ToArray();
			}

			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Saving Profile...";
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.SaveLinkConfigProfile(Profile));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}

			NeedToSave = false;
		}

		private void OnDataChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			NeedToSave = true;
		}
		#endregion

		#region Libraries
		private void UpdateLibrariesTotals()
		{
			var libraryReferences = ((List<LibraryReference>)gridControlLibraries.DataSource);

			var flaggedLibraries = libraryReferences.Count(l => l.Selected);
			var ignoredLibraries = libraryReferences.Count(l => !l.Selected);

			labelControlLibrariesTotals.Text = String.Format("Flagged Libraries: <b>{0}</b>    Ignored Libraries: <b>{1}</b>", flaggedLibraries, ignoredLibraries);
		}

		private void OnLibrariesSelectAll(object sender, EventArgs e)
		{
			((List<LibraryReference>)gridControlLibraries.DataSource).ForEach(l => l.Selected = true);
			gridViewLibraries.RefreshData();
			NeedToSave = true;
		}

		private void OnLibrariesClearAll(object sender, EventArgs e)
		{
			((List<LibraryReference>)gridControlLibraries.DataSource).ForEach(l => l.Selected = false);
			gridViewLibraries.RefreshData();
			NeedToSave = true;
		}

		private void repositoryItemCheckEditLibraries_CheckedChanged(object sender, EventArgs e)
		{
			gridViewLibraries.CloseEditor();
		}

		private void OnLibrariesGridDataChanged(object sender, CellValueChangedEventArgs e)
		{
			UpdateLibrariesTotals();
			OnDataChanged(sender, e);
		}
		#endregion

		#region Security Groups
		private void UpdateSecurityGroupsTotals()
		{
			var securityGroupReferences = ((List<SecurityGroupReference>)gridControlSecurityGroups.DataSource);

			var flaggedSecurityGroups = securityGroupReferences.Count(l => l.Selected);
			var ignoredSecurityGroups = securityGroupReferences.Count(l => !l.Selected);

			labelControlSecurityGroupsTotals.Text = String.Format("Flagged Groups: <b>{0}</b>    Ignored Groups: <b>{1}</b>", flaggedSecurityGroups, ignoredSecurityGroups);
		}

		private void OnSecurityGroupsSelectAll(object sender, EventArgs e)
		{
			((List<SecurityGroupReference>)gridControlSecurityGroups.DataSource).ForEach(g => g.Selected = true);
			gridViewSecurityGroups.RefreshData();
			NeedToSave = true;
		}

		private void OnSecurityGroupsClearAll(object sender, EventArgs e)
		{
			((List<SecurityGroupReference>)gridControlSecurityGroups.DataSource).ForEach(g => g.Selected = false);
			gridViewSecurityGroups.RefreshData();
			NeedToSave = true;
		}

		private void repositoryItemCheckEditSecurityGroups_CheckedChanged(object sender, EventArgs e)
		{
			gridViewSecurityGroups.CloseEditor();
		}

		private void OnSecurityGroupGridDataChanged(object sender, CellValueChangedEventArgs e)
		{
			UpdateSecurityGroupsTotals();
			OnDataChanged(sender, e);
		}
		#endregion

		#region Files
		public void ExportFiles()
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				dialog.FileName = string.Format("{0} Files ({1}).xlsx", ProfileName, DateTime.Now.ToString("MMddyy-hmmtt"));
				dialog.Filter = "Excel files|*.xlsx";
				dialog.Title = "Export Files";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					var options = new XlsxExportOptions();
					options.SheetName = Path.GetFileNameWithoutExtension(dialog.FileName);
					options.TextExportMode = TextExportMode.Text;
					options.ExportHyperlinks = true;
					options.ShowGridLines = true;
					options.ExportMode = XlsxExportMode.SingleFile;
					printableComponentLink.CreateDocument();
					printableComponentLink.PrintingSystem.ExportToXlsx(dialog.FileName, options);

					if (File.Exists(dialog.FileName))
						Process.Start(dialog.FileName);
				}
			}
		}

		private void LoadAffectedLinks()
		{
			_loading = true;
			gridControlFiles.DataSource = null;
			var affectedLinks = new List<LibraryLinkReference>();
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Loading Files...";
				form.TopMost = true;
				var thread = new Thread(() => affectedLinks.AddRange(WebSiteManager.Instance.SelectedSite.GetLinkConfigProfileAffectedLinks(Profile)));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}
			foreach (var linkReference in affectedLinks)
			{
				if (Profile.config.ignoredLinkReferences.Any(ignoredLink => ignoredLink.id == linkReference.id))
					linkReference.Selected = true;
			}
			gridControlFiles.DataSource = affectedLinks;
			UpdateFilesTotals();
			_loading = false;
		}

		private void UpdateFilesTotals()
		{
			var links = ((List<LibraryLinkReference>)gridControlFiles.DataSource);

			var totalLinks = links.Count;
			var flaggedLinks = links.Count(l => !l.Selected);
			var ignoredLinks = links.Count(l => l.Selected);

			labelControlFilesTotals.Text = String.Format("Total: <b>{0}</b>    Flagged: <b>{1}</b>    Ignored: <b>{2}</b>", totalLinks, flaggedLinks, ignoredLinks);
		}

		private void OnFilesRefresh(object sender, EventArgs e)
		{
			SaveData();
			LoadAffectedLinks();
		}

		private void OnFilesSelectAll(object sender, EventArgs e)
		{
			((List<LibraryLinkReference>)gridControlFiles.DataSource).ForEach(f => f.Selected = true);
			gridViewFiles.RefreshData();
			NeedToSave = true;
		}

		private void OnFilesClearAll(object sender, EventArgs e)
		{
			((List<LibraryLinkReference>)gridControlFiles.DataSource).ForEach(f => f.Selected = false);
			gridViewFiles.RefreshData();
			NeedToSave = true;
		}
		private void repositoryItemCheckEditFiles_CheckedChanged(object sender, EventArgs e)
		{
			gridViewFiles.CloseEditor();
		}

		private void OnFilesGridDataChanged(object sender, CellValueChangedEventArgs e)
		{
			UpdateFilesTotals();
			OnDataChanged(sender, e);
		}

		private void OnGridFilesRowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			var linkReference = gridViewFiles.GetRow(e.RowHandle) as LibraryLinkReference;
			if (linkReference == null) return;
			e.Appearance.ForeColor = linkReference.Selected ? Color.Red : Color.Black;
		}
		#endregion
	}
}

