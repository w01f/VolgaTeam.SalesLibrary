using System;
using System.IO;
using System.Linq;
using System.Text;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	abstract class PreviewGenerationLogger
	{
		protected readonly BasePreviewContainer _previewContainer;
		protected readonly StringBuilder _log;
		protected string _logPath;

		private DateTime _starDateTime;

		protected PreviewGenerationLogger(BasePreviewContainer previewContainer)
		{
			_previewContainer = previewContainer;
			_log = new StringBuilder();
		}

		public void StartLogging()
		{
			_starDateTime = DateTime.Now;

			_logPath = Path.Combine(_previewContainer.ContainerPath, String.Format("log_{0:MMddyy_hhmmsstt}.txt", _starDateTime));

			AddSourceObjectInfo();

			SaveToFile();
		}

		public void FinishLogging()
		{
			var finishDate = DateTime.Now;
			_log.AppendLine(String.Format(@"FINISH - {0:hh\:mm\:ss tt}", finishDate));
			_log.AppendLine(String.Format(@"TIME SEQUENCE - {0:hh\:mm\:ss}", finishDate - _starDateTime));
			_log.AppendLine("SUCCESS");

			SaveToFile();
		}

		public void LogStage(string format)
		{
			var formatContentPath = Path.Combine(_previewContainer.ContainerPath, format);
			var files = Directory.GetFiles(formatContentPath);
			if (files.Any())
			{
				_log.AppendLine(String.Format(@"{0} - ({1}) {2:hh\:mm\:ss tt}", format.ToUpper(), files.Length, DateTime.Now));
				SaveToFile();
			}
			else
			{
				_log.AppendLine(String.Format(@"{0} - Error - {1:hh\:mm\:ss tt}", format.ToUpper(), DateTime.Now));
				SaveToFile();
				throw PreviewGenerationException.Create(_logPath);
			}
		}

		protected virtual void AddSourceObjectInfo()
		{
			_log.AppendLine(String.Format(@"START - {0:MM/dd/yy hh\:mm\:ss tt}", _starDateTime));
			_log.AppendLine(String.Format("WV CODE - {0}", _previewContainer.ExtId));
		}

		protected void SaveToFile()
		{
			if (Directory.Exists(_previewContainer.ContainerPath))
				File.WriteAllText(_logPath, _log.ToString());
		}
	}
}
