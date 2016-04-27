using System.Collections.Generic;

namespace SalesLibraries.Common.Objects.PowerPoint
{
	class SlideFormat
	{
		public SlideFormatEnum Id { get; set; }
		public SlideSize DefaultSize { get; set; }
		public List<SlideSize> SupportedSizes { get; private set; }

		public SlideFormat()
		{
			SupportedSizes = new List<SlideSize>();
		}
	}
}
