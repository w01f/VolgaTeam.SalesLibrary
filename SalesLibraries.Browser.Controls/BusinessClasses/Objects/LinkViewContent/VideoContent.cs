using System.Linq;
using SalesLibraries.Browser.Controls.BusinessClasses.Enums;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Objects.LinkViewContent
{
	class VideoContent : ViewContent
	{
		private string _mp4Url;

		public override LinkContentType ContentType => LinkContentType.Video;

		public override void Load(object[] data)
		{
			base.Load(data);
			_mp4Url = (from object item in (object[])data[3] select item.ToString()).FirstOrDefault();
		}

		public string GetMp4Url()
		{
			return _mp4Url;
		}
	}
}
