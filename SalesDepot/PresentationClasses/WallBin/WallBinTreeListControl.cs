﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SalesDepot.BusinessClasses;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.PresentationClasses.Viewers;

namespace SalesDepot.PresentationClasses.WallBin
{
	[ToolboxItem(false)]
	public partial class WallBinTreeListControl : UserControl
	{
		private readonly List<FolderLink> _rootFolders = new List<FolderLink>();
		private Library _parentLibrary;

		public WallBinTreeListControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laDoubleClick.Font = new Font(laDoubleClick.Font.FontFamily, laDoubleClick.Font.Size - 3, laDoubleClick.Font.Style);
				laEndDate.Font = new Font(laEndDate.Font.FontFamily, laEndDate.Font.Size - 2, laEndDate.Font.Style);
				laStartDate.Font = new Font(laStartDate.Font.FontFamily, laStartDate.Font.Size - 2, laStartDate.Font.Style);
				laTreeViewProgressLable.Font = new Font(laTreeViewProgressLable.Font.FontFamily, laTreeViewProgressLable.Font.Size - 3, laTreeViewProgressLable.Font.Style);
				checkEditDateRange.Font = new Font(checkEditDateRange.Font.FontFamily, checkEditDateRange.Font.Size - 2, checkEditDateRange.Font.Style);
				buttonXRefresh.Font = new Font(buttonXRefresh.Font.FontFamily, buttonXRefresh.Font.Size - 2, buttonXRefresh.Font.Style);
				simpleButtonSearch.Font = new Font(simpleButtonSearch.Font.FontFamily, simpleButtonSearch.Font.Size - 2, simpleButtonSearch.Font.Style);
			}
		}

		#region TreeView Data Methods
		private void Refresh_Click(object sender, EventArgs e)
		{
			treeListAllFiles.SuspendLayout();
			laTreeViewProgressLable.Text = "Loading Tree View...";
			pnTreeViewProgress.Visible = true;
			circularProgress.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			treeListAllFiles.Nodes.Clear();
			TreeListNode expandNode = treeListAllFiles.AppendNode(new object[] { "Expand All" }, null);
			expandNode.StateImageIndex = 0;

			var thread = new Thread(delegate()
			{
				FormMain.Instance.Invoke((MethodInvoker)delegate
				{
					foreach (FolderLink rootFolder in _rootFolders)
					{
						TreeListNode rootNode = treeListAllFiles.AppendNode(new object[] { rootFolder.Folder.Name }, null, rootFolder);
						rootNode.StateImageIndex = 0;
						FillNode(rootNode, false);
						Application.DoEvents();
					}
				});
			});
			thread.Start();

			while (thread.IsAlive)
				Application.DoEvents();

			xtraTabControlFiles.Enabled = true;
			circularProgress.IsRunning = false;
			pnTreeViewProgress.Visible = false;
			treeListAllFiles.ResumeLayout();
			treeListAllFiles.Enabled = true;
		}

		private void treeListAllFiles_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var treeList = sender as TreeList;
				if (treeList != null)
				{
					var hitPoint = new Point(e.X, e.Y);
					TreeListHitInfo hitInfo = treeListAllFiles.CalcHitInfo(hitPoint);
					if (hitInfo.Node != null)
						if (hitInfo.Node.Tag != null)
							if (hitInfo.Node.Tag.GetType() == typeof(FileLink))
							{
								treeListAllFiles.Selection.Clear();
								hitInfo.Node.Selected = true;
								contextMenuStrip.Show(treeListAllFiles, hitPoint);
							}
				}
			}
		}

		private void treeListAllFiles_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Clicks == 2)
			{
				var treeList = sender as TreeList;
				if (treeList != null)
				{
					var hitPoint = new Point(e.X, e.Y);
					TreeListHitInfo hitInfo = treeList.CalcHitInfo(hitPoint);
					if (hitInfo.Node != null)
					{
						treeList.SuspendLayout();
						if (hitInfo.Node.GetValue(treeListColumnName).Equals("Expand All"))
						{
							var nodesToDelete = new List<TreeListNode>();
							for (int i = 1; i < treeList.Nodes.Count; i++)
								nodesToDelete.Add(treeList.Nodes[i]);
							foreach (TreeListNode node in nodesToDelete)
								treeList.Nodes.Remove(node);

							laTreeViewProgressLable.Text = "Loading Tree View...";
							pnTreeViewProgress.Visible = true;
							circularProgress.IsRunning = true;
							treeList.Enabled = false;

							var thread = new Thread(delegate()
							{
								FormMain.Instance.Invoke((MethodInvoker)delegate
								{
									foreach (FolderLink rootFolder in _rootFolders)
									{
										TreeListNode rootNode = treeList.AppendNode(new object[] { rootFolder.Folder.Name }, null, rootFolder);
										rootNode.StateImageIndex = 0;
										FillNode(rootNode, true);
										Application.DoEvents();
									}
								});
							});
							thread.Start();

							while (thread.IsAlive)
								Application.DoEvents();
							pnTreeViewProgress.Visible = false;
							circularProgress.IsRunning = false;
							treeList.ResumeLayout();
							treeList.Enabled = true;
							hitInfo.Node.SetValue(treeListColumnName, "Collapse All");
							hitInfo.Node.SetValue(treeListColumnPath, "Collapse All");
							hitInfo.Node.StateImageIndex = 1;
						}
						else if (hitInfo.Node.GetValue(treeListColumnName).Equals("Collapse All"))
						{
							treeList.SuspendLayout();
							treeList.Visible = false;
							treeList.CollapseAll();
							treeList.Nodes[1].Expanded = true;
							hitInfo.Node.SetValue(treeListColumnName, "Expand All");
							hitInfo.Node.SetValue(treeListColumnPath, "Expand All");
							hitInfo.Node.StateImageIndex = 0;
							treeList.Visible = true;
							treeList.ResumeLayout();
						}
						else if (hitInfo.Node.Tag != null)
						{
							if (hitInfo.Node.Tag.GetType() == typeof(FolderLink))
								FillNode(hitInfo.Node, false);
							else if (hitInfo.Node.Tag.GetType() == typeof(FileLink))
								ViewItem(hitInfo.Node.Tag as FileLink);
						}
						treeList.ResumeLayout();
					}
				}
			}
		}

		private void tmiOpen_Click(object sender, EventArgs e)
		{
			FileLink fileLink = null;
			switch (xtraTabControlFiles.SelectedTabPageIndex)
			{
				case 0:
					if (treeListAllFiles.Selection.Count > 0)
						fileLink = treeListAllFiles.Selection[0].Tag as FileLink;
					break;
				case 1:
					if (treeListSearchFiles.Selection.Count > 0)
						fileLink = treeListSearchFiles.Selection[0].Tag as FileLink;
					break;
			}
			if (fileLink != null)
				ViewItem(fileLink);
		}

		private void FillNode(TreeListNode node, bool showSubItems)
		{
			TreeListNode childNode;
			if (node.Tag != null)
			{
				var folderLink = node.Tag as FolderLink;
				if (folderLink != null && node.Nodes.Count == 0)
				{
					try
					{
						var folders = new List<DirectoryInfo>();
						folders.AddRange(folderLink.Folder.GetDirectories());
						folders.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
						foreach (DirectoryInfo subFolder in folders)
						{
							if (!SettingsManager.Instance.HiddenObjects.Any(x => subFolder.FullName.ToLower().Contains(x.ToLower())))
							{
								var subFolderLink = new FolderLink();
								subFolderLink.RootId = folderLink.RootId;
								subFolderLink.Folder = subFolder;
								childNode = treeListAllFiles.AppendNode(new object[] { subFolder.Name }, node, subFolderLink);
								childNode.StateImageIndex = 0;

								Application.DoEvents();

								if (showSubItems)
									FillNode(childNode, showSubItems);
								if (showSubItems && childNode.Nodes.Count == 0)
									node.Nodes.Remove(childNode);
							}
							Application.DoEvents();
						}
					}
					catch {}
					try
					{
						var files = new List<FileInfo>();
						files.AddRange(folderLink.Folder.GetFiles());
						files.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
						foreach (FileInfo file in files)
						{
							if (!SettingsManager.Instance.HiddenObjects.Any(x => file.Name.ToLower().Contains(x.ToLower())) && file.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate)
							{
								var fileLink = new FileLink();
								fileLink.RootId = folderLink.RootId;
								fileLink.File = file;
								childNode = treeListAllFiles.AppendNode(new object[] { file.Name + " (" + file.LastWriteTime.ToString("MM/dd/yy hh:mm tt") + ")" }, node, fileLink);
								childNode.StateImageIndex = GetImageindex(file);
							}
							Application.DoEvents();
						}
					}
					catch {}
					node.StateImageIndex = 1;
					node.Expanded = true;
				}
			}
		}

		private int GetImageindex(FileInfo file)
		{
			switch (file.Extension.ToUpper())
			{
				case ".XLS":
				case ".XLSX":
				case ".XLT":
				case ".XLTX":
					return 3;
				case ".BMP":
				case ".JPG":
				case ".JPEG":
				case ".PNG":
				case ".GIF":
				case ".TIF":
				case ".TIFF":
				case ".ICO":
					return 4;
				case ".PDF":
					return 5;
				case ".PPT":
				case ".PPTX":
					return 6;
				case ".MPEG":
				case ".MPG":
				case ".WMV":
				case ".ASF":
				case ".AVI":
				case ".MOV":
				case ".MP4":
				case ".M4V":
					return 7;
				case ".URL":
					return 8;
				case ".DOC":
				case ".DOCX":
					return 9;
				default:
					return 2;
			}
		}

		private string GetFileFormat(FileInfo file)
		{
			switch (file.Extension.ToUpper())
			{
				case ".XLS":
				case ".XLSX":
				case ".XLT":
				case ".XLTX":
					return "Excel File";
				case ".BMP":
				case ".JPG":
				case ".JPEG":
				case ".PNG":
				case ".GIF":
				case ".TIF":
				case ".TIFF":
				case ".ICO":
					return "Image File";
				case ".PDF":
					return "Adobe PDF";
				case ".PPT":
				case ".PPTX":
					return "PowerPoint File";
				case ".MPEG":
				case ".MPG":
				case ".WMV":
				case ".ASF":
				case ".AVI":
				case ".MOV":
				case ".MP4":
				case ".M4V":
					return "Video Clip";
				case ".URL":
					return "Website Link";
				case ".DOC":
				case ".DOCX":
					return "Word Document";
				default:
					return "Sales Depot File";
			}
		}
		#endregion

		#region Kew Word Files Tree View
		private void SearchFileInFolder(FolderLink folderLink, string keyWord, List<FileLink> files)
		{
			try
			{
				foreach (DirectoryInfo subFolder in folderLink.Folder.GetDirectories())
					if (SettingsManager.Instance.HiddenObjects.Where(x => subFolder.FullName.ToLower().Contains(x.ToLower())).Count() == 0)
					{
						var subFolderLink = new FolderLink();
						subFolderLink.RootId = folderLink.RootId;
						subFolderLink.Folder = subFolder;
						SearchFileInFolder(subFolderLink, keyWord, files);
					}
			}
			catch {}
			try
			{
				foreach (FileInfo file in folderLink.Folder.GetFiles("*" + keyWord + "*.*"))
				{
					if (((file.LastWriteTime >= dateEditStartDate.DateTime && file.LastWriteTime <= dateEditEndDate.DateTime) || !checkEditDateRange.Checked) && SettingsManager.Instance.HiddenObjects.Where(x => file.FullName.ToLower().Contains(x.ToLower())).Count() == 0 && file.LastWriteTime > _parentLibrary.DirectAccessFileBottomDate)
					{
						var fileLink = new FileLink();
						fileLink.RootId = folderLink.RootId;
						fileLink.File = file;
						files.Add(fileLink);
					}
				}
			}
			catch {}
		}

		private void btSearch_Click(object sender, EventArgs e)
		{
			treeListSearchFiles.SuspendLayout();
			treeListSearchFiles.Nodes.Clear();
			laTreeViewProgressLable.Text = "Searching Files...";
			pnTreeViewProgress.Visible = true;
			circularProgress.IsRunning = true;
			xtraTabControlFiles.Enabled = false;

			var files = new List<FileLink>();
			var thread = new Thread(delegate()
			{
				foreach (FolderLink folder in _rootFolders)
					SearchFileInFolder(folder, textEditKeyWord.EditValue != null ? textEditKeyWord.EditValue.ToString() : string.Empty, files);
				if (files.Count > 0)
				{
					files.Sort((x, y) => x.File.Name.CompareTo(y.File.Name));
					FormMain.Instance.Invoke((MethodInvoker)delegate
					{
						foreach (FileLink file in files)
						{
							TreeListNode childNode = treeListSearchFiles.AppendNode(new object[] { file.File.Name + " (" + file.File.LastWriteTime.ToShortDateString() + " " + file.File.LastWriteTime.ToShortTimeString() + ")" }, null, file);
							childNode.StateImageIndex = GetImageindex(file.File);
							Application.DoEvents();
						}
					});
				}
			});
			thread.Start();

			while (thread.IsAlive)
				Application.DoEvents();

			pnTreeViewProgress.Visible = false;
			circularProgress.IsRunning = false;
			treeListSearchFiles.ResumeLayout();
			xtraTabControlFiles.Enabled = true;
			if (files.Count == 0)
				AppManager.Instance.ShowInfo("Files was not found");
		}

		private void edKeyWord_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				btSearch_Click(null, null);
		}

		private void ckDateRange_CheckedChanged(object sender, EventArgs e)
		{
			if (checkEditDateRange.Checked)
				pnKeyWord.Height = gbDateRange.Bottom + 4;
			else
				pnKeyWord.Height = gbDateRange.Top + 4;
		}
		#endregion

		#region Other GUI Events
		private void xtraTabControlFiles_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			switch (xtraTabControlFiles.SelectedTabPageIndex)
			{
				case 0:
					treeListSearchFiles.Selection.Clear();
					break;
				case 1:
					treeListAllFiles.Selection.Clear();
					simpleButtonSearch.Refresh();
					break;
			}
		}
		#endregion

		#region Common Methods
		public void Init(Library library)
		{
			_parentLibrary = library;

			xtraTabPageRegular.Text = _parentLibrary.Parent.Name;

			_rootFolders.Clear();
			_rootFolders.AddRange(_parentLibrary.ExtraFolders);
			_rootFolders.Sort((x, y) => (x as RootFolder).Order.CompareTo((y as RootFolder).Order));
			_rootFolders.Insert(0, _parentLibrary.RootFolder);

			Refresh_Click(null, null);
			ckDateRange_CheckedChanged(null, null);
		}

		private void ViewItem(FileLink file)
		{
			try
			{
				var link = new LibraryLink(new LibraryFolder(new LibraryPage(_parentLibrary)));
				link.OriginalPath = file.File.FullName;
				link.SetProperties();
				LinkManager.Instance.OpenLink(link);
			}
			catch {}
		}
		#endregion

		#region Previewer
		private IFileViewer _selectedFileViewer;

		private void UpdateViewAccordingFileType(LibraryLink file)
		{
			barButtonItemOpenLink.Enabled = false;
			barButtonItemSave.Enabled = false;
			barButtonItemEmailLink.Enabled = false;
			barButtonItemPrintLink.Enabled = false;
			if (file != null)
			{
				barButtonItemOpenLink.Enabled = true;
				switch (file.Type)
				{
					case FileTypes.Presentation:
						barButtonItemSave.Enabled = true;
						barButtonItemEmailLink.Enabled = true;
						barButtonItemPrintLink.Enabled = true;
						break;
					case FileTypes.Excel:
					case FileTypes.PDF:
					case FileTypes.Word:
						barButtonItemSave.Enabled = true;
						barButtonItemEmailLink.Enabled = true;
						barButtonItemPrintLink.Enabled = true;
						break;
					case FileTypes.Other:
						barButtonItemSave.Enabled = true;
						barButtonItemEmailLink.Enabled = true;
						barButtonItemPrintLink.Enabled = true;
						break;
					case FileTypes.MediaPlayerVideo:
					case FileTypes.QuickTimeVideo:
						barButtonItemEmailLink.Enabled = true;
						break;
					case FileTypes.Url:
						break;
				}
			}
		}

		private void treeList_SelectionChanged(object sender, EventArgs e)
		{
			if (_selectedFileViewer != null)
			{
				pnPreview.Controls.Clear();
				_selectedFileViewer.ReleaseResources();
				_selectedFileViewer = null;
			}
			var treeList = sender as TreeList;
			if (treeList != null)
			{
				TreeListNode node = treeList.Selection.Count > 0 ? treeList.Selection[0] : null;
				if (node != null && node.Tag != null)
				{
					var file = node.Tag as FileLink;
					if (file != null)
					{
						var libraryFile = new LibraryLink(new LibraryFolder(new LibraryPage(_parentLibrary)));
						libraryFile.OriginalPath = file.File.FullName;
						libraryFile.SetProperties();

						using (var form = new FormProgress())
						{
							form.laProgress.Text = "Loading preview...";
							form.TopMost = true;
							var thread = new Thread(delegate()
							{
								switch (libraryFile.Type)
								{
									case FileTypes.Excel:
										FormMain.Instance.Invoke((MethodInvoker)delegate
										{
											try
											{
												_selectedFileViewer = new ExcelViewer(libraryFile);
											}
											catch
											{
												_selectedFileViewer = new DefaultViewer(libraryFile);
											}
										});
										break;
									case FileTypes.Word:
										FormMain.Instance.Invoke((MethodInvoker)delegate
										{
											try
											{
												_selectedFileViewer = new WordViewer(libraryFile);
											}
											catch
											{
												_selectedFileViewer = new DefaultViewer(libraryFile);
											}
										});
										break;
									case FileTypes.PDF:
										FormMain.Instance.Invoke((MethodInvoker)delegate
										{
											try
											{
												_selectedFileViewer = new PDFViewer(libraryFile);
											}
											catch
											{
												_selectedFileViewer = new DefaultViewer(libraryFile);
											}
										});
										break;
									case FileTypes.MediaPlayerVideo:
										FormMain.Instance.Invoke((MethodInvoker)delegate
										{
											try
											{
												_selectedFileViewer = new VideoViewer(libraryFile);
											}
											catch
											{
												_selectedFileViewer = new DefaultViewer(libraryFile);
											}
										});
										break;
									case FileTypes.Url:
										FormMain.Instance.Invoke((MethodInvoker)delegate
										{
											try
											{
												_selectedFileViewer = new WebViewer(libraryFile);
											}
											catch
											{
												_selectedFileViewer = new DefaultViewer(libraryFile);
											}
										});
										break;
									default:
										FormMain.Instance.Invoke((MethodInvoker)delegate { _selectedFileViewer = new DefaultViewer(libraryFile); });
										break;
								}
							});

							form.Show();
							Application.DoEvents();

							thread.Start();

							while (thread.IsAlive)
								Application.DoEvents();

							FormMain.Instance.ribbonControl.Enabled = true;
							form.Close();
						}
					}
				}
			}
			if (_selectedFileViewer == null)
				_selectedFileViewer = new EmptyViewer(null);
			(_selectedFileViewer as Control).Visible = true;
			UpdateViewAccordingFileType(_selectedFileViewer.File);
			pnPreview.Controls.Add(_selectedFileViewer as Control);
		}

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

		private void barButtonItemAddSlide_ItemClick(object sender, ItemClickEventArgs e)
		{
			var viewer = _selectedFileViewer as PowerPointViewer;
			if (viewer != null)
				viewer.InsertSlide();
		}

		private void barButtonItemOpenQuickView_ItemClick(object sender, ItemClickEventArgs e)
		{
			var viewer = _selectedFileViewer as PowerPointViewer;
			if (viewer != null)
				viewer.OpenInQuickView();
		}
		#endregion

		#endregion
	}
}