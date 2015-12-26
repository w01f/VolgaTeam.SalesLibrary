using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraPrinting;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities
{
	public interface IGroupControl
	{
		string GroupName { get; }
		PrintableComponentLink GetPrintLink();
	}
}
