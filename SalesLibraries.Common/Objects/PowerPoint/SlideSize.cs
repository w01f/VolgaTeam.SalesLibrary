namespace SalesLibraries.Common.Objects.PowerPoint
{
	public class SlideSize
	{
		public decimal Width { get; set; }
		public decimal Height { get; set; }

		public SlideOrientationEnum Orientation => Width > Height ? SlideOrientationEnum.Landscape : SlideOrientationEnum.Portrait;
	}
}
