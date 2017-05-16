using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.PresentationClasses.Common;
using SalesLibraries.SiteManager.ToolClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.LibraryFiles
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl
	public partial class LibraryControl : XtraTabPage, IGroupControl
	{
		public List<LibraryFilesModel> Records { get; private set; }

		private string _libraryName;

		public string GroupName
		{
			get { return _libraryName; }
			set
			{
				_libraryName = String.IsNullOrEmpty(value) ? "No Group" : value;
				Text = _libraryName;
			}
		}

		public LibraryControl(IEnumerable<LibraryFilesModel> records)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Records = new List<LibraryFilesModel>();
			Records.AddRange(records);
			gridControlData.DataSource = Records;
		}

		public PrintableComponentLink GetPrintLink()
		{
			advBandedGridViewData.CheckLoaded();
			var printLink = new PrintableComponentLink()
			{
				Landscape = true,
				PaperKind = PaperKind.A4,
				Component = gridControlData
			};
			return printLink;
		}

		public void ApplyFilter(LibraryFilter filterControlLibrary)
		{
			if (!filterControlLibrary.EnableFilter || filterControlLibrary.ShowAllLinks)
				gridControlData.DataSource = Records;
			else if (filterControlLibrary.ShowUntaggedLinks)
				gridControlData.DataSource = Records.Where(r => !r.HasCategories).ToList();
			else if (filterControlLibrary.ShowNokeywordLinks)
				gridControlData.DataSource = Records.Where(r => !r.HasKeywords).ToList();
			advBandedGridViewData.RefreshData();
			gridControlData.Update();
		}

		private void OnCustomDrawRowPreview(object sender, RowObjectCustomDrawEventArgs e)
		{
			if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}

		private void OnCustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}

		private void OnPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!(e.HitInfo.InRowCell || e.HitInfo.InRow || e.HitInfo.InDataRow)) return;
			e.Menu.Items.Add(new DXMenuItem("Preview this file", (o, args) =>
			{
				var fileModel = e.HitInfo.View.GetRow(e.HitInfo.RowHandle) as LibraryFilesModel;
				if (String.IsNullOrEmpty(fileModel?.previewUrl)) return;
				Process.Start(fileModel.previewUrl);
			}));
			e.Menu.Items.Add(new DXMenuItem("Download this file", (o, args) =>
			{
				var fileModel = e.HitInfo.View.GetRow(e.HitInfo.RowHandle) as LibraryFilesModel;
				if (String.IsNullOrEmpty(fileModel?.downloadUrl)) return;
				using (var saveDialog = new SaveFileDialog())
				{
					saveDialog.Title = "Download File";
					saveDialog.FileName = fileModel.fileName;
					saveDialog.InitialDirectory = Path.Combine(
						Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
						"Downloads");
					if (saveDialog.ShowDialog(FormMain.Instance) != DialogResult.Cancel)
					{
						var filePath = saveDialog.FileName;
						using (var form = new FormProgress())
						{
							FormMain.Instance.ribbonControl.Enabled = false;
							Enabled = false;
							form.laProgress.Text = "Downloading file...";
							form.TopMost = true;
							var thread = new Thread(() =>
							{
								var webClient = new WebClient();
								webClient.DownloadFile(fileModel.downloadUrl, filePath);
							});
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
						if (File.Exists(filePath))
							Process.Start(filePath);
					}
				}
			})
			{ BeginGroup = true });
		}
	}
}