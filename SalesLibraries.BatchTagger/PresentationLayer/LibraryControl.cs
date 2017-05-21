using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesLibraries.BatchTagger.BusinessClasses;
using SalesLibraries.Business.Contexts.Wallbin.Local;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Configuration;
using SalesLibraries.ServiceConnector.Models.Rest.BatchTagger;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.ServiceConnector.WallbinContentService;

namespace SalesLibraries.BatchTagger.PresentationLayer
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl
	public partial class LibraryControl : XtraTabPage
	{
		public List<LibraryFilesModel> Records { get; }

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

		private void DownloadSourceFile(LibraryFilesModel targetModel)
		{
			if (String.IsNullOrEmpty(targetModel?.downloadUrl)) return;
			using (var saveDialog = new SaveFileDialog())
			{
				saveDialog.Title = "Download File";
				saveDialog.FileName = targetModel.fileName;
				saveDialog.InitialDirectory = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
					"Downloads");
				if (saveDialog.ShowDialog(AppManager.Instance.MainForm) != DialogResult.Cancel)
				{
					var filePath = saveDialog.FileName;
					using (var form = new FormProgress())
					{
						form.laProgress.Text = "Downloading file...";
						form.TopMost = true;
						var thread = new Thread(() =>
						{
							using (var webClient = new WebClient())
							{
								webClient.DownloadFile(targetModel.downloadUrl, filePath);
							}
						});
						form.Show();
						thread.Start();
						while (thread.IsAlive)
						{
							Thread.Sleep(100);
							Application.DoEvents();
						}
						form.Close();
					}
					if (File.Exists(filePath))
						Process.Start(filePath);
				}
			}
		}

		private void EditSourceFileTags(LibraryFilesModel targetModel)
		{
			var libraryDatabaseFilePath = Path.Combine(Path.GetTempPath(), Constants.LocalStorageFileName);
			var tempDatabaseFilePath = Path.GetTempFileName();
			EditTagsLibraryContext libraryContext = null;
			BaseLibraryLink sourceLink = null;
			using (var formProgress = new FormProgress())
			{
				formProgress.TopMost = true;

				formProgress.laProgress.Text = "Loading Library Database...";
				var thread = new Thread(() =>
				{
					using (var webClient = new WebClient())
					{
						webClient.DownloadFile(targetModel.libraryDatabaseUrl, libraryDatabaseFilePath);
					}
					if (File.Exists(libraryDatabaseFilePath))
					{
						File.Copy(libraryDatabaseFilePath, tempDatabaseFilePath, true);
						libraryContext = new EditTagsLibraryContext(Constants.PrimaryFileStorageName, tempDatabaseFilePath);
						var linkId = Guid.Parse(targetModel.linkId);
						sourceLink = libraryContext.Library.Pages
							.SelectMany(p => p.AllGroupLinks)
							.FirstOrDefault(link => link.ExtId == linkId);
					}
				});
				formProgress.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				formProgress.Close();
			}

			if (libraryContext != null)
			{
				if (sourceLink != null)
				{
					using (var formEditLinkTags = new FormEditLinkTags(sourceLink))
					{
						formEditLinkTags.LoadData();
						if (formEditLinkTags.ShowDialog(AppManager.Instance.MainForm) == DialogResult.OK)
						{
							libraryContext.SaveChanges();
							using (var formProgress = new FormProgress())
							{
								formProgress.TopMost = true;

								formProgress.laProgress.Text = "Uploading Library Database...";
								var thread = new Thread(() =>
								{
									File.Copy(tempDatabaseFilePath, libraryDatabaseFilePath, true);

									var fileCategories = new List<LinkCategory>();
									foreach (var searchGroup in sourceLink.Tags.Categories.Union(sourceLink.TopLevelLink.Tags.Categories))
										foreach (var tag in searchGroup.Tags)
										{
											var category = new LinkCategory();
											category.libraryId = sourceLink.ParentLibrary.ExtId.ToString();
											category.linkId = sourceLink.ExtId.ToString();
											category.group = searchGroup.SuperGroup;
											category.category = searchGroup.Name;
											category.tag = tag.Name;
											fileCategories.Add(category);
										}

									var requestData = new BatchTaggerSetRequestData()
									{
										LibraryId = libraryContext.Library.ExtId,
										LinkId = sourceLink.ExtId,
										Categories = fileCategories.ToArray(),
										Keywords = String.Join(" ", sourceLink.Tags.Keywords.Union(sourceLink.TopLevelLink.Tags.Keywords).Select(x => x.Name).ToArray()),
										EncodedDatabase = Convert.ToBase64String(File.ReadAllBytes(libraryDatabaseFilePath))
									};

									AppManager.Instance.Connections.RestConnection.DoRequest(requestData, "Error uploading updates on server");

									targetModel.categories = String.Join(", ", requestData.Categories.Select(lc => lc.tag));
									targetModel.keywords = requestData.Keywords;
								});
								formProgress.Show();
								thread.Start();
								while (thread.IsAlive)
								{
									Thread.Sleep(100);
									Application.DoEvents();
								}
								formProgress.Close();
							}
						}
					}
				}

				libraryContext.Dispose();
			}



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
				DownloadSourceFile(fileModel);
			})
			{ BeginGroup = true });

			e.Menu.Items.Add(new DXMenuItem("Edit tags", (o, args) =>
			{
				var fileModel = e.HitInfo.View.GetRow(e.HitInfo.RowHandle) as LibraryFilesModel;
				EditSourceFileTags(fileModel);
				advBandedGridViewData.RefreshData();
				gridControlData.Update();
			})
			{ BeginGroup = true });
		}
	}
}