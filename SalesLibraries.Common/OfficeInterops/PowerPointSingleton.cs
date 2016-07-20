using Microsoft.Office.Core;

namespace SalesLibraries.Common.OfficeInterops
{
	public class PowerPointSingleton : PowerPointProcessor
	{
		private static readonly PowerPointSingleton _instance = new PowerPointSingleton();
		public static PowerPointSingleton Instance => _instance;
		private PowerPointSingleton() { }


		public override bool Connect(bool force = false)
		{
			var result = base.Connect(force);
			if (!result)
				return false;
			result = GetActivePresentation(force) != null;
			if (result)
				PowerPointObject.Visible = MsoTriState.msoCTrue;
			return result;
		}
	}
}
