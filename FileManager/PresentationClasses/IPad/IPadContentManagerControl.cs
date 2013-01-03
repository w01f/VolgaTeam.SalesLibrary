﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FileManager.Controllers;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.ToolClasses;
using FileManager.ToolForms;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.PresentationClasses.IPad
{
	[ToolboxItem(false)]
	public partial class IPadContentManagerControl : UserControl
	{
		private readonly List<VideoInfo> _videoFiles = new List<VideoInfo>();

		public IPadContentManagerControl(LibraryDecorator parent)
		{
			InitializeComponent();
			ParentDecorator = parent;
			Dock = DockStyle.Fill;
		}

		public void UpdateControlsState()
		{
			xtraTabControl.Enabled = this.ParentDecorator.Library.IPadManager.Enabled && !string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.SyncDestinationPath) && !string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Website.Replace("http://", string.Empty)) && !string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Login) && !string.IsNullOrEmpty(this.ParentDecorator.Library.IPadManager.Password);
		}

		#region Video Tab
		public void UpdateVideoFiles()
		{
			int focussedRow = gridViewVideo.FocusedRowHandle;

			gridControlVideo.DataSource = null;
			_videoFiles.Clear();

			_videoFiles.AddRange(ParentDecorator.Library.IPadManager.VideoFiles);
			gridControlVideo.DataSource = new BindingList<VideoInfo>(_videoFiles.ToArray());
			laVideoTitle.Text = string.Format("Your Library has {0} Video File{1}", _videoFiles.Count.ToString(), (_videoFiles.Count > 1 ? "s" : string.Empty));

			if (focussedRow >= 0 && focussedRow < gridViewVideo.RowCount)
				gridViewVideo.FocusedRowHandle = focussedRow;
		}

		public void ConvertSelectedVideoFiles()
		{
			VideoInfo[] videoFiles = _videoFiles.Where(x => x.Selected).ToArray();
			if (videoFiles.Length > 0)
				ConvertVideoFiles(videoFiles);
			else
				AppManager.Instance.ShowWarning("Please select one or several videos in the list below");
		}

		private void ConvertVideoFiles(VideoInfo[] videoFiles)
		{
			using (var form = new FormProgressConverVideo())
			{
				form.ProcessAborted += (progressSender, progressE) => { Globals.ThreadAborted = true; };
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				MainController.Instance.ActiveDecorator.Save();
				var thread = new Thread(delegate()
											{
												Globals.ThreadActive = true;
												Globals.ThreadAborted = false;
												foreach (VideoInfo videoFile in videoFiles)
												{
													if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
														videoFile.Parent.UpdateContent();
													else
														break;
												}
												if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
													ParentDecorator.Library.Save();
											});
				form.Show();
				FormWindowState savedState = FormMain.Instance.WindowState;
				if (ParentDecorator.Library.MinimizeOnSync)
					FormMain.Instance.WindowState = FormWindowState.Minimized;
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				Globals.ThreadActive = false;
				Globals.ThreadAborted = false;
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;

				if (form.CloseAfterSync)
					Application.Exit();
				else
					FormMain.Instance.WindowState = savedState;
			}

			UpdateVideoFiles();
		}

		private void gridViewVideo_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			int videoIndex = gridViewVideo.GetDataSourceRowIndex(e.RowHandle);
			if (videoIndex >= 0 && videoIndex < _videoFiles.Count)
			{
				VideoInfo videoInfo = _videoFiles[videoIndex];
				if (e.Column == gridColumnVideoMp4FileName)
				{
					if (string.IsNullOrEmpty(videoInfo.Mp4FilePath))
						e.Appearance.ForeColor = Color.Red;
					else
						e.Appearance.ForeColor = Color.Black;
				}
				else if (e.Column == gridColumnVideoOgvFileName)
				{
					if (string.IsNullOrEmpty(videoInfo.OgvFilePath))
						e.Appearance.ForeColor = Color.Red;
					else
						e.Appearance.ForeColor = Color.Black;
				}
				else if (e.Column == gridColumnVideoIPadCompatible)
				{
					if (string.IsNullOrEmpty(videoInfo.Mp4FilePath))
						e.Appearance.ForeColor = Color.Red;
					else
						e.Appearance.ForeColor = Color.Green;
				}

				if (videoInfo.Selected)
					e.Appearance.BackColor = Color.LightGreen;
				else
					e.Appearance.BackColor = Color.White;
			}
		}

		private void repositoryItemButtonEditVideoWmv_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewVideo.FocusedRowHandle >= 0)
			{
				VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(gridViewVideo.FocusedRowHandle)];
				if (File.Exists(videoInfo.SourceFilePath))
				{
					VideoHelper.Instance.OpenMediaPlayer(videoInfo.SourceFilePath);
				}
				else
					AppManager.Instance.ShowWarning("You need to convert this video first!");
			}
		}

		private void repositoryItemButtonEditVideoMp4_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewVideo.FocusedRowHandle >= 0)
			{
				VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(gridViewVideo.FocusedRowHandle)];
				string filePath = string.Empty;
				if (gridViewVideo.FocusedColumn == gridColumnVideoMp4FileName)
					filePath = videoInfo.Mp4FilePath;
				else
					filePath = videoInfo.SourceFilePath;
				if (File.Exists(filePath))
				{
					VideoHelper.Instance.OpenQuickTime(filePath);
				}
				else
					AppManager.Instance.ShowWarning("You need to convert this video first!");
			}
		}

		private void repositoryItemButtonEditVideoOgv_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewVideo.FocusedRowHandle >= 0)
			{
				VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(gridViewVideo.FocusedRowHandle)];
				if (File.Exists(videoInfo.OgvFilePath))
				{
					VideoHelper.Instance.OpenFirefox(videoInfo.OgvFilePath);
				}
				else
					AppManager.Instance.ShowWarning("You need to convert this video first!");
			}
		}

		private void repositoryItemButtonEditVideoFolder_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (gridViewVideo.FocusedRowHandle >= 0)
			{
				VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(gridViewVideo.FocusedRowHandle)];
				if (e.Button.Index == 0)
				{
					if (Directory.Exists(videoInfo.IPadFolderPath))
						Process.Start(videoInfo.IPadFolderPath);
					else
						AppManager.Instance.ShowWarning("You need to convert this video first!");
				}
				else if (e.Button.Index == 1)
				{
					ConvertVideoFiles(new[] { videoInfo });
				}
			}
		}

		private void gridViewVideo_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column == gridColumnVideoSourceFileName)
			{
				VideoInfo videoInfo = _videoFiles[gridViewVideo.GetDataSourceRowIndex(e.RowHandle)];
				if (Path.GetExtension(videoInfo.Parent.OriginalPath).ToLower().Contains("mp4"))
					e.RepositoryItem = repositoryItemButtonEditVideoMp4;
				else
					e.RepositoryItem = repositoryItemButtonEditVideoWmv;
			}
		}

		private void gridViewVideo_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
		{
			var vi = gridViewVideo.GetViewInfo() as GridViewInfo;
			var ri = vi.RowsInfo.GetInfoByHandle(e.RowHandle) as GridDataRowInfo;
			if (ri == null)
				return;
			e.RepositoryItem.Appearance.ForeColor = ri.Cells[e.Column].Appearance.ForeColor;
			e.RepositoryItem.Appearance.BackColor = ri.Cells[e.Column].Appearance.BackColor;
		}

		private void buttonXSelectAll_Click(object sender, EventArgs e)
		{
			foreach (VideoInfo videoInfo in _videoFiles)
				videoInfo.Selected = true;
			gridViewVideo.RefreshData();
		}

		private void buttonXClearAll_Click(object sender, EventArgs e)
		{
			foreach (VideoInfo videoInfo in _videoFiles)
				videoInfo.Selected = false;
			gridViewVideo.RefreshData();
		}

		private void repositoryItemButtonEditVideoWmv_EditValueChanged(object sender, EventArgs e)
		{
			gridViewVideo.CloseEditor();
		}
		#endregion

		#region Site Tab
		private void pbIE_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(ParentDecorator.Library.IPadManager.Website))
			{
				try
				{
					var process = new Process();
					process.StartInfo.FileName = "iexplore.exe";
					process.StartInfo.Arguments = ParentDecorator.Library.IPadManager.Website;
					process.Start();
				}
				catch
				{
					AppManager.Instance.ShowWarning("Couldn't open the website");
				}
			}
			else
				AppManager.Instance.ShowWarning("Website is no set");
		}

		private void pbChrome_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(ParentDecorator.Library.IPadManager.Website))
			{
				try
				{
					var process = new Process();
					process.StartInfo.FileName = "chrome.exe";
					process.StartInfo.Arguments = ParentDecorator.Library.IPadManager.Website;
					process.Start();
				}
				catch
				{
					AppManager.Instance.ShowWarning("Couldn't open the website");
				}
			}
			else
				AppManager.Instance.ShowWarning("Website is no set");
		}

		private void pbFirefox_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(ParentDecorator.Library.IPadManager.Website))
			{
				try
				{
					var process = new Process();
					process.StartInfo.FileName = "firefox.exe";
					process.StartInfo.Arguments = ParentDecorator.Library.IPadManager.Website;
					process.Start();
				}
				catch
				{
					AppManager.Instance.ShowWarning("Couldn't open the website");
				}
			}
			else
				AppManager.Instance.ShowWarning("Website is no set");
		}

		private void pbSafari_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(ParentDecorator.Library.IPadManager.Website))
			{
				try
				{
					var process = new Process();
					process.StartInfo.FileName = "safari.exe";
					process.StartInfo.Arguments = ParentDecorator.Library.IPadManager.Website;
					process.Start();
				}
				catch
				{
					AppManager.Instance.ShowWarning("Couldn't open the website");
				}
			}
			else
				AppManager.Instance.ShowWarning("Website is no set");
		}

		private void pbOpera_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(ParentDecorator.Library.IPadManager.Website))
			{
				try
				{
					var process = new Process();
					process.StartInfo.FileName = "opera.exe";
					process.StartInfo.Arguments = ParentDecorator.Library.IPadManager.Website;
					process.Start();
				}
				catch
				{
					AppManager.Instance.ShowWarning("Couldn't open the website");
				}
			}
			else
				AppManager.Instance.ShowWarning("Website is no set");
		}
		#endregion

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion

		public LibraryDecorator ParentDecorator { get; private set; }
	}
}