using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.CoreObjects.InteropClasses;

namespace SalesDepot.CoreObjects.ToolClasses
{
	public class SyncManager
	{
		public event EventHandler<SyncEventArgs> FileCreated;
		public event EventHandler<SyncEventArgs> FileUpdated;
		public event EventHandler<SyncEventArgs> FileDeleted;
		public event EventHandler<SyncEventArgs> FileDeclined;

		public event EventHandler<SyncEventArgs> FolderCreated;
		public event EventHandler<SyncEventArgs> FolderDeleted;

		public void SynchronizeFolders(DirectoryInfo a, DirectoryInfo b, HashSet<string> filesWhiteList, bool includeSubFolders = true)
		{
			var aFiles = a.GetFiles().Where(x => filesWhiteList.Contains(x.FullName) || filesWhiteList.Count == 0).ToArray();
			var tempDirs = a.GetDirectories();
			var aDirs = tempDirs.Where(x => filesWhiteList.Any(y => Path.GetDirectoryName(y).Contains(x.FullName)) || filesWhiteList.Count == 0).ToDictionary(di => di.Name);
			Application.DoEvents();

			var bFiles = b.GetFiles();
			tempDirs = b.GetDirectories();
			var bDirs = tempDirs.ToDictionary(di => di.Name);
			Application.DoEvents();

			foreach (var afi in aFiles)
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					var destinationPath = Path.Combine(b.FullName, afi.Name);
					if (destinationPath.Length < WinAPIHelper.MAX_PATH)
					{
						var bfi = bFiles.FirstOrDefault(x => x.Name.Equals(afi.Name));
						if (bfi != null)
						{
							if (afi.LastWriteTime > bfi.LastWriteTime)
							{
								afi.CopyTo(destinationPath, true);
								if (FileUpdated != null)
									FileUpdated(this, new SyncEventArgs(afi.FullName, bfi.FullName));
							}
						}
						else
						{
							bfi = afi.CopyTo(destinationPath, true);
							if (FileCreated != null)
								FileCreated(this, new SyncEventArgs(afi.FullName, bfi.FullName));
						}
					}
					else if (FileDeclined != null)
						FileDeclined(this, new SyncEventArgs(afi.FullName, destinationPath));
					Application.DoEvents();
				}
				else
					break;
			}

			foreach (var bfi in bFiles)
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					if ((filesWhiteList.Contains(Path.Combine(a.FullName, bfi.Name)) || filesWhiteList.Count <= 0) && (aFiles.Select(x => x.Name).Contains(bfi.Name) || filesWhiteList.Count != 0)) continue;
					bfi.Attributes = FileAttributes.Normal;
					bfi.Delete();
					if (FileDeleted != null)
						FileDeleted(this, new SyncEventArgs("empty", bfi.FullName));
					Application.DoEvents();
				}
				else
					break;
			}

			if (!includeSubFolders) return;
			foreach (var adi in aDirs.Values)
			{
				if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
				{
					if (bDirs.ContainsKey(adi.Name))
					{
						var bdi = bDirs[adi.Name];
						SynchronizeFolders(adi, bdi, filesWhiteList);
					}
					else
					{
						var bdi = b.CreateSubdirectory(adi.Name);
						SynchronizeFolders(adi, bdi, filesWhiteList);
						if (FolderCreated != null)
							FolderCreated(this, new SyncEventArgs(adi.FullName, bdi.FullName));
					}
					Application.DoEvents();
				}
				else
					break;
			}

			if ((!Globals.ThreadActive || Globals.ThreadAborted) && Globals.ThreadActive) return;
			foreach (var bdi in bDirs.Values)
			{
				if (aDirs.ContainsKey(bdi.Name)) continue;
				DeleteFolder(bdi);
				if (FolderDeleted != null)
					FolderDeleted(this, new SyncEventArgs("empty", bdi.FullName));
				Application.DoEvents();
			}
		}

		public static void MakeFolderAvailable(DirectoryInfo folder)
		{
			try
			{
				foreach (var subFolder in folder.GetDirectories())
					MakeFolderAvailable(subFolder);
				foreach (FileInfo file in folder.GetFiles())
					if (File.Exists(file.FullName))
						File.SetAttributes(file.FullName, FileAttributes.Normal);
			}
			catch {}
		}

		public static void DeleteFolder(DirectoryInfo folder, string filter = "")
		{
			try
			{
				if (!folder.Exists) return;
				MakeFolderAvailable(folder);
				foreach (var subFolder in folder.GetDirectories())
					DeleteFolder(subFolder, filter);
				foreach (var file in folder.GetFiles())
				{
					try
					{
						if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
							File.Delete(file.FullName);
					}
					catch
					{
						try
						{
							Thread.Sleep(100);
							if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
								File.Delete(file.FullName);
						}
						catch {}
					}
				}
				try
				{
					if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
						Directory.Delete(folder.FullName, false);
				}
				catch
				{
					try
					{
						Thread.Sleep(100);
						if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
							Directory.Delete(folder.FullName, false);
					}
					catch {}
				}
			}
			catch {}
		}
	}

	public class SyncEventArgs : EventArgs
	{
		public SyncEventArgs(string source, string destination)
		{
			Source = source;
			Destination = destination;
		}

		public string Source { get; private set; }
		public string Destination { get; private set; }
	}
}