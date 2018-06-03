using System.Collections.Generic;

namespace SalesLibraries.Business.Schema.Wallbin.Initialization.Patches
{
	public class PatchController
	{
		public List<IPatch> Patches { get; } = new List<IPatch>();

		public PatchController()
		{
			Patches.Add(new Patch11());
		}
	}
}
