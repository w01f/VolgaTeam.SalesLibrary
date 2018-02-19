using System;
using System.IO;
using System.Text;

namespace SalesLibraries.Common.Synchronization
{
	public class SyncLog
	{
		protected readonly string _name;
		private readonly bool _isAutoSync;
		private readonly int _afterInactivityMinutes;

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

		public SyncLog(string name, bool isAutoSync, int afterInactivityMinutes)
		{
			_name = name;
			_isAutoSync = isAutoSync;
			_afterInactivityMinutes = afterInactivityMinutes;
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
						_logRecords.AppendLine(String.Format("File created: {0}", e.DestinationFilePath));
						_filesCreated++;
						break;
					case SynchronizationOperation.Update:
						_logRecords.AppendLine(String.Format("File updated: {0}", e.DestinationFilePath));
						_filesUpdated++;
						break;
					case SynchronizationOperation.Delete:
						_logRecords.AppendLine(String.Format("File deleted: {0}", e.DestinationFilePath));
						_filesDeleted++;
						break;
				}
			};
			OnFolderSynchronized = (o, e) =>
			{
				switch (e.Operation)
				{
					case SynchronizationOperation.Add:
						_logRecords.AppendLine(String.Format("Folder created: {0}", e.DestinationFilePath));
						_foldersCreated++;
						break;
					case SynchronizationOperation.Delete:
						_logRecords.AppendLine(String.Format("Folder deleted: {0}", e.DestinationFilePath));
						_foldersDeleted++;
						break;
				}
			};
		}

		public void StartLogging()
		{
			Reset();
			_logRecords.AppendLine(String.Format("Sync started: {0:MM/dd/yy h:mm tt} {1}",
				DateTime.Now,
				_isAutoSync ? String.Format("(after {0} minutes inactivity)", _afterInactivityMinutes) : String.Empty));
		}

		public void AddRecord(string record)
		{
			_logRecords.AppendLine(record);
		}

		public void FinishLoging()
		{
			_logRecords.AppendLine(String.Format("Sync completed: {0:MM/dd/yy h:mm tt}", DateTime.Now));

			AppendSummary();
		}

		public void AbortLoging()
		{
			_logRecords.AppendLine(String.Format("Sync aborted: {0:MM/dd/yy h:mm tt}", DateTime.Now));

			AppendSummary();
		}

		private void AppendSummary()
		{
			_logRecords.AppendLine(String.Format("Total files created: {0:#,##0}", _filesCreated));
			_logRecords.AppendLine(String.Format("Total files updated: {0:#,##0}", _filesUpdated));
			_logRecords.AppendLine(String.Format("Total files deleted: {0:#,##0}", _filesDeleted));
			_logRecords.AppendLine(String.Format("Total folders created: {0:#,##0}", _foldersCreated));
			_logRecords.AppendLine(String.Format("Total folders deleted: {0:#,##0}", _foldersDeleted));
		}

		public string Save(string path)
		{
			var filePath = Path.Combine(path, String.Format("{0} {1} at {2:MM-dd-yy h-mm tt}.txt",
				_name,
				_isAutoSync ? "AutoTimer" : "Manual",
				DateTime.Now));
			File.WriteAllText(filePath, _logRecords.ToString());
			return filePath;
		}
	}
}
