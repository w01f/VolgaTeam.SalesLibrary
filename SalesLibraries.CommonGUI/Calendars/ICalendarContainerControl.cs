using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesLibraries.CommonGUI.BackgroundProcesses;

namespace SalesLibraries.CommonGUI.Calendars
{
	public interface ICalendarContainerControl
	{
		int CurrentFontSize { get; }
		Control ContainerControl { get; }
		List<CalendarPartControl> CalendarParts { get; }
		List<ButtonItem> CalendarToggles { get; }
		void InvokeInContainer(Delegate method);
		void RunProcessInBackground(string title, Action<CancellationToken, FormProgressCommon> process);
	}
}
