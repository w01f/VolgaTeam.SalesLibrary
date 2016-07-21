using System;
using System.Windows.Forms;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public class FormProgressBase : Form
	{
		public virtual string Title { get; set; }
		public event EventHandler<EventArgs> ProcessAborted;

		protected void AbortProcess()
		{
			ProcessAborted?.Invoke(this, new EventArgs());
		}
	}
}
