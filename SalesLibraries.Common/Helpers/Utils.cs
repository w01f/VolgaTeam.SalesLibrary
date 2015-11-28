using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;

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
			catch { }
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
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(handle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
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
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(taskBarHandle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
		}

		public static string FontToString(Font font)
		{
			string str = font.Name + ", " + font.Size.ToString("#0");
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

		public static ImageFormat GetImageFormat(Bitmap bitmap)
		{
			var img = bitmap.RawFormat;
			if (img.Equals(ImageFormat.Jpeg))
				return ImageFormat.Jpeg;
			if (img.Equals(ImageFormat.Bmp))
				return ImageFormat.Bmp;
			if (img.Equals(ImageFormat.Png))
				return ImageFormat.Png;
			if (img.Equals(ImageFormat.Emf))
				return ImageFormat.Emf;
			if (img.Equals(ImageFormat.Exif))
				return ImageFormat.Exif;
			if (img.Equals(ImageFormat.Gif))
				return ImageFormat.Gif;
			if (img.Equals(ImageFormat.Icon))
				return ImageFormat.Icon;
			if (img.Equals(ImageFormat.MemoryBmp))
				return ImageFormat.MemoryBmp;
			if (img.Equals(ImageFormat.Tiff))
				return ImageFormat.Tiff;
			return ImageFormat.Wmf;
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
			catch { }
		}

		public static void DeleteFolder(string folderPath, string filter = "")
		{
			DeleteFolder(new DirectoryInfo(folderPath), filter);
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
						catch { }
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
					catch { }
				}
			}
			catch { }
		}

		public static void OpenFile(string filePath)
		{
			try
			{
				Process.Start(filePath);
			}
			catch (Exception) { }
		}
	}
}
