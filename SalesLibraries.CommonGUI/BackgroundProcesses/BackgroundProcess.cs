using System;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	class BackgroundProcess
	{
		public string Title { get; set; }
		public bool ShowProgress { get; set; }
		public Action Process { get; set; }
		public Action AfterComplete { get; set; }
	}
}
