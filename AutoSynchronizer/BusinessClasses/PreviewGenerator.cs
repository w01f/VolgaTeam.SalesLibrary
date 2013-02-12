using System.IO;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class VideoPreviewGenerator : IPreviewGenerator
	{
		#region IPreviewGenerator Members
		public IPreviewContainer Parent { get; private set; }

		public VideoPreviewGenerator(IPreviewContainer parent)
		{
			Parent = parent;
		}

		public void GeneratePreview() { }
		#endregion
	}
}
