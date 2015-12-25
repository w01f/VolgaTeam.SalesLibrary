using System;

namespace SalesLibraries.Common.OfficeInterops
{
	public class WordHidden : WordProcessor, IDisposable
	{
		public void Dispose()
		{
			Disconnect();
		}
	}
}
