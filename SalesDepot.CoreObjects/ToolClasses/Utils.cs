using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using ICSharpCode.SharpZipLib.Zip;
using SalesDepot.CoreObjects.InteropClasses;

namespace SalesDepot.CoreObjects.ToolClasses
{
	public class Utils
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

		public static void CompressFiles(string[] filesPaths, string compressedFilePath)
		{
			using (var s = new ZipOutputStream(File.Create(compressedFilePath)))
			{
				s.SetLevel(9); // 0 - store only to 9 - means best compression

				var buffer = new byte[4096];

				foreach (string file in filesPaths)
				{
					// Using GetFileName makes the result compatible with XP
					// as the resulting path is not absolute.
					var entry = new ZipEntry(Path.GetFileName(file));

					// Setup the entry data as required.

					// Crc and size are handled by the library for seakable streams
					// so no need to do them here.

					// Could also use the last write time or similar for the file.
					entry.DateTime = DateTime.Now;
					s.PutNextEntry(entry);

					using (FileStream fs = File.OpenRead(file))
					{
						// Using a fixed size buffer here makes no noticeable difference for output
						// but keeps a lid on memory usage.
						int sourceBytes;
						do
						{
							sourceBytes = fs.Read(buffer, 0, buffer.Length);
							s.Write(buffer, 0, sourceBytes);
						} while (sourceBytes > 0);
					}
				}

				// Finish/Close arent needed strictly as the using statement does this automatically

				// Finish is important to ensure trailing information for a Zip file is appended.  Without this
				// the created file would be invalid.
				s.Finish();

				// Close is important to wrap things up and unlock the file.
				s.Close();
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


	}

	public static class Extensions
	{
		public static Point GetCenter(this Rectangle control)
		{
			return new Point(control.X + (control.Width / 2), control.Y + (control.Height / 2));
		}

		public static Point GetOffset(this Point point, int x, int y)
		{
			return new Point(point.X + x, point.Y + y);
		}

		public static Image Resize(this Image image, Size size)
		{
			var originalWidth = image.Width;
			var originalHeight = image.Height;
			var percentWidth = (float)size.Width / originalWidth;
			var percentHeight = (float)size.Height / originalHeight;
			var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
			var newWidth = (int)(originalWidth * percent);
			var newHeight = (int)(originalHeight * percent);
			Image newImage = new Bitmap(newWidth, newHeight);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
			}
			return newImage;
		}
	}
}