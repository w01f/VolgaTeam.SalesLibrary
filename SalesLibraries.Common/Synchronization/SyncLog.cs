using System;
using System.IO;
using System.Text;

namespace SalesLibraries.Common.Synchronization
{
	public class SyncLog
	{
		private readonly string _name;

		private int _filesCreated;
		private int _filesUpdated;
		private int _filesDeleted;
		private int _foldersCreated;
		private int _foldersDeleted;

		private readonly StringBuilder _logRecords = new StringBuilder();

		public EventHandler<SynchronizedEventArgs> OnFileSynchronized { get; private set; }
		public EventHandler<SynchronizedEventArgs> OnFolderSynchronized { get; private set; }

		public SyncLog(string name)
		{
			_name = name;
		}

		private void Reset()
		{
			_logRecords.Clear();

			_filesCreated = 0;
			_filesUpdated = 0;
			_filesDeleted = 0;
			_foldersCreated = 0;
			_foldersDeleted = 0;

			OnFileSynchronized = (o, e) =>
			{
				switch (e.Operation)
				{
					case SynchronizationOperation.Add:
						_logRecords.AppendLine(string.Format("File created: {0}", e.DestinationFilePath));
						_filesCreated++;
						break;
					case SynchronizationOperation.Update:
						_logRecords.AppendLine(string.Format("File updated: {0}", e.DestinationFilePath));
						_filesUpdated++;
						break;
					case SynchronizationOperation.Delete:
						_logRecords.AppendLine(string.Format("File deleted: {0}", e.DestinationFilePath));
						_filesDeleted++;
						break;
				}
			};
			OnFolderSynchronized = (o, e) =>
			{
				switch (e.Operation)
				{
					case SynchronizationOperation.Add:
						_logRecords.AppendLine(string.Format("Folder created: {0}", e.DestinationFilePath));
						_foldersCreated++;
						break;
					case SynchronizationOperation.Delete:
						_logRecords.AppendLine(string.Format("Folder deleted: {0}", e.DestinationFilePath));
						_foldersDeleted++;
						break;
				}
			};
		}

		public void StartLogging()
		{
			Reset();
			_logRecords.AppendLine(string.Format("Sync started: {0}", DateTime.Now.ToString("MM/dd/yy h:mm tt")));
		}

		public void AddRecord(string record)
		{
			_logRecords.AppendLine(record);
		}

		public void FinishLoging()
		{
			_logRecords.AppendLine(string.Format("Sync completed: {0}", DateTime.Now.ToString("MM/dd/yy h:mm tt")));

			AppendSummary();
		}

		public void AbortLoging()
		{
			_logRecords.AppendLine(string.Format("Sync aborted: {0}", DateTime.Now.ToString("MM/dd/yy h:mm tt")));

			AppendSummary();
		}

		private void AppendSummary()
		{
			_logRecords.AppendLine(string.Format("Total files created: {0}", _filesCreated.ToString("#,##0")));
			_logRecords.AppendLine(string.Format("Total files updated: {0}", _filesUpdated.ToString("#,##0")));
			_logRecords.AppendLine(string.Format("Total files deleted: {0}", _filesDeleted.ToString("#,##0")));
			_logRecords.AppendLine(string.Format("Total folders created: {0}", _foldersCreated.ToString("#,##0")));
			_logRecords.AppendLine(string.Format("Total folders deleted: {0}", _foldersDeleted.ToString("#,##0")));
		}

		public string Save(string path)
		{
			var filePath = Path.Combine(path, String.Format("{0} at {1}.txt", _name, DateTime.Now.ToString("MM-dd-yy h-mm tt")));
			File.WriteAllText(filePath, _logRecords.ToString());
			return filePath;
		}
	}
}
