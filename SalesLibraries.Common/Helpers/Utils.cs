using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Common.Helpers
{
	public static class Utils
	{
		public static void ReleaseComObject(object o)
		{
			try
			{
				Marshal.ReleaseComObject(o);
			}
			catch
			{
			}
			finally
			{
				o = null;
			}
		}

		public static void CompressFiles(IEnumerable<string> filesPaths, string compressedFilePath)
		{
			using (var zip = new ZipFile())
			{
				zip.AddFiles(filesPaths, false, "");
				zip.Save(compressedFilePath);
			}
		}

		public static void ActivateForm(IntPtr handle, bool maximized, bool topMost)
		{
			WinAPIHelper.ShowWindow(handle, maximized ? WindowShowStyle.ShowMaximized : WindowShowStyle.ShowNormal);
			uint lpdwProcessId = 0;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(),
				WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(handle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(),
				WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			if (topMost)
				WinAPIHelper.MakeTopMost(handle);
			else
				WinAPIHelper.MakeNormal(handle);
		}

		public static void ActivateTaskbar()
		{
			var taskBarHandle = WinAPIHelper.FindWindow("Shell_traywnd", "");
			WinAPIHelper.ShowWindow(taskBarHandle, WindowShowStyle.Show);
			uint lpdwProcessId;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(),
				WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(taskBarHandle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(),
				WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
		}

		public static string FontToString(Font font)
		{
			var str = font.Name + ", " + font.Size.ToString("#0");
			if (font.Bold)
				str = str + ", Bold";
			if (font.Italic)
				str = str + ", Italic";
			if (font.Underline)
				str = str + ", Underline";
			if (font.Strikeout)
				str = str + ", Strikeout";
			return str;
		}

		public static void PutImageToClipboard(Image imageData)
		{
			if (imageData == null) return;
			using (var stream = new MemoryStream())
			{
				imageData.Save(stream, ImageFormat.Png);
				var data = new DataObject("PNG", stream);
				Clipboard.Clear();
				Clipboard.SetDataObject(data, true);
			}
		}

		public static Image GetImageFormClipboard()
		{
			if (!Clipboard.ContainsData("PNG")) return null;
			return Image.FromStream((Stream)Clipboard.GetData("PNG"));
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
			catch
			{
			}
		}

		public static void DeleteFolder(string folderPath)
		{
			DeleteFolder(new DirectoryInfo(folderPath));
		}

		public static void DeleteFolder(DirectoryInfo folder)
		{
			try
			{
				if (!folder.Exists) return;
				MakeFolderAvailable(folder);
				foreach (var subFolder in folder.GetDirectories())
					DeleteFolder(subFolder);
				foreach (var file in folder.GetFiles())
				{
					try
					{
						if (File.Exists(file.FullName))
							File.Delete(file.FullName);
					}
					catch
					{
						try
						{
							Thread.Sleep(100);
							if (File.Exists(file.FullName))
								File.Delete(file.FullName);
						}
						catch
						{
						}
					}
				}
				try
				{
					if (Directory.Exists(folder.FullName))
						Directory.Delete(folder.FullName, false);
				}
				catch
				{
					try
					{
						Thread.Sleep(100);
						if (Directory.Exists(folder.FullName))
							Directory.Delete(folder.FullName, false);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		public static void CleanFolder(string folderPath)
		{
			CleanFolder(new DirectoryInfo(folderPath));
		}

		public static void CleanFolder(DirectoryInfo folder)
		{
			try
			{
				if (!folder.Exists) return;
				MakeFolderAvailable(folder);
				foreach (var subFolder in folder.GetDirectories())
					DeleteFolder(subFolder);
				foreach (var file in folder.GetFiles())
				{
					try
					{
						if (File.Exists(file.FullName))
							File.Delete(file.FullName);
					}
					catch
					{
						try
						{
							Thread.Sleep(100);
							if (File.Exists(file.FullName))
								File.Delete(file.FullName);
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		public static void OpenFile(string filePath)
		{
			try
			{
				Process.Start(filePath);
			}
			catch (Exception)
			{
			}
		}

		public static void OpenFile(string[] filePaths)
		{
			foreach (var filePath in filePaths)
			{
				try
				{
					Process.Start(filePath);
					break;
				}
				catch (Exception)
				{
				}
			}

		}

		public static void CopyToFolder(string targetRootPath, IEnumerable<string> sourceItemsPaths)
		{
			foreach (var sourcePath in sourceItemsPaths)
			{
				if (Directory.Exists(sourcePath))
				{
					var targetDirectoryPath = Path.Combine(targetRootPath, Path.GetFileName(sourcePath));
					if (!Directory.Exists(targetDirectoryPath))
						Directory.CreateDirectory(targetDirectoryPath);
					CopyToFolder(targetDirectoryPath, Directory.GetDirectories(sourcePath));
					CopyToFolder(targetDirectoryPath, Directory.GetFiles(sourcePath));
				}
				else if (File.Exists(sourcePath))
					File.Copy(sourcePath, Path.Combine(targetRootPath, Path.GetFileName(sourcePath)), true);
			}
		}

		public static bool MoveFileToArchive(string targetFilePath)
		{
			var folderPath = Path.GetDirectoryName(targetFilePath);
			var archivePath = Path.Combine(folderPath, Constants.FilesDeletedFromFolderArchiveName);
			var archivedFilePath = Path.Combine(archivePath, Path.GetFileName(targetFilePath));
			if (!Directory.Exists(archivePath))
				Directory.CreateDirectory(archivePath);
			try
			{
				if (File.Exists(archivedFilePath))
					File.Delete(archivedFilePath);
				File.Move(targetFilePath, archivedFilePath);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static string FormatFileSize(long size)
		{
			if (size >= 524288000)
				return String.Format("{0} gb", (size * 0.0009765625 * 0.0009765625 * 0.0009765625).ToString("# ##0"));
			if (size < 524288000 && size >= 512000)
				return String.Format("{0} mb", (size * 0.0009765625 * 0.0009765625).ToString("# ##0"));
			if (size < 512000)
				return String.Format("{0} kb", (size * 0.0009765625).ToString("# ##0"));
			return String.Format("{0} b", size.ToString("# ##0"));
		}
	}
}
