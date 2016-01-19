using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Models;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Video
{
	public partial class VideoContentEditor : UserControl
	{
		private bool _isDataChanged;
		private LibraryContext _libraryContext;
		private readonly List<VideoInfo> _videoInfoList = new List<VideoInfo>();

		public VideoContentEditor()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void LoadLibrary(LibraryContext libraryContext)
		{
			_libraryContext = libraryContext;
		}

		public void LoadVideoInfo()
		{
			_videoInfoList.Clear();
			MainController.Instance.ProcessManager.RunInQueue("Loading Video...",
				LoadVideoInfoInternal,
					() => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
					{
						gridControlVideo.DataSource = _videoInfoList;
						gridViewVideo.RefreshData();
						laVideoTitle.Text = _videoInfoList.Count > 0 ?
							String.Format("Your Library has {0} Video File{1}",
								_videoInfoList.Count, (_videoInfoList.Count > 1 ? "s" : String.Empty)) :
							"Your Library does not have any Video Files";
					}))
				);
		}

		private void LoadVideoInfoInternal()
		{
			_videoInfoList.AddRange(_libraryContext.Library.PreviewContainers
				.OfType<VideoPreviewContainer>()
				.Where(videContainer => _libraryContext.Library.GetPreviewableLinksBySourcePath(videContainer.SourcePath).Any())
				.Select(VideoInfo.Create));
			var i = 1;
			_videoInfoList.Sort((x, y) =>
			{
				if (x.Converted == y.Converted)
					return WinAPIHelper.StrCmpLogicalW(x.SourceFileName, y.SourceFileName);
				return y.Converted ? 1 : -1;
			});
			foreach (var videoInfo in _videoInfoList)
			{
				videoInfo.Index = i;
				i++;
			}
		}

		public void ProcessChanges()
		{
			if (!_isDataChanged) return;
			_libraryContext.SaveChanges();
			_isDataChanged = false;
		}

		public void ConvertSelected()
		{
			var selectedVideos = _videoInfoList.Where(vi => vi.Selected).ToList();
			if (!selectedVideos.Any())
			{
				MainController.Instance.PopupMessages.ShowWarning("Please select one or several videos in the list below");
				return;
			}
			Convert(selectedVideos);
		}

		private void Convert(IEnumerable<VideoInfo> videoInfos)
		{
			ProcessChanges();
			Enabled = false;
			MainController.Instance.MainForm.ribbonControl.Enabled = false;
			var form = new FormProgressConvertVideo();
			var savedState = MainController.Instance.MainForm.WindowState;
			if (_libraryContext.Library.SyncSettings.MinimizeOnSync)
				MainController.Instance.MainForm.WindowState = FormWindowState.Minimized;
			MainController.Instance.ProcessManager.RunWithProgress(form, true,
				cancellationToken =>
				{
					foreach (var videoInfo in videoInfos)
					{
						if (cancellationToken.IsCancellationRequested) return;
						videoInfo.UpdateContent(cancellationToken);
					}
				},
				cancellationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					MainController.Instance.MainForm.WindowState = savedState;
					LoadVideoInfo();
					MainController.Instance.MainForm.ribbonControl.Enabled = true;
					Enabled = true;
					_isDataChanged = true;
				})));
		}

		public void DeleteSelected()
		{
			var selectedVideos = _videoInfoList.Where(vi => vi.Selected).ToList();
			if (!selectedVideos.Any())
			{
				MainController.Instance.PopupMessages.ShowWarning("Please select one or several videos in the list below");
				return;
			}
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want To Remove this Video From your library?") != DialogResult.Yes)
				return;
			Delete(selectedVideos);
		}

		private void Delete(IEnumerable<VideoInfo> videoInfos)
		{
			ProcessChanges();
			MainController.Instance.ProcessManager.Run("Deleting Video...", cancellationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
			{
				foreach (var videoInfo in videoInfos)
				{
					if (cancellationToken.IsCancellationRequested) return;
					videoInfo.DeleteWithLinks();
					Application.DoEvents();
				}
			})));
			LoadVideoInfo();
			_isDataChanged = true;
		}

		private void ClearContent(IEnumerable<VideoInfo> videoInfos)
		{
			MainController.Instance.ProcessManager.Run("Deleting Video...",
				cancelationToken => videoInfos.ToList().ForEach(vi => vi.ClearContent()));
			_isDataChanged = true;
		}

		private void buttonXSelectAll_Click(object sender, EventArgs e)
		{
			_videoInfoList.ForEach(vi => vi.Selected = true);
			gridViewVideo.RefreshData();
		}

		private void buttonXClearAll_Click(object sender, EventArgs e)
		{
			_videoInfoList.ForEach(vi => vi.Selected = false);
			gridViewVideo.RefreshData();
		}

		#region Grid Editors Click Handlers
		private void repositoryItemButtonEditVideoMp4_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var videoInfo = gridViewVideo.GetFocusedRow() as VideoInfo;
			if (videoInfo == null) return;
			if (File.Exists(videoInfo.Mp4FilePath))
				VideoHelper.PlayVideo(videoInfo.Mp4FilePath);
			else
				MainController.Instance.PopupMessages.ShowWarning("You need to convert this video first!");
		}

		private void repositoryItemButtonEditVideoMp4_Click(object sender, EventArgs e)
		{
			repositoryItemButtonEditVideoMp4_ButtonClick(this, new ButtonPressedEventArgs(repositoryItemButtonEditVideoMp4.Buttons[0]));
		}

		private void repositoryItemButtonEditVideoFolder_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var videoInfo = gridViewVideo.GetFocusedRow() as VideoInfo;
			if (videoInfo == null) return;
			var folderPath = gridViewVideo.FocusedColumn == gridColumnVideoSourceFolder ?
				videoInfo.SourceFolderPath :
				videoInfo.PreviewContainerPath;
			if (Directory.Exists(folderPath))
				Process.Start(folderPath);
			else
				MainController.Instance.PopupMessages.ShowWarning("The folder is unavailable");
		}

		private void repositoryItemButtonEditVideoConvert_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var videoInfo = gridViewVideo.GetFocusedRow() as VideoInfo;
			if (videoInfo == null) return;
			Convert(new[] { videoInfo });
			LoadVideoInfo();
		}

		private void repositoryItemButtonEditVideoRefersh_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var videoInfo = gridViewVideo.GetFocusedRow() as VideoInfo;
			if (videoInfo == null) return;
			ClearContent(new[] { videoInfo });
			Convert(new[] { videoInfo });
			LoadVideoInfo();
		}

		private void repositoryItemCheckEdit_EditValueChanged(object sender, EventArgs e)
		{
			gridViewVideo.CloseEditor();
		}
		#endregion

		#region Grid Formatting
		private void gridViewVideo_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var videoInfo = gridViewVideo.GetRow(e.RowHandle) as VideoInfo;
			if (videoInfo == null) return;
			if (e.Column == gridColumnVideoMp4FileName)
				e.Appearance.ForeColor = String.IsNullOrEmpty(videoInfo.Mp4FilePath) ? Color.Red : Color.Green;
			e.Appearance.BackColor = videoInfo.Selected ? Color.LightGreen : Color.White;
		}

		private void gridViewVideo_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			var videoInfo = gridViewVideo.GetRow(e.RowHandle) as VideoInfo;
			if (videoInfo == null) return;
			var videoConverted = videoInfo.Converted;
			if (e.Column == gridColumnVideoIPadFolder)
				e.RepositoryItem = videoConverted ? repositoryItemButtonEditVideoFolderEnabled : repositoryItemButtonEditVideoFolderDisabled;
			else if (e.Column == gridColumnVideoConvert)
				e.RepositoryItem = videoConverted ? repositoryItemButtonEditVideoConvertDisabled : repositoryItemButtonEditVideoConvertEnabled;
		}

		private void gridViewVideo_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
		{
			var vi = (GridViewInfo)gridViewVideo.GetViewInfo();
			var ri = vi.RowsInfo.GetInfoByHandle(e.RowHandle) as GridDataRowInfo;
			if (ri == null) return;
			e.RepositoryItem.Appearance.ForeColor = ri.Cells[e.Column].Appearance.ForeColor;
			e.RepositoryItem.Appearance.BackColor = ri.Cells[e.Column].Appearance.BackColor;
		}
		#endregion
	}
}
