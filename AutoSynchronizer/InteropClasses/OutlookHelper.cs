using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Outlook;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.InteropClasses;

namespace AutoSynchronizer.InteropClasses
{
	public class OutlookHelper
	{
		private readonly OvernightsCalendar _calendar;
		private Application _outlookObject;

		public OutlookHelper(OvernightsCalendar calendar)
		{
			_calendar = calendar;
		}

		public bool Connect()
		{
			try
			{
				_outlookObject = new Application();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void Disconnect()
		{
			AppManager.Instance.ReleaseComObject(_outlookObject);
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}
	}
}