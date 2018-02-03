using System;
using System.Drawing;

namespace SalesLibraries.CommonGUI.Floater
{
	public class FloaterRequestedEventArgs : EventArgs
	{
		public Image Logo { get; set; }
		public Action AfterShow { get; set; }
		public Action AfterBack { get; set; }
	}
}
