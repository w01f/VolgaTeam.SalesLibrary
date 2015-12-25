using System;
using System.Threading;

namespace SalesLibraries.Common.OfficeInterops
{
	public class PowerPointHidden : PowerPointProcessor, IDisposable
	{
		public override bool Connect(bool forceNewObject = false)
		{
			return base.Connect(true);
		}

		public void Dispose()
		{
			Disconnect(true);
		}

		public bool DoTimeLimitedAction(Action action, int timeLimit = 120)
		{
			var actionDone = false;
			var actionIteroped = false;

			System.Threading.Tasks.Task.Run(() =>
			{
				Thread.Sleep(timeLimit * 1000);
				if (!actionDone)
				{
					Close();
					actionIteroped = true;
				}
			});

			try
			{
				action();
			}
			catch { }

			actionDone = true;

			return actionIteroped;
		}
	}
}
