using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using FileManager.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.ToolClasses
{
	public class ArchiveHelper
	{
		private static readonly ArchiveHelper _instance = new ArchiveHelper();

		private const string FtpLogin = "adsalesn";
		private const string FtpPassword = "M6&xQ^A!1d0b";

		public static ArchiveHelper Instance
		{
			get { return _instance; }
		}

		private ArchiveHelper() { }

		public void ArchiveFiles(IEnumerable<string> originalFiles)
		{
			var archiveRoot = AppModeManager.Instance.AppMode==AppModeEnum.Local?
				String.Format(@"{0}\file_manager\Archives", SettingsManager.Instance.SettingsRootPath):
				String.Format(@"{0}\Archives", SettingsManager.Instance.ApplicationLocalDataPath);
			if (!Directory.Exists(archiveRoot))
				Directory.CreateDirectory(archiveRoot);

			var archiveDateTime = DateTime.Now;
			var archiveName = archiveDateTime.ToString("MMddyy") + "-" + archiveDateTime.ToString("hhmmsstt");
			var archiveFolder = Path.Combine(archiveRoot, archiveName);
			if (!Directory.Exists(archiveFolder))
				Directory.CreateDirectory(archiveFolder);

			try
			{
				foreach (var file in originalFiles)
					File.Copy(file, Path.Combine(archiveFolder, Path.GetFileName(file)), true);
			}
			catch { }


			var ftpArchiveSettingspath = Path.Combine(SettingsManager.Instance.CloudResorcesPath, "ftp_upload.txt");
			var ftpArchivePath = String.Empty;
			if (File.Exists(ftpArchiveSettingspath))
				ftpArchivePath = File.ReadAllText(ftpArchiveSettingspath).Trim();

			if (String.IsNullOrEmpty(ftpArchivePath)) return;
			var archiveFileName = String.Format("{0}.zip", archiveName);
			var archiveFilePath = Path.Combine(SettingsManager.Instance.TempPath, archiveFileName);

			var logPath = Path.Combine(SettingsManager.Instance.LogRootPath, string.Format("FTP Log at {0}.txt", DateTime.Now.ToString("MM-dd-yy h-mm tt")));
			var ftpResult = String.Empty;
			try
			{
				Utils.CompressFiles(originalFiles, archiveFilePath);
				if (!File.Exists(archiveFilePath)) return;

				ftpArchivePath = ftpArchivePath.EndsWith("/") ? ftpArchivePath : ftpArchivePath + "/";
				ftpArchivePath += archiveFileName;
				var request = (FtpWebRequest)FtpWebRequest.Create(ftpArchivePath);
				request.KeepAlive = false;
				request.UsePassive = true;
				request.UseBinary = true;
				request.Proxy = null;
				request.Method = WebRequestMethods.Ftp.UploadFile;
				request.Proxy = null;
				request.Credentials = new NetworkCredential(FtpLogin, FtpPassword);

				var fileContents = File.ReadAllBytes(archiveFilePath);
				request.ContentLength = fileContents.Length;
				var requestStream = request.GetRequestStream();
				requestStream.Write(fileContents, 0, fileContents.Length);
				requestStream.Close();
				var response = (FtpWebResponse)request.GetResponse();
				response.Close();
				try
				{
					File.Delete(archiveFilePath);
				}
				catch { }
				ftpResult = String.Format("File {0} uploaded successfully", archiveFileName);
			}
			catch (Exception exception)
			{
				ftpResult = "File was not uploaded";
			}
			finally
			{
				using (var sw = new StreamWriter(logPath, false))
				{
					sw.Write(ftpResult);
					sw.Flush();
					sw.Close();
				}
			}
		}
	}
}
