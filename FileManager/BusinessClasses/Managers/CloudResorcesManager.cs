using System;
using System.IO;
using System.Net;
using SalesDepot.CoreObjects.ToolClasses;
using SharpCompress.Common;
using SharpCompress.Reader;

namespace FileManager.BusinessClasses
{
	class CloudResorcesManager
	{
		private static readonly CloudResorcesManager _instance = new CloudResorcesManager();

		private const string ResourcesArchiveName = "fm_resources.rar";

		public static CloudResorcesManager Instance
		{
			get { return _instance; }
		}

		private string ResourcesFileUrl
		{
			get { return String.Format("{0}/{1}", ServiceConnector.Instance.ResorcesUrl, ResourcesArchiveName); }
		}

		private string ResourcesFileLocalPath
		{
			get { return Path.Combine(SettingsManager.Instance.CloudResorcesPath, ResourcesArchiveName); }
		}

		private string ResourcesFileTempPath
		{
			get { return Path.Combine(SettingsManager.Instance.TempPath, ResourcesArchiveName); }
		}

		public void LoadResorces()
		{
			if (!NeedToUpdate()) return;
			if (!DownloadResources()) return;
			ExractResources();
		}

		private bool NeedToUpdate()
		{
			try
			{
				if (!File.Exists(ResourcesFileLocalPath)) return true;

				var localFileDate = File.GetLastWriteTime(ResourcesFileLocalPath);

				var req = WebRequest.Create(ResourcesFileUrl);
				req.Method = "HEAD";
				using (var response = req.GetResponse())
				{
					DateTime remoteFileDate;
					if (DateTime.TryParse(response.Headers.Get("Last-Modified"), out remoteFileDate))
						return remoteFileDate > localFileDate;
					return true;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		private bool DownloadResources()
		{
			WebResponse response = null;
			try
			{
				var request = WebRequest.Create(ResourcesFileUrl);
				response = request.GetResponse();
				using (var remoteStream = response.GetResponseStream())
				{
					if (remoteStream == null)
						return false;
					using (var localStream = File.Create(ResourcesFileTempPath))
					{
						var buffer = new byte[1024];
						int bytesRead;
						do
						{
							bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
							localStream.Write(buffer, 0, bytesRead);
						}
						while (bytesRead > 0);
						localStream.Close();
					}
					remoteStream.Close();
				}
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
			finally
			{
				if (response != null) response.Close();
			}
		}

		private void ExractResources()
		{
			if (!File.Exists(ResourcesFileTempPath)) return;
			if (Directory.Exists(SettingsManager.Instance.CloudResorcesPath))
				SyncManager.DeleteFolder(new DirectoryInfo(SettingsManager.Instance.CloudResorcesPath));
			using (Stream stream = File.OpenRead(ResourcesFileTempPath))
			{
				var reader = ReaderFactory.Open(stream);
				while (reader.MoveToNextEntry())
				{
					if (!reader.Entry.IsDirectory)
					{
						Console.WriteLine(reader.Entry.FilePath);
						reader.WriteEntryToDirectory(SettingsManager.Instance.ApplicationLocalDataPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
					}
				}
			}
			if (Directory.Exists(SettingsManager.Instance.CloudResorcesPath))
				File.Copy(ResourcesFileTempPath, ResourcesFileLocalPath);
			try
			{
				File.Delete(ResourcesFileTempPath);
			}
			catch { }
		}
	}
}
