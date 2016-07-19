using System.Drawing;

namespace SalesLibraries.ServiceConnector.Models.Rest.Wallbin.Settings
{
	public class AutoWidget
	{
		public string Extension { get; set; }
		public bool Inverted { get; set; }
		public Image OriginalImage { get; set; }
		public Image DisplayedImage { get; set; }
	}
}
