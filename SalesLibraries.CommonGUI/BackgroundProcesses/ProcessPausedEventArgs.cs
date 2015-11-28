using System;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public class ProcessPausedEventArgs : EventArgs
	{
		public FormProgressBase FormProgress { get; set; }
	}
}
