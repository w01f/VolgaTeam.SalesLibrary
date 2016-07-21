using System;
using System.IO;
using System.Linq;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Synchronization
{
	public sealed class SynchronizationHelper
	{
		private readonly object _lockObject = new object();

		private SynchronizationResult _abortResult = SynchronizationResult.Completed;

		public bool Completed { get; private set; }
		public event EventHandler<SynchronizingEventArgs> FileSynchronizing;
		public event EventHandler<SynchronizedEventArgs> FileSynchronized;

		public event EventHandler<SynchronizingEventArgs> FolderSynchronizing;
		public event EventHandler<SynchronizedEventArgs> FolderSynchronized;

		public event EventHandler<SynchronizationExceptionEventArgs> Error;
		public event EventHandler<SynchronizationCompletingEventArgs> SynchronizationCompleting;

		/// <summary>Synchronizes the files / sub-directories from a source folder to a destination folder. </summary>
		public SynchronizationResult SynchronizeFolder(
			SynchronizationOptions options)
		{
			try
			{
				Reset();

				if (options == null)
				{
					throw new ArgumentNullException("options");
				}

				var result = SynchronizeFolderInternal(options);

				if (SynchronizationCompleting == null)
					return result;

				SynchronizationCompleting(
					this,
					new SynchronizationCompletingEventArgs(result));

				return result;
			}
			catch (Exception ex)
			{
				if (SynchronizationCompleting == null)
					return SynchronizationResult.AbortedDueToError;

				SynchronizationCompleting(
					this,
					new SynchronizationCompletingEventArgs(SynchronizationResult.AbortedDueToError, ex));

				return SynchronizationResult.AbortedDueToError;
			}
			finally
			{
				Completed = true;
			}
		}

		public void Abort(SynchronizationResult result)
		{
			lock (_lockObject)
			{
				if (_abortResult != SynchronizationResult.Completed)
					return;
				_abortResult = result;
			}
		}

		private void Reset()
		{
			_abortResult = SynchronizationResult.Completed;
			Completed = false;
		}

		private SynchronizationResult SynchronizeFolderInternal(
			SynchronizationOptions options)
		{
			if (ShouldAbort())
			{
				return _abortResult;
			}

			var srcFiles = options.SourceDirectory.GetFiles();
			var dstFiles = options.DestinationDirectory.GetFiles();

			var srcDirs = options.SourceDirectory.GetDirectories();
			var dstDirs = options.DestinationDirectory.GetDirectories();

			var srcTaggedFiles = srcFiles
				.Where(fileInfo => options.FilterList == null || options.FilterList.IsMatchFile(fileInfo.FullName))
				.ToDictionary(fi => fi.Name);
			var srcTaggedDirs = srcDirs
				.Where(directoryInfo => options.FilterList == null || options.FilterList.IsMatchDirectory(directoryInfo.FullName))
				.ToDictionary(di => di.Name);

			var dstTaggedFiles = dstFiles.ToDictionary(fi => fi.Name);
			var dstTaggedDirs = dstDirs.ToDictionary(di => di.Name);

			foreach (var srcFileInfo in srcTaggedFiles.Values)
			{
				var toCopy = false;
				var operation = SynchronizationOperation.None;

				if (dstTaggedFiles.ContainsKey(srcFileInfo.Name))
				{
					var dstFileInfo = dstTaggedFiles[srcFileInfo.Name];
					if (srcFileInfo.LastWriteTimeUtc > dstFileInfo.LastWriteTimeUtc)
					{
						toCopy = true;
						operation = SynchronizationOperation.Update;
					}
				}
				else
				{
					toCopy = true;
					operation = SynchronizationOperation.Add;
				}

				if (ShouldAbort())
				{
					return _abortResult;
				}

				if (!toCopy)
					continue;

				var destinationFileName = Path.Combine(options.DestinationDirectory.FullName, srcFileInfo.Name);
				var args = new SynchronizingEventArgs(
					srcFileInfo.FullName,
					destinationFileName,
					operation);

				FileSynchronizing?.Invoke(this, args);

				if (args.Cancel)
					continue;

				try
				{
					srcFileInfo.CopyTo(destinationFileName, true);

					FileSynchronized?.Invoke(this, new SynchronizedEventArgs(args));
				}
				catch (Exception ex)
				{
					Error?.Invoke(this, new SynchronizationExceptionEventArgs(args, ex));
				}
			}

			if (options.DeleteExtraFilesInDestination)
			{
				foreach (var dstFileInfo in dstFiles)
				{
					if (ShouldAbort())
						return _abortResult;

					if (srcTaggedFiles.ContainsKey(dstFileInfo.Name))
						continue;

					var args = new SynchronizingEventArgs(
						dstFileInfo.FullName,
						dstFileInfo.FullName,
						SynchronizationOperation.Delete);

					FileSynchronizing?.Invoke(this, args);

					if (args.Cancel)
						continue;

					try
					{
						dstFileInfo.Delete();
						FileSynchronized?.Invoke(this, new SynchronizedEventArgs(args));
					}
					catch (Exception ex)
					{
						Error?.Invoke(this, new SynchronizationExceptionEventArgs(args, ex));
					}
				}
			}

			foreach (var srcSubDir in srcTaggedDirs.Values)
			{
				DirectoryInfo dstSubDir;
				SynchronizationOperation operation;
				if (dstTaggedDirs.ContainsKey(srcSubDir.Name))
				{
					dstSubDir = dstTaggedDirs[srcSubDir.Name];
					operation = SynchronizationOperation.Update;
				}
				else
				{
					dstSubDir = CreateFolder(Path.Combine(options.DestinationDirectory.FullName, srcSubDir.Name));
					operation = SynchronizationOperation.Add;
				}

				if (ShouldAbort())
				{
					return _abortResult;
				}

				var args = new SynchronizingEventArgs(
					srcSubDir.FullName,
					dstSubDir.FullName,
					operation);

				FolderSynchronizing?.Invoke(this, args);

				if (args.Cancel) continue;

				var childResult = SynchronizeFolderInternal(
					SynchronizationOptions.CopyForChildFolder(options, srcSubDir, dstSubDir));

				if (childResult != SynchronizationResult.Completed)
					return childResult;
			}

			// NOTE: this logic is not correct. We will end up deleting even files
			// whose extension is not present in the "extensions" parameter (if not null).
			if (options.DeleteExtraFilesInDestination)
			{
				foreach (var dstSubDir in dstDirs)
				{
					if (ShouldAbort())
						return _abortResult;

					if (!srcTaggedDirs.ContainsKey(dstSubDir.Name))
					{
						var args = new SynchronizingEventArgs(
							dstSubDir.FullName,
							dstSubDir.FullName,
							SynchronizationOperation.Delete);
						FolderSynchronizing?.Invoke(this, args);

						if (args.Cancel) continue;

						DeleteFolder(dstSubDir);
					}
				}
			}

			return SynchronizationResult.Completed;
		}

		private bool ShouldAbort()
		{
			return (_abortResult != SynchronizationResult.Completed);
		}

		public DirectoryInfo CreateFolder(string path)
		{
			var dirInfo = new DirectoryInfo(path);
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				FolderSynchronized?.Invoke(this, new SynchronizedEventArgs(path, path, SynchronizationOperation.Add));
			}
			return dirInfo;
		}

		public void DeleteFolder(DirectoryInfo folder)
		{
			Utils.DeleteFolder(folder);
			FolderSynchronized?.Invoke(this, new SynchronizedEventArgs(folder.FullName, folder.FullName, SynchronizationOperation.Delete));
		}
	}
}