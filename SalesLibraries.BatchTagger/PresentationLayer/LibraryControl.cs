using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesLibraries.BatchTagger.BusinessClasses;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
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

			ApplyData(records);

			RestoreLayout();
		}

		public void ApplyData(IEnumerable<LibraryFilesModel> records)
		{
			Records.Clear();
			Records.AddRange(records);
			gridControlData.DataSource = Records;
			advBandedGridViewData.RefreshData();
			gridControlData.Update();
		}

		public void SaveLayout()
		{
			advBandedGridViewData.SaveLayoutToXml(AppManager.Instance.Resources.LibraryControlLayoutConfigPath);
		}

		public void RestoreLayout()
		{
			if (File.Exists(AppManager.Instance.Resources.LibraryControlLayoutConfigPath))
				advBandedGridViewData.RestoreLayoutFromXml(AppManager.Instance.Resources.LibraryControlLayoutConfigPath);
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
					AppManager.Instance.ProcessManager.Run("Downloading file...", (cancelationToken, formProgess) =>
					{
						using (var webClient = new WebClient())
						{
							webClient.DownloadFile(targetModel.downloadUrl, filePath);
						}
					});
					if (File.Exists(filePath))
						Process.Start(filePath);
				}
			}
		}

		private void EditSourceFileTags(IList<LibraryFilesModel> targetModels)
		{
			var libraryCredentials =
				AppManager.Instance.LibraryCredentials.Items.FirstOrDefault(
					i => String.Equals(i.LibraryName, targetModels.First().library, StringComparison.OrdinalIgnoreCase));
			if (libraryCredentials == null)
			{
				using (var formCredentials = new FormLibraryCredentials())
				{
					if (formCredentials.ShowDialog(AppManager.Instance.MainForm) == DialogResult.OK)
					{
						libraryCredentials = formCredentials.GetCredentials();
						libraryCredentials.LibraryName = targetModels.First().library;
						AppManager.Instance.LibraryCredentials.Items.Add(libraryCredentials);
						AppManager.Instance.LibraryCredentials.Save();
					}
				}
			}

			if (libraryCredentials == null) return;

			var client = new WebDAVClient.Client(new NetworkCredential
			{
				UserName = libraryCredentials.User,
				Password = libraryCredentials.Password
			})
			{
				Server = AppManager.Instance.Connections.WebDavSite
			};

			EditTagsLibraryContext libraryContext = null;
			var sourceLinks = new List<BaseLibraryLink>();

			var libraryDatabaseRemoteFilePath = String.Format("/{0}", String.Join(@"/", libraryCredentials.LibraryName, Constants.LocalStorageFileName));
			var libraryDatabaseLocalFilePath = Path.Combine(Path.GetTempPath(), Constants.LocalStorageFileName);
			var tempDatabaseFilePath = Path.GetTempFileName();

			AppManager.Instance.ProcessManager.Run("Loading Library Database...", (cancelationToken, formProgess) =>
			{
				var remoteFile = client.GetFile(libraryDatabaseRemoteFilePath).Result;
				using (var localStream = File.Create(libraryDatabaseLocalFilePath))
				{
					using (var remoteStream = client.Download(libraryDatabaseRemoteFilePath).Result)
					{
						if (remoteStream != null)
						{
							var contentLenght = remoteFile.ContentLength ?? 0;
							var bufferSize = contentLenght / 10;
							var buffer = new byte[bufferSize];
							int bytesRead;
							do
							{
								bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
								localStream.Write(buffer, 0, bytesRead);
							} while (bytesRead > 0);
							remoteStream.Close();
						}
					}
					localStream.Close();
				}
				if (File.Exists(libraryDatabaseLocalFilePath))
				{
					File.Copy(libraryDatabaseLocalFilePath, tempDatabaseFilePath, true);
					libraryContext = new EditTagsLibraryContext(Constants.PrimaryFileStorageName, tempDatabaseFilePath);
					sourceLinks.AddRange(libraryContext.Library.Pages
						.SelectMany(p => p.AllGroupLinks)
						.Where(link => targetModels.Select(tm => Guid.Parse(tm.linkId)).Contains(link.ExtId)));
				}
			});

			if (libraryContext == null) return;

			if (sourceLinks.Any())
			{
				using (var formEditLinkTags = new FormEditLinkTags(sourceLinks))
				{
					formEditLinkTags.InitForm();
					if (formEditLinkTags.ShowDialog(AppManager.Instance.MainForm) == DialogResult.OK)
					{
						libraryContext.SaveChanges();
						AppManager.Instance.ProcessManager.Run("Uploading Library Database...", (cancelationToken, formProgess) =>
						{
							var linkInfoList = new List<LinkTagsInfo>();
							foreach (var sourceLink in sourceLinks)
							{
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
								linkInfoList.Add(new LinkTagsInfo
								{
									LinkId = sourceLink.ExtId,
									Categories = fileCategories.ToArray(),
									Keywords = String.Join(" ", sourceLink.Tags.Keywords.Union(sourceLink.TopLevelLink.Tags.Keywords).Select(x => x.Name).ToArray()),
								});
							}

							var requestData = new BatchTaggerSetRequestData()
							{
								LinkInfo = linkInfoList.ToArray()
							};

							AppManager.Instance.Connections.RestConnection.DoRequest(requestData, "Error uploading updates on server");

							File.Copy(tempDatabaseFilePath, libraryDatabaseLocalFilePath, true);
							AsyncHelper.RunSync(() => client.Upload(libraryDatabaseRemoteFilePath, File.OpenRead(libraryDatabaseLocalFilePath), String.Empty));

							foreach (var targetModel in targetModels)
							{
								var tagsInfo = linkInfoList.First(linkTagsInfo => linkTagsInfo.LinkId == Guid.Parse(targetModel.linkId));
								targetModel.categories = String.Join(", ", tagsInfo.Categories.Select(lc => lc.tag));
								targetModel.keywords = tagsInfo.Keywords;
								targetModel.linkModifyDate = DateTime.Now.ToString(CultureInfo.CurrentCulture);
							}
						});
					}
				}
			}

			libraryContext.Dispose();
		}

		private void OnCustomDrawRowPreview(object sender, RowObjectCustomDrawEventArgs e)
		{
			var selectedRows = advBandedGridViewData.GetSelectedRows();
			if (selectedRows.Contains(e.RowHandle))
				e.Appearance.BackColor = Color.FromArgb(205, 230, 247);
			else if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}

		private void OnCustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			var selectedRows = advBandedGridViewData.GetSelectedRows();
			if (selectedRows.Contains(e.RowHandle)) return;
			if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}

		private void OnPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (!(e.HitInfo.InRowCell || e.HitInfo.InRow || e.HitInfo.InDataRow)) return;

			var targetModels = advBandedGridViewData
				.GetSelectedRows()
				.Select(rowHandle => advBandedGridViewData.GetRow(rowHandle))
				.OfType<LibraryFilesModel>()
				.ToList();

			if (!targetModels.Any()) return;

			if (targetModels.Count == 1)
			{
				e.Menu.Items.Add(new DXMenuItem("Preview this file", (o, args) =>
				{
					var fileModel = targetModels.First();
					if (String.IsNullOrEmpty(fileModel.previewUrl)) return;
					Process.Start(fileModel.previewUrl);
				}));
				e.Menu.Items.Add(new DXMenuItem("Download this file", (o, args) =>
					{
						DownloadSourceFile(targetModels.First());
					})
				{ BeginGroup = true });
			}

			e.Menu.Items.Add(new DXMenuItem("Edit tags", (o, args) =>
			{
				EditSourceFileTags(targetModels);
				advBandedGridViewData.RefreshData();
				gridControlData.Update();
			})
			{ BeginGroup = targetModels.Count == 1 });
		}
	}
}