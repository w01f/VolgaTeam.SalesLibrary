using Microsoft.Office.Core;

namespace SalesLibraries.Common.OfficeInterops
{
	public class PowerPointSingleton:PowerPointProcessor
	{
		private static readonly PowerPointSingleton _instance = new PowerPointSingleton();
		public static PowerPointSingleton Instance
		{
			get { return _instance; }
		}
		private PowerPointSingleton() { }


		public override bool Connect(bool force = true)
		{
			var result = base.Connect(force);
			if (!result) return false;
			PowerPointObject.Visible = MsoTriState.msoCTrue;
			GetActivePresentation();
			return true;
		}
	}
}
