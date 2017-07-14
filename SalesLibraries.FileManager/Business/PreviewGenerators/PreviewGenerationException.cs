using System;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	class PreviewGenerationException : Exception
	{
		public PreviewGenerationException(string message) : base(message) { }

		public string LogPath { get; private set; }

		public static PreviewGenerationException Create(string logPath)
		{
			var exception = new PreviewGenerationException("Error occured while preview generation")
			{
				LogPath = logPath
			};
			return exception;
		}
	}
}
